using WpfCustomControls;

namespace R10_WallShear.LanguageModel
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
        private string _WallsDimention;
        public string WallsDimention { get { return _WallsDimention; } set { _WallsDimention = value; OnPropertyChanged(); } }
        
        private string _CornerProperty;
        public string CornerProperty { get { return _CornerProperty; } set { _CornerProperty = value; OnPropertyChanged(); } }
        private string _WallsProperty;
        public string WallsProperty { get { return _WallsProperty; } set { _WallsProperty = value; OnPropertyChanged(); } }
        private string _Iscorner;
        public string Iscorner { get { return _Iscorner; } set { _Iscorner = value; OnPropertyChanged(); } }
        private string _ApplyAllWalls;
        public string ApplyAllWalls { get { return _ApplyAllWalls; } set { _ApplyAllWalls = value; OnPropertyChanged(); } }
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
            WallsDimention = "Walls Dimention";
            CornerProperty = "Corner Property";
            WallsProperty = "Walls Property";
            Iscorner = "Iscorner";
            ApplyAllWalls = "Apply All Walls";
         
        }
        private void GetLanguageVN()
        {
            Identification = "Nhận dạng Tường";
            FamilyName = "Tên Family";
            TypeName = "Tên Type";
            Style = "Loại Tường";
            WallsDimention = "Kích thước Tường";
            CornerProperty = "Thông số Góc";
            WallsProperty = "Thông số Tường";
            Iscorner = "Có Góc";
            ApplyAllWalls = "Áp dụng các Tường";
          
        }
    }
}
