using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCustomControls;
namespace R11_FoundationPile
{
    public class SelectedIndexModel    :BaseViewModel
    {
        private int _SelectedIndexPileCategory;
        public int SelectedIndexPileCategory { get => _SelectedIndexPileCategory; set { _SelectedIndexPileCategory = value; OnPropertyChanged(); } }
        private int _SelectedIndexPileFamily;
        public int SelectedIndexPileFamily { get => _SelectedIndexPileFamily; set { _SelectedIndexPileFamily = value; OnPropertyChanged(); } }
        private int _SelectedIndexPileFamilyType;
        public int SelectedIndexPileFamilyType { get => _SelectedIndexPileFamilyType; set { _SelectedIndexPileFamilyType = value; OnPropertyChanged(); } }
        private int _SelectedIndexFoundationCategory;
        public int SelectedIndexFoundationCategory { get => _SelectedIndexFoundationCategory; set { _SelectedIndexFoundationCategory = value; OnPropertyChanged(); } }
        private int _SelectedIndexFoundationType;
        public int SelectedIndexFoundationType { get => _SelectedIndexFoundationType; set { _SelectedIndexFoundationType = value; OnPropertyChanged(); } }
        private int _SelectedIndexGroupFoundationModel;
        public int SelectedIndexGroupFoundationModel { get => _SelectedIndexGroupFoundationModel; set { _SelectedIndexGroupFoundationModel = value; OnPropertyChanged(); } }
        private int _SelectedIndexFoundationModel;
        public int SelectedIndexFoundationModel { get => _SelectedIndexFoundationModel; set { _SelectedIndexFoundationModel = value; OnPropertyChanged(); } }
        private int _SelectedIndexPile;
        public int SelectedIndexPile { get => _SelectedIndexPile; set { _SelectedIndexPile = value; OnPropertyChanged(); } }


        private int _SelectedIndexAllFoundation;
        public int SelectedIndexAllFoundation { get => _SelectedIndexAllFoundation; set { _SelectedIndexAllFoundation = value; OnPropertyChanged(); } }

        private int _SelectedIndexAllPile;
        public int SelectedIndexAllPile { get => _SelectedIndexAllPile; set { _SelectedIndexAllPile = value; OnPropertyChanged(); } }

        private int _SelectedIndexFoundationBarModel;
        public int SelectedIndexFoundationBarModel { get => _SelectedIndexFoundationBarModel; set { _SelectedIndexFoundationBarModel = value; OnPropertyChanged(); } }
        private int _SelectedIndexBarModel;
        public int SelectedIndexBarModel { get => _SelectedIndexBarModel; set { _SelectedIndexBarModel = value; OnPropertyChanged(); } }
        private ObservableCollection<string> _Representative;
        public ObservableCollection<string> Representative { get { if (_Representative == null) { _Representative = new ObservableCollection<string>(); } return _Representative; } set { _Representative = value; OnPropertyChanged(); } }

       
        public SelectedIndexModel()
        {
            SelectedIndexPileCategory = 0;
            SelectedIndexPileFamily = 0;
            SelectedIndexPileFamilyType = 0;
            SelectedIndexFoundationCategory = 0;
            SelectedIndexFoundationType = 0;
            SelectedIndexGroupFoundationModel = 0;
            SelectedIndexFoundationModel = 0;
            SelectedIndexFoundationBarModel = 0;
            SelectedIndexBarModel = 0;
            SelectedIndexPile = 0;
            SelectedIndexAllFoundation = 0;
            SelectedIndexAllPile = 0;
        }
        
        
    }
}
