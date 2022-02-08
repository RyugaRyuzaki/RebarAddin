using Autodesk.Revit.DB;
using R01_ColumnsRebar.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using WpfCustomControls;
namespace R01_ColumnsRebar.ViewModel
{
    public class BottomDowelsViewModel:BaseViewModel
    {
        #region property
        public Document Doc;
        private ColumnsModel _ColumnsModel;
        public ColumnsModel ColumnsModel { get { return _ColumnsModel; } set { _ColumnsModel = value; OnPropertyChanged(); } }
        #endregion
        #region Selected
        private BarMainModel _SelectedColumn;
        public BarMainModel SelectedColumn { get => _SelectedColumn; set { _SelectedColumn = value; OnPropertyChanged(); } }
        private BarModel _SelectedBar;
        public BarModel SelectedBar { get => _SelectedBar; set { _SelectedBar = value; OnPropertyChanged(); } }
        private bool _IsEnabledBottomDowels;
        public bool IsEnabledBottomDowels { get => _IsEnabledBottomDowels; set { _IsEnabledBottomDowels = value; OnPropertyChanged(); } }
        private bool _IsEnabledBottomTypeDowels;
        public bool IsEnabledBottomTypeDowels { get => _IsEnabledBottomTypeDowels; set { _IsEnabledBottomTypeDowels = value; OnPropertyChanged(); } }
        #endregion
        #region Command
        public ICommand LoadBottomDowelsViewCommand { get; set; }
        public ICommand SelectionColumnDowelsChangedCommand { get; set; }
        public ICommand CheckBottomDowelsCommand { get; set; }
        public ICommand SelectionBottomTypeDowelsChangedCommand { get; set; }
        public ICommand SelectionBarChangedCommand { get; set; }
        public ICommand ApplyAllBarCommand { get; set; }
        public ICommand BottomDowelsLaTextChangedCommand { get; set; }
        public ICommand BottomDowelsLbTextChangedCommand { get; set; }
        public ICommand BottomDowelsLcTextChangedCommand { get; set; }
        public ICommand FixedBottomBarDowelsCommand { get; set; }
        #endregion
        public BottomDowelsViewModel(Document doc, ColumnsModel columnsModel)
        {
            #region property
            Doc = doc;
            ColumnsModel = columnsModel;
            #endregion
            #region Load
            LoadBottomDowelsViewCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                if (SelectedColumn.BarModels.Count != 0) ColumnsModel.SelectedIndexModel.SelectedMainBar = 0;
                IsEnabledBottomDowels = (SelectedColumn.BarModels.Count != 0) && (SelectedBar != null);
                IsEnabledBottomTypeDowels = (SelectedBar == null) ? false : (SelectedBar.IsBottomDowels);
                BottomDowelsView uc = ProccessInfoClumns.FindChild<BottomDowelsView>(p, "BottomDowelsUC");
                DrawBottomDowelsComboBox(uc);
                ShowLaBottomDowels(uc);
                DrawMain(p);
                DrawSection(uc);
                ShowFixedBottom(uc);
            });
            CheckBottomDowelsCommand = new RelayCommand<ColumnsWindow>((p) => { return (SelectedBar != null); }, (p) =>
            {
                IsEnabledBottomTypeDowels = (SelectedBar.IsBottomDowels);
                BottomDowelsView uc = ProccessInfoClumns.FindChild<BottomDowelsView>(p, "BottomDowelsUC");
                ShowLaBottomDowels(uc);
                ShowFixedBottom(uc);
                
                DrawMain(p);
                DrawSection(uc);
            });
            SelectionBottomTypeDowelsChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return IsEnabledBottomTypeDowels; }, (p) =>
            {
                BottomDowelsView uc = ProccessInfoClumns.FindChild<BottomDowelsView>(p, "BottomDowelsUC");
                ShowLaBottomDowels(uc);
                ShowFixedBottom(uc);
                
                DrawMain(p);
                DrawSection(uc);
            });
            ApplyAllBarCommand = new RelayCommand<ColumnsWindow>((p) => { return (SelectedBar != null) && SelectedColumn.BarModels.Count != 0; }, (p) =>
            {
                BottomDowelsView uc = ProccessInfoClumns.FindChild<BottomDowelsView>(p, "BottomDowelsUC");
                for (int i = 0; i < SelectedColumn.BarModels.Count; i++)
                {
                    SelectedColumn.BarModels[i].BottomDowels = SelectedBar.BottomDowels;
                    SelectedColumn.BarModels[i].IsBottomDowels = SelectedBar.IsBottomDowels;
                    SelectedColumn.BarModels[i].LaBottomDowels = SelectedBar.LaBottomDowels;
                    SelectedColumn.BarModels[i].LbBottomDowels = SelectedBar.LbBottomDowels;
                    SelectedColumn.BarModels[i].LcBottomDowels = SelectedBar.LcBottomDowels;
                }
                DrawMain(p);
                DrawSection(uc);
                ShowFixedBottom(uc);
            });
            SelectionColumnDowelsChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                if (SelectedColumn.BarModels.Count != 0) ColumnsModel.SelectedIndexModel.SelectedMainBar = 0;
                IsEnabledBottomDowels = (SelectedBar != null);
                IsEnabledBottomTypeDowels = (SelectedBar == null) ? false : (SelectedBar.IsBottomDowels);
                BottomDowelsView uc = ProccessInfoClumns.FindChild<BottomDowelsView>(p, "BottomDowelsUC");
                ShowLaBottomDowels(uc);
                ShowFixedBottom(uc);
                DrawMain(p);
                DrawSection(uc);
            });
            SelectionBarChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                IsEnabledBottomDowels = (SelectedBar != null);
                IsEnabledBottomTypeDowels = (SelectedBar == null) ? false : (SelectedBar.IsBottomDowels);
                BottomDowelsView uc = ProccessInfoClumns.FindChild<BottomDowelsView>(p, "BottomDowelsUC");
                ShowLaBottomDowels(uc);
                DrawMain(p);
                DrawSection(uc);
            });
            BottomDowelsLaTextChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return (SelectedBar != null); }, (p) =>
            {
                BottomDowelsView uc = ProccessInfoClumns.FindChild<BottomDowelsView>(p, "BottomDowelsUC");
                if (double.TryParse(uc.BottomDowelsLaTextBox.Text.ToString(), out double S))
                {
                    DrawMain(p);
                    DrawSection(uc);
                }
            });
            BottomDowelsLbTextChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return (SelectedBar != null); }, (p) =>
            {
                BottomDowelsView uc = ProccessInfoClumns.FindChild<BottomDowelsView>(p, "BottomDowelsUC");
                if (double.TryParse(uc.BottomDowelsLbTextBox.Text.ToString(), out double S))
                {
                    DrawMain(p);
                    DrawSection(uc);
                }
            });
            BottomDowelsLcTextChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return (SelectedBar != null); }, (p) =>
            {
                BottomDowelsView uc = ProccessInfoClumns.FindChild<BottomDowelsView>(p, "BottomDowelsUC");
                if (double.TryParse(uc.BottomDowelsLcTextBox.Text.ToString(), out double S))
                {
                    DrawMain(p);
                    DrawSection(uc);
                }
            });
            FixedBottomBarDowelsCommand = new RelayCommand<ColumnsWindow>((p) => { return SelectedColumn.FixedBottom; }, (p) =>
            {
                BarMainModel barMainModelDown = ColumnsModel.BarMainModels.Where(x => x.NumberColumn == SelectedColumn.NumberColumn - 1).FirstOrDefault();
                if (barMainModelDown!=null)
                {
                    if (barMainModelDown.FixedTop)
                    {
                        BottomDowelsView uc = ProccessInfoClumns.FindChild<BottomDowelsView>(p, "BottomDowelsUC");
                        if (barMainModelDown.AddBarModels.Count==0)
                        {
                           
                            ObservableCollection<BarModel> barModelsDown = new ObservableCollection<BarModel>(barMainModelDown.BarModels.Where(x => x.IsTopDowels && x.TopDowels == 0).ToList());
                            if (barModelsDown.Count==SelectedColumn.BarModels.Count)
                            {
                                for (int i = 0; i < barModelsDown.Count; i++)
                                {
                                    //if (barMainModelDown.SplitOverlap==50)
                                    //{
                                    //    if (barModelsDown[i].EvenTop)
                                    //    {
                                    //        SelectedColumn.BarModels[i].LcBottomDowels = 0;
                                    //    }
                                    //    else
                                    //    {
                                    //        SelectedColumn.BarModels[i].LcBottomDowels = barMainModelDown.Overlap*barMainModelDown.Bar.Diameter;
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    SelectedColumn.BarModels[i].LcBottomDowels = 0;
                                    //}
                                    SelectedColumn.BarModels[i].LcBottomDowels = barModelsDown[i].LbTopDowels- barMainModelDown.Overlap * barMainModelDown.Bar.Diameter;
                                    SelectedColumn.BarModels[i].EvenBottom = barModelsDown[i].EvenTop;
                                    DrawMain(p);
                                    DrawSection(uc);
                                }
                            }
                        }
                        else
                        {
                           
                            if (barMainModelDown.AddBarModels.Count == SelectedColumn.BarModels.Count)
                            {
                                for (int i = 0; i < barMainModelDown.AddBarModels.Count; i++)
                                {
                                    if (barMainModelDown.SplitOverlap == 50)
                                    {
                                        if (barMainModelDown.AddBarModels[i].EvenTop)
                                        {
                                            SelectedColumn.BarModels[i].LcBottomDowels = 0;
                                        }
                                        else
                                        {
                                            SelectedColumn.BarModels[i].LcBottomDowels = barMainModelDown.Overlap * barMainModelDown.Bar.Diameter;
                                        }
                                    }
                                    else
                                    {
                                        SelectedColumn.BarModels[i].LcBottomDowels = 0;
                                    }
                                    SelectedColumn.BarModels[i].EvenBottom = barMainModelDown.AddBarModels[i].EvenTop;
                                    DrawMain(p);
                                    DrawSection(uc);
                                }
                            }
                        }
                    }
                }
            });
            #endregion
        }
        #region Draw
        private void DrawSection(BottomDowelsView p)
        {
            double d = (ColumnsModel.BarMainModels[ColumnsModel.SelectedIndexModel.SelectedColumn].BarModels.Count == 0) ? ColumnsModel.AllBars[3].Diameter : ColumnsModel.BarMainModels[ColumnsModel.SelectedIndexModel.SelectedColumn].BarModels[0].Bar.Diameter;
            p.CanvasSection.Children.Clear();
            InfoModel infoModelDown = ColumnsModel.InfoModels.Where(x => x.NumberColumn == ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].NumberColumn - 1).FirstOrDefault();
            StirrupModel stirrupModelDown = ColumnsModel.StirrupModels.Where(x => x.NumberColumn == ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn].NumberColumn - 1).FirstOrDefault();
            BarMainModel barMainModelDown = ColumnsModel.BarMainModels.Where(x => x.NumberColumn == ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn].NumberColumn - 1).FirstOrDefault();
            DrawMainCanvas.DrawSectionAndStirrupDowelsBottom(p.CanvasSection, ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn], ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn], ColumnsModel.DrawModelDowels, ColumnsModel.SectionStyle, ColumnsModel.Cover, d, ColumnsModel.DrawModelDowels.ColorStirrup, infoModelDown, stirrupModelDown);
            if (SelectedColumn.BarModels.Count != 0)
            {
                SolidColorBrush solidColorBrush = ColumnsModel.DrawModelDowels.ColorMainBar;
                if (ColumnsModel.SectionStyle == ErrorColumns.SectionStyle.RECTANGLE)
                {

                    for (int i = 0; i < SelectedColumn.BarModels.Count; i++)
                    {
                        if (i == ColumnsModel.SelectedIndexModel.SelectedMainBar)
                        {
                            solidColorBrush = ColumnsModel.DrawModelDowels.ColorMainBarChoose;
                        }
                        else
                        {
                            solidColorBrush = ColumnsModel.DrawModelDowels.ColorMainBar;
                        }
                        double left = ColumnsModel.DrawModelDowels.Left + SelectedColumn.BarModels[i].X0 / ColumnsModel.DrawModelDowels.Scale;
                        double top = ColumnsModel.DrawModelDowels.Top - SelectedColumn.BarModels[i].Y0 / ColumnsModel.DrawModelDowels.Scale;
                        DrawImage.DrawOneBarSection(p.CanvasSection, left, top, ColumnsModel.DrawModelDowels.Scale, SelectedBar.Bar.Diameter, solidColorBrush);
                    }
                }
                else
                {
                    for (int i = 0; i < SelectedColumn.BarModels.Count; i++)
                    {
                        if (i == ColumnsModel.SelectedIndexModel.SelectedMainBar)
                        {
                            solidColorBrush = ColumnsModel.DrawModelDowels.ColorMainBarChoose;
                        }
                        else
                        {
                            solidColorBrush = ColumnsModel.DrawModelDowels.ColorMainBar;
                        }
                        double left = ColumnsModel.DrawModelDowels.Left + SelectedColumn.BarModels[i].X0 / ColumnsModel.DrawModelDowels.Scale;
                        double top = ColumnsModel.DrawModelDowels.Top - SelectedColumn.BarModels[i].Y0 / ColumnsModel.DrawModelDowels.Scale;
                        DrawImage.DrawOneBarSection(p.CanvasSection, left, top, ColumnsModel.DrawModelDowels.Scale, SelectedBar.Bar.Diameter, solidColorBrush);
                    }
                }
                DrawMainCanvas.DrawBarBottomDowels(p.CanvasSection, ColumnsModel.DrawModelDowels, SelectedColumn, SelectedBar.BarNumber - 1, barMainModelDown);
            }
        }
        private void DrawMain(ColumnsWindow p)
        {
            InfoModel infoModelUp = ColumnsModel.InfoModels.Where(x => x.NumberColumn == ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].NumberColumn + 1).FirstOrDefault();
            //BarMainModel barMainModelUp = ColumnsModel.BarMainModels.Where(x => x.NumberColumn == SelectedColumn.NumberColumn + 1).FirstOrDefault();
            double ds = ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn].BarS.Diameter;
            double dsUp = (infoModelUp == null) ? (ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn].BarS.Diameter) : (ColumnsModel.StirrupModels[ColumnsModel.SelectedIndexModel.SelectedColumn + 1].BarS.Diameter);
            SelectedColumn.RefreshLocationBarModels(ColumnsModel.SectionStyle, ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn], ColumnsModel.Cover, ds,dsUp, infoModelUp);
            p.MainCanvas.Children.Clear();
            DrawMainCanvas.DrawInfoColumns(p.MainCanvas, ColumnsModel, ColumnsModel.SelectedIndexModel.SelectedColumn);
            DrawMainCanvas.DrawBarMains(p.MainCanvas, ColumnsModel, ColumnsModel.SelectedIndexModel.SelectedColumn, (SelectedBar != null) ? SelectedBar.BarNumber : 1000,1000);
            double top = ColumnsModel.DrawModel.Top - (ColumnsModel.InfoModels[ColumnsModel.SelectedIndexModel.SelectedColumn].TopPosition) / (ColumnsModel.DrawModel.Scale);
            p.scrollViewer.ScrollToBottom();
            p.scrollViewer.ScrollToVerticalOffset(top);
        }
        private void DrawBottomDowelsComboBox(BottomDowelsView p)
        {
            DrawImage.DrawBottomDowelsType1(p.BottomDowelsTypeCanvas1);
            DrawImage.DrawBottomDowelsType0(p.BottomDowelsTypeCanvas0);
        }
        #endregion
        #region Method
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
        #endregion
        #region ShowFixedBottom
        private void ShowFixedBottom(BottomDowelsView p)
        {
            InfoModel infoModelDown = ColumnsModel.InfoModels.Where(x => x.NumberColumn == SelectedColumn.NumberColumn - 1).FirstOrDefault();
            BarMainModel barMainModelDown = ColumnsModel.BarMainModels.Where(x => x.NumberColumn == SelectedColumn.NumberColumn - 1).FirstOrDefault();
            SelectedColumn.FixedBottom = SelectedColumn.ConditionFixedBottom(ColumnsModel.SectionStyle, infoModelDown, barMainModelDown);
            if (SelectedColumn.FixedBottom)
            {
                p.FixedDown.Visibility = System.Windows.Visibility.Visible;
            }
            else { p.FixedDown.Visibility = System.Windows.Visibility.Hidden; }
        }
        #endregion
        #region Change Top Value
        //private void ChangeBottomValue()
        //{
        //    if (SelectedColumn.BarModels.Count == 0 || SelectedBar == null) return;
        //    if (SelectedBar.IsBottomDowels)
        //    {
        //        if (SelectedBar.BottomDowels == 0)
        //        {
        //            SelectedBar.LaBottomDowels = 0;
        //            SelectedBar.LbBottomDowels = 0;
        //            SelectedBar.LcBottomDowels = (SelectedColumn.SplitOverlap == 100) ? SelectedColumn.Overlap * SelectedBar.Bar.Diameter : SelectedColumn.Overlap * SelectedBar.Bar.Diameter;
        //        }
        //        else
        //        {
        //            SelectedBar.LaBottomDowels = (SelectedColumn.SplitOverlap == 100) ? SelectedColumn.Overlap * SelectedBar.Bar.Diameter : SelectedColumn.Overlap * SelectedBar.Bar.Diameter;
        //            SelectedBar.LbBottomDowels = (SelectedColumn.SplitOverlap == 100) ? SelectedColumn.Overlap * SelectedBar.Bar.Diameter : SelectedColumn.Overlap * SelectedBar.Bar.Diameter;
        //            SelectedBar.LcBottomDowels = 0;
        //        }
        //    }
        //    else
        //    {
        //        SelectedBar.LaBottomDowels = 0;
        //        SelectedBar.LbBottomDowels = 0;
        //        SelectedBar.LcBottomDowels = 0;
        //    }
        //}
        #endregion
    }
}
