using log4net;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Time_Keeper
{
    public class NetworkOperations
    {
        public static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private bool NetworkAvail = false;

        /// <summary>
        /// Checks the network for availability of the X drive.
        /// </summary>
        public void CheckNetworkPath()
        {
            Task task = new Task(() => Directory.Exists(Properties.Settings.Default.publishPath));
            task.Start();
            NetworkAvail = task.Wait(5000) == true ? true : false;
            if (!NetworkAvail)
            {
                MessageBox.Show(new Form() { TopMost = true },
                    text: "X drive cannot be reached, check network connection",
                    caption: "Network Unreachable",
                    icon: MessageBoxIcon.Error,
                    buttons: MessageBoxButtons.OK);
                _logger.Error("X Drive could not be reached, internal network connection probably missing.");
            }
        }

        /// <summary>
        /// Deletes the 1.x application version save file.
        /// </summary>
        public void DeleteOldVersion()
        {
            if (File.Exists(Environment.CurrentDirectory + "\\OldTimeKeeper.exe"))
            {
                _logger.Info("Old version of application found, deleting it now.");
                File.Delete(Environment.CurrentDirectory + "\\OldTimeKeeper.exe");
            }
        }

        /// <summary>
        /// Checks the server to see if the available version is newer than the one running locally.
        /// </summary>
        /// <param name="_exeVersion">The local version of the application to run the compare against</param>
        public void UpdateCheck(Version _exeVersion)
        {
            string _filename = Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location);
            if(_filename.Contains("TimeKeeper"))
            {
                File.Move(Assembly.GetEntryAssembly().Location, string.Concat(Environment.CurrentDirectory, "\\Time Keeper.exe"));
            }

            CheckNetworkPath();

            string serverLoc = Properties.Settings.Default.publishPath;
            string _serverApp;
            string _serverConfig;

            if (!NetworkAvail)
            {
                return;
            }

            // Set the correct path for the server file
            _serverApp = File.Exists(Path.Combine(serverLoc, "Time Keeper.exe")) ? string.Concat(serverLoc, "Time Keeper.exe") : string.Concat(serverLoc, "TimeKeeper.exe");
            _serverConfig = File.Exists(Path.Combine(serverLoc, "Time Keeper.exe.config")) ? string.Concat(serverLoc, "Time Keeper.exe.config") : string.Empty;

            if (!File.Exists(Environment.CurrentDirectory + "\\Time Keeper.exe.config"))
            {
                _logger.Info("Config file missing, copying from server.");
                File.Copy(_serverConfig, Environment.CurrentDirectory + "\\Time Keeper.exe.config", true);
            }

            FileVersionInfo serverVersion = FileVersionInfo.GetVersionInfo(_serverApp);

            Version client = _exeVersion;
            Version server = new Version(string.Format("{0}.{1}.{2}.{3}", serverVersion.FileMajorPart, serverVersion.FileMinorPart, serverVersion.FileBuildPart, serverVersion.FilePrivatePart));

            // The available version is newer than the local version, ask the user if they want to update
            if (client < server)
            {
                DialogResult result = MessageBox.Show(new Form() { TopMost = true },
                    caption: "Update Available",
                    text: string.Format("Current Version: {0}\nAvailable Version: {1}\n\nDownload Now?", client, server),
                    buttons: MessageBoxButtons.YesNo,
                    icon: MessageBoxIcon.Question);
                // The user selected yes so we need to copy the newer version to the user selected directory
                if (result == DialogResult.Yes)
                {
                    FolderBrowserDialog saveLoc = new FolderBrowserDialog
                    {
                        SelectedPath = Environment.CurrentDirectory
                    };

                    if (saveLoc.ShowDialog() == DialogResult.OK)
                    {
                        string _folderPath = saveLoc.SelectedPath;
                        string _localFile;

                        // Set the correct path for the server file
                        if (File.Exists(Path.Combine(serverLoc, "Time Keeper.exe")))
                        {
                            // This is the .net version of Time Keeper
                            _serverApp = string.Concat(serverLoc, "Time Keeper.exe");
                        }
                        else
                        {
                            // This is the old Python version of Time Keeper
                            _serverApp = string.Concat(serverLoc, "TimeKeeper.exe");
                        }

                        try
                        {
                            // Set the correct path for the local file
                            if (File.Exists(Path.Combine(_folderPath, "Time Keeper.exe")))
                            {
                                // This is the .net version of Time Keeper
                                _localFile = string.Concat(_folderPath, "\\Time Keeper.exe");
                            }
                            else
                            {
                                // This is the Python version of Time Keeper
                                _localFile = string.Concat(_folderPath, "\\TimeKeeper.exe");
                            }
                            // Rename the old time keeper so we can delete it after
                            File.Move(_localFile, string.Concat(_folderPath, "\\OldTimeKeeper.exe"));
                            // Copy the server version to the selected folder path
                            File.Copy(_serverApp, string.Concat(_folderPath, "\\Time Keeper.exe"));
                        }
                        catch (FileNotFoundException)
                        {
                            File.Copy(_serverApp, string.Concat(_folderPath, "\\Time Keeper.exe"));
                        }
                        Process.Start(string.Concat(_folderPath, "\\Time Keeper.exe"));
                        Environment.Exit(0);
                    }
                    else
                    {
                        return; // Download cancelled
                    }
                }
            }
            else if (client >= server)
            {
                MessageBox.Show(new Form() { TopMost = true },
                    caption: "No Update",
                    text: "No updates are available at this time.",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Information);
                return; // The server version is not newer than what you are using
            }
        }
    }
}
