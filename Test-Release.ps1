#requires -Version 7.2
[CmdletBinding()]
param(
    [string]$Project,
    [ValidateSet('all', 'win-x64', 'win-x86', 'linux-x64', 'anycpu')][string]$Runtime = 'all',
    [string]$OutputDirectory = (Join-Path $PSScriptRoot 'Releases'),
    [ValidateSet('Debug', 'Release')][string]$Configuration = 'Release'
)
$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest
Import-Module (Join-Path $PSScriptRoot 'Build/Release.psm1') -Force

function Assert-Condition([bool]$Condition, [string]$Message) {
    if (-not $Condition) { throw $Message }
}
function Assert-Throws([scriptblock]$Action, [string]$Message) {
    $threw = $false
    try { & $Action | Out-Null } catch { $threw = $true }
    Assert-Condition $threw $Message
}
function Get-FileSha256([string]$Path) { return (Get-FileHash -LiteralPath $Path -Algorithm SHA256).Hash.ToLowerInvariant() }

$catalog = @(Get-ReleaseCatalog)
Assert-Condition ($catalog.Count -gt 0) 'Empty release catalog.'
$driverUtils = @(Get-ChildItem (Join-Path $PSScriptRoot 'OpenDrivers') -Filter DriverUtils.cs -File -Recurse | Where-Object { $_.FullName -notmatch '[\\/](bin|obj|Release)[\\/]' })
$registeredUtils = @($catalog | Where-Object { $_.ContainsKey('driverUtils') } | ForEach-Object { Resolve-ReleasePath $_._directory $_.driverUtils })
foreach ($file in $driverUtils) { Assert-Condition ($file.FullName -in $registeredUtils) "No release manifest for $($file.FullName)" }
foreach ($path in @('../outside', 'SCADA/../../outside', 'C:/outside', 'SCADA/*.dll')) {
    Assert-Throws { Resolve-ReleasePath $PSScriptRoot $path } "Unsafe path accepted: $path"
}
foreach ($manifest in $catalog) {
    $wrappers = @(Get-ChildItem -LiteralPath $manifest._directory -Filter 'Start*.bat' -File)
    Assert-Condition ($wrappers.Count -eq 1) "Expected one release BAT for $($manifest.id)"
    $text = [IO.File]::ReadAllText($wrappers[0].FullName)
    Assert-Condition ($text.Contains('Build-Release.ps1') -and $text.Contains('-Project "%~dp0release.json"') -and $text.Contains('%ERRORLEVEL%')) "Invalid wrapper for $($manifest.id)"
    Assert-Condition ($text -notmatch '(?im)^\s*(taskkill|sc\s+stop|net\s+stop|copy\s+/Y|dotnet\s+)') "Legacy deployment command in $($wrappers[0].FullName)"
}

$sample = Get-ReleaseMetadata $catalog[0] $Configuration ([datetime]'2026-01-02')
$next = $sample.Clone()
$next.Version = '9.8.7.6'; $next.DateRu = '03.02.2027'; $next.DateEn = '2027-02-03'
$firstReadme = Expand-ReleaseReadme $sample 'win-x64'
$nextReadme = Expand-ReleaseReadme $next 'win-x64'
foreach ($key in @('NameRu', 'NameEn', 'DescriptionRu', 'DescriptionEn', 'AuthorsRu', 'AuthorsEn', 'SourceUrl', 'ForumLinks')) {
    Assert-Condition ($firstReadme.Contains($sample[$key]) -and $nextReadme.Contains($sample[$key])) "README lost stable metadata: $key"
}
Assert-Condition ($nextReadme.Contains('9.8.7.6') -and $nextReadme.Contains('03.02.2027') -and $nextReadme.Contains('2027-02-03')) 'README did not update version/date.'
Write-Host 'PASS release catalog, DriverUtils coverage, BAT wrappers, path validation and README template'

