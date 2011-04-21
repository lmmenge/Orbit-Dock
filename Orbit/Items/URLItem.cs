using System;
using Microsoft.DirectX.Direct3D;
using Orbit.Utilities;

namespace Orbit.Items
{
	/// <summary>
	/// Summary description for URLItem.
	/// </summary>
	public class URLItem:StoredOrbitItem,IOpenItem
	{
		#region Internal Variables
		private string _URL;
		#endregion

		#region Constructor
		private URLItem(){}

		/// <summary>
		/// Creates a new instance of a URL Item
		/// </summary>
		/// <param name="Device">Direct3D Device to load the resources into</param>
		/// <param name="Path">Path to a INI used to create the item from</param>
		public URLItem(Device Device, string Path)
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
							this.URL=Params[1];
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
			System.Diagnostics.ProcessStartInfo ProcInfo=new System.Diagnostics.ProcessStartInfo(URL);
			ProcInfo.UseShellExecute=true;
			//ProcInfo.WorkingDirectory=System.IO.Path.GetDirectoryName(URL);
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

		#region Properties
		/// <summary>
		/// Gets/Sets the URL that this item points to
		/// </summary>
		public string URL
		{
			get
			{
				return _URL;
			}
			set
			{
				if(_URL!=value)
					_URL=value;
			}
		}
		#endregion
	}
}
