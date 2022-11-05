using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using TechnicianMap.Classes;

namespace TechnicianMap
{
	public partial class Form1 : Form
	{
		#region Variables
		string appName = "TechnicianMap.exe";
		string Apptitle = "Extra Assistance Live Ver:05/11/2022 ";
		string appPath = AppDomain.CurrentDomain.BaseDirectory;
		DBO conn = new DBO();
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
				return cp;
			}
		}
		#endregion Variables
		public Form1()
		{
			InitializeComponent();
		}

		#region Form Events
		private void Form1_Load(object sender, EventArgs e)
		{
			try
			{
				SetFormSizes();
				CreateValuesinConfig();
				Globals.conn = conn;
				CreateSqlConnection();
				FillListGrid(0);
				Update.Visible = CheckIfNewVersionExists() ? true : false;
			}
			catch (Exception ae)
			{
				MessageBox.Show(ae.Message);
			}
		}
		#endregion Form Events

		#region Initialize
		private void SetFormSizes()// εδω χτυπαει και το form resize
		{
			try
			{
				this.SetBounds(10, 10, 780, 700);
				MainPanel.Dock = DockStyle.Fill;
				ListGrid.Dock = DockStyle.Fill;
				ListGrid.BringToFront();
				Globals.SetAllGridSizes(this);
				Globals.DoubleBuffered(ListGrid,true);
			}
			catch (Exception ae)
			{
				MessageBox.Show(ae.Message);
			}
		}
		private void CreateValuesinConfig()
		{
			try
			{
				var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None); var settings = configFile.AppSettings.Settings;
				//your project needs a reference to System.Configuration.dll

				//iw setup
				if (settings["dbConnectPassword"] == null)
				{
					settings.Add("dbConnectPassword", "");
				}

				if (settings["LoginPathText"] == null)
				{
					settings.Add("LoginPathText", "192.168.0.247");
				}

				if (settings["LoginWsEndText"] == null)
				{
					settings.Add("LoginWsEndText", @"");
				}
				if (settings["LoginUserNameText"] == null)
				{
					settings.Add("LoginUserNameText", "");
				}
				if (settings["LocalModeRadio"] == null)
				{
					settings.Add("LocalModeRadio", "true");
				}
				if (settings["ConcurentTabsCheck"] == null)
				{
					settings.Add("ConcurentTabsCheck", "false");
				}

				if (settings["GridFontSize"] == null)
				{
					settings.Add("GridFontSize", "8");
				}
				if (settings["CheckForMessageInterval"] == null)
				{
					settings.Add("CheckForMessageInterval", "0");
				}



				configFile.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
			}
			catch (ConfigurationErrorsException)
			{
				Console.WriteLine("Error writing app settings");
			}

		}
		private void CreateSqlConnection()
		{
			try
			{
				conn.DBpath = ConfigurationManager.AppSettings["LoginPathText"]; //+ LoginInstanceText.Text;
				conn.DataBaseName = "RoadDb";
				conn.UserName = Globals.dbConnectUserName;
				conn.PassWord = Globals.dbConnectPassword;

				if (IsDatabaseOnLine() != 1)
				{
					MessageBox.Show("Πρόβλημα στην σύνδεση με τον Server");
					Application.Exit();
				}
			}
			catch (Exception ae)
			{
				MessageBox.Show(ae.Message);
			}
		}
		private Int16 IsDatabaseOnLine()
		{
			try
			{
				string sql = "select count(*) as result from dbo.FParams";
				DataTable dt = Globals.SqlToTable(sql);
				return dt.AsEnumerable().Select(dr => Convert.ToInt16(dr["result"])).FirstOrDefault();
			}
			catch (Exception ae)
			{
				MessageBox.Show(ae.Message); return 0;
			}
		}
		private void UpdateSetting(string key, string value)
		{
			Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			configuration.AppSettings.Settings[key].Value = value;
			configuration.Save();
			ConfigurationManager.RefreshSection("appSettings");
		}
		#endregion Initialize

		#region Update
		private bool CheckIfNewVersionExists()
		{
			try
			{
				string app = appPath + @"\" + appName;
				string UpdateUrl = "http://www.crmapps.gr/Updates/";
				UpdateUrl = UpdateUrl + appName;
				Uri myUri = new Uri(UpdateUrl);
				DateTime updatefile;
				DateTime currentfile = File.GetLastWriteTime(app);

				HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(myUri);
				HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
				updatefile = myHttpWebResponse.LastModified;
				if (updatefile > currentfile) { return true; }
				else { return false; }
			}
			catch (Exception ae)
			{
				MessageBox.Show(ae.ToString());
				return false;
			}
		}
		private void Update_Click(object sender, EventArgs e)
		{
			try
			{
				Cursor.Current = Cursors.AppStarting;
				string app = appPath + @"\" + appName;
				string UpdateUrl = "http://www.crmapps.gr/Updates/";
				string FinalUpdatePath = "";

				DateTime updatefile;
				DateTime currentfile = File.GetLastWriteTime(app);

				CreateTempDirForRemotes();

				if (File.Exists(appPath + "Temp\\" + appName))
				{
					File.Delete(appPath + "Temp\\" + appName);
				}

				UpdateUrl = UpdateUrl + appName;
				Uri myUri = new Uri(UpdateUrl);

				using (var myWebClient = new WebClient())
				{
					myWebClient.DownloadFile(myUri, appPath + "Temp\\" + appName);// 
				}

				FinalUpdatePath = appPath + "Temp";

				HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(myUri);
				HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
				updatefile = myHttpWebResponse.LastModified;

				DialogResult result1 = MessageBox.Show(" Θα γίνει αναβάθμιση εφαρμογής", "Προσοχή . Είστε σίγουροι?", MessageBoxButtons.OKCancel);
				switch (result1)
				{
					case DialogResult.OK:
						{
							string PrevAppDate = DateTime.Now.ToString("ddMMyyyyTHHmm");
							DirectoryCopy(appPath, appPath + @"\PreviousApp" + PrevAppDate, false);
							UpdateProcedure(FinalUpdatePath); Application.Restart();
							break;
						}
					case DialogResult.Cancel:
						{
							break;
						}

				}//switch
			}
			catch (Exception ae)
			{
				MessageBox.Show(ae.Message);
			}

			finally { Cursor.Current = Cursors.Default; }

		}
		private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
		{
			// Get the subdirectories for the specified directory.
			DirectoryInfo dir = new DirectoryInfo(sourceDirName);
			DirectoryInfo[] dirs = dir.GetDirectories();

			if (!dir.Exists)
			{
				throw new DirectoryNotFoundException(
					"Source directory does not exist or could not be found: "
					+ sourceDirName);
			}

			// If the destination directory doesn't exist, create it. 
			if (!Directory.Exists(destDirName))
			{
				Directory.CreateDirectory(destDirName);
			}

			// Get the files in the directory and copy them to the new location.
			FileInfo[] files = dir.GetFiles();
			foreach (FileInfo file in files)
			{
				string temppath = Path.Combine(destDirName, file.Name);
				file.CopyTo(temppath, true);
			}

			// If copying subdirectories, copy them and their contents to new location. 
			if (copySubDirs)
			{
				foreach (DirectoryInfo subdir in dirs)
				{
					string temppath = Path.Combine(destDirName, subdir.Name);
					DirectoryCopy(subdir.FullName, temppath, copySubDirs);
				}
			}
		}
		private void UpdateProcedure(string updatepath)
		{
			try
			{
				File.Delete("TechnicianMap.old"); // Delete the existing file if exists
				File.Move("TechnicianMap.exe", "TechnicianMap.old");

				string sourceDir = updatepath;
				string backupDir = AppDomain.CurrentDomain.BaseDirectory;

				string[] exeList = Directory.GetFiles(sourceDir, "*.exe");
				string[] dllList = Directory.GetFiles(sourceDir, "*.dll");
				string[] pdfList = Directory.GetFiles(sourceDir, "*.pdf");
				// Copy exe files. 
				foreach (string f in exeList)
				{
					// Remove path from the file name. 
					string fName = f.Substring(sourceDir.Length + 1);

					// Use the Path.Combine method to safely append the file name to the path. 
					// Will overwrite if the destination file already exists.
					File.Copy(Path.Combine(sourceDir, fName), Path.Combine(backupDir, fName), true);
				}
				// copy pdf files
				foreach (string f in pdfList)
				{
					// Remove path from the file name. 
					string fName = f.Substring(sourceDir.Length + 1);

					// Use the Path.Combine method to safely append the file name to the path. 
					// Will overwrite if the destination file already exists.
					File.Copy(Path.Combine(sourceDir, fName), Path.Combine(backupDir, fName), true);
				}
				// Copy dll files. 
				foreach (string f in dllList)
				{

					// Remove path from the file name. 
					string fName = f.Substring(sourceDir.Length + 1);

					try
					{
						// Will  overwrite if the destination file already exists.
						File.Copy(Path.Combine(sourceDir, fName), Path.Combine(backupDir, fName), true);
					}

					// Catch exception if the file was already copied. 
					catch (IOException copyError)
					{
						Console.WriteLine(copyError.Message);
					}
				}
			}

			catch (DirectoryNotFoundException dirNotFound)
			{
				Console.WriteLine(dirNotFound.Message); Application.Restart();
			}

			// System.Windows.Forms.Application.Exit();
		}
		public void CreateTempDirForRemotes()
		{
			try
			{
				if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\Temp")) { Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Temp"); }
				Array.ForEach(Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + @"\Temp\"), File.Delete);
			}
			catch (Exception ae)
			{
				MessageBox.Show(ae.Message);
			}

		}
		#endregion  Update

		#region DriversList
		private void Refresh_Click(object sender, EventArgs e)
		{
			Free_Click(null, null);
		}
		private void ListGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			ModifyListGrid(0);
		}
		private void Free_Click(object sender, EventArgs e)
		{
			try
			{
				if (AllFreeBusy.Checked)
				{
					FillListGrid(0);
				}
				else if (Busy.Checked)
				{
					FillListGrid(2);
				}
				else if (Free.Checked)
				{
					FillListGrid(1);
				}

				ListGrid.Refresh();
			}
			catch (Exception ae)
			{
				MessageBox.Show(ae.Message);
			}
		}
		//methods
		private string CreateSql()
		{
			try
			{
				string sql =
				" select Code," + Environment.NewLine +
				" driver,(select DriverSurName + ' '+ DriverName from dbo.FRdCarsDrivers where Code = FRdShift.Driver) as DriverSurName," + Environment.NewLine +
				" car,(select VehiclePlate from dbo.FRdCarsDrivers where Code = FRdShift.Car) as VehiclePlate," + Environment.NewLine +
				" (select AutoMoto from dbo.FRdCarsDrivers where Code = FRdShift.Driver) as AutoMoto, " + Environment.NewLine +
				" FromDate,ToDate, " + Environment.NewLine +

				"isnull(  " + Environment.NewLine +
				"			(	   " + Environment.NewLine +
				"				select  top 1 CallPlates from dbo.FRdCalls, dbo.FRdCallResponsers  " + Environment.NewLine +
				"				where FRdCalls.CallCode = dbo.FRdCallResponsers.CallCode and FRdCallResponsers.ResponseShift = FRdShift.Code  " + Environment.NewLine +
				"				and(year(ResponseArrivalDate) = '2000' or year(ResponseEndDate) = '2000')	" + Environment.NewLine +
				"				and ResponserCode =		" + Environment.NewLine +
				"						(select min(ResponserCode) from dbo.FRdCallResponsers where ResponseStartDate between FRdShift.FromDate and FRdShift.ToDate	  " + Environment.NewLine +
				"							AND FRdCallResponsers.ResponseShift = dbo.FRdShift.Code	  " + Environment.NewLine +
				"							and(year(ResponseArrivalDate) = '2000' or year(ResponseEndDate) = '2000')  " + Environment.NewLine +
				"						)" + Environment.NewLine +
				"			),'' " + Environment.NewLine +
				"		) as CallPlates," + Environment.NewLine +
						
				"isnull(   " + Environment.NewLine +
				"			(	   " + Environment.NewLine +
				"				select  top 1 TypeOfCall from dbo.FRdCalls, dbo.FRdCallResponsers  " + Environment.NewLine +
				"				where FRdCalls.CallCode = dbo.FRdCallResponsers.CallCode and FRdCallResponsers.ResponseShift = FRdShift.Code   " + Environment.NewLine +
				"				and(year(ResponseArrivalDate) = '2000' or year(ResponseEndDate) = '2000')	" + Environment.NewLine +
				"				and ResponserCode =	 " + Environment.NewLine +
				"				(select min(ResponserCode) from dbo.FRdCallResponsers where ResponseStartDate between FRdShift.FromDate and FRdShift.ToDate	  " + Environment.NewLine +
				"					AND FRdCallResponsers.ResponseShift = dbo.FRdShift.Code	 " + Environment.NewLine +
				"					and(year(ResponseArrivalDate) = '2000' or year(ResponseEndDate) = '2000')	" + Environment.NewLine +
				"				)  " + Environment.NewLine +
				"			),'999'  " + Environment.NewLine +
				"		) as TypeOfCall, " + Environment.NewLine +

				" case  " + Environment.NewLine +
				"when Code in " + Environment.NewLine +
				"(" + Environment.NewLine +
				"		Select ResponseShift from dbo.FRdCallResponsers where ResponseStartDate between FRdShift.FromDate and FRdShift.ToDate" + Environment.NewLine +
				"		AND FRdCallResponsers.ResponseShift = dbo.FRdShift.Code" + Environment.NewLine +
				"		and year(ResponseArrivalDate)= '2000'" + Environment.NewLine +
				"		and year(ResponseEndDate)= '2000' " + Environment.NewLine +
				"			and ResponserCode =" + Environment.NewLine +
				"		(select min(ResponserCode) from dbo.FRdCallResponsers where ResponseStartDate between FRdShift.FromDate and FRdShift.ToDate" + Environment.NewLine +
				"			AND FRdCallResponsers.ResponseShift = dbo.FRdShift.Code" + Environment.NewLine +
				"			and year(ResponseArrivalDate)= '2000'" + Environment.NewLine +
				"			and year(ResponseEndDate)= '2000'" + Environment.NewLine +
				"		)" + Environment.NewLine +
				") then 'OnTheRoad'" + Environment.NewLine +
				"when Code in " + Environment.NewLine +
				"(" + Environment.NewLine +
				"	Select ResponseShift from dbo.FRdCallResponsers where ResponseStartDate between FRdShift.FromDate and FRdShift.ToDate " + Environment.NewLine +
				"	AND FRdCallResponsers.ResponseShift = dbo.FRdShift.Code" + Environment.NewLine +
				"	and year(ResponseArrivalDate)<> '2000'" + Environment.NewLine +
				"	and year(ResponseEndDate)= '2000'" + Environment.NewLine +
				"	and ResponserCode =" + Environment.NewLine +
				"		(select min(ResponserCode) from dbo.FRdCallResponsers where ResponseStartDate between FRdShift.FromDate and FRdShift.ToDate" + Environment.NewLine +
				"			AND FRdCallResponsers.ResponseShift = dbo.FRdShift.Code" + Environment.NewLine +
				"			and year(ResponseArrivalDate)<> '2000'" + Environment.NewLine +
				"			and year(ResponseEndDate)= '2000' " + Environment.NewLine +
				"		)" + Environment.NewLine +
				") then 'OnTheDamage' " + Environment.NewLine +



				"	else 'Free' " + Environment.NewLine +
				" end as DriverStatus " + Environment.NewLine +
				" FROM dbo.FRdShift where Active = 1 and ShiftStatus = 0 " + Environment.NewLine +
				" and dbo.FRdShift.Driver in (select code from dbo.FRdCarsDrivers where Branch = 704) "; //704 Athens

				return sql;
			}
			catch (Exception ae)
			{
				MessageBox.Show(ae.Message); return "error";
			}
		}
		private void FillListGrid(int BusyType)
		{
			try
			{
				Cursor.Current = Cursors.AppStarting;
				DataTable dt = Globals.SqlToTable(CreateSql());
				ListGrid.DataSource = dt;
				if (dt.Rows.Count > 0)
				{
					if (!ListGrid.Columns.Contains("AutoMotoIcon") )
					{
						DataGridViewImageColumn AutoMotoIcon = new DataGridViewImageColumn();
						AutoMotoIcon.Name = "AutoMotoIcon";
						AutoMotoIcon.HeaderText = " ";
						ListGrid.Columns.Add(AutoMotoIcon);
					}
					if (!ListGrid.Columns.Contains("DriverStatusIcon"))
					{
						DataGridViewImageColumn DriverStatusIcon = new DataGridViewImageColumn();
						DriverStatusIcon.Name = "DriverStatusIcon";
						DriverStatusIcon.HeaderText = " ";
						ListGrid.Columns.Add(DriverStatusIcon);
					}
					if (!ListGrid.Columns.Contains("TypeOfCallIcon"))
					{
						DataGridViewImageColumn TypeOfCallIcon = new DataGridViewImageColumn();
						TypeOfCallIcon.Name = "TypeOfCallIcon";
						TypeOfCallIcon.HeaderText = " ";
						ListGrid.Columns.Add(TypeOfCallIcon);
					}
					ListGrid.DataSource = dt;
					ModifyListGrid(BusyType);
					Cursor.Current = Cursors.Default;
				}
			}
			catch (Exception ae)
			{
				MessageBox.Show(ae.Message);
			}
		}
		private void ModifyListGrid(int BusyType)
		{
			try
			{
				foreach (DataGridViewColumn clm in ListGrid.Columns)
				{
					clm.Visible = false;
				}
				//ListGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
				//ListGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

				ListGrid.Columns["AutoMotoIcon"].HeaderText = "Auto";
				ListGrid.Columns["AutoMotoIcon"].Visible = true;
				ListGrid.Columns["AutoMotoIcon"].DisplayIndex = 0;
				ListGrid.Columns["AutoMotoIcon"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
				ListGrid.Columns["AutoMotoIcon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
				ListGrid.Columns["AutoMotoIcon"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
				((DataGridViewImageColumn)ListGrid.Columns["AutoMotoIcon"]).ImageLayout = DataGridViewImageCellLayout.Zoom;

				ListGrid.Columns["DriverSurName"].HeaderText = "Οδηγός";
				ListGrid.Columns["DriverSurName"].Visible = true;
				ListGrid.Columns["DriverSurName"].DisplayIndex = 1;
				ListGrid.Columns["DriverSurName"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
				ListGrid.Columns["DriverSurName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
				ListGrid.Columns["DriverSurName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

				ListGrid.Columns["VehiclePlate"].HeaderText = "Extra Οχημα";
				ListGrid.Columns["VehiclePlate"].Visible = true;
				ListGrid.Columns["VehiclePlate"].DisplayIndex = 2;
				ListGrid.Columns["VehiclePlate"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
				ListGrid.Columns["VehiclePlate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
				ListGrid.Columns["VehiclePlate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

				ListGrid.Columns["TypeOfCallIcon"].HeaderText = "Είδος";
				ListGrid.Columns["TypeOfCallIcon"].Visible = true;
				ListGrid.Columns["TypeOfCallIcon"].DisplayIndex = 3;
				ListGrid.Columns["TypeOfCallIcon"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
				ListGrid.Columns["TypeOfCallIcon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
				ListGrid.Columns["TypeOfCallIcon"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
				((DataGridViewImageColumn)ListGrid.Columns["TypeOfCallIcon"]).ImageLayout = DataGridViewImageCellLayout.Zoom;

				ListGrid.Columns["CallPlates"].HeaderText = "Οχημα Πελάτη";
				ListGrid.Columns["CallPlates"].Visible = true;
				ListGrid.Columns["CallPlates"].DisplayIndex = 4;
				ListGrid.Columns["CallPlates"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
				ListGrid.Columns["CallPlates"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
				ListGrid.Columns["CallPlates"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

				ListGrid.Columns["DriverStatusIcon"].HeaderText = "Κατάσταση";
				ListGrid.Columns["DriverStatusIcon"].Visible = true;
				ListGrid.Columns["DriverStatusIcon"].DisplayIndex = 5;
				ListGrid.Columns["DriverStatusIcon"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
				ListGrid.Columns["DriverStatusIcon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
				ListGrid.Columns["DriverStatusIcon"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
				((DataGridViewImageColumn)ListGrid.Columns["DriverStatusIcon"]).ImageLayout = DataGridViewImageCellLayout.Zoom;

				int BusyDrivers = 0;

				for (int i = 0; i < ListGrid.Rows.Count; ++i)
				{
					string autoMoto = ListGrid.Rows[i].Cells["AutoMoto"].Value.ToString();
					if (autoMoto == "Auto")
					{
						ListGrid.Rows[i].Cells["AutoMotoIcon"].Value = Properties.Resources.Truck;

					}
					else if (autoMoto == "Moto")
					{
						ListGrid.Rows[i].Cells["AutoMotoIcon"].Value = Properties.Resources.Motorcycle2;
					}

					string status = ListGrid.Rows[i].Cells["DriverStatus"].Value.ToString();
					if (status == "OnTheRoad")
					{
						ListGrid.Rows[i].Cells["DriverStatusIcon"].Value = Properties.Resources.mapmarker;
						BusyDrivers++;
					}
					else if (status == "OnTheDamage")
					{
						ListGrid.Rows[i].Cells["DriverStatusIcon"].Value = Properties.Resources.Tools;
						BusyDrivers++;

					}
					else if (status == "Free")
					{
						ListGrid.Rows[i].Cells["DriverStatusIcon"].Value = Properties.Resources.OkImage;
					}


					string typeOfCall = ListGrid.Rows[i].Cells["TypeOfCall"].Value.ToString();
					if (typeOfCall == "999")
					{
						ListGrid.Rows[i].Cells["TypeOfCallIcon"].Value = Properties.Resources.sandglass;
					}
					else if (typeOfCall == "1")
					{
						ListGrid.Rows[i].Cells["TypeOfCallIcon"].Value = Properties.Resources.Tools;
					}
					else if (typeOfCall == "2")
					{
						ListGrid.Rows[i].Cells["TypeOfCallIcon"].Value = Properties.Resources.Accident;
					}


					if (BusyType == 1) // free
					{
						if (status != "Free")
						{
							ListGrid.Rows[i].Visible = false;
						}
					}
					else
					if (BusyType == 2)// Busy
					{
						if (status == "Free")
						{
							ListGrid.Rows[i].Visible = false;
						}
					}
				};


				DriversGraph.Maximum = ListGrid.Rows.Count;
				DriversGraph.Value = BusyDrivers;
				DriversGraph.Refresh();
				MaximumLabel.Text = ListGrid.Rows.Count.ToString();
				MaximumLabel.Refresh();
				ValueLabel.Text = BusyDrivers.ToString();
				ValueLabel.Refresh();

				//SetState, 1 = normal (green); 2 = error (red); 3 = warning (yellow).

				if (100* BusyDrivers / ListGrid.Rows.Count <50)
				{
					DriversGraph.SetState(1);
				}
				else
				if (100 * BusyDrivers / ListGrid.Rows.Count > 50 && 100 * BusyDrivers / ListGrid.Rows.Count <75)
				{
					DriversGraph.SetState(3);
				}
				else 				
				{
					DriversGraph.SetState(2);
				}
			}
			catch (Exception ae)
			{
				MessageBox.Show(ae.Message);
			}
		}
		#endregion DriversList

		
	}
}




//try
//{

//}
//catch (Exception ae)
//{
//	MessageBox.Show(ae.Message);
//}

//sqldt.AsEnumerable().Where(dr => Convert.ToBoolean(dr["isDefault"])).Select(dr => Convert.ToBoolean(dr["result"])).FirstOrDefault();
//https://stackoverflow.com/questions/10108481/linq-way-to-find-value-in-datatable
// return sqldt.AsEnumerable().Select(dr => Convert.ToBoolean(dr["result"])).FirstOrDefault();


//MessageBox.Show("Hello there", "Prompt", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

