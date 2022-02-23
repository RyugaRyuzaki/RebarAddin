using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WpfCustomControls;
using DSP;
using ViewCallOut = Autodesk.Revit.DB.View;
namespace R11_FoundationPile
{
    public class DimensionDetail : BaseViewModel
    {
        public ViewFamilyType Detail { get; set; }
        public BoundingBoxXYZ SectionBoxX { get; set; }
        
        private ObservableCollection<Grid> _AllGrid;
        public ObservableCollection<Grid> AllGrid { get => _AllGrid; set { _AllGrid = value; OnPropertyChanged(); } }
        private ObservableCollection<Grid> _GridX;
        public ObservableCollection<Grid> GridX { get { if (_GridX == null) { _GridX = new ObservableCollection<Grid>(); } return _GridX; } set { _GridX = value; OnPropertyChanged(); } }
        private ReferenceArray _RefGridX;
        public ReferenceArray RefGridX { get { if (_RefGridX == null) { _RefGridX = new ReferenceArray(); } return _RefGridX; } set { _RefGridX = value; OnPropertyChanged(); } }
        private ReferenceArray _RefGridXStart;
        public ReferenceArray RefGridXStart { get { if (_RefGridXStart == null) { _RefGridXStart = new ReferenceArray(); } return _RefGridXStart; } set { _RefGridXStart = value; OnPropertyChanged(); } }
        public Dimension DimensionGridX { get; set; }
        public Dimension DimensionGridXStart { get; set; }

        private ObservableCollection<Grid> _GridY;
        public ObservableCollection<Grid> GridY { get { if (_GridY == null) { _GridY = new ObservableCollection<Grid>(); } return _GridY; } set { _GridY = value; OnPropertyChanged(); } }
        private ReferenceArray _RefGridY;
        public ReferenceArray RefGridY { get { if (_RefGridY == null) { _RefGridY = new ReferenceArray(); } return _RefGridY; } set { _RefGridY = value; OnPropertyChanged(); } }
        private ReferenceArray _RefGridYStart;
        public ReferenceArray RefGridYStart { get { if (_RefGridYStart == null) { _RefGridYStart = new ReferenceArray(); } return _RefGridYStart; } set { _RefGridYStart = value; OnPropertyChanged(); } }
        public Dimension DimensionGridY { get; set; }
        public Dimension DimensionGridYStart { get; set; }
       
        public DimensionDetail(Document document)
        {
            GetAllGrid(document);
            
        }
        private void GetAllGrid(Document document)
        {
            AllGrid = new ObservableCollection<Grid>(new FilteredElementCollector(document).WhereElementIsNotElementType().OfClass(typeof(Grid)).Cast<Grid>().Where(x => ((x.Curve as Line) != null)).ToList());
            if (AllGrid.Count != 0)
            {
                for (int i = 0; i < AllGrid.Count; i++)
                {
                    Line a = AllGrid[i].Curve as Line;
                    bool x = (PointModel.AreEqual((a.Direction.AngleTo(XYZ.BasisX)), 0) || PointModel.AreEqual((a.Direction.AngleTo(XYZ.BasisX)), Math.PI));
                    if (x) GridX.Add(AllGrid[i]);
                    bool y = (PointModel.AreEqual((a.Direction.AngleTo(XYZ.BasisY)), 0) || PointModel.AreEqual((a.Direction.AngleTo(XYZ.BasisY)), Math.PI));
                    if (y) GridY.Add(AllGrid[i]);
                }
            }
            if (GridX.Count>=2)
            {
                GridX = new ObservableCollection<Grid>(GridX.OrderBy(x => (x.Curve as Line).GetEndPoint(0).Y).ToList());
                for (int i = 0; i < GridX.Count; i++)
                {
                    //Line a = AllGrid[i].Curve as Line;
                    RefGridX.Append(new Reference(GridX[i]));
                }
                RefGridXStart .Append(new Reference(GridX[0]));
                RefGridXStart .Append(new Reference(GridX[GridX.Count-1]));
            }
            if (GridY.Count >= 2)
            {
                GridY = new ObservableCollection<Grid>(GridY.OrderBy(x => (x.Curve as Line).GetEndPoint(0).X).ToList());
                for (int i = 0; i < GridY.Count; i++)
                {
                    //Line a = AllGrid[i].Curve as Line;
                    RefGridY.Append(new Reference(GridY[i]));
                }
                RefGridYStart.Append(new Reference(GridY[0]));
                RefGridYStart.Append(new Reference(GridY[GridY.Count - 1]));
            }
           
        }
        #region Fopundation
        private Line GetLineGridX(ViewPlan viewPlan, Document document, UnitProject unit, double offset0)
        {
            double offset = unit.Convert(offset0);
            double x1 = Math.Min((GridX[0].Curve as Line).GetEndPoint(0).X, (GridX[0].Curve as Line).GetEndPoint(1).X);
            double y1 = (GridX[0].Curve as Line).GetEndPoint(0).Y;
            double z1 = viewPlan.Origin.Z;

            double x2 = Math.Min((GridX[GridX.Count-1].Curve as Line).GetEndPoint(0).X, (GridX[GridX.Count - 1].Curve as Line).GetEndPoint(1).X);
            double y2= (GridX[GridX.Count - 1].Curve as Line).GetEndPoint(0).Y;
            double z2 = viewPlan.Origin.Z;

            XYZ p1 = new XYZ(x1- offset, y1, z1);
            XYZ p2 = new XYZ(x2- offset, y2, z2);
            return Line.CreateBound(p1, p2);
        }
      
