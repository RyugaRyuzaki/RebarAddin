using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB.Structure;

namespace R11_FoundationPile
{
    public class PileModel :BaseViewModel
    {
        #region property
        private int _PileNumber;
        public int PileNumber { get => _PileNumber; set { _PileNumber = value; OnPropertyChanged(); } }
        public FamilyInstance Pile { get ; set ;  }
        private Level _Level;
        public Level Level { get => _Level; set { _Level = value; OnPropertyChanged(); } }
        private LocationModel _Location;
        public LocationModel Location { get => _Location; set { _Location = value; OnPropertyChanged(); } }
        private XYZ _Point;
        public XYZ Point { get => _Point; set { _Point = value; OnPropertyChanged(); } }
        private double _Overlap;
        public double Overlap { get => _Overlap; set { _Overlap = value; OnPropertyChanged(); } }
        private double _LengthPile;
        public double LengthPile { get => _LengthPile; set { _LengthPile = value; OnPropertyChanged(); } }
        private double _DistanceToFoundation;
        public double DistanceToFoundation { get => _DistanceToFoundation; set { _DistanceToFoundation = value; OnPropertyChanged(); } }

        private IndependentTag _TagPile;
        public IndependentTag TagPile { get => _TagPile; set { _TagPile = value; OnPropertyChanged(); } }
        private TextNote _TextTagPile;
        public TextNote TextTagPile { get => _TextTagPile; set { _TextTagPile = value; OnPropertyChanged(); } }
        public Dimension DimensionGridX { get; set; }
        public Dimension DimensionGridY { get; set; }
        private ReferenceArray _RefGridX;
        public ReferenceArray RefGridX { get { if (_RefGridX == null) { _RefGridX = new ReferenceArray(); } return _RefGridX; } set { _RefGridX = value; OnPropertyChanged(); } }
        private ReferenceArray _RefGridY;
        public ReferenceArray RefGridY { get { if (_RefGridY == null) { _RefGridY = new ReferenceArray(); } return _RefGridY; } set { _RefGridY = value; OnPropertyChanged(); } }
        public SpotDimension SpotDimensionPile { get; set; }
        private bool _IsTestingPile;
        public bool IsTestingPile { get => _IsTestingPile; set { _IsTestingPile = value; OnPropertyChanged(); } }
        #endregion
        public PileModel(int pileNumber,Level level,SettingModel settingModel)
        {
            PileNumber = pileNumber;
            Level = level;
            Overlap = settingModel.Overlap;
            LengthPile = settingModel.LengthPile;
            DistanceToFoundation = settingModel.HeightFoundation - Overlap;
        }
        #region Method
        public void GetLocation(double x, double y, double z)
        {
            Location = new LocationModel(x, y, z);
        }
        #endregion
        #region create Pile
        private XYZ GetPointRectangle(UnitProject unit,ColumnModel columnModel)
        {
            XYZ p1 = columnModel.PointPosition + unit.Convert(Location.X) * columnModel.East.FaceNormal; 
            XYZ p2 = p1 + unit.Convert(Location.Y) * columnModel.Nouth.FaceNormal; 
            return p2;
        }
        private XYZ GetPointCylindrical(UnitProject unit, ColumnModel columnModel)
        {
            XYZ p1 = columnModel.PointPosition + unit.Convert(Location.X) * XYZ.BasisX;
            XYZ p2 = p1 + unit.Convert(Location.Y) * XYZ.BasisY;
            return p2;
        }
       
