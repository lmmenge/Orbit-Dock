@echo off
cls
echo WARNING!!!
echo This will overwrite your Binary structure!!!
pause

echo Creating Binary Directories
mkdir Bin
mkdir Bin\Debug
mkdir Bin\Release

echo Copying Base Directory Structure
xcopy BasicBinFolders Bin\Debug /e
xcopy BasicBinFolders Bin\Release /e

echo Copying Manifest files
copy Orbit\Orbit.exe.manifest Bin\Debug
copy Orbit\Orbit.exe.manifest Bin\Release

pause