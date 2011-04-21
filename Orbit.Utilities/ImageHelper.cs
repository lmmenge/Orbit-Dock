using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Orbit.Utilities
{
	/// <summary>
	/// Class specialized in converting Icon objects to Bitmap objects which contain 8-bit alpha channels
	/// </summary>
	public sealed class ImageHelper
	{
		private ImageHelper(){}

		/// <summary>
		/// Converts an Icon to a Bitmap
		/// </summary>
		/// <param name="icon">Icon object to convert</param>
		/// <returns>Bitmap object with the Icon's image</returns>
		public static Bitmap GetBitmapFromIcon(Icon icon)
		{
			// create the destination bitmap
			Bitmap dstBitmap=null;

			try
			{
				// get the icon information
				Win32.User32.IconInformation ii;
				Win32.User32.User32API.GetIconInfo(icon.Handle, out ii);

				// create Bitmap icon from the hBitmap
				Bitmap bmp = Bitmap.FromHbitmap(ii.hbmColor);

				// get clipping rectangle
				Rectangle bmBounds = new Rectangle(0, 0, bmp.Width, bmp.Height);

				// lock the hBitmap bitmap
				BitmapData bmData = bmp.LockBits(bmBounds, ImageLockMode.ReadOnly, bmp.PixelFormat);
				try
				{
					// create new bitmap from the data with 32bit color information
					dstBitmap = new Bitmap(bmData.Width, bmData.Height, bmData.Stride, PixelFormat.Format32bppArgb, bmData.Scan0);
				}
				catch(Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
				}
				// unlock
				bmp.UnlockBits(bmData);

				// check if this icon was really a 32Argb one or not
				// Explanation: icons which are not 32bit end up with all the alpha values being 0
				int i=0;
				int x=0;
				int y=0;
				bool foundValid=false;
				if(dstBitmap!=null)
				{
					while(i<dstBitmap.Width*dstBitmap.Height)
					{
						if(dstBitmap.GetPixel(x,y).A!=0)
						{
							foundValid=true;
							break;
						}
						x++;
						if(x==dstBitmap.Width)
						{
							x=0;
							y++;
						}
						i++;
					}
				}
				// if wasn't valid, create a 32Rgb one
				if(!foundValid)
				{
					if(dstBitmap!=null)
						dstBitmap.Dispose();
					dstBitmap=icon.ToBitmap();
				}

				// destroy the hBitmap
				Win32.GDI.GDIAPI.DeleteObject(ii.hbmColor);
				Win32.GDI.GDIAPI.DeleteObject(ii.hbmMask);
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
				dstBitmap=icon.ToBitmap();
			}

			//bmp.Dispose();
			return dstBitmap;
		}

		/// <summary>
		/// Gets the best size for the goal size
		/// </summary>
		/// <param name="originalImage">Original Image to be shrunk</param>
		/// <param name="newSize">Size to fit the image in</param>
		/// <returns>A clone of the Image if the originalImage's size is smaller than the newSize or a new Image if the originalImage should be resized to fit</returns>
		//[Obsolete("Use GetAspectThumbnail, instead", true)]
		public static Image GetBestSizeFor(Image originalImage, Size newSize)
		{
			return GetAspectThumbnail(originalImage, newSize);

			// the code below is in case we don't want to resize all images
			/*if(originalImage.Size.Width<newSize.Width && originalImage.Size.Height<newSize.Height)
			{
				return (Image)originalImage.Clone();
			}
			else
			{
				return GetAspectThumbnail(originalImage, newSize);
			}*/
		}
		/// <summary>
		/// Gets a thumbnail of an image
		/// </summary>
		/// <param name="originalImage">Orginal image to get the thumbnail from</param>
		/// <param name="newSize">Size of the thumbnail</param>
		/// <returns>A thumbnail of the image</returns>
		public static Image GetAspectThumbnail(Image originalImage, Size newSize)
		{
			// setting margins
			//int CropImage=newSize.Width-2;

			// Calculating aspect ratios
			SizeF SizeScaled=ImageHelper.GetAspectSizeThatFits(originalImage.Size, newSize);
							
			float TranslateX=(newSize.Width-SizeScaled.Width)/2;
			float TranslateY=(newSize.Height-SizeScaled.Height)/2;

			// drawing the thumbnail
			System.Drawing.Bitmap IconThumb=new Bitmap(newSize.Width, newSize.Height);
			using(Graphics g=Graphics.FromImage((Image)IconThumb))
			{
				g.Clear(Color.FromArgb(0x00, Color.White));
				// HIGH HIGH QUALITY
				//g.InterpolationMode=System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				//g.CompositingQuality=System.Drawing.Drawing2D.CompositingQuality.HighQuality;
				// Faster
				g.InterpolationMode=System.Drawing.Drawing2D.InterpolationMode.High;
				g.CompositingQuality=System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
				// These don't seem to be needed
				//g.SmoothingMode=System.Drawing.Drawing2D.SmoothingMode.HighQuality;

				//if(AddPadding)
					// THIS CODE HAS 1 PIXEL EXTRA IN WIDTH AND HEIGHT!!! IF PROBLEMS HAPPEN, REMOVE!!
				//	g.DrawImage((Image)OriginalImage, new Rectangle(new Point(Convert.ToInt32(1+TranslateX),Convert.ToInt32(1+TranslateY)), new Size(Convert.ToInt32(SizeScaled.Width-2), Convert.ToInt32(SizeScaled.Height-2))), 0-1, 0-1, OriginalImage.Width+1, OriginalImage.Height+1, GraphicsUnit.Pixel);
				//else
					g.DrawImage((Image)originalImage, new RectangleF(new PointF(TranslateX, TranslateY), new SizeF(SizeScaled.Width, SizeScaled.Height)), new Rectangle(new Point(0,0), originalImage.Size), GraphicsUnit.Pixel);
			}

			return IconThumb;
		}

		/// <summary>
		/// Gets a size that fits another size, keeping the original aspect ratio of the first size
		/// </summary>
		/// <param name="previousSize">Size we want to scale</param>
		/// <param name="toFitIn">Size we want to fit in</param>
		/// <returns>A Floating point size with the new size.</returns>
		public static SizeF GetAspectSizeThatFits(Size previousSize, Size toFitIn)
		{
			float x;
			float y;
			float aspectX=(float)toFitIn.Width/(float)previousSize.Width;
			float aspectY=(float)toFitIn.Height/(float)previousSize.Height;
			if(aspectX<=aspectY)
			{
				x=toFitIn.Width;
				y=previousSize.Height*aspectX;
			}
			else
			{
				y=toFitIn.Height;
				x=previousSize.Width*aspectY;
			}
			return new SizeF(x,y);
		}
	}
}
