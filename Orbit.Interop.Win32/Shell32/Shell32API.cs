using System;
using System.Runtime.InteropServices;

namespace Win32.Shell32
{
	/// <summary>
	/// Summary description for Shell32API.
	/// </summary>
	public sealed class Shell32API
	{
		/// <summary>
		/// Executes an action with a file
		/// </summary>
		/// <param name="lpExecInfo">Information on how to execute the file</param>
		/// <returns>True if succeeded. False if not</returns>
		[DllImport("shell32.dll")]
		public static extern bool ShellExecuteEx(ShellExecuteInfo lpExecInfo);

		/// <summary>
		/// Gets information on a file
		/// </summary>
		/// <param name="pszPath">Path to the file</param>
		/// <param name="dwFileAttributes">User attribute flags</param>
		/// <param name="psfi">The SHFileInfo structure to receive the information</param>
		/// <param name="cbSizeFileInfo">Size of the structured indicated in psfi</param>
		/// <param name="uFlags">Flags to indicate what file information to receive</param>
		/// <returns>0 if fails. Otherwise either succeeded and optionally the EXE type.</returns>
		[DllImport("shell32.dll")]
		public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFileInfo psfi, uint cbSizeFileInfo, ShellGetFileInfoFlags uFlags);
	}
}
