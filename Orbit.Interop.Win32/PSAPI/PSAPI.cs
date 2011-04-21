using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Win32.PSAPI
{
	/// <summary>
	/// Summary description for PSAPI.
	/// </summary>
	public sealed class PSAPI
	{
		/// <summary>
		/// The EnumProcessModules function retrieves a handle for each module in the specified process.
		/// </summary>
		/// <param name="hProcess">Handle to the process.</param>
		/// <param name="lphModule">Pointer to the array that receives the list of module handles.</param>
		/// <param name="cb">Size of the lphModule array, in bytes.</param>
		/// <param name="lpcbNeeded">Number of bytes required to store all module handles in the lphModule array.</param>
		/// <returns>If the function succeeds, the return value is nonzero.</returns>
		[DllImport("psapi.dll")]
		public static extern int EnumProcessModules(IntPtr hProcess, ref IntPtr lphModule, uint cb, ref uint lpcbNeeded);

		/// <summary>
		/// The GetModuleFileNameEx function retrieves the fully-qualified path for the file containing the specified module.
		/// </summary>
		/// <param name="hProcess">Handle to the process that contains the module. The handle must have the PROCESS_QUERY_INFORMATION and PROCESS_VM_READ access rights. For more information, see Process Security and Access Rights.</param>
		/// <param name="hModule">Handle to the module. If this parameter is NULL, GetModuleFileNameEx returns the path of the executable file of the process specified in hProcess.</param>
		/// <param name="lpFilename">Pointer to the null-terminated buffer that receives the fully-qualified path to the module. If the size of the file name is larger than the value of the nSize parameter, the file name is truncated and null terminated.</param>
		/// <param name="nSize">Size of the lpFilename buffer, in characters.</param>
		/// <returns>If the function succeeds, the return value specifies the length of the string copied to the buffer. If the function fails, the return value is zero.</returns>
		[DllImport("psapi.dll", CharSet = CharSet.Unicode)]
		public static extern int GetModuleFileNameExW(IntPtr hProcess, IntPtr hModule, StringBuilder lpFilename, uint nSize);
	}
}
