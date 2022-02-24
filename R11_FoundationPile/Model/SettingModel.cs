using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WpfCustomControls;
using DSP;
namespace R11_FoundationPile
{
    public class SettingModel : BaseViewModel
    {
        #region Property

        private List<string> _CategoryPiles;
        public List<string> CategoryPiles { get { if (_CategoryPiles == null) { _CategoryPiles = new List<string>() { "Structural Columns", "Structural Foundations" }; } return _CategoryPiles; } set { _CategoryPiles = value; OnPropertyChanged(); } }
        private string _SelectedCategoyryPile;
        public string SelectedCategoyryPile { get => _SelectedCategoyryPile; set { _SelectedCategoyryPile = value; OnPropertyChanged(); } }
        private string _StyleFamilyType;
        public string StyleFamilyType { get => _StyleFamilyType; set { _StyleFamilyType = value; OnPropertyChanged(); } }
        private ObservableCollection<Family> _FamilyPiles;
        public ObservableCollection<Family> FamilyPiles { get => _FamilyPiles; set { _FamilyPiles = value; OnPropertyChanged(); } }
        private Family _SelectedPileFamily;
        public Family SelectedPileFamily { get => _SelectedPileFamily; set { _SelectedPileFamily = value; OnPropertyChanged(); } }

        private ObservableCollection<FamilySymbol> _FamilySymbolPiles;
        public ObservableCollection<FamilySymbol> FamilySymbolPiles { get => _FamilySymbolPiles; set { _FamilySymbolPiles = value; OnPropertyChanged(); } }
        private FamilySymbol _SelectedPileFamilyType;
        public FamilySymbol SelectedPileFamilyType { get => _SelectedPileFamilyType; set { _SelectedPileFamilyType = value; OnPropertyChanged(); } }

        private double _DiameterPile;
        public double DiameterPile { get => _DiameterPile; set { _DiameterPile = value; OnPropertyChanged(); } }
        private double _b;
        public double b { get => _b; set { _b = value; OnPropertyChanged(); } }
        private double _h;
        public double h { get => _h; set { _h = value; OnPropertyChanged(); } }
        private double _DistancePP;    // distance pile to pile
        public double DistancePP { get => _DistancePP; set { _DistancePP = value; OnPropertyChanged(); } }
        private double _DistancePS;    // distance pile to Side of Foundation
        public double DistancePS { get => _DistancePS; set { _DistancePS = value; OnPropertyChanged(); } }
        private double _Overlap;    // Overlap pile to foundation
        public double Overlap { get => _Overlap; set { _Overlap = value; OnPropertyChanged(); } }
        private double _LengthPile;    // Overlap pile to foundation
        public double LengthPile { get => _LengthPile; set { _LengthPile = value; OnPropertyChanged(); } }

        private List<Parameter> _PileParameters;
        public List<Parameter> PileParameters { get => _PileParameters; set { _PileParameters = value; OnPropertyChanged(); } }
        private Parameter _SelectedParameters;
        public Parameter SelectedPileParameter { get => _SelectedParameters; set { _SelectedParameters = value; OnPropertyChanged(); } }

        private bool _IsApply;    // Overlap pile to foundation
        public bool IsApply { get => _IsApply; set { _IsApply = value; OnPropertyChanged(); } }


        private List<string> _CategoryFoundation;
        public List<string> CategoryFoundations { get { if (_CategoryFoundation == null) { _CategoryFoundation = new List<string>() { "Floors", "Structural Foundations" }; } return _CategoryFoundation; } set { _CategoryFoundation = value; OnPropertyChanged(); } }
        private string _SelectedCategoyryFoundation;
        public string SelectedCategoyryFoundation { get => _SelectedCategoyryFoundation; set { _SelectedCategoyryFoundation = value; OnPropertyChanged(); } }
        private ObservableCollection<FloorType> _FoundationTypes;
        public ObservableCollection<FloorType> FoundationTypes { get => _FoundationTypes; set { _FoundationTypes = value; OnPropertyChanged(); } }
        private FloorType _SelectedFoundationType;
        public FloorType SelectedFoundationType { get => _SelectedFoundationType; set { _SelectedFoundationType = value; OnPropertyChanged(); } }
        private FloorType _SelectedFormWorkType;
        public FloorType SelectedFormWorkType { get => _SelectedFormWorkType; set { _SelectedFormWorkType = value; OnPropertyChanged(); } }
        private double _HeightFoundation;    // Overlap pile to foundation
        public double HeightFoundation { get => _HeightFoundation; set { _HeightFoundation = value; OnPropertyChanged(); } }
        private double _HeightFormWork;    // Overlap pile to foundation
        public double HeightFormWork { get => _HeightFormWork; set { _HeightFormWork = value; OnPropertyChanged(); } }
        private double _OffsetFormWork;    // Overlap pile to foundation
        public double OffsetFormWork { get => _OffsetFormWork; set { _OffsetFormWork = value; OnPropertyChanged(); } }

