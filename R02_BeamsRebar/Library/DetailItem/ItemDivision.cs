using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Controls;
using R02_BeamsRebar.View;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media;
using WpfCustomControls;
namespace R02_BeamsRebar
{
    public class ItemDivision : BaseViewModel
    {
        #region property
        private string _Name;
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }
        private int _NoBar;
        public int NoBar { get => _NoBar; set { _NoBar = value; OnPropertyChanged(); } }
        private double _Diameter;
        public double Diameter { get => _Diameter; set { _Diameter = value; OnPropertyChanged(); } }
        private double _L;
        public double L { get => _L; set { _L = value; OnPropertyChanged(); } }
        private double _La;
        public double La { get => _La; set { _La = value; OnPropertyChanged(); } }
        private double _Lb;
        public double Lb { get => _Lb; set { _Lb = value; OnPropertyChanged(); } }
        private double _Length;
        public double Length { get => _Length; set { _Length = value; OnPropertyChanged(); } }
        private DetailShopStyle _Type;
        public DetailShopStyle Type { get => _Type; set { _Type = value; OnPropertyChanged(); } }
        private double _SlopeX;
        public double SlopeX { get => _SlopeX; set { _SlopeX = value; OnPropertyChanged(); } }
        private double _SlopeY;
        public double SlopeY { get => _SlopeY; set { _SlopeY = value; OnPropertyChanged(); } }
        private LocationBarModel _Location;
        public LocationBarModel Location { get => _Location; set { _Location = value; OnPropertyChanged(); } }
        private List<LocationBarModel> _AllLocation;
        public List<LocationBarModel> AllLocation { get => _AllLocation; set { _AllLocation = value; OnPropertyChanged(); } }
        private double _L1;
        public double L1 { get => _L1; set { _L1 = value; OnPropertyChanged(); } }
        private FamilySymbol _DetailShopType;
        public FamilySymbol DetailShopType { get => _DetailShopType; set { _DetailShopType = value; OnPropertyChanged(); } }
        private FamilyInstance _DetailShop;
        public FamilyInstance DetailShop { get => _DetailShop; set { _DetailShop = value; OnPropertyChanged(); } }
        #endregion
        public ItemDivision(string name, int number, double diameter, double l, double la, double lb)
        {
            Name = name; NoBar = number; Diameter = Math.Round(diameter, 3); L = Math.Round(l, 3); La = Math.Round(la, 3); Lb = Math.Round(lb, 3); Length = L + La + Lb; AllLocation = new List<LocationBarModel>();
        }
        #region PROPERTY
        public void SetTypeDown(double l1, double l2)
        {
            if (PointModel.AreEqual(l1, 0))
            {
                if (PointModel.AreEqual(l2, 0))
                {
                    Type = DetailShopStyle.DS00;
                }
                else
                {
                    Type = DetailShopStyle.DS02;
                }
            }
            else
            {
                if (PointModel.AreEqual(l2, 0))
                {
                    Type = DetailShopStyle.DS01;
                }
                else
                {
                    Type = DetailShopStyle.DS03;
                }
            }
        }
        public void SetTypeUp(double l1, double l2)
        {
            if (PointModel.AreEqual(l1, 0))
            {
                if (PointModel.AreEqual(l2, 0))
                {
                    Type = DetailShopStyle.DS00;
                }
                else
                {
                    Type = DetailShopStyle.DS05;
                }
            }
            else
            {
                if (PointModel.AreEqual(l2, 0))
                {
                    Type = DetailShopStyle.DS04;
                }
                else
                {
                    Type = DetailShopStyle.DS06;
                }
            }
        }
        public void GetAllLocation(double y)
        {
            switch (Type)
            {
                case DetailShopStyle.DS00:
                    GetAllLocationDS00();
                    break;
                case DetailShopStyle.DS01:
                    GetAllLocationDS01();
                    break;
                case DetailShopStyle.DS02:
                    GetAllLocationDS02();
                    break;
                case DetailShopStyle.DS03:
                    GetAllLocationDS03();
                    break;
                case DetailShopStyle.DS04:
                    GetAllLocationDS04();
                    break;
                case DetailShopStyle.DS05:
                    GetAllLocationDS05();
                    break;
                case DetailShopStyle.DS06:
                    GetAllLocationDS06();
                    break;
                case DetailShopStyle.DS07:
                    GetAllLocationDS078(y);
                    break;
                case DetailShopStyle.DS08:
                    GetAllLocationDS078(y);
                    break;
                case DetailShopStyle.DS09:
                    break;
                default: break;
            }

        }
        private void GetSlope(double y)
        {

            if (Type == DetailShopStyle.DS07 || Type == DetailShopStyle.DS08)
            {
                double slope = Math.Asin((y - Location.Y) / L);
                SlopeY = y - Location.Y;
                SlopeX = L * Math.Cos(slope);
            }
            else
            {
                if (Type == DetailShopStyle.DS09)
                {
                    SlopeX = y;
                    SlopeY = y;
                }
                SlopeX = 0;
                SlopeY = 0;
            }
        }
        private void GetAllLocationDS00()
        {
            AllLocation.Add(Location);
            AllLocation.Add(new LocationBarModel(Location.X + L, Location.Y));
        }
        private void GetAllLocationDS01()
        {
            AllLocation.Add(new LocationBarModel(Location.X, Location.Y + La));
            AllLocation.Add(Location);
            AllLocation.Add(new LocationBarModel(Location.X + L, Location.Y));
        }
        private void GetAllLocationDS02()
        {
            AllLocation.Add(Location);
            AllLocation.Add(new LocationBarModel(Location.X + L, Location.Y));
            AllLocation.Add(new LocationBarModel(Location.X + L, Location.Y + Lb));
        }
        private void GetAllLocationDS03()
        {
            AllLocation.Add(new LocationBarModel(Location.X, Location.Y + La));
            AllLocation.Add(Location);
            AllLocation.Add(new LocationBarModel(Location.X + L, Location.Y));
            AllLocation.Add(new LocationBarModel(Location.X + L, Location.Y + Lb));
        }
        private void GetAllLocationDS04()
        {
            AllLocation.Add(new LocationBarModel(Location.X, Location.Y - La));
            AllLocation.Add(Location);
            AllLocation.Add(new LocationBarModel(Location.X + L, Location.Y));
        }
        private void GetAllLocationDS05()
        {
            AllLocation.Add(Location);
            AllLocation.Add(new LocationBarModel(Location.X + L, Location.Y));
            AllLocation.Add(new LocationBarModel(Location.X + L, Location.Y - Lb));
        }
        private void GetAllLocationDS06()
        {
            AllLocation.Add(new LocationBarModel(Location.X, Location.Y - La));
            AllLocation.Add(Location);
            AllLocation.Add(new LocationBarModel(Location.X + L, Location.Y));
            AllLocation.Add(new LocationBarModel(Location.X + L, Location.Y - Lb));
        }
        private void GetAllLocationDS078(double y)
        {
            GetSlope(y);
            AllLocation.Add(Location);
            AllLocation.Add(new LocationBarModel(Location.X + La, Location.Y));
            AllLocation.Add(new LocationBarModel(Location.X + La + SlopeX, Location.Y + SlopeY));
            AllLocation.Add(new LocationBarModel(Location.X + La + SlopeX + Lb, Location.Y + SlopeY));
        }
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
                DetailShopType = GetAllFamilySymbol(family).Where(x => x.Name.Equals(Type.ToString())).FirstOrDefault();
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
        private void SetDetailItemParameter(BeamsModel BeamsModel, UnitProject unit, int i)
        {
            DetailShop.LookupParameter("Diameter").Set(unit.Convert(Diameter));
            DetailShop.LookupParameter("Number Bar").Set(NoBar);
            DetailShop.LookupParameter("Element Host").Set(BeamsModel.SettingModel.BeamsName);
            DetailShop.LookupParameter("Rebar Number").Set(i);
            switch (Type)
            {
                case DetailShopStyle.DS00:
                    DetailShop.LookupParameter("L").Set(unit.Convert(L));
                    break;
                case DetailShopStyle.DS01:
                    DetailShop.LookupParameter("L").Set(unit.Convert(L));
                    DetailShop.LookupParameter("La").Set(unit.Convert(La));
                    break;
                case DetailShopStyle.DS02:
                    DetailShop.LookupParameter("L").Set(unit.Convert(L));
                    DetailShop.LookupParameter("Lb").Set(unit.Convert(Lb));
                    break;
                case DetailShopStyle.DS03:
                    DetailShop.LookupParameter("L").Set(unit.Convert(L));
                    DetailShop.LookupParameter("La").Set(unit.Convert(La));
                    DetailShop.LookupParameter("Lb").Set(unit.Convert(Lb));
                    break;
                case DetailShopStyle.DS04:
                    DetailShop.LookupParameter("L").Set(unit.Convert(L));
                    DetailShop.LookupParameter("La").Set(unit.Convert(La));
                    break;
                case DetailShopStyle.DS05:
                    DetailShop.LookupParameter("L").Set(unit.Convert(L));
                    DetailShop.LookupParameter("Lb").Set(unit.Convert(Lb));
                    break;
                case DetailShopStyle.DS06:
                    DetailShop.LookupParameter("L").Set(unit.Convert(L));
                    DetailShop.LookupParameter("La").Set(unit.Convert(La));
                    DetailShop.LookupParameter("Lb").Set(unit.Convert(Lb));

                    break;
                case DetailShopStyle.DS07:
                    double slopeX7 = Math.Abs(AllLocation[2].X - AllLocation[1].X);
                    double slopeY7 = Math.Abs(AllLocation[2].Y - AllLocation[1].Y);
                    DetailShop.LookupParameter("La").Set(unit.Convert(La));
                    DetailShop.LookupParameter("Lb").Set(unit.Convert(Lb));
                    DetailShop.LookupParameter("SlopeX").Set(unit.Convert(slopeX7));
                    DetailShop.LookupParameter("SlopeY").Set(unit.Convert(slopeY7));
                    break;
                case DetailShopStyle.DS08:
                    double slopeX8 = Math.Abs(AllLocation[2].X - AllLocation[1].X);
                    double slopeY8 = Math.Abs(AllLocation[2].Y - AllLocation[1].Y);
                    DetailShop.LookupParameter("La").Set(unit.Convert(La));
                    DetailShop.LookupParameter("Lb").Set(unit.Convert(Lb));
                    DetailShop.LookupParameter("SlopeX").Set(unit.Convert(slopeX8));
                    DetailShop.LookupParameter("SlopeY").Set(unit.Convert(slopeY8));
                    break;
                case DetailShopStyle.DS09:
                    double L1 = Math.Abs(AllLocation[1].X - AllLocation[2].X);
                    DetailShop.LookupParameter("L").Set(unit.Convert(L));
                    DetailShop.LookupParameter("La").Set(unit.Convert(La));
                    DetailShop.LookupParameter("Lb").Set(unit.Convert(Lb));
                    DetailShop.LookupParameter("L1").Set(unit.Convert(L1));
                    break;
                case DetailShopStyle.DS10:
                    DetailShop.LookupParameter("L").Set(unit.Convert(L));
                    DetailShop.LookupParameter("La").Set(unit.Convert(La));
                    DetailShop.LookupParameter("Lb").Set(unit.Convert(Lb));
                    break;
                case DetailShopStyle.DS11:
                    DetailShop.LookupParameter("L").Set(unit.Convert(L));
                    DetailShop.LookupParameter("La").Set(unit.Convert(La));
                    DetailShop.LookupParameter("Lb").Set(unit.Convert(Lb));
                    break;
                case DetailShopStyle.DS12:
                    DetailShop.LookupParameter("L").Set(unit.Convert(L));
                    DetailShop.LookupParameter("La").Set(unit.Convert(La));
                    DetailShop.LookupParameter("Lb").Set(unit.Convert(Lb));
                    break;
                case DetailShopStyle.DS13:
                    DetailShop.LookupParameter("L").Set(unit.Convert(L));
                    DetailShop.LookupParameter("La").Set(unit.Convert(La));
                    DetailShop.LookupParameter("Lb").Set(unit.Convert(Lb));
                    break;
                default: break;
            }
        }
        public void CreateDetailItem(Document document, BeamsModel BeamsModel, UnitProject unit, double y0, int i)
        {
            GetDetailItem(document);
            if (DetailShopType != null)
            {
                XYZ point = TranformPoint(document, BeamsModel, unit,y0);
                DetailShop = document.Create.NewFamilyInstance(point, DetailShopType, BeamsModel.DetailShopView.DetailShop);
                SetDetailItemParameter(BeamsModel, unit, i);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Test");
            }
        }
        private XYZ TranformPoint(Document document, BeamsModel BeamsModel, UnitProject unit,double y0)
        {
            XYZ p1 = PointModel.ProjectToPlane(BeamsModel.InfoModels[0].LeftRightPlanar[0].Origin, BeamsModel.InfoModels[0].TopBottomPlanar[0]);
            XYZ p2 = PointModel.ProjectToPlane(p1, BeamsModel.PlanarFaces[0]);
            double zoffset = unit.Convert(BeamsModel.InfoModels[0].zOffset);
            XYZ p3 = p2 + zoffset *(-1)* XYZ.BasisZ;
            //XYZ p3 = p2;
            double x = unit.Convert(Location.X);
            double y = unit.Convert(Location.Y+y0);
            XYZ p4 = p3 + x * (-1) * BeamsModel.PlanarFaces[0].FaceNormal;
            XYZ p5 = p4 + y * (-1) * XYZ.BasisZ;
            return p5;
        }
        #endregion
        #region create image
        public void CreateImage(BarsDivisionView p, Document document,SettingModel settingModel, string folder, int i)
        {
            string path = folder + @"\" + settingModel.BeamsName + "_" + i + ".png";
            DrawImageRebar.DrawRebar(p.CanvasRebarImage,  this);
            DrawImageRebar.ConvertCanvasToBitmap(p.CanvasRebarImage, path);
            
            ImageTypeOptions a = new ImageTypeOptions(path, false, ImageTypeSource.Import);
            a.Resolution = 144;
            ImageType b = ImageType.Create(document,a);
           
            DetailShop.LookupParameter("Image").Set(b.Id);

        }
        //private Canvas GetCanvas(BarsDivisionView p)
        //{
        //    Canvas canvas = null;
        //    switch (Type)
        //    {
        //        case DetailShopStyle.DS00:canvas = p.DS00; break;
        //        case DetailShopStyle.DS01:canvas = p.DS01; break;
        //        case DetailShopStyle.DS02:canvas = p.DS02; break;
        //        case DetailShopStyle.DS03:canvas = p.DS03; break;
        //        case DetailShopStyle.DS04:canvas = p.DS04; break;
        //        case DetailShopStyle.DS05:canvas = p.DS05; break;
        //        case DetailShopStyle.DS06:canvas = p.DS06; break;
        //        case DetailShopStyle.DS07:canvas = p.DS07; break;
        //        case DetailShopStyle.DS08:canvas = p.DS08; break;
        //        case DetailShopStyle.DS09:canvas = p.DS09; break;
        //        case DetailShopStyle.DS10:canvas = p.DS10; break;
        //        case DetailShopStyle.DS11:canvas = p.DS11; break;
        //        case DetailShopStyle.DS12:canvas = p.DS12; break;
        //        case DetailShopStyle.DS13:canvas = p.DS13; break;
        //        default: break;
        //    }
        //    return canvas;
        //}
        #endregion
    }
    public enum DetailShopStyle
    {
        DS00, DS01, DS02, DS03, DS03A, DS04, DS05, DS06, DS06A, DS07, DS07A, DS07B, DS08, DS08A, DS09, DS10, DS10A, DS11, DS11A, DS12, DS12A, DS13, DS13A
    }
}