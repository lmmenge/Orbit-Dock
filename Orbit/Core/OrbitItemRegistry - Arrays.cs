using System;
using Microsoft.DirectX.Direct3D;

using Orbit.Items;
using Orbit.Configuration;

namespace Orbit.Core
{
	/// <summary>
	/// Encapsulates a full OrbitItem Registry
	/// </summary>
	public class OrbitItemRegistry
	{
		#region Variables
		#region Managed Variables
		private OrbitItem[] ItemRegistry;
		private LoopInfo[] LineRegistry;
		#endregion
		#endregion

		#region Constructor
		/// <summary>
		/// Creates a new instance of the OrbitItemRegistry class
		/// </summary>
		public OrbitItemRegistry()
		{
			ItemRegistry=new OrbitItem[0];
			LineRegistry=new LoopInfo[0];
		}
		#endregion

		#region Private Methods
		#region Item Registry
		/// <summary>
		/// Allocates space on the registry for an item and returns the index of the first allocated item
		/// </summary>
		/// <param name="amount">Amount of items to allocate</param>
		private int AllocateItem(int amount)
		{
			try
			{
				// Resizing ItemRegistry to fit a new Item
				OrbitItem[] RegBak = ItemRegistry;
				ItemRegistry=new OrbitItem[RegBak.Length+amount];
				int i=0;
				while(i<RegBak.Length)
				{
					ItemRegistry[i]=RegBak[i];
					i++;
				}
				int place=RegBak.Length;
				// clean up and leave
				RegBak=null;
				return place;
			}
			catch(Exception)
			{
				throw;
			}
		}

		#endregion

