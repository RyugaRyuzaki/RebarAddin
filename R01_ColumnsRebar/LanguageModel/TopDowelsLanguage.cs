using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCustomControls;
namespace R01_ColumnsRebar.LanguageModel
{
    public class TopDowelsLanguage : BaseViewModel
    {

        private string _TopDowelsProperty;
        public string TopDowelsProperty { get { return _TopDowelsProperty; } set { _TopDowelsProperty = value; OnPropertyChanged(); } }
        private string _ApplyAllBar;
        public string ApplyAllBar { get { return _ApplyAllBar; } set { _ApplyAllBar = value; OnPropertyChanged(); } }
        private string _Top;
        public string Top { get { return _Top; } set { _Top = value; OnPropertyChanged(); } }
        private string _TopDowels;
        public string TopDowels { get { return _TopDowels; } set { _TopDowels = value; OnPropertyChanged(); } }
        private string _FixedtoUpColumn;
        public string FixedtoUpColumn { get { return _FixedtoUpColumn; } set { _FixedtoUpColumn = value; OnPropertyChanged(); } }
       
        public TopDowelsLanguage(string language)
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
            TopDowelsProperty = "Top Dowels Property";
            ApplyAllBar = "Apply All Bar";
            Top = "Top";
            TopDowels = "Top Dowels";
            FixedtoUpColumn = "Fixed to Up Column";
        
        }
        private void GetLanguageVN()
        {
            TopDowelsProperty = "Thông số Neo thép trên";
            ApplyAllBar = "Áp dụng hết";
            Top = "Trên";
            TopDowels = "Neo Thép Trên";
            FixedtoUpColumn = "Chẵn Cột Trên";
         
        }
    }
}
