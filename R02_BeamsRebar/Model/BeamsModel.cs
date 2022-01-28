using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using R02_BeamsRebar.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Threading;

namespace R02_BeamsRebar
{
    public class BeamsModel : BaseViewModel
    {
        #region Other Property
        private string _FamilyType;
        public string FamilyType { get => _FamilyType; set { _FamilyType = value; OnPropertyChanged(); } }
        private string _AllTypes;
        public string AllTypes { get => _AllTypes; set { _AllTypes = value; OnPropertyChanged(); } }
        private List<int> _AllNumberBar;
        public List<int> AllNumberBar { get => _AllNumberBar; set { _AllNumberBar = value; OnPropertyChanged(); } }
        public List<RebarBarType> RebarBarTypes { get; set; }
        public List<RebarCoverType> RebarCoverTypes { get; set; }
        private RebarCoverType _RebarCoverType;
        public RebarCoverType RebarCoverType { get => _RebarCoverType; set { _RebarCoverType = value; OnPropertyChanged(); } }
        private double _Cover;
        public double Cover { get => _Cover; set { _Cover = value; OnPropertyChanged(); } }
        private List<RebarBarModel> _AllBars;
        public List<RebarBarModel> AllBars { get => _AllBars; set { _AllBars = value; OnPropertyChanged(); } }
        #endregion
        #region Main Property
        private SettingModel _SettingModel;
        public SettingModel SettingModel { get => _SettingModel; set { _SettingModel = value; OnPropertyChanged(); } }
        private List<InfoModel> _InfoModels;
        public List<InfoModel> InfoModels { get => _InfoModels; set { _InfoModels = value; OnPropertyChanged(); } }
        private List<StirrupModel> _StirrupModels;
        public List<StirrupModel> StirrupModels { get => _StirrupModels; set { _StirrupModels = value; OnPropertyChanged(); } }
        private List<DistributeStirrup> _DistributeStirrups;
        public List<DistributeStirrup> DistributeStirrups { get => _DistributeStirrups; set { _DistributeStirrups = value; OnPropertyChanged(); } }
        private List<NodeModel> _NodeModels;
        public List<NodeModel> NodeModels { get => _NodeModels; set { _NodeModels = value; OnPropertyChanged(); } }
        private List<SpecialNodeModel> _SpecialNodeModels;
        public List<SpecialNodeModel> SpecialNodeModels { get => _SpecialNodeModels; set { _SpecialNodeModels = value; OnPropertyChanged(); } }
        #endregion
        #region Section
        private ObservableCollection<SectionAreaModel> _SectionAreaModels;
        public ObservableCollection<SectionAreaModel> SectionAreaModels { get => _SectionAreaModels; set { _SectionAreaModels = value; OnPropertyChanged(); } }
        #endregion
        #region BarsMain
        private List<MainTopBarModel> _MainTopBarModel;
        public List<MainTopBarModel> MainTopBarModel { get => _MainTopBarModel; set { _MainTopBarModel = value; OnPropertyChanged(); } }
        private SingleMainTopBarModel _SingleMainTopBarModel;
        public SingleMainTopBarModel SingleMainTopBarModel { get => _SingleMainTopBarModel; set { _SingleMainTopBarModel = value; OnPropertyChanged(); } }
        private List<MainBottomBarModel> _MainBottomBarModel;
        public List<MainBottomBarModel> MainBottomBarModel { get => _MainBottomBarModel; set { _MainBottomBarModel = value; OnPropertyChanged(); } }
        private AddTopBarModel _AddTopBarModel;
        public AddTopBarModel AddTopBarModel { get => _AddTopBarModel; set { _AddTopBarModel = value; OnPropertyChanged(); } }
        private ObservableCollection<AddBottomBarModel> _AddBottomBarModel;
        public ObservableCollection<AddBottomBarModel> AddBottomBarModel { get => _AddBottomBarModel; set { _AddBottomBarModel = value; OnPropertyChanged(); } }
        private List<SelectedBottomModel> _SelectedBottomModels;
        public List<SelectedBottomModel> SelectedBottomModels { get => _SelectedBottomModels; set { _SelectedBottomModels = value; OnPropertyChanged(); } }
        private List<SideBarModel> _SideBarModel;
        public List<SideBarModel> SideBarModel { get => _SideBarModel; set { _SideBarModel = value; OnPropertyChanged(); } }
        private List<SpecialBarModel> _SpecialBarModel;
        public List<SpecialBarModel> SpecialBarModel { get => _SpecialBarModel; set { _SpecialBarModel = value; OnPropertyChanged(); } }
        private BarsDivisionModel _BarsDivisionModel;
        public BarsDivisionModel BarsDivisionModel { get { if (_BarsDivisionModel == null) { _BarsDivisionModel = new BarsDivisionModel(); } return _BarsDivisionModel; } set { _BarsDivisionModel = value; OnPropertyChanged(); } }
        private DivisionBar _DivisionBar;
        public DivisionBar DivisionBar { get { if (_DivisionBar == null) { _DivisionBar = new DivisionBar(0, 0); } return _DivisionBar; } set { _DivisionBar = value; OnPropertyChanged(); } }
        private DetailItemModel _DetailItemModel;
        public DetailItemModel DetailItemModel { get { if (_DetailItemModel == null) { _DetailItemModel = new DetailItemModel(); } return _DetailItemModel; } set { _DetailItemModel = value; OnPropertyChanged(); } }
        #endregion
        #region Draw Main
        private DrawModel _DrawModel;
        public DrawModel DrawModel { get => _DrawModel; set { _DrawModel = value; OnPropertyChanged(); } }
        private SelectedIndexModel _SelectedIndexModel;
        public SelectedIndexModel SelectedIndexModel { get => _SelectedIndexModel; set { _SelectedIndexModel = value; OnPropertyChanged(); } }

