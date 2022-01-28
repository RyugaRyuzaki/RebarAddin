using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace R11_FoundationPile
{
    public class CreateFoundationAndPiles
    {

        #region Create
        public static void Create(FoundationPileWindow p, FoundationPileModel foundationPileModel,Document document,UnitProject unit)
        {
            GetProgressBarCreateFoundationAndPiles(p, foundationPileModel, document);
            using (Transaction transaction = new Transaction(document, Guid.NewGuid().GetHashCode().ToString()))
            {
                transaction.Start();
                if (foundationPileModel.SettingModel.HeightFoundation != foundationPileModel.SettingModel.WidthFloor(document, foundationPileModel.SettingModel.SelectedFoundationType))
                {
                    foundationPileModel.SettingModel.SelectedFoundationType.Duplicate(foundationPileModel.SettingModel.SelectedFoundationType.Name + "Foundation");
                    CompoundStructure compound = foundationPileModel.SettingModel.SelectedFoundationType.GetCompoundStructure();
                    CompoundStructureLayer layer = compound.GetLayers().FirstOrDefault();
                    layer.Width = unit.Convert(foundationPileModel.SettingModel.HeightFoundation);
                    compound.SetLayer(0, layer);
                    foundationPileModel.SettingModel.SelectedFoundationType.SetCompoundStructure(compound);
                    SetValue(p, foundationPileModel, 1);
                }
                if (foundationPileModel.SettingModel.IsCreateFormWork)
                {
                    if (foundationPileModel.SettingModel.HeightFormWork != foundationPileModel.SettingModel.WidthFloor(document, foundationPileModel.SettingModel.SelectedFormWorkType))
                    {
                        foundationPileModel.SettingModel.SelectedFormWorkType.Duplicate(foundationPileModel.SettingModel.SelectedFormWorkType.Name + "FormWork");
                        CompoundStructure compound = foundationPileModel.SettingModel.SelectedFormWorkType.GetCompoundStructure();
                        CompoundStructureLayer layer = compound.GetLayers().FirstOrDefault();
                        layer.Width = unit.Convert(foundationPileModel.SettingModel.HeightFormWork);
                        compound.SetLayer(0, layer);
                        foundationPileModel.SettingModel.SelectedFormWorkType.SetCompoundStructure(compound);
                        SetValue(p, foundationPileModel, 1);
                    }
                }
              
                for (int i = 0; i < foundationPileModel.GroupFoundationModels.Count; i++)
                {
                    for (int j = 0; j < foundationPileModel.GroupFoundationModels[i].FoundationModels.Count; j++)
                    {
                        foundationPileModel.GroupFoundationModels[i].FoundationModels[j].CreateFoundation(document, unit, foundationPileModel.SettingModel, foundationPileModel.GroupFoundationModels[i].Type);
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
                        SetValue(p, foundationPileModel, 1);
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
                    SetValue(p, foundationPileModel, 1);
                   
                    transaction.Commit();
                }
                if (foundationPileModel.FoundationPileDetail.FoundationView != null)
                {
                    using (Transaction transaction = new Transaction(document, Guid.NewGuid().GetHashCode().ToString()))
                    {
                        transaction.Start("aaa");
                        foundationPileModel.SettingModel.GetFoundationViewType(document);
                        foundationPileModel.SettingModel.GetFoundationSectionType(document);
                        foundationPileModel.DimensionDetail.CreateDimensionGridX(foundationPileModel.FoundationPileDetail.FoundationView, document, unit, foundationPileModel.SettingModel);
                        foundationPileModel.DimensionDetail.CreateDimensionGridY(foundationPileModel.FoundationPileDetail.FoundationView, document, unit, foundationPileModel.SettingModel);
                        SetValue(p, foundationPileModel, 1); SetValue(p, foundationPileModel, 1);
                        for (int i = 0; i < foundationPileModel.GroupFoundationModels.Count; i++)
                        {
                            for (int j = 0; j < foundationPileModel.GroupFoundationModels[i].FoundationModels.Count; j++)
                            {
                                foundationPileModel.GroupFoundationModels[i].FoundationModels[j].ColumnModel.RefreshPlanarFaceNormal(document, unit);
                                foundationPileModel.DimensionDetail.CreateDimensionFoundationVertical(foundationPileModel.FoundationPileDetail.FoundationView, document, unit, foundationPileModel.SettingModel, foundationPileModel.GroupFoundationModels[i].FoundationModels[j], foundationPileModel.GroupFoundationModels[i].Image);
                                foundationPileModel.DimensionDetail.CreateDimensionFoundationHorizontal(foundationPileModel.FoundationPileDetail.FoundationView, document, unit, foundationPileModel.SettingModel, foundationPileModel.GroupFoundationModels[i].FoundationModels[j], foundationPileModel.GroupFoundationModels[i].Image);
                                foundationPileModel.GroupFoundationModels[i].FoundationModels[j].CreateTagFoundation(foundationPileModel.FoundationPileDetail.FoundationView, document, unit, foundationPileModel.SettingModel, foundationPileModel.GroupFoundationModels[i].Type);
                                SetValue(p, foundationPileModel, 1);
                                
                                if (foundationPileModel.GroupFoundationModels[i].FoundationModels[j].IsRepresentative&& foundationPileModel.SettingModel.FoundationDetailViewType!=null)
                                {
                                    foundationPileModel.GroupFoundationModels[i].FoundationModels[j].CreateCallOutFoundationDetailView(foundationPileModel.FoundationPileDetail.FoundationView, document, unit, foundationPileModel.SettingModel, foundationPileModel.GroupFoundationModels[i].Type);
                                    SetValue(p, foundationPileModel, 1);
                                }
                                foundationPileModel.GroupFoundationModels[i].FoundationModels[j].CreateFoundationSectionHorizontal(document, unit, foundationPileModel.SettingModel); SetValue(p, foundationPileModel, 1);
                                foundationPileModel.GroupFoundationModels[i].FoundationModels[j].CreateFoundationSectionVertical(document, unit, foundationPileModel.SettingModel, foundationPileModel.GroupFoundationModels[i].Image); SetValue(p, foundationPileModel, 1);
                            }
                        }
                        transaction.Commit();
                    }
                }
            }
            ResetValue(p, foundationPileModel);
        }
        #endregion
        private static void GetProgressBarCreateFoundationAndPiles(FoundationPileWindow p, FoundationPileModel foundationPileModel,Document document)
        {
            foundationPileModel.Value = 0; foundationPileModel.Percent = 0;
            p.ProgressWindow.Maximum = 0;
            if (foundationPileModel.SettingModel.HeightFoundation != foundationPileModel.SettingModel.WidthFloor(document, foundationPileModel.SettingModel.SelectedFoundationType)) p.ProgressWindow.Maximum++;
            if (foundationPileModel.SettingModel.IsCreateFormWork) p.ProgressWindow.Maximum++;
            for (int i = 0; i < foundationPileModel.GroupFoundationModels.Count; i++)
            {
                for (int j = 0; j < foundationPileModel.GroupFoundationModels[i].FoundationModels.Count; j++)
                {
                    p.ProgressWindow.Maximum++;
                }
            }
            p.ProgressWindow.Maximum+=1;
            p.ProgressWindow.Maximum+=2;
            for (int i = 0; i < foundationPileModel.GroupFoundationModels.Count; i++)
            {
                for (int j = 0; j < foundationPileModel.GroupFoundationModels[i].FoundationModels.Count; j++)
                {
                    p.ProgressWindow.Maximum++;
                    if (foundationPileModel.GroupFoundationModels[i].FoundationModels[j].IsRepresentative)
                    {
                        p.ProgressWindow.Maximum++;
                    }
                    p.ProgressWindow.Maximum+=2;
                }
            }

        }
        private static void SetValue(FoundationPileWindow p, FoundationPileModel foundationPileModel, int n)
        {
            foundationPileModel.Value += n;
            foundationPileModel.Percent = foundationPileModel.Value / p.ProgressWindow.Maximum * 100;
            p.ProgressWindow.Dispatcher.Invoke(() => p.ProgressWindow.Value = foundationPileModel.Value,
                DispatcherPriority.Background);
        }
        private static void ResetValue(FoundationPileWindow p, FoundationPileModel foundationPileModel)
        {
            foundationPileModel.Value = 0; ;
            foundationPileModel.Percent = 0;
            p.ProgressWindow.Dispatcher.Invoke(() => p.ProgressWindow.Value = foundationPileModel.Value,
                DispatcherPriority.Background);
        }
    }
}
