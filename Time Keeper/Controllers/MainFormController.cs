using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Time_Keeper.Interfaces;
using static Time_Keeper.CustomSystemMenu;

namespace Time_Keeper.Controllers
{
    public class MainFormController
    {
        IMainFormView _view;

        public static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const int ALWAYS_ON_TOP = 1000;
        NetworkOperations NetOps = new NetworkOperations();

        public void WndProc(ref Message msg)
        {
            if (msg.Msg == (int)WindowMessages.WM_SYSCOMMAND)
            {
                switch (msg.WParam.ToInt32())
                {
                    case ALWAYS_ON_TOP:
                        _view.mainForm.TopMost = !_view.mainForm.TopMost;
                        SystemMenu.ResetSystemMenu(_view.mainForm);
                        _view.m_SystemMenu = SystemMenu.fromForm(_view.mainForm);
                        if (_view.mainForm.TopMost)
                        {
                            _view.m_SystemMenu.AppendSeparator();
                            _view.m_SystemMenu.AppendMenu(ALWAYS_ON_TOP, "Always On Top", ItemFlags.MF_CHECKED);
                        }
                        else
                        {
                            _view.m_SystemMenu.AppendSeparator();
                            _view.m_SystemMenu.AppendMenu(ALWAYS_ON_TOP, "Always On Top");
                        }
                        return;
                    default:
                        break;
                }
            }

            base WndProc(ref msg);
        }

        public void StartClock()
        {
            _view.ClockTimer = new System.Timers.Timer
            {
                Interval = 1000
            };
            _view.ClockTimer.Tick += new EventHandler(ClockTimer_Tick);
            _view.ClockTimer.Enabled = true;
        }

        public void ClockTimer_Tick(object sender, EventArgs e)
        {
            _view.CurrentTime.Text = string.Format("Time: {0}", DateTime.Now.ToString("HH:mm:ss"));
        }

        public void HelpMenu_About_Click(object sender, EventArgs e)
        {
            AboutBoxForm aboutForm = new AboutBoxForm();
            aboutForm.Visible = false;

            AboutController aboutController = new AboutController(aboutForm);
            aboutController.LoadView();
            aboutForm.ShowDialog();
        }

        public void HelpMenu_Update_Click(object sender, EventArgs e)
        {
            NetOps.UpdateCheck(new Version(Assembly.GetExecutingAssembly().GetName().Version.ToString()));
        }

        public void FileMenu_Edit_Click(object sender, EventArgs e)
        {
            // Open the programs editor and pass in the current SQL database data adapter
            EditProgramsForm editForm = new EditProgramsForm();
            EditController editController = new EditController(editForm, _view.SQLDA);

            editForm.ShowDialog();
            // Reload the programs table to update any added or removed programs
            _view.SQLDA.ReadData(new string[] { "ProgramsTable" });
        }

        public void FileMenu_Reset_Click(object sender, EventArgs e)
        {
            // Ask the user if they are sure they want to remove all history
            DialogResult result = MessageBox.Show(
                caption: "Reset Time Keeper?",
                text: "Are you sure you want to reset all data?\n\nThis will remove all historic data and cannot be undone?",
                buttons: MessageBoxButtons.YesNo,
                icon: MessageBoxIcon.Warning);

            // The user selected yes so delete all data from all tables in the database
            if (result == DialogResult.Yes)
            {
                _logger.Info("User has chosen to clear historical application data, clearing the database now.)");
                foreach (DataTable table in _view.SQLDA.TKDS.Tables)
                {
                    _view.SQLDA.WriteSingleDataQuery("DELETE FROM " + table);
                }
                _view.SQLDA.ReadData(new string[] { "LogEntryTable", "LogTotalsTable", "ProgramsTable", "EntryDatesTable" });
            }

            // Set the date time picker to today
            _view.DatePicker.SelectionStart = DateTime.Now;
        }

        public void FileMenu_Quit_Click(object sender, EventArgs e)
        {
            _view.mainForm.Close();
        }

