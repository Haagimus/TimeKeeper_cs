using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Time_Keeper.Controllers;
using Time_Keeper.Interfaces;

namespace Time_Keeper
{
    public partial class EditProgramsForm : Form, IEditView
    {
        public EditProgramsForm()
        {
            InitializeComponent();
        }

        EditController _controller;

        public void SetController (EditController controller)
        {
            _controller = controller;
        }

        public DataAdapter SQLDA { get; set; }

        public List<Programs> ProgramsTable { get; set; }

        public List<Programs> ProgramsList
        {
            get { return SQLDA.ReadPrograms(); }
            set { lbPrograms.DataSource= value; }
        }

        public bool Loading { get; set; }

        public ListBox ProgramsListBox
        {
            get { return lbPrograms; }
            set { lbPrograms = value; }
        }

        public TextBox ProgramName
        {
            get { return tbProgramName; }
            set { tbProgramName = value; }
        }

        public TextBox ChargeCode
        {
            get { return tbProgramCode; }
            set { tbProgramCode = value; }
        }

        public TextBox Notes
        {
            get { return tbProgramNotes; }
            set { tbProgramNotes = value; }
        }

        public Button SubmitButton
        {
            get { return btnSubmit; }
            set { btnSubmit = value; }
        }

        public Button DeleteButton
        {
            get { return btnDeleteProgram; }
            set { btnDeleteProgram = value; }
        }

        public Button PromoteButton
        {
            get { return btnMoveUp; }
            set { btnMoveUp = value; }
        }

        public Button DemoteButton
        {
            get { return btnMoveDown; }
            set { btnMoveDown = value; }
        }

        public void ReloadDataSet(bool clearSelected = false)
        {
            _controller.ReloadDataSet(clearSelected);
        }

        public void SelectedValueChange(object sender, EventArgs e)
        {
            _controller.SelectedValueChange(sender, e);
        }

        public void DeleteProgram(object sender, EventArgs e)
        {
            _controller.DeleteProgram(sender, e);
        }

        public void PromoteProgram(object sender, EventArgs e)
        {
            _controller.PromoteProgram(sender, e);
        }

        public void DemoteProgram(object sender, EventArgs e)
        {
            _controller.DemoteProgram(sender, e);
        }

        public void Hotkeys(object sender, KeyEventArgs e)
        {
            _controller.Hotkeys(sender, e);
        }

        public void ButtonStates(object sender, EventArgs e)
        {
            _controller.ButtonStates(sender, e);
        }

        public void SubmitProgram(object sender, EventArgs e)
        {
            _controller.Submit(sender, e);
        }
    }
}
