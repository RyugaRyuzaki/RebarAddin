using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static R01_ColumnsRebar.ErrorColumns;
using WpfCustomControls;
namespace R01_ColumnsRebar
{
    public class DetailItemModel:BaseViewModel
    {
        #region property
        private ObservableCollection<DetailItem> _StirrupsDetailX;
        public ObservableCollection<DetailItem> StirrupsDetailX { get => _StirrupsDetailX; set { _StirrupsDetailX = value; OnPropertyChanged(); } }
        private ObservableCollection<DetailItem> _StirrupsDetailY;
        public ObservableCollection<DetailItem> StirrupsDetailY { get => _StirrupsDetailY; set { _StirrupsDetailY = value; OnPropertyChanged(); } }
        private ObservableCollection<DetailItem> _StirrupsSection;
        public ObservableCollection<DetailItem> StirrupsSection { get => _StirrupsSection; set { _StirrupsSection = value; OnPropertyChanged(); } }
        private ObservableCollection<DetailItem> _AddHSection;
        public ObservableCollection<DetailItem> AddHSection { get => _AddHSection; set { _AddHSection = value; OnPropertyChanged(); } }
        private ObservableCollection<DetailItem> _AddVSection;
        public ObservableCollection<DetailItem> AddVSection { get => _AddVSection; set { _AddVSection = value; OnPropertyChanged(); } }

        private ObservableCollection<DetailItem> _LongDetailX;
        public ObservableCollection<DetailItem> LongDetailX { get => _LongDetailX; set { _LongDetailX = value; OnPropertyChanged(); } }
        private ObservableCollection<DetailItem> _LongDetailY;
        public ObservableCollection<DetailItem> LongDetailY { get => _LongDetailY; set { _LongDetailY = value; OnPropertyChanged(); } }
        private ObservableCollection<DetailItem> _LongSection;
        public ObservableCollection<DetailItem> LongSection { get => _LongSection; set { _LongSection = value; OnPropertyChanged(); } }

      
        #endregion
        public DetailItemModel()
        {
            StirrupsDetailX = new ObservableCollection<DetailItem>();
            StirrupsDetailY = new ObservableCollection<DetailItem>();
            StirrupsSection = new ObservableCollection<DetailItem>();
            AddHSection = new ObservableCollection<DetailItem>();
            AddVSection = new ObservableCollection<DetailItem>();
            LongDetailX = new ObservableCollection<DetailItem>();
            LongDetailY = new ObservableCollection<DetailItem>();
            LongSection = new ObservableCollection<DetailItem>();
        }
        #region Detail
        public void GetStirrupDetail(SectionStyle sectionStyle, ObservableCollection<InfoModel> InfoModels, ObservableCollection<StirrupModel> StirrupModels,double Cover)
        {
            if (StirrupsDetailX.Count == 0)
            {
                StirrupsDetailX = ProcessDetailItem.GetStirrupsDetail(sectionStyle, InfoModels, StirrupModels, true, Cover);
                
            }
            else
            {
                StirrupsDetailX.Clear();
                StirrupsDetailX = ProcessDetailItem.GetStirrupsDetail(sectionStyle,InfoModels, StirrupModels, true, Cover);
            }
            if (StirrupsDetailY.Count == 0)
            {
                StirrupsDetailY = ProcessDetailItem.GetStirrupsDetail(sectionStyle, InfoModels, StirrupModels, false, Cover);
            }
            else
            {
                StirrupsDetailY.Clear();
                StirrupsDetailY = ProcessDetailItem.GetStirrupsDetail(sectionStyle, InfoModels, StirrupModels, false, Cover);
            }
        }

        #endregion
        #region Section
        public void GetStirrupSection(SectionStyle sectionStyle, ObservableCollection<InfoModel> InfoModels, ObservableCollection<StirrupModel> StirrupModels, double Cover)
        {
            if (StirrupsSection.Count == 0)
            {
                StirrupsSection = ProcessDetailItem.GetStirrupSection(sectionStyle, InfoModels, StirrupModels, Cover);

            }
            else
            {
                StirrupsSection.Clear();
                StirrupsSection = ProcessDetailItem.GetStirrupSection(sectionStyle, InfoModels, StirrupModels,  Cover);
            }
        }
        public void GetAddHSection(SectionStyle sectionStyle, ObservableCollection<InfoModel> InfoModels, ObservableCollection<StirrupModel> StirrupModels, double Cover)
        {
            if (AddHSection.Count == 0)
            {
                AddHSection = ProcessDetailItem.GetAddHSection(sectionStyle, InfoModels, StirrupModels, Cover,StirrupsSection.Count);

            }
            else
            {
                AddHSection.Clear();
                AddHSection = ProcessDetailItem.GetAddHSection(sectionStyle, InfoModels, StirrupModels, Cover, StirrupsSection.Count);
            }
        }
        public void GetAddVSection(SectionStyle sectionStyle, ObservableCollection<InfoModel> InfoModels, ObservableCollection<StirrupModel> StirrupModels, double Cover)
        {
            if (AddVSection.Count == 0)
            {
                AddVSection = ProcessDetailItem.GetAddVSection(sectionStyle, InfoModels, StirrupModels, Cover, StirrupsSection.Count+ AddHSection.Count);

            }
            else
            {
                AddVSection.Clear();
                AddVSection = ProcessDetailItem.GetAddVSection(sectionStyle, InfoModels, StirrupModels, Cover, StirrupsSection.Count + AddHSection.Count);
            }
        }
        #endregion
    }
}
