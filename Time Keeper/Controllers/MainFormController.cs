using log4net;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Time_Keeper.Interfaces;

namespace Time_Keeper.Controllers
{
    public class MainFormController
    {
        // TODO: Create an options page to allow users to toggle always on top, hover over options for the totals grid etc
        IMainFormView _view;

        public static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private const int ALWAYS_ON_TOP = 1000;
        private const string LogTable = "LogEntryTable";
        private const string TotalTable = "LogTotalsTable";
        private const string ProgramTable = "ProgramsTable";
        private const string DateTable = "EntryDatesTable";
        NetworkOperations NetOps = new NetworkOperations();

        public MainFormController(IMainFormView view)
        {
            _logger.Info("Opening the Main Form.");
            _view = view;
            _view.SQLDA = new SQLDataController();
            // Set the Properties.Default.Settings.saveFile path if it doesn't exist, this would only indicate a first launch
            if (_view.SaveLocation == "")
            {
                _view.SaveLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Programs\\TimeKeeper\\TimeKeeperData.sqlite");
                Properties.Settings.Default.Save();
            }

            if (!File.Exists(Properties.Settings.Default.saveFile))
            {
                _view.SQLDA.CreateFile(new string[] { LogTable, TotalTable, ProgramTable, DateTable });
            }

            if (Properties.Settings.Default.FirstRun)
            {
                WhatsNew firstRun = new WhatsNew();
                firstRun.ShowDialog();
            }

            NetOps.DeleteOldVersion();
            LoadView();
            _view.SetController(this);
        }