        private Line GetLineGridY(ViewPlan viewPlan, Document document, UnitProject unit, double offset0)
        {
            double offset = unit.Convert(offset0);
            double x1 = Math.Min((GridY[0].Curve as Line).GetEndPoint(0).X, (GridY[0].Curve as Line).GetEndPoint(1).X);
            double y1 = (GridY[0].Curve as Line).GetEndPoint(0).Y;
            double z1 = viewPlan.Origin.Z;

            double x2 = Math.Min((GridY[GridY.Count - 1].Curve as Line).GetEndPoint(0).X, (GridY[GridY.Count - 1].Curve as Line).GetEndPoint(1).X);
            double y2 = (GridY[GridY.Count - 1].Curve as Line).GetEndPoint(0).Y;
            double z2 = viewPlan.Origin.Z;

            XYZ p1 = new XYZ(x1, y1 -offset, z1);
            XYZ p2 = new XYZ(x2, y2 -offset, z2);
            return Line.CreateBound(p1, p2);
        }
       
       
        public void CreateDimensionGridX(ViewPlan viewPlan, Document document, UnitProject unit,SettingModel settingModel)
        {
            if(GridX.Count >= 2)
            {
                Line line = GetLineGridX(viewPlan, document, unit, settingModel.OffsetDim);
                DimensionGridX = document.Create.NewDimension(viewPlan, line, RefGridX, settingModel.SelectedDimensionType);
                Line lineStart = GetLineGridX(viewPlan, document, unit, 2*settingModel.OffsetDim);
                DimensionGridXStart = document.Create.NewDimension(viewPlan, lineStart, RefGridXStart, settingModel.SelectedDimensionType);
            }
        }
        public void CreateDimensionGridY(ViewPlan viewPlan, Document document, UnitProject unit, SettingModel settingModel)
        {
            if (GridY.Count >= 2)
            {
                Line line = GetLineGridY(viewPlan, document, unit, settingModel.OffsetDim);
                DimensionGridY = document.Create.NewDimension(viewPlan, line, RefGridY, settingModel.SelectedDimensionType);
                Line lineStart = GetLineGridY(viewPlan, document, unit, 2*settingModel.OffsetDim);
                DimensionGridYStart = document.Create.NewDimension(viewPlan, lineStart, RefGridYStart, settingModel.SelectedDimensionType);
            }
        }
        public void CreateDimensionFoundationVertical(ViewPlan viewPlan, Document document, UnitProject unit, SettingModel settingModel,FoundationModel foundationModel,int image)
        {
            ReferenceArray referenceArray = foundationModel.GetReferenceArrayVertical(viewPlan,document,unit,settingModel, image);
            if (referenceArray.Size >= 2)
            {
                Line line = foundationModel.GetFoundationLineDimVerticalImage0(viewPlan, document, unit, settingModel);
                Dimension dimension  = document.Create.NewDimension(viewPlan, line, referenceArray, settingModel.SelectedDimensionType);
            }
        }
        public void CreateDimensionFoundationHorizontal(ViewPlan viewPlan, Document document, UnitProject unit, SettingModel settingModel, FoundationModel foundationModel, int image)
        {
            ReferenceArray referenceArray = foundationModel.GetReferenceArrayHorizontal(viewPlan, document, unit, settingModel, image);
            if (referenceArray.Size >= 2)
            {
                Line line = foundationModel.GetFoundationLineDimHorizontalImage0(viewPlan, document, unit, settingModel);
                Dimension dimension = document.Create.NewDimension(viewPlan, line, referenceArray, settingModel.SelectedDimensionType);
            }
        }
        public void CreateDimensionFoundationVerticalCallOut(ViewCallOut viewCallOut, Document document, UnitProject unit, SettingModel settingModel, FoundationModel foundationModel, int image)
        {
            ReferenceArray referenceArray = foundationModel.GetReferenceArrayVerticalCallOut(viewCallOut, document, unit, settingModel, image);
            if (referenceArray.Size >= 2)
            {
                Line line = foundationModel.GetFoundationLineDimVerticalImage0CallOut(viewCallOut, document, unit, settingModel);
                Dimension dimension = document.Create.NewDimension(viewCallOut, line, referenceArray, settingModel.SelectedDimensionType);
            }
        }
        public void CreateDimensionFoundationHorizontalCallOut(ViewCallOut viewCallOut, Document document, UnitProject unit, SettingModel settingModel, FoundationModel foundationModel, int image)
        {
            ReferenceArray referenceArray = foundationModel.GetReferenceArrayHorizontalCallOut(viewCallOut, document, unit, settingModel, image);
            if (referenceArray.Size >= 2)
            {
                Line line = foundationModel.GetFoundationLineDimHorizontalImage0CallOut(viewCallOut, document, unit, settingModel);
                Dimension dimension = document.Create.NewDimension(viewCallOut, line, referenceArray, settingModel.SelectedDimensionType);
            }
        }
        #endregion
        #region   Pile
        private Line GetLineGridXPile(ViewPlan viewPlan, Document document, UnitProject unit, SettingModel settingModel, double offset0)
        {
            double offset = unit.Convert(offset0);
            double x1 = Math.Min((GridX[0].Curve as Line).GetEndPoint(0).X, (GridX[0].Curve as Line).GetEndPoint(1).X);
            double y1 = (GridX[0].Curve as Line).GetEndPoint(0).Y;
            double z1 = viewPlan.Origin.Z - unit.Convert(1.2 * settingModel.HeightFoundation);

            double x2 = Math.Min((GridX[GridX.Count - 1].Curve as Line).GetEndPoint(0).X, (GridX[GridX.Count - 1].Curve as Line).GetEndPoint(1).X);
            double y2 = (GridX[GridX.Count - 1].Curve as Line).GetEndPoint(0).Y;
            double z2 = viewPlan.Origin.Z - unit.Convert(1.2 * settingModel.HeightFoundation);

            XYZ p1 = new XYZ(x1 - offset, y1, z1);
            XYZ p2 = new XYZ(x2 - offset, y2, z2);
            return Line.CreateBound(p1, p2);
        }
        private Line GetLineGridYPile(ViewPlan viewPlan, Document document, UnitProject unit, SettingModel settingModel, double offset0)
        {
            double offset = unit.Convert(offset0);
            double x1 = Math.Min((GridY[0].Curve as Line).GetEndPoint(0).X, (GridY[0].Curve as Line).GetEndPoint(1).X);
            double y1 = (GridY[0].Curve as Line).GetEndPoint(0).Y;
            double z1 = viewPlan.Origin.Z - unit.Convert(1.2 * settingModel.HeightFoundation);

            double x2 = Math.Min((GridY[GridY.Count - 1].Curve as Line).GetEndPoint(0).X, (GridY[GridY.Count - 1].Curve as Line).GetEndPoint(1).X);
            double y2 = (GridY[GridY.Count - 1].Curve as Line).GetEndPoint(0).Y;
            double z2 = viewPlan.Origin.Z - unit.Convert(1.2 * settingModel.HeightFoundation);

            XYZ p1 = new XYZ(x1, y1 - offset, z1);
            XYZ p2 = new XYZ(x2, y2 - offset, z2);
            return Line.CreateBound(p1, p2);
        }
        public void CreateDimensionGridXPile(ViewPlan viewPlan, Document document, UnitProject unit, SettingModel settingModel)
        {
            if (GridX.Count >= 2)
            {
                Line line = GetLineGridXPile(viewPlan, document, unit, settingModel, settingModel.OffsetDim);
                DimensionGridX = document.Create.NewDimension(viewPlan, line, RefGridX, settingModel.SelectedDimensionType);
                Line lineStart = GetLineGridXPile(viewPlan, document, unit, settingModel, 2 * settingModel.OffsetDim);
                DimensionGridXStart = document.Create.NewDimension(viewPlan, lineStart, RefGridXStart, settingModel.SelectedDimensionType);
            }
        }
        public void CreateDimensionGridYPile(ViewPlan viewPlan, Document document, UnitProject unit, SettingModel settingModel)
        {
            if (GridY.Count >= 2)
            {
                Line line = GetLineGridYPile(viewPlan, document, unit, settingModel, settingModel.OffsetDim);
                DimensionGridY = document.Create.NewDimension(viewPlan, line, RefGridY, settingModel.SelectedDimensionType);
                Line lineStart = GetLineGridYPile(viewPlan, document, unit, settingModel, 2 * settingModel.OffsetDim);
                DimensionGridYStart = document.Create.NewDimension(viewPlan, lineStart, RefGridYStart, settingModel.SelectedDimensionType);
            }
        }
        #endregion
    }
}
