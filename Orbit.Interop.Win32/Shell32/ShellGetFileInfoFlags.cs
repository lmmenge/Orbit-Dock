using System;

namespace Win32.Shell32
{
	/// <summary>
	/// Summary description for ShellGetFileInfoFlags.
	/// </summary>
	public enum ShellGetFileInfoFlags
	{
		/// <summary>
		/// Version 5.0. Apply the appropriate overlays to the file's icon. The SHGFI_ICON flag must also be set.
		/// </summary>
		AddOverlays=0x000000020,
		/// <summary>
		/// Modify SHGFI_ATTRIBUTES to indicate that the dwAttributes member of the SHFILEINFO structure at psfi contains the specific attributes that are desired. These attributes are passed to IShellFolder::GetAttributesOf. If this flag is not specified, 0xFFFFFFFF is passed to IShellFolder::GetAttributesOf, requesting all attributes. This flag cannot be specified with the SHGFI_ICON flag.
		/// </summary>
		SpecifiedAttributes=0x000020000,
		/// <summary>
		/// Retrieve the item attributes. The attributes are copied to the dwAttributes member of the structure specified in the psfi parameter. These are the same attributes that are obtained from IShellFolder::GetAttributesOf.
		/// </summary>
		Attributes=0x800,
		/// <summary>
		/// Retrieve the display name for the file. The name is copied to the szDisplayName member of the structure specified in psfi. The returned display name uses the long file name, if there is one, rather than the 8.3 form of the file name.
		/// </summary>
		DisplayName=0x200,
		/// <summary>
		/// Retrieve the type of the executable file if pszPath identifies an executable file. The information is packed into the return value. This flag cannot be specified with any other flags.
		/// </summary>
		EXEType=0x2000,
		/// <summary>
		/// Retrieve the handle to the icon that represents the file and the index of the icon within the system image list. The handle is copied to the hIcon member of the structure specified by psfi, and the index is copied to the iIcon member.
		/// </summary>
		Icon=0x100,
		/// <summary>
		/// Retrieve the name of the file that contains the icon representing the file specified by pszPath, as returned by the IExtractIcon::GetIconLocation method of the file's icon handler. Also retrieve the icon index within that file. The name of the file containing the icon is copied to the szDisplayName member of the structure specified by psfi. The icon's index is copied to that structure's iIcon member.
		/// </summary>
		IconLocation=0x1000,
		/// <summary>
		/// Modify SHGFI_ICON, causing the function to retrieve the file's large icon. The SHGFI_ICON flag must also be set.
		/// </summary>
		LargeIcon=0x0,
		/// <summary>
		/// Modify SHGFI_ICON, causing the function to add the link overlay to the file's icon. The SHGFI_ICON flag must also be set.
		/// </summary>
		AddLinkOverlay=0x8000,
		/// <summary>
		/// Modify SHGFI_ICON, causing the function to retrieve the file's open icon. Also used to modify SHGFI_SYSICONINDEX, causing the function to return the handle to the system image list that contains the file's small open icon. A container object displays an open icon to indicate that the container is open. The SHGFI_ICON and/or SHGFI_SYSICONINDEX flag must also be set.
		/// </summary>
		OpenIcon=0x2,
		/// <summary>
		/// Version 5.0. Return the index of the overlay icon. The value of the overlay index is returned in the upper eight bits of the iIcon member of the structure specified by psfi.
		/// </summary>
		OverlayIndex=0x000000040,
		/// <summary>
		/// Indicate that pszPath is the address of an ITEMIDLIST structure rather than a path name.
		/// </summary>
		PathIsItemIDList=0x8,
		/// <summary>
		/// Modify SHGFI_ICON, causing the function to blend the file's icon with the system highlight color. The SHGFI_ICON flag must also be set.
		/// </summary>
		SelectedIcon=0x10000,
		/// <summary>
		/// Modify SHGFI_ICON, causing the function to retrieve a Shell-sized icon. If this flag is not specified the function sizes the icon according to the system metric values. The SHGFI_ICON flag must also be set.
		/// </summary>
		ShellIconSize=0x4,
		/// <summary>
		/// Modify SHGFI_ICON, causing the function to retrieve the file's small icon. Also used to modify SHGFI_SYSICONINDEX, causing the function to return the handle to the system image list that contains small icon images. The SHGFI_ICON and/or SHGFI_SYSICONINDEX flag must also be set.
		/// </summary>
		SmallIcon=0x1,
		/// <summary>
		/// Retrieve the index of a system image list icon. If successful, the index is copied to the iIcon member of psfi. The return value is a handle to the system image list. Only those images whose indices are successfully copied to iIcon are valid. Attempting to access other images in the system image list will result in undefined behavior.
		/// </summary>
		SystemImageListIconIndex=0x4000,
		/// <summary>
		/// Retrieve the string that describes the file's type. The string is copied to the szTypeName member of the structure specified in psfi.
		/// </summary>
		FileTypeName=0x400,
		/// <summary>
		/// Indicates that the function should not attempt to access the file specified by pszPath. Rather, it should act as if the file specified by pszPath exists with the file attributes passed in dwFileAttributes. This flag cannot be combined with the SHGFI_ATTRIBUTES, SHGFI_EXETYPE, or SHGFI_PIDL flags.
		/// </summary>
		UseFileAttributes=0x10,
	}
}
