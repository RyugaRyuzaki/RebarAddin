

using Autodesk.Revit.DB;
using R11_FoundationPile.View;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using WpfCustomControls;
namespace R11_FoundationPile.ViewModel
{
    public class SettingViewModel:BaseViewModel
    {
        #region Property
        public Document Doc;
        private FoundationPileModel _FoundationPileModel;
        public FoundationPileModel FoundationPileModel { get => _FoundationPileModel; set { _FoundationPileModel = value; OnPropertyChanged(); } }
        private List<string> _CategoryPiles;
        public List<string> CategoryPiles { get { if (_CategoryPiles == null) { _CategoryPiles = new List<string>() { "Structural Columns","Structural Foundation"}; } return _CategoryPiles; } set { _CategoryPiles = value; OnPropertyChanged(); } }
        private bool _IsApply;    // Overlap pile to foundation
        public bool IsApply { get => _IsApply; set { _IsApply = value; OnPropertyChanged(); } }
        #endregion
        #region Icommand
        public ICommand LoadSettingViewCommand { get; set; }
        public ICommand SelectionChangedCategoryPileCommand { get; set; }
        public ICommand SelectionChangedPileFamilyCommand { get; set; }
        public ICommand SelectionChangedPileFamilyTypeCommand { get; set; }
        public ICommand SelectionChangedCategoryFoundationCommand { get; set; }
        public ICommand SelectionChangedFoundationTypeCommand { get; set; }
        public ICommand ApplyPilePropertyCommand { get; set; }
        public ICommand ModifyPilePropertyCommand { get; set; }
        public ICommand CheckedTextCommand { get; set; }
        public ICommand IscreateFormWorkCommand { get; set; }
        #endregion
        public SettingViewModel(Document doc, FoundationPileModel foundationPileModel)
        {
            #region property
            Doc = doc;
            FoundationPileModel = foundationPileModel;
            #endregion
            #region
            LoadSettingViewCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {
                p.MainCanvas.Width = 820;
                IsApply = !FoundationPileModel.SettingModel.IsApply;
                SettingView uc = ProccessInfoClumns.FindChild<SettingView>(p, "SettingUC");
                //if (FoundationPileModel.IsCreateGrounpFoundation) uc.PileSettingGroupBox.IsEnabled = false;
                //FoundationPileModel.SettingModel.SelectedPileFamily = FoundationPileModel.SettingModel.FamilyPiles[FoundationPileModel.SelectedIndexModel.SelectedIndexPileFamily];
                //FoundationPileModel.SettingModel.SelectedPileFamilyType = FoundationPileModel.SettingModel.FamilySymbolPiles[FoundationPileModel.SelectedIndexModel.SelectedIndexPileFamilyType];
                //FoundationPileModel.SettingModel.GetDiameterPile(Doc);
                DrawSettingImage(uc);
            });
            SelectionChangedCategoryPileCommand = new RelayCommand<FoundationPileWindow>((p) => { return !FoundationPileModel.SettingModel.SelectedCategoyryPile.Equals("")&& IsApply; }, (p) =>
            {
                FoundationPileModel.SettingModel.GetFamilyPile(Doc);
                FoundationPileModel.SelectedIndexModel.SelectedIndexPileFamily = 0;
                FoundationPileModel.SettingModel.GetAllFamilySymbol();
                FoundationPileModel.SelectedIndexModel.SelectedIndexPileFamilyType = 0;
                FoundationPileModel.SettingModel.GetDiameterPile(Doc);
                FoundationPileModel.SettingModel.GetTag(Doc);
            });
            SelectionChangedPileFamilyCommand = new RelayCommand<FoundationPileWindow>((p) => { return FoundationPileModel.SettingModel.SelectedPileFamily !=null && IsApply; }, (p) =>
            {
                FoundationPileModel.SettingModel.GetAllFamilySymbol();
                FoundationPileModel.SelectedIndexModel.SelectedIndexPileFamilyType = 0;
                FoundationPileModel.SettingModel.GetDiameterPile(Doc);
            });
            SelectionChangedPileFamilyTypeCommand = new RelayCommand<FoundationPileWindow>((p) => { return FoundationPileModel.SettingModel.SelectedPileFamilyType != null && IsApply; }, (p) =>
            {
                FoundationPileModel.SettingModel.GetDiameterPile(Doc);
                FoundationPileModel.ChangeL1L2Foundation();
            });
            #endregion
            #region   Foundation
            SelectionChangedCategoryFoundationCommand = new RelayCommand<FoundationPileWindow>((p) => { return !FoundationPileModel.SettingModel.SelectedCategoyryFoundation.Equals("")&& IsApply; }, (p) =>
            {
                FoundationPileModel.SettingModel.GetFoundationTypes(Doc);
                FoundationPileModel.SelectedIndexModel.SelectedIndexFoundationType = 0;
                FoundationPileModel.SettingModel.GetTag(Doc);
            });
            SelectionChangedFoundationTypeCommand = new RelayCommand<FoundationPileWindow>((p) => { return FoundationPileModel.SettingModel.SelectedFoundationType!=null; }, (p) =>
            {
                //FoundationPileModel.SettingModel.HeightFoundation = FoundationPileModel.SettingModel.WidthFloor(Doc, FoundationPileModel.SettingModel.SelectedFoundationType);

            });
            ApplyPilePropertyCommand = new RelayCommand<FoundationPileWindow>((p) => { return !FoundationPileModel.SettingModel.IsApply && ConditionApplyPileProperty(p); }, (p) =>
            {
                FoundationPileModel.SettingModel.IsApply = true;
                IsApply = !FoundationPileModel.SettingModel.IsApply;
            });
            ModifyPilePropertyCommand = new RelayCommand<FoundationPileWindow>((p) => { return FoundationPileModel.SettingModel.IsApply&&FoundationPileModel.ConditionModifySetting(); }, (p) =>
            {
                FoundationPileModel.SettingModel.IsApply = false;
                IsApply = !FoundationPileModel.SettingModel.IsApply;
            });
            CheckedTextCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {
                SettingView uc = ProccessInfoClumns.FindChild<SettingView>(p, "SettingUC");
                ShowTagGrid(uc);
            });
            IscreateFormWorkCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {
                SettingView uc = ProccessInfoClumns.FindChild<SettingView>(p, "SettingUC");
                ShowFormWorkGrid(uc);
            });
            #endregion
        }

        private void ShowTagGrid(SettingView uc)
        {
            if (FoundationPileModel.SettingModel.CheckedText)
            {
                uc.TagGrid.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                if (uc.TagGrid.Visibility == System.Windows.Visibility.Visible) uc.TagGrid.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        private void ShowFormWorkGrid(SettingView uc)
        {
            if (FoundationPileModel.SettingModel.IsCreateFormWork)
            {
                uc.FormWorkGrid.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                if (uc.FormWorkGrid.Visibility == System.Windows.Visibility.Visible) uc.FormWorkGrid.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        private bool ConditionApplyPileProperty(FoundationPileWindow p)
        {
            if (FoundationPileModel.SettingModel.DiameterPile==0) { return false; }
            
            SettingView uc = ProccessInfoClumns.FindChild<SettingView>(p, "SettingUC");
            if (double.TryParse(uc.dppTextBox.Text.ToString(),out double S))
            {
                if (S < 1) return false;
            }
            else
            {
                return false;
            }
            if (double.TryParse(uc.dpsTextBox.Text.ToString(), out double S1))
            {
                if (S1 <= FoundationPileModel.SettingModel.DiameterPile/2) return false;
            }
            else
            {
                return false;
            }
            if (double.TryParse(uc.OverlapTextBox.Text.ToString(), out double S2))
            {
                if (S2 <=0) return false;
            }
            else
            {
                return false;
            }
            if (double.TryParse(uc.LengthTextBox.Text.ToString(), out double S3))
            {
                if (S3 <= FoundationPileModel.SettingModel.DiameterPile) return false;
            }
            else
            {
                return false;
            }
            if (FoundationPileModel.SettingModel.IsCreateFormWork)
            {
                if (double.TryParse(uc.HeightFormWorkTextBox.Text.ToString(), out double S4))
                {
                    if (S4 <=0) return false;
                }
                else
                {
                    return false;
                }

                if (double.TryParse(uc.OffsetFormWorkTextBox.Text.ToString(), out double S5))
                {
                    if (S5 <= 0) return false;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        #region Draw
        private void DrawSettingImage(SettingView p)
        {
            DrawImage.DrawPileLocation(p.CanvasOverlapPile);
            DrawImage.DrawLenthPile(p.CanvasLengthPile);
            DrawImage.DrawOffsetDim(p.OffsetCanvas);
        }
        #endregion
    }
}
