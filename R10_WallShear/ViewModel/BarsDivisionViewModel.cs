using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCustomControls;
using R10_WallShear.LanguageModel;
namespace R10_WallShear.ViewModel
{
    public class BarsDivisionViewModel  :BaseViewModel
    {
        #region property
        public Document Doc;
        private WallsModel _WallsModel;
        public WallsModel WallsModel { get => _WallsModel; set { _WallsModel = value; OnPropertyChanged(); } }
        #endregion
        private Languages _Languages;
        public Languages Languages { get { return _Languages; } set { _Languages = value; OnPropertyChanged(); } }
        public BarsDivisionViewModel(Document doc, WallsModel wallsModel, Languages languages)
        {
            #region property
            Doc = doc;
            WallsModel = wallsModel; Languages = languages;
            #endregion
        }
    }
}
