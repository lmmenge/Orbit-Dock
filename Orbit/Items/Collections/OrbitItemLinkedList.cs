using System;
using Orbit.Items;

namespace Orbit.Items.Collections
{
	/// <summary>
	/// Linked list implementation for OrbitItems
	/// </summary>
	public class OrbitItemLinkedList
	{
		#region Internal Variables
		#region Managed Variables
		private OrbitItemLinkNode FirstNode;
		private OrbitItemLinkNode LastNode;
		#endregion

		#region Seeking Optimization Variables
		private int LastSearchIndex=0;
		private OrbitItemLinkNode LastSearchNode;
		#endregion

		#region Properties
		private int _Length;
		#endregion
		#endregion

		#region Constructor
		/// <summary>
		/// Creates a new instance of the OrbitItemLinkedList object
		/// </summary>
		public OrbitItemLinkedList()
		{
			
		}
		#endregion

		#region Accessor
		/// <summary>
		/// Returns the OrbitItem in the specified index
		/// </summary>
		public OrbitItem this[int index]
		{
			get
			{
				return Find(index).Item;
			}
			set
			{
				OrbitItemLinkNode node=Find(index);
				if(!node.Item.Equals(value))
				{
					node.Item=value;
				}
			}
		}
		#endregion

		#region Private Methods
		private OrbitItemLinkNode Find(int index, bool nonOptimized)
		{
			if(index>=_Length || index<0 || FirstNode==null)
				return null;

			// start at the index closest to our goal
			int i=0;
			OrbitItemLinkNode node=FirstNode;

			// search!
			while(i!=index)
			{
				// no more nodes?
				if(node==null)
					return null;
				else
				{
					// get next
					node=node.Next;
				}
				// count
				i++;
			}

			// return the node
			return node;
		}
		private OrbitItemLinkNode Find(int index)
		{
			if(index>=_Length || index<0 || FirstNode==null)
				return null;

			// if finding already found item, return it
			if(index==LastSearchIndex)
				return LastSearchNode;

			// otherwise, search....
			// start at the index closest to our goal
			int i=0;
			OrbitItemLinkNode node=FirstNode;
			
			double DistanceFromLastSearch=Math.Abs(index-LastSearchIndex);
			double DistanceFromLastNode=Math.Abs(index-(_Length-1));
			if(LastSearchNode!=null && DistanceFromLastSearch<index && DistanceFromLastSearch<DistanceFromLastNode)
			{
				// use the last node searched because it's closer and it's available
				node=LastSearchNode;
				i=LastSearchIndex;
			}
			if(DistanceFromLastNode<DistanceFromLastSearch && DistanceFromLastNode<index)
			{
				// use the last node in the list as start because it's the closest one
				node=LastNode;
				i=_Length-1;
			}

			// set our search direction
			int SearchDirection;
			if(index>i)
				SearchDirection=1;
			else
				SearchDirection=-1;

			// search!
			while(i!=index)
			{
				// no more nodes?
				if(node==null)
					return null;
				else
				{
					// get next
					if(SearchDirection==1)
						node=node.Next;
					else
						node=node.Previous;
				}
				// count
				i=i+SearchDirection;
			}

			// save these values for optimization on next search
			LastSearchIndex=index;
			LastSearchNode=node;

			// return the node
			return node;
		}
		private OrbitItemLinkNode Find(OrbitItem item)
		{
			return Find(IndexOf(item));
		}
		#endregion

		#region Public Methods
		#region Searching
		/// <summary>
		/// Finds the index of an OrbitItem in the list
		/// </summary>
		/// <param name="item">OrbitItem to search for</param>
		/// <returns>Index of the OrbitItem. -1 if not found</returns>
		public int IndexOf(OrbitItem item)
		{
			if(item==null || FirstNode==null)
				return -1;

			OrbitItemLinkNode node=FirstNode;
			int i=0;
			while(node!=null)
			{
				if(node.Item.Equals(item))
					return i;
				node=node.Next;
				i++;
			}

			return -1;
		}
		#endregion

