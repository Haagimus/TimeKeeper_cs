using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Time_Keeper.Controllers;
using Time_Keeper.Interfaces;

namespace Time_Keeper
{
    public partial class MainForm : Form, IMainForm
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

        public Form mainForm
        {
            get { return this; }
            set { mainForm = value; }
        }
        public MenuStrip MainMenu
        {
            get { return menuStrip1; }
            set { menuStrip1 = value; }
        }
        public ToolStripMenuItem FileMenuOption
        {
            get { return FileMenu; }
            set { FileMenu = value; }
        }
        public ToolStripMenuItem SettingsMenuOption
        {
            get { return SettingsMenu; }
            set { SettingsMenu = value; }
        }
        public DataAdapter SQLDA { get; set; }
        public Timer ClockTimer { get; set; }
        public List<Programs> ProgramsTable { get; set; }
        public List<Entries> EntriesTable { get; set; }
        public List<Totals> TotalsTable { get; set; }
        public List<Dates> DatesTable { get; set; }
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
        public ToolStripMenuItem SettingsMenuUpdate
        {
            get { return SettingsMenu_Update; }
            set { SettingsMenu_Update = value; }
        }
        public ToolStripMenuItem SettingsMenuAutoUpdate
        {
            get { return SettingsMenu_UpdateOnStart; }
            set { SettingsMenu_UpdateOnStart = value; }
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
        public void SettingsMenuController()
        {
            _controller.SettingsMenuController();
        }
        public void StartClock()
        {
            _controller.StartClock();
        }
        public void ClockTimer_Tick(object sender, EventArgs e)
        {
            _controller.ClockTick(sender, e);
        }
        public void QuitApplication()
        {
            _controller.QuitApplication();
        }
        public void OpenAbout()
        {
            _controller.SettingsMenu_About_Click();
        }
        public void ManualUpdateCheck()
        {
            _controller.SettingsMenu_Update_Click();
        }
        public void OpenEditPrograms()
        {
            _controller.OpenEditPrograms();
        }
        public void ResetDatabase()
        {
            _controller.ResetDatabase();
        }
        public void BtnClockIn_Click()
        {
            _controller.ClockIn();
        }
        public void BtnClockOut_Click()
        {
            _controller.ClockOut();
        }
        public void BtnOpenDeltek_Click()
        {
            _controller.OpenDeltek();
        }
        public void LogsLog_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _controller.DeleteLogEntryRow(sender, e);
        }
        public void FrmMain_Load()
        {
            _controller.LoadFormData();
        }
        public void CalculateTotalHours()
        {
            _controller.CalculateTotalHours();
        }
        public decimal ReturnTotalHours(Programs program)
        {
            return _controller.ReturnTotalHours(program);
        }
        public void ToggleAutoUpdate()
        {
            _controller.ToggleAutoUpdate();
        }
        public void FrmMain_FormClosing()
        {
            _controller.FormClose();
        }
        public void ChangeSelectedProgram()
        {
            _controller.ChangeSelectedProgram();
        }
        public void LogsGrid_UserDeletedRow()
        {
            _controller.DeleteLogEntry();
        }
        public void LogsGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            _controller.EditLogEntry(sender, e);
        }
        public void TotalsGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            _controller.EditTotalEntry(sender, e);
        }
        public void DatePicker_DateChanged()
        {
            _controller.ChangeSelectedDate();
        }
        public void PopulateOffFridays()
        {
            _controller.PopulateOffFridays();
        }
        public void TotalsGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            _controller.AddDynamicTotalsTooltips(sender, e);
        }
    }
}