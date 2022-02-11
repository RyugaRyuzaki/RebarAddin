using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCustomControls.LanguageModel
{
    public class MenuLanguage : BaseViewModel
    {

        private string _Setting;
        public string Setting { get { return _Setting; } set { _Setting = value; OnPropertyChanged(); } }
        private string _Geometry;
        public string Geometry { get { return _Geometry; } set { _Geometry = value; OnPropertyChanged(); } }
        private string _Stirrups;
        public string Stirrups { get { return _Stirrups; } set { _Stirrups = value; OnPropertyChanged(); } }
        private string _AdditionalStirrups;
        public string AdditionalStirrups { get { return _AdditionalStirrups; } set { _AdditionalStirrups = value; OnPropertyChanged(); } }
        private string _Bars;
        public string Bars { get { return _Bars; } set { _Bars = value; OnPropertyChanged(); } }
        private string _TopDowels;
        public string TopDowels { get { return _TopDowels; } set { _TopDowels = value; OnPropertyChanged(); } }
        private string _BottomDowels;
        public string BottomDowels { get { return _BottomDowels; } set { _BottomDowels = value; OnPropertyChanged(); } }
        private string _BarsDivision;
        public string BarsDivision { get { return _BarsDivision; } set { _BarsDivision = value; OnPropertyChanged(); } }
        private string _PileDetail;
        public string PileDetail { get { return _PileDetail; } set { _PileDetail = value; OnPropertyChanged(); } }
        private string _Reinforcement;
        public string Reinforcement { get { return _Reinforcement; } set { _Reinforcement = value; OnPropertyChanged(); } }

        public MenuLanguage(string languge)
        {
            ChangedLanguage(languge);
        }
        public void ChangedLanguage(string languge)
        {
            switch (languge)
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
            Stirrups = "Stirrups";
            AdditionalStirrups = "Additional Stirrups";
            Bars = "Bars";
            TopDowels = "Top Dowels";
            BottomDowels = "Bottom Dowels";
            BarsDivision = "Bars Division";
            PileDetail = "Pile Detail";
            Reinforcement = "Reinforcement";
        }
        private void GetLanguageVN()
        {
            Setting = "Cài Đặt";
            Geometry = "Hình Dạng";
            Stirrups = "Thép Đai";
            AdditionalStirrups = "Đai tăng cường";
            Bars = "Thép chủ";
            TopDowels = "Neo Thép Trên";
            BottomDowels = "Neo Thép Dưới";
            BarsDivision = "Cắt Thép";
            PileDetail = "Chi tiết Cọc";
            Reinforcement = "Cốt thép";
        }
    }
}
