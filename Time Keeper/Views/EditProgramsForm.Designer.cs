namespace Time_Keeper
{
    partial class EditProgramsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditProgramsForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbPrograms = new System.Windows.Forms.ListBox();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnDeleteProgram = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbProgramName = new System.Windows.Forms.TextBox();
            this.tbProgramCode = new System.Windows.Forms.TextBox();
            this.tbProgramNotes = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lbPrograms, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnMoveUp, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnMoveDown, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.btnDeleteProgram, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label1, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.label2, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbProgramName, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbProgramCode, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbProgramNotes, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.btnSubmit, 3, 7);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(459, 226);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // lbPrograms
            // 
            this.lbPrograms.AllowDrop = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lbPrograms, 2);
            this.lbPrograms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPrograms.FormattingEnabled = true;
            this.lbPrograms.Location = new System.Drawing.Point(3, 3);
            this.lbPrograms.Name = "lbPrograms";
            this.tableLayoutPanel1.SetRowSpan(this.lbPrograms, 7);
            this.lbPrograms.Size = new System.Drawing.Size(144, 190);
            this.lbPrograms.TabIndex = 1;
            this.lbPrograms.SelectedValueChanged += new System.EventHandler(this.SelectedValueChange);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.AutoSize = true;
            this.btnMoveUp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMoveUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMoveUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveUp.Image")));
            this.btnMoveUp.Location = new System.Drawing.Point(153, 3);
            this.btnMoveUp.Name = "btnMoveUp";
            this.tableLayoutPanel1.SetRowSpan(this.btnMoveUp, 2);
            this.btnMoveUp.Size = new System.Drawing.Size(26, 50);
            this.btnMoveUp.TabIndex = 3;
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.PromoteProgram);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.AutoSize = true;
            this.btnMoveDown.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMoveDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMoveDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveDown.Image")));
            this.btnMoveDown.Location = new System.Drawing.Point(153, 143);
            this.btnMoveDown.Name = "btnMoveDown";
            this.tableLayoutPanel1.SetRowSpan(this.btnMoveDown, 2);
            this.btnMoveDown.Size = new System.Drawing.Size(26, 50);
            this.btnMoveDown.TabIndex = 4;
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.DemoteProgram);
            // 
            // btnDeleteProgram
            // 
            this.btnDeleteProgram.AutoSize = true;
            this.btnDeleteProgram.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDeleteProgram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDeleteProgram.Enabled = false;
            this.btnDeleteProgram.Location = new System.Drawing.Point(3, 199);
            this.btnDeleteProgram.Name = "btnDeleteProgram";
            this.btnDeleteProgram.Size = new System.Drawing.Size(69, 24);
            this.btnDeleteProgram.TabIndex = 5;
            this.btnDeleteProgram.Text = "Delete";
            this.btnDeleteProgram.UseVisualStyleBackColor = true;
            this.btnDeleteProgram.Click += new System.EventHandler(this.DeleteProgram);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(185, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(271, 28);
            this.label1.TabIndex = 6;
            this.label1.Text = "Program Name (Esc clears selection)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(185, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(271, 28);
            this.label3.TabIndex = 9;
            this.label3.Text = "Notes";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(185, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(271, 28);
            this.label2.TabIndex = 7;
            this.label2.Text = "Charge Code";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbProgramName
            // 
            this.tbProgramName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbProgramName.Location = new System.Drawing.Point(185, 31);
            this.tbProgramName.Name = "tbProgramName";
            this.tbProgramName.Size = new System.Drawing.Size(271, 20);
            this.tbProgramName.TabIndex = 0;
            this.tbProgramName.TextChanged += new System.EventHandler(this.ButtonStates);
            // 
            // tbProgramCode
            // 
            this.tbProgramCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbProgramCode.Location = new System.Drawing.Point(185, 87);
            this.tbProgramCode.Name = "tbProgramCode";
            this.tbProgramCode.Size = new System.Drawing.Size(271, 20);
            this.tbProgramCode.TabIndex = 8;
            this.tbProgramCode.TextChanged += new System.EventHandler(this.ButtonStates);
            // 
            // tbProgramNotes
            // 
            this.tbProgramNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbProgramNotes.Location = new System.Drawing.Point(185, 143);
            this.tbProgramNotes.Multiline = true;
            this.tbProgramNotes.Name = "tbProgramNotes";
            this.tableLayoutPanel1.SetRowSpan(this.tbProgramNotes, 2);
            this.tbProgramNotes.Size = new System.Drawing.Size(271, 50);
            this.tbProgramNotes.TabIndex = 10;
            this.tbProgramNotes.TextChanged += new System.EventHandler(this.ButtonStates);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSubmit.Location = new System.Drawing.Point(386, 199);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(70, 24);
            this.btnSubmit.TabIndex = 11;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.SubmitProgram);
            // 
            // EditProgramsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(459, 226);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditProgramsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Programs List";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Hotkeys);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox tbProgramName;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnDeleteProgram;
        private System.Windows.Forms.ListBox lbPrograms;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbProgramCode;
        private System.Windows.Forms.TextBox tbProgramNotes;
        private System.Windows.Forms.Button btnSubmit;
    }
}