        private List<Autodesk.Revit.DB.View> _ViewTemplate;
        public List<Autodesk.Revit.DB.View> ViewTemplates { get => _ViewTemplate; set { _ViewTemplate = value; OnPropertyChanged(); } }
        private Autodesk.Revit.DB.View _SelectedPilePlanTemplate;
        public Autodesk.Revit.DB.View SelectedPilePlanTemplate { get => _SelectedPilePlanTemplate; set { _SelectedPilePlanTemplate = value; OnPropertyChanged(); } }
        private Autodesk.Revit.DB.View _SelectedFoundationPlanTemplate;
        public Autodesk.Revit.DB.View SelectedFoundationPlanTemplate { get => _SelectedFoundationPlanTemplate; set { _SelectedFoundationPlanTemplate = value; OnPropertyChanged(); } }
        private Autodesk.Revit.DB.View _SelectedFoundationSectionTemplate;
        public Autodesk.Revit.DB.View SelectedFoundationSectionTemplate { get => _SelectedFoundationSectionTemplate; set { _SelectedFoundationSectionTemplate = value; OnPropertyChanged(); } }
        private Autodesk.Revit.DB.View _SelectedFoundationDetailTemplate;
        public Autodesk.Revit.DB.View SelectedFoundationDetailTemplate { get => _SelectedFoundationDetailTemplate; set { _SelectedFoundationDetailTemplate = value; OnPropertyChanged(); } }
        private string _PileNamePrefix;
        public string PileNamePrefix { get => _PileNamePrefix; set { _PileNamePrefix = value; OnPropertyChanged(); } }

        private string _FoundationPlaneName;    // Overlap pile to foundation
        public string FoundationPlaneName { get => _FoundationPlaneName; set { _FoundationPlaneName = value; OnPropertyChanged(); } }
        private string _FoundationNamePrefix;    // Overlap pile to foundation
        public string FoundationNamePrefix { get => _FoundationNamePrefix; set { _FoundationNamePrefix = value; OnPropertyChanged(); } }
        private bool _IsCreateFormWork;    // Overlap pile to foundation
        public bool IsCreateFormWork { get => _IsCreateFormWork; set { _IsCreateFormWork = value; OnPropertyChanged(); } }
        public WallType WallType { get; set; }


        public List<RebarCoverType> RebarCoverTypes { get; set; }
        private RebarCoverType _SelectedTopCover;    // Overlap pile to foundation
        public RebarCoverType SelectedTopCover { get => _SelectedTopCover; set { _SelectedTopCover = value; OnPropertyChanged(); } }
        private RebarCoverType _SelectedSideCover;    // Overlap pile to foundation
        public RebarCoverType SelectedSideCover { get => _SelectedSideCover; set { _SelectedSideCover = value; OnPropertyChanged(); } }
        private RebarCoverType _SelectedBotomCover;    // Overlap pile to foundation
        public RebarCoverType SelectedBotomCover { get => _SelectedBotomCover; set { _SelectedBotomCover = value; OnPropertyChanged(); } }
        private List<RebarHookType> _RebarHookTypes;
        public List<RebarHookType> RebarHookTypes { get => _RebarHookTypes; set { _RebarHookTypes = value; OnPropertyChanged(); } }
        private RebarHookType _SelectedHook;
        public RebarHookType SelectedHook { get => _SelectedHook; set { _SelectedHook = value; OnPropertyChanged(); } }

        private List<DimensionType> _AllDimensionType;
        public List<DimensionType> DimensionTypes { get => _AllDimensionType; set { _AllDimensionType = value; OnPropertyChanged(); } }
        private DimensionType _SelectedDimensionType;
        public DimensionType SelectedDimensionType { get => _SelectedDimensionType; set { _SelectedDimensionType = value; OnPropertyChanged(); } }

