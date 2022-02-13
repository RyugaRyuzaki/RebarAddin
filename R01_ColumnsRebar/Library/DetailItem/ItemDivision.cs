using Autodesk.Revit.DB;
using R01_ColumnsRebar.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using WpfCustomControls;
using DSP;
namespace R01_ColumnsRebar
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
        private ObservableCollection<LocationBarModel> _AllLocation;
        public ObservableCollection<LocationBarModel> AllLocation { get => _AllLocation; set { _AllLocation = value; OnPropertyChanged(); } }
        private double _L1;
        public double L1 { get => _L1; set { _L1 = value; OnPropertyChanged(); } }
        private FamilySymbol _DetailShopType;
        public FamilySymbol DetailShopType { get => _DetailShopType; set { _DetailShopType = value; OnPropertyChanged(); } }
        private FamilyInstance _DetailShop;
        public FamilyInstance DetailShop { get => _DetailShop; set { _DetailShop = value; OnPropertyChanged(); } }
        #endregion
        public ItemDivision(string name, int number, double diameter, double l, double la, double lb)
        {
            Name = name; NoBar = number; Diameter = Math.Round(diameter, 3); L = Math.Round(l, 3); La = Math.Round(la, 3); Lb = Math.Round(lb, 3); Length = L + La + Lb; AllLocation = new ObservableCollection<LocationBarModel>(); L1 = 0;
        }
        #region PROPERTY
       
        public void GetAllLocation()
        {
            if (AllLocation.Count!=0)
            {
                AllLocation.Clear();
            }
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
                case DetailShopStyle.DS03A:
                    GetAllLocationDS03A();
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
                case DetailShopStyle.DS06A:
                    GetAllLocationDS06A();
                    break;
                case DetailShopStyle.DS07:
                    GetAllLocationDS07();
                    break;
                case DetailShopStyle.DS07A:
                    GetAllLocationDS07A();
                    break;
                case DetailShopStyle.DS07B:
                    GetAllLocationDS07B();
                    break;
                case DetailShopStyle.DS08:
                    GetAllLocationDS08();
                    break;
                case DetailShopStyle.DS09:
                    break;
                default: break;
            }

        }
       
        private void GetAllLocationDS00()
        {
            AllLocation.Add(Location);
            AllLocation.Add(new LocationBarModel(Location.X, Location.Y, Location.Z + L));
        }
        private void GetAllLocationDS01()
        {
            AllLocation.Add(new LocationBarModel(Location.X + La, Location.Y , Location.Z));
            AllLocation.Add(Location);
            AllLocation.Add(new LocationBarModel(Location.X, Location.Y, Location.Z + L));
        }
        private void GetAllLocationDS02()
        {
            AllLocation.Add(Location);
            AllLocation.Add(new LocationBarModel(Location.X, Location.Y, Location.Z + L));
            AllLocation.Add(new LocationBarModel(Location.X + Lb, Location.Y , Location.Z + L));
        }
        private void GetAllLocationDS03()
        {
            AllLocation.Add(new LocationBarModel(Location.X + La, Location.Y , Location.Z));
            AllLocation.Add(Location);
            AllLocation.Add(new LocationBarModel(Location.X, Location.Y, Location.Z + L));
            AllLocation.Add(new LocationBarModel(Location.X + Lb, Location.Y , Location.Z + L));
        }
        private void GetAllLocationDS03A()
        {
            AllLocation.Add(new LocationBarModel(Location.X + La, Location.Y , Location.Z));
            AllLocation.Add(Location);
            AllLocation.Add(new LocationBarModel(Location.X, Location.Y, Location.Z + L));
            AllLocation.Add(new LocationBarModel(Location.X - Lb, Location.Y , Location.Z + L));
        }
        private void GetAllLocationDS04()
        {
            AllLocation.Add(new LocationBarModel(Location.X - La, Location.Y , Location.Z));
            AllLocation.Add(Location);
            AllLocation.Add(new LocationBarModel(Location.X, Location.Y, Location.Z + L));
        }
        private void GetAllLocationDS05()
        {
            AllLocation.Add(Location);
            AllLocation.Add(new LocationBarModel(Location.X, Location.Y, Location.Z + L));
            AllLocation.Add(new LocationBarModel(Location.X - Lb, Location.Y , Location.Z + L));
        }
        private void GetAllLocationDS06()
        {
            AllLocation.Add(new LocationBarModel(Location.X - La, Location.Y , Location.Z));
            AllLocation.Add(Location);
            AllLocation.Add(new LocationBarModel(Location.X, Location.Y, Location.Z + L));
            AllLocation.Add(new LocationBarModel(Location.X - Lb, Location.Y , Location.Z + L));
        }
        private void GetAllLocationDS06A()
        {
            AllLocation.Add(new LocationBarModel(Location.X - La, Location.Y , Location.Z));
            AllLocation.Add(Location);
            AllLocation.Add(new LocationBarModel(Location.X, Location.Y, Location.Z + L));
            AllLocation.Add(new LocationBarModel(Location.X + Lb, Location.Y , Location.Z + L));
        }
        private void GetAllLocationDS07()
        {
            AllLocation.Add(Location);
            AllLocation.Add(new LocationBarModel(Location.X, Location.Y, Location.Z + La));
            AllLocation.Add(new LocationBarModel(Location.X + SlopeY, Location.Y , Location.Z + La + SlopeX));
            AllLocation.Add(new LocationBarModel(Location.X + SlopeY, Location.Y , Location.Z + La + SlopeX + Lb));
        }
        private void GetAllLocationDS07A()
        {
            AllLocation.Add(new LocationBarModel(Location.X-L1, Location.Y, Location.Z ));
            AllLocation.Add(Location);
            AllLocation.Add(new LocationBarModel(Location.X, Location.Y, Location.Z + La));
            AllLocation.Add(new LocationBarModel(Location.X + SlopeY, Location.Y , Location.Z + La + SlopeX));
            AllLocation.Add(new LocationBarModel(Location.X + SlopeY, Location.Y , Location.Z + La + SlopeX + Lb));
        }
        private void GetAllLocationDS07B()
        {
            AllLocation.Add(new LocationBarModel(Location.X + L1, Location.Y , Location.Z));
            AllLocation.Add(Location);
            AllLocation.Add(new LocationBarModel(Location.X, Location.Y, Location.Z + La));
            AllLocation.Add(new LocationBarModel(Location.X + SlopeY, Location.Y , Location.Z + La + SlopeX));
            AllLocation.Add(new LocationBarModel(Location.X + SlopeY, Location.Y , Location.Z + La + SlopeX + Lb));
        }
        private void GetAllLocationDS08()
        {
            AllLocation.Add(Location);
            AllLocation.Add(new LocationBarModel(Location.X, Location.Y, Location.Z + La));
            AllLocation.Add(new LocationBarModel(Location.X - SlopeY, Location.Y , Location.Z + La + SlopeX));
            AllLocation.Add(new LocationBarModel(Location.X - SlopeY, Location.Y , Location.Z + La + SlopeX + Lb));
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
                DetailShopType = GetAllFamilySymbol(family).Where(x => x.Name.Equals(Type.ToString())).FirstOrDefault();
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Test");
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
        private void SetDetailItemParameter(ColumnsModel ColumnsModel, UnitProject unit, int i)
        {
            DetailShop.LookupParameter("Diameter").Set(unit.Convert(Diameter));
            DetailShop.LookupParameter("Number Bar").Set(NoBar);
            DetailShop.LookupParameter("Element Host").Set(ColumnsModel.SettingModel.ColumnsName);
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
                case DetailShopStyle.DS03A:
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
                case DetailShopStyle.DS06A:
                    DetailShop.LookupParameter("L").Set(unit.Convert(L));
                    DetailShop.LookupParameter("La").Set(unit.Convert(La));
                    DetailShop.LookupParameter("Lb").Set(unit.Convert(Lb));
                    break;
                case DetailShopStyle.DS07:
                    
                    DetailShop.LookupParameter("La").Set(unit.Convert(La));
                    DetailShop.LookupParameter("Lb").Set(unit.Convert(Lb));
                    DetailShop.LookupParameter("SlopeX").Set(unit.Convert(SlopeX));
                    DetailShop.LookupParameter("SlopeY").Set(unit.Convert(SlopeY));
                    break;
                case DetailShopStyle.DS07A:
                    
                    DetailShop.LookupParameter("La").Set(unit.Convert(La));
                    DetailShop.LookupParameter("Lb").Set(unit.Convert(Lb));
                    DetailShop.LookupParameter("SlopeX").Set(unit.Convert(SlopeX));
                    DetailShop.LookupParameter("SlopeY").Set(unit.Convert(SlopeY));
                    break;
                case DetailShopStyle.DS07B:
                    
                    DetailShop.LookupParameter("La").Set(unit.Convert(La));
                    DetailShop.LookupParameter("Lb").Set(unit.Convert(Lb));
                    DetailShop.LookupParameter("L1").Set(unit.Convert(L1));
                    DetailShop.LookupParameter("SlopeX").Set(unit.Convert(SlopeX));
                    DetailShop.LookupParameter("SlopeY").Set(unit.Convert(SlopeY));
                    break;
                case DetailShopStyle.DS08:
                    
                    DetailShop.LookupParameter("La").Set(unit.Convert(La));
                    DetailShop.LookupParameter("Lb").Set(unit.Convert(Lb));
                    DetailShop.LookupParameter("SlopeX").Set(unit.Convert(SlopeX));
                    DetailShop.LookupParameter("SlopeY").Set(unit.Convert(SlopeY));
                    break;
                case DetailShopStyle.DS08A:
                    
                    DetailShop.LookupParameter("La").Set(unit.Convert(La));
                    DetailShop.LookupParameter("Lb").Set(unit.Convert(Lb));
                    DetailShop.LookupParameter("SlopeX").Set(unit.Convert(SlopeX));
                    DetailShop.LookupParameter("SlopeY").Set(unit.Convert(SlopeY));
                    break;
                case DetailShopStyle.DS09:
                    
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
                case DetailShopStyle.DS10A:
                    DetailShop.LookupParameter("L").Set(unit.Convert(L));
                    DetailShop.LookupParameter("La").Set(unit.Convert(La));
                    break;
                case DetailShopStyle.DS11:
                    DetailShop.LookupParameter("L").Set(unit.Convert(L));
                    DetailShop.LookupParameter("La").Set(unit.Convert(La));
                    DetailShop.LookupParameter("Lb").Set(unit.Convert(Lb));
                    break;
                case DetailShopStyle.DS11A:
                    DetailShop.LookupParameter("L").Set(unit.Convert(L));
                    DetailShop.LookupParameter("La").Set(unit.Convert(La));
                    DetailShop.LookupParameter("Lb").Set(unit.Convert(Lb));
                    break;
                case DetailShopStyle.DS12:
                    DetailShop.LookupParameter("L").Set(unit.Convert(L));
                    DetailShop.LookupParameter("La").Set(unit.Convert(La));
                    DetailShop.LookupParameter("Lb").Set(unit.Convert(Lb));
                    break;
                case DetailShopStyle.DS12A:
                    DetailShop.LookupParameter("L").Set(unit.Convert(L));
                    DetailShop.LookupParameter("La").Set(unit.Convert(La));
                    DetailShop.LookupParameter("Lb").Set(unit.Convert(Lb));
                    break;
                case DetailShopStyle.DS13:
                    DetailShop.LookupParameter("L").Set(unit.Convert(L));
                    DetailShop.LookupParameter("La").Set(unit.Convert(La));
                    DetailShop.LookupParameter("Lb").Set(unit.Convert(Lb));
                    break;
                case DetailShopStyle.DS13A:
                    DetailShop.LookupParameter("L").Set(unit.Convert(L));
                    DetailShop.LookupParameter("La").Set(unit.Convert(La));
                    DetailShop.LookupParameter("Lb").Set(unit.Convert(Lb));
                    break;
                default: break;
            }
        }
        private bool ConditionRotation()
        {
            return ((Type==DetailShopStyle.DS10)||(Type == DetailShopStyle.DS10A) ||(Type == DetailShopStyle.DS11) ||(Type == DetailShopStyle.DS12) || (Type == DetailShopStyle.DS13) | (Type == DetailShopStyle.DS11A) || (Type == DetailShopStyle.DS12A) || (Type == DetailShopStyle.DS13A));
        }
        public void CreateDetailItem(Document document, ColumnsModel ColumnsModel, UnitProject unit, double y0, int i)
        {
            GetDetailItem(document);
            
            if (DetailShopType != null)
            {
               
                XYZ point = TranformPoint(document, ColumnsModel, unit, y0);
                DetailShop = document.Create.NewFamilyInstance(point, DetailShopType, ColumnsModel.DetailShopView.DetailShop);
                SetDetailItemParameter(ColumnsModel, unit, i);
                
                if (!ConditionRotation())
                {
                    Line line = GetAxis(document, ColumnsModel, point);
                    
                    ElementTransformUtils.RotateElement(document, DetailShop.Id, line, Math.PI / 2);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Test1");
            }
        }
        private XYZ TranformPoint(Document document, ColumnsModel ColumnsModel, UnitProject unit, double x0)
        {
            if (ColumnsModel.SectionStyle==ErrorColumns.SectionStyle.RECTANGLE)
            {
                XYZ p1 = PointModel.ProjectToPlane(ColumnsModel.DetailShopView.DetailShop.Origin, ColumnsModel.PlanarFaces[0]);
                XYZ p2 = PointModel.ProjectToPlane(p1, ColumnsModel.InfoModels[0].West);
                XYZ p3 = p2 + unit.Convert(Location.Z) * XYZ.BasisZ;
                XYZ p4 = p3 + unit.Convert(x0) * ColumnsModel.DetailShopView.DetailShop.RightDirection;
                return p4;
            }
            else
            {
                XYZ p1 = PointModel.ProjectToPlane(ColumnsModel.InfoModels[0].PointPosition, ColumnsModel.PlanarFaces[0]);
                XYZ p2 = p1 + unit.Convert(Location.Z) * XYZ.BasisZ;
                XYZ p3 = p2 + unit.Convert(x0) * ColumnsModel.DetailShopView.DetailShop.RightDirection;
                return p3;
            }
            
        }
        private Line GetAxis(Document document, ColumnsModel ColumnsModel,XYZ p0)
        {
            XYZ a = null;
            if (ColumnsModel.SectionStyle == ErrorColumns.SectionStyle.RECTANGLE)
            {
                a = p0 - 10 * ColumnsModel.InfoModels[0].South.FaceNormal;
            }
            else
            {
                a = p0 + 10 * XYZ.BasisY;
            }
            return Line.CreateBound(p0, a);
        }
        #region create image
        public void CreateImage(ColumnsWindow p, Document document, SettingModel settingModel, string folder, int i)
        {
            string path = folder + @"\" + settingModel.ColumnsName + "_" + i + ".png";
            DrawImageRebar.DrawRebar(p.CanvasRebarImage, this);
            DrawImageRebar.ConvertCanvasToBitmap(p.CanvasRebarImage, path);
            ImageTypeOptions a = new ImageTypeOptions(path, false, ImageTypeSource.Import);
            a.Resolution = 144;
            ImageType b = ImageType.Create(document, a);
            DetailShop.LookupParameter("Image").Set(b.Id);

        }
        //private Canvas GetCanvas(BarsDivisionView p)
        //{
        //    Canvas canvas = null;
        //    switch (Type)
        //    {
        //        case DetailShopStyle.DS00: canvas = p.DS00; break;
        //        case DetailShopStyle.DS01: canvas = p.DS01; break;
        //        case DetailShopStyle.DS02: canvas = p.DS02; break;
        //        case DetailShopStyle.DS03: canvas = p.DS03; break;
        //        case DetailShopStyle.DS04: canvas = p.DS04; break;
        //        case DetailShopStyle.DS05: canvas = p.DS05; break;
        //        case DetailShopStyle.DS06: canvas = p.DS06; break;
        //        case DetailShopStyle.DS07: canvas = p.DS07; break;
        //        case DetailShopStyle.DS08: canvas = p.DS08; break;
        //        case DetailShopStyle.DS09: canvas = p.DS09; break;
        //        case DetailShopStyle.DS10: canvas = p.DS10; break;
        //        case DetailShopStyle.DS11: canvas = p.DS11; break;
        //        case DetailShopStyle.DS12: canvas = p.DS12; break;
        //        case DetailShopStyle.DS13: canvas = p.DS13; break;
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