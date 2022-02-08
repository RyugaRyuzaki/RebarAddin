
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using static R01_ColumnsRebar.ErrorColumns;
using WpfCustomControls;
namespace R01_ColumnsRebar
{
    public class DrawModel :BaseViewModel
    {
        public DrawModel(double scale, double left, double top, double offset, double extend)
        {
            Scale = scale;
            Left = left;
            Top = top;
            Offset = offset;
            Extend = extend;
        }
        private double _Scale;
        public double Scale { get=>_Scale; set { _Scale = value;OnPropertyChanged(); } }
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
        public  SolidColorBrush ColorWhite = Brushes.Transparent;

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
        public void GetScale(SectionStyle sectionStyle, ObservableCollection<InfoModel> InfoModels, UnitProject unit)
        {
            double maxWidth = 0;
            double maxHeight = GetHeightMax(InfoModels)+2*80;
            double scaleWidth = 1;
            double scaleHeight = 1;
            if (sectionStyle == SectionStyle.RECTANGLE)
            {
                maxWidth = GetBmax(InfoModels) +GetHmax(InfoModels);
               
            }
            else
            {
                maxWidth =2* GetDmax(InfoModels);
            }
            if (maxWidth > 200)
            {
                scaleWidth = maxWidth / 200;

            }
            else
            {
                if (maxWidth < unit.Convert(200))
                {
                    scaleWidth = maxWidth / 200;
                }
                else
                {
                    scaleWidth = 1;
                }
            }
            if (maxHeight>4640)
            {
                scaleHeight = maxHeight / 4640;
                
            }
            else
            {
               
                if (maxHeight<unit.Convert(4640))
                {
                    scaleHeight=maxHeight / 4640;
                }
                else
                {
                    scaleHeight = 1;
                }
            }
            Scale = Math.Max(scaleHeight, scaleWidth);
            Width = 600;
            Height = (maxHeight / Scale + 160<850)?850: maxHeight / Scale + 160;
            Top = Height - 80;

        }
        public void GetScaleSection(SectionStyle sectionStyle,ObservableCollection<InfoModel> InfoModels,UnitProject unit)
        {
            double max = 0;
            if (sectionStyle==SectionStyle.RECTANGLE)
            {
                max = Math.Max(GetBmax(InfoModels), GetHmax(InfoModels));
                
            }
            else
            {
                max = GetDmax(InfoModels);
            }
            if (max > 270)
            {
                Scale = max / 270;
            }
            else
            {
                if (max<unit.Convert(270))
                {
                    Scale = max / 270;
                }
                else
                {
                    Scale = 1;
                }
            }
        }
        public void GetScaleDowels(SectionStyle sectionStyle, ObservableCollection<InfoModel> InfoModels, UnitProject unit)
        {
            double max = 0;
            if (sectionStyle == SectionStyle.RECTANGLE)
            {
                max = Math.Max(GetBmax(InfoModels), GetHmax(InfoModels));

            }
            else
            {
                max = GetDmax(InfoModels);
            }
            if (max > 190)
            {
                Scale = max / 190;
            }
            else
            {
                if (max < unit.Convert(190))
                {
                    Scale = max / 190;
                }
                else
                {
                    Scale = 1;
                }
            }
        }
        public double GetHeightMax(ObservableCollection<InfoModel> InfoModels)
        {

            return InfoModels[InfoModels.Count - 1].TopPosition;
        }
        public double GetHmax(ObservableCollection<InfoModel> InfoModels)
        {
            double a = 0;
            for (int i = 0; i < InfoModels.Count; i++)
            {
                if (a < InfoModels[i].h) a = InfoModels[i].h;
                
            }
            return a;
        }
        public double GetBmax(ObservableCollection<InfoModel> InfoModels)
        {
            double a = 0;
            for (int i = 0; i < InfoModels.Count; i++)
            {
                if (a < InfoModels[i].b) a = InfoModels[i].b;

            }
            return a;
        }
        public double GetDmax(ObservableCollection<InfoModel> InfoModels)
        {
            double a = 0;
            for (int i = 0; i < InfoModels.Count; i++)
            {
                if (a < InfoModels[i].D) a = InfoModels[i].D;

            }
            return a;
        }
    }
}
