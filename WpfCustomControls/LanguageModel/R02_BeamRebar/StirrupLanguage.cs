using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCustomControls.LanguageModel.R02_BeamRebar
{
    public class StirrupLanguage : BaseViewModel
    {

        private string _StirrupSpan;
        public string StirrupSpan { get { return _StirrupSpan; } set { _StirrupSpan = value; OnPropertyChanged(); } }
        private string _DistributeStirrup;
        public string DistributeStirrup { get { return _DistributeStirrup; } set { _DistributeStirrup = value; OnPropertyChanged(); } }
        private string _DistributeMain;
        public string DistributeMain { get { return _DistributeMain; } set { _DistributeMain = value; OnPropertyChanged(); } }
        private string _DistributeType;
        public string DistributeType { get { return _DistributeType; } set { _DistributeType = value; OnPropertyChanged(); } }
        private string _DetailStirrup;
        public string DetailStirrup { get { return _DetailStirrup; } set { _DetailStirrup = value; OnPropertyChanged(); } }
        private string _Span;
        public string Span { get { return _Span; } set { _Span = value; OnPropertyChanged(); } }
        private string _AntiShikage;
        public string AntiShikage { get { return _AntiShikage; } set { _AntiShikage = value; OnPropertyChanged(); } }
        public StirrupLanguage(string language)
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
            StirrupSpan = "Stirrup of Span";
            DistributeStirrup = "Distribute Stirrup";
            DistributeMain = "Dítribute Main";
            DistributeType = "Dítribute Type";
            DetailStirrup = "Detail Stirrup";
            Span = "Span";
            AntiShikage = "Anti-Shikage";
        }
        private void GetLanguageVN()
        {
            StirrupSpan = "Đai nhịp";
            DistributeStirrup = "Phân phối đai";
            DistributeMain = "Phân phối đai chính";
            DistributeType = "Loại Phân phối";
            DetailStirrup = "Chi tiết Đai";
            Span = "Nhịp";
            AntiShikage = "Đai tăng cường";
        }
    }
}
