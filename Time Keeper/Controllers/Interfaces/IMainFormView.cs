using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Time_Keeper.Controllers;

namespace Time_Keeper.Interfaces
{
    public interface IMainFormView
    {
        #region Form Variables
        void SetController(MainFormController controller);
        MainForm mainForm { get; set; }
        DataAdapter SQLDA { get; set; }
        List<Programs> ProgramsTable { get; set; }
        List<Entries> EntriesTable { get; set; }
        List<Totals> TotalsTable { get; set; }
        List<Dates> DatesTable { get; set; }
        NetworkOperations NetOps { get; set; }
        #endregion

        #region Form Controls
        MenuStrip MainMenu { get; set; }
        ToolStripMenuItem FileMenuOption { get; set; }
        ToolStripMenuItem HelpMenuOption { get; set; }
        Timer ClockTimer { get; set; }
        ToolStripMenuItem FileMenuEdit { get; set; }
        ToolStripMenuItem FileMenuReset { get; set; }
        ToolStripMenuItem FileMenuQuit { get; set; }
        ToolStripMenuItem HelpMenuUpdate { get; set; }
        ToolStripMenuItem HelpMenuAutoUpdate { get; set; }
        ComboBox ProgramsCombo { get; set; }
        Button ClockIn { get; set; }
        Button ClockOut { get; set; }
        TextBox TotalTime { get; set; }
        Button OpenDeltek { get; set; }
        MonthCalendar Calendar { get; set; }
        DataGridView LogsGrid { get; set; }
        DataGridView TotalsGrid { get; set; }
        DateTime CalendarSelection { get; set; }
        #endregion

        #region Form Methods
        void StartClock();
        void ClockTimer_Tick(object sender, EventArgs e);
        void HelpMenuController();
        void FileMenu_Quit_Click(object sender, EventArgs e);
        void HelpMenu_About_Click(object sender, EventArgs e);
        void HelpMenu_Update_Click(object sender, EventArgs e);
        void FileMenu_Edit_Click(object sender, EventArgs e);
        void FileMenu_Reset_Click(object sender, EventArgs e);
        void BtnClockIn_Click(object sender, EventArgs e);
        void BtnClockOut_Click(object sender, EventArgs e);
        void BtnOpenDeltek_Click(object sender, EventArgs e);
        void DgLog_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e);
        void FrmMain_Load(object sender, EventArgs e);
        void CalculateTotalHours();
        decimal ReturnTotalHours(Programs program);
        void MenuUpdateOnStart_Click(object sender, EventArgs e);
        void FrmMain_FormClosing(object sender, FormClosingEventArgs e);
        void CmbPrograms_SelectionChangeCommitted(object sender, EventArgs e);
        void DgLog_UserDeletedRow(object sender, DataGridViewRowEventArgs e);
        void DgLog_CellEndEdit(object sender, DataGridViewCellEventArgs e);
        void DgTotal_CellEndEdit(object sender, DataGridViewCellEventArgs e);
        void DatePicker_DateChanged(object sender, DateRangeEventArgs e);
        void PopulateOffFridays();
        void dgTotal_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e);
        #endregion
    }
}