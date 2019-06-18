using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Time_Keeper.Controllers;

namespace Time_Keeper.Interfaces
{
    public interface IMainForm
    {
        #region Form Variables
        void SetController(MainFormController controller);
        Form mainForm { get; set; }
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
        ToolStripMenuItem SettingsMenuOption { get; set; }
        Timer ClockTimer { get; set; }
        ToolStripMenuItem FileMenuEdit { get; set; }
        ToolStripMenuItem FileMenuReset { get; set; }
        ToolStripMenuItem FileMenuQuit { get; set; }
        ToolStripMenuItem SettingsMenuUpdate { get; set; }
        ToolStripMenuItem SettingsMenuAutoUpdate { get; set; }
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
        void SettingsMenuController();
        void QuitApplication();
        void OpenAbout();
        void ManualUpdateCheck();
        void OpenEditPrograms();
        void ResetDatabase();
        void BtnClockIn_Click();
        void BtnClockOut_Click();
        void BtnOpenDeltek_Click();
        void LogsLog_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e);
        void CalculateTotalHours();
        decimal ReturnTotalHours(Programs program);
        void ToggleAutoUpdate();
        void FrmMain_FormClosing();
        void ChangeSelectedProgram();
        void LogsGrid_UserDeletedRow();
        void LogsGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e);
        void TotalsGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e);
        void DatePicker_DateChanged();
        void PopulateOffFridays();
        void TotalsGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e);
        #endregion
    }
}