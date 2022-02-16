using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCustomControls;
namespace R11_FoundationPile
{
    public class GroupFoundationModel :BaseViewModel
    {
        #region  property
        private string _SectionStyle;
        public string SectionStyle { get => _SectionStyle; set { _SectionStyle = value; OnPropertyChanged(); } }
        private int _Type;
        public int Type { get => _Type; set { _Type = value; OnPropertyChanged(); } }
       
        private int _Image;
        public int Image { get => _Image; set { _Image = value; OnPropertyChanged(); } }

        private double _L1;
        public double L1 { get => _L1; set { _L1 = value; OnPropertyChanged(); } }
        private double _L2;
        public double L2 { get => _L2; set { _L2 = value; OnPropertyChanged(); } }
        private ObservableCollection<FoundationModel> _FoundationModels;
        public ObservableCollection<FoundationModel> FoundationModels { get { if (_FoundationModels == null) { _FoundationModels = new ObservableCollection<FoundationModel>(); } return _FoundationModels; } set { _FoundationModels = value; OnPropertyChanged(); } }
        private ObservableCollection<LayerPileModel> _LayerPileModels;
        public ObservableCollection<LayerPileModel> LayerPileModels { get { if (_LayerPileModels == null) { _LayerPileModels = new ObservableCollection<LayerPileModel>(); } return _LayerPileModels; } set { _LayerPileModels = value; OnPropertyChanged(); } }
        private bool _IsGenerate;
        public bool IsGenerate { get => _IsGenerate; set { _IsGenerate = value; OnPropertyChanged(); } }
        private bool _IsCreate;
        public bool IsCreate { get => _IsCreate; set { _IsCreate = value; OnPropertyChanged(); } }
        #endregion
        public GroupFoundationModel(int type,ObservableCollection<ColumnModel> columnModels,SettingModel settingModel)
        {
            SectionStyle = columnModels[0].Name;
            Type = type;
            Image = 0;
            for (int i = 0; i < columnModels.Count; i++)
            {
                FoundationModels.Add(new FoundationModel(Type,Image,i + 1, columnModels[i], settingModel));
            }
            IsGenerate = false;
            IsCreate = false;
            ChangedImage(settingModel);
        }
        #region   Method
        public void ChangedImage( SettingModel settingModel)
        {
            for (int i = 0; i < FoundationModels.Count; i++)
            {
                FoundationModels[i].Image = Image;
            }
            GetL1L2(settingModel);
        }
        public void GetL1L2(SettingModel settingModel)
        {
            if (Image == 0)
            {
                L1 = Math.Round( settingModel.DiameterPile , 3);
                L2 = Math.Round(2 * settingModel.DiameterPile, 3);
            }
            else
            {
                if (Image == 1)
                {
                    L1 = Math.Round(2* settingModel.DiameterPile , 3);
                    L2 = Math.Round(2.5 *settingModel.DiameterPile, 3);
                }
            }
        }
        public void AddLayerPileModel(int numberPile)
        {
            LayerPileModels.Add(new LayerPileModel(LayerPileModels.Count+1,numberPile));
        }
        public void DeleteLayerPileModel(int layerNumber)
        {
            if (layerNumber == LayerPileModels.Count-1)
            {
               
                LayerPileModels.RemoveAt(layerNumber);
            }
            else
            {
                LayerPileModels[layerNumber + 1].NumberLayer -= 1;
                LayerPileModels.RemoveAt(layerNumber);
            }
          
        }
        public int GetTotalPile()
        {
            int total = 0;
            if (LayerPileModels.Count==0)
            {
                total= 0;
            }
            else
            {
                for (int i = 0; i < LayerPileModels.Count; i++)
                {
                    total += LayerPileModels[i].NumberPile;
                }
            }
            return total;
        }
        public bool ConditionGenerate(SettingModel settingModel)
        {
            if (Image==0)
            {
                if (L1<=0||L2<=0)
                {
                    return false;
                }
                else
                {
                    if (L1+L2>settingModel.DistancePP*settingModel.DiameterPile)
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (Image==1)
                {
                    if(LayerPileModels.Count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        if (L1 <= 0 || L2 <= 0)
                        {
                            return false;
                        }
                        else
                        {
                            if (L1 < settingModel.DistancePP * settingModel.DiameterPile*0.5|| L2 < settingModel.DistancePP * settingModel.DiameterPile * 0.5)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        #endregion
        #region GetAllPile
        public void GenerateFoundationAndPiles(SettingModel settingModel)
        {
            for (int i = 0; i < FoundationModels.Count; i++)
            {
                FoundationModels[i].GetAllPiles( settingModel, L1, L2, LayerPileModels);
                FoundationModels[i].GetBoundingFoundation( settingModel, L1, L2, LayerPileModels);
               
            }
        }
        public void ModifyFoundationAndPiles()
        {
            for (int i = 0; i < FoundationModels.Count; i++)
            {
                FoundationModels[i].BoundingLocation.Clear();
                FoundationModels[i].PileModels.Clear();
            }
        }
        public double GetXMin()
        {
            return FoundationModels.Min(x=>x.GetMinX());
        }
        public double GetXMax()
        {
            return FoundationModels.Max(x => x.GetMaxX());
        }
        public double GetYMin()
        {
            return FoundationModels.Min(x => x.GetMinY());
        }
        public double GetYMax()
        {
            return FoundationModels.Max(x => x.GetMaxY());
        }
        #endregion
    }
    public  class LayerPileModel :BaseViewModel
    {
        private int _NumberLayer;
        public int NumberLayer { get => _NumberLayer; set { _NumberLayer = value; OnPropertyChanged(); } }
        private int _NumberPile;
        public int NumberPile { get => _NumberPile; set { _NumberPile = value; OnPropertyChanged(); } }
        public LayerPileModel(int numberLayer,int numberPile)
        {
            NumberLayer = numberLayer;
            NumberPile = numberPile;
        }
    }
    
}
