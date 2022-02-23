

using Autodesk.Revit.DB;
using R01_ColumnsRebar.View;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using static R01_ColumnsRebar.ErrorColumns;
using WpfCustomControls;
using WpfCustomControls.ViewModel;
using WpfCustomControls.LanguageModel;
namespace R01_ColumnsRebar.ViewModel
{
    public class AdditionalStirrupsViewModel : BaseViewModel
    {
        #region property
        public Document Doc;
        private ColumnsModel _ColumnsModel;
        public ColumnsModel ColumnsModel { get { return _ColumnsModel; } set { _ColumnsModel = value; OnPropertyChanged(); } }
        #endregion
        #region Selected
        private StirrupModel _SelectedStirrupModel;
        public StirrupModel SelectedStirrupModel { get { return _SelectedStirrupModel; } set { _SelectedStirrupModel = value; OnPropertyChanged(); } }

        #endregion
        #region Distribute Type
        private List<int> _NumberAdditional;
        public List<int> NumberAdditional { get { if (_NumberAdditional == null) { _NumberAdditional = new List<int>() { 1, 2, 3, 4,5,6 }; } return _NumberAdditional; } set { _NumberAdditional = value; OnPropertyChanged(); } }
        #endregion
        #region Command
        public ICommand LoadAdditionalStirrupsViewCommand { get; set; }
        public ICommand SelectionAdditionalStirrupsChangedCommand { get; set; }
        public ICommand HorizontalCheckedCommand { get; set; }
        public ICommand BarHorizontalChangedCommand { get; set; }
        public ICommand NumberBarHorizontalChangedCommand { get; set; }
        public ICommand TypeHorizontalChangedCommand { get; set; }
        public ICommand VerticalCheckedCommand { get; set; }
        public ICommand BarVerticalChangedCommand { get; set; }
        public ICommand NumberBarVerticalChangedCommand { get; set; }
        public ICommand TypeVerticalChangedCommand { get; set; }
        public ICommand ApplyAllColumnCommand { get; set; }
        public ICommand HorizontalaTextChangedCommand { get; set; }
        public ICommand VerticalaTextChangedCommand { get; set; }

