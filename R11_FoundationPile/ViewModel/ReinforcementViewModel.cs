using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using R11_FoundationPile.View;

namespace R11_FoundationPile.ViewModel
{
    public class ReinforcementViewModel : BaseViewModel
    {
        #region Property
        public Document Doc;
        private UnitProject _Unit;
        public UnitProject Unit { get { return _Unit; } set { _Unit = value; OnPropertyChanged(); } }
        private FoundationPileModel _FoundationPileModel;
        public FoundationPileModel FoundationPileModel{get => _FoundationPileModel; set{ _FoundationPileModel = value; OnPropertyChanged(); }}
        private GroupFoundationModel _SelectedGroupFoundationModel;
        public GroupFoundationModel SelectedGroupFoundationModel
        {
            get => _SelectedGroupFoundationModel; set
            {
                _SelectedGroupFoundationModel = value; OnPropertyChanged();
                if (SelectedGroupFoundationModel != null)
                {
                    SelectedFoundationModel = SelectedGroupFoundationModel.FoundationModels.Where(x => x.IsRepresentative).FirstOrDefault();
                    MainAddTopBarVisible = (SelectedFoundationModel.IsMainTopBar) ? (System.Windows.Visibility.Visible) : (System.Windows.Visibility.Collapsed);
                    MainAddHorizontalBarVisible = (SelectedFoundationModel.IsMainAddHorizontalBar) ? (System.Windows.Visibility.Visible) : (System.Windows.Visibility.Collapsed);
                    MainAddVerticalBarVisible = (SelectedFoundationModel.IsMainAddVerticalBar) ? (System.Windows.Visibility.Visible) : (System.Windows.Visibility.Collapsed);
                    SecondaryAddTopBarVisible = (SelectedFoundationModel.IsSecondaryTopBar) ? (System.Windows.Visibility.Visible) : (System.Windows.Visibility.Collapsed);
                    SecondaryAddHorizontalBarVisible = (SelectedFoundationModel.IsSecondaryAddHorizontalBar) ? (System.Windows.Visibility.Visible) : (System.Windows.Visibility.Collapsed);
                    SecondaryAddVerticalBarVisible = (SelectedFoundationModel.IsSecondaryAddVerticalBar) ? (System.Windows.Visibility.Visible) : (System.Windows.Visibility.Collapsed);
                }
            }
        }
        private FoundationModel _SelectedFoundationModel;
        public FoundationModel SelectedFoundationModel{get => _SelectedFoundationModel; set { _SelectedFoundationModel = value; OnPropertyChanged();}}
        private ObservableCollection<string> _AllSpans;
        public ObservableCollection<string> AllSpans { get { if (_AllSpans == null) { _AllSpans = new ObservableCollection<string>(new List<string> { "Horizontal", "Vertical" }); } return _AllSpans; } set { _AllSpans = value; OnPropertyChanged();
            } }
       
        #endregion
        #region Visibility
        private System.Windows.Visibility _MainAddTopBarVisible;    // Overlap pile to foundation
        public System.Windows.Visibility MainAddTopBarVisible { get => _MainAddTopBarVisible; set { _MainAddTopBarVisible = value; OnPropertyChanged(); } }
        private System.Windows.Visibility _MainAddHorizontalBarVisible;    // Overlap pile to foundation
        public System.Windows.Visibility MainAddHorizontalBarVisible { get => _MainAddHorizontalBarVisible; set { _MainAddHorizontalBarVisible = value; OnPropertyChanged(); } }
        private System.Windows.Visibility _MainAddVerticalBarVisible;    // Overlap pile to foundation
        public System.Windows.Visibility MainAddVerticalBarVisible { get => _MainAddVerticalBarVisible; set { _MainAddVerticalBarVisible = value; OnPropertyChanged(); } }
        private System.Windows.Visibility _SecondaryAddTopBarVisible;    // Overlap pile to foundation
        public System.Windows.Visibility SecondaryAddTopBarVisible { get => _SecondaryAddTopBarVisible; set { _SecondaryAddTopBarVisible = value; OnPropertyChanged(); } }
        private System.Windows.Visibility _SecondaryAddHorizontalBarVisible;    // Overlap pile to foundation
        public System.Windows.Visibility SecondaryAddHorizontalBarVisible { get => _SecondaryAddHorizontalBarVisible; set { _SecondaryAddHorizontalBarVisible = value; OnPropertyChanged(); } }
        private System.Windows.Visibility _SecondaryAddVerticalBarVisible;    // Overlap pile to foundation
        public System.Windows.Visibility SecondaryAddVerticalBarVisible { get => _SecondaryAddVerticalBarVisible; set { _SecondaryAddVerticalBarVisible = value; OnPropertyChanged(); } }

        #endregion
        private ObservableCollection<int> _NumberBars;
        public ObservableCollection<int> NumberBars
        {
            get { if (_NumberBars == null) { _NumberBars = new ObservableCollection<int> {1,2,3,4,5,6,7,8,9,10 }; } return _NumberBars; }
            set
            {
                _NumberBars = value; OnPropertyChanged();
            }
        }
        #region Icommand
        public ICommand LoadReinforcementViewCommand { get; set; }
        public ICommand CheckMainAddTopBarCommand { get; set; }
        public ICommand CheckMainAddHorizontalBarCommand { get; set; }
        public ICommand CheckMainAddVerticalBarCommand { get; set; }
        public ICommand CheckSecondaryAddTopBarCommand { get; set; }
        public ICommand CheckSecondaryAddHorizontalBarCommand { get; set; }
        public ICommand CheckSecondaryAddVerticalBarCommand { get; set; }
        public ICommand FixedMainBottomBarCommand { get; set; }
        public ICommand FixedMainTopBarCommand { get; set; }
        public ICommand FixedSecondaryBottomBarCommand { get; set; }
        public ICommand FixedSecondaryTopBarCommand { get; set; }

