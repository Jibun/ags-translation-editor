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
            this.lblTranslated.AutoSize = true;
            this.lblTranslated.Location = new System.Drawing.Point(12, 68);
            this.lblTranslated.Name = "lblTranslated";
            this.lblTranslated.Size = new System.Drawing.Size(57, 13);
            this.lblTranslated.TabIndex = 0;
            this.lblTranslated.Text = "Translated";
            // 
            // lblNotTranslated
            // 
            this.lblNotTranslated.AutoSize = true;
            this.lblNotTranslated.Location = new System.Drawing.Point(12, 91);
            this.lblNotTranslated.Name = "lblNotTranslated";
            this.lblNotTranslated.Size = new System.Drawing.Size(77, 13);
            this.lblNotTranslated.TabIndex = 1;
            this.lblNotTranslated.Text = "Not Translated";
            // 
            // lblCountEntries
            // 
            this.lblCountEntries.AutoSize = true;
            this.lblCountEntries.Location = new System.Drawing.Point(192, 50);
            this.lblCountEntries.Name = "lblCountEntries";
            this.lblCountEntries.Size = new System.Drawing.Size(39, 13);
            this.lblCountEntries.TabIndex = 2;
            this.lblCountEntries.Text = "Entries";
            // 
            // lblTranslatedCount
            // 
            this.lblTranslatedCount.AutoSize = true;
            this.lblTranslatedCount.Location = new System.Drawing.Point(192, 68);
            this.lblTranslatedCount.Name = "lblTranslatedCount";
            this.lblTranslatedCount.Size = new System.Drawing.Size(13, 13);
            this.lblTranslatedCount.TabIndex = 3;
            this.lblTranslatedCount.Text = "0";
            // 
            // lblNotTranslatedCount
            // 
            this.lblNotTranslatedCount.AutoSize = true;
            this.lblNotTranslatedCount.Location = new System.Drawing.Point(192, 91);
            this.lblNotTranslatedCount.Name = "lblNotTranslatedCount";
            this.lblNotTranslatedCount.Size = new System.Drawing.Size(13, 13);
            this.lblNotTranslatedCount.TabIndex = 4;
            this.lblNotTranslatedCount.Text = "0";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 13);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(315, 23);
            this.progressBar1.TabIndex = 5;
            // 
            // frmStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 142);
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
            this.Text = "Statistic";
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