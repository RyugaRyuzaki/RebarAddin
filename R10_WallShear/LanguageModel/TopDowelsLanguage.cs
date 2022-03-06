using WpfCustomControls;
namespace R10_WallShear.LanguageModel
{
    public class TopDowelsLanguage : BaseViewModel
    {

        private string _WallsProperty;
        public string WallsProperty { get { return _WallsProperty; } set { _WallsProperty = value; OnPropertyChanged(); } }

        private string _ApplyAllBar;
        public string ApplyAllBar { get { return _ApplyAllBar; } set { _ApplyAllBar = value; OnPropertyChanged(); } }

        private string _TopType;
        public string TopType { get { return _TopType; } set { _TopType = value; OnPropertyChanged(); } }

        private string _TopDowelsProperty;
        public string TopDowelsProperty { get { return _TopDowelsProperty; } set { _TopDowelsProperty = value; OnPropertyChanged(); } }
        private string _TopDowels;
        public string TopDowels { get { return _TopDowels; } set { _TopDowels = value; OnPropertyChanged(); } }

        private string _CornerTopDowelsProperty;
        public string CornerTopDowelsProperty { get { return _CornerTopDowelsProperty; } set { _CornerTopDowelsProperty = value; OnPropertyChanged(); } }
        private string _CornerTopDowels;
        public string CornerTopDowels { get { return _CornerTopDowels; } set { _CornerTopDowels = value; OnPropertyChanged(); } }

        private string _FixedToUp;
        public string FixedToUp { get { return _FixedToUp; } set { _FixedToUp = value; OnPropertyChanged(); } }

       
        public TopDowelsLanguage(string language)
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
            WallsProperty = "Walls Property";
          
            ApplyAllBar = "Apply All Bar";
            TopType = "Top Type";
            TopDowelsProperty = "Top Dowels Property";
            TopDowels = "Top Dowels";
            CornerTopDowelsProperty = "Corner Top Dowels Property";
            CornerTopDowels = "Corner Top Dowels";
            FixedToUp = "Fixed to Up ";
        
        }
        private void GetLanguageVN()
        {
            WallsProperty = "Thông số Tường";
           
            ApplyAllBar = "Áp dụng hết";
            TopType = "Loại Trên";
            TopDowelsProperty = "Thông số Neo thép trên";
            TopDowels = "Neo Thép Trên";
            CornerTopDowelsProperty = "Thông số Neo thép góc trên";
            CornerTopDowels = "Neo Thép góc Trên";
            FixedToUp = "Chẵn Tường Trên";
         
        }
    }
}
