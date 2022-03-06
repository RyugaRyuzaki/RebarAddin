using System.Collections.ObjectModel;
using WpfCustomControls;
namespace R10_WallShear
{
    public class BarModel : BaseViewModel
    {
        #region property
        private int _BarNumber;
        public int BarNumber { get => _BarNumber; set { _BarNumber = value; OnPropertyChanged(); } }
        private RebarBarModel _Bar;
        public RebarBarModel Bar { get => _Bar; set { _Bar = value; OnPropertyChanged(); } }
        private ObservableCollection<LocationBarModel> _Location;
        public ObservableCollection<LocationBarModel> Location { get { if (_Location == null) _Location = new ObservableCollection<LocationBarModel>(); return _Location; } set { _Location = value; OnPropertyChanged(); } }
        private int _TopDowels;
        public int TopDowels { get => _TopDowels; set { _TopDowels = value; OnPropertyChanged(); } }
        private bool _IsTopDowels;
        public bool IsTopDowels { get => _IsTopDowels; set { _IsTopDowels = value; OnPropertyChanged(); } }
        private double _LaTopDowels;
        public double LaTopDowels { get => _LaTopDowels; set { _LaTopDowels = value; OnPropertyChanged(); } }
        private double _LbTopDowels;
        public double LbTopDowels { get => _LbTopDowels; set { _LbTopDowels = value; OnPropertyChanged(); } }
        private int _BottomDowels;
        public int BottomDowels { get => _BottomDowels; set { _BottomDowels = value; OnPropertyChanged(); } }
        private bool _IsBottomDowels;
        public bool IsBottomDowels { get => _IsBottomDowels; set { _IsBottomDowels = value; OnPropertyChanged(); } }
        private double _LaBottomDowels;
        public double LaBottomDowels { get => _LaBottomDowels; set { _LaBottomDowels = value; OnPropertyChanged(); } }
        private double _LbBottomDowels;
        public double LbBottomDowels { get => _LbBottomDowels; set { _LbBottomDowels = value; OnPropertyChanged(); } }
        private double _LcBottomDowels;
        public double LcBottomDowels { get => _LcBottomDowels; set { _LcBottomDowels = value; OnPropertyChanged(); } }
        private double _X0;
        public double X0 { get => _X0; set { _X0 = value; OnPropertyChanged(); } }
        private double _Y0;
        public double Y0 { get => _Y0; set { _Y0 = value; OnPropertyChanged(); } }
        private double _L1;
        public double L1 { get => _L1; set { _L1 = value; OnPropertyChanged(); } }
        private double _L2;
        public double L2 { get => _L2; set { _L2 = value; OnPropertyChanged(); } }
        private double _L3;
        public double L3 { get => _L3; set { _L3 = value; OnPropertyChanged(); } }
        private bool _EvenTop;
        public bool EvenTop { get => _EvenTop; set { _EvenTop = value; OnPropertyChanged(); } }
        private bool _EvenBottom;
        public bool EvenBottom { get => _EvenBottom; set { _EvenBottom = value; OnPropertyChanged(); } }
      
        #endregion
        public BarModel(int barNumber, RebarBarModel bar, double spliOverlap, double overlap, bool Corner)
        {
            BarNumber = barNumber;
            Bar = bar;
            TopDowels = 0; BottomDowels = 0;
            IsTopDowels = true; LaTopDowels = overlap * Bar.Diameter; LbTopDowels = (spliOverlap == 50) ? ((BarNumber % 2 == 0) ? overlap * Bar.Diameter : overlap * 2 * Bar.Diameter) : overlap * Bar.Diameter;
            IsBottomDowels = false; LaBottomDowels = overlap * Bar.Diameter; LbBottomDowels = overlap * Bar.Diameter; LcBottomDowels = overlap * Bar.Diameter;
            L1 = 0;
            L2 = 0;
            L3 = 0;
            EvenTop = false;
            EvenBottom = false;
           
        }
        public BarModel(int barNumber, RebarBarModel bar, double spliOverlap, double overlap, double l1, double l2, double l3, bool Corner)
        {
            BarNumber = barNumber;
            Bar = bar;
            L1 = l1;
            L2 = l2;
            L3 = l3;
            TopDowels = 0; BottomDowels = 0;
            IsTopDowels = true; LaTopDowels = overlap * Bar.Diameter; LbTopDowels = (spliOverlap == 50) ? ((BarNumber % 2 == 0) ? overlap * Bar.Diameter : overlap * 2 * Bar.Diameter) : overlap * Bar.Diameter;
            IsBottomDowels = false; LaBottomDowels = overlap * Bar.Diameter; LbBottomDowels = overlap * Bar.Diameter; LcBottomDowels = overlap * Bar.Diameter;
            EvenTop = false;
            EvenBottom = false;
        }
        #region method
        public void GetAddBar(BarModel barModel, int nx,int ny, int nxCorner, int nyCorner,bool Corner)
        {

        }
       
