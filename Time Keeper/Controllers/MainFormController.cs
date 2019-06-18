using log4net;
using Microsoft.Win32;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Time_Keeper.Interfaces;

namespace Time_Keeper.Controllers
{
    public class MainFormController : MainForm
    {
        IMainForm _view;

        public static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private BindingSource logsBindingSource = new BindingSource();
        private BindingSource totalsBindingSource = new BindingSource();
        NetworkOperations NetOps = new NetworkOperations();

        public MainFormController(IMainForm view)
        {
            if (Properties.Settings.Default.AutoCheckUpdate)
            {
                _logger.Info("Automatic startup check for newer version.");
                NetOps.UpdateCheck(new Version(Assembly.GetExecutingAssembly().GetName().Version.ToString()));
            }

            _logger.Info("Opening the Main Form.");
            _view = view;
            _view.SQLDA = new SQLDataController();

            if (Properties.Settings.Default.WhatsNew)
            {
                WhatsNewForm whatsNewForm = new WhatsNewForm();

                WhatsNewController controller = new WhatsNewController(whatsNewForm);
                controller.LoadView();
                whatsNewForm.ShowDialog();
            }

            NetOps.DeleteOldVersion();
            LoadView(DateTime.Now);
            SettingsMenuController();
            LoadGridEventHandlers();
            _view.SetController(this);
        }

