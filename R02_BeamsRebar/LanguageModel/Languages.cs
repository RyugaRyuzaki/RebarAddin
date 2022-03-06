using System.Collections.Generic;
using WpfCustomControls;

namespace R02_BeamsRebar.LanguageModel
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
        private StirrupLanguage _StirrupLanguage;
        public StirrupLanguage StirrupLanguage { get { return _StirrupLanguage; } set { _StirrupLanguage = value; OnPropertyChanged(); } }
        private BarsMainLanguage _BarsMainLanguage;
        public BarsMainLanguage BarsMainLanguage { get { return _BarsMainLanguage; } set { _BarsMainLanguage = value; OnPropertyChanged(); } }
        private AddTopBarLanguage _AddTopBarLanguage;
        public AddTopBarLanguage AddTopBarLanguage { get { return _AddTopBarLanguage; } set { _AddTopBarLanguage = value; OnPropertyChanged(); } }
        private AddBottomBarLanguage _AddBottomBarLanguage;
        public AddBottomBarLanguage AddBottomBarLanguage { get { return _AddBottomBarLanguage; } set { _AddBottomBarLanguage = value; OnPropertyChanged(); } }
        private SpeciaBarLanguage _SpeciaBarLanguage;
        public SpeciaBarLanguage SpeciaBarLanguage { get { return _SpeciaBarLanguage; } set { _SpeciaBarLanguage = value; OnPropertyChanged(); } }
        private SectionAreaLanguage _SectionAreaLanguage;
        public SectionAreaLanguage SectionAreaLanguage { get { return _SectionAreaLanguage; } set { _SectionAreaLanguage = value; OnPropertyChanged(); } }
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
            StirrupLanguage = new StirrupLanguage(SelectedLanguage);
            BarsMainLanguage = new BarsMainLanguage(SelectedLanguage);
            AddTopBarLanguage = new AddTopBarLanguage(SelectedLanguage);
            AddBottomBarLanguage = new AddBottomBarLanguage(SelectedLanguage);
            SpeciaBarLanguage = new SpeciaBarLanguage(SelectedLanguage);
            SectionAreaLanguage = new SectionAreaLanguage(SelectedLanguage);
            BarsDivisionLanguage = new BarsDivisionLanguage(SelectedLanguage);
        }
        public void ChangeLanguages()
        {
            
            MenuLanguage.ChangedLanguage(SelectedLanguage);
            WindowLanguage.ChangedLanguage(SelectedLanguage);
            GeneralLanguage.ChangedLanguage(SelectedLanguage);
            SettingLanguage.ChangedLanguage(SelectedLanguage);
            GeometryLanguage.ChangedLanguage(SelectedLanguage);
            StirrupLanguage.ChangedLanguage(SelectedLanguage);
            BarsMainLanguage.ChangedLanguage(SelectedLanguage);
            AddTopBarLanguage.ChangedLanguage(SelectedLanguage);
            AddBottomBarLanguage.ChangedLanguage(SelectedLanguage);
            SpeciaBarLanguage.ChangedLanguage(SelectedLanguage);
            SectionAreaLanguage.ChangedLanguage(SelectedLanguage);
            BarsDivisionLanguage.ChangedLanguage(SelectedLanguage);
            
        }
    }
}
