

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Windows.Threading;
using System.Windows.Controls;
using WpfCustomControls;
using DSP;
namespace R02_BeamsRebar
{
    public class CreateViewDimension
    {
        public static void Create(BeamsWindow p,BeamsModel BeamsModel, UIDocument UiDoc, Document document, List<Element> beams, UnitProject unit)
        {

            ProgressBar uc = VisualTreeHelper.FindChild<ProgressBar>(p, "Progress");
            uc.Maximum = GetProgressBarViewDimension(beams,BeamsModel) * 1.0;
            double hMax = BeamsModel.GetHmax();
            double dsmax = ProcessInfoBeamRebar.GetDiameterStirrupMax(BeamsModel.InfoModels, BeamsModel.DistributeStirrups, BeamsModel.StirrupModels);
            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start(ActionDimension);
                #region View Secion
                BeamsModel.DetailBeamView.CeateDetailView(document, unit, BeamsModel.InfoModels[0],BeamsModel.PlanarFaces, beams, BeamsModel.SettingModel, BeamsModel.SettingModel.DetailViewName, 2 * BeamsModel.SettingModel.L1);
                BeamsModel.ProgressModel.SetValue(uc, 1);

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
                    BeamsModel.ProgressModel.SetValue(uc, 1); BeamsModel.ProgressModel.SetValue(uc, 1); BeamsModel.ProgressModel.SetValue(uc, 1);
                   
                }
               
                #endregion
                #region Dimention view section
                BeamsModel.DimensionView.CreateDimensionHorizontalDetail(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.PlanarFaces, BeamsModel.SettingModel, BeamsModel.InfoModels, false, (BeamsModel.SpecialNodeModels.Count == 0) ? BeamsModel.SettingModel.L1 * 2 : BeamsModel.SettingModel.L1 * 3);
                BeamsModel.ProgressModel.SetValue(uc, 1);
                for (int i = 0; i < BeamsModel.InfoModels.Count; i++)
                {
                    
                    BeamsModel.DimensionView.CreateDimensionVerticalSection(BeamsModel.SectionBeamViews[i].StartView, document, unit, BeamsModel.InfoModels[i].TopBottomPlanar, BeamsModel.InfoModels[i].LeftRightPlanar, BeamsModel.SettingModel, BeamsModel.InfoModels);
                    BeamsModel.DimensionView.CreateDimensionVerticalSection(BeamsModel.SectionBeamViews[i].MidView, document, unit, BeamsModel.InfoModels[i].TopBottomPlanar, BeamsModel.InfoModels[i].LeftRightPlanar, BeamsModel.SettingModel, BeamsModel.InfoModels);
                    BeamsModel.DimensionView.CreateDimensionVerticalSection(BeamsModel.SectionBeamViews[i].EndView, document, unit, BeamsModel.InfoModels[i].TopBottomPlanar, BeamsModel.InfoModels[i].LeftRightPlanar, BeamsModel.SettingModel, BeamsModel.InfoModels);
                    BeamsModel.DimensionView.CreateDimensionHorizontalSection(BeamsModel.SectionBeamViews[i].StartView, document, unit, BeamsModel.InfoModels[i].LeftRightPlanar, BeamsModel.InfoModels[i].TopBottomPlanar, BeamsModel.SettingModel, BeamsModel.InfoModels);
                    BeamsModel.DimensionView.CreateDimensionHorizontalSection(BeamsModel.SectionBeamViews[i].MidView, document, unit, BeamsModel.InfoModels[i].LeftRightPlanar, BeamsModel.InfoModels[i].TopBottomPlanar, BeamsModel.SettingModel, BeamsModel.InfoModels);
                    BeamsModel.DimensionView.CreateDimensionHorizontalSection(BeamsModel.SectionBeamViews[i].EndView, document, unit, BeamsModel.InfoModels[i].LeftRightPlanar, BeamsModel.InfoModels[i].TopBottomPlanar, BeamsModel.SettingModel, BeamsModel.InfoModels);
                    BeamsModel.ProgressModel.SetValue(uc, 1); BeamsModel.ProgressModel.SetValue(uc, 1); BeamsModel.ProgressModel.SetValue(uc, 1); BeamsModel.ProgressModel.SetValue(uc, 1); BeamsModel.ProgressModel.SetValue(uc, 1); BeamsModel.ProgressModel.SetValue(uc, 1);

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
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                    }
                    BeamsModel.DimensionView.CreateDimensionHorizontalAddTopBar(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.PlanarFaces, BeamsModel.ReferenceSpecialNode, BeamsModel.SettingModel, BeamsModel.InfoModels, false, 2 * BeamsModel.SettingModel.L1);
                }
                #endregion
                #region Set Parameter Beam
               
                for (int i = 0; i < beams.Count; i++)
                {
                   
                    BeamsModel.SettingModel.SelectedParameters.Set(BeamsModel.SettingModel.DetailViewName);
                    BeamsModel.ProgressModel.SetValue(uc, 1);
                }
                #endregion
                transaction.Commit();
                BeamsModel.IsCreateViewDimension = true;
                BeamsModel.ProgressModel.ResetValue(uc);
            }
        }
        private static int GetProgressBarViewDimension(List<Element> Beams, BeamsModel BeamsModel)
        {
            int a = 0;
            a = 0;
            #region View Secion
           a += 1;

            for (int i = 0; i < BeamsModel.InfoModels.Count; i++)
            {
                a+= 3;
            }
            #endregion
            a += 1;
            #region Dimention view section

            for (int i = 0; i < BeamsModel.InfoModels.Count; i++)
            {
               a += 6;
            }
            #endregion
            #region Dimension Special Node
            if (BeamsModel.SpecialNodeModels.Count != 0)
            {

                for (int i = 0; i < BeamsModel.SpecialNodeModels.Count; i++)
                {
                   a += 1;
                }

            }
            #endregion
            #region Set Parameter Beam
            for (int i = 0; i < Beams.Count; i++)
            {
                a += 1;
            }
            #endregion
            return a;
        }
       
        private static string ActionDimension = "Create Detail Beams";
        
    }
}
