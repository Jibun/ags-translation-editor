namespace AGS_TranslationEditor.forms {
    partial class GoToRowForm {
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
            this.lblGoToRow = new System.Windows.Forms.Label();
            this.txtBoxGoToRow = new System.Windows.Forms.TextBox();
            this.btnGoToRow = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblGoToRow
            // 
            this.lblGoToRow.AutoSize = true;
            this.lblGoToRow.Location = new System.Drawing.Point(20, 26);
            this.lblGoToRow.Name = "lblGoToRow";
            this.lblGoToRow.Size = new System.Drawing.Size(70, 13);
            this.lblGoToRow.TabIndex = 0;
            this.lblGoToRow.Text = "Row number:";
            // 
            // txtBoxGoToRow
            // 
            this.txtBoxGoToRow.Location = new System.Drawing.Point(96, 23);
            this.txtBoxGoToRow.Name = "txtBoxGoToRow";
            this.txtBoxGoToRow.Size = new System.Drawing.Size(128, 20);
            this.txtBoxGoToRow.TabIndex = 1;
            this.txtBoxGoToRow.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxGoToRow_KeyPress);
            // 
            // btnGoToRow
            // 
            this.btnGoToRow.Location = new System.Drawing.Point(230, 20);
            this.btnGoToRow.Name = "btnGoToRow";
            this.btnGoToRow.Size = new System.Drawing.Size(39, 24);
            this.btnGoToRow.TabIndex = 2;
            this.btnGoToRow.Text = "Go";
            this.btnGoToRow.UseVisualStyleBackColor = true;
            this.btnGoToRow.Click += new System.EventHandler(this.btnGoToRow_Click);
            // 
            // GoToRowForm
            // 
            this.AcceptButton = this.btnGoToRow;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 68);
            this.Controls.Add(this.btnGoToRow);
            this.Controls.Add(this.txtBoxGoToRow);
            this.Controls.Add(this.lblGoToRow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GoToRowForm";
            this.Text = "Go to row";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGoToRow;
        private System.Windows.Forms.TextBox txtBoxGoToRow;
        private System.Windows.Forms.Button btnGoToRow;
    }
}