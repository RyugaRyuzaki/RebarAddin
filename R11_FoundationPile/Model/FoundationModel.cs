using Autodesk.Revit.DB;
using DSP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WpfCustomControls;
using ViewCallOut = Autodesk.Revit.DB.View;
namespace R11_FoundationPile
{
    public class FoundationModel : BaseViewModel
    {
        #region property
        private int _FoundationNumber;
        public int FoundationNumber { get => _FoundationNumber; set { _FoundationNumber = value; OnPropertyChanged(); } }
        private int _Type;
        public int Type { get => _Type; set { _Type = value; OnPropertyChanged(); } }

        private int _Image;
        public int Image { get => _Image; set { _Image = value; OnPropertyChanged(); } }
        private string _LocationName;
        public string LocationName { get => _LocationName; set { _LocationName = value; OnPropertyChanged(); } }
        private LocationModel _Location;
        public LocationModel Location { get => _Location; set { _Location = value; OnPropertyChanged(); } }
        private ObservableCollection<LocationModel> _BoundingLocation;
        public ObservableCollection<LocationModel> BoundingLocation { get { if (_BoundingLocation == null) { _BoundingLocation = new ObservableCollection<LocationModel>(); } return _BoundingLocation; } set { _BoundingLocation = value; OnPropertyChanged(); } }
        private double _Length;
        public double Length { get => _Length; set { _Length = value; OnPropertyChanged(); } }
        private double _Width;
        public double Width { get => _Width; set { _Width = value; OnPropertyChanged(); } }
        private double _Height;
        public double Height { get => _Height; set { _Height = value; OnPropertyChanged(); } }

        private Floor _Foundation;
        public Floor Foundation { get => _Foundation; set { _Foundation = value; OnPropertyChanged(); } }
        private Floor _FormWork;
        public Floor FormWork { get => _FormWork; set { _FormWork = value; OnPropertyChanged(); } }
        private CurveArray _CurveArray;
        public CurveArray CurveArray { get => _CurveArray; set { _CurveArray = value; OnPropertyChanged(); } }
        private CurveLoop _FormWorkCurveLoop;
        public CurveLoop FormWorkCurveLoop { get => _FormWorkCurveLoop; set { _FormWorkCurveLoop = value; OnPropertyChanged(); } }

        private ObservableCollection<PileModel> _PileModels;
        public ObservableCollection<PileModel> PileModels { get { if (_PileModels == null) { _PileModels = new ObservableCollection<PileModel>(); } return _PileModels; } set { _PileModels = value; OnPropertyChanged(); } }
        private ColumnModel _ColumnModel;
        public ColumnModel ColumnModel { get => _ColumnModel; set { _ColumnModel = value; OnPropertyChanged(); } }
        private bool _IsRollBack;
        public bool IsRollBack { get { return _IsRollBack; } set { _IsRollBack = value; OnPropertyChanged(); } }
        private bool _IsRepresentative;
        public bool IsRepresentative { get { return _IsRepresentative; } set { _IsRepresentative = value; OnPropertyChanged(); } }


        private IndependentTag _TagFoundation;
        public IndependentTag TagFoundation { get => _TagFoundation; set { _TagFoundation = value; OnPropertyChanged(); } }
        private TextNote _TextTagFoundation;
        public TextNote TextTagFoundation { get => _TextTagFoundation; set { _TextTagFoundation = value; OnPropertyChanged(); } }

        public Autodesk.Revit.DB.View FoundationDetailView { get; set; }
        public ViewSection Horizontal { get; set; }
        public BoundingBoxXYZ SectionBoxHorizontal { get; set; }
        public ViewSection Vertical { get; set; }
        public BoundingBoxXYZ SectionBoxVertical { get; set; }

        #endregion
        #region Bar
        private string _SpanOrientation;
        public string SpanOrientation { get => _SpanOrientation; set { _SpanOrientation = value; OnPropertyChanged(); } }

        #endregion
        #region Wall
        public Wall WallLeft { get; set; }
        public Wall WallRight { get; set; }
        public Wall WallTop { get; set; }
        public Wall WallBottom { get; set; }
        #endregion
        public FoundationModel(int type, int image, int foundationNumber, ColumnModel columnModel, SettingModel settingModel)
        {
            Type = type; Image = image;
            FoundationNumber = foundationNumber;

            LocationName = columnModel.LocationName;
            Location = new LocationModel(columnModel.PointXPosition, columnModel.PointYPosition, columnModel.PointZPosition);
            ColumnModel = columnModel;
            CurveArray = new CurveArray();
            SpanOrientation = "Horizontal";

        }
        #region Reference 
        public void DeleteWall(Document document)
        {

            if (WallLeft != null) { ICollection<ElementId> a = document.Delete(WallLeft.Id); }
            if (WallRight != null) { ICollection<ElementId> b = document.Delete(WallRight.Id); }
            if (WallTop != null) { ICollection<ElementId> c = document.Delete(WallTop.Id); }
            if (WallBottom != null) { ICollection<ElementId> d = document.Delete(WallBottom.Id); }

        }
        public Curve GetCurvesHorizontal(double x0, UnitProject unit, double offset)
        {
            XYZ x = null;
            XYZ y = null;
            Curve curve = null;
            if (ColumnModel.Style.Equals("RECTANGLE"))
            {
                x = ColumnModel.East.FaceNormal;
                y = ColumnModel.Nouth.FaceNormal;
            }
            else
            {
                x = XYZ.BasisX;
                y = XYZ.BasisY;
            }
            double y1 = BoundingLocation.Min(b => b.Y);
            double y2 = BoundingLocation.Max(b => b.Y);
            XYZ p1 = ColumnModel.PointPosition + unit.Convert(x0 + ((x0 > 0) ? 1 : -1) * offset) * x + unit.Convert(y1) * y;
            XYZ p2 = ColumnModel.PointPosition + unit.Convert(x0 + ((x0 > 0) ? 1 : -1) * offset) * x + unit.Convert(y2) * y;
            curve = Line.CreateBound(p1, p2);
            return curve;
        }
        public Curve GetCurvesVertical(double y0, UnitProject unit, double offset)
        {
            XYZ x = null;
            XYZ y = null;
            Curve curve = null;
            if (ColumnModel.Style.Equals("RECTANGLE"))
            {
                x = ColumnModel.East.FaceNormal;
                y = ColumnModel.Nouth.FaceNormal;
            }
            else
            {
                x = XYZ.BasisX;
                y = XYZ.BasisY;
            }
            double x1 = BoundingLocation.Min(b => b.X);
            double x2 = BoundingLocation.Max(b => b.X);
            XYZ p1 = ColumnModel.PointPosition + unit.Convert(x1) * x + unit.Convert(y0 + ((y0 > 0) ? 1 : -1) * offset) * y;
            XYZ p2 = ColumnModel.PointPosition + unit.Convert(x2) * x + unit.Convert(y0 + ((y0 > 0) ? 1 : -1) * offset) * y;
            curve = Line.CreateBound(p1, p2);
            return curve;
        }

        #endregion
        #region Offset
        public List<LocationModel> GetOffsetBoundingFoundation2(SettingModel settingModel, double offset)
        {
            List<LocationModel> list = new List<LocationModel>();
            double angle = 2 * Math.PI / 5;
            double angle0 = 0;
            if (ColumnModel.Style.Equals("RECTANGLE"))
            {
                if (ColumnModel.b <= ColumnModel.h)
                {

                    angle0 = (IsRollBack) ? Math.PI * 1.5 : Math.PI * 0.5;
                }
                else
                {
                    angle0 = (IsRollBack) ? Math.PI * 2 : Math.PI;
                }
            }
            else
            {
                angle0 = (IsRollBack) ? Math.PI * 1.5 : Math.PI * 0.5;
            }
            double radius = settingModel.DistancePP * settingModel.DiameterPile + settingModel.DistancePS - offset;
            for (int i = 0; i < 5; i++)
            {
                double x = Math.Round(Math.Cos(angle0 + (i) * angle) * (radius), 9), y = Math.Round(Math.Sin(angle0 + (i) * angle) * (radius), 9), z = ColumnModel.PointZPosition;
                list.Add(new LocationModel(x, y, z));
            }
            return list;
        }
        public List<LocationModel> GetOffsetBoundingFoundation3(SettingModel settingModel, double offset)
        {
            List<LocationModel> list = new List<LocationModel>();
            double angle = 2 * Math.PI / 6;
            double angle0 = 0;
            if (ColumnModel.Style.Equals("RECTANGLE"))
            {
                if (ColumnModel.b <= ColumnModel.h)
                {

                    angle0 = 0;
                }
                else
                {
                    angle0 = Math.PI * 0.5;
                }
            }
            else
            {
                angle0 = 0;
            }
            double radius = settingModel.DistancePP * settingModel.DiameterPile + settingModel.DistancePS - offset;
            for (int i = 0; i < 6; i++)
            {
                double x = Math.Round(Math.Cos(angle0 + (i) * angle) * (radius), 9), y = Math.Round(Math.Sin(angle0 + (i) * angle) * (radius), 9), z = ColumnModel.PointZPosition;
                list.Add(new LocationModel(x, y, z));
            }
            return list;
        }
        public List<LocationModel> GetOffsetBoundingFoundation0(SettingModel settingModel, double offset)
        {
            List<LocationModel> list = new List<LocationModel>();
            double z0 = ColumnModel.PointZPosition;
            double x0 = BoundingLocation[0].X;
            double x1 = BoundingLocation[1].X;
            double x2 = BoundingLocation[2].X;
            double x3 = BoundingLocation[3].X;
            double x4 = BoundingLocation[4].X;
            double x5 = BoundingLocation[5].X;
            double y0 = BoundingLocation[0].Y;
            double y1 = BoundingLocation[1].Y;
            double y2 = BoundingLocation[2].Y;
            double y3 = BoundingLocation[3].Y;
            double y4 = BoundingLocation[4].Y;
            double y5 = BoundingLocation[5].Y;
            if (ColumnModel.Style.Equals("RECTANGLE") && (ColumnModel.b > ColumnModel.h))
            {
                double tan1 = (Math.Abs(x1 - x2)) / (Math.Abs(y1 - y2));
                double cotan1 = (Math.Abs(y1 - y2)) / (Math.Abs(x1 - x2));
                double sin1 = Math.Sqrt(1 / (1 + cotan1 * cotan1));
                double cos1 = Math.Sqrt(1 / (1 + tan1 * tan1));
                double o1 = offset - offset * sin1;
                double delx1 = o1 / sin1;
                double dely1 = delx1 * tan1;
                double dely2 = offset / cos1 - offset;
                double delx2 = dely2 / tan1;
                list.Add(new LocationModel(((x0 > 0) ? (x0 - offset) : (x0 + offset)), ((y0 > 0) ? (y0 - offset) : (y0 + offset)), z0));
                list.Add(new LocationModel(((x0 > x1) ? (x1 + dely1) : (x1 - dely1)), ((y1 > 0) ? (y1 - offset) : (y1 + offset)), z0));
                list.Add(new LocationModel(((x2 > 0) ? (x2 - offset) : (x2 + offset)), ((y2 > 0) ? (y2 - delx2) : (y2 + delx2)), z0));
                list.Add(new LocationModel(((x3 > 0) ? (x3 - offset) : (x3 + offset)), ((y3 > 0) ? (y3 - delx2) : (y3 + delx2)), z0));

                list.Add(new LocationModel(((x4 > 0) ? (x4 + dely1) : (x4 - dely1)), ((y5 > y4) ? (y4 - offset) : (y4 + offset)), z0));
                list.Add(new LocationModel(((x5 > 0) ? (x5 - offset) : (x5 + offset)), ((y5 > 0) ? (y5 - offset) : (y5 + offset)), z0));
            }
            else
            {
                double tan1 = (Math.Abs(y1 - y2)) / (Math.Abs(x1 - x2));
                double cotan1 = (Math.Abs(x1 - x2)) / (Math.Abs(y1 - y2));
                double sin1 = Math.Sqrt(1 / (1 + cotan1 * cotan1));
                double cos1 = Math.Sqrt(1 / (1 + tan1 * tan1));
                double o1 = offset - offset * sin1;
                double delx1 = o1 / sin1;
                double dely1 = delx1 * tan1;
                double dely2 = offset / cos1 - offset;
                double delx2 = dely2 / tan1;
                list.Add(new LocationModel(((x0 > 0) ? (x0 - offset) : (x0 + offset)), ((y0 > 0) ? (y0 - offset) : (y0 + offset)), z0));
                list.Add(new LocationModel(((x1 > 0) ? (x1 - offset) : (x1 + offset)), ((y0 > y1) ? (y1 + dely1) : (y1 - dely1)), z0));
                list.Add(new LocationModel(((x2 > 0) ? (x2 - delx2) : (x2 + delx2)), ((y2 > 0) ? (y2 - offset) : (y2 + offset)), z0));
                list.Add(new LocationModel(((x3 > 0) ? (x3 - delx2) : (x3 + delx2)), ((y3 > 0) ? (y3 - offset) : (y3 + offset)), z0));

                list.Add(new LocationModel(((x4 > 0) ? (x4 - offset) : (x4 + offset)), ((y5 > y4) ? (y4 + dely1) : (y4 - dely1)), z0));
                list.Add(new LocationModel(((x5 > 0) ? (x5 - offset) : (x5 + offset)), ((y5 > 0) ? (y5 - offset) : (y5 + offset)), z0));
            }
            return list;
        }
        #endregion
        #region PlanarFace
        public double GetP1(FoundationBarModel SelectedFoundationBarModel)
        {
            double p1 = 0;
            switch (SelectedFoundationBarModel.Image)
            {
                case 0:
                    if (ColumnModel.Style.Equals("RECTANGLE") && ColumnModel.b > ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p1 = BoundingLocation[2].X;
                        }
                        else
                        {
                            p1 = BoundingLocation[BoundingLocation.Count - 1].Y;
                        }
                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p1 = BoundingLocation[BoundingLocation.Count - 1].X;
                        }
                        else
                        {
                            p1 = BoundingLocation[2].Y;
                        }
                    }

