using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCustomControls;
using DSP;
namespace R11_FoundationPile
{
    public class ColumnModel:BaseViewModel
    {
        #region property
        private string _Name;
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }
        private string _Style;
        public string Style { get => _Style; set { _Style = value; OnPropertyChanged(); } }
        private string _LocationName;
        public string LocationName { get => _LocationName; set { _LocationName = value; OnPropertyChanged(); } }
        private double _b;
        public double b { get => _b; set { _b = value; OnPropertyChanged(); } }
        private double _h;
        public double h { get => _h; set { _h = value; OnPropertyChanged(); } }
        private double _D;
        public double D { get => _D; set { _D = value; OnPropertyChanged(); } }
        
        private XYZ _PointPosition;
        public XYZ PointPosition { get => _PointPosition; set { _PointPosition = value; OnPropertyChanged(); } }
        private double _PointXPosition;
        public double PointXPosition { get => _PointXPosition; set { _PointXPosition = value; OnPropertyChanged(); } }
        private double _PointYPosition;
        public double PointYPosition { get => _PointYPosition; set { _PointYPosition = value; OnPropertyChanged(); } }
        private double _PointZPosition;
        public double PointZPosition { get => _PointZPosition; set { _PointZPosition = value; OnPropertyChanged(); } }

        private PlanarFace _Bottom;
        public PlanarFace Bottom { get => _Bottom; set { _Bottom = value; OnPropertyChanged(); } }
        private PlanarFace _South;
        public PlanarFace South { get => _South; set { _South = value; OnPropertyChanged(); } }
        private PlanarFace _Nouth;
        public PlanarFace Nouth { get => _Nouth; set { _Nouth = value; OnPropertyChanged(); } }
        private PlanarFace _West;
        public PlanarFace West { get => _West; set { _West = value; OnPropertyChanged(); } }
        private PlanarFace _East;
        public PlanarFace East { get => _East; set { _East = value; OnPropertyChanged(); } }
        private List<CylindricalFace> _Cylindicals;
        public List<CylindricalFace> Cylindicals { get => _Cylindicals; set { _Cylindicals = value; OnPropertyChanged(); } }
        private Element _Element;
        public Element Element { get => _Element; set { _Element = value; OnPropertyChanged(); } }
        private Level _BottomLevel;
        public Level BottomLevel { get => _BottomLevel; set { _BottomLevel = value; OnPropertyChanged(); } }
        #endregion
        public ColumnModel(ErrorColumns.SectionStyle sectionStyle,Element element,  PlanarFace bottom, PlanarFace west, PlanarFace east, PlanarFace south, PlanarFace nouth, Document document,UnitProject unit)
        {
            Style = sectionStyle.ToString();
            Element = element;
            LocationName = Element.get_Parameter(BuiltInParameter.COLUMN_LOCATION_MARK).AsString();
            Bottom = bottom; South = south; Nouth = nouth; West = west; East = east;
            b = PointModel.DistanceTo2(West, East.Origin, document);
            h = PointModel.DistanceTo2(South, Nouth.Origin, document);
            ElementId elementId2 = Element.get_Parameter(BuiltInParameter.FAMILY_BASE_LEVEL_PARAM).AsElementId();
            BottomLevel = new FilteredElementCollector(document).OfClass(typeof(Level)).Cast<Level>().Where(x => x.Id == elementId2).FirstOrDefault();
            GetPointPositionRectangle(unit);
            PointXPosition = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, PointPosition.X, false));
            PointYPosition = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, PointPosition.Y, false));
            PointZPosition = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, PointPosition.Z, false));
            Name = "bxh : " + b + " x " + h;
        }

        public ColumnModel(ErrorColumns.SectionStyle sectionStyle, Element element,   PlanarFace bottom,  Document document)
        {
            Style = sectionStyle.ToString();
            Element = element; Bottom = bottom;
            LocationName = Element.get_Parameter(BuiltInParameter.COLUMN_LOCATION_MARK).AsString();
            Cylindicals = SolidFace.GetAllCylindricalFace(Element);
            D = ErrorColumns.GetDiameter(document, Element);
            ElementId elementId2 = Element.get_Parameter(BuiltInParameter.FAMILY_BASE_LEVEL_PARAM).AsElementId();
            BottomLevel = new FilteredElementCollector(document).OfClass(typeof(Level)).Cast<Level>().Where(x => x.Id == elementId2).FirstOrDefault();
            PointPosition =  PointModel.ProjectToPlane((Element.Location as LocationPoint).Point, Bottom);
            PointXPosition = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, PointPosition.X, false));
            PointYPosition = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, PointPosition.Y, false));
            PointZPosition = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, PointPosition.Z, false));
            Name = "D : " + D;
        }
        private void GetPointPositionRectangle(UnitProject unit)
        {
            XYZ p0 = PointModel.ProjectToPlane((Element.Location as LocationPoint).Point, Bottom);
            XYZ p1 = PointModel.ProjectToPlane(p0, West);
            XYZ p2 = PointModel.ProjectToPlane(p1, South);
            XYZ p3 = p2 + unit.Convert(b * 0.5) * East.FaceNormal;
            PointPosition = p3 + unit.Convert(h * 0.5) * Nouth.FaceNormal;
        }
        private double GetHb(List<PlanarFace> planarFaces, Document document)
        {
            double a = 0;
            List<double> b = new List<double>();
            for (int i = 0; i < planarFaces.Count; i++)
            {
                b.Add(PointModel.DistanceTo2(planarFaces[0], planarFaces[i].Origin, document));
            }
            for (int i = 0; i < b.Count; i++)
            {
                if (a < b[i]) a = b[i];
            }
            return a;
        }
        public void RefreshPlanarFaceNormal(Document document, UnitProject unit)
        {
            if (Style.Equals("RECTANGLE"))
            {
                West = SolidFace.GetWest(Element);
                South = SolidFace.GetSouth(Element);
                East = SolidFace.GetEast(Element);
                Nouth = SolidFace.GetNouth(Element);
                Bottom = SolidFace.GetBottom(Element);
                //GetPointPositionRectangle(unit);
                //PointXPosition = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, PointPosition.X, false));
                //PointYPosition = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, PointPosition.Y, false));
                //PointZPosition = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, PointPosition.Z, false));
            }
            else
            {
                //PointPosition = PointModel.ProjectToPlane((Element.Location as LocationPoint).Point, Bottom);
                //PointXPosition = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, PointPosition.X, false));
                //PointYPosition = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, PointPosition.Y, false));
                //PointZPosition = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, PointPosition.Z, false));
            }


        }
        
    }
}
