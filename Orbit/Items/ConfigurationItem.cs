using System;
using Microsoft.DirectX.Direct3D;

using Orbit.Core;

namespace Orbit.Items
{
	/// <summary>
	/// Summary description for ConfigurationItem.
	/// </summary>
	public class ConfigurationItem:StoredOrbitItem
	{
		#region Constructors
		private ConfigurationItem(){}

		/// <summary>
		/// Creates a new instance of a configuration Item
		/// </summary>
		/// <param name="Device">Direct3D Device to load the resources into</param>
		/// <param name="Path">Path to a INI used to create the item</param>
		public ConfigurationItem(Device Device, string Path)
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
	}
}
