﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AGS_TranslationEditor.Properties;
using static System.Windows.Forms.Application;

namespace AGS_TranslationEditor
{
    public partial class frmMain : Form
    {
        private int _selectedRow = 0;
        private string _currentfilename = "";
        private int _numEntries = 0;

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
                //Enable Buttons
                SaveStripButton.Enabled = true;
                StatsStripButton.Enabled = true;
                saveToolStripMenuItem.Enabled = true;
                saveAsToolStripMenuItem.Enabled = true;
                addToolStripMenuItem.Enabled = true;
                removeToolStripMenuItem.Enabled = true;
                
                //Clear the DataGrid
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();

                _numEntries = 0;
                List<string[]> entryList = null;
                _currentfilename = fileDialog.FileName;

                if (fileDialog.FileName.Contains(".tra"))
                {
                    entryList = AGS_Translation.ParseTRA_Translation(fileDialog.FileName);
                }
                else if (fileDialog.FileName.Contains(".trs"))
                {
                    entryList = AGS_Translation.ParseTRS_Translation(fileDialog.FileName);
                }

                List<string[]> testList = new List<string[]>();

                if (entryList != null)
                {
                    foreach (string[] entry in entryList)
                    {
                        //Populate DataGridView
                        string[] newRow = {entry[0], entry[1]};
                        dataGridView1.Rows.Add(newRow);
                        _numEntries++;
                    }
                }

                //dataGridView1.DataSource = entryList;
                lblFileStatus.Text = "File loaded";
                lblEntriesCount.Text = "Entries: " + _numEntries;

                this.Text = _currentfilename + " - AGS Translation Editor";
            }

        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void neuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                FileStream fs = new FileStream(_currentfilename, FileMode.Create);
                StreamWriter fw = new StreamWriter(fs);

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    fw.WriteLine(row.Cells[0].Value);
                    fw.WriteLine(row.Cells[1].Value);
                }

                fw.Close();
                fs.Close();

                lblFileStatus.Text = Resources.frmMain_saveToolStripMenuItem_Click_File_saved;
            }
        }

        private void richTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string newText = richTextBox2.Text;
                dataGridView1.Rows[_selectedRow].Cells[1].Value = newText;
                dataGridView1.Focus();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(saveDialog.FileName, FileMode.Create);
                    StreamWriter fw = new StreamWriter(fs);

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        fw.WriteLine(row.Cells[0].Value);
                        fw.WriteLine(row.Cells[1].Value);
                    }

                    fw.Close();
                    fs.Close();
                }
            }
        }

        private int CountNotTranslated()
        {
            int translatedCount = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string value = (string) row.Cells[1].Value;

                if (string.Equals(value, ""))
                    translatedCount++;
            }
            return translatedCount;
        }

        private void StatsStripButton_Click(object sender, EventArgs e)
        {
            frmStats StatsWindow = new frmStats();
            int countNotTrans = CountNotTranslated();

            StatsWindow.LoadData(_numEntries, countNotTrans);
            StatsWindow.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            saveToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;

            addToolStripMenuItem.Enabled = false;
            removeToolStripMenuItem.Enabled = false;

            SaveStripButton.Enabled = false;
            StatsStripButton.Enabled = false;

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                _selectedRow = dataGridView1.SelectedRows[0].Index;

                string test = (string) dataGridView1.Rows[_selectedRow].Cells[0].Value;
                richTextBox1.Text = test;

                string test2 = (string) dataGridView1.Rows[_selectedRow].Cells[1].Value;
                richTextBox2.Text = test2;
            }
            catch (Exception)
            {

            }
        }

        private void exportAstrsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            SaveFileDialog saveDialog = new SaveFileDialog();

            openDialog.Filter = "TRA File(*.tra)|*.tra";
            saveDialog.Filter = "TRS File(*.trs)|*.trs";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string tra_filename = openDialog.FileName;

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string trs_filename = saveDialog.FileName;
                    List<string[]> entryList = AGS_Translation.ParseTRA_Translation(tra_filename);

                    using (FileStream fs = new FileStream(trs_filename, FileMode.Create))
                    {
                        StreamWriter fw = new StreamWriter(fs);

                        foreach (string[] entry in entryList)
                        {
                            fw.WriteLine(entry[0]);
                            fw.WriteLine(entry[1]);
                        }

                        MessageBox.Show("Converted " + tra_filename + " to " + trs_filename, "Converted",
                            MessageBoxButtons.OK);
                    }
                }

            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add();
            dataGridView1.Rows[dataGridView1.RowCount - 1].Selected = true;
            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.RemoveAt(_selectedRow);
        }
    }
}
