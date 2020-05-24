namespace AGS_TranslationEditor
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goToRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getGameInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createTraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.csvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblEntriesCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFileStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.translationGrid = new System.Windows.Forms.DataGridView();
            this.colSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTranslation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.OpenStripButton = new System.Windows.Forms.ToolStripButton();
            this.SaveStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.StatsStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.translationGrid)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.toolStripMenuItem2,
            this.openToolStripMenuItem,
            this.toolStripMenuItem3,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            resources.ApplyResources(this.newToolStripMenuItem, "newToolStripMenuItem");
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            resources.ApplyResources(this.saveAsToolStripMenuItem, "saveAsToolStripMenuItem");
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            resources.ApplyResources(this.closeToolStripMenuItem, "closeToolStripMenuItem");
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            resources.ApplyResources(this.addToolStripMenuItem, "addToolStripMenuItem");
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            resources.ApplyResources(this.removeToolStripMenuItem, "removeToolStripMenuItem");
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchTextToolStripMenuItem,
            this.goToRowToolStripMenuItem});
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            resources.ApplyResources(this.searchToolStripMenuItem, "searchToolStripMenuItem");
            // 
            // searchTextToolStripMenuItem
            // 
            this.searchTextToolStripMenuItem.Name = "searchTextToolStripMenuItem";
            resources.ApplyResources(this.searchTextToolStripMenuItem, "searchTextToolStripMenuItem");
            this.searchTextToolStripMenuItem.Click += new System.EventHandler(this.textToolStripMenuItem_Click);
            // 
            // goToRowToolStripMenuItem
            // 
            this.goToRowToolStripMenuItem.Name = "goToRowToolStripMenuItem";
            resources.ApplyResources(this.goToRowToolStripMenuItem, "goToRowToolStripMenuItem");
            this.goToRowToolStripMenuItem.Click += new System.EventHandler(this.rowToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getGameInfoToolStripMenuItem,
            this.createTraToolStripMenuItem,
            this.convertToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            resources.ApplyResources(this.toolsToolStripMenuItem, "toolsToolStripMenuItem");
            // 
            // getGameInfoToolStripMenuItem
            // 
            this.getGameInfoToolStripMenuItem.Name = "getGameInfoToolStripMenuItem";
            resources.ApplyResources(this.getGameInfoToolStripMenuItem, "getGameInfoToolStripMenuItem");
            this.getGameInfoToolStripMenuItem.Click += new System.EventHandler(this.getGameInfoToolStripMenuItem_Click);
            // 
            // createTraToolStripMenuItem
            // 
            this.createTraToolStripMenuItem.Name = "createTraToolStripMenuItem";
            resources.ApplyResources(this.createTraToolStripMenuItem, "createTraToolStripMenuItem");
            this.createTraToolStripMenuItem.Click += new System.EventHandler(this.createTraToolStripMenuItem_Click);
            // 
            // convertToolStripMenuItem
            // 
            this.convertToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xmlToolStripMenuItem,
            this.csvToolStripMenuItem});
            this.convertToolStripMenuItem.Name = "convertToolStripMenuItem";
            resources.ApplyResources(this.convertToolStripMenuItem, "convertToolStripMenuItem");
            // 
            // xmlToolStripMenuItem
            // 
            this.xmlToolStripMenuItem.Name = "xmlToolStripMenuItem";
            resources.ApplyResources(this.xmlToolStripMenuItem, "xmlToolStripMenuItem");
            this.xmlToolStripMenuItem.Click += new System.EventHandler(this.xmlToolStripMenuItem_Click);
            // 
            // csvToolStripMenuItem
            // 
            this.csvToolStripMenuItem.Name = "csvToolStripMenuItem";
            resources.ApplyResources(this.csvToolStripMenuItem, "csvToolStripMenuItem");
            this.csvToolStripMenuItem.Click += new System.EventHandler(this.csvToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblEntriesCount,
            this.toolStripStatusLabel2,
            this.lblFileStatus});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // lblEntriesCount
            // 
            this.lblEntriesCount.Name = "lblEntriesCount";
            resources.ApplyResources(this.lblEntriesCount, "lblEntriesCount");
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            resources.ApplyResources(this.toolStripStatusLabel2, "toolStripStatusLabel2");
            // 
            // lblFileStatus
            // 
            this.lblFileStatus.Name = "lblFileStatus";
            this.lblFileStatus.Padding = new System.Windows.Forms.Padding(100, 0, 0, 0);
            resources.ApplyResources(this.lblFileStatus, "lblFileStatus");
            // 
            // translationGrid
            // 
            this.translationGrid.AllowUserToAddRows = false;
            this.translationGrid.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.translationGrid, "translationGrid");
            this.translationGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.translationGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.translationGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSource,
            this.colTranslation});
            this.translationGrid.MultiSelect = false;
            this.translationGrid.Name = "translationGrid";
            this.translationGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.translationGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.translationGrid.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.translationGrid_CellPainting);
            this.translationGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.translationGrid_CellValueChanged);
            this.translationGrid.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.translationGrid_RowPostPaint);
            this.translationGrid.SelectionChanged += new System.EventHandler(this.translationGrid_SelectionChanged);
            // 
            // colSource
            // 
            this.colSource.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.colSource, "colSource");
            this.colSource.Name = "colSource";
            this.colSource.ReadOnly = true;
            this.colSource.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // colTranslation
            // 
            this.colTranslation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.colTranslation, "colTranslation");
            this.colTranslation.Name = "colTranslation";
            this.colTranslation.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenStripButton,
            this.SaveStripButton,
            this.toolStripSeparator1,
            this.StatsStripButton,
            this.toolStripSeparator2});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // OpenStripButton
            // 
            this.OpenStripButton.Image = global::AGS_TranslationEditor.Properties.Resources.document_open_2x;
            resources.ApplyResources(this.OpenStripButton, "OpenStripButton");
            this.OpenStripButton.Name = "OpenStripButton";
            this.OpenStripButton.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // SaveStripButton
            // 
            this.SaveStripButton.Image = global::AGS_TranslationEditor.Properties.Resources.document_save;
            resources.ApplyResources(this.SaveStripButton, "SaveStripButton");
            this.SaveStripButton.Name = "SaveStripButton";
            this.SaveStripButton.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // StatsStripButton
            // 
            this.StatsStripButton.Image = global::AGS_TranslationEditor.Properties.Resources.stats;
            resources.ApplyResources(this.StatsStripButton, "StatsStripButton");
            this.StatsStripButton.Name = "StatsStripButton";
            this.StatsStripButton.Click += new System.EventHandler(this.StatsStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // richTextBox1
            // 
            resources.ApplyResources(this.richTextBox1, "richTextBox1");
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // richTextBox2
            // 
            resources.ApplyResources(this.richTextBox2, "richTextBox2");
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox2_KeyDown);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.translationGrid);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.translationGrid)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridView translationGrid;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton OpenStripButton;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.ToolStripStatusLabel lblEntriesCount;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton SaveStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton StatsStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel lblFileStatus;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getGameInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createTraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xmlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem csvToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goToRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchTextToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTranslation;
    }
}

