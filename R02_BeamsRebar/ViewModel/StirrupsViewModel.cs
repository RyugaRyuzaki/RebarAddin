using WpfCustomControls;
using Autodesk.Revit.DB;
using R02_BeamsRebar.View;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using WpfCustomControls.LanguageModel;
namespace R02_BeamsRebar.ViewModel
{
    public class StirrupsViewModel:BaseViewModel
    {
        #region Property
        public Document Doc;
        private BeamsModel _BeamsModel;
        public BeamsModel BeamsModel { get => _BeamsModel; set { _BeamsModel = value; OnPropertyChanged();
                if (BeamsModel!=null)
                {
                    Span = BeamsModel.InfoModels[BeamsModel.SelectedIndexModel.Span].NumberSpan;
                }
            } }
        #endregion
        #region Selected
        private StirrupModel _SelectedStirrupModel;
        public StirrupModel SelectedStirrupModel { get => _SelectedStirrupModel; set { _SelectedStirrupModel = value; OnPropertyChanged();} }
        private DistributeStirrup _SelectedDistribute;
        public DistributeStirrup SelectedDistribute { get => _SelectedDistribute; set { _SelectedDistribute = value; OnPropertyChanged();} }
        private bool _IsTypeStirrup;
        public bool IsTypeStirrup { get => _IsTypeStirrup; set { _IsTypeStirrup = value; OnPropertyChanged(); } }
        private bool _IsTypeDistribute;
        public bool IsTypeDistribute { get => _IsTypeDistribute; set { _IsTypeDistribute = value; OnPropertyChanged(); } }
        private int _Span;
        public int Span { get => _Span; set { _Span = value; OnPropertyChanged(); } }
        private List<int> _AllNa;
        public List<int> AllNa { get => _AllNa; set { _AllNa = value; OnPropertyChanged(); } }
        #endregion
        #region Image1
        private const double left = 30;
        private const double top = 30;
        private double scale1 { get; set; }
        #endregion
        #region Command
        public ICommand LoadStirrupViewCommand { get; set; }

