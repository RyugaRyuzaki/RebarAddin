using WpfCustomControls;

namespace R02_BeamsRebar.LanguageModel
{
    public class BarsDivisionLanguage : BaseViewModel
    {

        private string _OverLap;
        public string OverLap { get { return _OverLap; } set { _OverLap = value; OnPropertyChanged(); } }
        private string _MainTopBar;
        public string MainTopBar { get { return _MainTopBar; } set { _MainTopBar = value; OnPropertyChanged(); } }
        private string _MainBottomBar;
        public string MainBottomBar { get { return _MainBottomBar; } set { _MainBottomBar = value; OnPropertyChanged(); } }
        private string _AddTopBar;
        public string AddTopBar { get { return _AddTopBar; } set { _AddTopBar = value; OnPropertyChanged(); } }
        private string _AddBottomBar;
        public string AddBottomBar { get { return _AddBottomBar; } set { _AddBottomBar = value; OnPropertyChanged(); } }
        private string _SideBar;
        public string SideBar { get { return _SideBar; } set { _SideBar = value; OnPropertyChanged(); } }
        private string _SpecialBar;
        public string SpecialBar { get { return _SpecialBar; } set { _SpecialBar = value; OnPropertyChanged(); } }
        private string _StirrupBar;
        public string StirrupBar { get { return _StirrupBar; } set { _StirrupBar = value; OnPropertyChanged(); } }
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
            OverLap = "OverLap";
            MainTopBar = "Main Top Bar";
            MainBottomBar = "Main Bottom Bar";
            AddTopBar = "Add Top Bar";
            AddBottomBar = "Add Bottom Bar";
            SideBar = "Side Bar";
            SpecialBar = "Special Bar";
            StirrupBar = "Stirrup Bar";
        }
        private void GetLanguageVN()
        {
            OverLap = "Nối chồng";
            MainTopBar = "Thép chủ trên";
            MainBottomBar = "Thép chủ dưới";
            AddTopBar = "Tăng cường trên";
            AddBottomBar = "Tăng cường dưới";
            SideBar = "Thép giá";
            SpecialBar = "Thép Tập trung";
            StirrupBar = "Thép đai";
        }
    }
}
