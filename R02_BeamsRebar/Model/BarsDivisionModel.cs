
using R02_BeamsRebar.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace R02_BeamsRebar
{
    public class BarsDivisionModel : BaseViewModel
    {
        #region property
        private ObservableCollection<ItemDivision> _Stirrups;
        public ObservableCollection<ItemDivision> Stirrups { get => _Stirrups; set { _Stirrups = value; OnPropertyChanged(); } }
        private ObservableCollection<ItemDivision> _AntiStirrups;
        public ObservableCollection<ItemDivision> AntiStirrups { get => _AntiStirrups; set { _AntiStirrups = value; OnPropertyChanged(); } }
        private ObservableCollection<ItemDivision> _MainTop;
        public ObservableCollection<ItemDivision> MainTop { get => _MainTop; set { _MainTop = value; OnPropertyChanged(); } }

        private ObservableCollection<ItemDivision> _MainBottom;
        public ObservableCollection<ItemDivision> MainBottom { get => _MainBottom; set { _MainBottom = value; OnPropertyChanged(); } }

        private ObservableCollection<ItemDivision> _AddTop;
        public ObservableCollection<ItemDivision> AddTop { get => _AddTop; set { _AddTop = value; OnPropertyChanged(); } }
        private ObservableCollection<ItemDivision> _AddBottom;
        public ObservableCollection<ItemDivision> AddBottom { get => _AddBottom; set { _AddBottom = value; OnPropertyChanged(); } }
        private ObservableCollection<ItemDivision> _Side;
        public ObservableCollection<ItemDivision> Side { get => _Side; set { _Side = value; OnPropertyChanged(); } }
        private ObservableCollection<ItemDivision> _Special;
        public ObservableCollection<ItemDivision> Special { get => _Special; set { _Special = value; OnPropertyChanged(); } }
        #endregion
        public BarsDivisionModel()
        {
            Stirrups = new ObservableCollection<ItemDivision>();
            AntiStirrups = new ObservableCollection<ItemDivision>();
            MainTop = new ObservableCollection<ItemDivision>();
            MainBottom = new ObservableCollection<ItemDivision>();
            AddTop = new ObservableCollection<ItemDivision>();
            AddBottom = new ObservableCollection<ItemDivision>();
            Side = new ObservableCollection<ItemDivision>();
            Special = new ObservableCollection<ItemDivision>();
        }
        #region get bar top
        public void GetStirrups(List<InfoModel> InfoModels, List<StirrupModel> StirrupModels, List<DistributeStirrup> DistributeStirrups, List<SpecialBarModel> SpecialBarModels, List<SpecialNodeModel> SpecialNodeModels, DivisionBar DivisionBar)
        {
            if (Stirrups.Count ==0)
            {
                Stirrups = ProcessBarsDivision.GetStirrup(InfoModels, StirrupModels, DistributeStirrups, SpecialBarModels, SpecialNodeModels, DivisionBar);
            }
            else
            {
                Stirrups.Clear();
                Stirrups = ProcessBarsDivision.GetStirrup(InfoModels, StirrupModels, DistributeStirrups, SpecialBarModels, SpecialNodeModels, DivisionBar);
            }
        }
        public void GetAntiStirrups(List<InfoModel> InfoModels, List<StirrupModel> StirrupModels, List<DistributeStirrup> DistributeStirrups, List<SpecialBarModel> SpecialBarModels, List<SpecialNodeModel> SpecialNodeModels, DivisionBar DivisionBar,SettingModel settingModel)
        {
            if (AntiStirrups.Count == 0)
            {
                AntiStirrups = ProcessBarsDivision.GetAntiStirrup(InfoModels, StirrupModels, DistributeStirrups, SpecialBarModels, SpecialNodeModels, DivisionBar, settingModel);
            }
            else
            {
                AntiStirrups.Clear();
                AntiStirrups = ProcessBarsDivision.GetAntiStirrup(InfoModels, StirrupModels, DistributeStirrups, SpecialBarModels, SpecialNodeModels, DivisionBar, settingModel);
            }
        }
        public void GetMainTop(SingleMainTopBarModel SingleMainTopBarModel, List<MainTopBarModel> MainTopBarModel, DivisionBar DivisionBar, SelectedIndexModel SelectedIndexModel)
        {
            if (MainTop.Count ==0)
            {
                MainTop = ProcessBarsDivision.GetMainTop(SingleMainTopBarModel, MainTopBarModel, DivisionBar, SelectedIndexModel);

            }
            else
            {
                MainTop.Clear();
                MainTop = ProcessBarsDivision.GetMainTop(SingleMainTopBarModel, MainTopBarModel, DivisionBar, SelectedIndexModel);

            }
        }
        public void GetMainBottom(List<MainBottomBarModel> MainBottomBarModel, DivisionBar DivisionBar)
        {
            if (MainBottom.Count == 0)
            {
                MainBottom = ProcessBarsDivision.GetMainBottomMulti(MainBottomBarModel, DivisionBar);

            }
            else
            {
                MainBottom.Clear();
                MainBottom = ProcessBarsDivision.GetMainBottomMulti(MainBottomBarModel, DivisionBar);
            }
        }
        public void GetAddTop(AddTopBarModel AddTopBarModel, DivisionBar DivisionBar, SelectedIndexModel SelectedIndexModel)
        {
            if (AddTop.Count == 0)
            {
                AddTop = ProcessBarsDivision.GetAddTop(AddTopBarModel, DivisionBar, SelectedIndexModel);
            }
            else
            {
                AddTop.Clear();
                AddTop = ProcessBarsDivision.GetAddTop(AddTopBarModel, DivisionBar, SelectedIndexModel);
            }
        }
        public void GetAddBottom(ObservableCollection<AddBottomBarModel> AddBottomBarModel, DivisionBar DivisionBar, List<SelectedBottomModel> SelectedBottomModels)
        {
            if (AddBottom.Count == 0)
            {
                AddBottom = ProcessBarsDivision.GetAddBottom(AddBottomBarModel, DivisionBar, SelectedBottomModels);
            }
            else
            {
                AddBottom.Clear();
                AddBottom = ProcessBarsDivision.GetAddBottom(AddBottomBarModel, DivisionBar, SelectedBottomModels);
            }
        }
        public void GetSpecial(List<SpecialBarModel> SpecialBarModel, DivisionBar DivisionBar)
        {
            if (Special.Count == 0)
            {
                Special = ProcessBarsDivision.GetSpecialBar(SpecialBarModel, DivisionBar);
            }
            else
            {
                Special.Clear();
                Special = ProcessBarsDivision.GetSpecialBar(SpecialBarModel, DivisionBar);
            }
        }
        public void GetSide(List<SideBarModel> SideBarModel, DivisionBar DivisionBar)
        {
            if (Side.Count == 0)
            {
                Side = ProcessBarsDivision.GetSideBar(SideBarModel, DivisionBar);
            }
            else
            {
                Side.Clear();
                Side = ProcessBarsDivision.GetSideBar(SideBarModel, DivisionBar);
            }
        }
        #endregion

    }
    #region
    public class DivisionBar : BaseViewModel
    {
        #region property
      
        public List<string> Way { get; set; }
        private string _SelectedWay;
        public string SelectedWay { get => _SelectedWay; set { _SelectedWay = value; OnPropertyChanged(); } }
        private List<int> _ManyBeams;
        public List<int> ManyBeams { get => _ManyBeams; set { _ManyBeams = value; OnPropertyChanged(); } }
        private int _NumberBeams;
        public int NumberBeams { get => _NumberBeams; set { _NumberBeams = value; OnPropertyChanged(); } }
        private double _Lmax;
        public double Lmax { get => _Lmax; set { _Lmax = value; OnPropertyChanged(); } }
        private double _Overlap;
        public double Overlap { get => _Overlap; set { _Overlap = value; OnPropertyChanged(); } }
        #endregion
        public DivisionBar(double lmax, double overlap)
        {
            Lmax = lmax; Overlap = overlap;
            Way = new List<string>() { "Left to Right", "Right to Left" };
            SelectedWay = Way[0];
            ManyBeams = new List<int>() {1,2,3,4,5,6,7,8,9,10 };
            NumberBeams = ManyBeams[0];
        }
        
    }
    
    #endregion

}
