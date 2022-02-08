using Autodesk.Revit.DB;
using R01_ColumnsRebar.View;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using WpfCustomControls;
namespace R01_ColumnsRebar.ViewModel
{
    public class TopDowelsViewModel : BaseViewModel
    {
        #region property
        public Document Doc;
        private ColumnsModel _ColumnsModel;
        public ColumnsModel ColumnsModel { get { return _ColumnsModel; } set { _ColumnsModel = value; OnPropertyChanged(); } }
        #endregion
        #region Selected
        private BarMainModel _SelectedColumn;
        public BarMainModel SelectedColumn { get => _SelectedColumn; set { _SelectedColumn = value; OnPropertyChanged(); } }
        private BarModel _SelectedBar;
        public BarModel SelectedBar { get => _SelectedBar; set { _SelectedBar = value; OnPropertyChanged(); } }
        private BarModel _SelectedAddBar;
        public BarModel SelectedAddBar { get => _SelectedAddBar; set { _SelectedAddBar = value; OnPropertyChanged(); } }
        private bool _IsEnabledTopDowels;
        public bool IsEnabledTopDowels { get => _IsEnabledTopDowels; set { _IsEnabledTopDowels = value; OnPropertyChanged(); } }
        private bool _IsEnabledTopTypeDowels;
        public bool IsEnabledTopTypeDowels { get => _IsEnabledTopTypeDowels; set { _IsEnabledTopTypeDowels = value; OnPropertyChanged(); } }
        private bool _IsLock;
        public bool IsLock { get => _IsLock; set { _IsLock = value; OnPropertyChanged(); } }
        #endregion
        #region Command
        public ICommand LoadTopDowelsViewCommand { get; set; }
        public ICommand SelectionColumnDowelsChangedCommand { get; set; }
        public ICommand CheckAddBarDowelsCommand { get; set; }
        public ICommand CheckTopDowelsCommand { get; set; }
        public ICommand CheckUpTopBarDowelsCommand { get; set; }
        public ICommand SelectionTopTypeDowelsChangedCommand { get; set; }
        public ICommand SelectionBarChangedCommand { get; set; }
        public ICommand SelectionAddBarChangedCommand { get; set; }
        public ICommand ApplyAllBarCommand { get; set; }
        public ICommand TopDowelsLaTextChangedCommand { get; set; }
        public ICommand TopDowelsLbTextChangedCommand { get; set; }
        public ICommand L1AddBarTextChangedCommand { get; set; }
        public ICommand L2AddBarTextChangedCommand { get; set; }
        public ICommand L3AddBarTextChangedCommand { get; set; }
        public ICommand ApplyCommand { get; set; }
        public ICommand ModifyCommand { get; set; }
        public ICommand FixedTopBarDowelsCommand { get; set; }
        #endregion
        public TopDowelsViewModel(Document doc, ColumnsModel columnsModel)
        {
            #region property
            Doc = doc;
            ColumnsModel = columnsModel;       
            #endregion
            #region   Load
            LoadTopDowelsViewCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                if (SelectedColumn.BarModels.Count != 0) ColumnsModel.SelectedIndexModel.SelectedMainBar = 0;
                IsEnabledTopDowels = (SelectedColumn.BarModels.Count != 0) && (SelectedBar != null);
                IsEnabledTopTypeDowels = (SelectedBar == null) ? false : (SelectedBar.IsTopDowels);
                IsLock =  (SelectedColumn.AddBarModels.Count == 0);
                TopDowelsView uc = ProccessInfoClumns.FindChild<TopDowelsView>(p, "TopDowelsUC");
                DrawTopDowelsComboBox(uc);
                ShowLaTopDowels(uc);
                ShowAddTop(uc);
                ShowFixedTop(uc);
                //if (SelectedBar != null) ChangeTopValue();
                uc.AddBarListView.ItemsSource = null;
                uc.AddBarListView.ItemsSource = SelectedColumn.AddBarModels;
                ShowSelectionBars(uc);
                RefreshValueMainBar();
                DrawMain(p);
                DrawSection(uc);
            });
           
