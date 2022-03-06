using WpfCustomControls;
namespace R02_BeamsRebar.LanguageModel
{
    public class AddBottomBarLanguage : BaseViewModel
    {

        private string _AdditionalBottomBar;
        public string AdditionalBottomBar { get { return _AdditionalBottomBar; } set { _AdditionalBottomBar = value; OnPropertyChanged(); } }
        private string _FixedSpan;
        public string FixedSpan { get { return _FixedSpan; } set { _FixedSpan = value; OnPropertyChanged(); } }
        private string _Start;
        public string Start { get { return _Start; } set { _Start = value; OnPropertyChanged(); } }
        private string _Middle;
        public string Middle { get { return _Middle; } set { _Middle = value; OnPropertyChanged(); } }
        private string _End;
        public string End { get { return _End; } set { _End = value; OnPropertyChanged(); } }
        private string _Span;
        public string Span { get { return _Span; } set { _Span = value; OnPropertyChanged(); } }
        private string _Lenght;
        public string Lenght { get { return _Lenght; } set { _Lenght = value; OnPropertyChanged(); } }
      
        public AddBottomBarLanguage(string language)
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
            AdditionalBottomBar = "Additional Bottom Bar";
            FixedSpan = "Fixed Span";
            Start = "Start";
            Middle = "Middle";
            End = "End";
            Span = "Span";
            Lenght = "Lenght";
        }
        private void GetLanguageVN()
        {
            AdditionalBottomBar = "Tăng cường dưới";
            FixedSpan = "Chẵn nhịp";
            Start = "Đầu";
            Middle = "Giữa";
            End = "Cuối";
            Span = "Nhịp";
            Lenght = "Chiều dài";
        }
    }
}
