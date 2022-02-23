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
using R01_ColumnsRebar.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;
using WpfCustomControls;
using WpfCustomControls.ViewModel;
using WpfCustomControls.LanguageModel;
using DSP;
#endregion

namespace R01_ColumnsRebar
{
    public class ColumnsViewModel : BaseViewModel
    {
        #region property
        public UIDocument UiDoc;
        public Document Doc;
        private UnitProject _Unit;
        public UnitProject Unit { get { return _Unit; } set { _Unit = value; OnPropertyChanged(); } }
        private ColumnsModel _ColumnsModel;
        public ColumnsModel ColumnsModel { get => _ColumnsModel; set { _ColumnsModel = value; OnPropertyChanged(); } }
        private List<Element> _Columns;
        public List<Element> Columns { get => _Columns; set { _Columns = value; OnPropertyChanged(); } }
        private TransactionGroup _TransactionGroup;
        public TransactionGroup TransactionGroup { get => _TransactionGroup; set { _TransactionGroup = value; OnPropertyChanged(); } }
        private bool _UseDetailItem;
        public bool UseDetailItem { get => _UseDetailItem; set { _UseDetailItem = value; OnPropertyChanged(); } }
        #endregion
        #region Bars
        #endregion
        #region Icommand
        public ICommand LoadWindowCommand { get; set; }
        public ICommand SelectionMenuCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand OKCommand { get; set; }
        public ICommand PreviewTextInputCommand { get; set; }
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
        //private DowelsViewModel _DowelsViewModel;
        //public DowelsViewModel DowelsViewModel { get => _DowelsViewModel; set { _DowelsViewModel = value; OnPropertyChanged(); } }
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


        public ColumnsViewModel(UIDocument uiDoc, Document doc,List<Element> columns)
        {
            #region Get property
            UiDoc = uiDoc;
            Doc = doc;
            Unit = GetUnitProject();
            Columns = columns;
            ColumnsModel = new ColumnsModel(columns,Doc,Unit);
            Languages = new Languages("EN");
            TransactionGroup = new TransactionGroup(Doc);
            UseDetailItem = ColumnsModel.ConditionUseDetailItem(Doc);
            TaskBarViewModel = new TaskBarViewModel(Languages);
            
            #endregion
            #region SelectedViewModel
            SettingViewModel = new SettingViewModel(Doc, ColumnsModel, Languages);
            GeometryViewModel = new GeometryViewModel(ColumnsModel, Languages);
            StirrupsViewModel = new StirrupsViewModel(Doc, ColumnsModel, Languages);
            AdditionalStirrupsViewModel = new AdditionalStirrupsViewModel(Doc, ColumnsModel, Languages);
            BarsViewModel = new BarsViewModel(Doc, ColumnsModel, Languages);
            //DowelsViewModel = new DowelsViewModel(Doc, ColumnsModel);
            TopDowelsViewModel = new TopDowelsViewModel(Doc, ColumnsModel, Languages);
            BottomDowelsViewModel = new BottomDowelsViewModel(Doc, ColumnsModel, Languages);
            BarsDivisionViewModel = new BarsDivisionViewModel(Doc, ColumnsModel, Languages);
            StatusBarViewModel = new StatusBarViewModel(ColumnsModel.ProgressModel, Languages);
            StatusBarViewModel.SetStatusBarColumns();
            ActionViewModel = new ActionViewModel(Languages);
            ActionViewModel.SetStatusBarColumns();
            SelectedViewModel = SettingViewModel;
           
            SelectionMenuCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
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
                        SelectedViewModel = StirrupsViewModel;
                        break;
                    case 3:
                        SelectedViewModel = AdditionalStirrupsViewModel;
                        break;
                    case 4:
                        SelectedViewModel = BarsViewModel;
                        break;
                    case 5:
                        SelectedViewModel = TopDowelsViewModel;
                        break;
                    case 6:
                        SelectedViewModel = BottomDowelsViewModel;
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
            LoadWindowCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {   
                p.MainCanvas.Height = ColumnsModel.DrawModel.Height;
                p.MainCanvas.Width = ColumnsModel.DrawModel.Width;
                p.scrollViewer.ScrollToBottom();
                p.scrollViewer.ScrollToLeftEnd();
                DrawMenu(p);
                DrawInfo(p);
            });
            #endregion
            #region Action Command
            OKCommand = new RelayCommand<ColumnsWindow>((p) => { return ColumnsModel.ConditionButtonOK(); }, (p) =>
            {
                OKAction(p);
            });
            CancelCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
            CloseWindowCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                p.DialogResult = false;
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
        
        private void DrawMenu(ColumnsWindow p)
        {
            DrawIcon.DrawGeometry(p.GeometryCanvas);
            DrawIcon.DrawBarsStirrups(p.BarsCanvas,true);
            DrawIcon.DrawBarsStirrups(p.StirrupsCanvas,false);
            DrawIcon.DrawBarsAdditionalStirrups(p.AdditionalStirrupsCanvas, false);
            DrawIcon.DrawTopDowels(p.TopDowelsCanvas);
            DrawIcon.DrawBottomDowels(p.BottomDowelsCanvas);
            DrawIcon.DrawBarsDivision(p.BarsDivisionCanvas);
            DrawIcon.DrawSetting(p.SettingCanvas);
        }
        private void DrawInfo(ColumnsWindow p)
        {
            p.MainCanvas.Children.Clear();
            DrawMainCanvas.DrawInfoColumns(p.MainCanvas, ColumnsModel,ColumnsModel.SelectedIndexModel.SelectedColumn);
        }
        #endregion
        #region Action
        private void OKAction(ColumnsWindow p)
        {

            if (ColumnsModel.IsRebar)
            {
                OKActionRebar(p);
            }
            else
            {
                OKActionDetailItem(p);
            }

        }
        private void OKActionRebar(ColumnsWindow p)
        {
            TransactionGroup.Start("Action");
            if (TransactionGroup.HasStarted())
            {
                CreateViewDimension.Create(p, ColumnsModel, UiDoc, Doc, Columns, Unit);
                CreateRebar.Create(p, ColumnsModel, Doc,  Unit);
                if (ColumnsModel.ConditionCreateDetailShop(Doc))
                {
                    CreateDetailShop.Create(p, ColumnsModel, Doc, Unit, Columns);
                }
                TransactionGroup.Commit();
                p.DialogResult = true;
            }
        }
        private void OKActionDetailItem(ColumnsWindow p)
        {
            TransactionGroup.Start("Action");
           
            if (TransactionGroup.HasStarted())
            {
                CreateViewDimension.Create(p, ColumnsModel, UiDoc, Doc, Columns, Unit);
               
                CreateRebarDetailtem.Create(p, ColumnsModel, Doc, Unit);
                TransactionGroup.Commit();
                p.DialogResult = true;
            }

        }
        #endregion
    }
}
