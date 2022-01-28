
using Autodesk.Revit.DB;
using R02_BeamsRebar.View;
using System;
using System.Windows.Input;

namespace R02_BeamsRebar.ViewModel
{
    public class AddBottomBarViewModel:BaseViewModel
    {
        #region Property
        public Document Doc;
        private BeamsModel _BeamsModel;
        public BeamsModel BeamsModel { get => _BeamsModel; set { _BeamsModel = value; OnPropertyChanged(); } }
        #endregion
        #region SelectedBottomBarModel
        private SelectedBottomModel _BottomModels;
        public SelectedBottomModel BottomModels { get => _BottomModels; set { _BottomModels = value; OnPropertyChanged(); } }
        #endregion
        #region SelectedLayer
        private LayerModel _SelectedLayerStart;
        public LayerModel SelectedLayerStart { get => _SelectedLayerStart; set { _SelectedLayerStart = value; OnPropertyChanged(); } }
        private LayerModel _SelectedLayerEnd;
        public LayerModel SelectedLayerEnd { get => _SelectedLayerEnd; set { _SelectedLayerEnd = value; OnPropertyChanged(); } }
        private LayerModel _SelectedLayerMid;
        public LayerModel SelectedLayerMid { get => _SelectedLayerMid; set { _SelectedLayerMid = value; OnPropertyChanged(); } }
        #endregion
        #region Command
        public ICommand LoadAddBottomBarsViewCommand { get; set; }
        public ICommand FixedToSpanCommand { get; set; }
        public ICommand SelectionChangedSpanCommand { get; set; }

        public ICommand StartClickCommand { get; set; }
        public ICommand AddStartCommand { get; set; }
        public ICommand DeleteStartCommand { get; set; }
        public ICommand SelectionChangedLayerStartCommand { get; set; }
        public ICommand SelectionChangedBarStartCommand { get; set; }
        public ICommand SelectionChangedNumberStartCommand { get; set; }
        public ICommand LStartTextChangedCommand { get; set; }
        public ICommand LaStartTextChangedCommand { get; set; }

        public ICommand AddMidCommand { get; set; }
        public ICommand DeleteMidCommand { get; set; }
        public ICommand SelectionChangedNodeMidCommand { get; set; }
        public ICommand SelectionChangedLayerMidCommand { get; set; }
        public ICommand SelectionChangedBarMidCommand { get; set; }
        public ICommand SelectionChangedNumberMidCommand { get; set; }
        public ICommand LMidTextChangedCommand { get; set; }
        public ICommand LaMidTextChangedCommand { get; set; }

