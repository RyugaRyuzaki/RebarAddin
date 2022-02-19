using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace R04_SlabsRebar
{
    public partial class SlabsWindow
    {
        private SlabViewModel _viewModel;
        public SlabsWindow(SlabViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            this.DataContext = viewModel;
        }
    }
}
