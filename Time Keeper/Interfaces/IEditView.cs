using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Time_Keeper.Controllers;

namespace Time_Keeper.Interfaces
{
    public interface IEditView
    {
        #region Form Variables
        bool Loading { get; set; }
        List<Program> ProgramsList { get; set; }
        DataAdapter SQLDA { get; set; }
        #endregion

        #region Form Controls
        DataTable ProgramsTable { get; set; }
        ListBox ProgramsListBox { get; set; }
        TextBox ProgramName { get; set; }
        TextBox ChargeCode { get; set; }
        TextBox Notes { get; set; }
        Button SubmitButton { get; set; }
        Button DeleteButton { get; set; }
        Button PromoteButton { get; set; }
        Button DemoteButton { get; set; } 
        #endregion

        #region Form Controls
        void SetController(EditController controller);
        void ReloadDataSet(bool clearSelected = false);
        void SelectedValueChange(object sender, EventArgs e);
        void ButtonStates(object sender, EventArgs e);
        void PromoteProgram(object sender, EventArgs e);
        void DemoteProgram(object sender, EventArgs e);
        void SubmitProgram(object sender, EventArgs e);
        void DeleteProgram(object sender, EventArgs e);
        void Hotkeys(object sender, KeyEventArgs e);
        #endregion
    }
}
