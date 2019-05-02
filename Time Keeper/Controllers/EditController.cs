using log4net;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Time_Keeper.Interfaces;

namespace Time_Keeper.Controllers
{
    public class EditController
    {
        IEditView _view;

        public static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public EditController(IEditView view, DataAdapter adapter)
        {
            _logger.Info("Opening the Program editor.");

            _view = view;
            _view.Loading = true;
            _view.SQLDA = adapter;
            try
            {
                _view.ProgramsTable = _view.SQLDA.ReadPrograms((Programs)null);
                if (_view.ProgramsTable.Count > 0) ReloadDataSet(true);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message + "\n" + ex.InnerException);
            }
            _view.ProgramsListBox.SelectedValueChanged += new System.EventHandler(SelectedValueChange);
            _view.SetController(this);
            _view.ProgramName.Select();
            _view.Loading = false;
        }

        /// <summary>
        /// Sets the data source and display column for the programs list box
        /// </summary>
        public void ReloadDataSet(bool clearSelected = false)
        {
            _view.ProgramsListBox.SelectedValueChanged += null;
            _view.ProgramsTable = _view.SQLDA.ReadPrograms((Programs)null, _sorted: true);
            _view.ProgramsListBox.DataSource = _view.ProgramsTable;
            _view.ProgramsListBox.DisplayMember = "Name";
            _view.ProgramName.Text = string.Empty;
            _view.ChargeCode.Text = string.Empty;
            _view.Notes.Text = string.Empty;
            if (clearSelected) _view.ProgramsListBox.ClearSelected();
            _view.SubmitButton.Enabled = false;
            _view.ProgramsListBox.SelectedValueChanged += new System.EventHandler(SelectedValueChange);
        }

        public void SelectedValueChange(object sender, EventArgs e)
        {
            List<Programs> pgmInfo = new List<Programs>();
            if (!_view.Loading && _view.ProgramsListBox.SelectedIndex != -1)
            {
                pgmInfo = _view.SQLDA.ReadPrograms((Programs)_view.ProgramsListBox.SelectedItem);

                _view.ProgramName.Text = pgmInfo[0].Name;
                _view.ChargeCode.Text = pgmInfo[0].Code;
                _view.Notes.Text = pgmInfo[0].Notes;
            }

            // This all just controls what buttons are active and when, if nothing is selected none of the buttons should be active
            if (_view.ProgramsListBox.SelectedItems.Count == 0)
            {
                _view.DemoteButton.Enabled = false;
                _view.PromoteButton.Enabled = false;
                _view.DemoteButton.Enabled = false;
                _view.SubmitButton.Enabled = false;
            }
            else
            {
                _view.DeleteButton.Enabled = true;
                // The first item in the list box is selected so only demote should be enabled
                if (_view.ProgramsListBox.SelectedIndex == 0)
                {
                    _view.PromoteButton.Enabled = false;
                    _view.DemoteButton.Enabled = true;
                }
                // Something in the middle is selected so both promote and demote should be enabled
                else if (_view.ProgramsListBox.SelectedIndex > 0 && _view.ProgramsListBox.SelectedIndex < _view.ProgramsListBox.Items.Count - 1)
                {
                    _view.PromoteButton.Enabled = true;
                    _view.DemoteButton.Enabled = true;
                }
                // The last item in the list box is selected so only promote should be enabled
                else if (_view.ProgramsListBox.SelectedIndex == _view.ProgramsListBox.Items.Count - 1)
                {
                    _view.PromoteButton.Enabled = true;
                    _view.DemoteButton.Enabled = false;
                }
            }
        }

        public void DeleteProgram(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                caption: "Delete Program(s)",
                text: "Are you sure you want to delete the selected program(s)?\n\nThis action cannot be undone.",
                buttons: MessageBoxButtons.YesNo,
                icon: MessageBoxIcon.Exclamation);

            // If the user selects yes to the delete prompt then loop through the selected items and delete them from the programs table in the database

