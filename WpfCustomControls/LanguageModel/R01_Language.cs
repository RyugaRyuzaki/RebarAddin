using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCustomControls.LanguageModel.R01_ColumnRebar;

namespace WpfCustomControls.LanguageModel
{
    public class R01_Language  :BaseViewModel
    {

        
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
        public R01_Language(string language)
        {
            
            SettingLanguage = new SettingLanguage(language);
            GeometryLanguage = new GeometryLanguage(language);
            StirrupsLanguage = new StirrupsLanguage(language);
            AdditionalStirrupsLanguage = new AdditionalStirrupsLanguage(language);
            BarsLanguage = new BarsLanguage(language);
            TopDowelsLanguage = new TopDowelsLanguage(language);
            BottomDowelsLanguage = new BottomDowelsLanguage(language);
            BarsDivisionLanguage = new BarsDivisionLanguage(language);
        }
        public void ChangeLanguage(string languge)
        {
           
            SettingLanguage.ChangedLanguage(languge);
            GeometryLanguage.ChangedLanguage(languge);
            StirrupsLanguage.ChangedLanguage(languge);
            AdditionalStirrupsLanguage.ChangedLanguage(languge);
            BarsLanguage.ChangedLanguage(languge);
            TopDowelsLanguage.ChangedLanguage(languge);
            BottomDowelsLanguage.ChangedLanguage(languge);
            BarsDivisionLanguage.ChangedLanguage(languge);
        }
    }
}
