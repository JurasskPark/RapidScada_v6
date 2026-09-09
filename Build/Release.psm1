#requires -Version 7.2
Set-StrictMode -Version Latest

$script:RepositoryRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))
$script:ProjectInfoCache = @{}
$script:SupportedRuntimes = @('win-x64', 'win-x86', 'linux-x64', 'anycpu')

function Resolve-ReleasePath {
    param([string]$BaseDirectory, [string]$RelativePath)
    if ([string]::IsNullOrWhiteSpace($RelativePath) -or [IO.Path]::IsPathRooted($RelativePath) -or
        $RelativePath -match '(^|[\\/])\.\.([\\/]|$)' -or $RelativePath -match '[:*?]') {
        throw "Expected a relative path without traversal: $RelativePath"
    }
    $base = [IO.Path]::GetFullPath($BaseDirectory).TrimEnd('\', '/') + [IO.Path]::DirectorySeparatorChar
    $path = [IO.Path]::GetFullPath((Join-Path $base $RelativePath))
    if (-not $path.StartsWith($base, [StringComparison]::OrdinalIgnoreCase)) {
        throw "Path leaves its intended directory: $RelativePath"
    }
    return $path
}

function Read-ReleaseManifest {
    param([string]$Path)
    $fullPath = [IO.Path]::GetFullPath($Path)
    $manifest = Get-Content -LiteralPath $fullPath -Raw -Encoding utf8 | ConvertFrom-Json -AsHashtable
    foreach ($key in @('schemaVersion', 'id', 'versionProject', 'authors', 'sourceUrl', 'forums', 'runtimes', 'components')) {
        if (-not $manifest.ContainsKey($key)) { throw "Missing $key in $fullPath" }
    }
    if ($manifest.schemaVersion -ne 1 -or $manifest.id -notmatch '^[A-Za-z][A-Za-z0-9]+$') {
        throw "Invalid manifest identity: $fullPath"
    }
    if ($manifest.runtimes.Count -eq 0 -or $manifest.components.Count -eq 0) {
        throw "Empty runtime or component list: $fullPath"
    }
    foreach ($runtime in $manifest.runtimes) {
        if ($runtime -notin $script:SupportedRuntimes) { throw "Unsupported runtime: $runtime" }
    }
    $manifest['_path'] = $fullPath
    $manifest['_directory'] = Split-Path -Parent $fullPath
    if (-not $manifest.ContainsKey('assets')) { $manifest.assets = @() }
    foreach ($component in $manifest.components) {
        foreach ($key in @('project', 'destinations', 'kind')) {
            if (-not $component.ContainsKey($key)) { throw "Missing component $key in $fullPath" }
        }
        if ($component.kind -notin @('library', 'app')) { throw "Unknown component kind in $fullPath" }
        $projectPath = Resolve-ReleasePath $manifest._directory $component.project
        if (-not (Test-Path -LiteralPath $projectPath -PathType Leaf)) { throw "Project not found: $projectPath" }
        foreach ($destination in $component.destinations) {
            $null = Resolve-ReleasePath $script:RepositoryRoot $destination
            if ($destination -notmatch '^(SCADA|App)(/|\\|$)') { throw "Invalid package destination: $destination" }
        }
    }
    foreach ($asset in $manifest.assets) {
        $source = Resolve-ReleasePath $manifest._directory $asset.source
        $null = Resolve-ReleasePath $script:RepositoryRoot $asset.destination
        if ($asset.destination -notmatch '^(SCADA|App)(/|\\|$)') { throw "Invalid asset destination: $($asset.destination)" }
        if (-not (Test-Path -LiteralPath $source)) { throw "Package asset not found: $source" }
    }
    $null = Resolve-ReleasePath $manifest._directory $manifest.versionProject
    foreach ($url in @($manifest.sourceUrl) + @($manifest.forums | ForEach-Object { $_.url })) {
        if (-not [Uri]::IsWellFormedUriString($url, [UriKind]::Absolute) -or ([Uri]$url).Scheme -ne 'https') {
            throw "Invalid release URL: $url"
        }
    }
    return $manifest
}

function Get-ReleaseCatalog {
    $catalog = foreach ($root in @('OpenDrivers', 'OpenExtensions', 'OpenModules', 'OpenPlugins')) {
        Get-ChildItem -LiteralPath (Join-Path $script:RepositoryRoot $root) -Filter 'release.json' -File -Recurse |
            Where-Object { $_.FullName -notmatch '[\\/](bin|obj|Release|Releases|artifacts)[\\/]' } |
            ForEach-Object { Read-ReleaseManifest $_.FullName }
    }
    $duplicates = @($catalog | Group-Object { $_.id } | Where-Object Count -gt 1)
    if ($duplicates.Count -gt 0) { throw 'Duplicate release IDs in the catalog.' }
    return @($catalog | Sort-Object { $_.id })
}

function Get-ReleaseProjectInfo {
    param([string]$ProjectPath, [string]$Configuration = 'Release')
    $cacheKey = $ProjectPath + '|' + $Configuration
    if (-not $script:ProjectInfoCache.ContainsKey($cacheKey)) {
        $output = & dotnet msbuild $ProjectPath -nologo "-p:Configuration=$Configuration" '-target:GetAssemblyVersion' '-getProperty:AssemblyName,AssemblyVersion,Version,TargetFramework'
        if ($LASTEXITCODE -ne 0) { throw "Cannot evaluate $ProjectPath" }
        $properties = ($output -join [Environment]::NewLine | ConvertFrom-Json -AsHashtable).Properties
        if ($properties.AssemblyVersion -notmatch '^\d+\.\d+\.\d+\.\d+$') {
            throw "Expected a four-part assembly version in $ProjectPath"
        }
        $script:ProjectInfoCache[$cacheKey] = $properties
    }
    return $script:ProjectInfoCache[$cacheKey]
}

function Get-DriverDisplayMetadata {
    param([string]$Path)
    $source = [IO.File]::ReadAllText($Path)
    $metadata = @{}
    foreach ($name in @('DriverCode', 'NameRu', 'NameEn', 'DescriptionRu', 'DescriptionEn')) {
        $pattern = '\bpublic\s+const\s+string\s+' + $name + '\s*=\s*("(?:\\.|[^"\\])*")\s*;'
        $matches = [regex]::Matches($source, $pattern)
        if ($matches.Count -ne 1) { throw "Expected one string constant $name in $Path" }
        $metadata[$name] = ConvertFrom-Json -InputObject $matches[0].Groups[1].Value
        if ([string]::IsNullOrWhiteSpace($metadata[$name])) { throw "Empty $name in $Path" }
    }
    return $metadata
}

function Get-ReleaseMetadata {
    param([hashtable]$Manifest, [string]$Configuration = 'Release', [datetime]$Date = [datetime]::Now)
    $projectPath = Resolve-ReleasePath $Manifest._directory $Manifest.versionProject
    $properties = Get-ReleaseProjectInfo $projectPath $Configuration
    if ($Manifest.ContainsKey('driverUtils')) {
        $display = Get-DriverDisplayMetadata (Resolve-ReleasePath $Manifest._directory $Manifest.driverUtils)
        if ($display.DriverCode -ne $Manifest.id) { throw "Driver code mismatch in $($Manifest._path)" }
    } else {
        $display = $Manifest.display
    }
    foreach ($name in @('NameRu', 'NameEn', 'DescriptionRu', 'DescriptionEn')) {
        if (-not $display.ContainsKey($name) -or [string]::IsNullOrWhiteSpace($display[$name])) {
            throw "Missing display metadata $name in $($Manifest._path)"
        }
    }
    return @{
        Id = $Manifest.id
        NameRu = $display.NameRu
        NameEn = $display.NameEn
        DescriptionRu = $display.DescriptionRu
        DescriptionEn = $display.DescriptionEn
        Version = $properties.AssemblyVersion
        DateRu = $Date.ToString('dd.MM.yyyy', [Globalization.CultureInfo]::InvariantCulture)
        DateEn = $Date.ToString('yyyy-MM-dd', [Globalization.CultureInfo]::InvariantCulture)
        AuthorsRu = $Manifest.authors.ru -join [Environment]::NewLine
        AuthorsEn = $Manifest.authors.en -join [Environment]::NewLine
        SourceUrl = $Manifest.sourceUrl
        ForumLinks = ($Manifest.forums | ForEach-Object { $_.label + [Environment]::NewLine + $_.url }) -join ([Environment]::NewLine + [Environment]::NewLine)
        Notes = if ($Manifest.ContainsKey('notes')) { $Manifest.notes -join [Environment]::NewLine } else { '' }
    }
}

function Expand-ReleaseReadme {
    param([hashtable]$Metadata, [string]$Runtime, [string]$TemplatePath = (Join-Path $PSScriptRoot 'readme.template.txt'))
    $values = $Metadata.Clone()
    $values['Runtime'] = $Runtime
    $template = [IO.File]::ReadAllText($TemplatePath)
    $result = [regex]::Replace($template, '\{\{([A-Za-z][A-Za-z0-9]*)\}\}', [Text.RegularExpressions.MatchEvaluator]{
        param($match)
        $name = $match.Groups[1].Value
        if (-not $values.ContainsKey($name)) { throw "Unknown README placeholder: $name" }
        return [string]$values[$name]
    })
    if ($result -match '\{\{|\}\}') { throw 'Unresolved README template marker.' }
    return ($result -replace '\r?\n', [Environment]::NewLine).TrimEnd() + [Environment]::NewLine
}

function Get-ReleaseComponentRuntime {
    param([hashtable]$Component, [hashtable]$ProjectInfo, [string]$Runtime)
    $windowsOnly = ($ProjectInfo.TargetFramework -like '*-windows*') -or
        ($Component.ContainsKey('windowsOnly') -and $Component.windowsOnly)
    if ($Runtime -eq 'linux-x64' -and $windowsOnly) {
        if ($Component.kind -eq 'app') { return $null }
        return 'win-x64'
    }
    return $Runtime
}

function Copy-ReleaseFile {
    param([string]$Source, [string]$Payload, [string]$RelativeDestination)
    $destination = Resolve-ReleasePath $Payload $RelativeDestination
    if (Test-Path -LiteralPath $destination) {
        if ((Get-FileHash -LiteralPath $Source).Hash -ne (Get-FileHash -LiteralPath $destination).Hash) {
            throw "Different files map to the same package entry: $RelativeDestination"
        }
        return
    }
    $null = New-Item -ItemType Directory -Path (Split-Path -Parent $destination) -Force
    Copy-Item -LiteralPath $Source -Destination $destination
}

function Copy-ReleaseComponent {
    param([hashtable]$Component, [hashtable]$ProjectInfo, [string]$PublishDirectory, [string]$Payload)
    $assemblyName = $ProjectInfo.AssemblyName
    $mainDll = Join-Path $PublishDirectory ($assemblyName + '.dll')
    if (-not (Test-Path -LiteralPath $mainDll)) { throw "Missing published assembly: $mainDll" }
    $expected = [Collections.Generic.List[string]]::new()
    foreach ($destinationRoot in $Component.destinations) {
        $expected.Add(($destinationRoot + '/' + $assemblyName + '.dll').Replace('\', '/'))
        foreach ($file in Get-ChildItem -LiteralPath $PublishDirectory -File -Recurse) {
            $relative = [IO.Path]::GetRelativePath($PublishDirectory, $file.FullName).Replace('\', '/')
            if ($Component.kind -eq 'app') {
                Copy-ReleaseFile $file.FullName $Payload ($destinationRoot + '/' + $relative)
                continue
            }
            if ($file.Extension -eq '.pdb' -or $file.Name -match '\.(deps|runtimeconfig|staticwebassets[^.]*)\.json$') { continue }
            if ($relative.StartsWith($assemblyName + '/', [StringComparison]::OrdinalIgnoreCase)) {
                $relative = $relative.Substring($assemblyName.Length + 1)
            }
            if ($relative -match '^(?i)lang/(.+\.xml)$') {
                if ($Component.ContainsKey('languageDestination')) {
                    Copy-ReleaseFile $file.FullName $Payload ($Component.languageDestination + '/' + $Matches[1])
                }
                continue
            }
            if ($relative -match '^(?i)wwwroot/') { continue } # Static web assets are explicit manifest entries.
            if ($file.Name -match '^(?i)Scada.*\.dll$' -or $file.Name -in @('AgentClient.dll', 'PlgMimic.Common.dll')) { continue }
            if ($relative -eq ($assemblyName + '.dll')) {
                Copy-ReleaseFile $file.FullName $Payload ($destinationRoot + '/' + $relative)
            } elseif ($file.Extension -in @('.dll', '.so', '.dylib', '.dat') -or $relative.StartsWith('runtimes/')) {
                Copy-ReleaseFile $file.FullName $Payload ($destinationRoot + '/' + $assemblyName + '/' + $relative)
            }
        }
    }
    return $expected.ToArray()
}

function Assert-ReleaseArchive {
    param([string]$ArchivePath, [string[]]$RequiredEntries, [string]$ExpectedReadme)
    $archive = [IO.Compression.ZipFile]::OpenRead($ArchivePath)
    try {
        $entries = @($archive.Entries | Where-Object { $_.Name })
        $names = @($entries | ForEach-Object { $_.FullName })
        foreach ($name in $names) {
            if ($name.Contains('\') -or $name.StartsWith('/') -or $name -match '(^|/)\.\.(/|$)') {
                throw "Unsafe ZIP entry: $name"
            }
        }
        foreach ($required in @('readme.txt') + $RequiredEntries) {
            if ($required -notin $names) { throw "Missing ZIP entry: $required" }
        }
        if (@($names | Sort-Object -Unique).Count -ne $names.Count) { throw 'Duplicate ZIP entries.' }
        $reader = [IO.StreamReader]::new($archive.GetEntry('readme.txt').Open(), [Text.Encoding]::UTF8)
        try { $readme = $reader.ReadToEnd() } finally { $reader.Dispose() }
        if ($readme -ne $ExpectedReadme) { throw 'The ZIP README differs from the rendered template.' }
        foreach ($entry in $entries) {
            if ($entry.FullName.StartsWith('SCADA/') -and
                ($entry.Name -match '^(?i)Scada.*\.dll$' -or $entry.Name -in @('AgentClient.dll', 'PlgMimic.Common.dll'))) {
                throw "Host assembly was included in the module package: $($entry.FullName)"
            }
        }
        return $entries.Count
    } finally { $archive.Dispose() }
}

function Remove-ReleaseStaging {
    param([string]$Directory)
    $stagingRoot = [IO.Path]::GetFullPath((Join-Path $script:RepositoryRoot 'artifacts/release-builds')).TrimEnd('\', '/') + [IO.Path]::DirectorySeparatorChar
    $fullPath = [IO.Path]::GetFullPath($Directory)
    if (-not $fullPath.StartsWith($stagingRoot, [StringComparison]::OrdinalIgnoreCase) -or
        -not (Test-Path -LiteralPath (Join-Path $fullPath '.release-staging'))) {
        throw "Refusing to remove a directory outside generated release staging: $Directory"
    }
    Remove-Item -LiteralPath $fullPath -Recurse -Force
}

function Get-ReleaseRecordDirectory {
    param([string]$OutputDirectory)
    $recordRoot = Join-Path $script:RepositoryRoot 'artifacts/release-results'
    $output = [IO.Path]::GetFullPath($OutputDirectory).TrimEnd('\', '/')
    $defaultOutput = Join-Path $script:RepositoryRoot 'Releases'
    if ($output.Equals($defaultOutput, [StringComparison]::OrdinalIgnoreCase)) { return $recordRoot }
    $bytes = [Text.Encoding]::UTF8.GetBytes($output.ToUpperInvariant())
    $key = [Convert]::ToHexString([Security.Cryptography.SHA256]::HashData($bytes)).Substring(0, 16)
    return Join-Path $recordRoot ('output-' + $key)
}

function Invoke-ReleasePackage {
    param(
        [hashtable]$Manifest, [string]$Runtime, [string]$OutputDirectory,
        [string]$Configuration = 'Release', [datetime]$Date = [datetime]::Now,
        [switch]$IncludeApp, [switch]$KeepStaging
    )
    if ($Runtime -notin $Manifest.runtimes) { throw "$($Manifest.id) does not support package runtime $Runtime" }
    $metadata = Get-ReleaseMetadata $Manifest $Configuration $Date
    $packageName = $Manifest.id + '_' + $metadata.Version + '_' + $Runtime
    $runId = $packageName + '_' + [Guid]::NewGuid().ToString('N')
    $staging = Join-Path $script:RepositoryRoot ('artifacts/release-builds/' + $runId)
    $payload = Join-Path $staging 'payload'
    $null = New-Item -ItemType Directory -Path $payload -Force
    [IO.File]::WriteAllText((Join-Path $staging '.release-staging'), $runId)
    $required = [Collections.Generic.List[string]]::new()
    $componentIndex = 0
    Write-Host "Building $packageName"
    foreach ($component in $Manifest.components) {
        if ($component.kind -eq 'app' -and -not $IncludeApp) { continue }
        $projectPath = Resolve-ReleasePath $Manifest._directory $component.project
        $info = Get-ReleaseProjectInfo $projectPath $Configuration
        $componentRuntime = Get-ReleaseComponentRuntime $component $info $Runtime
        if (-not $componentRuntime) { continue }
        $componentIndex++
        $publishDirectory = Join-Path $staging ('publish/' + $componentIndex)
        $logPath = Join-Path $staging ($info.AssemblyName + '.log')
        $arguments = @('publish', $projectPath, '-c', $Configuration, '--self-contained', 'false', '--nologo', '-v', 'minimal', '-o', $publishDirectory)
        if ($componentRuntime -ne 'anycpu') { $arguments += @('--runtime', $componentRuntime) }
        if ($component.kind -eq 'app' -and $componentRuntime -eq 'anycpu') { $arguments += '-p:UseAppHost=false' }
        Write-Host "  $($info.AssemblyName) [$componentRuntime]"
        & dotnet @arguments *> $logPath
        if ($LASTEXITCODE -ne 0) {
            Get-Content -LiteralPath $logPath -Tail 30 | Write-Host
            throw "Publish failed. Log: $logPath"
        }
        foreach ($entry in @(Copy-ReleaseComponent $component $info $publishDirectory $payload)) { $required.Add($entry) }
    }
    foreach ($asset in $Manifest.assets) {
        if ($asset.ContainsKey('includeWithApp') -and $asset.includeWithApp -and -not $IncludeApp) { continue }
        $source = Resolve-ReleasePath $Manifest._directory $asset.source
        if (Test-Path -LiteralPath $source -PathType Container) {
            $files = @(Get-ChildItem -LiteralPath $source -File -Recurse)
            if ($files.Count -eq 0) { throw "Empty package asset directory: $source" }
            foreach ($file in $files) {
                $relative = [IO.Path]::GetRelativePath($source, $file.FullName).Replace('\', '/')
                $destination = $asset.destination.TrimEnd('/') + '/' + $relative
                Copy-ReleaseFile $file.FullName $payload $destination
                $required.Add($destination)
            }
        } else {
            Copy-ReleaseFile $source $payload $asset.destination
            $required.Add($asset.destination)
        }
    }
    $readme = Expand-ReleaseReadme $metadata $Runtime
    [IO.File]::WriteAllText((Join-Path $payload 'readme.txt'), $readme, [Text.UTF8Encoding]::new($true))
    $null = New-Item -ItemType Directory -Path $OutputDirectory -Force
    $archivePath = Join-Path $OutputDirectory ($packageName + '.zip')
    $temporaryArchive = Join-Path $OutputDirectory ('.' + $runId + '.tmp')
    try {
        [IO.Compression.ZipFile]::CreateFromDirectory($payload, $temporaryArchive, [IO.Compression.CompressionLevel]::Optimal, $false)
        $entryCount = Assert-ReleaseArchive $temporaryArchive $required.ToArray() $readme
        $payloadCount = @(Get-ChildItem -LiteralPath $payload -File -Recurse).Count
        if ($entryCount -ne $payloadCount) { throw 'ZIP file count differs from package staging.' }
        [IO.File]::Move($temporaryArchive, $archivePath, $true)
    } finally {
        if (Test-Path -LiteralPath $temporaryArchive) { Remove-Item -LiteralPath $temporaryArchive }
    }
    $hash = (Get-FileHash -LiteralPath $archivePath -Algorithm SHA256).Hash.ToLowerInvariant()
    [IO.File]::WriteAllText($archivePath + '.sha256', $hash + '  ' + [IO.Path]::GetFileName($archivePath) + [Environment]::NewLine)
    $result = [pscustomobject]@{
        Id = $Manifest.id; Version = $metadata.Version; Runtime = $Runtime; Date = $metadata.DateEn
        Archive = $archivePath; Sha256 = $hash; Files = $entryCount; IncludeApp = [bool]$IncludeApp
        RequiredEntries = $required.ToArray(); Manifest = $Manifest._path
    }
    $recordDirectory = Get-ReleaseRecordDirectory $OutputDirectory
    $null = New-Item -ItemType Directory -Path $recordDirectory -Force
    $logDirectory = Join-Path $recordDirectory $packageName
    $null = New-Item -ItemType Directory -Path $logDirectory -Force
    Get-ChildItem -LiteralPath $staging -Filter '*.log' -File | Copy-Item -Destination $logDirectory -Force
    $result | ConvertTo-Json -Depth 8 | Set-Content -LiteralPath (Join-Path $recordDirectory ($packageName + '.json')) -Encoding utf8
    if (-not $KeepStaging) { Remove-ReleaseStaging $staging }
    Write-Host "  ZIP: $archivePath ($entryCount files)"
    return $result
}

Export-ModuleMember -Function Get-ReleaseCatalog, Read-ReleaseManifest, Get-ReleaseProjectInfo, Get-DriverDisplayMetadata,
    Get-ReleaseMetadata, Expand-ReleaseReadme, Get-ReleaseComponentRuntime, Resolve-ReleasePath, Assert-ReleaseArchive,
    Invoke-ReleasePackage, Get-ReleaseRecordDirectory
