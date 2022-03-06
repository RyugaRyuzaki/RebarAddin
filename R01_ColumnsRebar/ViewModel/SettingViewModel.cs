

using Autodesk.Revit.DB;
using R01_ColumnsRebar.View;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using WpfCustomControls;
using WpfCustomControls.ViewModel;
using R01_ColumnsRebar.LanguageModel;
namespace R01_ColumnsRebar.ViewModel
{
    public class SettingViewModel:BaseViewModel
    {
        #region property
        public Document Doc;
        private ColumnsModel _ColumnsModel;
        public ColumnsModel ColumnsModel { get { return _ColumnsModel; } set { _ColumnsModel = value; OnPropertyChanged(); } }
        #endregion
        #region Command
        public ICommand LoadSettingViewCommand { get; set; }
        public ICommand ColumnsNameChangedCommand { get; set; }
        public ICommand PrefixLevelCommand { get; set; }
        public ICommand PrefixSectionChangedCommand { get; set; }
        public ICommand SelectionHookChangedCommand { get; set; }
        #endregion
        private Languages _Languages;
        public Languages Languages { get { return _Languages; } set { _Languages = value; OnPropertyChanged(); } }
        public SettingViewModel(Document doc, ColumnsModel columnsModel, Languages languages)
        {
            #region property
            Doc = doc;
            ColumnsModel = columnsModel;
            Languages = languages;
            #endregion
            #region LoadCommand
            LoadSettingViewCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                SettingView uc = ProccessInfoClumns.FindChild<SettingView>(p, "SettingUC");
                if (ColumnsModel.InfoModels.Count > 1) uc.PrefixLevel.Visibility = System.Windows.Visibility.Hidden;
                p.MainCanvas.Height = ColumnsModel.DrawModel.Height;
                p.MainCanvas.Width = ColumnsModel.DrawModel.Width;
                p.scrollViewer.ScrollToBottom();
                p.scrollViewer.ScrollToLeftEnd();
                DrawInfo(p);
               
                DrawCanvas1(uc);
                DrawCanvas2(uc);
                DrawCanvas3(uc);
                DrawCanvas4(uc);
            });
            ColumnsNameChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                ColumnsModel.SettingModel.GetDetailViewName();
            });
            PrefixLevelCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                ColumnsModel.SettingModel.GetDetailViewName();
            });
            PrefixSectionChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                ColumnsModel.SettingModel.GetSectionViewName();
            });
            SelectionHookChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                SettingView uc = ProccessInfoClumns.FindChild<SettingView>(p, "SettingUC");
                DrawCanvas4(uc);
            });
            #endregion
        }
        #region Draw
        private void DrawInfo(ColumnsWindow p)
        {
            p.MainCanvas.Children.Clear();
            DrawMainCanvas.DrawInfoColumns(p.MainCanvas, ColumnsModel, ColumnsModel.SelectedIndexModel.SelectedColumn);
        }
        private void DrawCanvas1(SettingView p)
        {
            DrawImage.DrawCanvas1SettingView(p.canvas1,ColumnsModel.DrawModelSection.ColorMainBarChoose,ColumnsModel.SectionStyle);
        }

        private void DrawCanvas2(SettingView p)
        {
            DrawImage.DrawCanvas2SettingView(p.canvas2, ColumnsModel.DrawModelSection.ColorMainBarChoose, ColumnsModel.SectionStyle);

        }
        private void DrawCanvas3(SettingView p)
        {
            p.canvas3.Children.Clear();
            DrawImage.DrawColumnsDetail(p.canvas3);
            DrawImage.DrawColumnsDetail1(p.canvas3);
        }
        private void DrawCanvas4(SettingView p)
        {
            p.canvas4.Children.Clear();
            double hook = ColumnsModel.SettingModel.SelectedHook.get_Parameter(BuiltInParameter.REBAR_HOOK_ANGLE).AsDouble();
            DrawImage.DrawLayerMainBar(p.canvas4, 50, 40, 1, 200, 5, 3, 14, 3, ColumnsModel.DrawModelSection.ColorMainBarChoose);
            DrawImage.DrawHook(p.canvas4, 50, 40, 1, 200, 5, 3, 14, hook, ColumnsModel.DrawModelSection.ColorStirrupChoose);
        }
        #endregion
    }
}
