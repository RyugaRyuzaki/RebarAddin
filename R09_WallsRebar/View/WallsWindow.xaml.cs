using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace R09_WallsRebar
{
    public partial class WallsWindow
    {
        private WallsViewModel _viewModel;

        public WallsWindow(WallsViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            this.DataContext = viewModel;
        }



    }
}
