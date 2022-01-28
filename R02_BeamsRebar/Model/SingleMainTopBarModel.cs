
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;

namespace R02_BeamsRebar
{
    public class SingleMainTopBarModel : BaseViewModel
    {
        private int _NumberBar;
        public int NumberBar { get => _NumberBar; set { _NumberBar = value; OnPropertyChanged(); } }
        private RebarBarModel _Bar;
        public RebarBarModel Bar { get => _Bar; set { _Bar = value; OnPropertyChanged(); } }
        private double _Length;
        public double Length { get => _Length; set { _Length = value; OnPropertyChanged(); } }
        private double _Temp;
        public double Temp { get => _Temp; set { _Temp = value; OnPropertyChanged(); } }
        private double _La;
        public double La { get => _La; set { _La = value; OnPropertyChanged(); } }
        private double _Lb;
        public double Lb { get => _Lb; set { _Lb = value; OnPropertyChanged(); } }
        private double _Y0;
        public double Y0 { get => _Y0; set { _Y0 = value; OnPropertyChanged(); } }
        private double _X0;
        public double X0 { get => _X0; set { _X0 = value; OnPropertyChanged(); } }
        private List<LocationBarModel> _Location;
        public List<LocationBarModel> Location { get => _Location; set { _Location = value; OnPropertyChanged(); } }
        public SingleMainTopBarModel(int numberBar, RebarBarModel bar, double la, double lb, double x, double y,double temp)
        {
            NumberBar = numberBar;
            Bar = bar;
            La = la;
            Lb = lb;
            X0 = x;
            Y0 = y;
            Temp = temp;
        }
        public void GetLength()
        {
            double a = 0;
            for (int i = 1; i < Location.Count; i++)
            {
                a += Math.Sqrt((Location[i].X - Location[i - 1].X) * (Location[i].X - Location[i - 1].X) + (Location[i].Y - Location[i - 1].Y) * (Location[i].Y - Location[i - 1].Y));
            }
            Length = Math.Round(a - La - Lb, 0);
        }
        public void GetLocation(List<LocationBarModel> location)
        {
            if (Location == null)
            {
                Location = new List<LocationBarModel>();
                for (int i = 0; i < location.Count; i++)
                {
                    Location.Add(location[i]);
                }
            }
            else
            {
                Location.Clear();
                Location = new List<LocationBarModel>();
                for (int i = 0; i < location.Count; i++)
                {
                    Location.Add(location[i]);
                }
            }
        }
        public void SetLocation(double ds,double c)
        {
            for (int i = 0; i < Location.Count; i++)
            {
                Location[i].Y = Temp +c+ds + Bar.Diameter / 2;
            }
        }
        private double GetY0Min()
        {
            double a = Location[0].Y;
            for (int i = 0; i < Location.Count; i++)
            {
                if (a > Location[i].Y) { a = Location[i].Y; }
            }
            return a;
        }
        public void Refresh(List<InfoModel> infoModels, List<NodeModel> AllNodeModel, double c, double ds)
        {
           
            List<LocationBarModel> location = ProcessInfoBeamRebar.GetLocationSingleMainTopBarModels(infoModels, AllNodeModel, c,La,Lb);
            GetLocation(location);
            for (int i = 0; i < Location.Count; i++)
            {
                Location[i].Y += ds + Bar.Diameter / 2;
            }
            X0 = Location[1].X;
            Y0 = Location[1].Y;
        }
        #region Create Maintop bar
        public bool ConditionMidAddTop()
        {
            for (int i = 1; i < Location.Count; i++)
            {
                if (!PointModel.AreEqual(Location[i].Y , Location[0].Y))
                {
                    return false;
                }
            }
            return true;
        }
        private bool ConditionOffset(double x0, out double y0)
        {
            y0 = 0;
            for (int j = 1; j < Location.Count; j++)
            {
                if (Location[j - 1].Y == Location[j].Y)
                {
                    if (Location[j].X > x0)
                    {
                        y0 = Location[j].Y;
                        return false;
                    }
                }
            }
            return true;
        }
        private List<Curve> GetCurve(Document document, PlanarFace planarFace, UnitProject unit, double c, double dsmax, InfoModel infoModel0)
        {
            List<Curve> curves = new List<Curve>();
            double cover = unit.Convert(c);
            double ds = unit.Convert(dsmax);
            double d = unit.Convert(Bar.Diameter);
            double zOffset = unit.Convert(infoModel0.zOffset);
            List<XYZ> vector = new List<XYZ>();
            XYZ p1 = PointModel.ProjectToPlane(infoModel0.LeftRightPlanar[0].Origin, infoModel0.TopBottomPlanar[0]);
            ///can sua
            XYZ p2 = p1 + (cover + ds + d / 2) * (-1) * infoModel0.LeftRightPlanar[0].FaceNormal;
            XYZ p0 = PointModel.ProjectToPlane(p2, planarFace);
            if (ConditionMidAddTop())
            {
                double x1 = unit.Convert(Location[0].X);
                double y1 = unit.Convert(Location[0].Y);
                double x2 = unit.Convert(Location[Location.Count - 1].X);
                double y2 = unit.Convert(Location[Location.Count - 1].Y);
                XYZ p1x = p0 + x1 * (-1) * planarFace.FaceNormal;
                XYZ p1y = p1x + (y1 + zOffset) * (-1) * XYZ.BasisZ;
                XYZ p2x = p0 + x2 * (-1) * planarFace.FaceNormal;
                XYZ p2y = p2x + (y2 + zOffset) * (-1) * XYZ.BasisZ;
                Curve c1 = Line.CreateBound(p1y, p2y);
                curves.Add(c1);
            }
            else
            {
                for (int i = 1; i < Location.Count; i++)
                {
                    double x1 = unit.Convert(Location[i - 1].X);
                    double y1 = unit.Convert(Location[i - 1].Y);
                    double x2 = unit.Convert(Location[i].X);
                    double y2 = unit.Convert(Location[i].Y);
                    XYZ p1x = p0 + x1 * (-1) * planarFace.FaceNormal;
                    XYZ p1y = p1x + (y1 + zOffset) * (-1) * XYZ.BasisZ;
                    XYZ p2x = p0 + x2 * (-1) * planarFace.FaceNormal;
                    XYZ p2y = p2x + (y2 + zOffset) * (-1) * XYZ.BasisZ;
                    Curve c1 = Line.CreateBound(p1y, p2y);
                    curves.Add(c1);
                }
            }

            return curves;
        }
        public void CreateMaintopBar(Document document, PlanarFace planarFace, UnitProject unit, double c, double dsmax, InfoModel infoModel, SettingModel settingModel)
        {
            List<Curve> curves = GetCurve(document, planarFace, unit, c, dsmax, infoModel);
            Bar.Rebar = Rebar.CreateFromCurves(document, RebarStyle.Standard, Bar.RebarBarType, null, null, infoModel.Element, (-1) * infoModel.LeftRightPlanar[0].FaceNormal, curves, RebarHookOrientation.Right, RebarHookOrientation.Right, true, true);
            RebarShapeDrivenAccessor rebarShape1 = Bar.Rebar.GetShapeDrivenAccessor();
            double s = unit.Convert((infoModel.b - 2 * c - 2 * dsmax - Bar.Diameter));
            rebarShape1.SetLayoutAsFixedNumber(NumberBar, s, true, true, true);
            Bar.SetPartitionRebar(settingModel.BeamsName);
        }
        #endregion
        #region Tag Bar
        public void CreateTagRebarDetailMainTop(Autodesk.Revit.DB.View view, Document document, UnitProject unit, List<InfoModel> infoModels, SettingModel settingModel, PlanarFace planarFace0, double h0, double v0)
        {
            if (Location.Count <= 4)
            {

                for (int i = 0; i < infoModels.Count; i++)
                {
                    double x0 = (infoModels[i].startPosition + infoModels[i].endPosition) / 2 + infoModels[i].b;
                    double y0 = GetY0Min();
                    Bar.CreateTagRebarDetailTop(view, document, unit, infoModels[0], settingModel, planarFace0, x0, y0, h0, v0);

                }
            }
            else
            {
                for (int i = 0; i < infoModels.Count; i++)
                {
                    double x0 = (infoModels[i].startPosition + infoModels[i].endPosition) / 2 + infoModels[i].b;
                    double y0;
                    if (!ConditionOffset(x0, out y0))
                    {
                        Bar.CreateTagRebarDetailTop(view, document, unit, infoModels[0], settingModel, planarFace0, x0, y0, h0, v0);
                    }

                }
            }
        }
        public void CreateTagRebarSection(List<SectionBeamView> SectionBeamViews, Document document, UnitProject unit, List<InfoModel> infoModels, PlanarFace planarFace0, SettingModel settingModel, double tagH0, double tagV0)
        {
            if (Location.Count <= 4)
            {
                for (int i = 0; i < SectionBeamViews.Count; i++)
                {
                    Bar.CreateTagRebarSectionUp(SectionBeamViews[i].StartView, document, unit, infoModels[i], planarFace0, settingModel, Location[1].Y, tagH0, tagV0);
                    Bar.CreateTagRebarSectionUp(SectionBeamViews[i].MidView, document, unit, infoModels[i], planarFace0, settingModel, Location[1].Y, tagH0, tagV0);
                    Bar.CreateTagRebarSectionUp(SectionBeamViews[i].EndView, document, unit, infoModels[i], planarFace0, settingModel, Location[1].Y, tagH0, tagV0);
                }
            }
            else
            {
                for (int i = 0; i < SectionBeamViews.Count; i++)
                {
                    double x0start = PointModel.DistanceTo2(planarFace0, SectionBeamViews[i].StartView.Origin, document);
                    double y0start;
                    if (!ConditionOffset(x0start, out y0start))
                    {
                        Bar.CreateTagRebarSectionUp(SectionBeamViews[i].StartView, document, unit, infoModels[i], planarFace0, settingModel, y0start, tagH0, tagV0);
                    }
                    double x0Mid = PointModel.DistanceTo2(planarFace0, SectionBeamViews[i].MidView.Origin, document);
                    double y0Mid;
                    if (!ConditionOffset(x0Mid, out y0Mid))
                    {
                        Bar.CreateTagRebarSectionUp(SectionBeamViews[i].MidView, document, unit, infoModels[i], planarFace0, settingModel, y0Mid, tagH0, tagV0);
                    }
                    double x0End = PointModel.DistanceTo2(planarFace0, SectionBeamViews[i].EndView.Origin, document);
                    double y0End;
                    if (!ConditionOffset(x0start, out y0End))
                    {
                        Bar.CreateTagRebarSectionUp(SectionBeamViews[i].EndView, document, unit, infoModels[i], planarFace0, settingModel, y0End, tagH0, tagV0);
                    }
                }
            }
        }
        #endregion
        
    }
}
