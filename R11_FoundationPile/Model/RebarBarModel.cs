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
       
        #region RebarServerUpdate
        private void GetXY(FoundationModel FoundationModel,out XYZ x, out XYZ y)
        {
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
        }
        private void GetAllReference(FoundationModel FoundationModel, XYZ x, XYZ y,out List<Reference> refHostTop, out List<Reference> refHostBottom, out List<Reference> refStartH, out List<Reference> refEndH, out List<Reference> refStartV, out List<Reference> refEndV,bool image23)
        {
            refHostTop = new List<Reference>();
            refHostBottom = new List<Reference>();
            refStartH = new List<Reference>();
            refEndH = new List<Reference>();
            refStartV = new List<Reference>();
            refEndV = new List<Reference>();
            refHostTop.Add(SolidFace.GetReference(FoundationModel.Foundation, XYZ.BasisZ));
            refHostBottom.Add(SolidFace.GetReference(FoundationModel.Foundation, -XYZ.BasisZ));
            refStartH.Add((image23) ? SolidFace.GetReference(FoundationModel.WallLeft, x) : SolidFace.GetReference(FoundationModel.Foundation, y));
            refEndH.Add((image23) ? SolidFace.GetReference(FoundationModel.WallRight, -x) : SolidFace.GetReference(FoundationModel.Foundation, -y));
            refStartV.Add((image23) ? SolidFace.GetReference(FoundationModel.WallTop, -y) : SolidFace.GetReference(FoundationModel.Foundation, x));
            refEndV.Add((image23) ? SolidFace.GetReference(FoundationModel.WallBottom, y) : SolidFace.GetReference(FoundationModel.Foundation, -x));
        }
        private double GetOffsetSide(UnitProject Unit, double dMainBottom, double dSide, double deltaZ0, double CoverSide, bool bottom, bool imgae23)
        {
            
            if (imgae23)
            {
                return (bottom) ? (-Unit.Convert(CoverSide)) : (-Unit.Convert(CoverSide + dMainBottom + dSide));
            }
            else
            {
                return (bottom) ? (0.0) : (-Unit.Convert(dMainBottom + dSide));
            }
            
        }
        private double GetOffsetTopBottom(double dMainBottom, double dMainTop, double deltaZ0, bool bottom, bool secondaty, bool horizontal)
        {
            double offet = 0;
            if (horizontal)
            {
                offet = deltaZ0;
            }
            else
            {
                if (bottom)
                {
                    if (secondaty)
                    {
                        offet = dMainBottom;
                    }
                    else
                    {
                        offet = 0.0;
                    }
                }
                else
                {
                    if (secondaty)
                    {
                        offet = dMainTop;
                    }
                    else
                    {
                        offet = 0.0;
                    }
                }
            }
            return offet;
        }
        private Rebar GetRebarServerGUID(Document document, SettingModel settingModel, FoundationModel FoundationModel, UnitProject Unit, BarModel barModel, bool horizontal)
        {
            var bar = Rebar.CreateFreeForm(document, new Guid("88e37c6b-3ad6-4de3-a610-fcadcbb3021c"), RebarBarType, FoundationModel.Foundation);
            bar.LookupParameter("Layout Rule").Set((int)RebarLayoutRule.MaximumSpacing);
            bar.LookupParameter("Spacing").Set(Unit.Convert(barModel.Distance));
            if (!horizontal)
            {
                if (barModel.HookLength != 0)
                {
                    bar.LookupParameter("Hook At Start").Set(barModel.Hook.Id);
                    bar.LookupParameter("Hook At End").Set(barModel.Hook.Id);
                    bar.SetHookRotationAngle(0, 0);
                    bar.SetHookRotationAngle(0, 1);
                }
            }
            else
            {
                bar.LookupParameter("Style").Set(1);
                if (settingModel.SelectedHook.Name.Contains("Stirrup"))
                {
                    bar.LookupParameter("Hook At Start").Set(settingModel.SelectedHook.Id);
                    bar.LookupParameter("Hook At End").Set(settingModel.SelectedHook.Id);
                    bar.SetHookRotationAngle(0, 0);
                    bar.SetHookRotationAngle(0, 1);
                }

            }
            return bar;
        }
        private void GetHandleConstraint(List<Reference> refHost, List<Reference> refStart, List<Reference> refEnd, UnitProject Unit, Rebar bar, double offsetTopBottom, double offsetSide, double dMainBottom, double dSide, double CoverSide, bool bottom, bool image23)
        {
            RebarConstraintsManager rManager = bar.GetRebarConstraintsManager();
            IList<RebarConstrainedHandle> handles = rManager.GetAllHandles();
            foreach (RebarConstrainedHandle handle in handles)
            {
                if (handle.GetHandleType() != RebarHandleType.StartOfBar ||
                   handle.GetHandleType() != RebarHandleType.EndOfBar)
                {
                    try
                    {
                        if (handle.GetHandleName().Equals("Host Surface"))
                        {
                            RebarConstraint constraint = RebarConstraint.Create(handle, refHost, true, -Unit.Convert(offsetTopBottom));
                            rManager.SetPreferredConstraintForHandle(handle, constraint);
                        }
                        if (handle.GetHandleName().Equals("Start Surface"))
                        {
                            RebarConstraint constraint = RebarConstraint.Create(handle, refStart, true, offsetSide);
                            rManager.SetPreferredConstraintForHandle(handle, constraint);
                        }
                        if (handle.GetHandleName().Equals("End Surface"))
                        {
                            RebarConstraint constraint = RebarConstraint.Create(handle, refEnd, true, offsetSide);
                            rManager.SetPreferredConstraintForHandle(handle, constraint);
                        }
                    }
                    catch (Exception e)
                    {
                        System.Windows.Forms.MessageBox.Show(e.Message);
                    }
                }

            }
        }
        private void CreateStartEnd(Document document, SettingModel settingModel, FoundationModel FoundationModel, List<Reference> refHost, List<Reference> refStart, List<Reference> refEnd, UnitProject Unit, BarModel barModel, double dMainBottom, double dMainTop, double dSide, double CoverTop, double CoverBottom, double CoverSide, double deltaZ0, bool bottom, bool secondaty, bool horizontal)
        {
            Rebar bar = GetRebarServerGUID(document, settingModel, FoundationModel, Unit, barModel, horizontal);
            double offsetTopBottom = GetOffsetTopBottom(dMainBottom, dMainTop, deltaZ0, bottom, secondaty, horizontal);
            double offsetSide = GetOffsetSide(Unit, dMainBottom, dSide, deltaZ0, CoverSide, bottom, false);
            GetHandleConstraint(refHost, refStart, refEnd, Unit, bar, offsetTopBottom, offsetSide, dMainBottom, dSide, CoverSide, bottom, false);
           
            Rebars.Add(bar);
            SetPartitionRebar(FoundationModel);
        }
        private void CreateStartEnd2(Document document, SettingModel settingModel, FoundationModel FoundationModel, List<Reference> refHost, List<Reference> refStart, List<Reference> refEnd, UnitProject Unit, BarModel barModel, double dMainBottom, double dMainTop, double dSide, double CoverTop, double CoverBottom, double CoverSide, double deltaZ0, bool bottom, bool secondaty, bool horizontal)
        {
            Rebar bar = GetRebarServerGUID(document, settingModel, FoundationModel, Unit, barModel, horizontal);
            
            bar.IncludeFirstBar = false;
            bar.IncludeLastBar = false;
            double offsetTopBottom = GetOffsetTopBottom(dMainBottom, dMainTop, deltaZ0, bottom, secondaty, horizontal);
            double offsetSide = GetOffsetSide(Unit, dMainBottom, dSide, deltaZ0, CoverSide, bottom, true);
            GetHandleConstraint(refHost, refStart, refEnd, Unit, bar, offsetTopBottom, offsetSide, dMainBottom, dSide, CoverSide, bottom, true);
           
            Rebars.Add(bar);
            SetPartitionRebar(FoundationModel);
        }
        #endregion
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
                SetPartitionRebar(FoundationModel);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            
        }
       
        private void CreateRebarImage0(Document document, SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel barModel, UnitProject Unit, double dMainBottom, double dMainTop, double dSide, double CoverTop, double CoverBottom, double CoverSide)
        {
            List<double> Distance = new List<double>();
            double delta = (settingModel.HeightFoundation - CoverBottom - CoverTop) / (barModel.Layer + 1);
            Curves = ProcessCurveRebar.GetCurvesImage0A(settingModel, FoundationModel, FoundationBarModel, barModel, Unit, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, out Distance);
            XYZ x=XYZ.BasisX ,y= XYZ.BasisY; GetXY(FoundationModel,out x, out y);
            Reference HostTop = SolidFace.GetReference(FoundationModel.Foundation, XYZ.BasisZ);
            List<Reference> refHostTop, refHostBottom, refStartH, refEndH, refStartV, refEndV;
            GetAllReference(FoundationModel, x, y, out refHostTop, out refHostBottom, out refStartH, out refEndH, out refStartV, out refEndV,false);
            if (barModel.IsModel)
            {
                switch (barModel.Name)
                {
                    case "MainBottom":
                        if (FoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            CreateStartEnd(document, settingModel, FoundationModel, refHostBottom, refStartH, refEndH, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, true, false, false);
                        }
                        else
                        {
                            CreateStartEnd(document, settingModel, FoundationModel, refHostBottom, refStartV, refEndV, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, true, false, false);
                        }
                        break;
                    case "MainTop":
                        if (FoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            CreateStartEnd(document, settingModel, FoundationModel, refHostTop, refStartH, refEndH, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, false, false, false);
                        }
                        else
                        {
                            CreateStartEnd(document, settingModel, FoundationModel, refHostTop, refStartV, refEndV, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, false, false, false);
                        }
                        break;
                    case "MainAddHorizontal":
                        for (int i = 0; i < barModel.Layer; i++)
                        {
                            if (FoundationBarModel.SpanOrientation.Equals("Horizontal"))
                            {
                                CreateStartEnd(document, settingModel, FoundationModel, refHostBottom, refStartH, refEndH, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta * (i + 1), false, false, true);
                            }
                            else
                            {
                                CreateStartEnd(document, settingModel, FoundationModel, refHostBottom, refStartV, refEndV, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta * (i + 1), false, false, true);
                            }
                        }

                        break;
                    case "MainAddVertical":
                        if (Curves.Count != 0)
                        {

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
                        break;
                    case "SecondaryBottom":
                        if (FoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            CreateStartEnd(document, settingModel, FoundationModel, refHostBottom, refStartV, refEndV, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, true, true, false);
                        }
                        else
                        {
                            CreateStartEnd(document, settingModel, FoundationModel, refHostBottom, refStartH, refEndH, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, true, true, false);
                        }
                        break;
                    case "SecondaryTop":
                        if (FoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            CreateStartEnd(document, settingModel, FoundationModel, refHostTop, refStartV, refEndV, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, false, true, false);
                        }
                        else
                        {
                            CreateStartEnd(document, settingModel, FoundationModel, refHostTop, refStartH, refEndH, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, false, true, false);
                        }
                        break;
                    case "SecondaryAddHorizontal":
                        for (int i = 0; i < barModel.Layer; i++)
                        {
                            if (FoundationBarModel.SpanOrientation.Equals("Horizontal"))
                            {
                                CreateStartEnd(document, settingModel, FoundationModel, refHostBottom, refStartV, refEndV, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta * (i + 1), false, false, true);
                            }
                            else
                            {
                                CreateStartEnd(document, settingModel, FoundationModel, refHostBottom, refStartH, refEndH, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta * (i + 1), false, false, true);
                            }
                        }
                        break;
                    case "SecondaryAddVertical":
                        if (Curves.Count != 0)
                        {
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
                        break;
                    case "Side":
                        CreateRebarSide(document, settingModel, FoundationModel, barModel, Unit, CoverTop, CoverBottom, CoverSide);
                        break;
                    default: break;
                }
            }
        }
        private void CreateRebarImage1(Document document, SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel barModel, UnitProject Unit, double dMainBottom, double dMainTop, double dSide, double CoverTop, double CoverBottom, double CoverSide)
        {
            List<double> Distance = new List<double>();
            Curves = ProcessCurveRebar.GetCurvesImage1(settingModel, FoundationModel, FoundationBarModel, barModel, Unit, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, out Distance);
            double delta = (settingModel.HeightFoundation - CoverBottom - CoverTop) / (barModel.Layer + 1);
            XYZ x = XYZ.BasisX, y = XYZ.BasisY; GetXY(FoundationModel,out x, out y);
            Reference HostTop = SolidFace.GetReference(FoundationModel.Foundation, XYZ.BasisZ);
            List<Reference> refHostTop, refHostBottom, refStartH, refEndH, refStartV, refEndV;
            GetAllReference(FoundationModel, x, y, out refHostTop, out refHostBottom, out refStartH, out refEndH, out refStartV, out refEndV, false);
            if (barModel.IsModel)
            {
                switch (barModel.Name)
                {
                    case "MainBottom":
                        if (FoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            CreateStartEnd(document, settingModel, FoundationModel, refHostBottom, refStartH, refEndH, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, true, false, false);
                        }
                        else
                        {
                            CreateStartEnd(document, settingModel, FoundationModel, refHostBottom, refStartV, refEndV, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, true, false, false);
                        }
                        break;
                    case "MainTop":
                        if (FoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            CreateStartEnd(document, settingModel, FoundationModel, refHostTop, refStartH, refEndH, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, false, false, false);
                        }
                        else
                        {
                            CreateStartEnd(document, settingModel, FoundationModel, refHostTop, refStartV, refEndV, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, false, false, false);
                        }
                        break;
                    case "MainAddHorizontal":
                        for (int i = 0; i < barModel.Layer; i++)
                        {
                            if (FoundationBarModel.SpanOrientation.Equals("Horizontal"))
                            {
                                CreateStartEnd(document, settingModel, FoundationModel, refHostBottom, refStartH, refEndH, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta * (i + 1), false, false, true);
                            }
                            else
                            {
                                CreateStartEnd(document, settingModel, FoundationModel, refHostBottom, refStartV, refEndV, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta * (i + 1), false, false, true);
                            }
                        }

                        break;
                    case "MainAddVertical":
                        if (Curves.Count != 0)
                        {
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
                        break;
                    case "SecondaryBottom":
                        if (FoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            CreateStartEnd(document, settingModel, FoundationModel, refHostBottom, refStartV, refEndV, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, true, true, false);
                        }
                        else
                        {
                            CreateStartEnd(document, settingModel, FoundationModel, refHostBottom, refStartH, refEndH, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, true, true, false);
                        }
                        break;
                    case "SecondaryTop":
                        if (FoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            CreateStartEnd(document, settingModel, FoundationModel, refHostTop, refStartV, refEndV, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, false, true, false);
                        }
                        else
                        {
                            CreateStartEnd(document, settingModel, FoundationModel, refHostTop, refStartH, refEndH, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, false, true, false);
                        }
                        break;
                    case "SecondaryAddHorizontal":
                        for (int i = 0; i < barModel.Layer; i++)
                        {
                            if (FoundationBarModel.SpanOrientation.Equals("Horizontal"))
                            {
                                CreateStartEnd(document, settingModel, FoundationModel, refHostBottom, refStartV, refEndV, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta * (i + 1), false, false, true);
                            }
                            else
                            {
                                CreateStartEnd(document, settingModel, FoundationModel, refHostBottom, refStartH, refEndH, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta * (i + 1), false, false, true);
                            }
                        }
                        break;
                    case "SecondaryAddVertical":
                        if (Curves.Count != 0)
                        {
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
                        break;
                    case "Side":
                        CreateRebarSide(document, settingModel, FoundationModel, barModel, Unit, CoverTop, CoverBottom, CoverSide);
                        break;
                    default: break;
                }
            }

        }
        private void CreateRebarImage2WallNull(Document document, SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel barModel, UnitProject Unit, double dMainBottom, double dMainTop, double dSide, double CoverTop, double CoverBottom, double CoverSide)
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
                    CreateRebarSide(document, settingModel, FoundationModel, barModel, Unit, CoverTop, CoverBottom, CoverSide);
                }

            }
        }
        private void CreateRebarImage2(Document document, SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel barModel, UnitProject Unit, double dMainBottom, double dMainTop, double dSide, double CoverTop, double CoverBottom, double CoverSide)
        {
            if (settingModel.WallType == null)
            {
                CreateRebarImage2WallNull(document, settingModel, FoundationModel, FoundationBarModel, barModel, Unit, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide);
            }
            else
            {
                List<double> Distance = new List<double>();
                Curves = ProcessCurveRebar.GetCurvesImage2(settingModel, FoundationModel, FoundationBarModel, barModel, Unit, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, out Distance);
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
                Reference HostTop = SolidFace.GetReference(FoundationModel.Foundation, XYZ.BasisZ);
                List<Reference> refHostTop = new List<Reference>();
                refHostTop.Add(HostTop);
                Reference HostBottom = SolidFace.GetReference(FoundationModel.Foundation, -XYZ.BasisZ);
                List<Reference> refHostBottom = new List<Reference>();
                refHostBottom.Add(HostBottom);

                List<Reference> refStartH = new List<Reference>();
                List<Reference> refEndH = new List<Reference>();
                List<Reference> refStartV = new List<Reference>();
                List<Reference> refEndV = new List<Reference>();
                Reference StartH = SolidFace.GetReference(FoundationModel.WallLeft, x);
                Reference EndH = SolidFace.GetReference(FoundationModel.WallRight, -x);
                Reference StartV = SolidFace.GetReference(FoundationModel.WallTop, -y);
                Reference EndV = SolidFace.GetReference(FoundationModel.WallBottom, y);
                refStartH.Add(StartH);
                refEndH.Add(EndH);
                refStartV.Add(StartV);
                refEndV.Add(EndV);
                double delta = (settingModel.HeightFoundation - CoverBottom - CoverTop) / (barModel.Layer + 1);
                if (barModel.IsModel)
                {
                    switch (barModel.Name)
                    {
                        case "MainBottom":
                            if (FoundationBarModel.SpanOrientation.Equals("Horizontal"))
                            {
                                CreateStartEnd2(document, settingModel, FoundationModel, refHostBottom, refStartH, refEndH, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, true, false, false);
                            }
                            else
                            {
                                CreateStartEnd2(document, settingModel, FoundationModel, refHostBottom, refStartV, refEndV, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, true, false, false);
                            }
                            break;
                        case "MainTop":
                            if (FoundationBarModel.SpanOrientation.Equals("Horizontal"))
                            {
                                CreateStartEnd2(document, settingModel, FoundationModel, refHostTop, refStartH, refEndH, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, false, false, false);
                            }
                            else
                            {
                                CreateStartEnd2(document, settingModel, FoundationModel, refHostTop, refStartV, refEndV, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, false, false, false);
                            }
                            break;
                        case "MainAddHorizontal":
                            for (int i = 0; i < barModel.Layer; i++)
                            {
                                if (FoundationBarModel.SpanOrientation.Equals("Horizontal"))
                                {
                                    CreateStartEnd2(document, settingModel, FoundationModel, refHostBottom, refStartH, refEndH, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta * (i + 1), false, false, true);
                                }
                                else
                                {
                                    CreateStartEnd2(document, settingModel, FoundationModel, refHostBottom, refStartV, refEndV, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta * (i + 1), false, false, true);
                                }
                            }
                            break;
                        case "MainAddVertical":
                            if (Curves.Count != 0)
                            {
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
                            break;
                        case "SecondaryBottom":
                            if (FoundationBarModel.SpanOrientation.Equals("Horizontal"))
                            {
                                CreateStartEnd2(document, settingModel, FoundationModel, refHostBottom, refStartV, refEndV, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, true, true, false);
                            }
                            else
                            {
                                CreateStartEnd2(document, settingModel, FoundationModel, refHostBottom, refStartH, refEndH, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, true, true, false);
                            }
                            break;
                        case "SecondaryTop":
                            if (FoundationBarModel.SpanOrientation.Equals("Horizontal"))
                            {
                                CreateStartEnd2(document, settingModel, FoundationModel, refHostTop, refStartV, refEndV, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, false, true, false);
                            }
                            else
                            {
                                CreateStartEnd2(document, settingModel, FoundationModel, refHostTop, refStartH, refEndH, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, false, true, false);
                            }
                            break;
                        case "SecondaryAddHorizontal":
                            for (int i = 0; i < barModel.Layer; i++)
                            {
                                if (FoundationBarModel.SpanOrientation.Equals("Horizontal"))
                                {
                                    CreateStartEnd2(document, settingModel, FoundationModel, refHostBottom, refStartV, refEndV, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta * (i + 1), false, false, true);
                                }
                                else
                                {
                                    CreateStartEnd2(document, settingModel, FoundationModel, refHostBottom, refStartH, refEndH, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta * (i + 1), false, false, true);
                                }
                            }
                            break;
                        case "SecondaryAddVertical":
                            if (Curves.Count != 0)
                            {
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
                            break;
                        case "Side":
                            if (Curves.Count != 0)
                            {
                                CreateRebarSide(document, settingModel, FoundationModel, barModel, Unit, CoverTop, CoverBottom, CoverSide);
                            }
                            break;
                        default: break;
                    }
                }
            }

        }

        private void CreateRebarImage3WallNull(Document document, SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel barModel, UnitProject Unit, double dMainBottom, double dMainTop, double dSide, double CoverTop, double CoverBottom, double CoverSide)
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
                    CreateRebarSide(document, settingModel, FoundationModel, barModel, Unit, CoverTop, CoverBottom, CoverSide);
                }

            }
        }
        private void CreateRebarImage3(Document document, SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel barModel, UnitProject Unit, double dMainBottom, double dMainTop, double dSide, double CoverTop, double CoverBottom, double CoverSide)
        {
            if (settingModel.WallType == null)
            {
                CreateRebarImage3WallNull(document, settingModel, FoundationModel, FoundationBarModel, barModel, Unit, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide);
            }
            else
            {
                List<double> Distance = new List<double>();
                Curves = ProcessCurveRebar.GetCurvesImage3(settingModel, FoundationModel, FoundationBarModel, barModel, Unit, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, out Distance);
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
                Reference HostTop = SolidFace.GetReference(FoundationModel.Foundation, XYZ.BasisZ);
                List<Reference> refHostTop = new List<Reference>();
                refHostTop.Add(HostTop);
                Reference HostBottom = SolidFace.GetReference(FoundationModel.Foundation, -XYZ.BasisZ);
                List<Reference> refHostBottom = new List<Reference>();
                refHostBottom.Add(HostBottom);

                List<Reference> refStartH = new List<Reference>();
                List<Reference> refEndH = new List<Reference>();
                List<Reference> refStartV = new List<Reference>();
                List<Reference> refEndV = new List<Reference>();
                Reference StartH = SolidFace.GetReference(FoundationModel.WallLeft, x);
                Reference EndH = SolidFace.GetReference(FoundationModel.WallRight, -x);
                Reference StartV = SolidFace.GetReference(FoundationModel.WallTop, -y);
                Reference EndV = SolidFace.GetReference(FoundationModel.WallBottom, y);
                refStartH.Add(StartH);
                refEndH.Add(EndH);
                refStartV.Add(StartV);
                refEndV.Add(EndV);
                double delta = (settingModel.HeightFoundation - CoverBottom - CoverTop) / (barModel.Layer + 1);
                if (barModel.IsModel)
                {
                    switch (barModel.Name)
                    {
                        case "MainBottom":
                            if (FoundationBarModel.SpanOrientation.Equals("Horizontal"))
                            {
                                CreateStartEnd2(document, settingModel, FoundationModel, refHostBottom, refStartH, refEndH, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, true, false, false);
                            }
                            else
                            {
                                CreateStartEnd2(document, settingModel, FoundationModel, refHostBottom, refStartV, refEndV, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, true, false, false);
                            }
                            break;
                        case "MainTop":
                            if (FoundationBarModel.SpanOrientation.Equals("Horizontal"))
                            {
                                CreateStartEnd2(document, settingModel, FoundationModel, refHostTop, refStartH, refEndH, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, false, false, false);
                            }
                            else
                            {
                                CreateStartEnd2(document, settingModel, FoundationModel, refHostTop, refStartV, refEndV, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, false, false, false);
                            }
                            break;
                        case "MainAddHorizontal":
                            for (int i = 0; i < barModel.Layer; i++)
                            {
                                if (FoundationBarModel.SpanOrientation.Equals("Horizontal"))
                                {
                                    CreateStartEnd2(document, settingModel, FoundationModel, refHostBottom, refStartH, refEndH, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta * (i + 1), false, false, true);
                                }
                                else
                                {
                                    CreateStartEnd2(document, settingModel, FoundationModel, refHostBottom, refStartV, refEndV, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta * (i + 1), false, false, true);
                                }
                            }
                            break;
                        case "MainAddVertical":
                            if (Curves.Count != 0)
                            {
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
                            break;
                        case "SecondaryBottom":
                            if (FoundationBarModel.SpanOrientation.Equals("Horizontal"))
                            {
                                CreateStartEnd2(document, settingModel, FoundationModel, refHostBottom, refStartV, refEndV, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, true, true, false);
                            }
                            else
                            {
                                CreateStartEnd2(document, settingModel, FoundationModel, refHostBottom, refStartH, refEndH, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, true, true, false);
                            }
                            break;
                        case "SecondaryTop":
                            if (FoundationBarModel.SpanOrientation.Equals("Horizontal"))
                            {
                                CreateStartEnd2(document, settingModel, FoundationModel, refHostTop, refStartV, refEndV, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, false, true, false);
                            }
                            else
                            {
                                CreateStartEnd2(document, settingModel, FoundationModel, refHostTop, refStartH, refEndH, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta, false, true, false);
                            }
                            break;
                        case "SecondaryAddHorizontal":
                            for (int i = 0; i < barModel.Layer; i++)
                            {
                                if (FoundationBarModel.SpanOrientation.Equals("Horizontal"))
                                {
                                    CreateStartEnd2(document, settingModel, FoundationModel, refHostBottom, refStartV, refEndV, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta * (i + 1), false, false, true);
                                }
                                else
                                {
                                    CreateStartEnd2(document, settingModel, FoundationModel, refHostBottom, refStartH, refEndH, Unit, barModel, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide, delta * (i + 1), false, false, true);
                                }
                            }
                            break;
                        case "SecondaryAddVertical":
                            if (Curves.Count != 0)
                            {
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
                            break;
                        case "Side":
                            if (Curves.Count != 0)
                            {
                                CreateRebarSide(document, settingModel, FoundationModel, barModel, Unit, CoverTop, CoverBottom, CoverSide);
                            }
                            break;
                        default: break;
                    }
                }
            }

        }
        public void CreateRebar(Document document, SettingModel settingModel, FoundationModel FoundationModel, FoundationBarModel FoundationBarModel, BarModel barModel, UnitProject Unit, double dMainBottom, double dMainTop, double dSide, double CoverTop, double CoverBottom, double CoverSide)
        {

            switch (FoundationBarModel.Image)
            {
                case 0: CreateRebarImage0(document, settingModel, FoundationModel, FoundationBarModel, barModel, Unit, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide); break;
                case 1: CreateRebarImage1(document, settingModel, FoundationModel, FoundationBarModel, barModel, Unit, dMainBottom, dMainTop, dSide, CoverTop, CoverBottom, CoverSide); break;
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
