@echo off
cls

echo Cleaning Orbit...
rmdir /q /s Orbit\obj

echo Cleaning DockSetup...
rmdir /q /s DockSetup\bin
rmdir /q /s DockSetup\obj

echo Cleaning ItemSetup...
rmdir /q /s ItemSetup\bin
rmdir /q /s ItemSetup\obj

echo Cleaning Orbit.Interop.Win32...
rmdir /q /s Orbit.Interop.win32\bin
rmdir /q /s Orbit.Interop.win32\obj

echo Cleaning Orbit.Utilites...
rmdir /q /s Orbit.Utilities\bin
rmdir /q /s Orbit.Utilities\obj

echo cleaning OrbitServicesClient...
rmdir /q /s OrbitServicesClient\bin
rmdir /q /s OrbitServicesClient\obj

echo cleaning LanguageLoader...
rmdir /q /s LanguageLoader\bin
rmdir /q /s LanguageLoader\obj

echo cleaning Orbit.Hook...
rmdir /q /s Orbit.Hook\Debug
rmdir /q /s Orbit.Hook\Release

pause