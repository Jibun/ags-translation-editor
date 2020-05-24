namespace AGS_TranslationEditor.forms {
    partial class SearchTextForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.txtBoxSearchText = new System.Windows.Forms.TextBox();
            this.lblSearchText = new System.Windows.Forms.Label();
            this.btnSearchNext = new System.Windows.Forms.Button();
            this.btnSearchPreview = new System.Windows.Forms.Button();
            this.optionsGroup = new System.Windows.Forms.GroupBox();
            this.wrapAroundCheck = new System.Windows.Forms.CheckBox();
            this.caseSensitiveCheck = new System.Windows.Forms.CheckBox();
            this.optionsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtBoxSearchText
            // 
            this.txtBoxSearchText.Location = new System.Drawing.Point(81, 21);
            this.txtBoxSearchText.Name = "txtBoxSearchText";
            this.txtBoxSearchText.Size = new System.Drawing.Size(323, 20);
            this.txtBoxSearchText.TabIndex = 0;
            // 
            // lblSearchText
            // 
            this.lblSearchText.AutoSize = true;
            this.lblSearchText.Location = new System.Drawing.Point(17, 24);
            this.lblSearchText.Name = "lblSearchText";
            this.lblSearchText.Size = new System.Drawing.Size(64, 13);
            this.lblSearchText.TabIndex = 1;
            this.lblSearchText.Text = "Search text:";
            // 
            // btnSearchNext
            // 
            this.btnSearchNext.Location = new System.Drawing.Point(307, 50);
            this.btnSearchNext.Name = "btnSearchNext";
            this.btnSearchNext.Size = new System.Drawing.Size(97, 22);
            this.btnSearchNext.TabIndex = 2;
            this.btnSearchNext.Text = "Search next";
            this.btnSearchNext.UseVisualStyleBackColor = true;
            this.btnSearchNext.Click += new System.EventHandler(this.btnSearchNext_Click);
            // 
            // btnSearchPreview
            // 
            this.btnSearchPreview.Location = new System.Drawing.Point(204, 50);
            this.btnSearchPreview.Name = "btnSearchPreview";
            this.btnSearchPreview.Size = new System.Drawing.Size(97, 22);
            this.btnSearchPreview.TabIndex = 3;
            this.btnSearchPreview.Text = "Search preview";
            this.btnSearchPreview.UseVisualStyleBackColor = true;
            this.btnSearchPreview.Click += new System.EventHandler(this.btnSearchPreview_Click);
            // 
            // optionsGroup
            // 
            this.optionsGroup.Controls.Add(this.wrapAroundCheck);
            this.optionsGroup.Controls.Add(this.caseSensitiveCheck);
            this.optionsGroup.Location = new System.Drawing.Point(20, 53);
            this.optionsGroup.Name = "optionsGroup";
            this.optionsGroup.Size = new System.Drawing.Size(142, 78);
            this.optionsGroup.TabIndex = 4;
            this.optionsGroup.TabStop = false;
            this.optionsGroup.Text = "Options";
            // 
            // wrapAroundCheck
            // 
            this.wrapAroundCheck.AutoSize = true;
            this.wrapAroundCheck.Enabled = false;
            this.wrapAroundCheck.Location = new System.Drawing.Point(22, 46);
            this.wrapAroundCheck.Name = "wrapAroundCheck";
            this.wrapAroundCheck.Size = new System.Drawing.Size(88, 17);
            this.wrapAroundCheck.TabIndex = 0;
            this.wrapAroundCheck.Text = "Wrap around";
            this.wrapAroundCheck.UseVisualStyleBackColor = true;
            // 
            // caseSensitiveCheck
            // 
            this.caseSensitiveCheck.AutoSize = true;
            this.caseSensitiveCheck.Checked = true;
            this.caseSensitiveCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.caseSensitiveCheck.Location = new System.Drawing.Point(22, 23);
            this.caseSensitiveCheck.Name = "caseSensitiveCheck";
            this.caseSensitiveCheck.Size = new System.Drawing.Size(94, 17);
            this.caseSensitiveCheck.TabIndex = 0;
            this.caseSensitiveCheck.Text = "Case sensitive";
            this.caseSensitiveCheck.UseVisualStyleBackColor = true;
            // 
            // SearchTextForm
            // 
            this.AcceptButton = this.btnSearchNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 142);
            this.Controls.Add(this.optionsGroup);
            this.Controls.Add(this.btnSearchPreview);
            this.Controls.Add(this.btnSearchNext);
            this.Controls.Add(this.lblSearchText);
            this.Controls.Add(this.txtBoxSearchText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SearchTextForm";
            this.Text = "Search text";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SearchTextForm_FormClosed);
            this.optionsGroup.ResumeLayout(false);
            this.optionsGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBoxSearchText;
        private System.Windows.Forms.Label lblSearchText;
        private System.Windows.Forms.Button btnSearchNext;
        private System.Windows.Forms.Button btnSearchPreview;
        private System.Windows.Forms.GroupBox optionsGroup;
        private System.Windows.Forms.CheckBox wrapAroundCheck;
        private System.Windows.Forms.CheckBox caseSensitiveCheck;
    }
}