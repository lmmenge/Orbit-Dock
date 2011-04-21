@echo off
cls

if exist Bin\Nightly echo Cleaning Nightly Directory
if exist Bin\Nightly rmdir /q /s Bin\Nightly

echo Creating Nightly folder structure
mkdir Bin\Nightly

echo Copying Resources
xcopy BasicBinFolders Bin\Nightly /e

echo Copying Binaries
copy Bin\Debug\*.exe Bin\Nightly
copy Bin\Debug\*.dll Bin\Nightly
copy Bin\Debug\*.manifest Bin\Nightly

pause