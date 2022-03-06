
using System.Collections.Generic;
using WpfCustomControls;
namespace R01_ColumnsRebar.LanguageModel
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
        private StirrupsLanguage _StirrupsLanguage;
        public StirrupsLanguage StirrupsLanguage { get { return _StirrupsLanguage; } set { _StirrupsLanguage = value; OnPropertyChanged(); } }
        private AdditionalStirrupsLanguage _AdditionalStirrupsLanguage;
        public AdditionalStirrupsLanguage AdditionalStirrupsLanguage { get { return _AdditionalStirrupsLanguage; } set { _AdditionalStirrupsLanguage = value; OnPropertyChanged(); } }
        private BarsLanguage _BarsLanguage;
        public BarsLanguage BarsLanguage { get { return _BarsLanguage; } set { _BarsLanguage = value; OnPropertyChanged(); } }
        private TopDowelsLanguage _TopDowelsLanguage;
        public TopDowelsLanguage TopDowelsLanguage { get { return _TopDowelsLanguage; } set { _TopDowelsLanguage = value; OnPropertyChanged(); } }
        private BottomDowelsLanguage _BottomDowelsLanguage;
        public BottomDowelsLanguage BottomDowelsLanguage { get { return _BottomDowelsLanguage; } set { _BottomDowelsLanguage = value; OnPropertyChanged(); } }
        private BarsDivisionLanguage _BarsDivisionLanguage;
        public BarsDivisionLanguage BarsDivisionLanguage { get { return _BarsDivisionLanguage; } set { _BarsDivisionLanguage = value; OnPropertyChanged(); } }
        public Languages(string language)
        {
            SelectedLanguage = language;
           MenuLanguage = new MenuLanguage(SelectedLanguage);
            WindowLanguage = new WindowLanguage(SelectedLanguage);
            GeneralLanguage = new GeneralLanguage(SelectedLanguage);
            SettingLanguage = new SettingLanguage(SelectedLanguage);
            GeometryLanguage = new GeometryLanguage(SelectedLanguage);
            StirrupsLanguage = new StirrupsLanguage(SelectedLanguage);
            AdditionalStirrupsLanguage = new AdditionalStirrupsLanguage(SelectedLanguage);
            BarsLanguage = new BarsLanguage(SelectedLanguage);
            TopDowelsLanguage = new TopDowelsLanguage(SelectedLanguage);
            BottomDowelsLanguage = new BottomDowelsLanguage(SelectedLanguage);
            BarsDivisionLanguage = new BarsDivisionLanguage(SelectedLanguage);
        }
        public void ChangeLanguages()
        {
           
            MenuLanguage.ChangedLanguage(SelectedLanguage);
            WindowLanguage.ChangedLanguage(SelectedLanguage);
            GeneralLanguage.ChangedLanguage(SelectedLanguage);
            SettingLanguage.ChangedLanguage(SelectedLanguage);
            GeometryLanguage.ChangedLanguage(SelectedLanguage);
            StirrupsLanguage.ChangedLanguage(SelectedLanguage);
            AdditionalStirrupsLanguage.ChangedLanguage(SelectedLanguage);
            BarsLanguage.ChangedLanguage(SelectedLanguage);
            TopDowelsLanguage.ChangedLanguage(SelectedLanguage);
            BottomDowelsLanguage.ChangedLanguage(SelectedLanguage);
            BarsDivisionLanguage.ChangedLanguage(SelectedLanguage);
        }
    }
}
