using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace R05_AutoJoint
{
    public partial class AutoJointWindow
    {
        private AutoJointViewModel _viewModel;

        public AutoJointWindow(AutoJointViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            this.DataContext = viewModel;
        }



    }
}
