﻿namespace Time_Keeper
{
    partial class WhatsNewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WhatsNewForm));
            this.lblVersion = new System.Windows.Forms.Label();
            this.cbWhatsNew = new System.Windows.Forms.CheckBox();
            this.tbChangeLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(0, 0);
            this.lblVersion.MaximumSize = new System.Drawing.Size(584, 0);
            this.lblVersion.MinimumSize = new System.Drawing.Size(584, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Padding = new System.Windows.Forms.Padding(25);
            this.lblVersion.Size = new System.Drawing.Size(584, 115);
            this.lblVersion.TabIndex = 0;
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbWhatsNew
            // 
            this.cbWhatsNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbWhatsNew.AutoSize = true;
            this.cbWhatsNew.Checked = global::Time_Keeper.Properties.Settings.Default.WhatsNew;
            this.cbWhatsNew.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbWhatsNew.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Time_Keeper.Properties.Settings.Default, "WhatsNew", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbWhatsNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbWhatsNew.Location = new System.Drawing.Point(447, 426);
            this.cbWhatsNew.Name = "cbWhatsNew";
            this.cbWhatsNew.Size = new System.Drawing.Size(137, 24);
            this.cbWhatsNew.TabIndex = 2;
            this.cbWhatsNew.Text = "Show at launch";
            this.cbWhatsNew.UseVisualStyleBackColor = true;
            this.cbWhatsNew.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // tbChangeLog
            // 
            this.tbChangeLog.BackColor = System.Drawing.SystemColors.Control;
            this.tbChangeLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbChangeLog.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbChangeLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbChangeLog.Location = new System.Drawing.Point(0, 115);
            this.tbChangeLog.Multiline = true;
            this.tbChangeLog.Name = "tbChangeLog";
            this.tbChangeLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbChangeLog.Size = new System.Drawing.Size(584, 305);
            this.tbChangeLog.TabIndex = 3;
            this.tbChangeLog.TabStop = false;
            // 
            // WhatsNewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(584, 450);
            this.Controls.Add(this.cbWhatsNew);
            this.Controls.Add(this.tbChangeLog);
            this.Controls.Add(this.lblVersion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WhatsNewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Time Keeper - What\'s New";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.CheckBox cbWhatsNew;
        private System.Windows.Forms.TextBox tbChangeLog;
    }
}