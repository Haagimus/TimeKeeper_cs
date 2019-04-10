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

        public DataAdapter SQLDA { get; set; }
        public string SaveLocation
        {
            get { return Properties.Settings.Default.saveFile; }
            set { Properties.Settings.Default.saveFile = value; }
        }
        public Timer ClockTimer { get; set; }
        public DataTable ProgramsTable { get; set; }
        public DataTable EntriesTable { get; set; }
        public DataTable TotalsTable { get; set; }
        public DataTable DatesTable { get; set; }
        public SystemMenu m_SystemMenu { get; set; }
        public NetworkOperations NetOps { get; set; }
        public DateTime CalendarSelection
        {
            get { return Calendar.SelectionStart; }
            set { Calendar.SelectionStart = value; }
        }

        public ToolStripMenuItem FileMenuEdit
        {
            get { return FileMenu_Edit; }
            set { FileMenu_Edit = value; }
        }
        public ToolStripMenuItem FileMenuReset
        {
            get { return FileMenu_Reset; }
            set { FileMenu_Reset = value; }
        }
        public ToolStripMenuItem FileMenuQuit
        {
            get { return FileMenu_Quit; }
            set { FileMenu_Quit = value; }
        }
        public ToolStripMenuItem HelpMenuUpdate
        {
            get { return HelpMenu_Update; }
            set { HelpMenu_Update = value; }
        }
        public ToolStripMenuItem HelpMenuAutoUpdate
        {
            get { return HelpMenu_UpdateOnStart; }
            set { HelpMenu_UpdateOnStart = value; }
        }
        public ComboBox ProgramsCombo
        {
            get { return cmbPrograms; }
            set { cmbPrograms = value; }
        }
        public Button ClockIn
        {
            get { return btnClockIn; }
            set { btnClockIn = value; }
        }
        public Button ClockOut
        {
            get { return btnClockOut; }
            set { btnClockOut = value; }
        }
        public TextBox TotalTime
        {
            get { return tbTotalTime; }
            set { tbTotalTime = value; }
        }
        public Button OpenDeltek
        {
            get { return btnOpenDeltek; }
            set { btnOpenDeltek = value; }
        }
        public MonthCalendar Calendar
        {
            get { return DatePicker; }
            set { DatePicker = value; }
        }
        public DataGridView LogsGrid
        {
            get { return dgLog; }
            set { dgLog = value; }
        }
        public DataGridView TotalsGrid
        {
            get { return dgTotal; }
            set { dgTotal = value; }
        }

        //public override void WndProc(ref Message msg)
        //{
        //    _controller.WndProc(ref msg);
        //}

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
        public double ReturnTotalHours()
        {
            return _controller.ReturnTotalHours();
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