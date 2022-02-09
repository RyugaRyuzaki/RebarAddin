using WpfCustomControls;

namespace R02_BeamsRebar
{
    public class LocationBarModel:BaseViewModel
    {
        private double _Y;
        public double Y { get => _Y; set { _Y = value; OnPropertyChanged(); } }
        private double _X;
        public double X { get => _X; set { _X = value; OnPropertyChanged(); } }
        public LocationBarModel( double x, double y)
        {
            X = x;
            Y = y;
        }

    }
}
