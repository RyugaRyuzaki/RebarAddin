
using Autodesk.Revit.DB;
using System;
using System.Linq;
using System.Windows.Media;
using static R11_FoundationPile.ErrorColumns;
using WpfCustomControls;

using DSP;

namespace R11_FoundationPile
{
    public class DrawModel : BaseViewModel
    {
        public DrawModel(double scale, double left, double top, double offset, double extend)
        {
            Scale = scale;
            Left = left;
            Top = top;
            Offset = offset;
            Extend = extend;
            Side = 60;
        }
        private double _Scale;
        public double Scale { get => _Scale; set { _Scale = value; OnPropertyChanged(); } }
        private double _Left;
        public double Left { get => _Left; set { _Left = value; OnPropertyChanged(); } }
        private double _Top;
        public double Top { get => _Top; set { _Top = value; OnPropertyChanged(); } }
        private double _Offset;
        public double Offset { get => _Offset; set { _Offset = value; OnPropertyChanged(); } }
        private double _Extend;
        public double Extend { get => _Extend; set { _Extend = value; OnPropertyChanged(); } }
        private double _Width;
        public double Width { get => _Width; set { _Width = value; OnPropertyChanged(); } }
        private double _Height;
        public double Height { get => _Height; set { _Height = value; OnPropertyChanged(); } }
        private double _Side;
        public double Side { get => _Side; set { _Side = value; OnPropertyChanged(); } }
        public SolidColorBrush ColorWhite = Brushes.Transparent;

        public SolidColorBrush ColorFill = Brushes.Gainsboro;
        public SolidColorBrush ColorMainBar = Brushes.Black;
        public SolidColorBrush ColorMainBarChoose = Brushes.Red;
        public SolidColorBrush ColorStirrup = Brushes.Black;
        public SolidColorBrush ColorStirrupChoose = Brushes.Blue;
        public SolidColorBrush ColorBound = Brushes.Black;
        public SolidColorBrush ColorBoundChoose = Brushes.Blue;
        public SolidColorBrush ColorNode = Brushes.Black;
        public SolidColorBrush ColorNodeChoose = Brushes.Blue;
        public SolidColorBrush ColorTag = Brushes.Chocolate;
        public double StrokeMain = 2;
        public double StrokeStirrup = 1.2;
        public double StrokeBound = 0.8;
        public double StrokeDim = 0.5;
        public void GetScale( FoundationModel foundationModel, UnitProject unit)
        {
            double width =2*(Math.Max((Math.Abs(foundationModel.BoundingLocation.Max(x=>x.X))),(Math.Abs(foundationModel.BoundingLocation.Min(x => x.X)))));
            double height = 2 * (Math.Max((Math.Abs(foundationModel.BoundingLocation.Max(x => x.Y))), (Math.Abs(foundationModel.BoundingLocation.Min(x => x.Y)))));
            double scaleWidth = 1;
            double scaleHeight = 1;
            if (width>820-2*Side)
            {
                scaleWidth = width / (820 - 2 * Side);
            }
            else
            {
                if (width < unit.Convert(820 - 2 * Side))
                {
                    scaleWidth = width / (820 - 2 * Side);
                }
                else
                {
                    scaleWidth = 1;
                }
            }
            if (height > 820 - 2 * Side)
            {
                scaleHeight = height / (820 - 2 * Side);
            }
            else
            {
                if (height < unit.Convert(820 - 2 * Side))
                {
                    scaleHeight = height / (820 - 2 * Side);
                }
                else
                {
                    scaleHeight = 1;
                }
            }
            Scale = Math.Max(scaleWidth, scaleHeight);
        }
        public void GetScaleHeigth(SettingModel settingModel, UnitProject unit)
        {
            double scaleH = 0;
            if (settingModel.HeightFoundation>410-2*Side)
            {
                scaleH = settingModel.HeightFoundation / (410 - 2 * Side);
            }
            else
            {
                if (settingModel.HeightFoundation < unit.Convert(410 - 2 * Side))
                {
                     scaleH = settingModel.HeightFoundation / (410 - 2 * Side);
                }
                else
                {
                    scaleH = 1;
                }
            }
            Scale = Math.Max(Scale, scaleH);
        }
        public void GetScaleBar(FoundationModel foundationModel, UnitProject unit)
        {
            Side = 30;
            double height = 2 * (Math.Max((Math.Abs(foundationModel.BoundingLocation.Max(x => x.Y))), (Math.Abs(foundationModel.BoundingLocation.Min(x => x.Y)))));
           
            if (height > 200 - 2 * Side)
            {
                Scale = height / (200 - 2 * Side);
            }
            else
            {
                if (height < unit.Convert(200 - 2 * Side))
                {
                    Scale = height / (200 - 2 * Side);
                }
                else
                {
                    Scale = 1;
                }
            }
        }
        public void GetScaleSection(FoundationModel foundationModel, UnitProject unit)
        {
            Side = 15;
            double width = 2 * (Math.Max((Math.Abs(foundationModel.BoundingLocation.Max(x => x.X))), (Math.Abs(foundationModel.BoundingLocation.Min(x => x.X)))));
            double height = 2 * (Math.Max((Math.Abs(foundationModel.BoundingLocation.Max(x => x.Y))), (Math.Abs(foundationModel.BoundingLocation.Min(x => x.Y)))));
            double scaleWidth = 1;
            double scaleHeight = 1;
            if (width > 350 - 2 * Side)
            {
                scaleWidth = width / (350 - 2 * Side);
            }
            else
            {
                if (width < unit.Convert(350 - 2 * Side))
                {
                    scaleWidth = width / (350 - 2 * Side);
                }
                else
                {
                    scaleWidth = 1;
                }
            }
            if (height > 350 - 2 * Side)
            {
                scaleHeight = height / (350 - 2 * Side);
            }
            else
            {
                if (height < unit.Convert(350 - 2 * Side))
                {
                    scaleHeight = height / (350 - 2 * Side);
                }
                else
                {
                    scaleHeight = 1;
                }
            }
            Scale = Math.Max(scaleWidth, scaleHeight);
        }
        public void GetScalePileDetail(FoundationPileDetail foundationPileDetail,Document document, UnitProject unit)
        {
            double maxY=double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, foundationPileDetail.FoundationBox.Max.Y, false));
            double minY=double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, foundationPileDetail.FoundationBox.Min.Y, false));

            double height = maxY - minY;
          
            if (height > 820 )
            {
                Scale = height / (820);
            }
            else
            {
                if (height < unit.Convert(820 ))
                {
                    Scale = height / (820);
                }
                else
                {
                    Scale = 1;
                }
            }
        }
    }
}
