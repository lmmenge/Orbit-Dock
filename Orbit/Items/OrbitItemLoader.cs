using System;
using Microsoft.DirectX.Direct3D;

namespace Orbit.Items
{
	/// <summary>
	/// Provides means of loading physically-stored Orbit items
	/// </summary>
	public class OrbitItemLoader
	{
		/// <summary>
		/// Gets the type of an item from an INI
		/// </summary>
		/// <param name="Path">Path to the item's INI</param>
		/// <returns>Type object with the item's type</returns>
		public static Type GetType(string Path)
		{
			// bail out if file doesn't exist
			if(!System.IO.File.Exists(Path))
				return null;

			// parse item and find out its action
			try
			{
				string Action="";
				// Loading item file and creating new item object
				System.IO.StreamReader iFile=new System.IO.StreamReader(Path);
				while (iFile.Peek()>=0)
				{
					// parse
					string[] Params=iFile.ReadLine().Split(new char[]{char.Parse("=")}, 2);
					switch (Params[0].ToLower())
					{
						case "action":
							Action=Params[1];
							break;
					}
				}
				iFile.Close();

				// find out from the action, which item this is
				switch(Action)
				{
					case "[task]":
						// TaskItem
						return typeof(TaskItem);
					case "[empty]":
						// EmptyItem
						return typeof(EmptyItem);
					case "[dir]":
						// FolderItem
						return typeof(FolderItem);
					case "[physicaldir]":
						// FileSystemDirectoryItem
						return typeof(FileSystemFolderItem);
					case "[tasks]":
						// TasksFolderItem
						return typeof(TasksFolderItem);
					case "[configuration]":
						// ConfigurationItem
						return typeof(ConfigurationItem);
					default:
						// One of the shortcut items
						// needs to parse the action to find out

						// if it's an URL
						if(Action.IndexOf(@"://")>=0)
							return typeof(URLItem);

						// if it's a shortcut
						if(Action.IndexOf(@":\")>=0)
							return typeof(ShortcutItem);
						break;
				}
				// couldn't find the item type
				// assume to be shortcut (shortcuts may not have :\ in them)
				return typeof(ShortcutItem);
			}
			catch(Exception)
			{
				// couldn't open file
				return null;
			}
		}

		/// <summary>
		/// Loads an OrbitItem off a INI file
		/// </summary>
		/// <param name="Device">Direct3D Device to load the resources into</param>
		/// <param name="Path">Path to the INI file to load this item from</param>
		/// <returns>An OrbitItem object corresponding to the INI file</returns>
		public static OrbitItem FromIni(Device Device, string Path)
		{
			try
			{
				Type type = GetType(Path);
				if(type==typeof(Orbit.Items.ShortcutItem))
				{
					return new ShortcutItem(Device, Path);
				}
				else if(type==typeof(Orbit.Items.URLItem))
				{
					return new URLItem(Device, Path);
				}
				else if(type==typeof(Orbit.Items.FolderItem))
				{
					return new FolderItem(Device, Path);
				}
				else if(type==typeof(Orbit.Items.TasksFolderItem))
				{
					return new TasksFolderItem(Device, Path);
				}
				else if(type==typeof(Orbit.Items.ConfigurationItem))
				{
					return new ConfigurationItem(Device, Path);
				}
				else if(type==typeof(Orbit.Items.FileSystemFolderItem))
				{
					return new FileSystemFolderItem(Device, Path);
				}
			}
			catch(Exception){}
			// if it got here, then it's because the item loaded to fail
			return null;
		}
		/// <summary>
		/// Loads the first loop
		/// </summary>
		/// <param name="Device">Direct3D Device to load the resources into</param>
		/// <param name="Path">Path to the INI file to load this item from</param>
		/// <returns>An OrbitItem object corresponding to the root loop</returns>
		public static OrbitItem[] LoadOrbitFolder(Device Device, string Path)
		{
			// bail out if dir no longer exists
			if(!System.IO.Directory.Exists(Path))
				return null;

			try
			{				
				// Get directory list
				string[] Dirs=System.IO.Directory.GetDirectories(Path);
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
					return null;

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
						ItemRegistry[i-s]=OrbitItemLoader.FromIni(Device, System.IO.Path.Combine(Dirs[i], "item.ini"));
						// set the parent name
						if(ItemRegistry[i-s]!=null)
						{
							ItemRegistry[i-s].Parent="Orbit";
							ItemRegistry[i-s].Line=0;
						}
					} 
					else
					{
						// if not, take note that yet another index is NOT an item
						s++;
					}
					i++;
				}

				// return the just loaded array
				return ItemRegistry;
			}
			catch(Exception)
			{
				// return null if failed
				return null;
			}
		}
	}
}