		#region Line Registry
		private void AllocateLine()
		{
			LoopInfo[] LineBak=this.LineRegistry;
			this.LineRegistry=new LoopInfo[LineBak.Length+1];
			int i=0;
			while (i<LineBak.Length)
			{
				this.LineRegistry[i]=LineBak[i];
				i++;
			}
			//this.LineRegistry[LineBak.Length].OpenIndex=-1;
			this.LineRegistry[LineBak.Length].StartIndex=0;
			this.LineRegistry[LineBak.Length].RotatedDegrees=0;
			this.LineRegistry[LineBak.Length].ShowsMoreIndicator=false;;

			// the line quantity change is done in the RegisterNewLine() method.
			// this is because only after that method is done is when the line's
			// properties are filled
		}
		private void DeleteLine()
		{
			if(this.LineRegistry.Length>0)
			{
				LoopInfo[] LineBak=this.LineRegistry;
				this.LineRegistry=new LoopInfo[LineBak.Length-1];
				int i=0;
				while (i<this.LineRegistry.Length)
				{
					this.LineRegistry[i]=LineBak[i];
					i++;
				}
			}
			// notify line quantity change
			if(this.LineQuantityChanged!=null)this.LineQuantityChanged(this, new EventArgs());
		}
		private void RegisterNewLine(int ItemQuantity, string LevelPath, double ParentRotationOffset)
		{
			// registering new line
			this.AllocateLine();

			#region Old Code
			/*// quantity shown
			double FirstRadius;
			double ThisItemsRadius;
			//int MaxVisibleItems;
			int VisibleItems;
			int RotatedDegrees;

			if(this.LineRegistry.Length>1)
			{
				// use first radius as reference
				FirstRadius=this.LineRegistry[0].AbsoluteRadius;
				// Get our relative radius
				ThisItemsRadius=(this.LineRegistry.Length-1)*(Global.Configuration.Runtime.LoopDistance+Global.Configuration.Runtime.IconSizeAverage);
				// Maximum visible items on what depends on loop size
				// we need this value beforehand here
				int MaxVisibleItems=Convert.ToInt32(((FirstRadius+ThisItemsRadius)*Math.Sqrt(8*Math.PI))/Global.Configuration.Runtime.IconSizeAverage);

				// find out how many items we should allow
				if(ItemQuantity>Global.Configuration.Appearance.ItemsShownPerLine && Global.Configuration.Appearance.ItemsShownPerLine!=0)
				{
					VisibleItems=Global.Configuration.Appearance.ItemsShownPerLine;
				}
				else
				{
					if(ItemQuantity>MaxVisibleItems)
						VisibleItems=MaxVisibleItems-1;
					else
						VisibleItems=ItemQuantity;
				}

				// find out our rotation
				double ThisLoopRotation=360/(((FirstRadius+ThisItemsRadius)*Math.Sqrt(8.0*Math.PI))/(Global.Configuration.Runtime.IconSizeAverage))*(VisibleItems-1);
				RotatedDegrees=Convert.ToInt32(ParentRotationOffset/Math.PI*180+ThisLoopRotation/2);
			}
			else
			{
				// find out how many items we should allow
				if(ItemQuantity>Global.Configuration.Runtime.MaxItemsShownInFirstLoop)
				{
					VisibleItems=Global.Configuration.Runtime.MaxItemsShownInFirstLoop;
					// get the radius of the first loop
					FirstRadius=((VisibleItems+1)*Global.Configuration.Runtime.IconSizeAverage)/Math.Sqrt(8.0*Math.PI);
				}
				else
				{
					VisibleItems=ItemQuantity;
					// get the radius of the first loop
					FirstRadius=(VisibleItems*Global.Configuration.Runtime.IconSizeAverage)/Math.Sqrt(8.0*Math.PI);
				}
				// get our relative radius
				ThisItemsRadius=0;
                
				// find our rotation
				RotatedDegrees=0;
			}

			// set it
			this.LineRegistry[this.LineRegistry.Length-1].RelativeRadius=ThisItemsRadius;
			this.LineRegistry[this.LineRegistry.Length-1].AbsoluteRadius=ThisItemsRadius+FirstRadius;
			this.LineRegistry[this.LineRegistry.Length-1].MaxVisibleItems=Convert.ToInt32(((FirstRadius+ThisItemsRadius)*Math.Sqrt(8*Math.PI))/Global.Configuration.Runtime.IconSizeAverage);
			this.LineRegistry[this.LineRegistry.Length-1].VisibleItems=VisibleItems;
			this.LineRegistry[this.LineRegistry.Length-1].RotatedDegrees=RotatedDegrees;*/
			#endregion

			// update the count
			this.LineRegistry[this.LineRegistry.Length-1].Count=ItemQuantity;
			this.LineRegistry[this.LineRegistry.Length-1].LevelPath=LevelPath;
			System.Diagnostics.Debug.WriteLine("New Line with level: "+LevelPath);
			UpdateLineMetrics(this.LineRegistry.Length-1);

			// find out our rotation
			if(this.LineRegistry.Length>1)
			{
				//double ThisLoopRotation=360/(((LineRegistry[0].AbsoluteRadius+LineRegistry[LineRegistry.Length-1].RelativeRadius)*Math.Sqrt(8.0*Math.PI))/(Global.Configuration.Runtime.IconSizeAverage))*(LineRegistry[LineRegistry.Length-1].VisibleItems-1);
				double ThisLoopRotation=360/((LineRegistry[LineRegistry.Length-1].AbsoluteRadius*Math.Sqrt(8.0*Math.PI))/(Global.Configuration.Runtime.IconSizeAverage))*(LineRegistry[LineRegistry.Length-1].VisibleItems-1);
				this.LineRegistry[this.LineRegistry.Length-1].RotatedDegrees=Convert.ToInt32(ParentRotationOffset/Math.PI*180+ThisLoopRotation/2);
			}
			else
			{
				this.LineRegistry[this.LineRegistry.Length-1].RotatedDegrees=0;
			}

			// register this as current new level if it has
			Global.Configuration.Runtime.CurrentLevel=LevelPath;

			if(this.LineQuantityChanged!=null)this.LineQuantityChanged(this, new EventArgs());
		}
		private void UpdateLineMetrics(int line)
		{
			// quantity shown
			double FirstRadius;
			double ThisItemsRadius;
			//int MaxVisibleItems;
			int VisibleItems;
			//int RotatedDegrees;

			if(this.LineRegistry.Length>1)
			{
				// use first radius as reference
				FirstRadius=this.LineRegistry[0].AbsoluteRadius;
				// Get our relative radius
				ThisItemsRadius=(this.LineRegistry.Length-1)*(Global.Configuration.Runtime.LoopDistance+Global.Configuration.Runtime.IconSizeAverage);
				// Maximum visible items on what depends on loop size
				// we need this value beforehand here
				int MaxVisibleItems=Convert.ToInt32(((FirstRadius+ThisItemsRadius)*Math.Sqrt(8*Math.PI))/Global.Configuration.Runtime.IconSizeAverage);

				// find out how many items we should allow
				if(LineRegistry[line].Count>Global.Configuration.Appearance.ItemsShownPerLine && Global.Configuration.Appearance.ItemsShownPerLine!=0)
				{
					VisibleItems=Global.Configuration.Appearance.ItemsShownPerLine;
				}
				else
				{
					if(LineRegistry[line].Count>MaxVisibleItems)
						VisibleItems=MaxVisibleItems-1;
					else
						VisibleItems=LineRegistry[line].Count;
				}

				// find out our rotation
				//double ThisLoopRotation=360/(((FirstRadius+ThisItemsRadius)*Math.Sqrt(8.0*Math.PI))/(Global.Configuration.Runtime.IconSizeAverage))*(VisibleItems-1);
				//RotatedDegrees=Convert.ToInt32(parentRotationOffset/Math.PI*180+ThisLoopRotation/2);
			}
			else
			{
				// find out how many items we should allow
				if(LineRegistry[line].Count>Global.Configuration.Runtime.MaxItemsShownInFirstLoop)
				{
					VisibleItems=Global.Configuration.Runtime.MaxItemsShownInFirstLoop;
					// get the radius of the first loop
					FirstRadius=((VisibleItems+1)*Global.Configuration.Runtime.IconSizeAverage)/Math.Sqrt(8.0*Math.PI);
				}
				else
				{
					VisibleItems=LineRegistry[line].Count;
					// get the radius of the first loop
					FirstRadius=(VisibleItems*Global.Configuration.Runtime.IconSizeAverage)/Math.Sqrt(8.0*Math.PI);
				}
				// get our relative radius
				ThisItemsRadius=0;
                
				// find our rotation
				//RotatedDegrees=0;
			}

			// set it
			this.LineRegistry[this.LineRegistry.Length-1].RelativeRadius=ThisItemsRadius;
			this.LineRegistry[this.LineRegistry.Length-1].AbsoluteRadius=ThisItemsRadius+FirstRadius;
			this.LineRegistry[this.LineRegistry.Length-1].MaxVisibleItems=Convert.ToInt32(((FirstRadius+ThisItemsRadius)*Math.Sqrt(8*Math.PI))/Global.Configuration.Runtime.IconSizeAverage);
			this.LineRegistry[this.LineRegistry.Length-1].VisibleItems=VisibleItems;
			//this.LineRegistry[this.LineRegistry.Length-1].RotatedDegrees=RotatedDegrees;
			//this.LineRegistry[this.LineRegistry.Length-1].Count=itemQuantity;
		}
		#endregion
		#endregion

