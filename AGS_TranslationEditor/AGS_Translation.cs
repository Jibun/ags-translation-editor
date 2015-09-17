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
using System.IO;
using System.Text;

namespace AGS_TranslationEditor
{
    class AGS_Translation
    {
        //Encryption string
        private static readonly char[] _passwEncString = { 'A','v','i','s',' ','D','u','r','g','a','n' };
        private static Dictionary<string, string> _transLines;
        
        /// <summary>
        /// Reads and parses a TRA file
        /// </summary>
        /// <param name="filename">Filename</param>
        /// <returns>A Dictionary with the translation entries</returns>
        public static Dictionary<string,string> ParseTRA_Translation(string filename)
        {
            using (FileStream fs = File.OpenRead(filename))
            {
                BinaryReader br = new BinaryReader(fs);
                _transLines = new Dictionary<string, string>();

                //Tranlsation File Signature
                char[] transsig = new char[16];
                transsig = br.ReadChars(15);
                //Check AGS Translation Header
                if (string.Compare(new string(transsig), "AGSTranslation") == 0)
                {
                    //Read Translation File BlockType for Example 1,2,3
                    int blockType = br.ReadInt32();
                    if (blockType == 1)
                    {
                        //Not used
                    }
                    else if (blockType == 2)
                    {
                        //Dummy Read
                        br.ReadInt32();
                        //Read GameID
                        int iGameUID = br.ReadInt32();
                        //Get GameTitle
                        int GameTitleLength = br.ReadInt32();
                        byte[] bGameTitle = br.ReadBytes(GameTitleLength);
                        char[] cGameTitle = Encoding.UTF7.GetChars(bGameTitle);
                        //Game Name
                        decrypt_text(cGameTitle);
                        string sGameTitle = new string(cGameTitle);

                        //dummy read
                        br.ReadInt32();
                        //calculate Translation length
                        long translationLength = br.ReadInt32() + fs.Position;

                        //Loop throught File and decrypt entries
                        while (fs.Position < translationLength)
                        {
                            int newlen = br.ReadInt32();

                            //Read original Text
                            byte[] bSourceBytes = br.ReadBytes(newlen);
                            char[] cSourceText = Encoding.UTF7.GetChars(bSourceBytes);
                            decrypt_text(cSourceText);
                            string sDecSourceText = new string(cSourceText).Trim('\0');

                            //Read Translated Text
                            newlen = br.ReadInt32();
                            byte[] bTranslatedBytes = br.ReadBytes(newlen);
                            char[] cTranslatedText = Encoding.UTF7.GetChars(bTranslatedBytes);
                            decrypt_text(cTranslatedText);
                            string sDecTranslatedText = new string(cTranslatedText).Trim('\0');

                            //Populate List with the data
                            if (!_transLines.ContainsKey(sDecSourceText))
                            {
                                _transLines.Add(sDecSourceText, sDecTranslatedText);
                            }
                            else
                            {
                                //Entry already in dictionary
                            }
                        }

                        //Close File
                        br.Close();
                        fs.Close();
                        return _transLines;
                    }
                    else if (blockType == 3)
                    {
                        //Not used
                    }
                }
                return _transLines;
            }
        }

        /// <summary>
        /// Parse a TRS file for AGS
        /// </summary>
        /// <param name="filename">Input filename</param>
        /// <returns>Dictionary with Translation entries</returns>
        public static Dictionary<string,string> ParseTRS_Translation(string filename)
        {
            string[] list = File.ReadAllLines(filename);
            _transLines = new Dictionary<string, string>();

            //Look for comments and remove them
            var result = Array.FindAll(list, s => !s.StartsWith("//"));

            for (int i = 0; i < result.Length;)
            {
                string sSourceText = result[i];
                i++;
                string sTranslationText = "";
                if (i < result.Length)
                {
                    sTranslationText = result[i];
                    i++;
                }

                if (!_transLines.ContainsKey(sSourceText))
                {
                    _transLines.Add(sSourceText, sTranslationText);
                }
                else
                {
                    //MessageBox.Show("Entry already in Dictionary!",string.Format("Key already available: {0}", sSourceText));
                }
            }
            return _transLines;
        }

