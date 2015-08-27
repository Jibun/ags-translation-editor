using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AGS_TranslationEditor
{
    internal class AGS_Translation
    {
        private static readonly char[] _passwencstring = { 'A', 'v', 'i', 's', ' ', 'D', 'u', 'r', 'g', 'a', 'n' };

        /// <summary>
        /// Reads and parses a TRA file
        /// </summary>
        /// <param name="filename">Filename</param>
        /// <returns>A ArrayList with the translation entries</returns>
        public static List<string[]> ParseTRA_Translation(string filename)
        {
            int iGameUID = 0;
            List<string[]> entryList = new List<string[]>();
            FileStream fs = File.OpenRead(filename);
            BinaryReader br = new BinaryReader(fs);
            
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
                    iGameUID = br.ReadInt32();

                    //Get GameTitle
                    int GameTitleLength = br.ReadInt32();
                    byte[] bGameTitle = br.ReadBytes(GameTitleLength);
                    char[] cGameTitle = new char[GameTitleLength];
                    Array.Copy(bGameTitle, 0, cGameTitle, 0, bGameTitle.Length);
                    //Game Name
                    decrypt_text(cGameTitle);
                    string sGameTitle = new string(cGameTitle);
                    //sGameTitle = sGameTitle.Trim('\0');

                    //dummy read
                    br.ReadInt32();

                    // Translation Entries
                    long translationLength = br.ReadInt32();
                    translationLength += fs.Position;

                    //Loop throught File and read entries
                    int newlen = 0;
                    while (fs.Position < translationLength)
                    {
                        newlen = br.ReadInt32();
                        
                        //Read original Text
                        byte[] bSourceBytes = br.ReadBytes(newlen);
                        char[] cSourceText = new char[bSourceBytes.Length + 1];
                        Array.Copy(bSourceBytes, 0, cSourceText, 0, bSourceBytes.Length);
                        decrypt_text(cSourceText);
                        string sDecSourceText = new string(cSourceText);
                        sDecSourceText = sDecSourceText.Trim('\0');

                        //Read Translated Text
                        newlen = br.ReadInt32();
                        byte[] bTranslatedBytes = br.ReadBytes(newlen);
                        char[] cTranslatedText = new char[bTranslatedBytes.Length + 1];
                        Array.Copy(bTranslatedBytes, 0, cTranslatedText, 0, bTranslatedBytes.Length);
                        decrypt_text(cTranslatedText);
                        string sDecTranslatedText = new string(cTranslatedText);
                        sDecTranslatedText = sDecTranslatedText.Trim('\0');
                        
                        //Populate List with the data
                        string[] newRow = {sDecSourceText, sDecTranslatedText};
                        entryList.Add(newRow);
                    }
                    fs.Close();
                    return entryList;
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
            return entryList;
        }

        /// <summary>
        /// Parse a TRS file for AGS
        /// </summary>
        /// <param name="filename">Input filename</param>
        /// <returns>List with Translation entries</returns>
        public static List<string[]> ParseTRS_Translation(string filename)
        {
            List<string[]> entryList = new List<string[]>();
            string[] list = File.ReadAllLines(filename);

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

                string[] newRow = { sSourceText, sTranslationText };
                entryList.Add(newRow);
            }
            return entryList;
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
        public static Gameinfo GetGamedata(string filename)
        {
            using (FileStream fs = new FileStream(filename,FileMode.Open))
            {
                Gameinfo info = new Gameinfo();

                const int block_size = 1024;
                long file_size = fs.Length;
                long position = 0;
                const string search_string = "Adventure Creator Game File v2";

                BinaryReader br = new BinaryReader(fs);

                while (position < file_size)
                {
                    byte[] data = br.ReadBytes(block_size);
                    
                    string temp_data = Encoding.Default.GetString(data);

                    if (temp_data.Contains(search_string))
                    {
                        int pos = temp_data.IndexOf(search_string,0);
                        pos = pos + 0x1E + (int)position;
                        fs.Position = pos;

                        //Dummy read 4 bytes
                        br.ReadInt32();
                        int version_string_length = br.ReadInt32();

                        //Get the AGS version the game was compiled with
                        string version = new string(br.ReadChars(version_string_length));
                        info.Version = version;

                        //save GameUID position for later
                        long gameuid_pos = fs.Position + 0x6f4;

                        //Get the game title
                        string GameTitle = new string(br.ReadChars(0x40));
                        GameTitle = GameTitle.Substring(0,GameTitle.IndexOf("\0"));
                        info.GameTitle = GameTitle;

                        //Read the GameUID
                        fs.Position = gameuid_pos;
                        int GameUID = br.ReadInt32();
                        GameUID = SwapEndianness(GameUID);
                        string sGameUID = GameUID.ToString("X");
                        info.GameUID = sGameUID;

                        MessageBox.Show(
                            "AGS Version: " + version + "\nGame Title: " + GameTitle + "\nGameUID: " + sGameUID,
                            "Game Information");

                        return info;
                    }

                    position = position + block_size;
                }
            }

            //nothing found
            return null;
        }

        /// <summary>
        /// Create a TRA File for AGS
        /// </summary>
        /// <param name="filename">Output filename</param>
        /// <param name="entries">List with Translation entries</param>
        public static void CreateTRA_File(Gameinfo info, string filename, List<string[]> entries)
        {
            using (FileStream fs = new FileStream(filename,FileMode.Create))
            {
                //Tail
                //Length: 38 / 0x00000026 (bytes)
                byte[] tail =
                {
                0x01, 0x00, 0x00, 0x00, 0x41, 0x01, 0x00, 0x00, 0x00, 0x41, 0x03, 0x00, 0x00, 0x00, 0x0C, 0x00,
                0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00,
                };

                //Write header "AGSTranslation\0
                //Length: 8 / 0x00000008 (bytes)
                byte[] AGSHeader =
                {
                    0x41, 0x47, 0x53, 0x54, 0x72, 0x61, 0x6E, 0x73, 0x6C, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x00,
                };
                fs.Write(AGSHeader,0,AGSHeader.Length);

                //Padding
                //Length: 8 / 0x00000008 (bytes)
                byte[] paddingBytes = {0x02, 0x00, 0x00, 0x00, 0x16, 0x00, 0x00, 0x00,};
                fs.Write(paddingBytes,0,paddingBytes.Length);

                //Write GameUID important or Translation doesnt load!
                //byte[] GameUID = Encoding.UTF8.GetBytes("751C1200");
                string GameUID = "751C1200";
                GameUID = info.GameUID;
                int decAgain = int.Parse(GameUID, System.Globalization.NumberStyles.HexNumber);
                byte[] bGameUID = BitConverter.GetBytes(SwapEndianness(decAgain));
                fs.Write(bGameUID,0,bGameUID.Length);

                //Write GameTitle
                string GameTitle = "Technobabylon\0";
                GameTitle = info.GameTitle + "\0";
                byte[] bGameTitle = Encoding.UTF8.GetBytes(GameTitle);
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

                //string dummy = "01000000";
                //decAgain = int.Parse(dummy, System.Globalization.NumberStyles.HexNumber);
                //byte[] bDummy = BitConverter.GetBytes(SwapEndianness(decAgain));
                fs.Write(bDummy, 0, bDummy.Length);

                //Write Length translation
                long translationLengthPosition = fs.Position;
                //Dummy write for later
                fs.Write(bDummy,0,bDummy.Length);
                
                long translationLength = 0;

                if (entries != null)
                {
                    foreach (string[] entry in entries)
                    {
                        if (!string.Equals(entry[1], ""))
                        {
                            //encrypt string write length  
                            string entry1 = entry[0];
                            entry1 = entry1 + "\0";
                            byte[] bEntry1 = Encoding.UTF8.GetBytes(entry1);

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
                            string entry2 = entry[1];
                            entry2 = entry2 + "\0";
                            byte[] bEntry2 = Encoding.UTF8.GetBytes(entry2);

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

        private static int SwapEndianness(int value)
        {
            var b1 = (value >> 0) & 0xff;
            var b2 = (value >> 8) & 0xff;
            var b3 = (value >> 16) & 0xff;
            var b4 = (value >> 24) & 0xff;

            return b1 << 24 | b2 << 16 | b3 << 8 | b4 << 0;
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

    }
}