using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace R02_BeamsRebar
{
    public class SpecialBarModel : BaseViewModel
    {
        private int _NumberNode;
        public int NumberNode { get => _NumberNode; set { _NumberNode = value; OnPropertyChanged(); } }
        private int _Span;
        public int Span { get => _Span; set { _Span = value; OnPropertyChanged(); } }
        private bool _IsSP;
        public bool IsSP { get => _IsSP; set { _IsSP = value; OnPropertyChanged(); } }
        private RebarBarModel _BarSP;
        public RebarBarModel BarSP { get => _BarSP; set { _BarSP = value; OnPropertyChanged(); } }
        private int _NumberSP;
        public int NumberSP { get => _NumberSP; set { _NumberSP = value; OnPropertyChanged(); } }
        private double _L1;
        public double L1 { get => _L1; set { _L1 = value; OnPropertyChanged(); } }
        private double _L2;
        public double L2 { get => _L2; set { _L2 = value; OnPropertyChanged(); } }
        private double _L3;
        public double L3 { get => _L3; set { _L3 = value; OnPropertyChanged(); } }
        private double _X0;
        public double X0 { get => _X0; set { _X0 = value; OnPropertyChanged(); } }
        private double _Y0;
        public double Y0 { get => _Y0; set { _Y0 = value; OnPropertyChanged(); } }
        private List<LocationBarModel> _LocationSP;
        public List<LocationBarModel> LocationSP { get => _LocationSP; set { _LocationSP = value; OnPropertyChanged(); } }
        private List<List<LocationBarModel>> _LocationST;
        public List<List<LocationBarModel>> LocationST { get => _LocationST; set { _LocationST = value; OnPropertyChanged(); } }
        private bool _IsST;
        public bool IsST { get => _IsST; set { _IsST = value; OnPropertyChanged(); } }
        private RebarBarModel _BarST;
        public RebarBarModel BarST { get => _BarST; set { _BarST = value; OnPropertyChanged(); } }
        private int _NumberST;
        public int NumberST { get => _NumberST; set { _NumberST = value; OnPropertyChanged(); } }
        private double _a;
        public double a { get => _a; set { _a = value; OnPropertyChanged(); } }
        public SpecialBarModel(int numberNode, int span, bool isSP, RebarBarModel barSP, int numberSP, double l1, double l2, double l3, bool isST, RebarBarModel barST, int numberST, double A)
        {
            NumberNode = numberNode;
            Span = span;
            IsSP = isSP;
            BarSP = barSP;
            NumberSP = numberSP;
            L1 = l1; L2 = l2; L3 = l3;
            IsST = isST;
            BarST = barST;
            NumberST = numberST;
            a = A;
        }
        public void GetLocationSP()
        {
            if (IsSP)
            {
                if (LocationSP == null)
                {
                    LocationSP = new List<LocationBarModel>();
                }
                else
                {
                    LocationSP.Clear();
                    LocationSP = new List<LocationBarModel>();
                }
                LocationSP.Add(new LocationBarModel(X0 - L3 / 2 - L2 - L1, Y0));
                LocationSP.Add(new LocationBarModel(X0 - L3 / 2 - L2, Y0));
                LocationSP.Add(new LocationBarModel(X0 - L3 / 2, Y0 + L2));
                LocationSP.Add(new LocationBarModel(X0 + L3 / 2, Y0 + L2));
                LocationSP.Add(new LocationBarModel(X0 + L3 / 2 + L2, Y0));
                LocationSP.Add(new LocationBarModel(X0 + L3 / 2 + L2 + L1, Y0));
            }
            else
            {
                if (LocationSP != null)
                {
                    LocationSP.Clear();
                }
            }

        }
        public void GetLocationST(double h, double c, double ds, double dTop)
        {
            if (IsST)
            {
                if (LocationST == null)
                {
                    LocationST = new List<List<LocationBarModel>>();
                }
                else
                {
                    LocationST.Clear();
                    LocationST = new List<List<LocationBarModel>>();
                }
                for (int i = 0; i < NumberST; i++)
                {
                    LocationST.Add(new List<LocationBarModel>(2));
                }
                for (int j = 0; j < NumberST / 2; j++)
                {
                    LocationST[j].Add(new LocationBarModel(X0 - L3 / 2 - j * a, Y0 - ds - dTop / 2));
                    LocationST[j].Add(new LocationBarModel(X0 - L3 / 2 - j * a, h - c));
                    LocationST[j + NumberST / 2].Add(new LocationBarModel(X0 + L3 / 2 + j * a, Y0 - ds - dTop / 2));
                    LocationST[j + NumberST / 2].Add(new LocationBarModel(X0 + L3 / 2 + j * a, h - c));
                }
            }
            else
            {
                if (LocationST != null)
                {
                    LocationST.Clear();
                }
            }

        }
        public void GetX0Y0(List<InfoModel> infoModels, SpecialNodeModel SpecialNodeModel, double c, double ds, double dTop)
        {
            if (IsSP||IsST)
            {
                InfoModel infoModel = infoModels.Where(x => x.NumberSpan == SpecialNodeModel.NumberSpan).FirstOrDefault();
                X0 = SpecialNodeModel.Mid;
                Y0 = Math.Abs(infoModel.zOffset) + c + ds + dTop / 2;
            }
        }
        #region Create Special bar
        private List<Curve> GetCurve(Document document, PlanarFace planarFace, UnitProject unit, double c, double dsmax, InfoModel infoModel0)
        {
            List<Curve> curves = new List<Curve>();
            double cover = unit.Convert(c);
            double ds = unit.Convert(dsmax);
            double d = unit.Convert(BarSP.Diameter);
            double b = unit.Convert(infoModel0.b);
            List<XYZ> vector = new List<XYZ>();
            XYZ p1 = PointModel.ProjectToPlane(infoModel0.LeftRightPlanar[0].Origin, infoModel0.TopBottomPlanar[0]);
            XYZ p2 = p1 + (cover + ds + d / 2) * (-1) * infoModel0.LeftRightPlanar[0].FaceNormal;

            XYZ p0 = PointModel.ProjectToPlane(p2, planarFace);
            for (int i = 1; i < LocationSP.Count; i++)
            {
                double x1 = unit.Convert(LocationSP[i - 1].X);
                double y1 = unit.Convert(LocationSP[i - 1].Y);
                double x2 = unit.Convert(LocationSP[i].X);
                double y2 = unit.Convert(LocationSP[i].Y);
                XYZ p1x = p0 + x1 * (-1) * planarFace.FaceNormal;
                XYZ p1y = p1x + y1 * (-1) * XYZ.BasisZ;
                XYZ p2x = p0 + x2 * (-1) * planarFace.FaceNormal;
                XYZ p2y = p2x + y2 * (-1) * XYZ.BasisZ;
                Curve c1 = Line.CreateBound(p1y, p2y);
                curves.Add(c1);
            }
            return curves;
        }

        public void CreateSpecialBar(Document document, PlanarFace planarFace, UnitProject unit, double c, double dsmax, InfoModel infoModel, SettingModel settingModel)
        {
            List<Curve> curves = GetCurve(document, planarFace, unit, c, dsmax, infoModel);
            BarSP.Rebar = Rebar.CreateFromCurves(document, RebarStyle.Standard, BarSP.RebarBarType, null, null, infoModel.Element, (-1) * infoModel.LeftRightPlanar[0].FaceNormal, curves, RebarHookOrientation.Right, RebarHookOrientation.Right, true, true);
            RebarShapeDrivenAccessor rebarShape1 = BarSP.Rebar.GetShapeDrivenAccessor();
            double s = unit.Convert((infoModel.b - 2 * c - 2 * dsmax - BarSP.Diameter));
            rebarShape1.SetLayoutAsFixedNumber(NumberSP, s, true, true, true);
            BarSP.SetPartitionRebar(settingModel.BeamsName);
        }

        #endregion
        #region StirrupSpecial
        private List<XYZ> GetOriginVectorStirrup(UnitProject unit, double c, double location, double x0, InfoModel infoModel)
        {
            List<XYZ> vector = new List<XYZ>();
            XYZ p3 = PointModel.ProjectToPlane(infoModel.LeftRightPlanar[0].Origin, infoModel.TopBottomPlanar[1]);
            XYZ p4 = PointModel.ProjectToPlane(infoModel.LeftRightPlanar[1].Origin, infoModel.TopBottomPlanar[1]);
            double cover = unit.Convert(c);
            double x = unit.Convert(x0);
            double location0 = unit.Convert(location);
            XYZ v12 = p4 - p3;
            XYZ p5 = p3 + (x + cover) * v12;
            XYZ p6 = p5 + cover * XYZ.BasisZ;
            XYZ p60 = PointModel.ProjectToPlane(p6, infoModel.StartPlanar);
            XYZ p61 = null;
            if (infoModel.ConsolLeft)
            {
                p61 = p60 + location0 * (-1) * infoModel.StartPlanar.FaceNormal;
            }
            else
            {
                p61 = p60 + location0 * infoModel.StartPlanar.FaceNormal;
            }

            vector.Add(p61);
            Transform t = Transform.CreateRotation(XYZ.BasisZ, infoModel.StartPlanar.FaceNormal.AngleTo(new XYZ(0, 1, 0)));
            vector.Add(t.BasisZ);
            vector.Add(-1 * t.BasisX);
            return vector;
        }
        private List<XYZ> GetScaleBoxStirrup(UnitProject unit, double c, double b0, double x0, InfoModel infoModel)
        {
            List<XYZ> vector = new List<XYZ>();
            XYZ p3 = PointModel.ProjectToPlane(infoModel.LeftRightPlanar[0].Origin, infoModel.TopBottomPlanar[1]);
            XYZ p4 = PointModel.ProjectToPlane(infoModel.LeftRightPlanar[1].Origin, infoModel.TopBottomPlanar[1]);
            double cover = unit.Convert(c);
            double x = unit.Convert(x0);
            double b = unit.Convert(b0);
            double h = unit.Convert(infoModel.h);
            XYZ v12 = p4 - p3;
            XYZ p5 = p3 + (x + cover) * (-1) * infoModel.LeftRightPlanar[0].FaceNormal;
            XYZ p5a = p3 + (b - cover) * (-1) * infoModel.LeftRightPlanar[0].FaceNormal;
            XYZ pb = p5a - p5;
            vector.Add(pb);
            XYZ p6 = p5 + cover * XYZ.BasisZ;
            XYZ p6a = p5 + (h - cover) * XYZ.BasisZ;
            XYZ ph = p6a - p6;
            vector.Add(ph);
            return vector;
        }
        public void CreateStirrupSpecial(Document document, UnitProject unit, double c, InfoModel infoModel, SpecialNodeModel specialNodeModel, SettingModel settingModel)
        {
            BarST.GetRebarShape(document, "Stirrup", settingModel);
            double location1 = specialNodeModel.Mid - L3 / 2 - a * (NumberST / 2 - 1) - infoModel.startPosition;
            double s = unit.Convert(a);
            List<XYZ> origin1 = GetOriginVectorStirrup(unit, c, location1, 0, infoModel);
            Rebar bar1 = Rebar.CreateFromRebarShape(document, BarST.RebarShape, BarST.RebarBarType, infoModel.Element, origin1[0], origin1[1], origin1[2]);
            BarST.RebarStirrup.Add(bar1);
            RebarShapeDrivenAccessor rebarShape1 = bar1.GetShapeDrivenAccessor();
            List<XYZ> vector1 = GetScaleBoxStirrup(unit, c, infoModel.b, 0, infoModel);
            rebarShape1.ScaleToBox(origin1[0], vector1[0], vector1[1]);
            rebarShape1.SetLayoutAsNumberWithSpacing(NumberST / 2, s, false, true, true);
            double location2 = specialNodeModel.Mid + L3 / 2 - infoModel.startPosition;
            List<XYZ> origin2 = GetOriginVectorStirrup(unit, c, location2, 0, infoModel);
            Rebar bar2 = Rebar.CreateFromRebarShape(document, BarST.RebarShape, BarST.RebarBarType, infoModel.Element, origin2[0], origin2[1], origin2[2]);
            BarST.RebarStirrup.Add(bar2);
            RebarShapeDrivenAccessor rebarShape2 = bar2.GetShapeDrivenAccessor();
            List<XYZ> vector2 = GetScaleBoxStirrup(unit, c, infoModel.b, 0, infoModel);
            rebarShape2.ScaleToBox(origin2[0], vector2[0], vector2[1]);
            rebarShape2.SetLayoutAsNumberWithSpacing(NumberST / 2, s, false, true, true);
            BarST.SetPartitionRebarStirrup(settingModel.BeamsName);
        }
        private XYZ GetXYZTagStirrupDetail(UnitProject unit, InfoModel infoModel, double L10, double h0)
        {
            XYZ p1 = PointModel.ProjectToPlane(infoModel.LeftRightPlanar[0].Origin, infoModel.TopBottomPlanar[0]);
            XYZ p2 = PointModel.ProjectToPlane(infoModel.LeftRightPlanar[1].Origin, infoModel.TopBottomPlanar[0]);
            double h = unit.Convert(h0);
            double L1 = unit.Convert(L10);
            XYZ v12 = p2 - p1;
            XYZ p3 = p1 + 0.5 * v12;
            XYZ p4 = PointModel.ProjectToPlane(p3, infoModel.StartPlanar);
            XYZ p5 = null;
            if (infoModel.ConsolLeft)
            {
                p5 = p4 + L1 * (-1) * infoModel.StartPlanar.FaceNormal;
            }
            else
            {
                p5 = p4 + L1 * infoModel.StartPlanar.FaceNormal;
            }
            XYZ p6 = p5 + h * XYZ.BasisZ;
            return p6;
        }
        public void CreateTagStirrupDetail(Autodesk.Revit.DB.View view, Document document, UnitProject unit, InfoModel infoModel, SettingModel settingModel, SpecialNodeModel specialNodeModel)
        {
            double location1 = specialNodeModel.Mid - L3 / 2 - a * (NumberST / 2 - 1) / 2 - infoModel.startPosition;
            XYZ origin1 = GetXYZTagStirrupDetail(unit, infoModel, location1, -infoModel.h / 2);
            BarST.Tag = IndependentTag.Create(document, view.Id, new Reference(BarST.RebarStirrup[0]), false, BarST.Mode, BarST.Horizontal, origin1);
            BarST.Tag.ChangeTypeId(settingModel.SelectedStirrupTag.Id);
            double location2 = specialNodeModel.Mid + L3 / 2 - infoModel.startPosition + a * (NumberST / 2 - 1) / 2;
            XYZ origin2 = GetXYZTagStirrupDetail(unit, infoModel, location2, -infoModel.h / 2);
            BarST.Tag = IndependentTag.Create(document, view.Id, new Reference(BarST.RebarStirrup[1]), false, BarST.Mode, BarST.Horizontal, origin2);
            BarST.Tag.ChangeTypeId(settingModel.SelectedStirrupTag.Id);
        }
        #endregion
    }
}
