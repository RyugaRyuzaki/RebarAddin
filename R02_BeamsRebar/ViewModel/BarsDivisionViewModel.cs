using Autodesk.Revit.DB;
using Microsoft.Win32;
using R02_BeamsRebar.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using WpfCustomControls;
using R02_BeamsRebar.LanguageModel;
namespace R02_BeamsRebar.ViewModel
{
    public class BarsDivisionViewModel : BaseViewModel
    {
        #region Property
        public Document Doc;
        private BeamsModel _BeamsModel;
        public BeamsModel BeamsModel { get => _BeamsModel; set { _BeamsModel = value; OnPropertyChanged(); } }
        private string _AttentionDetailItem;
        public string AttentionDetailItem { get => _AttentionDetailItem; set { _AttentionDetailItem = value; OnPropertyChanged(); } }
        #endregion
        private bool _IsApply;
        public bool IsApply { get => _IsApply; set { _IsApply = value; OnPropertyChanged(); } }

        #region Command
        public ICommand LoadBarsDivisionCommand { get; set; }
        public ICommand SelectionChangedWayCommand { get; set; }
        public ICommand SelectionChangedOddCommand { get; set; }
        public ICommand ApplyCommand { get; set; }
        public ICommand ModifyCommand { get; set; }
        #endregion
        public BarsDivisionViewModel(Document document, BeamsModel beamsModel)
        {
            #region Get Property
            Doc = document;
            BeamsModel = beamsModel;
            IsApply = true;
            AttentionDetailItem = "*Before you use this Extension, make sure you loaded All Family DetailShop of Author on your project";
            #endregion
            LoadBarsDivisionCommand = new RelayCommand<BeamsWindow>((p) => { return true; }, (p) =>
            {

                BarsDivisionView uc = ProcessInfoBeamRebar.FindChild<BarsDivisionView>(p, "BarsDivisionUC");
                DrawCanvas(uc);
                GetCanvasDetailItem(uc);
                LoadBarsDivision(p);
                PushItemSourceDataGrid(uc);
                if (BeamsModel.SpecialBarModel.Count == 0)
                {
                    uc.SpecialBarGroupBox.Visibility = System.Windows.Visibility.Hidden;
                }
                if (!ConditionAntiStirrupShow())
                {
                    uc.AntiBarGroupBox.Visibility = System.Windows.Visibility.Hidden;
                }
                if (!ConditionSideShow())
                {
                    uc.SideBarGroupBox.Visibility = System.Windows.Visibility.Hidden;
                }
            });
            ApplyCommand = new RelayCommand<BeamsWindow>((p) => { return ConditionButtonApply() && ConditionApply()&&BeamsModel.ConditionCreateDetailShop(Doc); }, (p) =>
            {
                //if (!BeamsModel.ConditionCreateDetailShop(Doc))
                //{
                //    if (System.Windows.Forms.MessageBox.Show(LoadDetailShop, LoadFamily, System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                //    {
                //        LoadDetailShopFamily();
                //        p.DialogResult = true;
                //    }
                //}
                BarsDivisionView uc = ProcessInfoBeamRebar.FindChild<BarsDivisionView>(p, "BarsDivisionUC");
                uc.ButtonApply.IsEnabled = false;
                IsApply = !IsApply;
                Apply(uc);
                DrawShopFull(p);
                BeamsModel.IsDetailIShop = true;
            });
            ModifyCommand = new RelayCommand<BeamsWindow>((p) => { return ConditionButtonApply() && !ConditionApply(); }, (p) =>
            {
                IsApply = !IsApply;
                BarsDivisionView uc = ProcessInfoBeamRebar.FindChild<BarsDivisionView>(p, "BarsDivisionUC");
                Modify(uc);
                p.canvas.Children.Clear();
                BeamsModel.IsDetailIShop = false;
            });
        }

        #region Get Event
        private void Modify(BarsDivisionView uc)
        {
            uc.ButtonApply.IsEnabled = true;
            uc.StirrupDataGrid.ItemsSource = null;
            uc.AntiStirrupDataGrid.ItemsSource = null;
            uc.MainTopDataGrid.ItemsSource = null;
            uc.MainBottomnDataGrid.ItemsSource = null;
            uc.AddTopDataGrid.ItemsSource = null;
            uc.AddBottomnDataGrid.ItemsSource = null;
            uc.SpecialDataGrid.ItemsSource = null;
            uc.SideDataGrid.ItemsSource = null;
            BeamsModel.BarsDivisionModel.Stirrups.Clear();
            BeamsModel.BarsDivisionModel.AntiStirrups.Clear();
            BeamsModel.BarsDivisionModel.MainTop.Clear();
            BeamsModel.BarsDivisionModel.MainBottom.Clear();
            BeamsModel.BarsDivisionModel.AddTop.Clear();
            BeamsModel.BarsDivisionModel.AddBottom.Clear();
            BeamsModel.BarsDivisionModel.Special.Clear();
            BeamsModel.BarsDivisionModel.Side.Clear();
        }

