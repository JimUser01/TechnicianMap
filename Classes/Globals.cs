using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;


namespace TechnicianMap.Classes
{
	class Globals
	{
		public static Form1 Form1;


		public static string encryptpass = "45";
		public static DBO conn;
		public static string GlobalUsername = "";
		public static string GlobalPassword = "";
		public static string dbConnectUserName = "MainRoadUser";
		public static string dbConnectPassword = "7028";
		public static string dbConnectPath = "";
		public static bool isLocal = true;
		public static bool ShowIcons = true;
		public static int GridFontSize = 10;
		public static string CategoryIconSizeLimit = "100";

		public static string GetToken()
		{
			return (5 * int.Parse(DateTime.Now.Day.ToString())).ToString() + (13 * int.Parse(DateTime.Now.Month.ToString())).ToString();
		}
		public static DataTable SqlToTable(string SqlString)
		{
			try
			{
				DataTable ReturnDt = null;
				if (Globals.isLocal)
				{
					ReturnDt = Globals.conn.Fetch_dt(SqlString);
				}
				//else
				//{
				//	using (EmsWebService.Service1 service = new EmsWebService.Service1()) // 
				//	{
				//		string Token = GetToken();
				//		SqlString = SSTCryptographer.Encrypt(SqlString, Globals.encryptpass);
				//		service.Url = Globals.WebUrl;//.Substring(0, Globals.WebUrl.Length - 1);
				//		service.Timeout = System.Threading.Timeout.Infinite;
				//		service.Credentials = new NetworkCredential(Globals.IIsUserName, Globals.IISPassword);
				//		ReturnDt = (DataTable)JsonConvert.DeserializeObject(service.GeneralTable(UserName: GlobalUsername, Password: GlobalPassword, UserCode: Token, SQlCode: SqlString), (typeof(DataTable)));
				//	}
				//}
				return ReturnDt;
			}
			catch (Exception ae)
			{
				MessageBox.Show(ae.Message); return null;
			}
		}
		public static void SqlSimple(string SqlString)
		{
			try
			{
				//DataTable ReturnDt = null;
				if (Globals.isLocal)
				{
					Globals.conn.Fetch_dt(SqlString);
				}
				//else
				//{
				//	using (EmsWebService.Service1 service = new EmsWebService.Service1()) // l
				//	{
				//		string Token = GetToken();
				//		SqlString = SSTCryptographer.Encrypt(SqlString, Globals.encryptpass);
				//		service.Url = Globals.WebUrl;//.Substring(0, Globals.WebUrl.Length - 1);.Substring(0, Globals.WebUrl.Length - 1);
				//		service.Timeout = System.Threading.Timeout.Infinite;
				//		service.Credentials = new NetworkCredential(Globals.IIsUserName, Globals.IISPassword);
				//		//ReturnDt = (DataTable)JsonConvert.DeserializeObject(service.GeneralUsageCrm (GlobalUsername, GlobalPassword, Token, SqlString), (typeof(DataTable)));
				//		service.GeneralUsage(GlobalUsername, GlobalPassword, Token, SqlString);
				//	}
				//}
				// ReturnDt = null;

			}
			catch (Exception ae)
			{
				//MessageBox.Show(ae.Message);
			}
		}

		public static void SetAllGridSizes(Control parent)
		{
			foreach (Control c in parent.Controls)
			{
				if (c.GetType() == typeof(DataGridView))
				{
					//if (c.Name != "UsersDataGrid")     // για καποιο λογο χτυπαει αυτό. ΠΑραμετροι Ασφάλεια
					//{
					//	((DataGridView)c).AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
					//	((DataGridView)c).DefaultCellStyle.WrapMode = DataGridViewTriState.True;
					//}
					((DataGridView)c).EnableHeadersVisualStyles = true;
					((DataGridView)c).ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.SystemColors.GradientActiveCaption;// Color.LightSteelBlue;
					((DataGridView)c).EnableHeadersVisualStyles = true;
					((DataGridView)c).CellBorderStyle = DataGridViewCellBorderStyle.Raised;
					((DataGridView)c).BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;  //System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
					((DataGridView)c).DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", Globals.GridFontSize);
					((DataGridView)c).RowsDefaultCellStyle.BackColor = System.Drawing.SystemColors.GradientActiveCaption;  //System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
					((DataGridView)c).AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;   //System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(210)))), ((int)(((byte)(251)))));
					((DataGridView)c).RowHeadersVisible = false;
					((DataGridView)c).AllowUserToAddRows = false;
					((DataGridView)c).AllowUserToOrderColumns = true;
					((DataGridView)c).AllowUserToResizeColumns = true;
					((DataGridView)c).SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
				}
				else
				{
					SetAllGridSizes(c);
				}
			}
		}
		public static void DoubleBuffered(DataGridView dgv, bool setting)
		{
			Type dgvType = dgv.GetType();
			PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
			pi.SetValue(dgv, setting, null);
		}

		
	}
}
