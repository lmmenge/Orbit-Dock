using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Orbit.OrbitServices;
using Orbit.OrbitServices.OrbitServicesHost;

namespace Orbit.OrbitServices.OrbitServicesClient
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class ExtendedErrorInfo : System.Windows.Forms.Form
	{
		private string ErrorInfo;

		private System.Windows.Forms.Panel ErrorPanel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox ErrorBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox ContactName;
		private System.Windows.Forms.TextBox ContactEmail;
		private System.Windows.Forms.TextBox ErrorDescription;
		private System.Windows.Forms.Button SendButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ExtendedErrorInfo()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ExtendedErrorInfo));
			this.ErrorPanel = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.ErrorBox = new System.Windows.Forms.PictureBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.ContactName = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.ContactEmail = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.ErrorDescription = new System.Windows.Forms.TextBox();
			this.SendButton = new System.Windows.Forms.Button();
			this.ErrorPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// ErrorPanel
			// 
			this.ErrorPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.ErrorPanel.BackColor = System.Drawing.Color.White;
			this.ErrorPanel.Controls.Add(this.label2);
			this.ErrorPanel.Controls.Add(this.label1);
			this.ErrorPanel.Controls.Add(this.ErrorBox);
			this.ErrorPanel.Location = new System.Drawing.Point(0, 0);
			this.ErrorPanel.Name = "ErrorPanel";
			this.ErrorPanel.Size = new System.Drawing.Size(452, 56);
			this.ErrorPanel.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(56, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(260, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Help the developer by providing extra information";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(52, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(300, 32);
			this.label1.TabIndex = 2;
			this.label1.Text = "Orbit Error Information";
			// 
			// ErrorBox
			// 
			this.ErrorBox.Image = ((System.Drawing.Image)(resources.GetObject("ErrorBox.Image")));
			this.ErrorBox.Location = new System.Drawing.Point(4, 4);
			this.ErrorBox.Name = "ErrorBox";
			this.ErrorBox.Size = new System.Drawing.Size(48, 48);
			this.ErrorBox.TabIndex = 1;
			this.ErrorBox.TabStop = false;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(8, 92);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(124, 16);
			this.label3.TabIndex = 3;
			this.label3.Text = "Contact Information";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 108);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 16);
			this.label4.TabIndex = 4;
			this.label4.Text = "Contact Name:";
			// 
			// ContactName
			// 
			this.ContactName.Location = new System.Drawing.Point(12, 124);
			this.ContactName.Name = "ContactName";
			this.ContactName.Size = new System.Drawing.Size(212, 21);
			this.ContactName.TabIndex = 0;
			this.ContactName.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(232, 108);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 16);
			this.label5.TabIndex = 6;
			this.label5.Text = "Contact E-Mail:";
			// 
			// ContactEmail
			// 
			this.ContactEmail.Location = new System.Drawing.Point(232, 124);
			this.ContactEmail.Name = "ContactEmail";
			this.ContactEmail.Size = new System.Drawing.Size(212, 21);
			this.ContactEmail.TabIndex = 1;
			this.ContactEmail.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 60);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(436, 28);
			this.label6.TabIndex = 8;
			this.label6.Text = "All of the fields below are optional, and you may fill them to provide extra info" +
				"rmation about this error for the developer.";
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.Location = new System.Drawing.Point(8, 152);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(108, 16);
			this.label7.TabIndex = 9;
			this.label7.Text = "Error Information";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(12, 168);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(432, 28);
			this.label8.TabIndex = 10;
			this.label8.Text = "Use the field below to provide a brief description of how the error happened and/" +
				"or computer specs so that the developer can better track the error cause.";
			// 
			// ErrorDescription
			// 
			this.ErrorDescription.Location = new System.Drawing.Point(12, 196);
			this.ErrorDescription.Multiline = true;
			this.ErrorDescription.Name = "ErrorDescription";
			this.ErrorDescription.Size = new System.Drawing.Size(432, 76);
			this.ErrorDescription.TabIndex = 2;
			this.ErrorDescription.Text = "";
			// 
			// SendButton
			// 
			this.SendButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.SendButton.Location = new System.Drawing.Point(368, 280);
			this.SendButton.Name = "SendButton";
			this.SendButton.TabIndex = 3;
			this.SendButton.Text = "Send";
			this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
			// 
			// ExtendedErrorInfo
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(450, 311);
			this.Controls.Add(this.SendButton);
			this.Controls.Add(this.ErrorDescription);
			this.Controls.Add(this.ContactEmail);
			this.Controls.Add(this.ContactName);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.ErrorPanel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ExtendedErrorInfo";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Extended Error Information";
			this.ErrorPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Send Report Code
		private bool SendReport(string errorInfo, string extendedErrorInformation)
		{
			bool ReportStatus=false;
			try
			{
				OrbitServices.OrbitServicesHost.OrbitServices OrbitService=new OrbitServices.OrbitServicesHost.OrbitServices();
				using(OrbitService)
				{
					//ReportStatus=OrbitService.ReportError(ErrorInfo);
					ReportStatus=OrbitService.ReportErrorWithInfo(errorInfo, extendedErrorInformation);
				}
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
				ReportStatus=false;
			}
			return ReportStatus;
		}
		#endregion

		#region Form Event Handlers
		private void SendButton_Click(object sender, System.EventArgs e)
		{
			this.SendButton.Enabled=false;
			string VersionInfo="";
			try
			{
				string ApplicationPath=Application.ExecutablePath.Substring(0,Application.ExecutablePath.Length-(Application.ExecutablePath.Length-Application.ExecutablePath.LastIndexOf("\\")))+"\\";
				System.Reflection.AssemblyName OrbitName = System.Reflection.AssemblyName.GetAssemblyName(ApplicationPath+"Orbit.exe");
				VersionInfo=OrbitName.Version.ToString();
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
			}

			string ExtendedInfo="SenderName="+this.ContactName.Text;
			ExtendedInfo+="\r\nSenderEmail="+this.ContactEmail.Text;
			//System.Globalization.DateTimeFormatInfo dtfi=new System.Globalization.DateTimeFormatInfo();
			//ExtendedInfo+="\r\nDate="+System.DateTime.Today.ToString();
			ExtendedInfo+="\r\nDate="+System.DateTime.Today.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
			ExtendedInfo+="\r\nIsFixed=False";
			ExtendedInfo+="\r\nErrorDescription="+this.ErrorDescription.Text.Replace("\r\n", "<br>");
			ExtendedInfo+="\r\nPresentInVersion="+VersionInfo;
			ExtendedInfo+="\r\nNotes=";

			bool ReportResult=this.SendReport(this.ErrorInfo, ExtendedInfo);
			if(ReportResult)
			{
				MessageBox.Show("Your report was successfully sent.", "Reporting Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("Your report could not be sent.\nPossible causes are that you are not connected to the internet or that the service is offline.", "Reporting Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
			this.Close();
		}

		#endregion

		#region Public Functions
		public void SendExtendedError(string ErrorInformation)
		{
			this.ErrorInfo=ErrorInformation;
			this.ShowDialog();
		}
		#endregion
	}
}
