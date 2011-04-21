using System;
using System.Drawing;
using Microsoft.DirectX.Direct3D;

namespace Orbit.Items
{
	/// <summary>
	/// Represents a physically stored OrbitItem which is enumerateable and has support for a loading progress bar
	/// </summary>
	public abstract class StoredLoaderItem:StoredOrbitItem, IEnumeratorItem
	{
		#region Internal Variables
		#region Counters
		/// <summary>
		/// Indicates how much of this item's childs have been loaded
		/// </summary>
		private float _LoadedPercentage=0;
		private int LoadTickStart;
		#endregion

		#region Managed Resources
		private VertexBuffer ProgressBgVertexBuffer;
		private VertexBuffer ProgressBarVertexBuffer;
		#endregion
		#endregion

		#region Overriden Destructor
		/// <summary>
		/// Disposes the StoredLoaderItem object
		/// </summary>
		public override void Dispose()
		{
			if(ProgressBgVertexBuffer!=null)
				ProgressBgVertexBuffer.Dispose();
			if(ProgressBarVertexBuffer!=null)
				ProgressBarVertexBuffer.Dispose();

			base.Dispose();
		}
		#endregion

		#region Resource Creation Calls
		/// <summary>
		/// Initializes the resources for the loading progress bars
		/// </summary>
		protected void InitializeLoadingResources()
		{
			try
			{
				this.ProgressBgVertexBuffer=new VertexBuffer(typeof(CustomVertex.PositionColoredTextured), 4, display, Usage.Dynamic, CustomVertex.PositionColoredTextured.Format, Pool.Default);
				this.ProgressBarVertexBuffer=new VertexBuffer(typeof(CustomVertex.PositionColoredTextured), 4, display, Usage.Dynamic, CustomVertex.PositionColoredTextured.Format, Pool.Default);
				UpdateBufferData(ProgressBgVertexBuffer, Color.White);
				UpdateBufferData(ProgressBarVertexBuffer, Color.FromKnownColor(KnownColor.Highlight));
			}
			catch(Exception)
			{
				throw;
			}
		}

		#endregion

		#region Overriden Drawing
		/// <summary>
		/// Draws this element once the scene has begun
		/// </summary>
		/// <param name="XOffset">Offset on the X axis</param>
		/// <param name="YOffset">Offset on the Y axis</param>
		public override void Draw(float XOffset, float YOffset)
		{
			base.Draw (XOffset, YOffset);
			if(LoadedPercentage!=0 && (Environment.TickCount-LoadTickStart)>400)
			{
				//base.DrawTextureOnBuffer(ProgressBgVertexBuffer, null, new RectangleF(new PointF(this.Rectangle.X,this.Rectangle.Y), new SizeF(this.Rectangle.Width/2, this.Rectangle.Height/10)), Color.White);
				//base.DrawTextureOnBuffer(ProgressBarVertexBuffer, null, new RectangleF(new PointF(this.Rectangle.X+1,this.Rectangle.Y+1), new SizeF((this.Rectangle.Width/2-2)*LoadedPercentage, this.Rectangle.Height/10-2)), Color.Green);
				base.DrawTextureOnBuffer(ProgressBgVertexBuffer, null, new RectangleF(new PointF(this.Rectangle.X,this.Rectangle.Y), new SizeF(this.Rectangle.Width/2, 3)), Color.White);
				base.DrawTextureOnBuffer(ProgressBarVertexBuffer, null, new RectangleF(new PointF(this.Rectangle.X+1,this.Rectangle.Y+1), new SizeF((this.Rectangle.Width/2-2)*LoadedPercentage, 1)), Color.FromKnownColor(KnownColor.Highlight));
			}
		}
		#endregion

		#region IEnumerator Implementation
		/// <summary>
		/// Gets the items contained by this item
		/// </summary>
		/// <returns>Returns an array of OrbitItems</returns>
		public abstract OrbitItem[] GetItems();
		#endregion

		#region Protected Properties
		/// <summary>
		/// Gets/Sets the percentage of loading for this item
		/// </summary>
		/// <remarks>Must be set to 0 before anything. Otherwise, the loading indicator will always show up, instead of only after 400ms</remarks>
		protected float LoadedPercentage
		{
			get
			{
				return _LoadedPercentage;
			}
			set
			{
				if(value==0)
					LoadTickStart=Environment.TickCount;
				_LoadedPercentage=value;
			}
		}
		#endregion
	}
}
