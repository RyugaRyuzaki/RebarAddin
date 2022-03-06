using WpfCustomControls;

namespace R02_BeamsRebar.LanguageModel
{
    public class BarsMainLanguage : BaseViewModel
    {

        private string _TopLongBar;
        public string TopLongBar { get { return _TopLongBar; } set { _TopLongBar = value; OnPropertyChanged(); } }
        private string _StyleBar;
        public string StyleBar { get { return _StyleBar; } set { _StyleBar = value; OnPropertyChanged(); } }
        private string _TopDetailBar;
        public string TopDetailBar { get { return _TopDetailBar; } set { _TopDetailBar = value; OnPropertyChanged(); } }
        private string _BottomLongBar;
        public string BottomLongBar { get { return _BottomLongBar; } set { _BottomLongBar = value; OnPropertyChanged(); } }
        private string _BottomDetailBar;
        public string BottomDetailBar { get { return _BottomDetailBar; } set { _BottomDetailBar = value; OnPropertyChanged(); } }
        public BarsMainLanguage(string language)
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
            TopLongBar = "Top Long Bar";
            StyleBar = "Style Bar";
            TopDetailBar = "Top Detail Bar";
            BottomLongBar = "Bottom Long Bar";
            BottomDetailBar = "Bottom Detail Bar";
        }
        private void GetLanguageVN()
        {
            TopLongBar = "Thép chủ trên";
            StyleBar = "Kiểu Thép";
            TopDetailBar = "Chi tiết thép chủ trên";
            BottomLongBar = "Thép chủ dưới";
            BottomDetailBar = "Chi tiết Thép chủ dưới";
        }
    }
}
