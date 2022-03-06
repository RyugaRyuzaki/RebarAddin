using WpfCustomControls;

namespace R10_WallShear.LanguageModel
{
    public class BottomDowelsLanguage : BaseViewModel
    {
        private string _WallsProperty;
        public string WallsProperty { get { return _WallsProperty; } set { _WallsProperty = value; OnPropertyChanged(); } }
     
        private string _ApplyAllBar;
        public string ApplyAllBar { get { return _ApplyAllBar; } set { _ApplyAllBar = value; OnPropertyChanged(); } }
        private string _BottomType;
        public string BottomType { get { return _BottomType; } set { _BottomType = value; OnPropertyChanged(); } }

        private string _BottomDowelsProperty;
        public string BottomDowelsProperty { get { return _BottomDowelsProperty; } set { _BottomDowelsProperty = value; OnPropertyChanged(); } }
        private string _BottomDowels;
        public string BottomDowels { get { return _BottomDowels; } set { _BottomDowels = value; OnPropertyChanged(); } }

        private string _CornerBarProperty;
        public string CornerBarProperty { get { return _CornerBarProperty; } set { _CornerBarProperty = value; OnPropertyChanged(); } }
        private string _CornerBottomDowelsProperty;
        public string CornerBottomDowelsProperty { get { return _CornerBottomDowelsProperty; } set { _CornerBottomDowelsProperty = value; OnPropertyChanged(); } }
        
        private string _CornerBottomDowels;
        public string CornerBottomDowels { get { return _CornerBottomDowels; } set { _CornerBottomDowels = value; OnPropertyChanged(); } }

        private string _FixedtoDown;
        public string FixedtoDown { get { return _FixedtoDown; } set { _FixedtoDown = value; OnPropertyChanged(); } }

        private string _AddBar;
        public string AddBar { get { return _AddBar; } set { _AddBar = value; OnPropertyChanged(); } }

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
            WallsProperty = "Walls Property";
            ApplyAllBar = "Apply All Bar";
            BottomType = "Bottom";
            BottomDowelsProperty = "Bottom Dowels Property";
            BottomDowels = "Bottom Dowels";
            CornerBarProperty = "Corner Property";
            CornerBottomDowelsProperty = "Corner Bottom Dowels Property";
            CornerBottomDowels = "Corner Bottom Dowels";
            FixedtoDown = "Fixed to Down";
            AddBar = "Add Bar";
        }
        private void GetLanguageVN()
        {
            WallsProperty = "Thông số Tường";
            ApplyAllBar = "Áp dụng hết";
            BottomType = "Dưới";
            BottomDowelsProperty = "Thông số Neo thép góc duới";
            BottomDowels = "Neo Thép dưới";
            CornerBarProperty = "Thông số thép góc duới";
            CornerBottomDowelsProperty = "Thông số Neo thép góc duới";
            CornerBottomDowels = "Neo Thép dưới";
            FixedtoDown = "Chẵn Tường dưới";
            AddBar = "Thêm Thép";
        }
    }
}
