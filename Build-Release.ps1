#requires -Version 7.2
[CmdletBinding(DefaultParameterSetName = 'Project')]
param(
    [Parameter(ParameterSetName = 'Project')][string]$Project,
    [Parameter(ParameterSetName = 'All', Mandatory)][switch]$All,
    [Parameter(ParameterSetName = 'List', Mandatory)][switch]$List,
    [ValidateSet('all', 'win-x64', 'win-x86', 'linux-x64', 'anycpu')][string]$Runtime = 'all',
    [ValidateSet('Debug', 'Release')][string]$Configuration = 'Release',
    [string]$OutputDirectory = (Join-Path $PSScriptRoot 'Releases'),
    [datetime]$Date = [datetime]::Now,
    [switch]$IncludeApp,
    [switch]$KeepStaging,
    [Alias('package-only')][switch]$PackageOnly
)
$ErrorActionPreference = 'Stop'
Import-Module (Join-Path $PSScriptRoot 'Build/Release.psm1') -Force
try {
    $catalog = @(Get-ReleaseCatalog)
    if ($List) {
        $catalog | ForEach-Object { [pscustomobject]@{ Id = $_.id; Runtimes = $_.runtimes -join ', '; Manifest = $_._path } } | Format-Table -AutoSize
        exit 0
    }
    if ($All) {
        $selected = $catalog
    } elseif ([string]::IsNullOrWhiteSpace($Project)) {
        throw 'Specify -Project <release ID or release.json path>, -All or -List.'
    } elseif (Test-Path -LiteralPath $Project) {
        $manifestPath = if (Test-Path -LiteralPath $Project -PathType Container) { Join-Path $Project 'release.json' } else { $Project }
        $selected = @(Read-ReleaseManifest $manifestPath)
    } else {
        $selected = @($catalog | Where-Object { $_.id -eq $Project })
        if ($selected.Count -eq 0) { throw "Release project not found: $Project" }
    }
    $output = [IO.Path]::GetFullPath($OutputDirectory)
    $results = [Collections.Generic.List[object]]::new()
    foreach ($manifest in $selected) {
        if ($Runtime -ne 'all' -and $Runtime -notin $manifest.runtimes) {
            if ($All) { Write-Host "Skipping $($manifest.id): $Runtime is unsupported."; continue }
            throw "$($manifest.id) does not support $Runtime."
        }
        $runtimes = if ($Runtime -eq 'all') { $manifest.runtimes } else { @($Runtime) }
        foreach ($targetRuntime in $runtimes) {
            $results.Add((Invoke-ReleasePackage $manifest $targetRuntime $output $Configuration $Date -IncludeApp:$IncludeApp -KeepStaging:$KeepStaging))
        }
    }
    Write-Host "Completed: $($results.Count) ZIP archives in $output"
} catch {
    Write-Error $_ -ErrorAction Continue
    exit 1
}

