using WpfCustomControls;

namespace R02_BeamsRebar
{
    public class DistributeStirrup : BaseViewModel
    {
        #region property
        private int _Type;
        public int Type { get => _Type; set { _Type = value; OnPropertyChanged();} }
        private double _S1;
        public double S1 { get => _S1; set { _S1 = value; OnPropertyChanged(); } }
        private double _S2;
        public double S2 { get => _S2; set { _S2 = value; OnPropertyChanged(); } }
        private double _L1;
        public double L1 { get => _L1; set { _L1 = value; OnPropertyChanged(); } }
        private double _L2;
        public double L2 { get => _L2; set { _L2 = value; OnPropertyChanged(); } }
        private double _S;
        public double S { get => _S; set { _S = value; OnPropertyChanged(); } }
        #endregion
        public DistributeStirrup(int type, double s1, double s2,  double s)
        {
            Type = type;
            S1 = s1;
            S2 = s2;
            S = s;
        }
        public void GetL1L2(double length)
        {
            if (Type==0)
            {
                L1 = 0;L2 = 0;S1 = 0;S2 = 0;
            }
            else
            {
                L1 = length / 4;
                L2 = length / 2;
                S = 0;
            }
        }
    }
}