        public void BtnClockIn_Click(object sender, EventArgs e)
        {
            if (_view.LogsGrid.RowCount > 0 && _view.LogsGrid.Rows[_view.LogsGrid.RowCount - 1].Cells[3].Value == DBNull.Value)
            {
                return;
            }
            DateTime t = DateTime.Now;

            _view.SQLDA.WriteSingleDataQuery("INSERT INTO LogEntryTable (Program, [In], Date) VALUES ('" + _view.ProgramsCombo.Text + "', '" + t + "', '" + _view.DatePicker.SelectionStart + "')");
            if (_view.LogsGrid.RowCount != 0)
            {
                _view.LogsGrid.FirstDisplayedScrollingRowIndex = _view.LogsGrid.RowCount - 1; // Scroll to bottom of grid
            }

            if (_view.DatesTable.Rows.Count == 0)
            {
                _view.SQLDA.WriteSingleDataQuery("INSERT INTO EntryDatesTable (EntryDate) VALUES ('" + _view.DatePicker.SelectionStart + "')");
            }

            bool exists = false;

            // Check if the Entry Date already exists, if it doesn't add it to the table
            foreach (DataRow row in _view.DatesTable.Rows)
            {
                if (Convert.ToDateTime(row[0]).ToShortDateString() == _view.DatePicker.SelectionStart.ToShortDateString())
                {
                    exists = true;
                }
            }

            if (!exists)
            {
                _view.SQLDA.WriteSingleDataQuery("INSERT INTO EntryDatesTable (EntryDate) VALUES ('" + _view.DatePicker.SelectionStart + "')");
            }

            _view.SQLDA.ReadFilteredData(new string[] { "LogEntryTable", "LogTotalsTable" }, _view.DatePicker.SelectionStart);
        }

        public void BtnClockOut_Click(object sender, EventArgs e)
        {
            if (_view.LogsGrid.RowCount == 0)
            {
                return;
            }
            DateTime t = DateTime.Now;
            string hours = ((t - Convert.ToDateTime(_view.LogsTable.Rows[_view.LogsGrid.RowCount - 1][2])).TotalMinutes / 60.0).ToString("N1");

            _view.SQLDA.WriteMultiDataQuery(new string[] {"UPDATE LogEntryTable SET Out='" + t + "' WHERE ID=" + _view.LogsTable.Rows[_view.LogsGrid.RowCount - 1][0],
            "UPDATE LogEntryTable SET Hours=" + hours + " WHERE ID=" + _view.LogsTable.Rows[_view.LogsGrid.RowCount - 1][0] });

            bool exists = false;

            // Check the log totals table to see if an entry already exists for the program being updated in the log, if it doesn't add it
            if (_view.TotalsTable.Rows.Count > 0)
            {
                foreach (object row in _view.TotalsTable.Rows)
                {
                    foreach (DataRow Row in _view.TotalsTable.Rows)
                    {
                        if (Row[1].ToString() == _view.TotalsTable.Rows[_view.LogsGrid.RowCount - 1][1].ToString())
                        {
                            exists = true;
                        }
                    }
                }
            }

            if (!exists)
            {
                _view.SQLDA.WriteSingleDataQuery("INSERT INTO LogTotalsTable (Program, Date) " +
                    "VALUES ('" + _view.LogsTable.Rows[_view.LogsGrid.RowCount - 1][1].ToString() + "', '" + _view.DatePicker.SelectionStart + "')");
            }

            _view.LogsGrid.FirstDisplayedScrollingRowIndex = _view.LogsGrid.RowCount - 1;

            _view.SQLDA.ReadFilteredData(new string[] { "LogEntryTable", "LogTotalsTable" }, _view.DatePicker.SelectionStart);
            _view.TotalsGrid.Refresh();
            CalculateTotalHours();
        }

