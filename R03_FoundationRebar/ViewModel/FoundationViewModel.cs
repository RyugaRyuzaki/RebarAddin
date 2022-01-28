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
using R03_FoundationRebar.ViewModel;
#endregion

namespace R03_FoundationRebar
{
    public class FoundationViewModel : BaseViewModel
    {
        #region Property
        public UIDocument UiDoc;
        public Document Doc;
        private UnitProject _Unit;
        public UnitProject Unit { get => _Unit; set { _Unit = value; OnPropertyChanged(); } }
        private FoundationModel _FoundationModel;
        public FoundationModel FoundationModel { get => _FoundationModel; set { _FoundationModel = value; OnPropertyChanged(); } }
        #endregion
        #region Menu
        private BaseViewModel _selectedViewModel;
        public BaseViewModel SelectedViewModel { get { return _selectedViewModel; } set { _selectedViewModel = value; OnPropertyChanged(nameof(SelectedViewModel)); } }
        private SettingViewModel _SettingViewModel;
        public SettingViewModel SettingViewModel { get { return _SettingViewModel; } set { _SettingViewModel = value; OnPropertyChanged(); } }
        private GeometryViewModel _GeometryViewModel;
        public GeometryViewModel GeometryViewModel { get { return _GeometryViewModel; } set { _GeometryViewModel = value; OnPropertyChanged(); } }
        #endregion
        #region Command
        public ICommand LoadWindowCommand { get; set; }
        public ICommand SelectionMenuCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand OKCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        #endregion
        public FoundationViewModel(UIDocument uiDoc, Document doc)
        {
            #region add property
            UiDoc = uiDoc;
            Doc = doc;
            SelectedIndexViewModel();
            #endregion
            #region Command
            LoadWindowCommand = new RelayCommand<FoundationWindow>((p) => { return true; }, (p) =>
            {
                DrawMenu(p);
            });
            SelectionMenuCommand = new RelayCommand<FoundationWindow>((p) => { return true; }, (p) =>
            {
                SelectionMenu(p);
            });
            #endregion
        }
        #region Menu method
        private void SelectedIndexViewModel()
        {
            SettingViewModel = new SettingViewModel();
            GeometryViewModel = new GeometryViewModel();
            SelectedViewModel = SettingViewModel;
        }
        private void SelectionMenu(FoundationWindow p)
        {
            switch (p.Menu.SelectedIndex)
            {
                case 0:
                    SelectedViewModel = SettingViewModel;
                    break;
                case 1:
                    SelectedViewModel = GeometryViewModel;
                    break;
                default:
                    break;
            }
        }
        #endregion
        #region Draw
        private void DrawMenu(FoundationWindow p)
        {
            DrawIcon.DrawSetting(p.SettingCanvas);
            DrawIcon.DrawGeometry(p.GeometryCanvas);
        }
        #endregion
    }
}
