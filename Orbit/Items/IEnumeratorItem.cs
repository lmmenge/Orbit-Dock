using System;

namespace Orbit.Items
{
	/// <summary>
	/// This interface provides methods for items that contain more items
	/// </summary>
	public interface IEnumeratorItem
	{
		/// <summary>
		/// Gets the items contained by this item
		/// </summary>
		/// <returns>Returns an array of OrbitItems</returns>
		OrbitItem[] GetItems();
	}
}
