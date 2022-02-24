#region Namespaces
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using WpfCustomControls;
using WpfCustomControls.ViewModel;
using WpfCustomControls.LanguageModel;
using R10_WallShear.ViewModel;
#endregion
using DSP;
using System.Windows;

namespace R10_WallShear
{
    public class WallShearViewModel : BaseViewModel
    {
        public UIDocument UiDoc;
        public Document Doc;
        private UnitProject _Unit;
        public UnitProject Unit { get { return _Unit; } set { _Unit = value; OnPropertyChanged(); } }
        private List<Element> _Walls;
        public List<Element> Walls { get => _Walls; set { _Walls = value; OnPropertyChanged(); } }
        private TransactionGroup _TransactionGroup;
        public TransactionGroup TransactionGroup { get => _TransactionGroup; set { _TransactionGroup = value; OnPropertyChanged(); } }
        private WallsModel _WallsModel;
        public WallsModel WallsModel { get { return _WallsModel; } set { _WallsModel = value; OnPropertyChanged(); } }
        #region Icommand
        public ICommand LoadWindowCommand { get; set; }
        public ICommand SelectionMenuCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand OKCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        #endregion
        #region Menu ViewModel
        private BaseViewModel _selectedViewModel;
        public BaseViewModel SelectedViewModel { get { return _selectedViewModel; } set { _selectedViewModel = value; OnPropertyChanged(nameof(SelectedViewModel)); } }
        private SettingViewModel _SettingViewModel;
        public SettingViewModel SettingViewModel { get => _SettingViewModel; set { _SettingViewModel = value; OnPropertyChanged(); } }
        private GeometryViewModel _GeometryViewModel;
        public GeometryViewModel GeometryViewModel { get => _GeometryViewModel; set { _GeometryViewModel = value; OnPropertyChanged(); } }
        private BarsViewModel _BarsViewModel;
        public BarsViewModel BarsViewModel { get => _BarsViewModel; set { _BarsViewModel = value; OnPropertyChanged(); } }
        private StirrupsViewModel _StirrupsViewModel;
        public StirrupsViewModel StirrupsViewModel { get => _StirrupsViewModel; set { _StirrupsViewModel = value; OnPropertyChanged(); } }
        private AdditionalStirrupsViewModel _AdditionalStirrupsViewModel;
        public AdditionalStirrupsViewModel AdditionalStirrupsViewModel { get => _AdditionalStirrupsViewModel; set { _AdditionalStirrupsViewModel = value; OnPropertyChanged(); } }
        private TopDowelsViewModel _TopDowelsViewModel;
        public TopDowelsViewModel TopDowelsViewModel { get => _TopDowelsViewModel; set { _TopDowelsViewModel = value; OnPropertyChanged(); } }
        private BottomDowelsViewModel _BottomDowelsViewModel;
        public BottomDowelsViewModel BottomDowelsViewModel { get => _BottomDowelsViewModel; set { _BottomDowelsViewModel = value; OnPropertyChanged(); } }
        private BarsDivisionViewModel _BarsDivisionViewModel;
        public BarsDivisionViewModel BarsDivisionViewModel { get => _BarsDivisionViewModel; set { _BarsDivisionViewModel = value; OnPropertyChanged(); } }
        #endregion
        private TaskBarViewModel _TaskBarViewModel;
        public TaskBarViewModel TaskBarViewModel { get { return _TaskBarViewModel; } set { _TaskBarViewModel = value; OnPropertyChanged(); } }
        private StatusBarViewModel _StatusBarViewModel;
        public StatusBarViewModel StatusBarViewModel { get { return _StatusBarViewModel; } set { _StatusBarViewModel = value; OnPropertyChanged(); } }
        private ActionViewModel _ActionViewModel;
        public ActionViewModel ActionViewModel { get { return _ActionViewModel; } set { _ActionViewModel = value; OnPropertyChanged(); } }
        private Languages _Languages;
        public Languages Languages { get { return _Languages; } set { _Languages = value; OnPropertyChanged(); } }
        public WallShearViewModel(UIDocument uiDoc, Document doc,List<Element> walls)
        {
            #region Property
            UiDoc = uiDoc;
            Doc = doc;
            Walls = walls;
            Unit = GetUnitProject();
            WallsModel = new WallsModel(Walls,Doc,Unit);
            Languages = new Languages("EN");
            TaskBarViewModel = new TaskBarViewModel(Languages);
            StatusBarViewModel = new StatusBarViewModel(WallsModel.ProgressModel, Languages);
            StatusBarViewModel.SetStatusBarWallsShear();
            ActionViewModel = new ActionViewModel(Languages);
            ActionViewModel.SetStatusBarWallsShear();
            #endregion

            #region SelectedViewModel
            SettingViewModel = new SettingViewModel(Doc,WallsModel);
            GeometryViewModel = new GeometryViewModel(WallsModel);
            StirrupsViewModel = new StirrupsViewModel(Doc,WallsModel);
            AdditionalStirrupsViewModel = new AdditionalStirrupsViewModel(Doc, WallsModel);
            BarsViewModel = new BarsViewModel(Doc, WallsModel);   
            TopDowelsViewModel = new TopDowelsViewModel(Doc, WallsModel);
            BottomDowelsViewModel = new BottomDowelsViewModel(Doc, WallsModel);
            BarsDivisionViewModel = new BarsDivisionViewModel();
            SelectedViewModel = SettingViewModel;
            SelectionMenuCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                switch (p.Menu.SelectedIndex)
                {
                    case 0:
                        SelectedViewModel = SettingViewModel;
                        break;
                    case 1:
                        SelectedViewModel = GeometryViewModel;
                        break;
                    case 2:
                        SelectedViewModel = BarsViewModel;
                        break;
                    case 3:
                        SelectedViewModel = TopDowelsViewModel;
                        break;
                    case 4:
                        SelectedViewModel = BottomDowelsViewModel;
                        break;
                    case 5:
                        SelectedViewModel = StirrupsViewModel;
                        break;
                    case 6:
                        SelectedViewModel = AdditionalStirrupsViewModel;
                        break;
                    case 7:
                        SelectedViewModel = BarsDivisionViewModel;
                        break;
                    default:
                        break;
                }
            });
            #endregion
            #region loadwindow
            LoadWindowCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                DrawMenu(p);

            });

            #endregion
            #region Action Command
            OKCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
            });
            CancelCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                p.DialogResult = false;
               
            });
            CloseWindowCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                p.DialogResult = true;
                if (TransactionGroup.HasStarted())
                {
                    TransactionGroup.RollBack();
                    System.Windows.MessageBox.Show("Progress is Cancel!", "Stop Progress",
                        MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            });
            #endregion
        }
        #region Get Property Method

        private UnitProject GetUnitProject()
        {
            UnitProject a = new UnitProject(1, "ft");
            ForgeTypeId forgeTypeId = Doc.GetUnits().GetFormatOptions(SpecTypeId.Length).GetUnitTypeId();
            if (forgeTypeId == UnitTypeId.Centimeters)
            {
                a.UnitInt = 1; a.UnitName = "cm";
            }
            if (forgeTypeId == UnitTypeId.Decimeters)
            {
                a.UnitInt = 2; a.UnitName = "dm";
            }
            if (forgeTypeId == UnitTypeId.Feet)
            {
                a.UnitInt = 3; a.UnitName = "ft";
            }
            if (forgeTypeId == UnitTypeId.Inches)
            {
                a.UnitInt = 4; a.UnitName = "in";
            }
            if (forgeTypeId == UnitTypeId.Meters)
            {
                a.UnitInt = 5; a.UnitName = "m";
            }
            if (forgeTypeId == UnitTypeId.Millimeters)
            {
                a.UnitInt = 6; a.UnitName = "mm";
            }
            if (forgeTypeId == UnitTypeId.Inches)
            {
                a.UnitInt = 7; a.UnitName = "inUS";
            }
            if (forgeTypeId == UnitTypeId.FeetFractionalInches)
            {
                a.UnitInt = 8; a.UnitName = "ft-in";
            }
            if (forgeTypeId == UnitTypeId.FractionalInches)
            {
                a.UnitInt = 9; a.UnitName = "inch";
            }
            if (forgeTypeId == UnitTypeId.MetersCentimeters)
            {
                a.UnitInt = 10; a.UnitName = "m";
            }
            return a;
        }
        #endregion
        #region Draw

        private void DrawMenu(WallShearWindow p)
        {
            DrawIcon.DrawGeometry(p.GeometryCanvas);
            DrawIcon.DrawBarsStirrups(p.BarsCanvas, true);
            DrawIcon.DrawBarsStirrups(p.StirrupsCanvas, false);
            DrawIcon.DrawBarsAdditionalStirrups(p.AdditionalStirrupsCanvas, false);
            DrawIcon.DrawTopDowels(p.TopDowelsCanvas);
            DrawIcon.DrawBottomDowels(p.BottomDowelsCanvas);
            DrawIcon.DrawBarsDivision(p.BarsDivisionCanvas);
            DrawIcon.DrawSetting(p.SettingCanvas);
        }
        //private void DrawInfo(WallShearWindow p)
        //{
        //    p.MainCanvas.Children.Clear();
        //    DrawMainCanvas.DrawInfoColumns(p.MainCanvas, ColumnsModel, ColumnsModel.SelectedIndexModel.SelectedColumn);
        //}
        #endregion

    }
}
