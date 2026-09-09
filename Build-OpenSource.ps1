param(
    [ValidateSet('Debug', 'Release')]
    [string]$Configuration = 'Release'
)

$ErrorActionPreference = 'Stop'
$roots = 'OpenDrivers', 'OpenExtensions', 'OpenModules', 'OpenPlugins'
$logDirectory = Join-Path $PSScriptRoot "artifacts\build-net10-$Configuration"
New-Item -ItemType Directory -Path $logDirectory -Force | Out-Null
$projects = @($roots | ForEach-Object {
    Get-ChildItem -LiteralPath (Join-Path $PSScriptRoot $_) -Filter '*.csproj' -Recurse |
        Where-Object { $_.FullName -notmatch '[\\/](bin|obj)[\\/]' }
} | Sort-Object FullName)

$results = @()
foreach ($project in $projects) {
    $relativePath = $project.FullName.Substring($PSScriptRoot.Length + 1)
    $logPath = Join-Path $logDirectory ($relativePath -replace '[\\/]', '_')
    $logPath += '.log'
    Write-Host "Building $relativePath"
    & dotnet build $project.FullName -c $Configuration --nologo -v minimal *> $logPath
    $exitCode = $LASTEXITCODE
    $results += [pscustomobject]@{ Project = $relativePath; ExitCode = $exitCode; Log = $logPath }
    if ($exitCode -ne 0) {
        Write-Host "FAILED: $relativePath (see $logPath)"
    }
}

$results | ConvertTo-Json -Depth 3 | Set-Content -LiteralPath (Join-Path $logDirectory 'results.json') -Encoding utf8
$failed = @($results | Where-Object ExitCode -ne 0)
Write-Host "Built $($projects.Count - $failed.Count)/$($projects.Count) projects. Logs: $logDirectory"
if ($failed.Count -gt 0) { exit 1 }
