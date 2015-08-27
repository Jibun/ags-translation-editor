namespace AGS_TranslationEditor
{
    partial class frmStats
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStats));
            this.lblTranslated = new System.Windows.Forms.Label();
            this.lblNotTranslated = new System.Windows.Forms.Label();
            this.lblCountEntries = new System.Windows.Forms.Label();
            this.lblTranslatedCount = new System.Windows.Forms.Label();
            this.lblNotTranslatedCount = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lblTranslated
            // 
            resources.ApplyResources(this.lblTranslated, "lblTranslated");
            this.lblTranslated.Name = "lblTranslated";
            // 
            // lblNotTranslated
            // 
            resources.ApplyResources(this.lblNotTranslated, "lblNotTranslated");
            this.lblNotTranslated.Name = "lblNotTranslated";
            // 
            // lblCountEntries
            // 
            resources.ApplyResources(this.lblCountEntries, "lblCountEntries");
            this.lblCountEntries.Name = "lblCountEntries";
            // 
            // lblTranslatedCount
            // 
            resources.ApplyResources(this.lblTranslatedCount, "lblTranslatedCount");
            this.lblTranslatedCount.Name = "lblTranslatedCount";
            // 
            // lblNotTranslatedCount
            // 
            resources.ApplyResources(this.lblNotTranslatedCount, "lblNotTranslatedCount");
            this.lblNotTranslatedCount.Name = "lblNotTranslatedCount";
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // frmStats
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblNotTranslatedCount);
            this.Controls.Add(this.lblTranslatedCount);
            this.Controls.Add(this.lblCountEntries);
            this.Controls.Add(this.lblNotTranslated);
            this.Controls.Add(this.lblTranslated);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStats";
            this.ShowIcon = false;
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTranslated;
        private System.Windows.Forms.Label lblNotTranslated;
        private System.Windows.Forms.Label lblCountEntries;
        private System.Windows.Forms.Label lblTranslatedCount;
        private System.Windows.Forms.Label lblNotTranslatedCount;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}