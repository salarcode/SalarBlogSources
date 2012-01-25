using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace PersianLeftToRightCorrection
{
	public partial class frmMassive : Form
	{
		public frmMassive()
		{
			InitializeComponent();
		}

		private void lnkWebLog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				Process.Start(new ProcessStartInfo
								{
									FileName = lnkWebLog.Text,
									UseShellExecute = true
								});
			}
			catch (Exception)
			{

			}
		}

		private void btnSrc_Click(object sender, EventArgs e)
		{
			if (dlgOpen.ShowDialog() == DialogResult.OK)
				txtSrc.Text = dlgOpen.FileName;
		}

		private void btnDest_Click(object sender, EventArgs e)
		{
			if (dlgSave.ShowDialog() == DialogResult.OK)
				txtDest.Text = dlgSave.FileName;
		}

		private void btnConvert_Click(object sender, EventArgs e)
		{
			if (!File.Exists(txtSrc.Text))
			{
				MessageBox.Show("File not found!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			try
			{
				using (var reader = new StreamReader(txtSrc.Text, true))
				using (var writer = new StreamWriter(txtDest.Text, false))
				{
					if (chkLineByLine.Checked)
					{
						while (!reader.EndOfStream)
						{
							string line = reader.ReadLine();
							line = PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(line);
							writer.WriteLine(line);
						}
					}
					else
					{
						var file = reader.ReadToEnd();
						file = PersianLeftToRightText.CorrectPersinRtlToDisplayLtr(file);
						writer.Write(file);
					}
				}
				MessageBox.Show("Convertion is done.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void frmMassive_Load(object sender, EventArgs e)
		{

		}
	}
}
