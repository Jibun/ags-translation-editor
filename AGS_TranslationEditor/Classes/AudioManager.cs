using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGS_TranslationEditor.Classes {
    public class AudioManager {

        private static AudioManager instance = null;

        public Audio audio;

        private AudioManager() {
            audio = new Audio();
    }

        public static AudioManager getInstance() {
            if (instance == null) {
                instance = new AudioManager();
            }
            return instance;
        }
    }
}
