

using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static R01_ColumnsRebar.ErrorColumns;
using WpfCustomControls;
using WpfCustomControls.Model;
using DSP;
namespace R01_ColumnsRebar
{
    public class ColumnsModel : BaseViewModel
    {
        #region property
        private string _FamilyType;
        public string FamilyType { get => _FamilyType; set { _FamilyType = value; OnPropertyChanged(); } }
        private string _AllType;
        public string AllType { get => _AllType; set { _AllType = value; OnPropertyChanged(); } }
        private string _Section;
        public string Section { get => _Section; set { _Section = value; OnPropertyChanged(); } }
        private SectionStyle _SectionStyle;
        public SectionStyle SectionStyle { get => _SectionStyle; set { _SectionStyle = value; OnPropertyChanged(); } }
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
        #region Info
        private SettingModel _SettingModel;
        public SettingModel SettingModel { get => _SettingModel; set { _SettingModel = value; OnPropertyChanged(); } }
        private ObservableCollection<InfoModel> _InfoModels;
        public ObservableCollection<InfoModel> InfoModels { get { if (_InfoModels == null) _InfoModels = new ObservableCollection<InfoModel>(); return _InfoModels; } set { _InfoModels = value; OnPropertyChanged(); } }
        #endregion
        #region Draw Main
        private DrawModel _DrawModel;
        public DrawModel DrawModel { get => _DrawModel; set { _DrawModel = value; OnPropertyChanged(); } }
        private DrawModel _DrawModelSection;
        public DrawModel DrawModelSection { get => _DrawModelSection; set { _DrawModelSection = value; OnPropertyChanged(); } }
        private DrawModel _DrawModelDowels;
        public DrawModel DrawModelDowels { get => _DrawModelDowels; set { _DrawModelDowels = value; OnPropertyChanged(); } }
        private SelectedIndexModel _SelectedIndexModel;
        public SelectedIndexModel SelectedIndexModel { get => _SelectedIndexModel; set { _SelectedIndexModel = value; OnPropertyChanged(); } }
        #endregion
        #region Stirrup
        private ObservableCollection<StirrupModel> _StirrupModels;
        public ObservableCollection<StirrupModel> StirrupModels { get { if (_StirrupModels == null) _StirrupModels = new ObservableCollection<StirrupModel>(); return _StirrupModels; } set { _StirrupModels = value; OnPropertyChanged(); } }
       
        #endregion
        #region Bars
        private ObservableCollection<BarMainModel> _BarMainModels;
        public ObservableCollection<BarMainModel> BarMainModels { get { if (_BarMainModels == null) _BarMainModels = new ObservableCollection<BarMainModel>(); return _BarMainModels; } set { _BarMainModels = value; OnPropertyChanged(); } }
     
       
        #endregion
        #region Action
        private bool _IsRebar;
        public bool IsRebar { get => _IsRebar; set { _IsRebar = value; OnPropertyChanged(); } }
        private bool _IsDetailItem;
        public bool IsDetailItem { get => _IsDetailItem; set { _IsDetailItem = value; OnPropertyChanged(); } }
        #endregion
        #region PlanarFace
        private ObservableCollection<PlanarFace> _PlanarFaces;
        public ObservableCollection<PlanarFace> PlanarFaces { get { if (_PlanarFaces == null) _PlanarFaces = new ObservableCollection<PlanarFace>(); return _PlanarFaces; } set { _PlanarFaces = value; OnPropertyChanged(); } }
        #endregion
        #region Create Section
        private DetailColumnView _DetailColumnView;
        public DetailColumnView DetailColumnView { get { if (_DetailColumnView == null) { _DetailColumnView = new DetailColumnView(); } return _DetailColumnView; } set { _DetailColumnView = value; OnPropertyChanged(); } }
        private ObservableCollection<SectionColumnView> _SectionColumnViews;
        public ObservableCollection<SectionColumnView> SectionColumnViews { get { if (_SectionColumnViews == null) { _SectionColumnViews = new ObservableCollection<SectionColumnView>(); } return _SectionColumnViews; } set { _SectionColumnViews = value; } }
        private DetailShopView _DetailShopView;
        public DetailShopView DetailShopView { get { if (_DetailShopView == null) { _DetailShopView = new DetailShopView(); } return _DetailShopView; } set { _DetailShopView = value; } }
        #endregion
        #region Create Dimension
        public DimensionView DimensionView { get; set; }
        //public ReferenceArray ReferenceColumns { get; set; }
        //public ReferenceArray ReferenceBeamLevel { get; set; }
        //public ReferenceArray ReferenceStirrup { get; set; }
        #endregion
        #region Tag Column
        private TagColumn _TagColumn;
        public TagColumn TagColumn { get => _TagColumn; set { _TagColumn = value; OnPropertyChanged(); } }
        #endregion
        #region Action
     
