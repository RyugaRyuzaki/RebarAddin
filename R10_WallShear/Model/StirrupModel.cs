
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace R10_WallShear
{
    public class StirrupModel : BaseViewModel
    {
        #region Property
        private int _NumberWall;
        public int NumberWall { get => _NumberWall; set { _NumberWall = value; OnPropertyChanged(); } }

        private RebarBarModel _BarS;
        public RebarBarModel BarS { get => _BarS; set { _BarS = value; OnPropertyChanged(); } }

        private RebarBarModel _BarH;
        public RebarBarModel BarH { get => _BarH; set { _BarH = value; OnPropertyChanged(); } }
        private bool _AddH;
        public bool AddH { get => _AddH; set { _AddH = value; OnPropertyChanged(); } }
        private int _TypeH;
        public int TypeH { get => _TypeH; set { _TypeH = value; OnPropertyChanged(); } }
        private double _aH;
        public double aH { get => _aH; set { _aH = value; OnPropertyChanged(); } }
        private double _DistanceH;
        public double DistanceH { get => _DistanceH; set { _DistanceH = value; OnPropertyChanged(); } }

    

        private int _TypeDis;
        public int TypeDis { get => _TypeDis; set { _TypeDis = value; OnPropertyChanged(); } }
        private double _S;
        public double S { get => _S; set { _S = value; OnPropertyChanged(); } }
        private double _S1;
        public double S1 { get => _S1; set { _S1 = value; OnPropertyChanged(); } }
        private double _S2;
        public double S2 { get => _S2; set { _S2 = value; OnPropertyChanged(); } }
        private double _L;
        public double L { get => _L; set { _L = value; OnPropertyChanged(); } }
        private double _L1;
        public double L1 { get => _L1; set { _L1 = value; OnPropertyChanged(); } }
        private double _L2;
        public double L2 { get => _L2; set { _L2 = value; OnPropertyChanged(); } }
        private bool _IsTiesUp;
        public bool IsTiesUp { get => _IsTiesUp; set { _IsTiesUp = value; OnPropertyChanged(); } }

        private bool _IsCorner;
        public bool IsCorner { get => _IsCorner; set { _IsCorner = value; OnPropertyChanged(); } }

        private RebarBarModel _BarSCorner;
        public RebarBarModel BarSCorner { get => _BarSCorner; set { _BarSCorner = value; OnPropertyChanged(); } }

        private RebarBarModel _BarHCorner;
        public RebarBarModel BarHCorner { get => _BarHCorner; set { _BarHCorner = value; OnPropertyChanged(); } }
        private bool _AddHCorner;
        public bool AddHCorner { get => _AddHCorner; set { _AddHCorner = value; OnPropertyChanged(); } }
        private int _TypeHCorner;
        public int TypeHCorner { get => _TypeHCorner; set { _TypeHCorner = value; OnPropertyChanged(); } }
        private int _nHCorner;
        public int nHCorner { get => _nHCorner; set { _nHCorner = value; OnPropertyChanged(); } }
        private double _aHCorner;
        public double aHCorner { get => _aHCorner; set { _aHCorner = value; OnPropertyChanged(); } }

        private RebarBarModel _BarVCorner;
        public RebarBarModel BarVCorner { get => _BarVCorner; set { _BarVCorner = value; OnPropertyChanged(); } }
        private bool _AddVCorner;
        public bool AddVCorner { get => _AddVCorner; set { _AddVCorner = value; OnPropertyChanged(); } }
        private int _TypeVCorner;
        public int TypeVCorner { get => _TypeVCorner; set { _TypeVCorner = value; OnPropertyChanged(); } }
        private int _nVCorner;
        public int nVCorner { get => _nVCorner; set { _nVCorner = value; OnPropertyChanged(); } }
        private double _aVCorner;
        public double aVCorner { get => _aVCorner; set { _aVCorner = value; OnPropertyChanged(); } }
        #endregion
        public StirrupModel(int numberWall, bool isties, RebarBarModel barS, RebarBarModel barH,  bool addH,int typeDis, double s, double s1, double s2,double ah,  double disH,  RebarBarModel barSCorner, RebarBarModel barHCorner, RebarBarModel barVCorner, bool addHCorner, bool addVCorner, double ahCorner, double avCorner)
        {
            NumberWall = numberWall;
            BarS = barS; BarH = barH; 
            AddH = addH;  TypeH = 0;aH = ah;  DistanceH = disH;
            TypeDis = typeDis; S = s; S1 = s1; S2 = s2; IsTiesUp = isties;
            IsCorner = false;
            BarSCorner = barSCorner;
            BarHCorner = barHCorner;
            BarVCorner = barVCorner;
            AddHCorner = addHCorner; AddVCorner = addVCorner; TypeHCorner = 0; TypeVCorner = 0; nHCorner = 1; nVCorner = 1;  aHCorner = ahCorner; aVCorner = avCorner;
        }
        public void GetDistribute(double l, double hb, double z)
        {
            L = (!IsTiesUp) ? l : (l + hb + z);
            switch (TypeDis)
            {
                case 0:
                    L1 = 0; L2 = 0;
                    break;
                case 1:
                    L1 = L * 1.0 / 4; L2 = L * 1.0 / 2;
                    break;
                case 2:
                    L1 = L * 1.0 / 6; L2 = L * 4.0 / 6;
                    break;
                case 3:
                    L1 = L * 1.0 / 8; L2 = L * 6.0 / 8;
                    break;
                default:
                    L1 = 0; L2 = 0;
                    break;
            }
        }
        #region Create Stirrup
        //private List<XYZ> GetOriginVectorStirrupRectangle(InfoModel infoModel, UnitProject unit, double Cover, double location0, double x0, double y0)
        //{
        //    List<XYZ> vector = new List<XYZ>();
        //    XYZ p3 = PointModel.ProjectToPlane(infoModel.South.Origin, infoModel.West);
        //    double cover = unit.Convert(Cover);
        //    double x = unit.Convert(x0);
        //    double y = unit.Convert(y0);
        //    double location = unit.Convert(location0);
        //    XYZ p5 = p3 + (x + cover) * infoModel.East.FaceNormal;
        //    XYZ p6 = p5 + (y + cover) * infoModel.Nouth.FaceNormal;
        //    XYZ p60 = PointModel.ProjectToPlane(p6, infoModel.Bottom);
        //    XYZ p61 = p60 + location * XYZ.BasisZ;
        //    vector.Add(p61);
        //    Transform t = Transform.CreateRotation(infoModel.East.FaceNormal, Math.PI / 2);
        //    vector.Add(t.BasisZ);
        //    vector.Add(t.BasisX);
        //    return vector;
        //}
        //private List<XYZ> GetScaleBoxStirrupRectangle(InfoModel infoModel, UnitProject unit, double Cover, double b0, double h0, double x0, double y0)
        //{
        //    List<XYZ> vector = new List<XYZ>();
        //    XYZ p3 = PointModel.ProjectToPlane(infoModel.West.Origin, infoModel.South);
        //    XYZ p4 = PointModel.ProjectToPlane(infoModel.East.Origin, infoModel.South);
        //    double cover = unit.Convert(Cover);
        //    double x = unit.Convert(x0);
        //    double y = unit.Convert(y0);
        //    double b = unit.Convert(b0);
        //    double h = unit.Convert(h0);
        //    XYZ p5 = p3 + (cover + x) * infoModel.East.FaceNormal;
        //    XYZ p5a = p3 + (b - cover) * infoModel.East.FaceNormal;
        //    XYZ pb = p5a - p5;
        //    vector.Add(pb);
        //    XYZ p6 = p5 + (cover + y) * infoModel.Nouth.FaceNormal;
        //    XYZ p6a = p5 + (h - cover) * infoModel.Nouth.FaceNormal;
        //    XYZ ph = p6a - p6;
        //    vector.Add(ph);
        //    return vector;
        //}
        //private List<XYZ> GetOriginVectorStirrup(SectionStyle sectionStyle, InfoModel infoModel, UnitProject unit, double Cover, double location0)
        //{
        //    if (sectionStyle == SectionStyle.RECTANGLE)
        //    {
        //        return GetOriginVectorStirrupRectangle(infoModel, unit, Cover, location0, 0, 0);
        //    }
        //    else
        //    {
        //        List<XYZ> vector = new List<XYZ>();
        //        XYZ p1 = PointModel.ProjectToPlane(infoModel.PointPosition, infoModel.Bottom);
        //        double cover = unit.Convert(Cover);
        //        double T = unit.Convert(infoModel.T);
        //        double location = unit.Convert(location0);
        //        XYZ p2 = p1 + (T * 0.5 - cover) * (-1) * XYZ.BasisX;
        //        XYZ p3 = p2 + (T * 0.5 - cover) * (-1) * XYZ.BasisY;
        //        XYZ p4 = p3 + location * XYZ.BasisZ;
        //        vector.Add(p4);
        //        Transform t = Transform.CreateRotation(XYZ.BasisX, Math.PI / 2);
        //        vector.Add(t.BasisZ);
        //        vector.Add(t.BasisX);
        //        return vector;
        //    }
        //}
        //private List<XYZ> GetScaleBoxStirrup(SectionStyle sectionStyle, InfoModel infoModel, UnitProject unit, double Cover)
        //{
        //    if (sectionStyle == SectionStyle.RECTANGLE)
        //    {
        //        return GetScaleBoxStirrupRectangle(infoModel, unit, Cover, infoModel.b, infoModel.h, 0, 0);
        //    }
        //    else
        //    {
        //        List<XYZ> vector = new List<XYZ>();
        //        XYZ p1 = PointModel.ProjectToPlane(infoModel.PointPosition, infoModel.Bottom);
        //        double cover = unit.Convert(Cover);
        //        double T = unit.Convert(infoModel.T);
        //        XYZ p2 = p1 + (T * 0.5 - cover) * (-1) * XYZ.BasisX;
        //        XYZ p3 = p1 + (T * 0.5 - cover) * (-1) * XYZ.BasisY;
        //        XYZ p4 = p3 + (T - 2 * cover) * XYZ.BasisX;
        //        XYZ p4a = p3 + (T - 2 * cover) * XYZ.BasisY;
        //        XYZ pb = p4 - p3;
        //        vector.Add(pb);
        //        XYZ ph = p4a - p3;
        //        vector.Add(ph);
        //        return vector;
        //    }
        //}
        //private void CreateStirrupTypeItem(SectionStyle sectionStyle, InfoModel infoModel, Document document, UnitProject unit, double Cover, int n, double s, double location0, bool start, string name)
        //{
        //    List<XYZ> origin = GetOriginVectorStirrup(sectionStyle, infoModel, unit, Cover, location0);
        //    Rebar bar = Rebar.CreateFromRebarShape(document, BarS.RebarShape, BarS.RebarBarType, infoModel.Element, origin[0], origin[1], origin[2]);
        //    BarS.RebarStirrup.Add(bar);
        //    RebarShapeDrivenAccessor rebarShape1 = bar.GetShapeDrivenAccessor();
        //    List<XYZ> vector = GetScaleBoxStirrup(sectionStyle, infoModel, unit, Cover);
        //    rebarShape1.ScaleToBox(origin[0], vector[0], vector[1]);
        //    if (start)
        //    {
        //        rebarShape1.SetLayoutAsNumberWithSpacing(n, unit.Convert(s), true, true, true);
        //    }
        //    else
        //    {
        //        rebarShape1.SetLayoutAsNumberWithSpacing(n, unit.Convert(s), true, false, false);
        //    }
        //    BarS.SetPartitionRebarStirrup(name);
        //}
        //private void CreateStirrupTypeItem1(SectionStyle sectionStyle, InfoModel infoModel, Document document, UnitProject unit, double Cover, string name)
        //{
        //    int n = (int)(L / S) + 1;
        //    double o1 = (L - (n - 1) * S) / 2;
        //    CreateStirrupTypeItem(sectionStyle, infoModel, document, unit, Cover, n, S, o1, true, name);
        //}
        //private void CreateStirrupTypeItem2(SectionStyle sectionStyle, InfoModel infoModel, Document document, UnitProject unit, double Cover, string name)
        //{
        //    int n1 = (int)(L1 / S1) + 1;
        //    int n2 = (int)(L2 / S2) + 1;
        //    double delta1 = (L1 - (n1 - 1) * S1) / 2;
        //    double delta2 = (L2 - (n2 - 1) * S2) / 2;
        //    double o1 = delta1;
        //    double o2 = delta2 + L1;
        //    double o3 = delta1 + L1 + L2;
        //    CreateStirrupTypeItem(sectionStyle, infoModel, document, unit, Cover, n1, S1, o1, true, name);
        //    CreateStirrupTypeItem(sectionStyle, infoModel, document, unit, Cover, n2, S2, o2, true, name);
        //    CreateStirrupTypeItem(sectionStyle, infoModel, document, unit, Cover, n1, S1, o3, true, name);
        //}
        //public void CreateStirrup(SectionStyle sectionStyle, InfoModel infoModel, Document document, UnitProject unit, SettingModel settingModel, double Cover)
        //{
        //    BarS.GetRebarShape(sectionStyle, document, "Stirrup", settingModel);
        //    if (BarS.RebarShape == null)
        //    {
        //        System.Windows.Forms.MessageBox.Show("There are no existing Stirrup RebarShape" + "\n" + "Please Load Rebar Shape Family", "ERROR", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Error);
        //        return;
        //    }
        //    if (TypeDis == 0)
        //    {
        //        CreateStirrupTypeItem1(sectionStyle, infoModel, document, unit, Cover, settingModel.ColumnsName);
        //    }
        //    else
        //    {
        //        CreateStirrupTypeItem2(sectionStyle, infoModel, document, unit, Cover, settingModel.ColumnsName);
        //    }
        //}
        #endregion
        #region Addtitional stirrup
        //private List<XYZ> GetOriginVectorAntiHorizontalRectangle(InfoModel infoModel, UnitProject unit, double Cover, double location0, double x0)
        //{
        //    List<XYZ> vector = new List<XYZ>();
        //    XYZ p1 = PointModel.ProjectToPlane(infoModel.West.Origin, infoModel.Nouth);
        //    double cover = unit.Convert(Cover);
        //    double x = unit.Convert(x0);
        //    double location = unit.Convert(location0);
        //    XYZ p2 = p1 + cover * infoModel.South.FaceNormal;
        //    XYZ p3 = p2 + x * infoModel.East.FaceNormal;
        //    XYZ p4 = PointModel.ProjectToPlane(p3, infoModel.Bottom);
        //    XYZ p5 = p4 + location * XYZ.BasisZ;
        //    vector.Add(p5);
        //    Transform t = Transform.CreateRotation(infoModel.East.FaceNormal, Math.PI / 2);
        //    vector.Add(t.BasisZ);
        //    vector.Add(t.BasisX);
        //    return vector;
        //}
        //private List<XYZ> GetScaleBoxAntiHorizontalRectangle(InfoModel infoModel, UnitProject unit, double Cover, double x0)
        //{
        //    List<XYZ> vector = new List<XYZ>();
        //    XYZ p1 = PointModel.ProjectToPlane(infoModel.West.Origin, infoModel.Nouth);
        //    double cover = unit.Convert(Cover);
        //    double x = unit.Convert(x0);
        //    double h = unit.Convert(infoModel.h);
        //    XYZ p2 = p1 + cover * infoModel.South.FaceNormal;
        //    XYZ p3 = p2 + x * infoModel.East.FaceNormal;
        //    XYZ p4 = PointModel.ProjectToPlane(p3, infoModel.Bottom);
        //    vector.Add(infoModel.East.FaceNormal);
        //    XYZ p5 = p4 + (h -2* cover) * infoModel.South.FaceNormal;
        //    XYZ ph = p5 - p4;
        //    vector.Add(ph);
        //    return vector;
        //}
        //private List<XYZ> GetOriginVectorAntiVerticalRectangle(InfoModel infoModel, UnitProject unit, double Cover, double location0, double y0)
        //{
        //    List<XYZ> vector = new List<XYZ>();
        //    XYZ p1 = PointModel.ProjectToPlane(infoModel.South.Origin, infoModel.West);
        //    double cover = unit.Convert(Cover);
        //    double y = unit.Convert(y0);
        //    double location = unit.Convert(location0);
        //    XYZ p2 = p1 + cover * infoModel.East.FaceNormal;
        //    XYZ p3 = p2 + y * infoModel.Nouth.FaceNormal;
        //    XYZ p4 = PointModel.ProjectToPlane(p3, infoModel.Bottom);
        //    XYZ p5 = p4 + location * XYZ.BasisZ;
        //    vector.Add(p5);
        //    Transform t = Transform.CreateRotation(infoModel.East.FaceNormal, Math.PI / 2);
        //    vector.Add(t.BasisZ);
        //    vector.Add(t.BasisX);
        //    return vector;
        //}
        //private List<XYZ> GetScaleBoxAntiVerticalRectangle(InfoModel infoModel, UnitProject unit, double Cover, double y0)
        //{
        //    List<XYZ> vector = new List<XYZ>();
        //    XYZ p1 = PointModel.ProjectToPlane(infoModel.South.Origin, infoModel.West);
        //    double cover = unit.Convert(Cover);
        //    double y = unit.Convert(y0);
        //    double b = unit.Convert(infoModel.b);
        //    XYZ p2 = p1 + cover * infoModel.East.FaceNormal;
        //    XYZ p3 = p2 + y * infoModel.Nouth.FaceNormal;
        //    XYZ p4 = PointModel.ProjectToPlane(p3, infoModel.Bottom);
        //    vector.Add(infoModel.Nouth.FaceNormal);
        //    XYZ p5 = p4 + (b -2* cover) * infoModel.East.FaceNormal;
        //    XYZ ph = p5 - p4;
        //    vector.Add(ph);
        //    return vector;
        //}
        //private List<XYZ> GetOriginVectorAddStirrupCylindrical(InfoModel infoModel, UnitProject unit, double Cover, double location0)
        //{
        //    List<XYZ> vector = new List<XYZ>();
        //    XYZ p1 = PointModel.ProjectToPlane(infoModel.PointPosition, infoModel.Bottom);
        //    double Dsqrt = unit.Convert(Math.Sqrt(2) * infoModel.T * 0.5 - Cover + BarH.Diameter);
        //    double location = unit.Convert(location0);
        //    XYZ p2 = p1 + (Dsqrt / 2) * (-1) * XYZ.BasisX;
        //    XYZ p3 = p2 + (Dsqrt / 2) * (-1) * XYZ.BasisY;
        //    XYZ p4 = p3 + location * XYZ.BasisZ;
        //    vector.Add(p4);
        //    Transform t = Transform.CreateRotation(XYZ.BasisX, Math.PI / 2);
        //    vector.Add(t.BasisZ);
        //    vector.Add(t.BasisX);
        //    return vector;
        //}
        //private List<XYZ> GetScaleBoxAddStirrupCylindrical(InfoModel infoModel, UnitProject unit, double Cover)
        //{
        //    List<XYZ> vector = new List<XYZ>();
        //    XYZ p1 = PointModel.ProjectToPlane(infoModel.PointPosition, infoModel.Bottom);
        //    double Dsqrt = unit.Convert(Math.Sqrt(2) * infoModel.T * 0.5 - Cover + BarH.Diameter);
        //    XYZ p2 = p1 + (Dsqrt / 2) * (-1) * XYZ.BasisX;
        //    XYZ p3 = p1 + (Dsqrt / 2) * (-1) * XYZ.BasisY;
        //    XYZ p4 = p3 + (Dsqrt) * XYZ.BasisX;
        //    XYZ p5 = p3 + (Dsqrt) * XYZ.BasisY;
        //    XYZ pb = p4 - p3;
        //    vector.Add(pb);
        //    XYZ ph = p5 - p3;
        //    vector.Add(ph);
        //    return vector;
        //}
        //private List<XYZ> GetOriginVectorAntiHorizontalCylindrical(InfoModel infoModel, UnitProject unit, double Cover, double location0)
        //{
        //    List<XYZ> vector = new List<XYZ>();
        //    XYZ p1 = PointModel.ProjectToPlane(infoModel.PointPosition, infoModel.Bottom);
        //    double cover = unit.Convert(Cover);
        //    double x = unit.Convert(infoModel.T / 2 )-cover;
        //    double location = unit.Convert(location0);
        //    XYZ p2 = p1 + x * (-1) * XYZ.BasisX;
        //    XYZ p3 = p2 + (unit.Convert(BarV.Diameter)) * (-1) * XYZ.BasisY;
        //    XYZ p4 = p3 + location * XYZ.BasisZ;
        //    vector.Add(p4);
        //    Transform t = Transform.CreateRotation(XYZ.BasisX, Math.PI / 2);
        //    vector.Add(t.BasisZ);
        //    vector.Add(t.BasisX);
        //    return vector;
        //}
        //private List<XYZ> GetScaleBoxAntiHorizontalCylindrical(InfoModel infoModel, UnitProject unit, double Cover)
        //{
        //    List<XYZ> vector = new List<XYZ>();
        //    XYZ p1 = PointModel.ProjectToPlane(infoModel.PointPosition, infoModel.Bottom);
        //    double cover = unit.Convert(Cover);
        //    double x = unit.Convert(infoModel.T / 2 ) - cover;
        //    double h = unit.Convert(infoModel.T);
        //    XYZ p2 = p1 + x * (-1) * XYZ.BasisX;
        //    XYZ p3 = p2 + (unit.Convert(BarV.Diameter)) * (-1) * XYZ.BasisY;

        //    vector.Add(XYZ.BasisY);
        //    XYZ p4 = p3 + (h - 2*cover) * XYZ.BasisX;
        //    XYZ ph = p4 - p3;
        //    vector.Add(ph);
        //    return vector;
        //}
        //private List<XYZ> GetOriginVectorAntiVerticalCylindrical(InfoModel infoModel, UnitProject unit, double Cover, double location0)
        //{
        //    List<XYZ> vector = new List<XYZ>();
        //    XYZ p1 = PointModel.ProjectToPlane(infoModel.PointPosition, infoModel.Bottom);
        //    double cover = unit.Convert(Cover);
        //    double y = unit.Convert(infoModel.T / 2 ) - cover;
        //    double location = unit.Convert(location0);
        //    XYZ p2 = p1 + y * XYZ.BasisY;
        //    XYZ p3 = p2 + (unit.Convert(BarV.Diameter)) * (-1) * XYZ.BasisX;
        //    XYZ p4 = p3 + location * XYZ.BasisZ;
        //    vector.Add(p4);
        //    Transform t = Transform.CreateRotation(XYZ.BasisX, Math.PI / 2);
        //    vector.Add(t.BasisZ);
        //    vector.Add(t.BasisX);
        //    return vector;
        //}
        //private List<XYZ> GetScaleBoxAntiVerticalCylindrical(InfoModel infoModel, UnitProject unit, double Cover)
        //{
        //    List<XYZ> vector = new List<XYZ>();
        //    XYZ p1 = PointModel.ProjectToPlane(infoModel.PointPosition, infoModel.Bottom);
        //    double cover = unit.Convert(Cover);
        //    double y = unit.Convert(infoModel.T / 2 ) - cover;
        //    double b = unit.Convert(infoModel.T);
        //    XYZ p2 = p1 + y * XYZ.BasisY;
        //    XYZ p3 = p2 + (unit.Convert(BarV.Diameter)) * (-1) * XYZ.BasisX;
        //    vector.Add(XYZ.BasisX);
        //    XYZ p4 = p3 + (b - 2*cover) * (-1) * XYZ.BasisY;
        //    XYZ ph = p4 - p3;
        //    vector.Add(ph);
        //    return vector;
        //}
        //private void CreateAddHorizontalStirrupRectangleType0Item(InfoModel infoModel, Document document, UnitProject unit, double Cover, double location0, bool start, string name, int n, double s)
        //{
        //    if (aH != 0)
        //    {
        //        List<XYZ> origin = GetOriginVectorStirrupRectangle(infoModel, unit, Cover, location0, (infoModel.b - 2 * Cover - aH) / 2, 0);
        //        Rebar bar = Rebar.CreateFromRebarShape(document, BarH.RebarShape, BarH.RebarBarType, infoModel.Element, origin[0], origin[1], origin[2]);
        //        BarH.RebarStirrup.Add(bar);
        //        RebarShapeDrivenAccessor rebarShape1 = bar.GetShapeDrivenAccessor();
        //        List<XYZ> vector = GetScaleBoxStirrupRectangle(infoModel, unit, Cover, infoModel.b - (infoModel.b - 2 * Cover - aH) / 2, infoModel.h, (infoModel.b - 2 * Cover - aH) / 2, 0);
        //        rebarShape1.ScaleToBox(origin[0], vector[0], vector[1]);
        //        if (start)
        //        {
        //            rebarShape1.SetLayoutAsNumberWithSpacing(n, unit.Convert(s), true, true, true);
        //        }
        //        else
        //        {
        //            rebarShape1.SetLayoutAsNumberWithSpacing(n, unit.Convert(s), true, false, false);
        //        }
        //        BarH.SetPartitionRebarStirrup(name);
        //    }

        //}
        //private void CreateAddHorizontalStirrupRectangleType1Item(InfoModel infoModel, Document document, UnitProject unit, double Cover, double location0, double x0, bool start, string name, int n, double s)
        //{
        //    List<XYZ> origin = GetOriginVectorAntiHorizontalRectangle(infoModel, unit, Cover, location0, x0);
        //    Rebar bar = Rebar.CreateFromRebarShape(document, BarH.RebarShape, BarH.RebarBarType, infoModel.Element, origin[0], origin[1], origin[2]);
        //    BarH.RebarStirrup.Add(bar);
        //    RebarShapeDrivenAccessor rebarShape1 = bar.GetShapeDrivenAccessor();
        //    List<XYZ> vector = GetScaleBoxAntiHorizontalRectangle(infoModel, unit, Cover, x0);
        //    rebarShape1.ScaleToBox(origin[0], vector[1], vector[0]);
        //    if (start)
        //    {
        //        rebarShape1.SetLayoutAsNumberWithSpacing(n, unit.Convert(s), true, true, true);
        //    }
        //    else
        //    {
        //        rebarShape1.SetLayoutAsNumberWithSpacing(n, unit.Convert(s), true, false, false);
        //    }
        //    BarH.SetPartitionRebarStirrup(name);
        //}
        //private void CreateAddVerticalStirrupRectangleType0Item(InfoModel infoModel, Document document, UnitProject unit, double Cover, double location0, bool start, string name, int n, double s)
        //{
        //    if (aV != 0)
        //    {
        //        List<XYZ> origin = GetOriginVectorStirrupRectangle(infoModel, unit, Cover, location0, 0, (infoModel.h - 2 * Cover - aV) / 2);
        //        Rebar bar = Rebar.CreateFromRebarShape(document, BarV.RebarShape, BarV.RebarBarType, infoModel.Element, origin[0], origin[1], origin[2]);
        //        BarV.RebarStirrup.Add(bar);
        //        RebarShapeDrivenAccessor rebarShape1 = bar.GetShapeDrivenAccessor();
        //        List<XYZ> vector = GetScaleBoxStirrupRectangle(infoModel, unit, Cover, infoModel.b, infoModel.h - (infoModel.h - 2 * Cover - aV) / 2, 0, (infoModel.h - 2 * Cover - aV) / 2);
        //        rebarShape1.ScaleToBox(origin[0], vector[0], vector[1]);
        //        if (start)
        //        {
        //            rebarShape1.SetLayoutAsNumberWithSpacing(n, unit.Convert(s), true, true, true);
        //        }
        //        else
        //        {
        //            rebarShape1.SetLayoutAsNumberWithSpacing(n, unit.Convert(s), true, false, false);
        //        }
        //        BarV.SetPartitionRebarStirrup(name);
        //    }

        //}
        //private void CreateAddVerticalStirrupRectangleType1Item(InfoModel infoModel, Document document, UnitProject unit, double Cover, double location0, double y0, bool start, string name, int n, double s)
        //{
        //    List<XYZ> origin = GetOriginVectorAntiVerticalRectangle(infoModel, unit, Cover, location0, y0);
        //    Rebar bar = Rebar.CreateFromRebarShape(document, BarH.RebarShape, BarH.RebarBarType, infoModel.Element, origin[0], origin[1], origin[2]);
        //    BarH.RebarStirrup.Add(bar);
        //    RebarShapeDrivenAccessor rebarShape1 = bar.GetShapeDrivenAccessor();
        //    List<XYZ> vector = GetScaleBoxAntiVerticalRectangle(infoModel, unit, Cover, y0);
        //    rebarShape1.ScaleToBox(origin[0], vector[1], vector[0]);
        //    if (start)
        //    {
        //        rebarShape1.SetLayoutAsNumberWithSpacing(n, unit.Convert(s), true, true, true);
        //    }
        //    else
        //    {
        //        rebarShape1.SetLayoutAsNumberWithSpacing(n, unit.Convert(s), true, false, false);
        //    }
        //    BarH.SetPartitionRebarStirrup(name);
        //}
        //private void CreateAddHorizontalStirrupCylindricalItem(InfoModel infoModel, Document document, UnitProject unit, double Cover, double location0, bool start, string name, int n, double s)
        //{
        //    if (AddH)
        //    {
        //        List<XYZ> origin = GetOriginVectorAddStirrupCylindrical(infoModel, unit, Cover, location0);
        //        Rebar bar = Rebar.CreateFromRebarShape(document, BarH.RebarShape, BarH.RebarBarType, infoModel.Element, origin[0], origin[1], origin[2]);
        //        BarH.RebarStirrup.Add(bar);
        //        RebarShapeDrivenAccessor rebarShape1 = bar.GetShapeDrivenAccessor();
        //        List<XYZ> vector = GetScaleBoxAddStirrupCylindrical(infoModel, unit, Cover);
        //        rebarShape1.ScaleToBox(origin[0], vector[0], vector[1]);
        //        if (start)
        //        {
        //            rebarShape1.SetLayoutAsNumberWithSpacing(n, unit.Convert(s), true, true, true);
        //        }
        //        else
        //        {
        //            rebarShape1.SetLayoutAsNumberWithSpacing(n, unit.Convert(s), true, false, false);
        //        }
        //        BarH.SetPartitionRebarStirrup(name);
        //    }

        //}
        //private void CreateAddVerticalStirrupCylindricalItem(InfoModel infoModel, Document document, UnitProject unit, double Cover, double location0, bool start, string name, int n, double s)
        //{
        //    if (AddV)
        //    {
        //        // Horizontal
        //        List<XYZ> origin = GetOriginVectorAntiHorizontalCylindrical(infoModel, unit, Cover, location0);
        //        Rebar bar = Rebar.CreateFromRebarShape(document, BarV.RebarShape, BarV.RebarBarType, infoModel.Element, origin[0], origin[1], origin[2]);
        //        BarV.RebarStirrup.Add(bar);
        //        RebarShapeDrivenAccessor rebarShape1 = bar.GetShapeDrivenAccessor();
        //        List<XYZ> vector = GetScaleBoxAntiHorizontalCylindrical(infoModel, unit, Cover);
        //        rebarShape1.ScaleToBox(origin[0], vector[1], vector[0]);
        //        if (start)
        //        {
        //            rebarShape1.SetLayoutAsNumberWithSpacing(n, unit.Convert(s), true, true, true);
        //        }
        //        else
        //        {
        //            rebarShape1.SetLayoutAsNumberWithSpacing(n, unit.Convert(s), true, false, false);
        //        }
        //        List<XYZ> origin1 = GetOriginVectorAntiVerticalCylindrical(infoModel, unit, Cover, location0);
        //        Rebar bar1 = Rebar.CreateFromRebarShape(document, BarV.RebarShape, BarV.RebarBarType, infoModel.Element, origin1[0], origin1[1], origin1[2]);
        //        BarV.RebarStirrup.Add(bar1);
        //        RebarShapeDrivenAccessor rebarShape12 = bar1.GetShapeDrivenAccessor();
        //        List<XYZ> vector1 = GetScaleBoxAntiVerticalCylindrical(infoModel, unit, Cover);
        //        rebarShape12.ScaleToBox(origin1[0], vector1[1], vector1[0]);
        //        if (start)
        //        {
        //            rebarShape12.SetLayoutAsNumberWithSpacing(n, unit.Convert(s), true, true, true);
        //        }
        //        else
        //        {
        //            rebarShape12.SetLayoutAsNumberWithSpacing(n, unit.Convert(s), true, false, false);
        //        }
        //        BarV.SetPartitionRebarStirrup(name);
        //    }

        //}

        //private void CreateAddHorizontalStirrupRectangleType0(InfoModel infoModel, Document document, UnitProject unit, double Cover, string name)
        //{
        //    if (TypeDis == 0)
        //    {
        //        int n = (int)(L / S) + 1;
        //        double o1 = (L - (n - 1) * S) / 2 + BarS.Diameter;
        //        CreateAddHorizontalStirrupRectangleType0Item(infoModel, document, unit, Cover, o1, true, name, n, S);
        //    }
        //    else
        //    {
        //        int n1 = (int)(L1 / S1) + 1;
        //        int n2 = (int)(L2 / S2) + 1;
        //        double delta1 = (L1 - (n1 - 1) * S1) / 2;
        //        double delta2 = (L2 - (n2 - 1) * S2) / 2;
        //        double o1 = delta1 + BarS.Diameter;
        //        double o2 = delta2 + L1 + BarS.Diameter;
        //        double o3 = delta1 + L1 + L2 + BarS.Diameter;
        //        CreateAddHorizontalStirrupRectangleType0Item(infoModel, document, unit, Cover, o1, true, name, n1, S1);
        //        CreateAddHorizontalStirrupRectangleType0Item(infoModel, document, unit, Cover, o2, true, name, n2, S2);
        //        CreateAddHorizontalStirrupRectangleType0Item(infoModel, document, unit, Cover, o3, true, name, n1, S1);
        //    }
        //}
        //private void CreateAddHorizontalStirrupRectangleType1(InfoModel infoModel, Document document, UnitProject unit, double Cover, double x0, string name)
        //{
        //    if (TypeDis == 0)
        //    {
        //        int n = (int)(L / S) + 1;
        //        double o1 = (L - (n - 1) * S) / 2 + BarS.Diameter;
        //        CreateAddHorizontalStirrupRectangleType1Item(infoModel, document, unit, Cover, o1, x0, true, name, n, S);
        //    }
        //    else
        //    {
        //        int n1 = (int)(L1 / S1) + 1;
        //        int n2 = (int)(L2 / S2) + 1;
        //        double delta1 = (L1 - (n1 - 1) * S1) / 2;
        //        double delta2 = (L2 - (n2 - 1) * S2) / 2;
        //        double o1 = delta1 + BarS.Diameter;
        //        double o2 = delta2 + L1 + BarS.Diameter;
        //        double o3 = delta1 + L1 + L2 + BarS.Diameter;
        //        CreateAddHorizontalStirrupRectangleType1Item(infoModel, document, unit, Cover, o1, x0, true, name, n1, S1);
        //        CreateAddHorizontalStirrupRectangleType1Item(infoModel, document, unit, Cover, o2, x0, true, name, n2, S2);
        //        CreateAddHorizontalStirrupRectangleType1Item(infoModel, document, unit, Cover, o3, x0, true, name, n1, S1);
        //    }
        //}
        //private void CreateAddVerticalStirrupRectangleType0(InfoModel infoModel, Document document, UnitProject unit, double Cover, string name)
        //{
        //    if (TypeDis == 0)
        //    {
        //        int n = (int)(L / S) + 1;
        //        double o1 = (L - (n - 1) * S) / 2 + BarS.Diameter + BarH.Diameter;
        //        CreateAddVerticalStirrupRectangleType0Item(infoModel, document, unit, Cover, o1, true, name, n, S);
        //    }
        //    else
        //    {
        //        int n1 = (int)(L1 / S1) + 1;
        //        int n2 = (int)(L2 / S2) + 1;
        //        double delta1 = (L1 - (n1 - 1) * S1) / 2;
        //        double delta2 = (L2 - (n2 - 1) * S2) / 2;
        //        double o1 = delta1 + BarS.Diameter + BarH.Diameter;
        //        double o2 = delta2 + L1 + BarS.Diameter + BarH.Diameter;
        //        double o3 = delta1 + L1 + L2 + BarS.Diameter + BarH.Diameter;
        //        CreateAddVerticalStirrupRectangleType0Item(infoModel, document, unit, Cover, o1, true, name, n1, S1);
        //        CreateAddVerticalStirrupRectangleType0Item(infoModel, document, unit, Cover, o2, true, name, n2, S2);
        //        CreateAddVerticalStirrupRectangleType0Item(infoModel, document, unit, Cover, o3, true, name, n1, S1);
        //    }
        //}
        //private void CreateAddVerticalStirrupRectangleType1(InfoModel infoModel, Document document, UnitProject unit, double Cover, double y0, string name)
        //{
        //    if (TypeDis == 0)
        //    {
        //        int n = (int)(L / S) + 1;
        //        double o1 = (L - (n - 1) * S) / 2 + BarS.Diameter + BarH.Diameter;
        //        CreateAddVerticalStirrupRectangleType1Item(infoModel, document, unit, Cover, o1, y0, true, name, n, S);
        //    }
        //    else
        //    {
        //        int n1 = (int)(L1 / S1) + 1;
        //        int n2 = (int)(L2 / S2) + 1;
        //        double delta1 = (L1 - (n1 - 1) * S1) / 2;
        //        double delta2 = (L2 - (n2 - 1) * S2) / 2;
        //        double o1 = delta1 + BarS.Diameter + BarH.Diameter;
        //        double o2 = delta2 + L1 + BarS.Diameter + BarH.Diameter;
        //        double o3 = delta1 + L1 + L2 + BarS.Diameter + BarH.Diameter;
        //        CreateAddVerticalStirrupRectangleType1Item(infoModel, document, unit, Cover, o1, y0, true, name, n1, S1);
        //        CreateAddVerticalStirrupRectangleType1Item(infoModel, document, unit, Cover, o2, y0, true, name, n2, S2);
        //        CreateAddVerticalStirrupRectangleType1Item(infoModel, document, unit, Cover, o3, y0, true, name, n1, S1);
        //    }
        //}
        //private void CreateAddHorizontalStirrupCylindrical(SectionStyle sectionStyle, InfoModel infoModel, Document document, UnitProject unit, SettingModel settingModel, double Cover, string name)
        //{
        //    BarH.GetRebarShape(sectionStyle, document, "Stirrup Cylindrical", settingModel);
        //    if (TypeDis == 0)
        //    {
        //        int n = (int)(L / S) + 1;
        //        double o1 = (L - (n - 1) * S) / 2 + BarS.Diameter;
        //        CreateAddHorizontalStirrupCylindricalItem(infoModel, document, unit, Cover, o1, true, name, n, S);
        //    }
        //    else
        //    {
        //        int n1 = (int)(L1 / S1) + 1;
        //        int n2 = (int)(L2 / S2) + 1;
        //        double delta1 = (L1 - (n1 - 1) * S1) / 2;
        //        double delta2 = (L2 - (n2 - 1) * S2) / 2;
        //        double o1 = delta1 + BarS.Diameter;
        //        double o2 = delta2 + L1 + BarS.Diameter;
        //        double o3 = delta1 + L1 + L2 + BarS.Diameter;
        //        CreateAddHorizontalStirrupCylindricalItem(infoModel, document, unit, Cover, o1, true, name, n1, S1);
        //        CreateAddHorizontalStirrupCylindricalItem(infoModel, document, unit, Cover, o2, true, name, n2, S2);
        //        CreateAddHorizontalStirrupCylindricalItem(infoModel, document, unit, Cover, o3, true, name, n1, S1);
        //    }
        //}
        //private void CreateAddVerticalStirrupCylindrical(SectionStyle sectionStyle, InfoModel infoModel, Document document, UnitProject unit, SettingModel settingModel, double Cover, string name)
        //{
        //    switch (TypeV)
        //    {
        //        case 0:
        //            BarV.RebarShape = settingModel.RebarShapes.Where(x => x.Name == "M_T10B").FirstOrDefault();
        //            break;
        //        case 1:
        //            BarV.RebarShape = settingModel.RebarShapes.Where(x => x.Name == "M_T10").FirstOrDefault();
        //            break;
        //        case 2:
        //            BarV.RebarShape = settingModel.RebarShapes.Where(x => x.Name == "M_T10C").FirstOrDefault();
        //            break;
        //        default:
        //            break;
        //    }
        //    if (BarV.RebarShape == null)
        //    {
        //        BarV.RebarShape = settingModel.RebarShapes.Where(x => x.Name == "M_T10").FirstOrDefault();
        //    }
        //    if (TypeDis == 0)
        //    {
        //        int n = (int)(L / S) + 1;
        //        double o1 = (L - (n - 1) * S) / 2 + BarS.Diameter;
        //        CreateAddVerticalStirrupCylindricalItem(infoModel, document, unit, Cover, o1, true, name, n, S);
        //    }
        //    else
        //    {
        //        int n1 = (int)(L1 / S1) + 1;
        //        int n2 = (int)(L2 / S2) + 1;
        //        double delta1 = (L1 - (n1 - 1) * S1) / 2;
        //        double delta2 = (L2 - (n2 - 1) * S2) / 2;
        //        double o1 = delta1 + BarS.Diameter;
        //        double o2 = delta2 + L1 + BarS.Diameter;
        //        double o3 = delta1 + L1 + L2 + BarS.Diameter;
        //        CreateAddVerticalStirrupCylindricalItem(infoModel, document, unit, Cover, o1, true, name, n1, S1);
        //        CreateAddVerticalStirrupCylindricalItem(infoModel, document, unit, Cover, o2, true, name, n2, S2);
        //        CreateAddVerticalStirrupCylindricalItem(infoModel, document, unit, Cover, o3, true, name, n1, S1);
        //    }
        //}
        //private void CreateAddHorizontalStirrupRectangle(SectionStyle sectionStyle, InfoModel infoModel, Document document, UnitProject unit, SettingModel settingModel, double Cover)
        //{
        //    if (AddH)
        //    {
        //        if (TypeH == 0)
        //        {
        //            BarH.GetRebarShape(sectionStyle, document, "Stirrup", settingModel);
        //            CreateAddHorizontalStirrupRectangleType0(infoModel, document, unit, Cover, settingModel.ColumnsName);
        //        }
        //        else
        //        {

        //            switch (TypeH)
        //            {
        //                case 1:
        //                    BarH.RebarShape = settingModel.RebarShapes.Where(x => x.Name == "M_T10B").FirstOrDefault();
        //                    break;
        //                case 2:
        //                    BarH.RebarShape = settingModel.RebarShapes.Where(x => x.Name == "M_T10").FirstOrDefault();
        //                    break;
        //                case 3:
        //                    BarH.RebarShape = settingModel.RebarShapes.Where(x => x.Name == "M_T10C").FirstOrDefault();
        //                    break;
        //                default:
        //                    break;
        //            }
        //            if (BarH.RebarShape == null)
        //            {
        //                BarH.RebarShape = settingModel.RebarShapes.Where(x => x.Name == "M_T10").FirstOrDefault();
        //            }
        //            double delta = (infoModel.b - 2 * Cover) / (nH + 1);
        //            for (int i = 0; i < nH; i++)
        //            {
        //                CreateAddHorizontalStirrupRectangleType1(infoModel, document, unit, Cover, Cover + (i+1)*delta, settingModel.ColumnsName);
        //            }
        //            //switch (nH)
        //            //{
        //            //    case 1:
        //            //        CreateAddHorizontalStirrupRectangleType1(infoModel, document, unit, Cover, infoModel.b * 0.5, settingModel.ColumnsName);
        //            //        break;
        //            //    case 2:
        //            //        CreateAddHorizontalStirrupRectangleType1(infoModel, document, unit, Cover, Cover + delta, settingModel.ColumnsName);
        //            //        CreateAddHorizontalStirrupRectangleType1(infoModel, document, unit, Cover, Cover + 2 * delta, settingModel.ColumnsName);
        //            //        break;
        //            //    case 3:
        //            //        CreateAddHorizontalStirrupRectangleType1(infoModel, document, unit, Cover, Cover + delta, settingModel.ColumnsName);
        //            //        CreateAddHorizontalStirrupRectangleType1(infoModel, document, unit, Cover, Cover + 2 * delta, settingModel.ColumnsName);
        //            //        CreateAddHorizontalStirrupRectangleType1(infoModel, document, unit, Cover, Cover + 3 * delta, settingModel.ColumnsName);
        //            //        break;
        //            //    case 4:
        //            //        CreateAddHorizontalStirrupRectangleType1(infoModel, document, unit, Cover, Cover + delta, settingModel.ColumnsName);
        //            //        CreateAddHorizontalStirrupRectangleType1(infoModel, document, unit, Cover, Cover + 2 * delta, settingModel.ColumnsName);
        //            //        CreateAddHorizontalStirrupRectangleType1(infoModel, document, unit, Cover, Cover + 3 * delta, settingModel.ColumnsName);
        //            //        CreateAddHorizontalStirrupRectangleType1(infoModel, document, unit, Cover, Cover + 4 * delta, settingModel.ColumnsName);
        //            //        break;
        //            //    default:
        //            //        break;
        //            //}

        //        }

        //    }
        //}
        //private void CreateAddVerticalStirrupRectangle(SectionStyle sectionStyle, InfoModel infoModel, Document document, UnitProject unit, SettingModel settingModel, double Cover)
        //{
        //    if (AddV)
        //    {
        //        if (TypeV == 0)
        //        {
        //            BarV.GetRebarShape(sectionStyle, document, "Stirrup", settingModel);
        //            CreateAddVerticalStirrupRectangleType0(infoModel, document, unit, Cover, settingModel.ColumnsName);
        //        }
        //        else
        //        {
        //            switch (TypeV)
        //            {
        //                case 1:
        //                    BarV.RebarShape = settingModel.RebarShapes.Where(x => x.Name == "M_T10B").FirstOrDefault();
        //                    break;
        //                case 2:
        //                    BarV.RebarShape = settingModel.RebarShapes.Where(x => x.Name == "M_T10").FirstOrDefault();
        //                    break;
        //                case 3:
        //                    BarV.RebarShape = settingModel.RebarShapes.Where(x => x.Name == "M_T10C").FirstOrDefault();
        //                    break;
        //                default:
        //                    break;
        //            }
        //            if (BarV.RebarShape == null)
        //            {
        //                BarV.RebarShape = settingModel.RebarShapes.Where(x => x.Name == "M_T10").FirstOrDefault();
        //            }
        //            double delta = (infoModel.h - 2 * Cover) / (nV + 1);
        //            for (int i = 0; i < nV; i++)
        //            {
        //                CreateAddVerticalStirrupRectangleType1(infoModel, document, unit, Cover, Cover + (i+1)*delta, settingModel.ColumnsName);
        //            }
        //            //switch (nV)
        //            //{
        //            //    case 1:
        //            //        CreateAddVerticalStirrupRectangleType1(infoModel, document, unit, Cover, infoModel.h * 0.5, settingModel.ColumnsName);
        //            //        break;
        //            //    case 2:
        //            //        CreateAddVerticalStirrupRectangleType1(infoModel, document, unit, Cover, Cover + delta, settingModel.ColumnsName);
        //            //        CreateAddVerticalStirrupRectangleType1(infoModel, document, unit, Cover, Cover + 2 * delta, settingModel.ColumnsName);
        //            //        break;
        //            //    case 3:
        //            //        CreateAddVerticalStirrupRectangleType1(infoModel, document, unit, Cover, Cover + delta, settingModel.ColumnsName);
        //            //        CreateAddVerticalStirrupRectangleType1(infoModel, document, unit, Cover, Cover + 2 * delta, settingModel.ColumnsName);
        //            //        CreateAddVerticalStirrupRectangleType1(infoModel, document, unit, Cover, Cover + 3 * delta, settingModel.ColumnsName);
        //            //        break;
        //            //    case 4:
        //            //        CreateAddVerticalStirrupRectangleType1(infoModel, document, unit, Cover, Cover + delta, settingModel.ColumnsName);
        //            //        CreateAddVerticalStirrupRectangleType1(infoModel, document, unit, Cover, Cover + 2 * delta, settingModel.ColumnsName);
        //            //        CreateAddVerticalStirrupRectangleType1(infoModel, document, unit, Cover, Cover + 3 * delta, settingModel.ColumnsName);
        //            //        CreateAddVerticalStirrupRectangleType1(infoModel, document, unit, Cover, Cover + 4 * delta, settingModel.ColumnsName);
        //            //        break;
        //            //    default:
        //            //        break;
        //            //}
        //        }
        //    }
        //}
        //public void CreateAddHorizontalStirrup(SectionStyle sectionStyle, InfoModel infoModel, Document document, UnitProject unit, SettingModel settingModel, double Cover)
        //{

        //    if (sectionStyle == SectionStyle.RECTANGLE)
        //    {
        //        CreateAddHorizontalStirrupRectangle(sectionStyle, infoModel, document, unit, settingModel, Cover);
        //    }
        //    else
        //    {
        //        CreateAddHorizontalStirrupCylindrical(sectionStyle, infoModel, document, unit, settingModel, Cover, settingModel.ColumnsName);
        //    }
        //}
        //public void CreateAddVerticalStirrup(SectionStyle sectionStyle, InfoModel infoModel, Document document, UnitProject unit, SettingModel settingModel, double Cover)
        //{
        //    if (sectionStyle == SectionStyle.RECTANGLE)
        //    {
        //        CreateAddVerticalStirrupRectangle(sectionStyle, infoModel, document, unit, settingModel, Cover);
        //    }
        //    else
        //    {
        //        CreateAddVerticalStirrupCylindrical(sectionStyle, infoModel, document, unit, settingModel, Cover, settingModel.ColumnsName);
        //    }
        //}
        #endregion
        #region Create Tag
        //private XYZ GetXYZTagStirrupDetail(SectionStyle sectionStyle, ViewSection viewSection, InfoModel infoModel, UnitProject unit, bool xy, double location0, double offset0)
        //{
        //    double left = 0;
        //    double location = unit.Convert(location0);
        //    double offset = unit.Convert(offset0);
        //    if (sectionStyle == SectionStyle.RECTANGLE)
        //    {
        //        left = (xy) ? unit.Convert(infoModel.b) : unit.Convert(infoModel.h);
        //    }
        //    else
        //    {
        //        left = unit.Convert(infoModel.T);
        //    }
        //    XYZ p1 = PointModel.ProjectToPlane(viewSection.Origin, infoModel.Bottom);
        //    XYZ p2 = p1 + (left + offset) * 0.5 * (-1) * viewSection.RightDirection;
        //    return p2 + (location) * XYZ.BasisZ;
        //}
        //private void CreateTagStirrupDetailItem(SectionStyle sectionStyle, ViewSection viewSection, InfoModel infoModel, Document document, UnitProject unit, SettingModel settingModel, Rebar rebar, bool xy, double location0, double offset0)
        //{
        //    XYZ origin = GetXYZTagStirrupDetail(sectionStyle, viewSection, infoModel, unit, xy, location0, offset0);
        //    BarS.Tag = IndependentTag.Create(document, viewSection.Id, new Reference(rebar), false, BarS.Mode, BarS.Vertical, origin);
        //    BarS.Tag.ChangeTypeId(settingModel.SelectedStirrupTag.Id);
            
        //}
        //public void CreateTagStirrupDetail(SectionStyle sectionStyle, ViewSection viewSection, InfoModel infoModel, Document document, UnitProject unit, SettingModel settingModel, bool xy, double offset0)
        //{
        //    if (TypeDis == 0)
        //    {
        //        CreateTagStirrupDetailItem(sectionStyle, viewSection, infoModel, document, unit, settingModel, BarS.RebarStirrup[0], xy, L * 0.125, offset0);
        //        CreateTagStirrupDetailItem(sectionStyle, viewSection, infoModel, document, unit, settingModel, BarS.RebarStirrup[0], xy, L * 0.5, offset0);
        //        CreateTagStirrupDetailItem(sectionStyle, viewSection, infoModel, document, unit, settingModel, BarS.RebarStirrup[0], xy, L * 0.875, offset0);
        //    }
        //    else
        //    {
        //        CreateTagStirrupDetailItem(sectionStyle, viewSection, infoModel, document, unit, settingModel, BarS.RebarStirrup[0], xy, L1 * 0.5, offset0);
        //        CreateTagStirrupDetailItem(sectionStyle, viewSection, infoModel, document, unit, settingModel, BarS.RebarStirrup[1], xy, L1 + L2 * 0.5, offset0);
        //        CreateTagStirrupDetailItem(sectionStyle, viewSection, infoModel, document, unit, settingModel, BarS.RebarStirrup[2], xy, L1 + L2 + L1 * 0.5, offset0);
        //    }
        //}
        #endregion
    }
}
