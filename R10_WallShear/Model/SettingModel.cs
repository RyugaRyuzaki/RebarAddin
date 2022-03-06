using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System.Collections.Generic;
using System.Linq;
using WpfCustomControls;
namespace R10_WallShear
{
    public class SettingModel:BaseViewModel
    {
        public string TagName = "Dim 2.0";
        private double _tmin;
        public double tmin { get => _tmin; set { _tmin = value; OnPropertyChanged(); } }
        private double _dmin;
        public double dmin { get => _dmin; set { _dmin = value; OnPropertyChanged(); } }
        private double _DimH;
        public double DimH { get => _DimH; set { _DimH = value; OnPropertyChanged(); } }
        private double _DimV;
        public double DimV { get => _DimV; set { _DimV = value; OnPropertyChanged(); } }
        private double _TagH;
        public double TagH { get => _TagH; set { _TagH = value; OnPropertyChanged(); } }
        private double _TagV;
        public double TagV { get => _TagV; set { _TagV = value; OnPropertyChanged(); } }
        private double _L1;
        public double L1 { get => _L1; set { _L1 = value; OnPropertyChanged(); } }
        private double _L2;
        public double L2 { get => _L2; set { _L2 = value; OnPropertyChanged(); } }
      
        private string _WallsName;
        public string WallsName { get => _WallsName; set { _WallsName = value; OnPropertyChanged(); } }
        private string _DetailViewName;
        public string DetailViewName { get => _DetailViewName; set { _DetailViewName = value; OnPropertyChanged(); } }
        private string _PrefixDetail;
        public string PrefixDetail { get => _PrefixDetail; set { _PrefixDetail = value; OnPropertyChanged(); } }
        private bool _IsPrefixDetail;
        public bool IsPrefixDetail { get => _IsPrefixDetail; set { _IsPrefixDetail = value; OnPropertyChanged(); } }
        private string _SectionViewName;
        public string SectionViewName { get => _SectionViewName; set { _SectionViewName = value; OnPropertyChanged(); } }
        private string _PrefixSection;
        public string PrefixSection { get => _PrefixSection; set { _PrefixSection = value; OnPropertyChanged(); } }
        private List<RebarHookType> _RebarHookTypes;
        public List<RebarHookType> RebarHookTypes { get => _RebarHookTypes; set { _RebarHookTypes = value; OnPropertyChanged(); } }
        private RebarHookType _SelectedHook;
        public RebarHookType SelectedHook { get => _SelectedHook; set { _SelectedHook = value; OnPropertyChanged(); } }
        private List<RebarShape> _RebarShapes;
        public List<RebarShape> RebarShapes { get => _RebarShapes; set { _RebarShapes = value; OnPropertyChanged(); } }
        private RebarShape _SelectedShapeStirrup;
        public RebarShape SelectedShapeStirrup { get => _SelectedShapeStirrup; set { _SelectedShapeStirrup = value; OnPropertyChanged(); } }
        private RebarShape _SelectedShapeAnti;
        public RebarShape SelectedShapeAnti { get => _SelectedShapeAnti; set { _SelectedShapeAnti = value; OnPropertyChanged(); } }
        private List<Parameter> _Parameters;
        public List<Parameter> Parameters { get => _Parameters; set { _Parameters = value; OnPropertyChanged(); } }
        private Parameter _SelectedParameters;
        public Parameter SelectedParameters { get => _SelectedParameters; set { _SelectedParameters = value; OnPropertyChanged(); } }
        private List<Autodesk.Revit.DB.View> _ViewTemplate;
        public List<Autodesk.Revit.DB.View> ViewTemplate { get => _ViewTemplate; set { _ViewTemplate = value; OnPropertyChanged(); } }
        private Autodesk.Revit.DB.View _SelectedSectionTemplate;
        public Autodesk.Revit.DB.View SelectedSectionTemplate { get => _SelectedSectionTemplate; set { _SelectedSectionTemplate = value; OnPropertyChanged(); } }
        private Autodesk.Revit.DB.View _SelectedDetailTemplate;
        public Autodesk.Revit.DB.View SelectedDetailTemplate { get => _SelectedDetailTemplate; set { _SelectedDetailTemplate = value; OnPropertyChanged(); } }
        private Autodesk.Revit.DB.View _SelectedDetailShopTemplate;
        public Autodesk.Revit.DB.View SelectedDetailShopTemplate { get => _SelectedDetailShopTemplate; set { _SelectedDetailShopTemplate = value; OnPropertyChanged(); } }
        private List<DimensionType> _AllDimensionType;
        public List<DimensionType> AllDimensionType { get => _AllDimensionType; set { _AllDimensionType = value; OnPropertyChanged(); } }
        private DimensionType _SelectedDimensionType;
        public DimensionType SelectedDimensionType { get => _SelectedDimensionType; set { _SelectedDimensionType = value; OnPropertyChanged(); } }
        private DimensionType _SelectedDimensionDiameterType;
        public DimensionType SelectedDimensionDiameterType { get => _SelectedDimensionDiameterType; set { _SelectedDimensionDiameterType = value; OnPropertyChanged(); } }
        private List<ElementType> _AllRebarTag;
        public List<ElementType> AllRebarTag { get => _AllRebarTag; set { _AllRebarTag = value; OnPropertyChanged(); } }
        private ElementType _SelectedRebarTag;
        public ElementType SelectedRebarTag { get => _SelectedRebarTag; set { _SelectedRebarTag = value; OnPropertyChanged(); } }
        private ElementType _SelectedStirrupTag;
        public ElementType SelectedStirrupTag { get => _SelectedStirrupTag; set { _SelectedStirrupTag = value; OnPropertyChanged(); } }
        private List<ElementType> _AllDetailItemTag;
        public List<ElementType> AllDetailItemTag { get => _AllDetailItemTag; set { _AllDetailItemTag = value; OnPropertyChanged(); } }
        private ElementType _SelectedDetailItemTag;
        public ElementType SelectedDetailItemTag { get => _SelectedDetailItemTag; set { _SelectedDetailItemTag = value; OnPropertyChanged(); } }
        private ElementType _SelectedDetailDistanceTag;
        public ElementType SelectedDetailDistanceTag { get => _SelectedDetailDistanceTag; set { _SelectedDetailDistanceTag = value; OnPropertyChanged(); } }
        private List<MultiReferenceAnnotationType> _MultiReferenceAnnotationType;
        public List<MultiReferenceAnnotationType> MultiReferenceAnnotationType { get => _MultiReferenceAnnotationType; set { _MultiReferenceAnnotationType = value; OnPropertyChanged(); } }
        private MultiReferenceAnnotationType _SelectedMultiType;
        public MultiReferenceAnnotationType SelectedMultiType { get => _SelectedMultiType; set { _SelectedMultiType = value; OnPropertyChanged(); } }
        private List<ElementType> _TextNotes;
        public List<ElementType> TextNotes { get => _TextNotes; set { _TextNotes = value; OnPropertyChanged(); } }
        private ElementType _SelectedTextNote;
        public ElementType SelectedTextNote { get => _SelectedTextNote; set { _SelectedTextNote = value; OnPropertyChanged(); } }
        public SettingModel(double tmin, double dmin, double dimH, double dimV, double tagH, double tagV, double l1, double l2,  string wallsName, string prefixSection, Element column, Document document)
        {
            
            this.tmin = tmin;
            this.dmin = dmin;
            DimH = dimH;
            DimV = dimV;
            TagH = tagH;
            TagV = tagV;
            L1 = l1; L2 = l2;
            WallsName = wallsName;
            PrefixSection = prefixSection;
            IsPrefixDetail = false;
            RebarHookTypes = new FilteredElementCollector(document).WhereElementIsElementType().OfClass(typeof(RebarHookType)).Cast<RebarHookType>().ToList();
            RebarHookTypes.Sort((x, y) => x.Name.CompareTo(y.Name));
            List<Parameter> a = column.GetOrderedParameters().ToList();
            Parameters = new List<Parameter>();
            foreach (var item in a)
            {
                if (item.StorageType == StorageType.String &&item.UserModifiable==false )
                {
                    Parameters.Add(item);
                }
            }
            ViewTemplate = new FilteredElementCollector(document).OfClass(typeof(Autodesk.Revit.DB.View)).WhereElementIsNotElementType().Cast<Autodesk.Revit.DB.View>().Where(x => x.IsTemplate)
                .Where(x => x.get_Parameter(BuiltInParameter.VIEW_DISCIPLINE).AsValueString().Equals("Structural"))
                .ToList();
            
            if (ViewTemplate.Count != 0)
            {
                SelectedSectionTemplate = ViewTemplate[0];
                SelectedDetailTemplate = ViewTemplate[0];
                SelectedDetailShopTemplate = ViewTemplate[0];
            }
            SelectedParameters = Parameters[0];
            
            SelectedHook = RebarHookTypes[0];
            PrefixDetail = column.get_Parameter(BuiltInParameter.WALL_BASE_CONSTRAINT).AsValueString();
            AllDimensionType = new FilteredElementCollector(document).OfClass(typeof(DimensionType)).Cast<DimensionType>().ToList();
            SelectedDimensionType = AllDimensionType.Where(x => x.FamilyName.Equals("Linear Dimension Style")).FirstOrDefault();
            SelectedDimensionDiameterType = AllDimensionType.Where(x => x.FamilyName.Equals("Diameter Dimension Style")).FirstOrDefault();
            GetDetailViewName();
            GetSectionViewName();
            RebarShapes = new FilteredElementCollector(document).OfClass(typeof(RebarShape)).Cast<RebarShape>().ToList();
            if (RebarShapes.Count != 0)
            {
                SelectedShapeStirrup = RebarShapes.Where(x => x.Name == "M_T1").FirstOrDefault();
                SelectedShapeAnti = RebarShapes.Where(x => x.Name == "M_T10").FirstOrDefault();
            }
            AllRebarTag = new FilteredElementCollector(document).WhereElementIsElementType().OfCategory(BuiltInCategory.OST_RebarTags).Cast<ElementType>().ToList();
            SelectedRebarTag = AllRebarTag[0];
            SelectedStirrupTag = AllRebarTag[0];
            AllDetailItemTag = new FilteredElementCollector(document).WhereElementIsElementType().OfCategory(BuiltInCategory.OST_DetailComponentTags).Cast<ElementType>().ToList();

            if ( AllDetailItemTag.Count != 0)
            {
                SelectedDetailItemTag = AllDetailItemTag[0];
                SelectedDetailDistanceTag = AllDetailItemTag[0];
            }
            MultiReferenceAnnotationType = new FilteredElementCollector(document).WhereElementIsElementType().OfClass(typeof(MultiReferenceAnnotationType)).Cast<MultiReferenceAnnotationType>().ToList();

            if (MultiReferenceAnnotationType != null && MultiReferenceAnnotationType.Count != 0)
            {
                SelectedMultiType = MultiReferenceAnnotationType[0];
            }
            TextNotes = new FilteredElementCollector(document).WhereElementIsElementType().OfClass(typeof(TextNoteType)).Cast<ElementType>().ToList();
            SelectedTextNote = TextNotes[0];
        }
        public void GetDetailViewName()
        {
            if (IsPrefixDetail)
            {
                DetailViewName = WallsName + "-" + PrefixDetail;
            }
            else
            {
                DetailViewName = WallsName;
            }
        }
        public void GetSectionViewName()
        {
            SectionViewName = PrefixSection + " - ";
        }

