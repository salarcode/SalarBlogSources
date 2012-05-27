namespace SQLServerPasswordChanger
{
	partial class ProjectInstaller
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

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.svcProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
			this.svcInstaller = new System.ServiceProcess.ServiceInstaller();
			// 
			// svcProcessInstaller
			// 
			this.svcProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
			this.svcProcessInstaller.Password = null;
			this.svcProcessInstaller.Username = null;
			// 
			// svcInstaller
			// 
			this.svcInstaller.Description = "Start this server manual, it only run once. Put the sql command in text file next" +
    " to the exe file.";
			this.svcInstaller.DisplayName = "SQLServer Password Changer";
			this.svcInstaller.ServiceName = "SQLServerPwd";
			// 
			// ProjectInstaller
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.svcProcessInstaller,
            this.svcInstaller});

		}

		#endregion

		private System.ServiceProcess.ServiceProcessInstaller svcProcessInstaller;
		private System.ServiceProcess.ServiceInstaller svcInstaller;
	}
}