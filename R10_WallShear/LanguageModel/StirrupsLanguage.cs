using WpfCustomControls;

namespace R10_WallShear.LanguageModel
{
    public class StirrupsLanguage : BaseViewModel
    {

        private string _StirrupsProperty;
        public string StirrupsProperty { get { return _StirrupsProperty; } set { _StirrupsProperty = value; OnPropertyChanged(); } }
        private string _ApplyAllWalls;
        public string ApplyAllWalls { get { return _ApplyAllWalls; } set { _ApplyAllWalls = value; OnPropertyChanged(); } }
        private string _StirrupsParameter;
        public string StirrupsParameter { get { return _StirrupsParameter; } set { _StirrupsParameter = value; OnPropertyChanged(); } }
        private string _Bars;
        public string Bars { get { return _Bars; } set { _Bars = value; OnPropertyChanged(); } }
        private string _StirrupsDistribute;
        public string StirrupsDistribute { get { return _StirrupsDistribute; } set { _StirrupsDistribute = value; OnPropertyChanged(); } }
        private string _DítributeType;
        public string DítributeType { get { return _DítributeType; } set { _DítributeType = value; OnPropertyChanged(); } }
        private string _TiesUpToBeams;
        public string TiesUpToBeams { get { return _TiesUpToBeams; } set { _TiesUpToBeams = value; OnPropertyChanged(); } }
       
        public StirrupsLanguage(string language)
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
            StirrupsProperty = "Stirrups Property";
            ApplyAllWalls = "Apply All Columns";
            StirrupsParameter = "Stirrups Parameter";
            Bars = "Bars";
            StirrupsDistribute = "Stirrups Distribute";
            DítributeType = "Dítribute Type";
            TiesUpToBeams = "Ties Up ToBeams";
        }
        private void GetLanguageVN()
        {
            StirrupsProperty = "Thông số Đai";
            ApplyAllWalls = "Áp dụng các Cột";
            StirrupsParameter = "Parameter Đai";
            Bars = "Thép";
            StirrupsDistribute = "Phân bố Thép Đai";
            DítributeType = "Loại Phân bố";
            TiesUpToBeams = "Sát dầm";
        }
    }
}
