
using WpfCustomControls.LanguageModel;
using System.Windows;
namespace WpfCustomControls.ViewModel
{
    public class ActionViewModel   :BaseViewModel
    {
        private Languages _Languages;
        public Languages Languages { get { return _Languages; } set { _Languages = value; OnPropertyChanged(); } }
      
        #region R01
        private Visibility _CreateStirrupBarsColumns;
        public Visibility CreateStirrupBarsColumns { get => _CreateStirrupBarsColumns; set { _CreateStirrupBarsColumns = value; OnPropertyChanged(); } }
        private Visibility _CreateMainBarsColumns;
        public Visibility CreateMainBarsColumns { get => _CreateMainBarsColumns; set { _CreateMainBarsColumns = value; OnPropertyChanged(); } }
        private Visibility _CreateTagBarsColumns;
        public Visibility CreateTagBarsColumns { get => _CreateTagBarsColumns; set { _CreateTagBarsColumns = value; OnPropertyChanged(); } }
        private Visibility _CreateDetailViewColumns;
        public Visibility CreateDetailViewColumns { get => _CreateDetailViewColumns; set { _CreateDetailViewColumns = value; OnPropertyChanged(); } }
        private Visibility _CreateSectionViewColumns;
        public Visibility CreateSectionViewColumns { get => _CreateSectionViewColumns; set { _CreateSectionViewColumns = value; OnPropertyChanged(); } }
        private Visibility _CreateDimensionViewColumns;
        public Visibility CreateDimensionViewColumns { get => _CreateDimensionViewColumns; set { _CreateDimensionViewColumns = value; OnPropertyChanged(); } }
        private Visibility _CreateDimensionSectionColumns;
        public Visibility CreateDimensionSectionColumns { get => _CreateDimensionSectionColumns; set { _CreateDimensionSectionColumns = value; OnPropertyChanged(); } }
        private Visibility _CreateDetailShopColumns;
        public Visibility CreateDetailShopColumns { get => _CreateDetailShopColumns; set { _CreateDetailShopColumns = value; OnPropertyChanged(); } }
        #endregion
        #region  R02
        private Visibility _CreateRebarBeams;
        public Visibility CreateRebarBeams { get => _CreateRebarBeams; set { _CreateRebarBeams = value; OnPropertyChanged(); } }
        private Visibility _CreateViewDimensionBeams;
        public Visibility CreateViewDimensionBeams { get => _CreateViewDimensionBeams; set { _CreateViewDimensionBeams = value; OnPropertyChanged(); } }
        private Visibility _CreateDetailShopBeams;
        public Visibility CreateDetailShopBeams { get => _CreateDetailShopBeams; set { _CreateDetailShopBeams = value; OnPropertyChanged(); } }
        private Visibility _CreateRebarDetailItemBeams;
        public Visibility CreateRebarDetailItemBeams { get => _CreateRebarDetailItemBeams; set { _CreateRebarDetailItemBeams = value; OnPropertyChanged(); } }
        #endregion
        #region R10
        private Visibility _CreateStirrupBarsWallsShear;
        public Visibility CreateStirrupBarsWallsShear { get => _CreateStirrupBarsWallsShear; set { _CreateStirrupBarsWallsShear = value; OnPropertyChanged(); } }
        private Visibility _CreateMainBarsWallsShear;
        public Visibility CreateMainBarsWallsShear { get => _CreateMainBarsWallsShear; set { _CreateMainBarsWallsShear = value; OnPropertyChanged(); } }
        private Visibility _CreateTagBarsWallsShear;
        public Visibility CreateTagBarsWallsShear { get => _CreateTagBarsWallsShear; set { _CreateTagBarsWallsShear = value; OnPropertyChanged(); } }
        private Visibility _CreateDetailViewWallsShear;
        public Visibility CreateDetailViewWallsShear { get => _CreateDetailViewWallsShear; set { _CreateDetailViewWallsShear = value; OnPropertyChanged(); } }
        private Visibility _CreateSectionViewWallsShear;
        public Visibility CreateSectionViewWallsShear { get => _CreateSectionViewWallsShear; set { _CreateSectionViewWallsShear = value; OnPropertyChanged(); } }
        private Visibility _CreateDimensionViewWallsShear;
        public Visibility CreateDimensionViewWallsShear { get => _CreateDimensionViewWallsShear; set { _CreateDimensionViewWallsShear = value; OnPropertyChanged(); } }
        private Visibility _CreateDimensionSectionWallsShear;
        public Visibility CreateDimensionSectionWallsShear { get => _CreateDimensionSectionWallsShear; set { _CreateDimensionSectionWallsShear = value; OnPropertyChanged(); } }
        private Visibility _CreateDetailShopWallsShear;
        public Visibility CreateDetailShopWallsShear { get => _CreateDetailShopWallsShear; set { _CreateDetailShopWallsShear = value; OnPropertyChanged(); } }
        #endregion
        #region R11
        private Visibility _CreateGrounpFoundation;
        public Visibility CreateGrounpFoundation { get => _CreateGrounpFoundation; set { _CreateGrounpFoundation = value; OnPropertyChanged(); } }
        private Visibility _CreatePileDetail;
        public Visibility CreatePileDetail { get => _CreatePileDetail; set { _CreatePileDetail = value; OnPropertyChanged(); } }
        private Visibility _CreateReinforcement;
        public Visibility CreateReinforcement { get => _CreateReinforcement; set { _CreateReinforcement = value; OnPropertyChanged(); } }

