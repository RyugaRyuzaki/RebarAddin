using System;
using WpfCustomControls;
namespace R11_FoundationPile
{
    public class BarModel : BaseViewModel
    {
        
        private string _Name;
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }
        private RebarBarModel _Bar;
        public RebarBarModel Bar { get => _Bar; set { _Bar = value; OnPropertyChanged(); } }
        private double _HookLength;
        public double HookLength { get => _HookLength; set { _HookLength = value; OnPropertyChanged(); } }
        private double _Distance;
        public double Distance { get => _Distance; set { _Distance = value; OnPropertyChanged(); } }
        private int _Number;
        public int Number { get => _Number; set { _Number = value; OnPropertyChanged(); } }
        private bool _IsModel;
        public bool IsModel { get => _IsModel; set { _IsModel = value; OnPropertyChanged(); } }
        public BarModel(string name,RebarBarModel rebarBarModel,double hookLength,double distance,int number,bool isModel)
        {
            Name = name;
            Bar = rebarBarModel;
            HookLength = hookLength;
            Distance = distance;
            Number = number;
            IsModel = isModel;
        }
        public void FixNumber(double p1, double p2, double coverSide)
        {
            Number = (int)((Math.Abs(p1 - p2) - 2 * coverSide - Bar.Diameter) / Distance) + 1;
            Distance = Math.Round((Math.Abs(p1 - p2) - 2 * coverSide - Bar.Diameter)/(Number-1), 3);
        }
      
    }
}
