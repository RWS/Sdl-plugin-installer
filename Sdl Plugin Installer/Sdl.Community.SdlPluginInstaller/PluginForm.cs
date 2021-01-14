using BrightIdeasSoftware;
using NLog;
using Sdl.Community.SdlPluginInstaller.Model;
using Sdl.Community.SdlPluginInstaller.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Sdl.Community.SdlPluginInstaller
{
    public partial class PluginForm : Form
    {
        private List<PluginPackageInfo> _installedPlugins;
        private readonly StudioVersionService _studioVersionService;
        private readonly PluginManagementService _pluginManagementService;
        private readonly List<StudioVersion> _installedStudioVersions;
        private readonly Logger _logger;
        private InstallService _installService;
        private readonly UninstallService _uninstallService;
        private readonly List<Label> _labels;
        public PluginForm()
        {
            InitializeComponent();
        }

        public PluginForm(Logger logger) : this()
        {
            _installedPlugins = new List<PluginPackageInfo>();
            _studioVersionService = new StudioVersionService();
            _pluginManagementService = new PluginManagementService(logger);
            _installedStudioVersions = new List<StudioVersion>();
            _labels = new List<Label>();
            _uninstallService = new UninstallService();
            _logger = logger;

        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {

                _installedStudioVersions.AddRange(_studioVersionService.GetInstalledStudioVersions());
                _installedStudioVersions.Reverse();

                for (var j = 0; j < _installedStudioVersions.Count; j++)
                {
                    var label = new Label
                    {
                        Text = string.IsNullOrEmpty(_installedStudioVersions[j].Edition) ? _installedStudioVersions[j].PublicVersion : _installedStudioVersions[j].PublicVersion + " " + _installedStudioVersions[j].Edition,
                        Anchor = AnchorStyles.Left,
                        Tag = j,
                        ForeColor = Color.FromArgb(76, 132, 169),
                        Font = new Font(Font, FontStyle.Regular),
                        AutoSize = true,
                        Cursor = Cursors.Hand
                    };
                    label.Click += studioLabel_Click;

                    _labels.Add(label);
                    labelsPanel.Controls.Add(label);
                }

                _labels[0].Font = new Font(_labels[0].Font, FontStyle.Underline);

                installedPluginListView.ShowGroups = false;

                authorColumn.AspectGetter = delegate (object rowObject)
                {
                    var pluginObject = (PluginPackageInfo)rowObject;

                    if (pluginObject.Author == null)
                    {
                        return "N/A";
                    }
                    return pluginObject.Author;
                };


                minStudioVersionColumn.AspectGetter = delegate (object rowObject)
                {
                    var pluginObject = (PluginPackageInfo)rowObject;
                    return pluginObject.MinRequiredProductVersion;
                };

                installedPluginListView.CellToolTipShowing += InstalledPluginListView_CellToolTipShowing;
                installedPluginListView.HeaderToolTipShowing += InstalledPluginListView_HeaderToolTipShowing;
                installedPluginListView.RowHeight = 50;
                installedPluginListView.FullRowSelect = true;
                //rendered for Name column
                DescribedTaskRenderer renderer = new DescribedTaskRenderer
                {
                    Aspect = "PluginName",
                    DescriptionAspectName = "Description",
                    DescriptionColor = Color.Gray,
                    TitleFont = new Font("Arial", 10, FontStyle.Bold)

                };
                nameColumn.Renderer = renderer;

                uninstallColumn.ButtonSize = new Size
                {
                    Width = 53,
                    Height = 20
                };


                uninstallColumn.AspectGetter = rowObject => "Uninstall";

                descriptionHeaderLbl.Text =
                    @"All installed plugins are listed below.  Ensure Studio is closed and then click the Uninstall button for the plugins you wish to remove.  Plugins that require Administrator rights for installation will not be listed unless you run this uninstaller with Admin rights.";

                //initialize list view with the first available studio version
                InitializeListView(_installedStudioVersions[0]);

                //creates a label if you need admin rights to see a plugin
                if (_pluginManagementService.NeedsAdminRights())
                {

                    var adminRightsLabel = new Label

                    {
                        Text = @"There are installed plugins requiring Administrator rights.",
                        ForeColor = Color.Red,
                        Bounds = new Rectangle(0, 0, 300, 15),
                        Anchor = AnchorStyles.Left
                    };
                    labelsPanel.Controls.Add(adminRightsLabel);
                }

                installedPluginListView.ButtonClick += InstalledPluginListView_ButtonClick;

            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Error constructing plugin window");
                throw;
            }
        }

        private void InstalledPluginListView_HeaderToolTipShowing(object sender, ToolTipShowingEventArgs e)
        {
            if (((ObjectListView)sender).HotColumnIndex == 3)
            {
                e.Text = minStudioVersionColumn.AspectName;
            }
        }

        private void InstalledPluginListView_CellToolTipShowing(object sender, ToolTipShowingEventArgs e)
        {
            if (((ObjectListView)sender).HotColumnIndex == 0)
            {
                var index = ((ObjectListView)sender).HotRowIndex;
                var plugin = _installedPlugins[index];
                e.Text = plugin.PluginName;
            }
            if (((ObjectListView)sender).HotColumnIndex == 2)
            {
                var index = ((ObjectListView)sender).HotRowIndex;
                var plugin = _installedPlugins[index];
                e.Text = plugin.Author;

            }
        }

        private void CheckForProcess(PluginPackageInfo pluginToUninstall, int rowIndex)
        {
            _installService = new InstallService(pluginToUninstall);

            var processes = _installService.GetStudioProcesses();
            if (processes.Count <= 0)
            {
                _uninstallService.UninstallPlugin(pluginToUninstall);
                installedPluginListView.RemoveObject(pluginToUninstall);
                _installedPlugins.RemoveRange(rowIndex, 1);

            }
            else
            {
                using (var processesForm = new Processes(processes))
                {
                    processesForm.ShowDialog();
                }
            }
        }
        private void InstalledPluginListView_ButtonClick(object sender, CellClickEventArgs e)
        {

            var rowIndex = e.RowIndex;
            var plugInToUninstall = _installedPlugins[rowIndex];

            //check for Studio processes
            CheckForProcess(plugInToUninstall, rowIndex);

        }


        private void InitializeListView(StudioVersion studioVersion)
        {
            _installedPlugins = _pluginManagementService.GetInstalledPlugins(studioVersion);
            installedPluginListView.SetObjects(_installedPlugins);
        }

        private void studioLabel_Click(object sender, EventArgs e)
        {
            var lbl = sender as Label;

            if (lbl == null) return;

            foreach (var lable in _labels)
            {
                lable.Font = new Font(lable.Font, FontStyle.Regular);
            }
            lbl.Font = new Font(lbl.Font, FontStyle.Underline);


            var selectedStudioVersion = _installedStudioVersions[(int)lbl.Tag];
            InitializeListView(selectedStudioVersion);

            installedPluginListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

        }
    }
}
