using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TechnicianMap.Classes
{
	public class DBO
	{
		public SqlConnection conn = new SqlConnection();
		public string DBpath;

		private string _username;
		public string UserName
		{
			get { return _username; }
			set { _username = value; }
		}

		private string _password;
		public string PassWord
		{
			get { return _password; }
			set { _password = value; }
		}

		private string _databasename;
		public string DataBaseName
		{
			get { return _databasename; }
			set { _databasename = value; }
		}
		public bool _debugmodechecked;
		public bool debugmodechecked
		{
			get { return _debugmodechecked; }
			set { _debugmodechecked = value; }
		}
		public string Constring;
		public DBO()
		{
			//this.Constring = "Data Source={0};user ID={1};password={2};ServerType=REMOTE;TrimTrailingSpaces=True;";
			// this.SqlConstring = "Server={0};Database=IwData;Trusted_Connection=Yes;";
			//this.Constring = "Server={0};Database=IwData; User Id = SA; Password = 12345;";
			//Server=myServerName\myInstanceName;Database=myDataBase;User Id=myUsername; Password = myPassword;
			this.Constring = " Server={0}; User Id = {1}; password = {2};Database={3} ;";
		}

		//Connecting
		public bool Connect()
		{
			if (this.conn.State == ConnectionState.Open)
				return true;
			else
			{
				if (
						!string.IsNullOrEmpty(this.Constring) &&
						!string.IsNullOrEmpty(this.DBpath)
					)
				{
					this.conn.ConnectionString = string.Format(this.Constring, this.DBpath, this.UserName, this.PassWord, this.DataBaseName);
					this.conn.Open();
				}

				if (this.conn.State != ConnectionState.Open)
					return false;
				else
					return true;
			}
		}
		public void Close()
		{
			if (this.conn.State == ConnectionState.Open)
			{
				this.conn.Close();
			}
		}
		/// <summary>
		/// Fetch result to DataTable
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public DataTable Fetch_dt(string query)
		{
			bool IsOpen = false;
			DataTable dtable = new DataTable();
			dtable.Locale = System.Globalization.CultureInfo.InvariantCulture;
			try
			{
				/* In case connection is closed , the open it */
				if (this.conn == null || this.conn.State != ConnectionState.Open)
				{
					this.Connect();
					/* Is opened by this */
					IsOpen = true;
				}

				SqlCommand command = new SqlCommand(query, this.conn);
				SqlDataAdapter adapter = new SqlDataAdapter();
				/* Execute command */
				adapter.SelectCommand = command;
				command.CommandTimeout = 9000;  //timeout
				adapter.Fill(dtable);

				return dtable;
			}
			catch (Exception ex)
			{
				if (debugmodechecked)
				{ MessageBox.Show(ex.Message + "Query= " + query); }
				else
				{ MessageBox.Show(ex.Message); }
				return dtable;
			}
			finally
			{
				/* if opened by this, then will be closed by this */
				if (IsOpen)
				{
					/* Close connection */
					this.Close();
				}
			}
		}
	}
}
