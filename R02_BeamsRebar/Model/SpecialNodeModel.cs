
using WpfCustomControls;
using Autodesk.Revit.DB;
namespace R02_BeamsRebar
{
    public class SpecialNodeModel:BaseViewModel
    {
        private int _NumberSpan;
        public int NumberSpan { get => _NumberSpan; set { _NumberSpan = value; OnPropertyChanged(); } }
        private int _NumberNode;
        public int NumberNode { get => _NumberNode; set { _NumberNode = value; OnPropertyChanged(); } }
        private double _Start;
        public double Start { get => _Start; set { _Start = value; OnPropertyChanged(); } }
        private double _End;
        public double End { get => _End; set { _End = value; OnPropertyChanged(); } }
        private double _Mid;
        public double Mid { get => _Mid; set { _Mid = value; OnPropertyChanged(); } }
        private double _Height;
        public double Height { get => _Height; set { _Height = value; OnPropertyChanged(); } }
        private Element _Element;
        public Element Element { get => _Element; set { _Element = value; OnPropertyChanged(); } }
        private bool _IsBeamColumn;
        public bool IsBeamColumn { get => _IsBeamColumn; set { _IsBeamColumn = value; OnPropertyChanged(); } }
        private PlanarFace _StartPlanarFace;
        public PlanarFace StartPlanarFace { get => _StartPlanarFace; set { _StartPlanarFace = value; OnPropertyChanged(); } }
        private PlanarFace _EndPlanarFace;
        public PlanarFace EndPlanarFace { get => _EndPlanarFace; set { _EndPlanarFace = value; OnPropertyChanged(); } }
        public SpecialNodeModel(int node,int numberSpan, double start, double end,Element element, PlanarFace startPlanarFace, PlanarFace endPlanarFace,Document document)
        {
            NumberNode = node;
            NumberSpan = numberSpan;
            Start = start;
            End = end;
            Mid = (Start + End) / 2;
            Element = element;
            StartPlanarFace = startPlanarFace;
            EndPlanarFace = endPlanarFace;
            IsBeamColumn = Element.Category.Name.Equals("Structural Columns");
            if (!IsBeamColumn)
            {
                ElementType elementType = document.GetElement(element.GetTypeId()) as ElementType;
                Height = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, elementType.LookupParameter("h").AsDouble(), false));
            }
            else { Height = 0; }
        }
    }
}