        private void Apply(BarsDivisionView uc)
        {
            double dsmax = ProcessInfoBeamRebar.GetDiameterStirrupMax(BeamsModel.InfoModels, BeamsModel.DistributeStirrups, BeamsModel.StirrupModels);
            if (BeamsModel.SelectedIndexModel.StyleMainTop == 0)
            {
                BeamsModel.SingleMainTopBarModel.Refresh(BeamsModel.InfoModels, BeamsModel.NodeModels, BeamsModel.Cover, dsmax);
                BeamsModel.SingleMainTopBarModel.GetLength();
            }
            else
            {
                for (int i = 0; i < BeamsModel.MainTopBarModel.Count; i++)
                {
                    BeamsModel.MainTopBarModel[i].Refresh(dsmax, BeamsModel.Cover);
                    BeamsModel.MainTopBarModel[i].GetLocationBarModels();
                }
            }
            for (int i = 0; i < BeamsModel.MainBottomBarModel.Count; i++)
            {
                BeamsModel.MainBottomBarModel[i].Refresh(dsmax, BeamsModel.Cover);
                BeamsModel.MainBottomBarModel[i].GetLocationBarModels();
            }
            BeamsModel.BarsDivisionModel.GetStirrups(BeamsModel.InfoModels, BeamsModel.StirrupModels, BeamsModel.DistributeStirrups, BeamsModel.SpecialBarModel, BeamsModel.SpecialNodeModels, BeamsModel.DivisionBar);
            SetItemDataGrid(uc.StirrupDataGrid, BeamsModel.BarsDivisionModel.Stirrups);
            if (ConditionAntiStirrupShow())
            {
                BeamsModel.BarsDivisionModel.GetAntiStirrups(BeamsModel.InfoModels, BeamsModel.StirrupModels, BeamsModel.DistributeStirrups, BeamsModel.SpecialBarModel, BeamsModel.SpecialNodeModels, BeamsModel.DivisionBar, BeamsModel.SettingModel);
                SetItemDataGrid(uc.AntiStirrupDataGrid, BeamsModel.BarsDivisionModel.AntiStirrups);
            }
            BeamsModel.BarsDivisionModel.GetMainTop(BeamsModel.SingleMainTopBarModel, BeamsModel.MainTopBarModel, BeamsModel.DivisionBar, BeamsModel.SelectedIndexModel);
            SetItemDataGrid(uc.MainTopDataGrid, BeamsModel.BarsDivisionModel.MainTop);
            BeamsModel.BarsDivisionModel.GetMainBottom(BeamsModel.MainBottomBarModel, BeamsModel.DivisionBar);
            SetItemDataGrid(uc.MainBottomnDataGrid, BeamsModel.BarsDivisionModel.MainBottom);
            BeamsModel.BarsDivisionModel.GetAddTop(BeamsModel.AddTopBarModel, BeamsModel.DivisionBar, BeamsModel.SelectedIndexModel);
            SetItemDataGrid(uc.AddTopDataGrid, BeamsModel.BarsDivisionModel.AddTop);
            BeamsModel.BarsDivisionModel.GetAddBottom(BeamsModel.AddBottomBarModel, BeamsModel.DivisionBar, BeamsModel.SelectedBottomModels);
            SetItemDataGrid(uc.AddBottomnDataGrid, BeamsModel.BarsDivisionModel.AddBottom);
            BeamsModel.BarsDivisionModel.GetSpecial(BeamsModel.SpecialBarModel, BeamsModel.DivisionBar);
            SetItemDataGrid(uc.SpecialDataGrid, BeamsModel.BarsDivisionModel.Special);
            BeamsModel.BarsDivisionModel.GetSide(BeamsModel.SideBarModel, BeamsModel.DivisionBar);
            SetItemDataGrid(uc.SideDataGrid, BeamsModel.BarsDivisionModel.Side);
        }
        private void SetItemDataGrid(DataGrid dataGrid, ObservableCollection<ItemDivision> itemDivisions)
        {
            if (itemDivisions.Count != 0)
            {
                dataGrid.ItemsSource = itemDivisions;
                dataGrid.Columns[8].Visibility = System.Windows.Visibility.Collapsed;
                dataGrid.Columns[9].Visibility = System.Windows.Visibility.Collapsed;
                dataGrid.Columns[10].Visibility = System.Windows.Visibility.Collapsed;
                dataGrid.Columns[11].Visibility = System.Windows.Visibility.Collapsed;
                dataGrid.Columns[12].Visibility = System.Windows.Visibility.Collapsed;
                dataGrid.Columns[13].Visibility = System.Windows.Visibility.Collapsed;
                dataGrid.Columns[14].Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        private void PushItemSourceDataGrid(BarsDivisionView uc)
        {
            PushItemDataGrid(uc.StirrupDataGrid, BeamsModel.BarsDivisionModel.Stirrups);
            PushItemDataGrid(uc.AntiStirrupDataGrid, BeamsModel.BarsDivisionModel.AntiStirrups);
            PushItemDataGrid(uc.MainTopDataGrid, BeamsModel.BarsDivisionModel.MainTop);
            PushItemDataGrid(uc.MainBottomnDataGrid, BeamsModel.BarsDivisionModel.MainBottom);
            PushItemDataGrid(uc.AddTopDataGrid, BeamsModel.BarsDivisionModel.AddTop);
            PushItemDataGrid(uc.AddBottomnDataGrid, BeamsModel.BarsDivisionModel.AddBottom);
            PushItemDataGrid(uc.SpecialDataGrid, BeamsModel.BarsDivisionModel.Special);
            PushItemDataGrid(uc.SideDataGrid, BeamsModel.BarsDivisionModel.Side);
        }

        private void PushItemDataGrid(DataGrid dataGrid, ObservableCollection<ItemDivision> itemDivisions)
        {
            if (itemDivisions.Count != 0)
            {
                dataGrid.ItemsSource = itemDivisions;
                dataGrid.Columns[8].Visibility = System.Windows.Visibility.Collapsed;
                dataGrid.Columns[9].Visibility = System.Windows.Visibility.Collapsed;
                dataGrid.Columns[10].Visibility = System.Windows.Visibility.Collapsed;
                dataGrid.Columns[11].Visibility = System.Windows.Visibility.Collapsed;
                dataGrid.Columns[12].Visibility = System.Windows.Visibility.Collapsed;
                dataGrid.Columns[13].Visibility = System.Windows.Visibility.Collapsed;
                dataGrid.Columns[14].Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                dataGrid.ItemsSource = null;
            }
        }

        private void LoadBarsDivision(BeamsWindow p)
        {
            p.canvas.Children.Clear();
            p.scrollViewer.ScrollToLeftEnd();
            if (!ConditionApply()) DrawShopFull(p);
        }

        private void DrawShopFull(BeamsWindow p)
        {
            p.canvas.Height = 2 * BeamsModel.DrawModel.Top + (2 * BeamsModel.SettingModel.L3 + BeamsModel.SettingModel.L4 + GetYmax()) / BeamsModel.DrawModel.Scale;
            DrawDivisionShop.DrawShopStirrup(p.canvas, BeamsModel.BarsDivisionModel, BeamsModel.DrawModel, BeamsModel.SettingModel, BeamsModel.Cover);
            DrawDivisionShop.DrawShopAntiStirrup(p.canvas, BeamsModel.BarsDivisionModel, BeamsModel.DrawModel, BeamsModel.SettingModel, BeamsModel.Cover);
            DrawDivisionShop.DrawShopMainTop(p.canvas, BeamsModel.BarsDivisionModel, BeamsModel.DrawModel, BeamsModel.SettingModel, BeamsModel.SelectedIndexModel);
            DrawDivisionShop.DrawShopMainBottom(p.canvas, BeamsModel.BarsDivisionModel, BeamsModel.DrawModel, BeamsModel.SettingModel);
            DrawDivisionShop.DrawShopAddTop(p.canvas, BeamsModel.BarsDivisionModel, BeamsModel.DrawModel, BeamsModel.SettingModel);
            DrawDivisionShop.DrawShopAddBottom(p.canvas, BeamsModel.BarsDivisionModel, BeamsModel.DrawModel, BeamsModel.SettingModel);
            DrawDivisionShop.DrawShopSpecial(p.canvas, BeamsModel.BarsDivisionModel, BeamsModel.DrawModel, BeamsModel.SettingModel);
            DrawDivisionShop.DrawShopSide(p.canvas, BeamsModel.BarsDivisionModel, BeamsModel.DrawModel, BeamsModel.SettingModel);
        }

        private double GetYmax()
        {
            double a = 0;
            for (int i = 0; i < BeamsModel.MainBottomBarModel.Count; i++)
            {
                if (a < BeamsModel.MainBottomBarModel[i].Y0) a = BeamsModel.MainBottomBarModel[i].Y0;
            }
            return a;
        }

        private bool ConditionApply()
        {
            return (BeamsModel.BarsDivisionModel.Stirrups.Count == 0 &&
                BeamsModel.BarsDivisionModel.AntiStirrups.Count == 0 &&
                BeamsModel.BarsDivisionModel.MainTop.Count == 0 &&
                BeamsModel.BarsDivisionModel.MainBottom.Count == 0 &&
                BeamsModel.BarsDivisionModel.AddTop.Count == 0 &&
                BeamsModel.BarsDivisionModel.AddBottom.Count == 0 &&
                BeamsModel.BarsDivisionModel.Side.Count == 0);
        }
        private bool ConditionAntiStirrupShow()
        {
            for (int i = 0; i < BeamsModel.StirrupModels.Count; i++)
            {
                return BeamsModel.StirrupModels[i].Anti && BeamsModel.StirrupModels[i].Na != 0 && BeamsModel.StirrupModels[i].Sa != 0;
            }
            return true;
        }
        private bool ConditionSideShow()
        {
            for (int i = 0; i < BeamsModel.SideBarModel.Count; i++)
            {
                return BeamsModel.SideBarModel[i].IsSide;
            }
            return false;
        }
        #endregion
        #region Condition Button
        private bool ConditionButtonApply()
        {
            return ((BeamsModel.DivisionBar.Lmax != 0) && (BeamsModel.DivisionBar.Overlap != 0));
        }
        #endregion
        #region Draw
        private void DrawCanvas(BarsDivisionView p)
        {
            p.TopCanvas.Children.Clear();
            DrawImage.DrawDivisionTop(p.TopCanvas, true);
            p.BottomCanvas.Children.Clear();
            DrawImage.DrawDivisionBottom(p.BottomCanvas, true);
        }
        private void GetCanvasDetailItem(BarsDivisionView p)
        {
            CanvasDetailItem c0 = new CanvasDetailItem(p.GridCanvas, 0, 0);
            c0.Draw(DetailShopStyle.DS00);
            CanvasDetailItem c1 = new CanvasDetailItem(p.GridCanvas, 1, 0);
            c1.Draw(DetailShopStyle.DS01);
            CanvasDetailItem c2 = new CanvasDetailItem(p.GridCanvas, 2, 0);
            c2.Draw(DetailShopStyle.DS02);
            CanvasDetailItem c3 = new CanvasDetailItem(p.GridCanvas, 3, 0);
            c3.Draw(DetailShopStyle.DS03);
            CanvasDetailItem c4 = new CanvasDetailItem(p.GridCanvas, 0, 1);
            c4.Draw(DetailShopStyle.DS04);
            CanvasDetailItem c5 = new CanvasDetailItem(p.GridCanvas, 1, 1);
            c5.Draw(DetailShopStyle.DS05);
            CanvasDetailItem c6 = new CanvasDetailItem(p.GridCanvas, 2, 1);
            c6.Draw(DetailShopStyle.DS06);
            CanvasDetailItem c7 = new CanvasDetailItem(p.GridCanvas, 3, 1);
            c7.Draw(DetailShopStyle.DS07);
            CanvasDetailItem c8 = new CanvasDetailItem(p.GridCanvas, 0, 2);
            c8.Draw(DetailShopStyle.DS08);
            CanvasDetailItem c9 = new CanvasDetailItem(p.GridCanvas, 1, 2);
            c9.Draw(DetailShopStyle.DS09);
            CanvasDetailItem c10 = new CanvasDetailItem(p.GridCanvas, 2, 2);
            c10.Draw(DetailShopStyle.DS10);
            CanvasDetailItem c11 = new CanvasDetailItem(p.GridCanvas, 3, 2);
            c11.Draw(DetailShopStyle.DS11);
            CanvasDetailItem c12 = new CanvasDetailItem(p.GridCanvas, 0, 3);
            c12.Draw(DetailShopStyle.DS12);
            CanvasDetailItem c13 = new CanvasDetailItem(p.GridCanvas, 1, 3);
            c13.Draw(DetailShopStyle.DS13);
        }
        #endregion
        #region Load Family
        //private static DirectoryInfo directory = new DirectoryInfo(@"C:\Users\Admin\AppData\Roaming\Autodesk\Revit\Addins\2021\DetailItem\");
        //private static FileInfo[] FileInfo = directory.GetFiles("*.rfa");
        //private static string LoadDetailShop = "Load Them to project and must created them handle" + "\n" + "Do you want to load them ?";
        //private static string LoadFamily = "There are no Family DetailShop in project";
        //private  void LoadDetailShopFamily()
        //{
        //    using (Transaction transaction = new Transaction(Doc))
        //    {
        //        transaction.Start("aa");
        //        Family family = null;
        //        foreach (var item in FileInfo)
        //        {
        //            if (item.Name.Contains("DS"))
        //            {
        //                Doc.LoadFamily(directory + item.Name, out family);
        //            }
        //        }
        //        transaction.Commit();
        //    }
        //}
        #endregion
    }

}