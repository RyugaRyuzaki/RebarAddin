using WpfCustomControls;

namespace R07_AboutAuthor
{
    public class AuthorLanguage :BaseViewModel
    {

        private string _About;
        public string About { get { return _About; } set { _About = value; OnPropertyChanged(); } }

        private string _Version;
        public string Version { get { return _Version; } set { _Version = value; OnPropertyChanged(); } }


        private string _Support;
        public string Support { get { return _Support; } set { _Support = value; OnPropertyChanged(); } }

        private string _Contract;
        public string Contact { get { return _Contract; } set { _Contract = value; OnPropertyChanged(); } }

        private string _Donation;
        public string Donation { get { return _Donation; } set { _Donation = value; OnPropertyChanged(); } }

        private string _Bank;
        public string Bank { get { return _Bank; } set { _Bank = value; OnPropertyChanged(); } }

        private string _Information;
        public string Information { get { return _Information; } set { _Information = value; OnPropertyChanged(); } }

        public AuthorLanguage(string languge)
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
            About = "About DSP Rebar";
            Version = "Version";
            Support = "Support";
            Contact = "Contact Us";
            Donation = "Donation";
            Bank = "Bank";
            Information = "Information";
        }
        private void GetLanguageVN()
        {
            About = "Đôi nét về DSP Rebar";
            Version = "Phiên bản";
            Support = "Hỗ Trợ";
            Contact = "Liên Hệ";
            Donation = "Ủng hộ";
            Bank = "Ngân Hàng";
            Information = "Thông tin";
        }
    }
}
