
namespace R02_BeamsRebar
{
    public class SelectedBottomModel:BaseViewModel
    {
        private int _Span;
        public int Span { get => _Span; set { _Span = value; OnPropertyChanged(); } }
        private bool _StartBottomChecked;
        public bool StartBottomChecked { get => _StartBottomChecked; set { _StartBottomChecked = value; OnPropertyChanged(); } }
        private bool _EndBottomChecked;
        public bool EndBottomChecked { get => _EndBottomChecked; set { _EndBottomChecked = value; OnPropertyChanged(); } }
        private int _SelectedLayerAddBottomStart;
        public int SelectedLayerAddBottomStart { get => _SelectedLayerAddBottomStart; set { _SelectedLayerAddBottomStart = value; OnPropertyChanged(); } }
        private int _SelectedLayerAddBottomEnd;
        public int SelectedLayerAddBottomEnd { get => _SelectedLayerAddBottomEnd; set { _SelectedLayerAddBottomEnd = value; OnPropertyChanged(); } }
        private int _SelectedLayerAddBottomMid;
        public int SelectedLayerAddBottomMid { get => _SelectedLayerAddBottomMid; set { _SelectedLayerAddBottomMid = value; OnPropertyChanged(); } }
        private double _Length;
        public double Length { get => _Length; set { _Length = value; OnPropertyChanged(); } }
        public SelectedBottomModel(int span, bool startBottomChecked, bool endBottomChecked ,int selectedLayerAddBottomStart, int selectedLayerAddBottomEnd, int selectedLayerAddBottomMid, double length)
        {
            Span = span;
            StartBottomChecked = startBottomChecked;
            EndBottomChecked = endBottomChecked;
            SelectedLayerAddBottomStart = selectedLayerAddBottomStart;
            SelectedLayerAddBottomEnd = selectedLayerAddBottomEnd;
            SelectedLayerAddBottomMid = selectedLayerAddBottomMid;
            Length = length;
        }
    }
}
