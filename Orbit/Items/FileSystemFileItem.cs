using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.DirectX.Direct3D;
using Orbit.Utilities;

namespace Orbit.Items
{
	/// <summary>
	/// An item that represents a file on the disk
	/// </summary>
	public class FileSystemFileItem:PreviewableItem,IOpenItem
	{
		#region Internal Variables
		#region Properties
		private string _Path;
		#endregion

		#region Internal control variables
		#endregion
		#endregion

		#region Constructors
		private FileSystemFileItem(){}

		/// <summary>
		/// Creates a new instance of a File System File Item
		/// </summary>
		/// <param name="Device">Direct3D Device to load the resources into</param>
		/// <param name="Path">Path to a file used to create the item from</param>
		public FileSystemFileItem(Device Device, string Path)
		{
			// checking the values
			ValidateDevice(Device);
			// bail out if file doesn't exist
			if(!System.IO.File.Exists(Path))
				throw new System.IO.FileNotFoundException();

			// settng the basic properties
			display=Device;

			try
			{
				// initialize common resources
				InitializeResources();
				// initialize preview resources
				InitializePreviewResources();

				// load from the file
				LoadFromPhysicalFile(Path);
			}
			catch(Exception)
			{
				throw;
			}
		}
		#endregion

		#region Internal File->Item conversion
		private void LoadFromPhysicalFile(string Path)
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
				//this.ItemPath=Path.Substring(0, Path.Length-(Path.Length-Path.LastIndexOf("\\")))+"\\";

				// setting properties
				// NON EXISTANT IN NEW CLASS
				//this.Line=Line;
				// NON EXISTANT IN NEW CLASS
				//this.ColorKey=Color.FromArgb(0, Color.White);

				// Adding new Texture to registry
				// checking if it has a custom Icon
				string ExtensionImage="";
				//string ToggledExtensionImage="";

				// get the file extension
				string Extension=System.IO.Path.GetExtension(Path);
				// get the filename of the supposed image for this extension
				string ExtensionFileName=Extension.Replace(".", "").ToLower()+".png";
				// get the path for that file
				ExtensionImage=System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), @"extensions\"+ExtensionFileName);

				// if the file exists, assign it to be the image
				// else, load its default filetype icon
				if(System.IO.File.Exists(ExtensionImage))
				{
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
				}
				else
				{
					string textureId = System.IO.Path.GetExtension(Path);
					if(textureId.ToLower()!=".lnk" && OrbitTextureCache.IsInCache(textureId))
					{
						this.SetIcon(OrbitTextureCache.GetReference(textureId));
					}
					else
					{
						try
						{
							// getting the handle to the icon from the SHFileInfo structure
							using(Icon IconO=Icon.FromHandle(FileInfo.hIcon))
							{
								// create the biggest possible icon
								using(Icon icon=new Icon(IconO, 128, 128))
								{
									// convert to bitmap
									using(Bitmap IconPic=Orbit.Utilities.ImageHelper.GetBitmapFromIcon(icon))
									{
										// setting it to be the icon
										this.SetIcon(IconPic);
										//this.SetOverlay(IconPic); // overlay not needed anymore. will use standard icon
									}
								}
							}
						}
						catch(Exception)
						{
							this.CannotLoadIcon();
						}
						if(textureId.ToLower()!=".lnk")
							OrbitTextureCache.InsertInCache(this._Icon, textureId);
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

		#region Public Methods
		/// <summary>
		/// Opens (runs) the file represented by this item
		/// </summary>
		public void Open()
		{
			// try running it with the default .Net class
			System.Diagnostics.ProcessStartInfo ProcInfo=new System.Diagnostics.ProcessStartInfo(Path);
			ProcInfo.UseShellExecute=true;
			ProcInfo.WorkingDirectory=System.IO.Path.GetDirectoryName(Path);
			//if(ItemRegistry[i].Action.IndexOf("\\")>=0)
			//	ProcInfo.WorkingDirectory=ItemRegistry[i].Action.Substring(0, ItemRegistry[i].Action.LastIndexOf("\\"));
			try
			{
				// try starting
				System.Diagnostics.Process.Start(ProcInfo);
			}
			catch(Exception)
			{
				// if failed, try using the Win32 API function
				Win32.Shell32.ShellExecuteInfo ShExInfo=new Win32.Shell32.ShellExecuteInfo();
				ShExInfo.fMask=0x0000000c;
				//ShExInfo.hwnd=(int)this.Handle;
				ShExInfo.hwnd=(int)System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
				ShExInfo.lpFile=ProcInfo.FileName;
				ShExInfo.lpDirectory=ProcInfo.WorkingDirectory;
				ShExInfo.lpParameters=ProcInfo.Arguments;
				ShExInfo.lpVerb="open";
				ShExInfo.nShow=1;
				ShExInfo.cbSize=System.Runtime.InteropServices.Marshal.SizeOf(ShExInfo);
				Win32.Shell32.Shell32API.ShellExecuteEx(ShExInfo);
			}
			ProcInfo=null;
		}

		#endregion

		#region Inherited Methods
		/// <summary>
		/// Gets the file preview image
		/// </summary>
		protected override void GetPreview()
		{
			if(System.IO.File.Exists(this.Path))
			{
				string Extension=System.IO.Path.GetExtension(this.Path).ToLower();
				if(Extension==".png"
					|| Extension==".jpg"
					|| Extension==".gif"
					|| Extension==".bmp"
					|| Extension==".jpeg")
				{
					try
					{
						using(Bitmap IconPic=(Bitmap)Bitmap.FromFile(this.Path))
						{
							// Create texture from final bitmap
							this.SetBackground(IconPic);
						}
						Compose();
					}
					catch(Exception){}
				}
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
	}
}
