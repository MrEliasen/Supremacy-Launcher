using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
//using System.Drawing;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Net;
using System.IO;
//using Microsoft.Win32;
//using System.Web;
using System.Diagnostics;
using System.Configuration;
using Newtonsoft.Json;
using System.Security.Cryptography;


namespace Supremacy_Launcher_v2
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Required for draging borderless form around.
        /// </summary>
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        //----------------------------------------------

        // make threadsafe calls
        private delegate void updateStatusBarDelegate(string message, int progPercent);
        private delegate void unlockUIElementsDelegate(bool updatesAvailable);
        private delegate void patchingCompleteDelegate();
        private delegate void downloadFailedDelegate(string filePath);

        /// <summary>
        /// the url to query for the latest version and download url. Should return a Semantic version number
        /// </summary>
        //private string _checkURL = "http://www.your-community-website.com/launcher/version.txt";

        /// <summary>
        /// the url to query for patch files to download based on the current version of the mod which is installed.
        /// This json file contain the array of all files and their hash for comparison.
        /// </summary>
        private string _patchURL = "http://www.your-community-website.com/launcher/patchinfo.json";

        /// <summary>
        /// The url from where they download the files.
        /// The base url from where files will be downloaded (relative path to the files in the patchinfo.json)
        /// </summary>
        private string _patchDownloadURL = "http://www.your-community-website.com/launcher/downloads/";

        /// <summary>
        /// will hold the files which the launhcer will need to downloadn
        /// </summary>
        List<string> _updatedFiles = new List<string>();

        /// <summary>
        /// The path to the ARMA 3 installation directory
        /// </summary>
        private string _arma3Path = "";

        /// <summary>
        /// This is the name of the mod directory which the launcher will check against. MUST BEGIN WITH @
        /// </summary>
        private string _modDirName = "@supremacy";

        /// <summary>
        /// The hostname or IP address of your arma 3 server
        /// </summary>
        private string _gameServerAddress = "server hostname or IP here";

        /// <summary>
        /// The port of your arma 3 server
        /// </summary>
        private string _gameServerPort = "2302";

        /// <summary>
        /// The latest version of the mod available.
        /// </summary>
        //private string _latestVersion = "Unknown";

        /// <summary>
        /// The latest version of the launcher
        /// </summary>
        protected string _latestLauncherVersion = "Unknown";

        /// <summary>
        /// Whether the the mod is already installed
        /// </summary>
        protected bool _isInstalled = false;

        /// <summary>
        /// hold the downloader client object.
        /// </summary>
        protected WebClient _Client;

        /// <summary>
        /// Keeps track of whether we are downloading or not
        /// </summary>
        protected bool _downloading = false;

        /// <summary>
        /// Threads
        /// </summary>
        Thread backgroundThread;
        Thread initDownload;
        Thread checkStatus;
        Thread patchFiles;
        protected bool threadsStarted = false;
        protected bool initThreadsStarted = false;

        public MainForm()
        {
            InitializeComponent();
            
            // check if the arma dir is setup, load the values from registry and basically just figure out what we need the user to do.
            // we do this in a new thread, since we do not want this to block the rest of the application.
            backgroundThread = new Thread(new ThreadStart(initCheck));
            backgroundThread.Start();
        }

        #region Thread functions;

        /// <summary>
        /// Thread for checking if arma is installed, the mod is installed, etc etc.
        /// </summary>
        private void initCheck()
        {
            // update the status bar
            updateStatusBar("Loading settings..", 15);

            // load the registry values.
            _arma3Path = getRegistryValue("arma3path");

            // update the status bar
            //updateStatusBar("Fetching version data..", 35);

            // Get latest version available.
            //_latestVersion = new WebClient().DownloadString(_checkURL).ToString();

            // update the status bar
            updateStatusBar("Validating ARMA 3 path..", 85);

            // Check if the mod is installed
            if (_arma3Path != "")
            {
                if (Directory.Exists(_arma3Path + "\\" + _modDirName))
                {
                    btnDownloadInstall.BackgroundImage = Properties.Resources.btnUpdatemod_disabled;
                    _isInstalled = true;
                }

                initThreadsStarted = true;

                // check if the files are up to date
                initDownload = new Thread(new ThreadStart(initPatchCheck));
                initDownload.Start();
            }
            else
            {
                unlockUIElements(true);
            }
        }

        #endregion;

        private void patchingComplete()
        {
            // wait for the downloads to complete
            while (!_isInstalled || _updatedFiles.Count > 0)
            {
                Thread.Sleep(500);
            }

            if (this.InvokeRequired)
            {
                patchingCompleteDelegate del = new patchingCompleteDelegate(patchingComplete);
                this.Invoke(del, new object[] { });
            }
            else
            {
                updateStatusBar("Installation Completed!", 100);

                // show play button and hide the others!
                btnDownloadInstall.BackgroundImage = Properties.Resources.btnUpdatemod_disabled;
                btnPlay.BackgroundImage = Properties.Resources.launcher_btnPlay;
                btnPlay.Enabled = true;
                btnPlay.Cursor = Cursors.Hand;
                btnReset.Enabled = true;
            }
        }


        /// <summary>
        /// Updates the status bar progress bar and label
        /// </summary>
        /// <param name="message"></param>
        /// <param name="progress"></param>
        private void updateStatusBar(string message = "", int progPercent = -1)
        {
            if (this.InvokeRequired)
            {
                updateStatusBarDelegate del = new updateStatusBarDelegate(updateStatusBar);
                this.Invoke(del, new object[] { message, progPercent });
            }
            else
            {
                this.lblStatus.Text = message.ToString();
               
                // only update the progress if its >= 0 so we can update the status text without having to change the progress bar 
                if (progPercent >= 0)
                {
                    statusProgressBar.Value = (int)progPercent;
                }
            }
        }

        /// <summary>
        /// Unlock elements of the UI based on initialisation script turned back with.
        /// </summary>
        private void unlockUIElements(bool updatesAvailable)
        {
            if (this.InvokeRequired)
            {
                unlockUIElementsDelegate del = new unlockUIElementsDelegate(unlockUIElements);
                this.Invoke(del, new object[] { updatesAvailable });
            }
            else
            {
                // unlock the general UI elements
                if (!_isInstalled || updatesAvailable)
                {
                    if (_arma3Path == "")
                    {
                        // update the status bar
                        updateStatusBar("Arma 3 path not set yet.", 0);
                    }
                    else
                    {
                        if (_isInstalled && updatesAvailable)
                        {
                            // update the status bar
                            btnDownloadInstall.BackgroundImage = Properties.Resources.btnUpdatemod;
                            updateStatusBar(_updatedFiles.Count + " file needs to be updated.", 0);
                            btnReset.Visible = true;
                        }
                        else
                        {
                            // update the status bar
                            updateStatusBar("Supremacy Mod not installed.", 0);
                        }
                    }

                    btnDownloadInstall.Enabled = true;
                    btnDownloadInstall.Cursor = Cursors.Hand;
                }
                else
                {
                    // update the status bar
                    updateStatusBar(_modDirName + " is to date!", 100);
                    btnPlay.BackgroundImage = Properties.Resources.launcher_btnPlay;
                    btnPlay.Enabled = true;
                    btnPlay.Cursor = Cursors.Hand;
                    btnDownloadInstall.BackgroundImage = Properties.Resources.btnUpdatemod_disabled;
                    btnDownloadInstall.Enabled = false;
                    btnDownloadInstall.Cursor = Cursors.Default;
                    btnReset.Visible = true;
                }
            }
        }

        /// <summary>
        /// Get a value from the registration database.
        /// </summary>
        /// <param name="name">the name of the key we are getting</param>
        /// <returns></returns>
        private string getRegistryValue(string name)
        {
            string result = "";

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                result = appSettings[name] ?? "";
            }
            catch (ConfigurationErrorsException){}

            return result;
        }

        /// <summary>
        /// Set a value in the registration database
        /// </summary>
        /// <param name="name">The key name</param>
        /// <param name="value">The value of the key</param>
        private void setRegistryValue(string name, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[name] == null)
                {
                    settings.Add(name, value);
                }
                else
                {
                    settings[name].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException) {}
        }

        /// <summary>
        /// Remove a value from the registration database
        /// </summary>
        /// <param name="name">the name of the key we are deleting</param>
        private void removeRegistryValue(string name)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configFile.AppSettings.Settings.Remove(name);
                configFile.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException) {}
        }

        /// <summary>
        /// Exits the application when the button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            backgroundThread.Abort();
            
            // Kill the threads
            if (initThreadsStarted)
            {
                initDownload.Abort();
            }

            // Kill the threads
            if (threadsStarted)
            {
                patchFiles.Abort();
                checkStatus.Abort();
            }

            // Kill the download if running
            if (this._Client != null)
            {
                this._Client.CancelAsync();
            }

            // shutdown application
            this.Close();
        }

        /// <summary>
        /// Enables draggings of the borderless form by the nagivation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_dragarea_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void pictureBox_logo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void downloadFile()
        {
            string filepath = _updatedFiles[0];

            try
            {
                // delete the file we are trying to download.. in case the download cannot overwrite.
                if (File.Exists(_arma3Path + filepath.Replace("/", "\\")))
                {
                    File.Delete(_arma3Path + filepath.Replace("/", "\\"));
                }

                _Client = new WebClient();
                Uri uri = new Uri(_patchDownloadURL + filepath);

                // setup the callback for when the download is complete
                _Client.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadCompleteCallback);

                // Specify a progress notification handler.
                _Client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressUpdateCallback);

                // begin the file download and save it locally.
                _Client.DownloadFileAsync(uri, _arma3Path + filepath.Replace("/", "\\"));

            } catch(Exception err) {
                downloadFailed(filepath.Replace("/", "\\"));
            };
        }

        private void downloadFailed(string filePath)
        {
            if (this.InvokeRequired)
            {
                downloadFailedDelegate del = new downloadFailedDelegate(downloadFailed);
                this.Invoke(del, new object[] { filePath });
            }
            else
            {
                MessageBox.Show("The File: \"" + _arma3Path + filePath + "\" could not get updated, file already in use. Try delete this file manually and run the updater again.");
                btnDownloadInstall.Enabled = true;
                btnDownloadInstall.Cursor = Cursors.Hand;
                btnReset.Enabled = true;
            }
        }

        private void DownloadCompleteCallback(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                return;
            }

            _updatedFiles.RemoveAt(0);
            if (_updatedFiles.Count > 0)
            {
                downloadFile();
            }
            else
            {
                _isInstalled = true;
            }
        }

        private void DownloadProgressUpdateCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            // Displays the operation identifier, and the transfer progress.
            updateStatusBar(
                string.Format("Downloading.. {0} of {1} KB ({2} MB) - {3} file(s) remaining",
               ((e.BytesReceived) / 1024),
               ((e.TotalBytesToReceive) / 1024),
               ((e.TotalBytesToReceive) / 1024)/1024,
               _updatedFiles.Count),
               (int)e.ProgressPercentage
            );
        }

        private void initPatchCheck()
        {
            if (Directory.Exists(_arma3Path + "\\" + _modDirName))
            {
                _isInstalled = true;
            }

            // update the status text
            updateStatusBar("Checking files for updates..", 0);

            // check if there are any patches available. Will return the download url.
            string jsonData = new WebClient().DownloadString(_patchURL).ToString();

            if (jsonData != "")
            {
                try
                {
                    dynamic fileData = JsonConvert.DeserializeObject(jsonData);
                    int filesTotal = fileData.Count;
                    int i = 1;

                    List<string> existingFiles = new List<string>();
                    List<string> patchingFiles = new List<string>();
                    
                    if (Directory.Exists(_arma3Path + "\\y" + _modDirName))
                    {
                        foreach (var item in Directory.GetFiles(_arma3Path + "\\" + _modDirName, "*", SearchOption.AllDirectories))
                        {
                            existingFiles.Add((string)item);
                        }
                    }

                    foreach (var item in fileData)
                    {
                        string filehash = "";
                        string winPath = ((string)item.path).Replace("/", "\\");
                        string directoryPath = winPath.Replace((string)item.name, "");

                        if (winPath == "")
                        {
                            continue;
                        }

                        patchingFiles.Add(_arma3Path + winPath);

                        if (_arma3Path != "" && !Directory.Exists(_arma3Path + directoryPath))
                        {
                            Directory.CreateDirectory(_arma3Path + directoryPath);
                        }

                        if (File.Exists(_arma3Path + winPath))
                        {
                            using (FileStream stream = File.OpenRead(_arma3Path + winPath))
                            {
                                SHA1Managed sha = new SHA1Managed();
                                byte[] hash = sha.ComputeHash(stream);
                                filehash = BitConverter.ToString(hash).Replace("-", String.Empty).ToLower();
                            }
                        }

                        if ((string)filehash.ToLower() != ((string)item.sha1).ToLower())
                        {
                            _updatedFiles.Add((string)item.path);
                        }

                        updateStatusBar("Checking files for updates..", (100 / filesTotal) * i);
                        i++;
                    }

                    string[] fileArray = existingFiles.ToArray();
                    string[] patchArray = patchingFiles.ToArray();

                    if (existingFiles.Count() > 0)
                    {
                        foreach (string file in fileArray)
                        {
                            if (!patchArray.Contains(file))
                            {
                                File.Delete(file);
                            }
                        }
                    }
                }
                catch (Exception err) { };
            }

            // Unlock the UI elements.
            unlockUIElements((_updatedFiles.Count > 0 ? true : false));
        }

        private void selectArmaDir()
        {
            // Reset the status bar
            updateStatusBar("", 0);

            if (_arma3Path == "")
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "Please select the directory containing your ARMA 3 installation (arma3.exe file).";

                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK)
                {
                    if (File.Exists(fbd.SelectedPath + "\\arma3.exe") && _arma3Path != fbd.SelectedPath)
                    {
                        _updatedFiles = new List<string>();
                        _arma3Path = fbd.SelectedPath;
                        setRegistryValue("arma3path", fbd.SelectedPath);
                        btnDownloadInstall.Visible = true;

                        //  run the init check again
                        backgroundThread = new Thread(new ThreadStart(initPatchCheck));
                        backgroundThread.Start();
                    }
                    else
                    {
                        MessageBox.Show("That folder did not appear to the folder where you have ARMA 3 installed.");
                    }
                }
            }
        }

        private void btnDownloadInstall_Click(object sender, EventArgs e)
        {
            if (_arma3Path == "")
            {
                selectArmaDir();
            }
            else
            {
                btnDownloadInstall.Enabled = false;
                btnReset.Enabled = false;

                checkStatus = new Thread(new ThreadStart(patchingComplete));
                checkStatus.Start();

                patchFiles = new Thread(new ThreadStart(downloadFile));
                patchFiles.Start();
                
                // let the exit function know the threads have started
                threadsStarted = true;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Please select the directory containing your ARMA 3 installation (arma3.exe file).";

            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (File.Exists(fbd.SelectedPath + "\\arma3.exe"))
                {
                    if (_arma3Path == fbd.SelectedPath)
                    {
                        return; 
                    }

                    _updatedFiles = new List<string>();
                    _arma3Path = fbd.SelectedPath;
                    setRegistryValue("arma3path", fbd.SelectedPath);

                    //  run the init check again
                    backgroundThread = new Thread(new ThreadStart(initPatchCheck));
                    backgroundThread.Start();
                }
                else
                {
                    MessageBox.Show("That folder did not appear to the folder where you have ARMA 3 installed.");
                }
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = _arma3Path + "\\arma3.exe";
            p.StartInfo.Arguments = "\"-mod=" + _modDirName + "\" -nosplash -noLogs -world=empty -connect=" + _gameServerAddress + " - port=" + _gameServerPort;
            p.Start();
        }
    }
}