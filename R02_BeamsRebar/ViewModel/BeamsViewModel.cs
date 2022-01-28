#region Namespaces
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using R02_BeamsRebar.ViewModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;
#endregion

namespace R02_BeamsRebar
{
    public class BeamsViewModel : BaseViewModel
    {
        #region Property
        public UIDocument UiDoc;
        public Document Doc;
        public List<Element> Beams;
        private UnitProject _Unit;
        public UnitProject Unit { get => _Unit; set { _Unit = value; OnPropertyChanged(); } }
        private BeamsModel _BeamsModel;
        public BeamsModel BeamsModel { get => _BeamsModel; set { _BeamsModel = value; OnPropertyChanged(); } }
        private TransactionGroup _TransactionGroup;
        public TransactionGroup TransactionGroup { get => _TransactionGroup; set { _TransactionGroup = value; OnPropertyChanged(); } }
        private bool _UseDetailItem;
        public bool UseDetailItem { get => _UseDetailItem; set { _UseDetailItem = value; OnPropertyChanged(); } }
        #endregion
        #region Menu
        private BaseViewModel _selectedViewModel;
        public BaseViewModel SelectedViewModel { get { return _selectedViewModel; } set { _selectedViewModel = value; OnPropertyChanged(nameof(SelectedViewModel)); } }
        private SettingViewModel _SettingViewModel;
        public SettingViewModel SettingViewModel { get { return _SettingViewModel; } set { _SettingViewModel = value; OnPropertyChanged(); } }
        private GeometryViewModel _GeometryViewModel;
        public GeometryViewModel GeometryViewModel { get { return _GeometryViewModel; } set { _GeometryViewModel = value; OnPropertyChanged(); } }
        private StirrupsViewModel _StirrupsViewModel;
        public StirrupsViewModel StirrupsViewModel { get { return _StirrupsViewModel; } set { _StirrupsViewModel = value; OnPropertyChanged(); } }
        private BarsMainViewModel _BarsMainViewModel;
        public BarsMainViewModel BarsMainViewModel { get { return _BarsMainViewModel; } set { _BarsMainViewModel = value; OnPropertyChanged(); } }
        private AddTopBarViewModel _AddTopBarViewModel;
        public AddTopBarViewModel AddTopBarViewModel { get { return _AddTopBarViewModel; } set { _AddTopBarViewModel = value; OnPropertyChanged(); } }
        private AddBottomBarViewModel _AddBottomBarViewModel;
        public AddBottomBarViewModel AddBottomBarViewModel { get { return _AddBottomBarViewModel; } set { _AddBottomBarViewModel = value; OnPropertyChanged(); } }
        private SpecialBarViewModel _SpecialBarViewModel;
        public SpecialBarViewModel SpecialBarViewModel { get { return _SpecialBarViewModel; } set { _SpecialBarViewModel = value; OnPropertyChanged(); } }
        private BarsDivisionViewModel _BarsDivisionViewModel;
        public BarsDivisionViewModel BarsDivisionViewModel { get { return _BarsDivisionViewModel; } set { _BarsDivisionViewModel = value; OnPropertyChanged(); } }
        private SectionAreaViewModel _SectionAreaViewModel;
        public SectionAreaViewModel SectionAreaViewModel { get { return _SectionAreaViewModel; } set { _SectionAreaViewModel = value; OnPropertyChanged(); } }
        #endregion
        #region Command
        public ICommand LoadWindowCommand { get; set; }
        public ICommand SelectionMenuCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand OKCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        //public ICommand DetailItemCommand { get; set; }

