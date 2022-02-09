

using Autodesk.Revit.DB;
using R02_BeamsRebar.View;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using WpfCustomControls;
namespace R02_BeamsRebar.ViewModel
{
    public class SectionAreaViewModel : BaseViewModel
    {
        #region Property
        public Document Doc;
        private BeamsModel _BeamsModel;
        public BeamsModel BeamsModel { get => _BeamsModel; set { _BeamsModel = value; OnPropertyChanged(); } }
        #endregion
        #region Section
        private SectionAreaModel _SelectedSectionAreaModel;
        public SectionAreaModel SelectedSectionAreaModel { get => _SelectedSectionAreaModel; set { _SelectedSectionAreaModel = value; OnPropertyChanged(); } }
        #endregion
        #region Image
        private const double left = 100;
        private const double top = 80;
        private const double extend = 5;
        public double scale { get; set; }
        #endregion

        #region Command
        public ICommand LoadSectionAreaCommand { get; set; }
        public ICommand SelectionChangedSpanCommand { get; set; }
        #endregion
        public SectionAreaViewModel(Document document, BeamsModel beamsModel)
        {
            #region Get Property
            Doc = document;
            BeamsModel = beamsModel;
            GetScale1();
            CheangedSectionArea();
            #endregion
            LoadSectionAreaCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                p.canvas.Width = BeamsModel.DrawModel.Width;
                p.canvas.Height = BeamsModel.DrawModel.Height;
                scale =GetScale1();
                SectionAreaView uc = ProcessInfoBeamRebar.FindChild<SectionAreaView>(p, "SectionAreaUC");
                CheangedSectionArea();
                DrawSection(p, uc.canvasStart, true, false, false);
                DrawSection(p, uc.canvasMid, false, true, false);
                DrawSection(p, uc.canvasEnd, false, false, true);
                DrawFull(p);
            });
            SelectionChangedSpanCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                SectionAreaView uc = ProcessInfoBeamRebar.FindChild<SectionAreaView>(p, "SectionAreaUC");
                DrawSection(p, uc.canvasStart, true, false, false);
                DrawSection(p, uc.canvasMid, false, true, false);
                DrawSection(p, uc.canvasEnd, false, false, true);
                double a = (((BeamsModel.InfoModels[BeamsModel.SelectedIndexModel.SectionSpan].startPosition)) / BeamsModel.DrawModel.Scale);
                p.scrollViewer.ScrollToLeftEnd();
                p.scrollViewer.ScrollToHorizontalOffset(a);
            });
        }

        private void DrawFull(BeamsWindow p)
        {
            p.canvas.Children.Clear();
            DrawMainCanvas.SpanBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.NodeBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.NodeModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.SpecialNode(p.canvas, BeamsModel.InfoModels, BeamsModel.SpecialNodeModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.DistributeStirrupBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.StirrupModels, BeamsModel.DistributeStirrups, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.MainTopBarBeams(p.canvas, BeamsModel.MainTopBarModel, BeamsModel.SingleMainTopBarModel, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.StyleMainTop, 1000);
            DrawMainCanvas.MainBottomBarBeams(p.canvas, BeamsModel.MainBottomBarModel, BeamsModel.DrawModel, 1000);
            if (BeamsModel.InfoModels.Count == 1)
            {
                DrawMainCanvas.AddTopBarStartBeams(p.canvas, BeamsModel.AddTopBarModel, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.SelectedLayerAddTopStart, BeamsModel.SelectedIndexModel.StartTopChecked);
                DrawMainCanvas.AddTopBarEndBeams(p.canvas, BeamsModel.AddTopBarModel, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.SelectedLayerAddTopEnd, BeamsModel.SelectedIndexModel.EndTopChecked);
            }
            else
            {
                DrawMainCanvas.AddTopBarStartBeams(p.canvas, BeamsModel.AddTopBarModel, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.SelectedLayerAddTopStart, BeamsModel.SelectedIndexModel.StartTopChecked);
                DrawMainCanvas.AddTopBarEndBeams(p.canvas, BeamsModel.AddTopBarModel, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.SelectedLayerAddTopEnd, BeamsModel.SelectedIndexModel.EndTopChecked);
                DrawMainCanvas.AddTopBarMidBeams(p.canvas, BeamsModel.AddTopBarModel, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.SelectedLayerAddTopMid);
            }
            DrawMainCanvas.AddBottomBar(p.canvas, BeamsModel.AddBottomBarModel, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.Span, BeamsModel.SelectedBottomModels);
            DrawMainCanvas.SideBar(p.canvas, BeamsModel.SideBarModel, BeamsModel.DrawModel, 1000);
            if (BeamsModel.SpecialNodeModels.Count != 0)
            {
                DrawMainCanvas.SpecialBar(p.canvas, BeamsModel.SpecialBarModel, BeamsModel.DrawModel, 1000);
            }
        }

        private void DrawSection(BeamsWindow p, Canvas canvas, bool start, bool mid, bool end)
        {
            canvas.Children.Clear();
            double hook = BeamsModel.SettingModel.SelectedHook.get_Parameter(BuiltInParameter.REBAR_HOOK_ANGLE).AsDouble();
            DrawMainCanvas.DrawSection(canvas,
                BeamsModel.InfoModels,
                BeamsModel.StirrupModels,
                BeamsModel.DistributeStirrups,
                left, top, scale,
                BeamsModel.Cover,
                GetdTopBeam(),
                BeamsModel.StirrupModels[BeamsModel.SelectedIndexModel.SectionSpan].a,
                BeamsModel.DrawModel.ColorStirrupChoose,
                BeamsModel.DrawModel.ColorMainBarChoose, BeamsModel.SelectedIndexModel.SectionSpan,
                30, 30,
                extend,
                hook,
                BeamsModel.SettingModel.PrefixDetail);

            DrawMainCanvas.DrawSectionBar(canvas, BeamsModel.InfoModels, BeamsModel.StirrupModels, BeamsModel.DistributeStirrups, BeamsModel.SideBarModel, BeamsModel.SectionAreaModels, left, top, scale, BeamsModel.Cover,
                BeamsModel.StirrupModels[BeamsModel.SelectedIndexModel.SectionSpan].BarS.Diameter,
                40, 40,
                BeamsModel.DrawModel.ColorMainBarChoose, BeamsModel.DrawModel.ColorTag, BeamsModel.SelectedIndexModel.SectionSpan, start, mid, end);
        }

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

        private void CheangedSectionArea()
        {
            for (int j = 0; j < BeamsModel.SectionAreaModels.Count; j++)
            {
                BeamsModel.SectionAreaModels[j].GetBar(BeamsModel.InfoModels[0], BeamsModel.InfoModels[j], BeamsModel.MainTopBarModel, BeamsModel.SingleMainTopBarModel, BeamsModel.MainBottomBarModel, BeamsModel.AddTopBarModel, BeamsModel.AddBottomBarModel[j], BeamsModel.SelectedIndexModel);
                BeamsModel.SectionAreaModels[j].GetNameSection(BeamsModel.InfoModels[j].NumberSpan, BeamsModel.SettingModel);
            }
        }

        private double GetScale1()
        {
            double scale = 0;
            double hmax = GetHmax();
            if (hmax > 430 - 2 * top)
            {
                scale = hmax / (430 - 2 * top);
            }
            else
            {
                if (hmax < 10)
                {
                    scale = hmax / (430 - 2 * top);
                }
                else
                {
                    scale = 1;
                }
            }
            return scale;
        }

        private double GetHmax()
        {
            double hmax = BeamsModel.InfoModels[0].h;
            for (int i = 0; i < BeamsModel.InfoModels.Count; i++)
            {
                if (hmax < BeamsModel.InfoModels[i].h)
                {
                    hmax = BeamsModel.InfoModels[i].h;
                }
            }
            return hmax;
        }
    }
}