        private ProgressModel _ProgressModel;
        public ProgressModel ProgressModel { get => _ProgressModel; set { _ProgressModel = value; OnPropertyChanged(); } }
        private bool _IsCreateStirrupBars;
        public bool IsCreateStirrupBars { get => _IsCreateStirrupBars; set { _IsCreateStirrupBars = value; OnPropertyChanged(); } }
        private bool _IsCreateMainBars;
        public bool IsCreateMainBars { get => _IsCreateMainBars; set { _IsCreateMainBars = value; OnPropertyChanged(); } }
        private bool _IsCreateTagBars;
        public bool IsCreateTagBars { get => _IsCreateTagBars; set { _IsCreateTagBars = value; OnPropertyChanged(); } }
        private bool _IsCreateDetailView;
        public bool IsCreateDetailView { get => _IsCreateDetailView; set { _IsCreateDetailView = value; OnPropertyChanged(); } }
        private bool _IsCreateSectionView;
        public bool IsCreateSectionView { get => _IsCreateSectionView; set { _IsCreateSectionView = value; OnPropertyChanged(); } }
        private bool _IsCreateDimensionView;
        public bool IsCreateDimensionView { get => _IsCreateDimensionView; set { _IsCreateDimensionView = value; OnPropertyChanged(); } }
        private bool _IsCreateDimensionSection;
        public bool IsCreateDimensionSection { get => _IsCreateDimensionSection; set { _IsCreateDimensionSection = value; OnPropertyChanged(); } }
        private bool _IsCreateDetailShop;
        public bool IsCreateDetailShop { get => _IsCreateDetailShop; set { _IsCreateDetailShop = value; OnPropertyChanged(); } }

