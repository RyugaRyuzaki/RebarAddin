using WpfCustomControls;
namespace R02_BeamsRebar
{
    public class SelectedIndexModel:BaseViewModel
    {
        private int _Span;
        public int Span { get => _Span; set { _Span = value; OnPropertyChanged(); } }
      
        private int _Node;
        public int Node { get => _Node; set { _Node = value; OnPropertyChanged(); } }
        private int _SelectedCover;
        public int SelectedCover { get => _SelectedCover; set { _SelectedCover = value; OnPropertyChanged(); } }
        private int _StirrupViewBarS;
        public int StirrupViewBarS { get => _StirrupViewBarS; set { _StirrupViewBarS = value; OnPropertyChanged(); } }
        private int _StirrupViewBarA;
        public int StirrupViewBarA { get => _StirrupViewBarA; set { _StirrupViewBarA = value; OnPropertyChanged(); } }
        private int _StyleMainTop;
        public int StyleMainTop { get => _StyleMainTop; set { _StyleMainTop = value; OnPropertyChanged(); } }
        private int _BarTop;
        public int BarTop { get => _BarTop; set { _BarTop = value; OnPropertyChanged(); } }
        private int _BarBottom;
        public int BarBottom { get => _BarBottom; set { _BarBottom = value; OnPropertyChanged(); } }
        private int _AddTop;
        public int AddTop { get => _AddTop; set { _AddTop = value; OnPropertyChanged(); } }
        private bool _StartTopChecked;
        public bool StartTopChecked { get => _StartTopChecked; set { _StartTopChecked = value; OnPropertyChanged(); } }
        private bool _EndTopChecked;
        public bool EndTopChecked { get => _EndTopChecked; set { _EndTopChecked = value; OnPropertyChanged(); } }
        private int _SelectedLayerAddTopStar;
        public int SelectedLayerAddTopStart { get => _SelectedLayerAddTopStar; set { _SelectedLayerAddTopStar = value; OnPropertyChanged(); } }
        private int _SelectedLayerAddTopEnd;
        public int SelectedLayerAddTopEnd { get => _SelectedLayerAddTopEnd; set { _SelectedLayerAddTopEnd = value; OnPropertyChanged(); } }
        private int _SelectedLayerAddTopMid;
        public int SelectedLayerAddTopMid { get => _SelectedLayerAddTopMid; set { _SelectedLayerAddTopMid = value; OnPropertyChanged(); } }
       

        private int _SideBar;
        public int SideBar { get => _SideBar; set { _SideBar = value; OnPropertyChanged(); } }
        private int _SpecialBar;
        public int SpecialBar { get => _SpecialBar; set { _SpecialBar = value; OnPropertyChanged(); } }
        private int _SectionSpan;
        public int SectionSpan { get => _SectionSpan; set { _SectionSpan = value; OnPropertyChanged(); } }
        public SelectedIndexModel(int span, int styleMainTop, int barTop, int barBottom, int addTop, int sideBar,int special)
        {
            Span = span;
            StyleMainTop = styleMainTop;
            BarTop = barTop;
            BarBottom = barBottom;
            AddTop = addTop;
            StartTopChecked = false;
            EndTopChecked = false;
            SideBar = sideBar;
            SpecialBar = special;
            StirrupViewBarS = 0;
            StirrupViewBarA = 0;
            SectionSpan = 0;
            SelectedLayerAddTopStart = 0; SelectedLayerAddTopEnd = 0; SelectedLayerAddTopMid = 0; SelectedCover = 0;
        }
    }
}
