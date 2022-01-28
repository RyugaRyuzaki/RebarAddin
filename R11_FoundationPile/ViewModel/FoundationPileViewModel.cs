#region Namespaces
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using R11_FoundationPile.View;
using R11_FoundationPile.ViewModel;
#endregion

namespace R11_FoundationPile
{
    public class FoundationPileViewModel : BaseViewModel
    {
        #region property
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
        public ICommand PreviewTextInputCommand { get; set; }
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
        #endregion
        public FoundationPileViewModel(UIDocument uiDoc, Document doc, List<Element> columns)
        {
            #region
            UiDoc = uiDoc;
            Doc = doc;
            Columns = columns;
            Unit = GetUnitProject();
            TransactionGroup = new TransactionGroup(Doc);
            FoundationPileModel = new FoundationPileModel(columns,Doc, Unit);
            SettingViewModel = new SettingViewModel(Doc,FoundationPileModel);
            GeometryViewModel = new GeometryViewModel(Doc, FoundationPileModel, Unit);
            
            PileDetailViewModel = new PileDetailViewModel(Doc, FoundationPileModel, Unit);
            ReinforcementViewModel = new ReinforcementViewModel(Doc, FoundationPileModel,Unit);
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
                if (!FoundationPileModel.IsCreateGrounpFoundation)
                {
                    if (TransactionGroup.HasStarted())
                    {
                        TransactionGroup.RollBack();
                        System.Windows.MessageBox.Show("Progress is Cancel!", "Stop Progress",
                            MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                    p.Close();
                }
                else
                {
                    p.DialogResult = true;
                }
                

            });
            CreateFoundationCommand = new RelayCommand<FoundationPileWindow>((p) => { return FoundationPileModel.ConditionCreateFoundation()&&!FoundationPileModel.IsCreateGrounpFoundation; }, (p) =>
            {
                CreateFoundation(p);
                ShowPileDetail(p);

            });
            CreatePileDetailCommand = new RelayCommand<FoundationPileWindow>((p) => { return FoundationPileModel.IsCreateGrounpFoundation&& FoundationPileModel.IsApplyRule&&!FoundationPileModel.IsCreatePileDetail; }, (p) =>
            {
                CreateFPileDetail(p);
                ShowReinforcement(p);
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
        private void ShowPileDetail(FoundationPileWindow p)
        {
           if(FoundationPileModel.IsCreateGrounpFoundation) { p.PileDetailListViewItem.Visibility = System.Windows.Visibility.Visible; }
            
        }
        private void ShowReinforcement(FoundationPileWindow p)
        {
           if(FoundationPileModel.IsCreatePileDetail) { p.ReinforcementListViewItem.Visibility = System.Windows.Visibility.Visible; }
        }
        #endregion
        #region Action
        
        private void CreateFoundation(FoundationPileWindow p)
        {
            TransactionGroup.Start("Action");
            if (TransactionGroup.HasStarted())
            {
                CreateFoundationAndPiles.Create(p,FoundationPileModel, Doc, Unit);
                TransactionGroup.Commit();
               
                //p.DialogResult = true;
            }
        }
        private void CreateFPileDetail(FoundationPileWindow p)
        {
            TransactionGroup.Start("Action");
            if (TransactionGroup.HasStarted())
            {
                CreatePileDetail.Create(p, FoundationPileModel, Doc, Unit);
                TransactionGroup.Commit();
                //p.DialogResult = true;
            }
        }
        #endregion
    }
}
