using WpfCustomControls;

namespace R02_BeamsRebar.LanguageModel
{
    public class AddTopBarLanguage : BaseViewModel
    {

        private string _AdditionalTopBar;
        public string AdditionalTopBar { get { return _AdditionalTopBar; } set { _AdditionalTopBar = value; OnPropertyChanged(); } }
        private string _FixedSpan;
        public string FixedSpan { get { return _FixedSpan; } set { _FixedSpan = value; OnPropertyChanged(); } }
        private string _Start;
        public string Start { get { return _Start; } set { _Start = value; OnPropertyChanged(); } }
        private string _Middle;
        public string Middle { get { return _Middle; } set { _Middle = value; OnPropertyChanged(); } }
        private string _End;
        public string End { get { return _End; } set { _End = value; OnPropertyChanged(); } }
        private string _Node;
        public string Node { get { return _Node; } set { _Node = value; OnPropertyChanged(); } }
        public AddTopBarLanguage(string language)
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
            AdditionalTopBar = "Additional Top Bar";
            FixedSpan = "Fixed Span";
            Start = "Start";
            Middle = "Middle";
            End = "End";
            Node = "Node";
        
        }
        private void GetLanguageVN()
        {
            AdditionalTopBar = "Tăng cường trên";
            FixedSpan = "Chẵn nhịp";
            Start = "Đầu";
            Middle = "Giữa";
            End = "Cuối";
            Node = "Gối";

        }
    }
}
