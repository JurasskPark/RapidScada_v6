param(
    [ValidateSet('Debug', 'Release')]
    [string]$Configuration = 'Release'
)

$ErrorActionPreference = 'Stop'
$roots = 'OpenDrivers', 'OpenExtensions', 'OpenModules', 'OpenPlugins'
$smokeProject = Join-Path $PSScriptRoot 'Tests\ResourceSmoke\ResourceSmoke.csproj'
& dotnet build $smokeProject -c $Configuration --nologo -v quiet
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
$smokeDll = Join-Path $PSScriptRoot "Tests\ResourceSmoke\bin\$Configuration\net10.0-windows\ResourceSmoke.dll"
$failed = 0
$projectCount = 0
$viewCount = 0

foreach ($root in $roots) {
    $directory = Join-Path $PSScriptRoot $root
    foreach ($project in (Get-ChildItem -LiteralPath $directory -Filter '*.csproj' -Recurse | Where-Object { $_.FullName -notmatch '[\\/](bin|obj|Release|Debug)[\\/]' })) {
        [xml]$xml = Get-Content -LiteralPath $project.FullName -Raw
        $framework = @($xml.Project.PropertyGroup.TargetFramework | Where-Object { $_ })
        if ($framework.Count -ne 1 -or $framework[0] -notin @('net10.0', 'net10.0-windows')) {
            throw "Unexpected target framework: $($project.FullName)"
        }
        $projectCount++
        $windowsForms = @($xml.Project.PropertyGroup.UseWindowsForms) -contains 'true'
        $outputType = @($xml.Project.PropertyGroup.OutputType | Where-Object { $_ })
        if ($windowsForms -and $outputType.Count -eq 0) {
            $assemblyName = @($xml.Project.PropertyGroup.AssemblyName | Where-Object { $_ }) | Select-Object -First 1
            if (!$assemblyName) { $assemblyName = $project.BaseName }
            $dll = Join-Path $project.DirectoryName "bin\$Configuration\$($framework[0])\$assemblyName.dll"
            if (!(Test-Path -LiteralPath $dll)) { throw "Build the open projects first: $dll" }
            & dotnet $smokeDll $dll
            if ($LASTEXITCODE -ne 0) { $failed++ }
            $viewCount++
        }
    }
    foreach ($resource in (Get-ChildItem -LiteralPath $directory -Filter '*.resx' -Recurse | Where-Object { $_.FullName -notmatch '[\\/](bin|obj|Release|Debug)[\\/]' })) {
        [xml]$xml = Get-Content -LiteralPath $resource.FullName -Raw
        $binaryEntries = @($xml.root.data | Where-Object { $_.mimetype -eq 'application/x-microsoft.net.object.binary.base64' })
        if ($binaryEntries.Count -gt 0) {
            & dotnet $smokeDll --check-binary-resources $resource.FullName
            if ($LASTEXITCODE -ne 0) { $failed++ }
        }
    }
}

$driverTests = Join-Path $PSScriptRoot "OpenDrivers\DrvDbDataTransferJP_v6\DrvDbDataTransferJP.Tests\bin\$Configuration\net10.0\DrvDbDataTransferJP.Tests.dll"
& dotnet $driverTests
if ($LASTEXITCODE -ne 0) { $failed++ }
Write-Host "Checked $projectCount target frameworks, $viewCount WinForms assemblies, resources and driver tests. Failures: $failed"
if ($failed -gt 0) { exit 1 }
