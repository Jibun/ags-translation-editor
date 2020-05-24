/*
    Copyright 2015 Bernd Keilmann

    This file is part of the AGS Translation Editor.

    AGS Translation Editor is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    AGS Translation Editor is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with AGS Translation Editor.  If not, see<http://www.gnu.org/licenses/>.

    Diese Datei ist Teil von AGS Translation Editor.

    AGS Translation Editor ist Freie Software: Sie können es unter den Bedingungen
    der GNU General Public License, wie von der Free Software Foundation,
    Version 3 der Lizenz oder (nach Ihrer Wahl) jeder späteren
    veröffentlichten Version, weiterverbreiten und/oder modifizieren.

    Fubar wird in der Hoffnung, dass es nützlich sein wird, aber
    OHNE JEDE GEWÄHRLEISTUNG, bereitgestellt; sogar ohne die implizite
    Gewährleistung der MARKTFÄHIGKEIT oder EIGNUNG FÜR EINEN BESTIMMTEN ZWECK.
    Siehe die GNU General Public License für weitere Details.

    Sie sollten eine Kopie der GNU General Public License zusammen mit diesem
    Programm erhalten haben.Wenn nicht, siehe <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using AGS_TranslationEditor.Classes;
using AGS_TranslationEditor.forms;
using AGS_TranslationEditor.Properties;
using Microsoft.VisualBasic.Devices;
using static System.Windows.Forms.Application;

namespace AGS_TranslationEditor
{
    public partial class MainForm : Form
    {
        
        public TranslationGridUtils translationGridUtils;

        private int selectedRow = 0;
        private string currentFileName = "";
        private int numEntries = 0;
        private bool documentChanged = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            documentChanged = false;
            saveToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;

            addToolStripMenuItem.Enabled = false;
            removeToolStripMenuItem.Enabled = false;

            searchTextToolStripMenuItem.Enabled = false;
            goToRowToolStripMenuItem.Enabled = false;

            SaveStripButton.Enabled = false;
            StatsStripButton.Enabled = false;

            translationGridUtils = new TranslationGridUtils(translationGrid);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
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
                searchTextToolStripMenuItem.Enabled = true;
                goToRowToolStripMenuItem.Enabled = true;

                //Clear the DataGrid
                translationGrid.Rows.Clear();
                translationGrid.Refresh();

                numEntries = 0;
                Dictionary<string, string> entryList = null;
                currentFileName = fileDialog.FileName;

                if (fileDialog.FileName.Contains(".tra"))
                {
                    entryList = AgsTranslation.ParseTraTranslation(fileDialog.FileName);
                }
                else if (fileDialog.FileName.Contains(".trs"))
                {
                    entryList = AgsTranslation.ParseTrsTranslation(fileDialog.FileName);
                }

                if (entryList != null)
                {
                    foreach (KeyValuePair<string, string> pair in entryList)
                    {
                        //Populate DataGridView
                        string[] newRow = {pair.Key, pair.Value};
                        translationGrid.Rows.Add(newRow);
                        numEntries++;
                    }
                }

                lblFileStatus.Text = "File loaded";
                lblEntriesCount.Text = "Entries: " + numEntries;

                //Set Form text to filename
                this.Text = currentFileName + " - AGS Translation Editor";
                documentChanged = false;
            }
        }

        private void SaveBeforeExiting() {
            string question = currentFileName.Substring(currentFileName.LastIndexOf("\\") + 1) +
                        " has been modified. Do you want to save the changes?";

            //Ask if user wants to save if data was changed
            if (MessageBox.Show(question, "AGS Translation Editor", MessageBoxButtons.YesNo) ==
                DialogResult.Yes) {
                //Save changes then exit
                if (translationGrid.Rows.Count > 0) {

                    Match extension = Regex.Match(currentFileName, "\\.[0-9a-z]+$");
                    if (extension.Value.Equals(".trs")) {
                        SaveFile(currentFileName);
                    } else {
                        SaveAs();
                    }
                }

            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e) {
            if (documentChanged) {
                SaveBeforeExiting();
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e) {
            Exit();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            if (translationGrid.Rows.Count > 0) {
                Match extension = Regex.Match(currentFileName, "\\.[0-9a-z]+$");
                if (extension.Value.Equals(".trs")) { 
                    SaveFile(currentFileName);

                    lblFileStatus.Text = Resources.frmMain_saveToolStripMenuItem_Click_File_saved;
                    MessageBox.Show(
                        $"File was saved as {currentFileName}", 
                        "File saved",
                        MessageBoxButtons.OK);
                } else {
                    SaveAs();
                }
            }
        }

        private void SaveFile(string filename) {
            FileStream fs = new FileStream(filename, FileMode.Create);
            StreamWriter fw = new StreamWriter(fs);

            foreach (DataGridViewRow row in translationGrid.Rows) {
                fw.WriteLine(row.Cells[0].Value);
                fw.WriteLine(row.Cells[1].Value);
            }
            fw.Close();
            fs.Close();
        }

        private void SaveAs() {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "trs";
            //saveDialog.AddExtension = true;
            saveDialog.Filter = "AGS Translation File(*.TRS)|*.trs";

            if (saveDialog.ShowDialog() == DialogResult.OK) {
                SaveFile(saveDialog.FileName);

                MessageBox.Show(
                    $"File was saved as {saveDialog.FileName}",
                    "File saved",
                    MessageBoxButtons.OK);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {
            if (translationGrid.Rows.Count > 0) {
                SaveAs();
            }
        }

        private void StatsStripButton_Click(object sender, EventArgs e)
        {
            StatsForm StatsWindow = new StatsForm();
            int countNotTrans = translationGridUtils.CountUntranslated();

            StatsWindow.LoadData(numEntries, countNotTrans);
            StatsWindow.Show();
        }

        private void translationGrid_SelectionChanged(object sender, EventArgs e) {

            if (translationGrid.SelectedRows.Count == 0) {
                return;
            }

            translationGridUtils.Search.ResetPosition();
            selectedRow = translationGrid.SelectedRows[0].Index;

            string originalText = (string) translationGrid.Rows[selectedRow].Cells[0].Value;
            richTextBox1.Text = originalText;

            string translationText = (string) translationGrid.Rows[selectedRow].Cells[1].Value;
            /*if(translationText.Length <= 0) {
                string tempText = originalText.Substring(originalText.IndexOf(" "));

                richTextBox2.Text = YandexTranslationApi.translate(null, "de", tempText, null, null);
            } else*/
            richTextBox2.Text = translationText;
            

        }

        private void translationGrid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e) {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var rightFormat = new StringFormat() {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth - 5, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, rightFormat);
        }

        private void translationGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            this.Text = currentFileName + " • - AGS Translation Editor";
            documentChanged = true;
        }

        private void translationGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e) {
            if (e.FormattedValue == null)
                return;
            if (e.RowIndex == -1 || e.ColumnIndex == -1) { 
                return;
            }

            if (translationGridUtils.Search.IsEnabled()) {
                String gridCellValue = e.FormattedValue.ToString();
                String text = translationGridUtils.Search.SearchedText;

                //check the index of text in the cell

                bool backgroundPainted = false;
                int startIndexInCellValue = -1;
                do {
                    int startLookingAt = 0;
                    if (startIndexInCellValue >= 0) {
                        startLookingAt = startIndexInCellValue + text.Length;
                    }
                    startIndexInCellValue = gridCellValue.IndexOf(text, startLookingAt, translationGridUtils.Search.ComparisonOption);

                    if (startIndexInCellValue >= 0) {

                        if (!backgroundPainted) {
                            e.Handled = true;
                            e.PaintBackground(e.CellBounds, true);
                            backgroundPainted = true;
                        }

                        //the highlite rectangle  
                        Rectangle hl_rect = new Rectangle();
                        hl_rect.Y = e.CellBounds.Y + 2;
                        hl_rect.Height = e.CellBounds.Height - 5;

                        //find size of text preceding searched text
                        String sBeforeSearchword = gridCellValue.Substring(0, startIndexInCellValue);
                        Size sBefore = TextRenderer.MeasureText(e.Graphics, sBeforeSearchword, e.CellStyle.Font, e.CellBounds.Size);

                        //find size of searched text
                        Size sText = TextRenderer.MeasureText(e.Graphics, text, e.CellStyle.Font, e.CellBounds.Size);

                        if (sBefore.Width > 5) {
                            hl_rect.X = e.CellBounds.X + sBefore.Width - 5;
                            hl_rect.Width = sText.Width - 6;
                        } else {
                            hl_rect.X = e.CellBounds.X + 2;
                            hl_rect.Width = sText.Width - 6;
                        }

                        //pick color  
                        SolidBrush hl_brush = new SolidBrush(Color.LightYellow);
                        if (translationGridUtils.Search.CurrentRow == e.RowIndex) {
                            if (translationGridUtils.Search.CurrentCell == e.ColumnIndex &&
                                    translationGridUtils.Search.CurrentIndex == startIndexInCellValue) {
                                hl_brush = new SolidBrush(Color.Red);
                            } else {
                                hl_brush = new SolidBrush(Color.DarkSalmon);
                            }
                        }

                        //paint background behind searched text  
                        e.Graphics.FillRectangle(hl_brush, hl_rect);
                        hl_brush.Dispose();
                        
                    }

                } while (startIndexInCellValue != -1);

                //Print cell text
                e.PaintContent(e.CellBounds);
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            translationGrid.Rows.Add();
            translationGrid.Rows[translationGrid.RowCount - 1].Selected = true;
            translationGrid.FirstDisplayedScrollingRowIndex = translationGrid.RowCount - 1;

            translationGridUtils.Search.ResetPosition();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            translationGrid.Rows.RemoveAt(selectedRow);

            translationGridUtils.Search.ResetPosition();
        }

        private void richTextBox2_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                string newText = richTextBox2.Text;
                translationGrid.Rows[selectedRow].Cells[1].Value = newText;
                translationGrid.Focus();

                translationGridUtils.Search.ResetPosition();
            }
        }

        private void getGameInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Game EXE File (*.exe)|*.exe";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                AgsTranslation.Gameinfo gameinfo = new AgsTranslation.Gameinfo();

                gameinfo = AgsTranslation.GetGameInfo(openDialog.FileName);
                MessageBox.Show(
                    "AGS Version: " + gameinfo.Version +
                    "\nGame Title: " + gameinfo.GameTitle +
                    "\nGameUID: " + gameinfo.GameUID,
                    "Game Information");
            }
        }

        private void createTraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openExeDialog = new OpenFileDialog();
            openExeDialog.Title = "Game EXE for Translation";
            openExeDialog.Filter = "AGS EXE File (*.exe)|*.exe";

            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "TRS Translation File (*.trs)|*.trs";
            openDialog.Title = "Open TRS Translation you want to use.";

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "tra";
            saveDialog.Filter = "TRA Translation File (*.tra)|*.tra";
            saveDialog.Title = "Save TRA Translation as...";

            if (openExeDialog.ShowDialog() == DialogResult.OK)
            {
                AgsTranslation.Gameinfo info = AgsTranslation.GetGameInfo(openExeDialog.FileName);

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    if (saveDialog.ShowDialog() == DialogResult.OK)
                        AgsTranslation.CreateTraFile(info, saveDialog.FileName,
                            AgsTranslation.ParseTrsTranslation(openDialog.FileName));
                }
            }
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.ShowDialog();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
           MessageBox.Show("This functionality is not yet implemented. We are sorry.", "Not available", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void xmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Save changes then exit
            if (translationGrid.Rows.Count > 0)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.DefaultExt = "xml";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    int counter = 0;

                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Encoding = Encoding.ASCII;
                    settings.Indent = true;

                    XmlWriter writer = XmlWriter.Create(saveDialog.FileName, settings);

                    writer.WriteStartDocument();
                    writer.WriteStartElement("AGSTranslationScript");

                    foreach (DataGridViewRow row in translationGrid.Rows)
                    {
                        writer.WriteStartElement("Entry" + counter);
                        writer.WriteElementString("SourceText", (string) row.Cells[0].Value);
                        writer.WriteElementString("TranslatedText", (string) row.Cells[1].Value);
                        writer.WriteEndElement();
                        counter++;
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Close();
                }
            }
        }

        private void csvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (translationGrid.Rows.Count > 0)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.DefaultExt = "csv";
                saveDialog.AddExtension = true;

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(saveDialog.FileName,FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    foreach (DataGridViewRow row in translationGrid.Rows)
                    {
                        sw.Write("{0};{1}\n",row.Cells[0].Value, row.Cells[1].Value);
                    }


                    sw.Close();
                    fs.Close();
                }
            }
        }

        private void textToolStripMenuItem_Click(object sender, EventArgs e) {
            var searchForm = new SearchTextForm();
            searchForm.Show(this);
        }

        private void rowToolStripMenuItem_Click(object sender, EventArgs e) {
            var searchForm = new GoToRowForm();
            searchForm.Show(this);
        }

    }

}
