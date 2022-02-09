
using WpfCustomControls;
using System;

namespace R02_BeamsRebar
{
    public class SectionModel :BaseViewModel
    {
        private string _Detail;
        public string Detail { get => _Detail; set { _Detail = value; OnPropertyChanged(); } }
        private double _Area;
        public double Area { get => _Area; set { _Area = value; OnPropertyChanged(); } }
        private RebarBarModel _Bar;
        public RebarBarModel Bar { get => _Bar; set { _Bar = value; OnPropertyChanged(); } }
        private int _NumberBar;
        public int NumberBar { get => _NumberBar; set { _NumberBar = value; OnPropertyChanged(); } }
        private double _Y0;
        public double Y0 { get => _Y0; set { _Y0 = value; OnPropertyChanged(); } }
        private const string phi = "Ø";
        public SectionModel(RebarBarModel bar, int numberBar, double y0)
        {
            Bar = bar;NumberBar = numberBar;Y0 = y0;
            GetDetail();
        }
        public void GetDetail()
        {
            Detail = NumberBar+ Bar.Type + Bar.Diameter;
            Area = Math.Round(NumberBar * Math.PI * Bar.Diameter * Bar.Diameter,1);
        }
    }
}