        #endregion
        private Languages _Languages;
        public Languages Languages { get { return _Languages; } set { _Languages = value; OnPropertyChanged(); } }
        public AdditionalStirrupsViewModel(Document doc, ColumnsModel columnsModel, Languages languages)
        {
            #region property
            Doc = doc;
            ColumnsModel = columnsModel;
            Languages = languages;
            #endregion
            #region LoadCommand
            LoadAdditionalStirrupsViewCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                p.MainCanvas.Height = ColumnsModel.DrawModel.Height;
                p.MainCanvas.Width = ColumnsModel.DrawModel.Width;
                p.scrollViewer.ScrollToBottom();
                p.scrollViewer.ScrollToLeftEnd();
                AdditionalStirrupsView uc = ProccessInfoClumns.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                //if (ColumnsModel.InfoModels.Count == 1) uc.ApplyAllButton.Visibility = System.Windows.Visibility.Hidden;
                DrawCanvasSection(uc);
                DrawCanvasAdditional(uc);
                DrawCanvasSection(uc);
                ShowComboboxItem(uc);
                SetVisibleaValueHorizontal(uc);
                SetVisibleaValueVertical(uc);
                DrawMain(p);
            });
            SelectionAdditionalStirrupsChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                AdditionalStirrupsView uc = ProccessInfoClumns.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                SetVisibleaValueHorizontal(uc);
                SetVisibleaValueVertical(uc);
                DrawCanvasSection(uc);
                DrawMain(p);
            });
            HorizontalCheckedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                AdditionalStirrupsView uc = ProccessInfoClumns.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                DrawCanvasSection(uc);
            });
            BarHorizontalChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                AdditionalStirrupsView uc = ProccessInfoClumns.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                DrawCanvasSection(uc);
            });
            NumberBarHorizontalChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                AdditionalStirrupsView uc = ProccessInfoClumns.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                DrawCanvasSection(uc);
            });
            TypeHorizontalChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                AdditionalStirrupsView uc = ProccessInfoClumns.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                DrawCanvasSection(uc);
                SetVisibleaValueHorizontal(uc);
            });
            VerticalCheckedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                AdditionalStirrupsView uc = ProccessInfoClumns.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                DrawCanvasSection(uc);
            });
            BarVerticalChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                AdditionalStirrupsView uc = ProccessInfoClumns.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                DrawCanvasSection(uc);
            });
            NumberBarVerticalChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                AdditionalStirrupsView uc = ProccessInfoClumns.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                DrawCanvasSection(uc);
            });
            TypeVerticalChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                AdditionalStirrupsView uc = ProccessInfoClumns.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                DrawCanvasSection(uc);
                SetVisibleaValueVertical(uc);
            });
            HorizontalaTextChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                AdditionalStirrupsView uc = ProccessInfoClumns.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                DrawCanvasSection(uc);
            });
            VerticalaTextChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                AdditionalStirrupsView uc = ProccessInfoClumns.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                DrawCanvasSection(uc);
            });
            ApplyAllColumnCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                for (int i = 0; i < ColumnsModel.StirrupModels.Count; i++)
                {
                    ColumnsModel.StirrupModels[i].IsTypeH = SelectedStirrupModel.IsTypeH;
                    ColumnsModel.StirrupModels[i].BarH = SelectedStirrupModel.BarH;
                    ColumnsModel.StirrupModels[i].TypeH = SelectedStirrupModel.TypeH;
                    ColumnsModel.StirrupModels[i].nH = SelectedStirrupModel.nH;
                    ColumnsModel.StirrupModels[i].aH = SelectedStirrupModel.aH;
                    ColumnsModel.StirrupModels[i].IsTypeV = SelectedStirrupModel.IsTypeV;
                    ColumnsModel.StirrupModels[i].BarV = SelectedStirrupModel.BarV;
                    ColumnsModel.StirrupModels[i].TypeV = SelectedStirrupModel.TypeV;
                    ColumnsModel.StirrupModels[i].nV = SelectedStirrupModel.nV;
                    ColumnsModel.StirrupModels[i].aV = SelectedStirrupModel.aV;

                }
                AdditionalStirrupsView uc = ProccessInfoClumns.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                DrawCanvasSection(uc);
            });
            #endregion
        }
        #region Visibility
        private void ShowComboboxItem(AdditionalStirrupsView p)
        {
            if (ColumnsModel.SectionStyle != SectionStyle.RECTANGLE)
            {
                p.HoComboBoxItem1.Visibility = System.Windows.Visibility.Collapsed;
                p.HoComboBoxItem2.Visibility = System.Windows.Visibility.Collapsed;
                p.HoComboBoxItem3.Visibility = System.Windows.Visibility.Collapsed;
                p.aHTextBlocka.Visibility = System.Windows.Visibility.Collapsed;
                p.aHTextBoxa.Visibility = System.Windows.Visibility.Collapsed;
                p.aHTextBlockUnit.Visibility = System.Windows.Visibility.Collapsed;
                p.nHTextBlock.Visibility = System.Windows.Visibility.Collapsed;
                p.nHComboBox.Visibility = System.Windows.Visibility.Collapsed;

                p.VeComboBoxItem3.Visibility = System.Windows.Visibility.Collapsed;

                p.aVTextBlocka.Visibility = System.Windows.Visibility.Collapsed;
                p.aVTextBoxa.Visibility = System.Windows.Visibility.Collapsed;
                p.aVTextBlockUnit.Visibility = System.Windows.Visibility.Collapsed;
                p.nVTextBlock.Visibility = System.Windows.Visibility.Collapsed;
                p.nVComboBox.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        private void SetVisibleaValueHorizontal(AdditionalStirrupsView p)
        {
            if (ColumnsModel.SectionStyle == SectionStyle.RECTANGLE)
            {
                if (SelectedStirrupModel.TypeH == 0)
                {
                    if (p.aHTextBlocka.Visibility == System.Windows.Visibility.Hidden) p.aHTextBlocka.Visibility = System.Windows.Visibility.Visible;
                    if (p.aHTextBoxa.Visibility == System.Windows.Visibility.Hidden) p.aHTextBoxa.Visibility = System.Windows.Visibility.Visible;
                    if (p.aHTextBlockUnit.Visibility == System.Windows.Visibility.Hidden) p.aHTextBlockUnit.Visibility = System.Windows.Visibility.Visible;
                    if (p.nHTextBlock.Visibility == System.Windows.Visibility.Visible) p.nHTextBlock.Visibility = System.Windows.Visibility.Hidden;
                    if (p.nHComboBox.Visibility == System.Windows.Visibility.Visible) p.nHComboBox.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    if (p.aHTextBlocka.Visibility == System.Windows.Visibility.Visible) p.aHTextBlocka.Visibility = System.Windows.Visibility.Hidden;
                    if (p.aHTextBoxa.Visibility == System.Windows.Visibility.Visible) p.aHTextBoxa.Visibility = System.Windows.Visibility.Hidden;
                    if (p.aHTextBlockUnit.Visibility == System.Windows.Visibility.Visible) p.aHTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                    if (p.nHTextBlock.Visibility == System.Windows.Visibility.Hidden) p.nHTextBlock.Visibility = System.Windows.Visibility.Visible;
                    if (p.nHComboBox.Visibility == System.Windows.Visibility.Hidden) p.nHComboBox.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }
        private void SetVisibleaValueVertical(AdditionalStirrupsView p)
        {
            if (ColumnsModel.SectionStyle == SectionStyle.RECTANGLE)
            {
                if (SelectedStirrupModel.TypeV == 0)
                {
                    if (p.aVTextBlocka.Visibility == System.Windows.Visibility.Hidden) p.aVTextBlocka.Visibility = System.Windows.Visibility.Visible;
                    if (p.aVTextBoxa.Visibility == System.Windows.Visibility.Hidden) p.aVTextBoxa.Visibility = System.Windows.Visibility.Visible;
                    if (p.aVTextBlockUnit.Visibility == System.Windows.Visibility.Hidden) p.aVTextBlockUnit.Visibility = System.Windows.Visibility.Visible;
                    if (p.nVTextBlock.Visibility == System.Windows.Visibility.Visible) p.nVTextBlock.Visibility = System.Windows.Visibility.Hidden;
                    if (p.nVComboBox.Visibility == System.Windows.Visibility.Visible) p.nVComboBox.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    if (p.aVTextBlocka.Visibility == System.Windows.Visibility.Visible) p.aVTextBlocka.Visibility = System.Windows.Visibility.Hidden;
                    if (p.aVTextBoxa.Visibility == System.Windows.Visibility.Visible) p.aVTextBoxa.Visibility = System.Windows.Visibility.Hidden;
                    if (p.aVTextBlockUnit.Visibility == System.Windows.Visibility.Visible) p.aVTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                    if (p.nVTextBlock.Visibility == System.Windows.Visibility.Hidden) p.nVTextBlock.Visibility = System.Windows.Visibility.Visible;
                    if (p.nVComboBox.Visibility == System.Windows.Visibility.Hidden) p.nVComboBox.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }
        #endregion
        #region Draw
        private void DrawCanvasAdditional(AdditionalStirrupsView p)
        {
            if (ColumnsModel.SectionStyle == SectionStyle.RECTANGLE)
            {
                DrawImage.DrawStirrup(p.HoCanvas0, 30, 10, 1, 80, 120, 5, 3, 12, ColumnsModel.DrawModel.ColorStirrupChoose);
                DrawImage.DimHorizontalText(p.HoCanvas0, 35, 70, 1, 70, 11, 20, 5, "a");
                DrawImage.DrawHookVertical(p.HoCanvas1, 70, 10, 1, 120, 5, 3, 12, Math.PI / 2, ColumnsModel.DrawModel.ColorStirrupChoose);
                DrawImage.DrawHookVertical(p.HoCanvas2, 70, 10, 1, 120, 5, 3, 12, 0.75 * Math.PI, ColumnsModel.DrawModel.ColorStirrupChoose);
                DrawImage.DrawHookVertical(p.HoCanvas3, 70, 10, 1, 120, 5, 3, 12, Math.PI, ColumnsModel.DrawModel.ColorStirrupChoose);

                DrawImage.DrawStirrup(p.VeCanvas0, 10, 30, 1, 120, 80, 5, 3, 12, ColumnsModel.DrawModel.ColorStirrupChoose);
                DrawImage.DimVerticalText(p.VeCanvas0, 70, 35, 1, 70, 11, 20, 5, "a");
                DrawImage.DrawHook(p.VeCanvas1, 10, 70, 1, 120, 5, 3, 12, Math.PI / 2, ColumnsModel.DrawModel.ColorStirrupChoose);
                DrawImage.DrawHook(p.VeCanvas2, 10, 70, 1, 120, 5, 3, 12, 0.75 * Math.PI, ColumnsModel.DrawModel.ColorStirrupChoose);
                DrawImage.DrawHook(p.VeCanvas3, 10, 70, 1, 120, 5, 3, 12, Math.PI, ColumnsModel.DrawModel.ColorStirrupChoose);
            }
            else
            {
                DrawImage.DrawStirrup(p.HoCanvas0, 10, 10, 1, 120, 120, 5, 3, 12, ColumnsModel.DrawModel.ColorStirrupChoose);
                DrawImage.DrawHookVertical(p.VeCanvas0, 70, 10, 1, 120, 5, 3, 12, Math.PI / 2, ColumnsModel.DrawModel.ColorStirrupChoose);
                DrawImage.DrawHook(p.VeCanvas0, 10, 70, 1, 120, 5, 3, 12, Math.PI / 2, ColumnsModel.DrawModel.ColorStirrupChoose);
                DrawImage.DrawHookVertical(p.VeCanvas1, 70, 10, 1, 120, 5, 3, 12, Math.PI * 0.75, ColumnsModel.DrawModel.ColorStirrupChoose);
                DrawImage.DrawHook(p.VeCanvas1, 10, 70, 1, 120, 5, 3, 12, Math.PI * 0.75, ColumnsModel.DrawModel.ColorStirrupChoose);
                DrawImage.DrawHookVertical(p.VeCanvas2, 70, 10, 1, 120, 5, 3, 12, Math.PI, ColumnsModel.DrawModel.ColorStirrupChoose);
                DrawImage.DrawHook(p.VeCanvas2, 10, 70, 1, 120, 5, 3, 12, Math.PI, ColumnsModel.DrawModel.ColorStirrupChoose);
            }

        }
        private void DrawCanvasSection(AdditionalStirrupsView p)
        {
            double b = ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].b;
            double h = ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].h;
            double D = (ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].D+2* ColumnsModel.Cover )/ Math.Sqrt(2);
            double d = (ColumnsModel.BarMainModels[ColumnsModel.SelectedIndexModel.SelectedColumn].BarModels.Count == 0) ? ColumnsModel.AllBars[3].Diameter : ColumnsModel.BarMainModels[ColumnsModel.SelectedIndexModel.SelectedColumn].BarModels[0].Bar.Diameter;
            DrawMainCanvas.DrawSectionAndStirrups(p.CanvasSection, ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn], ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn], ColumnsModel.DrawModelSection, ColumnsModel.SectionStyle, ColumnsModel.Cover, d, ColumnsModel.DrawModelSection.ColorStirrup);
            if (SelectedStirrupModel.AddH)
            {
                if (ColumnsModel.SectionStyle == SectionStyle.RECTANGLE)
                {
                    double left = ColumnsModel.DrawModelSection.Left + ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].WestPosition / (ColumnsModel.DrawModelSection.Scale);
                    double top = ColumnsModel.DrawModelSection.Top - ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].NouthPosition / (ColumnsModel.DrawModelSection.Scale);
                    switch (SelectedStirrupModel.TypeH)
                    {
                        case 0:
                            if (SelectedStirrupModel.aH > 0)
                            {
                                DrawImage.DrawStirrup(p.CanvasSection, left + (b / 2 - (SelectedStirrupModel.aH + 2 * ColumnsModel.Cover) / 2) / (ColumnsModel.DrawModelSection.Scale), top, ColumnsModel.DrawModelSection.Scale, SelectedStirrupModel.aH + 2 * ColumnsModel.Cover, ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].h, ColumnsModel.Cover, SelectedStirrupModel.BarH.Diameter, d, ColumnsModel.DrawModelSection.ColorMainBarChoose);
                            }
                            break;
                        case 1: DrawHookHorizontal(p, left, top, b, h, d, Math.PI / 2); break;
                        case 2: DrawHookHorizontal(p, left, top, b, h, d, 0.75*Math.PI ); break;
                        case 3: DrawHookHorizontal(p, left, top, b, h, d, Math.PI ); break;
                        default: break;
                    }

                }
                else
                {
                    double left = ColumnsModel.DrawModelSection.Left + ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].PointXPosition / (ColumnsModel.DrawModelSection.Scale);
                    double top = ColumnsModel.DrawModelSection.Top - ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].PointYPosition / (ColumnsModel.DrawModelSection.Scale);
                    DrawImage.DrawStirrup(p.CanvasSection, left - (D / 2 ) / (ColumnsModel.DrawModelSection.Scale), top - (D / 2) / (ColumnsModel.DrawModelSection.Scale), ColumnsModel.DrawModelSection.Scale, D, D, ColumnsModel.Cover, SelectedStirrupModel.BarH.Diameter, d, ColumnsModel.DrawModelSection.ColorMainBarChoose);
                }
            }
            if (SelectedStirrupModel.AddV)
            {
                if (ColumnsModel.SectionStyle == SectionStyle.RECTANGLE)
                {
                    double left = ColumnsModel.DrawModelSection.Left + ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].WestPosition / (ColumnsModel.DrawModelSection.Scale);
                    double top = ColumnsModel.DrawModelSection.Top - ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].NouthPosition / (ColumnsModel.DrawModelSection.Scale);
                    switch (SelectedStirrupModel.TypeV)
                    {
                        case 0:
                            if (SelectedStirrupModel.aV != 0)
                            {
                                DrawImage.DrawStirrup(p.CanvasSection, left , top + (h / 2 - (SelectedStirrupModel.aV + 2 * ColumnsModel.Cover) / 2) / (ColumnsModel.DrawModelSection.Scale), ColumnsModel.DrawModelSection.Scale, ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].b, SelectedStirrupModel.aV + 2 * ColumnsModel.Cover, ColumnsModel.Cover, SelectedStirrupModel.BarV.Diameter, d, ColumnsModel.DrawModelSection.ColorMainBarChoose);
                            }
                            break;
                        case 1: DrawHookVertical(p, left, top, b, h, d, Math.PI / 2); break;
                        case 2: DrawHookVertical(p, left, top, b, h, d, 0.75 * Math.PI); break;
                        case 3: DrawHookVertical(p, left, top, b, h, d, Math.PI); break;
                        default: break;
                    }

                }
                else
                {
                    double left = ColumnsModel.DrawModelSection.Left + ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].PointXPosition / (ColumnsModel.DrawModelSection.Scale);
                    double top = ColumnsModel.DrawModelSection.Top - ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].PointYPosition / (ColumnsModel.DrawModelSection.Scale);
                    switch (SelectedStirrupModel.TypeV)
                    {
                        case 0:
                            DrawImage.DrawHookVertical(p.CanvasSection, left , top-(ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].D / 2)/ ColumnsModel.DrawModelSection.Scale, ColumnsModel.DrawModelSection.Scale, ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].D, ColumnsModel.Cover, SelectedStirrupModel.BarV.Diameter, d, Math.PI/2, ColumnsModel.DrawModelSection.ColorMainBarChoose);
                            DrawImage.DrawHook(p.CanvasSection, left - (ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].D / 2) / ColumnsModel.DrawModelSection.Scale, top, ColumnsModel.DrawModelSection.Scale, ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].D, ColumnsModel.Cover, SelectedStirrupModel.BarV.Diameter, d, Math.PI/2, ColumnsModel.DrawModelSection.ColorMainBarChoose);
                            break;
                        case 1:
                            DrawImage.DrawHookVertical(p.CanvasSection, left, top - (ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].D / 2) / ColumnsModel.DrawModelSection.Scale, ColumnsModel.DrawModelSection.Scale, ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].D, ColumnsModel.Cover, SelectedStirrupModel.BarV.Diameter, d, Math.PI *0.75, ColumnsModel.DrawModelSection.ColorMainBarChoose);
                            DrawImage.DrawHook(p.CanvasSection, left - (ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].D / 2) / ColumnsModel.DrawModelSection.Scale, top, ColumnsModel.DrawModelSection.Scale, ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].D, ColumnsModel.Cover, SelectedStirrupModel.BarV.Diameter, d, Math.PI *0.75, ColumnsModel.DrawModelSection.ColorMainBarChoose);
                            break;
                        case 2:
                            DrawImage.DrawHookVertical(p.CanvasSection, left, top - (ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].D / 2) / ColumnsModel.DrawModelSection.Scale, ColumnsModel.DrawModelSection.Scale, ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].D, ColumnsModel.Cover, SelectedStirrupModel.BarV.Diameter, d, Math.PI , ColumnsModel.DrawModelSection.ColorMainBarChoose);
                            DrawImage.DrawHook(p.CanvasSection, left - (ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].D / 2) / ColumnsModel.DrawModelSection.Scale, top, ColumnsModel.DrawModelSection.Scale, ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].D, ColumnsModel.Cover, SelectedStirrupModel.BarV.Diameter, d, Math.PI , ColumnsModel.DrawModelSection.ColorMainBarChoose);
                            break;
                        default: break;
                    }
                }
            }
        }
        private void DrawHookHorizontal(AdditionalStirrupsView p, double left, double top, double b, double h, double d, double hook)
        {
            double delta = (b / (SelectedStirrupModel.nH + 1));
            for (int i = 0; i < SelectedStirrupModel.nH; i++)
            {
                DrawImage.DrawHookVertical(p.CanvasSection, left + (i+1)*delta / ColumnsModel.DrawModelSection.Scale, top, ColumnsModel.DrawModelSection.Scale, h, ColumnsModel.Cover, SelectedStirrupModel.BarH.Diameter, d, hook, ColumnsModel.DrawModelSection.ColorMainBarChoose);
            }
            //switch (SelectedStirrupModel.nH)
            //{
            //    case 1:
            //        DrawImage.DrawHookVertical(p.CanvasSection, left + (b / 2) / ColumnsModel.DrawModelSection.Scale, top, ColumnsModel.DrawModelSection.Scale, h, ColumnsModel.Cover, SelectedStirrupModel.BarH.Diameter, d, hook, ColumnsModel.DrawModelSection.ColorMainBarChoose);
            //        break;
            //    case 2:
            //        DrawImage.DrawHookVertical(p.CanvasSection, left + (b / 3) / ColumnsModel.DrawModelSection.Scale, top, ColumnsModel.DrawModelSection.Scale, h, ColumnsModel.Cover, SelectedStirrupModel.BarH.Diameter, d, hook, ColumnsModel.DrawModelSection.ColorMainBarChoose);
            //        DrawImage.DrawHookVertical(p.CanvasSection, left + (b*2 / 3) / ColumnsModel.DrawModelSection.Scale, top, ColumnsModel.DrawModelSection.Scale, h, ColumnsModel.Cover, SelectedStirrupModel.BarH.Diameter, d, hook, ColumnsModel.DrawModelSection.ColorMainBarChoose);
            //        break;
            //    case 3:
            //        DrawImage.DrawHookVertical(p.CanvasSection, left + (b / 4) / ColumnsModel.DrawModelSection.Scale, top, ColumnsModel.DrawModelSection.Scale, h, ColumnsModel.Cover, SelectedStirrupModel.BarH.Diameter, d, hook, ColumnsModel.DrawModelSection.ColorMainBarChoose);
            //        DrawImage.DrawHookVertical(p.CanvasSection, left + (b * 2 / 4) / ColumnsModel.DrawModelSection.Scale, top, ColumnsModel.DrawModelSection.Scale, h, ColumnsModel.Cover, SelectedStirrupModel.BarH.Diameter, d, hook, ColumnsModel.DrawModelSection.ColorMainBarChoose);
            //        DrawImage.DrawHookVertical(p.CanvasSection, left + (b * 3 / 4) / ColumnsModel.DrawModelSection.Scale, top, ColumnsModel.DrawModelSection.Scale, h, ColumnsModel.Cover, SelectedStirrupModel.BarH.Diameter, d, hook, ColumnsModel.DrawModelSection.ColorMainBarChoose);
            //        break;
            //    case 4:
            //        DrawImage.DrawHookVertical(p.CanvasSection, left + (b / 5) / ColumnsModel.DrawModelSection.Scale, top, ColumnsModel.DrawModelSection.Scale, h, ColumnsModel.Cover, SelectedStirrupModel.BarH.Diameter, d, hook, ColumnsModel.DrawModelSection.ColorMainBarChoose);
            //        DrawImage.DrawHookVertical(p.CanvasSection, left + (b * 2 / 5) / ColumnsModel.DrawModelSection.Scale, top, ColumnsModel.DrawModelSection.Scale, h, ColumnsModel.Cover, SelectedStirrupModel.BarH.Diameter, d, hook, ColumnsModel.DrawModelSection.ColorMainBarChoose);
            //        DrawImage.DrawHookVertical(p.CanvasSection, left + (b * 3 / 5) / ColumnsModel.DrawModelSection.Scale, top, ColumnsModel.DrawModelSection.Scale, h, ColumnsModel.Cover, SelectedStirrupModel.BarH.Diameter, d, hook, ColumnsModel.DrawModelSection.ColorMainBarChoose);
            //        DrawImage.DrawHookVertical(p.CanvasSection, left + (b * 4 / 5) / ColumnsModel.DrawModelSection.Scale, top, ColumnsModel.DrawModelSection.Scale, h, ColumnsModel.Cover, SelectedStirrupModel.BarH.Diameter, d, hook, ColumnsModel.DrawModelSection.ColorMainBarChoose);
            //        break;
            //    default: break;
            //}
        }
        private void DrawHookVertical(AdditionalStirrupsView p, double left, double top, double b, double h, double d, double hook)
        {
            double delta = (h / (SelectedStirrupModel.nV + 1));
            for (int i = 0; i < SelectedStirrupModel.nV; i++)
            {
                DrawImage.DrawHook(p.CanvasSection, left, top + (i+1)*delta / ColumnsModel.DrawModelSection.Scale, ColumnsModel.DrawModelSection.Scale, b, ColumnsModel.Cover, SelectedStirrupModel.BarV.Diameter, d, hook, ColumnsModel.DrawModelSection.ColorMainBarChoose);
            }
            switch (SelectedStirrupModel.nV)
            {
                case 1:
                    DrawImage.DrawHook(p.CanvasSection, left , top + (h / 2) / ColumnsModel.DrawModelSection.Scale, ColumnsModel.DrawModelSection.Scale, b, ColumnsModel.Cover, SelectedStirrupModel.BarV.Diameter, d, hook, ColumnsModel.DrawModelSection.ColorMainBarChoose);
                    break;
                case 2:
                    DrawImage.DrawHook(p.CanvasSection, left, top + (h / 3) / ColumnsModel.DrawModelSection.Scale, ColumnsModel.DrawModelSection.Scale, b, ColumnsModel.Cover, SelectedStirrupModel.BarV.Diameter, d, hook, ColumnsModel.DrawModelSection.ColorMainBarChoose);
                    DrawImage.DrawHook(p.CanvasSection, left, top + (h*2 / 3) / ColumnsModel.DrawModelSection.Scale, ColumnsModel.DrawModelSection.Scale, b, ColumnsModel.Cover, SelectedStirrupModel.BarV.Diameter, d, hook, ColumnsModel.DrawModelSection.ColorMainBarChoose);
                    break;
                case 3:
                    DrawImage.DrawHook(p.CanvasSection, left, top + (h / 4) / ColumnsModel.DrawModelSection.Scale, ColumnsModel.DrawModelSection.Scale, b, ColumnsModel.Cover, SelectedStirrupModel.BarV.Diameter, d, hook, ColumnsModel.DrawModelSection.ColorMainBarChoose);
                    DrawImage.DrawHook(p.CanvasSection, left, top + (h * 2 / 4) / ColumnsModel.DrawModelSection.Scale, ColumnsModel.DrawModelSection.Scale, b, ColumnsModel.Cover, SelectedStirrupModel.BarV.Diameter, d, hook, ColumnsModel.DrawModelSection.ColorMainBarChoose);
                    DrawImage.DrawHook(p.CanvasSection, left, top + (h * 3 / 4) / ColumnsModel.DrawModelSection.Scale, ColumnsModel.DrawModelSection.Scale, b, ColumnsModel.Cover, SelectedStirrupModel.BarV.Diameter, d, hook, ColumnsModel.DrawModelSection.ColorMainBarChoose);
                    break;
                case 4:
                    DrawImage.DrawHook(p.CanvasSection, left, top + (h / 5) / ColumnsModel.DrawModelSection.Scale, ColumnsModel.DrawModelSection.Scale, b, ColumnsModel.Cover, SelectedStirrupModel.BarV.Diameter, d, hook, ColumnsModel.DrawModelSection.ColorMainBarChoose);
                    DrawImage.DrawHook(p.CanvasSection, left, top + (h * 2 / 5) / ColumnsModel.DrawModelSection.Scale, ColumnsModel.DrawModelSection.Scale, b, ColumnsModel.Cover, SelectedStirrupModel.BarV.Diameter, d, hook, ColumnsModel.DrawModelSection.ColorMainBarChoose);
                    DrawImage.DrawHook(p.CanvasSection, left, top + (h * 3 / 5) / ColumnsModel.DrawModelSection.Scale, ColumnsModel.DrawModelSection.Scale, b, ColumnsModel.Cover, SelectedStirrupModel.BarV.Diameter, d, hook, ColumnsModel.DrawModelSection.ColorMainBarChoose);
                    DrawImage.DrawHook(p.CanvasSection, left, top + (h * 4 / 5) / ColumnsModel.DrawModelSection.Scale, ColumnsModel.DrawModelSection.Scale, b, ColumnsModel.Cover, SelectedStirrupModel.BarV.Diameter, d, hook, ColumnsModel.DrawModelSection.ColorMainBarChoose);
                    break;
                default: break;
            }
        }
        #endregion
        #region Exam
        private void DrawMain(ColumnsWindow p)
        {
            p.MainCanvas.Children.Clear();
            DrawMainCanvas.DrawInfoColumns(p.MainCanvas, ColumnsModel, 1000);
            DrawMainCanvas.DrawStirrup(p.MainCanvas, ColumnsModel, ColumnsModel.SelectedIndexModel.SelectedColumn);
            double top = ColumnsModel.DrawModel.Top - (ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].TopPosition) / (ColumnsModel.DrawModel.Scale);
            p.scrollViewer.ScrollToBottom();
            p.scrollViewer.ScrollToVerticalOffset(top);
        }
        #endregion
    }
}
