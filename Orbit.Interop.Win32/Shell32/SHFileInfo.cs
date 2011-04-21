using System;
using System.Runtime.InteropServices;

namespace Win32.Shell32
{
	// TODO: Look at the Attributes ENUM
	/// <summary>
	/// Contains information about a file object.
	/// </summary>
	public struct SHFileInfo
	{
		/// <summary>
		/// Handle to the icon that represents the file. You are responsible for destroying this handle with DestroyIcon when you no longer need it.
		/// </summary>
		public System.IntPtr hIcon;
		/// <summary>
		/// Index of the icon image within the system image list.
		/// </summary>
		public System.IntPtr iIcon;
		/// <summary>
		/// Array of values that indicates the attributes of the file object. For information about these values, see the IShellFolder::GetAttributesOf method.
		/// </summary>
		public uint dwAttributes;
		/// <summary>
		/// String that contains the name of the file as it appears in the Microsoft Windows Shell, or the path and file name of the file that contains the icon representing the file.
		/// </summary>
		[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 260)]
		public string szDisplayName;
		/// <summary>
		/// String that describes the type of file.
		/// </summary>
		[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 80)]
		public string szTypeName;
	}
}