        #endregion
        #region Create Section
        private DetailBeamView _DetailBeamView;
        public DetailBeamView DetailBeamView { get { if (_DetailBeamView == null) { _DetailBeamView = new DetailBeamView(); } return _DetailBeamView; } set { _DetailBeamView = value; } }
        private List<SectionBeamView> _SectionBeamViews;
        public List<SectionBeamView> SectionBeamViews { get { if (_SectionBeamViews == null) { _SectionBeamViews = new List<SectionBeamView>(); } return _SectionBeamViews; } set { _SectionBeamViews = value; } }
        private DetailShopView _DetailShopView;
        public DetailShopView DetailShopView { get { if (_DetailShopView == null) { _DetailShopView = new DetailShopView(); } return _DetailShopView; } set { _DetailShopView = value; } }
        #endregion
        #region Create Dimension
        public DimensionView DimensionView { get; set; }
        public List<PlanarFace> PlanarFaces { get; set; }
        public ReferenceArray ReferenceAddTopBar { get; set; }
        public ReferenceArray ReferenceAddBottomBar { get; set; }
        public ReferenceArray ReferenceSpecialNode { get; set; }
        public ReferenceArray ReferenceStirrupBar { get; set; }
        #endregion
        #region Is Rebar
        private bool _IsRebar;
        public bool IsRebar { get => _IsRebar; set { _IsRebar = value; OnPropertyChanged(); } }
        private bool _IsDetailItem;
        public bool IsDetailItem { get => _IsDetailItem; set { _IsDetailItem = value; OnPropertyChanged(); } }
        private bool _IsDetailShop;
        public bool IsDetailIShop { get => _IsDetailItem; set { _IsDetailItem = value; OnPropertyChanged(); } }
        #endregion
        #region Action
        private string _SelectedAction;
        public string SelectedAction { get => _SelectedAction; set { _SelectedAction = value; OnPropertyChanged(); } }
        private double _Percent;
        public double Percent { get => _Percent; set { _Percent = value; OnPropertyChanged(); } }
        private double _Value;
        public double Value { get => _Value; set { _Value = value; OnPropertyChanged(); } }
        #endregion
        public BeamsModel(Document document, List<Element> beams)
        {
            GetFamilyType(beams);
            GetAllNumberBar();
            GetRebarBarType(document);
            GetImageModel();
            GetRebarCoverType(document, beams);
            GetMainProperty(document, beams);
            GetScale();
            GetAllMainRebar();
            GetAddRebar();
            GetSpecialRebar();
            GetSectionArea();
            GetDimensionView();
            GetPlanarFace();
            IsRebar = true;
            IsDetailItem = false;
            IsDetailIShop = false;
        }
        #region Orther constructor
        private void GetFamilyType(List<Element> beams)
        {
            FamilyType = ErrorBeams.GetFamilyTypeName(beams[0]);
            AllTypes = ErrorBeams.GetTypes(beams);
        }
        private void GetAllNumberBar()
        {
            AllNumberBar = new List<int>();
            for (int i = 1; i <= 7; i++)
            {
                AllNumberBar.Add(i);
            }
        }
        private void GetRebarBarType(Document document)
        {
            
            RebarBarTypes = new FilteredElementCollector(document).OfClass(typeof(RebarBarType)).Cast<RebarBarType>().ToList();
            AllBars = new List<RebarBarModel>();
            foreach (var item in RebarBarTypes)
            {
                AllBars.Add(new RebarBarModel(document, item.Name, RebarBarTypes));
            }
            AllBars.Sort((x, y) => x.Diameter.CompareTo(y.Diameter));
        }
        private void GetRebarCoverType(Document document, List<Element> beams)
        {
            
            RebarCoverTypes = new FilteredElementCollector(document).WhereElementIsElementType().OfClass(typeof(RebarCoverType)).Cast<RebarCoverType>().ToList();
            RebarCoverType = RebarCoverTypes[SelectedIndexModel.SelectedCover];
            Cover = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, RebarCoverType.CoverDistance, false));
        }
        private void GetImageModel()
        {
            DrawModel = new DrawModel(1, 50, 80, 8, 4);

            SelectedIndexModel = new SelectedIndexModel(0, 0, 0, 0, 0, 0, 0);
        }
        public void GetScale()
        {
            DrawModel.GetScale(InfoModels, NodeModels, 300, 3500);
        }
        #endregion
        #region Main constructor
        private void GetMainProperty(Document document, List<Element> beams)
        {
            string level = beams[0].get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM).AsValueString();
            Line line = LineProcess.GetLineFromElement(beams[0]);
            InfoModels = ProcessInfoBeamRebar.GetInfoModelFull(beams, document, line, level);
            
            NodeModels = ProcessInfoBeamRebar.GetNodeModelFull(InfoModels, beams, document, line, level);
            SpecialNodeModels = ProcessInfoBeamRebar.GetSpecialNodeModel(InfoModels, beams, document, line, level);
            StirrupModels = new List<StirrupModel>();
            DistributeStirrups = new List<DistributeStirrup>();
            for (int i = 0; i < InfoModels.Count; i++)
            {
                StirrupModels.Add(new StirrupModel(0, AllBars[0], AllBars[0], false, Cover, 0, 0, 0));
                DistributeStirrups.Add(new DistributeStirrup(1, InfoModels[i].b / 2, InfoModels[i].b, 0));
            }
            double b = InfoModels[0].b;
            double h = InfoModels[0].h;
            SettingModel = new SettingModel(b / 5, b / 5, b, b, b / 2, b / 2, h, h, h, h, "Beams Name", "MC", beams[0], document);
        }
        #endregion
        #region Bars Constructor
        private void GetAllMainRebar()
        {
            double dsmax = ProcessInfoBeamRebar.GetDiameterStirrupMax(InfoModels, DistributeStirrups, StirrupModels);
            SingleMainTopBarModel = ProcessInfoBeamRebar.GetSingleMainTopBarModels(InfoModels, NodeModels, AllBars[3], Cover);
            SingleMainTopBarModel.Refresh(InfoModels, NodeModels, Cover, dsmax);
            SingleMainTopBarModel.GetLength();
            MainTopBarModel = ProcessInfoBeamRebar.GetMainTopBarModels(InfoModels, NodeModels, AllBars[3], Cover);
            MainBottomBarModel = ProcessInfoBeamRebar.GetMainBottomBarModels(InfoModels, NodeModels, AllBars[3], Cover);
            for (int i = 0; i < MainTopBarModel.Count; i++)
            {
                MainTopBarModel[i].Refresh(dsmax, Cover);
                MainTopBarModel[i].GetLocationBarModels();
            }
            for (int i = 0; i < MainBottomBarModel.Count; i++)
            {
                MainBottomBarModel[i].Refresh(dsmax, Cover);
                MainBottomBarModel[i].GetLocationBarModels();
            }
        }
        private void GetAddRebar()
        {
            AddBottomBarModel = new ObservableCollection<AddBottomBarModel>();
            SelectedBottomModels = new List<SelectedBottomModel>();
            for (int i = 0; i < InfoModels.Count; i++)
            {
                AddBottomBarModel.Add(new AddBottomBarModel());
                SelectedBottomModels.Add(new SelectedBottomModel(InfoModels[i].NumberSpan, false, false, 0, 0, 0, InfoModels[i].Length));
            }
            if (InfoModels[0].ConsolLeft)
            {
                if (InfoModels[InfoModels.Count - 1].ConsolRight)
                {
                    AddTopBarModel = new AddTopBarModel(NodeModels.Count);
                }
                else
                {
                    AddTopBarModel = new AddTopBarModel(NodeModels.Count - 1);
                }
            }
            else
            {
                if (InfoModels[InfoModels.Count - 1].ConsolRight)
                {
                    AddTopBarModel = new AddTopBarModel(NodeModels.Count - 1);
                }
                else
                {
                    AddTopBarModel = new AddTopBarModel(NodeModels.Count - 2);
                }
            }
        }
        private void GetSpecialRebar()
        {
            SideBarModel = new List<SideBarModel>();
            for (int i = 0; i < InfoModels.Count; i++)
            {
                SideBarModel.Add(new SideBarModel(InfoModels[i].NumberSpan, AllBars[3], false, 0, 0));
            }
            SpecialBarModel = new List<SpecialBarModel>();
            if (SpecialNodeModels.Count != 0)
            {

                for (int i = 0; i < SpecialNodeModels.Count; i++)
                {
                    SpecialBarModel.Add(new SpecialBarModel(SpecialNodeModels[i].NumberNode, SpecialNodeModels[i].NumberSpan, false, AllBars[3], 2, InfoModels[0].b, InfoModels[0].h, InfoModels[0].b, false, AllBars[3], 6, 0));
                }
            }
        }

        #endregion
        #region Section
        private void GetSectionArea()
        {
            SectionAreaModels = new ObservableCollection<SectionAreaModel>();
            for (int i = 0; i < InfoModels.Count; i++)
            {
                SectionAreaModels.Add(new SectionAreaModel(InfoModels[i].NumberSpan));
            }
        }
        #endregion
        #region Detail property
        private void GetDimensionView()
        {
            DimensionView = new DimensionView(SettingModel);
        }
        private void GetPlanarFace()
        {
            PlanarFaces = new List<PlanarFace>();
            List<PlanarFace> planarFacesNode = new List<PlanarFace>();
            for (int i = 0; i < NodeModels.Count; i++)
            {
                planarFacesNode.Add(NodeModels[i].StartPlanar);
                planarFacesNode.Add(NodeModels[i].EndPlanar);
            }
            if (InfoModels[0].ConsolLeft)
            {
                if (InfoModels[InfoModels.Count - 1].ConsolRight)
                {
                    PlanarFaces.Add(InfoModels[0].StartPlanar);
                    PlanarFaces.AddRange(planarFacesNode);
                    PlanarFaces.Add(InfoModels[InfoModels.Count - 1].EndPlanar);
                }
                else
                {
                    PlanarFaces.Add(InfoModels[0].StartPlanar);
                    PlanarFaces.AddRange(planarFacesNode);
                }
            }
            else
            {
                if (InfoModels[InfoModels.Count - 1].ConsolRight)
                {
                    PlanarFaces.AddRange(planarFacesNode);
                    PlanarFaces.Add(InfoModels[InfoModels.Count - 1].EndPlanar);
                }
                else
                {
                    PlanarFaces.AddRange(planarFacesNode);
                }
            }
        }
        public double GetHmax()
        {
            double hmax = InfoModels[0].h;
            for (int i = 0; i < InfoModels.Count; i++)
            {
                if (hmax < InfoModels[i].h)
                {
                    hmax = InfoModels[i].h;
                }
            }
            return hmax;
        }
        #endregion
        #region Draw
        public void DrawInfo(BeamsWindow p)
        {
            p.canvas.Children.Clear();
            DrawMainCanvas.SpanBeams(p.canvas, InfoModels, DrawModel, 1000);
            DrawMainCanvas.NodeBeams(p.canvas, InfoModels, NodeModels, DrawModel, 1000);
            DrawMainCanvas.SpecialNode(p.canvas, InfoModels, SpecialNodeModels, DrawModel, 1000);
        }
        public void CheangedSectionArea()
        {
            for (int j = 0; j < SectionAreaModels.Count; j++)
            {
                SectionAreaModels[j].GetBar(InfoModels[0], InfoModels[j], MainTopBarModel, SingleMainTopBarModel, MainBottomBarModel, AddTopBarModel, AddBottomBarModel[j], SelectedIndexModel);
                SectionAreaModels[j].GetNameSection(InfoModels[j].NumberSpan, SettingModel);
            }
        }
        #endregion

        #region Other
        public bool ConditionAction(BeamsWindow p,Document document)
        {
            if (DistributeStirrups[SelectedIndexModel.Span].Type == 0)
            {
                if (DistributeStirrups[SelectedIndexModel.Span].S == 0) return false;
            }
            else
            {
                if ((DistributeStirrups[SelectedIndexModel.Span].S1 == 0) || (DistributeStirrups[SelectedIndexModel.Span].S2 == 0)) return false;
            }
            if (StirrupModels[SelectedIndexModel.Span].Anti)
            {
                if (StirrupModels[SelectedIndexModel.Span].Na == 0) return false;
                if (StirrupModels[SelectedIndexModel.Span].Sa == 0) return false;
            }
            if (SpecialNodeModels.Count != 0)
            {
                if (SpecialBarModel[SelectedIndexModel.SpecialBar].IsST)
                {
                    if (SpecialBarModel[SelectedIndexModel.SpecialBar].a == 0) return false;
                }
                if (SpecialBarModel[SelectedIndexModel.SpecialBar].IsSP)
                {
                    if (SpecialBarModel[SelectedIndexModel.SpecialBar].L1 == 0 && SpecialBarModel[SelectedIndexModel.SpecialBar].L2 == 0 && SpecialBarModel[SelectedIndexModel.SpecialBar].L3 == 0)
                    {
                        return false;
                    }
                }
            }
            if (SelectedIndexModel.StartTopChecked)
            {
                if (AddTopBarModel.Start.Model.Count == 0) return false;
            }
            if (SelectedIndexModel.EndTopChecked)
            {
                if (AddTopBarModel.End.Model.Count == 0) return false;
            }
            if (SelectedBottomModels[SelectedIndexModel.Span].StartBottomChecked)
            {
                if (AddBottomBarModel[SelectedIndexModel.Span].Start.Model.Count == 0) return false;
            }
            if (SelectedBottomModels[SelectedIndexModel.Span].EndBottomChecked)
            {
                if (AddBottomBarModel[SelectedIndexModel.Span].End.Model.Count == 0) return false;
            }
            
            if (SettingModel.SelectedDimensionType == null) return false;
            if (SettingModel.AllRebarTag == null) return false;
            if (IsDetailItem)
            {
                if (SettingModel.AllDetailItemTag.Count == 0)
                {
                    return false;
                }
                else
                {
                    if (SettingModel.SelectedDetailItemTag==null)
                    {
                        return false;
                    }
                    if (SettingModel.SelectedDetailDistanceTag == null)
                    {
                        return false;
                    }
                }
                List<Family> familys = new FilteredElementCollector(document)
                   .OfClass(typeof(Family))
                   .Cast<Family>()
                   .Where(x => x.FamilyCategory.Name.Equals("Detail Items"))
                   .Where(x => x.Name.Contains("DT"))
                   .ToList();
                if (familys.Count==0)
                {
                    return false;
                }
                foreach (var item in familys)
                {
                    FamilySymbol a = GetAllFamilySymbol(item).Where(x => x.Name.Contains("DT")).FirstOrDefault();
                    if (a == null) return false;
                }
            }
            else
            {
                if (SettingModel.RebarShapes.Count == 0) { return false; }
                else
                {
                    if (!SettingModel.SelectedShapeStirrup.Name.Contains("M_T1")) return false;
                    for (int i = 0; i < StirrupModels.Count; i++)
                    {
                        if (StirrupModels[i].Anti)
                        {
                            if (SettingModel.SelectedShapeAnti==null) { return false; }
                            if (!SettingModel.SelectedShapeAnti.Name.Contains("M_T")) return false;
                        }
                    }
                }
            }
            
            return true;
        }
        public bool ConditionUseDetailItem(Document document)
        {
            List<Family> familys = new FilteredElementCollector(document)
                   .OfClass(typeof(Family))
                   .Cast<Family>()
                   .Where(x => x.FamilyCategory.Name.Equals("Detail Items"))
                   .Where(x => x.Name.Contains("DT"))
                   .ToList();
            if (familys.Count == 0)
            {
                return false;
            }
            foreach (var item in familys)
            {
                FamilySymbol a = GetAllFamilySymbol(item).Where(x => x.Name.Contains("DT")).FirstOrDefault();
                if (a == null) return false;
            }
            return true;
        }
        public bool ConditionCreateDetailShop(Document document)
        {
            List<Family> familys = new FilteredElementCollector(document)
                  .OfClass(typeof(Family))
                  .Cast<Family>()
                  .Where(x => x.FamilyCategory.Name.Equals("Detail Items"))
                  .Where(x => x.Name.Contains("DS"))
                  .ToList();
            if (familys.Count == 0)
            {
                return false;
            }
            foreach (var item in familys)
            {
                FamilySymbol a = GetAllFamilySymbol(item).Where(x => x.Name.Contains("DS")).FirstOrDefault();
                if (a == null) return false;
            }
            return true;
        }
        private  List<FamilySymbol> GetAllFamilySymbol(Family family)
        {
            List<FamilySymbol> familySymbols = new List<FamilySymbol>();

            foreach (ElementId familySymbolId in family.GetFamilySymbolIds())
            {
                FamilySymbol familySymbol = family.Document.GetElement(familySymbolId) as FamilySymbol;
                familySymbols.Add(familySymbol);
            }

            return familySymbols;
        }
        #endregion
        #region SaveFile
       
        #endregion
    }
}