        public void GetX0Y0(double x0, double y0)
        {
            X0 = x0; Y0 = y0;
        }

        public void GetLocationBar(InfoModel infoModel, double Cover)
        {
            if (Location.Count != 0)
            {
                Location.Clear();
            }
            Location.Add(new LocationBarModel(X0, Y0, infoModel.BottomPosition));
            Location.Add(new LocationBarModel(X0, Y0, infoModel.TopPosition - Cover));
        }
        public void GetX0Y0AddBar(double x0, double y0)
        {
            X0 = x0; Y0 = y0;
        }
        
        public void GetDowelsLocationBar(InfoModel infoModel, int total, int nx, int ny, double Cover, double splitOverlap, double overlap, double x01, double y01)
        {
            if (Location.Count != 0)
            {
                Location.Clear();
            }

            GetLocationBottom(infoModel, total, nx, ny);
            GetLocationTop(infoModel, Cover, splitOverlap, overlap, total, nx, ny, x01, y01);
        }



        private void GetLocationBottom(InfoModel infoModel, int total, int nx, int ny)
        {
            if (!IsBottomDowels)
            {
                Location.Add(new LocationBarModel(X0, Y0, infoModel.BottomPosition));
            }
            else
            {
                if (BottomDowels == 0)
                {
                    Location.Add(new LocationBarModel(X0, Y0, infoModel.BottomPosition + LcBottomDowels));
                }
                else
                {
                    if (LaBottomDowels == 0)
                    {
                        Location.Add(new LocationBarModel(X0, Y0, infoModel.BottomPosition - LbBottomDowels));
                    }
                    else
                    {
                        if (BarNumber <= nx)
                        {
                            Location.Add(new LocationBarModel(X0, Y0 - LaBottomDowels, infoModel.BottomPosition - LbBottomDowels));
                        }
                        if (BarNumber > nx && BarNumber <= nx + (ny - 2))
                        {
                            Location.Add(new LocationBarModel(X0 + LaBottomDowels, Y0, infoModel.BottomPosition - LbBottomDowels));
                        }
                        if (BarNumber > nx + (ny - 2) && BarNumber <= 2 * nx + (ny - 2))
                        {
                            Location.Add(new LocationBarModel(X0, Y0 + LaBottomDowels, infoModel.BottomPosition - LbBottomDowels));
                        }
                        if (BarNumber > total - (ny - 2))
                        {
                            Location.Add(new LocationBarModel(X0 - LaBottomDowels, Y0, infoModel.BottomPosition - LbBottomDowels));
                        }
                        Location.Add(new LocationBarModel(X0, Y0, infoModel.BottomPosition - LbBottomDowels));
                    }
                }
            }
        }
        private void GetLocationTop(InfoModel infoModel, double Cover, double splitOverlap, double overlap, int total, int nx, int ny, double x01, double y01)
        {
            if (IsTopDowels)
            {
                double hb = (PointModel.AreEqual(infoModel.zb + infoModel.hb, 0)) ? (infoModel.T) : (infoModel.zb + infoModel.hb);

                if (TopDowels == 0)
                {
                    Location.Add(new LocationBarModel(X0, Y0, infoModel.TopPosition - hb));
                    Location.Add(new LocationBarModel(x01, y01, infoModel.TopPosition));
                    Location.Add(new LocationBarModel(x01, y01, infoModel.TopPosition + LbTopDowels));
                }
                else
                {
                    if (LaTopDowels == 0)
                    {
                        Location.Add(new LocationBarModel(X0, Y0, infoModel.TopPosition - Cover - infoModel.zb));
                    }
                    else
                    {
                        Location.Add(new LocationBarModel(X0, Y0, infoModel.TopPosition - Cover - infoModel.zb));
                        if (BarNumber <= nx)
                        {
                            Location.Add(new LocationBarModel(X0, Y0 - LaTopDowels, infoModel.TopPosition - Cover - infoModel.zb));
                        }
                        if (BarNumber > nx && BarNumber <= nx + (ny - 2))
                        {
                            Location.Add(new LocationBarModel(X0 + LaTopDowels, Y0, infoModel.TopPosition - Cover - infoModel.zb));
                        }
                        if (BarNumber > nx + (ny - 2) && BarNumber <= 2 * nx + (ny - 2))
                        {
                            Location.Add(new LocationBarModel(X0, Y0 + LaTopDowels, infoModel.TopPosition - Cover - infoModel.zb));
                        }
                        if (BarNumber > total - (ny - 2))
                        {
                            Location.Add(new LocationBarModel(X0 - LaTopDowels, Y0, infoModel.TopPosition - Cover - infoModel.zb));
                        }

                    }

                }
            }
            else
            {
                Location.Add(new LocationBarModel(X0, Y0, infoModel.TopPosition - Cover));
            }
        }
        private void GetLocationBottomCorner(InfoModel infoModel, int nx, int ny)
        {
            if (!IsBottomDowels)
            {
                Location.Add(new LocationBarModel(X0, Y0, infoModel.BottomPosition));
            }
            else
            {
                if (BottomDowels == 0)
                {
                    Location.Add(new LocationBarModel(X0, Y0, infoModel.BottomPosition + LcBottomDowels));
                }
                else
                {
                    if (LaBottomDowels == 0)
                    {
                        Location.Add(new LocationBarModel(X0, Y0, infoModel.BottomPosition - LbBottomDowels));
                    }
                    else
                    {
                        int total = (2 * nx + 2 * (ny - 2));
                        if (BarNumber <= nx || (BarNumber > total && BarNumber < total + nx))
                        {
                            Location.Add(new LocationBarModel(X0, Y0 - LaBottomDowels, infoModel.BottomPosition - LbBottomDowels));
                        }
                        if ((BarNumber > nx && BarNumber <= nx + (ny - 2)) || (BarNumber > nx + total && BarNumber <= nx + (ny - 2) + total))
                        {
                            Location.Add(new LocationBarModel(X0 + LaBottomDowels, Y0, infoModel.BottomPosition - LbBottomDowels));
                        }
                        if ((BarNumber > nx + (ny - 2) && BarNumber <= 2 * nx + (ny - 2)) || (BarNumber > nx + (ny - 2) + total && BarNumber <= 2 * nx + (ny - 2) + total))
                        {
                            Location.Add(new LocationBarModel(X0, Y0 + LaBottomDowels, infoModel.BottomPosition - LbBottomDowels));
                        }

                        if ((BarNumber > total - (ny - 2) && BarNumber <= total) || (BarNumber > total - (ny - 2) + total && BarNumber <= total + total))
                        {
                            Location.Add(new LocationBarModel(X0 - LaBottomDowels, Y0, infoModel.BottomPosition - LbBottomDowels));
                        }
                        Location.Add(new LocationBarModel(X0, Y0, infoModel.BottomPosition - LbBottomDowels));
                    }
                }
            }
        }
        private void GetLocationTopCorner(InfoModel infoModel, double Cover, double splitOverlap, double overlap,  int nx, int ny, double x01, double y01)
        {
            if (IsTopDowels)
            {
                double hb = (PointModel.AreEqual(infoModel.zb + infoModel.hb, 0)) ? (infoModel.T) : (infoModel.zb + infoModel.hb);

                if (TopDowels == 0)
                {
                    Location.Add(new LocationBarModel(X0, Y0, infoModel.TopPosition - hb));
                    Location.Add(new LocationBarModel(x01, y01, infoModel.TopPosition));
                    Location.Add(new LocationBarModel(x01, y01, infoModel.TopPosition + LbTopDowels));
                }
                else
                {
                    if (LaTopDowels == 0)
                    {
                        Location.Add(new LocationBarModel(X0, Y0, infoModel.TopPosition - Cover - infoModel.zb));
                    }
                    else
                    {
                        int total = (2 * nx + 2 * (ny - 2));
                        Location.Add(new LocationBarModel(X0, Y0, infoModel.TopPosition - Cover - infoModel.zb));
                        if ((BarNumber <= nx) || (BarNumber > total && BarNumber < total + nx))
                        {
                            Location.Add(new LocationBarModel(X0, Y0 - LaTopDowels, infoModel.TopPosition - Cover - infoModel.zb));
                        }
                        if ((BarNumber > nx && BarNumber <= nx + (ny - 2)) ||(BarNumber > nx+total && BarNumber <= nx + (ny - 2)+total))
                        {
                            Location.Add(new LocationBarModel(X0 + LaTopDowels, Y0, infoModel.TopPosition - Cover - infoModel.zb));
                        }
                        if ((BarNumber > nx + (ny - 2) && BarNumber <= 2 * nx + (ny - 2))|| (BarNumber > nx + (ny - 2)+total && BarNumber <= 2 * nx + (ny - 2)+total))
                        {
                            Location.Add(new LocationBarModel(X0, Y0 + LaTopDowels, infoModel.TopPosition - Cover - infoModel.zb));
                        }
                        if ((BarNumber > total - (ny - 2)&&BarNumber<=total)||(BarNumber > total - (ny - 2)+total && BarNumber <= total+total))
                        {
                            Location.Add(new LocationBarModel(X0 - LaTopDowels, Y0, infoModel.TopPosition - Cover - infoModel.zb));
                        }
                    }

                }
            }
            else
            {
                Location.Add(new LocationBarModel(X0, Y0, infoModel.TopPosition - Cover));
            }
        }
        public void GetDowelsLocationBarCorner(InfoModel infoModel,  int nx, int ny, double Cover, double splitOverlap, double overlap, double x01, double y01)
        {
            if (Location.Count != 0)
            {
                Location.Clear();
            }

            GetLocationBottomCorner(infoModel,  nx, ny);
            GetLocationTopCorner(infoModel, Cover, splitOverlap, overlap, nx, ny, x01, y01);
        }
        #endregion
        #region Create Rebar
        //public bool ConditionMultiCurve()
        //{
        //    for (int i = 1; i < Location.Count; i++)
        //    {
        //        if (!PointModel.AreEqual(Location[i - 1].X, Location[i].X) || !PointModel.AreEqual(Location[i - 1].Y, Location[i].Y)) return false;
        //    }
        //    return true;
        //}
        //public List<CurveLoop> GetCurveLoop(SectionStyle sectionStyle, Document document, PlanarFace planarFace, UnitProject unit, InfoModel infoModel0)
        //{
        //    List<Curve> curves = new List<Curve>();
        //    XYZ p1 = (sectionStyle == SectionStyle.RECTANGLE) ? PointModel.ProjectToPlane(infoModel0.West.Origin, infoModel0.South) : infoModel0.PointPosition;
        //    XYZ p0 = PointModel.ProjectToPlane(p1, planarFace);
        //    if (IsTopDowels && TopDowels == 0)
        //    {
        //        if (ConditionMultiCurve())
        //        {

