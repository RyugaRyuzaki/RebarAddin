

using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static R01_ColumnsRebar.ErrorColumns;
using WpfCustomControls;
namespace R01_ColumnsRebar
{
    public class BarsDivisionModel : BaseViewModel
    {
        #region property
        private int _NumberColumn;
        public int NumberColumn { get => _NumberColumn; set { _NumberColumn = value; OnPropertyChanged(); } }
        private ObservableCollection<ItemDivision> _Stirrup;
        public ObservableCollection<ItemDivision> Stirrup { get => _Stirrup; set { _Stirrup = value; OnPropertyChanged(); } }

        private ObservableCollection<ItemDivision> _AddH;
        public ObservableCollection<ItemDivision> AddH { get => _AddH; set { _AddH = value; OnPropertyChanged(); } }
        private ObservableCollection<ItemDivision> _AddV;
        public ObservableCollection<ItemDivision> AddV { get => _AddV; set { _AddV = value; OnPropertyChanged(); } }
        private ObservableCollection<ItemDivision> _Main;
        public ObservableCollection<ItemDivision> Main { get => _Main; set { _Main = value; OnPropertyChanged(); } }
        
        #endregion
        public BarsDivisionModel(int numberColumn)
        {
            NumberColumn = numberColumn;
            Stirrup = new ObservableCollection<ItemDivision>();
            AddH = new ObservableCollection<ItemDivision>();
            AddV = new ObservableCollection<ItemDivision>();
            Main = new ObservableCollection<ItemDivision>();
        }
        public void GetStirrup(SectionStyle sectionStyle, StirrupModel stirrupModel, InfoModel infoModel, DivisionBar divisionBar, double Cover)
        {
            if (Stirrup.Count!=0)
            {
                Stirrup.Clear();
            }
            Stirrup = ProcessBarsDivision.GetStirrup(sectionStyle,stirrupModel, infoModel, divisionBar, Cover);

        }
        public void GetAddHorizontal(SectionStyle sectionStyle, StirrupModel stirrupModel, InfoModel infoModel, DivisionBar divisionBar, double Cover)
        {
            if (AddH.Count != 0)
            {
                AddH.Clear();
            }
            AddH = ProcessBarsDivision.GetAddHorizontal(sectionStyle, stirrupModel, infoModel, divisionBar, Cover);
        }
        public void GetAddVertical(SectionStyle sectionStyle, StirrupModel stirrupModel, InfoModel infoModel, DivisionBar divisionBar, double Cover)
        {
            if (AddV.Count != 0)
            {
                AddV.Clear();
            }
            AddV = ProcessBarsDivision.GetAddVertical(sectionStyle, stirrupModel, infoModel, divisionBar, Cover);
        }
        public void GetMain(SectionStyle sectionStyle, BarMainModel barMainModel, InfoModel infoModel, DivisionBar divisionBar, double Cover)
        {
            if (Main.Count != 0)
            {
                Main.Clear();
            }
            Main = ProcessBarsDivision.GetMain(sectionStyle,barMainModel,infoModel,divisionBar,Cover);

        }
       

    }
    #region
    public class DivisionBar : BaseViewModel
    {
        #region property
        private List<int> _ManyColumns;
        public List<int> ManyColumns { get => _ManyColumns; set { _ManyColumns = value; OnPropertyChanged(); } }
        private int _NumberColumns;
        public int NumberColumns { get => _NumberColumns; set { _NumberColumns = value; OnPropertyChanged(); } }
       
        #endregion
        public DivisionBar()
        {
            ManyColumns = new List<int>() {1,2,3,4,5,6,7,8,9,10 };
            NumberColumns = ManyColumns[0];
        }
        
    }
    
    #endregion

}