        private List<ElementType> _TextNotes;
        public List<ElementType> TextNotes { get => _TextNotes; set { _TextNotes = value; OnPropertyChanged(); } }
        private ElementType _SelectedTextNote;
        public ElementType SelectedTextNote { get => _SelectedTextNote; set { _SelectedTextNote = value; OnPropertyChanged(); } }
        private bool _CheckedText;    // Overlap pile to foundation
        public bool CheckedText { get => _CheckedText; set { _CheckedText = value; OnPropertyChanged(); } }
        private List<ElementType> _TagFoundations;
        public List<ElementType> TagFoundations { get => _TagFoundations; set { _TagFoundations = value; OnPropertyChanged(); } }
        private ElementType _SelectedFoundationTag;
        public ElementType SelectedFoundationTag { get => _SelectedFoundationTag; set { _SelectedFoundationTag = value; OnPropertyChanged(); } }
        private List<ElementType> _TagPiles;
        public List<ElementType> TagPiles { get => _TagPiles; set { _TagPiles = value; OnPropertyChanged(); } }
        private ElementType _SelectedPileTag;
        public ElementType SelectedPileTag { get => _SelectedPileTag; set { _SelectedPileTag = value; OnPropertyChanged(); } }
        private double _OffsetDim;
        public double OffsetDim { get => _OffsetDim; set { _OffsetDim = value; OnPropertyChanged(); } }

        public TagMode Mode = TagMode.TM_ADDBY_CATEGORY;
        public TagOrientation Horizontal = TagOrientation.Horizontal;
        public TagOrientation Vertical = TagOrientation.Vertical;

        #endregion
        #region   Spot codinator
        private ObservableCollection<SpotDimensionType> _SpotDimensionTypes;
        public ObservableCollection<SpotDimensionType> SpotDimensionTypes { get => _SpotDimensionTypes; set { _SpotDimensionTypes = value; OnPropertyChanged(); } }
        private SpotDimensionType _SelectedSpotDimensionType;
        public SpotDimensionType SelectedSpotDimensionType { get => _SelectedSpotDimensionType; set { _SelectedSpotDimensionType = value; OnPropertyChanged(); } }

        #endregion
        #region View
        public ViewFamilyType FoundationDetailViewType { get; set; }
        public ViewFamilyType FoundationSectionType { get; set; }
        private const string TypeDetail = "@FoundationDetail";
        private const string TypeSection = "@FoundationSection";
        #endregion
        public SettingModel(Document document, SelectedIndexModel selectedIndexModel)
        {
            SelectedCategoyryPile = CategoryPiles[0];
            GetFamilyPile(document);
            SelectedPileFamily = FamilyPiles[0];
            GetAllFamilySymbol();
            SelectedPileFamilyType = FamilySymbolPiles[0];
            GetDiameterPile(document);
            DistancePP = 3;
            if (DiameterPile != 0) { DistancePS = DiameterPile; Overlap = DiameterPile * 0.5; LengthPile = 10 * DiameterPile; }
            //GetParameter(document);
            ViewTemplates = new FilteredElementCollector(document).OfClass(typeof(Autodesk.Revit.DB.View)).WhereElementIsNotElementType().Cast<Autodesk.Revit.DB.View>().Where(x => x.IsTemplate)
              .Where(x => x.get_Parameter(BuiltInParameter.VIEW_DISCIPLINE).AsValueString().Equals("Structural"))
              .ToList();
            if (ViewTemplates.Count != 0)
            {
                SelectedPilePlanTemplate = ViewTemplates[0];
                SelectedFoundationPlanTemplate = ViewTemplates[0];
                SelectedFoundationSectionTemplate = ViewTemplates[0];
                SelectedFoundationDetailTemplate = ViewTemplates[0];
            }
            PileNamePrefix = "P-";
            RebarCoverTypes = new FilteredElementCollector(document).WhereElementIsElementType().OfClass(typeof(RebarCoverType)).Cast<RebarCoverType>().ToList();
            RebarCoverTypes = RebarCoverTypes.OrderBy(x => x.CoverDistance).ToList();
            if (RebarCoverTypes.Count != 0)
            {
                SelectedTopCover = RebarCoverTypes[0];
                SelectedSideCover = RebarCoverTypes[0];
                SelectedBotomCover = RebarCoverTypes[0];
            }
            SelectedCategoyryFoundation = CategoryFoundations[0];
            GetFoundationTypes(document);
            HeightFoundation = WidthFloor(document, SelectedFoundationType);
            HeightFormWork = WidthFloor(document, SelectedFormWorkType);
            OffsetFormWork = WidthFloor(document, SelectedFormWorkType);
            DimensionTypes = new FilteredElementCollector(document).OfClass(typeof(DimensionType)).Cast<DimensionType>().ToList();
            SelectedDimensionType = DimensionTypes.Where(x => x.FamilyName.Equals("Linear Dimension Style")).FirstOrDefault();
            FoundationPlaneName = "Foundation Plan";
            FoundationNamePrefix = "M - ";

            TextNotes = new FilteredElementCollector(document).WhereElementIsElementType().OfClass(typeof(TextNoteType)).Cast<ElementType>().OrderBy(x=>x.get_Parameter(BuiltInParameter.TEXT_SIZE).AsDouble()).ToList();
            SelectedTextNote = TextNotes[0];
            GetTag(document);
            OffsetDim = 700;
            GetSpotDimensionType(document);
            RebarHookTypes = new FilteredElementCollector(document).WhereElementIsElementType().OfClass(typeof(RebarHookType)).Cast<RebarHookType>().Where(x=>x.Name.Contains("Stirrup")).ToList();
            RebarHookTypes.Sort((x, y) => x.Name.CompareTo(y.Name));
            SelectedHook = RebarHookTypes[0];
            WallType  = new FilteredElementCollector(document).OfClass(typeof(WallType)).Cast<WallType>().Where(w => w.FamilyName.Equals("Basic Wall")).FirstOrDefault();
        }
        
