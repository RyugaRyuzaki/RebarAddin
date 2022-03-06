using Autodesk.Revit.DB;
using R11_FoundationPile.View;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WpfCustomControls;
using WpfCustomControls.ViewModel;
using DSP;
using R11_FoundationPile.LanguageModel;
namespace R11_FoundationPile.ViewModel
{
    public class GeometryViewModel : BaseViewModel
    {
        #region Property
        public Document Doc;
        private UnitProject _Unit;
        public UnitProject Unit { get { return _Unit; } set { _Unit = value; OnPropertyChanged(); } }
        private FoundationPileModel _FoundationPileModel;
        public FoundationPileModel FoundationPileModel { get => _FoundationPileModel; set { _FoundationPileModel = value; OnPropertyChanged(); } }
        private GroupFoundationModel _SelectedGroupFoundationModel;
        public GroupFoundationModel SelectedGroupFoundationModel { get => _SelectedGroupFoundationModel; set { _SelectedGroupFoundationModel = value; OnPropertyChanged(); } }
        #endregion
        #region Selected NumberPile
        private FoundationModel _SelectedFoundationModel;
        public FoundationModel SelectedFoundationModel { get => _SelectedFoundationModel; set { _SelectedFoundationModel = value; OnPropertyChanged(); } }
        private PileModel _SelectedPileModel;
        public PileModel SelectedPileModel { get => _SelectedPileModel; set { _SelectedPileModel = value; OnPropertyChanged(); } }
        private ObservableCollection<int> _AllNumberPile;
        public ObservableCollection<int> AllNumberPile { get { if (_AllNumberPile == null) { _AllNumberPile = new ObservableCollection<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; } return _AllNumberPile; } set { _AllNumberPile = value; OnPropertyChanged(); } }
        private int _SelectedNumberPile;
        public int SelectedNumberPile { get => _SelectedNumberPile; set { _SelectedNumberPile = value; OnPropertyChanged(); } }
        private LayerPileModel _SelectedLayerPileModel;
        public LayerPileModel SelectedLayerPileModel { get => _SelectedLayerPileModel; set { _SelectedLayerPileModel = value; OnPropertyChanged(); } }