		#region Public Methods
		#region Removal Methods
		/// <summary>
		///	Unloads an item (for internal use)
		/// </summary>
		/// <param name="index">Index of the OrbitItem to unload</param>
		public void Remove(int index)
		{
			try
			{
				// update line metrics
				LineRegistry[ItemRegistry[index].Line].Count--;
				UpdateLineMetrics(ItemRegistry[index].Line); 

				// dispose the item
				if(ItemRegistry[index]!=null)
					ItemRegistry[index].Dispose();
				// Resize Item index
				OrbitItem[] RegBak=ItemRegistry;
				ItemRegistry=new OrbitItem[RegBak.Length-1];
				// update the registry
				int i=0;
				int a=0;
				while(i<RegBak.Length)
				{
					// if you're not the removed item, you go to the new item registry
					if(i!=index)
					{
						ItemRegistry[a]=RegBak[i];
						a++;
					}
					i++;
				}
			}
			catch(Exception)
			{
				throw;
			}
		}


		/// <summary>
		/// Unloads/removes a line (a directory)
		/// </summary>
		/// <param name="line">Line to unload until</param>
		/// <remarks>All lines above that specified line's index will also be unloaded too</remarks>
		public void RemoveLine(int line)
		{
			try
			{
				while(this.LineRegistry.Length>line)
				{
					int i=0;
					int a=0;
					int[] items=new int[Count(this.LineRegistry.Length-1)];
					bool FoundAny=false;
					while (i<ItemRegistry.Length)
					{
						if(ItemRegistry[i].Line==this.LineRegistry.Length-1)
						{
							FoundAny=true;
							items[a]=i;
							a++;
						}
						i++;
					}
					if(FoundAny)
					{
						i=(items.Length-1);
						while(i>=0)
						{
							this.Remove(items[i]);
							i--;
						}
					}
					if(LineRegistry[this.LineRegistry.Length-1].TS!=null)
						LineRegistry[this.LineRegistry.Length-1].TS.Dispose();
					this.DeleteLine();
				}
			}
			catch(Exception)
			{
				throw;
			}
		}

