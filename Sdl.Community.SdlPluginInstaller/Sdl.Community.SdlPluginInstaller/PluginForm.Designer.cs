namespace Sdl.Community.SdlPluginInstaller
{
    partial class PluginForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.descriptionHeaderLbl = new System.Windows.Forms.Label();
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.installedPluginListView = new BrightIdeasSoftware.ObjectListView();
            this.nameColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.versionColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.authorColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.minStudioVersionColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.uninstallColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.headerTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labelsPanel = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.mainTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.installedPluginListView)).BeginInit();
            this.headerTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::Sdl.Community.SdlPluginInstaller.Properties.Resources.OpenExchange;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(68, 68);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // descriptionHeaderLbl
            // 
            this.descriptionHeaderLbl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.descriptionHeaderLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descriptionHeaderLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionHeaderLbl.Location = new System.Drawing.Point(77, 0);
            this.descriptionHeaderLbl.Name = "descriptionHeaderLbl";
            this.descriptionHeaderLbl.Size = new System.Drawing.Size(634, 74);
            this.descriptionHeaderLbl.TabIndex = 3;
            this.descriptionHeaderLbl.Text = "label1";
            this.descriptionHeaderLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.ColumnCount = 1;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.Controls.Add(this.installedPluginListView, 0, 2);
            this.mainTableLayoutPanel.Controls.Add(this.headerTableLayoutPanel, 0, 0);
            this.mainTableLayoutPanel.Controls.Add(this.labelsPanel, 0, 1);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 3;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(709, 413);
            this.mainTableLayoutPanel.TabIndex = 4;
            // 
            // installedPluginListView
            // 
            this.installedPluginListView.AllColumns.Add(this.nameColumn);
            this.installedPluginListView.AllColumns.Add(this.versionColumn);
            this.installedPluginListView.AllColumns.Add(this.authorColumn);
            this.installedPluginListView.AllColumns.Add(this.minStudioVersionColumn);
            this.installedPluginListView.AllColumns.Add(this.uninstallColumn);
            this.installedPluginListView.CellEditUseWholeCell = false;
            this.installedPluginListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumn,
            this.versionColumn,
            this.authorColumn,
            this.minStudioVersionColumn,
            this.uninstallColumn});
            this.installedPluginListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.installedPluginListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.installedPluginListView.HighlightBackgroundColor = System.Drawing.Color.Empty;
            this.installedPluginListView.HighlightForegroundColor = System.Drawing.Color.Empty;
            this.installedPluginListView.Location = new System.Drawing.Point(3, 113);
            this.installedPluginListView.Name = "installedPluginListView";
            this.installedPluginListView.Size = new System.Drawing.Size(703, 297);
            this.installedPluginListView.TabIndex = 5;
            this.installedPluginListView.UseCompatibleStateImageBehavior = false;
            this.installedPluginListView.View = System.Windows.Forms.View.Details;
            // 
            // nameColumn
            // 
            this.nameColumn.AspectName = "PluginName";
            this.nameColumn.FillsFreeSpace = true;
            this.nameColumn.Text = "Name";
            // 
            // versionColumn
            // 
            this.versionColumn.AspectName = "Version";
            this.versionColumn.Text = "Version";
            // 
            // authorColumn
            // 
            this.authorColumn.Text = "Author";
            // 
            // minStudioVersionColumn
            // 
            this.minStudioVersionColumn.AspectName = "MinRequiredProductVersion";
            this.minStudioVersionColumn.Text = "MinStudioVersion";
            // 
            // uninstallColumn
            // 
            this.uninstallColumn.AspectName = "";
            this.uninstallColumn.ButtonSizing = BrightIdeasSoftware.OLVColumn.ButtonSizingMode.FixedBounds;
            this.uninstallColumn.IsButton = true;
            this.uninstallColumn.Text = "Uninstall";
            // 
            // headerTableLayoutPanel
            // 
            this.headerTableLayoutPanel.ColumnCount = 2;
            this.headerTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.headerTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.headerTableLayoutPanel.Controls.Add(this.pictureBox1, 0, 0);
            this.headerTableLayoutPanel.Controls.Add(this.descriptionHeaderLbl, 1, 0);
            this.headerTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.headerTableLayoutPanel.Name = "headerTableLayoutPanel";
            this.headerTableLayoutPanel.RowCount = 1;
            this.headerTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.headerTableLayoutPanel.Size = new System.Drawing.Size(703, 74);
            this.headerTableLayoutPanel.TabIndex = 5;
            // 
            // labelsPanel
            // 
            this.labelsPanel.ColumnCount = 4;
            this.labelsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.labelsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.labelsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.labelsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.labelsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelsPanel.Location = new System.Drawing.Point(3, 83);
            this.labelsPanel.Name = "labelsPanel";
            this.labelsPanel.RowCount = 1;
            this.labelsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.labelsPanel.Size = new System.Drawing.Size(703, 24);
            this.labelsPanel.TabIndex = 6;
            // 
            // PluginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(709, 413);
            this.Controls.Add(this.mainTableLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(725, 451);
            this.Name = "PluginForm";
            this.Text = "PluginForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.mainTableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.installedPluginListView)).EndInit();
            this.headerTableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label descriptionHeaderLbl;
        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel headerTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel labelsPanel;
        private BrightIdeasSoftware.ObjectListView installedPluginListView;
        private BrightIdeasSoftware.OLVColumn nameColumn;
        private BrightIdeasSoftware.OLVColumn versionColumn;
        private BrightIdeasSoftware.OLVColumn authorColumn;
        private BrightIdeasSoftware.OLVColumn minStudioVersionColumn;
        private BrightIdeasSoftware.OLVColumn uninstallColumn;
    }
}