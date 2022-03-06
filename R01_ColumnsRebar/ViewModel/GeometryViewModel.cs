

using Autodesk.Revit.DB;
using R01_ColumnsRebar.View;
using System;
using System.Windows.Input;
using static R01_ColumnsRebar.ErrorColumns;
using WpfCustomControls;
using WpfCustomControls.ViewModel;
using R01_ColumnsRebar.LanguageModel;
namespace R01_ColumnsRebar.ViewModel
{
    public class GeometryViewModel:BaseViewModel
    {
        #region property
        private ColumnsModel _ColumnsModel;
        public ColumnsModel ColumnsModel { get => _ColumnsModel; set { _ColumnsModel = value; OnPropertyChanged(); } }
        #endregion
        #region Selected
        private InfoModel _SelectedColumn;
        public InfoModel SelectedColumn { get => _SelectedColumn; set { _SelectedColumn = value; OnPropertyChanged(); } }
        #endregion
        #region Icommand
        public ICommand LoadGeometryViewCommand { get; set; }
        public ICommand SelectionColumnChangedCommand { get; set; }
        #endregion
        private Languages _Languages;
        public Languages Languages { get { return _Languages; } set { _Languages = value; OnPropertyChanged(); } }
        public GeometryViewModel(ColumnsModel columnsModel, Languages languages)
        {
            #region property
            ColumnsModel = columnsModel;
            Languages = languages;
            #endregion
            #region loadwindow
            LoadGeometryViewCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                p.MainCanvas.Height = ColumnsModel.DrawModel.Height;
                p.MainCanvas.Width = ColumnsModel.DrawModel.Width;
                p.scrollViewer.ScrollToBottom();
                p.scrollViewer.ScrollToLeftEnd();
                DrawInfo(p);
                GeometryView uc = ProccessInfoClumns.FindChild<GeometryView>(p, "GeometryUC");
                DrawGeometry(uc);
                ShowSectionStyleProperty(uc);
            });
            SelectionColumnChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                DrawInfo(p);
            });
            #endregion
        }
        #region Draw
        private void DrawInfo(ColumnsWindow p)
        {
            p.MainCanvas.Children.Clear();
            DrawMainCanvas.DrawInfoColumns(p.MainCanvas, ColumnsModel, ColumnsModel.SelectedIndexModel.SelectedColumn);
            double top = ColumnsModel.DrawModel.Top - (ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].TopPosition) / (ColumnsModel.DrawModel.Scale);
            p.scrollViewer.ScrollToBottom();
            p.scrollViewer.ScrollToVerticalOffset(top);
        }
        private void DrawGeometry(GeometryView p)
        {
            if (ColumnsModel.SectionStyle==SectionStyle.RECTANGLE)
            {
                double left = 100, top = 50, b = 100, h = 200;
                DrawImage.DrawSection(p.canvasSection, 1, left, top, b,h);
                DrawImage.DimVerticalText(p.canvasSection, left, top, 1, 200, 11, 20, 5, "h");
                DrawImage.DimHorizontalText(p.canvasSection, left, top, 1, 100, 11, 20, 5, "b");
            }
            else
            {
                double left = 150, top = 150, d=200;
                DrawImage.DrawCylindricalSection(p.canvasSection, left, top, 1, d);
                DrawImage.DimVerticalText(p.canvasSection, left-d/2, top-d/2, 1, d, 11, 20, 5, "D");
            }
            DrawImage.DrawGeometryView(p.canvasProperty);
        }
        #endregion
        #region Visibility
        private void ShowSectionStyleProperty(GeometryView p)
        {
            if (ColumnsModel.SectionStyle == SectionStyle.RECTANGLE)
            {
                p.bTextblock.Visibility = System.Windows.Visibility.Visible;
                p.bTextbox.Visibility = System.Windows.Visibility.Visible;
                p.bTextblockUnit.Visibility = System.Windows.Visibility.Visible;
                p.hTextblock.Visibility = System.Windows.Visibility.Visible;
                p.hTextbox.Visibility = System.Windows.Visibility.Visible;
                p.hTextblockUnit.Visibility = System.Windows.Visibility.Visible;
                p.DTextblock.Visibility = System.Windows.Visibility.Hidden;
                p.DTextbox.Visibility = System.Windows.Visibility.Hidden;
                p.DTextblockUnit.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                p.bTextblock.Visibility = System.Windows.Visibility.Hidden;
                p.bTextbox.Visibility = System.Windows.Visibility.Hidden;
                p.bTextblockUnit.Visibility = System.Windows.Visibility.Hidden;
                p.hTextblock.Visibility = System.Windows.Visibility.Hidden;
                p.hTextbox.Visibility = System.Windows.Visibility.Hidden;
                p.hTextblockUnit.Visibility = System.Windows.Visibility.Hidden;
                p.DTextblock.Visibility = System.Windows.Visibility.Visible;
                p.DTextbox.Visibility = System.Windows.Visibility.Visible;
                p.DTextblockUnit.Visibility = System.Windows.Visibility.Visible;
            }
        }
        #endregion
        #region Draw
       
        #endregion
    }
}
