using System.Collections.Generic;
using WpfCustomControls;

namespace R11_FoundationPile.LanguageModel
{
    public class Languages :BaseViewModel
    {
        private List<string> _AllLanguages;
        public List<string> AllLanguages { get { if (_AllLanguages == null) { _AllLanguages = new List<string>() { "EN", "VN" }; } return _AllLanguages; } set { _AllLanguages = value; OnPropertyChanged(); } }

        private string _SelectedLanguage;
        public string SelectedLanguage { get { return _SelectedLanguage; } set { _SelectedLanguage = value; OnPropertyChanged(); } }
      
        private MenuLanguage _MenuLanguage;
        public MenuLanguage MenuLanguage { get { return _MenuLanguage; } set { _MenuLanguage = value; OnPropertyChanged(); } }
        private WindowLanguage _WindowLanguage;
        public WindowLanguage WindowLanguage { get { return _WindowLanguage; } set { _WindowLanguage = value; OnPropertyChanged(); } }
        private GeneralLanguage _GeneralLanguage;
        public GeneralLanguage GeneralLanguage { get { return _GeneralLanguage; } set { _GeneralLanguage = value; OnPropertyChanged(); } }

        private SettingLanguage _SettingLanguage;
        public SettingLanguage SettingLanguage { get { return _SettingLanguage; } set { _SettingLanguage = value; OnPropertyChanged(); } }
        private GeometryLanguage _GeometryLanguage;
        public GeometryLanguage GeometryLanguage { get { return _GeometryLanguage; } set { _GeometryLanguage = value; OnPropertyChanged(); } }
        private PileDetailLanguage _PileDetailLanguage;
        public PileDetailLanguage PileDetailLanguage { get { return _PileDetailLanguage; } set { _PileDetailLanguage = value; OnPropertyChanged(); } }
        private ReinforcementLanguage _ReinforcementLanguage;
        public ReinforcementLanguage ReinforcementLanguage { get { return _ReinforcementLanguage; } set { _ReinforcementLanguage = value; OnPropertyChanged(); } }
        public Languages(string language)
        {
            SelectedLanguage = language;
            MenuLanguage = new MenuLanguage(SelectedLanguage);
            WindowLanguage = new WindowLanguage(SelectedLanguage);
            GeneralLanguage = new GeneralLanguage(SelectedLanguage);
            SettingLanguage = new SettingLanguage(SelectedLanguage);
            GeometryLanguage = new GeometryLanguage(SelectedLanguage);
            PileDetailLanguage = new PileDetailLanguage(SelectedLanguage);
            ReinforcementLanguage = new ReinforcementLanguage(SelectedLanguage);
          
        }
        public void ChangeLanguages()
        {
            MenuLanguage.ChangedLanguage(SelectedLanguage);
            WindowLanguage.ChangedLanguage(SelectedLanguage);
            GeneralLanguage.ChangedLanguage(SelectedLanguage);
            SettingLanguage.ChangedLanguage(SelectedLanguage);
            GeometryLanguage.ChangedLanguage(SelectedLanguage);
            PileDetailLanguage.ChangedLanguage(SelectedLanguage);
            ReinforcementLanguage.ChangedLanguage(SelectedLanguage);
          
        }
    }
}
