using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;
using WpfCustomControls;
namespace R02_BeamsRebar
{
    public class MainTopBarModel : BaseViewModel
    {
        private int _BarNumber;
        public int BarNumber { get => _BarNumber; set { _BarNumber = value; OnPropertyChanged(); } }
        private int _NumberBar;
        public int NumberBar { get => _NumberBar; set { _NumberBar = value; OnPropertyChanged(); } }
        private RebarBarModel _Bar;
        public RebarBarModel Bar { get => _Bar; set { _Bar = value; OnPropertyChanged(); } }
        private double _Length;
        public double Length { get => _Length; set { _Length = value; OnPropertyChanged(); } }
        private double _La;
        public double La { get => _La; set { _La = value; OnPropertyChanged(); } }
        private double _Exa;
        public double Exa { get => _Exa; set { _Exa = value; OnPropertyChanged(); } }
        private double _Lb;
        public double Lb { get => _Lb; set { _Lb = value; OnPropertyChanged(); } }
        private double _Exb;
        public double Exb { get => _Exb; set { _Exb = value; OnPropertyChanged(); } }
        private double _Y0;
        public double Y0 { get => _Y0; set { _Y0 = value; OnPropertyChanged(); } }
        private double _X0;
        public double X0 { get => _X0; set { _X0 = value; OnPropertyChanged(); } }
        private double _Temp;
        public double Temp { get => _Temp; set { _Temp = value; OnPropertyChanged(); } }
        private List<LocationBarModel> _Location;
        public List<LocationBarModel> Location { get => _Location; set { _Location = value; OnPropertyChanged(); } }
        public MainTopBarModel(int barNumber, int numberBar, RebarBarModel bar, double length, double la, double exa, double lb, double exb, double x, double y, double temp)
        {
            BarNumber = barNumber;
            NumberBar = numberBar;
            Bar = bar;
            Length = length;
            La = la;
            if (La == 0)
            {
                Exa = exa;
            }
            else
            {
                Exa = 0;
            }

            Lb = lb;
            if (Lb == 0)
            {
                Exb = exb;
            }
            else
            {
                Exb = 0;
            }
            X0 = x;
            Y0 = y;
            Temp = temp;
            GetLocationBarModels();
        }
        public void GetLength()
        {
            double a = 0;
            for (int i = 1; i < Location.Count; i++)
            {
                a += Math.Sqrt((Location[i].X - Location[i - 1].X) * (Location[i].X - Location[i - 1].X) + (Location[i].Y - Location[i - 1].Y) * (Location[i].Y - Location[i - 1].Y));
            }
            Length = a - La - Lb;
        }
        public void GetLocationBarModels()
        {
            if (Location == null)
            {
                Location = new List<LocationBarModel>();
                if (La == 0)
                {
                    if (Lb == 0)
                    {
                        if (Exa == 0)
                        {
                            if (Exb == 0)
                            {
                                Location.Add(new LocationBarModel(X0, Y0));
                                Location.Add(new LocationBarModel(X0 + Length, Y0));
                            }
                            else
                            {
                                Location.Add(new LocationBarModel(X0, Y0));
                                //Location.Add(new LocationBarModel(X0 + Length, Y0));
                                Location.Add(new LocationBarModel(X0 + Length + Exb, Y0));
                            }
                        }
                        else
                        {
                            if (Exb == 0)
                            {
                                Location.Add(new LocationBarModel(X0 - Exa, Y0));
                                //Location.Add(new LocationBarModel(X0, Y0));
                                Location.Add(new LocationBarModel(X0 + Length, Y0));
                            }
                            else
                            {
                                Location.Add(new LocationBarModel(X0 - Exa, Y0));
                                //Location.Add(new LocationBarModel(X0, Y0));
                                //Location.Add(new LocationBarModel(X0 + Length, Y0));
                                Location.Add(new LocationBarModel(X0 + Length + Exb, Y0));
                            }
                        }
                    }
                    else
                    {
                        if (Exa == 0)
                        {
                            Location.Add(new LocationBarModel(X0, Y0));
                            Location.Add(new LocationBarModel(X0 + Length, Y0));
                            Location.Add(new LocationBarModel(X0 + Length, Y0 + Lb));
                        }
                        else
                        {
                            Location.Add(new LocationBarModel(X0 - Exa, Y0));
                            //Location.Add(new LocationBarModel(X0, Y0));
                            Location.Add(new LocationBarModel(X0 + Length, Y0));
                            Location.Add(new LocationBarModel(X0 + Length, Y0 + Lb));
                        }
                    }
                }
                else
                {
                    if (Lb == 0)
                    {
                        if (Exb == 0)
                        {
                            Location.Add(new LocationBarModel(X0, Y0 + La));
                            Location.Add(new LocationBarModel(X0, Y0));
                            Location.Add(new LocationBarModel(X0 + Length, Y0));
                        }
                        else
                        {
                            Location.Add(new LocationBarModel(X0, Y0 + La));
                            Location.Add(new LocationBarModel(X0, Y0));
                            //Location.Add(new LocationBarModel(X0 + Length, Y0));
                            Location.Add(new LocationBarModel(X0 + Length + Exb, Y0));
                        }
                    }
                    else
                    {
                        Location.Add(new LocationBarModel(X0, Y0 + La));
                        Location.Add(new LocationBarModel(X0, Y0));
                        Location.Add(new LocationBarModel(X0 + Length, Y0));
                        Location.Add(new LocationBarModel(X0 + Length, Y0 + Lb));
                    }
                }
            }
            else
            {
                Location.Clear();
                Location = new List<LocationBarModel>();
                if (La == 0)
                {
                    if (Lb == 0)
                    {
                        if (Exa == 0)
                        {
                            if (Exb == 0)
                            {
                                Location.Add(new LocationBarModel(X0, Y0));
                                Location.Add(new LocationBarModel(X0 + Length, Y0));
                            }
                            else
                            {
                                Location.Add(new LocationBarModel(X0, Y0));
                                //Location.Add(new LocationBarModel(X0 + Length, Y0));
                                Location.Add(new LocationBarModel(X0 + Length + Exb, Y0));
                            }
                        }
                        else
                        {
                            if (Exb == 0)
                            {
                                Location.Add(new LocationBarModel(X0 - Exa, Y0));
                                //Location.Add(new LocationBarModel(X0, Y0));
                                Location.Add(new LocationBarModel(X0 + Length, Y0));
                            }
                            else
                            {
                                Location.Add(new LocationBarModel(X0 - Exa, Y0));
                                //Location.Add(new LocationBarModel(X0, Y0));
                                //Location.Add(new LocationBarModel(X0 + Length, Y0));
                                Location.Add(new LocationBarModel(X0 + Length + Exb, Y0));
                            }
                        }
                    }
                    else
                    {
                        if (Exa == 0)
                        {
                            Location.Add(new LocationBarModel(X0, Y0));
                            Location.Add(new LocationBarModel(X0 + Length, Y0));
                            Location.Add(new LocationBarModel(X0 + Length, Y0 + Lb));
                        }
                        else
                        {
                            Location.Add(new LocationBarModel(X0 - Exa, Y0));
                            //Location.Add(new LocationBarModel(X0, Y0));
                            Location.Add(new LocationBarModel(X0 + Length, Y0));
                            Location.Add(new LocationBarModel(X0 + Length, Y0 + Lb));
                        }
                    }
                }
                else
                {
                    if (Lb == 0)
                    {
                        if (Exb == 0)
                        {
                            Location.Add(new LocationBarModel(X0, Y0 + La));
                            Location.Add(new LocationBarModel(X0, Y0));
                            Location.Add(new LocationBarModel(X0 + Length, Y0));
                        }
                        else
                        {
                            Location.Add(new LocationBarModel(X0, Y0 + La));
                            Location.Add(new LocationBarModel(X0, Y0));
                            //Location.Add(new LocationBarModel(X0 + Length, Y0));
                            Location.Add(new LocationBarModel(X0 + Length + Exb, Y0));
                        }
                    }
                    else
                    {
                        Location.Add(new LocationBarModel(X0, Y0 + La));
                        Location.Add(new LocationBarModel(X0, Y0));
                        Location.Add(new LocationBarModel(X0 + Length, Y0));
                        Location.Add(new LocationBarModel(X0 + Length, Y0 + Lb));
                    }
                }
            }
        }
        public void Refresh(double ds, double c)
        {
            Y0 = Temp + c + ds + Bar.Diameter / 2;
        }
        public bool ConditionMidAddTop()
        {
            for (int i = 1; i < Location.Count; i++)
            {
                if (Location[i].Y != Location[0].Y)
                {
                    return false;
                }
            }
            return true;
        }
        #region Create Maintop bar
        private List<Curve> GetCurve(Document document, PlanarFace planarFace, UnitProject unit, double c, double dsmax, InfoModel infoModel0)
        {
            List<Curve> curves = new List<Curve>();
            double cover = unit.Convert(c);
            double ds = unit.Convert(dsmax);
            double d = unit.Convert(Bar.Diameter);
            double zOffset = unit.Convert(infoModel0.zOffset);
            List<XYZ> vector = new List<XYZ>();
            XYZ p1 = PointModel.ProjectToPlane(infoModel0.LeftRightPlanar[0].Origin, infoModel0.TopBottomPlanar[0]);
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
        #region Tag
        public void CreateTagRebarDetailMainTop(Autodesk.Revit.DB.View view, Document document, UnitProject unit, List<InfoModel> infoModels, SettingModel settingModel, PlanarFace planarFace0, double h0, double v0)
        {
            for (int i = 0; i < infoModels.Count; i++)
            {
                double x0 = (infoModels[i].startPosition + infoModels[i].endPosition) / 2 + infoModels[i].b;
                if (Location[Location.Count - 1].X > x0 && Location[0].X < x0)
                {
                    Bar.CreateTagRebarDetailTop(view, document, unit, infoModels[0], settingModel, planarFace0, x0, Location[1].Y, h0, v0);

                }
            }
        }
        public void CreateTagRebarSection(List<SectionBeamView> SectionBeamViews, Document document, UnitProject unit, List<InfoModel> infoModels, PlanarFace planarFace0, SettingModel settingModel, double tagH0, double tagV0)
        {
            for (int i = 0; i < infoModels.Count; i++)
            {
                double x0 = PointModel.DistanceTo2(planarFace0, SectionBeamViews[i].StartView.Origin, document);
                if (Location[Location.Count - 1].X > x0 && Location[0].X < x0)
                {
                    Bar.CreateTagRebarSectionUp(SectionBeamViews[i].StartView, document, unit, infoModels[i], planarFace0, settingModel, Location[1].Y, tagH0, tagV0);
                    Bar.CreateTagRebarSectionUp(SectionBeamViews[i].MidView, document, unit, infoModels[i], planarFace0, settingModel, Location[1].Y, tagH0, tagV0);
                    Bar.CreateTagRebarSectionUp(SectionBeamViews[i].EndView, document, unit, infoModels[i], planarFace0, settingModel, Location[1].Y, tagH0, tagV0);
                }
            }
        }
        #endregion
    }
}
