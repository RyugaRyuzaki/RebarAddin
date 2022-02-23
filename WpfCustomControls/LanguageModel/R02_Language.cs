using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCustomControls.LanguageModel.R02_BeamRebar;

namespace WpfCustomControls.LanguageModel
{
    public class R02_Language : BaseViewModel
    {

        
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
        public R02_Language(string language)
        {
            
            SettingLanguage = new SettingLanguage(language);
            GeometryLanguage = new GeometryLanguage(language);
            StirrupLanguage = new StirrupLanguage(language);
            BarsMainLanguage = new BarsMainLanguage(language);
            AddTopBarLanguage = new AddTopBarLanguage(language);
            AddBottomBarLanguage = new AddBottomBarLanguage(language);
            SpeciaBarLanguage = new SpeciaBarLanguage(language);
            SectionAreaLanguage = new SectionAreaLanguage(language);
            BarsDivisionLanguage = new BarsDivisionLanguage(language);
        }
        public void ChangeLanguage(string languge)
        {
           
            SettingLanguage.ChangedLanguage(languge);
            GeometryLanguage.ChangedLanguage(languge);
            StirrupLanguage.ChangedLanguage(languge);
            BarsMainLanguage.ChangedLanguage(languge);
            AddTopBarLanguage.ChangedLanguage(languge);
            AddBottomBarLanguage.ChangedLanguage(languge);
            SpeciaBarLanguage.ChangedLanguage(languge);
            SectionAreaLanguage.ChangedLanguage(languge);
            BarsDivisionLanguage.ChangedLanguage(languge);
        }
    }
}
