using System;
using System.Data;
using System.Windows.Forms;
using Time_Keeper.Controllers;
using static Time_Keeper.CustomSystemMenu;

namespace Time_Keeper.Interfaces
{
    public interface IMainFormView
    {
        #region Form Variables
        MainForm mainForm { get; set; }
        DataAdapter SQLDA { get; set; }
        string SaveLocation { get; set; }
        Timer ClockTimer { get; set; }
        DataTable ProgramsTable { get; set; }
        DataTable EntriesTable { get; set; }
        DataTable TotalsTable { get; set; }
        DataTable DatesTable { get; set; }
        SystemMenu m_SystemMenu { get; set; }
        NetworkOperations NetOps { get; set; }
        DateTime CalendarSelection { get; set; }
        #endregion

        #region Form Controls
        void SetController(MainFormController controller);
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
        #endregion

        #region Form Methods
        void StartClock();
        void ClockTimer_Tick(object sender, EventArgs e);
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
        double ReturnTotalHours();
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