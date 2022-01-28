using Autodesk.Revit.DB;
using R10_WallShear.View;
using System.Linq;
using System.Windows.Input;

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

        #region Icommand
        public ICommand LoadTopDowelsCommand { get; set; }
        public ICommand SelectionWallDowelsChangedCommand { get; set; }
        public ICommand SelectionBarChangedCommand { get; set; }
        public ICommand ApplyAllBarCommand { get; set; }

        public ICommand CheckTopDowelsCommand { get; set; }
        public ICommand TopDowelsLaTextChangedCommand { get; set; }
        public ICommand TopDowelsLbTextChangedCommand { get; set; }
        public ICommand SelectionTopTypeDowelsChangedCommand { get; set; }
        public ICommand PushMainTopBarDowelsCommand { get; set; }
        public ICommand PushCornerTopBarDowelsCommand { get; set; }

        public ICommand SelectionBarCornerChangedCommand { get; set; }
        public ICommand CheckTopDowelsCornerCommand { get; set; }
        public ICommand TopDowelsCornerLaTextChangedCommand { get; set; }
        public ICommand TopDowelsCornerLbTextChangedCommand { get; set; }
        public ICommand SelectionTopTypeDowelsCornerChangedCommand { get; set; }
        public ICommand PushMainTopBarCornerDowelsCommand { get; set; }
        public ICommand PushCornerTopBarCornerDowelsCommand { get; set; }

        #endregion
        public TopDowelsViewModel(Document document, WallsModel wallsModel)
        {
            #region Property
            Doc = document;
            WallsModel = wallsModel;
            #endregion
            #region Load
            LoadTopDowelsCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {;
                IsEnabledTopDowels = (SelectedBar != null);
                IsEnabledTopTypeDowels = (SelectedBar == null) ? false : (SelectedBar.IsTopDowels);
                IsEnabledTopDowelsCorner =  (SelectedBarCorner != null);
                IsEnabledTopTypeDowelsCorner = ((SelectedBarCorner == null) ? false : (SelectedBarCorner.IsTopDowels));
                TopDowelsView uc = ProccessInfoWalls.FindChild<TopDowelsView>(p, "TopDowelsUC");
                ShowWallNumberComboBox(uc);
                DrawTopDowelsComboBox(uc);
                ShowLaTopDowels(uc);
                ShowCorner(uc);
                ShowPushTop(uc);
                DrawInfo(p);
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
                  ShowCorner(uc);
                  ShowPushTop(uc);
                  DrawInfo(p);
                  DrawSection(p);
              });
            SelectionBarChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBar != null; }, (p) =>
            {
                IsEnabledTopDowels = (SelectedWall.BarModels.Count != 0) && (SelectedBar != null);
                IsEnabledTopTypeDowels = (SelectedBar == null) ? false : (SelectedBar.IsTopDowels);
                TopDowelsView uc = ProccessInfoWalls.FindChild<TopDowelsView>(p, "TopDowelsUC");
                ShowLaTopDowels(uc);
                ShowPushTop(uc);
                RefreshValueBar();
                DrawInfo(p);
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
                ShowPushTop(uc);
                RefreshValueBar();
                DrawInfo(p);
                DrawSection(p);
            });
            TopDowelsLaTextChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBar.IsTopDowels && SelectedBar.TopDowels == 1; }, (p) =>
                {
                    TopDowelsView uc = ProccessInfoWalls.FindChild<TopDowelsView>(p, "TopDowelsUC");

                    DrawInfo(p);
                    DrawSection(p);
                });
            TopDowelsLbTextChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBar.IsTopDowels && SelectedBar.TopDowels == 0; }, (p) =>
            {
                TopDowelsView uc = ProccessInfoWalls.FindChild<TopDowelsView>(p, "TopDowelsUC");

                DrawInfo(p);
                DrawSection(p);
            });
            SelectionTopTypeDowelsChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBar.IsTopDowels; }, (p) =>
            {
                TopDowelsView uc = ProccessInfoWalls.FindChild<TopDowelsView>(p, "TopDowelsUC");
                ShowLaTopDowels(uc);
                ShowPushTop(uc);
                if ((SelectedBar.TopDowels != 0)) { SelectedBar.PushTop = "NONE"; }
                RefreshValueBar();
                DrawInfo(p);
                DrawSection(p);
            });
            PushMainTopBarDowelsCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBar!=null&& !SelectedBar.PushTop.Equals("Main"); }, (p) =>
            {
                SelectedBar.PushTop = SelectedBar.GetPushTop(false);
            });
            PushCornerTopBarDowelsCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBar != null && !SelectedBar.PushTop.Equals("Corner"); }, (p) =>
            {
                SelectedBar.PushTop = SelectedBar.GetPushTop(true);
            });
            #endregion
            #region CornerTopDowels
            SelectionBarCornerChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBarCorner != null; }, (p) =>
            {
                IsEnabledTopDowelsCorner = (SelectedBarCorner != null);
                IsEnabledTopTypeDowelsCorner = ((SelectedBarCorner == null) ? false : (SelectedBarCorner.IsTopDowels));
                TopDowelsView uc = ProccessInfoWalls.FindChild<TopDowelsView>(p, "TopDowelsUC");
                ShowLaTopDowelsCorner(uc);
                ShowPushTopCorner(uc);
                DrawInfo(p);
                DrawSection(p);
            });
            CheckTopDowelsCornerCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBarCorner != null; }, (p) =>
            {
                TopDowelsView uc = ProccessInfoWalls.FindChild<TopDowelsView>(p, "TopDowelsUC");
                ShowLaTopDowelsCorner(uc);
                ShowPushTopCorner(uc);
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
                ShowPushTopCorner(uc);
                if ((SelectedBarCorner.TopDowels != 0)) { SelectedBarCorner.PushTop = "NONE"; }
                DrawInfo(p);
                DrawSection(p);
            });
            PushMainTopBarCornerDowelsCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBarCorner != null && !SelectedBarCorner.PushTop.Equals("Main"); }, (p) =>
            {
                SelectedBarCorner.PushTop = SelectedBarCorner.GetPushTop(false);
            });
            PushCornerTopBarCornerDowelsCommand = new RelayCommand<WallShearWindow>((p) => { return  SelectedBarCorner != null && !SelectedBarCorner.PushTop.Equals("Corner"); }, (p) =>
            {
                SelectedBarCorner.PushTop = SelectedBarCorner.GetPushTop(true);
            });
            #endregion
        }
        #region Method
        private void ShowWallNumberComboBox(TopDowelsView uc)
        {
            if (WallsModel.InfoModels.Count == 1)
            {
                uc.WallNumberTextBlock.Visibility = System.Windows.Visibility.Hidden;
                uc.WallNumberComboBox.Visibility = System.Windows.Visibility.Hidden;
            }
        }
        private void ShowPushTop(TopDowelsView p)
        {
            if (SelectedBar==null||!SelectedBar.IsTopDowels||SelectedBar.TopDowels==1)
            {
                p.PushCornerTop.Visibility = System.Windows.Visibility.Hidden;
                p.PushMainTop.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                InfoModel infoModelUp = WallsModel.InfoModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault();
                if (infoModelUp==null)
                {
                    p.PushCornerTop.Visibility = System.Windows.Visibility.Hidden;
                    p.PushMainTop.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    
                    if (infoModelUp.IsCorner)
                    {
                        int nx = SelectedWall.nx;
                        int ny = SelectedWall.ny;
                        if ((SelectedBar.BarNumber>nx&&SelectedBar.BarNumber<=nx+ny-2)||(SelectedBar.BarNumber>2*nx+ny-2&&SelectedBar.BarNumber<=2*nx+2*(ny-2)))
                        {
                            p.PushCornerTop.Visibility = System.Windows.Visibility.Visible;
                            p.PushMainTop.Visibility = System.Windows.Visibility.Hidden;
                        }
                        else
                        {
                            p.PushCornerTop.Visibility = System.Windows.Visibility.Hidden;
                            p.PushMainTop.Visibility = System.Windows.Visibility.Visible;
                        }
                    }
                    else
                    {

                        p.PushCornerTop.Visibility = System.Windows.Visibility.Hidden;
                        p.PushMainTop.Visibility = System.Windows.Visibility.Visible;
                    }
                }
            }
        }
        private bool ConditionPushMainTop()
        {
            if (SelectedBar == null || !SelectedBar.IsTopDowels || SelectedBar.TopDowels == 1)
            {
                return false;
            }
            else
            {
                InfoModel infoModelUp = WallsModel.InfoModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault();
                if (infoModelUp == null)
                {
                    return false;
                }
                else
                {

                    if (infoModelUp.IsCorner)
                    {
                        //if (SelectedBar.X0 < infoModelUp.WestPosition + infoModelUp.L1 || SelectedBar.X0 > infoModelUp.WestPosition + infoModelUp.L1 + infoModelUp.L2)
                        //{
                        //    return false;
                        //}
                        //else
                        //{
                        //    return true;
                        //}
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
        private bool ConditionPushCornerTop()
        {
            if (SelectedBar == null || !SelectedBar.IsTopDowels || SelectedBar.TopDowels == 1)
            {
                return false;
            }
            else
            {
                InfoModel infoModelUp = WallsModel.InfoModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault();
                if (infoModelUp == null)
                {
                    return false;
                }
                else
                {

                    if (infoModelUp.IsCorner)
                    {
                        //if (SelectedBar.X0 < infoModelUp.WestPosition + infoModelUp.L1 || SelectedBar.X0 > infoModelUp.WestPosition + infoModelUp.L1 + infoModelUp.L2)
                        //{
                        //    return true;
                        //}
                        //else
                        //{
                        //    return false;
                        //}
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        private void ShowPushTopCorner(TopDowelsView p)
        {
            if (SelectedBarCorner == null || !SelectedBarCorner.IsTopDowels || SelectedBarCorner.TopDowels == 1)
            {
                p.PushCornerTopCorner.Visibility = System.Windows.Visibility.Hidden;
                p.PushMainTopCorner.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                InfoModel infoModelUp = WallsModel.InfoModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault();
                if (infoModelUp == null)
                {
                    p.PushCornerTopCorner.Visibility = System.Windows.Visibility.Hidden;
                    p.PushMainTopCorner.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {

                    if (infoModelUp.IsCorner)
                    {
                        //if (SelectedBarCorner.X0 < infoModelUp.WestPosition + infoModelUp.L1 || SelectedBarCorner.X0 > infoModelUp.WestPosition + infoModelUp.L1 + infoModelUp.L2)
                        //{
                        //    p.PushCornerTopCorner.Visibility = System.Windows.Visibility.Visible;
                        //    p.PushMainTopCorner.Visibility = System.Windows.Visibility.Hidden;
                        //}
                        //else
                        //{
                        //    p.PushCornerTopCorner.Visibility = System.Windows.Visibility.Hidden;
                        //    p.PushMainTopCorner.Visibility = System.Windows.Visibility.Visible;
                        //}
                        p.PushCornerTopCorner.Visibility = System.Windows.Visibility.Visible;
                        p.PushMainTopCorner.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        p.PushCornerTopCorner.Visibility = System.Windows.Visibility.Hidden;
                        p.PushMainTopCorner.Visibility = System.Windows.Visibility.Visible;
                    }
                }
            }
        }
        private bool ConditionPushMainTopCorner()
        {
            if (SelectedBarCorner == null || !SelectedBarCorner.IsTopDowels || SelectedBarCorner.TopDowels == 1)
            {
                return false;
            }
            else
            {
                InfoModel infoModelUp = WallsModel.InfoModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault();
                if (infoModelUp == null)
                {
                    return false;
                }
                else
                {

                    if (infoModelUp.IsCorner)
                    {
                        //if (SelectedBarCorner.X0 < infoModelUp.WestPosition + infoModelUp.L1 || SelectedBarCorner.X0 > infoModelUp.WestPosition + infoModelUp.L1 + infoModelUp.L2)
                        //{
                        //    return false;
                        //}
                        //else
                        //{
                        //    return true;
                        //}
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
        private bool ConditionPushCornerTopCorner()
        {
            if (SelectedBarCorner == null || !SelectedBarCorner.IsTopDowels || SelectedBarCorner.TopDowels == 1)
            {
                return false;
            }
            else
            {
                InfoModel infoModelUp = WallsModel.InfoModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault();
                if (infoModelUp == null)
                {
                    return false;
                }
                else
                {

                    if (infoModelUp.IsCorner)
                    {
                        //if (SelectedBarCorner.X0 < infoModelUp.WestPosition + infoModelUp.L1 || SelectedBarCorner.X0 > infoModelUp.WestPosition + infoModelUp.L1 + infoModelUp.L2)
                        //{
                        //    return true;
                        //}
                        //else
                        //{
                        //    return false;
                        //}
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
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
        
        private void ShowCorner(TopDowelsView p)
        {
            if (SelectedWall.IsCorner)
            {
                p.CornerDowelsGrid.Visibility = System.Windows.Visibility.Visible;
                p.CornerTopDowelsGrid.Visibility = System.Windows.Visibility.Visible;
                
            }
            else
            {
                p.CornerDowelsGrid.Visibility = System.Windows.Visibility.Hidden;
                p.CornerTopDowelsGrid.Visibility = System.Windows.Visibility.Hidden;
            }
            ShowLaTopDowelsCorner(p);
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
        private void DrawInfo(WallShearWindow p)
        {
            p.MainCanvas.Children.Clear();
            DrawMainCanvas.DrawInfoColumns(p.MainCanvas, WallsModel, WallsModel.SelectedIndexModel.SelectedWall);
            DrawMainCanvas.DrawBarMains(p.MainCanvas, WallsModel.DrawModel, SelectedWall, (SelectedBar == null) ? 1000 : SelectedBar.BarNumber, (SelectedBarCorner == null) ? 1000 : SelectedBarCorner.BarNumber);
            double top = WallsModel.DrawModel.Top - (WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall].TopPosition) / (WallsModel.DrawModel.Scale);
            if (SelectedWall.NumberWall == WallsModel.BarMainModels[WallsModel.BarMainModels.Count - 1].NumberWall) top -= WallsModel.AllBars[WallsModel.AllBars.Count-1].Diameter * 20;
            p.scrollViewer.ScrollToBottom();
            p.scrollViewer.ScrollToVerticalOffset(top);
        }
        private void DrawSection(WallShearWindow p)
        {
            p.CanvasSection.Children.Clear();
            InfoModel infoModelUp = WallsModel.InfoModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault();
            StirrupModel stirrupModelUp = WallsModel.StirrupModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault();
            BarMainModel barMainModelUp = WallsModel.BarMainModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault();
            DrawMainCanvas.DrawSectionTopDowels(p.CanvasSection, WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall], WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall], SelectedWall, WallsModel.DrawModelSection, WallsModel.Cover,(SelectedBar==null)?1000: WallsModel.SelectedIndexModel.SelectedMainBar,(SelectedBarCorner==null)?1000: WallsModel.SelectedIndexModel.SelectedCornerMainBar, infoModelUp,stirrupModelUp,barMainModelUp);
        }
        #endregion
        #region Refresh
        private void RefreshValueBar()
        {
            InfoModel infoModelUp = WallsModel.InfoModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault();
            double ds = WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall].BarS.Diameter;
            double dsUp = (infoModelUp == null) ? (WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall].BarS.Diameter) : (WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall + 1].BarS.Diameter);
            double dsUpCorner = 0;
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
            SelectedWall.RefreshLocationBarModels(WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall], WallsModel.Cover, ds, dsUp, dsUpCorner, infoModelUp);
        }
        #endregion
    }
}
