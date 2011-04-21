using System;
using Orbit.Items;

namespace Orbit.Items.Collections
{
	/// <summary>
	/// Encapsulates a node on the OrbitItemLinkedList
	/// </summary>
	public class OrbitItemLinkNode:IDisposable
	{
		#region Internal Variables
		private OrbitItem _Item;
		private OrbitItemLinkNode _Next;
		private OrbitItemLinkNode _Previous;
		#endregion

		#region Constructor
		/// <summary>
		/// Creates a new instance of the OrbitItemLinkNode class
		/// </summary>
		public OrbitItemLinkNode()
		{
			
		}
		#endregion
		
		#region IDisposable Members
		/// <summary>
		/// Disposes the OrbitItemLinkNode object
		/// </summary>
		public void Dispose(){}
		#endregion

		#region Properties
		/// <summary>
		/// Gets/Sets the OrbitItem that this node holds
		/// </summary>
		public OrbitItem Item
		{
			get
			{
				return _Item;
			}
			set
			{
				if(_Item!=value)
					_Item=value;
			}
		}
		/// <summary>
		/// Gets/Sets the next node in the list
		/// </summary>
		public OrbitItemLinkNode Next
		{
			get
			{
				return _Next;
			}
			set
			{
				if(_Next!=value)
					_Next=value;
			}
		}
		/// <summary>
		/// Gets/Sets the previous node in the list
		/// </summary>
		public OrbitItemLinkNode Previous
		{
			get
			{
				return _Previous;
			}
			set
			{
				if(_Previous!=value)
					_Previous=value;
			}
		}
		#endregion
	}
}