        #endregion
        public BeamsViewModel(UIDocument uiDoc, Document doc, List<Element> beams)
        {
            #region Get Property
            UiDoc = uiDoc;
            Doc = doc;
            Beams = beams;
            Unit = GetUnitProject();
            BeamsModel = new BeamsModel(Doc, Beams);
            UseDetailItem = BeamsModel.ConditionUseDetailItem(Doc);
            SelectedIndexViewModel();
            TransactionGroup = new TransactionGroup(Doc);
            #endregion
            #region Command
            LoadWindowCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                p.canvas.Width = BeamsModel.DrawModel.Width;
                p.canvas.Height = BeamsModel.DrawModel.Height;
                BeamsModel.DrawInfo(p);
                DrawMenu(p);
               
            });
            SelectionMenuCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                SelectionMenu(p);
            });
            //DetailItemCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            //{
            //    if (!BeamsModel.ConditionCreateDetailShop(Doc))
            //    {
            //        if (System.Windows.Forms.MessageBox.Show(LoadDetailItem, LoadFamily, System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
            //        {
            //            LoadDetailItemFamily();
            //            p.DialogResult = true;
            //        }
            //    }
            //});
            CancelCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                p.DialogResult = false;
                if (TransactionGroup.HasStarted())
                {
                    TransactionGroup.RollBack();
                    System.Windows.MessageBox.Show("Progress is Cancel!", "Stop Progress",
                        MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            });
            OKCommand = new RelayCommand<BeamsWindow>((p) => { return BeamsModel.ConditionAction(p, Doc); }, (p) =>
            {
                OKAction(p);
            });
            #endregion

        }


        #region Menu Method
        private void SelectedIndexViewModel()
        {
            SettingViewModel = new SettingViewModel(BeamsModel);
            GeometryViewModel = new GeometryViewModel(Doc, BeamsModel);
            StirrupsViewModel = new StirrupsViewModel(Doc, BeamsModel);
            BarsMainViewModel = new BarsMainViewModel(Doc, BeamsModel);
            AddTopBarViewModel = new AddTopBarViewModel(Doc, BeamsModel);
            AddBottomBarViewModel = new AddBottomBarViewModel(Doc, BeamsModel);
            SpecialBarViewModel = new SpecialBarViewModel(Doc, BeamsModel);
            BarsDivisionViewModel = new BarsDivisionViewModel(Doc, BeamsModel);
            SectionAreaViewModel = new SectionAreaViewModel(Doc, BeamsModel);
            SelectedViewModel = SettingViewModel;
        }
        private void SelectionMenu(BeamsWindow p)
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
                    SelectedViewModel = BarsMainViewModel;
                    break;
                case 4:
                    SelectedViewModel = AddTopBarViewModel;
                    break;
                case 5:
                    SelectedViewModel = AddBottomBarViewModel;
                    break;
                case 6:
                    SelectedViewModel = SpecialBarViewModel;
                    break;
                case 8:
                    SelectedViewModel = BarsDivisionViewModel;
                    break;
                case 7:
                    SelectedViewModel = SectionAreaViewModel;
                    break;
                default:
                    break;
            }
        }
        #endregion
        #region Draw
        private void DrawMenu(BeamsWindow p)
        {
            DrawIcon.DrawSetting(p.SettingCanvas);
            DrawIcon.DrawGeometry(p.GeometryCanvas);
            DrawIcon.DrawBarsStirrups(p.StirrupsCanvas, false);
            DrawIcon.DrawBarsStirrups(p.BarsMainCanvas, true);
            DrawIcon.DrawAddBar(p.AddTopBarCanvas, true);
            DrawIcon.DrawAddBar(p.AddBottomBarCanvas, false);
            DrawIcon.DrawSpecialBar(p.SpecialBarCanvas);
            DrawIcon.DrawBarsDivision(p.BarsDivisionCanvas);
            DrawIcon.DrawReinforcement(p.SectionAreaCanvas);
        }
        #endregion
        #region get property method
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
        #region Action
        private List<string> Action = new List<string>()
        {
            "Create View-Dimension",
            "Create Rebar",
            "Create Detail Layout",
            "Create Sheet",
            "Create Rebar DetailItem"
        };
        
        public void OKAction(BeamsWindow p)
        {
            
            if (BeamsModel.IsRebar)
            {
                OKActionRebar(p);
            }
            else
            {
                OKActionDetailItem(p);
            }

        }
        private void OKActionRebar(BeamsWindow p)
        {
            TransactionGroup.Start("Action");
            if (TransactionGroup.HasStarted())
            {
                CreateViewDimension.Create(p, BeamsModel, UiDoc, Doc, Beams, Unit);
                CreateRebar.Create(Action[1], p, BeamsModel, Doc, Unit);
                if (BeamsModel.ConditionCreateDetailShop(Doc)&&BeamsModel.IsDetailIShop)
                {
                    CreateDetailShop.Create(p, BeamsModel, Doc, Unit, Beams);
                }
               
                TransactionGroup.Commit();
                UiDoc.ActiveView = BeamsModel.DetailBeamView.DetailView;
                p.DialogResult = true;
            }
        }
        private void OKActionDetailItem(BeamsWindow p)
        {
            TransactionGroup.Start("Action");
            if (TransactionGroup.HasStarted())
            {
                CreateViewDimension.Create(p, BeamsModel, UiDoc, Doc, Beams, Unit);
                
                if (BeamsModel.ConditionUseDetailItem(Doc))
                {
                    CreateRebarDetailtem.Create(Action[4], p, BeamsModel, Doc, Unit);
                }
                
                if (BeamsModel.ConditionCreateDetailShop(Doc) && BeamsModel.IsDetailIShop)
                {
                    CreateDetailShop.Create(p, BeamsModel, Doc, Unit, Beams);
                }
                TransactionGroup.Commit();
                UiDoc.ActiveView = BeamsModel.DetailBeamView.DetailView;
                p.DialogResult = true;
            }

        }

        #endregion
        #region Load Family DetailItem
        //private static DirectoryInfo directory = new DirectoryInfo(@"C:\Users\Admin\AppData\Roaming\Autodesk\Revit\Addins\2021\DetailItem\");
        //private static FileInfo[] FileInfo = directory.GetFiles("*.rfa");
        //private static string LoadDetailItem = "Load Them to project and must created them handle" + "\n" + "Do you want to load them ?";
        //private static string LoadFamily = "There are no Family DetailItem in project";
        //private void LoadDetailItemFamily()
        //{
        //    using (Transaction transaction = new Transaction(Doc))
        //    {
        //        transaction.Start("aa");
        //        Family family = null;
        //        foreach (var item in FileInfo)
        //        {
        //            if (item.Name.Contains("DT")||item.Name.Contains("DetailItemTag"))
        //            {
        //                Doc.LoadFamily(directory + item.Name, out family);
        //            }
        //        }
        //        transaction.Commit();
        //    }
        //}
        #endregion

    }
}