        public ICommand EndClickCommand { get; set; }
        public ICommand AddEndCommand { get; set; }
        public ICommand DeleteEndCommand { get; set; }
        public ICommand SelectionChangedLayerEndCommand { get; set; }
        public ICommand SelectionChangedBarEndCommand { get; set; }
        public ICommand SelectionChangedNumberEndCommand { get; set; }
        public ICommand LEndTextChangedCommand { get; set; }
        public ICommand LaEndTextChangedCommand { get; set; }
        #endregion
        public AddBottomBarViewModel(Document document, BeamsModel beamsModel)
        {
            #region Get Property
            Doc = document;
            BeamsModel = beamsModel;
            #endregion
            #region Load
            LoadAddBottomBarsViewCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                p.canvas.Width = BeamsModel.DrawModel.Width;
                p.canvas.Height = BeamsModel.DrawModel.Height;
                AddBottomBarView uc = ProcessInfoBeamRebar.FindChild<AddBottomBarView>(p, "AddBottomBarUC");
                DrawImage.DrawMidAddBottomBar(uc.CanvasMid);
                DrawImage.DrawStartAddBottomBar(uc.CanvasStart, BottomModels.StartBottomChecked);
                DrawImage.DrawEndAddBottomBar(uc.CanvasEnd, BottomModels.EndBottomChecked);
                uc.ListViewStart.ItemsSource = BeamsModel.AddBottomBarModel[BeamsModel.SelectedIndexModel.Span].Start.Model;
                uc.ListViewMid.ItemsSource = BeamsModel.AddBottomBarModel[BeamsModel.SelectedIndexModel.Span].Mid.Model;
                uc.ListViewEnd.ItemsSource = BeamsModel.AddBottomBarModel[BeamsModel.SelectedIndexModel.Span].End.Model;
                DrawAddBottomBar(p);
            });
            SelectionChangedSpanCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                AddBottomBarView uc = ProcessInfoBeamRebar.FindChild<AddBottomBarView>(p, "AddBottomBarUC");
                uc.ListViewStart.ItemsSource = null;
                uc.ListViewMid.ItemsSource = null;
                uc.ListViewEnd.ItemsSource = null;
                DrawImage.DrawMidAddBottomBar(uc.CanvasMid);
                DrawImage.DrawStartAddBottomBar(uc.CanvasStart, BottomModels.StartBottomChecked);
                DrawImage.DrawEndAddBottomBar(uc.CanvasEnd, BottomModels.EndBottomChecked);
                uc.ListViewStart.ItemsSource = BeamsModel.AddBottomBarModel[BeamsModel.SelectedIndexModel.Span].Start.Model;
                uc.ListViewMid.ItemsSource = BeamsModel.AddBottomBarModel[BeamsModel.SelectedIndexModel.Span].Mid.Model;
                uc.ListViewEnd.ItemsSource = BeamsModel.AddBottomBarModel[BeamsModel.SelectedIndexModel.Span].End.Model;
                DrawAddBottomBar(p);
            });
            FixedToSpanCommand = new RelayCommand<BeamsWindow>((p) => { return ConditionFixedToSpan(); }, (p) =>
            {
                
                if (BeamsModel.AddBottomBarModel[BottomModels.Span - 1].Mid.Model.Count != 0)
                {
                    for (int i = 0; i < BeamsModel.AddBottomBarModel[BottomModels.Span - 1].Mid.Model.Count; i++)
                    {
                        BeamsModel.AddBottomBarModel[BottomModels.Span - 1].Mid.Model[i].L = BeamsModel.InfoModels[BottomModels.Span - 1].Length / 4 + BeamsModel.InfoModels[BottomModels.Span - 1].h;
                        BeamsModel.AddBottomBarModel[BottomModels.Span - 1].Mid.Model[i].La = BeamsModel.InfoModels[BottomModels.Span - 1].Length / 4 + BeamsModel.InfoModels[BottomModels.Span - 1].h;
                    }
                    DrawAddBottomBar(p);
                }

            });
            #endregion
            #region Start Event
            StartClickCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                AddBottomBarView uc = ProcessInfoBeamRebar.FindChild<AddBottomBarView>(p, "AddBottomBarUC");
                if (BeamsModel.AddBottomBarModel[BeamsModel.SelectedIndexModel.Span].Start.Model.Count != 0)
                {
                    BeamsModel.AddBottomBarModel[BeamsModel.SelectedIndexModel.Span].Start.Model.Clear();
                }
                DrawImage.DrawStartAddBottomBar(uc.CanvasStart, BottomModels.StartBottomChecked);
                DrawAddBottomBar(p);
            });
            AddStartCommand = new RelayCommand<BeamsWindow>((p) => { return AddLayerStart(p); }, (p) =>
            {
                AddBottomBarView uc = ProcessInfoBeamRebar.FindChild<AddBottomBarView>(p, "AddBottomBarUC");
                int n = (int)(uc.NStart.SelectedValue);
                double l = double.Parse(uc.LStart.Text);
                double la = double.Parse(uc.LaStart.Text);
                BeamsModel.AddBottomBarModel[BeamsModel.SelectedIndexModel.Span].Start.Model.Add(new LayerModel(BeamsModel.AddBottomBarModel[BeamsModel.SelectedIndexModel.Span].Start.Model.Count + 1, BeamsModel.AllBars[uc.BarStart.SelectedIndex], n, l, la));
                DrawAddBottomBar(p);
            });
            DeleteStartCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerStart(); }, (p) =>
            {
                BeamsModel.AddBottomBarModel[BeamsModel.SelectedIndexModel.Span].Start.DeleteItem(SelectedLayerStart.Layer - 1);
                DrawAddBottomBar(p);
            });
            SelectionChangedLayerStartCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerStart(); }, (p) =>{DrawAddBottomBar(p);});
            SelectionChangedBarStartCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerStart(); }, (p) =>{DrawAddBottomBar(p);});
            SelectionChangedNumberStartCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerStart(); }, (p) =>{DrawAddBottomBar(p);});
            LStartTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerStart(); }, (p) =>{DrawAddBottomBar(p);});
            LaStartTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerStart(); }, (p) =>{DrawAddBottomBar(p);});
            #endregion
            #region End Event
            EndClickCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                AddBottomBarView uc = ProcessInfoBeamRebar.FindChild<AddBottomBarView>(p, "AddBottomBarUC");
                if (BeamsModel.AddBottomBarModel[BeamsModel.SelectedIndexModel.Span].End.Model.Count != 0)
                {
                    BeamsModel.AddBottomBarModel[BeamsModel.SelectedIndexModel.Span].End.Model.Clear();
                }
                DrawImage.DrawEndAddBottomBar(uc.CanvasEnd, BottomModels.EndBottomChecked);
                DrawAddBottomBar(p);
            });
            AddEndCommand = new RelayCommand<BeamsWindow>((p) => { return AddLayerEnd(p); }, (p) =>
            {
                AddBottomBarView uc = ProcessInfoBeamRebar.FindChild<AddBottomBarView>(p, "AddBottomBarUC");
                int n = (int)(uc.NEnd.SelectedValue);
                double l = double.Parse(uc.LEnd.Text);
                double la = double.Parse(uc.LaEnd.Text);
                BeamsModel.AddBottomBarModel[BeamsModel.SelectedIndexModel.Span].End.Model.Add(new LayerModel(BeamsModel.AddBottomBarModel[BeamsModel.SelectedIndexModel.Span].End.Model.Count + 1, BeamsModel.AllBars[uc.BarEnd.SelectedIndex], n, l, la));
                DrawAddBottomBar(p);
            });
            DeleteEndCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerEnd(); }, (p) =>
            {
                BeamsModel.AddBottomBarModel[BeamsModel.SelectedIndexModel.Span].Start.DeleteItem(SelectedLayerEnd.Layer - 1);
                DrawAddBottomBar(p);
            });
            SelectionChangedLayerEndCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerEnd(); }, (p) =>
            {
                DrawAddBottomBar(p);
            });
            SelectionChangedBarEndCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerEnd(); }, (p) =>
            {
                DrawAddBottomBar(p);
            });
            SelectionChangedNumberEndCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerEnd(); }, (p) =>
            {
                DrawAddBottomBar(p);
            });
            LEndTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerEnd(); }, (p) =>
            {
                DrawAddBottomBar(p);
            });
            LaEndTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerEnd(); }, (p) =>
            {
                DrawAddBottomBar(p);
            });
            #endregion
            #region Mid Event

            AddMidCommand = new RelayCommand<BeamsWindow>((p) => { return AddLayerMid(p); }, (p) =>
            {
                AddBottomBarView uc = ProcessInfoBeamRebar.FindChild<AddBottomBarView>(p, "AddBottomBarUC");
                int n = (int)(uc.NMid.SelectedValue);
                double l = double.Parse(uc.LMid.Text);
                double la = double.Parse(uc.LaMid.Text);
                BeamsModel.AddBottomBarModel[BeamsModel.SelectedIndexModel.Span].Mid.Model.Add(new LayerModel(BeamsModel.AddBottomBarModel[BeamsModel.SelectedIndexModel.Span].Mid.Model.Count + 1, BeamsModel.AllBars[uc.BarMid.SelectedIndex], n, l, la));
                DrawAddBottomBar(p);
            });
            DeleteMidCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerMid(); }, (p) =>
            {
                BeamsModel.AddBottomBarModel[BeamsModel.SelectedIndexModel.Span].Mid.DeleteItem(SelectedLayerMid.Layer - 1);
                DrawAddBottomBar(p);
            });
            SelectionChangedLayerMidCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerMid(); }, (p) =>
            {
                DrawAddBottomBar(p);
            });
            SelectionChangedBarMidCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerMid(); }, (p) =>
            {
                DrawAddBottomBar(p);
            });
            SelectionChangedNumberMidCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerMid(); }, (p) =>
            {
                DrawAddBottomBar(p);
            });
            LMidTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerMid(); }, (p) =>
            {
                DrawAddBottomBar(p);
            });
            LaMidTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerMid(); }, (p) =>
            {
                DrawAddBottomBar(p);
            });
            #endregion
        }
        #region ConditionFixedSpan
        private bool ConditionFixedToSpan()
        {
            
            if (BeamsModel.AddBottomBarModel[BottomModels.Span - 1].Mid.Model.Count == 0) return false;
            return true;
        }
        #endregion
        private void DrawAddBottomBar(BeamsWindow p)
        {
            p.canvas.Children.Clear();
            DrawMainCanvas.SpanBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.Span);
            DrawMainCanvas.NodeBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.NodeModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.SpecialNode(p.canvas, BeamsModel.InfoModels, BeamsModel.SpecialNodeModels, BeamsModel.DrawModel, 1000);
            GetLocation();
            DrawMainCanvas.AddBottomBar(p.canvas, BeamsModel.AddBottomBarModel, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.Span, BeamsModel.SelectedBottomModels);
            p.scrollViewer.ScrollToRightEnd();
            p.scrollViewer.ScrollToHorizontalOffset(((BeamsModel.InfoModels[BeamsModel.SelectedIndexModel.Span].startPosition / 2 + BeamsModel.InfoModels[BeamsModel.SelectedIndexModel.Span].endPosition / 2) / BeamsModel.DrawModel.Scale - 10 * BeamsModel.DrawModel.Left));
        }

        private void GetLocation()
        {
            double dsmax = ProcessInfoBeamRebar.GetDiameterStirrupMax(BeamsModel.InfoModels, BeamsModel.DistributeStirrups, BeamsModel.StirrupModels);
            double dMainBottom = GetDMain();

            if (BeamsModel.InfoModels.Count > BeamsModel.NodeModels.Count)
            {
                for (int i = 0; i < BeamsModel.AddBottomBarModel.Count; i++)
                {
                    if (i == 0)
                    {
                        BeamsModel.AddBottomBarModel[i].GetLocationStart(BeamsModel.InfoModels[i], BeamsModel.InfoModels[0].startPosition, BeamsModel.Cover, dsmax, dMainBottom, BeamsModel.SettingModel.tmin);
                        BeamsModel.AddBottomBarModel[i].GetLocationEnd(BeamsModel.InfoModels[i], BeamsModel.NodeModels[i].End, BeamsModel.Cover, dsmax, dMainBottom, BeamsModel.SettingModel.tmin);
                    }
                    else
                    {
                        if (i == BeamsModel.AddBottomBarModel.Count - 1)
                        {
                            BeamsModel.AddBottomBarModel[i].GetLocationEnd(BeamsModel.InfoModels[i], BeamsModel.InfoModels[BeamsModel.InfoModels.Count - 1].endPosition, BeamsModel.Cover, dsmax, dMainBottom, BeamsModel.SettingModel.tmin);
                        }
                        else
                        {
                            BeamsModel.AddBottomBarModel[i].GetLocationEnd(BeamsModel.InfoModels[i], BeamsModel.NodeModels[i].End, BeamsModel.Cover, dsmax, dMainBottom, BeamsModel.SettingModel.tmin);
                        }
                        BeamsModel.AddBottomBarModel[i].GetLocationStart(BeamsModel.InfoModels[i], BeamsModel.NodeModels[i - 1].Start, BeamsModel.Cover, dsmax, dMainBottom, BeamsModel.SettingModel.tmin);
                    }
                }
            }
            else
            {
                if (BeamsModel.InfoModels.Count == BeamsModel.NodeModels.Count)
                {
                    if (BeamsModel.InfoModels[0].ConsolLeft)
                    {
                        for (int i = 0; i < BeamsModel.AddBottomBarModel.Count; i++)
                        {
                            if (i == 0)
                            {
                                BeamsModel.AddBottomBarModel[i].GetLocationStart(BeamsModel.InfoModels[i], BeamsModel.InfoModels[0].startPosition, BeamsModel.Cover, dsmax, dMainBottom, BeamsModel.SettingModel.tmin);
                                BeamsModel.AddBottomBarModel[i].GetLocationEnd(BeamsModel.InfoModels[i], BeamsModel.NodeModels[i].End, BeamsModel.Cover, dsmax, dMainBottom, BeamsModel.SettingModel.tmin);
                            }
                            else
                            {
                                BeamsModel.AddBottomBarModel[i].GetLocationStart(BeamsModel.InfoModels[i], BeamsModel.NodeModels[i - 1].Start, BeamsModel.Cover, dsmax, dMainBottom, BeamsModel.SettingModel.tmin);
                                BeamsModel.AddBottomBarModel[i].GetLocationEnd(BeamsModel.InfoModels[i], BeamsModel.NodeModels[i].End, BeamsModel.Cover, dsmax, dMainBottom, BeamsModel.SettingModel.tmin);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < BeamsModel.AddBottomBarModel.Count; i++)
                        {
                            if (i == BeamsModel.AddBottomBarModel.Count - 1)
                            {
                                BeamsModel.AddBottomBarModel[i].GetLocationStart(BeamsModel.InfoModels[i], BeamsModel.NodeModels[i].Start, BeamsModel.Cover, dsmax, dMainBottom, BeamsModel.SettingModel.tmin);
                                BeamsModel.AddBottomBarModel[i].GetLocationEnd(BeamsModel.InfoModels[i], BeamsModel.InfoModels[BeamsModel.InfoModels.Count - 1].endPosition, BeamsModel.Cover, dsmax, dMainBottom, BeamsModel.SettingModel.tmin);
                            }
                            else
                            {
                                BeamsModel.AddBottomBarModel[i].GetLocationStart(BeamsModel.InfoModels[i], BeamsModel.NodeModels[i].Start, BeamsModel.Cover, dsmax, dMainBottom, BeamsModel.SettingModel.tmin);
                                BeamsModel.AddBottomBarModel[i].GetLocationEnd(BeamsModel.InfoModels[i], BeamsModel.NodeModels[i + 1].End, BeamsModel.Cover, dsmax, dMainBottom, BeamsModel.SettingModel.tmin);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < BeamsModel.AddBottomBarModel.Count; i++)
                    {
                        BeamsModel.AddBottomBarModel[i].GetLocationStart(BeamsModel.InfoModels[i], BeamsModel.NodeModels[i].Start, BeamsModel.Cover, dsmax, dMainBottom, BeamsModel.SettingModel.tmin);
                        BeamsModel.AddBottomBarModel[i].GetLocationEnd(BeamsModel.InfoModels[i], BeamsModel.NodeModels[i + 1].End, BeamsModel.Cover, dsmax, dMainBottom, BeamsModel.SettingModel.tmin);
                    }
                }
            }
            for (int i = 0; i < BeamsModel.AddBottomBarModel.Count; i++)
            {
                BeamsModel.AddBottomBarModel[i].GetLocationMid(BeamsModel.InfoModels[i], BeamsModel.Cover, dsmax, dMainBottom, BeamsModel.SettingModel.tmin);
            }
        }
        private double GetDMain()
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
        #region bool Isnabled Button
        private bool AddLayerStart(BeamsWindow p)
        {
            AddBottomBarView uc = ProcessInfoBeamRebar.FindChild<AddBottomBarView>(p, "AddBottomBarUC");
            bool bar = (uc.BarStart.SelectedIndex != -1) ? true : false;
            bool n = (uc.NStart.SelectedIndex != -1) ? true : false;
            bool l = (double.TryParse(uc.LStart.Text, out double a));
            bool la = (double.TryParse(uc.LaStart.Text, out double b));
            return bar && n && l && la && BottomModels.StartBottomChecked;
        }
        private bool AddLayerEnd(BeamsWindow p)
        {
            AddBottomBarView uc = ProcessInfoBeamRebar.FindChild<AddBottomBarView>(p, "AddBottomBarUC");
            bool bar = (uc.BarEnd.SelectedIndex != -1) ? true : false;
            bool n = (uc.NEnd.SelectedIndex != -1) ? true : false;
            bool l = (double.TryParse(uc.LEnd.Text, out double a));
            bool la = (double.TryParse(uc.LaEnd.Text, out double b));
            return bar && n && l && la && BottomModels.EndBottomChecked;
        }
        private bool AddLayerMid(BeamsWindow p)
        {
            AddBottomBarView uc = ProcessInfoBeamRebar.FindChild<AddBottomBarView>(p, "AddBottomBarUC");
            bool bar = (uc.BarMid.SelectedIndex != -1) ? true : false;
            bool n = (uc.NMid.SelectedIndex != -1) ? true : false;
            bool l1 = (double.TryParse(uc.LMid.Text, out double a));
            bool l2 = (double.TryParse(uc.LaMid.Text, out double b));
            return bar && n && l1 && l2;
        }

        private bool DeleteLayerStart()
        {
            return (BottomModels.StartBottomChecked && BeamsModel.AddBottomBarModel[BeamsModel.SelectedIndexModel.Span].Start.Model.Count != 0 && SelectedLayerStart != null);
        }
        private bool DeleteLayerEnd()
        {
            return (BottomModels.EndBottomChecked && BeamsModel.AddBottomBarModel[BeamsModel.SelectedIndexModel.Span].End.Model.Count != 0 && SelectedLayerEnd != null);
        }
        private bool DeleteLayerMid()
        {
            return (BeamsModel.AddBottomBarModel[BeamsModel.SelectedIndexModel.Span].Mid.Model.Count != 0 && SelectedLayerMid != null);
        }
        #endregion
    }
}