        static void ConvertCharToByte(char[] chars, byte[] bytes)
        {
            int x = 0;
            foreach (char c in chars)
            {
                bytes[x] = (byte)c;
                x++;
            }
        }

        /// <summary>
        /// Create a TRA File for AGS
        /// </summary>
        /// <param name="info">Game Information like Title,UID</param>
        /// <param name="filename">Output filename</param>
        /// <param name="entryList">List with Translation entries</param>
        public static void CreateTRA_File(Gameinfo info, string filename, Dictionary<string,string> entryList)
        {
            using (FileStream fs = new FileStream(filename,FileMode.Create))
            {
                //Tail
                byte[] tail =
                {
                0x01, 0x00, 0x00, 0x00, 0x41, 0x01, 0x00, 0x00, 0x00, 0x41, 0x03, 0x00, 0x00, 0x00, 0x0C, 0x00,
                0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00,
                };

                //Write always header "AGSTranslation\0
                byte[] agsHeader =
                {0x41, 0x47, 0x53, 0x54, 0x72, 0x61, 0x6E, 0x73, 0x6C, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x00,};
                fs.Write(agsHeader,0,agsHeader.Length);

                //Padding not sure what exactly this is
                byte[] paddingBytes = {0x02, 0x00, 0x00, 0x00, 0x16, 0x00, 0x00, 0x00,};
                fs.Write(paddingBytes,0,paddingBytes.Length);

                //Write GameUID important or Translation does not load properly!
                string sGameUID = info.GameUID;
                int decAgain = int.Parse(sGameUID, System.Globalization.NumberStyles.HexNumber);
                byte[] bGameUID = BitConverter.GetBytes(SwapEndianness(decAgain));
                fs.Write(bGameUID,0,bGameUID.Length);

                //Encrypt and write the Title
                string GameTitle = info.GameTitle + "\0";
                byte[] bGameTitle = Encoding.UTF8.GetBytes(GameTitle);
                char[] cGameTitle = new char[GameTitle.Length];
                GameTitle.CopyTo(0, cGameTitle, 0, GameTitle.Length);
                encrypt_text(cGameTitle);
                //Write GameTitle Length
                byte[] bGameTitleLength = BitConverter.GetBytes(bGameTitle.Length);
                fs.Write(bGameTitleLength, 0, bGameTitleLength.Length);
                //Write the encrypted GameTitle
                ConvertCharToByte(cGameTitle, bGameTitle);
                fs.Write(bGameTitle, 0, bGameTitle.Length);

                //dummy write
                byte[] bDummy = {0x01, 0x00, 0x00, 0x00,};
                fs.Write(bDummy, 0, bDummy.Length);

                //Write Length translation
                long translationLengthPosition = fs.Position;
                //Dummy write for later
                fs.Write(bDummy,0,bDummy.Length);
                
                long translationLength = 0;

                if (entryList.Count > 0)
                {
                    foreach (KeyValuePair<string,string> pair in entryList)
                    {
                        if (!string.Equals(pair.Value, ""))
                        {
                            //encrypt string write length  
                            string entry1 = pair.Key;
                            entry1 = entry1 + "\0";

                            byte[] bEntry1 = Encoding.UTF8.GetBytes(entry1);
                            byte[] bEntry6 = Encoding.UTF7.GetBytes(entry1);
                            byte[] bEntry5 = Encoding.ASCII.GetBytes(entry1);

                            //Write string entry1 length
                            byte[] bEntry1Length = BitConverter.GetBytes(bEntry1.Length);
                            fs.Write(bEntry1Length, 0, bEntry1Length.Length);

                            char[] cEntry1 = new char[bEntry1.Length];
                            Array.Copy(bEntry1, cEntry1, bEntry1.Length);

                            encrypt_text(cEntry1);
                            ConvertCharToByte(cEntry1,bEntry1);
                            fs.Write(bEntry1, 0, bEntry1.Length);

                            //Encrypt Entry2 and write length  
                            string entry2 = pair.Value;
                            entry2 = entry2 + "\0";
                            byte[] bEntry2 = Encoding.UTF8.GetBytes(entry2);

                            //Write string entry2 length
                            byte[] bEntry2Length = BitConverter.GetBytes(bEntry2.Length);
                            fs.Write(bEntry2Length, 0, bEntry2Length.Length);

                            char[] cEntry2 = new char[bEntry2.Length];
                            Array.Copy(bEntry2, cEntry2, bEntry2.Length);
                            encrypt_text(cEntry2);
                            ConvertCharToByte(cEntry2,bEntry2);
                            fs.Write(bEntry2, 0, bEntry2.Length);

                            long lengthTemp = BitConverter.ToInt32(bEntry1Length, 0) + 4 +
                                              BitConverter.ToInt32(bEntry2Length, 0) + 4;
                            translationLength = translationLength + lengthTemp;
                        }
                    }
                        //Write Tail
                        fs.Write(tail, 0, tail.Length);

                        //Write Translation length + 10
                        byte[] b = BitConverter.GetBytes((int) (translationLength + 10));
                        fs.Position = translationLengthPosition;
                        fs.Write(b, 0, b.Length);

                        fs.Close();
                }
            }           
        }

