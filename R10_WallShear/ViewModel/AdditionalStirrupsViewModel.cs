using Autodesk.Revit.DB;
using R10_WallShear.View;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace R10_WallShear.ViewModel
{
    public class AdditionalStirrupsViewModel : BaseViewModel
    {
        #region property
        public Document Doc;
        private WallsModel _WallsModel;
        public WallsModel WallsModel { get => _WallsModel; set { _WallsModel = value; OnPropertyChanged(); } }
        #endregion
        #region Selected
        private StirrupModel _SelectedWall;
        public StirrupModel SelectedWall { get => _SelectedWall; set { _SelectedWall = value; OnPropertyChanged(); } }
        #endregion
        #region Distribute Type
        private List<int> _NumberAdditional;
        public List<int> NumberAdditional { get { if (_NumberAdditional == null) { _NumberAdditional = new List<int>() { 1, 2, 3, 4, 5, 6 }; } return _NumberAdditional; } set { _NumberAdditional = value; OnPropertyChanged(); } }
       
        #endregion
        #region Icommand
        public ICommand LoadAdditionalStirrupViewCommand { get; set; }
        public ICommand SelectionAdditionalStirrupsChangedCommand { get; set; }
        public ICommand HorizontalCheckedCommand { get; set; }
        public ICommand BarHorizontalChangedCommand { get; set; }
        public ICommand HorizontalDistanceTextChangedCommand { get; set; }
        public ICommand TypeHorizontalChangedCommand { get; set; }
        public ICommand NumberBarHorizontalChangedCommand { get; set; }
        public ICommand HorizontalaTextChangedCommand { get; set; }
        public ICommand FixedMainBarCommand { get; set; }

        public ICommand HorizontalCornerCheckedCommand { get; set; }
        public ICommand BarCornerHorizontalChangedCommand { get; set; }
        public ICommand NumberBarCornerHorizontalChangedCommand { get; set; }
        public ICommand HorizontalCorneraTextChangedCommand { get; set; }
        public ICommand TypeHorizontalCornerChangedCommand { get; set; }

        public ICommand VerticalCornerCheckedCommand { get; set; }
        public ICommand BarVerticalCornerChangedCommand { get; set; }
        public ICommand NumberBarVerticalCornerChangedCommand { get; set; }
        public ICommand VerticalaCornerTextChangedCommand { get; set; }
        public ICommand TypeVerticalCornerChangedCommand { get; set; }
        #endregion
        public AdditionalStirrupsViewModel(Document document, WallsModel wallsModel)
        {
            #region Property
            Doc = document;
            WallsModel = wallsModel;
            #endregion
            #region Load
            LoadAdditionalStirrupViewCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                AdditionalStirrupsView uc = ProccessInfoWalls.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                ShowCorner(uc);
                ShowHorizontal(uc);
                ShowTypeHorizontalCorner(uc);
                ShowTypeVerticalCorner(uc);
                DrawCanVasAddHorizontalMain(uc);
                DrawCanVasAddCorner(uc);
                DrawInfo(p);
                DrawSection(p);
                if (WallsModel.BarMainModels[WallsModel.SelectedIndexModel.SelectedWall].BarModels.Count!=0) { FixedMainBarAdditionalHorizontal(); }
            });
            SelectionAdditionalStirrupsChangedCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                AdditionalStirrupsView uc = ProccessInfoWalls.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                ShowCorner(uc);
                ShowHorizontal(uc);
                ShowTypeHorizontalCorner(uc);
                ShowTypeVerticalCorner(uc);
                DrawInfo(p);
                DrawSection(p);
            });
            HorizontalCheckedCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                AdditionalStirrupsView uc = ProccessInfoWalls.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                ShowHorizontal(uc);
                DrawSection(p);
            });
            BarHorizontalChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedWall.AddH; }, (p) =>
            {
                AdditionalStirrupsView uc = ProccessInfoWalls.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                DrawSection(p);
            });
            HorizontalDistanceTextChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedWall.AddH && SelectedWall.TypeH != 0; }, (p) =>
                {
                    AdditionalStirrupsView uc = ProccessInfoWalls.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                    DrawSection(p);
                });
            TypeHorizontalChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedWall.AddH; }, (p) =>
           {
               AdditionalStirrupsView uc = ProccessInfoWalls.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
               ShowHorizontal(uc);
               DrawSection(p);
           });
            //NumberBarHorizontalChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedWall.AddH && SelectedWall.TypeH == 0; }, (p) =>
            //{
            //    AdditionalStirrupsView uc = ProccessInfoWalls.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
            //    DrawSection(p);
            //});
            HorizontalaTextChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedWall.AddH && SelectedWall.TypeH == 0; }, (p) =>
            {
                AdditionalStirrupsView uc = ProccessInfoWalls.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                DrawSection(p);
            });
            FixedMainBarCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedWall.AddH && ConditionFixedMainBar(); }, (p) =>
             {
                 AdditionalStirrupsView uc = ProccessInfoWalls.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                 FixedMainBarAdditionalHorizontal();
                 DrawSection(p);
             });
            #endregion
            #region Horizontal Corner
            HorizontalCornerCheckedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedWall.IsCorner; }, (p) =>
            {
                AdditionalStirrupsView uc = ProccessInfoWalls.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                ShowTypeHorizontalCorner(uc);
                DrawSection(p);
            });
            BarCornerHorizontalChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedWall.AddHCorner; }, (p) =>
            {
                AdditionalStirrupsView uc = ProccessInfoWalls.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                DrawSection(p);
            });
            NumberBarCornerHorizontalChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedWall.AddHCorner && SelectedWall.TypeHCorner != 0; }, (p) =>
                {
                    AdditionalStirrupsView uc = ProccessInfoWalls.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                    DrawSection(p);
                });
            HorizontalCorneraTextChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedWall.AddHCorner && SelectedWall.TypeHCorner == 0; }, (p) =>
            {
                AdditionalStirrupsView uc = ProccessInfoWalls.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                DrawSection(p);
            });
            TypeHorizontalCornerChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedWall.AddHCorner; }, (p) =>
            {
                AdditionalStirrupsView uc = ProccessInfoWalls.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                ShowTypeHorizontalCorner(uc);
                DrawSection(p);
            });
            #endregion
            #region Verticcal Corner
            VerticalCornerCheckedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedWall.IsCorner; }, (p) =>
            {
                AdditionalStirrupsView uc = ProccessInfoWalls.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                ShowTypeVerticalCorner(uc);
                DrawSection(p);
            });
            BarVerticalCornerChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedWall.AddVCorner; }, (p) =>
            {
                AdditionalStirrupsView uc = ProccessInfoWalls.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                DrawSection(p);
            });
            NumberBarVerticalCornerChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedWall.AddVCorner && SelectedWall.TypeVCorner != 0; }, (p) =>
                {
                    AdditionalStirrupsView uc = ProccessInfoWalls.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                    DrawSection(p);
                });
            VerticalaCornerTextChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedWall.AddVCorner && SelectedWall.TypeVCorner == 0; }, (p) =>
            {
                AdditionalStirrupsView uc = ProccessInfoWalls.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                DrawSection(p);
            });
            TypeVerticalCornerChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedWall.AddVCorner; }, (p) =>
            {
                AdditionalStirrupsView uc = ProccessInfoWalls.FindChild<AdditionalStirrupsView>(p, "AdditionalUC");
                ShowTypeVerticalCorner(uc);
                DrawSection(p);
            });
            #endregion
        }
        #region Method
        private void FixedMainBarAdditionalHorizontal()
        {
            int a = WallsModel.BarMainModels[WallsModel.SelectedIndexModel.SelectedWall].nx;
            double d = WallsModel.BarMainModels[WallsModel.SelectedIndexModel.SelectedWall].Bar.Diameter;
            double L = (SelectedWall.IsCorner) ? WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall].L2 : WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall].L;

            if (SelectedWall.TypeH == 0)
            {
                SelectedWall.DistanceH = Math.Round( 2 * (L - 2 * WallsModel.Cover - 2 * SelectedWall.BarS.Diameter - d) / (a - 1),3);
                SelectedWall.aH = Math.Round((L - 2 * WallsModel.Cover - 2 * SelectedWall.BarS.Diameter - d) / (a - 1)+d + 2* SelectedWall.BarH.Diameter, 3);
                
            }
            else
            {
                SelectedWall.DistanceH = Math.Round((L - 2 * WallsModel.Cover - 2 * SelectedWall.BarS.Diameter - d) / (a - 1),3);
                SelectedWall.aH = WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall].T / 2;
            }
        }
        private bool ConditionFixedMainBar()
        {
            if (WallsModel.BarMainModels[WallsModel.SelectedIndexModel.SelectedWall].BarModels.Count == 0)
            {
                return false;
            }
            else
            {
                if (SelectedWall.TypeH == 0)
                {
                    if (SelectedWall.aH < 0 && SelectedWall.aH > WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall].T)
                    {
                        return false;
                    }
                }
                else
                {
                    if (SelectedWall.DistanceH < 0 && SelectedWall.DistanceH > WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall].T)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
       
        private void ShowHorizontal(AdditionalStirrupsView p)
        {
            if (SelectedWall.AddH)
            {
                p.DistanceTextBlock.Visibility = System.Windows.Visibility.Visible;
                p.DistanceTextBox.Visibility = System.Windows.Visibility.Visible;
                p.DistanceTextBlockUnit.Visibility = System.Windows.Visibility.Visible;
                if (SelectedWall.TypeH == 0)
                {
                    p.aHTextBlocka.Visibility = System.Windows.Visibility.Visible;
                    p.aHTextBlockUnit.Visibility = System.Windows.Visibility.Visible;
                    p.aHTextBoxa.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    p.aHTextBlocka.Visibility = System.Windows.Visibility.Hidden;
                    p.aHTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                    p.aHTextBoxa.Visibility = System.Windows.Visibility.Hidden;
                    
                }
                if (WallsModel.BarMainModels[WallsModel.SelectedIndexModel.SelectedWall].BarModels.Count == 0)
                {
                    p.FixedMainBar.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    p.FixedMainBar.Visibility = System.Windows.Visibility.Visible;
                }
            }
            else
            {
                p.aHTextBlocka.Visibility = System.Windows.Visibility.Hidden;
                p.aHTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                p.aHTextBoxa.Visibility = System.Windows.Visibility.Hidden;
                p.DistanceTextBlock.Visibility = System.Windows.Visibility.Hidden;
                p.DistanceTextBox.Visibility = System.Windows.Visibility.Hidden;
                p.DistanceTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                p.FixedMainBar.Visibility = System.Windows.Visibility.Hidden;

            }
        }
        private void ShowCorner(AdditionalStirrupsView p)
        {
            if (SelectedWall.IsCorner)
            {
                p.CornerHorizontalGrid.Visibility = System.Windows.Visibility.Visible;
                p.CornerVerticalGrid.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                p.CornerHorizontalGrid.Visibility = System.Windows.Visibility.Hidden;
                p.CornerVerticalGrid.Visibility = System.Windows.Visibility.Hidden;
            }
        }
        private void ShowTypeHorizontalCorner(AdditionalStirrupsView p)
        {
            if (SelectedWall.AddHCorner)
            {
                if (SelectedWall.TypeHCorner == 0)
                {
                    p.nHCornerComboBox.Visibility = System.Windows.Visibility.Hidden;
                    p.nHCornerTextBlock.Visibility = System.Windows.Visibility.Hidden;
                    p.aHCornerTextBlocka.Visibility = System.Windows.Visibility.Visible;
                    p.aHCornerTextBlockUnit.Visibility = System.Windows.Visibility.Visible;
                    p.aHCornerTextBoxa.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    p.nHCornerComboBox.Visibility = System.Windows.Visibility.Visible;
                    p.nHCornerTextBlock.Visibility = System.Windows.Visibility.Visible;
                    p.aHCornerTextBlocka.Visibility = System.Windows.Visibility.Hidden;
                    p.aHCornerTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                    p.aHCornerTextBoxa.Visibility = System.Windows.Visibility.Hidden;
                }
            }
            else
            {
                p.nHCornerComboBox.Visibility = System.Windows.Visibility.Hidden;
                p.nHCornerTextBlock.Visibility = System.Windows.Visibility.Hidden;
                p.aHCornerTextBlocka.Visibility = System.Windows.Visibility.Hidden;
                p.aHCornerTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                p.aHCornerTextBoxa.Visibility = System.Windows.Visibility.Hidden;
            }

        }
        private void ShowTypeVerticalCorner(AdditionalStirrupsView p)
        {
            if (SelectedWall.AddVCorner)
            {
                if (SelectedWall.TypeVCorner == 0)
                {
                    p.nVCornerComboBox.Visibility = System.Windows.Visibility.Hidden;
                    p.nVCornerTextBlock.Visibility = System.Windows.Visibility.Hidden;
                    p.aVCornerTextBlocka.Visibility = System.Windows.Visibility.Visible;
                    p.aVCornerTextBlockUnit.Visibility = System.Windows.Visibility.Visible;
                    p.aVCornerTextBoxa.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    p.nVCornerComboBox.Visibility = System.Windows.Visibility.Visible;
                    p.nVCornerTextBlock.Visibility = System.Windows.Visibility.Visible;
                    p.aVCornerTextBlocka.Visibility = System.Windows.Visibility.Hidden;
                    p.aVCornerTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                    p.aVCornerTextBoxa.Visibility = System.Windows.Visibility.Hidden;
                }
            }
            else
            {
                p.nVCornerComboBox.Visibility = System.Windows.Visibility.Hidden;
                p.nVCornerTextBlock.Visibility = System.Windows.Visibility.Hidden;
                p.aVCornerTextBlocka.Visibility = System.Windows.Visibility.Hidden;
                p.aVCornerTextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                p.aVCornerTextBoxa.Visibility = System.Windows.Visibility.Hidden;
            }
        }
        #endregion
        #region Draw
        private void DrawCanVasAddHorizontalMain(AdditionalStirrupsView p)
        {
            DrawImage.DrawStirrup(p.HoCanvas0, 30, 10, 1, 80, 120, 5, 3, 12, WallsModel.DrawModel.ColorStirrupChoose);
            DrawImage.DimHorizontalText(p.HoCanvas0, 35, 70, 1, 70, 11, 20, 5, "a");
            DrawImage.DrawHookVertical(p.HoCanvas1, 70, 10, 1, 120, 5, 3, 12, Math.PI / 2, WallsModel.DrawModel.ColorStirrupChoose);
            DrawImage.DrawHookVertical(p.HoCanvas2, 70, 10, 1, 120, 5, 3, 12, 0.75 * Math.PI, WallsModel.DrawModel.ColorStirrupChoose);
            DrawImage.DrawHookVertical(p.HoCanvas3, 70, 10, 1, 120, 5, 3, 12, Math.PI, WallsModel.DrawModel.ColorStirrupChoose);
        }
        private void DrawCanVasAddCorner(AdditionalStirrupsView p)
        {
            DrawImage.DrawStirrup(p.HoCornerCanvas0, 30, 10, 1, 80, 120, 5, 3, 12, WallsModel.DrawModel.ColorStirrupChoose);
            DrawImage.DimHorizontalText(p.HoCornerCanvas0, 35, 70, 1, 70, 11, 20, 5, "a");
            DrawImage.DrawHookVertical(p.HoCornerCanvas1, 70, 10, 1, 120, 5, 3, 12, Math.PI / 2, WallsModel.DrawModel.ColorStirrupChoose);
            DrawImage.DrawHookVertical(p.HoCornerCanvas2, 70, 10, 1, 120, 5, 3, 12, 0.75 * Math.PI, WallsModel.DrawModel.ColorStirrupChoose);
            DrawImage.DrawHookVertical(p.HoCornerCanvas3, 70, 10, 1, 120, 5, 3, 12, Math.PI, WallsModel.DrawModel.ColorStirrupChoose);

            DrawImage.DrawStirrup(p.VeCornerCanvas0, 10, 30, 1, 120, 80, 5, 3, 12, WallsModel.DrawModel.ColorStirrupChoose);
            DrawImage.DimVerticalText(p.VeCornerCanvas0, 70, 35, 1, 70, 11, 20, 5, "a");
            DrawImage.DrawHook(p.VeCornerCanvas1, 10, 70, 1, 120, 5, 3, 12, Math.PI / 2, WallsModel.DrawModel.ColorStirrupChoose);
            DrawImage.DrawHook(p.VeCornerCanvas2, 10, 70, 1, 120, 5, 3, 12, 0.75 * Math.PI, WallsModel.DrawModel.ColorStirrupChoose);
            DrawImage.DrawHook(p.VeCornerCanvas3, 10, 70, 1, 120, 5, 3, 12, Math.PI, WallsModel.DrawModel.ColorStirrupChoose);
        }
        private void DrawInfo(WallShearWindow p)
        {
            p.MainCanvas.Children.Clear();
            DrawMainCanvas.DrawInfoColumns(p.MainCanvas, WallsModel, WallsModel.SelectedIndexModel.SelectedWall);
            DrawMainCanvas.DrawStirrup(p.MainCanvas, WallsModel, SelectedWall.NumberWall - 1);
            double top = WallsModel.DrawModel.Top - (WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall].TopPosition) / (WallsModel.DrawModel.Scale);
            p.scrollViewer.ScrollToBottom();
            p.scrollViewer.ScrollToVerticalOffset(top);
        }
        private void DrawSection(WallShearWindow p)
        {
            p.CanvasSection.Children.Clear();
            double d = (WallsModel.BarMainModels[WallsModel.SelectedIndexModel.SelectedWall].BarModels.Count == 0) ? WallsModel.AllBars[3].Diameter : WallsModel.BarMainModels[WallsModel.SelectedIndexModel.SelectedWall].BarModels[0].Bar.Diameter;
            DrawMainCanvas.DrawAddStirrupsAndSection(p.CanvasSection, WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall], SelectedWall, WallsModel.BarMainModels[WallsModel.SelectedIndexModel.SelectedWall], WallsModel.DrawModelSection, WallsModel.Cover, d);
        }
        #endregion
    }
}