            if (result == DialogResult.Yes)
            {
                _logger.Info("User has chosen to delete the selected program, removing from the database now.)");
                _view.SQLDA.DeleteProgram((Programs)_view.ProgramsListBox.SelectedItem);
                ReloadDataSet(true);
                _view.ProgramName.Clear();
            }
        }

        public void PromoteProgram(object sender, EventArgs e)
        {
            // Save the selected row index so we can reselect it after the swap operation
            int selectedRow = _view.ProgramsListBox.SelectedIndex;

            // Set the item to -1 order, move the one beneath to item previous position, move item from -1 to one beneath start position
            _view.SQLDA.SwapPrograms(_promoteProgram: (Programs)_view.ProgramsListBox.SelectedItem,
                _demoteProgram: (Programs)_view.ProgramsListBox.Items[_view.ProgramsListBox.SelectedIndex - 1]);
            
            _view.ProgramsListBox.ClearSelected();
            _view.ReloadDataSet();
            _view.ProgramsListBox.SetSelected(selectedRow - 1, true);
            _view.ProgramsListBox.Refresh();
        }

        public void DemoteProgram(object sender, EventArgs e)
        {
            // Save the selected row index so we can reselect it after the swap operation
            int selectedRow = _view.ProgramsListBox.SelectedIndex;

            // Set the item to -1 order, move the one beneath to item previous position, move item from -1 to one beneath start position
            _view.SQLDA.SwapPrograms(_promoteProgram: (Programs)_view.ProgramsListBox.Items[_view.ProgramsListBox.SelectedIndex + 1],
                _demoteProgram: (Programs)_view.ProgramsListBox.SelectedItem);

            _view.ProgramsListBox.ClearSelected();
            _view.ReloadDataSet();
            _view.ProgramsListBox.SetSelected(selectedRow + 1, true);
            _view.ProgramsListBox.Refresh();
        }

        public void Hotkeys(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                _view.ProgramsListBox.ClearSelected();
                _view.ProgramName.Clear();
                _view.ChargeCode.Clear();
                _view.Notes.Clear();
            }

            if (e.KeyCode == Keys.Enter && _view.SubmitButton.Enabled == true && !_view.Notes.ContainsFocus)
            {
                _view.SubmitButton.PerformClick();
            }
        }

        public void ButtonStates(object sender, EventArgs e)
        {
            _view.SubmitButton.Enabled = true;
        }

        public void Submit(object sender, EventArgs e)
        {
            _view.SubmitButton.Enabled = false;
            Programs submitted = new Programs();
            submitted.Name = _view.ProgramName.Text;
            submitted.Code = _view.ChargeCode.Text;
            submitted.Notes = _view.Notes.Text;

            if (_view.ProgramsListBox.SelectedItem == null ||
                _view.SQLDA.ReadPrograms((Programs)_view.ProgramsListBox.SelectedItem).Count == 0)
            {
                try
                {
                    _view.SQLDA.AddProgram(_name: submitted.Name, 
                        _order: _view.ProgramsListBox.Items.Count,
                        _code: submitted.Code, 
                        _notes: submitted.Notes);
                    ReloadDataSet(true);
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException.Message.Contains("Violation of PRIMARY KEY"))
                    {
                        DuplicateEntry();
                        ReloadDataSet();
                        return;
                    }
                    else
                    {
                        _logger.Error(ex.Message + "\n" + ex.InnerException);
                    }
                }
            }
            else
            {
                try
                {
                    int selectedRow = _view.ProgramsListBox.SelectedIndex;

                    var item = _view.ProgramsListBox.SelectedItem;

                    _view.SQLDA.UpdateProgram(_program: _view.ProgramsTable[_view.ProgramsListBox.SelectedIndex],
                        _name: submitted.Name, 
                        _code: submitted.Code,
                        _notes: submitted.Notes,
                        _order: selectedRow + 1);
                    ReloadDataSet(true);
                    _view.ProgramsListBox.SetSelected(selectedRow, true);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Unable to create a constant value"))
                    {
                        DuplicateEntry();
                        ReloadDataSet(true);
                        return;
                    }
                }
            }
        }

        private void DuplicateEntry()
        {
            _logger.Warn("Program addition failed due to existing entry with same name, aborting.");
            // The insert attempt failed because the item already exists so alert the user
            MessageBox.Show(
                caption: "Program exists",
                text: "Duplicate entries not allowed, please check the entered name and try again.",
                buttons: MessageBoxButtons.OK,
                icon: MessageBoxIcon.Information);
        }
    }
}