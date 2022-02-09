using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCustomControls.LanguageModel.R01_ColumnRebar
{
    public class BottomDowelsLanguage : BaseViewModel
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
        private string _OK;
        public string OK { get { return _OK; } set { _OK = value; OnPropertyChanged(); } }
        private string _Cancel;
        public string Cancel { get { return _Cancel; } set { _Cancel = value; OnPropertyChanged(); } }
        private string _Columns;
        public string Columns { get { return _Columns; } set { _Columns = value; OnPropertyChanged(); } }
        public BottomDowelsLanguage(string language)
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
            Stirrups = "Stirrups";
            AdditionalStirrups = "Additional Stirrups";
            Bars = "Bars";
            TopDowels = "Top Dowels";
            BottomDowels = "Bottom Dowels";
            BarsDivision = "Bars Division";
            OK = "OK";
            Cancel = "Cancel";
            Columns = "Columns";
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
            OK = "Thực hiện";
            Cancel = "Huỷ";
            Columns = "Các Cột";
        }
    }
}
