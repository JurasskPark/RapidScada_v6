#requires -Version 7.2
$ErrorActionPreference = 'Stop'
$repository = Split-Path -Parent $PSScriptRoot
Import-Module (Join-Path $repository 'Build/Release.psm1') -Force
$directory = Join-Path $repository ('artifacts/release-failure-checks/' + [Guid]::NewGuid().ToString('N'))
$output = Join-Path $directory 'output with spaces'
$null = New-Item -ItemType Directory -Path $output -Force
$projectPath = Join-Path $directory 'ReleaseFailureProbe.csproj'
$project = @'
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <Version>1.2.3.4</Version>
    <EnableDefaultCompileItems>false</EnableDefaultCompileItems>
  </PropertyGroup>
  <Target Name="FailPublicationForTest" BeforeTargets="Publish">
    <Error Text="EXPECTED_RELEASE_PUBLICATION_FAILURE" />
  </Target>
</Project>
'@
[IO.File]::WriteAllText($projectPath, $project)
$manifest = @{
    schemaVersion=1; id='ReleaseFailureProbe'; versionProject='ReleaseFailureProbe.csproj'
    display=@{NameRu='Проверка';NameEn='Check';DescriptionRu='Проверка ошибки публикации.';DescriptionEn='Publication failure check.'}
    authors=@{ru=@('Проверка');en=@('Test')}
    sourceUrl='https://github.com/JurasskPark/RapidScada_v6'
    forums=@(@{label='Forum';url='https://forum.rapidscada.ru/'})
    runtimes=@('win-x64')
    components=@(@{project='ReleaseFailureProbe.csproj';kind='library';destinations=@('SCADA/ScadaComm/Drv')})
}
$manifestPath = Join-Path $directory 'release.json'
$manifest | ConvertTo-Json -Depth 8 | Set-Content -LiteralPath $manifestPath -Encoding utf8
$archivePath = Join-Path $output 'ReleaseFailureProbe_1.2.3.4_win-x64.zip'
$sentinel = [IO.Compression.ZipFile]::Open($archivePath, [IO.Compression.ZipArchiveMode]::Create)
try {
    $writer = [IO.StreamWriter]::new($sentinel.CreateEntry('previous-release.txt').Open())
    try { $writer.Write('Preserve the previous release if publishing fails.') } finally { $writer.Dispose() }
} finally { $sentinel.Dispose() }
$expectedHash = (Get-FileHash -LiteralPath $archivePath).Hash
$log = Join-Path $directory 'expected-failure.log'
& pwsh -NoProfile -File (Join-Path $repository 'Build-Release.ps1') -Project $manifestPath -Runtime win-x64 -OutputDirectory $output *> $log
if ($LASTEXITCODE -eq 0) { throw 'Failed publication returned success.' }
if (-not ([IO.File]::ReadAllText($log)).Contains('EXPECTED_RELEASE_PUBLICATION_FAILURE')) { throw "Publication failed for an unexpected reason. See $log" }
if ((Get-FileHash -LiteralPath $archivePath).Hash -ne $expectedHash) { throw 'Failed publication changed the previous archive.' }
if (@(Get-ChildItem -LiteralPath $output -Filter '*.tmp' -File).Count -ne 0) { throw 'Failed publication left a temporary output archive.' }

& (Join-Path $repository 'Build-Release.bat') -Project NoSuchReleaseForFailureTest *> (Join-Path $directory 'invalid-id.log')
if ($LASTEXITCODE -eq 0) { throw 'BAT did not propagate the invalid-project error.' }
Write-Host "PASS expected publish failure, previous ZIP preservation, output path with spaces and BAT error code. Logs: $directory"
