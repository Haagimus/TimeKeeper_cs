using log4net;
using System;
using System.Windows.Forms;
using Time_Keeper.Controllers;

namespace Time_Keeper
{
    internal static class Program
    {
        public static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            MainForm mainForm = new MainForm();
            MainFormController editController = new MainFormController();

            _logger.Debug("Application starting");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainForm.ShowDialog();
        }
    }
}
