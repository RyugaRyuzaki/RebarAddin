using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCustomControls.LanguageModel.R02_BeamRebar
{
    public class SpeciaBarLanguage : BaseViewModel
    {

        private string _SideBar;
        public string SideBar { get { return _SideBar; } set { _SideBar = value; OnPropertyChanged(); } }
        private string _ExLeft;
        public string ExLeft { get { return _ExLeft; } set { _ExLeft = value; OnPropertyChanged(); } }
        private string _ExRight;
        public string ExRight { get { return _ExRight; } set { _ExRight = value; OnPropertyChanged(); } }
        private string _Span;
        public string Span { get { return _Span; } set { _Span = value; OnPropertyChanged(); } }
        private string _Side;
        public string Side { get { return _Side; } set { _Side = value; OnPropertyChanged(); } }
        private string _SpecialPoint;
        public string SpecialPoint { get { return _SpecialPoint; } set { _SpecialPoint = value; OnPropertyChanged(); } }

        public SpeciaBarLanguage(string language)
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
            SideBar = "Side Bar";
            ExLeft = "ExLeft";
            ExRight = "ExRight";
            Span = "Span";
            Side = "Side";
            SpecialPoint = "Special Point Beams";
        }
        private void GetLanguageVN()
        {
            SideBar = "Thép giá";
            ExLeft = "Ex Trái";
            ExRight = "Ex Phải";
            Span = "Nhịp";
            Side = "Giá";
            SpecialPoint = "Thép tập trung";
        }
    }
}
