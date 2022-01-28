

namespace R02_BeamsRebar
{
    public partial class BeamsWindow
    {
        private BeamsViewModel _viewModel;

        public BeamsWindow(BeamsViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            this.DataContext = viewModel;
        }
    }
}
