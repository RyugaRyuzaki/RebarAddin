using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace R08_ContinueFootingRebar
{
    public partial class ContinueFoundationWindow
    {
        private ContinueFoundationViewModel _viewModel;

        public ContinueFoundationWindow(ContinueFoundationViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            this.DataContext = viewModel;
        }



    }
}
