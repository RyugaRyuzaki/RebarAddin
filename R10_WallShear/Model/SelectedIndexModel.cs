

namespace R10_WallShear
{
    public class SelectedIndexModel:BaseViewModel
    {
        #region property
        private int _SelectedWall;
        public int SelectedWall { get => _SelectedWall; set { _SelectedWall = value; OnPropertyChanged(); } }
        private int _SelectedMainBar;
        public int SelectedMainBar { get => _SelectedMainBar; set { _SelectedMainBar = value; OnPropertyChanged(); } }
        private int _SelectedCornerMainBar;
        public int SelectedCornerMainBar { get => _SelectedCornerMainBar; set { _SelectedCornerMainBar = value; OnPropertyChanged(); } }
        private int _SelectedAddBar;
        public int SelectedAddBar { get => _SelectedAddBar; set { _SelectedAddBar = value; OnPropertyChanged(); } }
        private int _SelectedStirrupType;
        public int SelectedStirrupType { get => _SelectedStirrupType; set { _SelectedStirrupType = value; OnPropertyChanged(); } }
        private int _SelectedCover;
        public int SelectedCover { get => _SelectedCover; set { _SelectedCover = value; OnPropertyChanged(); } }
        #endregion
        public SelectedIndexModel(int selectedWall, int selectedMainBar, int selectedStirrupType)
        {
            SelectedWall = selectedWall;
            SelectedMainBar = selectedMainBar;
            SelectedCornerMainBar = 0;
            SelectedAddBar = 0;
            SelectedStirrupType = selectedStirrupType;
            SelectedCover =1;
        }
    }
}
