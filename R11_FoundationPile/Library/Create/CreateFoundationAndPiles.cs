using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using WpfCustomControls.CustomControls;
using System.Windows.Controls;
using WpfCustomControls;
namespace R11_FoundationPile
{
    public class CreateFoundationAndPiles
    {

        #region Create
        public static void Create(FoundationPileWindow p, FoundationPileModel foundationPileModel,Document document,UnitProject unit)
        {
            ProgressBar uc = VisualTreeHelper.FindChild<ProgressBar>(p, "Progress");
            uc.Maximum = GetProgressBarCreateFoundationAndPiles(foundationPileModel, document) * 1.0;
            using (Transaction transaction = new Transaction(document, Guid.NewGuid().GetHashCode().ToString()))
            {
                transaction.Start();
                if (foundationPileModel.SettingModel.HeightFoundation != foundationPileModel.SettingModel.WidthFloor(document, foundationPileModel.SettingModel.SelectedFoundationType))
                {
                    FloorType floorType = foundationPileModel.SettingModel.SelectedFoundationType.Duplicate(foundationPileModel.SettingModel.HeightFoundation + "Foundation") as FloorType;

                    CompoundStructure compound = floorType.GetCompoundStructure();
                    CompoundStructureLayer layer = compound.GetLayers().FirstOrDefault();
                    layer.Width = unit.Convert(foundationPileModel.SettingModel.HeightFoundation);
                    compound.SetLayer(0, layer);
                    floorType.SetCompoundStructure(compound);
                    foundationPileModel.SettingModel.SelectedFoundationType = floorType;
                    foundationPileModel.ProgressModel.SetValue(uc, 1);
                }
                if (foundationPileModel.SettingModel.IsCreateFormWork)
                {
                    if (foundationPileModel.SettingModel.HeightFormWork != foundationPileModel.SettingModel.WidthFloor(document, foundationPileModel.SettingModel.SelectedFormWorkType))
                    {
                       
                        FloorType floorType = foundationPileModel.SettingModel.SelectedFormWorkType.Duplicate(foundationPileModel.SettingModel.HeightFormWork + "FormWork") as FloorType;
                        CompoundStructure compound = floorType.GetCompoundStructure();
                        CompoundStructureLayer layer = compound.GetLayers().FirstOrDefault();
                        layer.Width = unit.Convert(foundationPileModel.SettingModel.HeightFormWork);
                        compound.SetLayer(0, layer);
                        floorType.SetCompoundStructure(compound);
                        foundationPileModel.SettingModel.SelectedFormWorkType = floorType;
                        foundationPileModel.ProgressModel.SetValue(uc, 1);
                    }
                }
              
                for (int i = 0; i < foundationPileModel.GroupFoundationModels.Count; i++)
                {
                    for (int j = 0; j < foundationPileModel.GroupFoundationModels[i].FoundationModels.Count; j++)
                    {
                        foundationPileModel.GroupFoundationModels[i].FoundationModels[j].CreateFoundation(document, unit, foundationPileModel.SettingModel); foundationPileModel.ProgressModel.SetValue(uc, 1);
                        FailureHandlingOptions option = transaction.GetFailureHandlingOptions();
                        option.SetFailuresPreprocessor(new DeleteWarningSuper());
                        transaction.SetFailureHandlingOptions(option);
                        if (foundationPileModel.GroupFoundationModels[i].FoundationModels[j].Foundation==null || !foundationPileModel.GroupFoundationModels[i].FoundationModels[j].IscreatePile())
                        {
                            foundationPileModel.GroupFoundationModels[i].IsCreate = false;
                        }
                        else
                        {
                            foundationPileModel.GroupFoundationModels[i].IsCreate = true ;
                        }
                      
                    }
                }
              
                transaction.Commit();
            }
           
            foundationPileModel.IsCreateGrounpFoundation = foundationPileModel.ConditionShowPileDetail();
            if (foundationPileModel.IsCreateGrounpFoundation)
            {
               
                using (Transaction transaction = new Transaction(document, Guid.NewGuid().GetHashCode().ToString()))
                {
                    transaction.Start("aaa");
                   
                    foundationPileModel.FoundationPileDetail.CeateFoundationPlan(document, unit, foundationPileModel.SettingModel, foundationPileModel.GroupFoundationModels[0].FoundationModels[0].ColumnModel, foundationPileModel.GetMaxBoundBox(unit), foundationPileModel.GetMinBoundBox(unit));
                    foundationPileModel.ProgressModel.SetValue(uc, 1);

                    transaction.Commit();
                }
                if (foundationPileModel.FoundationPileDetail.FoundationView != null)
                {
                    using (Transaction transaction = new Transaction(document, Guid.NewGuid().GetHashCode().ToString()))
                    {
                        transaction.Start("aaa");
                        foundationPileModel.SettingModel.GetFoundationViewType(document); foundationPileModel.ProgressModel.SetValue(uc, 1);
                        foundationPileModel.SettingModel.GetFoundationSectionType(document); foundationPileModel.ProgressModel.SetValue(uc, 1);
                        foundationPileModel.DimensionDetail.CreateDimensionGridX(foundationPileModel.FoundationPileDetail.FoundationView, document, unit, foundationPileModel.SettingModel); foundationPileModel.ProgressModel.SetValue(uc, 1);
                        foundationPileModel.DimensionDetail.CreateDimensionGridY(foundationPileModel.FoundationPileDetail.FoundationView, document, unit, foundationPileModel.SettingModel); foundationPileModel.ProgressModel.SetValue(uc, 1);

                        for (int i = 0; i < foundationPileModel.GroupFoundationModels.Count; i++)
                        {
                            for (int j = 0; j < foundationPileModel.GroupFoundationModels[i].FoundationModels.Count; j++)
                            {
                                foundationPileModel.GroupFoundationModels[i].FoundationModels[j].ColumnModel.RefreshPlanarFaceNormal(document, unit); foundationPileModel.ProgressModel.SetValue(uc, 1);
                                foundationPileModel.DimensionDetail.CreateDimensionFoundationVertical(foundationPileModel.FoundationPileDetail.FoundationView, document, unit, foundationPileModel.SettingModel, foundationPileModel.GroupFoundationModels[i].FoundationModels[j], foundationPileModel.GroupFoundationModels[i].Image); foundationPileModel.ProgressModel.SetValue(uc, 1);
                                foundationPileModel.DimensionDetail.CreateDimensionFoundationHorizontal(foundationPileModel.FoundationPileDetail.FoundationView, document, unit, foundationPileModel.SettingModel, foundationPileModel.GroupFoundationModels[i].FoundationModels[j], foundationPileModel.GroupFoundationModels[i].Image); foundationPileModel.ProgressModel.SetValue(uc, 1);
                                foundationPileModel.GroupFoundationModels[i].FoundationModels[j].CreateTagFoundation(foundationPileModel.FoundationPileDetail.FoundationView, document, unit, foundationPileModel.SettingModel, foundationPileModel.GroupFoundationModels[i].Type); foundationPileModel.ProgressModel.SetValue(uc, 1);

                                if (foundationPileModel.GroupFoundationModels[i].FoundationModels[j].IsRepresentative&& foundationPileModel.SettingModel.FoundationDetailViewType!=null)
                                {
                                    foundationPileModel.GroupFoundationModels[i].FoundationModels[j].CreateCallOutFoundationDetailView(foundationPileModel.FoundationPileDetail.FoundationView, document, unit, foundationPileModel.SettingModel, foundationPileModel.GroupFoundationModels[i].Type); foundationPileModel.ProgressModel.SetValue(uc, 1);
                                    foundationPileModel.GroupFoundationModels[i].FoundationModels[j].CreateFoundationSectionHorizontal(document, unit, foundationPileModel.SettingModel, foundationPileModel.GroupFoundationModels[i].Image); foundationPileModel.ProgressModel.SetValue(uc, 1);
                                    foundationPileModel.GroupFoundationModels[i].FoundationModels[j].CreateFoundationSectionVertical(document, unit, foundationPileModel.SettingModel, foundationPileModel.GroupFoundationModels[i].Image); foundationPileModel.ProgressModel.SetValue(uc, 1);
                                }
                            }
                        }
                        transaction.Commit();
                    }
                }
            }
            foundationPileModel.ProgressModel.ResetValue(uc);


        }
        #endregion
        private static int GetProgressBarCreateFoundationAndPiles( FoundationPileModel foundationPileModel,Document document)
        {
            int a = 0;
            
            
            if (foundationPileModel.SettingModel.HeightFoundation != foundationPileModel.SettingModel.WidthFloor(document, foundationPileModel.SettingModel.SelectedFoundationType)) a++;
            if (foundationPileModel.SettingModel.IsCreateFormWork) a++;
            for (int i = 0; i < foundationPileModel.GroupFoundationModels.Count; i++)
            {
                for (int j = 0; j < foundationPileModel.GroupFoundationModels[i].FoundationModels.Count; j++)
                {
                    a++;
                }
            }
            a+=1;
            a+=4;
            for (int i = 0; i < foundationPileModel.GroupFoundationModels.Count; i++)
            {
                for (int j = 0; j < foundationPileModel.GroupFoundationModels[i].FoundationModels.Count; j++)
                {
                    a+=4;
                    if (foundationPileModel.GroupFoundationModels[i].FoundationModels[j].IsRepresentative)
                    {
                        a+=3;
                    }
                }
            }
            return a;
        }
        
    }
}
