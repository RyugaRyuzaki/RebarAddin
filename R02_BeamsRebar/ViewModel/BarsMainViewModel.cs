
using Autodesk.Revit.DB;
using R02_BeamsRebar.View;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace R02_BeamsRebar.ViewModel
{
    public class BarsMainViewModel:BaseViewModel
    {
        #region Property
        public Document Doc;
        private BeamsModel _BeamsModel;
        public BeamsModel BeamsModel { get => _BeamsModel; set { _BeamsModel = value; OnPropertyChanged();
                if (BeamsModel!=null)
                {
                    if (BeamsModel.MainTopBarModel.Count==1)
                    {
                        IsStyleMainTop = false;
                    }
                }
            } }
        #endregion
        #region Selected
        private bool _IsStyleMainTop;
        public bool IsStyleMainTop { get => _IsStyleMainTop; set { _IsStyleMainTop = value; OnPropertyChanged(); } }
        private MainTopBarModel _SelectedMainTop;
        public MainTopBarModel SelectedMainTop { get => _SelectedMainTop; set { _SelectedMainTop = value; OnPropertyChanged(); } }
        private List<string> _AllStyleTopBar;
        public List<string> AllStyleTopBar { get => _AllStyleTopBar; set { _AllStyleTopBar = value; OnPropertyChanged(); } }
        private MainBottomBarModel _SelectedMainBottom;
        public MainBottomBarModel SelectedMainBottom { get => _SelectedMainBottom; set { _SelectedMainBottom = value; OnPropertyChanged(); } }
        #endregion
        #region Command
        public ICommand LoadBarsMainViewCommand { get; set; }
        public ICommand SelectionChangedStyleMainTopBarCommand { get; set; }
        public ICommand SelectionChangedMainTopCommand { get; set; }
        public ICommand SelectionChangedBarTopCommand { get; set; }
        public ICommand SelectionChangedBarTopSCommand { get; set; }
        public ICommand SelectionChangedBarNumberBarTopCommand { get; set; }
        public ICommand SelectionChangedBarNumberBarTopSCommand { get; set; }
        public ICommand TopLaTextChangedCommand { get; set; }
        public ICommand TopLbTextChangedCommand { get; set; }
        public ICommand TopExaTextChangedCommand { get; set; }
        public ICommand TopExbTextChangedCommand { get; set; }
        public ICommand TopSLaTextChangedCommand { get; set; }
        public ICommand TopSLbTextChangedCommand { get; set; }

        public ICommand SelectionChangedMainBottomCommand { get; set; }
        public ICommand SelectionChangedBarBottomCommand { get; set; }
        public ICommand SelectionChangedNumberBarBottomCommand { get; set; }
        public ICommand BottomLaTextChangedCommand { get; set; }
        public ICommand BottomLbTextChangedCommand { get; set; }
        public ICommand BottomExaTextChangedCommand { get; set; }
        public ICommand BottomExbTextChangedCommand { get; set; }
        #endregion
        public BarsMainViewModel(Document document,BeamsModel beamsModel)
        {
            #region Get Property
            Doc = document;
            BeamsModel = beamsModel;
            #endregion
            #region
            AllStyleTopBar = new List<string>();
            AllStyleTopBar.Add("Single Bar");
            AllStyleTopBar.Add("Multiple Bar");
            #endregion
            #region Load
            LoadBarsMainViewCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                p.canvas.Width = BeamsModel.DrawModel.Width;
                p.canvas.Height = BeamsModel.DrawModel.Height;
                BarsMainView uc = ProcessInfoBeamRebar.FindChild<BarsMainView>(p, "BarsMainUC");
                if (BeamsModel.MainTopBarModel.Count==1)
                {
                    IsStyleMainTop = false;
                }
                else
                {
                    IsStyleMainTop = true;
                }
                if (BeamsModel.SelectedIndexModel.StyleMainTop == 0)
                {
                    HiddenMainTop(uc);
                }
                else
                {
                    VisibleMainTop(uc);
                }
                MainBarImage.DrawMainTopBar(uc.canvasTop);
                MainBarImage.DrawMainBottomBar(uc.canvasBottom);
                LoadBarsMainView(p);
                if (BeamsModel.SelectedIndexModel.StyleMainTop != 0)
                {
                    if (BeamsModel.MainTopBarModel.Count == 1)
                    {
                        uc.TopExaValue.IsEnabled = false;
                        uc.TopExbValue.IsEnabled = false;
                    }
                    else
                    {
                        if (BeamsModel.SelectedIndexModel.BarTop == 0)
                        {
                            uc.TopExaValue.IsEnabled = false;
                        }
                        else
                        {
                            uc.TopExaValue.IsEnabled = SelectedMainTop.La == 0;
                        }
                        if (BeamsModel.SelectedIndexModel.BarTop == BeamsModel.MainTopBarModel.Count - 1)
                        {
                            uc.TopExbValue.IsEnabled = false;
                        }
                        else
                        {
                            uc.TopExbValue.IsEnabled = SelectedMainTop.Lb == 0;
                        }
                    }
                }
                if (BeamsModel.MainBottomBarModel.Count == 1)
                {
                    uc.BottomExaValue.IsEnabled = false;
                    uc.BottomExbValue.IsEnabled = false;
                }
                else
                {
                    if (BeamsModel.SelectedIndexModel.BarBottom==0)
                    {
                        uc.BottomExaValue.IsEnabled = false;
                    }
                    else
                    {
                        uc.BottomExaValue.IsEnabled = SelectedMainBottom.La == 0;
                    }
                    if (BeamsModel.SelectedIndexModel.BarBottom == BeamsModel.MainBottomBarModel.Count-1)
                    {
                        uc.BottomExbValue.IsEnabled = false;
                    }
                    else
                    {
                        uc.BottomExbValue.IsEnabled = SelectedMainBottom.Lb == 0;
                    }
                }
            });
            #endregion
            #region MainTop Selection

            SelectionChangedStyleMainTopBarCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                BarsMainView uc = ProcessInfoBeamRebar.FindChild<BarsMainView>(p, "BarsMainUC");
                if (BeamsModel.SelectedIndexModel.StyleMainTop == 0)
                {
                    HiddenMainTop(uc);
                }
                else
                {
                    VisibleMainTop(uc);
                }
                LoadBarsMainView(p);
            });
            SelectionChangedMainTopCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                LoadBarsMainView(p);
                double left = ((SelectedMainTop.X0 + SelectedMainTop.Length / 2)) / BeamsModel.DrawModel.Scale - 10 * BeamsModel.DrawModel.Left;
                p.scrollViewer.ScrollToLeftEnd();
                p.scrollViewer.ScrollToHorizontalOffset(left);
                BarsMainView uc = ProcessInfoBeamRebar.FindChild<BarsMainView>(p, "BarsMainUC");
                if (BeamsModel.SelectedIndexModel.StyleMainTop != 0)
                {
                    if (BeamsModel.MainTopBarModel.Count == 1)
                    {
                        uc.TopExaValue.IsEnabled = false;
                        uc.TopExbValue.IsEnabled = false;
                    }
                    else
                    {
                        if (BeamsModel.SelectedIndexModel.BarTop == 0)
                        {
                            uc.TopExaValue.IsEnabled = false;
                        }
                        else
                        {
                            uc.TopExaValue.IsEnabled = SelectedMainTop.La == 0;
                        }
                        if (BeamsModel.SelectedIndexModel.BarTop == BeamsModel.MainTopBarModel.Count - 1)
                        {
                            uc.TopExbValue.IsEnabled = false;
                        }
                        else
                        {
                            uc.TopExbValue.IsEnabled = SelectedMainTop.Lb == 0;
                        }
                    }
                }
            });
            #endregion
            #region Selectionchanged Bar MainTop
            SelectionChangedBarTopCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                SelectionChangedBarTop(p);
            });
            SelectionChangedBarTopSCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                SelectionChangedBarTop(p);
            });
            #endregion
            #region Maintop TextChanged
            TopLaTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                BarsMainView uc = ProcessInfoBeamRebar.FindChild<BarsMainView>(p, "BarsMainUC");
                if (double.TryParse(uc.TopLaValue.Text, out double S))
                {
                    if (S < 0)
                    {
                        System.Windows.Forms.MessageBox.Show("Error Data","Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                    if (BeamsModel.SelectedIndexModel.StyleMainTop != 0)
                    {
                        if (BeamsModel.MainTopBarModel.Count == 1)
                        {
                            uc.TopExaValue.IsEnabled = false;
                        }
                        else
                        {
                            if (BeamsModel.SelectedIndexModel.BarTop == 0)
                            {
                                uc.TopExaValue.IsEnabled = false;
                            }
                            else
                            {
                                uc.TopExaValue.IsEnabled = SelectedMainTop.La == 0;
                            }
                        }
                    }
                    if (S != 0)
                    {
                        SelectedMainTop.Exa = 0;
                    }
                    SelectedMainTop.GetLocationBarModels();
                    TopLaLbExaExbTextChanged(p);
                    double left = ((SelectedMainTop.X0)) / BeamsModel.DrawModel.Scale - 2 * BeamsModel.DrawModel.Left;
                    p.scrollViewer.ScrollToLeftEnd();
                    p.scrollViewer.ScrollToHorizontalOffset(left);
                }
            });
            TopLbTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                BarsMainView uc = ProcessInfoBeamRebar.FindChild<BarsMainView>(p, "BarsMainUC");
                if (double.TryParse(uc.TopLbValue.Text, out double S))
                {
                    if (S < 0)
                    {
                        System.Windows.Forms.MessageBox.Show("Error Data", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                    if (BeamsModel.SelectedIndexModel.StyleMainTop != 0)
                    {
                        if (BeamsModel.MainTopBarModel.Count == 1)
                        {
                            uc.TopExbValue.IsEnabled = false;
                        }
                        else
                        {
                            if (BeamsModel.SelectedIndexModel.BarTop == BeamsModel.MainTopBarModel.Count - 1)
                            {
                                uc.TopExbValue.IsEnabled = false;
                            }
                            else
                            {
                                uc.TopExbValue.IsEnabled = SelectedMainTop.Lb == 0;
                            }
                        }
                    }
                    if (S != 0)
                    {
                        SelectedMainTop.Exb = 0;
                    }
                    SelectedMainTop.GetLocationBarModels();
                    TopLaLbExaExbTextChanged(p);
                    double left = ((SelectedMainTop.Location[SelectedMainTop.Location.Count - 1].X)) / BeamsModel.DrawModel.Scale - 2 * BeamsModel.DrawModel.Left;
                    p.scrollViewer.ScrollToLeftEnd();
                    p.scrollViewer.ScrollToHorizontalOffset(left);
                }
            });
            TopExaTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                BarsMainView uc = ProcessInfoBeamRebar.FindChild<BarsMainView>(p, "BarsMainUC");
                if (double.TryParse(uc.TopExaValue.Text, out double S))
                {
                    SelectedMainTop.GetLocationBarModels();
                    TopLaLbExaExbTextChanged(p);
                    double left = ((SelectedMainTop.X0)) / BeamsModel.DrawModel.Scale - 2 * BeamsModel.DrawModel.Left;
                    p.scrollViewer.ScrollToLeftEnd();
                    p.scrollViewer.ScrollToHorizontalOffset(left);
                }
            });
            TopExbTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                BarsMainView uc = ProcessInfoBeamRebar.FindChild<BarsMainView>(p, "BarsMainUC");
                if (double.TryParse(uc.TopExbValue.Text, out double S))
                {
                    SelectedMainTop.GetLocationBarModels();
                    TopLaLbExaExbTextChanged(p);
                    double left = ((SelectedMainTop.Location[SelectedMainTop.Location.Count - 1].X)) / BeamsModel.DrawModel.Scale - 2 * BeamsModel.DrawModel.Left;
                    p.scrollViewer.ScrollToLeftEnd();
                    p.scrollViewer.ScrollToHorizontalOffset(left);
                }
            });
            TopSLaTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                BarsMainView uc = ProcessInfoBeamRebar.FindChild<BarsMainView>(p, "BarsMainUC");
                if (double.TryParse(uc.TopSLaValue.Text, out double S))
                {
                    if (S < 0)
                    {
                        System.Windows.Forms.MessageBox.Show("Error Data", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                    TopSLaTextChanged(p);
                }
            });
            TopSLbTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                BarsMainView uc = ProcessInfoBeamRebar.FindChild<BarsMainView>(p, "BarsMainUC");
                if (double.TryParse(uc.TopSLbValue.Text, out double S))
                {
                    if (S < 0)
                    {
                        System.Windows.Forms.MessageBox.Show("Error Data", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                    TopSLbTextChanged(p);
                }
            });
            #endregion
            #region BottomChanged
            SelectionChangedMainBottomCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                SelectionChangedMainBottom(p);
                double left = ((SelectedMainBottom.X0)) / BeamsModel.DrawModel.Scale - 10 * BeamsModel.DrawModel.Left;
                p.scrollViewer.ScrollToLeftEnd();
                p.scrollViewer.ScrollToHorizontalOffset(left);
                BarsMainView uc = ProcessInfoBeamRebar.FindChild<BarsMainView>(p, "BarsMainUC");
                if (BeamsModel.MainBottomBarModel.Count == 1)
                {
                    uc.BottomExaValue.IsEnabled = false;
                    uc.BottomExbValue.IsEnabled = false;
                }
                else
                {
                    if (BeamsModel.SelectedIndexModel.BarBottom == 0)
                    {
                        uc.BottomExaValue.IsEnabled = false;
                    }
                    else
                    {
                        uc.BottomExaValue.IsEnabled = SelectedMainBottom.La == 0;
                    }
                    if (BeamsModel.SelectedIndexModel.BarBottom == BeamsModel.MainBottomBarModel.Count - 1)
                    {
                        uc.BottomExbValue.IsEnabled = false;
                    }
                    else
                    {
                        uc.BottomExbValue.IsEnabled = SelectedMainBottom.Lb == 0;
                    }
                }
            });
            SelectionChangedBarBottomCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                SelectionChangedBarBottom(p);
                double left = ((SelectedMainBottom.X0)) / BeamsModel.DrawModel.Scale - 10 * BeamsModel.DrawModel.Left;
                p.scrollViewer.ScrollToRightEnd();
                p.scrollViewer.ScrollToHorizontalOffset((left));
            });
            SelectionChangedNumberBarBottomCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                SelectionChangedNumberBarBottom(p);
                double left = ((SelectedMainBottom.X0)) / BeamsModel.DrawModel.Scale - 10 * BeamsModel.DrawModel.Left;
                p.scrollViewer.ScrollToRightEnd();
                p.scrollViewer.ScrollToHorizontalOffset((left));
            });

            BottomLaTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                BarsMainView uc = ProcessInfoBeamRebar.FindChild<BarsMainView>(p, "BarsMainUC");
                if (double.TryParse(uc.BottomLaValue.Text, out double S))
                {
                    if (S < 0)
                    {
                        System.Windows.Forms.MessageBox.Show("Error Data", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                    if (BeamsModel.MainBottomBarModel.Count == 1)
                    {
                        uc.BottomExaValue.IsEnabled = false;
                    }
                    else
                    {
                        if (BeamsModel.SelectedIndexModel.BarBottom == 0)
                        {
                            uc.BottomExaValue.IsEnabled = false;
                        }
                        else
                        {
                            uc.BottomExaValue.IsEnabled = SelectedMainBottom.La == 0;
                        }
                    }
                    if (S!=0)
                    {
                        SelectedMainBottom.Exa = 0;
                    }
                    SelectedMainBottom.GetLocationBarModels();
                    BottomLaLbExaExbTextChanged(p);
                    double left = ((SelectedMainBottom.X0)) / BeamsModel.DrawModel.Scale - 2 * BeamsModel.DrawModel.Left;
                    p.scrollViewer.ScrollToRightEnd();
                    p.scrollViewer.ScrollToHorizontalOffset((left));
                }
            });
            BottomLbTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                BarsMainView uc = ProcessInfoBeamRebar.FindChild<BarsMainView>(p, "BarsMainUC");
                if (double.TryParse(uc.BottomLbValue.Text, out double S))
                {
                    if (S < 0)
                    {
                        System.Windows.Forms.MessageBox.Show("Error Data", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                    if (BeamsModel.MainBottomBarModel.Count == 1)
                    {
                        uc.BottomExbValue.IsEnabled = false;
                    }
                    else
                    {
                        if (BeamsModel.SelectedIndexModel.BarBottom == BeamsModel.MainBottomBarModel.Count - 1)
                        {
                            uc.BottomExbValue.IsEnabled = false;
                        }
                        else
                        {
                            uc.BottomExbValue.IsEnabled = SelectedMainBottom.Lb == 0;
                        }
                    }
                    if (S != 0)
                    {
                        SelectedMainBottom.Exb = 0;
                    }
                    SelectedMainBottom.GetLocationBarModels();
                    BottomLaLbExaExbTextChanged(p);
                    double left = ((SelectedMainBottom.Location[SelectedMainBottom.Location.Count - 1].X)) / BeamsModel.DrawModel.Scale - 2 * BeamsModel.DrawModel.Left;
                    p.scrollViewer.ScrollToRightEnd();
                    p.scrollViewer.ScrollToHorizontalOffset((left));
                }
            });
            BottomExaTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                BarsMainView uc = ProcessInfoBeamRebar.FindChild<BarsMainView>(p, "BarsMainUC");
                if (double.TryParse(uc.BottomExaValue.Text, out double S))
                {
                    SelectedMainBottom.GetLocationBarModels();
                    BottomLaLbExaExbTextChanged(p);
                    double left = ((SelectedMainBottom.X0)) / BeamsModel.DrawModel.Scale - 2 * BeamsModel.DrawModel.Left;
                    p.scrollViewer.ScrollToRightEnd();
                    p.scrollViewer.ScrollToHorizontalOffset((left));
                }
            });
            BottomExbTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                BarsMainView uc = ProcessInfoBeamRebar.FindChild<BarsMainView>(p, "BarsMainUC");
                if (double.TryParse(uc.BottomExbValue.Text, out double S))
                {
                    SelectedMainBottom.GetLocationBarModels();
                    BottomLaLbExaExbTextChanged(p);
                    double left = ((SelectedMainBottom.Location[SelectedMainBottom.Location.Count - 1].X)) / BeamsModel.DrawModel.Scale - 2 * BeamsModel.DrawModel.Left;
                    p.scrollViewer.ScrollToRightEnd();
                    p.scrollViewer.ScrollToHorizontalOffset((left));
                }
            });
            #endregion
        }

        #region
        private void LoadBarsMainView(BeamsWindow p)
        {
            p.canvas.Children.Clear();
            DrawMainCanvas.SpanBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.NodeBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.NodeModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.SpecialNode(p.canvas, BeamsModel.InfoModels, BeamsModel.SpecialNodeModels, BeamsModel.DrawModel, 1000);
            double dsmax = ProcessInfoBeamRebar.GetDiameterStirrupMax(BeamsModel.InfoModels, BeamsModel.DistributeStirrups, BeamsModel.StirrupModels);
            if (BeamsModel.SelectedIndexModel.StyleMainTop == 0)
            {
                BeamsModel.SingleMainTopBarModel.Refresh(BeamsModel.InfoModels, BeamsModel.NodeModels, BeamsModel.Cover, dsmax);
                BeamsModel.SingleMainTopBarModel.GetLength();
            }
            else
            {
                for (int i = 0; i < BeamsModel.MainTopBarModel.Count; i++)
                {
                    BeamsModel.MainTopBarModel[i].Refresh(dsmax, BeamsModel.Cover);
                    BeamsModel.MainTopBarModel[i].GetLocationBarModels();
                }
            }
            for (int i = 0; i < BeamsModel.MainBottomBarModel.Count; i++)
            {
                BeamsModel.MainBottomBarModel[i].Refresh(dsmax, BeamsModel.Cover);
                BeamsModel.MainBottomBarModel[i].GetLocationBarModels();
            }
            DrawMainCanvas.MainTopBarBeams(p.canvas, BeamsModel.MainTopBarModel, BeamsModel.SingleMainTopBarModel, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.StyleMainTop, BeamsModel.SelectedIndexModel.BarTop);
        }
        private void SelectionChangedBarTop(BeamsWindow p)
        {
            p.canvas.Children.Clear();
            DrawMainCanvas.SpanBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.NodeBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.NodeModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.SpecialNode(p.canvas, BeamsModel.InfoModels, BeamsModel.SpecialNodeModels, BeamsModel.DrawModel, 1000);
            double dsmax = ProcessInfoBeamRebar.GetDiameterStirrupMax(BeamsModel.InfoModels, BeamsModel.DistributeStirrups, BeamsModel.StirrupModels);
            if (BeamsModel.SelectedIndexModel.StyleMainTop == 0)
            {
                BeamsModel.SingleMainTopBarModel.Refresh(BeamsModel.InfoModels, BeamsModel.NodeModels, BeamsModel.Cover, dsmax);
                BeamsModel.SingleMainTopBarModel.GetLength();
            }
            else
            {
                for (int i = 0; i < BeamsModel.MainTopBarModel.Count; i++)
                {
                    BeamsModel.MainTopBarModel[i].Refresh(dsmax, BeamsModel.Cover);
                    BeamsModel.MainTopBarModel[i].GetLocationBarModels();
                }
            }
            DrawMainCanvas.MainTopBarBeams(p.canvas, BeamsModel.MainTopBarModel, BeamsModel.SingleMainTopBarModel, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.StyleMainTop, BeamsModel.SelectedIndexModel.BarTop);
            double left = 0;
            if (BeamsModel.SelectedIndexModel.StyleMainTop == 0)
            {
                left = ((BeamsModel.SingleMainTopBarModel.Location[0].X)) / BeamsModel.DrawModel.Scale - 10 * BeamsModel.DrawModel.Left;
            }
            else
            {
                left = ((SelectedMainTop.X0)) / BeamsModel.DrawModel.Scale - 10 * BeamsModel.DrawModel.Left;
            }
            p.scrollViewer.ScrollToLeftEnd();
            p.scrollViewer.ScrollToHorizontalOffset(left);
        }
        private void TopLaLbExaExbTextChanged(BeamsWindow p)
        {
            p.canvas.Children.Clear();
            DrawMainCanvas.SpanBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.NodeBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.NodeModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.SpecialNode(p.canvas, BeamsModel.InfoModels, BeamsModel.SpecialNodeModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.MainTopBarBeams(p.canvas, BeamsModel.MainTopBarModel, BeamsModel.SingleMainTopBarModel, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.StyleMainTop, BeamsModel.SelectedIndexModel.BarTop);
        }
        private void TopSLaTextChanged(BeamsWindow p)
        {
            double dsmax = ProcessInfoBeamRebar.GetDiameterStirrupMax(BeamsModel.InfoModels, BeamsModel.DistributeStirrups, BeamsModel.StirrupModels);
            BeamsModel.SingleMainTopBarModel.Refresh(BeamsModel.InfoModels, BeamsModel.NodeModels, BeamsModel.Cover, dsmax);
            BeamsModel.SingleMainTopBarModel.GetLength();
            p.canvas.Children.Clear();
            DrawMainCanvas.SpanBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.NodeBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.NodeModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.SpecialNode(p.canvas, BeamsModel.InfoModels, BeamsModel.SpecialNodeModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.MainTopBarBeams(p.canvas, BeamsModel.MainTopBarModel, BeamsModel.SingleMainTopBarModel, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.StyleMainTop, BeamsModel.SelectedIndexModel.BarTop);
            p.scrollViewer.ScrollToLeftEnd();
            p.scrollViewer.ScrollToHorizontalOffset(((BeamsModel.SingleMainTopBarModel.Location[0].X)) / BeamsModel.DrawModel.Scale - 2 * BeamsModel.DrawModel.Left);
        }
        private void TopSLbTextChanged(BeamsWindow p)
        {
            double dsmax = ProcessInfoBeamRebar.GetDiameterStirrupMax(BeamsModel.InfoModels, BeamsModel.DistributeStirrups, BeamsModel.StirrupModels);
            BeamsModel.SingleMainTopBarModel.Refresh(BeamsModel.InfoModels, BeamsModel.NodeModels, BeamsModel.Cover, dsmax);
            BeamsModel.SingleMainTopBarModel.GetLength();
            p.canvas.Children.Clear();
            DrawMainCanvas.SpanBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.NodeBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.NodeModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.SpecialNode(p.canvas, BeamsModel.InfoModels, BeamsModel.SpecialNodeModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.MainTopBarBeams(p.canvas, BeamsModel.MainTopBarModel, BeamsModel.SingleMainTopBarModel, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.StyleMainTop, BeamsModel.SelectedIndexModel.BarTop);
            p.scrollViewer.ScrollToLeftEnd();
            p.scrollViewer.ScrollToHorizontalOffset(((BeamsModel.SingleMainTopBarModel.Location[BeamsModel.SingleMainTopBarModel.Location.Count - 1].X)) / BeamsModel.DrawModel.Scale - 2 * BeamsModel.DrawModel.Left);
        }
        private void SelectionChangedMainBottom(BeamsWindow p)
        {
            p.canvas.Children.Clear();
            DrawMainCanvas.SpanBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.NodeBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.NodeModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.SpecialNode(p.canvas, BeamsModel.InfoModels, BeamsModel.SpecialNodeModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.MainBottomBarBeams(p.canvas, BeamsModel.MainBottomBarModel, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.BarBottom);
        }
        private void SelectionChangedBarBottom(BeamsWindow p)
        {
            p.canvas.Children.Clear();
            DrawMainCanvas.SpanBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.NodeBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.NodeModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.SpecialNode(p.canvas, BeamsModel.InfoModels, BeamsModel.SpecialNodeModels, BeamsModel.DrawModel, 1000);
            double dsmax = ProcessInfoBeamRebar.GetDiameterStirrupMax(BeamsModel.InfoModels, BeamsModel.DistributeStirrups, BeamsModel.StirrupModels);
            for (int i = 0; i < BeamsModel.MainBottomBarModel.Count; i++)
            {
                BeamsModel.MainBottomBarModel[i].Refresh(dsmax, BeamsModel.Cover);
                BeamsModel.MainBottomBarModel[i].GetLocationBarModels();
            }
            DrawMainCanvas.MainBottomBarBeams(p.canvas, BeamsModel.MainBottomBarModel, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.BarBottom);
        }
        private void SelectionChangedNumberBarBottom(BeamsWindow p)
        {
            p.canvas.Children.Clear();
            DrawMainCanvas.SpanBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.NodeBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.NodeModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.SpecialNode(p.canvas, BeamsModel.InfoModels, BeamsModel.SpecialNodeModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.MainBottomBarBeams(p.canvas, BeamsModel.MainBottomBarModel, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.BarBottom);
        }
        private void BottomLaLbExaExbTextChanged(BeamsWindow p)
        {
            p.canvas.Children.Clear();
            DrawMainCanvas.SpanBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.NodeBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.NodeModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.SpecialNode(p.canvas, BeamsModel.InfoModels, BeamsModel.SpecialNodeModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.MainBottomBarBeams(p.canvas, BeamsModel.MainBottomBarModel, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.BarBottom);
        }
        private void VisibleMainTop(BarsMainView p)
        {
            p.ListViewTopBar.Visibility = System.Windows.Visibility.Visible;
            p.TopL.Visibility = System.Windows.Visibility.Visible;
            p.TopLValue.Visibility = System.Windows.Visibility.Visible;
            p.TopLUnit.Visibility = System.Windows.Visibility.Visible;
            p.TopLaValue.Visibility = System.Windows.Visibility.Visible;
            p.TopLbValue.Visibility = System.Windows.Visibility.Visible;
            p.TopExa.Visibility = System.Windows.Visibility.Visible;
            p.TopExaValue.Visibility = System.Windows.Visibility.Visible;
            p.TopExaUnit.Visibility = System.Windows.Visibility.Visible;
            p.TopExb.Visibility = System.Windows.Visibility.Visible;
            p.TopExbValue.Visibility = System.Windows.Visibility.Visible;
            p.TopExbUnit.Visibility = System.Windows.Visibility.Visible;
            p.NumberBarTop.Visibility = System.Windows.Visibility.Visible;
            p.BarTopType.Visibility = System.Windows.Visibility.Visible;
            p.PhiTop.Visibility = System.Windows.Visibility.Visible;
            p.ListViewTopBarS.Visibility = System.Windows.Visibility.Hidden;
            p.TopSL.Visibility = System.Windows.Visibility.Hidden;
            p.TopSLValue.Visibility = System.Windows.Visibility.Hidden;
            p.TopSLUnit.Visibility = System.Windows.Visibility.Hidden;
            p.TopSLaValue.Visibility = System.Windows.Visibility.Hidden;
            p.TopSLbValue.Visibility = System.Windows.Visibility.Hidden;
            p.NumberBarTopS.Visibility = System.Windows.Visibility.Hidden;
            p.BarTopTypeS.Visibility = System.Windows.Visibility.Hidden;
            p.PhiTopS.Visibility = System.Windows.Visibility.Hidden;
            p.TopExaValue.IsEnabled = false;
            p.TopExbValue.IsEnabled = false;
        }
        private void HiddenMainTop(BarsMainView p)
        {
            p.ListViewTopBar.Visibility = System.Windows.Visibility.Hidden;
            p.TopL.Visibility = System.Windows.Visibility.Hidden;
            p.TopLValue.Visibility = System.Windows.Visibility.Hidden;
            p.TopLUnit.Visibility = System.Windows.Visibility.Hidden;
            p.TopLaValue.Visibility = System.Windows.Visibility.Hidden;
            p.TopLbValue.Visibility = System.Windows.Visibility.Hidden;
            p.TopExa.Visibility = System.Windows.Visibility.Hidden;
            p.TopExaValue.Visibility = System.Windows.Visibility.Hidden;
            p.TopExaUnit.Visibility = System.Windows.Visibility.Hidden;
            p.TopExb.Visibility = System.Windows.Visibility.Hidden;
            p.TopExbValue.Visibility = System.Windows.Visibility.Hidden;
            p.TopExbUnit.Visibility = System.Windows.Visibility.Hidden;
            p.NumberBarTop.Visibility = System.Windows.Visibility.Hidden;
            p.BarTopType.Visibility = System.Windows.Visibility.Hidden;
            p.PhiTop.Visibility = System.Windows.Visibility.Hidden;
            p.ListViewTopBarS.Visibility = System.Windows.Visibility.Visible;
            p.TopSL.Visibility = System.Windows.Visibility.Visible;
            p.TopSLValue.Visibility = System.Windows.Visibility.Visible;
            p.TopSLUnit.Visibility = System.Windows.Visibility.Visible;
            p.TopSLaValue.Visibility = System.Windows.Visibility.Visible;
            p.TopSLbValue.Visibility = System.Windows.Visibility.Visible;
            p.NumberBarTopS.Visibility = System.Windows.Visibility.Visible;
            p.BarTopTypeS.Visibility = System.Windows.Visibility.Visible;
            p.PhiTopS.Visibility = System.Windows.Visibility.Visible;
            p.TopExaValue.IsEnabled = false;
            p.TopExbValue.IsEnabled = false;
        }
        #endregion
        
    }
}
