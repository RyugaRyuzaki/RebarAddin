
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;


namespace R10_WallShear
{
    public class InfoModel : BaseViewModel
    {
        #region property
        private int _NumberWall;
        public int NumberWall { get => _NumberWall; set { _NumberWall = value; OnPropertyChanged(); } }
        private double _D;
        public double T { get => _D; set { _D = value; OnPropertyChanged(); } }
        private double _L;
        public double L { get => _L; set { _L = value; OnPropertyChanged(); } }
        private double _TopPosition;
        public double TopPosition { get => _TopPosition; set { _TopPosition = value; OnPropertyChanged(); } }
        private double _BottomPosition;
        public double BottomPosition { get => _BottomPosition; set { _BottomPosition = value; OnPropertyChanged(); } }
        private double _SouthPosition;
        public double SouthPosition { get => _SouthPosition; set { _SouthPosition = value; OnPropertyChanged(); } }
        private double _NouthPosition;
        public double NouthPosition { get => _NouthPosition; set { _NouthPosition = value; OnPropertyChanged(); } }
        private double _WestPosition;
        public double WestPosition { get => _WestPosition; set { _WestPosition = value; OnPropertyChanged(); } }
        private double _EastPosition;
        public double EastPosition { get => _EastPosition; set { _EastPosition = value; OnPropertyChanged(); } }
        private PlanarFace _Top;
        public PlanarFace Top { get => _Top; set { _Top = value; OnPropertyChanged(); } }
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
        private Element _Element;
        public Element Element { get => _Element; set { _Element = value; OnPropertyChanged(); } }
        private double _eT;
        public double eT { get => _eT; set { _eT = value; OnPropertyChanged(); } }
        private double _eB;
        public double eB { get => _eB; set { _eB = value; OnPropertyChanged(); } }
        private double _hc;
        public double hc { get => _hc; set { _hc = value; OnPropertyChanged(); } }
        private double _hb;
        public double hb{ get => _hb; set { _hb = value; OnPropertyChanged(); } }
        private double _zb;
        public double zb { get => _zb; set { _zb = value; OnPropertyChanged(); } }
        private Level _TopLevel;
        public Level TopLevel { get => _TopLevel; set { _TopLevel = value; OnPropertyChanged(); } }
        private Level _BottomLevel;
        public Level BottomLevel { get => _BottomLevel; set { _BottomLevel = value; OnPropertyChanged(); } }

        private bool _IsCorner;
        public bool IsCorner { get => _IsCorner; set { _IsCorner = value; OnPropertyChanged(); } }
        private double _L1;
        public double L1 { get => _L1; set { _L1 = value; OnPropertyChanged(); } }
        private double _L2;
        public double L2 { get => _L2; set { _L2 = value; OnPropertyChanged(); } }
        #endregion
        public InfoModel(int numberWall,PlanarFace planarFace0, PlanarFace south0, PlanarFace west0, PlanarFace top, PlanarFace bottom, PlanarFace west, PlanarFace east, PlanarFace south, PlanarFace nouth, Element element, Document document)
        {
            NumberWall = numberWall;
            Top = top; Bottom = bottom; South = south; Nouth = nouth; West = west; East = east;
            T = PointModel.DistanceTo2(South, Nouth.Origin, document);
            L = PointModel.DistanceTo2(West, East.Origin, document);
            Element = element;
            ElementId elementId1 = Element.get_Parameter(BuiltInParameter.WALL_HEIGHT_TYPE).AsElementId();
            ElementId elementId2 = Element.get_Parameter(BuiltInParameter.WALL_BASE_CONSTRAINT).AsElementId();
            TopLevel = new FilteredElementCollector(document).OfClass(typeof(Level)).Cast<Level>().Where(x => x.Id==elementId1).FirstOrDefault();
            BottomLevel = new FilteredElementCollector(document).OfClass(typeof(Level)).Cast<Level>().Where(x => x.Id == elementId2).FirstOrDefault();
            hc = PointModel.DistanceTo2(Top, Bottom.Origin, document);
            double et = Element.get_Parameter(BuiltInParameter.WALL_TOP_OFFSET).AsDouble();
            eT = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, et, false));
            double eb = Element.get_Parameter(BuiltInParameter.WALL_BASE_OFFSET).AsDouble();
            eB = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, eb, false));
            TopPosition = PointModel.DistanceTo2(planarFace0, Top.Origin, document);
            BottomPosition = PointModel.DistanceTo2(planarFace0, Bottom.Origin, document);
            SouthPosition = PointModel.DistanceTo2(south0, South.Origin, document);
            NouthPosition = PointModel.DistanceTo2(south0, Nouth.Origin, document);
            WestPosition = PointModel.DistanceTo2(west0, West.Origin, document);
            EastPosition = PointModel.DistanceTo2(west0, East.Origin, document);
            GetBeams(document);
            IsCorner = false;
            GetL1L2();
        }
       
        private void GetBeams(Document document)
        {
            List<Element> beams = WallBoundingBox.GetBeamsBoudingBoxSameTopLevelPerpencularZOneWall(Element, document);
           
            if (beams.Count==0)
            {
                hb = 0;zb = 0;
            }
            else
            {
                List<PlanarFace> planarFaces = new List<PlanarFace>();
                for (int i = 0; i < beams.Count; i++)
                {
                    List<PlanarFace> planarFaces1 = SolidFace.GetTopPlanarFaceBeam(beams[i], document);
                    planarFaces.AddRange(planarFaces1);
                }
                planarFaces.OrderBy(x => x.Origin.Z);

                //hb = PointModel.DistanceTo2(planarFaces[0], planarFaces[planarFaces.Count - 1].Origin, document);
                hb = GetHb(planarFaces, document);
                double z = TopLevel.Elevation - Top.Origin.Z;
                //zb = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, zb, false));
                zb = PointModel.DistanceTo2(Top, planarFaces[0].Origin, document);
            }
           
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
        public void GetL1L2()
        {
            if (IsCorner)
            {
                L1 = T;L2 = L - 2 * L1;
            }
            else
            {
                L1 = 0;L2 = 0;
            }
        }
        
    }
}
