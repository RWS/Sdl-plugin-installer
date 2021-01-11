using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using CristiPotlog.Controls;
using NLog;
using Sdl.Community.Controls;
using Sdl.Community.SdlPluginInstaller.Model;
using Sdl.Community.SdlPluginInstaller.Properties;
using Sdl.Community.SdlPluginInstaller.Services;
using Sdl.Versioning;

namespace Sdl.Community.SdlPluginInstaller
{
    public partial class InstallerForm : Form
    {
        private readonly PluginPackageInfo _pluginPackageInfo;
        private readonly PluginVersioningService _VersionService;
        private readonly InstallService _installService;
        private readonly Logger _logger;
        private BackgroundWorker _bw;
        public InstallerForm()
        {
            InitializeComponent();
        }

        public InstallerForm(PluginPackageInfo pluginPackageInfo, Logger logger)
            : this()
        {
            _logger = logger;
            try
            {
                _pluginPackageInfo = pluginPackageInfo;
                _VersionService = new PluginVersioningService();
                _installService = new InstallService(_pluginPackageInfo);
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Error constructing installer window", null);
                throw;
            }
           
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.welcomePage.Description =
                string.Format(@"This wizard will guide you through the steps of installing {0} plugin",
                    _pluginPackageInfo.PluginName);


            textLicense.Rtf = Resources._160627__SDL_AppStore_End_User_Terms__Conditions_July_29_2016_Final;

            var installedStudioVersions = _VersionService.VersioningService.GetInstalledStudioVersions();
            if (installedStudioVersions.Count == 0)
            {
                this.finalPage.Description = string.Format("There are no versions of SDL Trados Studio installed.");
                this.pluginInstallWizzard.SelectedPage = this.finalPage;
            }

            studioVersionColumn.AspectGetter = delegate (object rowObject)
            {
                var studioVersion = (StudioVersion)rowObject;
                return string.Format("{0} - {1}", string.IsNullOrEmpty(studioVersion.Edition) ? studioVersion.PublicVersion : studioVersion.PublicVersion + " " + studioVersion.Edition, studioVersion.ExecutableVersion);
            };

            chkStudioVersions.SetObjects(installedStudioVersions);
            var versionsNotSupportedByPlugin = _VersionService.GetNotSupportedStudioVersions(_pluginPackageInfo);
            chkStudioVersions.DisableObjects(versionsNotSupportedByPlugin);

            chkStudioVersions.BuildList(true);
            appDataBtn.Checked = true;
            _bw = new BackgroundWorker { WorkerSupportsCancellation = true, WorkerReportsProgress = true };
            _bw.ProgressChanged += bw_ProgressChanged;
            _bw.DoWork += bw_DoWork;
            _bw.RunWorkerCompleted += bw_RunWorkerCompleted;

            //tooltips for radio buttons

            var appDataToolTip= new ToolTip();
            appDataToolTip.SetToolTip(appDataBtn,"This location should be used if you want the plugin to be available to all your domain machines.");

            var localAppDataToolTip = new ToolTip();
            localAppDataToolTip.SetToolTip(localAppDataBtn, "Install only for the current user");

            var commonToolTip = new ToolTip();
            commonToolTip.SetToolTip(commonAppDataBtn, "Install for all users of this local machine (requires Administrator rights)");
        }

        private void checkIAgree_CheckedChanged(object sender, EventArgs e)
        {
            this.pluginInstallWizzard.NextEnabled = this.checkIAgree.Checked;
        }

        private void pluginInstallWizzard_BeforeSwitchPages(object sender, Wizard.BeforeSwitchPagesEventArgs e)
        {
            // get wizard page already displayed
			WizardPage oldPage = this.pluginInstallWizzard.Pages[e.OldIndex];

			// check if we're going forward from options page
            if (oldPage == this.installedStudioVersionsPage && e.NewIndex > e.OldIndex)
            {
               var selectedStudioVersionsGeneric = chkStudioVersions.CheckedObjects;
                if (selectedStudioVersionsGeneric.Count == 0)
                {
                    MessageBox.Show(this,
                        Resources
                            .InstallerForm_pluginInstallWizzard_BeforeSwitchPages_Please_select_at_least_one_Studio_version_,
                        Resources.InstallerForm_pluginInstallWizzard_BeforeSwitchPages_Please_select_Studio_version, MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }


                _installService.StudioVersions = selectedStudioVersionsGeneric.OfType<StudioVersion>().ToList();
                if (!_installService.IsPluginInstalled()) return;
                var dialogResult = MessageBox.Show(this,
                    string.Format("Plugin {0} is already installed. Are you sure you want to continue?",
                        _pluginPackageInfo.PluginName),
                    Resources.InstallerForm_pluginInstallWizzard_BeforeSwitchPages_Please_select_Studio_version,
                    MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }

                var processes = _installService.GetStudioProcesses();
                if (processes.Count > 0)
                {
                    using (var processesForm = new Processes(processes))
                    {
                        processesForm.ShowDialog();
                    }
                    e.Cancel = true;
                    return;
                }
                _installService.RemoveInstalledPlugin();
            }
        }

        private void pluginInstallWizzard_AfterSwitchPages(object sender, Wizard.AfterSwitchPagesEventArgs e)
        {
            // get wizard page to be displayed
            WizardPage newPage = this.pluginInstallWizzard.Pages[e.NewIndex];

            // check if license page
            if (newPage == this.licensePage)
            {
                // sync next button's state with check box
                this.pluginInstallWizzard.NextEnabled = this.checkIAgree.Checked;
            }
            if (newPage == this.taskRunnerPage)
            {
                this.StartTask();
            }
        }

        private void StartTask()
        {
            this.progressLongTask.Value = this.progressLongTask.Minimum;
            this.pluginInstallWizzard.NextEnabled = false;
            this.pluginInstallWizzard.BackEnabled = false;

           
            _bw.RunWorkerAsync();
        }

        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressLongTask.Value = e.ProgressPercentage;
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.pluginInstallWizzard.NextEnabled = this.checkIAgree.Checked;
            this.pluginInstallWizzard.BackEnabled = true;
            if (e.Cancelled)
            {
                this.pluginInstallWizzard.Back();
            }
            else
            {
                this.pluginInstallWizzard.Next();
            }
           
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var backgroundWorker = sender as BackgroundWorker;
            var destinationFolder = Environment.SpecialFolder.ApplicationData;
         
             if (localAppDataBtn.Checked)
            {
                destinationFolder = Environment.SpecialFolder.LocalApplicationData;
            }
            else if (commonAppDataBtn.Checked)
            {
                destinationFolder = Environment.SpecialFolder.CommonApplicationData;
            }
            if (backgroundWorker != null)
                _installService.DeployPackage(backgroundWorker.ReportProgress,destinationFolder);

        }

        private void pluginInstallWizzard_Cancel(object sender, CancelEventArgs e)
        {
            _bw.CancelAsync();
        }
    }
}