        public void SettingsMenuController()
        {
            // Create settings menu options
            ToolStripItem _alwaysOnTop = new ToolStripMenuItem("Always On Top");
            ToolStripItem _whatsNew = new ToolStripMenuItem("Show What's New");
            ToolStripItem _autoStart = new ToolStripMenuItem("Auto Start");

            // Bind event handlers to newly created options
            _alwaysOnTop.Click += new EventHandler(ToggleAlwaysOnTop);
            _whatsNew.Click += new EventHandler(ToggleWhatsNew);
            _autoStart.Click += new EventHandler(ToggleAutoStart);

            // Set the checked state of the menu options based on the global settings
            try
            {
                ((ToolStripMenuItem)_alwaysOnTop).Checked = Properties.Settings.Default.AlwaysOnTop;
            }
            catch (NullReferenceException)
            {
                Properties.Settings.Default.AlwaysOnTop = false;
                Properties.Settings.Default.Save();
            }

            ((ToolStripMenuItem)_whatsNew).Checked = Properties.Settings.Default.WhatsNew;

            // If the global setting for always on top is enabled then set the form property to match
            if (Properties.Settings.Default.AlwaysOnTop)
            {
                _view.mainForm.TopMost = true;
            }
            else
            {
                _view.mainForm.TopMost = false;
            }

            // If the registry key setting for HKCU\Software\Microsoft\CurrentVersion\Run\TimeKeeper is 1 then auto run is enabled
            // so we need to check the option in the menu
            using (RegistryKey autoRun = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run"))
            {
                if (autoRun.GetValue("Time Keeper") == null)
                {
                    ((ToolStripMenuItem)_autoStart).Checked = false;
                }
                else
                {
                    ((ToolStripMenuItem)_autoStart).Checked = true;
                }
            }

            // Insert the settings menu options
            _view.SettingsMenuOption.DropDownItems.Insert(2, _alwaysOnTop);
            _view.SettingsMenuOption.DropDownItems.Insert(3, _whatsNew);
            _view.SettingsMenuOption.DropDownItems.Insert(4, _autoStart);
        }

        public void LoadGridEventHandlers()
        {
            _view.LogsGrid.CellValidating += new DataGridViewCellValidatingEventHandler(ValidateTime);
        }

        private void LoadView(DateTime _date)
        {
            if (_view.SQLDA is SQLDataController)
            {
                try // Load the entries log
                {
                    if (_view.SQLDA.ReadEntries(_view.CalendarSelection).Count > 0)
                    {
                        _view.EntriesTable = _view.SQLDA.ReadEntries(_date);
                        logsBindingSource.DataSource = _view.EntriesTable;
                        _view.LogsGrid.AutoGenerateColumns = false;

                        _view.LogsGrid.DataSource = logsBindingSource;
                    }
                    else
                    {
                        _view.LogsGrid.DataSource = null;
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message + "\n" + ex.InnerException);
                }

                try // Load the totals log
                {
                    if (_view.SQLDA.ReadTotals(_view.CalendarSelection).Count > 0)
                    {
                        _view.TotalsTable = _view.SQLDA.ReadTotals(_date);
                        totalsBindingSource.DataSource = _view.TotalsTable;
                        _view.TotalsGrid.AutoGenerateColumns = false;

                        _view.TotalsGrid.DataSource = totalsBindingSource;

                    }
                    else
                    {
                        _view.TotalsGrid.DataSource = null;
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message + "\n" + ex.InnerException);
                }

                try // If the dates table has entries in it set the dates table to that data
                {
                    if (_view.SQLDA.ReadDates().Count > 0) _view.DatesTable = _view.SQLDA.ReadDates();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message + "\n" + ex.InnerException);
                }

                try // Load the programs combo and set the data source
                {
                    if (_view.SQLDA.ReadPrograms((Programs)null).Count > 0)
                    {
                        _view.ProgramsTable = _view.SQLDA.ReadPrograms((Programs)null, sorted: true);
                        _view.ProgramsCombo.DataSource = _view.ProgramsTable;
                        _view.ProgramsCombo.DisplayMember = "Name";
                        _view.ProgramsCombo.Refresh();
                        _view.ProgramsCombo.SelectedIndex = Properties.Settings.Default.ProgramSelected;
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message + "\n" + ex.InnerException);
                }

                if (_view.ProgramsCombo.SelectedItem == null)
                {
                    _view.ClockIn.Enabled = false;
                    _view.ClockOut.Enabled = false;
                }
                else
                {
                    _view.ClockIn.Enabled = true;
                    _view.ClockOut.Enabled = true;
                }

                if (_view.LogsGrid.Rows.Count == 0)
                {
                    _view.ClockIn.Enabled = true;
                    _view.ClockOut.Enabled = false;
                }

                if (_view.LogsGrid.Rows.Count > 0 && _view.LogsGrid.Rows[_view.LogsGrid.Rows.Count - 1].Cells["Out"].Value == null)
                {
                    _view.ClockIn.Enabled = false;
                    _view.ClockOut.Enabled = true;
                }

                if (_view.LogsGrid.Rows.Count > 0 && _view.LogsGrid.Rows[_view.LogsGrid.Rows.Count - 1].Cells["Out"].Value != null)
                {
                    _view.ClockIn.Enabled = true;
                    _view.ClockOut.Enabled = false;
                }

                _view.TotalTime.Text = "Total: 0.0";
            }
            RunClock();
        }

        public void LoadFormData()
        {
            _view.LogsGrid.Columns["ID"].DataPropertyName = "EntryID";
            _view.LogsGrid.Columns["Program"].DataPropertyName = "ProgramName";
            _view.LogsGrid.Columns["In"].DataPropertyName = "In";
            _view.LogsGrid.Columns["Out"].DataPropertyName = "Out";
            _view.LogsGrid.Columns["Hours"].DataPropertyName = "Hours";
            _view.LogsGrid.Columns["LogDate"].DataPropertyName = "DateID";

            _view.TotalsGrid.AutoGenerateColumns = false;
            _view.TotalsGrid.DataSource = _view.TotalsTable;
            _view.TotalsGrid.Columns["TotalID"].DataPropertyName = "TotalID";
            _view.TotalsGrid.Columns["TotalProgram"].DataPropertyName = "ProgramName";
            _view.TotalsGrid.Columns["TotalHours"].DataPropertyName = "Hours";
            _view.TotalsGrid.Columns["TotalComments"].DataPropertyName = "Comments";
            _view.TotalsGrid.Columns["TotalDate"].DataPropertyName = "DateID";

            PopulateOffFridays();

            if (LogsGrid.Rows.Count > 1) CalculateTotalHours();
        }

