using System;

namespace Orbit.Items
{
	/// <summary>
	/// Flags that represents the possible context menu items shown by Orbit Items
	/// </summary>
	[Flags]
	public enum ItemMenuFlags
	{
		/// <summary>
		/// No items
		/// </summary>
		None=0,
		/// <summary>
		/// Add Item to this Item menu
		/// </summary>
		AddItemToItem=1,
		/// <summary>
		/// Add Item to this level menu
		/// </summary>
		AddItemToLevel=2,
		/// <summary>
		/// Remove this item menu
		/// </summary>
		RemoveItem=4,
		/// <summary>
		/// Item Properties menu
		/// </summary>
		ItemProperties=8,
		/// <summary>
		/// Start Loops here menu
		/// </summary>
		OpenInStart=16,
		/// <summary>
		/// Open In Explorer menu
		/// </summary>
		OpenInExplorer=32,
		/// <summary>
		/// Ignore this window menu
		/// </summary>
		IgnoreWindow=64,
		/// <summary>
		/// Separator between Item Properties menu and other menus
		/// </summary>
		MenuSeparator=128,
	}
}