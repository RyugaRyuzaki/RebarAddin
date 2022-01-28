using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace R01_ColumnsRebar
{
    public partial class ColumnsWindow
    {
        private ColumnsViewModel _viewModel;

        public ColumnsWindow(ColumnsViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = viewModel;
        }



    }
}