        public void CreatePile(Document document,UnitProject unit,SettingModel settingModel, ColumnModel columnModel)
        {
            if (Pile==null)
            {
                Point = (columnModel.Style.Equals("RECTANGLE"))? GetPointRectangle(unit, columnModel): GetPointCylindrical(unit, columnModel);
                StructuralType structuralType = (settingModel.SelectedCategoyryPile.Equals("Structural Columns")) ? (StructuralType.Column) : (StructuralType.Footing);
                Pile = document.Create.NewFamilyInstance(Point, settingModel.SelectedPileFamilyType, Level, structuralType);
                if (Pile!=null)
                {
                    if (structuralType== StructuralType.Column)
                    {
                        Pile.get_Parameter(BuiltInParameter.FAMILY_BASE_LEVEL_PARAM).Set(Level.Id);
                        Pile.get_Parameter(BuiltInParameter.FAMILY_BASE_LEVEL_OFFSET_PARAM).Set(-unit.Convert(settingModel.LengthPile) - unit.Convert(settingModel.HeightFoundation - settingModel.Overlap));
                        
                        Pile.get_Parameter(BuiltInParameter.SCHEDULE_TOP_LEVEL_OFFSET_PARAM).Set(-unit.Convert(settingModel.HeightFoundation-settingModel.Overlap));
                        Pile.get_Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_PARAM).Set(Level.Id);
                    }
                    else
                    {
                        Pile.get_Parameter(BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM).Set(-unit.Convert(settingModel.HeightFoundation - settingModel.Overlap));
                        Pile.LookupParameter("L").Set(unit.Convert(settingModel.LengthPile));
                    }
                    if(columnModel.Style.Equals("RECTANGLE"))
                    {
                        Line axis = Line.CreateBound(Point, Point + 0.1 * XYZ.BasisZ);
                        ElementTransformUtils.RotateElement(document, Pile.Id, axis, columnModel.South.FaceNormal.AngleTo((-1) * XYZ.BasisY));
                    }
                   
                }
            }
        }
        public void SetPileName(SettingModel settingModel)
        {
            if (Pile != null)
            {
                Pile.LookupParameter("Comments").Set(settingModel.PileNamePrefix+PileNumber);
            }
        }
        #endregion
        #region Dimension
        private double FinDistanceDistanceFromPointTogird(Grid grid)
        {
            Line l = (grid.Curve as Line);
            XYZ xYZ = PointModel.ProjectToLine(Point, l);
            return Point.DistanceTo(xYZ);
        }
        private Grid FindMinimumDistanceGridXToPile(ObservableCollection<Grid> grid)
        {
            double distance = grid.Min(x => FinDistanceDistanceFromPointTogird(x));
            if (PointModel.AreEqual(distance,0))
            {
                return null;
            }
            return grid.Where(y => FinDistanceDistanceFromPointTogird(y)==distance).FirstOrDefault();
        }
        private Reference ChangeReferencePile(Document document,Point point)
        {
            string sam = point.Reference.ConvertToStableRepresentation(document);
            string Refer = sam.Remove(sam.Length-1)+"5:LINEAR/1";
            return Reference.ParseFromStableRepresentation(document, Refer);
        }
        private void GetReferenceX(Document document,Grid grid)
        {
            RefGridX.Append(new Reference(grid));
            Point p = SolidFace.GetPointElement(document, Pile);
            if (p != null)
            {
                RefGridX.Append(ChangeReferencePile(document, p));
            }
        }
        private void GetReferenceY(Document document, Grid grid)
        {
            RefGridY.Append(new Reference(grid));
            Point p = SolidFace.GetPointElement(document, Pile);
            if (p != null)
            {
                RefGridY.Append(ChangeReferencePile(document, p));
            }
        }
        private Line GetLineDimentionX(ViewPlan viewPlan, UnitProject unit, SettingModel settingModel, Grid grid)
        {
            XYZ px = new XYZ(Point.X, Point.Y, viewPlan.Origin.Z - unit.Convert(1.2 * settingModel.HeightFoundation));
            XYZ p0 = px - unit.Convert(settingModel.DiameterPile) * XYZ.BasisX;
            Line l = (grid.Curve as Line);
            XYZ p1 = PointModel.ProjectToLine(px, l);
            XYZ p1a = new XYZ(p1.X, p1.Y, viewPlan.Origin.Z - unit.Convert(1.2 * settingModel.HeightFoundation));
            XYZ p2= p1a - unit.Convert(settingModel.DiameterPile) * XYZ.BasisX;
            if (PointModel.AreEqual(p0.DistanceTo(p2), 0)) return null;
            return Line.CreateBound(p0, p2);
        }
        private Line GetLineDimentionY(ViewPlan viewPlan, UnitProject unit, SettingModel settingModel, Grid grid)
        {
            XYZ px = new XYZ(Point.X, Point.Y, viewPlan.Origin.Z - unit.Convert(1.2 * settingModel.HeightFoundation));
            XYZ p0 = px + unit.Convert(settingModel.DiameterPile) * XYZ.BasisY;
            Line l = (grid.Curve as Line);
            XYZ p1 = PointModel.ProjectToLine(px, l);
            XYZ p1a = new XYZ(p1.X, p1.Y, viewPlan.Origin.Z - unit.Convert(1.2 * settingModel.HeightFoundation));
            XYZ p2 = p1a + unit.Convert(settingModel.DiameterPile) * XYZ.BasisY;
            if (PointModel.AreEqual(p0.DistanceTo(p2), 0)) return null;
            return Line.CreateBound(p0, p2);
        }
        public void CreateDimentionPileToGrid(ViewPlan viewPlan, Document document, UnitProject unit, SettingModel settingModel, ObservableCollection<Grid> gridXs, ObservableCollection<Grid> gridYs)
        {
            if(gridXs.Count>=2)
            {
                Grid gridX = FindMinimumDistanceGridXToPile(gridXs);
                
                if (gridX != null)
                {
                    Line lineX = GetLineDimentionX(viewPlan,unit, settingModel, gridX);
                    if (lineX!=null)
                    {
                        GetReferenceX(document, gridX);
                        if (RefGridX.Size == 2)
                        {
                            DimensionGridX = document.Create.NewDimension(viewPlan, lineX, RefGridX, settingModel.SelectedDimensionType);
                            if (DimensionGridX == null) return;
                        }
                    }
                    
                }
            }
            if (gridYs.Count >= 2)
            {
                Grid gridY = FindMinimumDistanceGridXToPile(gridYs);
                if (gridY != null)
                {
                    
                    Line lineY = GetLineDimentionY(viewPlan, unit, settingModel, gridY);
                    if (lineY != null)
                    {
                        GetReferenceY(document, gridY);
                        if (RefGridY.Size == 2)
                        {
                            DimensionGridY = document.Create.NewDimension(viewPlan, lineY, RefGridY, settingModel.SelectedDimensionType);
                        }
                    }
                }
            }

        }
        #endregion
        #region Tag
        private XYZ GetXYZTagPile(ViewPlan viewPlan, Document document, UnitProject unit, SettingModel settingModel)
        {
            XYZ p1 = Point + unit.Convert(settingModel.DiameterPile) * XYZ.BasisX;
            return p1 - unit.Convert(settingModel.DiameterPile) * XYZ.BasisY;
        }
        public void CreateTagPile(ViewPlan viewPlan, Document document, UnitProject unit, SettingModel settingModel)
        {
            XYZ origin = GetXYZTagPile(viewPlan, document, unit, settingModel);
            if (settingModel.CheckedText)
            {

                TagPile = IndependentTag.Create(document, viewPlan.Id, new Reference(Pile), false, settingModel.Mode, settingModel.Horizontal, origin);
                TagPile.ChangeTypeId(settingModel.SelectedPileTag.Id);
            }
            else
            {
                TextTagPile = TextNote.Create(document, viewPlan.Id, origin, settingModel.PileNamePrefix+PileNumber, settingModel.SelectedTextNote.Id);
            }
            if (IsTestingPile)
            {
                CreateTestingPileTag(viewPlan, document, unit, settingModel);
            }
           
        }
        private void CreateTestingPileTag(ViewPlan viewPlan, Document document, UnitProject unit, SettingModel settingModel)
        {
            XYZ p1 = new XYZ(Point.X, Point.Y, viewPlan.Origin.Z - unit.Convert(settingModel.HeightFoundation));
            XYZ bend2 = p1 - unit.Convert(settingModel.DiameterPile * 2) * XYZ.BasisY;
            XYZ bend3 = bend2 + unit.Convert(settingModel.DiameterPile * 2) * XYZ.BasisX;
            Line l1 = Line.CreateBound(p1, bend2);
            Line l2 = Line.CreateBound(bend2, bend3);
            DetailCurve detailCurve1 = document.Create.NewDetailCurve(viewPlan, l1);
            DetailCurve detailCurve2 = document.Create.NewDetailCurve(viewPlan, l2);
            TextNote textNote= TextNote.Create(document,viewPlan.Id,bend2,"Testing Pile",settingModel.SelectedTextNote.Id);
        }
        #endregion
        #region SpotDimention
        private Reference GetReferenceSpotCoordinate(Document document)
        {
            Point p = SolidFace.GetPointElement(document, Pile);
            if (p==null)
            {
                return null;
            }
            else
            {
                return ChangeReferencePile(document, p);
            }
        }
        private XYZ GetOriginSpotCoordinate(ViewPlan viewPlan, Document document, UnitProject unit, SettingModel settingModel)
        {
            return new XYZ(Point.X, Point.Y, viewPlan.Origin.Z - unit.Convert(1.2 * settingModel.HeightFoundation));
        }
        
        public void CreateSpotDimensionPileCoordinate(ViewPlan viewPlan, Document document, UnitProject unit, SettingModel settingModel)
        {
            Reference reference = GetReferenceSpotCoordinate(document);
            if (reference!=null)
            {
                XYZ o = GetOriginSpotCoordinate(viewPlan, document, unit, settingModel);
                XYZ b1 = o + unit.Convert(settingModel.DiameterPile ) * XYZ.BasisX*((Location.X>0)?1:-1);
                XYZ bend = b1 + unit.Convert(settingModel.DiameterPile ) * XYZ.BasisY * ((Location.Y > 0) ? 1 : -1);
                XYZ end = bend + unit.Convert(settingModel.DiameterPile ) * XYZ.BasisX * ((Location.X > 0) ? 1 : -1);
                XYZ refpt = new XYZ(Point.X, Point.Y, Point.Z - unit.Convert(settingModel.HeightFoundation));
                SpotDimensionPile = document.Create.NewSpotCoordinate(viewPlan, reference, o, bend, end, refpt, true);
            }

        }
        #endregion

    }
}
