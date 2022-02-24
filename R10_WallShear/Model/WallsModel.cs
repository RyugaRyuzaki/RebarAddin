using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DSP;
using WpfCustomControls.Model;
namespace R10_WallShear
{
    public class WallsModel : BaseViewModel
    {
        #region property
        private string _FamilyType;
        public string FamilyType { get => _FamilyType; set { _FamilyType = value; OnPropertyChanged(); } }
        private string _AllType;
        public string AllType { get => _AllType; set { _AllType = value; OnPropertyChanged(); } }
        private List<int> _AllNumberBarX;
        public List<int> AllNumberBarX { get => _AllNumberBarX; set { _AllNumberBarX = value; OnPropertyChanged(); } }
        private List<int> _AllNumberBarY;
        public List<int> AllNumberBarY { get => _AllNumberBarY; set { _AllNumberBarY = value; OnPropertyChanged(); } }
        public List<RebarBarType> RebarBarTypes { get; set; }
        public List<RebarCoverType> RebarCoverTypes { get; set; }
        private RebarCoverType _RebarCoverType;
        public RebarCoverType RebarCoverType { get => _RebarCoverType; set { _RebarCoverType = value; OnPropertyChanged(); } }
        private double _Cover;
        public double Cover { get => _Cover; set { _Cover = value; OnPropertyChanged(); } }
        private List<RebarBarModel> _AllBars;
        public List<RebarBarModel> AllBars { get => _AllBars; set { _AllBars = value; OnPropertyChanged(); } }
        #endregion
        #region Info
        private SettingModel _SettingModel;
        public SettingModel SettingModel { get => _SettingModel; set { _SettingModel = value; OnPropertyChanged(); } }
        private ObservableCollection<InfoModel> _InfoModels;
        public ObservableCollection<InfoModel> InfoModels { get { if (_InfoModels == null) _InfoModels = new ObservableCollection<InfoModel>(); return _InfoModels; } set { _InfoModels = value; OnPropertyChanged(); } }
        #endregion
        #region Bars
        private ObservableCollection<StirrupModel> _StirrupModels;
        public ObservableCollection<StirrupModel> StirrupModels { get { if (_StirrupModels == null) _StirrupModels = new ObservableCollection<StirrupModel>(); return _StirrupModels; } set { _StirrupModels = value; OnPropertyChanged(); } }
        private ObservableCollection<BarMainModel> _BarMainModels;
        public ObservableCollection<BarMainModel> BarMainModels { get { if (_BarMainModels == null) _BarMainModels = new ObservableCollection<BarMainModel>(); return _BarMainModels; } set { _BarMainModels = value; OnPropertyChanged(); } }
        #endregion
        #region Draw Main
        private DrawModel _DrawModel;
        public DrawModel DrawModel { get => _DrawModel; set { _DrawModel = value; OnPropertyChanged(); } }
        private DrawModel _DrawModelSection;
        public DrawModel DrawModelSection { get => _DrawModelSection; set { _DrawModelSection = value; OnPropertyChanged(); } }
        private SelectedIndexModel _SelectedIndexModel;
        public SelectedIndexModel SelectedIndexModel { get => _SelectedIndexModel; set { _SelectedIndexModel = value; OnPropertyChanged(); } }
        #endregion
        #region PlanarFace
        private ObservableCollection<PlanarFace> _PlanarFaces;
        public ObservableCollection<PlanarFace> PlanarFaces { get { if (_PlanarFaces == null) _PlanarFaces = new ObservableCollection<PlanarFace>(); return _PlanarFaces; } set { _PlanarFaces = value; OnPropertyChanged(); } }
        #endregion
        #region Action
        private bool _IsRebar;
        public bool IsRebar { get => _IsRebar; set { _IsRebar = value; OnPropertyChanged(); } }
        private bool _IsDetailItem;
        public bool IsDetailItem { get => _IsDetailItem; set { _IsDetailItem = value; OnPropertyChanged(); } }
        private string _SelectedAction;
        public string SelectedAction { get => _SelectedAction; set { _SelectedAction = value; OnPropertyChanged(); } }
        private int _Value;
        public int Value { get => _Value; set { _Value = value; OnPropertyChanged(); } }
        private double _Percent;
        public double Percent { get => _Percent; set { _Percent = value; OnPropertyChanged(); } }
        #region Action

