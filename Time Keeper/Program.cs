using log4net;
using System;
using System.Data.Entity;
using System.Threading;
using System.Windows.Forms;
using Time_Keeper.Controllers;

namespace Time_Keeper
{
    internal static class Program
    {
        public static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static Mutex mutex = null;
        
        /// <summary>
        /// The main Entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

            Database.SetInitializer(new CreateDatabaseIfNotExists<TimeKeeperDBEntities>());

            const string appName = "Time Keeper";
            bool createdNew;

            mutex = new Mutex(true, appName, out createdNew);

            foreach (string arg in args)
            {
                if (arg == "-allowMulti") createdNew = true;
            }

            if (!createdNew)
            {
                MessageBox.Show(text: "An instance of the Time Keeper application is already running.",
                    caption: "Time Keeper already running",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Information);
                return; // App is already running do not relaunch
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm mainForm = new MainForm();
            MainFormController mainController = new MainFormController(mainForm);

            _logger.Debug("Application starting");

            mainForm.ShowDialog();
        }
    }
}
