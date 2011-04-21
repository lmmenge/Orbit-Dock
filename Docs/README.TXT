Orbit Source Code
=================
Lucas Mendes Menge
February 7th, 2008


Disclaimer
==========
This is the source code for the application created by Lucas Mendes Menge called Orbit.
The application was created in Visual Studio.Net 2003 in C#. You may freely use the
source code contained here in your programs as long as you keep the source freely available
and give credit to the original authors. Lucas Mendes Menge cannot be held responsible for
any damage caused by the source code contained here to your computer whatsoever.

Instructions
============
Before opening and compiling the solution, it's recommended that you first set up the Binary
folders. This can be easily done for you by executing the "Script - Set Up Binary Folders.bat"
file in the root of the solution. That batch file will create the binary directories and copy
all the needed files for Orbit's execution to the correct folders.

Available Scripts
=================
In the root of the solution, there are 4 scripts:

Script - Clean Binary Dirs.bat
	- This script deletes the Bin folder in the root of the solution to decrease the size of the
	zipped source. Remember to back up your profile folder if you still want it, since it will be
	erased along with the binaries.

Script - Clean Object Dirs for Zipping.bat
	- This script goes through the projects's folders and deletes all the Bin and Obj folders
	to decrease the size of the zipped source. This does NOT, however, erase the main Bin folder
	in the root of the solution.

Script - Create Nightly Build.bat
	- This script creates the Nightly folder inside the Bin folder and sets it up with files from
	the Debug build so that it can be zipped and distributed as a nightly

Script - Set Up Binary Folders.bat
	- This script creates the binary folders and copies all the needed files for Orbit's execution
	to them.