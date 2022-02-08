using WpfCustomControls;

namespace R01_ColumnsRebar
{
    public class SelectedIndexModel:BaseViewModel
    {
        #region property
        private int _SelectedColumn;
        public int SelectedColumn { get => _SelectedColumn; set { _SelectedColumn = value; OnPropertyChanged(); } }
        private int _SelectedMainBar;
        public int SelectedMainBar { get => _SelectedMainBar; set { _SelectedMainBar = value; OnPropertyChanged(); } }
        private int _SelectedAddBar;
        public int SelectedAddBar { get => _SelectedAddBar; set { _SelectedAddBar = value; OnPropertyChanged(); } }
        private int _SelectedStirrupType;
        public int SelectedStirrupType { get => _SelectedStirrupType; set { _SelectedStirrupType = value; OnPropertyChanged(); } }
        private int _SelectedCover;
        public int SelectedCover { get => _SelectedCover; set { _SelectedCover = value; OnPropertyChanged(); } }
        #endregion
        public SelectedIndexModel(int selectedColumn, int selectedMainBar, int selectedStirrupType)
        {
            SelectedColumn = selectedColumn;
            SelectedMainBar = selectedMainBar;
            SelectedAddBar = 0;
            SelectedStirrupType = selectedStirrupType;
            SelectedCover =1;
        }
    }
}
