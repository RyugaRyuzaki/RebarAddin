using System.Collections.Generic;
using WpfCustomControls;

namespace R07_AboutAuthor
{
    public class Languages :BaseViewModel
    {
        private List<string> _AllLanguages;
        public List<string> AllLanguages { get { if (_AllLanguages == null) { _AllLanguages = new List<string>() { "EN", "VN" }; } return _AllLanguages; } set { _AllLanguages = value; OnPropertyChanged(); } }

        private string _SelectedLanguage;
        public string SelectedLanguage { get { return _SelectedLanguage; } set { _SelectedLanguage = value; OnPropertyChanged(); } }
        private AuthorLanguage _AuthorLanguage;
        public AuthorLanguage AuthorLanguage { get { return _AuthorLanguage; } set { _AuthorLanguage = value; OnPropertyChanged(); } }
        public Languages(string language)
        {
            SelectedLanguage = language;
            AuthorLanguage = new AuthorLanguage(SelectedLanguage);
        }
        public void ChangeLanguages()
        {
            AuthorLanguage.ChangedLanguage(SelectedLanguage);
        }
    }
}
