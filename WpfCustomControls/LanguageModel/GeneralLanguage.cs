using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCustomControls.LanguageModel
{
    public class GeneralLanguage : BaseViewModel
    {

        private string _NameBar;
        public string NameBar { get { return _NameBar; } set { _NameBar = value; OnPropertyChanged(); } }
        private string _Bar;
        public string Bar { get { return _Bar; } set { _Bar = value; OnPropertyChanged(); } }

        private string _Distance;
        public string Distance { get { return _Distance; } set { _Distance = value; OnPropertyChanged(); } }


        private string _NumberBar;
        public string NumberBar { get { return _NumberBar; } set { _NumberBar = value; OnPropertyChanged(); } }

        private string _HookLength;
        public string HookLength { get { return _HookLength; } set { _HookLength = value; OnPropertyChanged(); } }

      

        public GeneralLanguage(string languge)
        {
            ChangedLanguage(languge);
        }
        public void ChangedLanguage(string languge)
        {
            switch (languge)
            {
                case "EN": GetLanguageEN(); break;
                case "VN": GetLanguageVN(); break;
                default: GetLanguageEN(); break;
            }
        }
        private void GetLanguageEN()
        {
            NameBar = "Name Bar";
            Bar = "Bar";
            Distance = "Distance";
            NumberBar = "No";
            HookLength = "HookLength";
        }
        private void GetLanguageVN()
        {
            NameBar = "Loại Thép";
            Bar = "Thép";
            Distance = "KC";
            NumberBar = "SL";
            HookLength = "Dài Hook";
        }
    }
}
