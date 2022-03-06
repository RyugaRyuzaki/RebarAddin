using WpfCustomControls;

namespace R10_WallShear.LanguageModel
{
    public class WindowLanguage : BaseViewModel
    {

        private string _OK;
        public string OK { get { return _OK; } set { _OK = value; OnPropertyChanged(); } }

        private string _Cancel;
        public string Cancel { get { return _Cancel; } set { _Cancel = value; OnPropertyChanged(); } }
        private string _WallsProperty;
        public string WallsProperty { get { return _WallsProperty; } set { _WallsProperty = value; OnPropertyChanged(); } }

        private string _WallsSection;
        public string WallsSection { get { return _WallsSection; } set { _WallsSection = value; OnPropertyChanged(); } }
        #region R10
        private string _WallsShear;
        public string WallsShear { get { return _WallsShear; } set { _WallsShear = value; OnPropertyChanged(); } }
        private string _R10_CreateStirrupBarsWallsShear;
        public string R10_CreateStirrupBarsWallsShear { get => _R10_CreateStirrupBarsWallsShear; set { _R10_CreateStirrupBarsWallsShear = value; OnPropertyChanged(); } }
        private string _R10_CreateMainBarsWallsShear;
        public string R10_CreateMainBarsWallsShear { get => _R10_CreateMainBarsWallsShear; set { _R10_CreateMainBarsWallsShear = value; OnPropertyChanged(); } }
        private string _R10_CreateTagBarsWallsShear;
        public string R10_CreateTagBarsWallsShear { get => _R10_CreateTagBarsWallsShear; set { _R10_CreateTagBarsWallsShear = value; OnPropertyChanged(); } }
        private string _R10_CreateDetailViewWallsShear;
        public string R10_CreateDetailViewWallsShear { get => _R10_CreateDetailViewWallsShear; set { _R10_CreateDetailViewWallsShear = value; OnPropertyChanged(); } }
        private string _R10_CreateSectionViewWallsShear;
        public string R10_CreateSectionViewWallsShear { get => _R10_CreateSectionViewWallsShear; set { _R10_CreateSectionViewWallsShear = value; OnPropertyChanged(); } }
        private string _R10_CreateDimensionViewWallsShear;
        public string R10_CreateDimensionViewWallsShear { get => _R10_CreateDimensionViewWallsShear; set { _R10_CreateDimensionViewWallsShear = value; OnPropertyChanged(); } }
        private string _R10_CreateDimensionSectionWallsShear;
        public string R10_CreateDimensionSectionWallsShear { get => _R10_CreateDimensionSectionWallsShear; set { _R10_CreateDimensionSectionWallsShear = value; OnPropertyChanged(); } }
        private string _R10_CreateDetailShopWallsShear;
        public string R10_CreateDetailShopWallsShear { get => _R10_CreateDetailShopWallsShear; set { _R10_CreateDetailShopWallsShear = value; OnPropertyChanged(); } }

        #endregion

        public WindowLanguage(string languge)
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
            OK = "OK";
            Cancel = "Cancel";
            WallsShear = "Walls Shear";
            WallsProperty = "Walls Property";
            WallsSection = "Walls Section";
            R10_CreateStirrupBarsWallsShear = "Create StirrupBars";
            R10_CreateMainBarsWallsShear = "Create MainBars";
            R10_CreateTagBarsWallsShear = "Create TagBars";
            R10_CreateDetailViewWallsShear = "Create DetailView";
            R10_CreateSectionViewWallsShear = "Create SectionView";
            R10_CreateDimensionViewWallsShear = "Create DimensionView";
            R10_CreateDimensionSectionWallsShear = "Create DimensionSection";
            R10_CreateDetailShopWallsShear = "Create DetailShop";
        }
        private void GetLanguageVN()
        {
            OK = "Thực Hiện";
            Cancel = "Huỷ";
            WallsShear = "Tường";
            WallsProperty = "Thông số Tường";
            WallsSection = "Mặt cắt Tường";
            R10_CreateStirrupBarsWallsShear = "Tạo Thép Đai";
            R10_CreateMainBarsWallsShear = "Tạo Thép Chủ";
            R10_CreateTagBarsWallsShear = "Tạo Tag";
            R10_CreateDetailViewWallsShear = "Tạo Detail";
            R10_CreateSectionViewWallsShear = "Tạo MC";
            R10_CreateDimensionViewWallsShear = "Tạo KT Detail ";
            R10_CreateDimensionSectionWallsShear = "Tạo KT Mặt cắt";
            R10_CreateDetailShopWallsShear = "Tạo DetailShop";

        }
    }
}
