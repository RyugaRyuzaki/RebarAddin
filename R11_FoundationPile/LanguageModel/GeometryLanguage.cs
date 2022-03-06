using WpfCustomControls;

namespace R11_FoundationPile.LanguageModel
{
    public class GeometryLanguage : BaseViewModel
    {

        private string _GroupFoundationSetting;
        public string GroupFoundationSetting { get { return _GroupFoundationSetting; } set { _GroupFoundationSetting = value; OnPropertyChanged(); } }
        private string _FoundationType;
        public string FoundationType { get { return _FoundationType; } set { _FoundationType = value; OnPropertyChanged(); } }   
        private string _NumberPile;
        public string NumberPile { get { return _NumberPile; } set { _NumberPile = value; OnPropertyChanged(); } }
        private string _Image;
        public string Image { get { return _Image; } set { _Image = value; OnPropertyChanged(); } }
        private string _Add;
        public string Add { get { return _Add; } set { _Add = value; OnPropertyChanged(); } }
        private string _Delete;
        public string Delete { get { return _Delete; } set { _Delete = value; OnPropertyChanged(); } }

        private string _Generate;
        public string Generate { get { return _Generate; } set { _Generate = value; OnPropertyChanged(); } }
        private string _Modify;
        public string Modify { get { return _Modify; } set { _Modify = value; OnPropertyChanged(); } }

        private string _Reverse;
        public string Reverse { get { return _Reverse; } set { _Reverse = value; OnPropertyChanged(); } }  
        private string _FoundationProperty;
        public string FoundationProperty { get { return _FoundationProperty; } set { _FoundationProperty = value; OnPropertyChanged(); } }
        private string _FoundationRepresentative;
        public string FoundationRepresentative { get { return _FoundationRepresentative; } set { _FoundationRepresentative = value; OnPropertyChanged(); } }       
        private string _FoundationApply;
        public string FoundationApply { get { return _FoundationApply; } set { _FoundationApply = value; OnPropertyChanged(); } }   
        private string _PileProperty;
        public string PileProperty { get { return _PileProperty; } set { _PileProperty = value; OnPropertyChanged(); } }

      
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
            GroupFoundationSetting = "Group Foundation Setting";
            FoundationType = "Foundation Type";
            NumberPile = "Number Pile";
            Image = "Image";
            Add = "Add";
            Delete = "Delete";
            Generate = "Generate";
            Modify = "Modify";
            Reverse = "Reverse";
            FoundationProperty = "Foundation Property";
            FoundationRepresentative = "Foundation Representative";       
            FoundationApply = "Apply";
            PileProperty = "Pile Property";
        }
        private void GetLanguageVN()
        {
            GroupFoundationSetting = "Cài đặt Nhóm Móng";
            FoundationType = "Type Móng";
            NumberPile = "Số Cọc";
            Image = "Hình dạng";
            Add = "Thêm";
            Delete = "Xoá";
            Generate = "Khởi tạo";
            Modify = "Sửa";
            Reverse = "Đảo chiều";
            FoundationProperty = "Thông sô Móng";
            FoundationRepresentative = "Móng đại diện";
            FoundationApply = "Áp dụng";
            PileProperty = "Thông số Cọc";
        }
    }
}