        private bool _IsEnabled;
        public bool IsEnabled { get => _IsEnabled; set { _IsEnabled = value; OnPropertyChanged(); } }
        #endregion
        #region Icommand
        public ICommand LoadGeometryViewCommand { get; set; }
        public ICommand SelectionChangedGroupFoundationCommand { get; set; }
        public ICommand SelectionChangedImageFoundationCommand { get; set; }
        public ICommand AddLayerPileCommand { get; set; }
        public ICommand DeleteLayerPileCommand { get; set; }
        public ICommand GenerateFoundationCommand { get; set; }
        public ICommand ModifyFoundationCommand { get; set; }
        public ICommand ReverseFoundationCommand { get; set; }
        public ICommand SelectionChangedFoundationCommand { get; set; }
        public ICommand SelectionChangedPileCommand { get; set; }
        public ICommand ApplyRepresentativeCommand { get; set; }
        #endregion
        private Languages _Languages;
        public Languages Languages { get { return _Languages; } set { _Languages = value; OnPropertyChanged(); } }
        public GeometryViewModel(Document doc, FoundationPileModel foundationPileModel, UnitProject unitProject, Languages languages)
        {
            #region property
            Doc = doc;
            Unit = unitProject;
            FoundationPileModel = foundationPileModel;
            Languages = languages;
            SelectedNumberPile = AllNumberPile[0];
            #endregion
            #region Load
            LoadGeometryViewCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {
                p.MainCanvas.Width = 820;
                IsEnabled = !SelectedGroupFoundationModel.IsGenerate;
                GeometryView uc = ProccessInfoClumns.FindChild<GeometryView>(p, "GeometryUC");
                DrawImageFoundation(uc);
                ShowLayerPile(uc);
                ShowBHDColumnModel(uc);
                DrawMain(p);
            });
            #endregion
            #region   Event
            SelectionChangedGroupFoundationCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {
                IsEnabled = !SelectedGroupFoundationModel.IsGenerate;
                GeometryView uc = ProccessInfoClumns.FindChild<GeometryView>(p, "GeometryUC");
                ShowLayerPile(uc);
                ShowBHDColumnModel(uc);

                FoundationPileModel.SelectedIndexModel.SelectedIndexFoundationModel = 0;
                if (SelectedFoundationModel != null && SelectedFoundationModel.PileModels.Count != 0) FoundationPileModel.SelectedIndexModel.SelectedIndexPile = 0;
                DrawMain(p);
            });
            SelectionChangedImageFoundationCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {
                SelectedGroupFoundationModel.ChangedImage(FoundationPileModel.SettingModel);
               
                GeometryView uc = ProccessInfoClumns.FindChild<GeometryView>(p, "GeometryUC");
                ShowLayerPile(uc);
                ShowBHDColumnModel(uc);

            });
            AddLayerPileCommand = new RelayCommand<FoundationPileWindow>((p) => { return !SelectedGroupFoundationModel.IsGenerate; }, (p) =>
            {
                SelectedGroupFoundationModel.AddLayerPileModel(SelectedNumberPile);

                GeometryView uc = ProccessInfoClumns.FindChild<GeometryView>(p, "GeometryUC");
            });
            DeleteLayerPileCommand = new RelayCommand<FoundationPileWindow>((p) => { return SelectedGroupFoundationModel.LayerPileModels.Count != 0 && SelectedLayerPileModel != null && !SelectedGroupFoundationModel.IsGenerate; }, (p) =>
                {
                    SelectedGroupFoundationModel.DeleteLayerPileModel(SelectedLayerPileModel.NumberLayer - 1);

                    GeometryView uc = ProccessInfoClumns.FindChild<GeometryView>(p, "GeometryUC");
                });
            GenerateFoundationCommand = new RelayCommand<FoundationPileWindow>((p) => { return !SelectedGroupFoundationModel.IsGenerate && SelectedGroupFoundationModel.ConditionGenerate(FoundationPileModel.SettingModel) && FoundationPileModel.SettingModel.IsApply && ConditionL1L2(p); }, (p) =>
                {
                    GeometryView uc = ProccessInfoClumns.FindChild<GeometryView>(p, "GeometryUC");
                    SelectedGroupFoundationModel.GenerateFoundationAndPiles(FoundationPileModel.SettingModel);
                    SelectedGroupFoundationModel.IsGenerate = true;
                    IsEnabled = !SelectedGroupFoundationModel.IsGenerate;
                    if (SelectedFoundationModel.PileModels.Count != 0) FoundationPileModel.SelectedIndexModel.SelectedIndexPile = 0;
                    DrawMain(p);
                });
            ModifyFoundationCommand = new RelayCommand<FoundationPileWindow>((p) => { return SelectedGroupFoundationModel.IsGenerate && !FoundationPileModel.IsCreateGrounpFoundation; }, (p) =>
              {
                  SelectedGroupFoundationModel.IsGenerate = false;

                  SelectedGroupFoundationModel.ModifyFoundationAndPiles();

                  p.MainCanvas.Children.Clear();
                  IsEnabled = true;
              });
            ReverseFoundationCommand = new RelayCommand<FoundationPileWindow>((p) => { return SelectedGroupFoundationModel.IsGenerate && !FoundationPileModel.IsCreateGrounpFoundation && (SelectedGroupFoundationModel.Image == 0 || SelectedGroupFoundationModel.Image == 2) && SelectedFoundationModel != null; }, (p) =>
                     {
                         SelectedFoundationModel.IsRollBack = !SelectedFoundationModel.IsRollBack;
                         SelectedFoundationModel.GetBoundingFoundation(FoundationPileModel.SettingModel, SelectedGroupFoundationModel.L1, SelectedGroupFoundationModel.L2, SelectedGroupFoundationModel.LayerPileModels);
                         SelectedFoundationModel.GetAllPiles(FoundationPileModel.SettingModel, SelectedGroupFoundationModel.L1, SelectedGroupFoundationModel.L2, SelectedGroupFoundationModel.LayerPileModels);
                         DrawMain(p);
                     });
            SelectionChangedFoundationCommand = new RelayCommand<FoundationPileWindow>((p) => { return SelectedGroupFoundationModel.IsGenerate; }, (p) =>
            {
                DrawMain(p);
            });
            SelectionChangedPileCommand = new RelayCommand<FoundationPileWindow>((p) => { return SelectedGroupFoundationModel.IsGenerate && !IsEnabled; }, (p) =>
             {
                 DrawMain(p);
             });
            ApplyRepresentativeCommand = new RelayCommand<FoundationPileWindow>((p) => { return SelectedGroupFoundationModel.IsGenerate && !IsEnabled && SelectedFoundationModel != null && !SelectedFoundationModel.IsRepresentative && !FoundationPileModel.IsCreateGrounpFoundation; }, (p) =>
               {
                   SelectedFoundationModel.IsRepresentative = true;
                   //double maxDiameter = FoundationPileModel.AllBars.Max(x => x.Diameter);
                   //SelectedFoundationModel.GetBar(FoundationPileModel.AllBars[3]);
                   //SelectedFoundationModel.SetPropertyBar(Doc, maxDiameter * 5, FoundationPileModel.SettingModel);
                   for (int i = 0; i < SelectedGroupFoundationModel.FoundationModels.Count; i++)
                   {
                       if (SelectedGroupFoundationModel.FoundationModels[i].FoundationNumber != SelectedFoundationModel.FoundationNumber) { SelectedGroupFoundationModel.FoundationModels[i].IsRepresentative = false; }
                   }
                   for (int i = 0; i < FoundationPileModel.GroupFoundationModels.Count; i++)
                   {
                       for (int j = 0; j < FoundationPileModel.GroupFoundationModels[i].FoundationModels.Count; j++)
                       {
                           if (FoundationPileModel.GroupFoundationModels[i].FoundationModels[j].IsRepresentative) { FoundationPileModel.SelectedIndexModel.Representative.Add(FoundationPileModel.GroupFoundationModels[i].FoundationModels[j].LocationName); }
                       }
                   }
               });

            #endregion
        }
        #region method
        private void DrawImageFoundation(GeometryView p)
        {
            DrawImage.DrawImageFoundation0(p.FoundationImageCanvas0);
            DrawImage.DrawImageFoundation1(p.FoundationImageCanvas1);
            DrawImage.DrawImageFoundation2(p.FoundationImageCanvas2);
            DrawImage.DrawImageFoundation3(p.FoundationImageCanvas3);
        }

        private void ShowLayerPile(GeometryView p)
        {
            if (SelectedGroupFoundationModel.Image != 1)
            {
                p.LayerPileGrid.Visibility = System.Windows.Visibility.Hidden;

            }
            else
            {
                p.LayerPileGrid.Visibility = System.Windows.Visibility.Visible;
            }
            if (SelectedGroupFoundationModel.Image == 0 || SelectedGroupFoundationModel.Image == 1)
            {
                p.L1TextBlock.Visibility = System.Windows.Visibility.Visible;
                p.L1TextBlockUnit.Visibility = System.Windows.Visibility.Visible;
                p.L1TextBox.Visibility = System.Windows.Visibility.Visible;
                p.L2TextBlock.Visibility = System.Windows.Visibility.Visible;
                p.L2TextBlockUnit.Visibility = System.Windows.Visibility.Visible;
                p.L2TextBox.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                p.L1TextBlock.Visibility = System.Windows.Visibility.Hidden;
                p.L1TextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                p.L1TextBox.Visibility = System.Windows.Visibility.Hidden;
                p.L2TextBlock.Visibility = System.Windows.Visibility.Hidden;
                p.L2TextBlockUnit.Visibility = System.Windows.Visibility.Hidden;
                p.L2TextBox.Visibility = System.Windows.Visibility.Hidden;
            }

        }
        private bool ConditionL1L2(FoundationPileWindow p)
        {
            GeometryView uc = ProccessInfoClumns.FindChild<GeometryView>(p, "GeometryUC");
            if (!double.TryParse(uc.L1TextBox.Text.ToString(), out double S1))
            {
                return false;
            }
            if (!double.TryParse(uc.L2TextBox.Text.ToString(), out double S2))
            {
                return false;
            }
            return true;
        }
        private void ShowBHDColumnModel(GeometryView p)
        {
            if (SelectedGroupFoundationModel.SectionStyle.Contains("bxh"))
            {
                p.bGridViewColumn.Width = 60;
                p.hGridViewColumn.Width = 60;
                p.DGridViewColumn.Width = 0;
            }
            else
            {
                p.bGridViewColumn.Width = 0;
                p.hGridViewColumn.Width = 0;
                p.DGridViewColumn.Width = 60;
            }
            if (SelectedGroupFoundationModel.Image == 2 || SelectedGroupFoundationModel.Image == 3)
            {
                p.LengthGridViewColumn.Width = 0;
                p.WidthGridViewColumn.Width = 0;
            }
            else
            {
                p.LengthGridViewColumn.Width = 60;
                p.WidthGridViewColumn.Width = 60;
            }
        }

        #endregion
        #region DrawMain
        private void DrawMain(FoundationPileWindow p)
        {
            p.MainCanvas.Children.Clear();
            if (SelectedFoundationModel != null && SelectedGroupFoundationModel.IsGenerate)
            {
                if (SelectedFoundationModel.BoundingLocation.Count != 0 && SelectedFoundationModel.PileModels.Count != 0)
                {
                    FoundationPileModel.DrawModel.GetScale(SelectedFoundationModel, Unit);
                    DrawMainCanvas.DrawMainFoundation(p.MainCanvas, FoundationPileModel.DrawModel, SelectedFoundationModel, FoundationPileModel.SettingModel, (SelectedPileModel == null) ? 1000 : (SelectedPileModel.PileNumber - 1));
                }
            }
        }

        #endregion
    }
}
