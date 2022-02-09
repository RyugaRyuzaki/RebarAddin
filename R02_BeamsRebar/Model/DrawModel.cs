using WpfCustomControls;
using System;
using System.Collections.Generic;
using System.Windows.Media;
namespace R02_BeamsRebar
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
        public void GetScale(List<InfoModel> InfoModels,List<NodeModel> NodeModels,double height, double width)
        {
            double hmax = InfoModels[0].h;
            double lenght = 0;
            for (int i = 0; i < InfoModels.Count; i++)
            {
                lenght += InfoModels[i].Length;
                if (hmax < InfoModels[i].h)
                {
                    hmax = InfoModels[i].h;
                }
            }
            for (int i = 0; i < NodeModels.Count; i++)
            {
                lenght += NodeModels[i].Width;
            }

            double scale1 = (hmax > height - 2 * Top) ? hmax / (height - 2 * Top) : (hmax < height/10) ? hmax / (height - 2 * Top) : 1;

            double scale2 = (lenght > width - 2 * Left) ? lenght / (width - 2 * Left) : (lenght < width/10) ? lenght / (width - 2 * Left) : 1;
            Scale = Math.Max(scale1, scale2);
            Width = lenght / Scale + 2 * Left;
            Height = hmax / Scale + 2 * Top;
        }
    }
}
