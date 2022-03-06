using Autodesk.Revit.DB;
using R10_WallShear.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using WpfCustomControls;
using R10_WallShear.LanguageModel;
using Visibility = System.Windows.Visibility;
namespace R10_WallShear.ViewModel
{
    public class BarsViewModel : BaseViewModel
    {
        private   string path = @"E:\Book1.xlsx";
       
        #region property
        public Document Doc;
        private WallsModel _WallsModel;
        public WallsModel WallsModel { get => _WallsModel; set { _WallsModel = value; OnPropertyChanged(); } }
        #endregion
        #region Selected
        private BarMainModel _SelectedWall;
        public BarMainModel SelectedWall { get => _SelectedWall; set { _SelectedWall = value; OnPropertyChanged(); } }
        private BarModel _SelectedBar;
        public BarModel SelectedBar { get => _SelectedBar; set { _SelectedBar = value; OnPropertyChanged(); } }
        private BarModel _SelectedBarCorner;
        public BarModel SelectedBarCorner { get => _SelectedBarCorner; set { _SelectedBarCorner = value; OnPropertyChanged(); } }
        #endregion
        #region SplitOverlap
        private List<double> _SplitOverlap;
        public List<double> SplitOverlap { get { if (_SplitOverlap == null) _SplitOverlap = new List<double>() { 50, 100 }; return _SplitOverlap; } set { _SplitOverlap = value; OnPropertyChanged(); } }
        #endregion

        private Visibility _ShowCorner;
        public Visibility ShowCorner { get { return _ShowCorner; } set { _ShowCorner = value; OnPropertyChanged(); } }

        private bool _IsLockBar;
        public bool IsLockBar { get => _IsLockBar; set { _IsLockBar = value; OnPropertyChanged(); } }
        private bool _IsLockNy;
        public bool IsLockNy { get => _IsLockNy; set { _IsLockNy = value; OnPropertyChanged(); } }
        private bool _IsLockBarCorner;
        public bool IsLockBarCorner { get => _IsLockBarCorner; set { _IsLockBarCorner = value; OnPropertyChanged(); } }
        #region Icommand
        public ICommand LoadBarsViewCommand { get; set; }
        public ICommand SelectionWallsChangedCommand { get; set; }

        public ICommand SelectionBarChangedCommand { get; set; }
        public ICommand ApplyBarCommand { get; set; }
        public ICommand ModifyBarCommand { get; set; }

