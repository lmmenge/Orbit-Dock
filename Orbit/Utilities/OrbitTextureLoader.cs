using System;
using System.Drawing;
using Microsoft.DirectX.Direct3D;

namespace Orbit.Utilities
{
	/// <summary>
	/// Utility class for loading textures from files
	/// </summary>
	public sealed class OrbitTextureLoader
	{
		/// <summary>
		/// Loads a Texture using the GDI+ Bitmap loading as interim
		/// </summary>
		/// <param name="device">Direct3D Device to load the texture into</param>
		/// <param name="path">Path to load the texture from</param>
		/// <returns>A Direct3D Texture object. Null if failed</returns>
		private static Texture FromGDI(Device device, string path)
		{
			// bail if file doesn't exist
			if(!System.IO.File.Exists(path))
				return null;

			try
			{
				Texture LoadedTexture;
				// open the bitmap in GDI
				using(Bitmap LoadedBMP=(Bitmap)Bitmap.FromFile(path))
				{
					// load to D3D
					LoadedTexture=Texture.FromBitmap(device, LoadedBMP, Usage.Dynamic, Pool.Default);
				}
				return LoadedTexture;
			}
			catch(Exception)
			{
				// if can't load, return nothing
				return null;
			}
		}

		/// <summary>
		/// Loads a Texture using the TextureLoader as interim
		/// </summary>
		/// <param name="device">Direct3D Device to load the texture into</param>
		/// <param name="path">Path to load the texture from</param>
		/// <returns>A Direct3D Texture object. Null if failed</returns>
		private static Texture FromLoader(Device device, string path)
		{
			// bail out if file doesn't exist
			if(!System.IO.File.Exists(path))
				return null;

			try
			{
				// attempt to load directly
				Texture LoadedTexture=TextureLoader.FromFile(device, path);
				return LoadedTexture;
			}
			catch(Exception)
			{
				// return null if can't load
				return null;
			}
		}

		/// <summary>
		/// Loads a Texture from an icon using the GDI+ Bitmap loading as interim
		/// </summary>
		/// <param name="device">Direct3D Device to load the texture into</param>
		/// <param name="path">Path to load the texture from</param>
		/// <returns>A Direct3D Texture object. Null if failed</returns>
		private static Texture FromIcon(Device device, string path)
		{
			// bail out if file doesn't exist or it isn't an icon file
			if(!System.IO.File.Exists(path) || System.IO.Path.GetExtension(path).Trim().ToLower()!=".ico")
				return null;

			try
			{
				Texture LoadedTexture;
				// open the icon
				using(Icon i=new Icon(path))
				{
					// request the largest possible icon
					using(Icon a=new Icon(i, 128, 128))
					{
						// create a bitmap from that icon
						using(Bitmap LoadedBMP=ImageHelper.GetBitmapFromIcon(a))
						{
							// create a texture
							LoadedTexture=Texture.FromBitmap(device, LoadedBMP, Usage.Dynamic, Pool.Default);
						}
					}
				}
				// return the texture
				return LoadedTexture;
			}
			catch(Exception)
			{
				// return null if can't load
				return null;
			}
		}


		/// <summary>
		/// Loads a Texture trying both TextureLoader and GDI as interim
		/// </summary>
		/// <param name="device">Direct3D Device to load the texture into</param>
		/// <param name="path">Path to load the texture from</param>
		/// <returns>A Direct3D Texture object. Null if failed</returns>
		public static OrbitTexture Load(Device device, string path)
		{
			Texture loadedTexture=null;

			// if file is an ICO file, use the FromIcon, otherwise proceed to the other ones
			if(System.IO.Path.GetExtension(path).Trim().ToLower()==".ico")
			{
				loadedTexture=FromIcon(device, path);
			}
			// if not icon, try the other two possible methods
			if(loadedTexture==null)
			{
				// try TextureLoader
				loadedTexture=FromLoader(device, path);
				if(loadedTexture==null)
				{
					// this means TextureLoader failed. try GDI
					loadedTexture=FromGDI(device, path);
				}
			}

			// will return a texture of any of them succeeded. since both return null, will return null if failed.
			OrbitTexture orbitTexture = new OrbitTexture(loadedTexture, path);
			return orbitTexture.GetReference();
		}
		/// <summary>
		/// Loads a Texture from a GDI+ Bitmap
		/// </summary>
		/// <param name="device">Direct3D Device to load the texture into</param>
		/// <param name="sourceBitmap">Bitmap from which to create the texture</param>
		/// <returns>A OrbitTexture object. Null if failed</returns>
		public static OrbitTexture Load(Device device, Bitmap sourceBitmap)
		{
			// try and load from the bitmap
			Texture texture=null;
			try
			{
				texture = Texture.FromBitmap(device, sourceBitmap, Usage.Dynamic, Pool.Default);
			}
			catch(Exception)
			{
				// return nothing if it fails
				return null;
			}
			// create our OrbitTexture object which will house all the texture description parameters
			OrbitTexture orbitTexture = new OrbitTexture(texture);
			return orbitTexture.GetReference();
		}
		/// <summary>
		/// Loads a Texture from a GDI+ Bitmap
		/// </summary>
		/// <param name="device">Direct3D Device to load the texture into</param>
		/// <param name="sourceStream">Stream from which to create the texture</param>
		/// <returns>A OrbitTexture object. Null if failed</returns>
		public static OrbitTexture Load(Device device, System.IO.Stream sourceStream)
		{
			// try and load from the bitmap
			Texture texture=null;
			try
			{
				texture = new Texture(device, sourceStream, Usage.Dynamic, Pool.Default);
			}
			catch(Exception)
			{
				// return nothing if it fails
				return null;
			}
			// create our OrbitTexture object which will house all the texture description parameters
			OrbitTexture orbitTexture = new OrbitTexture(texture);
			return orbitTexture.GetReference();
		}
	}
}