foreach ($name in @('ReleaseSmoke', 'ResourceSmoke')) {
    & dotnet build (Join-Path $PSScriptRoot "Tests/$name/$name.csproj") -c Release --nologo -v quiet
    if ($LASTEXITCODE -ne 0) { throw "Cannot build $name" }
}
$smokeDll = Join-Path $PSScriptRoot 'Tests/ReleaseSmoke/bin/Release/net10.0-windows/ReleaseSmoke.dll'
$resourceDll = Join-Path $PSScriptRoot 'Tests/ResourceSmoke/bin/Release/net10.0-windows/ResourceSmoke.dll'
$dotnetX86 = Join-Path ${env:ProgramFiles(x86)} 'dotnet/dotnet.exe'
$selected = @($catalog | Where-Object { -not $Project -or $_.id -eq $Project })
Assert-Condition ($selected.Count -gt 0) "Unknown project: $Project"
$checkedArchives = 0
$checkedComponents = 0
$checkRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot 'artifacts/release-checks'))
$runDirectory = Join-Path $checkRoot ([Guid]::NewGuid().ToString('N'))
$null = New-Item -ItemType Directory -Path $runDirectory -Force
[IO.File]::WriteAllText((Join-Path $runDirectory '.release-check'), 'Generated release verification files')
try {
    foreach ($manifest in $selected) {
        $runtimes = @($manifest.runtimes | Where-Object { $Runtime -eq 'all' -or $_ -eq $Runtime })
        foreach ($targetRuntime in $runtimes) {
            $currentMetadata = Get-ReleaseMetadata $manifest $Configuration
            $packageName = $manifest.id + '_' + $currentMetadata.Version + '_' + $targetRuntime
            $recordPath = Join-Path (Get-ReleaseRecordDirectory $OutputDirectory) ($packageName + '.json')
            $record = Get-Content -LiteralPath $recordPath -Raw | ConvertFrom-Json
            $metadata = Get-ReleaseMetadata $manifest $Configuration ([datetime]$record.Date)
            $archivePath = Join-Path $OutputDirectory ($packageName + '.zip')
            $expectedReadme = Expand-ReleaseReadme $metadata $targetRuntime
            $count = Assert-ReleaseArchive $archivePath $record.RequiredEntries $expectedReadme
            Assert-Condition ($count -eq $record.Files) "File count changed: $packageName"
            Assert-Condition ((Get-FileSha256 $archivePath) -eq $record.Sha256) "Archive hash changed: $packageName"
            Assert-Condition (([IO.File]::ReadAllText($archivePath + '.sha256')).StartsWith($record.Sha256 + '  ')) "Invalid checksum file: $packageName"
            $directory = Join-Path $runDirectory $packageName
            [IO.Compression.ZipFile]::ExtractToDirectory($archivePath, $directory)
            $topLevel = @(Get-ChildItem -LiteralPath $directory | ForEach-Object Name)
            $allowedRoots = if ($record.IncludeApp) { @('SCADA', 'readme.txt', 'App') } else { @('SCADA', 'readme.txt') }
            Assert-Condition (@($topLevel | Where-Object { $_ -notin $allowedRoots }).Count -eq 0) "Unexpected ZIP root: $packageName"
            $metadataPath = Join-Path $runDirectory ($packageName + '.metadata.json')
            $metadata | ConvertTo-Json | Set-Content -LiteralPath $metadataPath -Encoding utf8
            foreach ($component in $manifest.components) {
                if ($component.kind -eq 'app' -and -not $record.IncludeApp) { continue }
                $projectPath = Resolve-ReleasePath $manifest._directory $component.project
                $info = Get-ReleaseProjectInfo $projectPath $Configuration
                $componentRuntime = Get-ReleaseComponentRuntime $component $info $targetRuntime
                if (-not $componentRuntime) { continue }
                [xml]$projectXml = [IO.File]::ReadAllText($projectPath)
                foreach ($destination in $component.destinations) {
                    $componentDirectory = Resolve-ReleasePath $directory $destination
                    $dllPath = Join-Path $componentDirectory ($info.AssemblyName + '.dll')
                    Assert-Condition ([Reflection.AssemblyName]::GetAssemblyName($dllPath).Version.ToString() -eq $info.AssemblyVersion) "Wrong DLL version: $dllPath"
                    $dependencyDirectory = if ($component.kind -eq 'app') { $componentDirectory } else { Join-Path $componentDirectory $info.AssemblyName }
                    $sql = Join-Path $dependencyDirectory 'Microsoft.Data.SqlClient.dll'
                    if (Test-Path -LiteralPath $sql) {
                        # Match the actual implementation from NuGet, not the portable throwing facade.
                        $assetFile = Join-Path (Split-Path -Parent $projectPath) 'obj/project.assets.json'
                        $assets = Get-Content -LiteralPath $assetFile -Raw | ConvertFrom-Json -AsHashtable
                        $sqlKey = @($assets.libraries.Keys | Where-Object { $_ -like 'Microsoft.Data.SqlClient/*' })[0]
                        $runtimeOS = if ($componentRuntime -like 'linux-*') { 'unix' } else { 'win' }
                        $candidateHashes = foreach ($packageRoot in $assets.packageFolders.Keys) {
                            $sqlRoot = Join-Path $packageRoot ($assets.libraries[$sqlKey].path + '/runtimes/' + $runtimeOS + '/lib')
                            if (Test-Path -LiteralPath $sqlRoot) {
                                Get-ChildItem -LiteralPath $sqlRoot -Filter Microsoft.Data.SqlClient.dll -File -Recurse | ForEach-Object { Get-FileSha256 $_.FullName }
                            }
                        }
                        Assert-Condition ((Get-FileSha256 $sql) -in $candidateHashes) "Incorrect SQL runtime implementation: $packageName/$($info.AssemblyName)"
                    }
                    foreach ($native in @(Get-ChildItem -LiteralPath $dependencyDirectory -Filter '*.SNI.dll' -File -ErrorAction SilentlyContinue)) {
                        $bytes = [IO.File]::ReadAllBytes($native.FullName)
                        $peOffset = [BitConverter]::ToInt32($bytes, 0x3c)
                        $machine = [BitConverter]::ToUInt16($bytes, $peOffset + 4)
                        $expectedMachine = if ($componentRuntime -eq 'win-x86') { 0x14c } else { 0x8664 }
                        Assert-Condition ($machine -eq $expectedMachine) "Incorrect native SQL architecture: $packageName"
                    }
                    if ($component.kind -eq 'app') {
                        $runtimeConfigPath = Join-Path $componentDirectory ($info.AssemblyName + '.runtimeconfig.json')
                        $runtimeConfig = Get-Content -LiteralPath $runtimeConfigPath -Raw | ConvertFrom-Json -AsHashtable
                        Assert-Condition ($runtimeConfig.runtimeOptions.tfm -eq 'net10.0') "Wrong application runtime: $packageName"
                        Assert-Condition (Test-Path -LiteralPath (Join-Path $componentDirectory ($info.AssemblyName + '.deps.json'))) "Missing application dependency map: $packageName"
                    }
                    if ($componentRuntime -eq 'linux-x64') { continue }
                    # Emulate the installed host using only explicitly referenced host DLLs.
                    foreach ($reference in $projectXml.SelectNodes('/Project/ItemGroup/Reference[HintPath]')) {
                        $hint = $reference.HintPath
                        $source = [IO.Path]::GetFullPath((Join-Path (Split-Path -Parent $projectPath) $hint))
                        $fileName = [IO.Path]::GetFileName($source)
                        if ($fileName -like 'Scada*.dll' -or $fileName -in @('AgentClient.dll', 'PlgMimic.Common.dll')) {
                            $actualName = [Reflection.AssemblyName]::GetAssemblyName($source).Name + '.dll'
                            Copy-Item -LiteralPath $source -Destination (Join-Path $componentDirectory $actualName) -Force
                        }
                    }
                    $runner = if ($componentRuntime -eq 'win-x86') { $dotnetX86 } else { 'dotnet' }
                    Assert-Condition ($runner -eq 'dotnet' -or (Test-Path -LiteralPath $runner)) '.NET 10 x86 runtime is required for win-x86 smoke tests.'
                    & $runner $smokeDll $dllPath $metadataPath $componentDirectory
                    if ($LASTEXITCODE -ne 0) { throw "Package load check failed: $packageName/$($info.AssemblyName)" }
                    if ($component.kind -ne 'app' -and $projectXml.SelectNodes('/Project/PropertyGroup/UseWindowsForms[text()="true"]').Count -gt 0) {
                        & $runner $resourceDll $dllPath
                        if ($LASTEXITCODE -ne 0) { throw "Packaged resource check failed: $packageName/$($info.AssemblyName)" }
                    }
                    $checkedComponents++
                }
            }
            $checkedArchives++
            Write-Host "PASS ZIP $packageName ($count files)"
        }
    }
    Assert-Condition ($checkedArchives -gt 0) 'No archives matched the requested runtime.'
    Write-Host "Completed: $checkedArchives archives, $checkedComponents Windows component load checks. Linux runtime execution requires a Linux host."
} finally {
    $fullPath = [IO.Path]::GetFullPath($runDirectory)
    Assert-Condition ($fullPath.StartsWith($checkRoot + [IO.Path]::DirectorySeparatorChar, [StringComparison]::OrdinalIgnoreCase) -and (Test-Path -LiteralPath (Join-Path $fullPath '.release-check'))) 'Refusing to delete outside generated verification staging.'
    Remove-Item -LiteralPath $fullPath -Recurse -Force
}
