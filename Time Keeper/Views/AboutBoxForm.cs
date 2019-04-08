using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Time_Keeper.Interfaces;
using Time_Keeper.Controllers;

namespace Time_Keeper
{
    partial class AboutBoxForm : Form, IAboutView
    {
        public AboutBoxForm()
        {
            InitializeComponent();
        }

        AboutController _controller;

        public void SetController(AboutController controller)
        {
            _controller = controller;
        }

        public void SetTitle() { Text = _controller.AssemblyTitle; }
        public void SetDescription() { textBoxDescription.Text = _controller.AssemblyDescription; }
        public void SetVersion() { labelVersion.Text = _controller.AssemblyVersion; }
        public void SetProduct() { labelProductName.Text = _controller.AssemblyProduct; }
        public void SetCopyright() { labelCopyright.Text = _controller.AssemblyCopyright; }
        public void SetCompany() { labelCompanyName.Text = _controller.AssemblyCompany; }


        public string AssemblyTitle
        {
            get { return Text; }
            set { Text = value; }
        }

        public string AssemblyVersion
        {
            get { return labelVersion.Text; }
            set { labelVersion.Text = value; }
        }

        public string AssemblyProduct
        {
            get { return labelProductName.Text; }
            set { labelProductName.Text = value; }
        }

        public string AssemblyCopyright
        {
            get { return labelCopyright.Text; }
            set { labelCopyright.Text = value; }
        }

        public string AssemblyCompany
        {
            get { return labelCompanyName.Text; }
            set { labelCompanyName.Text = value; }
        }

        public string AssemblyDescription
        {
            get { return textBoxDescription.Text; }
            set { textBoxDescription.Text = value; }
        }

        public void linkEmail_LinkClicked(object sender, EventArgs e)
        {
            _controller.SendEmail();
        }
    }
}
