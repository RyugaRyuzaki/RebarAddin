using R10_WallShear.View;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WpfCustomControls;
using R10_WallShear.LanguageModel;
namespace R10_WallShear.ViewModel
{
    public class GeometryViewModel : BaseViewModel
    {
        #region property
        private WallsModel _WallsModel;
        public WallsModel WallsModel { get => _WallsModel; set { _WallsModel = value; OnPropertyChanged(); } }
        #endregion
        #region Selected
        private InfoModel _SelectedWall;
        public InfoModel SelectedWall { get => _SelectedWall; set { _SelectedWall = value; OnPropertyChanged(); } }
        #endregion
        #region Icommand
        public ICommand LoadGeometryViewCommand { get; set; }
        public ICommand SelectionWallChangedCommand { get; set; }
        public ICommand CheckCornerCommand { get; set; }
        public ICommand L1TextChangedCommand { get; set; }
        public ICommand L2TextChangedCommand { get; set; }
        public ICommand ApplyAllWallsCommand { get; set; }
        #endregion
        private Languages _Languages;
        public Languages Languages { get { return _Languages; } set { _Languages = value; OnPropertyChanged(); } }
        public GeometryViewModel(WallsModel wallsModel, Languages languages)
        {
            #region property
            WallsModel = wallsModel;  Languages = languages;
            #endregion
            #region loadwindow
            LoadGeometryViewCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                GeometryView uc = ProccessInfoWalls.FindChild<GeometryView>(p, "GeometryUC");
                ShowWallNumberComboBox(uc);
                DrawGeometry(uc);
                p.MainCanvas.Height = WallsModel.DrawModel.Height;
                p.MainCanvas.Width = WallsModel.DrawModel.Width;
                DrawInfo(p);
                DrawSection(p);
            });
            SelectionWallChangedCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                DrawInfo(p);
                DrawSection(p);
            });
            CheckCornerCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                SelectedWall.GetL1L2();
                WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall].IsCorner = SelectedWall.IsCorner;
                WallsModel.BarMainModels[WallsModel.SelectedIndexModel.SelectedWall].IsCorner = SelectedWall.IsCorner;
                WallsModel.BarMainModels[WallsModel.SelectedIndexModel.SelectedWall].BarModels.Clear();
                WallsModel.BarMainModels[WallsModel.SelectedIndexModel.SelectedWall].BarCornerModels.Clear();
                WallsModel.GetVisibilityAllApplyBar();
                DrawSection(p);
                 
            });
            L1TextChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedWall.IsCorner; }, (p) =>
            {
                GeometryView uc = ProccessInfoWalls.FindChild<GeometryView>(p, "GeometryUC");
                if (double.TryParse(uc.L1TextBox.Text.ToString(), out double S))
                {
                    SelectedWall.L2 = SelectedWall.L - 2 * SelectedWall.L1;
                    WallsModel.BarMainModels[WallsModel.SelectedIndexModel.SelectedWall].BarModels.Clear();
                    WallsModel.BarMainModels[WallsModel.SelectedIndexModel.SelectedWall].BarCornerModels.Clear();
                    DrawSection(p);
                }
            });

            ApplyAllWallsCommand = new RelayCommand<WallShearWindow>((p) => { return ConditionApplyAllWalls(); }, (p) =>
            {
                GeometryView uc = ProccessInfoWalls.FindChild<GeometryView>(p, "GeometryUC");
                for (int i = 0; i < WallsModel.InfoModels.Count; i++)
                {
                    if (WallsModel.InfoModels[i].NumberWall!= SelectedWall.NumberWall)
                    {
                        WallsModel.InfoModels[i].IsCorner = SelectedWall.IsCorner;
                        WallsModel.InfoModels[i].GetL1L2();
                        WallsModel.StirrupModels[i].IsCorner = SelectedWall.IsCorner;
                        WallsModel.BarMainModels[i].IsCorner = SelectedWall.IsCorner;
                        WallsModel.BarMainModels[i].BarModels.Clear();
                        WallsModel.BarMainModels[i].BarCornerModels.Clear();
                    }
                   
                }
                WallsModel.GetVisibilityAllApplyBar();
                DrawInfo(p);
                DrawSection(p);
            });
            #endregion
        }

        private void ShowWallNumberComboBox(GeometryView uc)
        {
            if (WallsModel.InfoModels.Count==1)
            {
                uc.WallNumberTextBlock.Visibility = System.Windows.Visibility.Hidden;
                uc.WallNumberComboBox.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void DrawGeometry(GeometryView p)
        {
            DrawImage.DrawGeometryView(p.canvasProperty);
        }
        private void DrawInfo(WallShearWindow p)
        {
            p.MainCanvas.Children.Clear();
            DrawMainCanvas.DrawInfoWall(p.MainCanvas, WallsModel, WallsModel.SelectedIndexModel.SelectedWall);
            double top = WallsModel.DrawModel.Top - (WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall].TopPosition) / (WallsModel.DrawModel.Scale);
            if (SelectedWall.NumberWall == WallsModel.InfoModels[WallsModel.InfoModels.Count - 1].NumberWall) top -= WallsModel.AllBars[WallsModel.AllBars.Count - 1].Diameter * 20;
            p.scrollViewer.ScrollToBottom();
            p.scrollViewer.ScrollToVerticalOffset(top);
        }
        private void DrawSection(WallShearWindow p)
        {
            p.CanvasSection.Children.Clear();
            DrawMainCanvas.DrawSection(p.CanvasSection, WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall], WallsModel.DrawModelSection, WallsModel.Cover);
        }
        
        private bool ConditionApplyAllWalls()
        {
            ObservableCollection<InfoModel> infoModels = new ObservableCollection<InfoModel>(WallsModel.InfoModels.Where(x => x.IsCorner == SelectedWall.IsCorner).ToList());
            if (infoModels.Count == WallsModel.InfoModels.Count) return false;
            return true;
        }
    }
}
