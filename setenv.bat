@echo off

echo Enter the path to Sun Haven folder (e.g. C:\Program Files (x86)\Steam\steamapps\common\Sun Haven)
set /p "shpath=> "
setx SunHaven "%shpath%" /m || goto :error

echo Done!
echo SunHaven environment variable set to %shpath%
echo You can close this window (ESC)
PAUSE >nul
exit 0

:error
echo Something went wrong
echo You can close this window (ESC)
PAUSE >nul
exit 0