        private void ToggleAlwaysOnTop(object sender, EventArgs e)
        {
            ToolStripMenuItem _aot = sender as ToolStripMenuItem;
            _aot.Checked = !_aot.Checked;
            _view.mainForm.TopMost = _aot.Checked;
            Properties.Settings.Default.AlwaysOnTop = _aot.Checked;
            Properties.Settings.Default.Save();
        }

        private void ToggleWhatsNew(object sender, EventArgs e)
        {
            ToolStripMenuItem _wn = sender as ToolStripMenuItem;
            _wn.Checked = !_wn.Checked;
            Properties.Settings.Default.WhatsNew = _wn.Checked;
            Properties.Settings.Default.Save();
        }

        public void ToggleAutoUpdate()
        {
            SettingsMenuAutoUpdate.Checked = !SettingsMenuAutoUpdate.Checked;
            Properties.Settings.Default.AutoCheckUpdate = SettingsMenuAutoUpdate.Checked;
            Properties.Settings.Default.Save();
        }

        public void ToggleAutoStart(object sender, EventArgs e)
        {
            _logger.Info("Toggling auto start functionality.");
            ToolStripMenuItem _autoStart = sender as ToolStripMenuItem;
            _autoStart.Checked = !_autoStart.Checked;
            RegistryKey autoStart = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            if (autoStart.GetValue("Time Keeper") == null)
            {
                RegistryKey RKWrite = Registry.CurrentUser;
                RKWrite = RKWrite.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                RKWrite.SetValue("Time Keeper", Assembly.GetExecutingAssembly().Location);
                RKWrite.Close();
                _autoStart.Checked = true;
            }
            else
            {
                RegistryKey RKDelete = Registry.CurrentUser;
                RKDelete = RKDelete.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                RKDelete.DeleteValue("Time Keeper");
                RKDelete.Close();
                _autoStart.Checked = false;
            }
        }

        public void RunClock()
        {
            _view.ClockTimer = new Timer
            {
                Interval = 1000
            };
            _view.ClockTimer.Tick += new EventHandler(ClockTick);
            _view.ClockTimer.Enabled = true;
        }

        public void ClockTick(object sender, EventArgs e)
        {
            _view.mainForm.Text = string.Format("Time Keeper / Time: {0}", DateTime.Now.ToString("HH:mm:ss"));
            if (_view.LogsGrid.Rows.Count > 0) CalculateTotalHours();
        }

        public void OpenAboutView()
        {
            AboutBoxForm aboutForm = new AboutBoxForm();
            aboutForm.Visible = false;

            AboutController aboutController = new AboutController(aboutForm);
            aboutController.LoadView();
            aboutForm.ShowDialog();
        }

        public void ManualUpdateCheck()
        {
            _logger.Info("Manually checking network for newer version.");
            NetOps.UpdateCheck(new Version(Assembly.GetExecutingAssembly().GetName().Version.ToString()));
        }

        public void OpenEditProgramsView()
        {
            // Open the programs editor and pass in the current SQL database data adapter
            EditProgramsForm editForm = new EditProgramsForm();
            EditController editController = new EditController(editForm, _view.SQLDA);

            Application.OpenForms["MainForm"].Visible = false;

            editForm.ShowDialog();

            Application.OpenForms["MainForm"].Visible = true;
            LoadView(_view.CalendarSelection);
        }

