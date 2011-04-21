using System;
using Microsoft.DirectX.Direct3D;

namespace Orbit.Utilities
{
	/// <summary>
	/// Encapsulates a Direct3D Texture object
	/// </summary>
	public class OrbitTexture : IDisposable
	{
		#region Variables
		private Texture _Texture;
		private SurfaceDescription _Description;

		private String _Path;
		private int _References;
		//private static int _TotalInstances=0;
		#endregion

		#region Constructor and Destructor
		/// <summary>
		/// Creates a new instance of the OrbitTexture class
		/// </summary>
		/// <param name="sourceTexture">Direct3D Texture object to use as base for this object</param>
		public OrbitTexture(Texture sourceTexture)
		{
			_Texture = sourceTexture;
			if(_Texture!=null)
			{
				_Texture.AutoGenerateFilterType=TextureFilter.Linear;
				_Description = sourceTexture.GetLevelDescription(0);
			}
			_Path = null;
			//OrbitTexture._TotalInstances++;
		}
		/// <summary>
		/// Creates a new instance of the OrbitTexture class
		/// </summary>
		/// <param name="sourceTexture">Direct3D Texture object to use as base for this object</param>
		/// <param name="path">Path to the file that originated this texture</param>
		public OrbitTexture(Texture sourceTexture, string path)
		{
			_Texture = sourceTexture;
			if(_Texture!=null)
			{
				_Texture.AutoGenerateFilterType=TextureFilter.Linear;
				_Description = sourceTexture.GetLevelDescription(0);
			}
			_Path = path;
			//OrbitTexture._TotalInstances++;
		}
		/// <summary>
		/// Disposes the texture. You should never call this method manually. Use FreeReference() instead
		/// </summary>
		public void Dispose()
		{
			if(Disposed!=null) Disposed(this, new EventArgs());

			if(_References>0)
				//System.Diagnostics.Debug.WriteLine("DIRTY Dispose() on OrbitTexture");
				System.Windows.Forms.MessageBox.Show("DIRTY Dispose() on OrbitTexture");
			/*else
				System.Windows.Forms.MessageBox.Show("CLEAN Dispose() on OrbitTexture");*/
			
			//OrbitTexture._TotalInstances--;
            //System.Diagnostics.Debug.WriteLine("Total Instances: "+OrbitTexture._TotalInstances);
			//System.Windows.Forms.MessageBox.Show("Total Instances: "+OrbitTexture._TotalInstances);

			_References=0;
			try
			{
				if(_Texture!=null && !_Texture.Disposed)
				{
					_Texture.Dispose();
				}
			}
			catch(Exception){}
		}
		#endregion

		#region Reference Management
		/// <summary>
		/// Gets a reference to this object
		/// </summary>
		/// <returns>A reference of this object</returns>
		public OrbitTexture GetReference()
		{
			_References++;
			return this;
		}
		/// <summary>
		/// Frees this reference of this object. When the reference count gets to zero, this class will self-dispose. You should call this method instead of Dispose()
		/// </summary>
		public void FreeReference()
		{
			_References--;
			if(_References<=0)
			{
				//System.Diagnostics.Debug.WriteLine("Reference count = 0. Disposing");
				this.Dispose();
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the underlying Direct3D Texture object
		/// </summary>
		public Texture Texture
		{
			get
			{
				return _Texture;
			}
		}
		/// <summary>
		/// Gets the description parameters of the texture
		/// </summary>
		public SurfaceDescription Description
		{
			get
			{
				return _Description;
			}
		}
		/// <summary>
		/// Gets the path to the file that originated this texture. Null if generated from a bitmap in memory
		/// </summary>
		public string Path
		{
			get
			{
				return _Path;
			}
		}
		#endregion

		#region Events
		/// <summary>
		/// Occurs when this texture is effectively disposed. This is NOT triggered when you free a reference, but only when ALL references are freed.
		/// </summary>
		public event EventHandler Disposed;
		#endregion
	}
}
