using System;
using System.Drawing;
using Microsoft.DirectX.Direct3D;

using Orbit.Configuration;
using Orbit.Utilities;

namespace Orbit.Items
{
	/// <summary>
	/// Represents an Orbit item that has a preview image attached to it
	/// </summary>
	public abstract class PreviewableItem:OrbitItem
	{
		#region Internal Variables
		#region Counters and Flags
		private byte PreviewFrame=0;
		private bool HasThumbnail=false;
		//private bool UseHQComposing=false;
		#endregion

		#region Resources Variables
		private VertexBuffer IconVertexBuffer;
		private OrbitTexture IconTexture;
		//private Image IconImage;
		private VertexBuffer ScreenshotVertexBuffer;
		private OrbitTexture ScreenshotTexture;
		//private Image ScreenshotImage;
		#endregion
		#endregion

		/*#region Constructor
		/// <summary>
		/// Creates a new instance of the PreviewableItem class
		/// </summary>
		public PreviewableItem(){}
		#endregion*/

		#region Overriden Destructor
		/// <summary>
		/// Disposes the PreviewableItem object
		/// </summary>
		public override void Dispose()
		{
			try
			{
				if(IconVertexBuffer!=null)
					IconVertexBuffer.Dispose();
			}
			catch(Exception){}
			UnloadTexture(ref IconTexture);
			try
			{
				if(ScreenshotVertexBuffer!=null)
					ScreenshotVertexBuffer.Dispose();
			}
			catch(Exception){}
			UnloadTexture(ref ScreenshotTexture);

			base.Dispose();
		}
		#endregion

		#region Overriden Events
		/// <summary>
		/// Updates our internal vertex buffers with new color key information
		/// </summary>
		protected override void OnColorKeyChange()
		{
			UpdateBufferData(IconVertexBuffer, ColorKey);
			base.OnColorKeyChange();
		}

		#endregion

		#region Overriden Rendering
		/// <summary>
		/// Draws this element once the scene has begun
		/// </summary>
		/// <param name="XOffset">Offset on the X axis</param>
		/// <param name="YOffset">Offset on the Y axis</param>
		public override void Draw(float XOffset, float YOffset)
		{
			if(HasThumbnail)
				AnimatePreview();

			DrawD3D(XOffset, YOffset);

			/*if(UseHQComposing)
			{
				AnimatePreview();
				DrawD3D(XOffset, YOffset);
			}
			else
			{
				base.Draw(XOffset, YOffset);
			}*/
		}
		#endregion

