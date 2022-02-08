using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCustomControls.LanguageModel.R01ColumnRebar
{
    public class SettingLanguage : BaseViewModel
    {

        private string _RebarShapeHook;
        public string RebarShapeHook { get { return _RebarShapeHook; } set { _RebarShapeHook = value; OnPropertyChanged(); } }
        private string _StirrupShape;
        public string StirrupShape { get { return _StirrupShape; } set { _StirrupShape = value; OnPropertyChanged(); } }
        private string _AntiShape;
        public string AntiShape { get { return _AntiShape; } set { _AntiShape = value; OnPropertyChanged(); } }
        private string _Hook;
        public string Hook { get { return _Hook; } set { _Hook = value; OnPropertyChanged(); } }
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
            RebarShapeHook = "Rebar Shape and Hook";
            StirrupShape = "StirrupShape";
            AntiShape = "AntiShape";
            Hook = "Hook";
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
            RebarShapeHook = "Dạng Thép Và Hook";
            StirrupShape = "Dạng Thép Đai";
            AntiShape = "Anti Đai";
            Hook = "Móc Hook";
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
