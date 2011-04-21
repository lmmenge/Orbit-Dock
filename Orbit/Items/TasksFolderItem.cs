using System;
using Microsoft.DirectX.Direct3D;
using Orbit.Utilities;

namespace Orbit.Items
{
	/// <summary>
	/// Summary description for FolderItem.
	/// </summary>
	public class TasksFolderItem:StoredOrbitItem, IEnumeratorItem
	{
		#region Constructor
		private TasksFolderItem(){}

		/// <summary>
		/// Creates a new instance of a URL Item
		/// </summary>
		/// <param name="Device">Direct3D Device to load the resources into</param>
		/// <param name="Path">Path to a INI used to create the item from</param>
		public TasksFolderItem(Device Device, string Path)
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
			}
			catch(Exception)
			{
				throw;
			}
		}
		#endregion

		#region IEnumeratorItem Members
		/// <summary>
		/// Gets an array of OrbitItems representing the running tasks
		/// </summary>
		/// <returns>Array of OrbitItems</returns>
		public OrbitItem[] GetItems()
		{
			/*ExcludedWindow[] ExclusionList=LoadExcludedClassWindows();
			IntPtr[] TasksList=WindowsTaskManager.GetWindowHandles(ExclusionList);
			if(TasksList==null)
				return new OrbitItem[]{new EmptyItem(display, MagnifiedSize)};

			// actually catalog them
			OrbitItem[] ItemRegistry=new OrbitItem[TasksList.Length];

			int a=0;
			foreach(IntPtr Handle in TasksList)
			{
				ItemRegistry[a]=new TaskItem(display, MagnifiedSize, new WindowInformation(WindowsTaskManager.GetWindowText(Handle), Handle));
				if(ItemRegistry[a]!=null)
				{
					ItemRegistry[a].Parent=this.Name;
					ItemRegistry[a].Line=0;
				}
				a++;
			}

			return ItemRegistry;*/
			return GetTasks(display);
		}
		#endregion

		#region Public Static Methods
		/// <summary>
		/// Gets all the running tasks as OrbitItems
		/// </summary>
		/// <param name="device">Direct3D Device to load resources to</param>
		/// <returns>An array of OrbitItems</returns>
		public static OrbitItem[] GetTasks(Device device)
		{
			ExcludedWindow[] ExclusionList=LoadExcludedClassWindows();
			IntPtr[] TasksList=WindowsTaskManager.GetWindowHandles(ExclusionList);
			if(TasksList==null)
				return new OrbitItem[]{new EmptyItem(device)};

			// actually catalog them
			OrbitItem[] ItemRegistry=new OrbitItem[TasksList.Length];

			int a=0;
			foreach(IntPtr Handle in TasksList)
			{
				ItemRegistry[a]=new TaskItem(device, new WindowInformation(WindowsTaskManager.GetWindowText(Handle), Handle));
				if(ItemRegistry[a]!=null)
				{
					ItemRegistry[a].Parent="Orbit";
					ItemRegistry[a].Line=0;
				}
				a++;
			}

			return ItemRegistry;
		}
		#endregion

		#region Private Methods
		private static ExcludedWindow[] LoadExcludedClassWindows()
		{
			// create orbit's own excluded window
			ExcludedWindow me=new ExcludedWindow("WindowsForms10.Window.8.app2","Orbit.exe");

			// load the list
			ExcludedWindow[] CustomExcludeList=WindowsTaskManager.LoadExcludedClassWindows(Orbit.Configuration.ConfigurationInfo.LocationsConfig.GetExcludedTasksFilePath());

			if(CustomExcludeList==null)
				return new ExcludedWindow[]{me};

			// set our new list
			ExcludedWindow[] ExcludeList=new ExcludedWindow[CustomExcludeList.Length+1];
			// set our mandatory excluded tasks

			// excluding all orbit windows
			ExcludeList[0]=me;

			// copy our custom exclusion list
			int i=0;
			while(i<CustomExcludeList.Length)
			{
				ExcludeList[i+1]=CustomExcludeList[i];
				i++;
			}

			return ExcludeList;
		}
		#endregion
	}
}
