namespace Win32.Kernel32
{
	/// <summary>
	/// The valid access rights for process objects include the DELETE, READ_CONTROL, SYNCHRONIZE, WRITE_DAC, and WRITE_OWNER standard access rights, in addition to the following process-specific access rights.
	/// </summary>
	public enum DesiredAccess
	{
		/// <summary>
		/// All possible access rights for a process object.
		/// </summary>
		AllAccess=0xF0000 | 0x100000 | 0xFFF,
		/// <summary>
		/// Required to create a process.
		/// </summary>
		CreateProcess=0x0080,
		/// <summary>
		/// Required to create a thread.
		/// </summary>
		CreateThread=0x0002,
		/// <summary>
		/// Required to duplicate a handle using DuplicateHandle.
		/// </summary>
		DuplicateHandle=0x0040,
		/// <summary>
		/// Required to retrieve certain information about a process, such as its token, exit code, and priority class (see OpenProcessToken, GetExitCodeProcess, GetPriorityClass, and IsProcessInJob).
		/// </summary>
		QueryInformation=0x0400,
		/// <summary>
		/// Required to set memory limits using SetProcessWorkingSetSize.
		/// </summary>
		SetQuota=0x0100,
		/// <summary>
		/// Required to set certain information about a process, such as its priority class (see SetPriorityClass).
		/// </summary>
		SetInformation=0x0200,
		/// <summary>
		/// Required to terminate a process using TerminateProcess.
		/// </summary>
		Terminate=0x0001,
		/// <summary>
		/// Required to perform an operation on the address space of a process (see VirtualProtectEx and WriteProcessMemory).
		/// </summary>
		VMOperation=0x0008,
		/// <summary>
		/// Required to read memory in a process using ReadProcessMemory.
		/// </summary>
		VMRead=0x0010,
		/// <summary>
		/// Required to write to memory in a process using WriteProcessMemory.																																																																																																													   SYNCHRONIZE 	Required to wait for the process to terminate using the wait functions.
		/// </summary>
		VMWrite=0x0020
	}
}