        //            curves.Add(Line.CreateBound(GetPoint(sectionStyle, unit, p0, Location[0], infoModel0), GetPoint(sectionStyle, unit, p0, Location[Location.Count - 1], infoModel0)));
        //        }
        //        else
        //        {
        //            if (!PointModel.AreEqual(Location[Location.Count - 3].X, Location[Location.Count - 2].X) || !PointModel.AreEqual(Location[Location.Count - 3].Y, Location[Location.Count - 2].Y))
        //            {
        //                for (int i = 1; i < Location.Count; i++)
        //                {
        //                    curves.Add(Line.CreateBound(GetPoint(sectionStyle, unit, p0, Location[i - 1], infoModel0), GetPoint(sectionStyle, unit, p0, Location[i], infoModel0)));
        //                }
        //            }
        //            else
        //            {
        //                curves.Add(Line.CreateBound(GetPoint(sectionStyle, unit, p0, Location[0], infoModel0), GetPoint(sectionStyle, unit, p0, Location[1], infoModel0)));
        //                curves.Add(Line.CreateBound(GetPoint(sectionStyle, unit, p0, Location[1], infoModel0), GetPoint(sectionStyle, unit, p0, Location[Location.Count - 1], infoModel0)));
        //            }
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 1; i < Location.Count; i++)
        //        {
        //            curves.Add(Line.CreateBound(GetPoint(sectionStyle, unit, p0, Location[i - 1], infoModel0), GetPoint(sectionStyle, unit, p0, Location[i], infoModel0)));
        //        }
        //    }