        public ICommand SelectionBarCornerChangedCommand { get; set; }
        public ICommand ApplyBarCornerCommand { get; set; }
        public ICommand ModifyBarCornerCommand { get; set; }
        #endregion
        private Languages _Languages;
        public Languages Languages { get { return _Languages; } set { _Languages = value; OnPropertyChanged(); } }
        public BarsViewModel(Document document, WallsModel wallsModel, Languages languages)
        {
            #region Property
            Doc = document;
            WallsModel = wallsModel; Languages = languages;
           
            #endregion
            #region Load
            LoadBarsViewCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                BarsView uc = ProccessInfoWalls.FindChild<BarsView>(p, "BarsUC");
              
                if (SelectedWall != null)
                {
                    IsLockBar = SelectedWall.BarModels.Count == 0;
                    IsLockNy = !SelectedWall.IsCorner && IsLockBar;
                    if (SelectedWall.IsCorner)
                    {
                        SelectedWall.ny = 2;
                        IsLockBarCorner = SelectedWall.BarCornerModels.Count == 0;
                    }
                    if (SelectedWall.BarModels.Count != 0)
                    {
                        WallsModel.SelectedIndexModel.SelectedMainBar = 0;
                        //InfoModel infoModelUp = WallsModel.InfoModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault();
                        //SelectedWall.GetLocationBarModels(WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall], WallsModel.Cover, WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall].BarS.Diameter, infoModelUp);
                    }

                    
                }
                ShowCorner = (SelectedWall != null && SelectedWall.IsCorner) ? Visibility.Visible : Visibility.Collapsed;
                DrawInfo(p);
                DrawSection(p);
            });
            SelectionWallsChangedCommand = new RelayCommand<WallShearWindow>((p) => { return true; }, (p) =>
            {
                BarsView uc = ProccessInfoWalls.FindChild<BarsView>(p, "BarsUC");
                ShowCorner = (SelectedWall != null && SelectedWall.IsCorner) ? Visibility.Visible : Visibility.Collapsed;
                if (SelectedWall != null)
                {
                    IsLockBar = SelectedWall.BarModels.Count == 0;
                    IsLockNy = !SelectedWall.IsCorner && IsLockBar;
                    if (SelectedWall.IsCorner)
                    {
                        SelectedWall.ny = 2;
                        IsLockBarCorner = SelectedWall.BarCornerModels.Count == 0;
                    }
                    if (SelectedWall.BarModels.Count != 0)
                    {
                        //InfoModel infoModelUp = WallsModel.InfoModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault();
                        //SelectedWall.GetLocationBarModels(WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall], WallsModel.Cover, WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall].BarS.Diameter, infoModelUp);
                    }
                    DrawInfo(p);
                    DrawSection(p);
                     WallsModel.SelectedIndexModel.SelectedMainBar = 0;
                     WallsModel.SelectedIndexModel.SelectedCornerMainBar = 0;
                }

            });
            #endregion
            #region Bar
            SelectionBarChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedWall.BarModels.Count != 0 && SelectedBar != null; }, (p) =>
                  {
                      DrawSection(p);
                      DrawInfo(p);
                  });
            ApplyBarCommand = new RelayCommand<WallShearWindow>((p) => { return ConditionApplyBar(); }, (p) =>
            {
                BarsView uc = ProccessInfoWalls.FindChild<BarsView>(p, "BarsUC");
               
                InfoModel infoModelUp = WallsModel.InfoModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault();
                BarMainModel barMainModelUp = WallsModel.BarMainModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault();
                double ds = WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall].BarS.Diameter;
                double dsUp = (infoModelUp==null) ? (WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall].BarS.Diameter) : (WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall+1].BarS.Diameter);
                double dsUpCorner = GetdsUpCorner(ds,infoModelUp);
                SelectedWall.GetBarModels(WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall], WallsModel.Cover, WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall].BarS.Diameter);
                SelectedWall.GetLocationBarModels(WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall], WallsModel.Cover,ds,dsUp, dsUpCorner, infoModelUp);
                if(SelectedWall.BarCornerModels.Count!=0) SelectedWall.RefreshLocationBarCornerModels(WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall], WallsModel.Cover, ds, dsUp, dsUpCorner, infoModelUp);
                IsLockBar = false;
                IsLockNy = false;
                WallsModel.SelectedIndexModel.SelectedMainBar = 0;
                WallsModel.GetVisibilityAllApplyBar();
                DrawInfo(p);
                DrawSection(p);
            });
            ModifyBarCommand = new RelayCommand<WallShearWindow>((p) => { return !IsLockBar; }, (p) =>
            {
                SelectedWall.BarModels.Clear();
                SelectedWall.BarCornerModels.Clear();
                BarsView uc = ProccessInfoWalls.FindChild<BarsView>(p, "BarsUC");
                IsLockBar = true;
                IsLockBarCorner = true;
                IsLockNy = !SelectedWall.IsCorner && IsLockBar;
                WallsModel.GetVisibilityAllApplyBar();
                DrawInfo(p);
                DrawSection(p);
            });
            #endregion
            #region BarCorner
            SelectionBarCornerChangedCommand = new RelayCommand<WallShearWindow>((p) => { return SelectedWall.BarCornerModels.Count != 0 && SelectedBarCorner != null; }, (p) =>
                {
                    BarsView uc = ProccessInfoWalls.FindChild<BarsView>(p, "BarsUC");
                    DrawSection(p);
                    DrawInfo(p);

                });
            ApplyBarCornerCommand = new RelayCommand<WallShearWindow>((p) => { return ConditionApplyBarCorner()&&SelectedWall.BarModels.Count!=0; }, (p) =>
            {
                BarsView uc = ProccessInfoWalls.FindChild<BarsView>(p, "BarsUC");
                InfoModel infoModelUp = WallsModel.InfoModels.Where(x => x.NumberWall == SelectedWall.NumberWall + 1).FirstOrDefault();
                double ds = WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall].BarSCorner.Diameter;
                double dsUp = (infoModelUp == null) ? (WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall].BarSCorner.Diameter) : ((infoModelUp.IsCorner) ? (WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall + 1].BarSCorner.Diameter) : (WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall + 1].BarSCorner.Diameter));
                double dsUpCorner = GetdsUpCorner(ds,infoModelUp);
                SelectedWall.GetBarCornerModels(WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall], WallsModel.Cover,ds);
                SelectedWall.GetLocationBarCornerModels(WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall], WallsModel.Cover, ds,dsUp,dsUpCorner, infoModelUp);
                SelectedWall.RefreshLocationBarModels(WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall], WallsModel.Cover, ds, dsUp, dsUpCorner, infoModelUp);
                IsLockBarCorner = false;
                WallsModel.SelectedIndexModel.SelectedCornerMainBar = 0;
                WallsModel.GetVisibilityAllApplyBar();
                DrawInfo(p);
                DrawSection(p);
                
            });
            ModifyBarCornerCommand = new RelayCommand<WallShearWindow>((p) => { return !IsLockBarCorner; }, (p) =>
            {
                SelectedWall.BarCornerModels.Clear();
                BarsView uc = ProccessInfoWalls.FindChild<BarsView>(p, "BarsUC");
                IsLockBarCorner = true;
                WallsModel.GetVisibilityAllApplyBar();
                DrawInfo(p);
                DrawSection(p);
            });
            #endregion
        }

        private double GetdsUpCorner(double ds,InfoModel infoModelUp=null)
        {
            double dsUpCorner = 0;
            if (infoModelUp == null)
            {
                if (SelectedWall.IsCorner)
                {
                    dsUpCorner = WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall].BarSCorner.Diameter;
                }
                else
                {
                    dsUpCorner = ds;
                }
            }
            else
            {
                if (infoModelUp.IsCorner)
                {
                    dsUpCorner = WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall + 1].BarSCorner.Diameter;
                }
                else
                {
                    dsUpCorner = WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall + 1].BarS.Diameter;
                }
            }
            return dsUpCorner;
        }
        #region   Method
        //public List<string> ReadXLS()
        //{

        //    FileInfo existingFile = new FileInfo(path);

        //    List<string> list = new List<string>();
        //    using (ExcelPackage package = new ExcelPackage(existingFile))
        //    {
        //        //get the first worksheet in the workbook
        //        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
        //        int colCount = worksheet.Dimension.End.Column;  //get Column Count
        //        int rowCount = worksheet.Dimension.End.Row;     //get row count

        //        for (int row = 1; row <= rowCount; row++)
        //        {
        //            list.Add(worksheet.Cells[row, 1].Value.ToString().Trim());
        //        }
        //    }
        //    return list;
       
        #endregion
        #region Draw
        private void DrawInfo(WallShearWindow p)
        {
            p.MainCanvas.Children.Clear();
            DrawMainCanvas.DrawInfoWall(p.MainCanvas, WallsModel, WallsModel.SelectedIndexModel.SelectedWall);
            DrawMainCanvas.DrawBarMains(p.MainCanvas, WallsModel.DrawModel, SelectedWall, (SelectedBar == null) ? 1000 : SelectedBar.BarNumber,(SelectedBarCorner==null)?1000:SelectedBarCorner.BarNumber);
            double top = WallsModel.DrawModel.Top - (WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall].TopPosition) / (WallsModel.DrawModel.Scale);
            if (SelectedWall.NumberWall == WallsModel.InfoModels[WallsModel.InfoModels.Count - 1].NumberWall) top -= WallsModel.AllBars[WallsModel.AllBars.Count - 1].Diameter * 20;
            p.scrollViewer.ScrollToBottom();
            p.scrollViewer.ScrollToVerticalOffset(top);
        }
        private void DrawSection(WallShearWindow p)
        {
            p.CanvasSection.Children.Clear();
            DrawMainCanvas.DrawSectionMainBar(p.CanvasSection, WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall], WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall], SelectedWall, WallsModel.DrawModelSection, WallsModel.Cover, (SelectedBar == null) ? 1000 : SelectedBar.BarNumber-1, (SelectedBarCorner == null) ? 1000 : SelectedBarCorner.BarNumber-1);
        }
        #endregion
        #region Condition 
        private bool ConditionApplyBar()
        {
            if (SelectedWall.BarModels.Count != 0) return false;
            if (SelectedWall == null) return false;
            double ds = WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall].BarS.Diameter;
            double c = WallsModel.Cover;
            double d = SelectedWall.Bar.Diameter;
            double tmin = WallsModel.SettingModel.tmin;
            double nx = SelectedWall.nx, ny = SelectedWall.ny;
            double b = WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall].L;
            double h = WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall].T;
            double tx = (b - 2 * c - 2 * ds - d) / (nx - 1) - d;
            double ty = (h - 2 * c - 2 * ds - d) / (ny - 1) - d;
            if (tx < tmin) return false;
            if (ty < tmin) return false;
            return true;
        }
        private bool ConditionApplyBarCorner()
        {
            if (SelectedWall.BarCornerModels.Count != 0) return false;
            if (SelectedWall == null) return false;
            double ds = WallsModel.StirrupModels[WallsModel.SelectedIndexModel.SelectedWall].BarS.Diameter;
            double c = WallsModel.Cover;
            double d = SelectedWall.BarCorner.Diameter;
            double tmin = WallsModel.SettingModel.tmin;
            double nx = SelectedWall.nxCorner, ny = SelectedWall.nyCorner;
            double b = WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall].L1;
            double h = WallsModel.InfoModels[WallsModel.SelectedIndexModel.SelectedWall].T;
            double tx = (b - 2 * c - 2 * ds - d) / (nx - 1) - d;
            double ty = (h - 2 * c - 2 * ds - d) / (ny - 1) - d;
            if (tx < tmin) return false;
            if (ty < tmin) return false;
            return true;
           
        }
        #endregion
    }
    class BuildInParaID {

        public static readonly BuiltInParameter MarkPara = (BuiltInParameter)(-1001203);
    }

}