        private ProgressModel _ProgressModel;
        public ProgressModel ProgressModel { get => _ProgressModel; set { _ProgressModel = value; OnPropertyChanged(); } }
        private bool _IsCreateStirrupBars;
        public bool IsCreateStirrupBars { get => _IsCreateStirrupBars; set { _IsCreateStirrupBars = value; OnPropertyChanged(); } }
        private bool _IsCreateMainBars;
        public bool IsCreateMainBars { get => _IsCreateMainBars; set { _IsCreateMainBars = value; OnPropertyChanged(); } }
        private bool _IsCreateTagBars;
        public bool IsCreateTagBars { get => _IsCreateTagBars; set { _IsCreateTagBars = value; OnPropertyChanged(); } }
        private bool _IsCreateDetailView;
        public bool IsCreateDetailView { get => _IsCreateDetailView; set { _IsCreateDetailView = value; OnPropertyChanged(); } }
        private bool _IsCreateSectionView;
        public bool IsCreateSectionView { get => _IsCreateSectionView; set { _IsCreateSectionView = value; OnPropertyChanged(); } }
        private bool _IsCreateDimensionView;
        public bool IsCreateDimensionView { get => _IsCreateDimensionView; set { _IsCreateDimensionView = value; OnPropertyChanged(); } }
        private bool _IsCreateDimensionSection;
        public bool IsCreateDimensionSection { get => _IsCreateDimensionSection; set { _IsCreateDimensionSection = value; OnPropertyChanged(); } }
        private bool _IsCreateDetailShop;
        public bool IsCreateDetailShop { get => _IsCreateDetailShop; set { _IsCreateDetailShop = value; OnPropertyChanged(); } }
        #endregion
        #endregion
        public WallsModel(List<Element> walls, Document document, UnitProject unit)
        {
            IsRebar = true;
            GetFamilyType(walls, document);
            GetRebarBarType(document);
            GetRebarCoverType(document);
           
            GetInfoModels(walls, document);
            GetPlanarFace(walls, document);
            GetSettingModel(walls, document, unit);
            GetStirrupsModels();
            GetAllNumberBar();
            ProgressModel = new ProgressModel(0, 0);
        }
        #region Property
        private void GetFamilyType(List<Element> walls, Document document)
        {
            FamilyType = walls[0].get_Parameter(BuiltInParameter.ELEM_FAMILY_PARAM).AsValueString();
            for (int i = 0; i < walls.Count; i++)
            {
                AllType += ((walls[i].get_Parameter(BuiltInParameter.ELEM_TYPE_PARAM).AsValueString()) + " ");
            }
        }
        private void GetRebarBarType(Document document)
        {
            RebarBarTypes = new FilteredElementCollector(document).OfClass(typeof(RebarBarType)).Cast<RebarBarType>().ToList();
            AllBars = new List<RebarBarModel>();
            foreach (var item in RebarBarTypes)
            {
                AllBars.Add(new RebarBarModel(document, item.Name, RebarBarTypes));
            }
            AllBars.Sort((x, y) => x.Diameter.CompareTo(y.Diameter));

        }
        private void GetRebarCoverType(Document document)
        {
            RebarCoverTypes = new FilteredElementCollector(document).WhereElementIsElementType().OfClass(typeof(RebarCoverType)).Cast<RebarCoverType>().ToList();
            RebarCoverTypes = RebarCoverTypes.OrderBy(x => x.CoverDistance).ToList();
            RebarCoverType = RebarCoverTypes[1];
            Cover = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, RebarCoverType.CoverDistance, false));
        }
        public void CoverChange(Document document)
        {
            Cover = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, RebarCoverType.CoverDistance, false));
        }
        private void GetAllNumberBar()
        {
            int nx = (int) (GetLengthMax() / (2 * AllBars[AllBars.Count - 1].Diameter));
            int ny = (int) (GetThicknessMax() / (2 * AllBars[AllBars.Count - 1].Diameter));
            AllNumberBarX = new List<int>();   
            for (int i = 2; i <= nx; i++)
            {
                AllNumberBarX.Add(i);
            }
            AllNumberBarY = new List<int>();
            for (int i = 2; i <= ny; i++)
            {
                AllNumberBarY.Add(i);
            }

        }

        private double GetThicknessMax()
        {
            double a = 0;
            for (int i = 0; i < InfoModels.Count; i++)
            {
                if (a < InfoModels[i].T) a = InfoModels[i].T;
            }
            return a;
        }

        private double GetLengthMax()
        {
            double a = 0;
            for (int i = 0; i < InfoModels.Count; i++)
            {
                if (a < InfoModels[i].L) a = InfoModels[i].L;
            }
            return a;
        }

        private void GetInfoModels(List<Element> walls, Document document)
        {
            InfoModels = ProccessInfoWalls.GetInfoModels(walls, document);
        }
        private void GetSettingModel(List<Element> walls, Document document, UnitProject unit)
        {
            DrawModelSection = new DrawModel(1, 100, 200, 8, 4);
            DrawModelSection.Scale= DrawModelSection.GetScaleSection(InfoModels, unit);
            DrawModel = new DrawModel(1, 100, 4720, 8, 4);
            DrawModel.GetScale(InfoModels, unit,AllBars[AllBars.Count-1]);
            SelectedIndexModel = new SelectedIndexModel(0, 0, 0);
            double b = InfoModels[0].T;
            SettingModel = new SettingModel(b / 5, b / 5, b / 2, b / 2, b / 2, b / 2, b / 2, b / 2, "Walls", "MC", walls[0], document);
        }
        private void GetPlanarFace(List<Element> columns, Document document)
        {
            PlanarFaces = ProccessInfoWalls.GetPlanarFaces(columns, document);
        }
        #endregion
        #region Bars Method
        private void GetStirrupsModels()
        {
            for (int i = 0; i < InfoModels.Count; i++)
            {
                StirrupModels.Add(new StirrupModel(InfoModels[i].NumberWall,false, AllBars[0], AllBars[0], false,0,InfoModels[i].T/2,InfoModels[i].T/2,InfoModels[i].T/2,InfoModels[i].T/2, InfoModels[i].T / 2, AllBars[0], AllBars[0], AllBars[0],false,false,InfoModels[i].T/2,InfoModels[i].T/2));
                BarMainModels.Add(new BarMainModel(InfoModels[i].NumberWall, AllBars[3]));
            }
            for (int i = 0; i < StirrupModels.Count; i++)
            {
                double l = InfoModels[i].hc - InfoModels[i].hb - InfoModels[i].zb;
                double hb = InfoModels[i].hb;
                double z = InfoModels[i].zb;
                StirrupModels[i].GetDistribute(l, hb, z);
            }
        }
        #endregion
    }
}
