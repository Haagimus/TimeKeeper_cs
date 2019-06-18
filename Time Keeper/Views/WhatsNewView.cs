using System;
using System.Reflection;
using System.Windows.Forms;
using Time_Keeper.Controllers;
using Time_Keeper.Interfaces;

namespace Time_Keeper
{
    partial class WhatsNewForm : Form, IWhatsNew
    {
        public WhatsNewForm()
        {
            InitializeComponent();
        }

        WhatsNewController _controller;

        public void SetController(WhatsNewController controller)
        {
            _controller = controller;
        }

        public void SetVersion() { lblVersion.Text = _controller.AssemblyVersion; }
        public void SetChangeLog() { tbChangeLog.Text = _controller.ChangeLog; }
        public void SetCheckbox() { cbWhatsNew.Checked = _controller.CheckBoxState(); }

        public CheckBox CheckBoxShowAtStart
        {
            get { return cbWhatsNew; }
            set { cbWhatsNew = value; }
        }

        public string AssemblyVersion
        {
            get { return _controller.AssemblyVersion; }
            set { ChangeLog = value; }
        }

        public string ChangeLog
        {
            get { return tbChangeLog.Text; }
            set { tbChangeLog.Text = value; }
        }

        public void CheckedChanged(object sender, EventArgs e)
        {
            _controller.CheckedChanged(sender, e);
        }

        public void CheckedChanged()
        {
            throw new NotImplementedException();
        }
    }
}
