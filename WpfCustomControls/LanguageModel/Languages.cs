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
        private MenuLanguage _MenuLanguage;
        public MenuLanguage MenuLanguage { get { return _MenuLanguage; } set { _MenuLanguage = value; OnPropertyChanged(); } }
        private WindowLanguage _WindowLanguage;
        public WindowLanguage WindowLanguage { get { return _WindowLanguage; } set { _WindowLanguage = value; OnPropertyChanged(); } }
        private GeneralLanguage _GeneralLanguage;
        public GeneralLanguage GeneralLanguage { get { return _GeneralLanguage; } set { _GeneralLanguage = value; OnPropertyChanged(); } }
        private R01_Language _R01_Language;
        public R01_Language R01_Language { get { return _R01_Language; } set { _R01_Language = value; OnPropertyChanged(); } }
        private R02_Language _R02_Language;
        public R02_Language R02_Language { get { return _R02_Language; } set { _R02_Language = value; OnPropertyChanged(); } }
        private R11_Language _R11_Language;
        public R11_Language R11_Language { get { return _R11_Language; } set { _R11_Language = value; OnPropertyChanged(); } }
        public Languages(string language)
        {
            AuthorLanguage = new AuthorLanguage(language);
            MenuLanguage = new MenuLanguage(language);
            WindowLanguage = new WindowLanguage(language);
            GeneralLanguage = new GeneralLanguage(language);
            R01_Language = new R01_Language(language);
            R02_Language = new R02_Language(language);
            R11_Language = new R11_Language(language);
        }
        public void ChangeLanguages(string language)
        {
            AuthorLanguage.ChangedLanguage(language);
            MenuLanguage.ChangedLanguage(language);
            WindowLanguage.ChangedLanguage(language);
            GeneralLanguage.ChangedLanguage(language);
            R01_Language.ChangeLanguage(language);
            R02_Language.ChangeLanguage(language);
            R11_Language.ChangeLanguage(language);
        }
    }
}
