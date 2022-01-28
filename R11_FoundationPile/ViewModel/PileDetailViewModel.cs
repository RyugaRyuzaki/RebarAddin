using Autodesk.Revit.DB;
using R11_FoundationPile.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace R11_FoundationPile.ViewModel
{
    public class PileDetailViewModel :BaseViewModel
    {
        #region Property
        public Document Doc;
        private UnitProject _Unit;
        public UnitProject Unit { get { return _Unit; } set { _Unit = value; OnPropertyChanged(); } }
        private FoundationPileModel _FoundationPileModel;
        public FoundationPileModel FoundationPileModel { get => _FoundationPileModel; set { _FoundationPileModel = value; OnPropertyChanged(); } }
        private FoundationModel _SelectedFoundationModel;
        public FoundationModel SelectedFoundationModel { get => _SelectedFoundationModel; set { _SelectedFoundationModel = value; OnPropertyChanged(); } }
        private PileModel _SelectedPileModel;
        public PileModel SelectedPileModel { get => _SelectedPileModel; set { _SelectedPileModel = value; OnPropertyChanged(); } }
        private bool _IsEnabled;
        public bool IsEnabled { get => _IsEnabled; set { _IsEnabled = value; OnPropertyChanged(); } }
        #endregion
        #region Icommand
        public ICommand LoadPileDetailViewCommand { get; set; }
        public ICommand ApplyCommand { get; set; }
        public ICommand ModifyCommand { get; set; }
        public ICommand ApplyTestPileCommand { get; set; }
        #endregion
        public PileDetailViewModel(Document doc, FoundationPileModel foundationPileModel, UnitProject unitProject)
        {
            #region property
            Doc = doc;
            Unit = unitProject;
            FoundationPileModel = foundationPileModel;
            #endregion
            #region Load
            LoadPileDetailViewCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {
                PileDetailView uc = ProccessInfoClumns.FindChild<PileDetailView>(p, "PileDetailUC");
                DrawRuleImage(uc);
                IsEnabled = !FoundationPileModel.IsApplyRule;
                DrawSection(uc);
                DrawPileDetail(p);
            });
            ApplyCommand = new RelayCommand<FoundationPileWindow>((p) => { return !FoundationPileModel.IsApplyRule&&FoundationPileModel.ConditionCreateFoundation(); }, (p) =>
            {
                FoundationPileModel.GetAllFoundationModel();
                FoundationPileModel.SelectedIndexModel.SelectedIndexAllFoundation = 0;
                FoundationPileModel.IsApplyRule = true;
                IsEnabled = !FoundationPileModel.IsApplyRule;
                PileDetailView uc = ProccessInfoClumns.FindChild<PileDetailView>(p, "PileDetailUC");
                DrawSection(uc);
                DrawPileDetail(p);
            });
            ModifyCommand = new RelayCommand<FoundationPileWindow>((p) => { return FoundationPileModel.IsApplyRule; }, (p) =>
            {
                FoundationPileModel.AllFoundationModels.Clear();
                FoundationPileModel.IsApplyRule = false;
                IsEnabled = !FoundationPileModel.IsApplyRule;
                PileDetailView uc = ProccessInfoClumns.FindChild<PileDetailView>(p, "PileDetailUC");
                DrawSection(uc);
                DrawPileDetail(p);
            });
            ApplyTestPileCommand = new RelayCommand<FoundationPileWindow>((p) => { return FoundationPileModel.IsApplyRule&& SelectedFoundationModel!=null&& SelectedPileModel!=null&& !SelectedPileModel.IsTestingPile; }, (p) =>
            {
                SelectedPileModel.IsTestingPile = true;
                for (int i = 0; i < SelectedFoundationModel.PileModels.Count; i++)
                {
                    if (SelectedFoundationModel.PileModels[i].PileNumber != SelectedPileModel.PileNumber) SelectedFoundationModel.PileModels[i].IsTestingPile = false;
                }
            });
            #endregion
        }
        #region DrawRule
        private void DrawRuleImage( PileDetailView p)
        {
            DrawImage.DrawFoundationRule0(p.FoundationRuleCanvas0);
            DrawImage.DrawFoundationRule1(p.FoundationRuleCanvas1);
            DrawImage.DrawFoundationRule2(p.FoundationRuleCanvas2);
            DrawImage.DrawFoundationRule3(p.FoundationRuleCanvas3);
            DrawImage.DrawFoundationRule4(p.FoundationRuleCanvas4);
            DrawImage.DrawFoundationRule5(p.FoundationRuleCanvas5);
            DrawImage.DrawFoundationRule6(p.FoundationRuleCanvas6);
            DrawImage.DrawFoundationRule7(p.FoundationRuleCanvas7);
            DrawImage.DrawFoundationRule0(p.PileRuleCanvas0);
            DrawImage.DrawFoundationRule1(p.PileRuleCanvas1);
            DrawImage.DrawFoundationRule2(p.PileRuleCanvas2);
            DrawImage.DrawFoundationRule3(p.PileRuleCanvas3);
            DrawImage.DrawFoundationRule4(p.PileRuleCanvas4);
            DrawImage.DrawFoundationRule5(p.PileRuleCanvas5);
            DrawImage.DrawFoundationRule6(p.PileRuleCanvas6);
            DrawImage.DrawFoundationRule7(p.PileRuleCanvas7);
        }
        private void DrawSection(PileDetailView p)
        {
            p.PileDetailCanvas.Children.Clear();
            if (FoundationPileModel.IsApplyRule&& SelectedFoundationModel!=null)
            {
                if (SelectedFoundationModel.BoundingLocation.Count != 0 && SelectedFoundationModel.PileModels.Count != 0)
                {
                    FoundationPileModel.DrawModelSection.GetScaleSection(SelectedFoundationModel, Unit);
                    DrawMainCanvas.DrawMainFoundationSection(p.PileDetailCanvas, FoundationPileModel.DrawModelSection, SelectedFoundationModel, FoundationPileModel.SettingModel, (SelectedPileModel == null) ? 1000 : (SelectedPileModel.PileNumber - 1));
                }
            }
        }
        private void DrawPileDetail(FoundationPileWindow p)
        {
            p.MainCanvas.Children.Clear();
            
            if (FoundationPileModel.IsApplyRule&&FoundationPileModel.FoundationPileDetail.FoundationView!=null)
            {
                FoundationPileModel.DrawModelPileDetail.GetScalePileDetail(FoundationPileModel.FoundationPileDetail,Doc,Unit);
                double minX = double.Parse(UnitFormatUtils.Format(Doc.GetUnits(), SpecTypeId.Length, FoundationPileModel.FoundationPileDetail.FoundationBox.Min.X, false));
                double maxX = double.Parse(UnitFormatUtils.Format(Doc.GetUnits(), SpecTypeId.Length, FoundationPileModel.FoundationPileDetail.FoundationBox.Max.X, false));
                double minY = double.Parse(UnitFormatUtils.Format(Doc.GetUnits(), SpecTypeId.Length, FoundationPileModel.FoundationPileDetail.FoundationBox.Min.Y, false));
                p.MainCanvas.Width = (maxX - minX) / FoundationPileModel.DrawModelPileDetail.Scale;
                
                for (int i = 0; i < FoundationPileModel.AllFoundationModels.Count; i++)
                {
                    DrawMainCanvas.DrawMainPileDetail(p.MainCanvas, FoundationPileModel.DrawModelPileDetail, FoundationPileModel.AllFoundationModels[i], FoundationPileModel.SettingModel,((i==FoundationPileModel.SelectedIndexModel.SelectedIndexAllFoundation)?(FoundationPileModel.DrawModelPileDetail.ColorMainBarChoose):(FoundationPileModel.DrawModelPileDetail.ColorMainBar)), minX,minY);
                }
            }
        }
        #endregion
    }
}