        public void LoadView()
        {
            if (_view.SQLDA is SQLDataController)
            {
                //_view.SQLDA.ReadFilteredData(new string[] { LogTable, TotalTable }, DateTime.Now, true);
                //_view.SQLDA.ReadData(new string[] { ProgramTable, DateTable }, true);
                try
                {
                    _view.EntriesTable = _view.SQLDA.ReadEntries();
                    _view.TotalsTable = _view.SQLDA.ReadTotals();
                    _view.ProgramsTable = _view.SQLDA.ReadPrograms();
                    _view.DatesTable = _view.SQLDA.ReadDates();

                    _view.ProgramsCombo.DataSource = _view.ProgramsTable;
                    _view.ProgramsCombo.DisplayMember = _view.ProgramsTable[1].ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            StartClock();
        }

        //public void WndProc(ref Message msg)
        //{
        //    if (msg.Msg == (int)WindowMessages.WM_SYSCOMMAND)
        //    {
        //        switch (msg.WParam.ToInt32())
        //        {
        //            case ALWAYS_ON_TOP:
        //                //_view.mainForm.TopMost = !_view.mainForm.TopMost;
        //                //SystemMenu.ResetSystemMenu(_view.mainForm);
        //                //_view.m_SystemMenu = SystemMenu.fromForm(_view.mainForm);
        //                if (_view.mainForm.TopMost)
        //                {
        //                    _view.m_SystemMenu.AppendSeparator();
        //                    _view.m_SystemMenu.AppendMenu(ALWAYS_ON_TOP, "Always On Top", ItemFlags.MF_CHECKED);
        //                }
        //                else
        //                {
        //                    _view.m_SystemMenu.AppendSeparator();
        //                    _view.m_SystemMenu.AppendMenu(ALWAYS_ON_TOP, "Always On Top");
        //                }
        //                return;
        //            default:
        //                break;
        //        }
        //    }

        //    base WndProc(ref msg);
        //}

        public void StartClock()
        {
            _view.ClockTimer = new Timer
            {
                Interval = 1000
            };
            _view.ClockTimer.Tick += new EventHandler(ClockTimer_Tick);
            _view.ClockTimer.Enabled = true;
        }

        public void ClockTimer_Tick(object sender, EventArgs e)
        {
            Application.OpenForms[0].Text = string.Format("Time Keeper / Time: {0}", DateTime.Now.ToString("HH:mm:ss"));
            if (_view.LogsGrid.Rows.Count > 0) CalculateTotalHours();
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

            Application.OpenForms["MainForm"].Visible = false;

            editForm.ShowDialog();
            Application.OpenForms["MainForm"].Visible = true;
            // Reload the programs table to update any added or removed programs
            _view.SQLDA.ReadPrograms();
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
                foreach (Entries entry in _view.EntriesTable) _view.SQLDA.DeleteEntry(entry);
                foreach (Programs program in _view.ProgramsTable) _view.SQLDA.DeleteProgram(program);
                foreach (Totals total in _view.TotalsTable) _view.SQLDA.DeleteTotal(total);
                foreach (Dates date in _view.DatesTable) _view.SQLDA.DeleteDate(date);

                _view.EntriesTable = _view.SQLDA.ReadEntries();
                _view.TotalsTable = _view.SQLDA.ReadTotals();
                _view.ProgramsTable = _view.SQLDA.ReadPrograms();
                _view.DatesTable = _view.SQLDA.ReadDates();
            }

            // Set the date time picker to today
            _view.CalendarSelection = DateTime.Now;
        }

        public void FileMenu_Quit_Click(object sender, EventArgs e)
        {
            _view.mainForm.Close();
        }

        public void BtnClockIn_Click(object sender, EventArgs e)
        {
            bool exists = false;
            DateTime today = new DateTime();

            // Check if the Entry Date already exists, if it doesn't add it to the table
            foreach (var date in _view.DatesTable)
            {
                if (date.DateID.ToShortDateString() == _view.CalendarSelection.ToShortDateString())
                {
                    exists = true;
                    today = date.DateID;
                }
            }

            if (!exists)
            {
                _view.SQLDA.AddDate(_view.CalendarSelection);
                today = _view.Calendar.SelectionStart;
            }

            if (_view.LogsGrid.RowCount > 0 && _view.LogsGrid.Rows[_view.LogsGrid.RowCount - 1].Cells[3].Value == DBNull.Value)
            {
                return;
            }
            DateTime t = DateTime.Now;

            _view.SQLDA.AddEntry(_program: (Programs)_view.ProgramsCombo.SelectedItem, _in: t, _date: _view.SQLDA.ReadDates(today)[0]);

            if (_view.LogsGrid.RowCount != 0)
            {
                _view.LogsGrid.FirstDisplayedScrollingRowIndex = _view.LogsGrid.RowCount - 1; // Scroll to bottom of grid
            }

            if (_view.DatesTable.Count == 0)
            {
                _view.SQLDA.AddDate(today);
            }


            // Check if the Entry Date already exists, if it doesn't add it to the table
            foreach (Dates row in _view.DatesTable)
            {
                if (row.DateID.ToShortDateString() == _view.CalendarSelection.ToShortDateString())
                {
                    exists = true;
                }
            }

            if (!exists)
            {
                _view.SQLDA.AddDate(today);
            }

            _view.EntriesTable = _view.SQLDA.ReadEntries(today);
            _view.TotalsTable = _view.SQLDA.ReadTotals(today);
        }

        public void BtnClockOut_Click(object sender, EventArgs e)
        {
            if (_view.LogsGrid.RowCount == 0)
            {
                return;
            }
            DateTime t = DateTime.Now;
            string hours = ((t - Convert.ToDateTime(_view.EntriesTable[_view.LogsGrid.RowCount - 1].In)).TotalMinutes / 60.0).ToString("N1");

            var _entryID = _view.EntriesTable[_view.EntriesTable.Count - 1].EntryID;
            Programs _program = _view.SQLDA.ReadPrograms(_view.ProgramsCombo.SelectedText)[0];
            DateTime _in = (DateTime)_view.EntriesTable[_view.LogsGrid.RowCount - 1].In;
            DateTime _out = t;
            decimal _hours = Convert.ToDecimal(hours);

            _view.SQLDA.UpdateEntry(_entryID: _entryID,
                _program: _program,
                _in: _in,
                _out: _out,
                _hours: _hours);
            //_view.SQLDA.WriteMultiDataQuery(new string[] {"UPDATE LogEntryTable SET Out='" + t + "' WHERE ID=" + _view.LogsTable.Rows[_view.LogsGrid.RowCount - 1][0],
            //"UPDATE LogEntryTable SET Hours=" + hours + " WHERE ID=" + _view.LogsTable.Rows[_view.LogsGrid.RowCount - 1][0] });

            bool exists = false;

            // Check the log totals table to see if an entry already exists for the program being updated in the log, if it doesn't add it
            if (_view.TotalsTable.Count > 0)
            {
                foreach (object row in _view.TotalsTable)
                {
                    foreach (Totals Row in _view.TotalsTable)
                    {
                        if (Row.ProgramID == _view.TotalsTable[_view.TotalsTable.Count - 1].ProgramID)
                        {
                            exists = true;
                        }
                    }
                }
            }

            if (!exists)
            {
                _view.SQLDA.UpdateEntry(_entryID: _entryID, _program: _program, _in: _in, _out: _out, _hours: _hours);
            }

            _view.LogsGrid.FirstDisplayedScrollingRowIndex = _view.LogsGrid.RowCount - 1;

            _view.EntriesTable = _view.SQLDA.ReadEntries(_view.CalendarSelection);
            _view.TotalsTable = _view.SQLDA.ReadTotals(_view.CalendarSelection);
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
                _view.SQLDA.DeleteEntry((Entries)_view.LogsGrid[0, e.RowIndex].Value);

                _view.TotalsTable = _view.SQLDA.ReadTotals(_view.CalendarSelection);
                _view.LogsGrid.Refresh();
                if(_view.LogsGrid.Rows.Count > 0) CalculateTotalHours();
            }
        }

