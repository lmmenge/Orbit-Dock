using System;
using Microsoft.DirectX.Direct3D;

using Orbit.Core;
using Orbit.Utilities;

namespace Orbit.Items
{
	/// <summary>
	/// Summary description for FolderItem.
	/// </summary>
	public class FolderItem:StoredLoaderItem
	{
		#region Constructor
		private FolderItem(){}

		/// <summary>
		/// Creates a new instance of a Folder Item
		/// </summary>
		/// <param name="Device">Direct3D Device to load the resources into</param>
		/// <param name="Path">Path to a INI used to create the item from</param>
		public FolderItem(Device Device, string Path)
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

				// initialize our loader resources
				InitializeLoadingResources();

				// set the context menu flags
				this._MenuFlags=ItemMenuFlags.AddItemToLevel
					| ItemMenuFlags.AddItemToItem
					| ItemMenuFlags.ItemProperties
					| ItemMenuFlags.MenuSeparator
					| ItemMenuFlags.OpenInStart
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
				System.Diagnostics.Debug.WriteLine(_ItemPath);
			}
			catch(Exception)
			{
				throw;
			}
		}
		#endregion

		#region IEnumeratorItem Members
		/// <summary>
		/// Gets the OrbitItems from this item
		/// </summary>
		/// <returns>An Array of OrbitItems</returns>
		public override OrbitItem[] GetItems()
		{
			// bail out if dir no longer exists
			if(!System.IO.Directory.Exists(ItemPath))
				return null;

			try
			{		
				LoadedPercentage=0.0f;
				// Get directory list
				string[] Dirs=System.IO.Directory.GetDirectories(ItemPath);
				int i=0;

				// find out how many of those directories are items
				int ItemQuantity=0;
				while(i<Dirs.Length)
				{
					if(System.IO.File.Exists(System.IO.Path.Combine(Dirs[i], "item.ini")))
					{
						ItemQuantity++;
					}
					i++;
				}
				// if there are no items, create an empty item
				if(i==0)
				{
					LoadedPercentage=0;
					return new OrbitItem[]{new EmptyItem(display)};
				}

				// allocate space
				OrbitItem[] ItemRegistry=new OrbitItem[ItemQuantity];

				// Load item if there's an item configuration
				i=0;
				int s=0;
				while(i<Dirs.Length)
				{
					if(System.IO.File.Exists(Dirs[i]+"\\item.ini"))
					{
						// if has an ini
						// load the item
						ItemRegistry[i-s]=OrbitItemLoader.FromIni(display, System.IO.Path.Combine(Dirs[i], "item.ini"));
						// set the parent name
						if(ItemRegistry[i-s]!=null)
						{
							ItemRegistry[i-s].Parent=this.Name;
							ItemRegistry[i-s].Line=0;
						}
					} 
					else
					{
						// if not, take note that yet another index is NOT an item
						s++;
					}
					LoadedPercentage=(float)i/Dirs.Length;
					OnPaint();
					i++;
				}

				LoadedPercentage=0;

				// return the just loaded array
				return ItemRegistry;
			}
			catch(Exception)
			{
				// return null if failed
				LoadedPercentage=0;
				return null;
			}
		}

		#endregion
	}
}
