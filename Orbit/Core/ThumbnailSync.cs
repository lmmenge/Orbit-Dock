using System;
using System.Threading;

using Orbit.Items;

namespace Orbit.Core
{
	/// <summary>
	/// Class specialized in running through an Item array and, in a threaded fashion, acquiring their respective thumbnails
	/// </summary>
	public class ThumbnailSync:IDisposable
	{
		private OrbitItem[] Registry;
		private int Line;
		private Thread SyncThread;
		private bool CanRun;

		#region Public
		/// <summary>
		/// Creates a new instance of the ThumbnailSync class and starts the thumbnail acquiring thread
		/// </summary>
		/// <param name="RegistryToSync">Array of Item objects to acquire thumbnails</param>
		/// <param name="LineToSync">Loop number to sync</param>
		public ThumbnailSync(OrbitItem[] RegistryToSync, int LineToSync)
		{
			//System.Diagnostics.Debug.WriteLine("TS Started");
			if(RegistryToSync==null)
				throw new ArgumentNullException("RegistryToSync");

			Registry=RegistryToSync;
            Line=LineToSync;
			CanRun=true;

			SyncThread=new Thread(new ThreadStart(SyncProc));
			SyncThread.Name="ThumbnailSync";
			SyncThread.Priority=System.Threading.ThreadPriority.BelowNormal;
			SyncThread.Start();
			//System.Windows.Forms.MessageBox.Show("Thumbnail thread started.");
		}

		/// <summary>
		/// Aborts the thumbnail acquiring process
		/// </summary>
		public void AbortSync()
		{
			try
			{
				/*if(SyncThread!=null)
					SyncThread.Abort();*/
				CanRun=false;
			}
			catch(Exception)
			{
				//System.Windows.Forms.MessageBox.Show("Internal error aborting thumbnail thread. Please kill the Orbit.exe process.");
			}
			try
			{
				this.Dispose();
			}
			catch(Exception){}
		}
		#endregion

		#region Private
		private void SyncProcOld()
		{
			try
			{
				int i=0;
				while(i<Registry.Length)
				{
					if(Registry==null || Registry[i]==null || !CanRun)
						break;

					if(Registry[i].Line==Line)
					{
						if(Registry[i].GetType().Equals(typeof(FileSystemFileItem))
							|| Registry[i].GetType().Equals(typeof(TaskItem)))
						{
							//System.Diagnostics.Debug.WriteLine("Synching "+Registry[i].Name);
							((PreviewableItem)Registry[i]).GetThumbnailIfPossible();
						}
					}
					i++;
				}
				//System.Windows.Forms.MessageBox.Show("Thumbnail thread is finished.");
			}
			catch(Exception)
			{
				//System.Windows.Forms.MessageBox.Show("Error in the thumbnail thread.");
				//this.AbortSync();
			}
			this.Dispose();
		}
		private void SyncProc()
		{
			try
			{
				int i=0;
				//System.Diagnostics.Debug.WriteLine("TS Running");
				while(i<Registry.Length && CanRun)
				{
					if(Registry==null || Registry[i]==null)
						break;

					try
					{
						if(Registry[i].Line==Line)
						{
							if(Registry[i].GetType().Equals(typeof(FileSystemFileItem))
								|| Registry[i].GetType().Equals(typeof(TaskItem)))
							{
								//System.Diagnostics.Debug.WriteLine("Synching "+Registry[i].Name);
								((PreviewableItem)Registry[i]).GetThumbnailIfPossible();
								//System.Diagnostics.Debug.WriteLine(Registry[i].Name+" Done");
							}
						}
					}
					catch(Exception)
					{
						System.Diagnostics.Debug.WriteLine(Registry[i].Name + " Failed");
					}
					i++;
				}
			}
			catch(Exception)
			{
				System.Diagnostics.Debug.WriteLine("TS Failed");
			}
			//System.Diagnostics.Debug.WriteLine("TS Done");
			this.Dispose();
		}
		#endregion

		#region IDisposable Members
		/// <summary>
		/// Disposes the ThumbnailSync Object
		/// </summary>
		public void Dispose()
		{
			this.AbortSync();
		}
		#endregion
	}
}