		#endregion

		#region Adding Methods
		/// <summary>
		/// Loads a folder in the hard drive as the root line
		/// </summary>
		/// <param name="path">Path to load items from</param>
		/// <param name="device">Direct3D Device to use to load the root items</param>
		public void CreateRoot(string path, Device device)
		{
			try
			{
				// make sure we're empty
				this.RemoveLine(0);

				if(Global.Configuration.Behavior.ShowTasksOnly)
				{
					// load just tasks :P
					// this is just for testing as a task switcher :P just joking hehehe
					ItemRegistry=TasksFolderItem.GetTasks(device);

					// take screenshots if available and selected
					if(Global.Configuration.Appearance.ShowImageThumbnails)
					{
						ThumbnailSync TS=new ThumbnailSync(ItemRegistry, 0);
					}
				}
				else
				{
					// load new
					ItemRegistry=OrbitItemLoader.LoadOrbitFolder(device, path);
				}				

				foreach(OrbitItem item in ItemRegistry)
				{
					item.Paint+=new EventHandler(PreviewableItem_Paint);
				}

				RegisterNewLine(ItemRegistry.Length, Global.Configuration.Locations.ItemsPath, 0);
			}
			catch(Exception)
			{
				throw;
			}
		}


		/// <summary>
		/// Loads an OrbitItem array as the root line
		/// </summary>
		/// <param name="items">OrbitItem array containing the new root items</param>
		/// <param name="originatingPath">The path to the folder that contains these items</param>
		public void CreateRoot(OrbitItem[] items, string originatingPath)
		{
			try
			{
				// make sure we're empty
				this.RemoveLine(0);

				foreach(OrbitItem item in items)
				{
					item.Paint+=new EventHandler(PreviewableItem_Paint);
				}

				// load
				ItemRegistry=items;
				RegisterNewLine(ItemRegistry.Length, originatingPath, 0);
			}
			catch(Exception)
			{
				throw;
			}
		}
		
		/// <summary>
		/// Loads a dir (a line) resizing the registry only once and filling the items
		/// </summary>
		/// <param name="parentItem">Item in the registry to expand the registry from</param>
		public void ExpandFrom(OrbitItem parentItem)
		{
			// bail out if not the right type of item
			// TODO: block non IEnumeratorItem objects
			//if(!ParentItem.GetType().i)
			//throw new InvalidCastException("Not IEnumeratorItem");

			try
			{
				// get the items for that folder
				OrbitItem[] NewItems=((IEnumeratorItem)parentItem).GetItems();

				// bail out if returned null
				if(NewItems==null)
					return;

				// allocate space and move to that spot
				int AllocOffset=AllocateItem(NewItems.Length);

				// add the new items to the registry
				int i=0;
				while(i<NewItems.Length)
				{
					// set some properties
					NewItems[i].Line=this.LineRegistry.Length;
					NewItems[i].Parent=parentItem.Name;
					NewItems[i].Paint+=new EventHandler(PreviewableItem_Paint);

					// add
					ItemRegistry[AllocOffset+i]=NewItems[i];
					i++;
				}
				
				// registering new line
				// find out the next level
				// task folder items don't
				string Path=Global.Configuration.Locations.ItemsPath;
				if(parentItem is StoredLoaderItem || parentItem is FileSystemDirectoryItem)
				{
					// get the path
					if(parentItem.GetType()==typeof(FolderItem))
						Path=((StoredOrbitItem)parentItem).ItemPath;
					/*if(parentItem.GetType()==typeof(TasksFolderItem))
						Path=Global.Configuration.Runtime.CurrentLevel;*/

					// assign the level
					if(Path.LastIndexOf("\\")==Path.Length-1)
						Global.Configuration.Runtime.CurrentLevel=Path.Substring(0, Path.Length-1);
					else
						Global.Configuration.Runtime.CurrentLevel=Path;
				}
				this.RegisterNewLine(NewItems.Length, Path, parentItem.RotationOffset);

				// take screenshots if available and selected
				if(Global.Configuration.Appearance.ShowImageThumbnails)
				{
					ThumbnailSync TS=new ThumbnailSync(NewItems, this.LineRegistry.Length-1);
				}
			}
			catch(Exception)
			{
				throw;
			}
		}

