using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGS_TranslationEditor
{
    internal class AGS_Translation
    {
        private static char[] passwencstring = {'A', 'v', 'i', 's', ' ', 'D', 'u', 'r', 'g', 'a', 'n'};
        private static ArrayList entryList = new ArrayList();

        private static Int32 iGameUID;
        private static string sGameTitle;

        public static ArrayList ParseTranslation(string Filename)
        {
            FileStream fs = File.OpenRead(Filename);
            BinaryReader br = new BinaryReader(fs);

            long sizeFile = fs.Length;

            char[] transsig = new char[16];
            transsig = br.ReadChars(15);

            //Check AGS Translation Header
            if (string.Compare(new string(transsig), "AGSTranslation") == 0)
            {
                //Read Translation File BlockType fore Example 1,2,3
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

                    //Loop throught File
                    int newlen = br.ReadInt32();
                    char[] wasgamename = new char[100];
                    StringBuilder sb = new StringBuilder();

                    byte[] bwasgamename = br.ReadBytes(newlen);

                    int i = 0;
                    foreach (byte number in bwasgamename)
                    {
                        wasgamename[i] = Convert.ToChar(number);
                        i++;
                    }
                    //Game Name
                    decrypt_text(wasgamename);
                    sGameTitle = new string(wasgamename);
                    sGameTitle = sGameTitle.Trim('\0');

                    //dummy read
                    br.ReadInt32();

                    // Translation Entries
                    long translationLength = br.ReadInt32();
                    translationLength += fs.Position;
                    int entryCounter = 0;

                    while (fs.Position < translationLength)
                    {

                        /*if ((newlen < 0) || (newlen > 5000000))
                        MessageBox.Show("ReadString: file is corrupt");*/

                        //Read original Text
                        newlen = br.ReadInt32();

                        byte[] bSourceBytes = br.ReadBytes(newlen);
                        char[] cSourceText = new char[bSourceBytes.Length + 1];

                        //bwasgamename = br.ReadBytes(newlen);
                        //wasgamename = new char[bwasgamename.Length + 1];

                        i = 0;
                        foreach (byte number in bSourceBytes)
                        {
                            cSourceText[i] = Convert.ToChar(number);
                            i++;
                        }

                        //string sEncSourceText = new string(cSourceText);
                        decrypt_text(cSourceText);
                        string sDecSourceText = new string(cSourceText);
                        sDecSourceText = sDecSourceText.Trim('\0');

                        //Read Translated Text
                        newlen = br.ReadInt32();
                        byte[] bTranslatedBytes = br.ReadBytes(newlen);
                        char[] cTranslatedText = new char[bTranslatedBytes.Length + 1];

                        i = 0;
                        foreach (byte number in bTranslatedBytes)
                        {
                            cTranslatedText[i] = Convert.ToChar(number);
                            i++;
                        }
                        
                        //string sEncTranslatedText = new string(cTranslatedText);
                        decrypt_text(cTranslatedText);
                        string sDecTranslatedText = new string(cTranslatedText);
                        sDecTranslatedText = sDecTranslatedText.Trim('\0');


                        entryCounter++;

                        if (entryCounter == 1383)
                        {
                            int z = 0;
                        }

                        //Populate DataGridView
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

        public static void decrypt_text(char[] toEnc)
        {
            int adx = 0;
            int toencx = 0;

            while (true)
            {
                if (toEnc[toencx] == 0)
                    break;

                toEnc[toencx] -= passwencstring[adx];

                adx++;
                toencx++;

                if (adx > 10)
                    adx = 0;
            }
        }

        private static void decrypt_text(string sToEnc)
        {

            char[] toenc = new char[100];

            toenc = sToEnc.ToCharArray();


            int adx = 0;
            int toencx = 0;

            while (true)
            {
                if (toenc[toencx] == 0)
                    break;

                toenc[toencx] -= passwencstring[adx];

                adx++;
                toencx++;

                if (adx > 10)
                    adx = 0;
            }

            sToEnc = new String(toenc);
        }

        private static void encrypt_text(char[] toenc)
        {
            int adx = 0, tobreak = 0;
            int toencx = 0;

            while (tobreak == 0)
            {
                if (toenc[toencx] == 0)
                    tobreak = 1;

                toenc[toencx] += passwencstring[adx];
                adx++;
                toencx++;

                if (adx > 10)
                    adx = 0;
            }
        }

        public static ArrayList ParseTRSTranslation(string Filename)
        {
            //FileStream fs = File.OpenRead(Filename);
            //BinaryReader br = new BinaryReader(fs);
            StreamReader sr = new StreamReader(Filename);

            string line;

            while ((line = sr.ReadLine()) != null)
            {
                if (!line.Contains("//"))
                {
                    //string sSourceText = sr.ReadLine();
                    string sSourceText = line;
                    string sTranslationText = sr.ReadLine();

                    if (!sTranslationText.Contains("//"))
                    {
                        string[] newRow = {sSourceText, sTranslationText};
                        entryList.Add(newRow);
                    }
                }
            }
            
            sr.Close();
            return entryList;
        }
    }
}

