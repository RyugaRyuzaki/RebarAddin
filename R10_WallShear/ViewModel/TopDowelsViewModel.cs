using Autodesk.Revit.DB;
using R10_WallShear.LanguageModel;
using R10_WallShear.View;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WpfCustomControls;
using Visibility = System.Windows.Visibility;

namespace R10_WallShear.ViewModel
{
    public class TopDowelsViewModel : BaseViewModel
    {
        #region property
        public Document Doc;
        private WallsModel _WallsModel;
        public WallsModel WallsModel { get => _WallsModel; set { _WallsModel = value; OnPropertyChanged(); } }
        #endregion
        #region Selected
        private BarMainModel _SelectedWall;
        public BarMainModel SelectedWall { get => _SelectedWall; set { _SelectedWall = value; OnPropertyChanged(); } }
        private BarModel _SelectedBar;
        public BarModel SelectedBar { get => _SelectedBar; set { _SelectedBar = value; OnPropertyChanged(); } }
        private BarModel _SelectedBarCorner;
        public BarModel SelectedBarCorner { get => _SelectedBarCorner; set { _SelectedBarCorner = value; OnPropertyChanged(); } }
        #endregion
        private bool _IsEnabledTopDowels;
        public bool IsEnabledTopDowels { get => _IsEnabledTopDowels; set { _IsEnabledTopDowels = value; OnPropertyChanged(); } }
        private bool _IsEnabledTopTypeDowels;
        public bool IsEnabledTopTypeDowels { get => _IsEnabledTopTypeDowels; set { _IsEnabledTopTypeDowels = value; OnPropertyChanged(); } }
        private bool _IsEnabledTopDowelsCorner;
        public bool IsEnabledTopDowelsCorner { get => _IsEnabledTopDowelsCorner; set { _IsEnabledTopDowelsCorner = value; OnPropertyChanged(); } }
        private bool _IsEnabledTopTypeDowelsCorner;
        public bool IsEnabledTopTypeDowelsCorner { get => _IsEnabledTopTypeDowelsCorner; set { _IsEnabledTopTypeDowelsCorner = value; OnPropertyChanged(); } }
        private bool _IsLock;
        public bool IsLock { get => _IsLock; set { _IsLock = value; OnPropertyChanged(); } }
        private Visibility _ShowCorner;
        public Visibility ShowCorner { get { return _ShowCorner; } set { _ShowCorner = value; OnPropertyChanged(); } }
        private Visibility _ShowFixedToTop;
        public Visibility ShowFixedToTop { get { return _ShowFixedToTop; } set { _ShowFixedToTop = value; OnPropertyChanged(); } }
        private Visibility _ShowFixedToTopCorner;
        public Visibility ShowFixedToTopCorner { get { return _ShowFixedToTopCorner; } set { _ShowFixedToTopCorner = value; OnPropertyChanged(); } }
        #region Icommand
        public ICommand LoadTopDowelsCommand { get; set; }
        public ICommand SelectionWallDowelsChangedCommand { get; set; }
        public ICommand SelectionBarChangedCommand { get; set; }
        public ICommand ApplyAllBarCommand { get; set; }

        public ICommand CheckTopDowelsCommand { get; set; }
        public ICommand TopDowelsLaTextChangedCommand { get; set; }
        public ICommand TopDowelsLbTextChangedCommand { get; set; }
        public ICommand SelectionTopTypeDowelsChangedCommand { get; set; }
        public ICommand FixedTopBarDowelsCommand { get; set; }


        public ICommand SelectionBarCornerChangedCommand { get; set; }
        public ICommand CheckTopDowelsCornerCommand { get; set; }
        public ICommand TopDowelsCornerLaTextChangedCommand { get; set; }
        public ICommand TopDowelsCornerLbTextChangedCommand { get; set; }
        public ICommand SelectionTopTypeDowelsCornerChangedCommand { get; set; }
        public ICommand FixedTopBarDowelsCornerCommand { get; set; }

