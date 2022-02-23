#region Namespaces
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using R11_FoundationPile.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfCustomControls;
using WpfCustomControls.ViewModel;
using WpfCustomControls.LanguageModel;
using WpfCustomControls.Model;
using DSP;
using Application = Autodesk.Revit.ApplicationServices.Application;
#endregion

namespace R11_FoundationPile
{
    public class FoundationPileViewModel : BaseViewModel
    {
        #region property
        public Application App;
        public UIDocument UiDoc;
        public Document Doc;
        private UnitProject _Unit;
        public UnitProject Unit { get { return _Unit; } set { _Unit = value; OnPropertyChanged(); } }
        private List<Element> _Columns;
        public List<Element> Columns { get => _Columns; set { _Columns = value; OnPropertyChanged(); } }
        private TransactionGroup _TransactionGroup;
        public TransactionGroup TransactionGroup { get => _TransactionGroup; set { _TransactionGroup = value; OnPropertyChanged(); } }
        private FoundationPileModel _FoundationPileModel;
        public FoundationPileModel FoundationPileModel { get => _FoundationPileModel; set { _FoundationPileModel = value; OnPropertyChanged(); } }
        #endregion

        #region Icommand
        public ICommand LoadWindowCommand { get; set; }
        public ICommand SelectionMenuCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ICommand CreateFoundationCommand { get; set; }
        public ICommand CreatePileDetailCommand { get; set; }
        public ICommand CreateReinforcementCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        #endregion
        #region Menu ViewModel
        private BaseViewModel _selectedViewModel;
        public BaseViewModel SelectedViewModel { get { return _selectedViewModel; } set { _selectedViewModel = value; OnPropertyChanged(nameof(SelectedViewModel)); } }
        private SettingViewModel _SettingViewModel;
        public SettingViewModel SettingViewModel { get => _SettingViewModel; set { _SettingViewModel = value; OnPropertyChanged(); } }
        private GeometryViewModel _GeometryViewModel;
        public GeometryViewModel GeometryViewModel { get => _GeometryViewModel; set { _GeometryViewModel = value; OnPropertyChanged(); } }
        private ReinforcementViewModel _ReinforcementViewModel;
        public ReinforcementViewModel ReinforcementViewModel { get => _ReinforcementViewModel; set { _ReinforcementViewModel = value; OnPropertyChanged(); } }
        private PileDetailViewModel _PileDetailViewModel;
        public PileDetailViewModel PileDetailViewModel { get => _PileDetailViewModel; set { _PileDetailViewModel = value; OnPropertyChanged(); } }

        private TaskBarViewModel _TaskBarViewModel;
        public TaskBarViewModel TaskBarViewModel { get { return _TaskBarViewModel; } set { _TaskBarViewModel = value; OnPropertyChanged(); } }
        private StatusBarViewModel _StatusBarViewModel;
        public StatusBarViewModel StatusBarViewModel { get { return _StatusBarViewModel; } set { _StatusBarViewModel = value; OnPropertyChanged(); } }
        private ActionViewModel _ActionViewModel;
        public ActionViewModel ActionViewModel { get { return _ActionViewModel; } set { _ActionViewModel = value; OnPropertyChanged(); } }

