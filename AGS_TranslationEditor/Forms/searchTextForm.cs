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
    public partial class SearchTextForm : Form {

        public SearchTextForm() {
            InitializeComponent();
        }

        private void startSearch(bool forward) {
            MainForm parent = (MainForm)this.Owner;

            SearchOptions options = new SearchOptions();
            options.CaseSensitive = caseSensitiveCheck.Checked;
            options.Forward = forward;
            options.WrapAround = wrapAroundCheck.Checked;

            String text = txtBoxSearchText.Text;
            if (!String.IsNullOrWhiteSpace(text.Trim())) {
                if (parent.translationGridUtils.Search.IsEnabled() && !parent.translationGridUtils.Search.SearchedText.Equals(text)) {
                    parent.translationGridUtils.Search.ResetPosition();
                }
                parent.translationGridUtils.SearchText(text, options);
            } else {
                AudioManager.getInstance().audio.PlaySystemSound(System.Media.SystemSounds.Exclamation);
                MessageBox.Show("Text to search is empty.", "Empty text");
            }
        }

        private void btnSearchNext_Click(object sender, EventArgs e) {
            startSearch(true);
            
        }

        private void btnSearchPreview_Click(object sender, EventArgs e) {
            startSearch(false);
        }

        private void SearchTextForm_FormClosed(object sender, FormClosedEventArgs e) {
            MainForm parent = (MainForm)this.Owner;
            parent.translationGridUtils.Search.Disable();
        }
    }
}