        public void GetFoundationViewType(Document document)
        {
            List<ViewFamilyType> list = new FilteredElementCollector(document)
                                .OfClass(typeof(ViewFamilyType))
                                .Cast<ViewFamilyType>()
                                .Where(x => ViewFamily.StructuralPlan == x.ViewFamily && x.Name.Equals(TypeDetail)).ToList();
            if (list.Count == 0)
            {
                ViewFamilyType a = new FilteredElementCollector(document)
                                .OfClass(typeof(ViewFamilyType))
                                .Cast<ViewFamilyType>()
                                .Where(x => ViewFamily.StructuralPlan == x.ViewFamily).FirstOrDefault();
                FoundationDetailViewType = a.Duplicate(TypeDetail) as ViewFamilyType;
            }
            else
            {
                FoundationDetailViewType = list.Where(x => x.Name.Equals(TypeDetail)).FirstOrDefault();
            }
        }
        public void GetFoundationSectionType(Document document)
        {
            List<ViewFamilyType> list = new FilteredElementCollector(document)
                                .OfClass(typeof(ViewFamilyType))
                                .Cast<ViewFamilyType>()
                                .Where(x => ViewFamily.Section == x.ViewFamily && x.Name.Equals(TypeSection)).ToList();
            if (list.Count == 0)
            {
                ViewFamilyType a = new FilteredElementCollector(document)
                                .OfClass(typeof(ViewFamilyType))
                                .Cast<ViewFamilyType>()
                                .Where(x => ViewFamily.Section == x.ViewFamily).FirstOrDefault();
                FoundationSectionType = a.Duplicate(TypeSection) as ViewFamilyType;
            }
            else
            {
                FoundationSectionType = list.Where(x => x.Name.Equals(TypeSection)).FirstOrDefault();
            }
        }
        #region   Method
        public void GetSpotDimensionType(Document document)
        {
            SpotDimensionTypes =new ObservableCollection<SpotDimensionType> (new FilteredElementCollector(document).WhereElementIsElementType().OfClass(typeof(SpotDimensionType)).Cast<SpotDimensionType>().Where(x=>x.FamilyName.Equals("Spot Coordinates")).ToList());
            if (SpotDimensionTypes.Count != 0) SelectedSpotDimensionType = SpotDimensionTypes[0];
        }

