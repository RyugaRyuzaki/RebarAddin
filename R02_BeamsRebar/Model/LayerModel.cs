using WpfCustomControls;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;
namespace R02_BeamsRebar
{
    public class LayerModel : BaseViewModel
    {
        private int _Layer;
        public int Layer { get => _Layer; set { _Layer = value; OnPropertyChanged(); } }
        private RebarBarModel _Bar;
        public RebarBarModel Bar { get => _Bar; set { _Bar = value; OnPropertyChanged(); } }
        private int _NumberBar;
        public int NumberBar { get => _NumberBar; set { _NumberBar = value; OnPropertyChanged(); } }
        private double _L;
        public double L { get => _L; set { _L = value; OnPropertyChanged(); } }
        private double _La;
        public double La { get => _La; set { _La = value; OnPropertyChanged(); } }
        private double _Y0;
        public double Y0 { get => _Y0; set { _Y0 = value; OnPropertyChanged(); } }
        private double _X0;
        public double X0 { get => _X0; set { _X0 = value; OnPropertyChanged(); } }
        private List<LocationBarModel> _Location;
        public List<LocationBarModel> Location { get => _Location; set { _Location = value; OnPropertyChanged(); } }
        public LayerModel(int layer, RebarBarModel bar, int numberBar, double l, double la)
        {
            Layer = layer;
            Bar = bar;
            NumberBar = numberBar;
            L = l;
            La = la;
        }
        public void GetLocationStart()
        {
            if (Location == null)
            {
                Location = new List<LocationBarModel>();
                if (La != 0)
                {
                    Location.Add(new LocationBarModel(X0, Y0 + La));
                    Location.Add(new LocationBarModel(X0, Y0));
                    Location.Add(new LocationBarModel(X0 + L, Y0));
                }
                else
                {
                    Location.Add(new LocationBarModel(X0, Y0));
                    Location.Add(new LocationBarModel(X0 + L, Y0));
                }
            }
            else
            {
                Location.Clear();
                Location = new List<LocationBarModel>();
                if (La != 0)
                {
                    Location.Add(new LocationBarModel(X0, Y0 + La));
                    Location.Add(new LocationBarModel(X0, Y0));
                    Location.Add(new LocationBarModel(X0 + L, Y0));
                }
                else
                {
                    Location.Add(new LocationBarModel(X0, Y0));
                    Location.Add(new LocationBarModel(X0 + L, Y0));
                }
            }
        }
        public void GetLocationEnd()
        {
            if (Location == null)
            {
                Location = new List<LocationBarModel>();
                if (La != 0)
                {
                    Location.Add(new LocationBarModel(X0 - L, Y0));
                    Location.Add(new LocationBarModel(X0, Y0));
                    Location.Add(new LocationBarModel(X0, Y0 + La));
                }
                else
                {
                    Location.Add(new LocationBarModel(X0 - L, Y0));
                    Location.Add(new LocationBarModel(X0, Y0));
                }
            }
            else
            {
                Location.Clear();
                Location = new List<LocationBarModel>();
                if (La != 0)
                {
                    Location.Add(new LocationBarModel(X0 - L, Y0));
                    Location.Add(new LocationBarModel(X0, Y0));
                    Location.Add(new LocationBarModel(X0, Y0 + La));
                }
                else
                {
                    Location.Add(new LocationBarModel(X0 - L, Y0));
                    Location.Add(new LocationBarModel(X0, Y0));
                }
            }
        }
        public void GetLocatoionMid(double width, double c, double offset, bool isoffset)
        {
            if (Location == null)
            {
                Location = new List<LocationBarModel>();

            }
            else
            {
                Location.Clear();
                Location = new List<LocationBarModel>();
            }
            if (isoffset)
            {
                Location.Add(new LocationBarModel(X0 - L - width / 2, Y0 + offset / 2));
                Location.Add(new LocationBarModel(X0 - width / 2 + c, Y0 + offset / 2));
                //Location.Add(new LocationBarModel(X0, Y0));
                Location.Add(new LocationBarModel(X0 + width / 2 - c, Y0 - offset / 2));
                Location.Add(new LocationBarModel(X0 + La + width / 2, Y0 - offset / 2));
            }
            else
            {
                Location.Add(new LocationBarModel(X0 - L - width / 2, Y0));
                Location.Add(new LocationBarModel(X0 - width / 2 + c, Y0));
                //Location.Add(new LocationBarModel(X0, Y0));
                Location.Add(new LocationBarModel(X0 + width / 2 - c, Y0));
                Location.Add(new LocationBarModel(X0 + La + width / 2, Y0));
            }
        }
        public void GetLocationStartBottom()
        {
            if (Location == null)
            {
                Location = new List<LocationBarModel>();
            }
            else
            {
                Location.Clear();
                Location = new List<LocationBarModel>();
            }
            if (La != 0)
            {
                Location.Add(new LocationBarModel(X0, Y0 - La));
                Location.Add(new LocationBarModel(X0, Y0));
                Location.Add(new LocationBarModel(X0 + L, Y0));
            }
            else
            {
                Location.Add(new LocationBarModel(X0, Y0));
                Location.Add(new LocationBarModel(X0 + L, Y0));
            }
        }
        public void GetLocationEndBottom()
        {
            if (Location == null)
            {
                Location = new List<LocationBarModel>();
            }
            else
            {
                Location.Clear();
                Location = new List<LocationBarModel>();
            }
            if (La != 0)
            {
                Location.Add(new LocationBarModel(X0, Y0 - La));
                Location.Add(new LocationBarModel(X0, Y0));
                Location.Add(new LocationBarModel(X0 - L, Y0));
            }
            else
            {
                Location.Add(new LocationBarModel(X0, Y0));
                Location.Add(new LocationBarModel(X0 - L, Y0));
            }
        }
        public void GetLocationMidBottom()
        {
            if (Location == null)
            {
                Location = new List<LocationBarModel>();
            }
            else
            {
                Location.Clear();
                Location = new List<LocationBarModel>();
            }
            Location.Add(new LocationBarModel(X0 - L, Y0));
            Location.Add(new LocationBarModel(X0, Y0));
            Location.Add(new LocationBarModel(X0 + La, Y0));
        }
        #region Create MainBottom bar
        public List<Curve> GetCurve(Document document, PlanarFace planarFace, UnitProject unit, double c, double dsmax, InfoModel infoModel0)
        {
            List<Curve> curves = new List<Curve>();
            double cover = unit.Convert(c);
            double ds = unit.Convert(dsmax);
            double d = unit.Convert(Bar.Diameter);
            double b = unit.Convert(infoModel0.b);
            double zOffset = unit.Convert(infoModel0.zOffset);
            List<XYZ> vector = new List<XYZ>();
            XYZ p1 = PointModel.ProjectToPlane(infoModel0.LeftRightPlanar[0].Origin, infoModel0.TopBottomPlanar[0]);
            XYZ p2 = null;
            if (NumberBar==1)
            {
                p2 = p1 + (b/2) * (-1) * infoModel0.LeftRightPlanar[0].FaceNormal;
            }
            else
            {
                p2 = p1 + (cover + ds + d / 2) * (-1) * infoModel0.LeftRightPlanar[0].FaceNormal;
            }
           
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
        public List<Curve> GetCurveMid(Document document, PlanarFace planarFace, UnitProject unit, double c, double dsmax, InfoModel infoModel0)
        {
            List<Curve> curves = new List<Curve>();
            double cover = unit.Convert(c);
            double ds = unit.Convert(dsmax);
            double d = unit.Convert(Bar.Diameter);
            double b = unit.Convert(infoModel0.b);
            double zOffset = unit.Convert(infoModel0.zOffset);
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
            if (ConditionMidAddTop())
            {
                double x1 = unit.Convert(Location[0].X);
                double y1 = unit.Convert(Location[0].Y);
                double x2 = unit.Convert(Location[Location.Count-1].X);
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
        public void CreateAddBar(Document document, PlanarFace planarFace, UnitProject unit, double c, double dsmax, InfoModel infoModel, SettingModel settingModel)
        {
            List<Curve> curves = GetCurve(document, planarFace, unit, c, dsmax, infoModel);
            Bar.Rebar = Rebar.CreateFromCurves(document, RebarStyle.Standard, Bar.RebarBarType, null, null, infoModel.Element, (-1) * infoModel.LeftRightPlanar[0].FaceNormal, curves, RebarHookOrientation.Right, RebarHookOrientation.Right, true, true);
            RebarShapeDrivenAccessor rebarShape1 = Bar.Rebar.GetShapeDrivenAccessor();
            double s = unit.Convert((infoModel.b - 2 * c - 2 * dsmax - Bar.Diameter));
            if (NumberBar!=1)
            {
                rebarShape1.SetLayoutAsFixedNumber(NumberBar, s, true, true, true);
            }
            else
            {
                
                rebarShape1.SetLayoutAsSingle();
            }
            

            Bar.SetPartitionRebar(settingModel.BeamsName);
        }
        public void CreateAddBarMid(Document document, PlanarFace planarFace, UnitProject unit, double c, double dsmax, InfoModel infoModel, SettingModel settingModel)
        {
            List<Curve> curves = GetCurveMid(document, planarFace, unit, c, dsmax, infoModel);
            Bar.Rebar = Rebar.CreateFromCurves(document, RebarStyle.Standard, Bar.RebarBarType, null, null, infoModel.Element, (-1) * infoModel.LeftRightPlanar[0].FaceNormal, curves, RebarHookOrientation.Right, RebarHookOrientation.Right, true, true);
            RebarShapeDrivenAccessor rebarShape1 = Bar.Rebar.GetShapeDrivenAccessor();
            double s = unit.Convert((infoModel.b - 2 * c - 2 * dsmax - Bar.Diameter));
            
            if (NumberBar != 1)
            {
                rebarShape1.SetLayoutAsFixedNumber(NumberBar, s, true, true, true);
            }
            else
            {
               
                rebarShape1.SetLayoutAsSingle();
            }
          
            Bar.SetPartitionRebar(settingModel.BeamsName);
        }

        public Reference GetAddTopRightReference(Autodesk.Revit.DB.View view, Document document, PlanarFace planarFace, UnitProject unit, double c, double dsmax, InfoModel infoModel)
        {
            List<Curve> curves = GetCurve(document, planarFace, unit, c, dsmax, infoModel);
            XYZ p1 = curves[curves.Count - 1].GetEndPoint(1);
            XYZ p2 = p1 + 0.01 * XYZ.BasisZ;
            Line line = Line.CreateBound(p1, p2);
            DetailCurve detailCurve = document.Create.NewDetailCurve(view, line);
            return detailCurve.GeometryCurve.Reference;
        }
        public Reference GetAddTopLeftReference(Autodesk.Revit.DB.View view, Document document, PlanarFace planarFace, UnitProject unit, double c, double dsmax, InfoModel infoModel)
        {
            List<Curve> curves = GetCurve(document, planarFace, unit, c, dsmax, infoModel);
            XYZ p1 = curves[0].GetEndPoint(0);
            XYZ p2 = p1 + 0.01 * XYZ.BasisZ;
            Line line = Line.CreateBound(p1, p2);
            DetailCurve detailCurve = document.Create.NewDetailCurve(view, line);
            return detailCurve.GeometryCurve.Reference;
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
        #endregion
        #region Tag Bar
        public void CreateTagRebarDetailTopStart(Autodesk.Revit.DB.View view, Document document, UnitProject unit, InfoModel infoModel0, NodeModel nodeModel0, SettingModel settingModel, PlanarFace planarFace0, double h0, double v0, double detal0)
        {
            double x0 = Location[Location.Count - 1].X - Math.Abs(Location[Location.Count - 1].X - nodeModel0.End) / 2;
            double y0 = Location[Location.Count - 1].Y;
            Bar.CreateTagRebarDetailTop(view, document, unit, infoModel0, settingModel, planarFace0, x0, y0, h0, v0 - detal0);
        }
        public void CreateTagRebarDetailTopEnd(Autodesk.Revit.DB.View view, Document document, UnitProject unit, InfoModel infoModel0, NodeModel nodeModeEnd, SettingModel settingModel, PlanarFace planarFace0, double h0, double v0, double detal0)
        {
            double x0 = Location[0].X + Math.Abs(Location[0].X - nodeModeEnd.Start) / 2;
            double y0 = Location[0].Y;
            Bar.CreateTagRebarDetailTop(view, document, unit, infoModel0, settingModel, planarFace0, x0, y0, h0, v0 - detal0);
        }
        public void CreateTagRebarDetailTopMid(Autodesk.Revit.DB.View view, Document document, UnitProject unit, InfoModel infoModel0, NodeModel nodeMode, SettingModel settingModel, PlanarFace planarFace0, double h0, double v0, double detal0)
        {
            //Left
            double x0Left = Location[0].X + Math.Abs(Location[0].X - nodeMode.Start) / 2;
            double y0Left = Location[0].Y;
            Bar.CreateTagRebarDetailTop(view, document, unit, infoModel0, settingModel, planarFace0, x0Left, y0Left, -h0, v0 - detal0);
            //Right
            double x0Right = Location[Location.Count - 1].X - Math.Abs(Location[Location.Count - 1].X - nodeMode.End) / 2;
            double y0Right = Location[Location.Count - 1].Y;
            Bar.CreateTagRebarDetailTop(view, document, unit, infoModel0, settingModel, planarFace0, x0Right, y0Right, h0, v0 - detal0);
        }
        public void CreateTagRebarDetailBottomStart(Autodesk.Revit.DB.View view, Document document, UnitProject unit, InfoModel infoModel0, InfoModel infoModel, SettingModel settingModel, PlanarFace planarFace0, double h0, double v0, double detal0)
        {
            double x0 = Location[Location.Count - 1].X - Math.Abs(Location[Location.Count - 1].X - infoModel.startPosition) / 2;
            double y0 = Location[Location.Count - 1].Y;
            Bar.CreateTagRebarDetailBottom(view, document, unit, infoModel0, settingModel, planarFace0, x0, y0, h0, v0 - detal0);
        }
        public void CreateTagRebarDetailBottomEnd(Autodesk.Revit.DB.View view, Document document, UnitProject unit, InfoModel infoModel0, InfoModel infoModel, SettingModel settingModel, PlanarFace planarFace0, double h0, double v0, double detal0)
        {
            double x0 = Location[0].X + Math.Abs(Location[0].X - infoModel.endPosition) / 2;
            double y0 = Location[0].Y;
            Bar.CreateTagRebarDetailBottom(view, document, unit, infoModel0, settingModel, planarFace0, x0, y0, h0, v0 - detal0);
        }
        public void CreateTagRebarDetailBottomMid(Autodesk.Revit.DB.View view, Document document, UnitProject unit, InfoModel infoModel0, InfoModel infoModel, SettingModel settingModel, PlanarFace planarFace0, double h0, double v0, double detal0)
        {
            double x0 = (infoModel.startPosition + infoModel.endPosition) / 2 + infoModel.b * 2;
            double y0 = Location[0].Y;
            Bar.CreateTagRebarDetailBottom(view, document, unit, infoModel0, settingModel, planarFace0, x0, y0, h0, v0 - detal0);
        }
        #endregion
    }
}
