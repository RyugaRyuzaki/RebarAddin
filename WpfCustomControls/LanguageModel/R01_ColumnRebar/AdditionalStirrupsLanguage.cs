using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCustomControls.LanguageModel.R01_ColumnRebar
{
    public class AdditionalStirrupsLanguage : BaseViewModel
    {

        private string _AdditionalProperty;
        public string AdditionalProperty { get { return _AdditionalProperty; } set { _AdditionalProperty = value; OnPropertyChanged(); } }
        private string _ColumnsNo;
        public string ColumnsNo { get { return _ColumnsNo; } set { _ColumnsNo = value; OnPropertyChanged(); } }
        private string _AdditionalHorizontal;
        public string AdditionalHorizontal { get { return _AdditionalHorizontal; } set { _AdditionalHorizontal = value; OnPropertyChanged(); } }
        private string _Horizontal;
        public string Horizontal { get { return _Horizontal; } set { _Horizontal = value; OnPropertyChanged(); } }
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
        public AdditionalStirrupsLanguage(string language)
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
            AdditionalProperty = "Additional Property";
            ColumnsNo = "Columns No";
            AdditionalHorizontal = "Additional Horizontal";
            Horizontal = "Horizontal";
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
            AdditionalProperty = "Thông số Đai tăng cường";
            ColumnsNo = "Cột Số";
            AdditionalHorizontal = "Tăng cường ngang";
            Horizontal = "Phương ngang";
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
