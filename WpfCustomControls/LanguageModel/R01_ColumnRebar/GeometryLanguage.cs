using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCustomControls.LanguageModel.R01_ColumnRebar
{
    public class GeometryLanguage : BaseViewModel
    {

        private string _Identification;
        public string Identification { get { return _Identification; } set { _Identification = value; OnPropertyChanged(); } }
        private string _FamilyName;
        public string FamilyName { get { return _FamilyName; } set { _FamilyName = value; OnPropertyChanged(); } }
        private string _TypeName;
        public string TypeName { get { return _TypeName; } set { _TypeName = value; OnPropertyChanged(); } }
        private string _Style;
        public string Style { get { return _Style; } set { _Style = value; OnPropertyChanged(); } }
        private string _ColumnsDimention;
        public string ColumnsDimention { get { return _ColumnsDimention; } set { _ColumnsDimention = value; OnPropertyChanged(); } }
        private string _Number;
        public string Number { get { return _Number; } set { _Number = value; OnPropertyChanged(); } }
        private string _ColumnsProperty;
        public string ColumnsProperty { get { return _ColumnsProperty; } set { _ColumnsProperty = value; OnPropertyChanged(); } }
       
        public GeometryLanguage(string language)
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
            Identification = "Identification";
            FamilyName = "Family Name";
            TypeName = "Type Name";
            Style = "Style";
            ColumnsDimention = "Columns Dimention";
            Number = "Top Dowels";
            ColumnsProperty = "Columns Property";
         
        }
        private void GetLanguageVN()
        {
            Identification = "Nhận dạng Cột";
            FamilyName = "Tên Family";
            TypeName = "Tên Type";
            Style = "Loại Cột";
            ColumnsDimention = "Kích thước cột";
            Number = "Số";
            ColumnsProperty = "Thông số Cột";
          
        }
    }
}
