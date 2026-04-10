using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGS_TranslationEditor.Utils
{
    public class StringUtils
    {
        public static string ReadString(BinaryReader br)
        {
            int len = br.ReadInt32();
            if (len > 0)
            {
                byte[] data = br.ReadBytes(len);
                return Encoding.Default.GetString(data);
            }
            return string.Empty;
        }
    }
}
