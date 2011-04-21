using System;

namespace Orbit.Items
{
	/// <summary>
	/// This item represents an OrbitItem that's been loaded from the hard disk (INI)
	/// </summary>
	public abstract class StoredOrbitItem:OrbitItem
	{
		#region Internal Variables
		#region Properties
		/// <summary>
		/// The path to the folder where the item was loaded from
		/// </summary>
		protected string _ItemPath;
		#endregion
		#endregion

		#region Properties
		/// <summary>
		/// Gets the path where the virtual item is stored
		/// </summary>
		public string ItemPath
		{
			get
			{
				return _ItemPath;
			}
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Tells if an item is stored in a folder or one of its subfolders
		/// </summary>
		/// <param name="Path">Path to check if the item is inside</param>
		/// <returns>True if item is inside that folder</returns>
		public bool IsInsideFolder(string Path)
		{
			if(this.ItemPath==null || Path==null)return false;

			int MyPathLength=this.ItemPath.Length;
			int PathLength=Path.Length;
			// if my path is smaller, then i'm OBVIOUSLY not inside that folder
			if(MyPathLength<PathLength) return false;

			return this.ItemPath.Substring(0,Path.Length).ToLower().Equals(Path.ToLower());
		}
		#endregion
	}
}
