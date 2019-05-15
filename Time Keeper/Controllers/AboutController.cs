using System.Reflection;
using Time_Keeper.Interfaces;

namespace Time_Keeper.Controllers
{
    public class AboutController
    {
        IAboutView _view;

        public AboutController(IAboutView view)
        {
            _view = view;
            view.SetController(this);
        }

        public void LoadView()
        {
            _view.SetTitle();
            _view.SetVersion();
            _view.SetProduct();
            _view.SetDescription();
            _view.SetCompany();
            _view.SetCopyright();
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return string.Format("About {0}", titleAttribute.Title);
                    }
                }
                return string.Format("About {0}", System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase));
            }
        }

        public string AssemblyVersion
        {
            get
            {
                var app = Assembly.GetExecutingAssembly().GetName().Version;
                return string.Format("Version {0}", app.Major.ToString() + "." + app.Minor.ToString() + "." + app.Build.ToString());
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        public void SendEmail()
        {
            System.Diagnostics.Process.Start("mailto:Gary.Haag@L3T.com");
        }
    }
}
