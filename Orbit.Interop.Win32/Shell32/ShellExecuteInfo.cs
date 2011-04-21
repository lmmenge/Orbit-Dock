using System;
using System.Runtime.InteropServices;

namespace Win32.Shell32
{
	/// <summary>
	/// Contains information used by ShellExecuteEx.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public class ShellExecuteInfo 
	{
		/// <summary>
		/// Size of the structure, in bytes.
		/// </summary>
		public int cbSize;
		/// <summary>
		/// Array of flags that indicate the content and validity of the other structure members. This can be a combination of the following values.
		/// </summary>
		public int fMask;
		/// <summary>
		/// Window handle to any message boxes that the system might produce while executing this function.
		/// </summary>
		public int hwnd;
		/// <summary>
		/// String, referred to as a verb, that specifies the action to be performed. The set of available verbs depends on the particular file or folder. Generally, the actions available from an object's shortcut menu are available verbs. For more specific information about verbs, see Object Verbs. For further discussion of shortcut menus, see Extending Shortcut Menus. If you set this parameter to NULL:
		///
		/// * For systems prior to Windows 2000, the default verb is used if it is valid and available in the registry. If not, the "open" verb is used.
		/// * For Windows 2000 and later systems, the default verb is used if available. If not, the "open" verb is used. If neither verb is available, the system uses the first verb listed in the registry.
		/// </summary>
		//[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
		public string lpVerb;
		/// <summary>
		/// Address of a null-terminated string that specifies the name of the file or object on which ShellExecuteEx will perform the action specified by the lpVerb parameter. The system registry verbs that are supported by the ShellExecuteEx function include "open" for executable files and document files and "print" for document files for which a print handler has been registered. Other applications might have added Shell verbs through the system registry, such as "play" for .avi and .wav files. To specify a Shell namespace object, pass the fully qualified parse name and set the SEE_MASK_INVOKEIDLIST flag in the fMask parameter.
		/// 
		/// Note If the SEE_MASK_INVOKEIDLIST flag is set, you can use either lpFile or lpIDList to identify the item by its file system path or its PIDL respectively.
		/// Note If the path is not included with the name, the current directory is assumed. 
		/// </summary>
		//[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
		public string lpFile;
		/// <summary>
		/// Address of a null-terminated string that contains the application parameters. The parameters must be separated by spaces. If the lpFile member specifies a document file, lpParameters should be NULL.
		/// </summary>
		//[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
		public string lpParameters;
		/// <summary>
		/// Address of a null-terminated string that specifies the name of the working directory. If this member is not specified, the current directory is used as the working directory.
		/// </summary>
		//[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
		public string lpDirectory;
		/// <summary>
		/// Flags that specify how an application is to be shown when it is opened. It can be one of the SW_ values listed for the ShellExecute function. If lpFile specifies a document file, the flag is simply passed to the associated application. It is up to the application to decide how to handle it.
		/// </summary>
		public int nShow;
		/// <summary>
		/// If the function succeeds, it sets this member to a value greater than 32. If the function fails, it is set to an SE_ERR_XXX error value that indicates the cause of the failure. Although hInstApp is declared as an HINSTANCE for compatibility with 16-bit Windows applications, it is not a true HINSTANCE. It can be cast only to an int and compared to either 32 or the following SE_ERR_XXX error codes.
		/// </summary>
		public int hInstApp;
		/// <summary>
		/// Address of an ITEMIDLIST structure to contain an item identifier list uniquely identifying the file to execute. This member is ignored if the fMask member does not include SEE_MASK_IDLIST or SEE_MASK_INVOKEIDLIST.
		/// </summary>
		public int lpIDList;
		/// <summary>
		/// Address of a null-terminated string that specifies the name of a file class or a globally unique identifier (GUID). This member is ignored if fMask does not include SEE_MASK_CLASSNAME.
		/// </summary>
		public string lpClass;
		/// <summary>
		/// Handle to the registry key for the file class. This member is ignored if fMask does not include SEE_MASK_CLASSKEY.
		/// </summary>
		public int hkeyClass;
		/// <summary>
		/// Hot key to associate with the application. The low-order word is the virtual key code, and the high-order word is a modifier flag (HOTKEYF_). For a list of modifier flags, see the description of the WM_SETHOTKEY message. This member is ignored if fMask does not include SEE_MASK_HOTKEY.
		/// </summary>
		public int dwHotKey;
		/// <summary>
		/// Handle to the icon for the file class. This member is ignored if fMask does not include SEE_MASK_ICON.
		/// </summary>
		public int hIcon;
		/// <summary>
		/// Handle to the newly started application. This member is set on return and is always NULL unless fMask is set to SEE_MASK_NOCLOSEPROCESS. Even if fMask is set to SEE_MASK_NOCLOSEPROCESS, hProcess will be NULL if no process was launched. For example, if a document to be launched is a URL and an instance of Microsoft Internet Explorer is already running, it will display the document. No new process is launched, and hProcess will be NULL.
		/// 				
		/// Note ShellExecuteEx does not always return an hProcess, even if a process is launched as the result of the call. For example, an hProcess does not return when you use SEE_MASK_INVOKEIDLIST to invoke IContextMenu.
		/// </summary>
		public int hProcess;
	}

}
