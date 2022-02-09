using WpfCustomControls;
using Autodesk.Revit.DB;
using R02_BeamsRebar.View;
using System.Text.RegularExpressions;
using System.Windows.Input;
namespace R02_BeamsRebar.ViewModel
{
    public class GeometryViewModel:BaseViewModel
    {
        #region Property
        public Document Doc;
        private BeamsModel _BeamsModel;
        public BeamsModel BeamsModel { get => _BeamsModel; set { _BeamsModel = value; OnPropertyChanged(); } }
        #endregion
        #region Command
        public ICommand LoadGeometryViewCommand { get; set; }
        public ICommand SelectionChangedBeamsModelCommand { get; set; }
        public ICommand SelectionChangedHookCommand { get; set; }
        public ICommand SelectionChangedStirrupShapeCommand { get; set; }
        public ICommand SelectionChangedAntiShapeCommand { get; set; }
        public ICommand PrefixLevelCommand { get; set; }
        public ICommand BeamNameTextChangedCommand { get; set; }
        public ICommand PrefixSectionTextChangedCommand { get; set; }
        #endregion
        public GeometryViewModel(Document document,BeamsModel beamsModel)
        {
            #region Get Property
            Doc = document;
            BeamsModel = beamsModel;
            #endregion
            #region LoadCommand
            LoadGeometryViewCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                p.canvas.Width = BeamsModel.DrawModel.Width;
                p.canvas.Height = BeamsModel.DrawModel.Height;
                GeometryView uc = ProcessInfoBeamRebar.FindChild<GeometryView>(p, "GeometryUC");
                DrawCanvas2(uc);
                DrawImage.DrawGeometryView(uc.canvas);
                DrawInfo(p);
            });
            #endregion
            #region Command
            SelectionChangedBeamsModelCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                p.canvas.Children.Clear();
                DrawMainCanvas.SpanBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.Span);
                DrawMainCanvas.NodeBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.NodeModels, BeamsModel.DrawModel, 1000);
                DrawMainCanvas.SpecialNode(p.canvas, BeamsModel.InfoModels, BeamsModel.SpecialNodeModels, BeamsModel.DrawModel, 1000);
                double left = BeamsModel.InfoModels[BeamsModel.SelectedIndexModel.Span].startPosition / BeamsModel.DrawModel.Scale - 10 * BeamsModel.DrawModel.Left;
                p.scrollViewer.ScrollToLeftEnd();
                p.scrollViewer.ScrollToHorizontalOffset(left);
            });
            SelectionChangedHookCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                GeometryView uc = ProcessInfoBeamRebar.FindChild<GeometryView>(p, "GeometryUC");
                DrawCanvas2(uc);
            });
            PrefixLevelCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                BeamsModel.SettingModel.GetDetailViewName();
            });
            BeamNameTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                BeamsModel.SettingModel.GetDetailViewName();
                CheangedSectionArea();
            });
            PrefixSectionTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                BeamsModel.SettingModel.GetSectionViewName();
            });
            #endregion
        }
        #region Draw

        private void DrawCanvas2(GeometryView p)
        {
            p.canvas2.Children.Clear();
            double hook = BeamsModel.SettingModel.SelectedHook.get_Parameter(BuiltInParameter.REBAR_HOOK_ANGLE).AsDouble();
            DrawImage.DrawLayerMainBar(p.canvas2, 30, 20, 1, 200, 5, 3, 14, 3, BeamsModel.DrawModel.ColorMainBarChoose);
            DrawImage.DrawHook(p.canvas2, 30, 20, 1, 200, 5, 3, 14, hook, BeamsModel.DrawModel.ColorStirrupChoose);
        }
        private void CheangedSectionArea()
        {
            for (int j = 0; j < BeamsModel.SectionAreaModels.Count; j++)
            {
                BeamsModel.SectionAreaModels[j].GetBar(BeamsModel.InfoModels[0], BeamsModel.InfoModels[j], BeamsModel.MainTopBarModel, BeamsModel.SingleMainTopBarModel, BeamsModel.MainBottomBarModel, BeamsModel.AddTopBarModel, BeamsModel.AddBottomBarModel[j], BeamsModel.SelectedIndexModel);
                BeamsModel.SectionAreaModels[j].GetNameSection(BeamsModel.InfoModels[j].NumberSpan, BeamsModel.SettingModel);
            }
        }
        private void DrawInfo(BeamsWindow p)
        {
            p.canvas.Children.Clear();
            DrawMainCanvas.SpanBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.NodeBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.NodeModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.SpecialNode(p.canvas, BeamsModel.InfoModels, BeamsModel.SpecialNodeModels, BeamsModel.DrawModel, 1000);
        }
        #endregion
    }
}
