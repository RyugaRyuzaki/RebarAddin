using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using WpfCustomControls.CustomControls;
namespace WpfCustomControls.Model
{
    public class ProgressModel :BaseViewModel
    {
        private int _Value;
        public int Value { get => _Value; set { _Value = value; OnPropertyChanged(); } }
        private double _Percent;
        public double Percent { get => _Percent; set { _Percent = value; OnPropertyChanged(); } }
        private double _Maximum;
        public double Maximum { get => _Maximum; set { _Maximum = value; OnPropertyChanged(); } }
        public ProgressModel(int value,double percent)
        {
            Value = value;Percent = percent;
        }
        
        public void SetValue(ProgressBar p,  int n)
        {
            Value += n;
            Percent = (Value / p.Maximum) * 100;
            p.Dispatcher.Invoke(() => p.Value = Value,
                DispatcherPriority.Background);
        }
        public void ResetValue(ProgressBar p)
        {
            Value = 0; ;
            Percent = 0;
            p.Dispatcher.Invoke(() => p.Value = Value,
                DispatcherPriority.Background);
        }
    }
}
