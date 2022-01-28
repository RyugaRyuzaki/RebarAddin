

using Autodesk.Revit.DB;
using R01_ColumnsRebar.View;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using static R01_ColumnsRebar.ErrorColumns;

namespace R01_ColumnsRebar.ViewModel
{
    public class BarsDivisionViewModel : BaseViewModel
    {
        #region property
        public Document Doc;
        private ColumnsModel _ColumnsModel;
        public ColumnsModel ColumnsModel { get { return _ColumnsModel; } set { _ColumnsModel = value; OnPropertyChanged(); } }
        #endregion
        #region selected
        private BarsDivisionModel _SelectedBarsDivisionModel;
        public BarsDivisionModel SelectedBarsDivisionModel { get { return _SelectedBarsDivisionModel; } set { _SelectedBarsDivisionModel = value; OnPropertyChanged(); } }
        private bool _IsApply;
        public bool IsApply { get { return _IsApply; } set { _IsApply = value; OnPropertyChanged(); } }
        #endregion
        #region Command
        public ICommand LoadBarsDivisionViewCommand { get; set; }
        public ICommand SelectionBarDivisionChangedCommand { get; set; }
        public ICommand ApplyCommand { get; set; }
        public ICommand ModifyCommand { get; set; }
        #endregion
        public BarsDivisionViewModel(Document doc, ColumnsModel columnsModel)
        {
            #region property
            Doc = doc;
            ColumnsModel = columnsModel;

            #endregion
            #region load
            LoadBarsDivisionViewCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                p.MainCanvas.Height = ColumnsModel.DrawModel.Height;
                p.MainCanvas.Width = MaxWidthCanvas();
                
                IsApply = ConditionApplyButton();
                DrawBarDivision(p);
                BarsDivisionView uc = ProccessInfoClumns.FindChild<BarsDivisionView>(p, "BarsDivisionUC");
                PushItemSourceDataGrid(uc);
            });
            SelectionBarDivisionChangedCommand = new RelayCommand<ColumnsWindow>((p) => { return true; }, (p) =>
            {
                BarsDivisionView uc = ProccessInfoClumns.FindChild<BarsDivisionView>(p, "BarsDivisionUC");
                PushItemSourceDataGrid(uc);
            });
            ApplyCommand = new RelayCommand<ColumnsWindow>((p) => { return ConditionApplyButton() && IsApply; }, (p) =>
              {
                  ColumnsModel.ApplyBarDivision();
                  BarsDivisionView uc = ProccessInfoClumns.FindChild<BarsDivisionView>(p, "BarsDivisionUC");
                  PushItemSourceDataGrid(uc);
                  IsApply = false;
                  p.MainCanvas.Width = MaxWidthCanvas();
                  DrawBarDivision(p);
              });
            ModifyCommand = new RelayCommand<ColumnsWindow>((p) => { return !IsApply; }, (p) =>
            {
                ColumnsModel.ModifyBarDivision();
                BarsDivisionView uc = ProccessInfoClumns.FindChild<BarsDivisionView>(p, "BarsDivisionUC");
                PushItemSourceDataGrid(uc);
                IsApply = true; DrawBarDivision(p);
            });
            #endregion
        }
        private bool ConditionApplyButton()
        {

            for (int i = 0; i < ColumnsModel.BarMainModels.Count; i++)
            {
                if (ColumnsModel.BarMainModels[i].BarModels.Count == 0) return false;
            }

            return true;
        }
        private bool ConditioModifyButton()
        {
            for (int i = 0; i < ColumnsModel.BarMainModels.Count; i++)
            {
                if (ColumnsModel.BarMainModels[i].BarModels.Count != 0) return false;
            }

            return true;
        }
        private void SetItemDataGrid(DataGrid dataGrid, ObservableCollection<ItemDivision> itemDivisions)
        {
            if (itemDivisions.Count != 0)
            {
                dataGrid.ItemsSource = itemDivisions;
                dataGrid.Columns[3].Visibility = System.Windows.Visibility.Collapsed;
                dataGrid.Columns[4].Visibility = System.Windows.Visibility.Collapsed;
                dataGrid.Columns[5].Visibility = System.Windows.Visibility.Collapsed;
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
            PushItemDataGrid(uc.StirrupDataGrid, ColumnsModel.BarsDivisionModels[ColumnsModel.SelectedIndexModel.SelectedColumn].Stirrup);
            PushItemDataGrid(uc.AddHDataGrid, ColumnsModel.BarsDivisionModels[ColumnsModel.SelectedIndexModel.SelectedColumn].AddH);
            PushItemDataGrid(uc.AddVDataGrid, ColumnsModel.BarsDivisionModels[ColumnsModel.SelectedIndexModel.SelectedColumn].AddV);
            PushItemDataGrid(uc.BarDataGrid, ColumnsModel.BarsDivisionModels[ColumnsModel.SelectedIndexModel.SelectedColumn].Main);
        }

        private void PushItemDataGrid(DataGrid dataGrid, ObservableCollection<ItemDivision> itemDivisions)
        {
            if (itemDivisions.Count != 0)
            {
                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = itemDivisions;
                dataGrid.Columns[3].Visibility = System.Windows.Visibility.Collapsed;
                dataGrid.Columns[4].Visibility = System.Windows.Visibility.Collapsed;
                dataGrid.Columns[5].Visibility = System.Windows.Visibility.Collapsed;
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
        #region Draw
        private void DrawBarDivision(ColumnsWindow p)
        {
            DrawDetailShop.DrawInfoColumnsBarsDivision(p.MainCanvas, ColumnsModel);
        }
        private double MaxWidthCanvas()
        {
            double maxWidth = (ColumnsModel.SectionStyle == SectionStyle.RECTANGLE) ? ColumnsModel.DrawModel.GetBmax(ColumnsModel.InfoModels) : ColumnsModel.DrawModel.GetDmax(ColumnsModel.InfoModels);
            if (!ConditionApplyButton())
            {
                return ColumnsModel.DrawModel.Width;
            }
            else
            {
                int b = 0;
                for (int i = 0; i < ColumnsModel.BarsDivisionModels.Count; i++)
                {
                    if (ColumnsModel.BarsDivisionModels[i].Main.Count!=0)
                    {
                        if (b < ColumnsModel.BarsDivisionModels[i].Main.Count) b = ColumnsModel.BarsDivisionModels[i].Main.Count;
                    }
                }
                if (b==0)
                {
                    return ColumnsModel.DrawModel.Width;
                }
                else
                {
                    return (maxWidth + (b+1) * ColumnsModel.SettingModel.L2) / ColumnsModel.DrawModel.Scale + 2 * ColumnsModel.DrawModel.Left;
                }
            }
        }
        #endregion

    }
}