		#region Adding and Removing
		/// <summary>
		/// Adds a new OrbitItem to the list
		/// </summary>
		/// <param name="item">OrbitItem to add</param>
		public void Add(OrbitItem item)
		{
			if(item==null)
				return;

			// create our new node
			OrbitItemLinkNode newNode=new OrbitItemLinkNode();
			newNode.Item=item;

			// place it after the last node
			newNode.Previous=LastNode;

			if(_Length==0)
			{
				// adding the first node
				LastNode=newNode;
				FirstNode=newNode;
			}
			else
			{
				// adding to the last node
				// update the previously last node
				LastNode.Next=newNode;
			}
			// keep updated track of the last node and how many nodes are there
			LastNode=newNode;
			_Length++;

			//System.Diagnostics.Debug.WriteLine("Added \""+item.Name+"\"");
		}
		/// <summary>
		/// Removes an OrbitItem from the list
		/// </summary>
		/// <param name="item">OrbitItem to be removed</param>
		/// <remarks>This is the slowest of all three overloads. If you already know the node you want to remove, please use the Remove(OrbitItemLinkNode node) overload. If you have to search for it, then don't bother. This is what this overload will do</remarks>
		public void Remove(OrbitItem item)
		{
			// remove from the index
			Remove(IndexOf(item));
		}
		/// <summary>
		/// Removes an OrbitItem from the list
		/// </summary>
		/// <param name="index">The index of the OrbitItem to be removed</param>
		/// <remarks>This is the second slowest of all three overloads. If you already know the node you want to remove, please use the Remove(OrbitItemLinkNode node) overload. If you have to search for it, then don't bother. This is what this overload will do</remarks>
		public void Remove(int index)
		{
			// remove from the node
			Remove(Find(index));
		}
		/// <summary>
		/// Removes an OrbitItem from the list
		/// </summary>
		/// <param name="node">The OrbitItemLinkNode to be removed</param>
		public void Remove(OrbitItemLinkNode node)
		{
			if(node==null)
				return;

			// update the surrounding nodes
			if(node.Next!=null)
				node.Next.Previous=node.Previous;
			if(node.Previous!=null)
				node.Previous.Next=node.Next;

			// keep track of our last node
			if(LastNode==node)
				LastNode=node.Previous;
			// keep track of out first node
			if(FirstNode==node)
				FirstNode=node.Next;

			// dispose the node
			node.Dispose();

			// keep track of node count
			_Length--;

			//System.Diagnostics.Debug.WriteLine("Removed \""+node.Item.Name+"\"");
		}
		/// <summary>
		/// Moves an OrbitItem in the list
		/// </summary>
		/// <param name="item">OrbitItem to be moved</param>
		/// <param name="index">Index for that item to be placed in</param>
		/// <param name="place">Position of this new item relative to the existing item on that index</param>
		public void Move(OrbitItem item, int index, RelativePlace place)
		{
			if(item==null || index>=_Length || index<0)
				return;

			// finding our to-be-moved and where-to nodes
			OrbitItemLinkNode nodeToMove=Find(item);
			OrbitItemLinkNode nodeToPlace=Find(index);

			Remove(nodeToMove);

			if(place==RelativePlace.Before)
			{
				// place it before that node
				nodeToMove.Previous=nodeToPlace.Previous;
				nodeToMove.Next=nodeToPlace;

				// update the nodes around the newly placed node
				if(nodeToPlace.Previous!=null)
					nodeToPlace.Previous.Next=nodeToMove;

				// keep track of first node
				if(FirstNode==nodeToMove.Next)
					FirstNode=nodeToMove;
			}
			else
			{
				// place it after that node
				nodeToMove.Next=nodeToPlace.Next;
				nodeToMove.Previous=nodeToPlace;

				// update the nodes around the newly placed node
				if(nodeToPlace.Next!=null)
					nodeToPlace.Next.Previous=nodeToMove;

				// keep track of last node
				if(LastNode==nodeToMove.Previous)
					LastNode=nodeToMove;
			}
		}
		#endregion
		#endregion

		#region Public Properties
		/// <summary>
		/// Indicates how many items are there on the list
		/// </summary>
		public int Length
		{
			get
			{
				return _Length;
			}
		}
		#endregion
	}
}
