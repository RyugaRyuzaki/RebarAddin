
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace R02_BeamsRebar
{
    public class StirrupModel : BaseViewModel
    {
        private int _Type;
        public int Type { get => _Type; set { _Type = value; OnPropertyChanged(); } }
        private RebarBarModel _BarS;
        public RebarBarModel BarS { get => _BarS; set { _BarS = value; OnPropertyChanged(); } }
        private RebarBarModel _BarA;
        public RebarBarModel BarA { get => _BarA; set { _BarA = value; OnPropertyChanged(); } }
        private bool _Anti;
        public bool Anti { get => _Anti; set { _Anti = value; OnPropertyChanged(); } }
        private double _c;
        public double c { get => _c; set { _c = value; OnPropertyChanged(); } }
        private double _a;
        public double a { get => _a; set { _a = value; OnPropertyChanged(); } }
        private int _Na;
        public int Na { get => _Na; set { _Na = value; OnPropertyChanged(); } }
        private double _Sa;
        public double Sa { get => _Sa; set { _Sa = value; OnPropertyChanged(); } }
        public StirrupModel(int type, RebarBarModel barS, RebarBarModel barA, bool anti, double c, double a, int na, double sa)
        {
            Type = type;
            BarS = barS;
            BarA = barA;
            Anti = anti;
            this.c = c;
            this.a = a;
            Na = na;
            Sa = sa;
        }
        #region Create Stirrup
        private List<XYZ> GetOriginVectorStirrup(InfoModel infoModel,UnitProject unit, double location, double x0)
        {
            List<XYZ> vector = new List<XYZ>();
            XYZ p3 = PointModel.ProjectToPlane(infoModel.LeftRightPlanar[0].Origin, infoModel.TopBottomPlanar[1]);
            double cover = unit.Convert(c);
            double x = unit.Convert(x0);
            double location0 = unit.Convert(location);
            XYZ p5 = p3 + (x + cover) * (-1) * infoModel.LeftRightPlanar[0].FaceNormal;
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
            vector.Add(t.BasisX);
            return vector;
        }
        private List<XYZ> GetScaleBoxStirrup(InfoModel infoModel, UnitProject unit, double b0, double x0)
        {
            List<XYZ> vector = new List<XYZ>();
            XYZ p3 = PointModel.ProjectToPlane(infoModel.LeftRightPlanar[0].Origin, infoModel.TopBottomPlanar[1]);
            XYZ p4 = PointModel.ProjectToPlane(infoModel.LeftRightPlanar[1].Origin, infoModel.TopBottomPlanar[1]);
            double cover = unit.Convert(c);
            double x = unit.Convert(x0);
            double b = unit.Convert(b0);
            double h = unit.Convert(infoModel.h);
            //XYZ v12 = p4 - p3;
            //XYZ p5 = p3 + (cover + x) * v12;
            //XYZ p5a = p3 + (b - cover) * v12;
            XYZ p5 = p3 + (cover + x) * (-1) * infoModel.LeftRightPlanar[0].FaceNormal;
            XYZ p5a = p3 + (b - cover) * (-1) * infoModel.LeftRightPlanar[0].FaceNormal;
            XYZ pb = p5a - p5;
            vector.Add(pb);
            XYZ p6 = p5 + cover * XYZ.BasisZ;
            XYZ p6a = p5 + (h - cover) * XYZ.BasisZ;
            XYZ ph = p6a - p6;
            vector.Add(ph);
            return vector;
        }
        private void CreateStirrupType1Item(InfoModel infoModel, Document document, UnitProject unit, int n, double o1, double s, bool location, string name)
        {
            List<XYZ> origin = GetOriginVectorStirrup(infoModel,unit, o1, 0);
            Rebar bar = Rebar.CreateFromRebarShape(document, BarS.RebarShape, BarS.RebarBarType, infoModel.Element, origin[0], origin[1], origin[2]);
            BarS.RebarStirrup.Add(bar);
            RebarShapeDrivenAccessor rebarShape1 = bar.GetShapeDrivenAccessor();
            List<XYZ> vector = GetScaleBoxStirrup(infoModel,unit, infoModel.b, 0);
            rebarShape1.ScaleToBox(origin[0], vector[0], vector[1]);
            if (location)
            {
                rebarShape1.SetLayoutAsNumberWithSpacing(n, s, false, true, true);
            }
            else
            {
                rebarShape1.SetLayoutAsNumberWithSpacing(n, s, false, false, false);
            }
            BarS.SetPartitionRebarStirrup(name);
        }
        private void CreateStirrupType2Item(InfoModel infoModel,Document document, UnitProject unit, int n, double o1, double s, bool location, string name)
        {

            double x = (infoModel.b - 4 * c - a) / 2;
            double b0 = 4 * c + a + x;
            List<XYZ> origin1 = GetOriginVectorStirrup(infoModel,unit, o1, 0);
            Rebar bar1 = Rebar.CreateFromRebarShape(document, BarS.RebarShape, BarS.RebarBarType, infoModel.Element, origin1[0], origin1[1], origin1[2]);
            BarS.RebarStirrup.Add(bar1);
            RebarShapeDrivenAccessor rebarShape1 = bar1.GetShapeDrivenAccessor();
            List<XYZ> vector1 = GetScaleBoxStirrup(infoModel,unit, b0, 0);
            rebarShape1.ScaleToBox(origin1[0], vector1[0], vector1[1]);
            if (location)
            {
                rebarShape1.SetLayoutAsNumberWithSpacing(n, s, false, true, true);
            }
            else
            {
                rebarShape1.SetLayoutAsNumberWithSpacing(n, s, false, false, false);
            }

            List<XYZ> origin2 = GetOriginVectorStirrup(infoModel,unit, o1 + BarS.Diameter, x - BarS.Diameter);
            Rebar bar2 = Rebar.CreateFromRebarShape(document, BarS.RebarShape, BarS.RebarBarType, infoModel.Element, origin2[0], origin2[1], origin2[2]);
            BarS.RebarStirrup.Add(bar2);
            RebarShapeDrivenAccessor rebarShape2 = bar2.GetShapeDrivenAccessor();
            List<XYZ> vector2 = GetScaleBoxStirrup(infoModel,unit, infoModel.b, x - BarS.Diameter);
            rebarShape2.ScaleToBox(origin2[0], vector2[0], vector2[1]);
            if (location)
            {
                rebarShape2.SetLayoutAsNumberWithSpacing(n, s, false, true, true);
            }
            else
            {
                rebarShape2.SetLayoutAsNumberWithSpacing(n, s, false, false, false);
            }
            BarS.SetPartitionRebarStirrup(name);
        }
        private void CreateStirrup11(InfoModel infoModel, DistributeStirrup distributeStirrup ,Document document, UnitProject unit, string name, bool special, double start, double end)
        {
            double s = unit.Convert(distributeStirrup.S);
            if (!special)
            {
                int n = (int)(infoModel.Length / distributeStirrup.S) + 1;
                double o1 = (infoModel.Length - (n - 1) * distributeStirrup.S) / 2;
                CreateStirrupType1Item(infoModel,document, unit, n, o1, s, true, name);
            }
            else
            {
                int n1 = (int)(start / distributeStirrup.S) + 1;
                int n2 = (int)((infoModel.Length - end) / distributeStirrup.S) + 1;
                double delta1 = (start - (n1 - 1) * distributeStirrup.S) / 2;
                double o1 = delta1;
                double delta2 = ((infoModel.Length - end) - (n2 - 1) * distributeStirrup.S) / 2;
                double o2 = end + delta2;
                CreateStirrupType1Item(infoModel,document, unit, n1, o1, s, true, name);
                CreateStirrupType1Item(infoModel,document, unit, n2, o2, s, true, name);
            }
        }
        private void CreateStirrup21(InfoModel infoModel, DistributeStirrup distributeStirrup, Document document, UnitProject unit, string name, bool special, double start, double end)
        {
            double s = unit.Convert(distributeStirrup.S);
            if (!special)
            {
                int n = (int)(infoModel.Length / distributeStirrup.S) + 1;
                double o1 = (infoModel.Length - (n - 1) * distributeStirrup.S) / 2;
                CreateStirrupType2Item(infoModel,document, unit, n, o1, s, true, name);
            }
            else
            {
                int n1 = (int)(start / distributeStirrup.S) + 1;
                int n2 = (int)((infoModel.Length - end) / distributeStirrup.S) + 1;
                double delta1 = (start - (n1 - 1) * distributeStirrup.S) / 2;
                double o1 = delta1;
                double delta2 = ((infoModel.Length - end) - (n2 - 1) * distributeStirrup.S) / 2;
                double o2 = end + delta2;
                CreateStirrupType2Item(infoModel,document, unit, n1, o1, s, true, name);
                CreateStirrupType2Item(infoModel,document, unit, n2, o2, s, true, name);
            }
        }
        private void CreateStirrup12(InfoModel infoModel, DistributeStirrup distributeStirrup, Document document, UnitProject unit, string name, bool special, double start, double end)
        {
            double s1 = unit.Convert(distributeStirrup.S1);
            double s2 = unit.Convert(distributeStirrup.S2);
            if (!special)
            {
                int n1 = (int)(distributeStirrup.L1 / distributeStirrup.S1) + 1;
                int n2 = (int)(distributeStirrup.L2 / distributeStirrup.S2) + 1;
                double delta1 = (distributeStirrup.L1 - (n1 - 1) * distributeStirrup.S1) / 2;
                double delta2 = (distributeStirrup.L2 - (n2 - 1) * distributeStirrup.S2) / 2;
                double o1 = delta1;
                double o2 = delta2 + distributeStirrup.L1;
                double o3 = delta1 + distributeStirrup.L1 + distributeStirrup.L2;
                CreateStirrupType1Item(infoModel,document, unit, n1, o1, s1, true, name);
                CreateStirrupType1Item(infoModel,document, unit, n2, o2, s2, true, name);
                CreateStirrupType1Item(infoModel,document, unit, n1, o3, s1, true, name);
            }
            else
            {
                if ((end < distributeStirrup.L1 + distributeStirrup.L2) && start > distributeStirrup.L1)
                {
                    int n1 = (int)(distributeStirrup.L1 / distributeStirrup.S1) + 1;
                    double delta1 = (distributeStirrup.L1 - (n1 - 1) * distributeStirrup.S1) / 2;
                    int n2 = (int)((start - distributeStirrup.L1) / distributeStirrup.S2) + 1;
                    double delta2 = ((start - distributeStirrup.L1) - (n2 - 1) * distributeStirrup.S2) / 2;
                    int n3 = (int)((distributeStirrup.L1 + distributeStirrup.L2 - end) / distributeStirrup.S2) + 1;
                    double delta3 = ((distributeStirrup.L1 + distributeStirrup.L2 - end) - (n3 - 1) * distributeStirrup.S2) / 2;
                    double delta4 = delta1;
                    double o1 = delta1;
                    double o2 = delta2 + distributeStirrup.L1;
                    double o3 = delta3 + end;
                    double o4 = delta4 + distributeStirrup.L1 + distributeStirrup.L2;
                    CreateStirrupType1Item(infoModel, document, unit, n1, o1, s1, true, name);
                    CreateStirrupType1Item(infoModel, document, unit, n2, o2, s2, true, name);
                    CreateStirrupType1Item(infoModel, document, unit, n3, o3, s2, true, name);
                    CreateStirrupType1Item(infoModel, document, unit, n1, o4, s1, true, name);

                }
                else
                {
                    int n1 = (int)(distributeStirrup.L1 / distributeStirrup.S1) + 1;
                    int n2 = (int)(distributeStirrup.L2 / distributeStirrup.S2) + 1;
                    double delta1 = (distributeStirrup.L1 - (n1 - 1) * distributeStirrup.S1) / 2;
                    double delta2 = (distributeStirrup.L2 - (n2 - 1) * distributeStirrup.S2) / 2;
                    double o1 = delta1;
                    double o2 = delta2 + distributeStirrup.L1;
                    double o3 = delta1 + distributeStirrup.L1 + distributeStirrup.L2;
                    CreateStirrupType1Item(infoModel, document, unit, n1, o1, s1, true, name);
                    CreateStirrupType1Item(infoModel, document, unit, n2, o2, s2, true, name);
                    CreateStirrupType1Item(infoModel, document, unit, n1, o3, s1, true, name);
                }
            }
        }
        private void CreateStirrup22(InfoModel infoModel, DistributeStirrup distributeStirrup, Document document, UnitProject unit, string name, bool special, double start, double end)
        {

            double s1 = unit.Convert(distributeStirrup.S1);
            double s2 = unit.Convert(distributeStirrup.S2);
            if (!special)
            {
                int n1 = (int)(distributeStirrup.L1 / distributeStirrup.S1) + 1;
                int n2 = (int)(distributeStirrup.L2 / distributeStirrup.S2) + 1;
                double delta1 = (distributeStirrup.L1 - (n1 - 1) * distributeStirrup.S1) / 2;
                double delta2 = (distributeStirrup.L2 - (n2 - 1) * distributeStirrup.S2) / 2;
                double o1 = delta1;
                double o2 = delta2 + distributeStirrup.L1;
                double o3 = delta1 + distributeStirrup.L1 + distributeStirrup.L2;
                CreateStirrupType2Item(infoModel, document, unit, n1, o1, s1, true, name);
                CreateStirrupType2Item(infoModel, document, unit, n2, o2, s2, true, name);
                CreateStirrupType2Item(infoModel, document, unit, n1, o3, s1, true, name);
            }
            else
            {
                if ((end <= distributeStirrup.L1 + distributeStirrup.L2) && start >= distributeStirrup.L1)
                {
                    int n1 = (int)(distributeStirrup.L1 / distributeStirrup.S1) + 1;
                    double delta1 = (distributeStirrup.L1 - (n1 - 1) * distributeStirrup.S1) / 2;
                    int n2 = (int)((start - distributeStirrup.L1) / distributeStirrup.S2) + 1;
                    double delta2 = ((start - distributeStirrup.L1) - (n2 - 1) * distributeStirrup.S2) / 2;
                    int n3 = (int)((distributeStirrup.L1 + distributeStirrup.L2 - end) / distributeStirrup.S2) + 1;
                    double delta3 = ((distributeStirrup.L1 + distributeStirrup.L2 - end) - (n3 - 1) * distributeStirrup.S2) / 2;
                    double delta4 = delta1;
                    double o1 = delta1;
                    double o2 = delta2 + distributeStirrup.L1;
                    double o3 = delta3 + end;
                    double o4 = delta4 + distributeStirrup.L1 + distributeStirrup.L2;
                    CreateStirrupType2Item(infoModel, document, unit, n1, o1, s1, true, name);
                    CreateStirrupType2Item(infoModel, document, unit, n2, o2, s2, true, name);
                    CreateStirrupType2Item(infoModel, document, unit, n3, o3, s2, true, name);
                    CreateStirrupType2Item(infoModel, document, unit, n1, o4, s1, true, name);
                }
                else
                {
                    int n1 = (int)(distributeStirrup.L1 / distributeStirrup.S1) + 1;
                    int n2 = (int)(distributeStirrup.L2 / distributeStirrup.S2) + 1;
                    double delta1 = (distributeStirrup.L1 - (n1 - 1) * distributeStirrup.S1) / 2;
                    double delta2 = (distributeStirrup.L2 - (n2 - 1) * distributeStirrup.S2) / 2;
                    double o1 = delta1;
                    double o2 = delta2 + distributeStirrup.L1;
                    double o3 = delta1 + distributeStirrup.L1 + distributeStirrup.L2;
                    CreateStirrupType2Item(infoModel, document, unit, n1, o1, s1, true, name);
                    CreateStirrupType2Item(infoModel, document, unit, n2, o2, s2, true, name);
                    CreateStirrupType2Item(infoModel, document, unit, n1, o3, s1, true, name);
                }
            }

        }
        public void CreateStirrup(InfoModel infoModel, DistributeStirrup distributeStirrup, Document document, UnitProject unit, SettingModel settingModel, bool special, double start, double end)
        {
            BarS.GetRebarShape(document, "Stirrup", settingModel);
            if (BarS.RebarShape == null)
            {
                System.Windows.Forms.MessageBox.Show("There are no existing Stirrup RebarShape" + "\n" + "Please Load Rebar Shape Family", "ERROR", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            if (Type == 0)
            {
                if (distributeStirrup.Type == 0)
                {

                    CreateStirrup11(infoModel, distributeStirrup,document, unit, settingModel.BeamsName, special, start, end);
                }
                else
                {
                    
                    CreateStirrup12(infoModel, distributeStirrup, document, unit, settingModel.BeamsName, special, start, end);
                }
            }
            else
            {

                if (distributeStirrup.Type == 0)
                {
                    CreateStirrup21(infoModel, distributeStirrup, document, unit, settingModel.BeamsName, special, start, end);
                }
                else
                {
                    CreateStirrup22(infoModel, distributeStirrup, document, unit, settingModel.BeamsName, special, start, end);
                }
            }
        }
        #endregion
        #region Create Anti
        private List<XYZ> GetOriginVectorAnti(InfoModel infoModel, UnitProject unit, double location, double y0)
        {
            List<XYZ> vector = new List<XYZ>();
            XYZ p3 = PointModel.ProjectToPlane(infoModel.LeftRightPlanar[0].Origin, infoModel.TopBottomPlanar[1]);
            XYZ p4 = PointModel.ProjectToPlane(infoModel.LeftRightPlanar[1].Origin, infoModel.TopBottomPlanar[1]);
            double cover = unit.Convert(c);
            double y = unit.Convert(y0);
            double location0 = unit.Convert(location);
            XYZ v12 = p4 - p3;
            XYZ p5 = p3 + cover * v12;
            XYZ p6 = p5 + y * XYZ.BasisZ;
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
            vector.Add((-1)*t.BasisX);
            return vector;
        }
        private List<XYZ> GetScaleBoxAnti(InfoModel infoModel, UnitProject unit, double b0, double x0)
        {
            List<XYZ> vector = new List<XYZ>();
            XYZ p3 = PointModel.ProjectToPlane(infoModel.LeftRightPlanar[0].Origin, infoModel.TopBottomPlanar[1]);
            XYZ p4 = PointModel.ProjectToPlane(infoModel.LeftRightPlanar[1].Origin, infoModel.TopBottomPlanar[1]);
            double cover = unit.Convert(c);
            double x = unit.Convert(x0);
            double b = unit.Convert(b0);
            double h = unit.Convert(infoModel.h);
            //XYZ v12 = p4 - p3;
            //XYZ p5 = p3 + (cover + x) * v12;
            //XYZ p5a = p3 + (b - cover) * v12;
            XYZ p5 = p3 + (cover + x) * (-1) * infoModel.LeftRightPlanar[0].FaceNormal;
            XYZ p5a = p3 + (b - cover) * (-1) * infoModel.LeftRightPlanar[0].FaceNormal;
            XYZ pb = p5a - p5;
            vector.Add(pb);
            XYZ p6 = p5 + cover * XYZ.BasisZ;
            XYZ p6a = p5 + (h - cover) * XYZ.BasisZ;
            XYZ ph = p6a - p6;
            vector.Add(ph);
            return vector;
        }
        private void CreateAnti1(InfoModel infoModel, Document document, UnitProject unit, SettingModel settingModel)
        {
            int n = (int)(infoModel.Length / Sa) + 1;
            double o1 = (infoModel.Length - (n - 1) * Sa) / 2;
            double s = unit.Convert(Sa);
            double y0 = infoModel.h / 2 - c;
            List<XYZ> origin = GetOriginVectorAnti(infoModel,unit, o1, y0);
            BarA.Rebar = Rebar.CreateFromRebarShape(document, BarA.RebarShape, BarA.RebarBarType, infoModel.Element, origin[0], origin[1], origin[2]);
            RebarShapeDrivenAccessor rebarShape1 = BarA.Rebar.GetShapeDrivenAccessor();
            List<XYZ> vector = GetScaleBoxAnti(infoModel,unit, infoModel.b, 0);
            rebarShape1.ScaleToBox(origin[0], vector[0], vector[1]);
            rebarShape1.SetLayoutAsNumberWithSpacing(n, s, false, true, true);
            BarA.SetPartitionRebar(settingModel.BeamsName);
        }
        private void CreateAnti2(InfoModel infoModel, Document document, UnitProject unit, SettingModel settingModel)
        {
            int n = (int)(infoModel.Length / Sa) + 1;
            double o1 = (infoModel.Length - (n - 1) * Sa) / 2;
            double s = unit.Convert(Sa);
            double y1 = (infoModel.h - 2 * c) / 3;
            List<XYZ> origin1 = GetOriginVectorAnti(infoModel,unit, o1, y1);
            BarA.Rebar = Rebar.CreateFromRebarShape(document, BarA.RebarShape, BarA.RebarBarType, infoModel.Element, origin1[0], origin1[1], origin1[2]);
            RebarShapeDrivenAccessor rebarShape1 = BarA.Rebar.GetShapeDrivenAccessor();
            List<XYZ> vector1 = GetScaleBoxAnti(infoModel,unit, infoModel.b, 0);
            rebarShape1.ScaleToBox(origin1[0], vector1[0], vector1[1]);
            rebarShape1.SetLayoutAsNumberWithSpacing(n, s, false, true, true);
            List<XYZ> origin2 = GetOriginVectorAnti(infoModel,unit, o1, 2 * y1);
            BarA.SetPartitionRebar(settingModel.BeamsName);
            BarA.Rebar = Rebar.CreateFromRebarShape(document, BarA.RebarShape, BarA.RebarBarType, infoModel.Element, origin2[0], origin2[1], origin2[2]);
            RebarShapeDrivenAccessor rebarShape2 = BarA.Rebar.GetShapeDrivenAccessor();
            List<XYZ> vector2 = GetScaleBoxAnti(infoModel,unit, infoModel.b, 0);
            rebarShape2.ScaleToBox(origin2[0], vector2[0], vector2[1]);
            rebarShape2.SetLayoutAsNumberWithSpacing(n, s, false, true, true);
            BarA.SetPartitionRebar(settingModel.BeamsName);
        }
        public void CeateAnti(InfoModel infoModel,Document document, UnitProject unit, SettingModel settingModel)
        {
            if (Anti)
            {
                BarA.GetRebarShape(document, "Anti", settingModel);
                if (BarA.RebarShape == null)
                {
                    System.Windows.Forms.MessageBox.Show("There are no existing Stirrup RebarShape" + "\n" + "Please Load Rebar Shape Family", "ERROR", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }

                if (Na == 1)
                {
                    CreateAnti1(infoModel,document, unit, settingModel);
                }
                else
                {
                    CreateAnti2(infoModel,document, unit, settingModel);
                }
            }
        }
        #endregion
        #region Create Tag Stirrup Detail
        private XYZ GetXYZTagStirrupDetail(InfoModel infoModel, UnitProject unit, double L10, double h0)
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
        private void CreateTagStirrupDetailItem(Autodesk.Revit.DB.View view, InfoModel infoModel, Document document, UnitProject unit, SettingModel settingModel, Rebar bar, double L10, double h0)
        {
            XYZ origin = GetXYZTagStirrupDetail(infoModel,unit, L10, h0);
            BarS.Tag = IndependentTag.Create(document, view.Id, new Reference(bar), false, BarS.Mode, BarS.Horizontal, origin);
            BarS.Tag.ChangeTypeId(settingModel.SelectedStirrupTag.Id);
        }
        private void CreateTagStirrupDetail11(Autodesk.Revit.DB.View view, InfoModel infoModel, Document document, UnitProject unit, SettingModel settingModel, double h0, bool special, double start, double end)
        {
            if (!special)
            {
                CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[0], infoModel.Length / 8, h0);
                CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[0], infoModel.Length / 2, h0);
                CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[0], 7 * infoModel.Length / 8, h0);
            }
            else
            {
                double l1 = infoModel.Length / 8;
                double l2 = infoModel.Length / 4 + Math.Abs(start - infoModel.Length / 4) / 2;
                double l3 = (3 * infoModel.Length / 4 - end) / 2 + end;
                double l4 = 7 * infoModel.Length / 8;
                CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[0], l1, h0);
                CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[0], l2, h0);
                CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[1], l3, h0);
                CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[1], l4, h0);
            }
        }
        private void CreateTagStirrupDetail21(Autodesk.Revit.DB.View view, InfoModel infoModel, Document document, UnitProject unit, SettingModel settingModel, double h0, bool special, double start, double end)
        {
            if (!special)
            {
                CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[0], infoModel.Length / 8, h0);
                CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[0], infoModel.Length / 2, h0);
                CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[0], 7 * infoModel.Length / 8, h0);
            }
            else
            {
                double l1 = infoModel.Length / 8;
                double l2 = infoModel.Length / 4 + Math.Abs(start - infoModel.Length / 4) / 2;
                double l3 = (3 * infoModel.Length / 4 - end) / 2 + end;
                double l4 = 7 * infoModel.Length / 8;
                CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[0], l1, h0);
                CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[0], l2, h0);
                CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[2], l3, h0);
                CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[2], l4, h0);
            }
        }
        private void CreateTagStirrupDetail12(Autodesk.Revit.DB.View view, InfoModel infoModel, DistributeStirrup distributeStirrup, Document document, UnitProject unit, SettingModel settingModel, double h0, bool special, double start, double end)
        {
            if (!special)
            {
                CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[0], infoModel.Length / 8, h0);
                CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[1], infoModel.Length / 2, h0);
                CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[2], 7 * infoModel.Length / 8, h0);

            }
            else
            {
                if ((end < distributeStirrup.L1 + distributeStirrup.L2) && start > distributeStirrup.L1)
                {
                    double l1 = infoModel.Length / 8;
                    double l2 = infoModel.Length / 4 + Math.Abs(start - infoModel.Length / 4) / 2;
                    double l3 = (3 * infoModel.Length / 4 - end) / 2 + end;
                    double l4 = 7 * infoModel.Length / 8;
                    CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[0], l1, h0);
                    CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[1], l2, h0);
                    CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[2], l3, h0);
                    CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[3], l4, h0);
                }
                else
                {
                    CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[0], infoModel.Length / 8, h0);
                    CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[1], infoModel.Length / 2, h0);
                    CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[2], 7 * infoModel.Length / 8, h0);
                }
            }
        }
        private void CreateTagStirrupDetail22(Autodesk.Revit.DB.View view, InfoModel infoModel, DistributeStirrup distributeStirrup, Document document, UnitProject unit, SettingModel settingModel, double h0, bool special, double start, double end)
        {
            if (!special)
            {
                CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[0], infoModel.Length / 8, h0);
                CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[2], infoModel.Length / 2, h0);
                CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[4], 7 * infoModel.Length / 8, h0);
            }
            else
            {
                if ((end < distributeStirrup.L1 + distributeStirrup.L2) && start > distributeStirrup.L1)
                {
                    double l1 = infoModel.Length / 8;
                    double l2 = infoModel.Length / 4 + Math.Abs(start - infoModel.Length / 4) / 2;
                    double l3 = (3 * infoModel.Length / 4 - end) / 2 + end;
                    double l4 = 7 * infoModel.Length / 8;
                    CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[0], l1, h0);
                    CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[2], l2, h0);
                    CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[4], l3, h0);
                    CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[6], l4, h0);
                }
                else
                {
                    CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[0], infoModel.Length / 8, h0);
                    CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[2], infoModel.Length / 2, h0);
                    CreateTagStirrupDetailItem(view, infoModel, document, unit, settingModel, BarS.RebarStirrup[4], 7 * infoModel.Length / 8, h0);
                }
            }
        }
        public void CreateTagStirrupDetail(Autodesk.Revit.DB.View view, InfoModel infoModel, DistributeStirrup distributeStirrup, Document document, UnitProject unit, SettingModel settingModel, double h0, bool special, double start, double end)
        {
            if (Type == 0)
            {
                if (distributeStirrup.Type == 0)
                {
                    CreateTagStirrupDetail11(view, infoModel, document, unit, settingModel, h0, special, start, end);
                }
                else
                {
                    CreateTagStirrupDetail12(view, infoModel, distributeStirrup, document, unit, settingModel, h0, special, start, end);
                }
            }
            else
            {
                if (distributeStirrup.Type == 0)
                {
                    CreateTagStirrupDetail12(view, infoModel, distributeStirrup, document, unit, settingModel, h0, special, start, end);
                }
                else
                {
                    CreateTagStirrupDetail22(view, infoModel, distributeStirrup, document, unit, settingModel, h0, special, start, end);
                }
            }
        }
        #endregion
        #region Create Tag Stirrup Detail
        private XYZ GetXYZTagStirrupSection(ViewSection viewSection, InfoModel infoModel, UnitProject unit, double L10, double b0, double delta0)
        {
            PlanarFace planarPer0 = null;
            for (int i = 0; i < infoModel.LeftRightPlanar.Count; i++)
            {
                if (!(Math.Abs(infoModel.LeftRightPlanar[i].FaceNormal.AngleTo(viewSection.RightDirection) - Math.PI) < 1e-9))
                {
                    planarPer0 = infoModel.LeftRightPlanar[i];
                }
            }
            XYZ p1 = PointModel.ProjectToPlane(infoModel.TopBottomPlanar[0].Origin, planarPer0);
            XYZ p2 = PointModel.ProjectToPlane(infoModel.TopBottomPlanar[1].Origin, planarPer0);
            double b = unit.Convert(b0);
            double L1 = unit.Convert(L10);
            double delta = unit.Convert(delta0);
            double h= 0.5 * unit.Convert(infoModel.h);
            XYZ v12 = p2 - p1;
            XYZ p3 = p1 +(h)*(-1)*XYZ.BasisZ+ delta * (-1) * XYZ.BasisZ;
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
            XYZ p6 = p5 + b* planarPer0.FaceNormal;
            return p6;
        }
        private void CreateTagStirrupSectionItem(ViewSection viewSection, InfoModel infoModel, Document document, UnitProject unit, SettingModel settingModel, double L10, double b0, double delta0)
        {
            for (int i = 0; i < BarS.RebarStirrup.Count; i++)
            {
                Rebar reBar = new FilteredElementCollector(document, viewSection.Id).OfClass(typeof(Rebar)).Cast<Rebar>().Where(x => x.Id == BarS.RebarStirrup[i].Id).FirstOrDefault();
                if (reBar != null)
                {
                    XYZ origin = GetXYZTagStirrupSection(viewSection, infoModel, unit, L10, b0, delta0);
                    BarS.Tag = IndependentTag.Create(document, viewSection.Id, new Reference(reBar), true, BarS.Mode, BarS.Horizontal, origin);
                    BarS.Tag.ChangeTypeId(settingModel.SelectedStirrupTag.Id);
                }
            }
        }
        private void CreateTagStirrupSection11(ViewSection viewStart, ViewSection viewMid, ViewSection viewEnd, InfoModel infoModel, Document document, UnitProject unit, SettingModel settingModel, double b0)
        {
            CreateTagStirrupSectionItem(viewStart, infoModel, document, unit, settingModel, infoModel.Length / 8, b0, 0);
            CreateTagStirrupSectionItem(viewMid, infoModel, document, unit, settingModel, infoModel.Length / 2, b0, 0);
            CreateTagStirrupSectionItem(viewEnd, infoModel, document, unit, settingModel, 7 * infoModel.Length / 8, b0, 0);
        }
        private void CreateTagStirrupSection21(ViewSection viewStart, ViewSection viewMid, ViewSection viewEnd, InfoModel infoModel, Document document, UnitProject unit, SettingModel settingModel, double b0, double delta0)
        {
            CreateTagStirrupSectionItem(viewStart, infoModel, document, unit, settingModel, infoModel.Length / 8, b0, delta0);
            CreateTagStirrupSectionItem(viewMid, infoModel, document, unit, settingModel, infoModel.Length / 2, b0, delta0);
            CreateTagStirrupSectionItem(viewEnd, infoModel, document, unit, settingModel, 7 * infoModel.Length / 8, b0, delta0);
            CreateTagStirrupSectionItem(viewStart, infoModel, document, unit, settingModel, infoModel.Length / 8, b0, -delta0);
            CreateTagStirrupSectionItem(viewMid, infoModel, document, unit, settingModel, infoModel.Length / 2, b0, -delta0);
            CreateTagStirrupSectionItem(viewEnd, infoModel, document, unit, settingModel, 7 * infoModel.Length / 8, b0, -delta0);
        }
        private void CreateTagStirrupSection12(ViewSection viewStart, ViewSection viewMid, ViewSection viewEnd, InfoModel infoModel, DistributeStirrup distributeStirrup, Document document, UnitProject unit, SettingModel settingModel, double b0, bool special, double start0, double end0)
        {
            if (!special)
            {
                CreateTagStirrupSectionItem(viewStart, infoModel, document, unit, settingModel, infoModel.Length / 8, b0, 0);
                CreateTagStirrupSectionItem(viewMid, infoModel, document, unit, settingModel, infoModel.Length / 2, b0, 0);
                CreateTagStirrupSectionItem(viewEnd, infoModel, document, unit, settingModel, 7 * infoModel.Length / 8, b0, 0);
            }
            else
            {
                if ((end0 < distributeStirrup.L1 + distributeStirrup.L2) && start0 > distributeStirrup.L1)
                {
                    double start = unit.Convert(start0);
                    double end = unit.Convert(end0);
                    double length2 = unit.Convert(infoModel.Length / 2);
                    double viewcrop = viewMid.get_Parameter(BuiltInParameter.VIEWER_BOUND_OFFSET_FAR).AsDouble();
                    if (length2 < start)
                    {
                        CreateTagStirrupSectionItem(viewStart, infoModel, document, unit, settingModel, infoModel.Length / 8, b0, 0);
                        CreateTagStirrupSectionItem(viewMid, infoModel, document, unit, settingModel, infoModel.Length / 2, b0, 0);
                        CreateTagStirrupSectionItem(viewEnd, infoModel, document, unit, settingModel, 7 * infoModel.Length / 8, b0, 0);
                    }
                    else
                    {
                        if (length2 + viewcrop > end)
                        {
                            CreateTagStirrupSectionItem(viewStart, infoModel, document, unit, settingModel, infoModel.Length / 8, b0, 0);
                            CreateTagStirrupSectionItem(viewMid, infoModel, document, unit, settingModel, infoModel.Length / 2, b0, 0);
                            CreateTagStirrupSectionItem(viewEnd, infoModel, document, unit, settingModel, 7 * infoModel.Length / 8, b0, 0);
                        }
                    }
                }
                else
                {
                    CreateTagStirrupSectionItem(viewStart, infoModel, document, unit, settingModel, infoModel.Length / 8, b0, 0);
                    CreateTagStirrupSectionItem(viewMid, infoModel, document, unit, settingModel, infoModel.Length / 2, b0, 0);
                    CreateTagStirrupSectionItem(viewEnd, infoModel, document, unit, settingModel, 7 * infoModel.Length / 8, b0, 0);
                }
            }
        }
        private void CreateTagStirrupSection22(ViewSection viewStart, ViewSection viewMid, ViewSection viewEnd, InfoModel infoModel, DistributeStirrup distributeStirrup, Document document, UnitProject unit, SettingModel settingModel, double b0, double delta0, bool special, double start0, double end0)
        {
            if (!special)
            {
                CreateTagStirrupSectionItem(viewStart, infoModel, document, unit, settingModel, infoModel.Length / 8, b0, delta0);
                CreateTagStirrupSectionItem(viewMid, infoModel, document, unit, settingModel, infoModel.Length / 2, b0, delta0);
                CreateTagStirrupSectionItem(viewEnd, infoModel, document, unit, settingModel, 7 * infoModel.Length / 8, b0, delta0);
                CreateTagStirrupSectionItem(viewStart, infoModel, document, unit, settingModel, infoModel.Length / 8, b0, -delta0);
                CreateTagStirrupSectionItem(viewMid, infoModel, document, unit, settingModel, infoModel.Length / 2, b0, -delta0);
                CreateTagStirrupSectionItem(viewEnd, infoModel, document, unit, settingModel, 7 * infoModel.Length / 8, b0, -delta0);
            }
            else
            {
                if ((end0 < distributeStirrup.L1 + distributeStirrup.L2) && start0 > distributeStirrup.L1)
                {
                    double start = unit.Convert(start0);
                    double end = unit.Convert(end0);
                    double length2 = unit.Convert(infoModel.Length / 2);
                    double viewcrop = viewMid.get_Parameter(BuiltInParameter.VIEWER_BOUND_OFFSET_FAR).AsDouble();
                    if (length2 < start)
                    {
                        CreateTagStirrupSectionItem(viewStart, infoModel, document, unit, settingModel, infoModel.Length / 8, b0, delta0);
                        CreateTagStirrupSectionItem(viewMid, infoModel, document, unit, settingModel, infoModel.Length / 2, b0, delta0);
                        CreateTagStirrupSectionItem(viewEnd, infoModel, document, unit, settingModel, 7 * infoModel.Length / 8, b0, delta0);
                        CreateTagStirrupSectionItem(viewStart, infoModel, document, unit, settingModel, infoModel.Length / 8, b0, -delta0);
                        CreateTagStirrupSectionItem(viewMid, infoModel, document, unit, settingModel, infoModel.Length / 2, b0, -delta0);
                        CreateTagStirrupSectionItem(viewEnd, infoModel, document, unit, settingModel, 7 * infoModel.Length / 8, b0, -delta0);
                    }
                    else
                    {
                        if (length2 + viewcrop > end)
                        {
                            CreateTagStirrupSectionItem(viewStart, infoModel, document, unit, settingModel, infoModel.Length / 8, b0, delta0);
                            CreateTagStirrupSectionItem(viewMid, infoModel, document, unit, settingModel, infoModel.Length / 2, b0, delta0);
                            CreateTagStirrupSectionItem(viewEnd, infoModel, document, unit, settingModel, 7 * infoModel.Length / 8, b0, delta0);
                            CreateTagStirrupSectionItem(viewStart, infoModel, document, unit, settingModel, infoModel.Length / 8, b0, -delta0);
                            CreateTagStirrupSectionItem(viewMid, infoModel, document, unit, settingModel, infoModel.Length / 2, b0, -delta0);
                            CreateTagStirrupSectionItem(viewEnd, infoModel, document, unit, settingModel, 7 * infoModel.Length / 8, b0, -delta0);
                        }
                    }
                }
                else
                {
                    CreateTagStirrupSectionItem(viewStart, infoModel, document, unit, settingModel, infoModel.Length / 8, b0, delta0);
                    CreateTagStirrupSectionItem(viewMid, infoModel, document, unit, settingModel, infoModel.Length / 2, b0, delta0);
                    CreateTagStirrupSectionItem(viewEnd, infoModel, document, unit, settingModel, 7 * infoModel.Length / 8, b0, delta0);
                    CreateTagStirrupSectionItem(viewStart, infoModel, document, unit, settingModel, infoModel.Length / 8, b0, -delta0);
                    CreateTagStirrupSectionItem(viewMid, infoModel, document, unit, settingModel, infoModel.Length / 2, b0, -delta0);
                    CreateTagStirrupSectionItem(viewEnd, infoModel, document, unit, settingModel, 7 * infoModel.Length / 8, b0, -delta0);
                }
            }
        }
        public void CreateTagStirrupSection(ViewSection viewStart, ViewSection viewMid, ViewSection viewEnd, InfoModel infoModel, DistributeStirrup distributeStirrup, Document document, UnitProject unit, SettingModel settingModel, bool special, double start0, double end0)
        {
            if (Type == 0)
            {
                if (distributeStirrup.Type == 0)
                {
                    CreateTagStirrupSection11(viewStart,  viewMid, viewEnd, infoModel, document, unit, settingModel, settingModel.TagH/2);
                }
                else
                {
                    CreateTagStirrupSection12(viewStart,  viewMid, viewEnd, infoModel, distributeStirrup, document, unit, settingModel, settingModel.TagH/2, special, start0, end0);

                }
            }
            else
            {
                if (distributeStirrup.Type == 0)
                {
                    CreateTagStirrupSection21(viewStart,  viewMid, viewEnd, infoModel, document, unit, settingModel, settingModel.TagH/2, settingModel.TagV / 3);
                }
                else
                {
                    CreateTagStirrupSection22(viewStart,  viewMid, viewEnd, infoModel, distributeStirrup, document, unit, settingModel, settingModel.TagH/2, settingModel.TagV / 3, special, start0, end0);
                }
            }
        }
        #endregion
        #region Dimension Stirrup
        public Reference GetReferenceItem(ViewSection view, Document document, InfoModel infoModel,DistributeStirrup distributeStirrup, PlanarFace planarFace0, UnitProject unit, double l0)
        {
            double l = unit.Convert(infoModel.startPosition + l0);
            XYZ p1 = PointModel.ProjectToPlane(infoModel.LeftRightPlanar[0].Origin, infoModel.TopBottomPlanar[0]);
            XYZ p2 = p1 + unit.Convert(infoModel.b) * (-1) * infoModel.LeftRightPlanar[0].FaceNormal;
            XYZ p3= PointModel.ProjectToPlane(p2, planarFace0);
            XYZ p4 = p3 + l * (-1) * planarFace0.FaceNormal;
            XYZ p5 = p4 + 0.01 * XYZ.BasisZ;
            Line line = Line.CreateBound(p4, p5);
            DetailCurve detailCurve = document.Create.NewDetailCurve(view, line);
            return detailCurve.GeometryCurve.Reference;
        }
        #endregion
    }
}
