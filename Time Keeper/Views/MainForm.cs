using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Time_Keeper.Controllers;
using static Time_Keeper.CustomSystemMenu;
using Time_Keeper.Interfaces;

namespace Time_Keeper
{
    public partial class MainForm : Form, IMainFormView
    {
        public MainForm()
        {
            InitializeComponent();
        }

        MainFormController _controller;

        public void SetController(MainFormController controller)
        {
            _controller = controller;
        }

        public MainForm mainForm
        {
            get { return this; }
            set { mainForm = value; }
        }

        public DataAdapter SQLDA
        {
            get { return SQLDA; }
            set { SQLDA = value; }
        }
        public Properties.Settings tkSetting
        {
            get { return Properties.Settings.Default; }
            set { Properties.Settings.Default = value; }
        }
        public System.Timers.Timer ClockTimer
        {
            get { return ClockTimer; }
            set { ClockTimer = value; }
        }
        public DataTable ProgramsTable
        {
            get { return ProgramsTable; }
            set { ProgramsTable = value; }
        }
        public DataTable LogsTable
        {
            get { return LogsTable; }
            set { LogsTable = value; }
        }
        public DataTable TotalsTable
        {
            get { return TotalsTable; }
            set { TotalsTable = value; }
        }
        public DataTable DatesTable
        {
            get { return DatesTable; }
            set { DatesTable = value; }
        }
        public SystemMenu m_SystemMenu
        {
            get { return m_SystemMenu; }
            set { m_SystemMenu = value; }
        }
        public NetworkOperations NetOps
        {
            get { return NetOps; }
            set { NetOps = value; }
        }

        public ToolStripMenuItem FileMenuEdit
        {
            get { return FileMenuEdit; }
            set { FileMenuEdit = value; }
        }
        public ToolStripMenuItem FileMenuReset
        {
            get { return FileMenuReset; }
            set { FileMenuReset = value; }
        }
        public ToolStripMenuItem FileMenuQuit
        {
            get { return FileMenuQuit; }
            set { FileMenuQuit = value; }
        }
        public ToolStripMenuItem HelpMenuUpdate
        {
            get { return HelpMenuUpdate; }
            set { HelpMenuUpdate = value; }
        }
        public ToolStripMenuItem HelpMenuAutoUpdate
        {
            get { return HelpMenuAutoUpdate; }
            set { HelpMenuAutoUpdate = value; }
        }
        public ComboBox ProgramsCombo
        {
            get { return ProgramsCombo; }
            set { ProgramsCombo = value; }
        }
        public Button ClockIn
        {
            get { return ClockIn; }
            set { ClockIn = value; }
        }
        public Button ClockOut
        {
            get { return ClockOut; }
            set { ClockOut = value; }
        }
        public TextBox TotalTime
        {
            get { return TotalTime; }
            set { TotalTime = value; }
        }
        public Button OpenDeltek
        {
            get { return OpenDeltek; }
            set { OpenDeltek = value; }
        }
        public Label CurrentTime
        {
            get { return CurrentTime; }
            set { CurrentTime = value; }
        }
        public MonthCalendar Calendar
        {
            get { return Calendar; }
            set { Calendar = value; }
        }
        public DataGridView LogsGrid
        {
            get { return LogsGrid; }
            set { LogsGrid = value; }
        }
        public DataGridView TotalsGrid
        {
            get { return TotalsGrid; }
            set { TotalsGrid = value; }
        }

        protected override void WndProc(ref Message msg)
        {
            _controller.WndProc(ref msg);
        }
        public void StartClock()
        {
            _controller.StartClock();
        }
        public void ClockTimer_Tick(object sender, EventArgs e)
        {
            _controller.ClockTimer_Tick(sender, e);
        }
        public void FileMenu_Quit_Click(object sender, EventArgs e)
        {
            _controller.FileMenu_Quit_Click(sender, e);
        }
        public void HelpMenu_About_Click(object sender, EventArgs e)
        {
            _controller.HelpMenu_About_Click(sender, e);
        }
        public void HelpMenu_Update_Click(object sender, EventArgs e)
        {
            _controller.HelpMenu_Update_Click(sender, e);
        }
        public void FileMenu_Edit_Click(object sender, EventArgs e)
        {
            _controller.FileMenu_Edit_Click(sender, e);
        }
        public void FileMenu_Reset_Click(object sender, EventArgs e)
        {
            _controller.FileMenu_Reset_Click(sender, e);
        }
        public void BtnClockIn_Click(object sender, EventArgs e)
        {
            _controller.BtnClockIn_Click(sender, e);
        }
        public void BtnClockOut_Click(object sender, EventArgs e)
        {
            _controller.BtnClockOut_Click(sender, e);
        }
        public void BtnOpenDeltek_Click(object sender, EventArgs e)
        {
            _controller.BtnOpenDeltek_Click(sender, e);
        }
        public void DgLog_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _controller.LogsGrid_CellContentDoubleClick(sender, e);
        }
        public void FrmMain_Load(object sender, EventArgs e)
        {
            _controller.FrmMain_Load(sender, e);
        }
        public void CalculateTotalHours()
        {
            _controller.CalculateTotalHours();
        }
        public void MenuUpdateOnStart_Click(object sender, EventArgs e)
        {
            _controller.MenuUpdateOnStart_Click(sender, e);
        }
        public void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _controller.FrmMain_FormClosing(sender, e);
        }
        public void CmbPrograms_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _controller.CmbPrograms_SelectionChangeCommitted(sender, e);
        }
        public void DgLog_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            _controller.LogsGrid_UserDeletedRow(sender, e);
        }
        public void DgLog_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            _controller.LogsGrid_CellEndEdit(sender, e);
        }
        public void DgTotal_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            _controller.TotalsGrid_CellEndEdit(sender, e);
        }
        public void DatePicker_DateChanged(object sender, DateRangeEventArgs e)
        {
            _controller.DatePicker_DateChanged(sender, e);
        }
        public void PopulateOffFridays()
        {
            _controller.PopulateOffFridays();
        }
        public void dgTotal_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            _controller.TotalsGrid_CellFormatting(sender, e);
        }
    }
}