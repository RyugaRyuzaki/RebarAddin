using Autodesk.Revit.DB;
using R10_WallShear.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace R10_WallShear.ViewModel
{
    public class SettingViewModel :BaseViewModel
    {
        #region property
        public Document Doc;
        private WallsModel _WallsModel;
        public WallsModel WallsModel { get { return _WallsModel; } set { _WallsModel = value; OnPropertyChanged(); } }
        #endregion
        #region Command
        public ICommand LoadSettingViewCommand { get; set; }
        public ICommand ColumnsNameChangedCommand { get; set; }
        public ICommand PrefixLevelCommand { get; set; }
        public ICommand PrefixSectionChangedCommand { get; set; }
        public ICommand SelectionHookChangedCommand { get; set; }
        #endregion
        public SettingViewModel(Document doc, WallsModel wallsModel)
        {
            #region property
            Doc = doc;
            WallsModel = wallsModel;
            #endregion
            #region Load
            LoadSettingViewCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                p.MainCanvas.Height = WallsModel.DrawModel.Height;
                p.MainCanvas.Width = WallsModel.DrawModel.Width;
                p.scrollViewer.ScrollToBottom();
                p.scrollViewer.ScrollToLeftEnd();
                DrawInfo(p);
                DrawSection(p);
                SettingView uc = ProccessInfoWalls.FindChild<SettingView>(p, "SettingUC");
                DrawCanvas1(uc);
                DrawCanvas2(uc);
                DrawCanvas3(uc);
                DrawCanvas4(uc);
            });
            #endregion
        }
        #region Draw
        private void DrawInfo(WallShearWindow p)
        {
            p.MainCanvas.Children.Clear();
            DrawMainCanvas.DrawInfoColumns(p.MainCanvas, WallsModel, WallsModel.SelectedIndexModel.SelectedWall);
        }
        private void DrawSection(WallShearWindow p)
        {
            p.CanvasSection.Children.Clear();
            DrawMainCanvas.DrawSection(p.CanvasSection,WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall],WallsModel.DrawModelSection,WallsModel.Cover);
        }
        private void DrawCanvas1(SettingView p)
        {
            DrawImage.DrawCanvas1SettingView(p.canvas1, WallsModel.DrawModel.ColorMainBarChoose);
        }

        private void DrawCanvas2(SettingView p)
        {
            DrawImage.DrawCanvas2SettingView(p.canvas2, WallsModel.DrawModel.ColorMainBarChoose);

        }
        private void DrawCanvas3(SettingView p)
        {
            p.canvas3.Children.Clear();
            DrawImage.DrawColumnsDetail(p.canvas3);
            DrawImage.DrawColumnsDetail1(p.canvas3);
        }
        private void DrawCanvas4(SettingView p)
        {
            p.canvas4.Children.Clear();
            double hook = WallsModel.SettingModel.SelectedHook.get_Parameter(BuiltInParameter.REBAR_HOOK_ANGLE).AsDouble();
            DrawImage.DrawLayerMainBar(p.canvas4, 50, 40, 1, 200, 5, 3, 14, 3, WallsModel.DrawModel.ColorMainBarChoose);
            DrawImage.DrawHook(p.canvas4, 50, 40, 1, 200, 5, 3, 14, hook, WallsModel.DrawModel.ColorStirrupChoose);
        }
        #endregion
    }
}
