using System;
using Orbit.Utilities;

namespace Orbit.Utilities
{
	/// <summary>
	/// Provides means to share OrbitTexture instances across the program. Helps avoiding the creation of multiple instances of the same icon.
	/// </summary>
	public class OrbitTextureCache
	{
		#region Private Classes
		private class TextureNode
		{
			private OrbitTexture _Texture;
			private string _Id;

			public TextureNode(OrbitTexture texture, string id)
			{
				_Texture = texture;
				texture.Disposed+=new EventHandler(Texture_Disposed);
				_Id = id;
			}

			private void Texture_Disposed(object sender, EventArgs e)
			{
				if(Disposed!=null) Disposed(this, e);
			}

			public event EventHandler Disposed;

			public OrbitTexture Texture
			{
				get
				{
					return _Texture;
				}
			}
			public string Id
			{
				get
				{
					return _Id;
				}
			}
		}
		#endregion

		#region Variables
		private static TextureNode[] _Textures = new TextureNode[0];
		private static System.Threading.Mutex _Mutex = new System.Threading.Mutex();
		#endregion

		#region Event Handling
		private static void Texture_Disposed(object sender, EventArgs e)
		{
			RemoveFromCache(((TextureNode)sender).Id);
			/*if(!RemoveFromCache(((TextureNode)sender).Id))
				System.Windows.Forms.MessageBox.Show("Error removing freed texture from cache: "+((TextureNode)sender).Id);*/
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Verifies if a given OrbitTexture id exists in the cache
		/// </summary>
		/// <param name="id">OrbitTexture id to look for</param>
		/// <returns>True if the OrbitTexture already exists. False otherwise</returns>
		public static bool IsInCache(string id)
		{
			_Mutex.WaitOne();
			id=id.ToLower();

			for(int i=0; i<_Textures.Length; i++)
			{
				if(_Textures[i].Id==id)
				{
					_Mutex.ReleaseMutex();
					//System.Diagnostics.Debug.WriteLine("IsInCache(): " + id + " is in Cache");
					return true;
				}
			}
			_Mutex.ReleaseMutex();
			//System.Diagnostics.Debug.WriteLine("IsInCache(): " + id + " is not in Cache");
			return false;
		}
		/// <summary>
		/// Returns a reference to an existing OrbitTexture in the cache
		/// </summary>
		/// <param name="id">OrbitTexture id to return</param>
		/// <returns>The reference to the requested OrbitTexture. The instance was already notified of a new reference request - no need to call GetReference() on it</returns>
		public static OrbitTexture GetReference(string id)
		{
			_Mutex.WaitOne();
			id=id.ToLower();
			for(int i=0; i<_Textures.Length; i++)
			{
				if(_Textures[i].Id==id)
				{
					OrbitTexture texture = _Textures[i].Texture.GetReference();
					_Mutex.ReleaseMutex();
					//System.Diagnostics.Debug.WriteLine("GetReference(): Got reference to " + id);
					return texture;
				}
			}
			_Mutex.ReleaseMutex();
			return null;
		}
		/// <summary>
		/// Inserts an user-created OrbitTexture into the cache for future reference
		/// </summary>
		/// <param name="texture">The instance of the OrbitTexture to insert in the cache</param>
		/// <param name="id">The proposed Id of the OrbitTexture</param>
		/// <returns>True if successfully inserted. False otherwise</returns>
		public static bool InsertInCache(OrbitTexture texture, string id)
		{
			if(IsInCache(id))
				return false;

            _Mutex.WaitOne();
			// create the new node
			TextureNode node = new TextureNode(texture, id.ToLower());
			node.Disposed+=new EventHandler(Texture_Disposed);
			// expand the list
			TextureNode[] newList = new TextureNode[_Textures.Length+1];
			for(int i=0; i<_Textures.Length; i++)
			{
				newList[i] = _Textures[i];
			}
			newList[newList.Length-1] = node;
			// finally replace the list
			_Textures = newList;

			_Mutex.ReleaseMutex();
			//System.Diagnostics.Debug.WriteLine("InsertInCache(): Inserted " + id + " in cache");
			return true;
		}
		/// <summary>
		/// Removes an user-created OrbitTexture from the cache
		/// </summary>
		/// <param name="texture">The instance of the OrbitTexture to remove from the cache</param>
		/// <returns>True if successfully removed. False otherwise</returns>
		public static bool RemoveFromCache(OrbitTexture texture)
		{
			_Mutex.WaitOne();
			TextureNode toRemove = null;
			for(int i=0; i<_Textures.Length; i++)
			{
				if(_Textures[i].Texture == texture)
					toRemove = _Textures[i];
			}

			if(toRemove!=null)
			{
				// reduce the list
				TextureNode[] newList = new TextureNode[_Textures.Length-1];
				bool found=false;
				for(int i=0; i<_Textures.Length; i++)
				{
					if(_Textures[i]!=toRemove)
					{
						if(found)
							newList[i-1] = _Textures[i];
						else
							newList[i] = _Textures[i];
					}
					else
						found=true;
				}
				// finally replace the list
				_Textures = newList;

				_Mutex.ReleaseMutex();
				//System.Diagnostics.Debug.WriteLine("RemoveFromCache(): Removed " + toRemove.Id + " from cache");
				return true;
			}
			else
			{
				_Mutex.ReleaseMutex();
				//System.Diagnostics.Debug.WriteLine("RemoveFromCache(): Texture not found in cache");
				return false;
			}
		}
		/// <summary>
		/// Removes an user-created OrbitTexture from the cache
		/// </summary>
		/// <param name="id">The id</param>
		/// <returns>True if successfully removed. False otherwise</returns>
		public static bool RemoveFromCache(string id)
		{
			_Mutex.WaitOne();
			TextureNode toRemove = null;
			id = id.ToLower();
			for(int i=0; i<_Textures.Length; i++)
			{
				if(_Textures[i].Id == id)
					toRemove = _Textures[i];
			}

			if(toRemove!=null)
			{
				// reduce the list
				TextureNode[] newList = new TextureNode[_Textures.Length-1];
				bool found = false;
				for(int i=0; i<_Textures.Length; i++)
				{
					if(_Textures[i]!=toRemove)
					{
						if(found)
							newList[i-1] = _Textures[i];
						else
							newList[i] = _Textures[i];
					}
					else
						found=true;
				}
				// finally replace the list
				_Textures = newList;

				_Mutex.ReleaseMutex();
				//System.Diagnostics.Debug.WriteLine("RemoveFromCache(): Removed " + id + " from cache");
				return true;
			}
			else
			{
				_Mutex.ReleaseMutex();
				//System.Diagnostics.Debug.WriteLine("RemoveFromCache(): " + id + " not found in cache");
				return false;
			}
		}
		#endregion
	}
}
