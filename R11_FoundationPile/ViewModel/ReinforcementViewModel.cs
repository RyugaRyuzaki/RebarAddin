using Autodesk.Revit.DB;
using R11_FoundationPile.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WpfCustomControls;
using WpfCustomControls.ViewModel;
using DSP;
namespace R11_FoundationPile.ViewModel
{
    public class ReinforcementViewModel : BaseViewModel
    {
        #region Property
        public Document Doc;
        private UnitProject _Unit;
        public UnitProject Unit { get { return _Unit; } set { _Unit = value; OnPropertyChanged(); } }
        private FoundationPileModel _FoundationPileModel;
        public FoundationPileModel FoundationPileModel { get => _FoundationPileModel; set { _FoundationPileModel = value; OnPropertyChanged(); } }


        private ObservableCollection<string> _AllSpans;
        public ObservableCollection<string> AllSpans { get { if (_AllSpans == null) { _AllSpans = new ObservableCollection<string>(new List<string> { "Horizontal", "Vertical" }); } return _AllSpans; } set { _AllSpans = value; OnPropertyChanged(); } }
        #endregion
        private FoundationBarModel _SelectedFoundationBarModel;
        public FoundationBarModel SelectedFoundationBarModel { get => _SelectedFoundationBarModel; set { _SelectedFoundationBarModel = value; OnPropertyChanged(); } }
        private BarModel _SelectedBarModel;
        public BarModel SelectedBarModel
        {
            get => _SelectedBarModel; set
            {
                _SelectedBarModel = value; OnPropertyChanged();
                if (SelectedBarModel != null) IsEnabled = !(SelectedBarModel.Name.Contains("Bottom")||SelectedBarModel.Name.Contains("Side"));
            }
        }
        private bool _IsEnabled;
        public bool IsEnabled { get => _IsEnabled; set { _IsEnabled = value; OnPropertyChanged(); } }
        #region Icommand
        public ICommand LoadReinforcementViewCommand { get; set; }


