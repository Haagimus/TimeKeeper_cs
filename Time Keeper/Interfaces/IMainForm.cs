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
        void CreateSettingsMenu();
        void ToggleAutoUpdate(object sender, EventArgs e);
        void SettingsMenu_Update_Click(object sender, EventArgs e);
        void FileMenu_Edit_Click(object sender, EventArgs e);
        void FileMenu_Reset_Click(object sender, EventArgs e);
        void BtnClockIn_Click(object sender, EventArgs e);
        void BtnClockOut_Click(object sender, EventArgs e);
        void BtnOpenDeltek_Click(object sender, EventArgs e);
        void DgLog_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e);
        decimal ReturnTotalHours(Programs program);
        void FrmMain_FormClosing(object sender, EventArgs e);
        void CmbPrograms_SelectionChangeCommitted(object sender, EventArgs e);
        void LogsGrid_UserDeletedRow(object sender, EventArgs e);
        void LogsGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e);
        void TotalsGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e);
        void DatePicker_DateChanged(object sender, EventArgs e);
        void TotalsGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e);
        #endregion
    }
}