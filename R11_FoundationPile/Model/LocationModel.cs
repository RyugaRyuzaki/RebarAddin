using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R11_FoundationPile
{
    public class LocationModel  :BaseViewModel
    {
        private double _Y;
        public double Y { get => _Y; set { _Y = value; OnPropertyChanged(); } }
        private double _X;
        public double X { get => _X; set { _X = value; OnPropertyChanged(); } }
        private double _Z;
        public double Z { get => _Z; set { _Z = value; OnPropertyChanged(); } }
        public LocationModel(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

    }
}
