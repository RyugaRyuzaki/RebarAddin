using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace R07_AboutAuthor
{
    public partial class AboutWindow
    {
        private AboutViewModel _viewModel;

        public AboutWindow(AboutViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            this.DataContext = viewModel;
        }



    }
}
