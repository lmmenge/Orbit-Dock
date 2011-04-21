using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.DirectX.Direct3D;

namespace Orbit.Items
{
	/// <summary>
	/// Represents an item that pops up when the folder is empty
	/// </summary>
	public class EmptyItem:OrbitItem
	{
		#region Constructors
		private EmptyItem(){}

		/// <summary>
		/// Creates a new instance of an "Empty" item
		/// </summary>
		/// <param name="Device">Direct3D Device to load the resources to</param>
		public EmptyItem(Device Device)
		{
			// checking the values
			ValidateDevice(Device);

			// settng the basic properties
			display=Device;

			try
			{
				// initialize common resources
				InitializeResources();

				// set this item's properties
				this.Name="Empty";
				this.Description="This folder is empty";
				try
				{
					using(Bitmap b=new Bitmap(System.Reflection.Assembly.LoadFrom(Application.ExecutablePath).GetManifestResourceStream("Orbit.Images.FolderEmptyIcon.png")))
					{
						SetIcon(b);
					}
				}
				catch(Exception){}
			}
			catch(Exception)
			{
				throw;
			}
		}
		#endregion
	}
}
