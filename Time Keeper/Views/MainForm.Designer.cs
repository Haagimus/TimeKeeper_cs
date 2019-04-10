namespace Time_Keeper
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenu_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenu_Reset = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FileMenu_Quit = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu_Update = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.HelpMenu_About = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPrograms = new System.Windows.Forms.ComboBox();
            this.btnClockIn = new System.Windows.Forms.Button();
            this.btnClockOut = new System.Windows.Forms.Button();
            this.tbTotalTime = new System.Windows.Forms.TextBox();
            this.btnOpenDeltek = new System.Windows.Forms.Button();
            this.dgLog = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Program = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.In = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Out = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Hours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LogDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LogRowDelete = new System.Windows.Forms.DataGridViewImageColumn();
            this.dgTotal = new System.Windows.Forms.DataGridView();
            this.TotalID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalProgram = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalHours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalComments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DatePicker = new System.Windows.Forms.MonthCalendar();
            this.HelpMenu_UpdateOnStart = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTotal)).BeginInit();
            this.SuspendLayout();
            // 
            // FileMenu
            // 
            this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu_Edit,
            this.FileMenu_Reset,
            this.toolStripSeparator1,
            this.FileMenu_Quit});
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(37, 20);
            this.FileMenu.Text = "File";
            // 
            // FileMenu_Edit
            // 
            this.FileMenu_Edit.Name = "FileMenu_Edit";
            this.FileMenu_Edit.Size = new System.Drawing.Size(174, 22);
            this.FileMenu_Edit.Text = "Edit Programs";
            this.FileMenu_Edit.Click += new System.EventHandler(this.FileMenu_Edit_Click);
            // 
            // FileMenu_Reset
            // 
            this.FileMenu_Reset.Name = "FileMenu_Reset";
            this.FileMenu_Reset.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.FileMenu_Reset.Size = new System.Drawing.Size(174, 22);
            this.FileMenu_Reset.Text = "Reset Form";
            this.FileMenu_Reset.Click += new System.EventHandler(this.FileMenu_Reset_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(171, 6);
            // 
            // FileMenu_Quit
            // 
            this.FileMenu_Quit.Name = "FileMenu_Quit";
            this.FileMenu_Quit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.FileMenu_Quit.Size = new System.Drawing.Size(174, 22);
            this.FileMenu_Quit.Text = "Quit";
            this.FileMenu_Quit.Click += new System.EventHandler(this.FileMenu_Quit_Click);
            // 
            // HelpMenu
            // 
            this.HelpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpMenu_Update,
            this.HelpMenu_UpdateOnStart,
            this.toolStripSeparator2,
            this.HelpMenu_About});
            this.HelpMenu.Name = "HelpMenu";
            this.HelpMenu.Size = new System.Drawing.Size(44, 20);
            this.HelpMenu.Text = "Help";
            // 
            // HelpMenu_Update
            // 
            this.HelpMenu_Update.Name = "HelpMenu_Update";
            this.HelpMenu_Update.Size = new System.Drawing.Size(179, 22);
            this.HelpMenu_Update.Text = "Check for updates...";
            this.HelpMenu_Update.Click += new System.EventHandler(this.HelpMenu_Update_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(176, 6);
            // 
            // HelpMenu_About
            // 
            this.HelpMenu_About.Name = "HelpMenu_About";
            this.HelpMenu_About.Size = new System.Drawing.Size(179, 22);
            this.HelpMenu_About.Text = "About";
            this.HelpMenu_About.Click += new System.EventHandler(this.HelpMenu_About_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.HelpMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(581, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.CausesValidation = false;
            this.label1.Location = new System.Drawing.Point(13, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Select a program:";
            // 
            // cmbPrograms
            // 
            this.cmbPrograms.DisplayMember = "Programs.ProgramName";
            this.cmbPrograms.FormattingEnabled = true;
            this.cmbPrograms.Location = new System.Drawing.Point(114, 25);
            this.cmbPrograms.Name = "cmbPrograms";
            this.cmbPrograms.Size = new System.Drawing.Size(129, 21);
            this.cmbPrograms.TabIndex = 1;
            this.cmbPrograms.SelectionChangeCommitted += new System.EventHandler(this.CmbPrograms_SelectionChangeCommitted);
            // 
            // btnClockIn
            // 
            this.btnClockIn.CausesValidation = false;
            this.btnClockIn.Location = new System.Drawing.Point(16, 52);
            this.btnClockIn.Name = "btnClockIn";
            this.btnClockIn.Size = new System.Drawing.Size(110, 23);
            this.btnClockIn.TabIndex = 2;
            this.btnClockIn.Text = "Clock In";
            this.btnClockIn.UseVisualStyleBackColor = true;
            this.btnClockIn.Click += new System.EventHandler(this.BtnClockIn_Click);
            // 
            // btnClockOut
            // 
            this.btnClockOut.CausesValidation = false;
            this.btnClockOut.Location = new System.Drawing.Point(133, 52);
            this.btnClockOut.Name = "btnClockOut";
            this.btnClockOut.Size = new System.Drawing.Size(110, 23);
            this.btnClockOut.TabIndex = 3;
            this.btnClockOut.Text = "Clock Out";
            this.btnClockOut.UseVisualStyleBackColor = true;
            this.btnClockOut.Click += new System.EventHandler(this.BtnClockOut_Click);
            // 
            // tbTotalTime
            // 
            this.tbTotalTime.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTotalTime.Location = new System.Drawing.Point(16, 82);
            this.tbTotalTime.Name = "tbTotalTime";
            this.tbTotalTime.ReadOnly = true;
            this.tbTotalTime.Size = new System.Drawing.Size(227, 33);
            this.tbTotalTime.TabIndex = 9;
            this.tbTotalTime.TabStop = false;
            this.tbTotalTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnOpenDeltek
            // 
            this.btnOpenDeltek.CausesValidation = false;
            this.btnOpenDeltek.Location = new System.Drawing.Point(16, 122);
            this.btnOpenDeltek.Name = "btnOpenDeltek";
            this.btnOpenDeltek.Size = new System.Drawing.Size(227, 23);
            this.btnOpenDeltek.TabIndex = 4;
            this.btnOpenDeltek.Text = "Open Deltek";
            this.btnOpenDeltek.UseVisualStyleBackColor = true;
            this.btnOpenDeltek.Click += new System.EventHandler(this.BtnOpenDeltek_Click);
            // 
            // dgLog
            // 
            this.dgLog.AllowUserToAddRows = false;
            this.dgLog.AllowUserToDeleteRows = false;
            this.dgLog.AllowUserToResizeColumns = false;
            this.dgLog.AllowUserToResizeRows = false;
            this.dgLog.CausesValidation = false;
            this.dgLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Program,
            this.In,
            this.Out,
            this.Hours,
            this.LogDate,
            this.LogRowDelete});
            this.dgLog.Location = new System.Drawing.Point(255, 25);
            this.dgLog.Name = "dgLog";
            this.dgLog.RowHeadersVisible = false;
            this.dgLog.Size = new System.Drawing.Size(311, 294);
            this.dgLog.TabIndex = 6;
            this.dgLog.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgLog_CellContentDoubleClick);
            this.dgLog.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgLog_CellEndEdit);
            this.dgLog.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DgLog_UserDeletedRow);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // Program
            // 
            this.Program.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Program.HeaderText = "Program";
            this.Program.Name = "Program";
            // 
            // In
            // 
            dataGridViewCellStyle1.Format = "HH:mm";
            this.In.DefaultCellStyle = dataGridViewCellStyle1;
            this.In.HeaderText = "In";
            this.In.Name = "In";
            this.In.Width = 50;
            // 
            // Out
            // 
            dataGridViewCellStyle2.Format = "HH:mm";
            this.Out.DefaultCellStyle = dataGridViewCellStyle2;
            this.Out.HeaderText = "Out";
            this.Out.Name = "Out";
            this.Out.Width = 50;
            // 
            // Hours
            // 
            dataGridViewCellStyle3.Format = "N1";
            this.Hours.DefaultCellStyle = dataGridViewCellStyle3;
            this.Hours.HeaderText = "Hours";
            this.Hours.Name = "Hours";
            this.Hours.ReadOnly = true;
            this.Hours.Width = 50;
            // 
            // LogDate
            // 
            this.LogDate.HeaderText = "Date";
            this.LogDate.Name = "LogDate";
            this.LogDate.ReadOnly = true;
            this.LogDate.Visible = false;
            // 
            // LogRowDelete
            // 
            this.LogRowDelete.HeaderText = "D";
            this.LogRowDelete.Image = global::Time_Keeper.Properties.Resources.Delete_icon;
            this.LogRowDelete.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.LogRowDelete.Name = "LogRowDelete";
            this.LogRowDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.LogRowDelete.Width = 23;
            // 
            // dgTotal
            // 
            this.dgTotal.AllowUserToAddRows = false;
            this.dgTotal.AllowUserToDeleteRows = false;
            this.dgTotal.AllowUserToResizeColumns = false;
            this.dgTotal.AllowUserToResizeRows = false;
            this.dgTotal.CausesValidation = false;
            this.dgTotal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgTotal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TotalID,
            this.TotalProgram,
            this.TotalHours,
            this.TotalComments,
            this.TotalDate});
            this.dgTotal.DataMember = "LogTotalEntries";
            this.dgTotal.Location = new System.Drawing.Point(18, 331);
            this.dgTotal.MultiSelect = false;
            this.dgTotal.Name = "dgTotal";
            this.dgTotal.RowHeadersVisible = false;
            this.dgTotal.Size = new System.Drawing.Size(548, 239);
            this.dgTotal.TabIndex = 7;
            this.dgTotal.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgTotal_CellEndEdit);
            this.dgTotal.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgTotal_CellFormatting);
            // 
            // TotalID
            // 
            this.TotalID.HeaderText = "ID";
            this.TotalID.Name = "TotalID";
            this.TotalID.ReadOnly = true;
            this.TotalID.Visible = false;
            // 
            // TotalProgram
            // 
            this.TotalProgram.HeaderText = "Progarm";
            this.TotalProgram.Name = "TotalProgram";
            this.TotalProgram.ReadOnly = true;
            this.TotalProgram.Width = 135;
            // 
            // TotalHours
            // 
            dataGridViewCellStyle4.Format = "N1";
            this.TotalHours.DefaultCellStyle = dataGridViewCellStyle4;
            this.TotalHours.HeaderText = "Hours";
            this.TotalHours.Name = "TotalHours";
            this.TotalHours.ReadOnly = true;
            this.TotalHours.Width = 50;
            // 
            // TotalComments
            // 
            this.TotalComments.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TotalComments.HeaderText = "Comments";
            this.TotalComments.Name = "TotalComments";
            // 
            // TotalDate
            // 
            this.TotalDate.HeaderText = "Date";
            this.TotalDate.Name = "TotalDate";
            this.TotalDate.ReadOnly = true;
            this.TotalDate.Visible = false;
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            this.iDDataGridViewTextBoxColumn.Visible = false;
            // 
            // DatePicker
            // 
            this.DatePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatePicker.Location = new System.Drawing.Point(16, 157);
            this.DatePicker.MaxSelectionCount = 1;
            this.DatePicker.Name = "DatePicker";
            this.DatePicker.TabIndex = 11;
            this.DatePicker.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.DatePicker_DateChanged);
            // 
            // HelpMenu_UpdateOnStart
            // 
            this.HelpMenu_UpdateOnStart.Checked = global::Time_Keeper.Properties.Settings.Default.AutoCheckUpdate;
            this.HelpMenu_UpdateOnStart.Name = "HelpMenu_UpdateOnStart";
            this.HelpMenu_UpdateOnStart.Size = new System.Drawing.Size(179, 22);
            this.HelpMenu_UpdateOnStart.Text = "Update On Start";
            this.HelpMenu_UpdateOnStart.Click += new System.EventHandler(this.MenuUpdateOnStart_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 585);
            this.Controls.Add(this.DatePicker);
            this.Controls.Add(this.dgTotal);
            this.Controls.Add(this.dgLog);
            this.Controls.Add(this.btnOpenDeltek);
            this.Controls.Add(this.tbTotalTime);
            this.Controls.Add(this.btnClockOut);
            this.Controls.Add(this.btnClockIn);
            this.Controls.Add(this.cmbPrograms);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Time Keeper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTotal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem FileMenu;
        private System.Windows.Forms.ToolStripMenuItem FileMenu_Edit;
        private System.Windows.Forms.ToolStripMenuItem FileMenu_Reset;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem FileMenu_Quit;
        private System.Windows.Forms.ToolStripMenuItem HelpMenu;
        private System.Windows.Forms.ToolStripMenuItem HelpMenu_Update;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem HelpMenu_About;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbPrograms;
        private System.Windows.Forms.Button btnClockIn;
        private System.Windows.Forms.Button btnClockOut;
        private System.Windows.Forms.TextBox tbTotalTime;
        private System.Windows.Forms.Button btnOpenDeltek;
        private System.Windows.Forms.DataGridView dgLog;
        private System.Windows.Forms.DataGridView dgTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem HelpMenu_UpdateOnStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Program;
        private System.Windows.Forms.DataGridViewTextBoxColumn In;
        private System.Windows.Forms.DataGridViewTextBoxColumn Out;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hours;
        private System.Windows.Forms.DataGridViewTextBoxColumn LogDate;
        private System.Windows.Forms.DataGridViewImageColumn LogRowDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalProgram;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalHours;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalComments;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalDate;
        private System.Windows.Forms.MonthCalendar DatePicker;
    }
}

