namespace PersianLeftToRightCorrection
{
	partial class frmTest
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
			this.button1 = new System.Windows.Forms.Button();
			this.txtRTL = new System.Windows.Forms.TextBox();
			this.txtLTR = new System.Windows.Forms.TextBox();
			this.txtLTRFa = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(269, 104);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "تبدیل";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// txtRTL
			// 
			this.txtRTL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtRTL.HideSelection = false;
			this.txtRTL.Location = new System.Drawing.Point(12, 8);
			this.txtRTL.Multiline = true;
			this.txtRTL.Name = "txtRTL";
			this.txtRTL.Size = new System.Drawing.Size(332, 42);
			this.txtRTL.TabIndex = 1;
			this.txtRTL.Text = " متن english در؛ میان <آب (روانه \"the goal\" است) 1 + 2 ...>";
			this.txtRTL.TextChanged += new System.EventHandler(this.txtRTL_TextChanged);
			// 
			// txtLTR
			// 
			this.txtLTR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLTR.HideSelection = false;
			this.txtLTR.Location = new System.Drawing.Point(12, 56);
			this.txtLTR.Multiline = true;
			this.txtLTR.Name = "txtLTR";
			this.txtLTR.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtLTR.Size = new System.Drawing.Size(332, 42);
			this.txtLTR.TabIndex = 1;
			this.txtLTR.TextChanged += new System.EventHandler(this.txtLTR_TextChanged);
			// 
			// txtLTRFa
			// 
			this.txtLTRFa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLTRFa.BackColor = System.Drawing.SystemColors.Window;
			this.txtLTRFa.Location = new System.Drawing.Point(12, 131);
			this.txtLTRFa.Multiline = true;
			this.txtLTRFa.Name = "txtLTRFa";
			this.txtLTRFa.ReadOnly = true;
			this.txtLTRFa.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtLTRFa.Size = new System.Drawing.Size(332, 42);
			this.txtLTRFa.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(350, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "متن راست به چپ";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(350, 59);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "متن چپ به راست";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(350, 131);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(143, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "متن اصلاح شده چپ به راست";
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new System.Drawing.Point(12, 176);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.linkLabel1.Size = new System.Drawing.Size(132, 13);
			this.linkLabel1.TabIndex = 3;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "http://blog.salarcode.com";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// frmTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(498, 194);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtLTRFa);
			this.Controls.Add(this.txtLTR);
			this.Controls.Add(this.txtRTL);
			this.Controls.Add(this.button1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "frmTest";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Text = "نمایش صحیح متون فارسی-انگلیسی در متون چپ به راست";
			this.Load += new System.EventHandler(this.frmTest_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox txtRTL;
		private System.Windows.Forms.TextBox txtLTR;
		private System.Windows.Forms.TextBox txtLTRFa;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.LinkLabel linkLabel1;
	}
}

