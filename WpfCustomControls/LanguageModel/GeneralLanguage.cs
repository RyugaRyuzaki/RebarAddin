

namespace WpfCustomControls.LanguageModel
{
    public class GeneralLanguage : BaseViewModel
    {

        private string _NameBar;
        public string NameBar { get { return _NameBar; } set { _NameBar = value; OnPropertyChanged(); } }
        private string _LayerBar;
        public string LayerBar { get { return _LayerBar; } set { _LayerBar = value; OnPropertyChanged(); } }
        private string _Bar;
        public string Bar { get { return _Bar; } set { _Bar = value; OnPropertyChanged(); } }
        private string _Type;
        public string Type { get { return _Type; } set { _Type = value; OnPropertyChanged(); } }
        private string _Distance;
        public string Distance { get { return _Distance; } set { _Distance = value; OnPropertyChanged(); } }


        private string _NumberBar;
        public string NumberBar { get { return _NumberBar; } set { _NumberBar = value; OnPropertyChanged(); } }
        private string _BarNumber;
        public string BarNumber { get { return _BarNumber; } set { _BarNumber = value; OnPropertyChanged(); } }
        private string _HookLength;
        public string HookLength { get { return _HookLength; } set { _HookLength = value; OnPropertyChanged(); } }
        private string _HookType;
        public string HookType { get { return _HookType; } set { _HookType = value; OnPropertyChanged(); } }
        private string _ColumnsNumber;
        public string ColumnsNumber { get { return _ColumnsNumber; } set { _ColumnsNumber = value; OnPropertyChanged(); } }
        private string _BeamsNumber;
        public string BeamsNumber { get { return _BeamsNumber; } set { _BeamsNumber = value; OnPropertyChanged(); } }
        private string _WallsNumber;
        public string WallsNumber { get { return _WallsNumber; } set { _WallsNumber = value; OnPropertyChanged(); } }

        private string _Apply;
        public string Apply { get { return _Apply; } set { _Apply = value; OnPropertyChanged(); } }
      

        private string _Modify;
        public string Modify { get { return _Modify; } set { _Modify = value; OnPropertyChanged(); } }
        private string _AddLayer;
        public string AddLayer { get { return _AddLayer; } set { _AddLayer = value; OnPropertyChanged(); } }
        private string _DeleteLayer;
        public string DeleteLayer { get { return _DeleteLayer; } set { _DeleteLayer = value; OnPropertyChanged(); } }
        public GeneralLanguage(string languge)
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
            NameBar = "Name Bar";
            LayerBar = "Layer";
            Bar = "Bar";
            Type = "Type";
            Distance = "Distance";
            NumberBar = "No";
            BarNumber = "Bar No";
            HookLength = "HookLength";
            HookType = "Hook Type";
            ColumnsNumber = "Column No";
            BeamsNumber = "Beam No";
            WallsNumber = "Wall No";

            Apply = "Apply";
           
            Modify = "Cancel";
            AddLayer = "Add Layer";
            DeleteLayer = "Delete Layer";
        }
        private void GetLanguageVN()
        {
            NameBar = "Loại Thép";
            LayerBar = "Lớp";
            Bar = "Thép";
            Type = "Loại";
            Distance = "KC";
            NumberBar = "SL";
            BarNumber = "Số hiệu";
            HookLength = "Dài Móc";
            HookType = "Loại Móc";
            ColumnsNumber = "Cột số";
            BeamsNumber = "Dầm Số";
            WallsNumber = "Tường Số";

            Apply = "Áp dụng";
           
            Modify = "Sửa";
            AddLayer = "Thêm Lớp";
            DeleteLayer = "Xoá Lớp";
        }
    }
}
