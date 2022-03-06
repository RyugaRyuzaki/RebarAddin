using Autodesk.Revit.DB;
using R10_WallShear.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfCustomControls;
using R10_WallShear.LanguageModel;

namespace R10_WallShear.ViewModel 
{
    
    public class StirrupsViewModel:BaseViewModel
    {
        #region property
        public Document Doc;
        private WallsModel _WallsModel;
        public WallsModel WallsModel { get => _WallsModel; set { _WallsModel = value; OnPropertyChanged(); } }
        #endregion
        #region Selected
        private StirrupModel _SelectedWall;
        public StirrupModel SelectedWall { get => _SelectedWall; set { _SelectedWall = value; OnPropertyChanged(); } }
        #endregion
        #region Distribute Type
        private List<string> _DistributeType;
        public List<string> DistributeType { get { if (_DistributeType == null) { _DistributeType = new List<string>() { "High Column", "High Column /4", "High Column /6", "High Column /8" }; } return _DistributeType; } set { _DistributeType = value; OnPropertyChanged(); } }
        #endregion
        #region Icommand
        public ICommand LoadStirrupsViewCommand { get; set; }
        public ICommand SelectionStirrupsChangedCommand { get; set; }
        public ICommand ApplyAllWallsCommand { get; set; }
        public ICommand BarSelectionChangedCommand { get; set; }
        public ICommand BarCornerSelectionChangedCommand { get; set; }
        public ICommand SelectionCoverChangedCommand { get; set; }
        public ICommand STextChangedCommand { get; set; }
        public ICommand S1TextChangedCommand { get; set; }
        public ICommand S2TextChangedCommand { get; set; }
        public ICommand TiesUpCommand { get; set; }
        public ICommand SelectionDistributeChangedCommand { get; set; }
        #endregion
        private Languages _Languages;
        public Languages Languages { get { return _Languages; } set { _Languages = value; OnPropertyChanged(); } }
        public StirrupsViewModel(Document doc, WallsModel wallsModel, Languages languages)
        {
            #region property
            Doc = doc;
            WallsModel = wallsModel; Languages= languages;
            #endregion
            #region
            LoadStirrupsViewCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                p.MainCanvas.Height = WallsModel.DrawModel.Height;
                p.MainCanvas.Width = WallsModel.DrawModel.Width;
                StirrupsView uc = ProccessInfoWalls.FindChild<StirrupsView>(p, "StirrupsUC");
                //ShowWallNumberComboBox(uc);
                SetDistributeType(uc);
                DrawInfo(p);
                DrawSection(p);
                ShowCorner(uc);
                DrawDistribute(uc);
            });
            SelectionStirrupsChangedCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                StirrupsView uc = ProccessInfoWalls.FindChild<StirrupsView>(p, "StirrupsUC");
                SetDistributeType(uc);
                ShowCorner(uc);
                DrawInfo(p);
                DrawSection(p);
            });
            ApplyAllWallsCommand = new RelayCommand<WallShearWindow>((p) => { return ConditionApplyAllWalls(); }, (p) =>
            {
                StirrupsView uc = ProccessInfoWalls.FindChild<StirrupsView>(p, "StirrupsUC");
                for (int i = 0; i < WallsModel.StirrupModels.Count; i++)
                {
                    WallsModel.StirrupModels[i].BarS = SelectedWall.BarS;
                    WallsModel.StirrupModels[i].IsTiesUp = SelectedWall.IsTiesUp;
                    WallsModel.StirrupModels[i].S = SelectedWall.S;
                    WallsModel.StirrupModels[i].S1 = SelectedWall.S1;
                    WallsModel.StirrupModels[i].S2 = SelectedWall.S2;
                    WallsModel.StirrupModels[i].TypeDis = SelectedWall.TypeDis;
                    double l = WallsModel.InfoModels[i].hc - WallsModel.InfoModels[i].hb - WallsModel.InfoModels[i].zb;
                    double hb = WallsModel.InfoModels[i].hb;
                    double z = WallsModel.InfoModels[i].zb;
                    WallsModel.StirrupModels[i].GetDistribute(l, hb, z);
                    if (WallsModel.BarMainModels[i].BarModels.Count != 0)
                    {
                        WallsModel.BarMainModels[i].RefreshX0Y0BarModels(WallsModel.InfoModels[i], WallsModel.Cover, WallsModel.StirrupModels[i].BarS.Diameter);
                    }
                    if (WallsModel.BarMainModels[i].BarCornerModels.Count != 0)
                    {
                        WallsModel.BarMainModels[i].RefreshX0Y0BarCornerModels(WallsModel.InfoModels[i], WallsModel.Cover, Math.Max(WallsModel.StirrupModels[i].BarSCorner.Diameter, WallsModel.StirrupModels[i].BarS.Diameter));
                    }
                }
                DrawInfo(p);
                DrawSection(p);
            });
            BarSelectionChangedCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                StirrupsView uc = ProccessInfoWalls.FindChild<StirrupsView>(p, "StirrupsUC");
                if (WallsModel.BarMainModels[WallsModel.SelectedIndexModel.SelectedWall].BarModels.Count != 0)
                {

                    WallsModel.BarMainModels[WallsModel.SelectedIndexModel.SelectedWall].RefreshX0Y0BarModels(WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall], WallsModel.Cover, WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall].BarS.Diameter);
                }
                DrawInfo(p);
                DrawSection(p);
                
            });
            BarCornerSelectionChangedCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                StirrupsView uc = ProccessInfoWalls.FindChild<StirrupsView>(p, "StirrupsUC");
                if (WallsModel.BarMainModels[WallsModel.SelectedIndexModel.SelectedWall].BarCornerModels.Count != 0)
                {
                    WallsModel.BarMainModels[WallsModel.SelectedIndexModel.SelectedWall].RefreshX0Y0BarCornerModels(WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall], WallsModel.Cover, Math.Max(WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall].BarSCorner.Diameter, WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall].BarS.Diameter));
                }
                DrawInfo(p);
                DrawSection(p);
                
            });
            SelectionCoverChangedCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                StirrupsView uc = ProccessInfoWalls.FindChild<StirrupsView>(p, "StirrupsUC");
                WallsModel.CoverChange(Doc);
                for (int i = 0; i < WallsModel.StirrupModels.Count; i++)
                {
                    if (WallsModel.BarMainModels[i].BarModels.Count != 0)
                    {
                        WallsModel.BarMainModels[i].RefreshX0Y0BarModels(WallsModel.InfoModels[i], WallsModel.Cover, WallsModel.StirrupModels[i].BarS.Diameter);
                    }
                    if (WallsModel.BarMainModels[i].BarCornerModels.Count != 0)
                    {
                        WallsModel.BarMainModels[i].RefreshX0Y0BarCornerModels(WallsModel.InfoModels[i], WallsModel.Cover,Math.Max( WallsModel.StirrupModels[i].BarSCorner.Diameter, WallsModel.StirrupModels[i].BarS.Diameter));
                    }
                }
                DrawInfo(p);
                DrawSection(p);
            });
            STextChangedCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                StirrupsView uc = ProccessInfoWalls.FindChild<StirrupsView>(p, "StirrupsUC");
                if (double.TryParse(uc.STextBox.Text.ToString(), out double S))
                {
                    DrawInfo(p);
                }
            });
            S1TextChangedCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                StirrupsView uc = ProccessInfoWalls.FindChild<StirrupsView>(p, "StirrupsUC");
                if (double.TryParse(uc.S1TextBox.Text.ToString(), out double S))
                {
                    DrawInfo(p);
                }
            });
            S2TextChangedCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                StirrupsView uc = ProccessInfoWalls.FindChild<StirrupsView>(p, "StirrupsUC");
                if (double.TryParse(uc.S2TextBox.Text.ToString(), out double S))
                {
                    DrawInfo(p);
                }
            });
            TiesUpCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                StirrupsView uc = ProccessInfoWalls.FindChild<StirrupsView>(p, "StirrupsUC");
                SetDistributeType(uc);
                DrawDistribute(uc);
                DrawInfo(p);
            });
            SelectionDistributeChangedCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                StirrupsView uc = ProccessInfoWalls.FindChild<StirrupsView>(p, "StirrupsUC");
                SetDistributeType(uc);
                DrawDistribute(uc);
                DrawInfo(p);
            });
            #endregion
        }


        #region Method
        private void ShowWallNumberComboBox(StirrupsView uc)
        {
            if (WallsModel.InfoModels.Count == 1)
            {
                uc.WallNumberTextBlock.Visibility = System.Windows.Visibility.Hidden;
                uc.WallNumberComboBox.Visibility = System.Windows.Visibility.Hidden;
            }
        }
        private bool ConditionApplyAllWalls()
        {
            ObservableCollection<StirrupModel> stirrupModels = new ObservableCollection<StirrupModel>(WallsModel.StirrupModels.Where(x=>x.BarS.Diameter==SelectedWall.BarS.Diameter&&x.IsTiesUp==SelectedWall.IsTiesUp&&x.TypeDis==SelectedWall.TypeDis).ToList());
            if (stirrupModels.Count == WallsModel.StirrupModels.Count) return false;
            return true;
        }
        private void ShowCorner(StirrupsView p)
        {
            if (SelectedWall.IsCorner)
            {
                p.CornerGrid.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                p.CornerGrid.Visibility = System.Windows.Visibility.Hidden;
            }
        }
        private void SetDistributeType(StirrupsView p)
        {
            double l = WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall].hc - WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall].hb - WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall].zb;
            double hb = WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall].hb;
            double z = WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall].zb;
            SelectedWall.GetDistribute(l, hb, z);
            if (SelectedWall.TypeDis == 0)
            {
                p.STextBlock.Visibility = System.Windows.Visibility.Visible;
                p.STextBox.Visibility = System.Windows.Visibility.Visible;
                p.STextBlockUnit.Visibility = System.Windows.Visibility.Visible;
                p.S1TextBlock.Visibility = System.Windows.Visibility.Hidden;
                p.S1TextBox.Visibility = System.Windows.Visibility.Hidden;
                p.S1TextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                p.S2TextBlock.Visibility = System.Windows.Visibility.Hidden;
                p.S2TextBox.Visibility = System.Windows.Visibility.Hidden;
                p.S2TextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                p.STextBlock.Visibility = System.Windows.Visibility.Hidden;
                p.STextBox.Visibility = System.Windows.Visibility.Hidden;
                p.STextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                p.S1TextBlock.Visibility = System.Windows.Visibility.Visible;
                p.S1TextBox.Visibility = System.Windows.Visibility.Visible;
                p.S1TextBlockUnit.Visibility = System.Windows.Visibility.Visible;
                p.S2TextBlock.Visibility = System.Windows.Visibility.Visible;
                p.S2TextBox.Visibility = System.Windows.Visibility.Visible;
                p.S2TextBlockUnit.Visibility = System.Windows.Visibility.Visible;
            }
        }
        #endregion
        #region Draw
        private void DrawInfo(WallShearWindow p)
        {
            p.MainCanvas.Children.Clear();
            DrawMainCanvas.DrawInfoWall(p.MainCanvas, WallsModel, WallsModel.SelectedIndexModel.SelectedWall);
            DrawMainCanvas.DrawStirrup(p.MainCanvas, WallsModel, SelectedWall.NumberWall - 1);
            double top = WallsModel.DrawModel.Top - (WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall].TopPosition) / (WallsModel.DrawModel.Scale);
            if (SelectedWall.NumberWall == WallsModel.InfoModels[WallsModel.InfoModels.Count - 1].NumberWall) top -= WallsModel.AllBars[WallsModel.AllBars.Count - 1].Diameter * 20;
            p.scrollViewer.ScrollToBottom();
            p.scrollViewer.ScrollToVerticalOffset(top);
        }
        private void DrawSection(WallShearWindow p)
        {
            p.CanvasSection.Children.Clear();
            double d = (WallsModel.BarMainModels[WallsModel.SelectedIndexModel.SelectedWall].BarModels.Count == 0) ? WallsModel.AllBars[3].Diameter : WallsModel.BarMainModels[WallsModel.SelectedIndexModel.SelectedWall].Bar.Diameter;
            double dc = (WallsModel.BarMainModels[WallsModel.SelectedIndexModel.SelectedWall].BarModels.Count == 0) ? WallsModel.AllBars[3].Diameter : WallsModel.BarMainModels[WallsModel.SelectedIndexModel.SelectedWall].BarCorner.Diameter;
            DrawMainCanvas.DrawStirrupsAndSection(p.CanvasSection, WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall],SelectedWall, WallsModel.BarMainModels[WallsModel.SelectedIndexModel.SelectedWall], WallsModel.DrawModelSection, WallsModel.Cover,d,dc);
        }
        private void DrawDistribute(StirrupsView p)
        {
            DrawImage.DrawDistribute(p.CanvasDistribute, SelectedWall.TypeDis, SelectedWall.IsTiesUp);
        }
        #endregion
    }
}