        public void BtnOpenDeltek_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.deltekURL);
        }

        public void LogsGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            // If the user double clicked in the log entry grid and the click was in the delete column then delete that row
            if (e.ColumnIndex == 6)
            {
                _logger.Info("User deleting " + _view.LogsGrid[1, e.RowIndex].Value + " data row from the entry grid.");
                _view.SQLDA.WriteSingleDataQuery("DELETE FROM LogEntryTable WHERE ID=" + _view.LogsGrid[0, e.RowIndex].Value);

                _view.SQLDA.ReadFilteredData(new string[] { "LogEntryTable" }, _view.DatePicker.SelectionStart);
                _view.LogsGrid.Refresh();
                CalculateTotalHours();
            }
        }

        public void FrmMain_Load(object sender, EventArgs e)
        {
            // Check if the save database exists before trying to load the data from it
            if (!File.Exists(_view.tkSetting.saveFile))
            {
                return;
            }
            _view.LogsGrid.AutoGenerateColumns = false;
            _view.LogsGrid.DataSource = _view.LogsTable;
            _view.LogsGrid.Columns["ID"].DataPropertyName = "ID";
            _view.LogsGrid.Columns["Program"].DataPropertyName = "Program";
            _view.LogsGrid.Columns["In"].DataPropertyName = "In";
            _view.LogsGrid.Columns["Out"].DataPropertyName = "Out";
            _view.LogsGrid.Columns["Hours"].DataPropertyName = "Hours";
            _view.LogsGrid.Columns["LogDate"].DataPropertyName = "Date";

            _view.TotalsGrid.AutoGenerateColumns = false;
            _view.TotalsGrid.DataSource = _view.TotalsTable;
            _view.TotalsGrid.Columns["TotalID"].DataPropertyName = "ID";
            _view.TotalsGrid.Columns["TotalProgram"].DataPropertyName = "Program";
            _view.TotalsGrid.Columns["TotalHours"].DataPropertyName = "Hours";
            _view.TotalsGrid.Columns["TotalComments"].DataPropertyName = "Comments";
            _view.TotalsGrid.Columns["TotalDate"].DataPropertyName = "TotalsDate";

            PopulateOffFridays();

            CalculateTotalHours();
        }

        public void CalculateTotalHours()
        {
            double totalHours = 0.0;

            foreach (DataRow program in _view.TotalsTable.Rows)
            {
                double pgmHours = 0.0;
                foreach (DataRow row in _view.LogsTable.Rows)
                {
                    if (row["Program"].ToString() == program["Program"].ToString() &&
                        Convert.ToDateTime(row["Date"]).ToShortDateString() == Convert.ToDateTime(program["Date"]).ToShortDateString())
                    {
                        if (!row["Hours"].Equals(DBNull.Value))
                        {
                            pgmHours += Convert.ToDouble(row["Hours"]);
                        }
                    }
                    program["Hours"] = pgmHours;
                }
                if (_view.LogsTable.Rows.Count == 0)
                {
                    totalHours = 0;
                    program["Hours"] = 0;
                }
                totalHours += pgmHours;

                _view.SQLDA.WriteSingleDataQuery(("UPDATE LogTotalsTable SET Hours='" + pgmHours +
                "' WHERE Program='" + program[1] +
                "' AND Date LIKE '%" + _view.DatePicker.SelectionStart.ToShortDateString() + "%'"));
            }
            _view.TotalTime.Text = "Total: " + totalHours.ToString("N1");
        }

        public void MenuUpdateOnStart_Click(object sender, EventArgs e)
        {
            _view.HelpMenuAutoUpdate.Checked = !_view.HelpMenuAutoUpdate.Checked;
            Properties.Settings.Default.AutoCheckUpdate = _view.HelpMenuAutoUpdate.Checked;
            _view.tkSetting.Save();
        }

        public void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        public void CmbPrograms_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _view.tkSetting.ProgramSelected = _view.ProgramsCombo.SelectedIndex;
            _view.tkSetting.Save();
        }

        public void LogsGrid_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            CalculateTotalHours();
        }

        public void LogsGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            // The program name was manually updated so we only need to recalculate where the totals go
            if (e.ColumnIndex == 1)
            {
                CalculateTotalHours();
                return;
            }

            // The In or Out time was manually updated so recalculate totals by updating the row a with matching index
            if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
            {
                foreach (DataGridViewRow entry in _view.LogsGrid.Rows)
                {
                    DateTime entryIn = Convert.ToDateTime(_view.LogsTable.Rows[entry.Index]["In"]);
                    DateTime? entryOut = new DateTime();
                    double? entryTotal;

                    if (string.IsNullOrEmpty(Convert.ToString(_view.LogsTable.Rows[entry.Index]["Out"])))
                    {
                        entryOut = null;
                        entryTotal = null;
                    }
                    else
                    {
                        entryOut = Convert.ToDateTime(_view.LogsTable.Rows[entry.Index]["Out"]);
                        entryTotal = ((DateTime)entryOut - entryIn).TotalMinutes / 60;
                    }

                    object entryID = entry.Cells["ID"].Value;

                    entryTotal = entryTotal < 0 ? 0 : entryTotal;

                    // If the current row ID matches the one that was updated then send update queries to the database to update the saved data
                    if (entry.Cells["ID"].Value.Equals(_view.LogsGrid.Rows[e.RowIndex].Cells["ID"].Value))
                    {
                        if (entryIn != null)
                        {
                            try
                            {
                                _view.SQLDA.WriteSingleDataQuery("UPDATE LogEntryTable SET [In]='" + entryIn + "' WHERE ID=" + entryID);
                            }
                            catch (Exception ex)
                            {
                                _logger.Error("In Update failed: " + ex.Message);
                            }
                        }

                        if (entryOut != null)
                        {
                            try
                            {
                                _view.SQLDA.WriteSingleDataQuery("UPDATE LogEntryTable SET Out='" + entryOut + "' WHERE ID=" + entryID);
                            }
                            catch (Exception ex)
                            {
                                _logger.Error("Out Update failed: " + ex.Message);
                            }
                        }
                        if (entryTotal != null)
                        {
                            try
                            {
                                _view.SQLDA.WriteSingleDataQuery("UPDATE LogEntryTable SET Hours=" + entryTotal + " WHERE ID=" + entryID);
                            }
                            catch (Exception ex)
                            {
                                _logger.Error("Hours Update failed: " + ex.Message);
                            }
                        }
                    }
                }
                _view.SQLDA.ReadFilteredData(new string[] { "LogEntryTable" }, _view.DatePicker.SelectionStart);

                CalculateTotalHours();
            }
        }

        public void TotalsGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == 3)
            {
                // Update the corresponding item in the database
                _view.SQLDA.WriteSingleDataQuery("UPDATE LogTotalsTable SET Comments='" + _view.TotalsGrid[3, e.RowIndex].Value +
                    "' WHERE Program='" + _view.TotalsGrid[1, e.RowIndex].Value +
                    "' AND Date LIKE '%" + _view.DatePicker.SelectionStart.ToShortDateString() + "%'");
                _view.SQLDA.ReadFilteredData(new string[] { "LogTotalsTable" }, _view.DatePicker.SelectionStart);

            }
        }

        public void DatePicker_DateChanged(object sender, DateRangeEventArgs e)
        {
            _view.SQLDA.ReadFilteredData(new string[] { "LogEntryTable", "LogTotalsTable" }, _view.DatePicker.SelectionStart);
            CalculateTotalHours();
        }

        public void PopulateOffFridays()
        {
            // This bolds every even week friday of the calendar year indicating the fridays off as well as all weekends
            DateTime dateCheck = new DateTime(DateTime.Now.Year, 1, 1);

            CultureInfo myCI = CultureInfo.CurrentCulture;
            Calendar myCal = myCI.Calendar;
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek firstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            while (dateCheck.DayOfWeek != DayOfWeek.Friday)
            {
                dateCheck = dateCheck.AddDays(1);
            }

            DateTime weekend = new DateTime(DateTime.Now.Year, 1, 1);

            while (weekend.Year < DateTime.Now.AddYears(1).Year)
            {
                if (weekend.DayOfWeek == DayOfWeek.Saturday || weekend.DayOfWeek == DayOfWeek.Sunday)
                {
                    _view.DatePicker.AddBoldedDate(weekend);
                }
                weekend = weekend.AddDays(1);
            }

            while (dateCheck.Year < DateTime.Now.AddYears(1).Year)
            {
                dateCheck = dateCheck.AddDays(7);
                if (myCal.GetWeekOfYear(dateCheck, myCWR, firstDOW) % 2 == 0)
                {
                    _view.DatePicker.AddBoldedDate(dateCheck);
                }
            }

            _view.DatePicker.UpdateBoldedDates();
        }

        public void TotalsGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // TODO: This needs to return the program name and code for it to work right
            if ((e.ColumnIndex == _view.TotalsGrid.Columns["TotalProgram"].Index) && e.Value != null)
            {
                List<object> pgms = _view.SQLDA.SelectQuery(new string[] { "Program", "Code" }, new string[] { "ProgramsTable", "ProgramsTable" });
                DataGridViewCell cell = _view.TotalsGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                foreach (object pgm in pgms)
                {
                    if (e.Value.Equals(pgm))
                    {
                        cell.ToolTipText = "Charge Code: " + pgms[pgms.IndexOf(pgm) + pgms.Count / 2];
                    }
                }
            }
        }
    }
}