		#region Protected D3D Loading Methods
		/// <summary>
		/// Initializes resources needed for task preview matters
		/// </summary>
		protected void InitializePreviewResources()
		{
			/*if(!Orbit.Core.FeatureSets.UseHQPreviews)
				return;*/

			try
			{
				this.IconVertexBuffer=new VertexBuffer(typeof(CustomVertex.PositionColoredTextured), 4, display, Usage.Dynamic, CustomVertex.TransformedColoredTextured.Format, Pool.Default);
				this.ScreenshotVertexBuffer=new VertexBuffer(typeof(CustomVertex.PositionColoredTextured), 4, display, Usage.Dynamic, CustomVertex.TransformedColoredTextured.Format, Pool.Default);
				UpdateBufferData(IconVertexBuffer, Color.FromArgb(0x00, Color.White));
				UpdateBufferData(ScreenshotVertexBuffer, Color.FromArgb(0x00, Color.White));
			}
			catch(Exception)
			{
				// fallback if failed
				//Orbit.Core.FeatureSets.UseHQPreviews=false;
				throw;
			}
		}
		/// <summary>
		/// Sets the overlay badge for the preview
		/// </summary>
		/// <param name="NewImage">Image to set that to</param>
		[Obsolete("Use the SetIcon instead", true)]
		protected void SetOverlay(Image NewImage)
		{
			// checking if i have the needed info
			if(NewImage==null || this.IconTexture!=null/* || this.IconImage!=null*/)
				return;

			/*if(!Orbit.Core.FeatureSets.UseHQPreviews)
			{
				IconImage=ImageHelper.GetAspectThumbnail((Bitmap)NewImage, new Size(Global.Configuration.Appearance.IconMagnifiedSize, Global.Configuration.Appearance.IconMagnifiedSize));
				return;
			}*/

			try
			{
				//using(Bitmap IconThumb=(Bitmap)ImageHelper.GetAspectThumbnail((Bitmap)NewImage, new Size(Global.Configuration.Appearance.IconMagnifiedSize, Global.Configuration.Appearance.IconMagnifiedSize)))
				using(Bitmap IconThumb=(Bitmap)ImageHelper.GetBestSizeFor((Bitmap)NewImage, new Size(Global.Configuration.Appearance.IconMagnifiedSize, Global.Configuration.Appearance.IconMagnifiedSize)))
				{
					// load
					OrbitTexture oldTexture=this.IconTexture;
					this.IconTexture=OrbitTextureLoader.Load(display, IconThumb);
					UnloadTexture(ref oldTexture);
				}
			}
			catch(Exception)
			{
				try
				{
					UnloadTexture(ref IconTexture);
				}
				catch(Exception){}
			}
		}
		/// <summary>
		/// Sets the background image (the actual preview image)
		/// </summary>
		/// <param name="NewImage">Image to set that to</param>
		protected void SetBackground(Image NewImage)
		{
			// checking if i have the needed info
			if(NewImage==null || this.ScreenshotTexture!=null/* || this.ScreenshotImage!=null*/)
				return;

			/*if(!Orbit.Core.FeatureSets.UseHQPreviews)
			{
				ScreenshotImage=ImageHelper.GetAspectThumbnail((Bitmap)NewImage, new Size(Global.Configuration.Appearance.IconMagnifiedSize, Global.Configuration.Appearance.IconMagnifiedSize));
				return;
			}*/

			try
			{
				//using(Bitmap IconThumb=(Bitmap)ImageHelper.GetAspectThumbnail((Bitmap)NewImage, new Size(Global.Configuration.Appearance.IconMagnifiedSize, Global.Configuration.Appearance.IconMagnifiedSize)))
				using(Bitmap IconThumb=(Bitmap)ImageHelper.GetBestSizeFor((Bitmap)NewImage, new Size(Global.Configuration.Appearance.IconMagnifiedSize, Global.Configuration.Appearance.IconMagnifiedSize)))
				{
					// load
					OrbitTexture oldTexture=this.ScreenshotTexture;
					this.ScreenshotTexture=OrbitTextureLoader.Load(display, IconThumb);
					UnloadTexture(ref oldTexture);
				}
			}
			catch(Exception)
			{
				try
				{
					UnloadTexture(ref ScreenshotTexture);
				}
				catch(Exception){}
			}
		}
		#endregion

		#region Private Composing Methods
		/// <summary>
		/// Starts the preview and overlay composing
		/// </summary>
		protected void Compose()
		{
			if(Orbit.Core.FeatureSets.UseHQPreviews)
				ComposeHQ();
			else
				ComposeLQ();

			HasThumbnail=true;
		}
		private void ComposeHQ()
		{
			//UseHQComposing=true;
		}
		private void ComposeLQ()
		{
			PreviewFrame=255;
			UpdateBufferData(ScreenshotVertexBuffer, Color.FromArgb(PreviewFrame, Color.White));
			//UseHQComposing=true;
			/*using(Bitmap Icon=new Bitmap(Global.Configuration.Appearance.IconMagnifiedSize, Global.Configuration.Appearance.IconMagnifiedSize))
			{
				using(Graphics g=Graphics.FromImage(Icon))
				{
					g.InterpolationMode=System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
					g.CompositingQuality=System.Drawing.Drawing2D.CompositingQuality.HighQuality;
					g.SmoothingMode=System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

					// drawing the preview
					try
					{
						if(ScreenshotImage!=null)
						{
							if(Icon.Width>Icon.Height)
								g.DrawImageUnscaled((Image)ScreenshotImage, new Rectangle(new Point(0, (Global.Configuration.Appearance.IconMagnifiedSize-Icon.Height)/2), new Size(Global.Configuration.Appearance.IconMagnifiedSize, Global.Configuration.Appearance.IconMagnifiedSize)));
							else
								g.DrawImageUnscaled((Image)ScreenshotImage, new Rectangle(new Point((Global.Configuration.Appearance.IconMagnifiedSize-Icon.Height)/2, 0), new Size(Global.Configuration.Appearance.IconMagnifiedSize, Global.Configuration.Appearance.IconMagnifiedSize)));
						}
					}
					catch(Exception){}					

					// drawing the icon
					try
					{
						int OverlaySize=Icon.Width/2;
						if(IconImage!=null)
							g.DrawImage((Image)IconImage, new Rectangle(new Point(Icon.Width-OverlaySize,Icon.Height-OverlaySize), new Size(OverlaySize, OverlaySize)));
					}
					catch(Exception){}
				}
				if(ScreenshotImage!=null)
					ScreenshotImage.Dispose();
				if(IconImage!=null)
					IconImage.Dispose();

				this.SetIcon(Icon);
				OnPaint();
			}*/
		}
		#endregion

