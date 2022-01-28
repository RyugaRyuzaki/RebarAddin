

using Autodesk.Revit.DB;
using R01_ColumnsRebar.View;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace R01_ColumnsRebar.ViewModel
{
    public class StirrupsViewModel:BaseViewModel
    {
        #region property
        public Document Doc;
        private ColumnsModel _ColumnsModel;
        public ColumnsModel ColumnsModel { get { return _ColumnsModel; } set { _ColumnsModel = value; OnPropertyChanged(); } }
        #endregion
        #region Selected
        private StirrupModel _SelectedStirrupModel;
        public StirrupModel SelectedStirrupModel { get { return _SelectedStirrupModel; } set { _SelectedStirrupModel = value; OnPropertyChanged(); } }
        
        #endregion
        #region Distribute Type
        private List<string> _DistributeType;
        public List<string> DistributeType { get { if (_DistributeType==null) { _DistributeType = new List<string>() {"High Column", "High Column /4",  "High Column /6", "High Column /8" }; } return _DistributeType; } set { _DistributeType = value; OnPropertyChanged(); } }
        #endregion
        #region Command
        public ICommand LoadStirrupsViewCommand { get; set; }
        public ICommand SelectionStirrupsChangedCommand { get; set; }
        public ICommand SelectionDistributeChangedCommand { get; set; }
        public ICommand SelectionCoverChangedCommand { get; set; }
        public ICommand BarSelectionChangedCommand { get; set; }
        public ICommand TiesUpCommand { get; set; }
        public ICommand ApplyAllColumnCommand { get; set; }
        public ICommand STextChangedCommand { get; set; }
        public ICommand S1TextChangedCommand { get; set; }
        public ICommand S2TextChangedCommand { get; set; }
        #endregion
        public StirrupsViewModel(Document doc, ColumnsModel columnsModel)
        {
            #region property
            Doc = doc;
            ColumnsModel = columnsModel;
            
            #endregion
            #region LoadCommand
            LoadStirrupsViewCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                p.MainCanvas.Height = ColumnsModel.DrawModel.Height;
                p.MainCanvas.Width = ColumnsModel.DrawModel.Width;
                p.scrollViewer.ScrollToBottom();
                p.scrollViewer.ScrollToLeftEnd();
                StirrupsView uc = ProccessInfoClumns.FindChild<StirrupsView>(p, "StirrupsUC");
                if (ColumnsModel.InfoModels.Count == 1) uc.ApplyAllButton.Visibility = System.Windows.Visibility.Hidden;
                SetDistributeType(uc);
                DrawStirrup(uc);
                DrawDistribute(uc);
                DrawMain(p);
            });
            SelectionStirrupsChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                StirrupsView uc = ProccessInfoClumns.FindChild<StirrupsView>(p, "StirrupsUC");
                SetDistributeType(uc);
                DrawStirrup(uc);
                DrawMain(p);
            });
            SelectionDistributeChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                StirrupsView uc = ProccessInfoClumns.FindChild<StirrupsView>(p, "StirrupsUC");
                SetDistributeType(uc);
                DrawDistribute(uc);
               
                DrawMain(p);
            });
            SelectionCoverChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                ColumnsModel.CoverChange(Doc);
                StirrupsView uc = ProccessInfoClumns.FindChild<StirrupsView>(p, "StirrupsUC");
                SetDistributeType(uc);
                DrawStirrup(uc);
                for (int i = 0; i < ColumnsModel.StirrupModels.Count; i++)
                {
                    if (ColumnsModel.BarMainModels[i].BarModels.Count != 0)
                    {
                        InfoModel infoModelUp = ColumnsModel.InfoModels.Where(x => x.NumberColumn == ColumnsModel.BarMainModels[i].NumberColumn + 1).FirstOrDefault();
                        StirrupModel stirrupModelUp = ColumnsModel.StirrupModels.Where(x => x.NumberColumn == ColumnsModel.BarMainModels[i].NumberColumn + 1).FirstOrDefault();
                        BarMainModel barMainModelUp = ColumnsModel.BarMainModels.Where(x => x.NumberColumn == ColumnsModel.BarMainModels[i].NumberColumn + 1).FirstOrDefault();
                        double ds = ColumnsModel.StirrupModels[i].BarS.Diameter;
                        double dsUp = (infoModelUp == null) ? (ColumnsModel.StirrupModels[i].BarS.Diameter) : (stirrupModelUp.BarS.Diameter);
                        ColumnsModel.BarMainModels[i].RefreshX0Y0BarModels(ColumnsModel.SectionStyle, ColumnsModel.InfoModels[i], ColumnsModel.Cover,ds);
                        ColumnsModel.BarMainModels[i].RefreshLocationBarModels(ColumnsModel.SectionStyle, ColumnsModel.InfoModels[i], ColumnsModel.Cover, ds,dsUp, infoModelUp);
                    }
                    if (ColumnsModel.BarMainModels[i].AddBarModels.Count != 0)
                    {
                        InfoModel infoModelUp = ColumnsModel.InfoModels.Where(x => x.NumberColumn == ColumnsModel.BarMainModels[i].NumberColumn + 1).FirstOrDefault();
                        StirrupModel stirrupModelUp = ColumnsModel.StirrupModels.Where(x => x.NumberColumn == ColumnsModel.BarMainModels[i].NumberColumn + 1).FirstOrDefault();
                        InfoModel infoModel = (infoModelUp == null) ? ColumnsModel.InfoModels[i] : infoModelUp;
                        double topPosition = ColumnsModel.InfoModels[i].TopPosition;
                        double ds = (infoModelUp == null) ? (ColumnsModel.StirrupModels[i].BarS.Diameter) : (stirrupModelUp.BarS.Diameter);
                        ColumnsModel.BarMainModels[i].RefreshX0Y0AddBarModels(ColumnsModel.SectionStyle, infoModel, ColumnsModel.Cover, ds);
                        ColumnsModel.BarMainModels[i].RefreshLocationAddBarModels(ColumnsModel.SectionStyle, infoModel, topPosition, ColumnsModel.Cover, ds);
                    }
                }
                
                DrawMain(p);
            });
            BarSelectionChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                StirrupsView uc = ProccessInfoClumns.FindChild<StirrupsView>(p, "StirrupsUC");
                DrawStirrup(uc);
                if (ColumnsModel.BarMainModels[ColumnsModel.SelectedIndexModel.SelectedColumn].BarModels.Count!=0)
                {
                    RefreshValueMainBar();
                }
                if (ColumnsModel.BarMainModels[ColumnsModel.SelectedIndexModel.SelectedColumn].AddBarModels.Count != 0)
                {
                    RefreshValueAddBar();
                }
            });
            TiesUpCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                StirrupsView uc = ProccessInfoClumns.FindChild<StirrupsView>(p, "StirrupsUC");
                SetDistributeType(uc);
                DrawDistribute(uc);
                DrawMain(p);
            });
            ApplyAllColumnCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                for (int i = 0; i < ColumnsModel.StirrupModels.Count; i++)
                {
                    ColumnsModel.StirrupModels[i].BarS = SelectedStirrupModel.BarS;
                    ColumnsModel.StirrupModels[i].IsTiesUp = SelectedStirrupModel.IsTiesUp;
                    ColumnsModel.StirrupModels[i].S = SelectedStirrupModel.S;
                    ColumnsModel.StirrupModels[i].S1 = SelectedStirrupModel.S1;
                    ColumnsModel.StirrupModels[i].S2 = SelectedStirrupModel.S2;
                    ColumnsModel.StirrupModels[i].TypeDis = SelectedStirrupModel.TypeDis;
                    double l = ColumnsModel.InfoModels[i].hc - ColumnsModel.InfoModels[i].hb - ColumnsModel.InfoModels[i].zb;
                    double hb = ColumnsModel.InfoModels[i].hb;
                    double z = ColumnsModel.InfoModels[i].zb;
                    ColumnsModel.StirrupModels[i].GetDistribute(l, hb, z);
                    if (ColumnsModel.BarMainModels[i].BarModels.Count != 0)
                    {
                        InfoModel infoModelUp = ColumnsModel.InfoModels.Where(x => x.NumberColumn == ColumnsModel.BarMainModels[i].NumberColumn + 1).FirstOrDefault();
                        StirrupModel stirrupModelUp = ColumnsModel.StirrupModels.Where(x => x.NumberColumn == ColumnsModel.BarMainModels[i].NumberColumn + 1).FirstOrDefault();
                        BarMainModel barMainModelUp = ColumnsModel.BarMainModels.Where(x => x.NumberColumn == ColumnsModel.BarMainModels[i].NumberColumn + 1).FirstOrDefault();
                        double ds = ColumnsModel.StirrupModels[i].BarS.Diameter;
                        double dsUp = (infoModelUp == null) ? (ColumnsModel.StirrupModels[i].BarS.Diameter) : (stirrupModelUp.BarS.Diameter);
                        ColumnsModel.BarMainModels[i].RefreshX0Y0BarModels(ColumnsModel.SectionStyle, ColumnsModel.InfoModels[i], ColumnsModel.Cover, ds);
                        ColumnsModel.BarMainModels[i].RefreshLocationBarModels(ColumnsModel.SectionStyle, ColumnsModel.InfoModels[i], ColumnsModel.Cover, ds,dsUp, infoModelUp);
                    }
                    if (ColumnsModel.BarMainModels[i].AddBarModels.Count != 0)
                    {
                        InfoModel infoModelUp = ColumnsModel.InfoModels.Where(x => x.NumberColumn == ColumnsModel.BarMainModels[i].NumberColumn + 1).FirstOrDefault();
                        StirrupModel stirrupModelUp = ColumnsModel.StirrupModels.Where(x => x.NumberColumn == ColumnsModel.BarMainModels[i].NumberColumn + 1).FirstOrDefault();
                        InfoModel infoModel = (infoModelUp == null) ? ColumnsModel.InfoModels[i] : infoModelUp;
                        double topPosition = ColumnsModel.InfoModels[i].TopPosition;
                        double ds = (infoModelUp == null) ? (ColumnsModel.StirrupModels[i].BarS.Diameter) : (stirrupModelUp.BarS.Diameter);
                        ColumnsModel.BarMainModels[i].RefreshX0Y0AddBarModels(ColumnsModel.SectionStyle, infoModel, ColumnsModel.Cover, ds);
                        ColumnsModel.BarMainModels[i].RefreshLocationAddBarModels(ColumnsModel.SectionStyle, infoModel, topPosition, ColumnsModel.Cover, ds);
                    }
                }
                DrawMain(p);
            });
            STextChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                StirrupsView uc = ProccessInfoClumns.FindChild<StirrupsView>(p, "StirrupsUC");
                if (double.TryParse(uc.STextBox.Text.ToString(),out double S))
                {
                    DrawMain(p);
                }
            });
            S1TextChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                StirrupsView uc = ProccessInfoClumns.FindChild<StirrupsView>(p, "StirrupsUC");
                if (double.TryParse(uc.S1TextBox.Text.ToString(), out double S))
                {
                    DrawMain(p);
                }
            });
            S2TextChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                StirrupsView uc = ProccessInfoClumns.FindChild<StirrupsView>(p, "StirrupsUC");
                if (double.TryParse(uc.S2TextBox.Text.ToString(), out double S))
                {
                    DrawMain(p);
                }
            });
            #endregion
        }
        #region Draw
        private void DrawMain(ColumnsWindow p)
        {
            p.MainCanvas.Children.Clear();
            DrawMainCanvas.DrawInfoColumns(p.MainCanvas, ColumnsModel, 1000);
            DrawMainCanvas.DrawStirrup(p.MainCanvas, ColumnsModel, ColumnsModel.SelectedIndexModel.SelectedColumn);
            double top = ColumnsModel.DrawModel.Top-( ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].TopPosition) / ( ColumnsModel.DrawModel.Scale);
            p.scrollViewer.ScrollToBottom();
            p.scrollViewer.ScrollToVerticalOffset(top);
        }
        private void DrawStirrup(StirrupsView p)
        {
            double d = (ColumnsModel.BarMainModels[ColumnsModel.SelectedIndexModel.SelectedColumn].BarModels.Count == 0) ? ColumnsModel.AllBars[3].Diameter : ColumnsModel.BarMainModels[ColumnsModel.SelectedIndexModel.SelectedColumn].BarModels[0].Bar.Diameter;
            DrawMainCanvas.DrawSectionAndStirrups(p.CanvasSection,ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn], ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn], ColumnsModel.DrawModelSection,ColumnsModel.SectionStyle,ColumnsModel.Cover,d, ColumnsModel.DrawModelSection.ColorStirrupChoose);
        }
        private void DrawDistribute(StirrupsView p)
        {
            DrawImage.DrawDistribute(p.CanvasDistribute, SelectedStirrupModel.TypeDis, SelectedStirrupModel.IsTiesUp);
        }
        #endregion
        #region method
        private void SetDistributeType(StirrupsView p)
        {
            double l = ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].hc - ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].hb - ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].zb;
            double hb = ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].hb;
            double z = ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].zb;
            SelectedStirrupModel.GetDistribute(l, hb, z);
            if(SelectedStirrupModel.TypeDis == 0)
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
        private void RefreshValueMainBar()
        {
            InfoModel infoModelUp = ColumnsModel.InfoModels.Where(x => x.NumberColumn == ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].NumberColumn + 1).FirstOrDefault();
            StirrupModel stirrupModelUp = ColumnsModel.StirrupModels.Where(x => x.NumberColumn == ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn].NumberColumn + 1).FirstOrDefault();
            BarMainModel barMainModelUp = ColumnsModel.BarMainModels.Where(x => x.NumberColumn == ColumnsModel.BarMainModels[ColumnsModel.SelectedIndexModel.SelectedColumn].NumberColumn + 1).FirstOrDefault();
            double ds = ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn].BarS.Diameter;
            double dsUp = (infoModelUp == null) ? (ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn].BarS.Diameter) : (ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn + 1].BarS.Diameter);
            ColumnsModel.BarMainModels[ColumnsModel.SelectedIndexModel.SelectedColumn].RefreshX0Y0BarModels(ColumnsModel.SectionStyle, ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn], ColumnsModel.Cover, ds);
            ColumnsModel.BarMainModels[ColumnsModel.SelectedIndexModel.SelectedColumn].RefreshLocationBarModels(ColumnsModel.SectionStyle, ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn], ColumnsModel.Cover, ds,dsUp, infoModelUp);
        }
        private void RefreshValueAddBar()
        {
            InfoModel infoModelUp = ColumnsModel.InfoModels.Where(x => x.NumberColumn == ColumnsModel.BarMainModels[ColumnsModel.SelectedIndexModel.SelectedColumn].NumberColumn + 1).FirstOrDefault();
            InfoModel infoModel = (infoModelUp == null) ? ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn] : infoModelUp;
            double topPosition = ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].TopPosition;
            double ds = (infoModelUp == null) ? (ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn].BarS.Diameter) : (ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn + 1].BarS.Diameter);
            ColumnsModel.BarMainModels[ColumnsModel.SelectedIndexModel.SelectedColumn].RefreshX0Y0AddBarModels(ColumnsModel.SectionStyle, infoModel, ColumnsModel.Cover, ds);
            ColumnsModel.BarMainModels[ColumnsModel.SelectedIndexModel.SelectedColumn].RefreshLocationAddBarModels(ColumnsModel.SectionStyle, infoModel, topPosition, ColumnsModel.Cover, ds);
        }
    }
}
