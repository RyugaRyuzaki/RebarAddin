using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace R11_FoundationPile
{
    public partial class FoundationPileWindow
    {
        private FoundationPileViewModel _viewModel;

        public FoundationPileWindow(FoundationPileViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            this.DataContext = viewModel;
        }
    }
}
