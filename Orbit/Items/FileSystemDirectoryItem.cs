using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.DirectX.Direct3D;
using Orbit.Utilities;

namespace Orbit.Items
{
	/// <summary>
	/// An item that represents a directory on the disk
	/// </summary>
	public class FileSystemDirectoryItem:LoaderItem
	{
		#region Internal Variables
		private string _Path;
		#endregion

		#region Constructors
		/// <summary>
		/// Creates a new instance of the FileSystemDirectoryItem class
		/// </summary>
		private FileSystemDirectoryItem(){}

		/// <summary>
		/// Creates a new instance of a File System Directory Item
		/// </summary>
		/// <param name="Device">Direct3D Device to load the resources into</param>
		/// <param name="Path">Path to a directory used to create the item from or to the INI to create the folder from</param>
		public FileSystemDirectoryItem(Device Device, string Path)
		{
			// checking the values
			ValidateDevice(Device);
			// bail out if file doesn't exist
			if(!System.IO.Directory.Exists(Path))
				throw new System.IO.FileNotFoundException();

			// settng the basic properties
			display=Device;

			try
			{
				// initialize common resources
				InitializeResources();

				// initialize our loader resources
				InitializeLoadingResources();

				// set the context menu flags
				this._MenuFlags=ItemMenuFlags.OpenInStart
					| ItemMenuFlags.OpenInExplorer;

				// load from physical folder
				LoadFromPhysicalDirectory(Path);
			}
			catch(Exception)
			{
				throw;
			}
		}
		#endregion

