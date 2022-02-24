using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCustomControls.LanguageModel.R10_WallsShearRebar
{
    public class AdditionalStirrupsLanguage : BaseViewModel
    {

        private string _AdditionalProperty;
        public string AdditionalProperty { get { return _AdditionalProperty; } set { _AdditionalProperty = value; OnPropertyChanged(); } }
        private string _AdditionalHorizontal;
        public string AdditionalHorizontal { get { return _AdditionalHorizontal; } set { _AdditionalHorizontal = value; OnPropertyChanged(); } }
        private string _Horizontal;
        public string Horizontal { get { return _Horizontal; } set { _Horizontal = value; OnPropertyChanged(); } }
        private string _AdditionalVertical;
        public string AdditionalVertical { get { return _AdditionalVertical; } set { _AdditionalVertical = value; OnPropertyChanged(); } }
        private string _Vertical;
        public string Vertical { get { return _Vertical; } set { _Vertical = value; OnPropertyChanged(); } }
       
        public AdditionalStirrupsLanguage(string language)
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
            AdditionalProperty = "Additional Property";
            AdditionalHorizontal = "Additional Horizontal";
            AdditionalVertical = "Additional Vertical";
            Horizontal = "Horizontal";
            Vertical = "Vertical";
        }
        private void GetLanguageVN()
        {
            AdditionalProperty = "Thông số Đai tăng cường";
            AdditionalHorizontal = "Tăng cường ngang";
            AdditionalVertical = "Tăng cường Dọc";
            Horizontal = "Phương ngang";
            Vertical = "Phương dọc";
        }
    }
}
