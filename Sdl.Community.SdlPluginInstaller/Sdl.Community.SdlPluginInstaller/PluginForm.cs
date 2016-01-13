using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using CristiPotlog.Controls;
using NLog;
using Sdl.Community.SdlPluginInstaller.Model;
using Sdl.Community.SdlPluginInstaller.Services;

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

        public PluginForm(Logger logger):this()
        {
            _installedPlugins = new List<PluginPackageInfo>();
            _studioVersionService = new StudioVersionService();
            _pluginManagementService = new PluginManagementService();
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
                        Text = _installedStudioVersions[j].PublicVersion,
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
                    else
                    {
                        return pluginObject.Author;
                    }
                };

                installedPluginListView.CellToolTipShowing += CellToolTipShowing;
                
                installedPluginListView.RowHeight = 50;
                installedPluginListView.FullRowSelect = true;
                DescribedTaskRenderer renderer = new DescribedTaskRenderer
                {
                    Aspect = "PluginName",
                    DescriptionAspectName = "Description",
                    DescriptionColor = Color.Gray,
                    TitleFont = new Font("Arial",10,FontStyle.Bold)

                };  
                nameColumn.Renderer = renderer;

                uninstallColumn.ButtonSize = new Size
                {
                    Width = 53,
                    Height = 20
                };
                
                
                uninstallColumn.AspectGetter = rowObject => "Uninstall";
                
                descriptionHeaderLbl.Text =
                    @"From this screen you are able to see what plugins you have installed, also you can uninstall them.";
                InitializeListView(_installedStudioVersions[0]);

                installedPluginListView.ButtonClick += InstalledPluginListView_ButtonClick;

                installedPluginListView.HeaderStyle = ColumnHeaderStyle.None;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Error constructing plugin window");
                throw;
            }
        }

        public void CellToolTipShowing(object sender, ToolTipShowingEventArgs e)
        {
            if (((ObjectListView) sender).HotColumnIndex == 0)
            {
                var index = ((ObjectListView)sender).HotRowIndex;
                var plugin = _installedPlugins[index];
                e.Text = plugin.PluginName;
            }
            if (((ObjectListView) sender).HotColumnIndex == 2)
            {
                var index = ((ObjectListView)sender).HotRowIndex;
                var plugin = _installedPlugins[index];
                e.Text = plugin.Author;
            }
            
        }

        private void CheckForProcess(PluginPackageInfo plugInToUninstall, int rowIndex)
        {
            _installService = new InstallService(plugInToUninstall);

            var processes = _installService.GetStudioProcesses();
            if (processes.Count <= 0)
            {
                _uninstallService.UninstallPlugin(plugInToUninstall);


                installedPluginListView.RemoveObject(plugInToUninstall);
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
            CheckForProcess(plugInToUninstall,rowIndex);

           

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


            var selectedStudioVersion = _installedStudioVersions[(int) lbl.Tag];
            InitializeListView(selectedStudioVersion);

            installedPluginListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

        }
    }
}
