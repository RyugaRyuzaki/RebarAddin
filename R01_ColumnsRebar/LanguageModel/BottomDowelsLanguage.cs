using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCustomControls;
namespace R01_ColumnsRebar.LanguageModel
{
    public class BottomDowelsLanguage : BaseViewModel
    {

        private string _BottomDowelsProperty;
        public string BottomDowelsProperty { get { return _BottomDowelsProperty; } set { _BottomDowelsProperty = value; OnPropertyChanged(); } }
        private string _ApplyAllBar;
        public string ApplyAllBar { get { return _ApplyAllBar; } set { _ApplyAllBar = value; OnPropertyChanged(); } }
        private string _Bottom;
        public string Bottom { get { return _Bottom; } set { _Bottom = value; OnPropertyChanged(); } }
        private string _BottomDowels;
        public string BottomDowels { get { return _BottomDowels; } set { _BottomDowels = value; OnPropertyChanged(); } }
        private string _FixedtoDownColumn;
        public string FixedtoDownColumn { get { return _FixedtoDownColumn; } set { _FixedtoDownColumn = value; OnPropertyChanged(); } }
        public BottomDowelsLanguage(string language)
        {
            ChangedLanguage(language);
        }
        public void ChangedLanguage(string language)
        {
            switch (language)
            {
                case "EN": GetLanguageEN(); break;
                case "VN": GetLanguageVN(); break;
                default: GetLanguageEN(); break;
            }
        }
        private void GetLanguageEN()
        {
            BottomDowelsProperty = "BottomDowelsProperty";
            ApplyAllBar = "Apply All Bar";
            Bottom = "Bottom";
            BottomDowels = "Bottom Dowels";
            FixedtoDownColumn = "Fixed to Dơn Column";
        }
        private void GetLanguageVN()
        {
            BottomDowelsProperty = "Thông số Neo thép duới";
            ApplyAllBar = "Áp dụng hết";
            Bottom = "Dưới";
            BottomDowels = "Neo Thép dưới";
            FixedtoDownColumn = "Chẵn Cột dưới";
        }
    }
}
