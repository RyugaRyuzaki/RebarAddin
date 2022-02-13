
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Windows.Threading;
using WpfCustomControls;
using System.Windows.Controls;
using DSP;
namespace R01_ColumnsRebar
{
    public class CreateViewDimension
    {
        public static void Create(ColumnsWindow p, ColumnsModel ColumnsModel, UIDocument UiDoc, Document document, List<Element> columns, UnitProject unit)
        {
            ProgressBar uc = VisualTreeHelper.FindChild<ProgressBar>(p, "Progress");
            uc.Maximum = GetProgressBarViewDimension(ColumnsModel) * 1.0;

            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start(ActionDimension[0]);
                ColumnsModel.DetailColumnView.CeateDetailView(ColumnsModel.SectionStyle, document, unit, ColumnsModel.InfoModels, ColumnsModel.PlanarFaces, columns, ColumnsModel.SettingModel, ColumnsModel.SettingModel.DetailViewName + "X", ColumnsModel.SettingModel.DetailViewName + "Y", ColumnsModel.SettingModel.L1);
                ColumnsModel.ProgressModel.SetValue(uc, 1);
                transaction.Commit();
                ColumnsModel.IsCreateDetailView = true;
            }
            double offset0 = (ColumnsModel.SectionStyle == ErrorColumns.SectionStyle.RECTANGLE) ? (Math.Max((ColumnsModel.InfoModels[0].b), (ColumnsModel.InfoModels[0].h))) : (ColumnsModel.InfoModels[0].D);
            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start("bbb");
                for (int i = 0; i < ColumnsModel.SectionColumnViews.Count; i++)
                {
                    ColumnsModel.SectionColumnViews[i].CreateSectionView(ColumnsModel.SectionStyle, unit, document, ColumnsModel.InfoModels[i], ColumnsModel.StirrupModels[i], ColumnsModel.SettingModel, offset0);
                    ColumnsModel.ProgressModel.SetValue(uc, 1);
                    ColumnsModel.ProgressModel.SetValue(uc, 1);
                    ColumnsModel.ProgressModel.SetValue(uc, 1);
                }
                
                transaction.Commit();
                ColumnsModel.IsCreateSectionView = true;
            }

            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start(ActionDimension[2]);

                if (ColumnsModel.SectionStyle == ErrorColumns.SectionStyle.RECTANGLE)
                {
                    ColumnsModel.DimensionView.CreateDimensionHorizontalDetailRectangle(ColumnsModel.DetailColumnView.DetailViewX, document, unit, ColumnsModel.PlanarFaces, ColumnsModel.InfoModels, ColumnsModel.SettingModel, 2 * offset0); ColumnsModel.ProgressModel.SetValue(uc, 1);
                    ColumnsModel.DimensionView.CreateDimensionHorizontalDetailRectangle(ColumnsModel.DetailColumnView.DetailViewY, document, unit, ColumnsModel.PlanarFaces, ColumnsModel.InfoModels, ColumnsModel.SettingModel, 2 * offset0); ColumnsModel.ProgressModel.SetValue(uc, 1);
                    ColumnsModel.DimensionView.CreateDimensionHorizontalDetailStirrupRectangle(ColumnsModel.DetailColumnView.DetailViewX, document, unit, ColumnsModel.PlanarFaces, ColumnsModel.InfoModels, ColumnsModel.StirrupModels, ColumnsModel.SettingModel, offset0, true); ColumnsModel.ProgressModel.SetValue(uc, 1);
                    ColumnsModel.DimensionView.CreateDimensionHorizontalDetailStirrupRectangle(ColumnsModel.DetailColumnView.DetailViewY, document, unit, ColumnsModel.PlanarFaces, ColumnsModel.InfoModels, ColumnsModel.StirrupModels, ColumnsModel.SettingModel, offset0, false); ColumnsModel.ProgressModel.SetValue(uc, 1);
                }
                else
                {
                    ColumnsModel.DimensionView.CreateDimensionHorizontalDetailCylindrical(ColumnsModel.DetailColumnView.DetailViewX, document, unit, ColumnsModel.PlanarFaces, ColumnsModel.InfoModels, ColumnsModel.SettingModel, 2 * offset0); ColumnsModel.ProgressModel.SetValue(uc, 1);
                    ColumnsModel.DimensionView.CreateDimensionHorizontalDetailCylindrical(ColumnsModel.DetailColumnView.DetailViewY, document, unit, ColumnsModel.PlanarFaces, ColumnsModel.InfoModels, ColumnsModel.SettingModel, 2 * offset0); ColumnsModel.ProgressModel.SetValue(uc, 1);
                    ColumnsModel.DimensionView.CreateDimensionHorizontalDetailStirrupCylindrical(ColumnsModel.DetailColumnView.DetailViewX, document, unit, ColumnsModel.PlanarFaces, ColumnsModel.InfoModels, ColumnsModel.StirrupModels, ColumnsModel.SettingModel, offset0); ColumnsModel.ProgressModel.SetValue(uc, 1);
                    ColumnsModel.DimensionView.CreateDimensionHorizontalDetailStirrupCylindrical(ColumnsModel.DetailColumnView.DetailViewY, document, unit, ColumnsModel.PlanarFaces, ColumnsModel.InfoModels, ColumnsModel.StirrupModels, ColumnsModel.SettingModel, offset0); ColumnsModel.ProgressModel.SetValue(uc, 1);
                }

