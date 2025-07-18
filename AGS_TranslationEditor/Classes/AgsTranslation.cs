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
    class AgsTranslation
    {
        //Encryption string
        private static readonly char[] pwdEncChars = { 'A','v','i','s',' ','D','u','r','g','a','n' };
        private static readonly byte[] pwdEncBytes = { 0x41, 0x76, 0x69, 0x73, 0x20, 0x44, 0x75, 0x72, 0x67, 0x61, 0x6e };
        private static Dictionary<string, string> translationLines;
        
        /// <summary>
        /// Reads and parses a TRA file
        /// </summary>
        /// <param name="filename">Filename</param>
        /// <returns>A Dictionary with the translation entries</returns>
        public static Dictionary<string,string> ParseTraTranslation(string filename)
        {
            Encoding encoding = Encoding.UTF7;
            bool decrypt = true;

            using (FileStream fs = File.OpenRead(filename))
            {
                BinaryReader br = new BinaryReader(fs, encoding);
                translationLines = new Dictionary<string, string>();

                //Translation File Signature
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
                        if (decrypt) {
                            DecryptBytes(bGameTitle);
                        }
                        char[] cGameTitle = encoding.GetChars(bGameTitle);
                        string sGameTitle = new string(cGameTitle).Trim('\0');

                        //dummy read
                        br.ReadInt32();
                        //calculate Translation length
                        long translationLength = br.ReadInt32() + fs.Position;

                        int i = 0;
                        //Loop throught File and decrypt entries
                        while (fs.Position < translationLength)
                        {
                            int newlen = br.ReadInt32();

                            //Read original Text
                            byte[] bSourceBytes = br.ReadBytes(newlen);
                            if (decrypt) {
                                DecryptBytes(bSourceBytes);
                            }
                            char[] cSourceText = encoding.GetChars(bSourceBytes);
                            string sDecSourceText = new string(cSourceText).Trim('\0');

                            //Read Translated Text
                            newlen = br.ReadInt32();
                            byte[] bTranslatedBytes = br.ReadBytes(newlen);
                            if (decrypt) {
                                DecryptBytes(bTranslatedBytes);
                            }
                            char[] cTranslatedText = encoding.GetChars(bTranslatedBytes, 0, newlen);
                            string sDecTranslatedText = new string(cTranslatedText).Trim('\0');

                            //Populate List with the data
                            if (!translationLines.ContainsKey(sDecSourceText))
                            {
                                translationLines.Add(sDecSourceText, sDecTranslatedText);
                            }
                            else
                            {
                                //Entry already in dictionary
                            }
                            i++;
                        }

                        //Close File
                        br.Close();
                        fs.Close();
                        return translationLines;
                    }
                    else if (blockType == 3)
                    {
                        //Not used
                    }
                }
                return translationLines;
            }
        }

        /// <summary>
        /// Parse a TRS file for AGS
        /// </summary>
        /// <param name="filename">Input filename</param>
        /// <returns>Dictionary with Translation entries</returns>
        public static Dictionary<string,string> ParseTrsTranslation(string filename)
        {
            string[] list = File.ReadAllLines(filename);
            translationLines = new Dictionary<string, string>();

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

                if (!translationLines.ContainsKey(sSourceText))
                {
                    translationLines.Add(sSourceText, sTranslationText);
                }
                else
                {
                    //MessageBox.Show("Entry already in Dictionary!",string.Format("Key already available: {0}", sSourceText));
                }
            }
            return translationLines;
        }

        static byte[] CharsToBytes(char[] chars)
        {
            byte[] bytes = new byte[chars.Length];
            int x = 0;
            foreach (char c in chars)
            {
                bytes[x] = (byte)c;
                x++;
            }

            return bytes;
        }

        /// <summary>
        /// Create a TRA File for AGS
        /// </summary>
        /// <param name="info">Game Information like Title,UID</param>
        /// <param name="filename">Output filename</param>
        /// <param name="entryList">List with Translation entries</param>
        public static void CreateTraFile(GameInfo info, string filename, Dictionary<string,string> entryList)
        {

            Encoding encoding = Encoding.Default; //GetEncoding(1252); //Encoding.UTF8;
            bool encrypt = true;

            using (FileStream fs = new FileStream(filename,FileMode.Create))
            {
                //Tail
                byte[] tail =
                {
                0x01, 0x00, 0x00, 0x00, 0x41, 0x01, 0x00, 0x00, 0x00, 0x41, 0x03, 0x00, 0x00, 0x00, 0x0C, 0x00,
                0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00,
                };

                //Write always header "AGSTranslation\0"
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
                char[] cGameTitle = GameTitle.ToCharArray();
                
                //Write GameTitle Length
                byte[] bGameTitleLength = BitConverter.GetBytes(GameTitle.Length);
                fs.Write(bGameTitleLength, 0, bGameTitleLength.Length);

                //Write the encrypted GameTitle
                byte[] bGameTitle = CharsToBytes(cGameTitle);
                if (encrypt) {
                    EncryptBytes(bGameTitle);
                }
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
                            //Get original string
                            string entry1 = pair.Key;
                            entry1 = entry1 + "\0";

                            //Write original string length
                            byte[] bEntry1Length = BitConverter.GetBytes(entry1.Length);
                            fs.Write(bEntry1Length, 0, bEntry1Length.Length);

                            //Write original string bytes
                            char[] cEntry1 = entry1.ToCharArray();
                            byte[] bEntry1 = CharsToBytes(cEntry1);
                            if (encrypt) {
                                EncryptBytes(bEntry1);
                            }
                            fs.Write(bEntry1, 0, bEntry1.Length);

                            //Get translation string  
                            string entry2 = pair.Value;
                            entry2 = entry2 + "\0";

                            //Write translation string length
                            byte[] bEntry2Length = BitConverter.GetBytes(entry2.Length);
                            fs.Write(bEntry2Length, 0, bEntry2Length.Length);

                            //Write translation string bytes
                            char[] cEntry2 = entry2.ToCharArray();
                            byte[] bEntry2 = CharsToBytes(cEntry2);
                            if (encrypt) {
                                EncryptBytes(bEntry2);
                            }
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
        /// Decrypt a byte array
        /// </summary>
        /// <param name="toEnc">byte array to decrypt</param>
        public static void DecryptBytes(byte[] toEnc) {
            int adx = 0;
            int toencx = 0;

            while (toencx < toEnc.Length) {

                toEnc[toencx] -= pwdEncBytes[adx];

                adx++;
                toencx++;

                if (adx > 10)
                    adx = 0;
            }
        }

        /// <summary>
        /// Decrypt a char array
        /// </summary>
        /// <param name="toEnc">char array to decrypt</param>
        public static void DecryptChars(char[] toEnc)
        {
            int adx = 0;
            int toencx = 0;

            while (toencx < toEnc.Length)
            {
                /*if (toEnc[toencx] == 0)
                    break;*/

                if (toEnc[toencx] - pwdEncChars[adx] < 0) {
                    toEnc[toencx] += (char)256;
                }
                toEnc[toencx] -= pwdEncChars[adx];

                adx++;
                toencx++;

                if (adx > 10)
                    adx = 0;
            }
        }

        /// <summary>
        /// Encrypt a byte array
        /// </summary>
        /// <param name="toenc">byte array to encrypt</param>
        public static void EncryptBytes(byte[] toenc) {
            int adx = 0;
            int toencx = 0;

            while (toencx < toenc.Length) {
                toenc[toencx] += pwdEncBytes[adx];
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
        public static void EncryptChars(char[] toenc)
        {
            int adx = 0;
            int toencx = 0;

            while (toencx < toenc.Length)
            {
                toenc[toencx] += pwdEncChars[adx];
                adx++;
                toencx++;

                if (adx > 10)
                    adx = 0;
            }
        }

        public class GameInfo
        {
            public string Version { get; set; }
            public string GameTitle { get; set; }
            public string GameUID { get; set; }
        }

        /// <summary>
        /// Get Game information (GameTitle and GameUID) from AGS EXE File
        /// </summary>
        /// <param name="filename">Game EXE File</param>
        public static GameInfo GetGameInfo(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                //The string we want to search in the AGS Game File or Executable
                String searchString = "Adventure Creator Game File v2";
                        
                // Gameinfo class to hold the information
                GameInfo info = new GameInfo();

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
                        int startPosition = tempData.IndexOf(searchString, 0, StringComparison.Ordinal);
                        //Calculate and set the position to start reading
                        startPosition = startPosition + 0x1E + (int)position;
                        fs.Position = startPosition;

                        byte versionNextByte = br.ReadByte();
                        if (versionNextByte == 0x00) {
                            position = startPosition + 1;
                            continue;
                        }

                        //Dummy read 3 bytes
                        br.ReadBytes(3);

                        //Get the AGS version the game was compiled with
                        int versionStringLength = br.ReadInt32();
                        info.Version = new string(br.ReadChars(versionStringLength));

                        //fix for newer versions (haven't found a proper pattern)
                        Char versionNext = (char)versionNextByte;
                        if (versionNext == '1' || versionNext == '2' || versionNext == '5')
                            br.ReadInt32();

                        //Calculate and save GameUID position for later use
                        long gameUIDPosition = fs.Position + 0x6f4;

                        //Get the game title
                        string gameTitle = new string(br.ReadChars(0x40));
                        info.GameTitle = gameTitle.Substring(0, gameTitle.IndexOf("\0", StringComparison.Ordinal));

                        //Read the GameUID
                        fs.Position = gameUIDPosition;
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