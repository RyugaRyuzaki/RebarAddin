using WpfCustomControls;

namespace R02_BeamsRebar.LanguageModel
{
    public class SettingLanguage : BaseViewModel
    {

        private string _ReinforcementStructural;
        public string ReinforcementStructural { get { return _ReinforcementStructural; } set { _ReinforcementStructural = value; OnPropertyChanged(); } }
        private string _SectionSetting;
        public string SectionSetting { get { return _SectionSetting; } set { _SectionSetting = value; OnPropertyChanged(); } }
        private string _DetailSetting;
        public string DetailSetting { get { return _DetailSetting; } set { _DetailSetting = value; OnPropertyChanged(); } }
       
        public SettingLanguage(string language)
        {
            ChangedLanguage(language);
        }
        public void ChangedLanguage(string language)
        {
            switch (language)
            {
                case "EN": GetLanguageEN(); break;
                case "VN": GetLanguageVN(); break;
                default: GetLanguageEN(); break;
            }
        }
        private void GetLanguageEN()
        {
            ReinforcementStructural = "Reinforcement Structural";
            SectionSetting = "Section Setting";
            DetailSetting = "Detail Setting";
        }
        private void GetLanguageVN()
        {
            ReinforcementStructural = "Kết cấu Cốt thép";
            SectionSetting = "Cài đặt mặt cắt";
            DetailSetting = "Cài đặt Chi tiết";
        }
    }
}