        public void FrmMain_Load(object sender, EventArgs e)
        {
            // Check if the save database exists before trying to load the data from it
            if (!File.Exists(_view.SaveLocation))
            {
                return;
            }
            _view.LogsGrid.AutoGenerateColumns = false;
            _view.LogsGrid.DataSource = _view.EntriesTable;
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

            if(_view.LogsGrid.Rows.Count > 0) CalculateTotalHours();
        }

        public void CalculateTotalHours()
        {
            var lastOut = _view.LogsGrid.Rows[_view.LogsGrid.Rows.Count - 1].Cells["Out"].Value;
            var test = (DateTime.Now - Convert.ToDateTime(_view.LogsGrid.Rows[_view.LogsGrid.Rows.Count - 1].Cells["In"].Value));
            decimal estimatedTotal = ReturnTotalHours() + Convert.ToDecimal(test.TotalMinutes / 60);

            if (lastOut.Equals(DBNull.Value) && ReturnTotalHours().ToString("N1") != estimatedTotal.ToString("N1"))
            {
                _view.TotalTime.Text = string.Format("Total: {0} ({1})", ReturnTotalHours().ToString("N1"), estimatedTotal.ToString("N1"));
            }
            else
            {
                _view.TotalTime.Text = "Total: " + ReturnTotalHours().ToString("N1");
            }

        }

        public decimal ReturnTotalHours(bool UpdateTotalGrid = false)
        {
            decimal totalHours = 0;

            foreach (Totals program in _view.TotalsTable)
            {
                decimal pgmHours = 0;
                foreach (Entries row in _view.EntriesTable)
                {
                    if (row.ProgramID == program.ProgramID &&
                        Convert.ToDateTime(row.DateID).ToShortDateString() == Convert.ToDateTime(program.DateID).ToShortDateString())
                    {
                        if (!row.Hours.Equals(DBNull.Value))
                        {
                            pgmHours += Convert.ToDecimal(row.Hours);
                        }
                    }
                    program.Hours = pgmHours;
                }
                if (_view.EntriesTable.Count == 0)
                {
                    totalHours = 0;
                    program.Hours = 0;
                }
                totalHours += pgmHours;
                if (UpdateTotalGrid)
                {
                    _view.SQLDA.UpdateTotal(_totalID: program.TotalID,
                        _program: _view.SQLDA.ReadPrograms(program.Program.ToString())[0]);
                }
            }
            return totalHours;
        }

        public void MenuUpdateOnStart_Click(object sender, EventArgs e)
        {
            _view.HelpMenuAutoUpdate.Checked = !_view.HelpMenuAutoUpdate.Checked;
            Properties.Settings.Default.AutoCheckUpdate = _view.HelpMenuAutoUpdate.Checked;
            Properties.Settings.Default.Save();
        }

