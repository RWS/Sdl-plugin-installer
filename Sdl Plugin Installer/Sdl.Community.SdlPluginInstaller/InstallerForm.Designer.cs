using Sdl.Community.Controls;

namespace Sdl.Community.SdlPluginInstaller
{
    partial class InstallerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            BrightIdeasSoftware.HeaderStateStyle headerStateStyle1 = new BrightIdeasSoftware.HeaderStateStyle();
            BrightIdeasSoftware.HeaderStateStyle headerStateStyle2 = new BrightIdeasSoftware.HeaderStateStyle();
            BrightIdeasSoftware.HeaderStateStyle headerStateStyle3 = new BrightIdeasSoftware.HeaderStateStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstallerForm));
            this.headerFormatStyle1 = new BrightIdeasSoftware.HeaderFormatStyle();
            this.pluginInstallWizzard = new CristiPotlog.Controls.Wizard();
            this.welcomePage = new Sdl.Community.Controls.WizardPage();
            this.finalPage = new Sdl.Community.Controls.WizardPage();
            this.taskRunnerPage = new Sdl.Community.Controls.WizardPage();
            this.labelProgress = new System.Windows.Forms.Label();
            this.progressLongTask = new System.Windows.Forms.ProgressBar();
            this.installedStudioVersionsPage = new Sdl.Community.Controls.WizardPage();
            this.tableLayoutPanelStudioVersions = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkStudioVersions = new BrightIdeasSoftware.ObjectListView();
            this.studioVersionColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.label2 = new System.Windows.Forms.Label();
            this.lselectLbl = new System.Windows.Forms.Label();
            this.folderGroupBox = new System.Windows.Forms.GroupBox();
            this.commonAppDataBtn = new System.Windows.Forms.RadioButton();
            this.localAppDataBtn = new System.Windows.Forms.RadioButton();
            this.appDataBtn = new System.Windows.Forms.RadioButton();
            this.licensePage = new Sdl.Community.Controls.WizardPage();
            this.textLicense = new System.Windows.Forms.RichTextBox();
            this.checkIAgree = new System.Windows.Forms.CheckBox();
            this.pluginInstallWizzard.SuspendLayout();
            this.taskRunnerPage.SuspendLayout();
            this.installedStudioVersionsPage.SuspendLayout();
            this.tableLayoutPanelStudioVersions.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkStudioVersions)).BeginInit();
            this.folderGroupBox.SuspendLayout();
            this.licensePage.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerFormatStyle1
            // 
            this.headerFormatStyle1.Hot = headerStateStyle1;
            this.headerFormatStyle1.Normal = headerStateStyle2;
            this.headerFormatStyle1.Pressed = headerStateStyle3;
            // 
            // pluginInstallWizzard
            // 
            this.pluginInstallWizzard.Controls.Add(this.welcomePage);
            this.pluginInstallWizzard.Controls.Add(this.finalPage);
            this.pluginInstallWizzard.Controls.Add(this.taskRunnerPage);
            this.pluginInstallWizzard.Controls.Add(this.installedStudioVersionsPage);
            this.pluginInstallWizzard.Controls.Add(this.licensePage);
            this.pluginInstallWizzard.HeaderImage = global::Sdl.Community.SdlPluginInstaller.Properties.Resources.OpenExchange;
            this.pluginInstallWizzard.Location = new System.Drawing.Point(0, 0);
            this.pluginInstallWizzard.Name = "pluginInstallWizzard";
            this.pluginInstallWizzard.Pages.AddRange(new Sdl.Community.Controls.WizardPage[] {
            this.welcomePage,
            this.licensePage,
            this.installedStudioVersionsPage,
            this.taskRunnerPage,
            this.finalPage});
            this.pluginInstallWizzard.Size = new System.Drawing.Size(466, 355);
            this.pluginInstallWizzard.TabIndex = 0;
            this.pluginInstallWizzard.WelcomeImage = global::Sdl.Community.SdlPluginInstaller.Properties.Resources.sdl_logo1;
            this.pluginInstallWizzard.BeforeSwitchPages += new CristiPotlog.Controls.Wizard.BeforeSwitchPagesEventHandler(this.pluginInstallWizzard_BeforeSwitchPages);
            this.pluginInstallWizzard.AfterSwitchPages += new CristiPotlog.Controls.Wizard.AfterSwitchPagesEventHandler(this.pluginInstallWizzard_AfterSwitchPages);
            this.pluginInstallWizzard.Cancel += new System.ComponentModel.CancelEventHandler(this.pluginInstallWizzard_Cancel);
            // 
            // welcomePage
            // 
            this.welcomePage.Description = "This wizard will guide you through the steps of performing a SDL plugin installat" +
    "ion";
            this.welcomePage.Location = new System.Drawing.Point(0, 0);
            this.welcomePage.Name = "welcomePage";
            this.welcomePage.Size = new System.Drawing.Size(466, 307);
            this.welcomePage.Style = Sdl.Community.Controls.WizardPageStyle.Welcome;
            this.welcomePage.TabIndex = 10;
            this.welcomePage.Title = "Welcome to the SDL plugin installer wizard";
            // 
            // finalPage
            // 
            this.finalPage.Description = "Thank you for using the SDL plugin installer. Press OK to exit";
            this.finalPage.Location = new System.Drawing.Point(0, 0);
            this.finalPage.Name = "finalPage";
            this.finalPage.Size = new System.Drawing.Size(466, 307);
            this.finalPage.Style = Sdl.Community.Controls.WizardPageStyle.Finish;
            this.finalPage.TabIndex = 14;
            this.finalPage.Title = "SDL plugin installer has finished.";
            // 
            // taskRunnerPage
            // 
            this.taskRunnerPage.Controls.Add(this.labelProgress);
            this.taskRunnerPage.Controls.Add(this.progressLongTask);
            this.taskRunnerPage.Location = new System.Drawing.Point(0, 0);
            this.taskRunnerPage.Name = "taskRunnerPage";
            this.taskRunnerPage.Size = new System.Drawing.Size(428, 208);
            this.taskRunnerPage.TabIndex = 13;
            this.taskRunnerPage.Title = "SDL plugin is being installed";
            // 
            // labelProgress
            // 
            this.labelProgress.Location = new System.Drawing.Point(16, 102);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(252, 16);
            this.labelProgress.TabIndex = 3;
            this.labelProgress.Text = "Please wait while the SDL plugin is being installed...";
            // 
            // progressLongTask
            // 
            this.progressLongTask.Location = new System.Drawing.Point(12, 122);
            this.progressLongTask.Name = "progressLongTask";
            this.progressLongTask.Size = new System.Drawing.Size(436, 20);
            this.progressLongTask.TabIndex = 2;
            // 
            // installedStudioVersionsPage
            // 
            this.installedStudioVersionsPage.Controls.Add(this.tableLayoutPanelStudioVersions);
            this.installedStudioVersionsPage.Description = "Please select for which Studio versions you want to install the plugin";
            this.installedStudioVersionsPage.Location = new System.Drawing.Point(0, 0);
            this.installedStudioVersionsPage.Name = "installedStudioVersionsPage";
            this.installedStudioVersionsPage.Size = new System.Drawing.Size(428, 208);
            this.installedStudioVersionsPage.TabIndex = 12;
            this.installedStudioVersionsPage.Title = "Installed Studio versions";
            // 
            // tableLayoutPanelStudioVersions
            // 
            this.tableLayoutPanelStudioVersions.ColumnCount = 2;
            this.tableLayoutPanelStudioVersions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelStudioVersions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelStudioVersions.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanelStudioVersions.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanelStudioVersions.Controls.Add(this.lselectLbl, 0, 1);
            this.tableLayoutPanelStudioVersions.Controls.Add(this.folderGroupBox, 1, 1);
            this.tableLayoutPanelStudioVersions.Location = new System.Drawing.Point(12, 79);
            this.tableLayoutPanelStudioVersions.Name = "tableLayoutPanelStudioVersions";
            this.tableLayoutPanelStudioVersions.RowCount = 2;
            this.tableLayoutPanelStudioVersions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelStudioVersions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelStudioVersions.Size = new System.Drawing.Size(442, 213);
            this.tableLayoutPanelStudioVersions.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkStudioVersions);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(224, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(215, 100);
            this.panel1.TabIndex = 2;
            // 
            // chkStudioVersions
            // 
            this.chkStudioVersions.AllColumns.Add(this.studioVersionColumn);
            this.chkStudioVersions.CellEditUseWholeCell = false;
            this.chkStudioVersions.CheckBoxes = true;
            this.chkStudioVersions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.studioVersionColumn});
            this.chkStudioVersions.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkStudioVersions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkStudioVersions.FullRowSelect = true;
            this.chkStudioVersions.HasCollapsibleGroups = false;
            this.chkStudioVersions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.chkStudioVersions.HideSelection = false;
            this.chkStudioVersions.Location = new System.Drawing.Point(0, 0);
            this.chkStudioVersions.Name = "chkStudioVersions";
            this.chkStudioVersions.Size = new System.Drawing.Size(215, 100);
            this.chkStudioVersions.TabIndex = 0;
            this.chkStudioVersions.UseCompatibleStateImageBehavior = false;
            this.chkStudioVersions.View = System.Windows.Forms.View.Details;
            // 
            // studioVersionColumn
            // 
            this.studioVersionColumn.AspectName = "";
            this.studioVersionColumn.FillsFreeSpace = true;
            this.studioVersionColumn.Groupable = false;
            this.studioVersionColumn.IsEditable = false;
            this.studioVersionColumn.Width = 151;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(213, 52);
            this.label2.TabIndex = 3;
            this.label2.Text = "All Studio versions installed on the machine are displayed on the list on the rig" +
    "ht. Studio versions that are not compatible with the plugin will be grayed out.";
            // 
            // lselectLbl
            // 
            this.lselectLbl.AutoSize = true;
            this.lselectLbl.Location = new System.Drawing.Point(3, 106);
            this.lselectLbl.Name = "lselectLbl";
            this.lselectLbl.Size = new System.Drawing.Size(215, 26);
            this.lselectLbl.TabIndex = 4;
            this.lselectLbl.Text = "Please select the folder where the plugin will be installed";
            // 
            // folderGroupBox
            // 
            this.folderGroupBox.Controls.Add(this.commonAppDataBtn);
            this.folderGroupBox.Controls.Add(this.localAppDataBtn);
            this.folderGroupBox.Controls.Add(this.appDataBtn);
            this.folderGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.folderGroupBox.Location = new System.Drawing.Point(224, 109);
            this.folderGroupBox.Name = "folderGroupBox";
            this.folderGroupBox.Size = new System.Drawing.Size(215, 101);
            this.folderGroupBox.TabIndex = 5;
            this.folderGroupBox.TabStop = false;
            this.folderGroupBox.Text = "Locations";
            // 
            // commonAppDataBtn
            // 
            this.commonAppDataBtn.AutoSize = true;
            this.commonAppDataBtn.Location = new System.Drawing.Point(6, 66);
            this.commonAppDataBtn.Name = "commonAppDataBtn";
            this.commonAppDataBtn.Size = new System.Drawing.Size(148, 17);
            this.commonAppDataBtn.TabIndex = 2;
            this.commonAppDataBtn.TabStop = true;
            this.commonAppDataBtn.Text = "This computer for all users";
            this.commonAppDataBtn.UseVisualStyleBackColor = true;
            // 
            // localAppDataBtn
            // 
            this.localAppDataBtn.AutoSize = true;
            this.localAppDataBtn.Location = new System.Drawing.Point(6, 43);
            this.localAppDataBtn.Name = "localAppDataBtn";
            this.localAppDataBtn.Size = new System.Drawing.Size(146, 17);
            this.localAppDataBtn.TabIndex = 1;
            this.localAppDataBtn.TabStop = true;
            this.localAppDataBtn.Text = "This computer for me only";
            this.localAppDataBtn.UseVisualStyleBackColor = true;
            // 
            // appDataBtn
            // 
            this.appDataBtn.AutoSize = true;
            this.appDataBtn.Location = new System.Drawing.Point(6, 19);
            this.appDataBtn.Name = "appDataBtn";
            this.appDataBtn.Size = new System.Drawing.Size(148, 17);
            this.appDataBtn.TabIndex = 0;
            this.appDataBtn.TabStop = true;
            this.appDataBtn.Text = "All your domain computers";
            this.appDataBtn.UseVisualStyleBackColor = true;
            // 
            // licensePage
            // 
            this.licensePage.Controls.Add(this.textLicense);
            this.licensePage.Controls.Add(this.checkIAgree);
            this.licensePage.Description = "Please read the following license agreement and confirm that you agree with all t" +
    "erms and conditions.";
            this.licensePage.Location = new System.Drawing.Point(0, 0);
            this.licensePage.Name = "licensePage";
            this.licensePage.Size = new System.Drawing.Size(428, 208);
            this.licensePage.TabIndex = 11;
            this.licensePage.Title = "License Agreement";
            // 
            // textLicense
            // 
            this.textLicense.Location = new System.Drawing.Point(12, 73);
            this.textLicense.Name = "textLicense";
            this.textLicense.Size = new System.Drawing.Size(442, 196);
            this.textLicense.TabIndex = 4;
            this.textLicense.Text = "";
            // 
            // checkIAgree
            // 
            this.checkIAgree.AutoSize = true;
            this.checkIAgree.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkIAgree.Location = new System.Drawing.Point(12, 275);
            this.checkIAgree.Name = "checkIAgree";
            this.checkIAgree.Size = new System.Drawing.Size(252, 18);
            this.checkIAgree.TabIndex = 3;
            this.checkIAgree.Text = "I agree with this license\'s terms and conditions.";
            this.checkIAgree.CheckedChanged += new System.EventHandler(this.checkIAgree_CheckedChanged);
            // 
            // InstallerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 355);
            this.Controls.Add(this.pluginInstallWizzard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InstallerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SDL Plugin Installer";
            this.pluginInstallWizzard.ResumeLayout(false);
            this.taskRunnerPage.ResumeLayout(false);
            this.installedStudioVersionsPage.ResumeLayout(false);
            this.tableLayoutPanelStudioVersions.ResumeLayout(false);
            this.tableLayoutPanelStudioVersions.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkStudioVersions)).EndInit();
            this.folderGroupBox.ResumeLayout(false);
            this.folderGroupBox.PerformLayout();
            this.licensePage.ResumeLayout(false);
            this.licensePage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CristiPotlog.Controls.Wizard pluginInstallWizzard;
        private WizardPage taskRunnerPage;
        private WizardPage installedStudioVersionsPage;
        private WizardPage licensePage;
        private WizardPage welcomePage;
        private System.Windows.Forms.CheckBox checkIAgree;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelStudioVersions;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private WizardPage finalPage;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.ProgressBar progressLongTask;
        private System.Windows.Forms.RichTextBox textLicense;
        private BrightIdeasSoftware.HeaderFormatStyle headerFormatStyle1;
        private BrightIdeasSoftware.ObjectListView chkStudioVersions;
        private BrightIdeasSoftware.OLVColumn studioVersionColumn;
        private System.Windows.Forms.Label lselectLbl;
        private System.Windows.Forms.GroupBox folderGroupBox;
        private System.Windows.Forms.RadioButton commonAppDataBtn;
        private System.Windows.Forms.RadioButton localAppDataBtn;
        private System.Windows.Forms.RadioButton appDataBtn;
    }
}

