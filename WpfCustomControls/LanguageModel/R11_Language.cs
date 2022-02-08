using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCustomControls.LanguageModel.R11_FoundationPile;
namespace WpfCustomControls.LanguageModel
{
    public class R11_Language  :BaseViewModel
    {

        private MenuLanguage _MenuLanguage;
        public MenuLanguage MenuLanguage { get { return _MenuLanguage; } set { _MenuLanguage = value; OnPropertyChanged(); } }
        private SettingLanguage _SettingLanguage;
        public SettingLanguage SettingLanguage { get { return _SettingLanguage; } set { _SettingLanguage = value; OnPropertyChanged(); } }
        public R11_Language(string language)
        {
            MenuLanguage = new MenuLanguage(language);
            SettingLanguage = new SettingLanguage(language);
        }
        public void ChangeLanguage(string languge)
        {
            MenuLanguage.ChangedLanguage(languge);
            SettingLanguage.ChangedLanguage(languge);
        }
    }
}
