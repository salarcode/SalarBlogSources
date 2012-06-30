using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace SQLServerPasswordChanger
{
	public partial class TheService : ServiceBase
	{
		public TheService()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			var appPath = Path.GetDirectoryName(Application.ExecutablePath);
			try
			{
				using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString))
				{
					conn.Open();
					var sqlCommand = File.ReadAllText(Path.Combine(appPath, "cmd.sql"));
					sqlCommand = ReplaceStrEx(sqlCommand, "GO", "\n;\n", StringComparison.InvariantCulture);

					using (var cmd = conn.CreateCommand())
					{
						cmd.CommandText = sqlCommand;
						int rows = cmd.ExecuteNonQuery();

						File.WriteAllText(Path.Combine(appPath, Program.LogFilename), "Done, " + rows + " rows affected.");
					}
				}
			}
			catch (Exception ex)
			{
				File.WriteAllText(Path.Combine(appPath, Program.LogFilename), ex.ToString());
			}
			this.Stop();
		}

		protected override void OnStop()
		{
		}


		/// <summary>
		/// Implements fast string replacing algorithm for CS
		/// </summary>
		public static string ReplaceStrEx(string original, string pattern, string replacement, StringComparison comparisonType)
		{
			if (original == null)
			{
				return null;
			}

			if (String.IsNullOrEmpty(pattern))
			{
				return original;
			}

			int lenPattern = pattern.Length;
			int idxPattern = -1;
			int idxLast = 0;

			StringBuilder result = new StringBuilder();
			try
			{
				while (true)
				{
					idxPattern = original.IndexOf(pattern, idxPattern + 1, comparisonType);

					if (idxPattern < 0)
					{
						result.Append(original, idxLast, original.Length - idxLast);

						break;
					}

					result.Append(original, idxLast, idxPattern - idxLast);
					result.Append(replacement);

					idxLast = idxPattern + lenPattern;
				}

				return result.ToString();
			}
			finally
			{
				result.Length = 0;
			}
		}

	}
}