            CheckTopDowelsCommand = new RelayCommand<ColumnsWindow>((p) => { return (SelectedBar != null); }, (p) =>
            {
                IsEnabledTopTypeDowels = SelectedBar.IsTopDowels;
                TopDowelsView uc = ProccessInfoClumns.FindChild<TopDowelsView>(p, "TopDowelsUC");
                ShowLaTopDowels(uc);
                ShowFixedTop(uc);
                ShowAddTop(uc);
                RefreshValueMainBar();
                RefreshValueAddBar();
                DrawMain(p);
                DrawSection(uc);
            });
            SelectionColumnDowelsChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                if (SelectedColumn.BarModels.Count != 0) ColumnsModel.SelectedIndexModel.SelectedMainBar = 0;
                if (SelectedColumn.AddBarModels.Count != 0) ColumnsModel.SelectedIndexModel.SelectedAddBar = 0;
                IsLock = (SelectedColumn.AddBarModels.Count == 0);
                IsEnabledTopDowels = (SelectedColumn.BarModels.Count != 0) && (SelectedBar != null);
                IsEnabledTopTypeDowels = (SelectedBar == null) ? false : (SelectedBar.IsTopDowels);
                TopDowelsView uc = ProccessInfoClumns.FindChild<TopDowelsView>(p, "TopDowelsUC");
                ShowLaTopDowels(uc);
                ShowAddTop(uc);
                ShowFixedTop(uc);
                //if (SelectedBar != null) ChangeTopValue();
                uc.AddBarListView.ItemsSource = null;
                uc.AddBarListView.ItemsSource = SelectedColumn.AddBarModels;
                RefreshValueMainBar();
                RefreshValueAddBar();
                DrawMain(p);
                DrawSection(uc);
                // itemsource=null;
                //if()
                
            });
            SelectionTopTypeDowelsChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                TopDowelsView uc = ProccessInfoClumns.FindChild<TopDowelsView>(p, "TopDowelsUC");    
                ShowLaTopDowels(uc);
                ShowAddTop(uc);
                ShowFixedTop(uc);
                //ChangeTopValue();
                RefreshValueMainBar();
                RefreshValueAddBar();
                DrawMain(p);
                DrawSection(uc);
            });
            SelectionBarChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                IsEnabledTopDowels = (SelectedBar != null);
                IsEnabledTopTypeDowels = (SelectedBar == null) ? false : (SelectedBar.IsTopDowels);
                TopDowelsView uc = ProccessInfoClumns.FindChild<TopDowelsView>(p, "TopDowelsUC");
                ShowLaTopDowels(uc);
                RefreshValueMainBar();
                RefreshValueAddBar();
                DrawMain(p);
                //DrawSection(uc);
            });
           
            ApplyAllBarCommand = new RelayCommand<ColumnsWindow>((p) => { return (SelectedBar != null) && SelectedColumn.BarModels.Count != 0; }, (p) =>
            {
                TopDowelsView uc = ProccessInfoClumns.FindChild<TopDowelsView>(p, "TopDowelsUC");
                for (int i = 0; i < SelectedColumn.BarModels.Count; i++)
                {
                    SelectedColumn.BarModels[i].IsTopDowels = SelectedBar.IsTopDowels;
                    SelectedColumn.BarModels[i].TopDowels = SelectedBar.TopDowels;
                    SelectedColumn.BarModels[i].LaTopDowels = SelectedBar.LaTopDowels;
                    if (SelectedColumn.BarModels[i].TopDowels!=0)
                    {
                        SelectedColumn.BarModels[i].LbTopDowels = 0;
                    }
                    else
                    {
                        if (SelectedColumn.SplitOverlap == 100)
                        {
                            SelectedColumn.BarModels[i].LbTopDowels = SelectedBar.Bar.Diameter * SelectedColumn.Overlap;
                        }
                        else
                        {
                            SelectedColumn.BarModels[i].LbTopDowels = (SelectedColumn.BarModels[i].BarNumber % 2 == 0) ? SelectedBar.Bar.Diameter * SelectedColumn.Overlap : 2 * SelectedBar.Bar.Diameter * SelectedColumn.Overlap;
                        }
                    }  
                }
                ShowAddTop(uc);
                ShowFixedTop(uc);
                RefreshValueMainBar();
                RefreshValueAddBar();
                DrawMain(p);
                DrawSection(uc);
            });
            TopDowelsLaTextChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return (SelectedBar != null); }, (p) =>
            {
                TopDowelsView uc = ProccessInfoClumns.FindChild<TopDowelsView>(p, "TopDowelsUC");
                if (double.TryParse(uc.TopDowelsLaTextBox.Text.ToString(), out double S))
                {
                    RefreshValueMainBar();
                    RefreshValueAddBar();
                    DrawMain(p);
                    DrawSection(uc);
                }
            });
            TopDowelsLbTextChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return (SelectedBar != null); }, (p) =>
            {
                TopDowelsView uc = ProccessInfoClumns.FindChild<TopDowelsView>(p, "TopDowelsUC");
                if (double.TryParse(uc.TopDowelsLbTextBox.Text.ToString(), out double S))
                {
                    RefreshValueMainBar();
                    RefreshValueAddBar();
                    DrawMain(p);
                    DrawSection(uc);
                }
            });
            FixedTopBarDowelsCommand = new RelayCommand<ColumnsWindow>((p) => { return SelectedColumn.FixedTop; }, (p) =>
            {
                if (SelectedColumn.AddBarModels.Count==0)
                {
                    int number = 0;
                    for (int i = 0; i < SelectedColumn.BarModels.Count; i++)
                    {
                        if (SelectedColumn.BarModels[i].IsTopDowels&&SelectedColumn.BarModels[i].TopDowels==0)
                        {
                            double l = (SelectedColumn.SplitOverlap == 50) ? ((number%2!=0)?(SelectedColumn.Bar.Diameter * SelectedColumn.Overlap) :(SelectedColumn.Bar.Diameter * SelectedColumn.Overlap*2)) : (SelectedColumn.Bar.Diameter*SelectedColumn.Overlap);
                            SelectedColumn.BarModels[i].LbTopDowels = l;
                            SelectedColumn.BarModels[i].EvenTop = number % 2 != 0;
                            number++;
                        }
                    }
                }
                else
                {
                    int number = 0;
                    for (int i = 0; i < SelectedColumn.AddBarModels.Count; i++)
                    {
                        double l = (SelectedColumn.SplitOverlap == 50) ? ((number % 2 != 0) ? (SelectedColumn.AddBar.Diameter * SelectedColumn.Overlap) : (SelectedColumn.AddBar.Diameter * SelectedColumn.Overlap * 2)) : (SelectedColumn.AddBar.Diameter * SelectedColumn.Overlap);
                        SelectedColumn.AddBarModels[i].L3 = l;
                        SelectedColumn.AddBarModels[i].EvenTop = number % 2 != 0;
                        number++;
                    }
                }
                RefreshValueMainBar();
                RefreshValueAddBar();
                DrawMain(p);
                TopDowelsView uc = ProccessInfoClumns.FindChild<TopDowelsView>(p, "TopDowelsUC");
                DrawSection(uc);
            });
            #endregion
            #region AddBar

            SelectionAddBarChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return SelectedColumn.AddBarModels.Count != 0; }, (p) =>
            {
                TopDowelsView uc = ProccessInfoClumns.FindChild<TopDowelsView>(p, "TopDowelsUC");
                RefreshValueMainBar();
                RefreshValueAddBar();
                DrawMain(p);
                DrawSection(uc);
            });
            ApplyCommand = new RelayCommand<ColumnsWindow>((p) => { return IsLock&&ConditionApplyAddBar(p); }, (p) =>
            {
                TopDowelsView uc = ProccessInfoClumns.FindChild<TopDowelsView>(p, "TopDowelsUC");
                double L1 = double.Parse(uc.L1AddBar.Text.ToString());
                double L2 = double.Parse(uc.L2AddBar.Text.ToString());
                double L3 = double.Parse(uc.L3AddBar.Text.ToString()); 
                uc.AddBarListView.ItemsSource = null;
                InfoModel infoModelUp = ColumnsModel.InfoModels.Where(x => x.NumberColumn == SelectedColumn.NumberColumn + 1).FirstOrDefault();
                InfoModel infoModel = (infoModelUp == null) ? ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn] : infoModelUp;
                double topPosition = ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].TopPosition;
                double ds = (infoModelUp == null) ? (ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn].BarS.Diameter) : (ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn + 1].BarS.Diameter);
                SelectedColumn.GetAddBarModels(ColumnsModel.SectionStyle, infoModel,  ColumnsModel.Cover, ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn].BarS.Diameter, L1, L2, L3);
                SelectedColumn.RefreshLocationAddBarModels(ColumnsModel.SectionStyle, infoModel, topPosition, ColumnsModel.Cover, ds);
                ColumnsModel.SelectedIndexModel.SelectedAddBar = 0;
                uc.AddBarListView.ItemsSource = SelectedColumn.AddBarModels;
                IsLock = (SelectedColumn.AddBarModels.Count == 0);
                RefreshValueMainBar();
                DrawMain(p);
                DrawSection(uc);
                ShowFixedTop(uc);
            });
            ModifyCommand = new RelayCommand<ColumnsWindow>((p) => { return !IsLock&&SelectedColumn.ConditionShowAddTop(); }, (p) =>
            {
                IsLock = true;
                SelectedColumn.AddBarModels.Clear();
                RefreshValueMainBar();
                RefreshValueAddBar();
                DrawMain(p);
                TopDowelsView uc = ProccessInfoClumns.FindChild<TopDowelsView>(p, "TopDowelsUC");
                DrawSection(uc);
            });
            L1AddBarTextChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return SelectedColumn.AddBarModels.Count != 0; }, (p) =>
                {
                    TopDowelsView uc = ProccessInfoClumns.FindChild<TopDowelsView>(p, "TopDowelsUC");
                    if (double.TryParse(uc.L1AddBar.Text.ToString(), out double S) && SelectedAddBar != null)
                    {
                        RefreshValueMainBar();
                        RefreshValueAddBar();
                        DrawMain(p);
                        DrawSection(uc);
                    }
                });
            L2AddBarTextChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return SelectedColumn.AddBarModels.Count != 0; }, (p) =>
            {
                TopDowelsView uc = ProccessInfoClumns.FindChild<TopDowelsView>(p, "TopDowelsUC");
                if (double.TryParse(uc.L2AddBar.Text.ToString(), out double S) && SelectedAddBar != null)
                {
                    RefreshValueMainBar();
                    RefreshValueAddBar();
                    DrawMain(p);
                    DrawSection(uc);
                }
            });
            L3AddBarTextChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return SelectedColumn.AddBarModels.Count != 0; }, (p) =>
            {
                TopDowelsView uc = ProccessInfoClumns.FindChild<TopDowelsView>(p, "TopDowelsUC");
                if (double.TryParse(uc.L3AddBar.Text.ToString(), out double S) && SelectedAddBar != null)
                {
                    RefreshValueMainBar();
                    RefreshValueAddBar();
                    DrawMain(p);
                    DrawSection(uc);
                }
            });
            #endregion
        }
        #region Draw
        private void DrawTopDowelsComboBox(TopDowelsView p)
        {
            DrawImage.DrawTopDowelsType0(p.TopDowelsTypeCanvas0);
            DrawImage.DrawTopDowelsType1(p.TopDowelsTypeCanvas1);
            DrawImage.DrawAddBarDowels(p.CanvasAddBar);
        }
        private void DrawMain(ColumnsWindow p)
        {
            p.MainCanvas.Children.Clear();
            DrawMainCanvas.DrawInfoColumns(p.MainCanvas, ColumnsModel, ColumnsModel.SelectedIndexModel.SelectedColumn);
            DrawMainCanvas.DrawBarMains(p.MainCanvas, ColumnsModel, ColumnsModel.SelectedIndexModel.SelectedColumn, (SelectedBar != null) ? SelectedBar.BarNumber : 1000,(SelectedAddBar!=null)?SelectedAddBar.BarNumber:1000);
            double top0 = ColumnsModel.DrawModel.Top - (ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].TopPosition) / (ColumnsModel.DrawModel.Scale);
            p.scrollViewer.ScrollToBottom();
            p.scrollViewer.ScrollToVerticalOffset(top0);
            
        }
        private void RefreshValueMainBar()
        {
            InfoModel infoModelUp = ColumnsModel.InfoModels.Where(x => x.NumberColumn == ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].NumberColumn + 1).FirstOrDefault();
            StirrupModel stirrupModelUp = ColumnsModel.StirrupModels.Where(x => x.NumberColumn == ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn].NumberColumn + 1).FirstOrDefault();
            BarMainModel barMainModelUp = ColumnsModel.BarMainModels.Where(x => x.NumberColumn == SelectedColumn.NumberColumn + 1).FirstOrDefault();
            double ds = ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn].BarS.Diameter;
            double dsUp = (infoModelUp == null) ? (ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn].BarS.Diameter) : (ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn + 1].BarS.Diameter);
            SelectedColumn.RefreshLocationBarModels(ColumnsModel.SectionStyle, ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn], ColumnsModel.Cover, ds,dsUp, infoModelUp);
        }
        private void RefreshValueAddBar()
        {
            InfoModel infoModelUp = ColumnsModel.InfoModels.Where(x => x.NumberColumn == SelectedColumn.NumberColumn + 1).FirstOrDefault();
            InfoModel infoModel = (infoModelUp == null) ? ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn] : infoModelUp;
            double topPosition = ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].TopPosition;
            double ds = (infoModelUp == null) ? (ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn].BarS.Diameter) : (ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn + 1].BarS.Diameter);
            SelectedColumn.RefreshLocationAddBarModels(ColumnsModel.SectionStyle, infoModel, topPosition, ColumnsModel.Cover, ds);
        }
        private void DrawSection(TopDowelsView p)
        {
            double d = (ColumnsModel.BarMainModels[ColumnsModel.SelectedIndexModel.SelectedColumn].BarModels.Count == 0) ? ColumnsModel.AllBars[3].Diameter : ColumnsModel.BarMainModels[ColumnsModel.SelectedIndexModel.SelectedColumn].BarModels[0].Bar.Diameter;
            InfoModel infoModelUp = ColumnsModel.InfoModels.Where(x => x.NumberColumn == ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].NumberColumn + 1).FirstOrDefault();
            StirrupModel stirrupModelUp = ColumnsModel.StirrupModels.Where(x => x.NumberColumn == ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn].NumberColumn + 1).FirstOrDefault();
            p.CanvasSection.Children.Clear();
            DrawMainCanvas.DrawSectionAndStirrupDowelsTop(p.CanvasSection, ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn], ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn], ColumnsModel.DrawModelDowels, ColumnsModel.SectionStyle, ColumnsModel.Cover, d, ColumnsModel.DrawModelDowels.ColorStirrup, infoModelUp, stirrupModelUp);

            if (SelectedColumn.BarModels.Count != 0)
            {
                SolidColorBrush solidColorBrush = ColumnsModel.DrawModelDowels.ColorMainBar;
                if (ColumnsModel.SectionStyle == ErrorColumns.SectionStyle.RECTANGLE)
                {

                    for (int i = 0; i < SelectedColumn.BarModels.Count; i++)
                    {
                        if (i == ColumnsModel.SelectedIndexModel.SelectedMainBar)
                        {
                            solidColorBrush = ColumnsModel.DrawModelDowels.ColorMainBarChoose;
                        }
                        else
                        {
                            solidColorBrush = ColumnsModel.DrawModelDowels.ColorMainBar;
                        }
                        double left = ColumnsModel.DrawModelDowels.Left + SelectedColumn.BarModels[i].X0 / ColumnsModel.DrawModelDowels.Scale;
                        double top = ColumnsModel.DrawModelDowels.Top - SelectedColumn.BarModels[i].Y0 / ColumnsModel.DrawModelDowels.Scale;
                        DrawImage.DrawOneBarSection(p.CanvasSection, left, top, ColumnsModel.DrawModelDowels.Scale, SelectedBar.Bar.Diameter, solidColorBrush);
                    }
                }
                else
                {
                    for (int i = 0; i < SelectedColumn.BarModels.Count; i++)
                    {
                        if (i == ColumnsModel.SelectedIndexModel.SelectedMainBar)
                        {
                            solidColorBrush = ColumnsModel.DrawModelDowels.ColorMainBarChoose;
                        }
                        else
                        {
                            solidColorBrush = ColumnsModel.DrawModelDowels.ColorMainBar;
                        }
                        double left = ColumnsModel.DrawModelDowels.Left + SelectedColumn.BarModels[i].X0 / ColumnsModel.DrawModelDowels.Scale;
                        double top = ColumnsModel.DrawModelDowels.Top - SelectedColumn.BarModels[i].Y0 / ColumnsModel.DrawModelDowels.Scale;
                        DrawImage.DrawOneBarSection(p.CanvasSection, left, top, ColumnsModel.DrawModelDowels.Scale, SelectedBar.Bar.Diameter, solidColorBrush);
                    }
                }
                DrawMainCanvas.DrawBarTopDowels(p.CanvasSection, ColumnsModel.DrawModelDowels, SelectedColumn, SelectedBar.BarNumber - 1);

            }
            //if (SelectedColumn.AddBarModels.Count != 0)
            //{
            //    DrawMainCanvas.DrawAddBarTopDowels(p.CanvasSection, ColumnsModel.DrawModelDowels, SelectedColumn, SelectedAddBar.BarNumber - 1);
            //}
        }
        #endregion
        #region Method
        private void ShowAddTop(TopDowelsView p)
        {
            if (SelectedColumn.ConditionShowAddTop())
            {
                p.GridAddBarDowels.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                p.GridAddBarDowels.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void ShowSelectionBars(TopDowelsView p)
        {
            if (ColumnsModel.SectionStyle == ErrorColumns.SectionStyle.RECTANGLE)
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
        private void ShowLaTopDowels(TopDowelsView p)
        {
            if (SelectedBar == null)
            {
                p.TopDowelsLaTextBlock.Visibility = System.Windows.Visibility.Hidden;
                p.TopDowelsLaTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                p.TopDowelsLaTextBox.Visibility = System.Windows.Visibility.Hidden;
                p.TopDowelsLbTextBlock.Visibility = System.Windows.Visibility.Hidden;
                p.TopDowelsLbTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                p.TopDowelsLbTextBox.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                if (SelectedBar.IsTopDowels)
                {
                    if (SelectedBar.TopDowels == 1)
                    {
                        p.TopDowelsLaTextBlock.Visibility = System.Windows.Visibility.Visible;
                        p.TopDowelsLaTextBlockUnit.Visibility = System.Windows.Visibility.Visible;
                        p.TopDowelsLaTextBox.Visibility = System.Windows.Visibility.Visible;
                        p.TopDowelsLbTextBlock.Visibility = System.Windows.Visibility.Hidden;
                        p.TopDowelsLbTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                        p.TopDowelsLbTextBox.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else
                    {
                        p.TopDowelsLaTextBlock.Visibility = System.Windows.Visibility.Hidden;
                        p.TopDowelsLaTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                        p.TopDowelsLaTextBox.Visibility = System.Windows.Visibility.Hidden;
                        p.TopDowelsLbTextBlock.Visibility = System.Windows.Visibility.Visible;
                        p.TopDowelsLbTextBlockUnit.Visibility = System.Windows.Visibility.Visible;
                        p.TopDowelsLbTextBox.Visibility = System.Windows.Visibility.Visible;
                    }
                }
                else
                {
                    p.TopDowelsLaTextBlock.Visibility = System.Windows.Visibility.Hidden;
                    p.TopDowelsLaTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                    p.TopDowelsLaTextBox.Visibility = System.Windows.Visibility.Hidden;
                    p.TopDowelsLbTextBlock.Visibility = System.Windows.Visibility.Hidden;
                    p.TopDowelsLbTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                    p.TopDowelsLbTextBox.Visibility = System.Windows.Visibility.Hidden;
                }

            }

        }
       
       
        private bool ConditionApplyAddBar(ColumnsWindow p)
        {
            TopDowelsView uc = ProccessInfoClumns.FindChild<TopDowelsView>(p, "TopDowelsUC");
            return SelectedColumn.ConditionShowAddTop()&&(double.TryParse(uc.L1AddBar.Text.ToString(), out double S1)) && (double.TryParse(uc.L2AddBar.Text.ToString(), out double S2)) && (double.TryParse(uc.L3AddBar.Text.ToString(), out double S3));
        }
        #endregion
        #region ShowFixedTop
        private void ShowFixedTop(TopDowelsView p)
        {
            InfoModel infoModelUp = ColumnsModel.InfoModels.Where(x => x.NumberColumn == ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].NumberColumn + 1).FirstOrDefault();
            BarMainModel barMainModelUp = ColumnsModel.BarMainModels.Where(x => x.NumberColumn == SelectedColumn.NumberColumn + 1).FirstOrDefault();
            SelectedColumn.FixedTop = SelectedColumn.ConditionFixedTop(ColumnsModel.SectionStyle, infoModelUp, barMainModelUp);
            if (SelectedColumn.FixedTop)
            {
                p.FixedTop.Visibility = System.Windows.Visibility.Visible;
            }
            else { p.FixedTop.Visibility = System.Windows.Visibility.Hidden; }
        }
        #endregion
        #region Change Top Value
        //private void ChangeTopValue()
        //{
        //    if (SelectedColumn.BarModels.Count == 0 || SelectedBar == null) return;
        //    if (SelectedBar.IsTopDowels)
        //    {
        //        if (SelectedBar.TopDowels==0)
        //        {
        //            SelectedBar.LaTopDowels = 0;
        //            SelectedBar.LbTopDowels = (SelectedColumn.SplitOverlap==100)?SelectedColumn.Overlap*SelectedBar.Bar.Diameter:((SelectedBar.BarNumber%2==0)? SelectedColumn.Overlap * SelectedBar.Bar.Diameter: 2*SelectedColumn.Overlap * SelectedBar.Bar.Diameter);
        //        }
        //        else
        //        {
        //            SelectedBar.LaTopDowels = SelectedColumn.Overlap * SelectedBar.Bar.Diameter;
        //            SelectedBar.LbTopDowels = 0;
        //        }
        //    }
        //    else
        //    {
        //        SelectedBar.LaTopDowels = 0;
        //        SelectedBar.LbTopDowels = 0;
        //    }
        //}
        #endregion
    }
}
