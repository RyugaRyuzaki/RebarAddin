using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace R06_StairRebar
{
    public partial class StairWindow
    {
        private StairViewModel _viewModel;

        public StairWindow(StairViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            this.DataContext = viewModel;
        }



    }
}
