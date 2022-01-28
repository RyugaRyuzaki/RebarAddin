using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R02_BeamsRebar
{
    public class DetailItemModel:BaseViewModel
    {
        #region property
        private ObservableCollection<DetailItem> _StirrupsDetail;
        public ObservableCollection<DetailItem> StirrupsDetail { get => _StirrupsDetail; set { _StirrupsDetail = value; OnPropertyChanged(); } }
        private ObservableCollection<DetailItem> _StirrupsSection;
        public ObservableCollection<DetailItem> StirrupsSection { get => _StirrupsSection; set { _StirrupsSection = value; OnPropertyChanged(); } }
        private ObservableCollection<DetailItem> _AntiSection;
        public ObservableCollection<DetailItem> AntiSection { get => _AntiSection; set { _AntiSection = value; OnPropertyChanged(); } }
        private ObservableCollection<DetailItem> _LongSection;
        public ObservableCollection<DetailItem> LongSection { get => _LongSection; set { _LongSection = value; OnPropertyChanged(); } }

        private ObservableCollection<DetailItem> _MainTopDetail;
        public ObservableCollection<DetailItem> MainTopDetail { get => _MainTopDetail; set { _MainTopDetail = value; OnPropertyChanged(); } }

        private ObservableCollection<DetailItem> _MainBottomDetail;
        public ObservableCollection<DetailItem> MainBottomDetail { get => _MainBottomDetail; set { _MainBottomDetail = value; OnPropertyChanged(); } }

        private ObservableCollection<DetailItem> _AddTopDetail;
        public ObservableCollection<DetailItem> AddTopDetail { get => _AddTopDetail; set { _AddTopDetail = value; OnPropertyChanged(); } }

        private ObservableCollection<DetailItem> _AddBottomDetail;
        public ObservableCollection<DetailItem> AddBottomDetail { get => _AddBottomDetail; set { _AddBottomDetail = value; OnPropertyChanged(); } }

        private ObservableCollection<DetailItem> _SideDetail;
        public ObservableCollection<DetailItem> SideBarDetail { get => _SideDetail; set { _SideDetail = value; OnPropertyChanged(); } }

        private ObservableCollection<DetailItem> _SpecialDetail;
        public ObservableCollection<DetailItem> SpecialDetail { get => _SpecialDetail; set { _SpecialDetail = value; OnPropertyChanged(); } }
        private ObservableCollection<DetailItem> _SpecialStirrupDetail;
        public ObservableCollection<DetailItem> SpecialStirrupDetail { get => _SpecialStirrupDetail; set { _SpecialStirrupDetail = value; OnPropertyChanged(); } }
        #endregion
        public DetailItemModel()
        {
            StirrupsDetail = new ObservableCollection<DetailItem>();
            StirrupsSection = new ObservableCollection<DetailItem>();
            AntiSection = new ObservableCollection<DetailItem>();
            MainTopDetail = new ObservableCollection<DetailItem>();
            MainBottomDetail = new ObservableCollection<DetailItem>();
            AddTopDetail = new ObservableCollection<DetailItem>();
            AddBottomDetail = new ObservableCollection<DetailItem>();
            SideBarDetail = new ObservableCollection<DetailItem>();
            SpecialDetail = new ObservableCollection<DetailItem>();
            SpecialStirrupDetail = new ObservableCollection<DetailItem>();
            LongSection = new ObservableCollection<DetailItem>();
        }
        #region Detail
        public void GetStirrupDetail(List<InfoModel> InfoModels, List<StirrupModel> StirrupModels, List<DistributeStirrup> DistributeStirrups, List<SpecialBarModel> SpecialBarModels, List<SpecialNodeModel> SpecialNodeModels)
        {
            if (StirrupsDetail.Count == 0)
            {
                StirrupsDetail = ProcessDetailItem.GetStirrupsDetail(InfoModels, StirrupModels, DistributeStirrups, SpecialBarModels, SpecialNodeModels);
            }
            else
            {
                StirrupsDetail.Clear();
                StirrupsDetail = ProcessDetailItem.GetStirrupsDetail(InfoModels, StirrupModels, DistributeStirrups, SpecialBarModels, SpecialNodeModels);
            }
        }
        public void GetAntiStirrupSection(List<InfoModel> InfoModels, List<StirrupModel> StirrupModels, List<DistributeStirrup> DistributeStirrups,SettingModel settingModel)
        {
            int number = InfoModels.Count ;
            if (AntiSection.Count == 0)
            {
                AntiSection = ProcessDetailItem.GetAntiStirrupsSection(InfoModels, StirrupModels, DistributeStirrups, settingModel,number);
            }
            else
            {
                AntiSection.Clear();
                AntiSection = ProcessDetailItem.GetAntiStirrupsSection(InfoModels, StirrupModels, DistributeStirrups, settingModel, number);
            }
        }
        public void GetMainTopDetail(SingleMainTopBarModel SingleMainTopBarModel, List<MainTopBarModel> MainTopBarModel, SelectedIndexModel SelectedIndexModel,List<InfoModel> infoModels)
        {
            int number =(AntiSection.Count==0)? infoModels.Count+1: AntiSection[AntiSection.Count - 1].RebarNumber + 1;
            if (MainTopDetail.Count == 0)
            {
                MainTopDetail = ProcessDetailItem.GetMainTopDetail(SingleMainTopBarModel, MainTopBarModel, SelectedIndexModel, number);
            }
            else
            {
                MainTopDetail.Clear();
                MainTopDetail = ProcessDetailItem.GetMainTopDetail(SingleMainTopBarModel, MainTopBarModel, SelectedIndexModel, number);
            }
        }
        public void GetMainBottomDetail( List<MainBottomBarModel> mainBottomBarModels)
        {

            int number = MainTopDetail[MainTopDetail.Count - 1].RebarNumber + 1;
            if (MainBottomDetail.Count == 0)
            {
                MainBottomDetail = ProcessDetailItem.GetMainBottomDetail(mainBottomBarModels, number);
            }
            else
            {
                MainBottomDetail.Clear();
                MainBottomDetail = ProcessDetailItem.GetMainBottomDetail(mainBottomBarModels, number);
            }
        }
        public void GetAddTopDetail(AddTopBarModel addTopBarModel, List<InfoModel>infoModels)
        {
            int number = MainBottomDetail[MainBottomDetail.Count - 1].RebarNumber + 1;
            if (AddTopDetail.Count == 0)
            {
                AddTopDetail = ProcessDetailItem.GetAddTopBarDetail(addTopBarModel, infoModels,number);
            }
            else
            {
                AddTopDetail.Clear();
                AddTopDetail = ProcessDetailItem.GetAddTopBarDetail(addTopBarModel, infoModels,number);
            }
        }
        public void GetAddBottomDetail(ObservableCollection<AddBottomBarModel> addBottomBarModels, List<SelectedBottomModel> selectedBottomModels)
        {
            int number =(AddTopDetail.Count==0)? MainBottomDetail[MainBottomDetail.Count - 1].RebarNumber + 1 : AddTopDetail[AddTopDetail.Count - 1].RebarNumber + 1;
            if (AddBottomDetail.Count == 0)
            {
                AddBottomDetail = ProcessDetailItem.GetAddBottomBarDetail(addBottomBarModels, selectedBottomModels, number);
            }
            else
            {
                AddBottomDetail.Clear();
                AddBottomDetail = ProcessDetailItem.GetAddBottomBarDetail(addBottomBarModels, selectedBottomModels, number);
            }
        }
        public void GetSideDetail(List<SideBarModel> SideBarModel)
        {
            int number = 0;
            if (AddBottomDetail.Count == 0)
            {
                number = (AddTopDetail.Count == 0) ? MainBottomDetail[MainBottomDetail.Count - 1].RebarNumber + 1 : AddTopDetail[AddTopDetail.Count - 1].RebarNumber + 1;
            }
            else
            {
                number= AddBottomDetail[AddBottomDetail.Count - 1].RebarNumber + 1;
            }
            if (SideBarDetail.Count == 0)
            {
                SideBarDetail = ProcessDetailItem.GetSideBarDetail(SideBarModel, number);
            }
            else
            {
                SideBarDetail.Clear();
                SideBarDetail = ProcessDetailItem.GetSideBarDetail(SideBarModel, number);
            }
        }
        public void GetSpecialDetail(List<SpecialBarModel> SpecialBarModel)
        {
            int number = 0;
            if (SideBarDetail.Count==0)
            {
                if (AddBottomDetail.Count == 0)
                {
                    number = (AddTopDetail.Count == 0) ? MainBottomDetail[MainBottomDetail.Count - 1].RebarNumber + 1 : AddTopDetail[AddTopDetail.Count - 1].RebarNumber + 1;
                }
                else
                {
                    number = AddBottomDetail[AddBottomDetail.Count - 1].RebarNumber + 1;
                }
            }
            else
            {
                number = SideBarDetail[SideBarDetail.Count - 1].RebarNumber + 1;
            }
            
            if (SpecialDetail.Count == 0)
            {
                SpecialDetail = ProcessDetailItem.GetSpecialBarDetail(SpecialBarModel,number);
            }
            else
            {
                SpecialDetail.Clear();
                SpecialDetail = ProcessDetailItem.GetSpecialBarDetail(SpecialBarModel, number);
            }
        }
        public void GetSpecialStirrupDetail(List<SpecialBarModel> SpecialBarModel, List<InfoModel> infoModels,double cover)
        {
            int number = 0;
            if (SpecialDetail.Count==0)
            {
                if (SideBarDetail.Count == 0)
                {
                    if (AddBottomDetail.Count == 0)
                    {
                        number = (AddTopDetail.Count == 0) ? MainBottomDetail[MainBottomDetail.Count - 1].RebarNumber + 1 : AddTopDetail[AddTopDetail.Count - 1].RebarNumber + 1;
                    }
                    else
                    {
                        number = AddBottomDetail[AddBottomDetail.Count - 1].RebarNumber + 1;
                    }
                }
                else
                {
                    number = SideBarDetail[SideBarDetail.Count - 1].RebarNumber + 1;
                }
            }
            else
            {
                number = SpecialDetail[SpecialDetail.Count - 1].RebarNumber + 1;
            }
            

            if (SpecialStirrupDetail.Count == 0)
            {
                SpecialStirrupDetail = ProcessDetailItem.GetSpecialBarStirrupDetail(SpecialBarModel, infoModels,cover, number);
            }
            else
            {
                SpecialStirrupDetail.Clear();
                SpecialStirrupDetail = ProcessDetailItem.GetSpecialBarStirrupDetail(SpecialBarModel, infoModels, cover, number);
            }
        }
        #endregion
        #region Section
        public void GetStirrupSection(List<InfoModel> InfoModels, List<StirrupModel> StirrupModels, List<DistributeStirrup> DistributeStirrups)
        {
            if (StirrupsSection.Count == 0)
            {
                StirrupsSection = ProcessDetailItem.GetStirrupsSection(InfoModels, StirrupModels, DistributeStirrups);
            }
            else
            {
                StirrupsSection.Clear();
                StirrupsSection = ProcessDetailItem.GetStirrupsSection(InfoModels, StirrupModels, DistributeStirrups);
            }
        }
        
        public void GetLongSection(ObservableCollection<SectionAreaModel> SectionAreaModels, List<InfoModel> infoModels, List<SideBarModel> sideBarModels,double Cover,double dsmax)
        {
            if (LongSection.Count == 0)
            {
                LongSection = ProcessDetailItem.GetLongSection(SectionAreaModels, infoModels, sideBarModels,MainTopDetail,AddTopDetail,AddBottomDetail,MainBottomDetail,SideBarDetail, Cover, dsmax);
            }
            else
            {
                LongSection.Clear();
                LongSection = ProcessDetailItem.GetLongSection(SectionAreaModels, infoModels, sideBarModels, MainTopDetail, AddTopDetail, AddBottomDetail, MainBottomDetail, SideBarDetail, Cover, dsmax);
            }
        }
        #endregion
    }
}
