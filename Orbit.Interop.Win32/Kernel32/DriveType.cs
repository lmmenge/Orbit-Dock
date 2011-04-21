namespace Win32.Kernel32
{
	/// <summary>
	/// Types of logical drives
	/// </summary>
	public enum DriveType
	{
		/// <summary>
		/// CD-ROM
		/// </summary>
		CD=5,
		/// <summary>
		/// Fixed Disk
		/// </summary>
		Fixed=3,
		/// <summary>
		/// Removable Disk
		/// </summary>
		Removable=2,
		/// <summary>
		/// Remote Disk
		/// </summary>
		Remote=4,
		/// <summary>
		/// Ram Disk
		/// </summary>
		RamDisk=6
	}
}