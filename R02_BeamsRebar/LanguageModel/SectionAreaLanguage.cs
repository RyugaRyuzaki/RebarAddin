using WpfCustomControls;

namespace R02_BeamsRebar.LanguageModel
{
    public class SectionAreaLanguage : BaseViewModel
    {

        private string _Start;
        public string Start { get { return _Start; } set { _Start = value; OnPropertyChanged(); } }
        private string _Middle;
        public string Middle { get { return _Middle; } set { _Middle = value; OnPropertyChanged(); } }
        private string _End;
        public string End { get { return _End; } set { _End = value; OnPropertyChanged(); } }
        private string _Span;
        public string Span { get { return _Span; } set { _Span = value; OnPropertyChanged(); } }

        public SectionAreaLanguage(string language)
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
            Start = "Start";
            Middle = "Middle";
            End = "End";
            Span = "Span";
        }
        private void GetLanguageVN()
        {
            Start = "Đầu";
            Middle = "Giữa";
            End = "Cuối";
            Span = "Nhịp";
        }
    }
}