        #endregion
        private Languages _Languages;
        public Languages Languages { get { return _Languages; } set { _Languages = value; OnPropertyChanged(); } }
        public TopDowelsViewModel(Document document, WallsModel wallsModel, Languages languages)
        {
            #region Property
            Doc = document;
            WallsModel = wallsModel; Languages = languages;
            #endregion
            #region Load
            LoadTopDowelsCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                ;
                IsEnabledTopDowels = (SelectedBar != null);
                IsEnabledTopTypeDowels = (SelectedBar == null) ? false : (SelectedBar.IsTopDowels);
                IsEnabledTopDowelsCorner = (SelectedBarCorner != null);
                IsEnabledTopTypeDowelsCorner = ((SelectedBarCorner == null) ? false : (SelectedBarCorner.IsTopDowels));
                TopDowelsView uc = ProccessInfoWalls.FindChild<TopDowelsView>(p, "TopDowelsUC");

                DrawTopDowelsComboBox(uc);
                ShowLaTopDowels(uc);
                ShowLaTopDowelsCorner(uc);
                ShowCorner = (SelectedWall != null && SelectedWall.IsCorner) ? Visibility.Visible : Visibility.Collapsed;
                ShowFixedToTop = (SelectedWall != null && ConditionShowFixedToUpBar()) ? Visibility.Visible : Visibility.Collapsed;
                ShowFixedToTopCorner = (SelectedWall != null && ConditionShowFixedToUpBarCorner()) ? Visibility.Visible : Visibility.Collapsed;
                DrawMain(p);
                DrawSection(p);
            });
            SelectionWallDowelsChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedWall != null; }, (p) =>
              {
                  if (SelectedWall.BarModels.Count != 0) WallsModel.SelectedIndexModel.SelectedMainBar = 0;
                  if (SelectedWall.BarCornerModels.Count != 0) WallsModel.SelectedIndexModel.SelectedCornerMainBar = 0;
                  IsEnabledTopDowels = (SelectedWall.BarModels.Count != 0) && (SelectedBar != null);
                  IsEnabledTopTypeDowels = (SelectedBar == null) ? false : (SelectedBar.IsTopDowels);
                  IsEnabledTopDowelsCorner = (SelectedBarCorner != null);
                  IsEnabledTopTypeDowelsCorner = ((SelectedBarCorner == null) ? false : (SelectedBarCorner.IsTopDowels));
                  TopDowelsView uc = ProccessInfoWalls.FindChild<TopDowelsView>(p, "TopDowelsUC");
                  ShowLaTopDowels(uc);
                  ShowCorner = (SelectedWall != null && SelectedWall.IsCorner) ? Visibility.Visible : Visibility.Collapsed;
                  ShowFixedToTop = (SelectedWall != null && ConditionShowFixedToUpBar()) ? Visibility.Visible : Visibility.Collapsed;
                  ShowFixedToTopCorner = (SelectedWall != null && ConditionShowFixedToUpBarCorner()) ? Visibility.Visible : Visibility.Collapsed;
                  DrawMain(p);
                  DrawSection(p);
              });
            SelectionBarChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBar != null; }, (p) =>
            {
                IsEnabledTopDowels = (SelectedWall.BarModels.Count != 0) && (SelectedBar != null);
                IsEnabledTopTypeDowels = (SelectedBar == null) ? false : (SelectedBar.IsTopDowels);
                TopDowelsView uc = ProccessInfoWalls.FindChild<TopDowelsView>(p, "TopDowelsUC");
                ShowLaTopDowels(uc);
                RefreshValueBar();
                DrawMain(p);
                DrawSection(p);
            });
            ApplyAllBarCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                TopDowelsView uc = ProccessInfoWalls.FindChild<TopDowelsView>(p, "TopDowelsUC");

            });
            #endregion
            #region TopDowels
            CheckTopDowelsCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBar != null; }, (p) =>
            {
                TopDowelsView uc = ProccessInfoWalls.FindChild<TopDowelsView>(p, "TopDowelsUC");
                IsEnabledTopTypeDowels = (SelectedBar == null) ? false : (SelectedBar.IsTopDowels);
                ShowLaTopDowels(uc);

                RefreshValueBar();
                ShowFixedToTop = (SelectedWall != null && ConditionShowFixedToUpBar()) ? Visibility.Visible : Visibility.Collapsed;
                DrawMain(p);
                DrawSection(p);
            });
            TopDowelsLaTextChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBar.IsTopDowels && SelectedBar.TopDowels == 1; }, (p) =>
                {
                    TopDowelsView uc = ProccessInfoWalls.FindChild<TopDowelsView>(p, "TopDowelsUC");
                    if (double.TryParse(uc.TopDowelsLaTextBox.Text.ToString(), out double S))
                    {
                        RefreshValueBar();
                        DrawMain(p);
                        DrawSection(p);
                    }

                });
            TopDowelsLbTextChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBar.IsTopDowels && SelectedBar.TopDowels == 0; }, (p) =>
            {

                TopDowelsView uc = ProccessInfoWalls.FindChild<TopDowelsView>(p, "TopDowelsUC");
                if (double.TryParse(uc.TopDowelsLbTextBox.Text.ToString(), out double S))
                {
                    RefreshValueBar();
                    DrawMain(p);
                    DrawSection(p);
                }
            });
            SelectionTopTypeDowelsChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBar.IsTopDowels; }, (p) =>
            {
                TopDowelsView uc = ProccessInfoWalls.FindChild<TopDowelsView>(p, "TopDowelsUC");
                ShowLaTopDowels(uc);
                RefreshValueBar();
                ShowFixedToTop = (SelectedWall != null && ConditionShowFixedToUpBar()) ? Visibility.Visible : Visibility.Collapsed;
                DrawMain(p);
                DrawSection(p);
            });
            FixedTopBarDowelsCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                int number = 0;
                for (int i = 0; i < SelectedWall.BarModels.Count; i++)
                {
                    if (SelectedWall.BarModels[i].IsTopDowels && SelectedWall.BarModels[i].TopDowels == 0)
                    {
                        double l = (SelectedWall.SplitOverlap == 50) ? ((number % 2 != 0) ? (SelectedWall.Bar.Diameter * SelectedWall.Overlap) : (SelectedWall.Bar.Diameter * SelectedWall.Overlap * 2)) : (SelectedWall.Bar.Diameter * SelectedWall.Overlap);
                        SelectedWall.BarModels[i].LbTopDowels = l;
                        SelectedWall.BarModels[i].EvenTop = number % 2 != 0;
                        number++;
                    }
                }
                RefreshValueBar();
                DrawMain(p);
                DrawSection(p);
            });
            #endregion
            #region CornerTopDowels
            SelectionBarCornerChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBarCorner != null; }, (p) =>
            {
                IsEnabledTopDowelsCorner = (SelectedBarCorner != null);
                IsEnabledTopTypeDowelsCorner = ((SelectedBarCorner == null) ? false : (SelectedBarCorner.IsTopDowels));
                TopDowelsView uc = ProccessInfoWalls.FindChild<TopDowelsView>(p, "TopDowelsUC");
                ShowLaTopDowelsCorner(uc);
                DrawMain(p);
                DrawSection(p);
            });
            CheckTopDowelsCornerCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBarCorner != null; }, (p) =>
            {
                TopDowelsView uc = ProccessInfoWalls.FindChild<TopDowelsView>(p, "TopDowelsUC");
                IsEnabledTopTypeDowelsCorner = ((SelectedBarCorner == null) ? false : (SelectedBarCorner.IsTopDowels));
                ShowLaTopDowelsCorner(uc);
                RefreshValueBar();
                ShowFixedToTopCorner = (SelectedWall != null && ConditionShowFixedToUpBarCorner()) ? Visibility.Visible : Visibility.Collapsed;
                DrawMain(p);
                DrawSection(p);
            });
            TopDowelsCornerLaTextChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBarCorner.IsTopDowels && SelectedBarCorner.TopDowels == 1; }, (p) =>
            {
                TopDowelsView uc = ProccessInfoWalls.FindChild<TopDowelsView>(p, "TopDowelsUC");

            });
            TopDowelsCornerLbTextChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBarCorner.IsTopDowels && SelectedBarCorner.TopDowels == 0; }, (p) =>
            {
                TopDowelsView uc = ProccessInfoWalls.FindChild<TopDowelsView>(p, "TopDowelsUC");

            });
            SelectionTopTypeDowelsCornerChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBarCorner.IsTopDowels; }, (p) =>
            {
                TopDowelsView uc = ProccessInfoWalls.FindChild<TopDowelsView>(p, "TopDowelsUC");
                ShowLaTopDowelsCorner(uc);
                RefreshValueBar();
                ShowFixedToTopCorner = (SelectedWall != null && ConditionShowFixedToUpBarCorner()) ? Visibility.Visible : Visibility.Collapsed;
                DrawMain(p);
                DrawSection(p);

            });
            FixedTopBarDowelsCornerCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                int number = 0;
                for (int i = 0; i < SelectedWall.BarCornerModels.Count; i++)
                {
                    if (SelectedWall.BarCornerModels[i].IsTopDowels && SelectedWall.BarCornerModels[i].TopDowels == 0)
                    {
                        double l = (SelectedWall.SplitOverlap == 50) ? ((number % 2 != 0) ? (SelectedWall.Bar.Diameter * SelectedWall.Overlap) : (SelectedWall.Bar.Diameter * SelectedWall.Overlap * 2)) : (SelectedWall.Bar.Diameter * SelectedWall.Overlap);
                        SelectedWall.BarCornerModels[i].LbTopDowels = l;
                        SelectedWall.BarCornerModels[i].EvenTop = number % 2 != 0;
                        number++;
                    }
                }
                RefreshValueBar();
                DrawMain(p);
                DrawSection(p);
            });
            #endregion
        }
        #region Condition
        private bool ConditionShowFixedToUpBar()
        {
            if (SelectedWall == null) return false;
            if (WallsModel.InfoModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault() == null)
            {
                return false;
            }
            else
            {
                InfoModel infoModelUp = WallsModel.InfoModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault();
                BarMainModel barMainModelUp = WallsModel.BarMainModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault();

                ObservableCollection<BarModel> barModel = new ObservableCollection<BarModel>(SelectedWall.BarModels.Where(x => x.IsTopDowels && x.TopDowels == 0).ToList());
                if (barModel.Count == 0) return false;
                if (SelectedWall.IsCorner)
                {
                    if (infoModelUp.IsCorner)
                    {
                        if (barMainModelUp.BarCornerModels.Count == 0 || barMainModelUp.BarModels.Count == 0) return false;
                        if (barModel.Count != barMainModelUp.BarModels.Count) return false;
                    }
                    else
                    {
                        if (barMainModelUp.BarModels.Count == 0) return false;
                        ObservableCollection<BarModel> barModelUp = new ObservableCollection<BarModel>(barMainModelUp.BarModels.Where(x => (x.X0 >= infoModelUp.WestPosition + infoModelUp.L1) && (x.X0 <= infoModelUp.WestPosition + infoModelUp.L1 + infoModelUp.L2)).ToList());
                        if (barModel.Count != barModelUp.Count) return false;
                    }
                }
                else
                {
                    if (infoModelUp.IsCorner)
                    {
                        if (barMainModelUp.BarCornerModels.Count == 0 || barMainModelUp.BarModels.Count == 0) return false;
                        ObservableCollection<BarModel> barModel1 = new ObservableCollection<BarModel>(barModel.Where(x => (x.Location[x.Location.Count - 1].X >= infoModelUp.WestPosition + infoModelUp.L1) && (x.Location[x.Location.Count - 1].X <= infoModelUp.WestPosition + infoModelUp.L1 + infoModelUp.L2)).ToList());
                        if (barModel1.Count != barMainModelUp.BarModels.Count) return false;
                    }
                    else
                    {
                        if (barMainModelUp.BarModels.Count == 0) return false;
                        if (barModel.Count != barMainModelUp.BarModels.Count) return false;

                    }
                }
            }

            return true;
        }
        private bool ConditionShowFixedToUpBarCorner()
        {
            if (SelectedWall == null) return false;
            if (WallsModel.InfoModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault() == null)
            {
                return false;
            }
            else
            {
                InfoModel infoModelUp = WallsModel.InfoModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault();
                BarMainModel barMainModelUp = WallsModel.BarMainModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault();

                ObservableCollection<BarModel> barCornerModel = new ObservableCollection<BarModel>(SelectedWall.BarCornerModels.Where(x => x.IsTopDowels && x.TopDowels == 0).ToList());
                if (barCornerModel.Count == 0) return false;
                if (SelectedWall.IsCorner)
                {
                    if (infoModelUp.IsCorner)
                    {
                        if (barMainModelUp.BarCornerModels.Count == 0 || barMainModelUp.BarModels.Count == 0) return false;
                        if (barCornerModel.Count != barMainModelUp.BarCornerModels.Count) return false;
                    }
                    else
                    {
                        if (barMainModelUp.BarModels.Count == 0) return false;
                        ObservableCollection<BarModel> barModelUp = new ObservableCollection<BarModel>(barMainModelUp.BarModels.Where(x => ((x.X0 >= infoModelUp.WestPosition ) && (x.X0 <= infoModelUp.WestPosition + infoModelUp.L1 ))||((x.X0 >= infoModelUp.WestPosition+infoModelUp.L1+infoModelUp.L2) )).ToList());
                        if (barCornerModel.Count != barModelUp.Count) return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
        #endregion
        #region Method
        private void ShowWallNumberComboBox(TopDowelsView uc)
        {
            if (WallsModel.InfoModels.Count == 1)
            {
                uc.WallNumberTextBlock.Visibility = System.Windows.Visibility.Hidden;
                uc.WallNumberComboBox.Visibility = System.Windows.Visibility.Hidden;
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


        private void ShowLaTopDowelsCorner(TopDowelsView p)
        {
            if (SelectedBarCorner == null)
            {
                p.TopDowelsCornerLaTextBlock.Visibility = System.Windows.Visibility.Hidden;
                p.TopDowelsCornerLaTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                p.TopDowelsCornerLaTextBox.Visibility = System.Windows.Visibility.Hidden;
                p.TopDowelsCornerLbTextBlock.Visibility = System.Windows.Visibility.Hidden;
                p.TopDowelsCornerLbTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                p.TopDowelsCornerLbTextBox.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                if (SelectedBarCorner.IsTopDowels)
                {
                    if (SelectedBarCorner.TopDowels == 1)
                    {
                        p.TopDowelsCornerLaTextBlock.Visibility = System.Windows.Visibility.Visible;
                        p.TopDowelsCornerLaTextBlockUnit.Visibility = System.Windows.Visibility.Visible;
                        p.TopDowelsCornerLaTextBox.Visibility = System.Windows.Visibility.Visible;
                        p.TopDowelsCornerLbTextBlock.Visibility = System.Windows.Visibility.Hidden;
                        p.TopDowelsCornerLbTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                        p.TopDowelsCornerLbTextBox.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else
                    {
                        p.TopDowelsCornerLaTextBlock.Visibility = System.Windows.Visibility.Hidden;
                        p.TopDowelsCornerLaTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                        p.TopDowelsCornerLaTextBox.Visibility = System.Windows.Visibility.Hidden;
                        p.TopDowelsCornerLbTextBlock.Visibility = System.Windows.Visibility.Visible;
                        p.TopDowelsCornerLbTextBlockUnit.Visibility = System.Windows.Visibility.Visible;
                        p.TopDowelsCornerLbTextBox.Visibility = System.Windows.Visibility.Visible;
                    }
                }
                else
                {
                    p.TopDowelsCornerLaTextBlock.Visibility = System.Windows.Visibility.Hidden;
                    p.TopDowelsCornerLaTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                    p.TopDowelsCornerLaTextBox.Visibility = System.Windows.Visibility.Hidden;
                    p.TopDowelsCornerLbTextBlock.Visibility = System.Windows.Visibility.Hidden;
                    p.TopDowelsCornerLbTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                    p.TopDowelsCornerLbTextBox.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }
        #endregion
        #region DrawMenu
        private void DrawTopDowelsComboBox(TopDowelsView p)
        {
            DrawImage.DrawTopDowelsType0(p.TopDowelsTypeCanvas0);
            DrawImage.DrawTopDowelsType1(p.TopDowelsTypeCanvas1);
            DrawImage.DrawTopDowelsType0(p.TopDowelsCornerTypeCanvas0);
            DrawImage.DrawTopDowelsType1(p.TopDowelsCornerTypeCanvas1);
        }
        #endregion
        #region Draw
        private void DrawMain(WallShearWindow p)
        {
            p.MainCanvas.Children.Clear();
            DrawMainCanvas.DrawInfoWall(p.MainCanvas, WallsModel, WallsModel.SelectedIndexModel.SelectedWall);
            DrawMainCanvas.DrawBarMains(p.MainCanvas, WallsModel.DrawModel, SelectedWall, (SelectedBar == null) ? 1000 : SelectedBar.BarNumber, (SelectedBarCorner == null) ? 1000 : SelectedBarCorner.BarNumber);
            double top = WallsModel.DrawModel.Top - (WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall].TopPosition) / (WallsModel.DrawModel.Scale);
            if (SelectedWall.NumberWall == WallsModel.BarMainModels[WallsModel.BarMainModels.Count - 1].NumberWall) top -= WallsModel.AllBars[WallsModel.AllBars.Count - 1].Diameter * 20;
            p.scrollViewer.ScrollToBottom();
            p.scrollViewer.ScrollToVerticalOffset(top);
        }
        private void DrawSection(WallShearWindow p)
        {
            p.CanvasSection.Children.Clear();
            InfoModel infoModelUp = WallsModel.InfoModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault();
            StirrupModel stirrupModelUp = WallsModel.StirrupModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault();
            BarMainModel barMainModelUp = WallsModel.BarMainModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault();
            DrawMainCanvas.DrawSectionTopDowels(p.CanvasSection, WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall], WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall], SelectedWall, WallsModel.DrawModelSection, WallsModel.Cover, (SelectedBar == null) ? 1000 : WallsModel.SelectedIndexModel.SelectedMainBar, (SelectedBarCorner == null) ? 1000 : WallsModel.SelectedIndexModel.SelectedCornerMainBar, infoModelUp, stirrupModelUp, barMainModelUp);
        }
        #endregion
        #region Refresh
        private void GerAllDiameterStirrupBar(out double ds, out double dsUp, out double dsUpCorner, InfoModel infoModelUp = null)
        {
            ds = WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall].BarS.Diameter;
            dsUp = (infoModelUp == null) ? (WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall].BarS.Diameter) : (WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall + 1].BarS.Diameter);
            dsUpCorner = 0;
            if (infoModelUp == null)
            {
                if (SelectedWall.IsCorner)
                {
                    dsUpCorner = WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall].BarSCorner.Diameter;
                }
                else
                {
                    dsUpCorner = ds;
                }
            }
            else
            {
                if (infoModelUp.IsCorner)
                {
                    dsUpCorner = WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall + 1].BarSCorner.Diameter;
                }
                else
                {
                    dsUpCorner = WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall + 1].BarS.Diameter;
                }
            }
        }
        private void RefreshValueBar()
        {

            InfoModel infoModelUp = WallsModel.InfoModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault();
            GerAllDiameterStirrupBar(out double ds, out double dsUp, out double dsUpCorner, infoModelUp);
            SelectedWall.RefreshLocationBarModels(WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall], WallsModel.Cover, ds, dsUp, dsUpCorner, infoModelUp);
            if (SelectedWall.BarCornerModels.Count != 0)
            {
                SelectedWall.RefreshLocationBarCornerModels(WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall], WallsModel.Cover, ds, dsUp, dsUpCorner, infoModelUp);
            }

        }
        #endregion
    }
}
