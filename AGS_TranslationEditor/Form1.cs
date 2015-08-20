using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.Application;

namespace AGS_TranslationEditor
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void öffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "AGS Translation File|*.tra";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.Rows.Clear();
                ArrayList entryList = AGS_Translation.ParseTranslation(fileDialog.FileName);

                foreach (string[] entry in entryList)
                {
                    //Populate DataGridView
                    string[] newRow = { entry[0], entry[1] };
                    dataGridView1.Rows.Add(newRow);
                }
                 
            }

        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void neuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            DataTable table = new DataTable();
            //table.Columns.Add("Original");
            //table.Columns.Add("Translated");

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                int numEntries = 0;
                dataGridView1.Rows.Clear();
                ArrayList entryList = AGS_Translation.ParseTRSTranslation(fileDialog.FileName);

                foreach (string[] entry in entryList)
                {
                    //Populate DataGridView
                    string[] newRow = { entry[0], entry[1] };

                    //table.Rows.Add(newRow);
                    dataGridView1.Rows.Add(newRow);
                    numEntries++;
                }

                //dataGridView1.DataSource = table;

                toolStripStatusLabel1.Text = "Entries: " + numEntries;
            }
        }
    }
}
