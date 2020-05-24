using AGS_TranslationEditor.Classes;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AGS_TranslationEditor.forms {
    public partial class GoToRowForm : Form {

        public GoToRowForm() {
            InitializeComponent();
        }

        private void btnGoToRow_Click(object sender, EventArgs e) {
            MainForm parent = (MainForm)this.Owner;

            if (int.TryParse(txtBoxGoToRow.Text, out int rowNumber)) {
                rowNumber -= 1;

                parent.translationGridUtils.GoToRow(rowNumber);
            } else {
                AudioManager.getInstance().audio.PlaySystemSound(System.Media.SystemSounds.Exclamation);
                MessageBox.Show("Field must be a number.", "Wrong format");
            }

        }

        private void txtBoxGoToRow_KeyPress(object sender, KeyPressEventArgs e) {
            if(!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar)) {
                e.Handled = true;
                AudioManager.getInstance().audio.PlaySystemSound(System.Media.SystemSounds.Exclamation);
            }
        }
    }
}
