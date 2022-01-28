using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace R10_WallShear
{
    public partial class WallShearWindow
    {
        private WallShearViewModel _viewModel;

        public WallShearWindow(WallShearViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            this.DataContext = viewModel;
        }



    }
}
