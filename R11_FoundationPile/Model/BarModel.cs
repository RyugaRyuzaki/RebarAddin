using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using DSP;
using System;
using System.Linq;
using WpfCustomControls;
namespace R11_FoundationPile
{
    public class BarModel : BaseViewModel
    {

        private string _Name;
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }
        private RebarBarModel _Bar;
        public RebarBarModel Bar { get => _Bar; set { _Bar = value; OnPropertyChanged(); } }
        private double _HookLength;
        public double HookLength { get => _HookLength; set { _HookLength = value; OnPropertyChanged(); } }
        private double _Distance;
        public double Distance { get => _Distance; set { _Distance = value; OnPropertyChanged(); } }
        private int _Number;
        public int Number { get => _Number; set { _Number = value; OnPropertyChanged(); } }
        private int _Layer;
        public int Layer { get => _Layer; set { _Layer = value; OnPropertyChanged(); } }
        private bool _IsModel;
        public bool IsModel { get => _IsModel; set { _IsModel = value; OnPropertyChanged(); } }
        public RebarHookType Hook { get; set; }
        public BarModel(string name, RebarBarModel rebarBarModel, double hookLength, double distance, int number,int layer, bool isModel)
        {
            Name = name;
            Bar = rebarBarModel;
            HookLength = hookLength;
            Distance = distance;
            Number = number;
            IsModel = isModel;
            Layer = layer;
        }
        public int GetNumberBottom(double p1, double p2, double coverSide)
        {
            double sum = (Math.Abs(p1 - p2) - 2 * coverSide - Bar.Diameter);
            return (int)(sum / Distance) + 2;
        }
        public int GetNumberTop(double p1, double p2, double coverSide, BarModel mainBottom, BarModel side)
        {
            double sum = (Math.Abs(p1 - p2) - 2 * coverSide - Bar.Diameter - 2 * mainBottom.Bar.Diameter - 2 * side.Bar.Diameter);
            return (int)(sum / Distance) + 2;
        }
        public double FixDistanceBottom(double p1, double p2, double coverSide)
        {
            double sum = (Math.Abs(p1 - p2) - 2 * coverSide - Bar.Diameter);
            return Math.Round(sum / (Number - 1), 3);
        }
        public double FixDistanceTop(double p1, double p2, double coverSide, BarModel mainBottom, BarModel side)
        {
            double sum = (Math.Abs(p1 - p2) - 2 * coverSide - Bar.Diameter - 2 * mainBottom.Bar.Diameter - 2 * side.Bar.Diameter);
            return Math.Round(sum / (Number - 1), 3);
        }
        public double FixDistance(double p1, double p2, double p3, double p4, double coverSide, BarModel mainBottom, BarModel secondaryBottom, BarModel side)
        {
            switch (Name)
            {
                case "MainBottom": return FixDistanceBottom(p3, p4, coverSide);
                case "MainTop": return FixDistanceTop(p3, p4, coverSide, secondaryBottom, side);
                case "MainAddHorizontal": return FixDistanceBottom(p3, p4, coverSide);
                case "MainAddVertical": return Distance;
                case "SecondaryBottom": return FixDistanceBottom(p1, p2, coverSide);
                case "SecondaryTop": return FixDistanceTop(p1, p2, coverSide, mainBottom, side);
                case "SecondaryAddHorizontal": return FixDistanceBottom(p3, p4, coverSide);
                case "SecondaryAddVertical": return Distance;
                case "Side": return Distance;
                default: return Distance;
            }
        }
        public int FixNumber(double p1, double p2, double p3, double p4, double coverSide, BarModel mainBottom, BarModel secondaryBottom, BarModel side)
        {
            switch (Name)
            {
                case "MainBottom": return GetNumberBottom(p3, p4, coverSide);
                case "MainTop": return GetNumberTop(p3, p4, coverSide, secondaryBottom, side);
                case "MainAddHorizontal": return GetNumberBottom(p3, p4, coverSide);
                case "MainAddVertical": return Number;
                case "SecondaryBottom": return GetNumberBottom(p1, p2, coverSide);
                case "SecondaryTop": return GetNumberTop(p1, p2, coverSide, mainBottom, side);
                case "SecondaryAddHorizontal": return GetNumberBottom(p3, p4, coverSide);
                case "SecondaryAddVertical": return Number;
                case "Side": return Number;
                default: return Number;
            }
        }
        public void FixNumberBottom(double p1, double p2, double coverSide)
        {
            double sum = (Math.Abs(p1 - p2) - 2 * coverSide - Bar.Diameter);
            Number = (int)(sum / Distance) + 1;
            Distance = Math.Round(sum / (Number - 1), 3);
        }
        public void FixNumberTop(double p1, double p2, double coverSide, BarModel mainBottom, BarModel side)
        {
            double sum = (Math.Abs(p1 - p2) - 2 * coverSide - Bar.Diameter - 2 * mainBottom.Bar.Diameter - 2 * side.Bar.Diameter);
            Number = (int)(sum / Distance) + 1;
            Distance = Math.Round(sum / (Number - 1), 3);
        }
        #region Create HookLength

        private void CreateHookLengthItem(Document document, SettingModel settingModel, UnitProject unit,string locationName)
        {
            RebarHookType hook = new FilteredElementCollector(document).WhereElementIsElementType().OfClass(typeof(RebarHookType)).Cast<RebarHookType>().Where(x => x.Name.Contains("Standard")).FirstOrDefault();
            if (hook == null)
            {
                hook = settingModel.RebarHookTypes[0];
            }
            try
            {
                Hook = hook.Duplicate(Name + " " + HookLength + " " + unit.UnitName + " " + locationName) as RebarHookType;
            }
            catch
            {
                Hook = hook.Duplicate(Name + " " + HookLength + " " + unit.UnitName + " " + locationName+" Copy ") as RebarHookType;
            }
           
        }
       
        public void CreateHookLength(Document document, SettingModel settingModel, UnitProject unit, string locationName)
        {
            if (IsModel && ((Name.Contains("Bottom")) || Name.Contains("Top")))
            {
                CreateHookLengthItem(document, settingModel, unit, locationName);
               
            }
        }
        #endregion
        #region   Create Rebar
        private void CreateMainBottomBar(Document document, SettingModel settingModel, UnitProject unit)
        {
            
        }
        public void IsCreateRebar(Document document, SettingModel settingModel, UnitProject unit)
        {
            if (IsModel)
            {
                switch (Name)
                {
                    case "MainBottom": CreateMainBottomBar(document,settingModel,unit); break;
                    case "MainTop": break;
                    case "MainAddHorizontal": break;
                    case "MainAddVertical": break;
                    case "SecondaryBottom": break;
                    case "SecondaryTop": break;
                    case "SecondaryAddHorizontal": break;
                    case "SecondaryAddVertical": break;
                    case "Side": break;
                    default: break;
                }
            }
        }
        #endregion

    }
}
