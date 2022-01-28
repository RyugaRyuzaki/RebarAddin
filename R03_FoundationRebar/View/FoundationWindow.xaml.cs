using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace R03_FoundationRebar
{
    public partial class FoundationWindow
    {
        private FoundationViewModel _viewModel;

        public FoundationWindow(FoundationViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            this.DataContext = viewModel;
        }



    }
}
