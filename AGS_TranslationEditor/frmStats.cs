using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AGS_TranslationEditor
{
    public partial class frmStats : Form
    {
        public frmStats()
        {
            InitializeComponent();
        }

        internal void LoadData(int countEntries, int NotTranslatedCount)
        {
            if (countEntries > 0)
            {
                int translatedCount = countEntries - NotTranslatedCount;
                float progressValue = (translatedCount*100)/countEntries;
                progressBar1.Value = Convert.ToInt32(progressValue);

                lblTranslatedCount.Text = translatedCount.ToString() + " (" + progressValue + "%)";
                lblNotTranslatedCount.Text = NotTranslatedCount.ToString();
            }
        }

    }
}
