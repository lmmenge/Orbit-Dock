using System;
using System.Runtime.InteropServices;

namespace Win32.Kernel32
{
	/// <summary>
	/// Summary description for Kernel32API.
	/// </summary>
	public sealed class Kernel32API
	{
		/// <summary>
		/// Gets the media type of a logical drive
		/// </summary>
		/// <param name="driveLetter">Letter to the logical drive</param>
		/// <returns>Returns the media type of a logical drive</returns>
		[DllImport("kernel32.dll")]
		public static extern DriveType GetDriveType(string driveLetter);

		/// <summary>
		/// The OpenProcess function opens an existing process object.
		/// </summary>
		/// <param name="dwDesiredAccess">Access to the process object. This access right is checked against any security descriptor for the process. This parameter can be one or more of the process access rights.</param>
		/// <param name="bInheritHandle">If this parameter is TRUE, the handle is inheritable. If the parameter is FALSE, the handle cannot be inherited.</param>
		/// <param name="dwProcessId">Identifier of the process to open.</param>
		/// <returns>If the function succeeds, the return value is an open handle to the specified process. If the function fails, the return value is NULL</returns>
		// TODO: check if bInheritHandle can be bool
		[DllImport("kernel32")]
		public static extern int OpenProcess(DesiredAccess dwDesiredAccess, int bInheritHandle, int dwProcessId);
		
		/// <summary>
		/// The CloseHandle function closes an open object handle.
		/// </summary>
		/// <param name="hObject">Handle to an open object. This parameter can be a pseudo handle or INVALID_HANDLE_VALUE.</param>
		/// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.</returns>
		[DllImport("kernel32")]
		public static extern int CloseHandle(IntPtr hObject);
	}
}
