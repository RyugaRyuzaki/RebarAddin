using WpfCustomControls;

namespace R02_BeamsRebar.LanguageModel
{
    public class MenuLanguage : BaseViewModel
    {

        private string _Setting;
        public string Setting { get { return _Setting; } set { _Setting = value; OnPropertyChanged(); } }
        private string _Geometry;
        public string Geometry { get { return _Geometry; } set { _Geometry = value; OnPropertyChanged(); } }
        private string _Stirrups;
        public string Stirrups { get { return _Stirrups; } set { _Stirrups = value; OnPropertyChanged(); } }
        private string _AdditionalStirrups;
        public string AdditionalStirrups { get { return _AdditionalStirrups; } set { _AdditionalStirrups = value; OnPropertyChanged(); } }
        private string _Bars;
        public string Bars { get { return _Bars; } set { _Bars = value; OnPropertyChanged(); } }
      
        private string _TopDowels;
        public string TopDowels { get { return _TopDowels; } set { _TopDowels = value; OnPropertyChanged(); } }
        private string _BottomDowels;
        public string BottomDowels { get { return _BottomDowels; } set { _BottomDowels = value; OnPropertyChanged(); } }
        private string _BarsDivision;
        public string BarsDivision { get { return _BarsDivision; } set { _BarsDivision = value; OnPropertyChanged(); } }
        private string _PileDetail;
        public string PileDetail { get { return _PileDetail; } set { _PileDetail = value; OnPropertyChanged(); } }
        private string _Reinforcement;
        public string Reinforcement { get { return _Reinforcement; } set { _Reinforcement = value; OnPropertyChanged(); } }
        private string _BarsMain;
        public string BarsMain { get { return _BarsMain; } set { _BarsMain = value; OnPropertyChanged(); } }
        private string _AddTopBar;
        public string AddTopBar { get { return _AddTopBar; } set { _AddTopBar = value; OnPropertyChanged(); } }
        private string _AddBottomBar;
        public string AddBottomBar { get { return _AddBottomBar; } set { _AddBottomBar = value; OnPropertyChanged(); } }
        private string _SpecialBar;
        public string SpecialBar { get { return _SpecialBar; } set { _SpecialBar = value; OnPropertyChanged(); } }
        private string _SectionArea;
        public string SectionArea { get { return _SectionArea; } set { _SectionArea = value; OnPropertyChanged(); } }
        public MenuLanguage(string languge)
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
            Setting = "Setting";
            Geometry = "Geometry";
            Stirrups = "Stirrups";
            AdditionalStirrups = "Additional Stirrups";
            Bars = "Bars";
            TopDowels = "Top Dowels";
            BottomDowels = "Bottom Dowels";
            BarsDivision = "Bars Division";
            PileDetail = "Pile Detail";
            Reinforcement = "Reinforcement";
            BarsMain = "Bars Main";
            AddTopBar = "Add Top Bar";
            AddBottomBar = "Add Bottom Bar";
            SpecialBar = "Special Bar";
            SectionArea = "Section Area";
        }
        private void GetLanguageVN()
        {
            Setting = "Cài Đặt";
            Geometry = "Hình Dạng";
            Stirrups = "Thép Đai";
            AdditionalStirrups = "Đai tăng cường";
            Bars = "Thép chủ";
            TopDowels = "Neo Thép Trên";
            BottomDowels = "Neo Thép Dưới";
            BarsDivision = "Cắt Thép";
            PileDetail = "Chi tiết Cọc";
            Reinforcement = "Cốt thép";
            BarsMain = "Thép chủ";
            AddTopBar = "Tăng cường trên";
            AddBottomBar = "Tăng cường dưới";
            SpecialBar = "Thép đặc biệt";
            SectionArea = "Mặt cắt Thép";
        }
    }
}
