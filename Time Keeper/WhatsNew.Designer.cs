namespace Time_Keeper
{
    partial class WhatsNew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WhatsNew));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbWhatsNew = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.MaximumSize = new System.Drawing.Size(584, 0);
            this.label1.MinimumSize = new System.Drawing.Size(584, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(25);
            this.label1.Size = new System.Drawing.Size(584, 115);
            this.label1.TabIndex = 0;
            this.label1.Text = "v2.1.0";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 115);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.MaximumSize = new System.Drawing.Size(584, 0);
            this.label2.MinimumSize = new System.Drawing.Size(584, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(25);
            this.label2.Size = new System.Drawing.Size(584, 290);
            this.label2.TabIndex = 1;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // cbWhatsNew
            // 
            this.cbWhatsNew.AutoSize = true;
            this.cbWhatsNew.Checked = global::Time_Keeper.Properties.Settings.Default.WhatsNew;
            this.cbWhatsNew.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbWhatsNew.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Time_Keeper.Properties.Settings.Default, "WhatsNew", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbWhatsNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbWhatsNew.Location = new System.Drawing.Point(435, 414);
            this.cbWhatsNew.Name = "cbWhatsNew";
            this.cbWhatsNew.Size = new System.Drawing.Size(137, 24);
            this.cbWhatsNew.TabIndex = 2;
            this.cbWhatsNew.Text = "Show at launch";
            this.cbWhatsNew.UseVisualStyleBackColor = true;
            this.cbWhatsNew.CheckedChanged += new System.EventHandler(this.cbWhatsNew_CheckedChanged);
            // 
            // WhatsNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 450);
            this.Controls.Add(this.cbWhatsNew);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WhatsNew";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Time Keeper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbWhatsNew;
    }
}