        public void ResetDatabase()
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
                if (_view.SQLDA.ReadEntries().Count > 0) foreach (Entries entry in _view.EntriesTable) _view.SQLDA.DeleteEntry(entry);
                if (_view.SQLDA.ReadTotals().Count > 0) foreach (Totals total in _view.TotalsTable) _view.SQLDA.DeleteTotal(total);
                if (_view.SQLDA.ReadPrograms((Programs)null).Count > 0) foreach (Programs program in _view.ProgramsTable) _view.SQLDA.DeleteProgram(program);
                if (_view.SQLDA.ReadDates().Count > 0) foreach (Dates date in _view.DatesTable) _view.SQLDA.DeleteDate(date);
                _view.CalendarSelection = DateTime.Now;

                _view.TotalTime.Clear();
                LoadView(_view.CalendarSelection);
                _view.ProgramsCombo.DataSource = null;
            }
        }

        public void EntryClockIn()
        {
            DateTime today = _view.CalendarSelection;
            _view.SQLDA.AddDate(today);

            if (_view.LogsGrid.RowCount > 0 && _view.LogsGrid.Rows[_view.LogsGrid.RowCount - 1].Cells[3].Value == DBNull.Value)
            {
                return;
            }
            DateTime t = DateTime.Now;
            Dates date = _view.SQLDA.ReadDates(today)[0];

            _view.SQLDA.AddEntry(program: (Programs)_view.ProgramsCombo.SelectedItem, timeIn: t, date: date);

            if (_view.LogsGrid.RowCount != 0)
            {
                _view.LogsGrid.FirstDisplayedScrollingRowIndex = _view.LogsGrid.RowCount - 1; // Scroll to bottom of grid
            }

            CheckZeroTotals();
            LoadView(today);
        }

        public void EntryClockOut()
        {
            if (_view.LogsGrid.RowCount == 0)
            {
                return;
            }
            DateTime t = DateTime.Now;
            string hours = ((t - Convert.ToDateTime(_view.EntriesTable[_view.LogsGrid.RowCount - 1].In)).TotalMinutes / 60.0).ToString("N1");

            var _entry = _view.EntriesTable[_view.EntriesTable.Count - 1];
            DateTime _in = (DateTime)_view.EntriesTable[_view.LogsGrid.RowCount - 1].In;
            DateTime _out = t;
            decimal _hours = decimal.Parse(hours);

            _view.SQLDA.UpdateEntry(entryID: _entry.EntryID,
                timeIn: _in,
                timeOut: _out,
                hours: _hours);

            _view.LogsGrid.FirstDisplayedScrollingRowIndex = _view.LogsGrid.RowCount - 1;

            CheckZeroTotals(_entry);
            CalculateTotalHours();
            LoadView(_view.CalendarSelection);
        }

        public void OpenDeltekWebpage()
        {
            Process.Start(Properties.Settings.Default.deltekURL);
        }

        public void DeleteLogEntryRow(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            // If the user double clicked in the log entry grid and the click was in the delete column then delete that row
            if (e.ColumnIndex == 6)
            {
                _logger.Info("User deleting " + _view.LogsGrid[1, e.RowIndex].Value + " data row from the entry grid.");
                _view.SQLDA.DeleteEntry(_view.EntriesTable.Find(entry => entry.EntryID.Equals(_view.LogsGrid[0, e.RowIndex].Value)));

                if (_view.LogsGrid.Rows.Count > 0) LoadView(_view.CalendarSelection);
                if (_view.LogsGrid.Rows.Count == 0) _view.TotalTime.Text = "Total: 0.0";

                CalculateTotalHours();
                CheckZeroTotals();
                LoadView(_view.CalendarSelection);
            }
        }

        private void CheckZeroTotals(Entries _entry = null)
        {
            bool exists = false;
            if (_entry != null)
            {
                foreach (Totals total in _view.SQLDA.ReadTotals(_view.CalendarSelection))
                {
                    if (total.ProgramName.Equals(_entry.ProgramName)) exists = true;
                }

                if (!exists)
                {
                    _view.SQLDA.AddTotal(program: _view.SQLDA.ReadPrograms(_entry.ProgramName)[0],
                        date: _view.SQLDA.ReadDates(_entry.DateID)[0]);
                }
            }
            foreach (Totals total in _view.SQLDA.ReadTotals(_view.CalendarSelection))
            {
                if (total.Hours == decimal.Zero)
                {
                    _view.SQLDA.DeleteTotal(total: total);
                }
            }
        }

        public void CalculateTotalHours()
        {
            object lastOut = (_view.LogsGrid.Rows.Count != 0) ? _view.LogsGrid.Rows[_view.LogsGrid.Rows.Count - 1].Cells["Out"].Value : null;

            var timeDiff = new TimeSpan();
            if (lastOut == null && _view.LogsGrid.Rows.Count != 0)
            {
                timeDiff = (DateTime.Now - Convert.ToDateTime(_view.LogsGrid.Rows[_view.LogsGrid.Rows.Count - 1].Cells["In"].Value));
            }
            decimal loggedTotal = 0;
            decimal calculatedTotal = 0;

            foreach (Programs program in _view.SQLDA.ReadPrograms((string)null))
            {
                loggedTotal += ReturnTotalHoursAction(program);
            }
            calculatedTotal = loggedTotal + Convert.ToDecimal(timeDiff.TotalMinutes / 60);

            if (loggedTotal == 0 && calculatedTotal < (decimal)0.1)
            {
                _view.TotalTime.Text = "Total: 0.0";
            }
            else if (loggedTotal == 0 && calculatedTotal > (decimal)0.1)
            {
                _view.TotalTime.Text = string.Format("Total: 0.0 ({0})", (timeDiff.TotalMinutes / 60).ToString("N1"));
            }
            else if (lastOut == null && loggedTotal.ToString("N1") != calculatedTotal.ToString("N1"))
            {
                _view.TotalTime.Text = string.Format("Total: {0} ({1})", loggedTotal.ToString("N1"), calculatedTotal.ToString("N1"));
            }
            else
            {
                _view.TotalTime.Text = string.Format("Total: {0}", loggedTotal.ToString("N1"));
            }

            foreach (Programs program in _view.SQLDA.ReadPrograms((string)null))
            {
                decimal totalHours = ReturnTotalHoursAction(program);
                foreach (Totals total in _view.SQLDA.ReadTotals(_view.CalendarSelection))
                {
                    if (total.ProgramName.Equals(program.Name))
                    {
                        _view.SQLDA.UpdateTotal(totalID: total.TotalID,
                            program: program.Name,
                            comments: total.Comments,
                            hours: totalHours);
                    }
                }
            }
        }

        public decimal ReturnTotalHoursAction(Programs program)
        {
            var date = _view.SQLDA.ReadDates(_view.CalendarSelection);
            decimal totalHours = 0;

            // loop through each entry in the log for the selected date
            foreach (Entries entry in _view.SQLDA.ReadEntries(date[0].DateID))
            {
                // Total up all the entries for that program
                if (entry.ProgramName.Equals(program.Name) && entry.Out != null)
                {
                    // Entry matches program so add it to program hours
                    totalHours += (decimal)entry.Hours;
                }
            }
            return totalHours;
        }

        public void FormClose()
        {
            Application.Exit();
        }

        public void ChangeSelectedProgram()
        {
            Properties.Settings.Default.ProgramSelected = _view.ProgramsCombo.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        public void DeleteLogEntry()
        {
            CalculateTotalHours();
            LoadView(_view.CalendarSelection);
        }

        public void EditLogEntry(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            // The program name was manually updated so we only need to recalculate where the totals go
            if (e.ColumnIndex == 1)
            {
                CalculateTotalHours();
                LoadView(_view.CalendarSelection);
                return;
            }

            // The In or Out time was manually updated so recalculate totals by updating the row a with matching index
            if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
            {
                foreach (Entries entry in _view.EntriesTable)
                {
                    DateTime entryIn = (DateTime)_view.EntriesTable[e.RowIndex].In;
                    DateTime entryOut = new DateTime();
                    decimal entryTotal = new decimal();

                    if (!string.IsNullOrEmpty(_view.EntriesTable[e.RowIndex].Out.ToString()))
                    {
                        entryOut = (DateTime)_view.EntriesTable[e.RowIndex].Out;
                        entryTotal = (decimal)(entryOut - entryIn).TotalMinutes / 60;
                    }

                    int entryID = entry.EntryID;

                    entryTotal = entryTotal == decimal.Zero ? decimal.Zero : entryTotal;

                    // If the current row ID matches the one that was updated then send update queries to the database to update the saved data
                    if (entry.EntryID.Equals(_view.LogsGrid.Rows[e.RowIndex].Cells["ID"].Value))
                    {
                        if (entryIn != null)
                        {
                            try
                            {
                                _view.SQLDA.UpdateEntry(entryID: entryID,
                                    timeIn: entryIn);
                            }
                            catch (Exception ex)
                            {
                                _logger.Error("In Update failed: " + ex.Message);
                            }
                        }

                        if (entryOut != new DateTime() && entryTotal != new decimal())
                        {
                            try
                            {
                                _view.SQLDA.UpdateEntry(entryID: entryID,
                                    timeIn: entryIn,
                                    timeOut: entryOut,
                                    hours: entryTotal);
                            }
                            catch (Exception ex)
                            {
                                _logger.Error("Hours Update failed: " + ex.Message);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("not a valid time entry");

            }
            _view.EntriesTable = _view.SQLDA.ReadEntries(_view.CalendarSelection);

            CalculateTotalHours();
            LoadView(_view.CalendarSelection);
        }

        private void ValidateTime(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
            {
                DateTime temp;
                if (string.IsNullOrEmpty(e.FormattedValue.ToString())) { return; }

                if(!DateTime.TryParse(e.FormattedValue.ToString(), out temp))
                {
                    MessageBox.Show("Time entry is not in correct format.", "Invalid Time", MessageBoxButtons.OK);
                    e.Cancel = true;
                }
            }
        }

        public void EditTotalEntry(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == 3)
            {
                Console.WriteLine(Convert.ToInt32(_view.TotalsGrid.Rows[e.RowIndex].Cells["TotalID"].Value));
                Console.WriteLine(_view.TotalsGrid["TotalProgram", e.RowIndex].Value);
                Console.WriteLine((decimal)_view.TotalsGrid["TotalHours", e.RowIndex].Value);
                Console.WriteLine((string)_view.TotalsGrid["TotalComments", e.RowIndex].Value);
                // Update the corresponding item in the database
                _view.SQLDA.UpdateTotal(totalID: Convert.ToInt32(_view.TotalsGrid.Rows[e.RowIndex].Cells["TotalID"].Value),
                    program: _view.TotalsGrid["TotalProgram", e.RowIndex].Value.ToString(),
                    hours: Convert.ToDecimal(_view.TotalsGrid["TotalHours", e.RowIndex].Value),
                    comments: _view.TotalsGrid["TotalComments", e.RowIndex].Value.ToString());
                _view.TotalsTable = _view.SQLDA.ReadTotals(_view.CalendarSelection);
            }
            LoadView(_view.CalendarSelection);
        }

        public void ChangeSelectedDate()
        {
            _view.CalendarSelection = _view.Calendar.SelectionStart;
            LoadView(_view.CalendarSelection);
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

        public void AddDynamicTotalsTooltips(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.ColumnIndex == _view.TotalsGrid.Columns["TotalProgram"].Index) && e.Value != null)
            {
                DataGridViewCell cell = _view.TotalsGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                for (int i = 0; i < _view.ProgramsTable.Count; i++)
                {
                    if (e.Value.Equals(_view.ProgramsTable[i].Name))
                    {
                        cell.ToolTipText = "Charge Code: " + _view.ProgramsTable[i].Code +
                            "\nNotes: " + _view.ProgramsTable[i].Notes;
                    }
                }
            }
        }
    }
}