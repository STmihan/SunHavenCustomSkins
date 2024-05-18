@echo off
IF "%SunHaven%"=="" (
    echo You didn't set the SunHaven environment variable. Set it first using setenv.bat or by yourself
    echo You can close this window "ESC"
    PAUSE >nul
    exit 1
)

echo SunHaven = %SunHaven%
SET sln=%~dp0..\CustomSkins.sln
SET buildDir=%~dp0..\build

echo %sln%
dotnet build -c=Release "%sln%"
mkdir "%buildDir%"
mkdir "%buildDir%\CustomSkins"
xcopy /q/y/i/s/e "%SunHaven%\BepInEx\plugins\CustomSkins" "%buildDir%\CustomSkins"
echo Done! Check %buildDir% folder
echo You can close this window "ESC"
PAUSE >nul
exit 0