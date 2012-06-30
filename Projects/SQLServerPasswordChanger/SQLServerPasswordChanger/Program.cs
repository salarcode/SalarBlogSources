using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace SQLServerPasswordChanger
{
	static class Program
	{
		public const string LogFilename = "result.log";

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main()
		{
			Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

			if (Environment.UserInteractive)
			{
				bool installed;
				bool started;
				CheckService("SQLServerPwd", out installed, out started);
				if (installed)
				{
					if (MessageBox.Show("Do you want to uinstall the 'SQL Server Password Changer' service?", "SQLServerPwd", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						try
						{
							UninstallService();
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
				else
				{
					if (MessageBox.Show("Do you want to install the 'SQL Server Password Changer' service?", "SQLServerPwd", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						try
						{
							InstallService();
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
			}
			else
			{
				ServiceBase[] ServicesToRun;
				ServicesToRun = new ServiceBase[] 
				{ 
					new TheService() 
				};
				ServiceBase.Run(ServicesToRun);
			}
		}

		static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			if (Environment.UserInteractive)
			{
				MessageBox.Show(e.Exception.ToString());
			}
			else
			{
				File.WriteAllText(LogFilename, e.ToString());
			}
		}


		/// <summary>
		/// Strart stopped service
		/// </summary>
		public static void StartService(string svcName)
		{
			try
			{
				var svc = new ServiceController(svcName);
				svc.Start();
			}
			catch (Exception)
			{
			}
		}

		public static void InstallService()
		{
			var ti = new TransactedInstaller();
			ti.Installers.Add(new ProjectInstaller());
			ti.Context = new InstallContext("", null);

			string path = Application.ExecutablePath;
			ti.Context.Parameters["assemblypath"] = path;

			ti.Install(new Hashtable());
		}

		public static void UninstallService()
		{
			var ti = new TransactedInstaller();
			ti.Installers.Add(new ProjectInstaller());
			ti.Context = new InstallContext("", null);
			string path = Application.ExecutablePath;

			ti.Context.Parameters["assemblypath"] = path;
			ti.Uninstall(null);
		}



		public static void CheckService(string svcName, out bool installed, out bool running)
		{
			installed = false;
			running = false;

			try
			{
				var svc = new ServiceController(svcName);
				running = svc.Status == ServiceControllerStatus.Running;
				installed = true;
			}
			catch (Exception)
			{
				// exception means not installed
				installed = false;
			}
		}

	}
}
