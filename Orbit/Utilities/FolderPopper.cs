using System;
using Orbit.Configuration;
using Orbit.Items;

namespace Orbit.Utilities
{
	/// <summary>
	/// Summary description for FolderPopper.
	/// </summary>
	public class FolderPopper
	{
		private System.Threading.Thread _CountThread;
		private int _Timeout;
		private bool _Enabled;
		private OrbitItem _Item;

		/// <summary>
		/// Creates a new instance of the FolderPopper class
		/// </summary>
		public FolderPopper()
		{
			_Timeout=400;
			_Enabled=false;
		}

		/// <summary>
		/// Starts the pop up counter for the given item
		/// </summary>
		/// <param name="item">Item to open when the timer runs out</param>
		public void Run(OrbitItem item)
		{
			if(item==null)
				return;

			if(_CountThread!=null)
			{
				_Enabled=false;
				_CountThread.Abort();
			}

			_Item=item;
			_Enabled=true;
			_CountThread=new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));
			_CountThread.Name="FolderPop";
			_CountThread.Start();
			//System.Windows.Forms.MessageBox.Show("over!");
		}

		private void ThreadProc()
		{
			//System.Windows.Forms.MessageBox.Show("looping");
			int i=0;
			while(i<_Timeout && _Enabled)
			{
				System.Threading.Thread.Sleep(50);
				i+=50;

				// will only pop if:
				// - hit timeout limit
				// - item still exists
				// - item still has the mouse over it
				// - item hasn't popped up
				// - item has finished animating
				if(i>=_Timeout && _Item!=null && _Item.IsMouseOver && !_Item.IsToggled && _Item.AnimationState>=Global.Configuration.Appearance.IconAlpha)
				{
					//System.Windows.Forms.MessageBox.Show("ticking");
					if(this.Tick!=null)this.Tick(_Item, null);
				}
				/*else
				{
					System.Windows.Forms.MessageBox.Show("failed tick");
				}*/
			}
			_Enabled=false;
		}

		/// <summary>
		/// Gets/sets the enabled state of the pop up timer
		/// </summary>
		public bool Enabled
		{
			get
			{
				return _Enabled;
			}
			set
			{
				_Enabled=value;
			}
		}
		/// <summary>
		/// Gets/sets the timeout interval
		/// </summary>
		public int Timeout
		{
			get
			{
				return _Timeout;
			}
			set
			{
				_Timeout=value;
			}
		}
		/// <summary>
		/// Occurs when the pop up interval is done
		/// </summary>
		public event EventHandler Tick;
	}
}
