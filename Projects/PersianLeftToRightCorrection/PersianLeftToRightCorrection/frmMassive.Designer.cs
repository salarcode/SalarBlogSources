namespace PersianLeftToRightCorrection
{
	partial class frmMassive
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMassive));
			this.lnkWebLog = new System.Windows.Forms.LinkLabel();
			this.btnSrc = new System.Windows.Forms.Button();
			this.txtSrc = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkLineByLine = new System.Windows.Forms.CheckBox();
			this.btnConvert = new System.Windows.Forms.Button();
			this.txtDest = new System.Windows.Forms.TextBox();
			this.btnDest = new System.Windows.Forms.Button();
			this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
			this.dlgSave = new System.Windows.Forms.SaveFileDialog();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lnkWebLog
			// 
			this.lnkWebLog.AutoSize = true;
			this.lnkWebLog.Location = new System.Drawing.Point(16, 117);
			this.lnkWebLog.Name = "lnkWebLog";
			this.lnkWebLog.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lnkWebLog.Size = new System.Drawing.Size(136, 13);
			this.lnkWebLog.TabIndex = 4;
			this.lnkWebLog.TabStop = true;
			this.lnkWebLog.Text = "http://blog.salarcode.com/";
			this.lnkWebLog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkWebLog_LinkClicked);
			// 
			// btnSrc
			// 
			this.btnSrc.Location = new System.Drawing.Point(372, 18);
			this.btnSrc.Name = "btnSrc";
			this.btnSrc.Size = new System.Drawing.Size(75, 23);
			this.btnSrc.TabIndex = 5;
			this.btnSrc.Text = "فایل مبدا";
			this.btnSrc.UseVisualStyleBackColor = true;
			this.btnSrc.Click += new System.EventHandler(this.btnSrc_Click);
			// 
			// txtSrc
			// 
			this.txtSrc.Location = new System.Drawing.Point(19, 20);
			this.txtSrc.Name = "txtSrc";
			this.txtSrc.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtSrc.Size = new System.Drawing.Size(347, 21);
			this.txtSrc.TabIndex = 6;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkLineByLine);
			this.groupBox1.Controls.Add(this.lnkWebLog);
			this.groupBox1.Controls.Add(this.btnConvert);
			this.groupBox1.Controls.Add(this.txtDest);
			this.groupBox1.Controls.Add(this.txtSrc);
			this.groupBox1.Controls.Add(this.btnDest);
			this.groupBox1.Controls.Add(this.btnSrc);
			this.groupBox1.Location = new System.Drawing.Point(13, 13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(470, 137);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "تبدیل";
			// 
			// chkLineByLine
			// 
			this.chkLineByLine.AutoSize = true;
			this.chkLineByLine.Checked = true;
			this.chkLineByLine.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkLineByLine.Location = new System.Drawing.Point(268, 74);
			this.chkLineByLine.Name = "chkLineByLine";
			this.chkLineByLine.Size = new System.Drawing.Size(98, 17);
			this.chkLineByLine.TabIndex = 8;
			this.chkLineByLine.Text = "تبدیل خط به خط";
			this.chkLineByLine.UseVisualStyleBackColor = true;
			// 
			// btnConvert
			// 
			this.btnConvert.Location = new System.Drawing.Point(281, 97);
			this.btnConvert.Name = "btnConvert";
			this.btnConvert.Size = new System.Drawing.Size(85, 33);
			this.btnConvert.TabIndex = 7;
			this.btnConvert.Text = "انجام تبدیل";
			this.btnConvert.UseVisualStyleBackColor = true;
			this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
			// 
			// txtDest
			// 
			this.txtDest.Location = new System.Drawing.Point(19, 47);
			this.txtDest.Name = "txtDest";
			this.txtDest.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtDest.Size = new System.Drawing.Size(347, 21);
			this.txtDest.TabIndex = 6;
			// 
			// btnDest
			// 
			this.btnDest.Location = new System.Drawing.Point(372, 45);
			this.btnDest.Name = "btnDest";
			this.btnDest.Size = new System.Drawing.Size(75, 23);
			this.btnDest.TabIndex = 5;
			this.btnDest.Text = "فایل مقصد";
			this.btnDest.UseVisualStyleBackColor = true;
			this.btnDest.Click += new System.EventHandler(this.btnDest_Click);
			// 
			// dlgOpen
			// 
			this.dlgOpen.DefaultExt = "*.*";
			this.dlgOpen.Filter = "*.*|*.*";
			// 
			// dlgSave
			// 
			this.dlgSave.DefaultExt = "*.*|*.*";
			this.dlgSave.Filter = "*.*|*.*";
			// 
			// frmMassive
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(494, 161);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmMassive";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Text = "نمایش صحیح متون فارسی-انگلیسی در متون چپ به راست";
			this.Load += new System.EventHandler(this.frmMassive_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.LinkLabel lnkWebLog;
		private System.Windows.Forms.Button btnSrc;
		private System.Windows.Forms.TextBox txtSrc;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnConvert;
		private System.Windows.Forms.TextBox txtDest;
		private System.Windows.Forms.Button btnDest;
		private System.Windows.Forms.CheckBox chkLineByLine;
		private System.Windows.Forms.OpenFileDialog dlgOpen;
		private System.Windows.Forms.SaveFileDialog dlgSave;
	}
}