                    break;
                case 1:
                    if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                    {
                        p1 = BoundingLocation[0].X;
                    }
                    else
                    {
                        p1 = BoundingLocation[0].Y;
                    }

                    break;
                case 2:
                    if (ColumnModel.Style.Equals("RECTANGLE") && ColumnModel.b > ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p1 = BoundingLocation[2].X;
                        }
                        else
                        {
                            p1 = BoundingLocation[BoundingLocation.Count - 1].Y;
                        }
                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p1 = BoundingLocation[BoundingLocation.Count - 1].X;
                        }
                        else
                        {
                            p1 = BoundingLocation[2].Y;
                        }
                    }
                    break;
                case 3:
                    if (ColumnModel.Style.Equals("RECTANGLE") && ColumnModel.b > ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p1 = BoundingLocation[BoundingLocation.Count - 1].X;
                        }
                        else
                        {
                            p1 = BoundingLocation[3].Y;
                        }
                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p1 = BoundingLocation[3].X;
                        }
                        else
                        {
                            p1 = BoundingLocation[BoundingLocation.Count - 1].Y;
                        }
                    }
                    break;
                default:
                    if (ColumnModel.Style.Equals("RECTANGLE") && ColumnModel.b > ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p1 = BoundingLocation[2].X;
                        }
                        else
                        {
                            p1 = BoundingLocation[BoundingLocation.Count - 1].Y;
                        }
                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p1 = BoundingLocation[BoundingLocation.Count - 1].X;
                        }
                        else
                        {
                            p1 = BoundingLocation[2].Y;
                        }
                    }
                    break;
            }
            return p1;
        }
        public double GetP2(FoundationBarModel SelectedFoundationBarModel)
        {
            double p2 = 0;
            switch (SelectedFoundationBarModel.Image)
            {
                case 0:

                    if (ColumnModel.Style.Equals("RECTANGLE") && ColumnModel.b > ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p2 = BoundingLocation[0].X;
                        }
                        else
                        {
                            p2 = BoundingLocation[0].Y;
                        }
                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p2 = BoundingLocation[0].X;
                        }
                        else
                        {
                            p2 = BoundingLocation[0].Y;
                        }
                    }

                    break;
                case 1:
                    if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                    {
                        p2 = BoundingLocation[BoundingLocation.Count - 1].X;
                    }
                    else
                    {
                        p2 = BoundingLocation[1].Y;
                    }

                    break;
                case 2:
                    if (ColumnModel.Style.Equals("RECTANGLE") && ColumnModel.b > ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p2 = BoundingLocation[0].X;
                        }
                        else
                        {
                            p2 = BoundingLocation[1].Y;
                        }
                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p2 = BoundingLocation[1].X;
                        }
                        else
                        {
                            p2 = BoundingLocation[0].Y;
                        }
                    }
                    break;
                case 3:
                    if (ColumnModel.Style.Equals("RECTANGLE") && ColumnModel.b > ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p2 = BoundingLocation[1].X;
                        }
                        else
                        {
                            p2 = BoundingLocation[0].Y;
                        }
                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p2 = BoundingLocation[0].X;
                        }
                        else
                        {
                            p2 = BoundingLocation[1].Y;
                        }
                    }
                    break;
                default:
                    if (ColumnModel.Style.Equals("RECTANGLE") && ColumnModel.b > ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p2 = BoundingLocation[0].X;
                        }
                        else
                        {
                            p2 = BoundingLocation[0].Y;
                        }
                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p2 = BoundingLocation[0].X;
                        }
                        else
                        {
                            p2 = BoundingLocation[0].Y;
                        }
                    }
                    break;
            }
            return p2;
        }
        public double GetP3(FoundationBarModel SelectedFoundationBarModel)
        {
            double p3 = 0;
            switch (SelectedFoundationBarModel.Image)
            {
                case 0:

                    if (ColumnModel.Style.Equals("RECTANGLE") && ColumnModel.b > ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p3 = BoundingLocation[BoundingLocation.Count - 1].Y;
                        }
                        else
                        {
                            p3 = BoundingLocation[2].X;
                        }

                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p3 = BoundingLocation[2].Y;
                        }
                        else
                        {

                            p3 = BoundingLocation[BoundingLocation.Count - 1].X;
                        }
                    }

                    break;
                case 1:
                    if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                    {
                        p3 = BoundingLocation[0].Y;
                    }
                    else
                    {
                        p3 = BoundingLocation[0].X;
                    }
                    break;
                case 2:
                    if (ColumnModel.Style.Equals("RECTANGLE") && ColumnModel.b > ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p3 = BoundingLocation[BoundingLocation.Count - 1].Y;
                        }
                        else
                        {
                            p3 = BoundingLocation[2].X;
                        }

                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p3 = BoundingLocation[2].Y;
                        }
                        else
                        {

                            p3 = BoundingLocation[BoundingLocation.Count - 1].X;
                        }
                    }
                    break;
                case 3:
                    if (ColumnModel.Style.Equals("RECTANGLE") && ColumnModel.b > ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p3 = BoundingLocation[3].Y;

                        }
                        else
                        {
                            p3 = BoundingLocation[BoundingLocation.Count - 1].X;
                        }
                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p3 = BoundingLocation[BoundingLocation.Count - 1].Y;
                        }
                        else
                        {
                            p3 = BoundingLocation[3].X;
                        }
                    }
                    break;
                default:
                    if (ColumnModel.Style.Equals("RECTANGLE") && ColumnModel.b > ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p3 = BoundingLocation[BoundingLocation.Count - 1].Y;
                        }
                        else
                        {
                            p3 = BoundingLocation[2].X;
                        }

                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p3 = BoundingLocation[2].Y;
                        }
                        else
                        {
                            p3 = BoundingLocation[BoundingLocation.Count - 1].X;
                        }
                    }
                    break;
            }
            return p3;
        }
        public double GetP4(FoundationBarModel SelectedFoundationBarModel)
        {
            double p4 = 0;
            switch (SelectedFoundationBarModel.Image)
            {
                case 0:

                    if (ColumnModel.Style.Equals("RECTANGLE") && ColumnModel.b > ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p4 = BoundingLocation[0].Y;
                        }
                        else
                        {
                            p4 = BoundingLocation[0].X;
                        }

                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p4 = BoundingLocation[0].Y;
                        }
                        else
                        {
                            p4 = BoundingLocation[0].X;
                        }
                    }

                    break;
                case 1:
                    if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                    {
                        p4 = BoundingLocation[1].Y;
                    }
                    else
                    {
                        p4 = BoundingLocation[BoundingLocation.Count - 1].X;
                    }
                    break;
                case 2:
                    if (ColumnModel.Style.Equals("RECTANGLE") && ColumnModel.b > ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p4 = BoundingLocation[1].Y;
                        }
                        else
                        {
                            p4 = BoundingLocation[0].X;
                        }

                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p4 = BoundingLocation[0].Y;
                        }
                        else
                        {
                            p4 = BoundingLocation[1].X;
                        }
                    }
                    break;
                case 3:
                    if (ColumnModel.Style.Equals("RECTANGLE") && ColumnModel.b > ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p4 = BoundingLocation[0].Y;
                        }
                        else
                        {
                            p4 = BoundingLocation[1].X;
                        }
                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p4 = BoundingLocation[1].Y;
                        }
                        else
                        {
                            p4 = BoundingLocation[0].X;
                        }
                    }
                    break;
                default:
                    if (ColumnModel.Style.Equals("RECTANGLE") && ColumnModel.b > ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p4 = BoundingLocation[0].Y;
                        }
                        else
                        {
                            p4 = BoundingLocation[0].X;
                        }

                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p4 = BoundingLocation[0].Y;
                        }
                        else
                        {
                            p4 = BoundingLocation[0].X;
                        }
                    }
                    break;
            }
            return p4;
        }
        #endregion
        #region method

        public void SetRebarCover(SettingModel settingModel)
        {
            Parameter coverBottom = Foundation.LookupParameter("Rebar Cover - Bottom Face");
            if (coverBottom != null) coverBottom.Set(settingModel.SelectedBotomCover.Id);
            Parameter coverTop = Foundation.LookupParameter("Rebar Cover - Top Face");
            if (coverTop != null) coverTop.Set(settingModel.SelectedTopCover.Id);
            Parameter coverSide = Foundation.LookupParameter("Rebar Cover - Other Faces");
            if (coverSide != null) coverSide.Set(settingModel.SelectedSideCover.Id);
        }
        public bool IscreatePile()
        {
            for (int i = 0; i < PileModels.Count; i++)
            {
                if (PileModels[i].Pile == null) return false;
            }
            return true;
        }
        #endregion
        #region Piles
        private List<double> GetListDeltaXY(double dp_p, double L2, ObservableCollection<LayerPileModel> layerPileModels)
        {
            List<double> list = new List<double>();
            for (int i = 1; i < layerPileModels.Count; i++)
            {
                if ((layerPileModels[i].NumberPile % 2 == 0 && layerPileModels[i - 1].NumberPile % 2 == 0) || (layerPileModels[i].NumberPile % 2 != 0 && layerPileModels[i - 1].NumberPile % 2 != 0))
                {

                    list.Add(dp_p);
                }
                else
                {
                    list.Add(L2);
                }
            }
            return list;
        }
        private double GetXYCenter(List<double> list)
        {
            if (list.Count == 0)
            {
                return 0;
            }
            else
            {
                if (list.Count == 1)
                {
                    return list[0] / 2;
                }
                else
                {
                    if (list.Count % 2 == 0)
                    {
                        double a = 0;
                        for (int i = 0; i < list.Count / 2; i++)
                        {
                            a += list[i];
                        }
                        return a;
                    }
                    else
                    {
                        double a = 0;
                        for (int i = 0; i < (list.Count - 1) / 2; i++)
                        {
                            a += list[i];
                        }
                        a += list[(list.Count - 1) / 2] / 2;
                        return a;
                    }
                }
            }
        }
        private List<double> GetListXYHorizontal(double dp_p, double L2, ObservableCollection<LayerPileModel> layerPileModels)
        {
            List<double> list = new List<double>();
            List<double> listDelta = GetListDeltaXY(dp_p, L2, layerPileModels);
            double sum = listDelta.Sum();
            double center = GetXYCenter(listDelta);
            int i = 0;
            double y = center;
            list.Add(y);
            while (i < listDelta.Count)
            {
                y -= listDelta[i];
                list.Add(y);
                i++;
            }
            return list;
        }
        private List<double> GetListXYVertical(double dp_p, double L2, ObservableCollection<LayerPileModel> layerPileModels)
        {
            List<double> list = new List<double>();
            List<double> listDelta = GetListDeltaXY(dp_p, L2, layerPileModels);
            double sum = listDelta.Sum();
            double center = GetXYCenter(listDelta);
            int i = 0;
            double y = -center;
            list.Add(y);
            while (i < listDelta.Count)
            {
                y += listDelta[i];
                list.Add(y);
                i++;
            }
            return list;
        }
        private bool GetOdd(ObservableCollection<LayerPileModel> layerPileModels)
        {
            for (int i = 0; i < layerPileModels.Count; i++)
            {
                if (layerPileModels[i].NumberPile % 2 == 0) return false;
            }
            return true;
        }
        private bool GetEvent(ObservableCollection<LayerPileModel> layerPileModels)
        {
            for (int i = 0; i < layerPileModels.Count; i++)
            {
                if (layerPileModels[i].NumberPile % 2 != 0) return false;
            }
            return true;
        }
        public void GetAllPiles(SettingModel settingModel, double L1, double L2, ObservableCollection<LayerPileModel> layerPileModels)
        {
            if (PileModels.Count != 0)
            {
                PileModels.Clear();
            }
            switch (Image)
            {
                case 0: GetAllPiles0(settingModel, L1, L2); break;
                case 1: GetAllPiles1(settingModel, layerPileModels, L1, L2); break;
                case 2: GetAllPiles2(settingModel); break;
                case 3: GetAllPiles3(settingModel); break;
                default: GetAllPiles0(settingModel, L1, L2); break;
            }
            ReNamePiles();
        }

        private void GetAllPiles0(SettingModel settingModel, double L1, double L2)
        {
            double dp_p = settingModel.DistancePP * settingModel.DiameterPile;
            var a1 = new PileModel(1, ColumnModel.BottomLevel, settingModel);
            var a2 = new PileModel(2, ColumnModel.BottomLevel, settingModel);
            var a3 = new PileModel(3, ColumnModel.BottomLevel, settingModel);
            double x1 = 0, y1 = 0, z1 = ColumnModel.PointZPosition;
            double x2 = 0, y2 = 0, z2 = ColumnModel.PointZPosition;
            double x3 = 0, y3 = 0, z3 = ColumnModel.PointZPosition;
            if (ColumnModel.Style.Equals("RECTANGLE"))
            {
                if (ColumnModel.b <= ColumnModel.h)
                {
                    x1 = dp_p * 0.5; y1 = (IsRollBack) ? -L1 : L1;
                    x2 = 0; y2 = (IsRollBack) ? L2 : -L2;
                    x3 = -dp_p * 0.5; y3 = y1;
                }
                else
                {
                    x1 = (IsRollBack) ? L1 : -L1; y1 = dp_p * 0.5;
                    x2 = (IsRollBack) ? -L2 : L2; y2 = 0;
                    x3 = x1; y3 = -dp_p * 0.5;
                }
            }
            else
            {
                x1 = dp_p * 0.5; y1 = (IsRollBack) ? -L1 : L1;
                x2 = 0; y2 = (IsRollBack) ? L2 : -L2;
                x3 = -dp_p * 0.5; y3 = y1;
            }
            a1.GetLocation(x1, y1, z1);
            a2.GetLocation(x2, y2, z2);
            a3.GetLocation(x3, y3, z3);
            PileModels.Add(a1);
            PileModels.Add(a2);
            PileModels.Add(a3);
        }

        private void GetAllPiles2(SettingModel settingModel)
        {
            double angle = 2 * Math.PI / 5;
            double angle0 = 0;
            if (ColumnModel.Style.Equals("RECTANGLE"))
            {
                if (ColumnModel.b <= ColumnModel.h)
                {

                    angle0 = (IsRollBack) ? Math.PI * 1.5 : Math.PI * 0.5;
                }
                else
                {
                    angle0 = (IsRollBack) ? Math.PI * 2 : Math.PI;
                }
            }
            else
            {
                angle0 = (IsRollBack) ? Math.PI * 1.5 : Math.PI * 0.5;
            }
            double radius = settingModel.DistancePP * settingModel.DiameterPile;
            var a1 = new PileModel(1, ColumnModel.BottomLevel, settingModel);
            a1.GetLocation(0, 0, ColumnModel.PointZPosition);
            PileModels.Add(a1);
            for (int i = 0; i < 5; i++)
            {
                double x = Math.Round(Math.Cos(angle0 + (i) * angle) * (radius), 9), y = Math.Round(Math.Sin(angle0 + (i) * angle) * (radius), 9), z = ColumnModel.PointZPosition;
                var a = new PileModel(i + 2, ColumnModel.BottomLevel, settingModel);
                a.GetLocation(x, y, z);
                PileModels.Add(a);
            }
        }
        private void GetAllPiles3(SettingModel settingModel)
        {
            double angle = 2 * Math.PI / 6;
            double angle0 = 0;
            if (ColumnModel.Style.Equals("RECTANGLE"))
            {
                if (ColumnModel.b <= ColumnModel.h)
                {

                    angle0 = 0;
                }
                else
                {
                    angle0 = Math.PI * 0.5;
                }
            }
            else
            {
                angle0 = 0;
            }
            double radius = settingModel.DistancePP * settingModel.DiameterPile;
            var a1 = new PileModel(1, ColumnModel.BottomLevel, settingModel);
            a1.GetLocation(0, 0, ColumnModel.PointZPosition);
            PileModels.Add(a1);
            for (int i = 0; i < 6; i++)
            {
                double x = Math.Round(Math.Cos(angle0 + (i) * angle) * (radius), 9), y = Math.Round(Math.Sin(angle0 + (i) * angle) * (radius), 9), z = ColumnModel.PointZPosition;
                var a = new PileModel(i + 2, ColumnModel.BottomLevel, settingModel);
                a.GetLocation(x, y, z);
                PileModels.Add(a);
            }
        }
        private void GetAllPiles1(SettingModel settingModel, ObservableCollection<LayerPileModel> layerPileModels, double L1, double L2)
        {
            if (ColumnModel.Style.Equals("RECTANGLE"))
            {
                if (ColumnModel.b <= ColumnModel.h)
                {
                    GetAllPiles1Item1(settingModel, layerPileModels, L1, L2);
                }
                else
                {
                    GetAllPiles1Item2(settingModel, layerPileModels, L1, L2);
                }
            }
            else
            {
                GetAllPiles1Item1(settingModel, layerPileModels, L1, L2);
            }
        }

        private void GetAllPiles1Item1(SettingModel settingModel, ObservableCollection<LayerPileModel> layerPileModels, double L1, double L2)
        {
            double dp_p = settingModel.DistancePP * settingModel.DiameterPile;
            if (layerPileModels.Count == 1)
            {
                double y = 0;
                GetAllPiles1Item1OneLayer(dp_p, y, settingModel, layerPileModels[0]);

            }
            else
            {
                if (GetOdd(layerPileModels) || GetEvent(layerPileModels))
                {
                    if (layerPileModels.Count % 2 == 0)
                    {
                        for (int i = 0; i < layerPileModels.Count / 2; i++)
                        {
                            GetAllPiles1Item1OneLayer(dp_p, (layerPileModels.Count / 2 - 1 - i) * dp_p + dp_p * 0.5, settingModel, layerPileModels[i]);
                            GetAllPiles1Item1OneLayer(dp_p, -(layerPileModels.Count / 2 - 1 - i) * dp_p - dp_p * 0.5, settingModel, layerPileModels[i + layerPileModels.Count / 2]);
                        }
                    }
                    else
                    {
                        GetAllPiles1Item1OneLayer(dp_p, 0, settingModel, layerPileModels[(layerPileModels.Count - 1) / 2]);
                        for (int i = 0; i < (layerPileModels.Count - 1) / 2; i++)
                        {
                            GetAllPiles1Item1OneLayer(dp_p, ((layerPileModels.Count - 1) / 2 + i) * dp_p, settingModel, layerPileModels[i]);
                            GetAllPiles1Item1OneLayer(dp_p, -((layerPileModels.Count - 1) / 2 + i) * dp_p, settingModel, layerPileModels[i + (layerPileModels.Count - 1) / 2 + 1]);
                        }
                    }
                }
                else
                {
                    List<double> list = GetListXYHorizontal(dp_p, L2, layerPileModels);
                    for (int i = 0; i < list.Count; i++)
                    {
                        GetAllPiles1Item1OneLayer(2 * L1, list[i], settingModel, layerPileModels[i]);
                    }
                }

            }
        }


        private void GetAllPiles1Item1OneLayer(double x, double y, SettingModel settingModel, LayerPileModel layerPileModel)
        {
            if (layerPileModel.NumberPile == 1)
            {
                var a = new PileModel(1, ColumnModel.BottomLevel, settingModel);
                a.GetLocation(0, y, ColumnModel.PointZPosition);
                PileModels.Add(a);
            }
            else
            {
                if (layerPileModel.NumberPile % 2 == 0)
                {
                    for (int i = 0; i < layerPileModel.NumberPile / 2; i++)
                    {
                        var a1 = new PileModel(1, ColumnModel.BottomLevel, settingModel);
                        var a2 = new PileModel(1, ColumnModel.BottomLevel, settingModel);
                        double x1 = 0, x2 = 0, z = ColumnModel.PointZPosition;
                        x1 = 0 - x * 0.5 - i * x;
                        x2 = 0 + x * 0.5 + i * x;
                        a1.GetLocation(x1, y, z);
                        a2.GetLocation(x2, y, z);
                        PileModels.Add(a1);
                        PileModels.Add(a2);
                    }
                }
                else
                {
                    var a = new PileModel(1, ColumnModel.BottomLevel, settingModel);
                    double z = ColumnModel.PointZPosition;
                    a.GetLocation(0, y, z);
                    PileModels.Add(a);
                    for (int i = 0; i < (layerPileModel.NumberPile - 1) / 2; i++)
                    {
                        var a1 = new PileModel(1, ColumnModel.BottomLevel, settingModel);
                        var a2 = new PileModel(1, ColumnModel.BottomLevel, settingModel);
                        double x1 = 0, x2 = 0;
                        x1 = 0 - (i + 1) * x;
                        x2 = 0 + (i + 1) * x;
                        a1.GetLocation(x1, y, z);
                        a2.GetLocation(x2, y, z);
                        PileModels.Add(a1);
                        PileModels.Add(a2);
                    }
                }
            }

        }
        private void GetAllPiles1Item2(SettingModel settingModel, ObservableCollection<LayerPileModel> layerPileModels, double L1, double L2)
        {
            double dp_p = settingModel.DistancePP * settingModel.DiameterPile;
            if (layerPileModels.Count == 1)
            {
                double x = 0;
                GetAllPiles1Item2OneLayer(x, dp_p, settingModel, layerPileModels[0]);

            }
            else
            {
                if (GetOdd(layerPileModels) || GetEvent(layerPileModels))
                {
                    if (layerPileModels.Count % 2 == 0)
                    {
                        for (int i = 0; i < layerPileModels.Count / 2; i++)
                        {
                            GetAllPiles1Item2OneLayer((layerPileModels.Count / 2 - 1 - i) * dp_p + dp_p * 0.5, dp_p, settingModel, layerPileModels[i]);
                            GetAllPiles1Item2OneLayer(-(layerPileModels.Count / 2 - 1 - i) * dp_p - dp_p * 0.5, dp_p, settingModel, layerPileModels[i + layerPileModels.Count / 2]);
                        }
                    }
                    else
                    {
                        GetAllPiles1Item2OneLayer(0, dp_p, settingModel, layerPileModels[(layerPileModels.Count - 1) / 2]);
                        for (int i = 0; i < (layerPileModels.Count - 1) / 2; i++)
                        {
                            GetAllPiles1Item2OneLayer(((layerPileModels.Count - 1) / 2 + i) * dp_p, dp_p, settingModel, layerPileModels[i]);
                            GetAllPiles1Item2OneLayer(-((layerPileModels.Count - 1) / 2 + i) * dp_p, dp_p, settingModel, layerPileModels[i + (layerPileModels.Count - 1) / 2 + 1]);
                        }
                    }
                }
                else
                {
                    List<double> list = GetListXYVertical(dp_p, L2, layerPileModels);
                    for (int i = 0; i < list.Count; i++)
                    {
                        GetAllPiles1Item2OneLayer(list[i], 2 * L1, settingModel, layerPileModels[i]);
                    }
                }

            }
        }
        private void GetAllPiles1Item2OneLayer(double x, double y, SettingModel settingModel, LayerPileModel layerPileModel)
        {
            if (layerPileModel.NumberPile == 1)
            {
                var a = new PileModel(1, ColumnModel.BottomLevel, settingModel);
                a.GetLocation(x, 0, ColumnModel.PointZPosition);
                PileModels.Add(a);
            }
            else
            {
                if (layerPileModel.NumberPile % 2 == 0)
                {
                    for (int i = 0; i < layerPileModel.NumberPile / 2; i++)
                    {
                        var a1 = new PileModel(1, ColumnModel.BottomLevel, settingModel);
                        var a2 = new PileModel(1, ColumnModel.BottomLevel, settingModel);
                        double y1 = 0, y2 = 0, z = ColumnModel.PointZPosition;
                        y1 = 0 - y * 0.5 - i * y;
                        y2 = 0 + y * 0.5 + i * y;
                        a1.GetLocation(x, y1, z);
                        a2.GetLocation(x, y2, z);
                        PileModels.Add(a1);
                        PileModels.Add(a2);
                    }
                }
                else
                {
                    var a = new PileModel(1, ColumnModel.BottomLevel, settingModel);
                    double z = ColumnModel.PointZPosition;
                    a.GetLocation(x, 0, z);
                    PileModels.Add(a);
                    for (int i = 0; i < (layerPileModel.NumberPile - 1) / 2; i++)
                    {
                        var a1 = new PileModel(1, ColumnModel.BottomLevel, settingModel);
                        var a2 = new PileModel(1, ColumnModel.BottomLevel, settingModel);
                        double y1 = 0, y2 = 0;
                        y1 = 0 - (i + 1) * y;
                        y2 = 0 + (i + 1) * y;
                        a1.GetLocation(x, y1, z);
                        a2.GetLocation(x, y2, z);
                        PileModels.Add(a1);
                        PileModels.Add(a2);
                    }
                }
            }
        }
        private void ReNamePiles()
        {
            if (PileModels.Count != 0)
            {
                PileModels = new ObservableCollection<PileModel>(PileModels.OrderBy(x => x.Location.X).ThenBy(x => x.Location.Y).ToList());
                for (int i = 0; i < PileModels.Count; i++)
                {
                    PileModels[i].PileNumber = i + 1;
                }
            }
        }
        public void ReNameAllPiles(int rule, int number)
        {
            if (PileModels.Count != 0)
            {
                switch (rule)
                {
                    case 0: PileModels = new ObservableCollection<PileModel>(PileModels.OrderBy(x => x.Location.Y).ThenBy(x => x.Location.X).ToList()); break;
                    case 1: PileModels = new ObservableCollection<PileModel>(PileModels.OrderBy(x => x.Location.Y).ThenByDescending(x => x.Location.X)); break;
                    case 2: PileModels = new ObservableCollection<PileModel>(PileModels.OrderByDescending(x => x.Location.Y).ThenBy(x => x.Location.X)); break;
                    case 3: PileModels = new ObservableCollection<PileModel>(PileModels.OrderByDescending(x => x.Location.Y).ThenByDescending(x => x.Location.X)); break;
                    case 4: PileModels = new ObservableCollection<PileModel>(PileModels.OrderBy(x => x.Location.X).ThenBy(x => x.Location.Y)); break;
                    case 5: PileModels = new ObservableCollection<PileModel>(PileModels.OrderBy(x => x.Location.X).ThenByDescending(x => x.Location.Y)); break;
                    case 6: PileModels = new ObservableCollection<PileModel>(PileModels.OrderByDescending(x => x.Location.X).ThenBy(x => x.Location.Y)); break;
                    case 7: PileModels = new ObservableCollection<PileModel>(PileModels.OrderByDescending(x => x.Location.X).ThenByDescending(x => x.Location.Y)); break;
                    default: PileModels = new ObservableCollection<PileModel>(PileModels.OrderBy(x => x.Location.Y).ThenBy(x => x.Location.X)); break;
                }
            }
            for (int i = 0; i < PileModels.Count; i++)
            {
                PileModels[i].PileNumber = i + 1 + number;
            }
        }
        #endregion
        #region Foundation
        public void GetBoundingFoundation(SettingModel settingModel, double L1, double L2, ObservableCollection<LayerPileModel> layerPileModels)
        {
            if (BoundingLocation.Count != 0)
            {
                BoundingLocation.Clear();
            }
            switch (Image)
            {
                case 0: GetBoundingFoundation0(settingModel, L1, L2); break;
                case 1: GetBoundingFoundation1(settingModel, layerPileModels); break;
                case 2: GetBoundingFoundation2(settingModel); break;
                case 3: GetBoundingFoundation3(settingModel); break;
                default: GetBoundingFoundation0(settingModel, L1, L2); break;
            }
        }

        private void GetBoundingFoundation0(SettingModel settingModel, double L1, double L2)
        {
            double dp_p = settingModel.DistancePP * settingModel.DiameterPile;
            double dp_s = settingModel.DistancePS;

            double x1 = 0, y1 = 0, z1 = ColumnModel.PointZPosition;
            double x2 = 0, y2 = 0, z2 = ColumnModel.PointZPosition;
            double x3 = 0, y3 = 0, z3 = ColumnModel.PointZPosition;
            double x4 = 0, y4 = 0, z4 = ColumnModel.PointZPosition;
            double x5 = 0, y5 = 0, z5 = ColumnModel.PointZPosition;
            double x6 = 0, y6 = 0, z6 = ColumnModel.PointZPosition;
            if (ColumnModel.Style.Equals("RECTANGLE"))
            {
                if (ColumnModel.b <= ColumnModel.h)
                {
                    x1 = dp_p * 0.5 + dp_s; y1 = L1 + dp_s;
                    x2 = x1; y2 = L1 - dp_s;
                    x3 = dp_s; y3 = -L2 - dp_s;
                    x4 = -dp_s; y4 = -L2 - dp_s;
                    x5 = -dp_p * 0.5 - dp_s; y5 = L1 - dp_s;
                    x6 = x5; y6 = L1 + dp_s;
                    BoundingLocation.Add(new LocationModel(x1, (IsRollBack) ? -y1 : y1, z1));
                    BoundingLocation.Add(new LocationModel(x2, (IsRollBack) ? -y2 : y2, z2));
                    BoundingLocation.Add(new LocationModel(x3, (IsRollBack) ? -y3 : y3, z3));
                    BoundingLocation.Add(new LocationModel(x4, (IsRollBack) ? -y4 : y4, z4));
                    BoundingLocation.Add(new LocationModel(x5, (IsRollBack) ? -y5 : y5, z5));
                    BoundingLocation.Add(new LocationModel(x6, (IsRollBack) ? -y6 : y6, z6));
                    Width = 2 * settingModel.DistancePS + settingModel.DistancePP * settingModel.DiameterPile;
                    Length = 2 * settingModel.DistancePS + L1 + L2;
                }
                else
                {
                    x1 = -L1 - dp_s; y1 = dp_p * 0.5 + dp_s;
                    x2 = -L1 + dp_s; y2 = y1;
                    x3 = L2 + dp_s; y3 = dp_s;
                    x4 = x3; y4 = -dp_s;
                    x5 = -L1 + dp_s; y5 = -dp_p * 0.5 - dp_s;
                    x6 = x1; y6 = y5;
                    BoundingLocation.Add(new LocationModel((IsRollBack) ? -x1 : x1, y1, z1));
                    BoundingLocation.Add(new LocationModel((IsRollBack) ? -x2 : x2, y2, z2));
                    BoundingLocation.Add(new LocationModel((IsRollBack) ? -x3 : x3, y3, z3));
                    BoundingLocation.Add(new LocationModel((IsRollBack) ? -x4 : x4, y4, z4));
                    BoundingLocation.Add(new LocationModel((IsRollBack) ? -x5 : x5, y5, z5));
                    BoundingLocation.Add(new LocationModel((IsRollBack) ? -x6 : x6, y6, z6));
                    Length = 2 * settingModel.DistancePS + settingModel.DistancePP * settingModel.DiameterPile;
                    Width = 2 * settingModel.DistancePS + L1 + L2;
                }
            }
            else
            {
                x1 = dp_p * 0.5 + dp_s; y1 = L1 + dp_s;
                x2 = x1; y2 = L1 - dp_s;
                x3 = dp_s; y3 = -L2 - dp_s;
                x4 = -dp_s; y4 = -L2 - dp_s;
                x5 = -dp_p * 0.5 - dp_s; y5 = L1 - dp_s;
                x6 = x5; y6 = L1 + dp_s;
                BoundingLocation.Add(new LocationModel(x1, (IsRollBack) ? -y1 : y1, z1));
                BoundingLocation.Add(new LocationModel(x2, (IsRollBack) ? -y2 : y2, z2));
                BoundingLocation.Add(new LocationModel(x3, (IsRollBack) ? -y3 : y3, z3));
                BoundingLocation.Add(new LocationModel(x4, (IsRollBack) ? -y4 : y4, z4));
                BoundingLocation.Add(new LocationModel(x5, (IsRollBack) ? -y5 : y5, z5));
                BoundingLocation.Add(new LocationModel(x6, (IsRollBack) ? -y6 : y6, z6));
                Width = 2 * settingModel.DistancePS + settingModel.DistancePP * settingModel.DiameterPile;
                Length = 2 * settingModel.DistancePS + L1 + L2;
            }

        }


        private void GetBoundingFoundation2(SettingModel settingModel)
        {
            double angle = 2 * Math.PI / 5;
            double angle0 = 0;
            if (ColumnModel.Style.Equals("RECTANGLE"))
            {
                if (ColumnModel.b <= ColumnModel.h)
                {

                    angle0 = (IsRollBack) ? Math.PI * 1.5 : Math.PI * 0.5;
                }
                else
                {
                    angle0 = (IsRollBack) ? Math.PI * 2 : Math.PI;
                }
            }
            else
            {
                angle0 = (IsRollBack) ? Math.PI * 1.5 : Math.PI * 0.5;
            }
            double radius = settingModel.DistancePP * settingModel.DiameterPile + settingModel.DistancePS;
            for (int i = 0; i < 5; i++)
            {
                double x = Math.Round(Math.Cos(angle0 + (i) * angle) * (radius), 9), y = Math.Round(Math.Sin(angle0 + (i) * angle) * (radius), 9), z = ColumnModel.PointZPosition;
                BoundingLocation.Add(new LocationModel(x, y, z));
            }
        }

        private void GetBoundingFoundation3(SettingModel settingModel)
        {

            double angle = 2 * Math.PI / 6;
            double angle0 = 0;
            if (ColumnModel.Style.Equals("RECTANGLE"))
            {
                if (ColumnModel.b <= ColumnModel.h)
                {

                    angle0 = 0;
                }
                else
                {
                    angle0 = Math.PI * 0.5;
                }
            }
            else
            {
                angle0 = 0;
            }
            double radius = settingModel.DistancePP * settingModel.DiameterPile + settingModel.DistancePS;
            for (int i = 0; i < 6; i++)
            {
                double x = Math.Round(Math.Cos(angle0 + (i) * angle) * (radius), 9), y = Math.Round(Math.Sin(angle0 + (i) * angle) * (radius), 9), z = ColumnModel.PointZPosition;
                BoundingLocation.Add(new LocationModel(x, y, z));
            }
        }

        private void GetBoundingFoundation1(SettingModel settingModel, ObservableCollection<LayerPileModel> layerPileModels)
        {
            if (ColumnModel.Style.Equals("RECTANGLE"))
            {
                if (ColumnModel.b <= ColumnModel.h)
                {
                    GetBoundingFoundation1Item1(settingModel, layerPileModels);
                }
                else
                {
                    GetBoundingFoundation1Item2(settingModel, layerPileModels);
                }
            }
            else
            {
                GetBoundingFoundation1Item1(settingModel, layerPileModels);
            }
        }
        private void GetBoundingFoundation1Item1(SettingModel settingModel, ObservableCollection<LayerPileModel> layerPileModels)
        {
            double dp_p = settingModel.DistancePP * settingModel.DiameterPile;
            Width = Math.Abs(PileModels.Max(x => x.Location.X) - PileModels.Min(x => x.Location.X)) + 2 * settingModel.DistancePS;
            Height = Math.Abs(PileModels.Max(x => x.Location.Y) - PileModels.Min(x => x.Location.Y)) + 2 * settingModel.DistancePS;
            double x1 = PileModels.Min(x => x.Location.X) - settingModel.DistancePS, y1 = PileModels.Min(x => x.Location.Y) - settingModel.DistancePS, z = ColumnModel.PointZPosition;
            double x2 = x1, y2 = PileModels.Max(x => x.Location.Y) + settingModel.DistancePS;
            double x3 = PileModels.Max(x => x.Location.X) + settingModel.DistancePS, y3 = y2;
            double x4 = PileModels.Max(x => x.Location.X) + settingModel.DistancePS, y4 = y1;
            BoundingLocation.Add(new LocationModel(x1, y1, z));
            BoundingLocation.Add(new LocationModel(x2, y2, z));
            BoundingLocation.Add(new LocationModel(x3, y3, z));
            BoundingLocation.Add(new LocationModel(x4, y4, z));
        }
        private void GetBoundingFoundation1Item2(SettingModel settingModel, ObservableCollection<LayerPileModel> layerPileModels)
        {
            double dp_p = settingModel.DistancePP * settingModel.DiameterPile;
            Width = Math.Abs(PileModels.Max(x => x.Location.Y) - PileModels.Min(x => x.Location.Y)) + 2 * settingModel.DistancePS;
            Height = Math.Abs(PileModels.Max(x => x.Location.X) - PileModels.Min(x => x.Location.X)) + 2 * settingModel.DistancePS;
            double x1 = PileModels.Min(x => x.Location.X) - settingModel.DistancePS, y1 = PileModels.Min(x => x.Location.Y) - settingModel.DistancePS, z = ColumnModel.PointZPosition;
            double x2 = x1, y2 = PileModels.Max(x => x.Location.Y) + settingModel.DistancePS;
            double x3 = PileModels.Max(x => x.Location.X) + settingModel.DistancePS, y3 = y2;
            double x4 = PileModels.Max(x => x.Location.X) + settingModel.DistancePS, y4 = y1;
            BoundingLocation.Add(new LocationModel(x1, y1, z));
            BoundingLocation.Add(new LocationModel(x2, y2, z));
            BoundingLocation.Add(new LocationModel(x3, y3, z));
            BoundingLocation.Add(new LocationModel(x4, y4, z));
        }



        private int MaxNumberPileOneLayer(ObservableCollection<LayerPileModel> layerPileModels)
        {
            int a = 0;
            for (int i = 0; i < layerPileModels.Count; i++)
            {
                if (a < layerPileModels[i].NumberPile) a = layerPileModels[i].NumberPile;
            }
            return a;
        }
        #endregion
        #region Create

        private Curve GetCurveCylindrical(UnitProject unit, LocationModel l1, LocationModel l2)
        {
            XYZ p1 = ColumnModel.PointPosition + unit.Convert(l1.X) * XYZ.BasisX;
            XYZ p1a = p1 + unit.Convert(l1.Y) * XYZ.BasisY;
            XYZ p2 = ColumnModel.PointPosition + unit.Convert(l2.X) * XYZ.BasisX;
            XYZ p2a = p2 + unit.Convert(l2.Y) * XYZ.BasisY;
            return (Line.CreateBound(p1a, p2a));
        }
        private Curve GetCurveRectangle(UnitProject unit, LocationModel l1, LocationModel l2)
        {

            XYZ p1 = ColumnModel.PointPosition + unit.Convert(l1.X) * ColumnModel.East.FaceNormal;
            XYZ p1a = p1 + unit.Convert(l1.Y) * ColumnModel.Nouth.FaceNormal;
            XYZ p2 = ColumnModel.PointPosition + unit.Convert(l2.X) * ColumnModel.East.FaceNormal;
            XYZ p2a = p2 + unit.Convert(l2.Y) * ColumnModel.Nouth.FaceNormal;
            return (Line.CreateBound(p1a, p2a));
        }

        private void GetCurveArrayItem1(UnitProject unit)
        {
            for (int i = 0; i < BoundingLocation.Count; i++)
            {
                Curve curve = null;
                if (i == 0)
                {
                    curve = GetCurveRectangle(unit, BoundingLocation[BoundingLocation.Count - 1], BoundingLocation[i]);
                }
                else
                {
                    curve = GetCurveRectangle(unit, BoundingLocation[i - 1], BoundingLocation[i]);
                }
                CurveArray.Append(curve);
            }
        }
        private void GetCurveArrayItem2(UnitProject unit)
        {
            for (int i = 0; i < BoundingLocation.Count; i++)
            {
                Curve curve = null;
                if (i == 0)
                {
                    curve = GetCurveCylindrical(unit, BoundingLocation[BoundingLocation.Count - 1], BoundingLocation[i]);
                }
                else
                {
                    curve = GetCurveCylindrical(unit, BoundingLocation[i - 1], BoundingLocation[i]);
                }
                CurveArray.Append(curve);
            }
        }



        private void GetCurveArray(UnitProject unit)
        {
            if (ColumnModel.Style.Equals("RECTANGLE"))
            {
                GetCurveArrayItem1(unit);
            }
            else
            {
                GetCurveArrayItem2(unit);
            }
        }

        private CurveLoop GetCurveLoopFormWork()
        {
            CurveLoop curves = new CurveLoop();
            for (int i = 0; i < CurveArray.Size; i++)
            {
                Curve curve = CurveArray.get_Item(i);
                curves.Append(curve);

            }
            return curves;
        }
        public void CreateFoundation(Document document, UnitProject unit, SettingModel settingModel)
        {
            if (Foundation == null)
            {
                GetCurveArray(unit);

                if (settingModel.SelectedCategoyryFoundation.Equals("Floors"))
                {
                    Foundation = document.Create.NewFloor(CurveArray, settingModel.SelectedFoundationType, ColumnModel.BottomLevel, true, XYZ.BasisZ);

                }
                else
                {
                    Foundation = document.Create.NewFoundationSlab(CurveArray, settingModel.SelectedFoundationType, ColumnModel.BottomLevel, true, XYZ.BasisZ);
                }
                if (Foundation != null)
                {
                    Parameter comments = Foundation.LookupParameter("Comments");
                    if (comments != null) comments.Set(settingModel.FoundationNamePrefix + Type);
                    if ((Image == 2 || Image == 3) && settingModel.WallType != null && IsRepresentative)
                    {
                        double coverSide = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, settingModel.SelectedSideCover.CoverDistance, false));

                        double xLeft = BoundingLocation.Min(x => x.X);
                        double xRight = BoundingLocation.Max(x => x.X);
                        double yTop = BoundingLocation.Max(x => x.Y);
                        double yBottom = BoundingLocation.Min(x => x.Y);
                        Curve curveLeft = GetCurvesHorizontal(xLeft, unit, coverSide);
                        Curve curveRight = GetCurvesHorizontal(xRight, unit, coverSide);
                        Curve curveTop = GetCurvesVertical(yTop, unit, coverSide);
                        Curve curveBottom = GetCurvesVertical(yBottom, unit, coverSide);
                        WallLeft = Wall.Create(document, curveLeft, settingModel.WallType.Id, ColumnModel.BottomLevel.Id, unit.Convert(2 * settingModel.HeightFoundation), -unit.Convert(settingModel.HeightFoundation), true, true);
                        WallRight = Wall.Create(document, curveRight, settingModel.WallType.Id, ColumnModel.BottomLevel.Id, unit.Convert(2 * settingModel.HeightFoundation), -unit.Convert(settingModel.HeightFoundation), true, true);
                        WallTop = Wall.Create(document, curveTop, settingModel.WallType.Id, ColumnModel.BottomLevel.Id, unit.Convert(2 * settingModel.HeightFoundation), -unit.Convert(settingModel.HeightFoundation), true, true);
                        WallBottom = Wall.Create(document, curveBottom, settingModel.WallType.Id, ColumnModel.BottomLevel.Id, unit.Convert(2 * settingModel.HeightFoundation), -unit.Convert(settingModel.HeightFoundation), true, true);

                    }
                }
            }
            for (int i = 0; i < PileModels.Count; i++)
            {
                PileModels[i].CreatePile(document, unit, settingModel, ColumnModel);
                //if (!JoinGeometryUtils.AreElementsJoined(document, PileModels[i].Pile, Foundation))
                //{
                //    JoinGeometryUtils.JoinGeometry(document, PileModels[i].Pile, Foundation );
                //}
            }
            if (settingModel.IsCreateFormWork)
            {
                CurveLoop curves = GetCurveLoopFormWork();

                //FormWorkCurveLoop = CurveLoop.CreateViaOffset(curves, unit.Convert(settingModel.OffsetFormWork*((IsRollBack)?1:-1)), XYZ.BasisZ);
                bool a = ColumnModel.Style.Equals("RECTANGLE") && ColumnModel.b > ColumnModel.h;
                FormWorkCurveLoop = CurveLoop.CreateViaOffset(curves, unit.Convert(((Image == 2 || Image == 3) ? 1 : -1) * settingModel.OffsetFormWork), XYZ.BasisZ);
                if (FormWorkCurveLoop != null)
                {
                    CurveArray array = new CurveArray();
                    foreach (var item in FormWorkCurveLoop)
                    {
                        array.Append(item);
                    }
                    if (array.Size == CurveArray.Size)
                    {
                        if (settingModel.SelectedCategoyryFoundation.Equals("Floors"))
                        {
                            FormWork = document.Create.NewFloor(array, settingModel.SelectedFormWorkType, ColumnModel.BottomLevel, false, XYZ.BasisZ);

                        }
                        else
                        {
                            FormWork = document.Create.NewFoundationSlab(array, settingModel.SelectedFormWorkType, ColumnModel.BottomLevel, false, XYZ.BasisZ);
                        }
                        if (FormWork != null)
                        {
                            FormWork.LookupParameter("Height Offset From Level").Set(-unit.Convert(settingModel.HeightFoundation));
                        }
                    }
                }
            }

        }
        public List<XYZ> GetListXYZCurveArray()
        {
            List<XYZ> list = new List<XYZ>();
            for (int i = 0; i < CurveArray.Size; i++)
            {
                Line line = CurveArray.get_Item(i) as Line;
                if (line != null)
                {
                    XYZ e1 = line.GetEndPoint(0);
                    XYZ e2 = line.GetEndPoint(1);
                    list.Add(e1);
                    list.Add(e2);
                }
            }
            return list;
        }

        #endregion
        #region MinMax
        public double GetMinX()
        {
            return BoundingLocation.Min(x => x.X) + ColumnModel.PointXPosition;
        }
        public double GetMinY()
        {
            return BoundingLocation.Min(x => x.Y) + ColumnModel.PointYPosition;
        }
        public double GetMaxX()
        {
            return BoundingLocation.Max(x => x.X) + ColumnModel.PointXPosition;
        }
        public double GetMaxY()
        {
            return BoundingLocation.Max(x => x.Y) + ColumnModel.PointYPosition;
        }
        #endregion
        #region Dim Foundation


        private ObservableCollection<PlanarFace> GetFoundationPlanarFaceDimVerticalImage0(Document document)
        {
            ObservableCollection<PlanarFace> planarFaces = new ObservableCollection<PlanarFace>();
            Solid a = SolidFace.GetSolidOneElement(Foundation as Element);
            FaceArray faceArray = a.Faces;
            XYZ vector = (ColumnModel.Style.Equals("RECTANGLE")) ? (ColumnModel.Nouth.FaceNormal) : (XYZ.BasisY);
            foreach (var item in faceArray)
            {
                PlanarFace planar = item as PlanarFace;
                if (planar != null)
                {
                    bool con = (PointModel.AreEqual((planar.FaceNormal.AngleTo(vector)), 0)) || (PointModel.AreEqual((planar.FaceNormal.AngleTo(vector)), Math.PI));
                    bool con1 = (PointModel.AreEqual((planar.FaceNormal.AngleTo(XYZ.BasisZ)), Math.PI * 0.5));
                    if (con && con1) planarFaces.Add(planar);
                }
            }
            return planarFaces;
        }
        public Line GetFoundationLineDimVerticalImage0(ViewPlan viewPlan, Document document, UnitProject unit, SettingModel settingModel)
        {

            XYZ p0 = new XYZ(ColumnModel.PointPosition.X, ColumnModel.PointPosition.Y, viewPlan.Origin.Z);
            XYZ p1 = p0 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.East.FaceNormal : XYZ.BasisX) * unit.Convert(BoundingLocation.Min(x => x.X) - settingModel.OffsetDim);
            XYZ p2 = p1 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.Nouth.FaceNormal : XYZ.BasisY) * unit.Convert(BoundingLocation.Max(x => x.Y));
            XYZ p3 = p1 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.Nouth.FaceNormal : XYZ.BasisY) * unit.Convert(BoundingLocation.Min(x => x.Y));
            return Line.CreateBound(p2, p3);
        }
        private ObservableCollection<PlanarFace> GetFoundationPlanarFaceDimHorizontalImage0(Document document)
        {
            ObservableCollection<PlanarFace> planarFaces = new ObservableCollection<PlanarFace>();
            Solid a = SolidFace.GetSolidOneElement(Foundation as Element);
            FaceArray faceArray = a.Faces;
            XYZ vector = (ColumnModel.Style.Equals("RECTANGLE")) ? (ColumnModel.East.FaceNormal) : (XYZ.BasisX);
            foreach (var item in faceArray)
            {
                PlanarFace planar = item as PlanarFace;
                if (planar != null)
                {
                    bool con = (PointModel.AreEqual((planar.FaceNormal.AngleTo(vector)), 0)) || (PointModel.AreEqual((planar.FaceNormal.AngleTo(vector)), Math.PI));
                    bool con1 = (PointModel.AreEqual((planar.FaceNormal.AngleTo(XYZ.BasisZ)), Math.PI * 0.5));
                    if (con && con1) planarFaces.Add(planar);
                }
            }

            return planarFaces;
        }
        public Line GetFoundationLineDimHorizontalImage0(ViewPlan viewPlan, Document document, UnitProject unit, SettingModel settingModel)
        {
            XYZ p0 = new XYZ(ColumnModel.PointPosition.X, ColumnModel.PointPosition.Y, viewPlan.Origin.Z);
            XYZ p1 = p0 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.Nouth.FaceNormal : XYZ.BasisY) * unit.Convert(BoundingLocation.Max(x => x.Y) + settingModel.OffsetDim);
            XYZ p2 = p1 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.East.FaceNormal : XYZ.BasisX) * unit.Convert(BoundingLocation.Max(x => x.X));
            XYZ p3 = p1 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.East.FaceNormal : XYZ.BasisX) * unit.Convert(BoundingLocation.Min(x => x.X));
            return Line.CreateBound(p2, p3);
        }
        public Line GetFoundationLineDimVerticalImage0CallOut(ViewCallOut viewCallOut, Document document, UnitProject unit, SettingModel settingModel)
        {

            XYZ p0 = new XYZ(ColumnModel.PointPosition.X, ColumnModel.PointPosition.Y, viewCallOut.Origin.Z);
            XYZ p1 = p0 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.East.FaceNormal : XYZ.BasisX) * unit.Convert(BoundingLocation.Min(x => x.X) - settingModel.OffsetDim);
            XYZ p2 = p1 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.Nouth.FaceNormal : XYZ.BasisY) * unit.Convert(BoundingLocation.Max(x => x.Y));
            XYZ p3 = p1 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.Nouth.FaceNormal : XYZ.BasisY) * unit.Convert(BoundingLocation.Min(x => x.Y));
            return Line.CreateBound(p2, p3);
        }
        public Line GetFoundationLineDimHorizontalImage0CallOut(ViewCallOut viewCallOut, Document document, UnitProject unit, SettingModel settingModel)
        {
            XYZ p0 = new XYZ(ColumnModel.PointPosition.X, ColumnModel.PointPosition.Y, viewCallOut.Origin.Z);
            XYZ p1 = p0 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.Nouth.FaceNormal : XYZ.BasisY) * unit.Convert(BoundingLocation.Max(x => x.Y) + settingModel.OffsetDim);
            XYZ p2 = p1 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.East.FaceNormal : XYZ.BasisX) * unit.Convert(BoundingLocation.Max(x => x.X));
            XYZ p3 = p1 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.East.FaceNormal : XYZ.BasisX) * unit.Convert(BoundingLocation.Min(x => x.X));
            return Line.CreateBound(p2, p3);
        }

        public Reference GetReferenceFoundation23Top(ViewPlan viewPlan, Document document, UnitProject unit, SettingModel settingModel)
        {
            XYZ p0 = new XYZ(ColumnModel.PointPosition.X, ColumnModel.PointPosition.Y, viewPlan.Origin.Z);
            XYZ p1 = p0 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.East.FaceNormal : XYZ.BasisX) * unit.Convert(BoundingLocation.Min(x => x.X));
            XYZ p2 = p1 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.Nouth.FaceNormal : XYZ.BasisY) * unit.Convert(BoundingLocation.Max(x => x.Y));
            XYZ p3 = p2 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.East.FaceNormal : XYZ.BasisX) * 0.01;
            Line line = Line.CreateBound(p2, p3);
            DetailCurve detailCurve = document.Create.NewDetailCurve(viewPlan, line);
            return detailCurve.GeometryCurve.Reference;
        }
        public Reference GetReferenceFoundation23Bottom(ViewPlan viewPlan, Document document, UnitProject unit, SettingModel settingModel)
        {
            XYZ p0 = new XYZ(ColumnModel.PointPosition.X, ColumnModel.PointPosition.Y, viewPlan.Origin.Z);
            XYZ p1 = p0 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.East.FaceNormal : XYZ.BasisX) * unit.Convert(BoundingLocation.Min(x => x.X));
            XYZ p2 = p1 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.Nouth.FaceNormal : XYZ.BasisY) * unit.Convert(BoundingLocation.Min(x => x.Y));
            XYZ p3 = p2 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.East.FaceNormal : XYZ.BasisX) * 0.01;
            Line line = Line.CreateBound(p2, p3);
            DetailCurve detailCurve = document.Create.NewDetailCurve(viewPlan, line);
            return detailCurve.GeometryCurve.Reference;
        }
        public Reference GetReferenceFoundation23Left(ViewPlan viewPlan, Document document, UnitProject unit, SettingModel settingModel)
        {
            XYZ p0 = new XYZ(ColumnModel.PointPosition.X, ColumnModel.PointPosition.Y, viewPlan.Origin.Z);
            XYZ p1 = p0 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.Nouth.FaceNormal : XYZ.BasisY) * unit.Convert(BoundingLocation.Max(x => x.Y));
            XYZ p2 = p1 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.East.FaceNormal : XYZ.BasisX) * unit.Convert(BoundingLocation.Min(x => x.X));
            XYZ p3 = p2 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.Nouth.FaceNormal : XYZ.BasisY) * 0.01;
            Line line = Line.CreateBound(p2, p3);
            DetailCurve detailCurve = document.Create.NewDetailCurve(viewPlan, line);
            return detailCurve.GeometryCurve.Reference;
        }
        public Reference GetReferenceFoundation23Right(ViewPlan viewPlan, Document document, UnitProject unit, SettingModel settingModel)
        {
            XYZ p0 = new XYZ(ColumnModel.PointPosition.X, ColumnModel.PointPosition.Y, viewPlan.Origin.Z);
            XYZ p1 = p0 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.Nouth.FaceNormal : XYZ.BasisY) * unit.Convert(BoundingLocation.Max(x => x.Y));
            XYZ p2 = p1 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.East.FaceNormal : XYZ.BasisX) * unit.Convert(BoundingLocation.Max(x => x.X));
            XYZ p3 = p2 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.Nouth.FaceNormal : XYZ.BasisY) * 0.01;
            Line line = Line.CreateBound(p2, p3);
            DetailCurve detailCurve = document.Create.NewDetailCurve(viewPlan, line);
            return detailCurve.GeometryCurve.Reference;
        }
        public Reference GetReferenceFoundation23TopCallOut(ViewCallOut viewCallOut, Document document, UnitProject unit, SettingModel settingModel)
        {
            XYZ p0 = new XYZ(ColumnModel.PointPosition.X, ColumnModel.PointPosition.Y, viewCallOut.Origin.Z);
            XYZ p1 = p0 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.East.FaceNormal : XYZ.BasisX) * unit.Convert(BoundingLocation.Min(x => x.X));
            XYZ p2 = p1 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.Nouth.FaceNormal : XYZ.BasisY) * unit.Convert(BoundingLocation.Max(x => x.Y));
            XYZ p3 = p2 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.East.FaceNormal : XYZ.BasisX) * 0.01;
            Line line = Line.CreateBound(p2, p3);
            DetailCurve detailCurve = document.Create.NewDetailCurve(viewCallOut, line);
            return detailCurve.GeometryCurve.Reference;
        }
        public Reference GetReferenceFoundation23BottomCallOut(ViewCallOut viewCallOut, Document document, UnitProject unit, SettingModel settingModel)
        {
            XYZ p0 = new XYZ(ColumnModel.PointPosition.X, ColumnModel.PointPosition.Y, viewCallOut.Origin.Z);
            XYZ p1 = p0 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.East.FaceNormal : XYZ.BasisX) * unit.Convert(BoundingLocation.Min(x => x.X));
            XYZ p2 = p1 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.Nouth.FaceNormal : XYZ.BasisY) * unit.Convert(BoundingLocation.Min(x => x.Y));
            XYZ p3 = p2 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.East.FaceNormal : XYZ.BasisX) * 0.01;
            Line line = Line.CreateBound(p2, p3);
            DetailCurve detailCurve = document.Create.NewDetailCurve(viewCallOut, line);
            return detailCurve.GeometryCurve.Reference;
        }
        public Reference GetReferenceFoundation23LeftCallOut(ViewCallOut viewCallOut, Document document, UnitProject unit, SettingModel settingModel)
        {
            XYZ p0 = new XYZ(ColumnModel.PointPosition.X, ColumnModel.PointPosition.Y, viewCallOut.Origin.Z);
            XYZ p1 = p0 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.Nouth.FaceNormal : XYZ.BasisY) * unit.Convert(BoundingLocation.Max(x => x.Y));
            XYZ p2 = p1 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.East.FaceNormal : XYZ.BasisX) * unit.Convert(BoundingLocation.Min(x => x.X));
            XYZ p3 = p2 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.Nouth.FaceNormal : XYZ.BasisY) * 0.01;
            Line line = Line.CreateBound(p2, p3);
            DetailCurve detailCurve = document.Create.NewDetailCurve(viewCallOut, line);
            return detailCurve.GeometryCurve.Reference;
        }
        public Reference GetReferenceFoundation23RightCallOut(ViewCallOut viewCallOut, Document document, UnitProject unit, SettingModel settingModel)
        {
            XYZ p0 = new XYZ(ColumnModel.PointPosition.X, ColumnModel.PointPosition.Y, viewCallOut.Origin.Z);
            XYZ p1 = p0 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.Nouth.FaceNormal : XYZ.BasisY) * unit.Convert(BoundingLocation.Max(x => x.Y));
            XYZ p2 = p1 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.East.FaceNormal : XYZ.BasisX) * unit.Convert(BoundingLocation.Max(x => x.X));
            XYZ p3 = p2 + ((ColumnModel.Style.Equals("RECTANGLE")) ? ColumnModel.Nouth.FaceNormal : XYZ.BasisY) * 0.01;
            Line line = Line.CreateBound(p2, p3);
            DetailCurve detailCurve = document.Create.NewDetailCurve(viewCallOut, line);
            return detailCurve.GeometryCurve.Reference;
        }

        private Reference ChangeReference(Document document, PlanarFace planarFace)
        {
            string sam = planarFace.Reference.ConvertToStableRepresentation(document);
            string Refer = sam.Replace("SURFACE", "LINEAR");
            return Reference.ParseFromStableRepresentation(document, Refer);
        }
        public ReferenceArray GetReferenceArrayVertical(ViewPlan viewPlan, Document document, UnitProject unit, SettingModel settingModel, int image)
        {
            ReferenceArray referenceArray = new ReferenceArray();
            switch (image)
            {
                case 0:
                    {
                        ObservableCollection<PlanarFace> planarFaces = GetFoundationPlanarFaceDimVerticalImage0(document);
                        for (int i = 0; i < planarFaces.Count; i++)
                        {
                            referenceArray.Append(ChangeReference(document, planarFaces[i]));
                        }
                    }
                    break;
                case 1:
                    {
                        ObservableCollection<PlanarFace> planarFaces = GetFoundationPlanarFaceDimVerticalImage0(document);
                        for (int i = 0; i < planarFaces.Count; i++)
                        {
                            referenceArray.Append(ChangeReference(document, planarFaces[i]));
                        }
                    }
                    break;
                case 2:
                    {
                        referenceArray.Append(GetReferenceFoundation23Top(viewPlan, document, unit, settingModel));
                        referenceArray.Append(GetReferenceFoundation23Bottom(viewPlan, document, unit, settingModel));
                    }
                    break;
                case 3:
                    {
                        referenceArray.Append(GetReferenceFoundation23Top(viewPlan, document, unit, settingModel));
                        referenceArray.Append(GetReferenceFoundation23Bottom(viewPlan, document, unit, settingModel));
                    }
                    break;
                default:
                    {
                        ObservableCollection<PlanarFace> planarFaces = GetFoundationPlanarFaceDimVerticalImage0(document);
                        for (int i = 0; i < planarFaces.Count; i++)
                        {
                            referenceArray.Append(ChangeReference(document, planarFaces[i]));
                        }
                    }
                    break;
            }
            return referenceArray;
        }
        public ReferenceArray GetReferenceArrayHorizontal(ViewPlan viewPlan, Document document, UnitProject unit, SettingModel settingModel, int image)
        {
            ReferenceArray referenceArray = new ReferenceArray();
            switch (image)
            {
                case 0:
                    {
                        ObservableCollection<PlanarFace> planarFaces = GetFoundationPlanarFaceDimHorizontalImage0(document);
                        for (int i = 0; i < planarFaces.Count; i++)
                        {
                            referenceArray.Append(ChangeReference(document, planarFaces[i]));
                        }
                    }
                    break;
                case 1:
                    {
                        ObservableCollection<PlanarFace> planarFaces = GetFoundationPlanarFaceDimHorizontalImage0(document);
                        for (int i = 0; i < planarFaces.Count; i++)
                        {
                            referenceArray.Append(ChangeReference(document, planarFaces[i]));
                        }
                    }
                    break;
                case 2:
                    {
                        referenceArray.Append(GetReferenceFoundation23Left(viewPlan, document, unit, settingModel));
                        referenceArray.Append(GetReferenceFoundation23Right(viewPlan, document, unit, settingModel));
                    }
                    break;
                case 3:
                    {
                        referenceArray.Append(GetReferenceFoundation23Left(viewPlan, document, unit, settingModel));
                        referenceArray.Append(GetReferenceFoundation23Right(viewPlan, document, unit, settingModel));
                    }
                    break;
                default:
                    {
                        ObservableCollection<PlanarFace> planarFaces = GetFoundationPlanarFaceDimHorizontalImage0(document);
                        for (int i = 0; i < planarFaces.Count; i++)
                        {
                            referenceArray.Append(ChangeReference(document, planarFaces[i]));
                        }
                    }
                    break;
            }

            return referenceArray;
        }
        public ReferenceArray GetReferenceArrayVerticalCallOut(ViewCallOut viewCallOut, Document document, UnitProject unit, SettingModel settingModel, int image)
        {
            ReferenceArray referenceArray = new ReferenceArray();
            switch (image)
            {
                case 0:
                    {
                        ObservableCollection<PlanarFace> planarFaces = GetFoundationPlanarFaceDimVerticalImage0(document);
                        for (int i = 0; i < planarFaces.Count; i++)
                        {
                            referenceArray.Append(ChangeReference(document, planarFaces[i]));
                        }
                    }
                    break;
                case 1:
                    {
                        ObservableCollection<PlanarFace> planarFaces = GetFoundationPlanarFaceDimVerticalImage0(document);
                        for (int i = 0; i < planarFaces.Count; i++)
                        {
                            referenceArray.Append(ChangeReference(document, planarFaces[i]));
                        }
                    }
                    break;
                case 2:
                    {
                        referenceArray.Append(GetReferenceFoundation23TopCallOut(viewCallOut, document, unit, settingModel));
                        referenceArray.Append(GetReferenceFoundation23BottomCallOut(viewCallOut, document, unit, settingModel));
                    }
                    break;
                case 3:
                    {
                        referenceArray.Append(GetReferenceFoundation23TopCallOut(viewCallOut, document, unit, settingModel));
                        referenceArray.Append(GetReferenceFoundation23BottomCallOut(viewCallOut, document, unit, settingModel));
                    }
                    break;
                default:
                    {
                        ObservableCollection<PlanarFace> planarFaces = GetFoundationPlanarFaceDimVerticalImage0(document);
                        for (int i = 0; i < planarFaces.Count; i++)
                        {
                            referenceArray.Append(ChangeReference(document, planarFaces[i]));
                        }
                    }
                    break;
            }
            return referenceArray;
        }
        public ReferenceArray GetReferenceArrayHorizontalCallOut(ViewCallOut viewCallOut, Document document, UnitProject unit, SettingModel settingModel, int image)
        {
            ReferenceArray referenceArray = new ReferenceArray();
            switch (image)
            {
                case 0:
                    {
                        ObservableCollection<PlanarFace> planarFaces = GetFoundationPlanarFaceDimHorizontalImage0(document);
                        for (int i = 0; i < planarFaces.Count; i++)
                        {
                            referenceArray.Append(ChangeReference(document, planarFaces[i]));
                        }
                    }
                    break;
                case 1:
                    {
                        ObservableCollection<PlanarFace> planarFaces = GetFoundationPlanarFaceDimHorizontalImage0(document);
                        for (int i = 0; i < planarFaces.Count; i++)
                        {
                            referenceArray.Append(ChangeReference(document, planarFaces[i]));
                        }
                    }
                    break;
                case 2:
                    {
                        referenceArray.Append(GetReferenceFoundation23LeftCallOut(viewCallOut, document, unit, settingModel));
                        referenceArray.Append(GetReferenceFoundation23RightCallOut(viewCallOut, document, unit, settingModel));
                    }
                    break;
                case 3:
                    {
                        referenceArray.Append(GetReferenceFoundation23LeftCallOut(viewCallOut, document, unit, settingModel));
                        referenceArray.Append(GetReferenceFoundation23RightCallOut(viewCallOut, document, unit, settingModel));
                    }
                    break;
                default:
                    {
                        ObservableCollection<PlanarFace> planarFaces = GetFoundationPlanarFaceDimHorizontalImage0(document);
                        for (int i = 0; i < planarFaces.Count; i++)
                        {
                            referenceArray.Append(ChangeReference(document, planarFaces[i]));
                        }
                    }
                    break;
            }

            return referenceArray;
        }
        public ReferenceArray GetReferenceArrayFoundationTopBottomSection(Document document, UnitProject unit, SettingModel settingModel, PlanarFace top, PlanarFace bottom)
        {

            ReferenceArray referenceArray = new ReferenceArray();
            referenceArray.Append(ChangeReference(document, top));
            referenceArray.Append(ChangeReference(document, bottom));
            return referenceArray;
        }
        public Line GetLineDimensionHorizontalTopBottomSection(UnitProject unit, SettingModel settingModel, PlanarFace top, PlanarFace bottom)
        {
            XYZ x = null;
            XYZ y = null;
            if (ColumnModel.Style.Equals("RECTANGLE"))
            {
                x = ColumnModel.East.FaceNormal;
                y = ColumnModel.Nouth.FaceNormal;
            }
            else
            {
                x = XYZ.BasisX;
                y = XYZ.BasisY;
            }
            bool rotate = (ColumnModel.Style.Equals("RECTANGLE")) && (ColumnModel.b > ColumnModel.h);
            double lx = 0, ly = 0;
            switch (Image)
            {
                case 0:
                    lx = (rotate ? ((IsRollBack) ? (BoundingLocation[2].X) : (BoundingLocation[0].X)) : (BoundingLocation[5].X));
                    ly = (rotate) ? (0) : ((BoundingLocation[0].Y + BoundingLocation[0].Y) * 0.5);
                    break;
                case 1:
                    lx = BoundingLocation[0].X; ly = 0;
                    break;
                case 2:
                    lx = (rotate) ? ((IsRollBack) ? (BoundingLocation[2].X) : (BoundingLocation[0].X)) : (BoundingLocation[4].X);
                    ly = (rotate) ? (0) : (BoundingLocation[1].Y);
                    break;
                case 3:
                    lx = (rotate) ? (BoundingLocation[4].X) : (BoundingLocation[3].X);
                    ly = 0;
                    break;
                default:
                    lx = (rotate ? ((IsRollBack) ? (BoundingLocation[2].X) : (BoundingLocation[0].X)) : (BoundingLocation[4].X));
                    ly = (rotate) ? (0) : ((BoundingLocation[0].Y + BoundingLocation[0].Y) * 0.5);
                    break;
            }
            XYZ p0 = ColumnModel.PointPosition + unit.Convert(lx + ((lx > 0) ? 2 : -2) * settingModel.HeightFoundation) * Horizontal.RightDirection + unit.Convert(ly) * y;
            XYZ p1 = PointModel.ProjectToPlane(p0, bottom);
            XYZ p2 = PointModel.ProjectToPlane(p0, top);
            return Line.CreateBound(p1, p2);
        }
        public Line GetLineDimensionVerticalTopBottomSection(UnitProject unit, SettingModel settingModel, PlanarFace top, PlanarFace bottom)
        {
            XYZ x = null;
            XYZ y = null;
            if (ColumnModel.Style.Equals("RECTANGLE"))
            {
                x = ColumnModel.East.FaceNormal;
                y = ColumnModel.Nouth.FaceNormal;
            }
            else
            {
                x = XYZ.BasisX;
                y = XYZ.BasisY;
            }
            bool rotate = (ColumnModel.Style.Equals("RECTANGLE")) && (ColumnModel.b > ColumnModel.h);
            double lx = 0, ly = 0;
            switch (Image)
            {
                case 0:
                    ly = (rotate ? (BoundingLocation[0].Y) : ((IsRollBack) ? (BoundingLocation[2].Y) : (BoundingLocation[0].Y)));
                    lx = (rotate) ? ((BoundingLocation[0].X + BoundingLocation[0].X) * 0.5) : (0);
                    break;
                case 1:
                    ly = BoundingLocation[1].Y; lx = 0;
                    break;
                case 2:
                    lx = (rotate) ? ((IsRollBack) ? (BoundingLocation[2].X) : (BoundingLocation[0].X)) : (BoundingLocation[4].X);
                    lx = (rotate) ? (BoundingLocation[1].X) : (BoundingLocation[1].Y);
                    break;
                case 3:
                    lx = 0;
                    ly = (rotate) ? (BoundingLocation[0].Y) : (BoundingLocation[5].Y);
                    break;
                default:
                    ly = (rotate ? (BoundingLocation[0].Y) : ((IsRollBack) ? (BoundingLocation[2].Y) : (BoundingLocation[0].Y)));
                    lx = (rotate) ? ((BoundingLocation[0].X + BoundingLocation[0].X) * 0.5) : (0);
                    break;
            }
            XYZ p0 = ColumnModel.PointPosition + unit.Convert(lx) * x - unit.Convert(ly + 2 * settingModel.HeightFoundation) * Vertical.RightDirection;
            XYZ p1 = PointModel.ProjectToPlane(p0, bottom);
            XYZ p2 = PointModel.ProjectToPlane(p0, top);
            return Line.CreateBound(p1, p2);
        }
        public void CreateDimensionFoundationHorizontalSection(Document document, UnitProject unit, SettingModel settingModel)
        {
            PlanarFace top = SolidFace.GetTop(Foundation);
            PlanarFace bottom = SolidFace.GetBottom(Foundation);
            ReferenceArray referenceArray = GetReferenceArrayFoundationTopBottomSection(document, unit, settingModel, top, bottom);
            if (referenceArray.Size >= 2)
            {
                Line line = GetLineDimensionHorizontalTopBottomSection(unit, settingModel, top, bottom);
                Dimension dimension = document.Create.NewDimension(Horizontal, line, referenceArray, settingModel.SelectedDimensionType);
            }
        }
        public void CreateDimensionFoundationVerticalSection(Document document, UnitProject unit, SettingModel settingModel)
        {
            PlanarFace top = SolidFace.GetTop(Foundation);
            PlanarFace bottom = SolidFace.GetBottom(Foundation);
            ReferenceArray referenceArray = GetReferenceArrayFoundationTopBottomSection(document, unit, settingModel, top, bottom);
            if (referenceArray.Size >= 2)
            {
                Line line = GetLineDimensionVerticalTopBottomSection(unit, settingModel, top, bottom);
                Dimension dimension = document.Create.NewDimension(Vertical, line, referenceArray, settingModel.SelectedDimensionType);
            }
        }
        #endregion
        #region Tag
        private XYZ GetXYZTagFoundation(ViewPlan viewPlan, Document document, UnitProject unit, SettingModel settingModel)
        {
            XYZ p0 = new XYZ(ColumnModel.PointPosition.X, ColumnModel.PointPosition.Y, viewPlan.Origin.Z);
            return p0 + ((ColumnModel.Style.Equals("RECTANGLE")) ? (ColumnModel.Nouth.FaceNormal) : (XYZ.BasisY)) * unit.Convert(BoundingLocation.Min(x => x.Y) - settingModel.OffsetDim * 0.5);
        }
        public void CreateTagFoundation(ViewPlan viewPlan, Document document, UnitProject unit, SettingModel settingModel, int type)
        {
            XYZ origin = GetXYZTagFoundation(viewPlan, document, unit, settingModel);
            if (settingModel.CheckedText)
            {

                TagFoundation = IndependentTag.Create(document, viewPlan.Id, new Reference(Foundation), false, settingModel.Mode, settingModel.Horizontal, origin);
                TagFoundation.ChangeTypeId(settingModel.SelectedFoundationTag.Id);
            }
            else
            {
                TextTagFoundation = TextNote.Create(document, viewPlan.Id, origin, settingModel.FoundationNamePrefix + type, settingModel.SelectedTextNote.Id);
            }
        }
        #endregion
        #region CallOut
        private XYZ GetOriginCallOutMin(UnitProject unit, SettingModel settingModel)
        {
            double minX = BoundingLocation.Max(x => Math.Abs(x.X));
            double minY = BoundingLocation.Max(x => Math.Abs(x.Y));
            XYZ p1 = ColumnModel.PointPosition + unit.Convert(minY + settingModel.HeightFoundation) * ((ColumnModel.Style.Equals("RECTANGLE")) ? (ColumnModel.South.FaceNormal) : (-XYZ.BasisY));
            return p1 + unit.Convert(minX + settingModel.HeightFoundation) * ((ColumnModel.Style.Equals("RECTANGLE")) ? (ColumnModel.West.FaceNormal) : (-XYZ.BasisX));
        }
        private XYZ GetOriginCallOutMax(UnitProject unit, SettingModel settingModel)
        {
            double maxX = BoundingLocation.Max(x => Math.Abs(x.X));
            double maxY = BoundingLocation.Max(x => Math.Abs(x.Y));
            XYZ p1 = ColumnModel.PointPosition + unit.Convert(maxY + settingModel.HeightFoundation) * ((ColumnModel.Style.Equals("RECTANGLE")) ? (ColumnModel.Nouth.FaceNormal) : (XYZ.BasisY));
            return p1 + unit.Convert(maxX + settingModel.HeightFoundation) * ((ColumnModel.Style.Equals("RECTANGLE")) ? (ColumnModel.East.FaceNormal) : (XYZ.BasisX));
        }
        public void CreateCallOutFoundationDetailView(ViewPlan viewPlan, Document document, UnitProject unit, SettingModel settingModel, int type)
        {
            XYZ oMin = GetOriginCallOutMin(unit, settingModel);
            XYZ oMax = GetOriginCallOutMax(unit, settingModel);
            FoundationDetailView = ViewSection.CreateCallout(document, viewPlan.Id, settingModel.FoundationDetailViewType.Id, oMin, oMax);

            if (FoundationDetailView != null)
            {
                FoundationDetailView.ViewTemplateId = settingModel.SelectedFoundationDetailTemplate.Id;
                try
                {
                    FoundationDetailView.Name = settingModel.FoundationNamePrefix + type.ToString() + " " + LocationName + " Detail";
                }
                catch (Exception)
                {

                    FoundationDetailView.Name += "Copy";
                }
            }
            FoundationDetailView.get_Parameter(BuiltInParameter.VIEWER_CROP_REGION_VISIBLE).Set(0);
        }
        #endregion
        #region Section
        private void GetBoundingBoxHorizontal(UnitProject unit, SettingModel settingModel, int image)
        {
            XYZ origin = null;
            switch (image)
            {
                case 0:

                    if (ColumnModel.Style.Equals("RECTANGLE") && ColumnModel.b > ColumnModel.h)
                    {
                        origin = ColumnModel.PointPosition + ColumnModel.East.FaceNormal * unit.Convert((BoundingLocation[0].X + BoundingLocation[1].X) * 0.5);
                    }
                    else
                    {
                        origin = ColumnModel.PointPosition;
                    }
                    break;
                case 1: origin = ColumnModel.PointPosition; break;
                case 2:

                    if (ColumnModel.Style.Equals("RECTANGLE") && ColumnModel.b > ColumnModel.h)
                    {
                        origin = ColumnModel.PointPosition + ColumnModel.East.FaceNormal * unit.Convert((BoundingLocation[1].X));
                    }
                    else
                    {
                        origin = ColumnModel.PointPosition;
                    }
                    break;
                case 3: origin = ColumnModel.PointPosition; break;
                default:
                    if (ColumnModel.Style.Equals("RECTANGLE") && ColumnModel.b > ColumnModel.h)
                    {
                        origin = ColumnModel.PointPosition + ColumnModel.East.FaceNormal * unit.Convert((BoundingLocation[0].X + BoundingLocation[1].X) * 0.5);
                    }
                    else
                    {
                        origin = ColumnModel.PointPosition;
                    }
                    break;
            }
            XYZ baseX = ((ColumnModel.Style.Equals("RECTANGLE")) ? (ColumnModel.Nouth.FaceNormal) : (XYZ.BasisY));
            XYZ baseY = XYZ.BasisZ;
            XYZ baseZ = ((ColumnModel.Style.Equals("RECTANGLE")) ? (ColumnModel.East.FaceNormal) : (XYZ.BasisX));
            Transform t = Transform.Identity;
            t.Origin = origin;
            t.BasisX = baseX;
            t.BasisY = baseY;
            t.BasisZ = baseZ;
            SectionBoxHorizontal = new BoundingBoxXYZ();
            SectionBoxHorizontal.Transform = t;
            double maxY = BoundingLocation.Max(x => Math.Abs(x.Y));
            XYZ max = new XYZ(unit.Convert(maxY + settingModel.HeightFoundation), unit.Convert(settingModel.HeightFoundation), unit.Convert(settingModel.HeightFoundation));
            XYZ min = new XYZ(-unit.Convert(maxY + settingModel.HeightFoundation), -unit.Convert(settingModel.HeightFoundation * 2), 0);
            SectionBoxHorizontal.Max = max;
            SectionBoxHorizontal.Min = min;
        }

        private void GetBoundingBoxVertical(UnitProject unit, SettingModel settingModel, int image)
        {
            XYZ origin = null;
            switch (image)
            {
                case 0:
                    if (ColumnModel.Style.Equals("RECTANGLE") && ColumnModel.b > ColumnModel.h)
                    {
                        origin = ColumnModel.PointPosition;
                    }
                    else
                    {
                        origin = ColumnModel.PointPosition + ((ColumnModel.Style.Equals("RECTANGLE")) ? (ColumnModel.Nouth.FaceNormal) : (XYZ.BasisY)) * unit.Convert((BoundingLocation[0].Y + BoundingLocation[1].Y) * 0.5);
                    }

                    break;
                case 1: origin = ColumnModel.PointPosition; break;
                case 2:
                    if (ColumnModel.Style.Equals("RECTANGLE") && ColumnModel.b > ColumnModel.h)
                    {
                        origin = ColumnModel.PointPosition;
                    }
                    else
                    {
                        origin = ColumnModel.PointPosition + ((ColumnModel.Style.Equals("RECTANGLE")) ? (ColumnModel.Nouth.FaceNormal) : (XYZ.BasisY)) * unit.Convert(BoundingLocation[1].Y);
                    }

                    break;
                case 3: origin = ColumnModel.PointPosition; break;
                default:
                    origin = ColumnModel.PointPosition + ((ColumnModel.Style.Equals("RECTANGLE")) ? (ColumnModel.Nouth.FaceNormal) : (XYZ.BasisY)) * unit.Convert((BoundingLocation[0].Y + BoundingLocation[1].Y) * 0.5);
                    break;
            }
            XYZ baseX = ((ColumnModel.Style.Equals("RECTANGLE")) ? (ColumnModel.West.FaceNormal) : (-XYZ.BasisX));
            XYZ baseY = XYZ.BasisZ;
            XYZ baseZ = ((ColumnModel.Style.Equals("RECTANGLE")) ? (ColumnModel.Nouth.FaceNormal) : (XYZ.BasisY));
            Transform t = Transform.Identity;
            t.Origin = origin;
            t.BasisX = baseX;
            t.BasisY = baseY;
            t.BasisZ = baseZ;
            SectionBoxVertical = new BoundingBoxXYZ();
            SectionBoxVertical.Transform = t;
            double maxY = BoundingLocation.Max(x => Math.Abs(x.X));
            XYZ max = new XYZ(unit.Convert(maxY + settingModel.HeightFoundation), unit.Convert(settingModel.HeightFoundation), unit.Convert(settingModel.HeightFoundation));
            XYZ min = new XYZ(-unit.Convert(maxY + settingModel.HeightFoundation), -unit.Convert(settingModel.HeightFoundation * 2), 0);
            SectionBoxVertical.Max = max;
            SectionBoxVertical.Min = min;
        }
        public void CreateFoundationSectionHorizontal(Document document, UnitProject unit, SettingModel settingModel, int image)
        {
            GetBoundingBoxHorizontal(unit, settingModel, image);
            Horizontal = ViewSection.CreateSection(document, settingModel.FoundationSectionType.Id, SectionBoxHorizontal);
        }
        public void CreateFoundationSectionVertical(Document document, UnitProject unit, SettingModel settingModel, int image)
        {
            GetBoundingBoxVertical(unit, settingModel, image);
            Vertical = ViewSection.CreateSection(document, settingModel.FoundationSectionType.Id, SectionBoxVertical);
        }
        #endregion
    }
}
