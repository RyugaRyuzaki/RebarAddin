

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Windows.Threading;

namespace R02_BeamsRebar
{
    public class CreateViewDimension
    {
        public static void Create(BeamsWindow p,BeamsModel BeamsModel, UIDocument UiDoc, Document document, List<Element> beams, UnitProject unit)
        {
            
            GetProgressBarViewDimension(p, beams, BeamsModel);
            double hMax = BeamsModel.GetHmax();
            double dsmax = ProcessInfoBeamRebar.GetDiameterStirrupMax(BeamsModel.InfoModels, BeamsModel.DistributeStirrups, BeamsModel.StirrupModels);
            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start(ActionDimension);
                BeamsModel.SelectedAction = ActionDimension;
                #region View Secion
                BeamsModel.DetailBeamView.CeateDetailView(document, unit, BeamsModel.InfoModels[0],BeamsModel.PlanarFaces, beams, BeamsModel.SettingModel, BeamsModel.SettingModel.DetailViewName, 2 * BeamsModel.SettingModel.L1);
                SetValue(p, 1, BeamsModel);
               
                for (int i = 0; i < BeamsModel.InfoModels.Count; i++)
                {
                    BeamsModel.SectionBeamViews.Add(new SectionBeamView());
                }
                for (int i = 0; i < BeamsModel.InfoModels.Count; i++)
                {
                    
                    BeamsModel.SectionBeamViews[i].CeateSectionView(document, BeamsModel.InfoModels[i], BeamsModel.SettingModel);
                    BeamsModel.SectionBeamViews[i].StartView.Name = BeamsModel.SectionAreaModels[i].NameStart;
                    BeamsModel.SectionBeamViews[i].MidView.Name = BeamsModel.SectionAreaModels[i].NameMiddle;
                    BeamsModel.SectionBeamViews[i].EndView.Name = BeamsModel.SectionAreaModels[i].NamelEnd;
                    SetValue(p, 1, BeamsModel);
                    SetValue(p, 1, BeamsModel);
                    SetValue(p, 1, BeamsModel);
                   
                }
               
                #endregion
                #region Dimention view section
                BeamsModel.DimensionView.CreateDimensionHorizontalDetail(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.PlanarFaces, BeamsModel.SettingModel, BeamsModel.InfoModels, false, (BeamsModel.SpecialNodeModels.Count == 0) ? BeamsModel.SettingModel.L1 * 2 : BeamsModel.SettingModel.L1 * 3);
                
                SetValue(p, 1, BeamsModel);
                for (int i = 0; i < BeamsModel.InfoModels.Count; i++)
                {
                    
                    BeamsModel.DimensionView.CreateDimensionVerticalSection(BeamsModel.SectionBeamViews[i].StartView, document, unit, BeamsModel.InfoModels[i].TopBottomPlanar, BeamsModel.InfoModels[i].LeftRightPlanar, BeamsModel.SettingModel, BeamsModel.InfoModels);
                    BeamsModel.DimensionView.CreateDimensionVerticalSection(BeamsModel.SectionBeamViews[i].MidView, document, unit, BeamsModel.InfoModels[i].TopBottomPlanar, BeamsModel.InfoModels[i].LeftRightPlanar, BeamsModel.SettingModel, BeamsModel.InfoModels);
                    BeamsModel.DimensionView.CreateDimensionVerticalSection(BeamsModel.SectionBeamViews[i].EndView, document, unit, BeamsModel.InfoModels[i].TopBottomPlanar, BeamsModel.InfoModels[i].LeftRightPlanar, BeamsModel.SettingModel, BeamsModel.InfoModels);
                    BeamsModel.DimensionView.CreateDimensionHorizontalSection(BeamsModel.SectionBeamViews[i].StartView, document, unit, BeamsModel.InfoModels[i].LeftRightPlanar, BeamsModel.InfoModels[i].TopBottomPlanar, BeamsModel.SettingModel, BeamsModel.InfoModels);
                    BeamsModel.DimensionView.CreateDimensionHorizontalSection(BeamsModel.SectionBeamViews[i].MidView, document, unit, BeamsModel.InfoModels[i].LeftRightPlanar, BeamsModel.InfoModels[i].TopBottomPlanar, BeamsModel.SettingModel, BeamsModel.InfoModels);
                    BeamsModel.DimensionView.CreateDimensionHorizontalSection(BeamsModel.SectionBeamViews[i].EndView, document, unit, BeamsModel.InfoModels[i].LeftRightPlanar, BeamsModel.InfoModels[i].TopBottomPlanar, BeamsModel.SettingModel, BeamsModel.InfoModels);
                    SetValue(p, 1, BeamsModel);
                    SetValue(p, 1, BeamsModel);
                    SetValue(p, 1, BeamsModel);
                    SetValue(p, 1, BeamsModel);
                    SetValue(p, 1, BeamsModel);
                    SetValue(p, 1, BeamsModel);
                   
                }
                #endregion
                #region Dimension Special Node
                if (BeamsModel.SpecialNodeModels.Count != 0)
                {
                    BeamsModel.ReferenceSpecialNode = BeamsModel.DimensionView.GetReferenceArray(document, BeamsModel.PlanarFaces);
                    for (int i = 0; i < BeamsModel.SpecialNodeModels.Count; i++)
                    {
                        Reference start = BeamsModel.DimensionView.ChangeReference(document, BeamsModel.SpecialNodeModels[i].StartPlanarFace);
                        Reference end = BeamsModel.DimensionView.ChangeReference(document, BeamsModel.SpecialNodeModels[i].EndPlanarFace);
                        BeamsModel.ReferenceSpecialNode.Append(start);
                        BeamsModel.ReferenceSpecialNode.Append(end);
                        SetValue(p, 1, BeamsModel);
                    }
                    BeamsModel.DimensionView.CreateDimensionHorizontalAddTopBar(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.PlanarFaces, BeamsModel.ReferenceSpecialNode, BeamsModel.SettingModel, BeamsModel.InfoModels, false, 2 * BeamsModel.SettingModel.L1);
                }
                #endregion
                #region Set Parameter Beam
               
                for (int i = 0; i < beams.Count; i++)
                {
                   
                    BeamsModel.SettingModel.SelectedParameters.Set(BeamsModel.SettingModel.DetailViewName);
                    SetValue(p, 1, BeamsModel);
                }
                #endregion
                transaction.Commit();
                
            }
        }
        private static void GetProgressBarViewDimension(BeamsWindow p, List<Element> Beams, BeamsModel BeamsModel)
        {
            BeamsModel.Value = 0;
            p.ProgressWindow.Maximum = 0;
            #region View Secion
            p.ProgressWindow.Maximum += 1;

            for (int i = 0; i < BeamsModel.InfoModels.Count; i++)
            {
                p.ProgressWindow.Maximum += 3;
            }
            #endregion
            p.ProgressWindow.Maximum += 1;
            #region Dimention view section

            for (int i = 0; i < BeamsModel.InfoModels.Count; i++)
            {
                p.ProgressWindow.Maximum += 6;
            }
            #endregion
            #region Dimension Special Node
            if (BeamsModel.SpecialNodeModels.Count != 0)
            {

                for (int i = 0; i < BeamsModel.SpecialNodeModels.Count; i++)
                {
                    p.ProgressWindow.Maximum += 1;
                }

            }
            #endregion
            #region Set Parameter Beam
            for (int i = 0; i < Beams.Count; i++)
            {
                p.ProgressWindow.Maximum += 1;
            }
            #endregion
        }
        private static void SetValue(BeamsWindow p, int n, BeamsModel BeamsModel)
        {
            BeamsModel.Value += n;
            BeamsModel.Percent = BeamsModel.Value / p.ProgressWindow.Maximum * 100;
            p.ProgressWindow.Dispatcher.Invoke(() => p.ProgressWindow.Value = BeamsModel.Value,
                DispatcherPriority.Background);
        }
        private static string ActionDimension = "Create Detail Beams";
        
    }
}
