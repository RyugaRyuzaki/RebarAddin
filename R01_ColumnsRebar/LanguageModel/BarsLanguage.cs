using WpfCustomControls;

namespace R01_ColumnsRebar.LanguageModel
{
    public class BarsLanguage : BaseViewModel
    {

        private string _BarsProperty;
        public string BarsProperty { get { return _BarsProperty; } set { _BarsProperty = value; OnPropertyChanged(); } }
        private string _SplitOverlap;
        public string SplitOverlap { get { return _SplitOverlap; } set { _SplitOverlap = value; OnPropertyChanged(); } }
        private string _AreaReinforcement;
        public string AreaReinforcement { get { return _AreaReinforcement; } set { _AreaReinforcement = value; OnPropertyChanged(); } }
        private string _Overlap;
        public string Overlap { get { return _Overlap; } set { _Overlap = value; OnPropertyChanged(); } }

        private string _BarsInformation;
        public string BarsInformation { get { return _BarsInformation; } set { _BarsInformation = value; OnPropertyChanged(); } }
        public BarsLanguage(string language)
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
            BarsProperty = "Bars Property";
            SplitOverlap = "Split Overlap";
            AreaReinforcement = "%*Area Reinforcement";
            Overlap = "Overlap";
            BarsInformation = "Bars Information";
        }
        private void GetLanguageVN()
        {
            BarsProperty = "Thông số thép";
            SplitOverlap = "Cắt nối chồng";
            AreaReinforcement = "%*Diện tích Thép";
            Overlap = "Đoạn cắt";
            BarsInformation = "Thông tin Thép";
        }
    }
}
