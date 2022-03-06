using WpfCustomControls;

namespace R10_WallShear.LanguageModel
{
    public class AdditionalStirrupsLanguage : BaseViewModel
    {

        private string _AdditionalProperty;
        public string AdditionalProperty { get { return _AdditionalProperty; } set { _AdditionalProperty = value; OnPropertyChanged(); } }
        private string _AdditionalHorizontalMain;
        public string AdditionalHorizontalMain { get { return _AdditionalHorizontalMain; } set { _AdditionalHorizontalMain = value; OnPropertyChanged(); } }
        private string _Horizontal;
        public string Horizontal { get { return _Horizontal; } set { _Horizontal = value; OnPropertyChanged(); } }
        private string _FixToMainBar;
        public string FixToMainBar { get { return _FixToMainBar; } set { _FixToMainBar = value; OnPropertyChanged(); } }
        private string _AdditionalHorizontalCorner;
        public string AdditionalHorizontalCorner { get { return _AdditionalHorizontalCorner; } set { _AdditionalHorizontalCorner = value; OnPropertyChanged(); } }
        private string _HorizontalCorner;
        public string HorizontalCorner { get { return _HorizontalCorner; } set { _HorizontalCorner = value; OnPropertyChanged(); } }
        private string _AdditionalVerticalCorner;
        public string AdditionalVerticalCorner { get { return _AdditionalVerticalCorner; } set { _AdditionalVerticalCorner = value; OnPropertyChanged(); } }
        private string _VerticalCorner;
        public string VerticalCorner { get { return _VerticalCorner; } set { _VerticalCorner = value; OnPropertyChanged(); } }
       
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
            AdditionalHorizontalMain = "Additional Horizontal Main";
            Horizontal = "Horizontal";
            FixToMainBar = "Fix To MainBar";
            AdditionalHorizontalCorner = "Additional Horizontal Corner";
            HorizontalCorner = "Horizontal Corner";
            AdditionalVerticalCorner = "Additional Vertical Corner";
            VerticalCorner = "VerticalCorner";
        }
        private void GetLanguageVN()
        {
            AdditionalProperty = "Thông số Đai tăng cường";
            AdditionalHorizontalMain = "Tăng cường ngang Thép chủ";
            Horizontal = "Phương ngang";
            FixToMainBar = "Chẵn Thép chủ";
            AdditionalHorizontalCorner = "Tăng cường ngang Thép góc";
            HorizontalCorner = "Phương ngang thép góc";
            AdditionalVerticalCorner = "Tăng cường dọc Thép góc";
            VerticalCorner = "Phương dọc thép góc";
        }
    }
}
