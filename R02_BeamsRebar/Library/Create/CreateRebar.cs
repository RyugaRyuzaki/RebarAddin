
using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using System.Windows.Controls;
using WpfCustomControls;
using DSP;
namespace R02_BeamsRebar
{
    public class CreateRebar
    {
        #region Create
        public static void Create(string action, BeamsWindow p, BeamsModel BeamsModel, Document document, UnitProject unit)
        {
            ProgressBar uc = VisualTreeHelper.FindChild<ProgressBar>(p, "Progress");
            uc.Maximum = GetProgressBarRebar(BeamsModel) * 1.0;
            double dsmax = ProcessInfoBeamRebar.GetDiameterStirrupMax(BeamsModel.InfoModels, BeamsModel.DistributeStirrups, BeamsModel.StirrupModels);
            CreateStirrupBar(action, uc, BeamsModel, document, unit, dsmax);
            CreateDimensionStirrupBar(action, uc, BeamsModel, document, unit, dsmax);
            CreateMainTopBar(action, uc, BeamsModel, document, unit, dsmax);
            CreateMainBottomBar(action, uc, BeamsModel, document, unit, dsmax);
            CreateAddTopBar(action, uc, BeamsModel, document, unit, dsmax);
            CreateAddBottomBar(action, uc, BeamsModel, document, unit, dsmax);
            CreateDimensionAddBottomBar(action, uc, BeamsModel, document, unit, dsmax);
            CreateSideBar(action, uc, BeamsModel, document, unit, dsmax);
            CreateSpecialBar(action, uc, BeamsModel, document, unit, dsmax);
            BeamsModel.IsCreateRebar = true;
            BeamsModel.ProgressModel.ResetValue(uc);
            #region Dimension Add Top Bar

            //using (Transaction transaction = new Transaction(document))
            //{
            //    transaction.Start(ActionRebar[6]);
            //    BeamsModel.SelectedAction = ActionRebar[6];
            //    BeamsModel.ReferenceAddTopBar = BeamsModel.DimensionView.GetReferenceArray(document, BeamsModel.PlanarFaces);
            //    if (BeamsModel.SelectedIndexModel.StartTopChecked)
            //    {
            //        BeamsModel.ReferenceAddTopBar.Append(BeamsModel.AddTopBarModel.Start.Model[0].GetAddTopRightReference(BeamsModel.DetailBeamView.DetailView, document, BeamsModel.PlanarFaces[0], unit, BeamsModel.Cover, dsmax, BeamsModel.InfoModels[0]));

            //    }
            //    if (BeamsModel.SelectedIndexModel.EndTopChecked)
            //    {
            //        BeamsModel.ReferenceAddTopBar.Append(BeamsModel.AddTopBarModel.End.Model[0].GetAddTopLeftReference(BeamsModel.DetailBeamView.DetailView, document, BeamsModel.PlanarFaces[0], unit, BeamsModel.Cover, dsmax, BeamsModel.InfoModels[0]));

            //    }
            //    if (BeamsModel.InfoModels.Count > 1)
            //    {
            //        if (BeamsModel.AddTopBarModel.Mid.Count != 0)
            //        {
            //            for (int i = 0; i < BeamsModel.AddTopBarModel.Mid.Count; i++)
            //            {
            //                if (BeamsModel.AddTopBarModel.Mid[i].Model.Count != 0)
            //                {
            //                    BeamsModel.ReferenceAddTopBar.Append(BeamsModel.AddTopBarModel.Mid[i].Model[0].GetAddTopRightReference(BeamsModel.DetailBeamView.DetailView, document, BeamsModel.PlanarFaces[0], unit, BeamsModel.Cover, dsmax, BeamsModel.InfoModels[0]));
            //                    BeamsModel.ReferenceAddTopBar.Append(BeamsModel.AddTopBarModel.Mid[i].Model[0].GetAddTopLeftReference(BeamsModel.DetailBeamView.DetailView, document, BeamsModel.PlanarFaces[0], unit, BeamsModel.Cover, dsmax, BeamsModel.InfoModels[0]));

            //                }
            //            }
            //        }
            //    }
            //    BeamsModel.DimensionView.CreateDimensionHorizontalAddTopBar(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.PlanarFaces, BeamsModel.ReferenceAddTopBar, BeamsModel.SettingModel, BeamsModel.InfoModels, true, BeamsModel.SettingModel.L1);
            //    SetValue(p, 1, BeamsModel);
            //    transaction.Commit();
            //}


            #endregion
        }
        #endregion
        #region Create Item
        private static void CreateStirrupBar(string action, ProgressBar uc, BeamsModel BeamsModel, Document document, UnitProject unit,double dsmax)
        {
           
            bool hasSpecial = (BeamsModel.SpecialBarModel.Count != 0);
            BeamsModel.ReferenceStirrupBar = BeamsModel.DimensionView.GetReferenceArray(document, BeamsModel.PlanarFaces);
            for (int i = 0; i < BeamsModel.InfoModels.Count; i++)
            {
                SpecialBarModel a = null;
                if (hasSpecial)
                {
                    a = BeamsModel.SpecialBarModel.Where(x => x.Span == BeamsModel.InfoModels[i].NumberSpan).FirstOrDefault();
                }
                bool special = (hasSpecial && (a != null) && (a.IsST));
                double start = 0;
                double end = 0;
                SpecialNodeModel b = null;
                if (special)
                {
                    b = BeamsModel.SpecialNodeModels.Where(x => x.NumberSpan == a.Span).FirstOrDefault();
                    start = b.Mid - a.L3 / 2 - a.a * a.NumberST / 2 - BeamsModel.InfoModels[i].startPosition;
                    end = b.Mid + a.L3 / 2 - BeamsModel.InfoModels[i].startPosition + a.a * a.NumberST / 2;
                }

                using (Transaction transaction = new Transaction(document))
                {
                    transaction.Start(ActionRebar[0]);
                    BeamsModel.StirrupModels[i].CreateStirrup(BeamsModel.InfoModels[i], BeamsModel.DistributeStirrups[i], document, unit, BeamsModel.SettingModel, special, start, end);
                    BeamsModel.StirrupModels[i].CeateAnti(BeamsModel.InfoModels[i], document, unit, BeamsModel.SettingModel);
                    BeamsModel.ProgressModel.SetValue(uc,1);
                    BeamsModel.StirrupModels[i].CreateTagStirrupDetail(BeamsModel.DetailBeamView.DetailView, BeamsModel.InfoModels[i], BeamsModel.DistributeStirrups[i], document, unit, BeamsModel.SettingModel, BeamsModel.SettingModel.L1 * 0.85, special, start, end);
                    BeamsModel.ProgressModel.SetValue(uc, 1);
                    BeamsModel.StirrupModels[i].CreateTagStirrupSection(BeamsModel.SectionBeamViews[i].StartView, BeamsModel.SectionBeamViews[i].MidView, BeamsModel.SectionBeamViews[i].EndView, BeamsModel.InfoModels[i], BeamsModel.DistributeStirrups[i], document, unit, BeamsModel.SettingModel, special, start, end);
                    BeamsModel.ProgressModel.SetValue(uc, 1); BeamsModel.ProgressModel.SetValue(uc, 1);
                    FailureHandlingOptions option = transaction.GetFailureHandlingOptions();
                    option.SetFailuresPreprocessor(new DeleteWarningSuper());
                    transaction.SetFailureHandlingOptions(option);
                    BeamsModel.ReferenceStirrupBar.Append(BeamsModel.StirrupModels[i].GetReferenceItem(BeamsModel.DetailBeamView.DetailView, document, BeamsModel.InfoModels[i], BeamsModel.DistributeStirrups[i], BeamsModel.PlanarFaces[0], unit, BeamsModel.DistributeStirrups[i].L1));
                    BeamsModel.ReferenceStirrupBar.Append(BeamsModel.StirrupModels[i].GetReferenceItem(BeamsModel.DetailBeamView.DetailView, document, BeamsModel.InfoModels[i], BeamsModel.DistributeStirrups[i], BeamsModel.PlanarFaces[0], unit, BeamsModel.DistributeStirrups[i].L1 + BeamsModel.DistributeStirrups[i].L2));
                    BeamsModel.ProgressModel.SetValue(uc, 1);
                    transaction.Commit();
                }

            }
        }
        private static void CreateDimensionStirrupBar(string action, ProgressBar uc, BeamsModel BeamsModel, Document document, UnitProject unit, double dsmax)
        {
            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start(ActionRebar[1]);
                BeamsModel.DimensionView.CreateDimensionHorizontalAddTopBar(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.PlanarFaces, BeamsModel.ReferenceStirrupBar, BeamsModel.SettingModel, BeamsModel.InfoModels, true, BeamsModel.SettingModel.L1);
                BeamsModel.ProgressModel.SetValue(uc, 1);
                transaction.Commit();
            }
        }
        private static void CreateMainTopBar(string action, ProgressBar uc, BeamsModel BeamsModel, Document document, UnitProject unit, double dsmax)
        {
            
            if (BeamsModel.SelectedIndexModel.StyleMainTop == 0)
            {
                using (Transaction transaction = new Transaction(document))
                {
                    transaction.Start(ActionRebar[2]);
                    BeamsModel.SingleMainTopBarModel.CreateMaintopBar(document, BeamsModel.PlanarFaces[0], unit, BeamsModel.Cover, dsmax, BeamsModel.InfoModels[0], BeamsModel.SettingModel);
                    BeamsModel.ProgressModel.SetValue(uc, 1);
                    FailureHandlingOptions option = transaction.GetFailureHandlingOptions();
                    option.SetFailuresPreprocessor(new DeleteWarningSuper());
                    transaction.SetFailureHandlingOptions(option);
                    BeamsModel.SingleMainTopBarModel.CreateTagRebarDetailMainTop(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.InfoModels, BeamsModel.SettingModel, BeamsModel.PlanarFaces[0], BeamsModel.SettingModel.TagH, BeamsModel.SettingModel.TagV);
                    BeamsModel.ProgressModel.SetValue(uc, 1);
                    BeamsModel.SingleMainTopBarModel.CreateTagRebarSection(BeamsModel.SectionBeamViews, document, unit, BeamsModel.InfoModels, BeamsModel.PlanarFaces[0], BeamsModel.SettingModel, BeamsModel.SettingModel.TagH, BeamsModel.SettingModel.TagV);
                    BeamsModel.ProgressModel.SetValue(uc, 1);
                    transaction.Commit();
                }

            }
            else
            {
                for (int i = 0; i < BeamsModel.MainTopBarModel.Count; i++)
                {
                    using (Transaction transaction = new Transaction(document))
                    {
                        transaction.Start(ActionRebar[2]);
                        BeamsModel.MainTopBarModel[i].CreateMaintopBar(document, BeamsModel.PlanarFaces[0], unit, BeamsModel.Cover, dsmax, BeamsModel.InfoModels[0], BeamsModel.SettingModel);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        FailureHandlingOptions option = transaction.GetFailureHandlingOptions();
                        option.SetFailuresPreprocessor(new DeleteWarningSuper());
                        transaction.SetFailureHandlingOptions(option);
                        BeamsModel.MainTopBarModel[i].CreateTagRebarDetailMainTop(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.InfoModels, BeamsModel.SettingModel, BeamsModel.PlanarFaces[0], BeamsModel.SettingModel.TagH, BeamsModel.SettingModel.TagV);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        BeamsModel.MainTopBarModel[i].CreateTagRebarSection(BeamsModel.SectionBeamViews, document, unit, BeamsModel.InfoModels, BeamsModel.PlanarFaces[0], BeamsModel.SettingModel, BeamsModel.SettingModel.TagH, BeamsModel.SettingModel.TagV);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        transaction.Commit();
                    }
                }
            }
        }
        private static void CreateMainBottomBar(string action, ProgressBar uc, BeamsModel BeamsModel, Document document, UnitProject unit, double dsmax)
        {
            for (int i = 0; i < BeamsModel.MainBottomBarModel.Count; i++)
            {
                using (Transaction transaction = new Transaction(document))
                {
                    transaction.Start(ActionRebar[4]);
                    
                    BeamsModel.MainBottomBarModel[i].CreateMainBottomBar(document, BeamsModel.PlanarFaces[0], unit, BeamsModel.Cover, dsmax, BeamsModel.InfoModels[0], BeamsModel.SettingModel);
                    BeamsModel.ProgressModel.SetValue(uc, 1);
                    FailureHandlingOptions option = transaction.GetFailureHandlingOptions();
                    option.SetFailuresPreprocessor(new DeleteWarningSuper());
                    transaction.SetFailureHandlingOptions(option);
                    BeamsModel.MainBottomBarModel[i].CreateTagRebarDetailMainBottom(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.InfoModels, BeamsModel.SettingModel, BeamsModel.PlanarFaces[0], BeamsModel.SettingModel.TagH, BeamsModel.SettingModel.TagV);
                    BeamsModel.ProgressModel.SetValue(uc, 1);
                    BeamsModel.MainBottomBarModel[i].CreateTagRebarSection(BeamsModel.SectionBeamViews, document, unit, BeamsModel.InfoModels, BeamsModel.PlanarFaces[0], BeamsModel.SettingModel, BeamsModel.SettingModel.TagH, BeamsModel.SettingModel.TagV);
                    BeamsModel.ProgressModel.SetValue(uc, 1);
                    transaction.Commit();
                }

            }
        }
        private static void CreateAddTopBar(string action, ProgressBar uc, BeamsModel BeamsModel, Document document, UnitProject unit, double dsmax)
        {
            if (BeamsModel.SelectedIndexModel.StartTopChecked)
            {
                using (Transaction transaction = new Transaction(document))
                {
                    transaction.Start(ActionRebar[4]);
                    
                    BeamsModel.AddTopBarModel.Start.CreateBar(document, BeamsModel.PlanarFaces[0], unit, BeamsModel.Cover, dsmax, BeamsModel.InfoModels[0], BeamsModel.SettingModel);
                    BeamsModel.ProgressModel.SetValue(uc, 1);
                    FailureHandlingOptions option = transaction.GetFailureHandlingOptions();
                    option.SetFailuresPreprocessor(new DeleteWarningSuper());
                    transaction.SetFailureHandlingOptions(option);
                    BeamsModel.AddTopBarModel.Start.CreateTagRebarDetailAddTopStart(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.InfoModels[0], BeamsModel.NodeModels[0], BeamsModel.SettingModel, BeamsModel.PlanarFaces[0], BeamsModel.SettingModel.TagH, BeamsModel.SettingModel.TagV);
                    BeamsModel.ProgressModel.SetValue(uc, 1);
                    BeamsModel.AddTopBarModel.Start.CreateTagRebarSectionAddTopStart(BeamsModel.SectionBeamViews[0], document, unit, BeamsModel.InfoModels[0], BeamsModel.PlanarFaces[0], BeamsModel.SettingModel, BeamsModel.SettingModel.TagH, -BeamsModel.SettingModel.tmin / 2);
                    BeamsModel.ProgressModel.SetValue(uc, 1);
                    transaction.Commit();
                }
            }
            if (BeamsModel.SelectedIndexModel.EndTopChecked)
            {
                using (Transaction transaction = new Transaction(document))
                {
                    transaction.Start(ActionRebar[4]);
                    BeamsModel.AddTopBarModel.End.CreateBar(document, BeamsModel.PlanarFaces[0], unit, BeamsModel.Cover, dsmax, BeamsModel.InfoModels[0], BeamsModel.SettingModel);
                    BeamsModel.ProgressModel.SetValue(uc, 1);
                    FailureHandlingOptions option = transaction.GetFailureHandlingOptions();
                    option.SetFailuresPreprocessor(new DeleteWarningSuper());
                    transaction.SetFailureHandlingOptions(option);
                    BeamsModel.AddTopBarModel.End.CreateTagRebarDetailAddTopEnd(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.InfoModels[0], BeamsModel.NodeModels[BeamsModel.NodeModels.Count - 1], BeamsModel.SettingModel, BeamsModel.PlanarFaces[0], BeamsModel.SettingModel.TagH, BeamsModel.SettingModel.TagV);
                    BeamsModel.ProgressModel.SetValue(uc, 1);
                    BeamsModel.AddTopBarModel.End.CreateTagRebarSectionAddTopEnd(BeamsModel.SectionBeamViews[BeamsModel.SectionBeamViews.Count - 1], document, unit, BeamsModel.InfoModels[0], BeamsModel.PlanarFaces[0], BeamsModel.SettingModel, BeamsModel.SettingModel.TagH, -BeamsModel.SettingModel.tmin / 2);
                    BeamsModel.ProgressModel.SetValue(uc, 1);
                    transaction.Commit();
                }

            }
            if (BeamsModel.InfoModels.Count > 1)
            {
                if (BeamsModel.AddTopBarModel.Mid.Count != 0)
                {
                    for (int i = 0; i < BeamsModel.AddTopBarModel.Mid.Count; i++)
                    {
                        using (Transaction transaction = new Transaction(document))
                        {
                            transaction.Start(ActionRebar[4]);
                            BeamsModel.AddTopBarModel.Mid[i].CreateBarMid(document, BeamsModel.PlanarFaces[0], unit, BeamsModel.Cover, dsmax, BeamsModel.InfoModels[0], BeamsModel.SettingModel);
                            BeamsModel.ProgressModel.SetValue(uc, 1);
                            FailureHandlingOptions option = transaction.GetFailureHandlingOptions();
                            option.SetFailuresPreprocessor(new DeleteWarningSuper());
                            transaction.SetFailureHandlingOptions(option);
                            if (BeamsModel.AddTopBarModel.Mid[i].Model.Count != 0)
                            {
                                NodeModel nodeModel = BeamsModel.NodeModels.Where(x => PointModel.AreEqual(x.Mid, BeamsModel.AddTopBarModel.Mid[i].Model[0].X0)).FirstOrDefault();
                                if (nodeModel != null)
                                {
                                    BeamsModel.AddTopBarModel.Mid[i].CreateTagRebarDetailAddTopMid(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.InfoModels[0], nodeModel, BeamsModel.SettingModel, BeamsModel.PlanarFaces[0], BeamsModel.SettingModel.TagH, BeamsModel.SettingModel.TagV);
                                    BeamsModel.ProgressModel.SetValue(uc, 1);
                                    BeamsModel.AddTopBarModel.Mid[i].CreateTagRebarSectionAddTopMid(BeamsModel.SectionBeamViews[i], BeamsModel.SectionBeamViews[i + 1], document, unit, BeamsModel.InfoModels[0], BeamsModel.PlanarFaces[0], BeamsModel.SettingModel, BeamsModel.SettingModel.TagH, -BeamsModel.SettingModel.tmin / 2);
                                    BeamsModel.ProgressModel.SetValue(uc, 1);
                                }
                            }
                            transaction.Commit();
                        }

                    }
                }
            }
        }
        private static void CreateAddBottomBar(string action, ProgressBar uc, BeamsModel BeamsModel, Document document, UnitProject unit, double dsmax)
        {
            for (int i = 0; i < BeamsModel.AddBottomBarModel.Count; i++)
            {
                using (Transaction transaction = new Transaction(document))
                {
                    transaction.Start(ActionRebar[5]);
                   
                    if (BeamsModel.SelectedBottomModels[i].StartBottomChecked)
                    {
                        BeamsModel.AddBottomBarModel[i].Start.CreateBar(document, BeamsModel.PlanarFaces[0], unit, BeamsModel.Cover, dsmax, BeamsModel.InfoModels[0], BeamsModel.SettingModel);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        FailureHandlingOptions option = transaction.GetFailureHandlingOptions();
                        option.SetFailuresPreprocessor(new DeleteWarningSuper());
                        transaction.SetFailureHandlingOptions(option);
                        BeamsModel.AddBottomBarModel[i].Start.CreateTagRebarDetailAddBottomStart(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.InfoModels[0], BeamsModel.InfoModels[i], BeamsModel.SettingModel, BeamsModel.PlanarFaces[0], BeamsModel.SettingModel.TagH, BeamsModel.SettingModel.TagV);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        BeamsModel.AddBottomBarModel[i].Start.CreateTagRebarSectionAddBottomStart(BeamsModel.SectionBeamViews[i], document, unit, BeamsModel.InfoModels[0], BeamsModel.PlanarFaces[0], BeamsModel.SettingModel, BeamsModel.SettingModel.TagH, BeamsModel.SettingModel.tmin / 2);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                    }
                    if (BeamsModel.SelectedBottomModels[i].EndBottomChecked)
                    {
                        BeamsModel.AddBottomBarModel[i].End.CreateBar(document, BeamsModel.PlanarFaces[0], unit, BeamsModel.Cover, dsmax, BeamsModel.InfoModels[0], BeamsModel.SettingModel);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        FailureHandlingOptions option = transaction.GetFailureHandlingOptions();
                        option.SetFailuresPreprocessor(new DeleteWarningSuper());
                        transaction.SetFailureHandlingOptions(option);
                        BeamsModel.AddBottomBarModel[i].End.CreateTagRebarDetailAddBottomEnd(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.InfoModels[0], BeamsModel.InfoModels[i], BeamsModel.SettingModel, BeamsModel.PlanarFaces[0], BeamsModel.SettingModel.TagH, BeamsModel.SettingModel.TagV);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        BeamsModel.AddBottomBarModel[i].Start.CreateTagRebarSectionAddBottomEnd(BeamsModel.SectionBeamViews[i], document, unit, BeamsModel.InfoModels[0], BeamsModel.PlanarFaces[0], BeamsModel.SettingModel, BeamsModel.SettingModel.TagH, BeamsModel.SettingModel.tmin / 2);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                    }
                    if (BeamsModel.AddBottomBarModel[i].Mid.Model.Count != 0)
                    {
                        BeamsModel.AddBottomBarModel[i].Mid.CreateBarMid(document, BeamsModel.PlanarFaces[0], unit, BeamsModel.Cover, dsmax, BeamsModel.InfoModels[0], BeamsModel.SettingModel);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        FailureHandlingOptions option = transaction.GetFailureHandlingOptions();
                        option.SetFailuresPreprocessor(new DeleteWarningSuper());
                        transaction.SetFailureHandlingOptions(option);
                        BeamsModel.AddBottomBarModel[i].Mid.CreateTagRebarDetailAddBottomMid(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.InfoModels[0], BeamsModel.InfoModels[i], BeamsModel.SettingModel, BeamsModel.PlanarFaces[0], BeamsModel.SettingModel.TagH, BeamsModel.SettingModel.TagV);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        BeamsModel.AddBottomBarModel[i].Start.CreateTagRebarSectionAddBottomMid(BeamsModel.SectionBeamViews[i], document, unit, BeamsModel.InfoModels[0], BeamsModel.PlanarFaces[0], BeamsModel.SettingModel, BeamsModel.SettingModel.TagH, BeamsModel.SettingModel.tmin / 2);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                    }
                    transaction.Commit();
                }

            }
        }
        private static void CreateDimensionAddBottomBar(string action, ProgressBar uc, BeamsModel BeamsModel, Document document, UnitProject unit, double dsmax)
        {
            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start(ActionRebar[6]);
                
                BeamsModel.ReferenceAddBottomBar = BeamsModel.DimensionView.GetReferenceArray(document, BeamsModel.PlanarFaces);
                for (int i = 0; i < BeamsModel.AddBottomBarModel.Count; i++)
                {
                    if (BeamsModel.SelectedBottomModels[i].StartBottomChecked)
                    {
                        BeamsModel.ReferenceAddBottomBar.Append(BeamsModel.AddBottomBarModel[i].Start.Model[0].GetAddTopRightReference(BeamsModel.DetailBeamView.DetailView, document, BeamsModel.PlanarFaces[0], unit, BeamsModel.Cover, dsmax, BeamsModel.InfoModels[0]));

                    }
                    if (BeamsModel.SelectedBottomModels[i].EndBottomChecked)
                    {
                        BeamsModel.ReferenceAddBottomBar.Append(BeamsModel.AddBottomBarModel[i].End.Model[0].GetAddTopLeftReference(BeamsModel.DetailBeamView.DetailView, document, BeamsModel.PlanarFaces[0], unit, BeamsModel.Cover, dsmax, BeamsModel.InfoModels[0]));

                    }
                    if (BeamsModel.AddBottomBarModel[i].Mid.Model.Count != 0)
                    {
                        BeamsModel.ReferenceAddBottomBar.Append(BeamsModel.AddBottomBarModel[i].Mid.Model[0].GetAddTopRightReference(BeamsModel.DetailBeamView.DetailView, document, BeamsModel.PlanarFaces[0], unit, BeamsModel.Cover, dsmax, BeamsModel.InfoModels[0]));
                        BeamsModel.ReferenceAddBottomBar.Append(BeamsModel.AddBottomBarModel[i].Mid.Model[0].GetAddTopLeftReference(BeamsModel.DetailBeamView.DetailView, document, BeamsModel.PlanarFaces[0], unit, BeamsModel.Cover, dsmax, BeamsModel.InfoModels[0]));
                    }
                }
                BeamsModel.DimensionView.CreateDimensionHorizontalAddTopBar(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.PlanarFaces, BeamsModel.ReferenceAddBottomBar, BeamsModel.SettingModel, BeamsModel.InfoModels, false, BeamsModel.SettingModel.L1);
                BeamsModel.ProgressModel.SetValue(uc, 1);
                transaction.Commit();
            }
        }
        private static void CreateSideBar(string action, ProgressBar uc, BeamsModel BeamsModel, Document document, UnitProject unit, double dsmax)
        {
            for (int i = 0; i < BeamsModel.SideBarModel.Count; i++)
            {
                if (BeamsModel.SideBarModel[i].IsSide)
                {
                    using (Transaction transaction = new Transaction(document))
                    {
                        transaction.Start(ActionRebar[7]);
                        BeamsModel.SideBarModel[i].CreateSideBar(document, BeamsModel.PlanarFaces[0], unit, BeamsModel.Cover, dsmax, BeamsModel.InfoModels[0], BeamsModel.SettingModel);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        FailureHandlingOptions option = transaction.GetFailureHandlingOptions();
                        option.SetFailuresPreprocessor(new DeleteWarningSuper());
                        transaction.SetFailureHandlingOptions(option);
                        BeamsModel.SideBarModel[i].CreateTagRebarDetail(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.InfoModels[0], BeamsModel.SettingModel, BeamsModel.PlanarFaces[0], BeamsModel.SettingModel.TagH, BeamsModel.SettingModel.TagV);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        BeamsModel.SideBarModel[i].CreateTagRebarSection(BeamsModel.SectionBeamViews[i], document, unit, BeamsModel.InfoModels[0], BeamsModel.SettingModel, BeamsModel.PlanarFaces[0], BeamsModel.SettingModel.TagH, -BeamsModel.SettingModel.tmin);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        transaction.Commit();
                    }

                }
            }
        }
        private static void CreateSpecialBar(string action, ProgressBar uc, BeamsModel BeamsModel, Document document, UnitProject unit, double dsmax)
        {
            if (BeamsModel.SpecialBarModel.Count != 0)
            {
                for (int i = 0; i < BeamsModel.SpecialBarModel.Count; i++)
                {
                    InfoModel b = BeamsModel.InfoModels.Where(x => x.NumberSpan == BeamsModel.SpecialBarModel[i].Span).FirstOrDefault();
                    if (BeamsModel.SpecialBarModel[i].IsSP)
                    {
                        using (Transaction transaction = new Transaction(document))
                        {
                            transaction.Start(ActionRebar[8]);
                            BeamsModel.SpecialBarModel[i].CreateSpecialBar(document, BeamsModel.PlanarFaces[0], unit, BeamsModel.Cover, dsmax, BeamsModel.InfoModels[0], BeamsModel.SettingModel);
                            BeamsModel.ProgressModel.SetValue(uc, 1);
                            FailureHandlingOptions option = transaction.GetFailureHandlingOptions();
                            option.SetFailuresPreprocessor(new DeleteWarningSuper());
                            transaction.SetFailureHandlingOptions(option);
                            double x0 = BeamsModel.SpecialBarModel[i].X0 + BeamsModel.SpecialBarModel[i].L3 / 2 + BeamsModel.SpecialBarModel[i].L2 + BeamsModel.SpecialBarModel[i].L1 / 2;
                            BeamsModel.SpecialBarModel[i].BarSP.CreateTagRebarDetailTop(BeamsModel.DetailBeamView.DetailView, document, unit, BeamsModel.InfoModels[0], BeamsModel.SettingModel, BeamsModel.PlanarFaces[0], x0, BeamsModel.SpecialBarModel[i].Y0, BeamsModel.SettingModel.TagH, BeamsModel.SettingModel.TagV);
                            BeamsModel.ProgressModel.SetValue(uc, 1);
                            transaction.Commit();
                        }

                    }
                    if (BeamsModel.SpecialBarModel[i].IsST && b != null)
                    {
                        using (Transaction transaction = new Transaction(document))
                        {
                            transaction.Start(ActionRebar[8]);
                            BeamsModel.SpecialBarModel[i].CreateStirrupSpecial(document, unit, BeamsModel.Cover, b, BeamsModel.SpecialNodeModels[i], BeamsModel.SettingModel);
                            BeamsModel.ProgressModel.SetValue(uc, 1);
                            FailureHandlingOptions option = transaction.GetFailureHandlingOptions();
                            option.SetFailuresPreprocessor(new DeleteWarningSuper());
                            transaction.SetFailureHandlingOptions(option);
                            BeamsModel.SpecialBarModel[i].CreateTagStirrupDetail(BeamsModel.DetailBeamView.DetailView, document, unit, b, BeamsModel.SettingModel, BeamsModel.SpecialNodeModels[i]);
                            BeamsModel.ProgressModel.SetValue(uc, 1);
                            transaction.Commit();
                        }

                    }
                }
            }
        }
        #endregion
        #region Action
        private static int GetProgressBarStirrupBar( BeamsModel BeamsModel, double dsmax)
        {
            int pro = 0;
            for (int i = 0; i < BeamsModel.InfoModels.Count; i++)
            {
                pro += 4;
            }
            return pro;
        }
        private static int GetProgressBarDimensionStirrupBar( BeamsModel BeamsModel, double dsmax)
        {
            int pro = 1;
            return pro;
        }
        private static int GetProgressBarMainTopBar( BeamsModel BeamsModel, double dsmax)
        {
            int pro = 0;
            if (BeamsModel.SelectedIndexModel.StyleMainTop == 0)
            {
                pro += 3;
            }
            else
            {
                for (int i = 0; i < BeamsModel.MainTopBarModel.Count; i++)
                {
                    pro += 3;
                }
            }
            return pro;
        }
        private static int GetProgressBarMainBottomBar( BeamsModel BeamsModel, double dsmax)
        {
            int pro = 0;
            for (int i = 0; i < BeamsModel.MainBottomBarModel.Count; i++)
            {
                pro += 3;
            }
            return pro;
        }
        private static int GetProgressBarAddTopBar( BeamsModel BeamsModel, double dsmax)
        {
            int pro = 0;
            if (BeamsModel.SelectedIndexModel.StartTopChecked)
            {
                pro += 3;
            }
            if (BeamsModel.SelectedIndexModel.EndTopChecked)
            {
                pro += 3;
            }
            if (BeamsModel.InfoModels.Count > 1)
            {
                if (BeamsModel.AddTopBarModel.Mid.Count != 0)
                {
                    for (int i = 0; i < BeamsModel.AddTopBarModel.Mid.Count; i++)
                    {
                        pro += 1;
                        if (BeamsModel.AddTopBarModel.Mid[i].Model.Count != 0)
                        {
                            NodeModel nodeModel = BeamsModel.NodeModels.Where(x => PointModel.AreEqual(x.Mid, BeamsModel.AddTopBarModel.Mid[i].Model[0].X0)).FirstOrDefault();
                            if (nodeModel != null)
                            {
                                pro += 3;
                            }
                        }
                    }
                }
            }
            return pro;
        }
        private static int GetProgressBarAddBottomBar( BeamsModel BeamsModel, double dsmax)
        {
            int pro = 0;
            for (int i = 0; i < BeamsModel.AddBottomBarModel.Count; i++)
            {
                if (BeamsModel.SelectedBottomModels[i].StartBottomChecked)
                {
                    pro += 3;
                }
                if (BeamsModel.SelectedBottomModels[i].EndBottomChecked)
                {
                    pro += 3;
                }
                if (BeamsModel.AddBottomBarModel[i].Mid.Model.Count != 0)
                {
                    pro += 3;
                }
            }
            return pro;
        }
        private static int GetProgressBarDimensionAddBottomBar( BeamsModel BeamsModel, double dsmax)
        {
            int pro = 1;
           
            return pro;
        }
        private static int GetProgressBarSideBar( BeamsModel BeamsModel, double dsmax)
        {
            int pro = 0;
            for (int i = 0; i < BeamsModel.SideBarModel.Count; i++)
            {
                if (BeamsModel.SideBarModel[i].IsSide)
                {
                    pro += 5;
                }
            }
            return pro;
        }
        private static int GetProgressBarSpecialBar( BeamsModel BeamsModel, double dsmax)
        {
            int pro = 0;
            if (BeamsModel.SpecialBarModel.Count != 0)
            {
                for (int i = 0; i < BeamsModel.SpecialBarModel.Count; i++)
                {
                    InfoModel b = BeamsModel.InfoModels.Where(x => x.NumberSpan == BeamsModel.SpecialBarModel[i].Span).FirstOrDefault();
                    if (BeamsModel.SpecialBarModel[i].IsSP)
                    {
                        pro += 2;
                    }
                    if (BeamsModel.SpecialBarModel[i].IsST && b != null)
                    {
                        pro += 2;
                    }
                }
            }
            return pro;
        }
        private static int GetProgressBarRebar( BeamsModel BeamsModel)
        {
            
            int a = 0;
            double dsmax = ProcessInfoBeamRebar.GetDiameterStirrupMax(BeamsModel.InfoModels, BeamsModel.DistributeStirrups, BeamsModel.StirrupModels);
            #region Stirrup bar
            a += GetProgressBarStirrupBar( BeamsModel, dsmax);
            #endregion
            #region Dimension Stirrup
            a += GetProgressBarDimensionStirrupBar(BeamsModel, dsmax);
            #endregion
            #region Main Top bar
            a += GetProgressBarMainTopBar(BeamsModel, dsmax);
            #endregion
            #region Main Bottom Bar
            a += GetProgressBarMainBottomBar( BeamsModel, dsmax);
            #endregion
            #region Add Top Bar
           a += GetProgressBarAddTopBar( BeamsModel, dsmax);
            #endregion
            #region Dimension Add Top Bar
            //p.ProgressWindow.Maximum += 1;
            #endregion
            #region Add Bottom Bar
            a += GetProgressBarAddBottomBar( BeamsModel, dsmax);
            #endregion
            #region Dimension Add Bottom Bar
            a += GetProgressBarDimensionAddBottomBar( BeamsModel, dsmax);
            #endregion
            #region Side Bar
            a += GetProgressBarSideBar(BeamsModel, dsmax);
            #endregion
            #region Special Bar
            a += GetProgressBarSpecialBar( BeamsModel, dsmax);
            #endregion
            return a;
        }
        
        private static List<string> ActionRebar = new List<string>()
        {
            "Create Stirrup Bars",
            "Create Dimension Stirrup Bars",
            "Create Main-Top Bars",
            "Create Main-Bottom Bars",
            "Create Additional - Top Bars",
            "Create Additional - Bottom Bars",
            "Create Dimension AddBottom Bars",
            "Create Side Bar",
            "Create Special Bars"
        };
        #endregion
    }
}
