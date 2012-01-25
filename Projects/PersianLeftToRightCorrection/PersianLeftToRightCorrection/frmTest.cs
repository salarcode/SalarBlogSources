using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PersianLeftToRightCorrection
{
	public partial class frmTest : Form
	{
		public frmTest()
		{
			InitializeComponent();
		}

		void Do()
		{
			txtLTRFa.Text = PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(txtRTL.Text);
			txtLTR.Text = txtRTL.Text;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Do();
		}

		private void txtLTR_TextChanged(object sender, EventArgs e)
		{
			txtLTRFa.Text = PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(txtLTR.Text);
		}

		private void txtRTL_TextChanged(object sender, EventArgs e)
		{
			Do();
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{

		}

		private void frmTest_Load(object sender, EventArgs e)
		{


		}


	}
}
