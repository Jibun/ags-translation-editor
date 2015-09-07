using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace AGS_TranslationEditor
{
    internal class AGS_Translation
    {
        private static readonly char[] _passwencstring = { 'A', 'v', 'i', 's', ' ', 'D', 'u', 'r', 'g', 'a', 'n' };
        //private string _fileName;
        private static Dictionary<string, string> _translatedLines;

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
                _translatedLines = new Dictionary<string, string>();

                long sizeFile = fs.Length;

                char[] transsig = new char[16];
                transsig = br.ReadChars(15);

                //Check AGS Translation Header
                if (string.Compare(new string(transsig), "AGSTranslation") == 0)
                {
                    //Read Translation File BlockType for Example 1,2,3
                    int blockType = br.ReadInt32();
                    if (blockType == 1)
                    {

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
                        int newlen = 0;
                        while (fs.Position < translationLength)
                        {
                            newlen = br.ReadInt32();

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
                            if (!_translatedLines.ContainsKey(sDecSourceText))
                            {
                                _translatedLines.Add(sDecSourceText, sDecTranslatedText);
                            }
                        }
                        fs.Close();
                        return _translatedLines;

                    }
                    else if (blockType == 3)
                    {
                        /*// game settings
                    int temp = language_file->ReadInt32();
                    // normal font
                    if (temp >= 0)
                        SetNormalFont(temp);
                    temp = language_file->ReadInt32();
                    // speech font
                    if (temp >= 0)
                        SetSpeechFont(temp);
                    temp = language_file->ReadInt32();
                    // text direction
                    if (temp == 1)
                    {
                        play.text_align = SCALIGN_LEFT;
                        game.options[OPT_RIGHTLEFTWRITE] = 0;
                    }
                    else if (temp == 2)
                    {
                        play.text_align = SCALIGN_RIGHT;
                        game.options[OPT_RIGHTLEFTWRITE] = 1;
                    }
                     */
                    }
                }
                return _translatedLines;
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
            _translatedLines = new Dictionary<string, string>();

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

                if (!_translatedLines.ContainsKey(sSourceText))
                {
                    _translatedLines.Add(sSourceText, sTranslationText);
                }
                else
                {
                    //MessageBox.Show("Entry already in Dictionary!",string.Format("Key already available: {0}", sSourceText));
                }
            }
            return _translatedLines;
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
            using (FileStream fs = new FileStream(filename,FileMode.Open))
            {
                //The string we want to search in the AGS Game executable
                const string search_string = "Adventure Creator Game File v2";
                // Gameinfo class to hold the information
                Gameinfo info = new Gameinfo();

                const int block_size = 1024;
                long file_size = fs.Length;
                long position = 0;
                                
                //Read AGS EXE and search for string, should actually never reach the end 
                BinaryReader br = new BinaryReader(fs);
                while (position < file_size)
                {
                    byte[] data = br.ReadBytes(block_size);
                    string temp_data = Encoding.Default.GetString(data);

                    //If the search string is found get the game info
                    if (temp_data.Contains(search_string))
                    {
                        int pos = temp_data.IndexOf(search_string,0);
                        //Calculate and set the position to start reading
                        pos = pos + 0x1E + (int)position;
                        fs.Position = pos;

                        //Dummy read 4 bytes
                        br.ReadInt32();
                        int version_string_length = br.ReadInt32();

                        //Get the AGS version the game was compiled with
                        info.Version = new string(br.ReadChars(version_string_length));

                        //Calculate and save GameUID position for later use
                        long gameuid_pos = fs.Position + 0x6f4;

                        //Get the game title
                        string GameTitle = new string(br.ReadChars(0x40));
                        info.GameTitle = GameTitle.Substring(0, GameTitle.IndexOf("\0"));

                        //Read the GameUID
                        fs.Position = gameuid_pos;
                        int GameUID = br.ReadInt32();
                        GameUID = SwapEndianness(GameUID);
                        string sGameUID = GameUID.ToString("X");
                        info.GameUID = sGameUID;

                        //return the Game information
                        return info;
                    }
                    //Calculate new postiton
                    position = position + block_size;
                }
            }

            //if nothing found return just null
            return null;
        }

        /// <summary>
        /// Create a TRA File for AGS
        /// </summary>
        /// <param name="filename">Output filename</param>
        /// <param name="entries">List with Translation entries</param>
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
                byte[] AGSHeader =
                {0x41, 0x47, 0x53, 0x54, 0x72, 0x61, 0x6E, 0x73, 0x6C, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x00,};
                fs.Write(AGSHeader,0,AGSHeader.Length);

                //Padding not sure what exactly this is
                byte[] paddingBytes = {0x02, 0x00, 0x00, 0x00, 0x16, 0x00, 0x00, 0x00,};
                fs.Write(paddingBytes,0,paddingBytes.Length);

                //Write GameUID important or Translation does not load properly!
                string GameUID = info.GameUID;
                int decAgain = int.Parse(GameUID, System.Globalization.NumberStyles.HexNumber);
                byte[] bGameUID = BitConverter.GetBytes(SwapEndianness(decAgain));
                fs.Write(bGameUID,0,bGameUID.Length);

                //Write GameTitle
                string GameTitle = info.GameTitle + "\0";
                byte[] bGameTitle = Encoding.UTF8.GetBytes(GameTitle);
                //byte[] bGameTitle = Encoding.ASCII.GetBytes(GameTitle);
                char[] cGameTitle = new char[bGameTitle.Length];
                //Write Title Length
                byte[] bGameTitleLength = BitConverter.GetBytes(bGameTitle.Length);
                fs.Write(bGameTitleLength, 0, bGameTitleLength.Length);
                //Encrypt and write the Title
                Array.Copy(bGameTitle, cGameTitle, bGameTitle.Length);
                encrypt_text(cGameTitle);
                int i = 0;
                foreach (char c in cGameTitle)
                {
                    bGameTitle[i] = (byte)c;
                    i++;
                }
                fs.Write(bGameTitle, 0, bGameTitle.Length);

                //dummy write
                //Length: 4 / 0x00000004 (bytes)
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
                            //byte[] bEntry1 = Encoding.ASCII.GetBytes(entry1);

                            //Write string entry1 length
                            byte[] bEntry1Length = BitConverter.GetBytes(bEntry1.Length);
                            fs.Write(bEntry1Length, 0, bEntry1Length.Length);

                            char[] cEntry1 = new char[bEntry1.Length];
                            Array.Copy(bEntry1, cEntry1, bEntry1.Length);
                            encrypt_text(cEntry1);
                            int x = 0;
                            foreach (char c in cEntry1)
                            {
                                bEntry1[x] = (byte) c;
                                x++;
                            }
                            fs.Write(bEntry1, 0, bEntry1.Length);

                            //Encrypt Entry2 write length  
                            string entry2 = pair.Value;
                            entry2 = entry2 + "\0";
                            byte[] bEntry2 = Encoding.UTF8.GetBytes(entry2);
                            //byte[] bEntry2 = Encoding.ASCII.GetBytes(entry2);

                            //Write string entry2 length
                            byte[] bEntry2Length = BitConverter.GetBytes(bEntry2.Length);
                            fs.Write(bEntry2Length, 0, bEntry2Length.Length);

                            char[] cEntry2 = new char[bEntry2.Length];
                            Array.Copy(bEntry2, cEntry2, bEntry2.Length);
                            encrypt_text(cEntry2);
                            x = 0;
                            foreach (char c in cEntry2)
                            {
                                bEntry2[x] = (byte) c;
                                x++;
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

                toEnc[toencx] -= _passwencstring[adx];

                adx++;
                toencx++;

                if (adx > 10)
                    adx = 0;
            }
        }

        public static byte[] decrypt_text(string toEnc)
        {
            string text = toEnc + "\0";
            byte[] btext = Encoding.UTF8.GetBytes(text);
            char[] ctext = new char[btext.Length];
            //Encrypt and write the Title
            Array.Copy(btext, ctext, btext.Length);
            decrypt_text(ctext);
            int i = 0;
            foreach (char c in ctext)
            {
                btext[i] = (byte)c;
                i++;
            }

            return btext;
        }


        /// <summary>
        /// Encrypt a char array
        /// </summary>
        /// <param name="toenc">char array to encrypt</param>
        public static void encrypt_text(char[] toenc)
        {
            int adx = 0;//, tobreak = 0;
            int toencx = 0;

            //while (tobreak == 0)
            while (toencx < toenc.Length)
            {
                /*if (toenc[toencx] == 0)
                    tobreak = 1;
                    */
                toenc[toencx] += _passwencstring[adx];
                adx++;
                toencx++;

                if (adx > 10)
                    adx = 0;
            }
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