using System;
using Microsoft.DirectX.Direct3D;
using Orbit.Utilities;

namespace Orbit.Items
{
	/// <summary>
	/// Summary description for ShortcutItem.
	/// </summary>
	public class ShortcutItem:StoredOrbitItem,IOpenItem
	{
		#region Internal Variables
		private string _Path;
		private string _Arguments;
		#endregion

		#region Constructor
		private ShortcutItem(){}

		/// <summary>
		/// Creates a new instance of a URL Item
		/// </summary>
		/// <param name="Device">Direct3D Device to load the resources into</param>
		/// <param name="Path">Path to the INI file to load this item from</param>
		public ShortcutItem(Device Device, string Path)
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

				// set the context menu flags
				this._MenuFlags=ItemMenuFlags.AddItemToLevel
					| ItemMenuFlags.ItemProperties
					| ItemMenuFlags.MenuSeparator
					| ItemMenuFlags.RemoveItem;

				// load from the file
				LoadFromIni(Path);
			}
			catch(Exception)
			{
				throw;
			}
		}
		#endregion

		#region Internal INI->Item Conversion
		private void LoadFromIni(string Path)
		{
			try
			{
				// Loading item file and creating new item object
				System.IO.StreamReader iFile=new System.IO.StreamReader(Path);
				while (iFile.Peek()>=0)
				{
					// parse
					string[] Params=iFile.ReadLine().Split(new char[]{char.Parse("=")}, 2);
					switch (Params[0].ToLower())
					{
						case "name":
							this.Name=Params[1];
							break;
						case "image":
							SetIcon(Params[1]);
							break;
						case "toggleimage":
							SetToggledIcon(Params[1]);
							break;
						case "hoverimage":
							SetHoverIcon(Params[1]);
							break;
						case "action":
							this.Path=Params[1];
							break;
						case "args":
							this.Arguments=Params[1];
							break;
						case "runandleave":
							this.RunAndLeave=bool.Parse(Params[1]);
							break;
						case "description":
							this.Description=Params[1];
							break;
					}
				}
				iFile.Close();
				// set properties
				//this.ItemPath=Path.Substring(0,Path.Length-(Path.Length-Path.LastIndexOf("\\")))+"\\";
				this._ItemPath=System.IO.Path.GetDirectoryName(Path);
			}
			catch(Exception)
			{
				throw;
			}
		}
		#endregion

		#region Methods
		/// <summary>
		/// Opens (runs) the file represented by this item
		/// </summary>
		public void Open()
		{
			// try running it with the default .Net class
			System.Diagnostics.ProcessStartInfo ProcInfo=new System.Diagnostics.ProcessStartInfo(Path, Arguments);
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
				try
				{
					// if failed, try using the Win32 API function
					Win32.Shell32.ShellExecuteInfo ShExInfo=new Win32.Shell32.ShellExecuteInfo();
					ShExInfo.fMask=0x0000000c;
					//ShExInfo.hwnd=(int)this.Handle;
					ShExInfo.hwnd=(int)System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
					ShExInfo.lpFile=ProcInfo.FileName;
					ShExInfo.lpParameters=ProcInfo.Arguments;
					ShExInfo.lpDirectory=ProcInfo.WorkingDirectory;
					ShExInfo.lpParameters=ProcInfo.Arguments;
					ShExInfo.lpVerb="open";
					ShExInfo.nShow=1;
					ShExInfo.cbSize=System.Runtime.InteropServices.Marshal.SizeOf(ShExInfo);
					Win32.Shell32.Shell32API.ShellExecuteEx(ShExInfo);
				}
				catch(Exception){}
			}
			ProcInfo=null;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets/Sets the path that this item points to
		/// </summary>
		public string Path
		{
			get
			{
				return _Path;
			}
			set
			{
				if(_Path!=value)
					_Path=value;
			}
		}
		/// <summary>
		/// Gets/Sets the arguments with which to run this shortcut
		/// </summary>
		public string Arguments
		{
			get
			{
				return _Arguments;
			}
			set
			{
				if(_Arguments!=value)
					_Arguments=value;
			}
		}
		#endregion
	}
}
