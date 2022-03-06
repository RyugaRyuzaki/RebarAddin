using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfCustomControls;
using R10_WallShear.LanguageModel;
using R10_WallShear.View;
using Visibility = System.Windows.Visibility;
namespace R10_WallShear.ViewModel
{
    public class BottomDowelsViewModel :BaseViewModel
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
        private bool _IsEnabledBottomDowels;
        public bool IsEnabledBottomDowels { get => _IsEnabledBottomDowels; set { _IsEnabledBottomDowels = value; OnPropertyChanged(); } }
        private bool _IsEnabledBottomTypeDowels;
        public bool IsEnabledBottomTypeDowels { get => _IsEnabledBottomTypeDowels; set { _IsEnabledBottomTypeDowels = value; OnPropertyChanged(); } }
        private bool _IsEnabledBottomDowelsCorner;
        public bool IsEnabledBottomDowelsCorner { get => _IsEnabledBottomDowelsCorner; set { _IsEnabledBottomDowelsCorner = value; OnPropertyChanged(); } }
        private bool _IsEnabledBottomTypeDowelsCorner;
        public bool IsEnabledBottomTypeDowelsCorner { get => _IsEnabledBottomTypeDowelsCorner; set { _IsEnabledBottomTypeDowelsCorner = value; OnPropertyChanged(); } }
        private bool _IsLock;
        public bool IsLock { get => _IsLock; set { _IsLock = value; OnPropertyChanged(); } }
        private Visibility _ShowCorner;
        public Visibility ShowCorner { get { return _ShowCorner; } set { _ShowCorner = value; OnPropertyChanged(); } }
        private Visibility _ShowAddBar;
        public Visibility ShowAddBar { get { return _ShowAddBar; } set { _ShowAddBar = value; OnPropertyChanged(); } }
        #region Command
        public ICommand LoadBottomDowelsViewCommand { get; set; }
        public ICommand SelectionWallDowelsChangedCommand { get; set; }
        public ICommand ApplyAllBarCommand { get; set; }
        public ICommand CheckBottomDowelsCommand { get; set; }
        public ICommand SelectionBottomTypeDowelsChangedCommand { get; set; }
        public ICommand SelectionBarChangedCommand { get; set; }
      
        public ICommand BottomDowelsLaTextChangedCommand { get; set; }
        public ICommand BottomDowelsLbTextChangedCommand { get; set; }
        public ICommand BottomDowelsLcTextChangedCommand { get; set; }
        public ICommand FixedBottomBarDowelsCommand { get; set; }

