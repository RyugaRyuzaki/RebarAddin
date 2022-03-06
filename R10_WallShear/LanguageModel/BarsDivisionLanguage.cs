using WpfCustomControls;

namespace R10_WallShear.LanguageModel
{
    public class BarsDivisionLanguage : BaseViewModel
    {

        private string _DivisionProperty;
        public string DivisionProperty { get { return _DivisionProperty; } set { _DivisionProperty = value; OnPropertyChanged(); } }
        private string _MainBarsDivision;
        public string MainBarsDivision { get { return _MainBarsDivision; } set { _MainBarsDivision = value; OnPropertyChanged(); } }
        private string _StirrupDivision;
        public string StirrupDivision { get { return _StirrupDivision; } set { _StirrupDivision = value; OnPropertyChanged(); } }
        private string _AddHorizontalStirrupMain;
        public string AddHorizontalStirrupMain { get { return _AddHorizontalStirrupMain; } set { _AddHorizontalStirrupMain = value; OnPropertyChanged(); } }
         private string _AddHorizontalStirrupCorner;
        public string AddHorizontalStirrupCorner { get { return _AddHorizontalStirrupCorner; } set { _AddHorizontalStirrupCorner = value; OnPropertyChanged(); } }
        private string _AddVerticalStirrupCorner;
        public string AddVerticalStirrupCorner { get { return _AddVerticalStirrupCorner; } set { _AddVerticalStirrupCorner = value; OnPropertyChanged(); } }
       
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
            AddHorizontalStirrupMain = "Add-Horizontal Stirrup Main";
            AddHorizontalStirrupCorner = "Add-Horizontal Corner Stirrup Main";
            AddVerticalStirrupCorner = "Add-Vertical Corner Stirrup Main";
        }
        private void GetLanguageVN()
        {
            DivisionProperty = "Thông số Chia thép";
            MainBarsDivision = "Thép chủ";
            StirrupDivision = "Thép đain";
            AddHorizontalStirrupMain = "Thép tăng cường đai ngang Thép chủ";
            AddHorizontalStirrupCorner = "Thép tăng cường đai ngang Thép góc";
            AddVerticalStirrupCorner = "Thép tăng cường đai dọc Thép góc";
        }
    }
}