        public ICommand SelectionChangedBeamsModelCommand { get; set; }
        public ICommand SelectionChangedStirrupTypeCommand { get; set; }
        public ICommand SelectionChangedCoverCommand { get; set; }
        public ICommand SelectionChangedDistributeTypeCommand { get; set; }
        public ICommand SelectionChangedNaCommand { get; set; }
        public ICommand StirrupBarATextChangedCommand { get; set; }
        public ICommand STextChangedCommand { get; set; }
        public ICommand S1TextChangedCommand { get; set; }
        public ICommand S2TextChangedCommand { get; set; }
        public ICommand L1TextChangedCommand { get; set; }
        public ICommand L2TextChangedCommand { get; set; }
        public ICommand AntiSaTextChangedCommand { get; set; }
        public ICommand AntiClickCommand { get; set; }
        #endregion
        private Languages _Languages;
        public Languages Languages { get { return _Languages; } set { _Languages = value; OnPropertyChanged(); } }
        public StirrupsViewModel(Document document,BeamsModel beamsModel, Languages languages)
        {
            #region Get Property
            Doc = document;
            AddAllNa();
            BeamsModel = beamsModel;
            Languages = languages;
            GetScale1();
            #endregion
            #region Load
            LoadStirrupViewCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                p.canvas.Width = BeamsModel.DrawModel.Width;
                p.canvas.Height = BeamsModel.DrawModel.Height;
                StirrupsView uc = ProcessInfoBeamRebar.FindChild<StirrupsView>(p, "StirrupsUC");
                DrawSection(uc);
                DrawStirrup(uc);
                DrawDistribute(uc);
                p.canvas.Children.Clear();
                DrawMainCanvas.SpanBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.Span);
                DrawMainCanvas.NodeBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.NodeModels, BeamsModel.DrawModel, 1000);
                DrawMainCanvas.SpecialNode(p.canvas, BeamsModel.InfoModels, BeamsModel.SpecialNodeModels, BeamsModel.DrawModel, 1000);
                DrawMainCanvas.DistributeStirrupBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.StirrupModels, BeamsModel.DistributeStirrups, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.Span);
            });
            #endregion
            #region Command
            SelectionChangedBeamsModelCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                StirrupsView uc = ProcessInfoBeamRebar.FindChild<StirrupsView>(p, "StirrupsUC");
                DrawSection(uc);
                DrawStirrup(uc);
                DrawDistribute(uc);
                p.canvas.Children.Clear();
                DrawMainCanvas.SpanBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.Span);
                DrawMainCanvas.NodeBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.NodeModels, BeamsModel.DrawModel, 1000);
                DrawMainCanvas.SpecialNode(p.canvas, BeamsModel.InfoModels, BeamsModel.SpecialNodeModels, BeamsModel.DrawModel, 1000);
                DrawMainCanvas.DistributeStirrupBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.StirrupModels, BeamsModel.DistributeStirrups, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.Span);
                double left = BeamsModel.InfoModels[BeamsModel.SelectedIndexModel.Span].startPosition / BeamsModel.DrawModel.Scale - 10 * BeamsModel.DrawModel.Left;
                p.scrollViewer.ScrollToLeftEnd();
                p.scrollViewer.ScrollToHorizontalOffset(left);
            });
            SelectionChangedStirrupTypeCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                StirrupsView uc = ProcessInfoBeamRebar.FindChild<StirrupsView>(p, "StirrupsUC");
                DrawSection(uc);
                DrawStirrup(uc);
            });
            SelectionChangedDistributeTypeCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                StirrupsView uc = ProcessInfoBeamRebar.FindChild<StirrupsView>(p, "StirrupsUC");
                DrawDistribute(uc);
                SS1S2TextChanged(p);
            });
            StirrupBarATextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                StirrupsView uc = ProcessInfoBeamRebar.FindChild<StirrupsView>(p, "StirrupsUC");
                if (double.TryParse(uc.StirrupBarA.Text, out double S))
                {
                    if (SelectedStirrupModel.a > BeamsModel.InfoModels[BeamsModel.SelectedIndexModel.Span].b - 2 * SelectedStirrupModel.c)
                    {
                        MessageBox.Show("a<=b-2c", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        return;
                    }
                    DrawSection(uc);
                    DrawStirrup(uc);
                }
            });
            STextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                StirrupsView uc = ProcessInfoBeamRebar.FindChild<StirrupsView>(p, "StirrupsUC");
                if (double.TryParse(uc.DisS.Text, out double S))
                {
                    if (S >= BeamsModel.InfoModels[BeamsModel.SelectedIndexModel.Span].Length)
                    {
                        uc.DisS.Background = Brushes.Brown;
                        return;
                    }
                    uc.DisS.Background = Brushes.White;
                    SS1S2TextChanged(p);
                }
            });
            S1TextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                StirrupsView uc = ProcessInfoBeamRebar.FindChild<StirrupsView>(p, "StirrupsUC");
                if (double.TryParse(uc.DisS1.Text, out double S))
                {
                    if (S >= BeamsModel.InfoModels[BeamsModel.SelectedIndexModel.Span].Length)
                    {
                        uc.DisS1.Background = Brushes.Brown;
                        return;
                    }
                    uc.DisS1.Background = Brushes.White;
                    SS1S2TextChanged(p);
                }
            });
            S2TextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                StirrupsView uc = ProcessInfoBeamRebar.FindChild<StirrupsView>(p, "StirrupsUC");
                if (double.TryParse(uc.DisS2.Text, out double S))
                {
                    if (S >= BeamsModel.InfoModels[BeamsModel.SelectedIndexModel.Span].Length)
                    {
                        uc.DisS2.Background = Brushes.Brown;
                        return;
                    }
                    uc.DisS2.Background = Brushes.White;
                    SS1S2TextChanged(p);
                }
            });
            SelectionChangedNaCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                StirrupsView uc = ProcessInfoBeamRebar.FindChild<StirrupsView>(p, "StirrupsUC");
                DrawSection(uc);
                DrawStirrup(uc);
            });
            AntiClickCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                StirrupsView uc = ProcessInfoBeamRebar.FindChild<StirrupsView>(p, "StirrupsUC");
                DrawSection(uc);
                DrawStirrup(uc);
            });
            SelectionChangedCoverCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                if (!SelectedStirrupModel.Anti) { SelectedStirrupModel.Na = 0; SelectedStirrupModel.Sa = 0; }
                StirrupsView uc = ProcessInfoBeamRebar.FindChild<StirrupsView>(p, "StirrupsUC");
                SelectionChangedCover(p);
            });
            #endregion
        }
        #region
        private void AddAllNa()
        {
            AllNa = new List<int>();
            for (int i = 0; i <= 2; i++)
            {
                AllNa.Add(i);
            }
        }
        #endregion
        #region Draw
        private void GetScale1()
        {
            scale1 = 1;
            double hmax = 0;
            for (int i = 0; i < BeamsModel.InfoModels.Count; i++)
            {
                if (hmax < BeamsModel.InfoModels[i].h) hmax = BeamsModel.InfoModels[i].h;
            }

            if (hmax > 300 - 2 * top)
            {
                scale1 = hmax / (300 - 2 * top);
            }
            else
            {
                if (hmax < 5)
                {
                    scale1 = hmax / (300 - 2 * top);
                }
                else
                {
                    scale1 = 1;
                }
            }
        }
        private void DrawSection(StirrupsView p)
        {
            p.canStirrupType.Children.Clear();
            double b = BeamsModel.InfoModels[BeamsModel.SelectedIndexModel.Span].b;
            double h = BeamsModel.InfoModels[BeamsModel.SelectedIndexModel.Span].h;
            DrawImage.DrawSection(p.canStirrupType, scale1, left, top, b, h);
            DrawImage.DimHorizontal(p.canStirrupType, left, top, scale1, b, 11, 20, 5);
            DrawImage.DimVertical(p.canStirrupType, left, top, scale1, h, 11, 20, 5);
            DrawImage.DimHorizontalText(p.canStirrupType, left, top + h / (2 * scale1), scale1, SelectedStirrupModel.c, 11, 20, 5, "c");
        }
        private void DrawStirrup(StirrupsView p)
        {
            IsTypeStirrup = SelectedStirrupModel.Type == 1;
            double b = BeamsModel.InfoModels[BeamsModel.SelectedIndexModel.Span].b;
            double h = BeamsModel.InfoModels[BeamsModel.SelectedIndexModel.Span].h;
            double ds = SelectedStirrupModel.BarS.Diameter;
            double a = SelectedStirrupModel.a;
            if (!IsTypeStirrup)
            {
                DrawImage.DrawStirrup(p.canStirrupType, left, top, scale1, b, h, SelectedStirrupModel.c, ds , GetdTopBeam(), BeamsModel.DrawModel.ColorStirrupChoose);
            }
            else
            {
                DrawImage.DrawStirrup(p.canStirrupType, left, top, scale1, (b + 2 * SelectedStirrupModel.c + 2 * ds + a) / 2,h, SelectedStirrupModel.c, ds, GetdTopBeam(), BeamsModel.DrawModel.ColorStirrupChoose);
                DrawImage.DrawStirrup(p.canStirrupType, left + (b - 2 * SelectedStirrupModel.c - 2 * ds - a) / (2 * scale1), top, scale1, (b + 2 * SelectedStirrupModel.c + 2 * ds + a) / 2, h, SelectedStirrupModel.c, ds, GetdTopBeam(), BeamsModel.DrawModel.ColorStirrupChoose);
            }
            DrawHook(p);
        }
        private void DrawHook(StirrupsView p)
        {
            double hook = BeamsModel.SettingModel.SelectedHook.get_Parameter(BuiltInParameter.REBAR_HOOK_ANGLE).AsDouble();
            if (SelectedStirrupModel.Anti)
            {
                double b = BeamsModel.InfoModels[BeamsModel.SelectedIndexModel.Span].b;
                double h = BeamsModel.InfoModels[BeamsModel.SelectedIndexModel.Span].h;
                switch (SelectedStirrupModel.Na)
                {
                    case 0:
                        break;
                    case 1:
                        DrawImage.DrawHook(p.canStirrupType, left, top + h / (2 * scale1), scale1, b, SelectedStirrupModel.c, SelectedStirrupModel.BarA.Diameter, BeamsModel.AllBars[3].Diameter, hook, BeamsModel.DrawModel.ColorMainBarChoose);
                        break;
                    case 2:
                        double dis = (h - 2 * BeamsModel.Cover - 2 * SelectedStirrupModel.BarS.Diameter - BeamsModel.AllBars[3].Diameter) / 3;
                        DrawImage.DrawHook(p.canStirrupType, left, top + h / (2 * scale1) - dis / (2 * scale1), scale1, b, SelectedStirrupModel.c, SelectedStirrupModel.BarA.Diameter, BeamsModel.AllBars[3].Diameter, hook, BeamsModel.DrawModel.ColorMainBarChoose);
                        DrawImage.DrawHook(p.canStirrupType, left, top + h / (2 * scale1) + dis / (2 * scale1), scale1, b, SelectedStirrupModel.c, SelectedStirrupModel.BarA.Diameter, BeamsModel.AllBars[3].Diameter, hook, BeamsModel.DrawModel.ColorMainBarChoose);
                        break;
                    default:
                        break;
                }
            }
        }
        private void DrawDistribute(StirrupsView p)
        {
            IsTypeDistribute = SelectedDistribute.Type == 1;
            p.DisType.Children.Clear();
            if (!IsTypeDistribute)
            {
                DrawImage.DrawDistribute1(p.DisType);
                SelectedDistribute.S1 = 0;
                SelectedDistribute.S2 = 0;
                p.DisS.IsEnabled = true;
            }
            else
            {
                p.DisS.IsEnabled = false;
                SelectedDistribute.S = 0;
                DrawImage.DrawDistribute2(p.DisType);
            }
        }
        #endregion
        #region orther
        private double GetdTopBeam()
        {
            double a = 0;
            if (BeamsModel.SelectedIndexModel.StyleMainTop == 0)
            {
                a = BeamsModel.SingleMainTopBarModel.Bar.Diameter;
            }
            else
            {
                for (int i = 0; i < BeamsModel.MainTopBarModel.Count; i++)
                {
                    if (BeamsModel.InfoModels[BeamsModel.SelectedIndexModel.SectionSpan].startPosition < BeamsModel.MainTopBarModel[i].X0 + BeamsModel.MainTopBarModel[i].Length)
                    {
                        return BeamsModel.MainTopBarModel[i].Bar.Diameter;
                    }
                }
            }
            return a;
        }
        private void SS1S2TextChanged(BeamsWindow p)
        {
            p.canvas.Children.Clear();
            DrawMainCanvas.SpanBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.Span);
            DrawMainCanvas.NodeBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.NodeModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.SpecialNode(p.canvas, BeamsModel.InfoModels, BeamsModel.SpecialNodeModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.DistributeStirrupBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.StirrupModels, BeamsModel.DistributeStirrups, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.Span);
            double left = BeamsModel.InfoModels[BeamsModel.SelectedIndexModel.Span].startPosition / BeamsModel.DrawModel.Scale - 10 * BeamsModel.DrawModel.Left;
            p.scrollViewer.ScrollToLeftEnd();
            p.scrollViewer.ScrollToHorizontalOffset(left);
        }
        private void SelectionChangedCover(BeamsWindow p)
        {
            BeamsModel.Cover = double.Parse(UnitFormatUtils.Format(Doc.GetUnits(), SpecTypeId.Length, BeamsModel.RebarCoverType.CoverDistance, false));
            for (int i = 0; i < BeamsModel.StirrupModels.Count; i++)
            {
                BeamsModel.StirrupModels[i].c = BeamsModel.Cover;
            }
            p.canvas.Children.Clear();
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

            DrawMainCanvas.SpanBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.Span);
            DrawMainCanvas.NodeBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.NodeModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.SpecialNode(p.canvas, BeamsModel.InfoModels, BeamsModel.SpecialNodeModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.DistributeStirrupBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.StirrupModels, BeamsModel.DistributeStirrups, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.Span);
            double left = BeamsModel.InfoModels[BeamsModel.SelectedIndexModel.Span].startPosition / BeamsModel.DrawModel.Scale - 10 * BeamsModel.DrawModel.Left;
            p.scrollViewer.ScrollToLeftEnd();
            p.scrollViewer.ScrollToHorizontalOffset(left);
        }
        #endregion
    }
}