        #endregion
        #region BarDivision
        private DivisionBar _DivisionBar;
        public DivisionBar DivisionBar { get => _DivisionBar; set { _DivisionBar = value; OnPropertyChanged(); } }
        private ObservableCollection<BarsDivisionModel> _BarsDivisionModels;
        public ObservableCollection<BarsDivisionModel> BarsDivisionModels { get { if (_BarsDivisionModels == null) { _BarsDivisionModels = new ObservableCollection<BarsDivisionModel>(); } return _BarsDivisionModels; } set { _BarsDivisionModels = value; OnPropertyChanged(); } }
        #endregion
        #region DetailItem
        private DetailItemModel _DetailItemModel;
        public DetailItemModel DetailItemModel { get { if (_DetailItemModel == null) { _DetailItemModel = new DetailItemModel(); } return _DetailItemModel;  } set { _DetailItemModel = value; OnPropertyChanged(); } }
        #endregion
        public ColumnsModel(List<Element> columns, Document document,UnitProject unit)
        {
            IsRebar = true;
            GetFamilyType(columns, document);
            GetSectionStyle(columns, document);
            GetRebarBarType(document);
            GetRebarCoverType(document);
            GetAllNumberBar();
            GetInfoModels(columns, document);
            GetSettingModel(columns, document,unit);
            GetBarMainModels();
            GetPlanarFace(columns, document);
            GetSectionColumnsView();
            GetDimensionView();
            GetBarDivision();
            ProgressModel = new ProgressModel(0, 0);
        }
        #region Property
        private void GetFamilyType(List<Element> columns, Document document)
        {
            FamilyType = columns[0].get_Parameter(BuiltInParameter.ELEM_FAMILY_PARAM).AsValueString();
            for (int i = 0; i < columns.Count; i++)
            {
                AllType += ((columns[i].get_Parameter(BuiltInParameter.ELEM_TYPE_PARAM).AsValueString()) + " ");
            }
        }
        private void GetSectionStyle(List<Element> columns, Document document)
        {
            SectionStyle = ErrorColumns.GetSectionStyle(document, columns[0]);
            Section = SectionStyle.ToString();
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
        private void GetRebarCoverType(Document document)
        {
            RebarCoverTypes = new FilteredElementCollector(document).WhereElementIsElementType().OfClass(typeof(RebarCoverType)).Cast<RebarCoverType>().ToList();
            RebarCoverTypes=RebarCoverTypes.OrderBy(x => x.CoverDistance).ToList();
            RebarCoverType = RebarCoverTypes[1];
            Cover = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, RebarCoverType.CoverDistance, false));
        }
        public void CoverChange(Document document)
        {
            Cover = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, RebarCoverType.CoverDistance, false));
        }
        private void GetAllNumberBar()
        {
            AllNumberBar = new List<int>();
            if (SectionStyle == SectionStyle.RECTANGLE)
            {
                for (int i = 2; i <= 10; i++)
                {
                    AllNumberBar.Add(i);
                }
            }
            else
            {
                for (int i = 1; i <= 5; i++)
                {
                    AllNumberBar.Add(4*i);
                }
            }
           
        }
        private void GetInfoModels(List<Element> columns, Document document)
        {
            if (SectionStyle == SectionStyle.RECTANGLE)
            {
                InfoModels = ProccessInfoClumns.GetInfoModelsRectanggle(columns, document);
            }
            else
            {
                InfoModels = ProccessInfoClumns.GetInfoModelsCylindical(columns, document);
            }
        }
        private void GetSettingModel(List<Element> columns, Document document,UnitProject unit)
        {
            DrawModel = new DrawModel(1, 100, 4720, 8, 4);
            DrawModel.GetScale(SectionStyle, InfoModels, unit);
            DrawModelSection = new DrawModel(1, (SectionStyle == SectionStyle.RECTANGLE)?40:175, (SectionStyle == SectionStyle.RECTANGLE)?310:175, 8, 4);
            DrawModelSection.GetScaleSection(SectionStyle, InfoModels, unit);
            DrawModelDowels = new DrawModel(1, (SectionStyle == SectionStyle.RECTANGLE) ? 80 : 175, (SectionStyle == SectionStyle.RECTANGLE) ? 270 : 175, 8, 4);
            DrawModelDowels.GetScaleDowels(SectionStyle, InfoModels, unit);
            SelectedIndexModel = new SelectedIndexModel(0, 0, 0);
            double b = (SectionStyle == SectionStyle.RECTANGLE) ? InfoModels[0].b : InfoModels[0].D;
            SettingModel = new SettingModel(SectionStyle,b / 5, b / 5, b, b, b / 2, b / 2, b, b,  "Columns", "MC", columns[0], document);
            TagColumn = new TagColumn(SettingModel, document);
        }
        private void GetBarDivision()
        {
            DivisionBar = new DivisionBar();
        }
        #endregion
        #region Bars Method
        private void GetBarMainModels()
        {
            for (int i = 0; i < InfoModels.Count; i++)
            {
                BarMainModels.Add(new BarMainModel(InfoModels[i].NumberColumn, SectionStyle, AllBars[3]));
                StirrupModels.Add(new StirrupModel(InfoModels[i].NumberColumn, AllBars[0], AllBars[0], AllBars[0], false, false,0,(SectionStyle==SectionStyle.RECTANGLE)?InfoModels[i].b/2: InfoModels[i].D/2, (SectionStyle == SectionStyle.RECTANGLE) ? InfoModels[i].b / 2: InfoModels[i].D / 2, (SectionStyle == SectionStyle.RECTANGLE) ? InfoModels[i].b : InfoModels[i].D, false, InfoModels[i].b / 2, InfoModels[i].h / 2));
            }
            for (int i = 0; i < StirrupModels.Count; i++)
            {
                double l = InfoModels[i].hc - InfoModels[i].hb - InfoModels[i].zb;
                double hb = InfoModels[i].hb;
                double z = InfoModels[i].zb;
                StirrupModels[i].GetDistribute(l, hb, z);
            }
            for (int i = 0; i < InfoModels.Count; i++)
            {
                BarsDivisionModels.Add(new BarsDivisionModel(InfoModels[i].NumberColumn));
            }
        }
        #endregion
        #region PlanarFace
        private void GetPlanarFace(List<Element> columns, Document document)
        {
            PlanarFaces = ProccessInfoClumns.GetPlanarFaces(columns, document);
        }
        #endregion
        #region SectionColumnsView
        private void GetSectionColumnsView()
        {
            for (int i = 0; i < InfoModels.Count; i++)
            {
                SectionColumnViews.Add(new SectionColumnView());
            }
        }
        private void GetDimensionView()
        {
            DimensionView = new DimensionView(SettingModel);
        }
        #endregion
        #region
        public void ApplyBarDivision()
        {
            for (int i = 0; i < BarsDivisionModels.Count; i++)
            {
                BarsDivisionModels[i].GetStirrup(SectionStyle, StirrupModels[i], InfoModels[i],DivisionBar, Cover);
                BarsDivisionModels[i].GetAddHorizontal(SectionStyle, StirrupModels[i], InfoModels[i], DivisionBar, Cover);
                BarsDivisionModels[i].GetAddVertical(SectionStyle, StirrupModels[i], InfoModels[i], DivisionBar, Cover);
                BarsDivisionModels[i].GetMain(SectionStyle, BarMainModels[i], InfoModels[i], DivisionBar, Cover);
            }
        }
        public void ModifyBarDivision()
        {
            for (int i = 0; i < BarsDivisionModels.Count; i++)
            {
                BarsDivisionModels[i].Stirrup.Clear();
                BarsDivisionModels[i].AddH.Clear();
                BarsDivisionModels[i].AddV.Clear();
                BarsDivisionModels[i].Main.Clear();
            }
        }
        #endregion
        #region ConditionButton OK
        public bool ConditionButtonOK()
        {
            if (StirrupModels[SelectedIndexModel.SelectedColumn].TypeDis == 0)
            {
                if (StirrupModels[SelectedIndexModel.SelectedColumn].S <= 0) return false;
            }
            else
            {
                if (StirrupModels[SelectedIndexModel.SelectedColumn].S1 <= 0|| StirrupModels[SelectedIndexModel.SelectedColumn].S2 <= 0) return false;
            }
            for (int i = 0; i < BarMainModels.Count; i++)
            {
                if (BarMainModels[i].BarModels.Count == 0) return false;
            }
            
            if (SectionStyle == SectionStyle.RECTANGLE)
            {
                if (!SettingModel.SelectedShapeStirrup.Name.Equals("M_T1")) return false;
                if (StirrupModels[SelectedIndexModel.SelectedColumn].AddH && StirrupModels[SelectedIndexModel.SelectedColumn].TypeH == 0)
                {
                    if (StirrupModels[SelectedIndexModel.SelectedColumn].aH <= 0) return false;
                }
                if (StirrupModels[SelectedIndexModel.SelectedColumn].AddV && StirrupModels[SelectedIndexModel.SelectedColumn].TypeV == 0)
                {
                    if (StirrupModels[SelectedIndexModel.SelectedColumn].aV <= 0) return false;
                }
            }
            else
            {
                if (!SettingModel.SelectedShapeStirrup.Name.Equals("M_T3")/*|| !SettingModel.SelectedShapeStirrup.Name.Equals("M_SP")*/) return false;
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
        private List<FamilySymbol> GetAllFamilySymbol(Family family)
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

        #region   DetailItem
        public void GetDetailItem()
        {
            
            DetailItemModel = new DetailItemModel();
            
           
        }
        #endregion
    }
}
