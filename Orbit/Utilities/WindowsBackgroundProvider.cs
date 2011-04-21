using System;
using System.Drawing;
using Microsoft.Win32;
using Microsoft.DirectX.Direct3D;

namespace Orbit.Utilities
{
	/// <summary>
	/// Provides means to acquire the Windows Wallpaper
	/// </summary>
	public class WindowsBackgroundProvider:BackgroundProvider
	{
		#region Private Members
		private string LastConvertedWallpaperPath="";
		private string WallpaperPath="";
		#endregion

		#region Creator
		/// <summary>
		/// Initiates a new instance of the WindowsBackgroundProvider class
		/// </summary>
		/// <param name="D3DDevice">Direct3D Device to load the background to</param>
		/// <param name="D3DSprite">Direct3D Sprite to use to paint the background</param>
		public WindowsBackgroundProvider(Device D3DDevice, Sprite D3DSprite)
		{
			// verifying values
			if(D3DDevice==null)
				throw new ArgumentNullException("D3DDevice");
			if(D3DSprite==null)
				throw new ArgumentNullException("D3DSprite");

			// gather data
			display=D3DDevice;
			SpritePainter=D3DSprite;

			SyncBackground();
		}

		#endregion

		#region Utility Methods
		private void SyncBackground()
		{
			// need to explain?
			SyncBackgroundColor();
			SyncWallpaperStyle();
			SyncWallpaper();
		}

		private void SyncBackgroundColor()
		{
			// read the background color
			_BackgroundColor=Color.FromKnownColor(KnownColor.Desktop);
		}
		private void SyncWallpaperStyle()
		{
			try
			{
				// open the Desktop key
				Microsoft.Win32.RegistryKey DesktopKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop");
				// read the wallpaperstyle key and cast its values to my custom enum
				_StretchMode=(Orbit.Configuration.BackgroundStretchMode)int.Parse((string)DesktopKey.GetValue("WallpaperStyle"));
				// confirm it's not being tiled
				// only happens on stretch mode 0
				if(_StretchMode==0)
				{
					// if tilewallpaper is 1, then it's tiled
					if(DesktopKey.GetValue("TileWallpaper").ToString()=="1")
						_StretchMode=Orbit.Configuration.BackgroundStretchMode.Tile;
				}

				// debug out stretch mode change
				//System.Diagnostics.Debug.WriteLine(_StretchMode.ToString()+", number: "+DesktopKey.GetValue("WallpaperStyle").ToString());
				// close the key
				DesktopKey.Close();
			}
			catch(Exception ex)
			{
				// in case it fails (be registry access problems OR weird registry value), set stretch to none
				_StretchMode=Orbit.Configuration.BackgroundStretchMode.None;
				// debug out error
				System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
			}
		}
		private void SyncWallpaper()
		{
			// open the dekstop registry key
			Microsoft.Win32.RegistryKey DesktopKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop");
			// read the last converted wallpaper (in case the new wallpaper is a JPEG converted to wallpaper1.bmp)
			string LastConvertedWallpaperPathNew=(string)DesktopKey.GetValue("ConvertedWallpaper");
			// read the actual wallpaper path
			string WallpaperPathNew=(string)DesktopKey.GetValue("Wallpaper");
			DesktopKey.Close();

			if(WallpaperPathNew=="" || WallpaperPathNew==null)
				return;

			// if the WallpaperPath has changed, then the user set a new BMP
			// if the LastConvertedWallpaper changed, then the user set a new JPEG (BMP name doesn't necessarily change if previous wallpaper was a JPEG too)
			if(WallpaperPathNew!=WallpaperPath || LastConvertedWallpaperPathNew!=LastConvertedWallpaperPath)
			{
				// load new bg
				SetBg(WallpaperPathNew);
				// update the image information
				Microsoft.DirectX.Direct3D.ImageInformation ImageInformation=Microsoft.DirectX.Direct3D.TextureLoader.ImageInformationFromFile(WallpaperPathNew);
				_BackgroundSize=new Size(ImageInformation.Width, ImageInformation.Height);
				// debug out wallpaper changed
				//System.Diagnostics.Debug.WriteLine("Wallpaper path changed to: "+WallpaperPathNew);
				// update the track strings
				WallpaperPath=WallpaperPathNew;
				LastConvertedWallpaperPath=LastConvertedWallpaperPathNew;
			}
		}
		#endregion

		#region Inherited Methods
		/// <summary>
		/// Resyncs the Windows Wallpaper if needed
		/// </summary>
		public override void Prepare()
		{
			SyncBackground();
			base.Prepare ();
		}
		#endregion

		#region Overriden Properties
		/// <summary>
		/// Gets the background image size
		/// </summary>
		public override Size BackgroundSize
		{
			get
			{
				return base.BackgroundSize;
			}
		}

		/// <summary>
		/// Gets the background image path
		/// </summary>
		public override string BackgroundPath
		{
			get
			{
				return base.BackgroundPath;
			}
		}
		#endregion
	}
}
