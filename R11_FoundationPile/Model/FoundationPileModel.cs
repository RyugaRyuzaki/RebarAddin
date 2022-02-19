using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCustomControls;
using WpfCustomControls.Model;
using DSP;
namespace R11_FoundationPile
{
    public class FoundationPileModel  :BaseViewModel
    {
        #region Property
        private ObservableCollection<ColumnModel> _ColumnModels;
        public ObservableCollection<ColumnModel> ColumnModels { get { if (_ColumnModels == null) { _ColumnModels = new ObservableCollection<ColumnModel>(); } return _ColumnModels; } set { _ColumnModels = value; OnPropertyChanged(); } }
        private SettingModel _SettingModel;
        public SettingModel SettingModel { get => _SettingModel; set { _SettingModel = value; OnPropertyChanged(); } }
        private SelectedIndexModel _SelectedIndexModel;
        public SelectedIndexModel SelectedIndexModel { get => _SelectedIndexModel; set { _SelectedIndexModel = value; OnPropertyChanged(); } }
        private ObservableCollection<GroupFoundationModel> _GroupFoundationModels;
        public ObservableCollection<GroupFoundationModel> GroupFoundationModels { get { if (_GroupFoundationModels == null) { _GroupFoundationModels = new ObservableCollection<GroupFoundationModel>(); } return _GroupFoundationModels; } set { _GroupFoundationModels = value; OnPropertyChanged(); } }
        #endregion
        #region Draw
        private DrawModel _DrawModel;
        public DrawModel DrawModel { get => _DrawModel; set { _DrawModel = value; OnPropertyChanged(); } }
        private DrawModel _DrawModelSection;
        public DrawModel DrawModelSection { get => _DrawModelSection; set { _DrawModelSection = value; OnPropertyChanged(); } }
        private DrawModel _DrawModelPileDetail;
        public DrawModel DrawModelPileDetail { get => _DrawModelPileDetail; set { _DrawModelPileDetail = value; OnPropertyChanged(); } }
        private DrawModel _DrawModelBar;
        public DrawModel DrawModelBar { get => _DrawModelBar; set { _DrawModelBar = value; OnPropertyChanged(); } }
        #endregion
        #region   CreateGroupFoundation
        private bool _IsCreateGrounpFoundation;
        public bool IsCreateGrounpFoundation { get => _IsCreateGrounpFoundation; set { _IsCreateGrounpFoundation = value; OnPropertyChanged(); } }
        private bool _IsCreatePileDetail;
        public bool IsCreatePileDetail { get => _IsCreatePileDetail; set { _IsCreatePileDetail = value; OnPropertyChanged(); } }
        private bool _IsCreateReinforcement;
        public bool IsCreateReinforcement { get => _IsCreateReinforcement; set { _IsCreateReinforcement = value; OnPropertyChanged(); } }
        #endregion
        #region   View
        private FoundationPileDetail _FoundationPileDetail;
        public FoundationPileDetail FoundationPileDetail { get => _FoundationPileDetail; set { _FoundationPileDetail = value; OnPropertyChanged(); } }
        private int _RuleFoundation;
        public int RuleFoundation { get => _RuleFoundation; set { _RuleFoundation = value; OnPropertyChanged(); } }
        private int _RulePile;
        public int RulePile { get => _RulePile; set { _RulePile = value; OnPropertyChanged(); } }
        private ObservableCollection<FoundationModel> _AllFoundationModels;
        public ObservableCollection<FoundationModel> AllFoundationModels { get { if (_AllFoundationModels == null) { _AllFoundationModels = new ObservableCollection<FoundationModel>(); } return _AllFoundationModels; } set { _AllFoundationModels = value; OnPropertyChanged(); } }
        private bool _IsApplyRule;
        public bool IsApplyRule { get => _IsApplyRule; set { _IsApplyRule = value; OnPropertyChanged(); } }
        #endregion
        #region Dimension
        private DimensionDetail _DimensionDetail;
        public DimensionDetail DimensionDetail { get => _DimensionDetail; set { _DimensionDetail = value; OnPropertyChanged(); } }
        #endregion
        #region Action
      
