using WpfCustomControls;
using Autodesk.Revit.DB;
using System;
namespace R02_BeamsRebar
{
    public class NodeModel :BaseViewModel
    {
        private int _NumberNode;
        public int NumberNode { get=>_NumberNode; set { _NumberNode = value;OnPropertyChanged(); } }
        private double _Start;
        public double Start { get => _Start; set { _Start = value; OnPropertyChanged(); } }
        private double _Mid;
        public double Mid { get => _Mid; set { _Mid = value; OnPropertyChanged(); } }
        private double _End;
        public double End { get => _End; set { _End = value; OnPropertyChanged(); } }
        private double _Width;
        public double Width { get => _Width; set { _Width = value; OnPropertyChanged(); } }
        private double _HLeft;
        public double HLeft { get => _HLeft; set { _HLeft = value; OnPropertyChanged(); } }
        private double _HRight;
        public double HRight { get => _HRight; set { _HRight = value; OnPropertyChanged(); } }
        private double _ZLeft;
        public double ZLeft { get => _ZLeft; set { _ZLeft = value; OnPropertyChanged(); } }
        private double _ZRight;
        public double ZRight { get => _ZRight; set { _ZRight = value; OnPropertyChanged(); } }
        private PlanarFace _StartPlanar;
        public PlanarFace StartPlanar { get => _StartPlanar; set { _StartPlanar = value; OnPropertyChanged(); } }
        private PlanarFace _EndPlanar;
        public PlanarFace EndPlanar { get => _EndPlanar; set { _EndPlanar = value; OnPropertyChanged(); } }
        public NodeModel(int numberNode,double start, double end,double hLeft, double hRight, double zLeft, double zRight, PlanarFace startPlanar, PlanarFace endPlanar)
        {
            NumberNode = numberNode;
            Start = start;
            End = end;
            HLeft = hLeft;
            HRight = hRight;
            ZLeft = Math.Abs(zLeft);
            ZRight = Math.Abs(zRight);
            Mid = (Start + End) / 2;
            Width = Math.Abs(Start - End);
            StartPlanar = startPlanar;
            EndPlanar = endPlanar;
        }
    }
}