		#region Private Rendering Methods
		private void DrawD3D(float ParentOffsetX, float ParentOffsetY)
		{
			try
			{
				// draw screenshot
				if(ScreenshotTexture!=null)
					DrawTextureOnBuffer(ScreenshotVertexBuffer, ScreenshotTexture.Texture, new RectangleF(new PointF(this.Rectangle.X+ParentOffsetX, this.Rectangle.Y+ParentOffsetY), new SizeF(this.Rectangle.Width, this.Rectangle.Height)), Color.FromArgb(Convert.ToInt32(PreviewFrame*(this.AnimationState/255f)), Color.White));

				// draw icon
				float SinOp=(float)Math.Sin(Math.PI/2*PreviewFrame/255f);
				float CosOp=(float)Math.Cos(Math.PI/2+Math.PI/2*PreviewFrame/255f)+1;
				float Offset=this.Rectangle.Width*SinOp/2;
				float Size=this.Rectangle.Width*(0.5f+CosOp/2);
				// make the overlays blend nicely with the image behind - still stains in transparent mode when NOT over an image :(
				if(ScreenshotTexture!=null)
					display.RenderState.BlendOperation=BlendOperation.Add;
				if(IconTexture==null)
					DrawTextureOnBuffer(IconVertexBuffer, _Icon.Texture, new RectangleF(new PointF(this.Rectangle.X+ParentOffsetX+Offset, this.Rectangle.Y+ParentOffsetY+Offset), new SizeF(Size, Size)), ColorKey);
				else
					DrawTextureOnBuffer(IconVertexBuffer, IconTexture.Texture, new RectangleF(new PointF(this.Rectangle.X+ParentOffsetX+Offset, this.Rectangle.Y+ParentOffsetY+Offset), new SizeF(Size, Size)), ColorKey);
			}
			catch(Exception){}
		}
		private void AnimatePreview()
		{			
			// animate
			if(PreviewFrame==255)
				return;

			//PreviewFrame+=15;
			if(PreviewFrame+Global.Configuration.Runtime.LastFrameSpeed<255)
				PreviewFrame+=Global.Configuration.Runtime.LastFrameSpeed;
			else
				PreviewFrame=255;

			UpdateBufferData(ScreenshotVertexBuffer, Color.FromArgb(PreviewFrame, Color.White));
			OnPaint();
		}
		#endregion

		#region Private Preview Acquiring Methods
		/// <summary>
		/// Implements how the inheritant class acquires the preview images
		/// </summary>
		protected abstract void GetPreview();
		#endregion

		#region Public Thumbnail Acquiring Methods
		/// <summary>
		/// Acquires a thumbnail image for the running task
		/// </summary>
		public void GetThumbnailIfPossible()
		{
			if(HasThumbnail)
				return;

			GetPreview();
			OnPaint();
		}
		#endregion
	}
}