        private ProgressModel _ProgressModel;
        public ProgressModel ProgressModel { get => _ProgressModel; set { _ProgressModel = value; OnPropertyChanged(); } }
        #endregion
        #region Bar
        public List<RebarBarType> RebarBarTypes { get; set; }
        private List<RebarBarModel> _AllBars;
        public List<RebarBarModel> AllBars { get => _AllBars; set { _AllBars = value; OnPropertyChanged(); } }
        private ObservableCollection<FoundationBarModel> _FoundationBarModels;
        public ObservableCollection<FoundationBarModel> FoundationBarModels { get { if (_FoundationBarModels == null) { _FoundationBarModels = new ObservableCollection<FoundationBarModel>(); } return _FoundationBarModels; } set { _FoundationBarModels = value; OnPropertyChanged(); } }
        #endregion
        public FoundationPileModel(List<Element> columns,Document  document, UnitProject unit)
        {
            GetRebarBarType(document);
            GetSettingMOdel(document);
            GetColumnModels(columns, document, unit);
            GetGroupFoundationModels(document);
            GetDrawModel();
            FoundationPileDetail = new FoundationPileDetail(document);
            DimensionDetail = new DimensionDetail(document);
            RuleFoundation = 0;
            RulePile = 0;
            ProgressModel = new ProgressModel(0, 0);
        }
        #region   Method
        public FoundationModel FindFoundationModelByLoacationName(string locationName)
        {
            FoundationModel foundationModel = null;
            for (int i = 0; i < GroupFoundationModels.Count; i++)
            {

                for (int j = 0; j < GroupFoundationModels[i].FoundationModels.Count; j++)
                {
                    if (GroupFoundationModels[i].FoundationModels[j].IsRepresentative&& GroupFoundationModels[i].FoundationModels[j].LocationName.Equals(locationName))
                    {
                        foundationModel = GroupFoundationModels[i].FoundationModels[j];
                    }
                }

            }
            return foundationModel;
        }
        public void GetBarModels(Document document)
        {
            
            for (int i = 0; i < GroupFoundationModels.Count; i++)
            {

                for (int j = 0; j < GroupFoundationModels[i].FoundationModels.Count; j++)
                {
                    if (GroupFoundationModels[i].FoundationModels[j].IsRepresentative)
                    {
                        var a= (new FoundationBarModel(
                            GroupFoundationModels[i].FoundationModels[j].Type,
                            GroupFoundationModels[i].FoundationModels[j].Image,
                            GroupFoundationModels[i].FoundationModels[j].LocationName,
                            GroupFoundationModels[i].FoundationModels[j].SpanOrientation,
                            document,
                            SettingModel,
                            AllBars
                            ));
                        double coverSide = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, SettingModel.SelectedSideCover.CoverDistance, false));
                        FoundationModel foundationModel = FindFoundationModelByLoacationName(a.LocationName);
                        double p1 = foundationModel.GetP1(a);
                        double p2 = foundationModel.GetP2(a);
                        double p3 = foundationModel.GetP3(a);
                        double p4 = foundationModel.GetP4(a);
                        a.FixNumber(p1, p2, p3, p4, coverSide);
                        a.FixDistance(p1, p2, p3, p4, coverSide);
                        FoundationBarModels.Add(a);
                    }
                }
              
            }
        }
        private void GetRebarBarType(Document document)
        {
            RebarBarTypes =( new FilteredElementCollector(document).OfClass(typeof(RebarBarType)).Cast<RebarBarType>().ToList());
            RebarBarTypes.Sort((x, y) => x.BarDiameter.CompareTo(y.BarDiameter));
            AllBars = new List<RebarBarModel>();
            foreach (var item in RebarBarTypes)
            {
                AllBars.Add(new RebarBarModel(document, item.Name, RebarBarTypes));
            }
            AllBars.Sort((x, y) => x.Diameter.CompareTo(y.Diameter));
        }
        private void GetSettingMOdel(Document document)
        {
            SelectedIndexModel = new SelectedIndexModel();
            SettingModel = new SettingModel(document,SelectedIndexModel);
        }
        private void GetColumnModels(List<Element> columns, Document document, UnitProject unit)
        {
            ColumnModels = ProccessInfoClumns.GetColumnModels(columns, document, unit);
        }
        private void GetGroupFoundationModels(Document document)
        {
            GroupFoundationModels = ProccessInfoClumns.GetGroupFoundationModels(ColumnModels,SettingModel);
           
        }
        #endregion
        #region Condition
        public bool ConditionShowPileDetail()
        {
            for (int i = 0; i < GroupFoundationModels.Count; i++)
            {
                if (GroupFoundationModels[i].IsCreate == false)
                {
                    return false;
                }
            }
            return true;
        }
        public bool ConditionCreateFoundation()
        {
            for (int i = 0; i < GroupFoundationModels.Count; i++)
            {
                if (GroupFoundationModels[i].IsGenerate==false)
                {
                    return false;
                }
                List<FoundationModel> foundationModels = GroupFoundationModels[i].FoundationModels.Where(x => x.IsRepresentative).ToList();
                if (foundationModels.Count != 1) return false;
            }
            return true;
        }
        public bool ConditionModifySetting()
        {
            for (int i = 0; i < GroupFoundationModels.Count; i++)
            {
                if (GroupFoundationModels[i].IsGenerate == true)
                {
                    return false;
                }
            }
            return true;
        }
        public void ChangeL1L2Foundation()
        {
            for (int i = 0; i < GroupFoundationModels.Count; i++)
            {
                GroupFoundationModels[i].GetL1L2(SettingModel);
            }
        }
        private void GetDrawModel()
        {
            DrawModel = new DrawModel(1, 410, 410, 8, 4);
            DrawModelSection = new DrawModel(1, 175, 175, 8, 4);
            DrawModelPileDetail = new DrawModel(1, 0, 820, 8, 4);
            DrawModelBar = new DrawModel(1, 175, 100, 8, 4);
        }
        public bool GetCreateFoundation()
        {
            for (int i = 0; i < GroupFoundationModels.Count; i++)
            {
                if (GroupFoundationModels[i].IsCreate == false) return false;
            }
            return true;
        }

        #endregion
        #region BoundingBox

        public XYZ GetMaxBoundBox(UnitProject unit)
        {
            XYZ a = null;
            double xMax = GroupFoundationModels.Max(x => x.GetXMax());
            double yMax = GroupFoundationModels.Max(x => x.GetYMax());
            
            a = new XYZ(unit.Convert(xMax)+ unit.Convert(SettingModel.HeightFoundation), unit.Convert(yMax) + unit.Convert(SettingModel.HeightFoundation), unit.Convert(SettingModel.HeightFoundation ));
            return a;
        }
        public XYZ GetMinBoundBox(UnitProject unit)
        {
            XYZ a = null;
            double xMin = GroupFoundationModels.Min(x => x.GetXMin());
            double yMin = GroupFoundationModels.Min(x => x.GetYMin());
            a = new XYZ(unit.Convert(xMin) - unit.Convert(SettingModel.HeightFoundation), unit.Convert(yMin) - unit.Convert(SettingModel.HeightFoundation), unit.Convert(-SettingModel.HeightFoundation ));
            return a;
        }
        public void GetAllFoundationModel()
        {
            GetAllFoundationModelItem();
            switch (RuleFoundation)
            {
                case 0: AllFoundationModels = new ObservableCollection<FoundationModel>(AllFoundationModels.OrderBy(x=>x.Location.Y).ThenBy(x=>x.Location.X).ToList()); break;
                case 1: AllFoundationModels = new ObservableCollection<FoundationModel>(AllFoundationModels.OrderBy(x => x.Location.Y).ThenByDescending(x => x.Location.X).ToList()); break; 
                case 2: AllFoundationModels = new ObservableCollection<FoundationModel>(AllFoundationModels.OrderByDescending(x => x.Location.Y).ThenBy(x => x.Location.X).ToList()); break;
                case 3: AllFoundationModels = new ObservableCollection<FoundationModel>(AllFoundationModels.OrderByDescending(x => x.Location.Y).ThenByDescending(x => x.Location.X).ToList()); break;
                case 4: AllFoundationModels = new ObservableCollection<FoundationModel>(AllFoundationModels.OrderBy(x => x.Location.X).ThenBy(x => x.Location.Y).ToList()); break; 
                case 5: AllFoundationModels = new ObservableCollection<FoundationModel>(AllFoundationModels.OrderBy(x => x.Location.X).ThenByDescending(x => x.Location.Y).ToList()); break;
                case 6: AllFoundationModels = new ObservableCollection<FoundationModel>(AllFoundationModels.OrderByDescending(x => x.Location.X).ThenBy(x => x.Location.Y).ToList()); break; 
                case 7: AllFoundationModels = new ObservableCollection<FoundationModel>(AllFoundationModels.OrderByDescending(x => x.Location.X).ThenByDescending(x => x.Location.Y).ToList()); break; 
                default: AllFoundationModels = new ObservableCollection<FoundationModel>(AllFoundationModels.OrderBy(x => x.Location.Y).ThenBy(x => x.Location.X)); break; 
            }
            RenameAllPile();
        }
        private void RenameAllPile()
        {
            int number = 0;
            for (int i = 0; i < AllFoundationModels.Count; i++)
            {
                AllFoundationModels[i].ReNameAllPiles(RulePile, number);
                number += AllFoundationModels[i].PileModels.Count;
            }
        }
        private void GetAllFoundationModelItem()
        {
            //if (IsCreateGrounpFoundation)
            //{

            //}
            for (int i = 0; i < GroupFoundationModels.Count; i++)
            {
                for (int j = 0; j < GroupFoundationModels[i].FoundationModels.Count; j++)
                {
                    AllFoundationModels.Add(GroupFoundationModels[i].FoundationModels[j]);
                }
            }
        }
        
        #endregion

    }
}
