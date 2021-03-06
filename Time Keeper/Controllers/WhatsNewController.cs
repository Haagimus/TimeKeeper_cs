﻿using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Time_Keeper.Interfaces;

namespace Time_Keeper.Controllers
{
    class WhatsNewController
    {
        IWhatsNew _view;
        public WhatsNewController(IWhatsNew view)
        {
            _view = view;
            view.SetController(this);
        }

        public void LoadView()
        {
            _view.SetVersion();
            _view.SetChangeLog();
            _view.SetCheckbox();
        }

        public string AssemblyVersion
        {
            get
            {
                var app = Assembly.GetExecutingAssembly().GetName().Version;
                return string.Format("v{0}", app.Major.ToString() + "." + app.Minor.ToString() + "." + app.Build.ToString());
            }
        }

        public string ChangeLog
        {
            get { return Properties.Resources.Change_Log; } 
        }

        public bool CheckBoxState()
        {
            return Properties.Settings.Default.WhatsNew;
        }

        public void CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.WhatsNew = _view.CheckBoxShowAtStart.Checked;
            Properties.Settings.Default.Save();
        }
    }
}
