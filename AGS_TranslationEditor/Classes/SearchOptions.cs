using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGS_TranslationEditor.Classes {
    public class SearchOptions {

        private bool wrapAround;
        private bool forward;
        private bool caseSensitive;

        public bool WrapAround { get => wrapAround; set => wrapAround = value; }
        public bool Forward { get => forward; set => forward = value; }
        public bool CaseSensitive { get => caseSensitive; set => caseSensitive = value; }
    }
}