        public ICommand SelectionChangedFoundationBarModelCommand { get; set; }
        public ICommand SelectionChangedSpanOrientationCommand { get; set; }
        public ICommand SelectionChangedBarModelCommand { get; set; }
        public ICommand CheckModelCommand { get; set; }
        public ICommand FixedNumberBarCommand { get; set; }
        public ICommand HookLengthTextChangedCommand { get; set; }
        public ICommand DistanceTextChangedCommand { get; set; }
        public ICommand NumberBarTextChangedCommand { get; set; }
        public ICommand SelectionChangedBarCommand { get; set; }
        #endregion
        private TaskBarViewModel _TaskBarViewModel;
        public TaskBarViewModel TaskBarViewModel { get { return _TaskBarViewModel; } set { _TaskBarViewModel = value; OnPropertyChanged(); } }
        public ReinforcementViewModel(Document doc, FoundationPileModel foundationPileModel, UnitProject unit, TaskBarViewModel taskBarViewModel)
        {
            #region property
            Doc = doc;
            Unit = unit;
            FoundationPileModel = foundationPileModel;
            FoundationPileModel.GetBarModels(Doc);
            TaskBarViewModel = taskBarViewModel;
            #endregion
            #region Load
            LoadReinforcementViewCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {
                p.MainCanvas.Width = 820;
                ReinforcementView uc = ProccessInfoClumns.FindChild<ReinforcementView>(p, "ReinforcementUC");
                ShowProperty(uc);
                DrawSpanOrientation(uc);
               
                if (SelectedBarModel != null) IsEnabled = !(SelectedBarModel.Name.Contains("Bottom") || SelectedBarModel.Name.Contains("Side"));
                double coverSide = double.Parse(UnitFormatUtils.Format(Doc.GetUnits(), SpecTypeId.Length, FoundationPileModel.SettingModel.SelectedSideCover.CoverDistance, false));
                FoundationModel foundationModel = FoundationPileModel.FindFoundationModelByLoacationName(SelectedFoundationBarModel.LocationName);
                double p1 = GetP1(foundationModel);
                double p2 = GetP2(foundationModel);
                double p3 = GetP3(foundationModel);
                double p4 = GetP4(foundationModel);
                SelectedFoundationBarModel.FixNumber(p1, p2, p3, p4, coverSide);
                DrawMain(p);
            });
            SelectionChangedFoundationBarModelCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {
                if (SelectedBarModel != null) IsEnabled = !(SelectedBarModel.Name.Contains("Bottom") || SelectedBarModel.Name.Contains("Side"));
                ReinforcementView uc = ProccessInfoClumns.FindChild<ReinforcementView>(p, "ReinforcementUC");
                FoundationPileModel.SelectedIndexModel.SelectedIndexBarModel = 0;

                ShowProperty(uc);
                DrawSpanOrientation(uc);
                DrawMain(p);
            });
            SelectionChangedSpanOrientationCommand = new RelayCommand<FoundationPileWindow>((p) => { return SelectedFoundationBarModel != null; }, (p) =>
            {
                ReinforcementView uc = ProccessInfoClumns.FindChild<ReinforcementView>(p, "ReinforcementUC");
                DrawSpanOrientation(uc);
                DrawMain(p);
            });
            SelectionChangedBarModelCommand = new RelayCommand<FoundationPileWindow>((p) => { return SelectedFoundationBarModel != null; }, (p) =>
            {
                if (SelectedBarModel != null) IsEnabled = !(SelectedBarModel.Name.Contains("Bottom") || SelectedBarModel.Name.Contains("Side"));
                ReinforcementView uc = ProccessInfoClumns.FindChild<ReinforcementView>(p, "ReinforcementUC");
                ShowProperty(uc);
                DrawMain(p);

            });
            CheckModelCommand = new RelayCommand<FoundationPileWindow>((p) => { return IsEnabled; }, (p) =>
            {
                ReinforcementView uc = ProccessInfoClumns.FindChild<ReinforcementView>(p, "ReinforcementUC");
                ShowProperty(uc);
                DrawMain(p);

            });
            FixedNumberBarCommand = new RelayCommand<FoundationPileWindow>((p) => { return SelectedBarModel.Name.Contains("Bottom")||SelectedBarModel.Name.Contains("Top"); }, (p) =>
            {
                double coverSide = double.Parse(UnitFormatUtils.Format(Doc.GetUnits(), SpecTypeId.Length, FoundationPileModel.SettingModel.SelectedSideCover.CoverDistance, false));
                FoundationModel foundationModel = FoundationPileModel.FindFoundationModelByLoacationName(SelectedFoundationBarModel.LocationName);
                double p1 = GetP1(foundationModel);
                double p2 = GetP2(foundationModel);
                double p3 = GetP3(foundationModel);
                double p4 = GetP4(foundationModel);
                BarModel mainBottom = SelectedFoundationBarModel.BarModels.Where(x => x.Name.Equals("MainBottom")).FirstOrDefault();
                BarModel secondaryBottom = SelectedFoundationBarModel.BarModels.Where(x => x.Name.Equals("SecondaryBottom")).FirstOrDefault();
                BarModel side = SelectedFoundationBarModel.BarModels.Where(x => x.Name.Equals("Side")).FirstOrDefault();
                SelectedBarModel.Distance = SelectedBarModel.FixDistance(p1, p2, p3, p4, coverSide, mainBottom, secondaryBottom, side);
                DrawMain(p);
            });
            HookLengthTextChangedCommand = new RelayCommand<FoundationPileWindow>((p) => { return SelectedBarModel.Name.Contains("Bottom")||SelectedBarModel.Name.Contains("Top"); }, (p) =>
            {
                ReinforcementView uc = ProccessInfoClumns.FindChild<ReinforcementView>(p, "ReinforcementUC");
                if (double.TryParse(uc.HookLengthTextBox.Text.ToString(),out double S))
                {
                    DrawMain(p);
                }
            });
            DistanceTextChangedCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {
                ReinforcementView uc = ProccessInfoClumns.FindChild<ReinforcementView>(p, "ReinforcementUC");
                if (double.TryParse(uc.DistanceTextBox.Text.ToString(), out double S))
                {
                    if (S>0)
                    {
                        double coverSide = double.Parse(UnitFormatUtils.Format(Doc.GetUnits(), SpecTypeId.Length, FoundationPileModel.SettingModel.SelectedSideCover.CoverDistance, false));
                        FoundationModel foundationModel = FoundationPileModel.FindFoundationModelByLoacationName(SelectedFoundationBarModel.LocationName);
                        double p1 = GetP1(foundationModel);
                        double p2 = GetP2(foundationModel);
                        double p3 = GetP3(foundationModel);
                        double p4 = GetP4(foundationModel);
                        BarModel mainBottom = SelectedFoundationBarModel.BarModels.Where(x => x.Name.Equals("MainBottom")).FirstOrDefault();
                        BarModel secondaryBottom = SelectedFoundationBarModel.BarModels.Where(x => x.Name.Equals("SecondaryBottom")).FirstOrDefault();
                        BarModel side = SelectedFoundationBarModel.BarModels.Where(x => x.Name.Equals("Side")).FirstOrDefault();
                        SelectedBarModel.Number = SelectedBarModel.FixNumber(p1, p2, p3, p4, coverSide, mainBottom, secondaryBottom, side);
                        DrawMain(p);
                    }
                }
            });
            NumberBarTextChangedCommand = new RelayCommand<FoundationPileWindow>((p) => { return !(SelectedBarModel.Name.Contains("Bottom") || SelectedBarModel.Name.Contains("Top")); }, (p) =>
            {
                ReinforcementView uc = ProccessInfoClumns.FindChild<ReinforcementView>(p, "ReinforcementUC");
                if (int.TryParse(uc.NumberTextBox.Text.ToString(), out int S))
                {
                    DrawMain(p);
                }
            });
            SelectionChangedBarCommand = new RelayCommand<FoundationPileWindow>((p) => { return SelectedBarModel.IsModel; }, (p) =>
            {
                DrawMain(p);
            });
            #endregion



        }


        #region Method
        private void ShowProperty(ReinforcementView uc)
        {
            if (SelectedBarModel.IsModel)
            {
                uc.BarTextBlock.Visibility = System.Windows.Visibility.Visible;
                uc.BarComboBox.Visibility = System.Windows.Visibility.Visible;

                uc.HookLengthTextBlock.Visibility = System.Windows.Visibility.Visible;
                uc.HookLengthTextBlockUnit.Visibility = System.Windows.Visibility.Visible;
                uc.HookLengthTextBox.Visibility = System.Windows.Visibility.Visible;

                uc.HookTypeTextBlock.Visibility = System.Windows.Visibility.Visible;

                uc.DistanceTextBlock.Visibility = System.Windows.Visibility.Visible;
                uc.DistanceTextBox.Visibility = System.Windows.Visibility.Visible;
                uc.DistanceTextBlockUnit.Visibility = System.Windows.Visibility.Visible;

                uc.NumberTextBlock.Visibility = System.Windows.Visibility.Visible;
                uc.NumberTextBox.Visibility = System.Windows.Visibility.Visible;

                uc.FixNumberButton.Visibility = System.Windows.Visibility.Visible;
                if (SelectedBarModel.Name.Contains("Add") || SelectedBarModel.Name.Contains("Side"))
                {
                    uc.HookLengthTextBlock.Visibility = System.Windows.Visibility.Hidden;
                    uc.HookLengthTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                    uc.HookLengthTextBox.Visibility = System.Windows.Visibility.Hidden;
                    uc.HookTypeTextBlock.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    uc.HookLengthTextBlock.Visibility = System.Windows.Visibility.Visible;
                    uc.HookLengthTextBlockUnit.Visibility = System.Windows.Visibility.Visible;
                    uc.HookLengthTextBox.Visibility = System.Windows.Visibility.Visible;
                    uc.HookTypeTextBlock.Visibility = System.Windows.Visibility.Hidden;
                }
                if (SelectedBarModel.Name.Contains("Bottom") || SelectedBarModel.Name.Contains("Top"))
                {
                    uc.FixNumberButton.Visibility = System.Windows.Visibility.Visible;
                    uc.NumberTextBlock.Visibility = System.Windows.Visibility.Hidden;
                    uc.NumberTextBox.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    uc.FixNumberButton.Visibility = System.Windows.Visibility.Hidden;
                    uc.NumberTextBlock.Visibility = System.Windows.Visibility.Visible;
                    uc.NumberTextBox.Visibility = System.Windows.Visibility.Visible;
                }
            }
            else
            {
                uc.BarTextBlock.Visibility = System.Windows.Visibility.Hidden;
                uc.BarComboBox.Visibility = System.Windows.Visibility.Hidden;

                uc.HookLengthTextBlock.Visibility = System.Windows.Visibility.Hidden;
                uc.HookLengthTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                uc.HookLengthTextBox.Visibility = System.Windows.Visibility.Hidden;

                uc.HookTypeTextBlock.Visibility = System.Windows.Visibility.Hidden;

                uc.DistanceTextBlock.Visibility = System.Windows.Visibility.Hidden;
                uc.DistanceTextBox.Visibility = System.Windows.Visibility.Hidden;
                uc.DistanceTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;

                uc.NumberTextBlock.Visibility = System.Windows.Visibility.Hidden;
                uc.NumberTextBox.Visibility = System.Windows.Visibility.Hidden;

                uc.FixNumberButton.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        #endregion
        #region Draw
        private void DrawSpanOrientation(ReinforcementView p)
        {
            p.BarCanvas.Children.Clear();
            if (SelectedFoundationBarModel != null)
            {
                FoundationModel foundationModel = FoundationPileModel.FindFoundationModelByLoacationName(SelectedFoundationBarModel.LocationName);
                if (foundationModel != null)
                {

                    if (foundationModel.BoundingLocation.Count != 0 && foundationModel.PileModels.Count != 0)
                    {
                        FoundationPileModel.DrawModelBar.GetScaleBar(foundationModel, Unit);
                        DrawMainCanvas.DrawBarFoundationSection(p.BarCanvas, FoundationPileModel.DrawModelBar, foundationModel, FoundationPileModel.SettingModel, 1000, foundationModel.Image, SelectedFoundationBarModel.SpanOrientation);
                    }
                }

            }
        }
        private void DrawMain(FoundationPileWindow p)
        {
            p.MainCanvas.Children.Clear();
            if (SelectedFoundationBarModel != null)
            {
                FoundationModel foundationModel = FoundationPileModel.FindFoundationModelByLoacationName(SelectedFoundationBarModel.LocationName);
                if (foundationModel != null)
                {
                    FoundationPileModel.DrawModel.GetScale(foundationModel, Unit);
                    FoundationPileModel.DrawModel.GetScaleHeigth(FoundationPileModel.SettingModel, Unit);
                    double hook = FoundationPileModel.SettingModel.SelectedHook.get_Parameter(BuiltInParameter.REBAR_HOOK_ANGLE).AsDouble();
                    double coverTop = double.Parse(UnitFormatUtils.Format(Doc.GetUnits(), SpecTypeId.Length, FoundationPileModel.SettingModel.SelectedTopCover.CoverDistance, false));
                    double coverBottom = double.Parse(UnitFormatUtils.Format(Doc.GetUnits(), SpecTypeId.Length, FoundationPileModel.SettingModel.SelectedBotomCover.CoverDistance, false));
                    double coverSide = double.Parse(UnitFormatUtils.Format(Doc.GetUnits(), SpecTypeId.Length, FoundationPileModel.SettingModel.SelectedSideCover.CoverDistance, false));
                    DrawMainCanvas.DrawBarSection(p.MainCanvas, FoundationPileModel.DrawModel, foundationModel, SelectedFoundationBarModel.BarModels,SelectedBarModel, FoundationPileModel.SettingModel, foundationModel.Image, SelectedFoundationBarModel.SpanOrientation, coverTop, coverBottom, coverSide, GetP1(foundationModel), GetP2(foundationModel), GetP3(foundationModel), GetP4(foundationModel), hook);
                }

            }
        }
        #endregion
        private double GetP1(FoundationModel foundationModel)
        {
            double p1 = 0;
            switch (SelectedFoundationBarModel.Image)
            {
                case 0:
                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE") && foundationModel.ColumnModel.b > foundationModel.ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p1 = foundationModel.BoundingLocation[2].X;
                        }
                        else
                        {
                            p1 = foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].Y;
                        }
                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p1 = foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].X;
                        }
                        else
                        {
                            p1 = foundationModel.BoundingLocation[2].Y;
                        }
                    }

                    break;
                case 1:
                    if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                    {
                        p1 = foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].X;
                    }
                    else
                    {
                        p1 = foundationModel.BoundingLocation[0].Y;
                    }

                    break;
                case 2:
                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE") && foundationModel.ColumnModel.b > foundationModel.ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p1 = foundationModel.BoundingLocation[2].X;
                        }
                        else
                        {
                            p1 = foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].Y;
                        }
                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p1 = foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].X;
                        }
                        else
                        {
                            p1 = foundationModel.BoundingLocation[2].Y;
                        }
                    }
                    break;
                case 3:
                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE") && foundationModel.ColumnModel.b > foundationModel.ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p1 = foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].X;
                        }
                        else
                        {
                            p1 = foundationModel.BoundingLocation[3].Y;
                        }
                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p1 = foundationModel.BoundingLocation[3].X;
                        }
                        else
                        {
                            p1 = foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].Y;
                        }
                    }
                    break;
                default:
                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE") && foundationModel.ColumnModel.b > foundationModel.ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p1 = foundationModel.BoundingLocation[2].X;
                        }
                        else
                        {
                            p1 = foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].Y;
                        }
                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p1 = foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].X;
                        }
                        else
                        {
                            p1 = foundationModel.BoundingLocation[2].Y;
                        }
                    }
                    break;
            }
            return p1;
        }
        private double GetP2(FoundationModel foundationModel)
        {
            double p2 = 0;
            switch (SelectedFoundationBarModel.Image)
            {
                case 0:

                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE") && foundationModel.ColumnModel.b > foundationModel.ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p2 = foundationModel.BoundingLocation[0].X;
                        }
                        else
                        {
                            p2 = foundationModel.BoundingLocation[0].Y;
                        }
                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p2 = foundationModel.BoundingLocation[0].X;
                        }
                        else
                        {
                            p2 = foundationModel.BoundingLocation[0].Y;
                        }
                    }

                    break;
                case 1:
                    if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                    {
                        p2 = foundationModel.BoundingLocation[0].X;
                    }
                    else
                    {
                        p2 = foundationModel.BoundingLocation[1].Y;
                    }

                    break;
                case 2:
                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE") && foundationModel.ColumnModel.b > foundationModel.ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p2 = foundationModel.BoundingLocation[0].X;
                        }
                        else
                        {
                            p2 = foundationModel.BoundingLocation[1].Y;
                        }
                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p2 = foundationModel.BoundingLocation[1].X;
                        }
                        else
                        {
                            p2 = foundationModel.BoundingLocation[0].Y;
                        }
                    }
                    break;
                case 3:
                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE") && foundationModel.ColumnModel.b > foundationModel.ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p2 = foundationModel.BoundingLocation[1].X;
                        }
                        else
                        {
                            p2 = foundationModel.BoundingLocation[0].Y;
                        }
                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p2 = foundationModel.BoundingLocation[0].X;
                        }
                        else
                        {
                            p2 = foundationModel.BoundingLocation[1].Y;
                        }
                    }
                    break;
                default:
                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE") && foundationModel.ColumnModel.b > foundationModel.ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p2 = foundationModel.BoundingLocation[0].X;
                        }
                        else
                        {
                            p2 = foundationModel.BoundingLocation[0].Y;
                        }
                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p2 = foundationModel.BoundingLocation[0].X;
                        }
                        else
                        {
                            p2 = foundationModel.BoundingLocation[0].Y;
                        }
                    }
                    break;
            }
            return p2;
        }
        private double GetP3(FoundationModel foundationModel)
        {
            double p3 = 0;
            switch (SelectedFoundationBarModel.Image)
            {
                case 0:

                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE") && foundationModel.ColumnModel.b > foundationModel.ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p3 = foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].Y;
                        }
                        else
                        {
                            p3 = foundationModel.BoundingLocation[2].X;
                        }

                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p3 = foundationModel.BoundingLocation[2].Y;
                        }
                        else
                        {

                            p3 = foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].X;
                        }
                    }

                    break;
                case 1:
                    if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                    {
                        p3 = foundationModel.BoundingLocation[0].Y;
                    }
                    else
                    {
                        p3 = foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].X;
                    }
                    break;
                case 2:
                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE") && foundationModel.ColumnModel.b > foundationModel.ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p3 = foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].Y;
                        }
                        else
                        {
                            p3 = foundationModel.BoundingLocation[2].X;
                        }

                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p3 = foundationModel.BoundingLocation[2].Y;
                        }
                        else
                        {

                            p3 = foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].X;
                        }
                    }
                    break;
                case 3:
                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE") && foundationModel.ColumnModel.b > foundationModel.ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p3 = foundationModel.BoundingLocation[3].Y;

                        }
                        else
                        {
                            p3 = foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].X;
                        }
                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p3 = foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].Y;
                        }
                        else
                        {
                            p3 = foundationModel.BoundingLocation[3].X;
                        }
                    }
                    break;
                default:
                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE") && foundationModel.ColumnModel.b > foundationModel.ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p3 = foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].Y;
                        }
                        else
                        {
                            p3 = foundationModel.BoundingLocation[2].X;
                        }

                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p3 = foundationModel.BoundingLocation[2].Y;
                        }
                        else
                        {
                            p3 = foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].X;
                        }
                    }
                    break;
            }
            return p3;
        }
        private double GetP4(FoundationModel foundationModel)
        {
            double p4 = 0;
            switch (SelectedFoundationBarModel.Image)
            {
                case 0:

                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE") && foundationModel.ColumnModel.b > foundationModel.ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p4 = foundationModel.BoundingLocation[0].Y;
                        }
                        else
                        {
                            p4 = foundationModel.BoundingLocation[0].X;
                        }

                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p4 = foundationModel.BoundingLocation[0].Y;
                        }
                        else
                        {
                            p4 = foundationModel.BoundingLocation[0].X;
                        }
                    }

                    break;
                case 1:
                    if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                    {
                        p4 = foundationModel.BoundingLocation[1].Y;
                    }
                    else
                    {
                        p4 = foundationModel.BoundingLocation[0].X;
                    }
                    break;
                case 2:
                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE") && foundationModel.ColumnModel.b > foundationModel.ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p4 = foundationModel.BoundingLocation[1].Y;
                        }
                        else
                        {
                            p4 = foundationModel.BoundingLocation[0].X;
                        }

                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p4 = foundationModel.BoundingLocation[0].Y;
                        }
                        else
                        {
                            p4 = foundationModel.BoundingLocation[1].X;
                        }
                    }
                    break;
                case 3:
                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE") && foundationModel.ColumnModel.b > foundationModel.ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p4 = foundationModel.BoundingLocation[0].Y;
                        }
                        else
                        {
                            p4 = foundationModel.BoundingLocation[1].X;
                        }
                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p4 = foundationModel.BoundingLocation[1].Y;
                        }
                        else
                        {
                            p4 = foundationModel.BoundingLocation[0].X;
                        }
                    }
                    break;
                default:
                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE") && foundationModel.ColumnModel.b > foundationModel.ColumnModel.h)
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p4 = foundationModel.BoundingLocation[0].Y;
                        }
                        else
                        {
                            p4 = foundationModel.BoundingLocation[0].X;
                        }

                    }
                    else
                    {
                        if (SelectedFoundationBarModel.SpanOrientation.Equals("Horizontal"))
                        {
                            p4 = foundationModel.BoundingLocation[0].Y;
                        }
                        else
                        {
                            p4 = foundationModel.BoundingLocation[0].X;
                        }
                    }
                    break;
            }
            return p4;
        }
    }
}
