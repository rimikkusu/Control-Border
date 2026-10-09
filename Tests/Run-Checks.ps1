param(
    [string]$MsBuildPath = 'C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe'
)
$ErrorActionPreference = 'Stop'
$projectRoot = Split-Path -Parent $PSScriptRoot
$projectPath = Join-Path $projectRoot 'Border Control\Border Control.csproj'
$buildDir = Join-Path $PSScriptRoot 'bin'
New-Item -ItemType Directory -Path $buildDir -Force | Out-Null
& $MsBuildPath $projectPath /t:Build /p:Configuration=Debug /p:UseSharedCompilation=false /nologo /v:minimal
if ($LASTEXITCODE -ne 0) { throw 'Game build failed.' }
$compilerPath = Join-Path (Split-Path -Parent $MsBuildPath) 'Roslyn\csc.exe'
$gamePath = Join-Path $projectRoot 'Border Control\bin\Debug\Border Control.exe'
$runnerPath = Join-Path $buildDir 'GameplayChecks.exe'
& $compilerPath /nologo /target:exe "/out:$runnerPath" "/reference:$gamePath" /reference:System.Windows.Forms.dll /reference:System.Drawing.dll /reference:System.Design.dll (Join-Path $PSScriptRoot 'GameplayChecks.cs')
if ($LASTEXITCODE -ne 0) { throw 'Test runner build failed.' }
Copy-Item -LiteralPath $gamePath -Destination $buildDir -Force
$artOutput = Join-Path $buildDir 'ControlBorderArt\Lobby'
New-Item -ItemType Directory -Path $artOutput -Force | Out-Null
Get-ChildItem -LiteralPath (Join-Path $projectRoot 'ControlBorderArt\Lobby') -Filter '*.png' | Copy-Item -Destination $artOutput -Force
& $runnerPath | Tee-Object -FilePath (Join-Path $PSScriptRoot 'latest-result.txt')
if ($LASTEXITCODE -ne 0) { throw 'Gameplay checks failed.' }