                transaction.Commit();
                ColumnsModel.IsCreateDimensionView = true;
            }
            
            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start(ActionDimension[3]);
                for (int i = 0; i < ColumnsModel.InfoModels.Count; i++)
                {
                    if (ColumnsModel.SectionStyle == ErrorColumns.SectionStyle.RECTANGLE)
                    {
                        //ColumnsModel.DimensionView.CreateDimensionHorizontalSectionRectangle(ColumnsModel.SectionColumnViews[i].StartView, document, unit, ColumnsModel.InfoModels[i], ColumnsModel.SettingModel); SetValue(p, ColumnsModel, 1);
                        //ColumnsModel.DimensionView.CreateDimensionVerticalSectionRectangle(ColumnsModel.SectionColumnViews[i].StartView, document, unit, ColumnsModel.InfoModels[i], ColumnsModel.SettingModel); SetValue(p, ColumnsModel, 1);
                        ColumnsModel.DimensionView.CreateDimensionHorizontalSectionRectangle(ColumnsModel.SectionColumnViews[i].MidView, document, unit, ColumnsModel.InfoModels[i], ColumnsModel.SettingModel); ColumnsModel.ProgressModel.SetValue(uc, 1);
                        ColumnsModel.DimensionView.CreateDimensionVerticalSectionRectangle(ColumnsModel.SectionColumnViews[i].MidView, document, unit, ColumnsModel.InfoModels[i], ColumnsModel.SettingModel); ColumnsModel.ProgressModel.SetValue(uc, 1);
                        //ColumnsModel.DimensionView.CreateDimensionHorizontalSectionRectangle(ColumnsModel.SectionColumnViews[i].EndView, document, unit, ColumnsModel.InfoModels[i], ColumnsModel.SettingModel); SetValue(p, ColumnsModel, 1);
                        //ColumnsModel.DimensionView.CreateDimensionVerticalSectionRectangle(ColumnsModel.SectionColumnViews[i].EndView, document, unit, ColumnsModel.InfoModels[i], ColumnsModel.SettingModel); SetValue(p, ColumnsModel, 1);
                    }
                    else
                    {
                        //ColumnsModel.DimensionView.CreateDimensionHorizontalSectionCylindrical(ColumnsModel.SectionColumnViews[i].StartView, document, unit, ColumnsModel.InfoModels[i], ColumnsModel.SettingModel); SetValue(p, ColumnsModel, 1);
                        //ColumnsModel.DimensionView.CreateDimensionVerticalSectionCylindrical(ColumnsModel.SectionColumnViews[i].StartView, document, unit, ColumnsModel.InfoModels[i], ColumnsModel.SettingModel); SetValue(p, ColumnsModel, 1);
                        ColumnsModel.DimensionView.CreateDimensionHorizontalSectionCylindrical(ColumnsModel.SectionColumnViews[i].MidView, document, unit, ColumnsModel.InfoModels[i], ColumnsModel.SettingModel); ColumnsModel.ProgressModel.SetValue(uc, 1);
                        ColumnsModel.DimensionView.CreateDimensionVerticalSectionCylindrical(ColumnsModel.SectionColumnViews[i].MidView, document, unit, ColumnsModel.InfoModels[i], ColumnsModel.SettingModel); ColumnsModel.ProgressModel.SetValue(uc, 1);
                        //ColumnsModel.DimensionView.CreateDimensionHorizontalSectionCylindrical(ColumnsModel.SectionColumnViews[i].EndView, document, unit, ColumnsModel.InfoModels[i], ColumnsModel.SettingModel); SetValue(p, ColumnsModel, 1);
                        //ColumnsModel.DimensionView.CreateDimensionVerticalSectionCylindrical(ColumnsModel.SectionColumnViews[i].EndView, document, unit, ColumnsModel.InfoModels[i], ColumnsModel.SettingModel); SetValue(p, ColumnsModel, 1);
                    }

                }
                transaction.Commit();
                ColumnsModel.IsCreateDimensionSection = true;
            }
            ColumnsModel.ProgressModel.ResetValue(uc);
        }
        private static int GetProgressBarViewDimension(ColumnsModel ColumnsModel)
        {
            int a = 0;
            a += 1;
            for (int i = 0; i < ColumnsModel.SectionColumnViews.Count; i++)
            {
               a += 3;
            }
            a += 4;
            for (int i = 0; i < ColumnsModel.SectionColumnViews.Count; i++)
            {
                a += 6;
            }
            return a;
        }
       
        private static List<string> ActionDimension = new List<string>()
        {
          "Create Detail View",
          "Create Section View",
          "Create Dimension View",
          "Create Dimension Section",
        };

    }
}
