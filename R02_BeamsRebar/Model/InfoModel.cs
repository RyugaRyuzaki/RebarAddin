using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using WpfCustomControls;
namespace R02_BeamsRebar
{
    public class InfoModel : BaseViewModel
    {
        public InfoModel(Element element, Document document, int numberSpan, double startPos, double endPos, bool consolLeft, bool consolRight, PlanarFace start, PlanarFace end)
        {
            FamilyType = ErrorBeams.GetFamilyTypeName(element);
            Type = (element.get_Parameter(BuiltInParameter.ELEM_TYPE_PARAM).AsValueString());
            NumberSpan = numberSpan;
            ElementType elementType = document.GetElement(element.GetTypeId()) as ElementType;
            b = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, elementType.LookupParameter("b").AsDouble(), false));
            h = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, elementType.LookupParameter("h").AsDouble(), false));
            zOffset = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, element.get_Parameter(BuiltInParameter.Z_OFFSET_VALUE).AsDouble(), false));
            startOffset = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, element.get_Parameter(BuiltInParameter.STRUCTURAL_BEAM_END0_ELEVATION).AsDouble(), false));
            endtOffset = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, element.get_Parameter(BuiltInParameter.STRUCTURAL_BEAM_END1_ELEVATION).AsDouble(), false));
            startPosition = startPos;
            endPosition = endPos;
            StartPlanar = start;
            EndPlanar = end;
            Length = Math.Abs(startPosition - endPosition);
            ConsolLeft = consolLeft;
            ConsolRight = consolRight;
            Element = element;
        }

        #region property
        private string _FamilyType;
        public string FamilyType { get => _FamilyType; set { _FamilyType = value; OnPropertyChanged(); } }
        private string _Type;
        public string Type { get => _Type; set { _Type = value; OnPropertyChanged(); } }
        private int _NumberSpan;
        public int NumberSpan { get => _NumberSpan; set { _NumberSpan = value; OnPropertyChanged(); } }
        private double _b;
        public double b { get => _b; set { _b = value; OnPropertyChanged(); } }

        private double _h;
        public double h { get => _h; set { _h = value; OnPropertyChanged(); } }

        private double _Length;
        public double Length { get => _Length; set { _Length = value; OnPropertyChanged(); } }

        private double _zOffset;
        public double zOffset { get => _zOffset; set { _zOffset = value; OnPropertyChanged(); } }

        private double _startOffset;
        public double startOffset { get => _startOffset; set { _startOffset = value; OnPropertyChanged(); } }

        private double _endtOffset;
        public double endtOffset { get => _endtOffset; set { _endtOffset = value; OnPropertyChanged(); } }

        private double _startPosition;
        public double startPosition { get => _startPosition; set { _startPosition = value; OnPropertyChanged(); } }

        private double _endtPosition;
        public double endPosition { get => _endtPosition; set { _endtPosition = value; OnPropertyChanged(); } }

        private PlanarFace _StartPlanar;
        public PlanarFace StartPlanar { get => _StartPlanar; set { _StartPlanar = value; OnPropertyChanged(); } }
        private PlanarFace _EndPlanar;
        public PlanarFace EndPlanar { get => _EndPlanar; set { _EndPlanar = value; OnPropertyChanged(); } }
        private List<PlanarFace> _TopBottomPlanar;
        public List<PlanarFace> TopBottomPlanar { get => _TopBottomPlanar; set { _TopBottomPlanar = value; OnPropertyChanged(); } }
        private List<PlanarFace> _LeftRightPlanar;
        public List<PlanarFace> LeftRightPlanar { get => _LeftRightPlanar; set { _LeftRightPlanar = value; OnPropertyChanged(); } }
        private Element _Element;
        public Element Element { get => _Element; set { _Element = value; OnPropertyChanged(); } }

        public bool ConsolLeft { get; set; }
        public bool ConsolRight { get; set; }

        #endregion


    }
}
