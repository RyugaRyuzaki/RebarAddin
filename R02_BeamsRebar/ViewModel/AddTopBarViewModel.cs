
using Autodesk.Revit.DB;
using R02_BeamsRebar.View;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace R02_BeamsRebar.ViewModel
{
    public class AddTopBarViewModel:BaseViewModel
    {
        #region Property
        public Document Doc;
        private BeamsModel _BeamsModel;
        public BeamsModel BeamsModel { get => _BeamsModel; set { _BeamsModel = value; OnPropertyChanged(); } }
        #endregion
        #region SelectedItem
        private LayerModel _SelectedLayerStart;
        public LayerModel SelectedLayerStart{get => _SelectedLayerStart; set{_SelectedLayerStart = value; OnPropertyChanged();} }
        private LayerModel _SelectedLayerEnd;
        public LayerModel SelectedLayerEnd{get => _SelectedLayerEnd; set{ _SelectedLayerEnd = value; OnPropertyChanged();}}
        private LayerModel _SelectedLayerMid;
        public LayerModel SelectedLayerMid{get => _SelectedLayerMid; set{_SelectedLayerMid = value; OnPropertyChanged();}}
        #endregion
        #region Node
        private ObservableCollection<int> _AllNode;
        public ObservableCollection<int> AllNode { get => _AllNode; set { _AllNode = value; OnPropertyChanged(); } }
        private int _SelectedNode;
        public int SelectedNode { get => _SelectedNode; set { _SelectedNode = value; OnPropertyChanged(); } }
        #endregion
        #region Command
        public ICommand LoadAddTopBarsViewCommand { get; set; }
        public ICommand FixedToSpanCommand { get; set; }
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
        public ICommand L1MidTextChangedCommand { get; set; }
        public ICommand L2MidTextChangedCommand { get; set; }

        public ICommand EndClickCommand { get; set; }
        public ICommand AddEndCommand { get; set; }
        public ICommand DeleteEndCommand { get; set; }
        public ICommand SelectionChangedLayerEndCommand { get; set; }
        public ICommand SelectionChangedBarEndCommand { get; set; }
        public ICommand SelectionChangedNumberEndCommand { get; set; }
        public ICommand LEndTextChangedCommand { get; set; }
        public ICommand LaEndTextChangedCommand { get; set; }
        #endregion
        public AddTopBarViewModel(Document document, BeamsModel beamsModel)
        {
            #region Get Property
            Doc = document;
            BeamsModel = beamsModel;
            GetNode();
            #endregion
            #region Load
            LoadAddTopBarsViewCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                p.canvas.Width = BeamsModel.DrawModel.Width;
                p.canvas.Height = BeamsModel.DrawModel.Height;
                AddTopBarView uc = ProcessInfoBeamRebar.FindChild<AddTopBarView>(p, "AddTopBarUC");
                if (BeamsModel.InfoModels.Count == 1)
                {
                    uc.Middle.Visibility = System.Windows.Visibility.Hidden;
                    uc.GMiddle.Background = Brushes.Gainsboro;
                }
                else
                {
                    uc.Middle.Visibility = System.Windows.Visibility.Visible;
                    uc.GMiddle.Background = Brushes.Transparent;
                    DrawImage.DrawMidAddTopBar(uc.CanvasMid);
                    uc.ListViewMid.ItemsSource = BeamsModel.AddTopBarModel.Mid[BeamsModel.SelectedIndexModel.AddTop].Model;
                }
                if (BeamsModel.InfoModels[0].ConsolLeft)
                {
                    uc.Start.Visibility = System.Windows.Visibility.Hidden;
                    uc.GStart.Background = Brushes.Gainsboro;
                }
                else
                {
                    uc.Start.Visibility = System.Windows.Visibility.Visible;
                    uc.GStart.Background = Brushes.Transparent;
                    DrawImage.DrawStartAddTopBar(uc.CanvasStart, BeamsModel.SelectedIndexModel.StartTopChecked);
                }
                if (BeamsModel.InfoModels[BeamsModel.InfoModels.Count - 1].ConsolRight)
                {
                    uc.End.Visibility = System.Windows.Visibility.Hidden;
                    uc.GEnd.Background = Brushes.Gainsboro;
                }
                else
                {
                    uc.End.Visibility = System.Windows.Visibility.Visible;
                    uc.GEnd.Background = Brushes.Transparent;
                    DrawImage.DrawEndAddTopBar(uc.CanvasEnd, BeamsModel.SelectedIndexModel.EndTopChecked);
                }
                StartEnd(p);
            });
            FixedToSpanCommand = new RelayCommand<BeamsWindow>((p) => { return ConditionFixedToSpan(); }, (p) =>
            {
                if (BeamsModel.SelectedIndexModel.StartTopChecked)
                {
                    if (BeamsModel.AddTopBarModel.Start.Model.Count != 0)
                    {
                        for (int i = 0; i < BeamsModel.AddTopBarModel.Start.Model.Count; i++)
                        {
                            BeamsModel.AddTopBarModel.Start.Model[i].L = BeamsModel.InfoModels[0].Length / 4 + BeamsModel.NodeModels[0].Width - BeamsModel.Cover;
                        }
                    }
                }
                if (BeamsModel.SelectedIndexModel.EndTopChecked)
                {
                    if (BeamsModel.AddTopBarModel.End.Model.Count != 0)
                    {
                        for (int i = 0; i < BeamsModel.AddTopBarModel.End.Model.Count; i++)
                        {
                            BeamsModel.AddTopBarModel.End.Model[i].L = BeamsModel.InfoModels[BeamsModel.InfoModels.Count-1].Length / 4 + BeamsModel.NodeModels[BeamsModel.NodeModels.Count-1].Width - BeamsModel.Cover;
                        }
                    }
                }
                if (BeamsModel.InfoModels.Count!=1)
                {
                    for (int i = 0; i < BeamsModel.AddTopBarModel.Mid.Count; i++)
                    {
                        if (BeamsModel.AddTopBarModel.Mid[i].Model.Count!=0)
                        {
                            InfoModel infoModelLeft = null;
                            InfoModel infoModelRight = null;
                            for (int j = 1; j < BeamsModel.InfoModels.Count; j++)
                            {
                                if (BeamsModel.AddTopBarModel.Mid[i].Model[0].X0>BeamsModel.InfoModels[j-1].endPosition&&BeamsModel.AddTopBarModel.Mid[i].Model[0].X0<BeamsModel.InfoModels[j].startPosition)
                                {
                                    infoModelLeft = BeamsModel.InfoModels[j - 1];
                                    infoModelRight = BeamsModel.InfoModels[j ];
                                }
                            }
                            if (infoModelLeft!=null&&infoModelRight!=null)
                            {
                                for (int j = 0; j < BeamsModel.AddTopBarModel.Mid[i].Model.Count; j++)
                                {
                                    BeamsModel.AddTopBarModel.Mid[i].Model[j].L = infoModelLeft.Length / 4;
                                    BeamsModel.AddTopBarModel.Mid[i].Model[j].La = infoModelRight.Length / 4;
                                }
                            }
                           
                        }
                    }
                }
                StartEnd(p);
            });
            #endregion
            #region Start Event
            StartClickCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                AddTopBarView uc = ProcessInfoBeamRebar.FindChild<AddTopBarView>(p, "AddTopBarUC");
                if (BeamsModel.AddTopBarModel.Start.Model.Count != 0)
                {
                    BeamsModel.AddTopBarModel.Start.Model.Clear();
                }
                DrawImage.DrawStartAddTopBar(uc.CanvasStart, BeamsModel.SelectedIndexModel.StartTopChecked);
            });
            AddStartCommand = new RelayCommand<BeamsWindow>((p) => { return AddLayerStart(p); }, (p) =>
            {
                AddTopBarView uc = ProcessInfoBeamRebar.FindChild<AddTopBarView>(p, "AddTopBarUC");
                int n = (int)(uc.NStart.SelectedValue);
                double l = double.Parse(uc.LStart.Text);
                double la = double.Parse(uc.LaStart.Text);
                if (BeamsModel.AddTopBarModel.Start.Model.Count > 0 && n < 2)
                {
                    MessageBox.Show("Error Selection", "ERROR", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    return;
                }
                BeamsModel.AddTopBarModel.Start.Model.Add(new LayerModel(BeamsModel.AddTopBarModel.Start.Model.Count + 1, BeamsModel.AllBars[uc.BarStart.SelectedIndex], n, l, la));
                StartEnd(p);
                p.scrollViewer.ScrollToLeftEnd();
                p.scrollViewer.ScrollToHorizontalOffset(((BeamsModel.AddTopBarModel.Start.Model[0].Location[0].X)) / BeamsModel.DrawModel.Scale - 10 * BeamsModel.DrawModel.Left);
            });
            DeleteStartCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerStart(); }, (p) =>
            {
                BeamsModel.AddTopBarModel.Start.DeleteItem(SelectedLayerStart.Layer - 1);
                StartEnd(p);
                p.scrollViewer.ScrollToLeftEnd();
                p.scrollViewer.ScrollToHorizontalOffset(((BeamsModel.AddTopBarModel.Start.Model[0].Location[0].X)) / BeamsModel.DrawModel.Scale - 10 * BeamsModel.DrawModel.Left);
            });
            SelectionChangedLayerStartCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerStart(); }, (p) =>
            {
                StartEnd(p);
                p.scrollViewer.ScrollToLeftEnd();
                p.scrollViewer.ScrollToHorizontalOffset(((BeamsModel.AddTopBarModel.Start.Model[0].Location[0].X)) / BeamsModel.DrawModel.Scale - 10 * BeamsModel.DrawModel.Left);
            });
            SelectionChangedBarStartCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerStart(); }, (p) =>
            {
                StartEnd(p);
                p.scrollViewer.ScrollToLeftEnd();
                p.scrollViewer.ScrollToHorizontalOffset(((BeamsModel.AddTopBarModel.Start.Model[0].Location[0].X)) / BeamsModel.DrawModel.Scale - 10 * BeamsModel.DrawModel.Left);
            });
            SelectionChangedNumberStartCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerStart(); }, (p) =>
            {
                StartEnd(p);
                p.scrollViewer.ScrollToLeftEnd();
                p.scrollViewer.ScrollToHorizontalOffset(((BeamsModel.AddTopBarModel.Start.Model[0].Location[0].X)) / BeamsModel.DrawModel.Scale - 10 * BeamsModel.DrawModel.Left);
            });
            LStartTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerStart(); }, (p) =>
            {
                AddTopBarView uc = ProcessInfoBeamRebar.FindChild<AddTopBarView>(p, "AddTopBarUC");
                if (double.TryParse(uc.LStart.Text, out double S))
                {
                    StartEnd(p);
                    p.scrollViewer.ScrollToLeftEnd();
                    p.scrollViewer.ScrollToHorizontalOffset(((BeamsModel.AddTopBarModel.Start.Model[0].Location[0].X)) / BeamsModel.DrawModel.Scale - 10 * BeamsModel.DrawModel.Left);
                }
            });
            LaStartTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerStart(); }, (p) =>
            {
                AddTopBarView uc = ProcessInfoBeamRebar.FindChild<AddTopBarView>(p, "AddTopBarUC");
                if (double.TryParse(uc.LaStart.Text, out double S))
                {
                    StartEnd(p);
                    p.scrollViewer.ScrollToLeftEnd();
                    p.scrollViewer.ScrollToHorizontalOffset(((BeamsModel.AddTopBarModel.Start.Model[0].Location[0].X)) / BeamsModel.DrawModel.Scale - 10 * BeamsModel.DrawModel.Left);
                }
            });
            #endregion
            #region End Event
            EndClickCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                AddTopBarView uc = ProcessInfoBeamRebar.FindChild<AddTopBarView>(p, "AddTopBarUC");
                if (BeamsModel.AddTopBarModel.End.Model.Count != 0)
                {
                    BeamsModel.AddTopBarModel.End.Model.Clear();
                }
                BeamsModel.SelectedIndexModel.SelectedLayerAddTopEnd = 0;
                DrawImage.DrawEndAddTopBar(uc.CanvasEnd, BeamsModel.SelectedIndexModel.EndTopChecked);
                StartEnd(p);
                p.scrollViewer.ScrollToLeftEnd();
                p.scrollViewer.ScrollToHorizontalOffset(((BeamsModel.AddTopBarModel.End.Model[0].Location[0].X)) / BeamsModel.DrawModel.Scale - 2 * BeamsModel.DrawModel.Left);
            });
            AddEndCommand = new RelayCommand<BeamsWindow>((p) => { return AddLayerEnd(p); }, (p) =>
            {
                AddTopBarView uc = ProcessInfoBeamRebar.FindChild<AddTopBarView>(p, "AddTopBarUC");
                int n = (int)(uc.NEnd.SelectedValue);
                double l = double.Parse(uc.LEnd.Text);
                double la = double.Parse(uc.LaEnd.Text);
                if (BeamsModel.AddTopBarModel.End.Model.Count > 0 && n < 2)
                {
                    MessageBox.Show("Error Selection", "ERROR", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    return;
                }
                BeamsModel.AddTopBarModel.End.Model.Add(new LayerModel(BeamsModel.AddTopBarModel.End.Model.Count + 1, BeamsModel.AllBars[uc.BarEnd.SelectedIndex], n, l, la));
                StartEnd(p);
                p.scrollViewer.ScrollToLeftEnd();
                p.scrollViewer.ScrollToHorizontalOffset(((BeamsModel.AddTopBarModel.End.Model[0].Location[0].X)) / BeamsModel.DrawModel.Scale - 10 * BeamsModel.DrawModel.Left);
            });
            DeleteEndCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerEnd(); }, (p) =>
            {
                BeamsModel.AddTopBarModel.End.DeleteItem(SelectedLayerEnd.Layer - 1);
                StartEnd(p);
                p.scrollViewer.ScrollToLeftEnd();
                p.scrollViewer.ScrollToHorizontalOffset(((BeamsModel.AddTopBarModel.End.Model[0].Location[0].X)) / BeamsModel.DrawModel.Scale - 10 * BeamsModel.DrawModel.Left);
            });
            SelectionChangedLayerEndCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerEnd(); }, (p) =>
            {
                StartEnd(p);
                p.scrollViewer.ScrollToLeftEnd();
                p.scrollViewer.ScrollToHorizontalOffset(((BeamsModel.AddTopBarModel.End.Model[0].Location[0].X)) / BeamsModel.DrawModel.Scale - 10 * BeamsModel.DrawModel.Left);
            });
            SelectionChangedBarEndCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerEnd(); }, (p) =>
            {
                StartEnd(p);
                p.scrollViewer.ScrollToLeftEnd();
                p.scrollViewer.ScrollToHorizontalOffset(((BeamsModel.AddTopBarModel.End.Model[0].Location[0].X)) / BeamsModel.DrawModel.Scale - 10 * BeamsModel.DrawModel.Left);
            });
            SelectionChangedNumberEndCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerEnd(); }, (p) =>
            {
                StartEnd(p);
                p.scrollViewer.ScrollToLeftEnd();
                p.scrollViewer.ScrollToHorizontalOffset(((BeamsModel.AddTopBarModel.End.Model[0].Location[0].X)) / BeamsModel.DrawModel.Scale - 10 * BeamsModel.DrawModel.Left);
            });
            LEndTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerEnd(); }, (p) =>
            {
                AddTopBarView uc = ProcessInfoBeamRebar.FindChild<AddTopBarView>(p, "AddTopBarUC");
                if (double.TryParse(uc.LEnd.Text, out double S))
                {
                    StartEnd(p);
                    p.scrollViewer.ScrollToLeftEnd();
                    p.scrollViewer.ScrollToHorizontalOffset(((BeamsModel.AddTopBarModel.End.Model[0].Location[0].X)) / BeamsModel.DrawModel.Scale - 10 * BeamsModel.DrawModel.Left);
                }
            });
            LaEndTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerEnd(); }, (p) =>
            {
                AddTopBarView uc = ProcessInfoBeamRebar.FindChild<AddTopBarView>(p, "AddTopBarUC");
                if (double.TryParse(uc.LaEnd.Text, out double S))
                {
                    StartEnd(p);
                    p.scrollViewer.ScrollToLeftEnd();
                    p.scrollViewer.ScrollToHorizontalOffset(((BeamsModel.AddTopBarModel.End.Model[0].Location[0].X)) / BeamsModel.DrawModel.Scale - 10 * BeamsModel.DrawModel.Left);
                }
            });
            #endregion
            #region Mid Event

            AddMidCommand = new RelayCommand<BeamsWindow>((p) => { return AddLayerMid(p); }, (p) =>
            {
                AddTopBarView uc = ProcessInfoBeamRebar.FindChild<AddTopBarView>(p, "AddTopBarUC");
                int n = (int)(uc.NMid.SelectedValue);
                double l = double.Parse(uc.L1Mid.Text);
                double la = double.Parse(uc.L2Mid.Text);
                if (BeamsModel.AddTopBarModel.Mid[BeamsModel.SelectedIndexModel.AddTop].Model.Count > 0 && n < 2)
                {
                    MessageBox.Show("Error Selection", "ERROR", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    return;
                }
                BeamsModel.AddTopBarModel.Mid[BeamsModel.SelectedIndexModel.AddTop].Model.Add(new LayerModel(BeamsModel.AddTopBarModel.Mid[BeamsModel.SelectedIndexModel.AddTop].Model.Count + 1, BeamsModel.AllBars[uc.BarMid.SelectedIndex], n, l, la));
                Mid(p);
            });
            DeleteMidCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerMid(); }, (p) =>
            {
                BeamsModel.AddTopBarModel.Mid[BeamsModel.SelectedIndexModel.AddTop].DeleteItem(SelectedLayerMid.Layer - 1);
                Mid(p);
            });
            SelectionChangedNodeMidCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {
                AddTopBarView uc = ProcessInfoBeamRebar.FindChild<AddTopBarView>(p, "AddTopBarUC");
                uc.ListViewMid.ItemsSource = null;
                uc.ListViewMid.ItemsSource = BeamsModel.AddTopBarModel.Mid[BeamsModel.SelectedIndexModel.AddTop].Model;
                BeamsModel.SelectedIndexModel.SelectedLayerAddTopMid = 0;
                Mid(p);
            });
            SelectionChangedLayerMidCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerMid(); }, (p) =>
            {
                Mid(p);
            });
            SelectionChangedBarMidCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerMid(); }, (p) =>
            {
                Mid(p);
            });
            SelectionChangedNumberMidCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerMid(); }, (p) =>
            {
                Mid(p);
            });
            L1MidTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerMid(); }, (p) =>
            {
                AddTopBarView uc = ProcessInfoBeamRebar.FindChild<AddTopBarView>(p, "AddTopBarUC");
                if (double.TryParse(uc.L1Mid.Text, out double S))
                {
                    Mid(p);
                }
            });
            L2MidTextChangedCommand = new RelayCommand<BeamsWindow>((p) => { return DeleteLayerMid(); }, (p) =>
            {
                AddTopBarView uc = ProcessInfoBeamRebar.FindChild<AddTopBarView>(p, "AddTopBarUC");
                if (double.TryParse(uc.L2Mid.Text, out double S))
                {
                    Mid(p);
                }
            });
            #endregion

        }

        #region ConditionFixedSpan
        private bool ConditionFixedToSpan()
        {
            if (BeamsModel.SelectedIndexModel.StartTopChecked)
            {
                if (BeamsModel.AddTopBarModel.Start.Model.Count == 0) return false;
            }
            if (BeamsModel.SelectedIndexModel.EndTopChecked)
            {
                if (BeamsModel.AddTopBarModel.End.Model.Count == 0) return false;
            }
            if (BeamsModel.InfoModels.Count != 1)
            {
                for (int i = 0; i < BeamsModel.AddTopBarModel.Mid.Count; i++)
                {
                    if (BeamsModel.AddTopBarModel.Mid[i].Model.Count == 0) return false;
                }
            }
            return true;
        }
        #endregion

        private void StartEnd(BeamsWindow p)
        {
            p.canvas.Children.Clear();
            DrawMainCanvas.SpanBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.NodeBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.NodeModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.SpecialNode(p.canvas, BeamsModel.InfoModels, BeamsModel.SpecialNodeModels, BeamsModel.DrawModel, 1000);
            BeamsModel.AddTopBarModel.GetLocationStart(BeamsModel.InfoModels, BeamsModel.DistributeStirrups, BeamsModel.StirrupModels, BeamsModel.NodeModels, BeamsModel.MainTopBarModel, BeamsModel.SingleMainTopBarModel, BeamsModel.SelectedIndexModel, BeamsModel.Cover, BeamsModel.SettingModel.tmin);
            BeamsModel.AddTopBarModel.GetLocationEnd(BeamsModel.InfoModels, BeamsModel.DistributeStirrups, BeamsModel.StirrupModels, BeamsModel.NodeModels, BeamsModel.MainTopBarModel, BeamsModel.SingleMainTopBarModel, BeamsModel.SelectedIndexModel, BeamsModel.Cover, BeamsModel.SettingModel.tmin);
            if (BeamsModel.InfoModels.Count == 1)
            {
                DrawMainCanvas.AddTopBarStartBeams(p.canvas, BeamsModel.AddTopBarModel, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.SelectedLayerAddTopStart, BeamsModel.SelectedIndexModel.StartTopChecked);
                DrawMainCanvas.AddTopBarEndBeams(p.canvas, BeamsModel.AddTopBarModel, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.SelectedLayerAddTopEnd, BeamsModel.SelectedIndexModel.EndTopChecked);
            }
            else
            {
                BeamsModel.AddTopBarModel.GetLocationMid(BeamsModel.InfoModels, BeamsModel.DistributeStirrups, BeamsModel.StirrupModels, BeamsModel.NodeModels, BeamsModel.MainTopBarModel, BeamsModel.SingleMainTopBarModel, BeamsModel.SelectedIndexModel, AllNode, BeamsModel.Cover, BeamsModel.SettingModel.tmin);
                DrawMainCanvas.AddTopBarStartBeams(p.canvas, BeamsModel.AddTopBarModel, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.SelectedLayerAddTopStart, BeamsModel.SelectedIndexModel.StartTopChecked);
                DrawMainCanvas.AddTopBarEndBeams(p.canvas, BeamsModel.AddTopBarModel, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.SelectedLayerAddTopEnd, BeamsModel.SelectedIndexModel.EndTopChecked);
                DrawMainCanvas.AddTopBarMidBeams(p.canvas, BeamsModel.AddTopBarModel, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.SelectedLayerAddTopMid);
            }
        }
        private void Mid(BeamsWindow p)
        {
            NodeModel nodeModel = BeamsModel.NodeModels.Where(x => x.NumberNode == AllNode[BeamsModel.SelectedIndexModel.AddTop]).FirstOrDefault();
            p.canvas.Children.Clear();
            DrawMainCanvas.SpanBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.NodeBeams(p.canvas, BeamsModel.InfoModels, BeamsModel.NodeModels, BeamsModel.DrawModel, 1000);
            DrawMainCanvas.SpecialNode(p.canvas, BeamsModel.InfoModels, BeamsModel.SpecialNodeModels, BeamsModel.DrawModel, 1000);
            BeamsModel.AddTopBarModel.GetLocationStart(BeamsModel.InfoModels, BeamsModel.DistributeStirrups, BeamsModel.StirrupModels, BeamsModel.NodeModels, BeamsModel.MainTopBarModel, BeamsModel.SingleMainTopBarModel, BeamsModel.SelectedIndexModel, BeamsModel.Cover, BeamsModel.SettingModel.tmin);
            BeamsModel.AddTopBarModel.GetLocationEnd(BeamsModel.InfoModels, BeamsModel.DistributeStirrups, BeamsModel.StirrupModels, BeamsModel.NodeModels, BeamsModel.MainTopBarModel, BeamsModel.SingleMainTopBarModel, BeamsModel.SelectedIndexModel, BeamsModel.Cover, BeamsModel.SettingModel.tmin);
            BeamsModel.AddTopBarModel.GetLocationMid(BeamsModel.InfoModels, BeamsModel.DistributeStirrups, BeamsModel.StirrupModels, BeamsModel.NodeModels, BeamsModel.MainTopBarModel, BeamsModel.SingleMainTopBarModel, BeamsModel.SelectedIndexModel, AllNode, BeamsModel.Cover, BeamsModel.SettingModel.tmin);
            DrawMainCanvas.AddTopBar(p.canvas, BeamsModel.AddTopBarModel, BeamsModel.DrawModel, BeamsModel.SelectedIndexModel.SelectedLayerAddTopStart, BeamsModel.SelectedIndexModel.SelectedLayerAddTopEnd, BeamsModel.SelectedIndexModel.AddTop, BeamsModel.SelectedIndexModel.StartTopChecked, BeamsModel.SelectedIndexModel.EndTopChecked, BeamsModel.SelectedIndexModel.SelectedLayerAddTopMid);
            p.scrollViewer.ScrollToRightEnd();
            p.scrollViewer.ScrollToHorizontalOffset(((nodeModel.Mid)) / BeamsModel.DrawModel.Scale - 5 * BeamsModel.DrawModel.Left);
        }
        #region bool Isnabled Button
        private bool AddLayerStart(BeamsWindow p)
        {
            AddTopBarView uc = ProcessInfoBeamRebar.FindChild<AddTopBarView>(p, "AddTopBarUC");
            bool bar = (uc.BarStart.SelectedIndex != -1) ? true : false;
            bool n = (uc.NStart.SelectedIndex != -1) ? true : false;
            bool l = (double.TryParse(uc.LStart.Text, out double a));
            bool la = (double.TryParse(uc.LaStart.Text, out double b));
            return bar && n && l && la && BeamsModel.SelectedIndexModel.StartTopChecked;
        }
        private bool AddLayerEnd(BeamsWindow p)
        {
            AddTopBarView uc = ProcessInfoBeamRebar.FindChild<AddTopBarView>(p, "AddTopBarUC");
            bool bar = (uc.BarEnd.SelectedIndex != -1) ? true : false;
            bool n = (uc.NEnd.SelectedIndex != -1) ? true : false;
            bool l = (double.TryParse(uc.LEnd.Text, out double a));
            bool la = (double.TryParse(uc.LaEnd.Text, out double b));
            return bar && n && l && la && BeamsModel.SelectedIndexModel.EndTopChecked;
        }
        private bool AddLayerMid(BeamsWindow p)
        {
            AddTopBarView uc = ProcessInfoBeamRebar.FindChild<AddTopBarView>(p, "AddTopBarUC");
            bool bar = (uc.BarMid.SelectedIndex != -1) ? true : false;
            bool n = (uc.NMid.SelectedIndex != -1) ? true : false;
            bool l1 = (double.TryParse(uc.L1Mid.Text, out double a));
            bool l2 = (double.TryParse(uc.L2Mid.Text, out double b));
            return bar && n && l1 && l2;
        }

        private bool DeleteLayerStart()
        {
            return (BeamsModel.SelectedIndexModel.StartTopChecked && BeamsModel.AddTopBarModel.Start.Model.Count != 0 && SelectedLayerStart != null);
        }
        private bool DeleteLayerEnd()
        {
            return (BeamsModel.SelectedIndexModel.EndTopChecked && BeamsModel.AddTopBarModel.End.Model.Count != 0 && SelectedLayerEnd != null);
        }
        private bool DeleteLayerMid()
        {
            return (BeamsModel.AddTopBarModel.Mid[BeamsModel.SelectedIndexModel.AddTop].Model.Count != 0 && SelectedLayerMid != null);
        }
        private void GetNode()
        {
            if (BeamsModel.AddTopBarModel.Mid != null)
            {
                AllNode = new ObservableCollection<int>();
                if (BeamsModel.InfoModels[0].ConsolLeft)
                {
                    for (int i = 0; i < BeamsModel.AddTopBarModel.Mid.Count; i++)
                    {
                        AllNode.Add(BeamsModel.NodeModels[i].NumberNode);
                    }
                }
                else
                {
                    for (int i = 0; i < BeamsModel.AddTopBarModel.Mid.Count; i++)
                    {
                        AllNode.Add(BeamsModel.NodeModels[i + 1].NumberNode);
                    }
                }
            }
        }
        #endregion
        #region Draw
        //private void DrawAddTopBar(APIWindow p, int nodeNumber)
        //{
        //    p.canvas.Children.Clear();
        //    DrawMainCanvas.SpanBeams(p.canvas, AllBeamsModel, DrawModel, 1000);
        //    DrawMainCanvas.NodeBeams(p.canvas, AllBeamsModel, AllNodeModel, DrawModel, nodeNumber);
        //    DrawMainCanvas.SpecialNode(p.canvas, AllBeamsModel, SpecialNodeModel, DrawModel, 1000);
        //    AddTopBarModel.GetLocationStart(AllBeamsModel, AllNodeModel, MainTopBarModel, SingleMainTopBarModel, SelectedIndexModel, DisplayRebarCover, SettingModel.tmin);
        //    AddTopBarModel.GetLocationEnd(AllBeamsModel, AllNodeModel, MainTopBarModel, SingleMainTopBarModel, SelectedIndexModel, DisplayRebarCover, SettingModel.tmin);
        //    AddTopBarModel.GetLocationMid(AllBeamsModel, AllNodeModel, MainTopBarModel, SingleMainTopBarModel, SelectedIndexModel, AllNode, DisplayRebarCover, SettingModel.tmin);
        //    if (AllBeamsModel.Count == 1)
        //    {
        //        DrawMainCanvas.AddTopBarStartBeams(p.canvas, AddTopBarModel, DrawModel, SelectedIndexModel.SelectedLayerAddTopStart, SelectedIndexModel.StartTopChecked);
        //        DrawMainCanvas.AddTopBarEndBeams(p.canvas, AddTopBarModel, DrawModel, SelectedIndexModel.SelectedLayerAddTopEnd, SelectedIndexModel.EndTopChecked);
        //    }
        //    else
        //    {
        //        DrawMainCanvas.AddTopBarStartBeams(p.canvas, AddTopBarModel, DrawModel, SelectedIndexModel.SelectedLayerAddTopStart, SelectedIndexModel.StartTopChecked);
        //        DrawMainCanvas.AddTopBarEndBeams(p.canvas, AddTopBarModel, DrawModel, SelectedIndexModel.SelectedLayerAddTopEnd, SelectedIndexModel.EndTopChecked);
        //        DrawMainCanvas.AddTopBarMidBeams(p.canvas, AddTopBarModel, DrawModel, SelectedIndexModel.SelectedLayerAddTopMid);
        //    }
        //    p.scrollViewer.ScrollToLeftEnd();
        //}
        #endregion
    }
}
