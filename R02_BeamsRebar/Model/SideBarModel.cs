using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;

namespace R02_BeamsRebar
{
    public class SideBarModel : BaseViewModel
    {
        private int _Span;
        public int Span { get => _Span; set { _Span = value; OnPropertyChanged(); } }
        private int _NumberBar;
        public int NumberBar { get => _NumberBar; set { _NumberBar = value; OnPropertyChanged(); } }
        private RebarBarModel _Bar;
        public RebarBarModel Bar { get => _Bar; set { _Bar = value; OnPropertyChanged(); } }

        private double _ExLeft;
        public double ExLeft { get => _ExLeft; set { _ExLeft = value; OnPropertyChanged(); } }
        private double _ExRight;
        public double ExRight { get => _ExRight; set { _ExRight = value; OnPropertyChanged(); } }
        private bool _IsSide;
        public bool IsSide { get => _IsSide; set { _IsSide = value; OnPropertyChanged(); } }
        private List<LocationBarModel> _Location;
        public List<LocationBarModel> Location { get => _Location; set { _Location = value; OnPropertyChanged(); } }
        private double _X0;
        public double X0 { get => _X0; set { _X0 = value; OnPropertyChanged(); } }
        private double _Y0;
        public double Y0 { get => _Y0; set { _Y0 = value; OnPropertyChanged(); } }
        public SideBarModel(int span, RebarBarModel bar, bool isSide, double exLeft, double exRight)
        {
            Span = span;
            NumberBar = 2;
            Bar = bar;
            IsSide = isSide;
            ExLeft = exLeft; ExRight = exRight;
        }
        public void GetLocation(InfoModel infoModel, double c, double ds, double dTop, double dBottom)
        {
            if (IsSide)
            {
                GetX0Y0(infoModel, c, ds, dTop, dBottom);
                if (Location == null)
                {
                    Location = new List<LocationBarModel>();

                }
                else
                {
                    Location.Clear();
                    Location = new List<LocationBarModel>();
                }
                Location.Add(new LocationBarModel(X0 - ExLeft - infoModel.Length / 2, Y0));
                Location.Add(new LocationBarModel(X0 + infoModel.Length / 2 + ExRight, Y0));
            }
            else
            {
                if (Location != null)
                {
                    Location.Clear();
                }
            }
        }
        private void GetX0Y0(InfoModel infoModel, double c, double ds, double dTop, double dBottom)
        {
            X0 = infoModel.startPosition + infoModel.Length / 2;
            double delta = (Math.Abs(infoModel.zOffset) + infoModel.h - (2 * c - 2 * ds - (dTop + dBottom) / 2)) / (NumberBar / 2 + 1);
            Y0 = Math.Abs(infoModel.zOffset) + infoModel.h / 2;
        }
        #region Create MainBottom bar

        private List<Curve> GetCurve(Document document, PlanarFace planarFace, UnitProject unit, double c, double dsmax, InfoModel infoModel0)
        {
            List<Curve> curves = new List<Curve>();
            double cover = unit.Convert(c);
            double ds = unit.Convert(dsmax);
            double d = unit.Convert(Bar.Diameter);
            double b = unit.Convert(infoModel0.b);
            List<XYZ> vector = new List<XYZ>();
            XYZ p1 = PointModel.ProjectToPlane(infoModel0.LeftRightPlanar[0].Origin, infoModel0.TopBottomPlanar[0]);
            XYZ p2 = null;
            if (NumberBar == 1)
            {
                p2 = p1 + (b / 2) * (-1) * infoModel0.LeftRightPlanar[0].FaceNormal;
            }
            else
            {
                p2 = p1 + (cover + ds + d / 2) * (-1) * infoModel0.LeftRightPlanar[0].FaceNormal;
            }

            XYZ p0 = PointModel.ProjectToPlane(p2, planarFace);
            double x1 = unit.Convert(Location[0].X);
            double y1 = unit.Convert(Location[0].Y);
            double x2 = unit.Convert(Location[Location.Count - 1].X);
            double y2 = unit.Convert(Location[Location.Count - 1].Y);
            XYZ p1x = p0 + x1 * (-1) * planarFace.FaceNormal;
            XYZ p1y = p1x + y1 * (-1) * XYZ.BasisZ;
            XYZ p2x = p0 + x2 * (-1) * planarFace.FaceNormal;
            XYZ p2y = p2x + y2 * (-1) * XYZ.BasisZ;
            Curve c1 = Line.CreateBound(p1y, p2y);
            curves.Add(c1);

            return curves;
        }
        public void CreateSideBar(Document document, PlanarFace planarFace, UnitProject unit, double c, double dsmax, InfoModel infoModel, SettingModel settingModel)
        {
            List<Curve> curves = GetCurve(document, planarFace, unit, c, dsmax, infoModel);
            Bar.Rebar = Rebar.CreateFromCurves(document, RebarStyle.Standard, Bar.RebarBarType, null, null, infoModel.Element, (-1) * infoModel.LeftRightPlanar[0].FaceNormal, curves, RebarHookOrientation.Right, RebarHookOrientation.Right, true, true);
            RebarShapeDrivenAccessor rebarShape1 = Bar.Rebar.GetShapeDrivenAccessor();
            double s = unit.Convert((infoModel.b - 2 * c - 2 * dsmax - Bar.Diameter));
            rebarShape1.SetLayoutAsFixedNumber(NumberBar, s, true, true, true);
            Bar.SetPartitionRebar(settingModel.BeamsName);
        }

        #endregion
        #region Tag
        public void CreateTagRebarDetail(Autodesk.Revit.DB.View view, Document document, UnitProject unit, InfoModel infoModel0, SettingModel settingModel, PlanarFace planarFace0, double h0, double v0)
        {
            Bar.CreateTagRebarDetailTop(view, document, unit, infoModel0, settingModel, planarFace0, X0 + infoModel0.b, Y0, h0, -v0);
        }
        public void CreateTagRebarSection(SectionBeamView SectionBeamView, Document document, UnitProject unit, InfoModel infoModel0, SettingModel settingModel, PlanarFace planarFace0, double tagH0, double tagV0)
        {
            Bar.CreateTagRebarSectionItem(SectionBeamView.StartView, document, unit, infoModel0, planarFace0, settingModel, Location[1].Y, tagH0, tagV0);
            Bar.CreateTagRebarSectionItem(SectionBeamView.EndView, document, unit, infoModel0, planarFace0, settingModel, Location[1].Y, tagH0, tagV0);
            Bar.CreateTagRebarSectionItem(SectionBeamView.MidView, document, unit, infoModel0, planarFace0, settingModel, Location[1].Y, tagH0, tagV0);
        }
        #endregion
    }
}
