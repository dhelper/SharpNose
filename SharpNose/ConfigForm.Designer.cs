/*
 * Created by SharpDevelop.
 * User: Dror B. Helper
 * Date: 18/11/2009
 * Time: 21:33
 * 
 */
namespace SharpNose
{
	partial class ConfigForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.pbSymbol = new System.Windows.Forms.PictureBox();
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.lblNunitDirectoryName = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.lblNunitDirectory = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pbSymbol)).BeginInit();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(12, 149);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(93, 149);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// pbSymbol
			// 
			this.pbSymbol.BackgroundImage = global::SharpNose.Resources.Crystal_Clear_app_All_software_is_current;
			this.pbSymbol.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.pbSymbol.InitialImage = global::SharpNose.Resources.Crystal_Clear_app_All_software_is_current;
			this.pbSymbol.Location = new System.Drawing.Point(13, 13);
			this.pbSymbol.Name = "pbSymbol";
			this.pbSymbol.Size = new System.Drawing.Size(75, 69);
			this.pbSymbol.TabIndex = 2;
			this.pbSymbol.TabStop = false;
			// 
			// folderBrowserDialog
			// 
			this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.ProgramFiles;
			// 
			// lblNunitDirectoryName
			// 
			this.lblNunitDirectoryName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.lblNunitDirectoryName.Location = new System.Drawing.Point(13, 89);
			this.lblNunitDirectoryName.Name = "lblNunitDirectoryName";
			this.lblNunitDirectoryName.Size = new System.Drawing.Size(144, 23);
			this.lblNunitDirectoryName.TabIndex = 3;
			this.lblNunitDirectoryName.Text = "Nunit Directory:";
			this.lblNunitDirectoryName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// button1
			// 
			this.button1.BackgroundImage = global::SharpNose.Resources.Crystal_Clear_filesystem_folder_grey_open;
			this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.button1.Location = new System.Drawing.Point(360, 103);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(32, 32);
			this.button1.TabIndex = 4;
			this.button1.UseVisualStyleBackColor = true;
			// 
			// lblNunitDirectory
			// 
			this.lblNunitDirectory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblNunitDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.lblNunitDirectory.Location = new System.Drawing.Point(13, 112);
			this.lblNunitDirectory.Name = "lblNunitDirectory";
			this.lblNunitDirectory.Size = new System.Drawing.Size(341, 23);
			this.lblNunitDirectory.TabIndex = 5;
			this.lblNunitDirectory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ConfigForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(404, 180);
			this.ControlBox = false;
			this.Controls.Add(this.lblNunitDirectory);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.lblNunitDirectoryName);
			this.Controls.Add(this.pbSymbol);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConfigForm";
			this.Text = "#Nose Configration";
			((System.ComponentModel.ISupportInitialize)(this.pbSymbol)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label lblNunitDirectoryName;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label lblNunitDirectory;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.PictureBox pbSymbol;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
	}
}
