@echo off
setlocal
cd /d "%~dp0"

dotnet build -c Release
if errorlevel 1 exit /b %errorlevel%

copy /Y "bin\Release\netstandard2.0\JobMap.Generator.dll" "..\JobMap.Generator.dll"
if errorlevel 1 exit /b %errorlevel%

echo Deployed JobMap.Generator.dll to Assets/JobMap.