        public ICommand CheckBottomDowelsCornerCommand { get; set; }
        public ICommand SelectionBottomTypeDowelsCornerChangedCommand { get; set; }
        public ICommand SelectionBarCornerChangedCommand { get; set; }
        public ICommand BottomDowelsCornerLaTextChangedCommand { get; set; }
        public ICommand BottomDowelsCornerLbTextChangedCommand { get; set; }
        public ICommand BottomDowelsCornerLcTextChangedCommand { get; set; }
        public ICommand FixedBottomBarDowelsCornerCommand { get; set; }
        #endregion
        private Languages _Languages;
        public Languages Languages { get { return _Languages; } set { _Languages = value; OnPropertyChanged(); } }
        public BottomDowelsViewModel(Document document, WallsModel wallsModel, Languages languages)
        {
            #region Property
            Doc = document;
            WallsModel = wallsModel; Languages = languages;
            #endregion
            #region  Load
            LoadBottomDowelsViewCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                IsEnabledBottomDowels =  (SelectedBar != null);
                IsEnabledBottomTypeDowels = (SelectedBar == null) ? false : (SelectedBar.IsBottomDowels);
                IsEnabledBottomDowelsCorner = (SelectedBarCorner != null);
                IsEnabledBottomTypeDowelsCorner = ((SelectedBarCorner == null) ? false : (SelectedBarCorner.IsBottomDowels));
                BottomDowelsView uc = VisualTreeHelper.FindChild<BottomDowelsView>(p, "BottomDowelsUC");
                ShowCorner = (SelectedWall != null && SelectedWall.IsCorner) ? Visibility.Visible : Visibility.Collapsed;
                InfoModel infoModelDown = WallsModel.InfoModels.Where(x => x.NumberWall == SelectedWall.NumberWall - 1).FirstOrDefault();
                ShowAddBar = (SelectedWall != null && SelectedWall.IsCorner&&SelectedWall.NumberWall!=1&& infoModelDown!=null&& !infoModelDown.IsCorner) ? Visibility.Visible : Visibility.Collapsed;
                DrawBottomDowelsComboBox(uc);
                ShowLaBottomDowels(uc);
                ShowLaBottomDowelsCorner(uc);
                DrawMain(p);
                DrawSection(p);
            });
            SelectionWallDowelsChangedCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                if (SelectedWall.BarModels.Count != 0) WallsModel.SelectedIndexModel.SelectedMainBar = 0;
                if (SelectedWall.BarCornerModels.Count != 0) WallsModel.SelectedIndexModel.SelectedCornerMainBar = 0;
                IsEnabledBottomDowels = (SelectedBar != null);
                IsEnabledBottomTypeDowels = (SelectedBar == null) ? false : (SelectedBar.IsBottomDowels);
                IsEnabledBottomDowelsCorner = (SelectedBarCorner != null);
                IsEnabledBottomTypeDowelsCorner = ((SelectedBarCorner == null) ? false : (SelectedBarCorner.IsBottomDowels));
                ShowCorner = (SelectedWall != null && SelectedWall.IsCorner) ? Visibility.Visible : Visibility.Collapsed;
                InfoModel infoModelDown = WallsModel.InfoModels.Where(x => x.NumberWall == SelectedWall.NumberWall - 1).FirstOrDefault();
                ShowAddBar = (SelectedWall != null && SelectedWall.IsCorner && SelectedWall.NumberWall != 1 && infoModelDown != null && !infoModelDown.IsCorner) ? Visibility.Visible : Visibility.Collapsed;
                BottomDowelsView uc = VisualTreeHelper.FindChild<BottomDowelsView>(p, "BottomDowelsUC");
                ShowLaBottomDowels(uc);
                ShowLaBottomDowelsCorner(uc);
                ShowFixedBottom(uc);
                DrawMain(p);
                DrawSection(p);
            });
            #endregion
            #region Main
            CheckBottomDowelsCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBar != null; }, (p) =>
            {
                IsEnabledBottomTypeDowels = (SelectedBar.IsBottomDowels);
                RefreshValueBar();
                BottomDowelsView uc = VisualTreeHelper.FindChild<BottomDowelsView>(p, "BottomDowelsUC");
                ShowLaBottomDowels(uc);
                ShowFixedBottom(uc);
                DrawMain(p);
                DrawSection(p);

            });
            SelectionBottomTypeDowelsChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBar != null&&SelectedBar.IsBottomDowels; }, (p) =>
            {
                RefreshValueBar();
                BottomDowelsView uc = VisualTreeHelper.FindChild<BottomDowelsView>(p, "BottomDowelsUC");
                ShowLaBottomDowels(uc);
                ShowFixedBottom(uc);
                DrawMain(p);
                DrawSection(p);

            });
            SelectionBarChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedWall != null; }, (p) =>
            {
                IsEnabledBottomDowels = (SelectedBar != null);
                IsEnabledBottomTypeDowels = (SelectedBar == null) ? false : (SelectedBar.IsBottomDowels);
                
                BottomDowelsView uc = VisualTreeHelper.FindChild<BottomDowelsView>(p, "BottomDowelsUC");
                ShowLaBottomDowels(uc);

                DrawMain(p);
                DrawSection(p);

            });
            BottomDowelsLaTextChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBar != null && SelectedBar.IsBottomDowels; }, (p) =>
            {
                BottomDowelsView uc = VisualTreeHelper.FindChild<BottomDowelsView>(p, "BottomDowelsUC");
                if (double.TryParse(uc.BottomDowelsLaTextBox.Text.ToString(), out double S))
                {
                    RefreshValueBar();
                    DrawMain(p);
                    DrawSection(p);
                }

            });
            BottomDowelsLbTextChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBar != null && SelectedBar.IsBottomDowels; }, (p) =>
            {
                BottomDowelsView uc = VisualTreeHelper.FindChild<BottomDowelsView>(p, "BottomDowelsUC");
                if (double.TryParse(uc.BottomDowelsLbTextBox.Text.ToString(), out double S))
                {
                    RefreshValueBar();
                    DrawMain(p);
                    DrawSection(p);
                }

            });
            BottomDowelsLcTextChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBar != null && SelectedBar.IsBottomDowels; }, (p) =>
            {
                BottomDowelsView uc = VisualTreeHelper.FindChild<BottomDowelsView>(p, "BottomDowelsUC");
                if (double.TryParse(uc.BottomDowelsLcTextBox.Text.ToString(), out double S))
                {
                    RefreshValueBar();
                    DrawMain(p);
                    DrawSection(p);
                }

            });
            FixedBottomBarDowelsCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {


            });
            #endregion
            #region Corner

            CheckBottomDowelsCornerCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBarCorner != null; }, (p) =>
            {
                IsEnabledBottomTypeDowelsCorner = (SelectedBarCorner.IsBottomDowels);
                RefreshValueBar();
                BottomDowelsView uc = VisualTreeHelper.FindChild<BottomDowelsView>(p, "BottomDowelsUC");
                ShowLaBottomDowelsCorner(uc);
                ShowFixedBottomCorner(uc);
                DrawMain(p);
                DrawSection(p);

            });
            SelectionBottomTypeDowelsCornerChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBarCorner != null && SelectedBarCorner.IsBottomDowels; }, (p) =>
            {
                RefreshValueBar();
                BottomDowelsView uc = VisualTreeHelper.FindChild<BottomDowelsView>(p, "BottomDowelsUC");
                ShowLaBottomDowelsCorner(uc);
                ShowFixedBottomCorner(uc);
                DrawMain(p);
                DrawSection(p);

            });
            SelectionBarCornerChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedWall!=null; }, (p) =>
            {
                IsEnabledBottomDowelsCorner = (SelectedBarCorner != null);
                IsEnabledBottomTypeDowelsCorner = ((SelectedBarCorner == null) ? false : (SelectedBarCorner.IsTopDowels));
                BottomDowelsView uc = VisualTreeHelper.FindChild<BottomDowelsView>(p, "BottomDowelsUC");
                ShowLaBottomDowelsCorner(uc);
                DrawMain(p);
                DrawSection(p);
            });
            BottomDowelsCornerLaTextChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBarCorner != null && SelectedBarCorner.IsBottomDowels;  }, (p) =>
            {
                BottomDowelsView uc = VisualTreeHelper.FindChild<BottomDowelsView>(p, "BottomDowelsUC");
                if (double.TryParse(uc.BottomDowelsCornerLaTextBox.Text.ToString(), out double S))
                {
                    RefreshValueBar();
                    DrawMain(p);
                    DrawSection(p);
                }

            });
            BottomDowelsCornerLbTextChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBarCorner != null && SelectedBarCorner.IsBottomDowels;  }, (p) =>
            {
                BottomDowelsView uc = VisualTreeHelper.FindChild<BottomDowelsView>(p, "BottomDowelsUC");
                if (double.TryParse(uc.BottomDowelsCornerLbTextBox.Text.ToString(), out double S))
                {
                    RefreshValueBar();
                    DrawMain(p);
                    DrawSection(p);
                }

            });
            BottomDowelsCornerLcTextChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedBarCorner != null && SelectedBarCorner.IsBottomDowels;  }, (p) =>
            {
                BottomDowelsView uc = VisualTreeHelper.FindChild<BottomDowelsView>(p, "BottomDowelsUC");
                if (double.TryParse(uc.BottomDowelsCornerLcTextBox.Text.ToString(), out double S))
                {
                    RefreshValueBar();
                    DrawMain(p);
                    DrawSection(p);
                }

            });
            #endregion

        }


        #region Method
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
        private void ShowFixedBottom(BottomDowelsView uc)
        {
            
        }
        private void ShowFixedBottomCorner(BottomDowelsView uc)
        {

        }
        private void ShowLaBottomDowels(BottomDowelsView p)
        {
            if (SelectedBar != null && SelectedBar.IsBottomDowels && SelectedBar.BottomDowels == 1)
            {
                p.BottomDowelsLaTextBlock.Visibility = System.Windows.Visibility.Visible;
                p.BottomDowelsLaTextBox.Visibility = System.Windows.Visibility.Visible;
                p.BottomDowelsLaTextBlockUnit.Visibility = System.Windows.Visibility.Visible;
                p.BottomDowelsLbTextBlock.Visibility = System.Windows.Visibility.Visible;
                p.BottomDowelsLbTextBox.Visibility = System.Windows.Visibility.Visible;
                p.BottomDowelsLbTextBlockUnit.Visibility = System.Windows.Visibility.Visible;

                p.BottomDowelsLcTextBlock.Visibility = System.Windows.Visibility.Hidden;
                p.BottomDowelsLcTextBox.Visibility = System.Windows.Visibility.Hidden;
                p.BottomDowelsLcTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                p.BottomDowelsLaTextBlock.Visibility = System.Windows.Visibility.Hidden;
                p.BottomDowelsLaTextBox.Visibility = System.Windows.Visibility.Hidden;
                p.BottomDowelsLaTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                p.BottomDowelsLbTextBlock.Visibility = System.Windows.Visibility.Hidden;
                p.BottomDowelsLbTextBox.Visibility = System.Windows.Visibility.Hidden;
                p.BottomDowelsLbTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                p.BottomDowelsLcTextBlock.Visibility = System.Windows.Visibility.Visible;
                p.BottomDowelsLcTextBox.Visibility = System.Windows.Visibility.Visible;
                p.BottomDowelsLcTextBlockUnit.Visibility = System.Windows.Visibility.Visible;
            }
            if (SelectedBar != null && SelectedBar.IsBottomDowels && SelectedBar.BottomDowels == 0)
            {
                p.BottomDowelsLcTextBlock.Visibility = System.Windows.Visibility.Visible;
                p.BottomDowelsLcTextBox.Visibility = System.Windows.Visibility.Visible;
                p.BottomDowelsLcTextBlockUnit.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                p.BottomDowelsLcTextBlock.Visibility = System.Windows.Visibility.Hidden;
                p.BottomDowelsLcTextBox.Visibility = System.Windows.Visibility.Hidden;
                p.BottomDowelsLcTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
            }
        }
        private void ShowLaBottomDowelsCorner(BottomDowelsView p)
        {
            if (SelectedBarCorner != null && SelectedBarCorner.IsBottomDowels && SelectedBarCorner.BottomDowels == 1)
            {
                p.BottomDowelsCornerLaTextBlock.Visibility = System.Windows.Visibility.Visible;
                p.BottomDowelsCornerLaTextBox.Visibility = System.Windows.Visibility.Visible;
                p.BottomDowelsCornerLaTextBlockUnit.Visibility = System.Windows.Visibility.Visible;
                p.BottomDowelsCornerLbTextBlock.Visibility = System.Windows.Visibility.Visible;
                p.BottomDowelsCornerLbTextBox.Visibility = System.Windows.Visibility.Visible;
                p.BottomDowelsCornerLbTextBlockUnit.Visibility = System.Windows.Visibility.Visible;

                p.BottomDowelsCornerLcTextBlock.Visibility = System.Windows.Visibility.Hidden;
                p.BottomDowelsCornerLcTextBox.Visibility = System.Windows.Visibility.Hidden;
                p.BottomDowelsCornerLcTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                p.BottomDowelsCornerLaTextBlock.Visibility = System.Windows.Visibility.Hidden;
                p.BottomDowelsCornerLaTextBox.Visibility = System.Windows.Visibility.Hidden;
                p.BottomDowelsCornerLaTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                p.BottomDowelsCornerLbTextBlock.Visibility = System.Windows.Visibility.Hidden;
                p.BottomDowelsCornerLbTextBox.Visibility = System.Windows.Visibility.Hidden;
                p.BottomDowelsCornerLbTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                p.BottomDowelsCornerLcTextBlock.Visibility = System.Windows.Visibility.Visible;
                p.BottomDowelsCornerLcTextBox.Visibility = System.Windows.Visibility.Visible;
                p.BottomDowelsCornerLcTextBlockUnit.Visibility = System.Windows.Visibility.Visible;
            }
            if (SelectedBarCorner != null && SelectedBarCorner.IsBottomDowels && SelectedBarCorner.BottomDowels == 0)
            {
                p.BottomDowelsCornerLcTextBlock.Visibility = System.Windows.Visibility.Visible;
                p.BottomDowelsCornerLcTextBox.Visibility = System.Windows.Visibility.Visible;
                p.BottomDowelsCornerLcTextBlockUnit.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                p.BottomDowelsCornerLcTextBlock.Visibility = System.Windows.Visibility.Hidden;
                p.BottomDowelsCornerLcTextBox.Visibility = System.Windows.Visibility.Hidden;
                p.BottomDowelsCornerLcTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
            }
        }
        #endregion

        #region
        private void DrawBottomDowelsComboBox(BottomDowelsView p)
        {
            DrawImage.DrawBottomDowelsType1(p.BottomDowelsTypeCanvas1);
            DrawImage.DrawBottomDowelsType0(p.BottomDowelsTypeCanvas0);
            DrawImage.DrawBottomDowelsType1(p.BottomDowelsCornerTypeCanvas1);
            DrawImage.DrawBottomDowelsType0(p.BottomDowelsCornerTypeCanvas0);
        }
        private void DrawSection(BottomDowelsView uc)
        {
            throw new NotImplementedException();
        }

        private void DrawMain(WallShearWindow p)
        {
            p.MainCanvas.Children.Clear();
            DrawMainCanvas.DrawInfoWall(p.MainCanvas, WallsModel, WallsModel.SelectedIndexModel.SelectedWall);
            DrawMainCanvas.DrawBarMains(p.MainCanvas, WallsModel.DrawModel, SelectedWall, (SelectedBar == null) ? 1000 : SelectedBar.BarNumber, (SelectedBarCorner == null) ? 1000 : SelectedBarCorner.BarNumber);
            double bottom = WallsModel.DrawModel.Top - (WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall].BottomPosition) / (WallsModel.DrawModel.Scale);
            p.scrollViewer.ScrollToBottom();
            p.scrollViewer.ScrollToVerticalOffset(bottom);
        }
        private void DrawSection(WallShearWindow p)
        {
            p.CanvasSection.Children.Clear();
            InfoModel infoModelUp = WallsModel.InfoModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault();
            StirrupModel stirrupModelUp = WallsModel.StirrupModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault();
            BarMainModel barMainModelUp = WallsModel.BarMainModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault();
            DrawMainCanvas.DrawSectionBottomDowels(p.CanvasSection, WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall], WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall], SelectedWall, WallsModel.DrawModelSection, WallsModel.Cover, (SelectedBar == null) ? 1000 : WallsModel.SelectedIndexModel.SelectedMainBar, (SelectedBarCorner == null) ? 1000 : WallsModel.SelectedIndexModel.SelectedCornerMainBar, infoModelUp, stirrupModelUp, barMainModelUp);
        }
        #endregion
    }
}
