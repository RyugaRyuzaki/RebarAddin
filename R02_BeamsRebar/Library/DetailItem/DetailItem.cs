using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCustomControls;
using DSP;
namespace R02_BeamsRebar
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
        public void GetPropertyStirrup(double overight,double distance,double length, double diameter,int number, double l)
        {
            Diameter = diameter;Distance = distance;L = l;NoBar = number;Length = length;
            
        }
        public void GetPropertyStirrupSection( double distance, double diameter, double l,double la, double lb)
        {
            Diameter = diameter; Distance = distance; L = l;  La = la;Lb = lb;

        }
        public void GetPropertyLong( double diameter, int number)
        {
            Diameter = diameter;  NoBar = number; 
        }
        #endregion
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
        private void SetDetailItemParameter(BeamsModel BeamsModel, UnitProject unit)
        {
            Detail.LookupParameter("Diameter").Set(unit.Convert(Diameter));
            Detail.LookupParameter("Number Bar").Set(NoBar);
            Detail.LookupParameter("Element Host").Set(BeamsModel.SettingModel.BeamsName);
            Detail.LookupParameter("Rebar Number").Set(RebarNumber);
            switch (Type)
            {
                case DetailItemStyle.DT00:
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
                case DetailItemStyle.DT05:
                    break;
                case DetailItemStyle.DT06:
                    break;
                case DetailItemStyle.DT07:
                    break;
                default: break;
            }
        }
        public void CreateStirrupDetailItem(Document document, BeamsModel BeamsModel, UnitProject unit)
        {
            GetDetailItem(document);
            if (DetailItemType != null)
            {
                Line line = GetLineStirrupDetail( BeamsModel, unit);
                Detail = document.Create.NewFamilyInstance(line, DetailItemType, BeamsModel.DetailBeamView.DetailView);
                SetDetailItemParameter(BeamsModel, unit);
            }
        }
        public void CreateStirrupSectionItem(Document document, BeamsModel BeamsModel,InfoModel infoModel, UnitProject unit,ViewSection viewSection, double tagH, double tagV)
        {
            GetDetailItem(document);
            
            if (DetailItemType != null)
            {
                XYZ point = TranformPointStirrupSection(viewSection, infoModel, BeamsModel.PlanarFaces[0], unit);
                Detail = document.Create.NewFamilyInstance(point, DetailItemType, viewSection);
                Detail.LookupParameter("Diameter").Set(unit.Convert(Diameter));
                Detail.LookupParameter("Element Host").Set(BeamsModel.SettingModel.BeamsName);
                Detail.LookupParameter("Rebar Number").Set(RebarNumber);
                Detail.LookupParameter("Distance").Set(unit.Convert(Distance));
                Detail.LookupParameter("TagH").Set(unit.Convert(tagH));
                Detail.LookupParameter("TagV").Set(unit.Convert(tagV));
                Detail.LookupParameter("L").Set(unit.Convert(L));
                Detail.LookupParameter("La").Set(unit.Convert(La));
                Detail.LookupParameter("Lb").Set(unit.Convert(Lb));
                Detail.LookupParameter("c").Set(unit.Convert(BeamsModel.Cover));
            }
        }
        public void CreateAntiStirrupSectionItem(Document document, BeamsModel BeamsModel, InfoModel infoModel, UnitProject unit, ViewSection viewSection)
        {
            GetDetailItem(document);

            if (DetailItemType != null)
            {
                XYZ point = TranformPointAntiStirrupSection(viewSection, infoModel, BeamsModel.PlanarFaces[0], unit);
                Detail = document.Create.NewFamilyInstance(point, DetailItemType, viewSection);
                Detail.LookupParameter("Diameter").Set(unit.Convert(Diameter));
                Detail.LookupParameter("Element Host").Set(BeamsModel.SettingModel.BeamsName);
                Detail.LookupParameter("Rebar Number").Set(RebarNumber);
                Detail.LookupParameter("Distance").Set(unit.Convert(Distance));
                Detail.LookupParameter("TagH").Set(unit.Convert(BeamsModel.SettingModel.TagH));
                Detail.LookupParameter("TagVDown").Set(unit.Convert(BeamsModel.SettingModel.TagV/2));
                Detail.LookupParameter("L").Set(unit.Convert(L));
                Detail.LookupParameter("La").Set(unit.Convert(La));
                Detail.LookupParameter("Lb").Set(unit.Convert(Lb));
            }
        }
        public void CreateLongDetailItem(Document document, BeamsModel BeamsModel, UnitProject unit)
        {
            GetDetailItem(document);
            if (DetailItemType != null)
            {
                Line line = GetLineLongDetail( BeamsModel, unit);
                Detail = document.Create.NewFamilyInstance(line, DetailItemType, BeamsModel.DetailBeamView.DetailView);
                SetDetailItemParameter(BeamsModel, unit);
            }
        }
        public void CreateLongSectionItem(Document document, BeamsModel BeamsModel,  UnitProject unit, ViewSection viewSection,bool middle)
        {
            GetDetailItem(document);
            if (DetailItemType != null)
            {
                if (NoBar != 1)
                {
                    Line line = GetLineLongSection(viewSection, BeamsModel.InfoModels[0], unit);
                    Detail = document.Create.NewFamilyInstance(line, DetailItemType, viewSection);
                    Detail.LookupParameter("Diameter").Set(unit.Convert(Diameter));
                    Detail.LookupParameter("Element Host").Set(BeamsModel.SettingModel.BeamsName);
                    Detail.LookupParameter("Number Bar").Set(NoBar);
                    Detail.LookupParameter("Rebar Number").Set(RebarNumber);
                    Detail.LookupParameter("TagH").Set(unit.Convert(BeamsModel.SettingModel.TagH));
                    if (Type == DetailItemStyle.DT02)
                    {
                        Detail.LookupParameter("TagVUp").Set(unit.Convert((middle) ? BeamsModel.SettingModel.tmin / 2 : BeamsModel.SettingModel.TagV));
                    }
                    if (Type == DetailItemStyle.DT03)
                    {
                        Detail.LookupParameter("TagVDown").Set(unit.Convert((middle) ? BeamsModel.SettingModel.tmin / 2 : BeamsModel.SettingModel.TagV));
                    }
                }
                else
                {
                    XYZ point = GetPointLongSection(viewSection, BeamsModel.InfoModels[0], unit);
                    Detail = document.Create.NewFamilyInstance(point, DetailItemType, viewSection);
                    Detail.LookupParameter("Diameter").Set(unit.Convert(Diameter));
                    Detail.LookupParameter("Element Host").Set(BeamsModel.SettingModel.BeamsName);
                    Detail.LookupParameter("Number Bar").Set(NoBar);
                    Detail.LookupParameter("Rebar Number").Set(RebarNumber);
                }
                
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Test");
            }
        }
        private XYZ TranformPoint( BeamsModel BeamsModel, UnitProject unit)
        {
            XYZ p1 = PointModel.ProjectToPlane(BeamsModel.DetailBeamView.DetailView.Origin, BeamsModel.InfoModels[0].TopBottomPlanar[0]);
            XYZ p2 = PointModel.ProjectToPlane(p1, BeamsModel.PlanarFaces[0]);
            double zoffset = unit.Convert(BeamsModel.InfoModels[0].zOffset);
            XYZ p3 = p2 + zoffset * (-1) * XYZ.BasisZ;
            //XYZ p3 = p2;
            double x = unit.Convert(Location.X);
            double y = unit.Convert(Location.Y );
            XYZ p4 = p3 + x * (-1) * BeamsModel.PlanarFaces[0].FaceNormal;
            XYZ p5 = p4 + y * (-1) * XYZ.BasisZ;
            return p5;
        }
        private XYZ TranformPointStirrupSection(ViewSection viewSection,InfoModel infoModel0,PlanarFace planarFace0, UnitProject unit)
        {
            double b2 = unit.Convert(AllLocation[0].X);
            //double zOffset = unit.Convert(Math.Abs(infoModel0.zOffset));
            double zOffset = 0;
            double x0 = unit.Convert(Location.X);
            XYZ p1 = PointModel.ProjectToPlane(infoModel0.LeftRightPlanar[0].Origin, infoModel0.TopBottomPlanar[0]);
            XYZ p2 = p1 + b2 * (-1) * infoModel0.LeftRightPlanar[0].FaceNormal;
            XYZ p3 = p2 + zOffset *(-1)* XYZ.BasisZ;
            XYZ p4 = PointModel.ProjectToPlane(p3, planarFace0);
            XYZ p5 = p4 + x0 *(-1)* planarFace0.FaceNormal;
            return p5;
        }
        private XYZ TranformPointAntiStirrupSection(ViewSection viewSection, InfoModel infoModel, PlanarFace planarFace0, UnitProject unit)
        {
            double b2 = unit.Convert(AllLocation[0].X);
            double zOffset = unit.Convert(Location.Y);
            double x0 = unit.Convert(Location.X);
            XYZ p1 = PointModel.ProjectToPlane(infoModel.LeftRightPlanar[0].Origin, infoModel.TopBottomPlanar[0]);
            XYZ p2 = p1 + b2 * (-1) * infoModel.LeftRightPlanar[0].FaceNormal;
            XYZ p3 = p2 + zOffset * (-1) * XYZ.BasisZ;
            XYZ p4 = PointModel.ProjectToPlane(p3, planarFace0);
            XYZ p5 = p4 + x0 * (-1) * planarFace0.FaceNormal;
            return p5;
        }
        private XYZ TranformPointLong(BeamsModel BeamsModel, UnitProject unit,double x0, double y0)
        {
            XYZ p1 = PointModel.ProjectToPlane(BeamsModel.DetailBeamView.DetailView.Origin, BeamsModel.InfoModels[0].TopBottomPlanar[0]);
            XYZ p2 = PointModel.ProjectToPlane(p1, BeamsModel.PlanarFaces[0]);
            double zoffset = unit.Convert(BeamsModel.InfoModels[0].zOffset);
            XYZ p3 = p2 + zoffset * (-1) * XYZ.BasisZ;
            //XYZ p3 = p2;
            double x = unit.Convert(x0);
            double y = unit.Convert(y0);
            XYZ p4 = p3 + x * (-1) * BeamsModel.PlanarFaces[0].FaceNormal;
            XYZ p5 = p4 + y * (-1) * XYZ.BasisZ;
            return p5;
        }
        private Line GetLineStirrupDetail( BeamsModel BeamsModel, UnitProject unit)
        {
            Line a = null;
            XYZ x0 = TranformPoint( BeamsModel, unit);
            double l = unit.Convert(Length);
            XYZ x1 = x0 + l * (-1) * BeamsModel.PlanarFaces[0].FaceNormal;
            a = Line.CreateBound(x0, x1);
            
            return a;
        }
        private Line GetLineLongDetail( BeamsModel BeamsModel, UnitProject unit)
        {
            XYZ x0 = TranformPointLong( BeamsModel, unit, AllLocation[0].X, AllLocation[0].Y);
            XYZ x1 = TranformPointLong( BeamsModel, unit, AllLocation[1].X, AllLocation[1].Y);
            return Line.CreateBound(x0, x1);
        }
        private Line GetLineLongSection(ViewSection viewSection,InfoModel infoModel0, UnitProject unit)
        {
            XYZ p1 = PointModel.ProjectToPlane(viewSection.Origin, infoModel0.TopBottomPlanar[0]);
            double zoffset = unit.Convert(Math.Abs(infoModel0.zOffset));
            //double zoffset = 0;
            XYZ p2 = p1 + zoffset * XYZ.BasisZ;
            XYZ p3= PointModel.ProjectToPlane(p2, infoModel0.LeftRightPlanar[0]);
            double x1 = unit.Convert(AllLocation[0].X);
            double x2 = unit.Convert(AllLocation[1].X- AllLocation[0].X);
            XYZ p4 = p3 + x1 * (-1) * infoModel0.LeftRightPlanar[0].FaceNormal;
            XYZ p5 = p4 + unit.Convert(Location.Y) * (-1) * XYZ.BasisZ;
            XYZ p6 = p5 + x2 * (-1) * infoModel0.LeftRightPlanar[0].FaceNormal;
            return Line.CreateBound(p5, p6);
        }
        private XYZ GetPointLongSection(ViewSection viewSection, InfoModel infoModel0, UnitProject unit)
        {
            XYZ p1 = PointModel.ProjectToPlane(viewSection.Origin, infoModel0.TopBottomPlanar[0]);
            double zoffset = unit.Convert(Math.Abs(infoModel0.zOffset));
            //double zoffset = 0;
            XYZ p2 = p1 + zoffset * XYZ.BasisZ;
            XYZ p3 = PointModel.ProjectToPlane(p2, infoModel0.LeftRightPlanar[0]);
            double x1 = unit.Convert(AllLocation[0].X);
            double x2 = unit.Convert(AllLocation[1].X - AllLocation[0].X);
            XYZ p4 = p3 + x1 * (-1) * infoModel0.LeftRightPlanar[0].FaceNormal;
            return (p4 + unit.Convert(Location.Y) * (-1) * XYZ.BasisZ);
        }
        #region Tag
        private XYZ GetXYZTagStirrupDetail(InfoModel infoModel, UnitProject unit, double L10, double h0)
        {
            XYZ p1 = PointModel.ProjectToPlane(infoModel.LeftRightPlanar[0].Origin, infoModel.TopBottomPlanar[0]);
            double h = unit.Convert(h0);
            double L1 = unit.Convert(L10);
            XYZ p3 = p1 + (infoModel.b/2)*(-1)*infoModel.LeftRightPlanar[0].FaceNormal;
            XYZ p4 = PointModel.ProjectToPlane(p3, infoModel.StartPlanar);
            XYZ p5 = null;
            if (infoModel.ConsolLeft)
            {
                p5 = p4 + L1 * (-1) * infoModel.StartPlanar.FaceNormal;
            }
            else
            {
                p5 = p4 + L1 * infoModel.StartPlanar.FaceNormal;
            }
            XYZ p6 = p5 + h * XYZ.BasisZ;
            return p6;
        }
        public void CreateTagStirrupDetail(Autodesk.Revit.DB.View view, InfoModel infoModel, Document document, UnitProject unit, SettingModel settingModel,  double L10, double h0)
        {
            XYZ origin = GetXYZTagStirrupDetail(infoModel, unit, L10, h0);
            
            Tag = IndependentTag.Create(document, view.Id, new Reference(Detail), false, Mode, Horizontal, origin);
            Tag.ChangeTypeId(settingModel.SelectedDetailDistanceTag.Id);
        }
        private XYZ GetXYZOriginTagLongDetail(UnitProject unit, InfoModel infoModel0, PlanarFace planarFace0)
        {
            XYZ p1 = PointModel.ProjectToPlane(infoModel0.LeftRightPlanar[0].Origin, infoModel0.TopBottomPlanar[0]);
            double b = unit.Convert(infoModel0.b);
            XYZ p2 = p1 + (b / 2) * (-1) * infoModel0.LeftRightPlanar[0].FaceNormal;
            XYZ p3 = PointModel.ProjectToPlane(p2, planarFace0);
            return p3;
        }
        public void CreateTagLongDetailTop(Autodesk.Revit.DB.View view, Document document, UnitProject unit, InfoModel infoModel0, SettingModel settingModel, PlanarFace planarFace0, double x0, double y0, double h0, double v0)
        {
            double x = unit.Convert(x0+ infoModel0.b/2);
            double y = unit.Convert(y0);
            double h = unit.Convert(h0);
            double v = unit.Convert(v0);
            double zOffset = unit.Convert(infoModel0.zOffset);
            XYZ p0 = GetXYZOriginTagLongDetail(unit, infoModel0, planarFace0);
            XYZ p1 = p0 + x * (-1) * planarFace0.FaceNormal;
            XYZ leaderEnd = p1 - (y + zOffset) * XYZ.BasisZ;
            XYZ LeaderElbow = p1 + v * XYZ.BasisZ;
            XYZ tagHead = LeaderElbow + h * (-1) * planarFace0.FaceNormal;
            Tag = IndependentTag.Create(document, view.Id, new Reference(Detail), true, Mode, Horizontal, tagHead);
            Tag.ChangeTypeId(settingModel.SelectedDetailItemTag.Id);
            Tag.LeaderEndCondition = LeaderEndCondition.Free;
            Tag.LeaderEnd = leaderEnd;
            Tag.LeaderElbow = LeaderElbow;
            Tag.TagHeadPosition = tagHead;

        }
        public void CreateTagLongDetailBottom(Autodesk.Revit.DB.View view, Document document, UnitProject unit, InfoModel infoModel0, SettingModel settingModel, PlanarFace planarFace0, double x0, double y0, double h0, double v0)
        {
            double x = unit.Convert(x0 + infoModel0.b / 2);
            double y = unit.Convert(y0);
            double h = unit.Convert(h0);
            double v = unit.Convert(v0);
            double zOffset = unit.Convert(infoModel0.zOffset);
            XYZ p0 = GetXYZOriginTagLongDetail(unit, infoModel0, planarFace0);
            XYZ p1 = p0 + x * (-1) * planarFace0.FaceNormal;
            XYZ leaderEnd = p1 - (y + zOffset) * XYZ.BasisZ;
            XYZ LeaderElbow = p1 + (y + v) * (-1) * XYZ.BasisZ;
            XYZ tagHead = LeaderElbow + h * (-1) * planarFace0.FaceNormal;
            Tag = IndependentTag.Create(document, view.Id, new Reference(Detail), true, Mode, Horizontal, tagHead);
            Tag.ChangeTypeId(settingModel.SelectedDetailItemTag.Id);
            Tag.LeaderEndCondition = LeaderEndCondition.Free;
            Tag.LeaderEnd = leaderEnd;
            Tag.LeaderElbow = LeaderElbow;
            Tag.TagHeadPosition = tagHead;
        }
        #endregion
    }
    #region
    public enum DetailItemStyle
    {
        DT00, DT00A, DT01, DT02, DT03, DT04, DT04A, DT04B, DT05, DT05A, DT06, DT06A, DT07, DT07A
    }
    #endregion
}