        public void GetTag(Document document)
        {
            BuiltInCategory categoryFoundation = (SelectedCategoyryFoundation.Equals("Floors")) ? (BuiltInCategory.OST_FloorTags) : (BuiltInCategory.OST_StructuralFoundationTags);
            BuiltInCategory categoryPile = (SelectedCategoyryPile.Equals("Structural Columns")) ? (BuiltInCategory.OST_StructuralColumnTags) : (BuiltInCategory.OST_StructuralFoundationTags);
            TagFoundations = new FilteredElementCollector(document).WhereElementIsElementType().OfCategory(categoryFoundation).Cast<ElementType>().ToList();
            if (TagFoundations.Count != 0)
            {
                SelectedFoundationTag = TagFoundations[0];
            }
            TagPiles = new FilteredElementCollector(document).WhereElementIsElementType().OfCategory(categoryPile).Cast<ElementType>().ToList();
            if (TagPiles.Count != 0)
            {
                SelectedPileTag = TagPiles[0];
            }
        }
        public void GetFamilyPile(Document document)
        {
            FamilyPiles = new ObservableCollection<Family>(new FilteredElementCollector(document).OfClass(typeof(Family)).Cast<Family>().Where(x => x.FamilyCategory.Name.Equals(SelectedCategoyryPile)).ToList());
        }
        public void GetFamilySymBollTitleBlock(Document document)
        {
            List<Family> TitleBlockFamily = new FilteredElementCollector(document).OfClass(typeof(Family)).Cast<Family>().Where(x => x.FamilyCategory.Name.Equals("TitleBlocks")).ToList();
            ObservableCollection<FamilySymbol> TitileBlocks = new ObservableCollection<FamilySymbol>();
            foreach (var item in TitleBlockFamily)
            {
                foreach (ElementId familySymbolId in item.GetFamilySymbolIds())
                {
                    FamilySymbol familySymbol = item.Document.GetElement(familySymbolId) as FamilySymbol;
                    TitileBlocks.Add(familySymbol);
                }
            }
        }
        public void GetAllFamilySymbol()
        {
            FamilySymbolPiles = new ObservableCollection<FamilySymbol>();
            foreach (ElementId familySymbolId in SelectedPileFamily.GetFamilySymbolIds())
            {
                FamilySymbol familySymbol = SelectedPileFamily.Document.GetElement(familySymbolId) as FamilySymbol;
                FamilySymbolPiles.Add(familySymbol);
            }
        }
        public void GetDiameterPile(Document document)
        {
            if (SelectedPileFamilyType != null)
            {
                ElementType elementType = document.GetElement(SelectedPileFamilyType.Id) as ElementType;
                Parameter bP = elementType.LookupParameter("b");
                Parameter hP = elementType.LookupParameter("h");
                if (bP == null || hP == null)
                {

                    if (bP != null)
                    {
                        DiameterPile = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, bP.AsDouble(), false));
                        StyleFamilyType = "CYLINDICAL";
                    }
                    else
                    {
                        Parameter dP = elementType.LookupParameter("Dp");
                        if (dP == null)
                        {
                            DiameterPile = 0;
                            StyleFamilyType = "ORTHER";
                        }
                        else
                        {
                            DiameterPile = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, dP.AsDouble(), false));
                            StyleFamilyType = "CYLINDICAL";
                        }
                    }

                }
                else
                {
                    b = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, bP.AsDouble(), false));
                    h = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, hP.AsDouble(), false));
                    DiameterPile = Math.Max(b, h);
                    StyleFamilyType = "RECTANGLE";
                }
            }
        }
        public void GetFoundationTypes(Document document)
        {
            List<FloorType> floorTypes = new FilteredElementCollector(document)
                  .OfClass(typeof(FloorType))
                  .Cast<FloorType>()
                  .ToList();
            bool foundation = SelectedCategoyryFoundation == "Floors";
            FoundationTypes = new ObservableCollection<FloorType>(floorTypes.Where(x => ConditionOneLayer(x)).Where(x => (foundation) ? (!x.IsFoundationSlab) : (x.IsFoundationSlab)).OrderBy(x => WidthFloor(document, x)).ToList());
            if (FoundationTypes.Count != 0) { SelectedFoundationType = FoundationTypes[0]; SelectedFormWorkType = FoundationTypes[0]; }
        }

        private bool ConditionOneLayer(FloorType floorType)
        {
            CompoundStructure compound = floorType.GetCompoundStructure();
            return (compound.GetWidth() == compound.GetLayerWidth(compound.GetLastCoreLayerIndex()));
        }
        public double WidthFloor(Document document, FloorType floorType)
        {
            CompoundStructure compound = floorType.GetCompoundStructure();
            return double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, compound.GetWidth(), false));
        }
      
        #endregion
    }
}