        private Languages _Languages;
        public Languages Languages { get { return _Languages; } set { _Languages = value; OnPropertyChanged(); } }
        #endregion
        public FoundationPileViewModel(Application app,UIDocument uiDoc, Document doc, List<Element> columns)
        {
            #region
            App = app;
            UiDoc = uiDoc;
            Doc = doc;
            Columns = columns;
            Unit = GetUnitProject();
            Languages = new Languages("EN");
            TransactionGroup = new TransactionGroup(Doc);
            TaskBarViewModel = new TaskBarViewModel(Languages);
            FoundationPileModel = new FoundationPileModel(columns, Doc, Unit);
            SettingViewModel = new SettingViewModel(Doc, FoundationPileModel, Languages);
            GeometryViewModel = new GeometryViewModel(Doc, FoundationPileModel, Unit, Languages);

           
            StatusBarViewModel = new StatusBarViewModel(FoundationPileModel.ProgressModel, Languages);
            StatusBarViewModel.SetStatusBarFoundationPile();
            ActionViewModel = new ActionViewModel(Languages);
            ActionViewModel.SetStatusBarFoundationPile();

            SelectedViewModel = SettingViewModel;
            #endregion
            #region Load
            LoadWindowCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {
                DrawMenu(p);
                p.ReinforcementListViewItem.Visibility = System.Windows.Visibility.Collapsed;
                p.PileDetailListViewItem.Visibility = System.Windows.Visibility.Collapsed;
            });
            SelectionMenuCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
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
                        SelectedViewModel = PileDetailViewModel;
                        break;
                    case 3:
                        SelectedViewModel = ReinforcementViewModel;
                        break;
                    default:
                        break;
                }
            });
            CancelCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {
                p.Close();

            });
            CloseWindowCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {
                p.DialogResult = true;
                if (!FoundationPileModel.IsCreateGrounpFoundation|| !FoundationPileModel.IsCreatePileDetail|| !FoundationPileModel.IsCreateReinforcement)
                {
                    if (TransactionGroup.HasStarted())
                    {
                        TransactionGroup.RollBack();
                        System.Windows.MessageBox.Show("Progress is Cancel!", "Stop Progress",
                            MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                   
                }
                DeleteWallTemp();




            });
            CreateFoundationCommand = new RelayCommand<FoundationPileWindow>((p) => { return FoundationPileModel.ConditionCreateFoundation() && !FoundationPileModel.IsCreateGrounpFoundation; }, (p) =>
              {
                  CreateFoundation(p);
                  ShowPileDetailAndReinforcement(p);
                  StatusBarViewModel.HasCreateReinforcement(true);
                  StatusBarViewModel.HasCreatePileDetail(true);
              });
            CreatePileDetailCommand = new RelayCommand<FoundationPileWindow>((p) => { return FoundationPileModel.IsCreateGrounpFoundation && FoundationPileModel.IsApplyRule && !FoundationPileModel.IsCreatePileDetail; }, (p) =>
               {
                   CreatePileDetailPlan(p);
                   
               });
            CreateReinforcementCommand = new RelayCommand<FoundationPileWindow>((p) => { return FoundationPileModel.IsCreateGrounpFoundation&&FoundationPileModel.FoundationBarModels.Count!=0&& !FoundationPileModel. IsCreateReinforcement; }, (p) =>
            {
                CreateReinforcement(p);
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
        #region Draw Menu
        private void DrawMenu(FoundationPileWindow p)
        {
            DrawIcon.DrawSetting(p.SettingCanvas);
            DrawIcon.DrawGeometry(p.GeometryCanvas);
            DrawIcon.DrawBarsStirrups(p.ReinforcementCanvas, true);
            DrawIcon.DrawPileDetail(p.PileDetailCanvas);
        }
        #endregion
        #region CreateGroupFoundation
        private void ShowPileDetailAndReinforcement(FoundationPileWindow p)
        {
            if (FoundationPileModel.IsCreateGrounpFoundation)
            {
                p.PileDetailListViewItem.Visibility = System.Windows.Visibility.Visible; PileDetailViewModel = new PileDetailViewModel(Doc, FoundationPileModel, Unit, Languages);
                p.ReinforcementListViewItem.Visibility = System.Windows.Visibility.Visible; ReinforcementViewModel = new ReinforcementViewModel(Doc, FoundationPileModel, Unit, Languages);
            }

        }
        private void ShowReinforcement(FoundationPileWindow p)
        {
            if (FoundationPileModel.IsCreatePileDetail)
          
            { }
        }
        #endregion
        #region Action

        private void CreateFoundation(FoundationPileWindow p)
        {
            TransactionGroup.Start("Action");
            if (TransactionGroup.HasStarted())
            {
                CreateFoundationAndPiles.Create(p, FoundationPileModel, Doc, Unit);
                AddSharedParams.ShareParameterPile(Doc, App,((FoundationPileModel.SettingModel.SelectedCategoyryPile.Equals("Structural Columns"))?(BuiltInCategory.OST_StructuralColumns):(BuiltInCategory.OST_StructuralFoundation)));
                TransactionGroup.Commit();

                //p.DialogResult = true;
            }
        }
        private void CreatePileDetailPlan(FoundationPileWindow p)
        {
            TransactionGroup.Start("Action");
            if (TransactionGroup.HasStarted())
            {
                CreatePileDetail.Create(p, FoundationPileModel, Doc, Unit);
                TransactionGroup.Commit();
                //p.DialogResult = true;
            }
        }
        private void CreateReinforcement(FoundationPileWindow p)
        {
            TransactionGroup.Start("Action");
            if (TransactionGroup.HasStarted())
            {
                CreateRebar.Create(p, FoundationPileModel, Doc, Unit);
                TransactionGroup.Commit();
                //p.DialogResult = true;
            }
        }
        private void DeleteWallTemp()
        {
            TransactionGroup.Start("Action");
            if (TransactionGroup.HasStarted())
            {
                DeleteWall.Delete(FoundationPileModel, Doc);
                TransactionGroup.Commit();
                //p.DialogResult = true;
            }
        }
        #endregion
    }
}
