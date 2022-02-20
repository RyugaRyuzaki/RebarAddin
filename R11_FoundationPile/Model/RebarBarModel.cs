using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using DSP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WpfCustomControls;

namespace R11_FoundationPile
{
    public class RebarBarModel : BaseViewModel
    {
        private string _Type;
        public string Type { get => _Type; set { _Type = value; OnPropertyChanged(); } }
        private double _Diameter;
        public double Diameter { get => _Diameter; set { _Diameter = value; OnPropertyChanged(); } }
        private RebarBarType _RebarBarType;
        public RebarBarType RebarBarType { get => _RebarBarType; set { _RebarBarType = value; OnPropertyChanged(); } }
        private RebarShape _RebarShape;
        public RebarShape RebarShape { get => _RebarShape; set { _RebarShape = value; OnPropertyChanged(); } }
        private ObservableCollection<Rebar> _Rebars;
        public ObservableCollection<Rebar> Rebars { get { if (_Rebars == null) { _Rebars = new ObservableCollection<Rebar>(); } return _Rebars; } set { _Rebars = value; OnPropertyChanged(); } }
        private ObservableCollection<Curve> _Curves;
        public ObservableCollection<Curve> Curves { get { if (_Curves == null) { _Curves = new ObservableCollection<Curve>(); } return _Curves; } set { _Curves = value; OnPropertyChanged(); } }
        public TagMode Mode = TagMode.TM_ADDBY_CATEGORY;
        public TagOrientation Horizontal = TagOrientation.Horizontal;
        public TagOrientation Vertical = TagOrientation.Vertical;
        private IndependentTag _Tag;
        public IndependentTag Tag { get => _Tag; set { _Tag = value; OnPropertyChanged(); } }
        private MultiReferenceAnnotation _MultiTag;
        public MultiReferenceAnnotation MultiTag { get => _MultiTag; set { _MultiTag = value; OnPropertyChanged(); } }
        private MultiReferenceAnnotationOptions _MultiTagOption;
        public MultiReferenceAnnotationOptions MultiTagOption { get => _MultiTagOption; set { _MultiTagOption = value; OnPropertyChanged(); } }
        private RebarHookType _Hook;
        public RebarHookType Hook { get => _Hook; set { _Hook = value; OnPropertyChanged(); } }
        public RebarBarModel(Document document, string type, List<RebarBarType> rebarBarType)
        {
            Type = type;
            RebarBarType = rebarBarType.Where(x => x.Name == Type).SingleOrDefault();
            Diameter = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, RebarBarType.get_Parameter(BuiltInParameter.REBAR_BAR_DIAMETER).AsDouble(), false));

        }
        private void SetPartitionRebar(FoundationModel foundationModel)
        {
            if (Rebars.Count != 0)
            {
                for (int i = 0; i < Rebars.Count; i++)
                {
                    Rebars[i].LookupParameter("Partition").Set(foundationModel.LocationName);
                }
            }

        }
        private void CreateRebarSide(Document document, SettingModel settingModel, FoundationModel FoundationModel, BarModel barModel, UnitProject Unit, double CoverTop, double CoverBottom, double CoverSide)
        {
            try
            {
                var bar = Rebar.CreateFromCurves(document, RebarStyle.StirrupTie, RebarBarType, null, null, FoundationModel.Foundation, XYZ.BasisZ, Curves, RebarHookOrientation.Left, RebarHookOrientation.Right, false, true);
                if (settingModel.SelectedHook.Name.Contains("Stirrup"))
                {
                    bar.LookupParameter("Hook At Start").Set(settingModel.SelectedHook.Id);
                    bar.LookupParameter("Hook At End").Set(settingModel.SelectedHook.Id);
                    bar.SetHookRotationAngle(Math.PI, 0);
                    bar.SetHookRotationAngle(0, 1);
                }

                if (barModel.Layer > 1)
                {
                    RebarShapeDrivenAccessor rebarShape1 = bar.GetShapeDrivenAccessor();
                    double s = Unit.Convert((settingModel.HeightFoundation - CoverBottom - CoverTop) / (barModel.Layer + 1));
                    rebarShape1.SetLayoutAsNumberWithSpacing(barModel.Layer, s, true, true, true);
                }

                Rebars.Add(bar);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            SetPartitionRebar(FoundationModel);
        }
        private void CreateRebarSide2(Document document, SettingModel settingModel, FoundationModel FoundationModel, BarModel barModel, UnitProject Unit, double CoverTop, double CoverBottom, double CoverSide)
        {
            try
            {
                var bar = Rebar.CreateFromCurves(document, RebarStyle.StirrupTie, RebarBarType, null, null, FoundationModel.Foundation, XYZ.BasisZ, Curves, RebarHookOrientation.Left, RebarHookOrientation.Right, false, true);
                if (settingModel.SelectedHook.Name.Contains("Stirrup"))
                {
                    bar.LookupParameter("Hook At Start").Set(settingModel.SelectedHook.Id);
                    bar.LookupParameter("Hook At End").Set(settingModel.SelectedHook.Id);
                    bar.SetHookRotationAngle(0, 0);
                    bar.SetHookRotationAngle(Math.PI, 1);
                }

                if (barModel.Layer > 1)
                {
                    RebarShapeDrivenAccessor rebarShape1 = bar.GetShapeDrivenAccessor();
                    double s = Unit.Convert((settingModel.HeightFoundation - CoverBottom - CoverTop) / (barModel.Layer + 1));
                    rebarShape1.SetLayoutAsNumberWithSpacing(barModel.Layer, s, true, true, true);
                }

                Rebars.Add(bar);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            SetPartitionRebar(FoundationModel);
        }
        private void CreateRebarImage0(Document document, SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel barModel, UnitProject Unit, double dMainBottom, double dMainTop, double dSide, double CoverTop, double CoverBottom, double CoverSide)
        {
            List<double> Distance = new List<double>();
            Curves = ProcessCurveRebar.GetCurvesImage0A(settingModel, FoundationModel, FoundationBarModel, barModel, Unit, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, out Distance);
            if (Curves.Count != 0)
            {
                if ((barModel.Name.Contains("Bottom") || barModel.Name.Contains("Top")))
                {
                    for (int i = 0; i < Curves.Count; i++)
                    {
                        List<Curve> curves1 = new List<Curve>();
                        curves1.Add(Curves[i]);
                        CurveLoop curveL = CurveLoop.Create(curves1);
                        List<CurveLoop> curveLs = new List<CurveLoop>();
                        curveLs.Add(curveL);
                        try
                        {

                            var bar = Rebar.CreateFreeForm(document, RebarBarType, FoundationModel.Foundation, curveLs, out RebarFreeFormValidationResult a);
                            if (barModel.HookLength != 0)
                            {
                                bar.LookupParameter("Hook At Start").Set(barModel.Hook.Id);
                                bar.LookupParameter("Hook At End").Set(barModel.Hook.Id);
                                bar.SetHookRotationAngle((barModel.Name.Contains("Bottom") ? (Math.PI * 1.5) : (Math.PI * 0.5)), 0);
                                bar.SetHookRotationAngle((barModel.Name.Contains("Bottom") ? (Math.PI * 1.5) : (Math.PI * 0.5)), 1);
                            }
                            Rebars.Add(bar);
                        }
                        catch (Exception e)
                        {
                            System.Windows.Forms.MessageBox.Show(e.Message);
                        }
                    }
                    SetPartitionRebar(FoundationModel);
                }
                if ((barModel.Name.Contains("Horizontal")))
                {
                    for (int i = 0; i < Curves.Count; i++)
                    {
                        List<Curve> curves1 = new List<Curve>();
                        curves1.Add(Curves[i]);
                        try
                        {

                            var bar = Rebar.CreateFromCurves(document, RebarStyle.StirrupTie, RebarBarType, null, null, FoundationModel.Foundation, XYZ.BasisZ, curves1, RebarHookOrientation.Left, RebarHookOrientation.Right, false, true);
                            if (settingModel.SelectedHook.Name.Contains("Stirrup"))
                            {
                                bar.LookupParameter("Hook At Start").Set(settingModel.SelectedHook.Id);
                                bar.LookupParameter("Hook At End").Set(settingModel.SelectedHook.Id);
                                bar.SetHookRotationAngle((Math.PI * 0.5), 0);
                                bar.SetHookRotationAngle((Math.PI * 1.5), 1);
                            }
                            if (barModel.Layer > 1)
                            {
                                RebarShapeDrivenAccessor rebarShape1 = bar.GetShapeDrivenAccessor();
                                double s = Unit.Convert((settingModel.HeightFoundation - CoverBottom - CoverTop) / (barModel.Layer + 1));
                                rebarShape1.SetLayoutAsNumberWithSpacing(barModel.Layer, s, true, true, true);
                            }

                            Rebars.Add(bar);
                        }
                        catch (Exception e)
                        {
                            System.Windows.Forms.MessageBox.Show(e.Message);
                        }
                    }
                    SetPartitionRebar(FoundationModel);
                }
                if ((barModel.Name.Contains("Vertical")) && (barModel.Name.Contains("Main")))
                {
                    XYZ x = null;
                    XYZ y = null;
                    if (FoundationModel.ColumnModel.Style.Equals("RECTANGLE"))
                    {
                        x = FoundationModel.ColumnModel.East.FaceNormal;
                        y = FoundationModel.ColumnModel.Nouth.FaceNormal;
                    }
                    else
                    {
                        x = XYZ.BasisX;
                        y = XYZ.BasisY;
                    }
                    XYZ normal = (FoundationBarModel.SpanOrientation.Equals("Horizontal")) ? x : y;
                    for (int i = 0; i < Curves.Count; i++)
                    {
                        List<Curve> curves1 = new List<Curve>();
                        curves1.Add(Curves[i]);
                        try
                        {

                            var bar = Rebar.CreateFromCurves(document, RebarStyle.StirrupTie, RebarBarType, null, null, FoundationModel.Foundation, normal, curves1, RebarHookOrientation.Left, RebarHookOrientation.Right, false, true);
                            if (settingModel.SelectedHook.Name.Contains("Stirrup"))
                            {
                                bar.LookupParameter("Hook At Start").Set(settingModel.SelectedHook.Id);
                                bar.LookupParameter("Hook At End").Set(settingModel.SelectedHook.Id);
                                bar.SetHookRotationAngle((Math.PI * 0.5), 0);
                                bar.SetHookRotationAngle((Math.PI * 1.5), 1);
                            }
                            RebarShapeDrivenAccessor rebarShape1 = bar.GetShapeDrivenAccessor();
                            double s = Unit.Convert(Distance[i]);
                            rebarShape1.SetLayoutAsNumberWithSpacing(barModel.Layer + 2, s, true, false, false);

                            Rebars.Add(bar);
                        }
                        catch (Exception e)
                        {
                            System.Windows.Forms.MessageBox.Show(e.Message);
                        }
                    }
                    SetPartitionRebar(FoundationModel);
                }
                if ((barModel.Name.Contains("Vertical")) && (barModel.Name.Contains("Secondary")))
                {
                    XYZ x = null;
                    XYZ y = null;
                    if (FoundationModel.ColumnModel.Style.Equals("RECTANGLE"))
                    {
                        x = FoundationModel.ColumnModel.East.FaceNormal;
                        y = FoundationModel.ColumnModel.Nouth.FaceNormal;
                    }
                    else
                    {
                        x = XYZ.BasisX;
                        y = XYZ.BasisY;
                    }
                    XYZ normal = (FoundationBarModel.SpanOrientation.Equals("Horizontal")) ? y : x;
                    for (int i = 0; i < Curves.Count; i++)
                    {
                        List<Curve> curves1 = new List<Curve>();
                        curves1.Add(Curves[i]);
                        try
                        {

                            var bar = Rebar.CreateFromCurves(document, RebarStyle.StirrupTie, RebarBarType, null, null, FoundationModel.Foundation, normal, curves1, RebarHookOrientation.Left, RebarHookOrientation.Right, false, true);
                            if (settingModel.SelectedHook.Name.Contains("Stirrup"))
                            {
                                bar.LookupParameter("Hook At Start").Set(settingModel.SelectedHook.Id);
                                bar.LookupParameter("Hook At End").Set(settingModel.SelectedHook.Id);
                                bar.SetHookRotationAngle((Math.PI * 0.5), 0);
                                bar.SetHookRotationAngle((Math.PI * 1.5), 1);
                            }

                            RebarShapeDrivenAccessor rebarShape1 = bar.GetShapeDrivenAccessor();
                            double s = Unit.Convert(Distance[i]);
                            rebarShape1.SetLayoutAsNumberWithSpacing(barModel.Layer + 2, s, true, false, false);
                            Rebars.Add(bar);
                        }
                        catch (Exception e)
                        {
                            System.Windows.Forms.MessageBox.Show(e.Message);
                        }
                    }
                    SetPartitionRebar(FoundationModel);
                }
                if (barModel.Name.Equals("Side"))
                {
                    CreateRebarSide(document, settingModel, FoundationModel, barModel, Unit, CoverTop, CoverBottom, CoverSide);
                }

            }
        }
        private void CreateRebarImage1(Document document, SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel barModel, UnitProject Unit, double dMainBottom, double dMainTop, double dSide, double CoverTop, double CoverBottom, double CoverSide)
        {
            XYZ x = null;
            XYZ y = null;
            if (FoundationModel.ColumnModel.Style.Equals("RECTANGLE"))
            {
                x = FoundationModel.ColumnModel.East.FaceNormal;
                y = FoundationModel.ColumnModel.Nouth.FaceNormal;
            }
            else
            {
                x = XYZ.BasisX;
                y = XYZ.BasisY;
            }
            List<double> Distance = new List<double>();
            Curves = ProcessCurveRebar.GetCurvesImage1(settingModel, FoundationModel, FoundationBarModel, barModel, Unit, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, out Distance);

            if (Curves.Count != 0)
            {
                XYZ normal = null;
                switch (barModel.Name)
                {
                    case "MainBottom":

                        try
                        {
                            normal = (FoundationBarModel.SpanOrientation.Equals("Horizontal")) ? y : x;
                            var bar1 = Rebar.CreateFromCurves(document, RebarStyle.Standard, RebarBarType, null, null, FoundationModel.Foundation, y, Curves, RebarHookOrientation.Right, RebarHookOrientation.Right, false, true);
                            if (barModel.HookLength != 0)
                            {
                                bar1.LookupParameter("Hook At Start").Set(barModel.Hook.Id);
                                bar1.LookupParameter("Hook At End").Set(barModel.Hook.Id);
                                bar1.SetHookRotationAngle(0, 0);
                                bar1.SetHookRotationAngle(0, 1);
                            }
                            RebarShapeDrivenAccessor rebarShape1 = bar1.GetShapeDrivenAccessor();
                            double s1 = Unit.Convert(barModel.Distance);
                            rebarShape1.SetLayoutAsNumberWithSpacing(barModel.Number, s1, true, true, true);
                            Rebars.Add(bar1);
                            SetPartitionRebar(FoundationModel);

                        }
                        catch (Exception e)
                        {
                            System.Windows.Forms.MessageBox.Show(e.Message);
                        }

                        break;
                    case "MainTop":
                        try
                        {
                            normal = (FoundationBarModel.SpanOrientation.Equals("Horizontal")) ? y : x;
                            var bar2 = Rebar.CreateFromCurves(document, RebarStyle.Standard, RebarBarType, null, null, FoundationModel.Foundation, normal, Curves, RebarHookOrientation.Right, RebarHookOrientation.Right, false, true);
                            if (barModel.HookLength != 0)
                            {
                                bar2.LookupParameter("Hook At Start").Set(barModel.Hook.Id);
                                bar2.LookupParameter("Hook At End").Set(barModel.Hook.Id);
                                bar2.SetHookRotationAngle(Math.PI, 0);
                                bar2.SetHookRotationAngle(Math.PI, 1);
                            }
                            RebarShapeDrivenAccessor rebarShape2 = bar2.GetShapeDrivenAccessor();
                            double s1 = Unit.Convert(barModel.Distance);
                            rebarShape2.SetLayoutAsNumberWithSpacing(barModel.Number, s1, true, true, true);
                            Rebars.Add(bar2);
                            SetPartitionRebar(FoundationModel);
                        }
                        catch (Exception e)
                        {

                            System.Windows.Forms.MessageBox.Show(e.Message);
                        }

                        break;
                    case "MainAddHorizontal":
                        for (int i = 0; i < Curves.Count; i++)
                        {
                            List<Curve> curves1 = new List<Curve>();
                            curves1.Add(Curves[i]);
                            try
                            {

                                normal = (FoundationBarModel.SpanOrientation.Equals("Horizontal")) ? y : x;
                                var bar1 = Rebar.CreateFromCurves(document, RebarStyle.StirrupTie, RebarBarType, null, null, FoundationModel.Foundation, normal, curves1, RebarHookOrientation.Right, RebarHookOrientation.Right, false, true);
                                if (settingModel.SelectedHook.Name.Contains("Stirrup"))
                                {
                                    bar1.LookupParameter("Hook At Start").Set(settingModel.SelectedHook.Id);
                                    bar1.LookupParameter("Hook At End").Set(settingModel.SelectedHook.Id);
                                    bar1.SetHookRotationAngle(0, 0);
                                    bar1.SetHookRotationAngle(0, 1);
                                }
                                RebarShapeDrivenAccessor rebarShape1 = bar1.GetShapeDrivenAccessor();
                                double s1 = Unit.Convert(barModel.Distance);
                                rebarShape1.SetLayoutAsNumberWithSpacing(barModel.Number, s1, true, true, true);
                                Rebars.Add(bar1);
                            }
                            catch (Exception e)
                            {
                                System.Windows.Forms.MessageBox.Show(e.Message);
                            }

                        }
                        SetPartitionRebar(FoundationModel);
                        break;
                    case "MainAddVertical":
                        normal = (FoundationBarModel.SpanOrientation.Equals("Horizontal")) ? x : y;
                        for (int i = 0; i < Curves.Count; i++)
                        {
                            List<Curve> curves1 = new List<Curve>();
                            curves1.Add(Curves[i]);
                            try
                            {

                                var bar = Rebar.CreateFromCurves(document, RebarStyle.StirrupTie, RebarBarType, null, null, FoundationModel.Foundation, normal, curves1, RebarHookOrientation.Left, RebarHookOrientation.Right, false, true);
                                if (settingModel.SelectedHook.Name.Contains("Stirrup"))
                                {
                                    bar.LookupParameter("Hook At Start").Set(settingModel.SelectedHook.Id);
                                    bar.LookupParameter("Hook At End").Set(settingModel.SelectedHook.Id);
                                    bar.SetHookRotationAngle((Math.PI * 0.5), 0);
                                    bar.SetHookRotationAngle((Math.PI * 1.5), 1);
                                }
                                RebarShapeDrivenAccessor rebarShape1 = bar.GetShapeDrivenAccessor();
                                double s = Unit.Convert(Distance[i]);
                                rebarShape1.SetLayoutAsNumberWithSpacing(barModel.Layer + 2, s, true, false, false);

                                Rebars.Add(bar);
                            }
                            catch (Exception e)
                            {
                                System.Windows.Forms.MessageBox.Show(e.Message);
                            }
                        }
                        SetPartitionRebar(FoundationModel);
                        break;
                    case "SecondaryBottom":
                        try
                        {
                            normal = (FoundationBarModel.SpanOrientation.Equals("Horizontal")) ? x : y;
                            var bar2 = Rebar.CreateFromCurves(document, RebarStyle.Standard, RebarBarType, null, null, FoundationModel.Foundation, normal, Curves, RebarHookOrientation.Right, RebarHookOrientation.Right, true, true);
                            if (barModel.HookLength != 0)
                            {
                                bar2.LookupParameter("Hook At Start").Set(barModel.Hook.Id);
                                bar2.LookupParameter("Hook At End").Set(barModel.Hook.Id);
                                bar2.SetHookRotationAngle(Math.PI, 0);
                                bar2.SetHookRotationAngle(Math.PI, 1);
                            }
                            RebarShapeDrivenAccessor rebarShape2 = bar2.GetShapeDrivenAccessor();
                            double s2 = Unit.Convert(barModel.Distance);
                            rebarShape2.SetLayoutAsNumberWithSpacing(barModel.Number, s2, true, true, true);
                            Rebars.Add(bar2);
                            SetPartitionRebar(FoundationModel);
                        }
                        catch (Exception e)
                        {

                            System.Windows.Forms.MessageBox.Show(e.Message);
                        }
                        break;
                    case "SecondaryTop":

                        try
                        {
                            normal = (FoundationBarModel.SpanOrientation.Equals("Horizontal")) ? x : y;
                            var bar2 = Rebar.CreateFromCurves(document, RebarStyle.Standard, RebarBarType, null, null, FoundationModel.Foundation, normal, Curves, RebarHookOrientation.Right, RebarHookOrientation.Right, false, true);
                            if (barModel.HookLength != 0)
                            {
                                bar2.LookupParameter("Hook At Start").Set(barModel.Hook.Id);
                                bar2.LookupParameter("Hook At End").Set(barModel.Hook.Id);
                                bar2.SetHookRotationAngle(0, 0);
                                bar2.SetHookRotationAngle(0, 1);
                            }
                            RebarShapeDrivenAccessor rebarShape2 = bar2.GetShapeDrivenAccessor();
                            double s2 = Unit.Convert(barModel.Distance);
                            rebarShape2.SetLayoutAsNumberWithSpacing(barModel.Number, s2, true, true, true);
                            Rebars.Add(bar2);
                            SetPartitionRebar(FoundationModel);
                        }
                        catch (Exception e)
                        {

                            System.Windows.Forms.MessageBox.Show(e.Message);
                        }
                        break;
                    case "SecondaryAddHorizontal":
                        for (int i = 0; i < Curves.Count; i++)
                        {
                            List<Curve> curves1 = new List<Curve>();
                            curves1.Add(Curves[i]);
                            try
                            {

                                normal = (FoundationBarModel.SpanOrientation.Equals("Horizontal")) ? x : y;
                                var bar1 = Rebar.CreateFromCurves(document, RebarStyle.StirrupTie, RebarBarType, null, null, FoundationModel.Foundation, normal, curves1, RebarHookOrientation.Right, RebarHookOrientation.Right, false, true);
                                if (settingModel.SelectedHook.Name.Contains("Stirrup"))
                                {
                                    bar1.LookupParameter("Hook At Start").Set(settingModel.SelectedHook.Id);
                                    bar1.LookupParameter("Hook At End").Set(settingModel.SelectedHook.Id);
                                    bar1.SetHookRotationAngle(0, 0);
                                    bar1.SetHookRotationAngle(0, 1);
                                }
                                RebarShapeDrivenAccessor rebarShape1 = bar1.GetShapeDrivenAccessor();
                                double s1 = Unit.Convert(barModel.Distance);
                                rebarShape1.SetLayoutAsNumberWithSpacing(barModel.Number, s1, true, true, true);
                                Rebars.Add(bar1);

                            }
                            catch (Exception e)
                            {
                                System.Windows.Forms.MessageBox.Show(e.Message);
                            }
                        }
                        SetPartitionRebar(FoundationModel);
                        break;
                    case "SecondaryAddVertical":
                        normal = (FoundationBarModel.SpanOrientation.Equals("Horizontal")) ? y : x;
                        for (int i = 0; i < Curves.Count; i++)
                        {
                            List<Curve> curves1 = new List<Curve>();
                            curves1.Add(Curves[i]);
                            try
                            {

                                var bar = Rebar.CreateFromCurves(document, RebarStyle.StirrupTie, RebarBarType, null, null, FoundationModel.Foundation, normal, curves1, RebarHookOrientation.Left, RebarHookOrientation.Right, false, true);
                                if (settingModel.SelectedHook.Name.Contains("Stirrup"))
                                {
                                    bar.LookupParameter("Hook At Start").Set(settingModel.SelectedHook.Id);
                                    bar.LookupParameter("Hook At End").Set(settingModel.SelectedHook.Id);
                                    bar.SetHookRotationAngle((Math.PI * 0.5), 0);
                                    bar.SetHookRotationAngle((Math.PI * 1.5), 1);
                                }
                                RebarShapeDrivenAccessor rebarShape1 = bar.GetShapeDrivenAccessor();
                                double s = Unit.Convert(Distance[i]);
                                rebarShape1.SetLayoutAsNumberWithSpacing(barModel.Layer + 2, s, true, false, false);

                                Rebars.Add(bar);
                            }
                            catch (Exception e)
                            {
                                System.Windows.Forms.MessageBox.Show(e.Message);
                            }
                        }
                        SetPartitionRebar(FoundationModel);
                        break;
                    case "Side":
                        CreateRebarSide(document, settingModel, FoundationModel, barModel, Unit, CoverTop, CoverBottom, CoverSide);
                        break;
                    default: break;
                }


            }
        }
        private void CreateRebarImage2(Document document, SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel barModel, UnitProject Unit, double dMainBottom, double dMainTop, double dSide, double CoverTop, double CoverBottom, double CoverSide)
        {
            List<double> Distance = new List<double>();
            Curves = ProcessCurveRebar.GetCurvesImage2(settingModel, FoundationModel, FoundationBarModel, barModel, Unit, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, out Distance);
            if (Curves.Count != 0)
            {
                if ((barModel.Name.Contains("Bottom") || barModel.Name.Contains("Top")))
                {
                    for (int i = 0; i < Curves.Count; i++)
                    {
                        List<Curve> curves1 = new List<Curve>();
                        curves1.Add(Curves[i]);
                        CurveLoop curveL = CurveLoop.Create(curves1);
                        List<CurveLoop> curveLs = new List<CurveLoop>();
                        curveLs.Add(curveL);
                        try
                        {

                            var bar = Rebar.CreateFreeForm(document, RebarBarType, FoundationModel.Foundation, curveLs, out RebarFreeFormValidationResult a);
                            if (barModel.HookLength != 0)
                            {
                                bar.LookupParameter("Hook At Start").Set(barModel.Hook.Id);
                                bar.LookupParameter("Hook At End").Set(barModel.Hook.Id);
                                bar.SetHookRotationAngle((barModel.Name.Contains("Bottom") ? (Math.PI * 1.5) : (Math.PI * 0.5)), 0);
                                bar.SetHookRotationAngle((barModel.Name.Contains("Bottom") ? (Math.PI * 1.5) : (Math.PI * 0.5)), 1);
                            }
                            Rebars.Add(bar);
                        }
                        catch (Exception e)
                        {
                            System.Windows.Forms.MessageBox.Show(e.Message);
                        }
                    }
                    SetPartitionRebar(FoundationModel);
                }
                if ((barModel.Name.Contains("Horizontal")))
                {
                    for (int i = 0; i < Curves.Count; i++)
                    {
                        List<Curve> curves1 = new List<Curve>();
                        curves1.Add(Curves[i]);
                        try
                        {

                            var bar = Rebar.CreateFromCurves(document, RebarStyle.StirrupTie, RebarBarType, null, null, FoundationModel.Foundation, XYZ.BasisZ, curves1, RebarHookOrientation.Left, RebarHookOrientation.Right, false, true);
                            if (settingModel.SelectedHook.Name.Contains("Stirrup"))
                            {
                                bar.LookupParameter("Hook At Start").Set(settingModel.SelectedHook.Id);
                                bar.LookupParameter("Hook At End").Set(settingModel.SelectedHook.Id);
                                bar.SetHookRotationAngle((Math.PI * 0.5), 0);
                                bar.SetHookRotationAngle((Math.PI * 1.5), 1);
                            }
                            if (barModel.Layer > 1)
                            {
                                RebarShapeDrivenAccessor rebarShape1 = bar.GetShapeDrivenAccessor();
                                double s = Unit.Convert((settingModel.HeightFoundation - CoverBottom - CoverTop) / (barModel.Layer + 1));
                                rebarShape1.SetLayoutAsNumberWithSpacing(barModel.Layer, s, true, true, true);
                            }

                            Rebars.Add(bar);
                        }
                        catch (Exception e)
                        {
                            System.Windows.Forms.MessageBox.Show(e.Message);
                        }
                    }
                    SetPartitionRebar(FoundationModel);
                }
                if ((barModel.Name.Contains("Vertical")) && (barModel.Name.Contains("Main")))
                {
                    XYZ x = null;
                    XYZ y = null;
                    if (FoundationModel.ColumnModel.Style.Equals("RECTANGLE"))
                    {
                        x = FoundationModel.ColumnModel.East.FaceNormal;
                        y = FoundationModel.ColumnModel.Nouth.FaceNormal;
                    }
                    else
                    {
                        x = XYZ.BasisX;
                        y = XYZ.BasisY;
                    }
                    XYZ normal = (FoundationBarModel.SpanOrientation.Equals("Horizontal")) ? x : y;
                    for (int i = 0; i < Curves.Count; i++)
                    {
                        List<Curve> curves1 = new List<Curve>();
                        curves1.Add(Curves[i]);
                        try
                        {

                            var bar = Rebar.CreateFromCurves(document, RebarStyle.StirrupTie, RebarBarType, null, null, FoundationModel.Foundation, normal, curves1, RebarHookOrientation.Left, RebarHookOrientation.Right, false, true);
                            if (settingModel.SelectedHook.Name.Contains("Stirrup"))
                            {
                                bar.LookupParameter("Hook At Start").Set(settingModel.SelectedHook.Id);
                                bar.LookupParameter("Hook At End").Set(settingModel.SelectedHook.Id);
                                bar.SetHookRotationAngle((Math.PI * 0.5), 0);
                                bar.SetHookRotationAngle((Math.PI * 1.5), 1);
                            }
                            RebarShapeDrivenAccessor rebarShape1 = bar.GetShapeDrivenAccessor();
                            double s = Unit.Convert(Distance[i]);
                            rebarShape1.SetLayoutAsNumberWithSpacing(barModel.Layer + 2, s, true, false, false);

                            Rebars.Add(bar);
                        }
                        catch (Exception e)
                        {
                            System.Windows.Forms.MessageBox.Show(e.Message);
                        }
                    }
                    SetPartitionRebar(FoundationModel);
                }
                if ((barModel.Name.Contains("Vertical")) && (barModel.Name.Contains("Secondary")))
                {
                    XYZ x = null;
                    XYZ y = null;
                    if (FoundationModel.ColumnModel.Style.Equals("RECTANGLE"))
                    {
                        x = FoundationModel.ColumnModel.East.FaceNormal;
                        y = FoundationModel.ColumnModel.Nouth.FaceNormal;
                    }
                    else
                    {
                        x = XYZ.BasisX;
                        y = XYZ.BasisY;
                    }
                    XYZ normal = (FoundationBarModel.SpanOrientation.Equals("Horizontal")) ? y : x;
                    for (int i = 0; i < Curves.Count; i++)
                    {
                        List<Curve> curves1 = new List<Curve>();
                        curves1.Add(Curves[i]);
                        try
                        {

                            var bar = Rebar.CreateFromCurves(document, RebarStyle.StirrupTie, RebarBarType, null, null, FoundationModel.Foundation, normal, curves1, RebarHookOrientation.Left, RebarHookOrientation.Right, false, true);
                            if (settingModel.SelectedHook.Name.Contains("Stirrup"))
                            {
                                bar.LookupParameter("Hook At Start").Set(settingModel.SelectedHook.Id);
                                bar.LookupParameter("Hook At End").Set(settingModel.SelectedHook.Id);
                                bar.SetHookRotationAngle((Math.PI * 0.5), 0);
                                bar.SetHookRotationAngle((Math.PI * 1.5), 1);
                            }

                            RebarShapeDrivenAccessor rebarShape1 = bar.GetShapeDrivenAccessor();
                            double s = Unit.Convert(Distance[i]);
                            rebarShape1.SetLayoutAsNumberWithSpacing(barModel.Layer + 2, s, true, false, false);
                            Rebars.Add(bar);
                        }
                        catch (Exception e)
                        {
                            System.Windows.Forms.MessageBox.Show(e.Message);
                        }
                    }
                    SetPartitionRebar(FoundationModel);
                }
                if (barModel.Name.Equals("Side"))
                {
                    CreateRebarSide2(document, settingModel, FoundationModel, barModel, Unit, CoverTop, CoverBottom, CoverSide);
                }

            }
        }
        private void CreateRebarImage3(Document document, SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel barModel, UnitProject Unit, double dMainBottom, double dMainTop, double dSide, double CoverTop, double CoverBottom, double CoverSide)
        {
            List<double> Distance = new List<double>();
            Curves = ProcessCurveRebar.GetCurvesImage3(settingModel, FoundationModel, FoundationBarModel, barModel, Unit, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, out Distance);
            if (Curves.Count != 0)
            {
                if ((barModel.Name.Contains("Bottom") || barModel.Name.Contains("Top")))
                {
                    for (int i = 0; i < Curves.Count; i++)
                    {
                        List<Curve> curves1 = new List<Curve>();
                        curves1.Add(Curves[i]);
                        CurveLoop curveL = CurveLoop.Create(curves1);
                        List<CurveLoop> curveLs = new List<CurveLoop>();
                        curveLs.Add(curveL);
                        try
                        {

                            var bar = Rebar.CreateFreeForm(document, RebarBarType, FoundationModel.Foundation, curveLs, out RebarFreeFormValidationResult a);
                            if (barModel.HookLength != 0)
                            {
                                bar.LookupParameter("Hook At Start").Set(barModel.Hook.Id);
                                bar.LookupParameter("Hook At End").Set(barModel.Hook.Id);
                                bar.SetHookRotationAngle((barModel.Name.Contains("Bottom") ? (Math.PI * 1.5) : (Math.PI * 0.5)), 0);
                                bar.SetHookRotationAngle((barModel.Name.Contains("Bottom") ? (Math.PI * 1.5) : (Math.PI * 0.5)), 1);
                            }
                            Rebars.Add(bar);
                        }
                        catch (Exception e)
                        {
                            System.Windows.Forms.MessageBox.Show(e.Message);
                        }
                    }
                    SetPartitionRebar(FoundationModel);
                }
                if ((barModel.Name.Contains("Horizontal")))
                {
                    for (int i = 0; i < Curves.Count; i++)
                    {
                        List<Curve> curves1 = new List<Curve>();
                        curves1.Add(Curves[i]);
                        try
                        {

                            var bar = Rebar.CreateFromCurves(document, RebarStyle.StirrupTie, RebarBarType, null, null, FoundationModel.Foundation, XYZ.BasisZ, curves1, RebarHookOrientation.Left, RebarHookOrientation.Right, false, true);
                            if (settingModel.SelectedHook.Name.Contains("Stirrup"))
                            {
                                bar.LookupParameter("Hook At Start").Set(settingModel.SelectedHook.Id);
                                bar.LookupParameter("Hook At End").Set(settingModel.SelectedHook.Id);
                                bar.SetHookRotationAngle((Math.PI * 0.5), 0);
                                bar.SetHookRotationAngle((Math.PI * 1.5), 1);
                            }
                            if (barModel.Layer > 1)
                            {
                                RebarShapeDrivenAccessor rebarShape1 = bar.GetShapeDrivenAccessor();
                                double s = Unit.Convert((settingModel.HeightFoundation - CoverBottom - CoverTop) / (barModel.Layer + 1));
                                rebarShape1.SetLayoutAsNumberWithSpacing(barModel.Layer, s, true, true, true);
                            }

                            Rebars.Add(bar);
                        }
                        catch (Exception e)
                        {
                            System.Windows.Forms.MessageBox.Show(e.Message);
                        }
                    }
                    SetPartitionRebar(FoundationModel);
                }
                if ((barModel.Name.Contains("Vertical")) && (barModel.Name.Contains("Main")))
                {
                    XYZ x = null;
                    XYZ y = null;
                    if (FoundationModel.ColumnModel.Style.Equals("RECTANGLE"))
                    {
                        x = FoundationModel.ColumnModel.East.FaceNormal;
                        y = FoundationModel.ColumnModel.Nouth.FaceNormal;
                    }
                    else
                    {
                        x = XYZ.BasisX;
                        y = XYZ.BasisY;
                    }
                    XYZ normal = (FoundationBarModel.SpanOrientation.Equals("Horizontal")) ? x : y;
                    for (int i = 0; i < Curves.Count; i++)
                    {
                        List<Curve> curves1 = new List<Curve>();
                        curves1.Add(Curves[i]);
                        try
                        {

                            var bar = Rebar.CreateFromCurves(document, RebarStyle.StirrupTie, RebarBarType, null, null, FoundationModel.Foundation, normal, curves1, RebarHookOrientation.Left, RebarHookOrientation.Right, false, true);
                            if (settingModel.SelectedHook.Name.Contains("Stirrup"))
                            {
                                bar.LookupParameter("Hook At Start").Set(settingModel.SelectedHook.Id);
                                bar.LookupParameter("Hook At End").Set(settingModel.SelectedHook.Id);
                                bar.SetHookRotationAngle((Math.PI * 0.5), 0);
                                bar.SetHookRotationAngle((Math.PI * 1.5), 1);
                            }
                            RebarShapeDrivenAccessor rebarShape1 = bar.GetShapeDrivenAccessor();
                            double s = Unit.Convert(Distance[i]);
                            rebarShape1.SetLayoutAsNumberWithSpacing(barModel.Layer + 2, s, true, false, false);

                            Rebars.Add(bar);
                        }
                        catch (Exception e)
                        {
                            System.Windows.Forms.MessageBox.Show(e.Message);
                        }
                    }
                    SetPartitionRebar(FoundationModel);
                }
                if ((barModel.Name.Contains("Vertical")) && (barModel.Name.Contains("Secondary")))
                {
                    XYZ x = null;
                    XYZ y = null;
                    if (FoundationModel.ColumnModel.Style.Equals("RECTANGLE"))
                    {
                        x = FoundationModel.ColumnModel.East.FaceNormal;
                        y = FoundationModel.ColumnModel.Nouth.FaceNormal;
                    }
                    else
                    {
                        x = XYZ.BasisX;
                        y = XYZ.BasisY;
                    }
                    XYZ normal = (FoundationBarModel.SpanOrientation.Equals("Horizontal")) ? y : x;
                    for (int i = 0; i < Curves.Count; i++)
                    {
                        List<Curve> curves1 = new List<Curve>();
                        curves1.Add(Curves[i]);
                        try
                        {

                            var bar = Rebar.CreateFromCurves(document, RebarStyle.StirrupTie, RebarBarType, null, null, FoundationModel.Foundation, normal, curves1, RebarHookOrientation.Left, RebarHookOrientation.Right, false, true);
                            if (settingModel.SelectedHook.Name.Contains("Stirrup"))
                            {
                                bar.LookupParameter("Hook At Start").Set(settingModel.SelectedHook.Id);
                                bar.LookupParameter("Hook At End").Set(settingModel.SelectedHook.Id);
                                bar.SetHookRotationAngle((Math.PI * 0.5), 0);
                                bar.SetHookRotationAngle((Math.PI * 1.5), 1);
                            }

                            RebarShapeDrivenAccessor rebarShape1 = bar.GetShapeDrivenAccessor();
                            double s = Unit.Convert(Distance[i]);
                            rebarShape1.SetLayoutAsNumberWithSpacing(barModel.Layer + 2, s, true, false, false);
                            Rebars.Add(bar);
                        }
                        catch (Exception e)
                        {
                            System.Windows.Forms.MessageBox.Show(e.Message);
                        }
                    }
                    SetPartitionRebar(FoundationModel);
                }
                if (barModel.Name.Equals("Side"))
                {
                    CreateRebarSide2(document, settingModel, FoundationModel, barModel, Unit, CoverTop, CoverBottom, CoverSide);
                }

            }
        }
        public void CreateRebar(Document document, SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel barModel, UnitProject Unit, double dMainBottom, double dMainTop, double dSide, double CoverTop, double CoverBottom, double CoverSide)
        {

            switch (FoundationBarModel.Image)
            {
                case 0: CreateRebarImage0(document, settingModel, FoundationModel, FoundationBarModel, barModel, Unit, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide); break;

                case 1:
                    CreateRebarImage1(document, settingModel, FoundationModel, FoundationBarModel, barModel, Unit, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide); break;
                case 2: CreateRebarImage2(document, settingModel, FoundationModel, FoundationBarModel, barModel, Unit, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide); break;
                case 3: CreateRebarImage3(document, settingModel, FoundationModel, FoundationBarModel, barModel, Unit, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide); break;
                default: CreateRebarImage0(document, settingModel, FoundationModel, FoundationBarModel, barModel, Unit, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide); break;
            }
        }



        //#region Tag
        ////private XYZ GetXYZOriginTagDetail(UnitProject unit, InfoModel infoModel0, PlanarFace planarFace0)
        ////{
        ////    XYZ p1 = PointModel.ProjectToPlane(infoModel0.LeftRightPlanar[0].Origin, infoModel0.TopBottomPlanar[0]);
        ////    double b = unit.Convert(infoModel0.b);
        ////    XYZ p2 = p1 + (b / 2) * (-1) * infoModel0.LeftRightPlanar[0].FaceNormal;
        ////    XYZ p3 = PointModel.ProjectToPlane(p2, planarFace0);
        ////    return p3;
        ////}
        ////public void CreateTagRebarDetailTop(Autodesk.Revit.DB.View view, Document document, UnitProject unit, InfoModel infoModel0, SettingModel settingModel, PlanarFace planarFace0, double x0, double y0, double h0, double v0)
        ////{
        ////    double x = unit.Convert(x0);
        ////    double y = unit.Convert(y0);
        ////    double h = unit.Convert(h0);
        ////    double v = unit.Convert(v0);
        ////    double zOffset = unit.Convert(infoModel0.zOffset);
        ////    XYZ p0 = GetXYZOriginTagDetail(unit, infoModel0, planarFace0);
        ////    XYZ p1 = p0 + x * (-1) * planarFace0.FaceNormal;
        ////    XYZ leaderEnd = p1 - (y + zOffset) * XYZ.BasisZ;
        ////    XYZ LeaderElbow = p1 + v * XYZ.BasisZ;
        ////    XYZ tagHead = LeaderElbow + h * (-1) * planarFace0.FaceNormal;
        ////    Tag = IndependentTag.Create(document, view.Id, new Reference(Rebar), true, Mode, Horizontal, tagHead);
        ////    Tag.ChangeTypeId(settingModel.SelectedRebarTag.Id);
        ////    Tag.LeaderEndCondition = LeaderEndCondition.Free;
        ////    Tag.LeaderEnd = leaderEnd;
        ////    Tag.LeaderElbow = LeaderElbow;
        ////    Tag.TagHeadPosition = tagHead;

        ////}
        ////public void CreateTagRebarDetailBottom(Autodesk.Revit.DB.View view, Document document, UnitProject unit, InfoModel infoModel0, SettingModel settingModel, PlanarFace planarFace0, double x0, double y0, double h0, double v0)
        ////{
        ////    double x = unit.Convert(x0);
        ////    double y = unit.Convert(y0);
        ////    double h = unit.Convert(h0);
        ////    double v = unit.Convert(v0);
        ////    double zOffset = unit.Convert(infoModel0.zOffset);
        ////    XYZ p0 = GetXYZOriginTagDetail(unit, infoModel0, planarFace0);
        ////    XYZ p1 = p0 + x * (-1) * planarFace0.FaceNormal;
        ////    XYZ leaderEnd = p1 - (y + zOffset) * XYZ.BasisZ;
        ////    XYZ LeaderElbow = p1 + (y + v) * (-1) * XYZ.BasisZ;
        ////    XYZ tagHead = LeaderElbow + h * (-1) * planarFace0.FaceNormal;
        ////    Tag = IndependentTag.Create(document, view.Id, new Reference(Rebar), true, Mode, Horizontal, tagHead);
        ////    Tag.ChangeTypeId(settingModel.SelectedRebarTag.Id);
        ////    Tag.LeaderEndCondition = LeaderEndCondition.Free;
        ////    Tag.LeaderEnd = leaderEnd;
        ////    Tag.LeaderElbow = LeaderElbow;
        ////    Tag.TagHeadPosition = tagHead;
        ////}
        ////private XYZ GetXYZOriginTagHeadSectionUp(UnitProject unit, InfoModel infoModel, PlanarFace planarFace0, double x0, double y0, double tagH0, double tagV0)
        ////{
        ////    double tagH = unit.Convert(tagH0);
        ////    double tagV = unit.Convert(tagV0);
        ////    double x = unit.Convert(x0);
        ////    double y = unit.Convert(y0);
        ////    XYZ p1 = PointModel.ProjectToPlane(infoModel.LeftRightPlanar[1].Origin, infoModel.TopBottomPlanar[0]);
        ////    XYZ p2 = PointModel.ProjectToPlane(p1, planarFace0);
        ////    XYZ p3 = p2 + x * (-1) * planarFace0.FaceNormal;
        ////    XYZ p5 = p3 + tagV * XYZ.BasisZ;
        ////    XYZ p6 = p5 + tagH * infoModel.LeftRightPlanar[1].FaceNormal;
        ////    return p6;
        ////}
        ////private List<XYZ> GetXYZVectorLineDimensionSectionUp(ViewSection viewSection, Document document, UnitProject unit, InfoModel infoModel, PlanarFace planarFace0, double y0, double tagH0, double tagV0)
        ////{
        ////    List<XYZ> a = new List<XYZ>();
        ////    double tagV = unit.Convert(tagV0);
        ////    double x0 = PointModel.DistanceTo2(planarFace0, viewSection.Origin, document);
        ////    XYZ tagHead = GetXYZOriginTagHeadSectionUp(unit, infoModel, planarFace0, x0, y0, tagH0, tagV0);
        ////    double x = unit.Convert(x0);
        ////    XYZ p1 = PointModel.ProjectToPlane(infoModel.LeftRightPlanar[0].Origin, infoModel.TopBottomPlanar[0]);
        ////    XYZ p2 = PointModel.ProjectToPlane(infoModel.LeftRightPlanar[1].Origin, infoModel.TopBottomPlanar[0]);
        ////    XYZ p3 = p1 + tagV * XYZ.BasisZ;
        ////    XYZ p4 = p2 + tagV * XYZ.BasisZ;
        ////    XYZ p3a = PointModel.ProjectToPlane(p3, planarFace0);
        ////    XYZ p4a = PointModel.ProjectToPlane(p4, planarFace0);
        ////    XYZ lineDirection = viewSection.RightDirection;
        ////    XYZ lineOrigin1 = p3a + 0.5 * (p4a - p3a);
        ////    XYZ lineOrigin = lineOrigin1 + x * (-1) * planarFace0.FaceNormal;
        ////    XYZ planeNormal = (-1) * planarFace0.FaceNormal;
        ////    a.Add(tagHead); a.Add(lineOrigin); a.Add(lineDirection); a.Add(planeNormal);
        ////    return a;
        ////}
        ////private XYZ GetXYZOriginTagHeadSectionDown(UnitProject unit, InfoModel infoModel, PlanarFace planarFace0, double x0, double y0, double tagH0, double tagV0)
        ////{
        ////    double tagH = unit.Convert(tagH0);
        ////    double tagV = unit.Convert(tagV0);
        ////    double x = unit.Convert(x0);
        ////    double y = unit.Convert(y0);
        ////    XYZ p1 = PointModel.ProjectToPlane(infoModel.LeftRightPlanar[1].Origin, infoModel.TopBottomPlanar[1]);
        ////    XYZ p2 = PointModel.ProjectToPlane(p1, planarFace0);
        ////    XYZ p3 = p2 + x * (-1) * planarFace0.FaceNormal;
        ////    XYZ p5 = p3 + tagV * (-1) * XYZ.BasisZ;
        ////    XYZ p6 = p5 + tagH * infoModel.LeftRightPlanar[1].FaceNormal;
        ////    return p6;
        ////}
        ////private List<XYZ> GetXYZVectorLineDimensionSectionDown(ViewSection viewSection, Document document, UnitProject unit, InfoModel infoModel, PlanarFace planarFace0, double y0, double tagH0, double tagV0)
        ////{
        ////    List<XYZ> a = new List<XYZ>();
        ////    double tagV = unit.Convert(tagV0);
        ////    double x0 = PointModel.DistanceTo2(planarFace0, viewSection.Origin, document);
        ////    XYZ tagHead = GetXYZOriginTagHeadSectionDown(unit, infoModel, planarFace0, x0, y0, tagH0, tagV0);
        ////    double x = unit.Convert(x0);
        ////    XYZ p1 = PointModel.ProjectToPlane(infoModel.LeftRightPlanar[0].Origin, infoModel.TopBottomPlanar[1]);
        ////    XYZ p2 = PointModel.ProjectToPlane(infoModel.LeftRightPlanar[1].Origin, infoModel.TopBottomPlanar[1]);
        ////    XYZ p3 = p1 + tagV * (-1) * XYZ.BasisZ;
        ////    XYZ p4 = p2 + tagV * (-1) * XYZ.BasisZ;
        ////    XYZ p3a = PointModel.ProjectToPlane(p3, planarFace0);
        ////    XYZ p4a = PointModel.ProjectToPlane(p4, planarFace0);
        ////    XYZ lineDirection = viewSection.RightDirection;
        ////    XYZ lineOrigin1 = p3a + 0.5 * (p4a - p3a);
        ////    XYZ lineOrigin = lineOrigin1 + x * (-1) * planarFace0.FaceNormal;
        ////    XYZ planeNormal = (-1) * planarFace0.FaceNormal;
        ////    a.Add(tagHead); a.Add(lineOrigin); a.Add(lineDirection); a.Add(planeNormal);
        ////    return a;
        ////}

        ////public void CreateTagRebarSectionUp(ViewSection viewSection, Document document, UnitProject unit, InfoModel infoModel, PlanarFace planarFace0, SettingModel settingModel, double y0, double tagH0, double tagV0)
        ////{
        ////    MultiTagOption = new MultiReferenceAnnotationOptions(settingModel.SelectedMultiType);
        ////    List<XYZ> vector = GetXYZVectorLineDimensionSectionUp(viewSection, document, unit, infoModel, planarFace0, y0, tagH0, tagV0);
        ////    MultiTagOption.TagHeadPosition = vector[0];
        ////    MultiTagOption.DimensionLineOrigin = vector[1];
        ////    MultiTagOption.DimensionLineDirection = vector[2];
        ////    MultiTagOption.DimensionPlaneNormal = vector[3];
        ////    List<ElementId> a = new List<ElementId>();
        ////    a.Add(Rebar.Id);
        ////    MultiTagOption.SetElementsToDimension(a);
        ////    MultiTag = MultiReferenceAnnotation.Create(document, viewSection.Id, MultiTagOption);
        ////}
        ////public void CreateTagRebarSectionDown(ViewSection viewSection, Document document, UnitProject unit, InfoModel infoModel, PlanarFace planarFace0, SettingModel settingModel, double y0, double tagH0, double tagV0)
        ////{
        ////    MultiTagOption = new MultiReferenceAnnotationOptions(settingModel.SelectedMultiType);
        ////    List<XYZ> vector = GetXYZVectorLineDimensionSectionDown(viewSection, document, unit, infoModel, planarFace0, y0, tagH0, tagV0);
        ////    MultiTagOption.TagHeadPosition = vector[0];
        ////    MultiTagOption.DimensionLineOrigin = vector[1];
        ////    MultiTagOption.DimensionLineDirection = vector[2];
        ////    MultiTagOption.DimensionPlaneNormal = vector[3];
        ////    List<ElementId> a = new List<ElementId>();
        ////    a.Add(Rebar.Id);
        ////    MultiTagOption.SetElementsToDimension(a);
        ////    MultiTag = MultiReferenceAnnotation.Create(document, viewSection.Id, MultiTagOption);
        ////}
        ////private XYZ GetXYZOriginTagHeadSectionItem(UnitProject unit, InfoModel infoModel0, PlanarFace planarFace0, double x0, double y0, double tagH0, double tmin0)
        ////{
        ////    double tagH = unit.Convert(tagH0);
        ////    double tmin = unit.Convert(tmin0);
        ////    double x = unit.Convert(x0);
        ////    double y = unit.Convert(y0);
        ////    XYZ p1 = PointModel.ProjectToPlane(infoModel0.LeftRightPlanar[1].Origin, infoModel0.TopBottomPlanar[0]);
        ////    XYZ p2 = PointModel.ProjectToPlane(p1, planarFace0);
        ////    XYZ p3 = p2 + x * (-1) * planarFace0.FaceNormal;
        ////    XYZ p5 = p3 + (tmin -y) * XYZ.BasisZ;
        ////    XYZ p6 = p5 + tagH * infoModel0.LeftRightPlanar[1].FaceNormal;
        ////    return p6;
        ////}
        ////private List<XYZ> GetXYZVectorLineDimensionSectionItem(ViewSection viewSection, Document document, UnitProject unit, InfoModel infoModel0, PlanarFace planarFace0, double y0, double tagH0, double tmin0)
        ////{
        ////    List<XYZ> a = new List<XYZ>();
        ////    double tmin = unit.Convert(tmin0);
        ////    double x0 = PointModel.DistanceTo2(planarFace0, viewSection.Origin, document);
        ////    double y = unit.Convert(y0);
        ////    XYZ tagHead = GetXYZOriginTagHeadSectionItem(unit, infoModel0, planarFace0, x0, y0, tagH0, tmin0);
        ////    double x = unit.Convert(x0);
        ////    XYZ p1 = PointModel.ProjectToPlane(infoModel0.LeftRightPlanar[0].Origin, infoModel0.TopBottomPlanar[0]);
        ////    XYZ p2 = PointModel.ProjectToPlane(infoModel0.LeftRightPlanar[1].Origin, infoModel0.TopBottomPlanar[0]);
        ////    XYZ p3 = p1 + (tmin - y) *  XYZ.BasisZ;
        ////    XYZ p4 = p2 + (tmin - y) *  XYZ.BasisZ;
        ////    XYZ p3a = PointModel.ProjectToPlane(p3, planarFace0);
        ////    XYZ p4a = PointModel.ProjectToPlane(p4, planarFace0);
        ////    XYZ p3b = p3a + x * (-1) * planarFace0.FaceNormal;
        ////    XYZ p4b = p4a + x * (-1) * planarFace0.FaceNormal;
        ////    XYZ lineDirection = viewSection.RightDirection;
        ////    XYZ lineOrigin = p3b + 0.5 * (p4b - p3b);
        ////    XYZ planeNormal = (-1) * planarFace0.FaceNormal;
        ////    a.Add(tagHead); a.Add(lineOrigin); a.Add(lineDirection); a.Add(planeNormal);
        ////    return a;
        ////}
        ////public void CreateTagRebarSectionItem(ViewSection viewSection, Document document, UnitProject unit, InfoModel infoModel0, PlanarFace planarFace0, SettingModel settingModel, double y0, double tagH0, double tmin0)
        ////{

        ////    MultiTagOption = new MultiReferenceAnnotationOptions(settingModel.SelectedMultiType);
        ////    List<XYZ> vector = GetXYZVectorLineDimensionSectionItem(viewSection, document, unit, infoModel0, planarFace0, y0, tagH0, tmin0);
        ////    MultiTagOption.TagHeadPosition = vector[0];
        ////    MultiTagOption.DimensionLineOrigin = vector[1];
        ////    MultiTagOption.DimensionLineDirection = vector[2];
        ////    MultiTagOption.DimensionPlaneNormal = vector[3];
        ////    List<ElementId> a = new List<ElementId>();
        ////    Rebar bar = new FilteredElementCollector(document, viewSection.Id).WhereElementIsNotElementType().OfClass(typeof(Rebar)).Cast<Rebar>().Where(x => x.Id == Rebar.Id).FirstOrDefault();
        ////    if (bar != null)
        ////    {
        ////        a.Add(bar.Id);
        ////        MultiTagOption.SetElementsToDimension(a);
        ////        MultiTag = MultiReferenceAnnotation.Create(document, viewSection.Id, MultiTagOption);
        ////    }
        ////}
        //#endregion
    }
}