        #endregion
        public ActionViewModel(Languages languages)
        {
            Languages = languages;
            HideAllStatus();
        }
        private void HideAllStatus()
        {
           

            HasCreateStirrupBarsColumns(false);
            HasCreateMainBarsColumns(false);
            HasCreateTagBarsColumns(false);
            HasCreateDetailViewColumns(false);
            HasCreateSectionViewColumns(false);
            HasCreateDimensionViewColumns(false);
            HasCreateDimensionSectionColumns(false);
            HasCreateDetailShopColumns(false);


            HasCreateRebarBeams(false);
            HasCreateViewDimensionBeams(false);
            HasCreateDetailShopBeams(false);
            HasCreateRebarDetailItemBeams(false);

            HasCreateStirrupBarsWallsShear(false);
            HasCreateMainBarsWallsShear(false);
            HasCreateTagBarsWallsShear(false);
            HasCreateDetailViewWallsShear(false);
            HasCreateSectionViewWallsShear(false);
            HasCreateDimensionViewWallsShear(false);
            HasCreateDimensionSectionWallsShear(false);
            HasCreateDetailShopWallsShear(false);

            HasCreateGrounpFoundation(false);
            HasCreatePileDetail(false);
            HasCreateReinforcement(false);
        }
       
        public void SetStatusBarColumns()
        {
            HasCreateStirrupBarsColumns(true);
            HasCreateMainBarsColumns(true);
            HasCreateTagBarsColumns(true);
            HasCreateDetailViewColumns(true);
            HasCreateSectionViewColumns(true);
            HasCreateDimensionViewColumns(true);
            HasCreateDimensionSectionColumns(true);
            HasCreateDetailShopColumns(true);
        }
        public void SetStatusBarBeams()
        {
            HasCreateRebarBeams(true);
            HasCreateViewDimensionBeams(true);
            HasCreateDetailShopBeams(true);
            HasCreateRebarDetailItemBeams(true);
        }
        public void SetStatusBarWallsShear()
        {
            HasCreateStirrupBarsWallsShear(true);
            HasCreateMainBarsWallsShear(true);
            HasCreateTagBarsWallsShear(true);
            HasCreateDetailViewWallsShear(true);
            HasCreateSectionViewWallsShear(true);
            HasCreateDimensionViewWallsShear(true);
            HasCreateDimensionSectionWallsShear(true);
            HasCreateDetailShopWallsShear(true);
        }

        public void SetStatusBarFoundationPile()
        {
            HasCreateGrounpFoundation(true);
            HasCreatePileDetail(true);
            HasCreateReinforcement(true);

        }
        #region   R01
        private void HasCreateStirrupBarsColumns(bool isCreate)
        {
            CreateStirrupBarsColumns = (isCreate) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        private void HasCreateMainBarsColumns(bool isCreate)
        {
            CreateMainBarsColumns = (isCreate) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        private void HasCreateTagBarsColumns(bool isCreate)
        {
            CreateTagBarsColumns = (isCreate) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        private void HasCreateDetailViewColumns(bool isCreate)
        {
            CreateDetailViewColumns = (isCreate) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        private void HasCreateSectionViewColumns(bool isCreate)
        {
            CreateSectionViewColumns = (isCreate) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        private void HasCreateDimensionViewColumns(bool isCreate)
        {
            CreateDimensionViewColumns = (isCreate) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        private void HasCreateDimensionSectionColumns(bool isCreate)
        {
            CreateDimensionSectionColumns = (isCreate) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        private void HasCreateDetailShopColumns(bool isCreate)
        {
            CreateDetailShopColumns = (isCreate) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        #endregion
        #region   R02
        private void HasCreateRebarBeams(bool isCreate)
        {
            CreateRebarBeams = (isCreate) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        private void HasCreateViewDimensionBeams(bool isCreate)
        {
            CreateViewDimensionBeams = (isCreate) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        private void HasCreateDetailShopBeams(bool isCreate)
        {
            CreateDetailShopBeams = (isCreate) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        private void HasCreateRebarDetailItemBeams(bool isCreate)
        {
            CreateRebarDetailItemBeams = (isCreate) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        #endregion
        #region   R10
        private void HasCreateStirrupBarsWallsShear(bool isCreate)
        {
            CreateStirrupBarsWallsShear = (isCreate) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        private void HasCreateMainBarsWallsShear(bool isCreate)
        {
            CreateMainBarsWallsShear = (isCreate) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        private void HasCreateTagBarsWallsShear(bool isCreate)
        {
            CreateTagBarsWallsShear = (isCreate) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        private void HasCreateDetailViewWallsShear(bool isCreate)
        {
            CreateDetailViewWallsShear = (isCreate) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        private void HasCreateSectionViewWallsShear(bool isCreate)
        {
            CreateSectionViewWallsShear = (isCreate) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        private void HasCreateDimensionViewWallsShear(bool isCreate)
        {
            CreateDimensionViewWallsShear = (isCreate) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        private void HasCreateDimensionSectionWallsShear(bool isCreate)
        {
            CreateDimensionSectionWallsShear = (isCreate) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        private void HasCreateDetailShopWallsShear(bool isCreate)
        {
            CreateDetailShopWallsShear = (isCreate) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        #endregion
        #region R11
        private void HasCreateGrounpFoundation(bool isCreate)
        {
            CreateGrounpFoundation = (isCreate) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        private void HasCreatePileDetail(bool isCreate)
        {
            CreatePileDetail = (isCreate) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        private void HasCreateReinforcement(bool isCreate)
        {
            CreateReinforcement = (isCreate) ? (Visibility.Visible) : (Visibility.Collapsed);
        }
        #endregion
    }
}
