using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static R01_ColumnsRebar.ErrorColumns;
using WpfCustomControls;
using DSP;
namespace R01_ColumnsRebar
{
    public class DetailItem:BaseViewModel
    {
        #region property
        private int _RebarNumber;
        public int RebarNumber { get => _RebarNumber; set { _RebarNumber = value; OnPropertyChanged(); } }
        private int _NoBar;
        public int NoBar { get => _NoBar; set { _NoBar = value; OnPropertyChanged(); } }
        private double _Diameter;
        public double Diameter { get => _Diameter; set { _Diameter = value; OnPropertyChanged(); } }
        private double _Length;
        public double Length { get => _Length; set { _Length = value; OnPropertyChanged(); } }
        private double _L;
        public double L { get => _L; set { _L = value; OnPropertyChanged(); } }
        private double _La;
        public double La { get => _La; set { _La = value; OnPropertyChanged(); } }
        private double _Lb;
        public double Lb { get => _Lb; set { _Lb = value; OnPropertyChanged(); } }
        private double _Overight;
        public double Overight { get => _Overight; set { _Overight = value; OnPropertyChanged(); } }
        private double _Distance;
        public double Distance { get => _Distance; set { _Distance = value; OnPropertyChanged(); } }
        private DetailItemStyle _Type;
        public DetailItemStyle Type { get => _Type; set { _Type = value; OnPropertyChanged(); } }
        private LocationBarModel _Location;
        public LocationBarModel Location { get => _Location; set { _Location = value; OnPropertyChanged(); } }
        private List<LocationBarModel> _AllLocation;
        public List<LocationBarModel> AllLocation { get => _AllLocation; set { _AllLocation = value; OnPropertyChanged(); } }
        private FamilySymbol _DetailItemType;
        public FamilySymbol DetailItemType { get => _DetailItemType; set { _DetailItemType = value; OnPropertyChanged(); } }
        private FamilyInstance _Detail;
        public FamilyInstance Detail { get => _Detail; set { _Detail = value; OnPropertyChanged(); } }
        public TagMode Mode = TagMode.TM_ADDBY_CATEGORY;
        public TagOrientation Horizontal = TagOrientation.Horizontal;
        public TagOrientation Vertical = TagOrientation.Vertical;
        private IndependentTag _Tag;
        public IndependentTag Tag { get => _Tag; set { _Tag = value; OnPropertyChanged(); } }
        #endregion
        public DetailItem()
        {
            AllLocation = new List<LocationBarModel>(2);
        }
        #region
        public void GetPropertyStirrup(double overight,double distance, double diameter,int number, double l)
        {
            Diameter = diameter;Distance = distance;L = l;NoBar = number; Overight = overight;  Type = DetailItemStyle.DT01;
            
        }
        public void GetPropertyStirrupSection(SectionStyle sectionStyle, double distance, double diameter, int number, double l,double la,double lb)
        {
            Diameter = diameter; Distance = distance; L = l;La = la;Lb = lb; NoBar = number;  Type =(sectionStyle==SectionStyle.RECTANGLE)? DetailItemStyle.DT04A:DetailItemStyle.DT04B;

        }
        public void GetPropertyAddHSection( double distance, double diameter, int number, double l, double la, double lb)
        {
            Diameter = diameter; Distance = distance; L = l; La = la; Lb = lb; NoBar = number; 

        }
        public void GetPropertyLong( double diameter, int number)
        {
            Diameter = diameter;  NoBar = number; 
        }
        #endregion
        #region Setproperty
        private void GetDetailItem(Document document)
        {
            try
            {
                Family family = new FilteredElementCollector(document)
                    .OfClass(typeof(Family))
                    .Cast<Family>()
                    .Where(x => x.FamilyCategory.Name.Equals("Detail Items"))
                    .Where(x => x.Name.Equals(Type.ToString()))
                    .FirstOrDefault();
                DetailItemType = GetAllFamilySymbol(family).Where(x => x.Name.Equals(Type.ToString())).FirstOrDefault();
            }
            catch (Exception)
            {
               
            }
        }
        private static List<FamilySymbol> GetAllFamilySymbol(Family family)
        {
            List<FamilySymbol> familySymbols = new List<FamilySymbol>();

            foreach (ElementId familySymbolId in family.GetFamilySymbolIds())
            {
                FamilySymbol familySymbol = family.Document.GetElement(familySymbolId) as FamilySymbol;
                familySymbols.Add(familySymbol);
            }

            return familySymbols;
        }
        private void SetDetailItemParameter(ColumnsModel columnsModel, UnitProject unit)
        {
            Detail.LookupParameter("Diameter").Set(unit.Convert(Diameter));
            Detail.LookupParameter("Element Host").Set(columnsModel.SettingModel.ColumnsName);
            Detail.LookupParameter("Number Bar").Set(NoBar);
            Detail.LookupParameter("Rebar Number").Set(RebarNumber);
            switch (Type)
            {
                case DetailItemStyle.DT00:
                    break;
                case DetailItemStyle.DT00A:
                    break;
                case DetailItemStyle.DT01:
                    Detail.LookupParameter("L").Set(unit.Convert(L));
                    Detail.LookupParameter("Distance").Set(unit.Convert(Distance));
                    break;
                case DetailItemStyle.DT02:
                    break;
                case DetailItemStyle.DT03:
                    break;
                case DetailItemStyle.DT04:
                    break;
                case DetailItemStyle.DT04A:
                    Detail.LookupParameter("L").Set(unit.Convert(L));
                    Detail.LookupParameter("La").Set(unit.Convert(La));
                    Detail.LookupParameter("Lb").Set(unit.Convert(Lb));
                    Detail.LookupParameter("Distance").Set(unit.Convert(Distance));
                    break;
                case DetailItemStyle.DT04B:
                    Detail.LookupParameter("L").Set(unit.Convert(L));
                    Detail.LookupParameter("La").Set(unit.Convert(La));
                    Detail.LookupParameter("Distance").Set(unit.Convert(Distance));
                    break;
                case DetailItemStyle.DT05A:
                    Detail.LookupParameter("L").Set(unit.Convert(L));
                    Detail.LookupParameter("La").Set(unit.Convert(La));
                    Detail.LookupParameter("Lb").Set(unit.Convert(Lb));
                    Detail.LookupParameter("Distance").Set(unit.Convert(Distance));
                    break;
                case DetailItemStyle.DT06A:
                    Detail.LookupParameter("L").Set(unit.Convert(L));
                    Detail.LookupParameter("La").Set(unit.Convert(La));
                    Detail.LookupParameter("Lb").Set(unit.Convert(Lb));
                    Detail.LookupParameter("Distance").Set(unit.Convert(Distance));
                    break;
                case DetailItemStyle.DT07A:
                    Detail.LookupParameter("L").Set(unit.Convert(L));
                    Detail.LookupParameter("La").Set(unit.Convert(La));
                    Detail.LookupParameter("Lb").Set(unit.Convert(Lb));
                    Detail.LookupParameter("Distance").Set(unit.Convert(Distance));
                    break;
                default: break;
            }
        }
        #endregion
        #region Stirrup Detail
        public void CreateStirrupDetailItemX(Document document, ColumnsModel columnsModel, UnitProject unit)
        {
            GetDetailItem(document);
            if (DetailItemType != null)
            {
                Line line = GetLineStirrupDetailX(columnsModel, unit);
                Detail = document.Create.NewFamilyInstance(line, DetailItemType, columnsModel.DetailColumnView.DetailViewX);
                SetDetailItemParameter(columnsModel, unit);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Test null");
            }
        }
        public void CreateStirrupDetailItemY(Document document, ColumnsModel columnsModel, UnitProject unit)
        {
            GetDetailItem(document);
            if (DetailItemType != null)
            {
                Line line = GetLineStirrupDetailY(columnsModel, unit);
                Detail = document.Create.NewFamilyInstance(line, DetailItemType, columnsModel.DetailColumnView.DetailViewY);
                SetDetailItemParameter(columnsModel, unit);
            }
        }
        public void CreateStirrupDetailItemSection(Document document, ColumnsModel columnsModel, ViewSection viewSection, UnitProject unit,bool rotate)
        {
            GetDetailItem(document);
            if (DetailItemType != null)
            {
               
                XYZ point = TranformPointStirrupSection(columnsModel.SectionStyle, viewSection, columnsModel.InfoModels[0], columnsModel.PlanarFaces[0], unit);
                Detail = document.Create.NewFamilyInstance(point, DetailItemType, viewSection);
                SetDetailItemParameter(columnsModel, unit);
                if (rotate)
                {
                    Line line = GetAxis(document, columnsModel, point);
                    ElementTransformUtils.RotateElement(document, Detail.Id, line, -Math.PI / 2);
                }
            }
        }
        private Line GetAxis(Document document, ColumnsModel ColumnsModel, XYZ p0)
        {
            
            return Line.CreateBound(p0, p0+10*XYZ.BasisZ);
        }
        private XYZ TranformPointStirrupDetailX(SectionStyle sectionStyle,ViewSection viewSection, InfoModel infoModel0,PlanarFace planarFace0, UnitProject unit)
        {
            if (sectionStyle == SectionStyle.RECTANGLE)
            {
                XYZ p1 = PointModel.ProjectToPlane(viewSection.Origin, planarFace0);
                XYZ p2 = PointModel.ProjectToPlane(p1, infoModel0.West);
                XYZ p3 = p2 + unit.Convert(infoModel0.b / 2) * infoModel0.East.FaceNormal;
                return  p3 ;
            }
            else
            {
                XYZ p1 = PointModel.ProjectToPlane(infoModel0.PointPosition, planarFace0);
                XYZ p2 = new XYZ(p1.X, viewSection.Origin.Y, p1.Z);
                return p2 ;
            }
        }
        private XYZ TranformPointStirrupDetailY(SectionStyle sectionStyle, ViewSection viewSection, InfoModel infoModel0, PlanarFace planarFace0, UnitProject unit)
        {
            if (sectionStyle == SectionStyle.RECTANGLE)
            {
                XYZ p1 = PointModel.ProjectToPlane(viewSection.Origin, planarFace0);
                XYZ p2 = PointModel.ProjectToPlane(p1, infoModel0.South);
                XYZ p3 = p2 + unit.Convert(infoModel0.h / 2) * infoModel0.Nouth.FaceNormal;
                return p3 ;
            }
            else
            {
                XYZ p1 = PointModel.ProjectToPlane(infoModel0.PointPosition, planarFace0);
                XYZ p2 = new XYZ(viewSection.Origin.X, p1.Y, p1.Z);
                return p2 ;
            }
        }
        private XYZ TranformPointStirrupSection(SectionStyle sectionStyle, ViewSection viewSection, InfoModel infoModel0, PlanarFace planarFace0, UnitProject unit)
        {
            if (sectionStyle == SectionStyle.RECTANGLE)
            {
                XYZ p1 = PointModel.ProjectToPlane(viewSection.Origin, planarFace0);
                XYZ p2 = PointModel.ProjectToPlane(p1, infoModel0.South);
                XYZ p3 = PointModel.ProjectToPlane(p2, infoModel0.West);
                XYZ p4 = p3 + unit.Convert(Location.Y) * infoModel0.Nouth.FaceNormal;
                XYZ p5 = p4 + unit.Convert(Location.X) * infoModel0.East.FaceNormal;
                XYZ p6 = p5 + unit.Convert(Location.Z) * XYZ.BasisZ;
                return p6;
            }
            else
            {
                XYZ p0a = infoModel0.PointPosition;
                XYZ p0 = PointModel.ProjectToPlane(p0a, planarFace0);
               
                XYZ p1 = p0 + unit.Convert(Location.X) * XYZ.BasisX;
                XYZ p2 = p1 + unit.Convert(Location.Y) * XYZ.BasisY;
                XYZ p3 = p2 + unit.Convert(Location.Z) * XYZ.BasisZ;
                return p3;
            }
        }
        private Line GetLineStirrupDetailX(ColumnsModel columnsModel, UnitProject unit)
        {
            XYZ p0 = TranformPointStirrupDetailX( columnsModel.SectionStyle,columnsModel.DetailColumnView.DetailViewX,columnsModel.InfoModels[0],columnsModel.PlanarFaces[0],unit);
            XYZ p1 = p0 + unit.Convert(AllLocation[0].Z) * XYZ.BasisZ;
            XYZ p1a = p1 + unit.Convert(AllLocation[0].X)*(-1) * columnsModel.DetailColumnView.DetailViewX.RightDirection;
            XYZ p2 = p0 + unit.Convert(AllLocation[1].Z) * XYZ.BasisZ;
            XYZ p2a = p2 + unit.Convert(AllLocation[1].X) * (-1) * columnsModel.DetailColumnView.DetailViewX.RightDirection;

            return Line.CreateBound(p1a, p2a);
        }
        private Line GetLineStirrupDetailY(ColumnsModel columnsModel, UnitProject unit)
        {
            XYZ p0 = TranformPointStirrupDetailY(columnsModel.SectionStyle, columnsModel.DetailColumnView.DetailViewY, columnsModel.InfoModels[0], columnsModel.PlanarFaces[0], unit);
            XYZ p1 = p0 + unit.Convert(AllLocation[0].Z) * XYZ.BasisZ;
            XYZ p1a = p1 + unit.Convert(AllLocation[0].Y) * (-1) * columnsModel.DetailColumnView.DetailViewY.RightDirection;
            XYZ p2 = p0 + unit.Convert(AllLocation[1].Z) * XYZ.BasisZ;
            XYZ p2a = p2 + unit.Convert(AllLocation[1].Y) * (-1) * columnsModel.DetailColumnView.DetailViewY.RightDirection;

            return Line.CreateBound(p1a, p2a);
        }
        #endregion
        #region Tag

        #endregion
    }
    #region
    public enum DetailItemStyle
    {
        DT00,DT00A,DT01,DT02,DT03,DT04,DT04A,DT04B,DT05,DT05A,DT06,DT06A,DT07,DT07A
    }
    #endregion
}
