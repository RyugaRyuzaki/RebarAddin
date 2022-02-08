using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCustomControls.LanguageModel.R11_FoundationPile
{
    public class MenuLanguage:BaseViewModel
    {

        private string _Setting;
        public string Setting { get { return _Setting; } set { _Setting = value; OnPropertyChanged(); } }
        private string _Geometry;
        public string Geometry { get { return _Geometry; } set { _Geometry = value; OnPropertyChanged(); } }
        private string _PileDetail;
        public string PileDetail { get { return _PileDetail; } set { _PileDetail = value; OnPropertyChanged(); } }
        private string _Reinforcement;
        public string Reinforcement { get { return _Reinforcement; } set { _Reinforcement = value; OnPropertyChanged(); } }
        private string _FoundationHeader;
        public string FoundationHeader { get { return _FoundationHeader; } set { _FoundationHeader = value; OnPropertyChanged(); } }
        private string _CreateFoundation;
        public string CreateFoundation { get { return _CreateFoundation; } set { _CreateFoundation = value; OnPropertyChanged(); } }
        private string _CreatePileDetail;
        public string CreatePileDetail { get { return _CreatePileDetail; } set { _CreatePileDetail = value; OnPropertyChanged(); } }
        private string _CreateReinforcement;
        public string CreateReinforcement { get { return _CreateReinforcement; } set { _CreateReinforcement = value; OnPropertyChanged(); } }
        private string _Cancel;
        public string Cancel { get { return _Cancel; } set { _Cancel = value; OnPropertyChanged(); } }
        public MenuLanguage(string language)
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
            Setting = "Setting";
            Geometry = "Geometry";
            PileDetail = "Pile Detail";
            Reinforcement = "Reinforcement";
            FoundationHeader = "Foundation";
            CreateFoundation = "Create Foundation";
            CreatePileDetail = "Create PileDetail";
            CreateReinforcement = "Create Reinforcement";
            Cancel = "Cancel";
        }
        private void GetLanguageVN()
        {
            Setting = "Cài Đặt";
            Geometry = "Hình Dạng";
            PileDetail = "Chi tiết Cọc";
            Reinforcement = "Cốt thép";
            FoundationHeader = "Móng";
            CreateFoundation = "Tạo Móng";
            CreatePileDetail = "Tạo MB Cọc";
            CreateReinforcement = "Tạo Cốt thép";
            Cancel = "Huỷ";
        }
    }
}