		#endregion

		#region Utility Methods
		/// <summary>
		/// Returns how many items are there in a line
		/// </summary>
		/// <param name="line">Line to find items</param>
		/// <returns>Amount of items in that line</returns>
		public int Count(int line)
		{
			return LineRegistry[line].Count;
			/*int i=0;
			int qtd=0;
			while(i<ItemRegistry.Length)
			{
				if(ItemRegistry[i]!=null && ItemRegistry[i].Line==line)
					qtd++;
				i++;
			}
			return qtd;*/
		}

		
		/// <summary>
		/// Returns the index of an item in the registry
		/// </summary>
		/// <param name="item">Item to be found</param>
		/// <returns>The index of the OrbitItem in the registry</returns>
		public int IndexOf(OrbitItem item)
		{
			int a=-1;
			if(item==null)
				return a;

			int i=0;
			while (i<ItemRegistry.Length)
			{
				//if(ItemRegistry[i].Equals(item))
				if(ItemRegistry[i]==item)
				{
					a=i;
					break;
				}
				i++;
			}
			return a;
		}

		
		/// <summary>
		/// Returns the currently selected item index
		/// </summary>
		/// <returns>The index of the currently selected item. -1 if no item is selected</returns>
		public int GetSelectedItem()
		{
			int i=0;
			while (i<ItemRegistry.Length)
			{
				if(ItemRegistry[i]!=null && ItemRegistry[i].IsMouseOver) // TODO: Verify WHY this null item is here in the first place
				{
					return i;
				}
				i++;
			}
			return -1;
		}


		/// <summary>
		/// Returns toggled item from a line
		/// </summary>
		/// <param name="line">Line to find the toggled item from</param>
		/// <returns>The index of the currently toggled item in that line. -1 if no item is toggled.</returns>
		public int GetToggledItem(int line)
		{
			int i=0;
			int a=-1;
			while (i<ItemRegistry.Length)
			{
				if(ItemRegistry[i].Line==line && ItemRegistry[i].IsToggled)
				{
					a=i;
					break;
				}
				i++;
			}
			return a;
		}

		
		/// <summary>
		/// Sets the toggled item on a line
		/// </summary>
		/// <param name="itemIndex">Index of the item to be toggled</param>
		/// <remarks>The previous toggled item in the line where this item belongs to will be untoggled automatically</remarks>
		public void SetToggledItem(int itemIndex)
		{
			int PreviousToggle=this.GetToggledItem(ItemRegistry[itemIndex].Line);
			if(PreviousToggle>=0)
			{
				ItemRegistry[PreviousToggle].IsToggled=false;
			}
			ItemRegistry[itemIndex].IsToggled=true;
		}
		#endregion
		#endregion

		#region Operators
		/// <summary>
		/// Gets an item in the registry
		/// </summary>
		public OrbitItem this[long index]
		{
			get
			{
				return ItemRegistry[index];
			}
			set
			{
				ItemRegistry[index]=value;
			}
		}
		#endregion

		#region Private Event Handlers
		private void PreviewableItem_Paint(object sender, EventArgs e)
		{
			if(this.Paint!=null)this.Paint(this, new EventArgs());
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the amount if items in the registry
		/// </summary>
		public int Length
		{
			get
			{
				return ItemRegistry.Length;
			}
		}
		/// <summary>
		/// Gets the information on the line registry
		/// </summary>
		public LoopInfo[] Lines
		{
			get
			{
				return LineRegistry;
			}
		}

		/// <summary>
		/// Gets the OrbitItems contained in this object
		/// </summary>
		/// <remarks>This can be also accessed via the OrbitItemRegistry[] operator</remarks>
		public OrbitItem[] Items
		{
			get
			{
				return ItemRegistry;
			}
		}
		#endregion

		#region Events
		/// <summary>
		/// Occurs when the amount of lines in the registry has changed. 
		/// This happens when you load/unload a line
		/// </summary>
		public event EventHandler LineQuantityChanged;
		/// <summary>
		/// Occurs when the thumbnail thread requests a paint event
		/// </summary>
		public event EventHandler Paint;
		#endregion
	}
}