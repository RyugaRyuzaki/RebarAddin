using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace R00_Startup
{
    public partial class LicenseWindow
    {
        private LicenseViewModel _viewModel;

        public LicenseWindow(LicenseViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            this.DataContext = viewModel;
        }



    }
}
