using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace R10_WallShear.ViewModel
{
    public class BottomDowelsViewModel :BaseViewModel
    {
        #region property
        public Document Doc;
        private WallsModel _WallsModel;
        public WallsModel WallsModel { get => _WallsModel; set { _WallsModel = value; OnPropertyChanged(); } }
        #endregion
        #region Selected
        private BarMainModel _SelectedWall;
        public BarMainModel SelectedWall { get => _SelectedWall; set { _SelectedWall = value; OnPropertyChanged(); } }
        private BarModel _SelectedBar;
        public BarModel SelectedBar { get => _SelectedBar; set { _SelectedBar = value; OnPropertyChanged(); } }
        private BarModel _SelectedBarCorner;
        public BarModel SelectedBarCorner { get => _SelectedBarCorner; set { _SelectedBarCorner = value; OnPropertyChanged(); } }
        #endregion
        private bool _IsEnabledTopDowels;
        public bool IsEnabledTopDowels { get => _IsEnabledTopDowels; set { _IsEnabledTopDowels = value; OnPropertyChanged(); } }
        private bool _IsEnabledTopTypeDowels;
        public bool IsEnabledTopTypeDowels { get => _IsEnabledTopTypeDowels; set { _IsEnabledTopTypeDowels = value; OnPropertyChanged(); } }
        private bool _IsEnabledTopDowelsCorner;
        public bool IsEnabledTopDowelsCorner { get => _IsEnabledTopDowelsCorner; set { _IsEnabledTopDowelsCorner = value; OnPropertyChanged(); } }
        private bool _IsEnabledTopTypeDowelsCorner;
        public bool IsEnabledTopTypeDowelsCorner { get => _IsEnabledTopTypeDowelsCorner; set { _IsEnabledTopTypeDowelsCorner = value; OnPropertyChanged(); } }
        private bool _IsLock;
        public bool IsLock { get => _IsLock; set { _IsLock = value; OnPropertyChanged(); } }

        #region Icommand
        public ICommand LoadTopDowelsCommand { get; set; }
        public ICommand SelectionWallDowelsChangedCommand { get; set; }
        public ICommand SelectionBarChangedCommand { get; set; }
        public ICommand ApplyAllBarCommand { get; set; }

        public ICommand CheckTopDowelsCommand { get; set; }
        public ICommand TopDowelsLaTextChangedCommand { get; set; }
        public ICommand TopDowelsLbTextChangedCommand { get; set; }
        public ICommand SelectionTopTypeDowelsChangedCommand { get; set; }
        public ICommand PushMainTopBarDowelsCommand { get; set; }
        public ICommand PushCornerTopBarDowelsCommand { get; set; }

        public ICommand SelectionBarCornerChangedCommand { get; set; }
        public ICommand CheckTopDowelsCornerCommand { get; set; }
        public ICommand TopDowelsCornerLaTextChangedCommand { get; set; }
        public ICommand TopDowelsCornerLbTextChangedCommand { get; set; }
        public ICommand SelectionTopTypeDowelsCornerChangedCommand { get; set; }
        public ICommand PushMainTopBarCornerDowelsCommand { get; set; }
        public ICommand PushCornerTopBarCornerDowelsCommand { get; set; }

        #endregion
        public BottomDowelsViewModel(Document document, WallsModel wallsModel)
        {
            #region Property
            Doc = document;
            WallsModel = wallsModel;
            #endregion
        }
    }
}
