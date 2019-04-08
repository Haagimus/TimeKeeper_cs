using Time_Keeper.Interfaces;
using log4net;
using System.Data;
using System.Windows.Forms;
using System;
using System.Collections.Generic;

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
            _view.ProgramsTable = _view.SQLDA.TKDS.Tables["ProgramsTable"];
            view.SetController(this);
            LoadView();
            _view.Loading = false;
        }

        public void LoadView()
        {
            _view.ReloadDataSet();
        }

        /// <summary>
        /// Sets the data source and display column for the programs list box
        /// </summary>
        public void ReloadDataSet(bool clearSelected = false)
        {
            _view.SQLDA.ReadData(new string[] { "ProgramsTable" });
            _view.ProgramsListBox.DataSource = _view.ProgramsTable;
            _view.ProgramsListBox.DisplayMember = _view.ProgramsTable.Columns[1].ToString();
            _view.ProgramName.Text = string.Empty;
            _view.ChargeCode.Text = string.Empty;
            _view.Notes.Text = string.Empty;
            _view.ProgramsListBox.Refresh();
            if (clearSelected) _view.ProgramsListBox.ClearSelected();
            _view.SubmitButton.Enabled = false;
        }

        public void SelectedValueChange(object sender, EventArgs e)
        {
            List<object> pgmInfo = null;
            if (!_view.Loading && _view.ProgramsListBox.SelectedIndex != -1)
            {
                pgmInfo = _view.SQLDA.SelectQuery(new string[] { "Code", "Notes" },
                    new string[] { "ProgramsTable", "ProgramsTable" },
                    new string[] { "Program = '" + _view.ProgramsListBox.GetItemText(_view.ProgramsListBox.SelectedItem) + "'", "Program = '" + _view.ProgramsListBox.GetItemText(_view.ProgramsListBox.SelectedItem) + "'" });

                _view.ProgramName.Text = _view.ProgramsListBox.GetItemText(_view.ProgramsListBox.SelectedItem);
                _view.ChargeCode.Text = pgmInfo[0].ToString();
                _view.Notes.Text = pgmInfo[1].ToString();
            }

            // This all just controls what buttons are active and when, if nothing is selected none of the buttons should be active
            if (_view.ProgramsListBox.SelectedItems.Count == 0)
            {
                _view.DemoteButton.Enabled = false;
                _view.PromoteButton.Enabled = false;
                _view.DemoteButton.Enabled = false;
                _view.SubmitButton.Enabled = false;
            }
            else if (_view.ProgramsListBox.SelectedItems.Count == 1)
            {
                _view.DemoteButton.Enabled = true;

                if (_view.ProgramsListBox.SelectedIndex == _view.ProgramsListBox.Items.Count - 1)
                {
                    _view.PromoteButton.Enabled = true;
                    _view.DemoteButton.Enabled = false;
                }
                else if (_view.ProgramsListBox.SelectedIndex == 0)
                {
                    _view.PromoteButton.Enabled = false;
                    _view.DemoteButton.Enabled = true;
                }
                else if (_view.ProgramsListBox.SelectedIndex > 0 && _view.ProgramsListBox.SelectedIndex < _view.ProgramsListBox.Items.Count - 1)
                {
                    _view.PromoteButton.Enabled = true;
                    _view.DemoteButton.Enabled = true;
                }
            }
            else if (_view.ProgramsListBox.SelectedItems.Count > 1)
            {
                _view.PromoteButton.Enabled = false;
                _view.DemoteButton.Enabled = false;
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
                _view.SQLDA.DeleteProgram(Convert.ToInt32((_view.ProgramsListBox.SelectedItem as DataRowView)["ID"]));
                ReloadDataSet(true);
                _view.ProgramName.Clear();
            }
        }

        public void PromoteProgram(object sender, EventArgs e)
        {
            // TODO: Update this to work with EF6
            int selectedRow = _view.ProgramsListBox.SelectedIndex;
            // Get the current order number of the selected item
            List<object> item = _view.SQLDA.ReadDataQuery(new string[] { "[Order]" }, new string[] { "ProgramsTable" }, new string[] { "Program='" + _view.ProgramsListBox.Text + "'" });
            int start = Convert.ToInt32(item[0]);
            int end = Convert.ToInt32(item[0]) - 1;
            int temp = -1;

            // Set the item to -1 order, move the one beneath to item previous position, move item from -1 to one beneath start position
            _view.SQLDA.WriteMultiDataQuery(new string[]{"UPDATE ProgramsTable SET [Order]=" + temp + " WHERE [Order]=" + start,
                "UPDATE ProgramsTable SET [Order]=" + start + " WHERE [Order]=" + end,
                "UPDATE ProgramsTable SET [Order]=" + end + " WHERE [Order]=" + temp});

            _view.SQLDA.ReadData(new string[] { "ProgramsTable" });
            _view.ProgramsListBox.ClearSelected();
            _view.ProgramsListBox.SetSelected(selectedRow - 1, true);
            _view.ProgramsListBox.Refresh();
        }

        public void DemoteProgram(object sender, EventArgs e)
        {
            // TODO: Update this to work with EF6
            int selectedRow = Convert.ToInt32((_view.ProgramsListBox.SelectedItem as DataRowView)["ID"]);
            // Get the current order number of the selected item
            List<ProgramEntry> programs = _view.SQLDA.ReadPrograms();

            // Store the selected item order number in a temp positon

            // Update the order of the selected item to -1

            // Update the order of the first item below to the temp position

            //List<object> item = SQLDA.ReadDataQuery(new string[] { "[Order]" }, new string[] { "ProgramsTable" }, new string[] { "Program='" + lbPrograms.Text + "'" });
            int start = programs[0].Order;
            int end = programs[0].Order + 1;
            int temp = -1;

            // Set the item to -1 order, move the one beneath to item previous position, move item from -1 to one beneath start position
            _view.SQLDA.WriteMultiDataQuery(new string[]{"UPDATE ProgramsTable SET [Order]=" + temp + " WHERE [Order]=" + start,
                "UPDATE ProgramsTable SET [Order]=" + start + " WHERE [Order]=" + end,
                "UPDATE ProgramsTable SET [Order]=" + end + " WHERE [Order]=" + temp});

            _view.SQLDA.ReadData(new string[] { "ProgramsTable" });
            _view.ProgramsListBox.ClearSelected();
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

            if (e.KeyCode == Keys.Enter && _view.SubmitButton.Enabled == true)
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

            if (_view.ProgramsListBox.SelectedItems.Count == 0)
            {
                try
                {
                    _view.SQLDA.AddProgram(_view.ProgramName.Text, _view.ProgramsListBox.Items.Count, _view.ChargeCode.Text, _view.Notes.Text);
                    ReloadDataSet(true);
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.InnerException.Message.Contains("UNIQUE constraint failed"))
                    {
                        DuplicateEntry();
                        ReloadDataSet();
                        return;
                    }
                }
            }
            else
            {
                try
                {
                    _view.SQLDA.UpdateProgram(Convert.ToInt32((_view.ProgramsListBox.SelectedItem as DataRowView)["ID"]), _view.ProgramName.Text, _view.ChargeCode.Text, _view.Notes.Text);
                    ReloadDataSet();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.InnerException.Message.Contains("UNIQUE constraint failed"))
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