		#region Internal Directory->Item conversion
		private void LoadFromPhysicalDirectory(string Path)
		{
			try
			{
				// Loading item file and creating new item object
				// get extra information on this file that the System.IO.FileInfo doesn't give us
				Win32.Shell32.SHFileInfo FileInfo=new Win32.Shell32.SHFileInfo();
				Win32.Shell32.Shell32API.SHGetFileInfo(Path, 0, ref FileInfo, (uint)System.Runtime.InteropServices.Marshal.SizeOf(FileInfo), Win32.Shell32.ShellGetFileInfoFlags.DisplayName | Win32.Shell32.ShellGetFileInfoFlags.FileTypeName | Win32.Shell32.ShellGetFileInfoFlags.LargeIcon | Win32.Shell32.ShellGetFileInfoFlags.Icon);

				// set the item name to the file name
				this.Name=FileInfo.szDisplayName;
				// set the item description to the file type
				this.Description=FileInfo.szTypeName;
				// keep the path value
				this._Path=Path;
				// NON EXISTANT IN NEW CLASS
				//this.ItemPath=Path;

				// setting properties
				// NON EXISTANT IN NEW CLASS
				//this.Line=Line;
				// NON EXISTATN IN NEW CLASS
				//this.ColorKey=Color.FromArgb(0, Color.White);

				// Adding new Texture to registry
				// checking if it has a custom Icon
				string ExtensionImage="";
				string ToggledExtensionImage="";
				
				ExtensionImage=System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), @"extensions\folder.png");
				ToggledExtensionImage=System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), @"extensions\folder_open.png");
				if(System.IO.File.Exists(ExtensionImage))
				{
					//ImagePath=ExtensionImage;
					if(OrbitTextureCache.IsInCache(ExtensionImage))
					{
						this.SetIcon(OrbitTextureCache.GetReference(ExtensionImage));
					}
					else
					{
						SetIcon(ExtensionImage);
						OrbitTextureCache.InsertInCache(this._Icon, ExtensionImage);
					}
					//SetIcon(ExtensionImage);
					if(System.IO.File.Exists(ToggledExtensionImage))
						//ToggledImagePath=ToggledExtensionImage;
						SetToggledIcon(ToggledExtensionImage);
				}
				else
				{
					try
					{
						// getting the handle to the icon from the SHFileInfo structure
						using(Icon IconO=Icon.FromHandle(FileInfo.hIcon))
						{
							// create the biggest possible icon
							using(Icon icon=new Icon(IconO, 256, 256))
							{
								// convert to bitmap
								using(Bitmap IconPic=Orbit.Utilities.ImageHelper.GetBitmapFromIcon(icon))
								{
									// setting it to be the icon
									this.SetIcon(IconPic);
								}
							}
						}
					}
					catch(Exception)
					{
						this.CannotLoadIcon();
					}
				}
				// don't forget to destroy the handle to the other icon
				Win32.User32.User32API.DestroyIcon(FileInfo.hIcon);
			}
			catch(Exception)
			{
				throw;
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the path to the physical file represented by this item
		/// </summary>
		public string Path
		{
			get
			{
				return _Path;
			}
		}
		#endregion

		#region IEnumeratorItem Members
		/// <summary>
		/// Gets the OrbitItems from this item
		/// </summary>
		/// <returns>An Array of OrbitItems</returns>
		/// <remarks>This will not load thumbnail previews for image files. It's up to the calling method to call the thumbnail acquiring method.</remarks>
		public override OrbitItem[] GetItems()
		{
			// bail out if directory doesn't exist
			if(!System.IO.Directory.Exists(Path))
				return null;

			try
			{
				LoadedPercentage=0.0f;
				// Get directory list
				string[] Dirs=System.IO.Directory.GetDirectories(Path);
				string[] Files=System.IO.Directory.GetFiles(Path);
				
				// checking if all items are accessible
				int i=0;
				int ItemQuantity=0;
				int FilesQuantity=0;
				Microsoft.Win32.RegistryKey Rk=Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced");
				bool ShowHidden=(int)Rk.GetValue("Hidden")==1;
				bool ShowSystem=(int)Rk.GetValue("ShowSuperHidden")==1;
				if(!(ShowHidden && ShowSystem))
				{
					while(i<Dirs.Length)
					{
						System.IO.DirectoryInfo di=new System.IO.DirectoryInfo(Dirs[i]);
						if(this.IsFileSystemItemShown(di.Attributes, ShowSystem, ShowHidden))
							ItemQuantity++;

						i++;
					}
					i=0;
					while(i<Files.Length)
					{
						System.IO.FileInfo fi=new System.IO.FileInfo(Files[i]);
						if(this.IsFileSystemItemShown(fi.Attributes, ShowSystem, ShowHidden))
							FilesQuantity++;
						i++;
					}
				}
				else
				{
					ItemQuantity=Dirs.Length;
					FilesQuantity=Files.Length;
				}
				
				// create an empty item if folder is empty
				if(ItemQuantity+FilesQuantity==0)
				{
					LoadedPercentage=0;
					return new OrbitItem[]{new EmptyItem(display)};
				}

				// allocate room for new items
				OrbitItem[] ItemRegistry=new OrbitItem[ItemQuantity+FilesQuantity];

				// Load item
				i=0;
				int a=0;
				while(i<Dirs.Length)
				{
					try
					{
						if(!(ShowHidden && ShowSystem))
						{
							System.IO.DirectoryInfo di=new System.IO.DirectoryInfo(Dirs[i]);
							if(!this.IsFileSystemItemShown(di.Attributes, ShowSystem, ShowHidden))
							{
								i++;
								continue;
							}
						}
						ItemRegistry[a]=new FileSystemDirectoryItem(display, Dirs[i]);
						ItemRegistry[a].Parent=this.Name;
						LoadedPercentage=(float)(a)/(Dirs.Length+Files.Length);
						OnPaint();
						a++;
					}
					catch(Exception){}
					i++;
				}
				i=0;
				a=0;
				while(i<Files.Length)
				{
					if(!(ShowHidden && ShowSystem))
					{
						System.IO.FileInfo fi=new System.IO.FileInfo(Files[i]);
						if(!this.IsFileSystemItemShown(fi.Attributes, ShowSystem, ShowHidden))
						{
							i++;
							continue;
						}
					}
					ItemRegistry[ItemQuantity+a]=new FileSystemFileItem(display, Files[i]);
					ItemRegistry[ItemQuantity+a].Parent=this.Name;
					LoadedPercentage=(float)(Dirs.Length+a)/(Dirs.Length+Files.Length);
					OnPaint();
					i++;
					a++;
				}

				LoadedPercentage=0;

				return ItemRegistry;
			}
			catch(Exception)
			{
				LoadedPercentage=0;
				return null;
			}
		}
		#endregion

		#region Private Windows Integration Methods
		private bool IsFileSystemItemShown(System.IO.FileAttributes Attributes, bool ShowSystem, bool ShowHidden)
		{
			bool Show=true;
			if((Attributes & System.IO.FileAttributes.Hidden)==System.IO.FileAttributes.Hidden)
			{
				if(ShowHidden)
					Show&=true;
				else
					Show&=false;
			}
			if((Attributes & System.IO.FileAttributes.System)==System.IO.FileAttributes.System)
			{
				if(ShowSystem)
					Show&=true;
				else
					Show&=false;
			}

			return Show;
		}
		#endregion
	}
}
