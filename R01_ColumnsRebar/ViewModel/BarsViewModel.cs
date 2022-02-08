

using Autodesk.Revit.DB;
using R01_ColumnsRebar.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Media;
using WpfCustomControls;
namespace R01_ColumnsRebar.ViewModel
{
    public class BarsViewModel:BaseViewModel
    {
        #region property
        public Document Doc;
        private ColumnsModel _ColumnsModel;
        public ColumnsModel ColumnsModel { get { return _ColumnsModel; } set { _ColumnsModel = value; OnPropertyChanged(); } }
        #endregion
        #region Selected
        private BarMainModel _SelectedColumn;
        public BarMainModel SelectedColumn { get => _SelectedColumn; set { _SelectedColumn = value; OnPropertyChanged();} }
        private BarModel _SelectedBar;
        public BarModel SelectedBar { get => _SelectedBar; set { _SelectedBar = value; OnPropertyChanged(); } }
        private RebarBarModel _SelectedRebarBarModel;
        public RebarBarModel SelectedRebarBarModel { get { return _SelectedRebarBarModel; } set { _SelectedRebarBarModel = value; OnPropertyChanged(); } }
        private bool _IsLock;
        public bool IsLock { get => _IsLock; set { _IsLock = value; OnPropertyChanged(); } }
        #endregion
        #region SplitOverlap
        private List<double> _SplitOverlap;
        public List<double> SplitOverlap { get { if (_SplitOverlap == null) _SplitOverlap = new List<double>() { 50,100}; return _SplitOverlap; } set { _SplitOverlap = value; OnPropertyChanged(); } }
        #endregion
        #region Command
        public ICommand LoadBarsViewCommand { get; set; }
        public ICommand ApplyCommand { get; set; }
        public ICommand ModifyCommand { get; set; }
        public ICommand SelectionColumnsChangedCommand { get; set; }
        public ICommand SelectionBarChangedCommand { get; set; }
        public ICommand PreviewTextInputCommand { get; set; }
        #endregion
        public BarsViewModel(Document doc,ColumnsModel columnsModel)
        {
            #region property
            Doc = doc;
            ColumnsModel = columnsModel;
            SelectedRebarBarModel = ColumnsModel.AllBars[3];
            IsLock = true;
            #endregion
            #region LoadCommand
            LoadBarsViewCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                p.MainCanvas.Height = ColumnsModel.DrawModel.Height;
                p.MainCanvas.Width = ColumnsModel.DrawModel.Width;
                p.scrollViewer.ScrollToBottom();
                p.scrollViewer.ScrollToLeftEnd();
                IsLock = SelectedColumn.BarModels.Count == 0;
                BarsView uc = ProccessInfoClumns.FindChild<BarsView>(p, "BarsUC");
                //if (ColumnsModel.InfoModels.Count == 1) uc.ApplyAllButton.Visibility = System.Windows.Visibility.Hidden;
                ShowSelectionBars(uc);
                DrawSection(uc);
                DrawMain(p);
            });
            ApplyCommand = new RelayCommand<ColumnsWindow>((p) => { return ConditionApply(); }, (p) =>
            {
                SelectedColumn.GetBarModels(ColumnsModel.SectionStyle, ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn], ColumnsModel.Cover, ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn].BarS.Diameter);
                SelectedColumn.GetLocationBarModels(ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn], ColumnsModel.Cover);
                InfoModel infoModelUp = ColumnsModel.InfoModels.Where(x => x.NumberColumn == ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].NumberColumn + 1).FirstOrDefault();
                //BarMainModel barMainModelUp = ColumnsModel.BarMainModels.Where(x => x.NumberColumn == SelectedColumn.NumberColumn + 1).FirstOrDefault();
                double ds = ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn].BarS.Diameter;
                double dsUp = (infoModelUp == null) ? (ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn].BarS.Diameter) : (ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn+1].BarS.Diameter);
                SelectedColumn.RefreshLocationBarModels(ColumnsModel.SectionStyle, ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn], ColumnsModel.Cover, ds,dsUp, infoModelUp);
                DrawMain(p);
                ColumnsModel.SelectedIndexModel.SelectedMainBar = 0;
                IsLock = SelectedColumn.BarModels.Count == 0;
                BarsView uc = ProccessInfoClumns.FindChild<BarsView>(p, "BarsUC");
                DrawSection(uc);
            });
            ModifyCommand = new RelayCommand<ColumnsWindow>((p) => { return SelectedColumn.BarModels.Count != 0; }, (p) =>
            {
                SelectedColumn.BarModels.Clear();
                SelectedColumn.AddBarModels.Clear();
                
                IsLock = SelectedColumn.BarModels.Count == 0;
                BarsView uc = ProccessInfoClumns.FindChild<BarsView>(p, "BarsUC");
                DrawSection(uc);
                
            });
            SelectionColumnsChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                IsLock = SelectedColumn.BarModels.Count == 0;
                DrawMain(p);
                BarsView uc = ProccessInfoClumns.FindChild<BarsView>(p, "BarsUC");
                DrawSection(uc);
            });
            SelectionBarChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                BarsView uc = ProccessInfoClumns.FindChild<BarsView>(p, "BarsUC");
                DrawSection(uc);
                DrawMain(p);
            });
            PreviewTextInputCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                BarsView uc = ProccessInfoClumns.FindChild<BarsView>(p, "BarsUC");
                uc.OverlapTextBox.Foreground = double.TryParse(uc.OverlapTextBox.Text.ToString(), out double a) ? Brushes.Black : Brushes.Red;
                uc.OverlapTextBox.Text = Regex.Replace(uc.OverlapTextBox.Text, "[^0-9.-]+", "");
            });
            #endregion
        }
        #region Draw
        private void DrawMain(ColumnsWindow p)
        {
            p.MainCanvas.Children.Clear();
            DrawMainCanvas.DrawInfoColumns(p.MainCanvas, ColumnsModel, ColumnsModel.SelectedIndexModel.SelectedColumn);
            DrawMainCanvas.DrawBarMains(p.MainCanvas, ColumnsModel,ColumnsModel.SelectedIndexModel.SelectedColumn, (SelectedBar!=null)?SelectedBar.BarNumber:1000,1000);
            double top = ColumnsModel.DrawModel.Top - (ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].TopPosition) / (ColumnsModel.DrawModel.Scale);
            p.scrollViewer.ScrollToBottom();
            p.scrollViewer.ScrollToVerticalOffset(top);
        }
        #endregion
        #region Condition 
        private bool ConditionApply()
        {
            if (SelectedColumn.BarModels.Count!=0) return false;
            if (ColumnsModel.SectionStyle == ErrorColumns.SectionStyle.RECTANGLE)
            {
                double ds = ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn].BarS.Diameter;
                double c = ColumnsModel.Cover;
                double d = SelectedRebarBarModel.Diameter;
                double tmin = ColumnsModel.SettingModel.tmin;
                double nx = SelectedColumn.nx, ny = SelectedColumn.ny;
                double b = ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].b;
                double h = ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].h;
                double tx = (b - 2 * c - 2 * ds - d) / (nx - 1)-d;
                double ty = (h - 2 * c - 2 * ds - d) / (ny - 1)-d;
                if (tx < tmin) return false;
                if (ty < tmin) return false;
            }
            else
            {
                double ds = ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn].BarS.Diameter;
                double c = ColumnsModel.Cover;
                double d = SelectedRebarBarModel.Diameter;
                double D = ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].D-2*c-2*ds-d;
                double tmin = ColumnsModel.SettingModel.tmin;
                double t = (D * Math.PI - SelectedColumn.nd * d) / (SelectedColumn.nd - 1);
                if (t < tmin) return false;
            }
            return true;
        }
        #endregion
        #region showproperety
        private void ShowSelectionBars(BarsView p)
        {
            if (ColumnsModel.SectionStyle==ErrorColumns.SectionStyle.RECTANGLE)
            {
                p.BarXTextBlock.Visibility = System.Windows.Visibility.Visible;
                p.BarXComboBox.Visibility = System.Windows.Visibility.Visible;
                p.BarYTextBlock.Visibility = System.Windows.Visibility.Visible;
                p.BarYComboBox.Visibility = System.Windows.Visibility.Visible;
                p.BarDTextBlock.Visibility = System.Windows.Visibility.Hidden;
                p.BarDComboBox.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                p.BarXTextBlock.Visibility = System.Windows.Visibility.Hidden;
                p.BarXComboBox.Visibility = System.Windows.Visibility.Hidden;
                p.BarYTextBlock.Visibility = System.Windows.Visibility.Hidden;
                p.BarYComboBox.Visibility = System.Windows.Visibility.Hidden;
                p.BarDTextBlock.Visibility = System.Windows.Visibility.Visible;
                p.BarDComboBox.Visibility = System.Windows.Visibility.Visible;
            }
        }
        #endregion
        #region Draw
        private void Getx0y0Reactangle(int i,out double x0,out double y0)
        {
            x0 = 0;y0 = 0;
            if (i==0)
            {
                x0 = 11;
            }
            if (i<SelectedColumn.nx)
            {
                y0 = -11;
            }
            if (i==SelectedColumn.nx-1)
            {
                x0 = -11;
            }
            if (i>=SelectedColumn.nx&&i<=SelectedColumn.nx+SelectedColumn.ny-2)
            {
                x0 = -11;
                if (i== SelectedColumn.nx + SelectedColumn.ny - 2)
                {
                    y0 = 15;
                }
            }
            if (i > SelectedColumn.nx + SelectedColumn.ny - 2&&i<= SelectedColumn.nx + SelectedColumn.ny - 2+SelectedColumn.nx-1)
            {
                y0 = 15;
                if (i== SelectedColumn.nx + SelectedColumn.ny - 2 + SelectedColumn.nx - 1)
                {
                    x0 = 11;
                }
            }
            if (i > SelectedColumn.nx + SelectedColumn.ny - 2 + SelectedColumn.nx - 1)
            {
                x0 = 11;
            }
        }
        private void DrawSection(BarsView p)
        {
            double d = (ColumnsModel.BarMainModels[ColumnsModel.SelectedIndexModel.SelectedColumn].BarModels.Count == 0) ? ColumnsModel.AllBars[3].Diameter : ColumnsModel.BarMainModels[ColumnsModel.SelectedIndexModel.SelectedColumn].BarModels[0].Bar.Diameter;
            DrawMainCanvas.DrawSectionAndStirrups(p.CanvasSection, ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn], ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn], ColumnsModel.DrawModelSection, ColumnsModel.SectionStyle, ColumnsModel.Cover, d, ColumnsModel.DrawModelSection.ColorStirrup);
            if (SelectedColumn.BarModels.Count!=0)
            {
                SolidColorBrush solidColorBrush = ColumnsModel.DrawModelSection.ColorMainBar;
                if (ColumnsModel.SectionStyle == ErrorColumns.SectionStyle.RECTANGLE)
                {
                    
                    for (int i = 0; i < SelectedColumn.BarModels.Count; i++)
                    {
                        if (i==ColumnsModel.SelectedIndexModel.SelectedMainBar)
                        {
                            solidColorBrush = ColumnsModel.DrawModelSection.ColorMainBarChoose;
                        }
                        else
                        {
                            solidColorBrush = ColumnsModel.DrawModelSection.ColorMainBar;
                        }
                        double left = ColumnsModel.DrawModelSection.Left + SelectedColumn.BarModels[i].X0 / ColumnsModel.DrawModelSection.Scale;
                        double top = ColumnsModel.DrawModelSection.Top - SelectedColumn.BarModels[i].Y0 / ColumnsModel.DrawModelSection.Scale;
                        DrawImage.DrawOneBarSection(p.CanvasSection, left, top, ColumnsModel.DrawModelSection.Scale, SelectedColumn.Bar.Diameter, solidColorBrush);
                        double x0 = 0;
                        double y0 = 0;

                        Getx0y0Reactangle(i, out x0, out y0);
                        DrawImage.DrawTextOneBarSection(p.CanvasSection, left+x0 , top+y0 , SelectedColumn.BarModels[i].BarNumber, solidColorBrush);
                    }
                }
                else
                {
                    for (int i = 0; i < SelectedColumn.BarModels.Count; i++)
                    {
                        if (i == ColumnsModel.SelectedIndexModel.SelectedMainBar)
                        {
                            solidColorBrush = ColumnsModel.DrawModelSection.ColorMainBarChoose;
                        }
                        else
                        {
                            solidColorBrush = ColumnsModel.DrawModelSection.ColorMainBar;
                        }
                        double left = ColumnsModel.DrawModelSection.Left + SelectedColumn.BarModels[i].X0 / ColumnsModel.DrawModelSection.Scale;
                        double top = ColumnsModel.DrawModelSection.Top - SelectedColumn.BarModels[i].Y0 / ColumnsModel.DrawModelSection.Scale;
                        DrawImage.DrawOneBarSection(p.CanvasSection, left, top, ColumnsModel.DrawModelSection.Scale, SelectedColumn.Bar.Diameter, solidColorBrush);
                        
                        double x0 = (SelectedColumn.BarModels[i].X0 >= 0) ? -11 : 11;
                        double y0 = (SelectedColumn.BarModels[i].Y0 >= 0) ? 11 : -11;
                        DrawImage.DrawTextOneBarSection(p.CanvasSection, left+x0, top+y0, SelectedColumn.BarModels[i].BarNumber, solidColorBrush);
                    }
                }
                
            }
        }
        #endregion
    }
}
