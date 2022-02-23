using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCustomControls.LanguageModel.R01_ColumnRebar
{
    public class BarsDivisionLanguage : BaseViewModel
    {

        private string _DivisionProperty;
        public string DivisionProperty { get { return _DivisionProperty; } set { _DivisionProperty = value; OnPropertyChanged(); } }
        private string _MainBarsDivision;
        public string MainBarsDivision { get { return _MainBarsDivision; } set { _MainBarsDivision = value; OnPropertyChanged(); } }
        private string _StirrupDivision;
        public string StirrupDivision { get { return _StirrupDivision; } set { _StirrupDivision = value; OnPropertyChanged(); } }
        private string _AddHorizontalStirrup;
        public string AddHorizontalStirrup { get { return _AddHorizontalStirrup; } set { _AddHorizontalStirrup = value; OnPropertyChanged(); } }
        private string _AddVerticalStirrup;
        public string AddVerticalStirrup { get { return _AddVerticalStirrup; } set { _AddVerticalStirrup = value; OnPropertyChanged(); } }
       
        public BarsDivisionLanguage(string language)
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
            DivisionProperty = "Division Property";
            MainBarsDivision = "Main-Bars Division";
            StirrupDivision = "Stirrup Division";
            AddHorizontalStirrup = "Add-Horizontal Stirrup";
            AddVerticalStirrup = "Add-Vertical Stirrup";
        }
        private void GetLanguageVN()
        {
            DivisionProperty = "Thông số Chia thép";
            MainBarsDivision = "Thép chủ";
            StirrupDivision = "Thép đain";
            AddHorizontalStirrup = "Thép tăng cường đai ngang";
            AddVerticalStirrup = "Thép tăng cường đai ngang";
        }
    }
}
