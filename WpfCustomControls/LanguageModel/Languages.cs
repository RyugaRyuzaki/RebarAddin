using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCustomControls.LanguageModel
{
    public class Languages :BaseViewModel
    {

        private AuthorLanguage _AuthorLanguage;
        public AuthorLanguage AuthorLanguage { get { return _AuthorLanguage; } set { _AuthorLanguage = value; OnPropertyChanged(); } }
        private R01_Language _R01_Language;
        public R01_Language R01_Language { get { return _R01_Language; } set { _R01_Language = value; OnPropertyChanged(); } }
        private R11_Language _R11_Language;
        public R11_Language R11_Language { get { return _R11_Language; } set { _R11_Language = value; OnPropertyChanged(); } }
        public Languages(string language)
        {
            AuthorLanguage = new AuthorLanguage(language);
            R01_Language = new R01_Language(language);
            R11_Language = new R11_Language(language);
        }
        public void ChangeLanguages(string language)
        {
            AuthorLanguage.ChangedLanguage(language);
            R01_Language.ChangeLanguage(language);
            R11_Language.ChangeLanguage(language);
        }
    }
}
