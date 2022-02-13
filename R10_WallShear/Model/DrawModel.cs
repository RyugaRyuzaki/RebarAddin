
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using DSP;
namespace R10_WallShear
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
        public void GetScale(ObservableCollection<InfoModel> InfoModels, UnitProject unit, RebarBarModel rebarBarModel)
        {
            double maxHeight = GetHeightMax(InfoModels);

            Scale = GetScaleSection(InfoModels, unit);
            Width = 630;
            Height = ((maxHeight+rebarBarModel.Diameter*40) / Scale + 80 < 560) ? 560 : (maxHeight + rebarBarModel.Diameter * 40) / Scale + 80;
            Top = Height - 40;

        }
        public double GetScaleSection(ObservableCollection<InfoModel> InfoModels, UnitProject unit)
        {
            double maxB = GetBmax(InfoModels);
            double maxH = GetHmax(InfoModels);
            double scaleB = 1;
            double scaleH = 1;
            if (maxB > 160 || maxB < unit.Convert(160))
            {
                scaleB = maxB / 160;
            }
            else
            {
                scaleB = 1;
            }
            if (maxH > 440 || maxH < unit.Convert(440))
            {
                scaleH = maxH / 440;
            }
            else
            {
                scaleH = 1;
            }
            return Math.Max(scaleB,scaleH);
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
                if (a < InfoModels[i].L) a = InfoModels[i].L;

            }
            return a;
        }
        public double GetBmax(ObservableCollection<InfoModel> InfoModels)
        {
            double a = 0;
            for (int i = 0; i < InfoModels.Count; i++)
            {
                if (a < InfoModels[i].T) a = InfoModels[i].T;

            }
            return a;
        }
    }
}