        /// <summary>
        /// Decrypt a char array
        /// </summary>
        /// <param name="toEnc">char array to decrypt</param>
        public static void decrypt_text(char[] toEnc)
        {
            int adx = 0;
            int toencx = 0;

            while (toencx < toEnc.Length)
            {
                if (toEnc[toencx] == 0)
                    break;

                toEnc[toencx] -= _passwEncString[adx];

                adx++;
                toencx++;

                if (adx > 10)
                    adx = 0;
            }
        }

        /// <summary>
        /// Encrypt a char array
        /// </summary>
        /// <param name="toenc">char array to encrypt</param>
        public static void encrypt_text(char[] toenc)
        {
            int adx = 0;
            int toencx = 0;

            while (toencx < toenc.Length)
            {
                toenc[toencx] += _passwEncString[adx];
                adx++;
                toencx++;

                if (adx > 10)
                    adx = 0;
            }
        }


        public class Gameinfo
        {
            public string Version { get; set; }
            public string GameTitle { get; set; }
            public string GameUID { get; set; }
        }

        /// <summary>
        /// Get Game information (GameTitle and GameUID) from AGS EXE File
        /// </summary>
        /// <param name="filename">Game EXE File</param>
        public static Gameinfo GetGameInfo(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                //The string we want to search in the AGS Game executable
                const string searchString = "Adventure Creator Game File v2";
                // Gameinfo class to hold the information
                Gameinfo info = new Gameinfo();

                const int blockSize = 1024;
                long fileSize = fs.Length;
                long position = 0;

                //Read AGS EXE and search for string, should actually never reach the end 
                BinaryReader br = new BinaryReader(fs);
                while (position < fileSize)
                {
                    byte[] data = br.ReadBytes(blockSize);
                    string tempData = Encoding.Default.GetString(data);

                    //If the search string is found get the game info
                    if (tempData.Contains(searchString))
                    {
                        int pos = tempData.IndexOf(searchString, 0);
                        //Calculate and set the position to start reading
                        pos = pos + 0x1E + (int)position;
                        fs.Position = pos;

                        //Dummy read 4 bytes
                        br.ReadInt32();
                        int versionStringLength = br.ReadInt32();

                        //Get the AGS version the game was compiled with
                        info.Version = new string(br.ReadChars(versionStringLength));

                        //Calculate and save GameUID position for later use
                        long gameuidPos = fs.Position + 0x6f4;

                        //Get the game title
                        string gameTitle = new string(br.ReadChars(0x40));
                        info.GameTitle = gameTitle.Substring(0, gameTitle.IndexOf("\0"));

                        //Read the GameUID
                        fs.Position = gameuidPos;
                        int GameUID = br.ReadInt32();
                        GameUID = SwapEndianness(GameUID);
                        info.GameUID = GameUID.ToString("X");

                        //return the Game information
                        return info;
                    }
                    //Calculate new postiton
                    position = position + blockSize;
                }
            }

            //if nothing found return just null
            return null;
        }

        /// <summary>
        /// Help function to swap between endianns
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Value to swap</returns>
        private static int SwapEndianness(int value)
        {
            var b1 = (value >> 0) & 0xff;
            var b2 = (value >> 8) & 0xff;
            var b3 = (value >> 16) & 0xff;
            var b4 = (value >> 24) & 0xff;

            return b1 << 24 | b2 << 16 | b3 << 8 | b4 << 0;
        }

    }
}