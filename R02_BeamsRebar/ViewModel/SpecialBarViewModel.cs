
using Autodesk.Revit.DB;
using R02_BeamsRebar.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using WpfCustomControls;
using R02_BeamsRebar.LanguageModel;
namespace R02_BeamsRebar.ViewModel
{
    public class SpecialBarViewModel:BaseViewModel
    {
        #region Property
        public Document Doc;
        private BeamsModel _BeamsModel;
        public BeamsModel BeamsModel { get => _BeamsModel; set { _BeamsModel = value; OnPropertyChanged(); } }
        #endregion
        #region Selected
        private SideBarModel _SelectedSideBarModel;
        public SideBarModel SelectedSideBarModel { get => _SelectedSideBarModel; set { _SelectedSideBarModel = value; OnPropertyChanged(); } }
        private SpecialBarModel _SelectedSpecialBarModel;
        public SpecialBarModel SelectedSpecialBarModel { get => _SelectedSpecialBarModel; set { _SelectedSpecialBarModel = value; OnPropertyChanged(); } }
        #endregion
        #region Number
        private List<int> _AllNumberSide;
        public List<int> AllNumberSide { get => _AllNumberSide; set { _AllNumberSide = value; OnPropertyChanged(); } }
        private List<int> _AllNumberSpecial;
        public List<int> AllNumberSpecial { get => _AllNumberSpecial; set { _AllNumberSpecial = value; OnPropertyChanged(); } }
        private List<int> _AllNumberStirrup;
        public List<int> AllNumberStirrup { get => _AllNumberStirrup; set { _AllNumberStirrup = value; OnPropertyChanged(); } }
        #endregion
        #region Command
        public ICommand LoadSpecialPointViewCommand { get; set; }
        public ICommand SelectionChangedSideBarCommand { get; set; }
        public ICommand SelectionChangedBarSideCommand { get; set; }
        public ICommand ExLeftMidTextChangedCommand { get; set; }
        public ICommand ExRightMidTextChangedCommand { get; set; }
        public ICommand IsCheckSideBarCommand { get; set; }
        public ICommand SelectionChangedSpecialCommand { get; set; }
        public ICommand IsCheckSpecialBarCommand { get; set; }
        public ICommand IsCheckStirrupBarCommand { get; set; }
        public ICommand SelectionChangedNumberBarStirrupCommand { get; set; }
        public ICommand StirrupBarTextChangedCommand { get; set; }
        public ICommand L1SpecialTextChangedCommand { get; set; }
        public ICommand L2SpecialTextChangedCommand { get; set; }
        public ICommand L3SpecialTextChangedCommand { get; set; }
        #endregion
        public SpecialBarViewModel(Document document, BeamsModel beamsModel)
        {
            #region Get Property
            Doc = document;
            BeamsModel = beamsModel;
            GetAllNumber();
            #endregion
            #region Load
            LoadSpecialPointViewCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                SpecialBarView uc = ProcessInfoBeamRebar.FindChild<SpecialBarView>(p, "SpecialBarUC");
                if (BeamsModel.SpecialNodeModels.Count == 0)
                {
                    uc.GbSpecial.Background = Brushes.Gainsboro;
                    uc.GrSpecial.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    uc.GbSpecial.Background = Brushes.Transparent;
                    uc.GrSpecial.Visibility = System.Windows.Visibility.Visible;
                    BeamsModel.SelectedIndexModel.SpecialBar = 0;
                    DrawCanvas2(uc);
                    DrawCanvas3(uc);
                }
                DrawCanvas1(uc);
                DrawSpecialBar(p, true);

            });
            #endregion
            #region SideBar
            SelectionChangedSideBarCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                DrawSpecialBar(p, true);
                SpecialBarView uc = ProcessInfoBeamRebar.FindChild<SpecialBarView>(p, "SpecialBarUC");
                DrawCanvas1(uc);
            });
            ExLeftMidTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                DrawSpecialBar(p, true);
            });
            ExLeftMidTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                DrawSpecialBar(p, true);
            });
            IsCheckSideBarCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                SpecialBarView uc = ProcessInfoBeamRebar.FindChild<SpecialBarView>(p, "SpecialBarUC");
                DrawCanvas1(uc);
                DrawSpecialBar(p, true);
            });

            #endregion
            #region Special Bar
            SelectionChangedSpecialCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                SpecialBarView uc = ProcessInfoBeamRebar.FindChild<SpecialBarView>(p, "SpecialBarUC");
                DrawCanvas2(uc);
                DrawSpecialBar(p, false);
            });
            StirrupBarTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                SpecialBarView uc = ProcessInfoBeamRebar.FindChild<SpecialBarView>(p, "SpecialBarUC");
                DrawCanvas2(uc);
                DrawSpecialBar(p, false);
            });
            SelectionChangedNumberBarStirrupCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                SpecialBarView uc = ProcessInfoBeamRebar.FindChild<SpecialBarView>(p, "SpecialBarUC");
                DrawCanvas2(uc);
                DrawSpecialBar(p, false);
            });
            L1SpecialTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                SpecialBarView uc = ProcessInfoBeamRebar.FindChild<SpecialBarView>(p, "SpecialBarUC");
                DrawCanvas2(uc);
                DrawSpecialBar(p, false);
            });
            L2SpecialTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                SpecialBarView uc = ProcessInfoBeamRebar.FindChild<SpecialBarView>(p, "SpecialBarUC");
                DrawCanvas2(uc);
                DrawSpecialBar(p, false);
            });
            L3SpecialTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                SpecialBarView uc = ProcessInfoBeamRebar.FindChild<SpecialBarView>(p, "SpecialBarUC");
                DrawCanvas2(uc);
                DrawSpecialBar(p, false);
            });
            IsCheckSpecialBarCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                SpecialBarView uc = ProcessInfoBeamRebar.FindChild<SpecialBarView>(p, "SpecialBarUC");
                DrawCanvas2(uc);
                DrawSpecialBar(p, false);
            });
            IsCheckStirrupBarCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                SpecialBarView uc = ProcessInfoBeamRebar.FindChild<SpecialBarView>(p, "SpecialBarUC");
                DrawCanvas2(uc);
                DrawSpecialBar(p, false);
            });
            #endregion
        }

        private void DrawSpecialBar(BeamsWindow p, bool SideorSpecial)
        {
            p.canvas.Children.Clear();
            DrawMainCanvas.SpanBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.NodeBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.NodeModels, BeamsModel.DrawModel, 1000);
            GetLocationSideBar();
            p.scrollViewer.ScrollToLeftEnd();
            if (BeamsModel.SpecialNodeModels.Count != 0)
            {
                GetLocationSpecialBar();

                if (SideorSpecial)
                {
                    DrawMainCanvas.SpecialNode(p.canvas, BeamsModel.InfoModels, BeamsModel.SpecialNodeModels, BeamsModel.DrawModel, 1000);
                    DrawMainCanvas.SideBar(p.canvas, BeamsModel.SideBarModel, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.SideBar);
                    DrawMainCanvas.SpecialBar(p.canvas, BeamsModel.SpecialBarModel, BeamsModel.DrawModel, 1000);
                    p.scrollViewer.ScrollToHorizontalOffset(((BeamsModel.InfoModels[BeamsModel.SelectedIndexModel.SideBar].startPosition) / BeamsModel.DrawModel.Scale));
                }
                else
                {
                    DrawMainCanvas.SpecialNode(p.canvas, BeamsModel.InfoModels, BeamsModel.SpecialNodeModels, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.SpecialBar);
                    DrawMainCanvas.SideBar(p.canvas, BeamsModel.SideBarModel, BeamsModel.DrawModel, 1000);
                    DrawMainCanvas.SpecialBar(p.canvas, BeamsModel.SpecialBarModel, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.SpecialBar);
                    p.scrollViewer.ScrollToHorizontalOffset(((BeamsModel.SpecialNodeModels[BeamsModel.SelectedIndexModel.SpecialBar].Mid) / BeamsModel.DrawModel.Scale - 10 * BeamsModel.DrawModel.Left));
                }
            }
            else
            {
                DrawMainCanvas.SideBar(p.canvas, BeamsModel.SideBarModel, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.SideBar);
                p.scrollViewer.ScrollToHorizontalOffset(((BeamsModel.InfoModels[BeamsModel.SelectedIndexModel.SideBar].startPosition) / BeamsModel.DrawModel.Scale));
            }
        }

        private void GetLocationSideBar()
        {
            double dsmax = ProcessInfoBeamRebar.GetDiameterStirrupMax(BeamsModel.InfoModels, BeamsModel.DistributeStirrups, BeamsModel.StirrupModels);
            double dTop = GetDMainTop();
            double dBottom = GetDMainBottom();
            for (int i = 0; i < BeamsModel.SideBarModel.Count; i++)
            {
                BeamsModel.SideBarModel[i].GetLocation(BeamsModel.InfoModels[i], BeamsModel.Cover, dsmax, dTop, dBottom);
            }
        }

        private double GetDMainBottom()
        {
            double dMainBottom = 0;

            for (int i = 0; i < BeamsModel.MainBottomBarModel.Count; i++)
            {
                if (dMainBottom < BeamsModel.MainBottomBarModel[i].Bar.Diameter)
                {
                    dMainBottom = BeamsModel.MainBottomBarModel[i].Bar.Diameter;
                }
            }

            return dMainBottom;
        }

        private void GetLocationSpecialBar()
        {
            double dsmax = ProcessInfoBeamRebar.GetDiameterStirrupMax(BeamsModel.InfoModels, BeamsModel.DistributeStirrups, BeamsModel.StirrupModels);
            double dTop = GetDMainTop();

            for (int i = 0; i < BeamsModel.SpecialBarModel.Count; i++)
            {
                InfoModel infoModel = BeamsModel.InfoModels.Where(x => x.NumberSpan == BeamsModel.SpecialNodeModels[i].NumberSpan).FirstOrDefault();
                double h = Math.Abs(infoModel.zOffset) + infoModel.h;
                BeamsModel.SpecialBarModel[i].GetX0Y0(BeamsModel.InfoModels, BeamsModel.SpecialNodeModels[i], BeamsModel.Cover, dsmax, dTop);
                BeamsModel.SpecialBarModel[i].GetLocationSP();
                BeamsModel.SpecialBarModel[i].GetLocationST(h, BeamsModel.Cover, dsmax, dTop);
            }
        }

        private double GetDMainTop()
        {
            double dMainTop = 0;
            if (BeamsModel.MainTopBarModel.Count == 1)
            {
                dMainTop = BeamsModel.SingleMainTopBarModel.Bar.Diameter;
            }
            else
            {
                if (BeamsModel.SelectedIndexModel.StyleMainTop == 0)
                {
                    dMainTop = BeamsModel.SingleMainTopBarModel.Bar.Diameter;
                }
                else
                {
                    for (int i = 0; i < BeamsModel.MainTopBarModel.Count; i++)
                    {
                        if (dMainTop < BeamsModel.MainTopBarModel[i].Bar.Diameter)
                        {
                            dMainTop = BeamsModel.MainTopBarModel[i].Bar.Diameter;
                        }
                    }
                }
            }
            return dMainTop;
        }

        private void GetAllNumber()
        {
            AllNumberSide = new List<int>();
            for (int i = 2; i <= 6; i += 2)
            {
                AllNumberSide.Add(i);
            }
            AllNumberSpecial = new List<int>();
            for (int i = 2; i <= 4; i++)
            {
                AllNumberSpecial.Add(i);
            }
            AllNumberStirrup = new List<int>();
            for (int i = 6; i <= 12; i += 2)
            {
                AllNumberStirrup.Add(i);
            }
        }
        #region DrawImage
        private void DrawCanvas1(SpecialBarView p)
        {
            double b = 120, h = 200, c = 5, ds = 3, d = 12, left = 70, top = 30, scale = 1; int n = 2;
            double r = (ds + d) / (2 * scale);

            p.canvas1.Children.Clear();
            DrawImage.DrawStirrup(p.canvas1, left, top, scale, b, h, c, ds, d, BeamsModel.DrawModel.ColorStirrupChoose);
            DrawImage.DrawSection(p.canvas1, scale, left, top, b, h);
            DrawImage.DrawLayerMainBar(p.canvas1, left, top + (c + r + ds / 2) / scale, scale, b, c, ds, d, n, BeamsModel.DrawModel.ColorMainBar);

            DrawImage.DrawLayerMainBar(p.canvas1, left, top + (h - c - r - ds / 2) / scale, scale, b, c, ds, d, n, BeamsModel.DrawModel.ColorMainBar);
            if (SelectedSideBarModel.IsSide)
            {
                DrawImage.DrawLayerMainBar(p.canvas1, left, top + (h / 2) / scale, scale, b, c, ds, d, 1, BeamsModel.DrawModel.ColorMainBarChoose);
            }

        }
        private void DrawCanvas2(SpecialBarView p)
        {
            p.canvas2.Children.Clear();
            DrawImage.DrawSpecialPoint(p.canvas2);
            if (SelectedSpecialBarModel.IsSP)
            {
                DrawImage.DrawSpecialBarDetail(p.canvas2);
            }
            if (SelectedSpecialBarModel.IsST)
            {
                DrawImage.DrawStirrupPoint(p.canvas2, SelectedSpecialBarModel.NumberST, SelectedSpecialBarModel.a);
            }
        }
        private void DrawCanvas3(SpecialBarView p)
        {
            DrawImage.DrawSpecialBar(p.canvas3);
        }
        #endregion

    }
}