        private List<Element> GetAllElement(Document document)
        {
            return new FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_StructuralColumns).WhereElementIsNotElementType().Where(x => IsNotNullPara(document, x)).ToList();

        }
        private bool IsNotNullPara(Document document, Element element)
        {
            ElementType elementType = document.GetElement(element.GetTypeId()) as ElementType;
            return elementType.LookupParameter("h") != null;
        }
        private List<List<Element>> GetAllListElement(Document document)
        {
            List<List<Element>> AllListElement = new List<List<Element>>();
            List<Element> AllElements = GetAllElement(document);
            if (AllElements.Count != 0)
            {

                // tạo 1 listtamj bằng với AllElement để cho dữ liệu khỏi bị mất
                List<Element> listTemp = AllElements;
                //lấy 1 biến tổng của các element cần lọc tức là AllElement(listTemp)
                int i = listTemp.Count;
                //bắt đầu chạy ngược nếu mà i==0 thì dừng
                while (i > 0)
                {
                    //tạo list đầu tiên.
                    List<Element> list = new List<Element>();
                    //lấy 1 e0 là Element đầu tiên  của list tạm
                    Element e0 = listTemp[0];
                    //khai báo biến double a0 = e0.LookupParameter("W").AsDouble();
                    ElementType elementTypea = document.GetElement(e0.GetTypeId()) as ElementType;
                    double a0 = elementTypea.LookupParameter("h").AsDouble();
                    //Bắt đầu chạy 1 vòng lập tìm ra các element nào có b0=a0 bo khai báo trong vòng lập vì không thể so sánh 1 parameter với 1 parameter dc
                    for (int j = 0; j < listTemp.Count; j++)
                    {
                        //khai báo biến double b0 = listTemp[j].LookupParameter("W").AsDouble();
                        ElementType elementTypeb = document.GetElement(listTemp[j].GetTypeId()) as ElementType;
                        double b0 = elementTypeb.LookupParameter("h").AsDouble();
                        //nếu a0=b0 thì mình add list
                        if (AreEqual(b0, a0))
                        {
                            list.Add(listTemp[j]);

                        }
                    }
                    // Remove các element trong listemp từ list
                    for (int k = 0; k < list.Count; k++)
                    {
                        listTemp.Remove(list[k]);
                    }
                    // Add list và AllListElement
                    AllListElement.Add(list);
                    //gán ngược lại biến i vì lúc này số lượng element trong listTemp đã bị thay đổi do đã trừ bớt ra.
                    i = listTemp.Count;
                    // đến khi không còn element nào trong listTemp này tức là i==0 thì vòng white sẽ dừng lại

                }

            }
            return AllListElement;
        }
        public static bool AreEqual(double firstValue, double secondValue, double tolerance = 1.0e-9)
        {
            return (secondValue - tolerance < firstValue && firstValue < secondValue + tolerance);
        }
    }
}
