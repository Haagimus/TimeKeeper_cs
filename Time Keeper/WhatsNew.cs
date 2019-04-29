using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Time_Keeper
{
    public partial class WhatsNew : Form
    {
        public WhatsNew()
        {
            InitializeComponent();
        }

        private void cbWhatsNew_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.WhatsNew = cbWhatsNew.Checked;
            Properties.Settings.Default.Save();
        }
    }
}
