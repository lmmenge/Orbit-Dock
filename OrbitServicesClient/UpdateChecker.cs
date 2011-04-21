using System;
using System.Windows.Forms;

using Orbit.Language;
using Orbit.OrbitServices.OrbitServicesHost;

namespace Orbit.OrbitServices.OrbitServicesClient
{
	/// <summary>
	/// Summary description for UpdateChecker.
	/// </summary>
	public sealed class UpdateChecker:IDisposable
	{
		private LanguageLoader Language;
		private float MyVersion;
		private bool RunSilent;
		System.Threading.Thread UpdateThread;
		OrbitServices.OrbitServicesHost.OrbitServices OrbitService=new Orbit.OrbitServices.OrbitServicesHost.OrbitServices();

		#region Class Creation
		public UpdateChecker(string currentVersion)
		{
			Language=new LanguageLoader();
			this.Language.LoadDefaultLanguage();
			MyVersion=float.Parse(currentVersion.Replace(".", "").Trim(), new System.Globalization.NumberFormatInfo())/(float)1000;
		}

		#endregion

		#region Update Procedure
		public void RunUpdateCheck(bool quiet)
		{
			RunSilent=quiet;
			UpdateThread=new System.Threading.Thread(new System.Threading.ThreadStart(CheckForUpdateThread));
			UpdateThread.Name="UpdateThread";
			UpdateThread.Start();
		}

		private void CheckForUpdateThread()
		{
			bool HasUpdate=false;
			try
			{
				float LastVersion=OrbitService.GetLatestVersionNumber();
				if(LastVersion>MyVersion)
					HasUpdate=true;
			}
			catch(Exception)
			{
				if(!RunSilent)
					//MessageBox.Show("Unable to connect from the Orbit Services server.", "Service Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					MessageBox.Show(this.Language.Language.Orbit.Messages.UnableToConnect, "Orbit", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if(HasUpdate)
			{
				//if(MessageBox.Show("An update to Orbit is available.\nDo you want to visit the Orbit Homepage?", "Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Information)==DialogResult.Yes)
				if(MessageBox.Show(this.Language.Language.Orbit.Messages.UpdateAvailable, "Orbit", MessageBoxButtons.YesNo, MessageBoxIcon.Information)==DialogResult.Yes)
				{
					try
					{
						string HpAddress;
						HpAddress=OrbitService.GetHomepageAddress();
						System.Diagnostics.Process.Start(HpAddress);
					}
					catch(Exception)
					{
						//MessageBox.Show("Unable to retrieve the homepage address from the Orbit Services server.", "Service Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						MessageBox.Show(this.Language.Language.Orbit.Messages.UnableToConnect, "Orbit", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
			else
			{
				if(!RunSilent)
					//MessageBox.Show("You have the latest version of Orbit.", "Orbit", MessageBoxButtons.OK, MessageBoxIcon.Information);
					MessageBox.Show(this.Language.Language.Orbit.Messages.YouAreUpToDate, "Orbit", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		#endregion

		#region IDisposable Members
		public void Dispose()
		{
			OrbitService.Dispose();
		}

		#endregion
	}
}
