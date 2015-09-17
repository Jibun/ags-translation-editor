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