        public void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        public void CmbPrograms_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Properties.Settings.Default.ProgramSelected = _view.ProgramsCombo.SelectedIndex;
            Properties.Settings.Default.Save();
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
                foreach (Entries entry in _view.LogsGrid.Rows)
                {
                    DateTime entryIn = (DateTime)_view.EntriesTable[e.RowIndex].In;
                    DateTime entryOut = new DateTime();
                    decimal entryTotal = new decimal();

                    if (string.IsNullOrEmpty(_view.EntriesTable[e.RowIndex].Out.ToString()))
                    {
                        // TODO: This needs to be filled in
                    }
                    else
                    {
                        entryOut = (DateTime)_view.EntriesTable[e.RowIndex].Out;
                        entryTotal = (decimal)(entryOut - entryIn).TotalMinutes / 60;
                    }

                    int entryID = entry.EntryID;
                    Programs entryProgram = entry.Program;

                    entryTotal = entryTotal < 0 ? 0 : entryTotal;

                    // If the current row ID matches the one that was updated then send update queries to the database to update the saved data
                    if (entry.EntryID.Equals(_view.LogsGrid.Rows[e.RowIndex].Cells["ID"].Value))
                    {
                        if (entryIn != null)
                        {
                            try
                            {
                                _view.SQLDA.UpdateEntry(_entryID: entryID, _program: entryProgram, _in: entryIn);
                            }
                            catch (Exception ex)
                            {
                                _logger.Error("In Update failed: " + ex.Message);
                            }
                        }

                        if (entryOut !=  new DateTime() && entryTotal != new decimal())
                        {
                            try
                            {
                                _view.SQLDA.UpdateEntry(_entryID: entryID, _program: entryProgram, _in: entryIn, _out: entryOut, _hours: entryTotal);
                            }
                            catch (Exception ex)
                            {
                                _logger.Error("Hours Update failed: " + ex.Message);
                            }
                        }
                    }
                }
                _view.EntriesTable = _view.SQLDA.ReadEntries(_view.CalendarSelection);

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
                _view.SQLDA.UpdateTotal(_totalID: (int)_view.TotalsGrid["ID", e.RowIndex].Value,
                    _program: (Programs)_view.TotalsGrid["Program", e.RowIndex].Value,
                    _hours: (decimal)_view.TotalsGrid["Hours", e.RowIndex].Value,
                    _comments: (string)_view.TotalsGrid["Comments", e.RowIndex].Value);
                //_view.SQLDA.WriteSingleDataQuery("UPDATE LogTotalsTable SET Comments='" + _view.TotalsGrid[3, e.RowIndex].Value +
                //    "' WHERE Program='" + _view.TotalsGrid[1, e.RowIndex].Value +
                //    "' AND Date LIKE '%" + _view.Calendar.SelectionStart.ToShortDateString() + "%'");
                _view.TotalsTable = _view.SQLDA.ReadTotals(_view.CalendarSelection);
            }
        }

        public void DatePicker_DateChanged(object sender, DateRangeEventArgs e)
        {
            _view.CalendarSelection = _view.Calendar.SelectionStart;
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
                    _view.Calendar.AddBoldedDate(weekend);
                }
                weekend = weekend.AddDays(1);
            }

            while (dateCheck.Year < DateTime.Now.AddYears(1).Year)
            {
                dateCheck = dateCheck.AddDays(7);
                if (myCal.GetWeekOfYear(dateCheck, myCWR, firstDOW) % 2 == 0)
                {
                    _view.Calendar.AddBoldedDate(dateCheck);
                }
            }

            _view.Calendar.UpdateBoldedDates();
        }

        public void TotalsGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.ColumnIndex == _view.TotalsGrid.Columns["TotalProgram"].Index) && e.Value != null)
            {
                DataGridViewCell cell = _view.TotalsGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                for (int i = 0; i < _view.ProgramsTable.Count; i++)
                {
                    if (e.Value.Equals(_view.ProgramsTable[i].ProgramID))
                    {
                        cell.ToolTipText = "Charge Code: " + _view.ProgramsTable[i].Code +
                            "\nNotes: " + _view.ProgramsTable[i].Notes;
                    }
                }
            }
        }
    }
}