        public ICommand SelectionChangedGroupFoundationCommand { get; set; }
        public ICommand SelectionChangedSpanOrientationCommand { get; set; }
        #endregion
        public ReinforcementViewModel(Document doc, FoundationPileModel foundationPileModel,UnitProject unit)
        {
            #region property
            Doc = doc;
            Unit = unit;
            FoundationPileModel = foundationPileModel;
            #endregion
            LoadReinforcementViewCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {
                p.MainCanvas.Width = 820;
                ReinforcementView uc = ProccessInfoClumns.FindChild<ReinforcementView>(p, "ReinforcementUC");
                DrawSpanOrientation(uc);
            });
            CheckMainAddTopBarCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {

                if (SelectedFoundationModel.IsMainTopBar)
                {
                    MainAddTopBarVisible = System.Windows.Visibility.Visible;
                }
                else
                {
                    if (MainAddTopBarVisible == System.Windows.Visibility.Visible) MainAddTopBarVisible = System.Windows.Visibility.Collapsed;
                }

            });
            CheckMainAddHorizontalBarCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {

                if (SelectedFoundationModel.IsMainAddHorizontalBar)
                {
                    MainAddHorizontalBarVisible = System.Windows.Visibility.Visible;
                }
                else
                {
                    if (MainAddHorizontalBarVisible == System.Windows.Visibility.Visible) MainAddHorizontalBarVisible = System.Windows.Visibility.Collapsed;
                }

            });
            CheckMainAddVerticalBarCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {

                if (SelectedFoundationModel.IsMainAddVerticalBar)
                {
                    MainAddVerticalBarVisible = System.Windows.Visibility.Visible;
                }
                else
                {
                    if (MainAddVerticalBarVisible == System.Windows.Visibility.Visible) MainAddVerticalBarVisible = System.Windows.Visibility.Collapsed;
                }

            });
            CheckSecondaryAddTopBarCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {

                if (SelectedFoundationModel.IsSecondaryTopBar)
                {
                    SecondaryAddTopBarVisible = System.Windows.Visibility.Visible;
                }
                else
                {
                    if (SecondaryAddTopBarVisible == System.Windows.Visibility.Visible) SecondaryAddTopBarVisible = System.Windows.Visibility.Collapsed;
                }

            });
            CheckSecondaryAddHorizontalBarCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {

                if (SelectedFoundationModel.IsSecondaryAddHorizontalBar)
                {
                    SecondaryAddHorizontalBarVisible = System.Windows.Visibility.Visible;
                }
                else
                {
                    if (SecondaryAddHorizontalBarVisible == System.Windows.Visibility.Visible) SecondaryAddHorizontalBarVisible = System.Windows.Visibility.Collapsed;
                }

            });
            CheckSecondaryAddVerticalBarCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {

                if (SelectedFoundationModel.IsSecondaryAddVerticalBar)
                {
                    SecondaryAddVerticalBarVisible = System.Windows.Visibility.Visible;
                }
                else
                {
                    if (SecondaryAddVerticalBarVisible == System.Windows.Visibility.Visible) SecondaryAddVerticalBarVisible = System.Windows.Visibility.Collapsed;
                }

            });
            FixedMainBottomBarCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {

                

            });
            FixedMainTopBarCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {

               

            });
            FixedSecondaryBottomBarCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {

               

            });
            FixedSecondaryTopBarCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {

               

            });
            SelectionChangedGroupFoundationCommand = new RelayCommand<FoundationPileWindow>((p) => { return SelectedGroupFoundationModel!=null; }, (p) =>
            {

                ReinforcementView uc = ProccessInfoClumns.FindChild<ReinforcementView>(p, "ReinforcementUC");
                DrawSpanOrientation(uc);

            });
            SelectionChangedSpanOrientationCommand = new RelayCommand<FoundationPileWindow>((p) => { return SelectedFoundationModel!=null; }, (p) =>
            {

                ReinforcementView uc = ProccessInfoClumns.FindChild<ReinforcementView>(p, "ReinforcementUC");
                DrawSpanOrientation(uc);

            });
        }
        #region Method
        private void DrawSpanOrientation(ReinforcementView p)
        {
            p.BarCanvas.Children.Clear();
            if (SelectedFoundationModel != null)
            {
                if (SelectedFoundationModel.BoundingLocation.Count != 0 && SelectedFoundationModel.PileModels.Count != 0)
                {
                    FoundationPileModel.DrawModelBar.GetScaleBar(SelectedFoundationModel, Unit);
                    DrawMainCanvas.DrawBarFoundation(p.BarCanvas, FoundationPileModel.DrawModelBar, SelectedFoundationModel, FoundationPileModel.SettingModel,1000,SelectedGroupFoundationModel.Image,SelectedFoundationModel.SpanOrientation);
                }
            }

        }
        #endregion

    }
}
