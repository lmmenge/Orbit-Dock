using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
//using System.Data;

using Orbit.OrbitServices;
using Orbit.OrbitServices.OrbitServicesHost;

namespace Orbit.OrbitServices.OrbitServicesClient
{
	/// <summary>
	/// Error report window
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class ErrorReport : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox ErrorBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel ErrorPanel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox ErrorInformationBox;
		private System.Windows.Forms.Panel DetailsPanel;
		private System.Windows.Forms.Panel QuestionPanel;
		private System.Windows.Forms.Button SendButton;
		private System.Windows.Forms.Button DontReportButton;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.LinkLabel ShowDetailsLink;
		private string ErrorInfo;

		public ErrorReport(string errorInformation)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			ErrorInfo=errorInformation;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ErrorReport));
			this.ErrorPanel = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.ErrorBox = new System.Windows.Forms.PictureBox();
			this.label3 = new System.Windows.Forms.Label();
			this.DetailsPanel = new System.Windows.Forms.Panel();
			this.ShowDetailsLink = new System.Windows.Forms.LinkLabel();
			this.ErrorInformationBox = new System.Windows.Forms.TextBox();
			this.QuestionPanel = new System.Windows.Forms.Panel();
			this.SendButton = new System.Windows.Forms.Button();
			this.DontReportButton = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.ErrorPanel.SuspendLayout();
			this.DetailsPanel.SuspendLayout();
			this.QuestionPanel.SuspendLayout();
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
			this.ErrorPanel.Size = new System.Drawing.Size(424, 56);
			this.ErrorPanel.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(56, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(208, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Orbit Error Handling and Reporting Tool";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(52, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(144, 32);
			this.label1.TabIndex = 2;
			this.label1.Text = "Orbit Error";
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
			this.label3.Location = new System.Drawing.Point(8, 60);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(408, 56);
			this.label3.TabIndex = 2;
			this.label3.Text = @"An unexpected error has happened while you were running Orbit. All the needed error and debug information regarding this error was successfully caught. This information regards only what was happening inside Orbit, and no personal information is contained in it.";
			// 
			// DetailsPanel
			// 
			this.DetailsPanel.BackColor = System.Drawing.SystemColors.Control;
			this.DetailsPanel.Controls.Add(this.ShowDetailsLink);
			this.DetailsPanel.Controls.Add(this.ErrorInformationBox);
			this.DetailsPanel.Location = new System.Drawing.Point(8, 120);
			this.DetailsPanel.Name = "DetailsPanel";
			this.DetailsPanel.Size = new System.Drawing.Size(408, 104);
			this.DetailsPanel.TabIndex = 1;
			this.DetailsPanel.Tag = "104";
			// 
			// ShowDetailsLink
			// 
			this.ShowDetailsLink.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.ShowDetailsLink.Location = new System.Drawing.Point(4, 4);
			this.ShowDetailsLink.Name = "ShowDetailsLink";
			this.ShowDetailsLink.Size = new System.Drawing.Size(400, 16);
			this.ShowDetailsLink.TabIndex = 0;
			this.ShowDetailsLink.TabStop = true;
			this.ShowDetailsLink.Text = "Click here for details on this error";
			this.ShowDetailsLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ShowDetailsLink_LinkClicked);
			// 
			// ErrorInformationBox
			// 
			this.ErrorInformationBox.Location = new System.Drawing.Point(4, 20);
			this.ErrorInformationBox.Multiline = true;
			this.ErrorInformationBox.Name = "ErrorInformationBox";
			this.ErrorInformationBox.ReadOnly = true;
			this.ErrorInformationBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.ErrorInformationBox.Size = new System.Drawing.Size(400, 80);
			this.ErrorInformationBox.TabIndex = 1;
			this.ErrorInformationBox.Text = "";
			// 
			// QuestionPanel
			// 
			this.QuestionPanel.Controls.Add(this.SendButton);
			this.QuestionPanel.Controls.Add(this.DontReportButton);
			this.QuestionPanel.Controls.Add(this.label5);
			this.QuestionPanel.Location = new System.Drawing.Point(8, 228);
			this.QuestionPanel.Name = "QuestionPanel";
			this.QuestionPanel.Size = new System.Drawing.Size(408, 60);
			this.QuestionPanel.TabIndex = 0;
			// 
			// SendButton
			// 
			this.SendButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.SendButton.Location = new System.Drawing.Point(250, 35);
			this.SendButton.Name = "SendButton";
			this.SendButton.TabIndex = 0;
			this.SendButton.Text = "Send";
			this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
			// 
			// DontReportButton
			// 
			this.DontReportButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.DontReportButton.Location = new System.Drawing.Point(330, 35);
			this.DontReportButton.Name = "DontReportButton";
			this.DontReportButton.TabIndex = 1;
			this.DontReportButton.Text = "Don\'t Send";
			this.DontReportButton.Click += new System.EventHandler(this.DontReportButton_Click);
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.Location = new System.Drawing.Point(2, 3);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(404, 28);
			this.label5.TabIndex = 8;
			this.label5.Text = "Do you want to send this information to the Orbit developer in order to try to fi" +
				"x this error in the future?";
			// 
			// ErrorReport
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(422, 295);
			this.Controls.Add(this.QuestionPanel);
			this.Controls.Add(this.DetailsPanel);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.ErrorPanel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ErrorReport";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Orbit Error";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ErrorPanel.ResumeLayout(false);
			this.DetailsPanel.ResumeLayout(false);
			this.QuestionPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Panel Code
		private void ShowPanel(Panel PanelToShow)
		{
			int OldHeight=PanelToShow.Height;

			this.DetailsPanel.Height=20;

			this.QuestionPanel.Top=144;
			this.Height=236;

			if(OldHeight==20)
			{
				PanelToShow.Height=Convert.ToInt32(PanelToShow.Tag, new System.Globalization.NumberFormatInfo());

				switch(((System.Windows.Forms.Panel)PanelToShow).Name.ToLower(System.Globalization.CultureInfo.InvariantCulture))
				{
					case "detailspanel":
						this.QuestionPanel.Top+=PanelToShow.Height-20;
						this.Height=320;
						break;
				}
			}
		}
		#endregion

		#region Form Event Handlers
		private void Form1_Load(object sender, System.EventArgs e)
		{
			this.ShowPanel(DetailsPanel);
			this.ErrorInformationBox.Text=this.ErrorInfo;
		}

		private void DontReportButton_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void ShowDetailsLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.ShowPanel(DetailsPanel);
		}

		private void SendButton_Click(object sender, System.EventArgs e)
		{
			this.Hide();
			OrbitServicesClient.ExtendedErrorInfo  ErrorInfo=new ExtendedErrorInfo();
			ErrorInfo.SendExtendedError(this.ErrorInfo);
			this.Close();
		}
		#endregion

		#region Static Functions
		public static bool WasReported(string errorData)
		{
			try
			{
				OrbitServices.OrbitServicesHost.OrbitServices os=new OrbitServices.OrbitServicesHost.OrbitServices();
				return os.WasReported(errorData);
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
				return false;
			}
		}
		#endregion
	}
}
