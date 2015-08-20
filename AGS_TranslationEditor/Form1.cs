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
            fileDialog.Filter = "AGS Translation File(*.TRA,*.TRS)|*.tra;*.trs";
            
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.Rows.Clear();
                int numEntries = 0;
                ArrayList entryList = null;

                if (fileDialog.FileName.Contains(".tra"))
                {
                    entryList = AGS_Translation.ParseTRA_Translation(fileDialog.FileName);
                }
                else if (fileDialog.FileName.Contains(".trs"))
                {
                    entryList = AGS_Translation.ParseTRS_Translation(fileDialog.FileName);
                }

                if (entryList != null)
                {
                    foreach (string[] entry in entryList)
                    {
                        //Populate DataGridView
                        string[] newRow = {entry[0], entry[1]};
                        dataGridView1.Rows.Add(newRow);
                        numEntries++;
                    }
                }

                toolStripStatusLabel1.Text = "Entries: " + numEntries;
            }

        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void neuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "AGS Translation File|*.trs";

            DataSet ds = new DataSet();
            DataTable table = new DataTable();

            table.Clear();
            dataGridView1.Columns.RemoveAt(0);
            dataGridView1.Columns.RemoveAt(0);
            table.Columns.Add("Original");
            table.Columns.Add("Translated");


            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                int numEntries = 0;

                if(dataGridView1.RowCount > 0)
                dataGridView1.Rows.Clear();

                ArrayList entryList = AGS_Translation.ParseTRS_Translation(fileDialog.FileName);

                foreach (string[] entry in entryList)
                {
                    //Populate DataGridView
                    string[] newRow = { entry[0], entry[1] };
                    //DataRow row = table.NewRow();
                    //row["Source Text"] = entry[0];
                    //row["Translation"] = entry[1];

                    //table.Rows.Add(row);
                    table.Rows.Add(newRow);
                    
                    numEntries++;
                }


                ds.Tables.Add(table);
                DataSet set = table.DataSet;
                dataGridView1.DataSource = table;
                //dataGridView1.DataSource = ds;

                toolStripStatusLabel1.Text = "Entries: " + numEntries;
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string test = dataGridView1[0, e.RowIndex].Value.ToString();
                richTextBox1.Text = test;

                string test2 = dataGridView1[1, e.RowIndex].Value.ToString();
                richTextBox2.Text = test2;
            }
            catch (Exception)
            {
                
                //throw;
            }
            
        }
    }
}