        //    CurveLoop curveLoop = CurveLoop.Create(curves);
        //    List<CurveLoop> curveLoop1 = new List<CurveLoop>();
        //    curveLoop1.Add(curveLoop);
        //    return curveLoop1;
        //}
        //private XYZ GetPoint(SectionStyle sectionStyle, UnitProject unit, XYZ p0, LocationBarModel location, InfoModel infoModel0)
        //{


        //    if (sectionStyle == SectionStyle.RECTANGLE)
        //    {
        //        XYZ px = p0 + unit.Convert(location.X) * infoModel0.East.FaceNormal;
        //        XYZ py = px + unit.Convert(location.Y) * infoModel0.Nouth.FaceNormal;
        //        return py + unit.Convert(location.Z) * XYZ.BasisZ;
        //    }
        //    else
        //    {
        //        XYZ px = p0 + unit.Convert(location.X) * XYZ.BasisX;
        //        XYZ py = px + unit.Convert(location.Y) * XYZ.BasisY;
        //        return py + unit.Convert(location.Z) * XYZ.BasisZ;
        //    }
        //    //return new XYZ(px.X, py.Y, pz.Z);
        //}
        //public void CreateMainBar(SectionStyle sectionStyle, Document document, PlanarFace planarFace, UnitProject unit, InfoModel infoModel0, InfoModel infoModel, SettingModel settingModel)
        //{
        //    List<CurveLoop> curveLoop = GetCurveLoop(sectionStyle, document, planarFace, unit, infoModel0);
        //    Bar.Rebar = Rebar.CreateFreeForm(document, Bar.RebarBarType, infoModel.Element, curveLoop, out RebarFreeFormValidationResult a);
        //    Bar.SetPartitionRebar(settingModel.ColumnsName);
        //}
        #endregion
    }
}
