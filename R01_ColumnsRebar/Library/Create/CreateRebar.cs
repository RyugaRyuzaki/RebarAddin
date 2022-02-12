
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using WpfCustomControls;
namespace R01_ColumnsRebar
{
    public class CreateRebar
    {
        #region Create
        public static void Create(ColumnsWindow p, ColumnsModel columnsModel, Document document, UnitProject unit)
        {
            ProgressBar uc = VisualTreeHelper.FindChild<ProgressBar>(p, "Progress");
            uc.Maximum = GetProgressBarRebar(document, columnsModel) * 1.0;
            CreateStirrupBar(uc, columnsModel, document, unit);
            CreateMainBar(uc, columnsModel, document, unit);
            CreateTagBar(uc, columnsModel, document, unit);
        }

        private static void CreateStirrupBar(ProgressBar uc, ColumnsModel columnsModel, Document document, UnitProject unit)
        {
            double offset0 = (columnsModel.SectionStyle == ErrorColumns.SectionStyle.RECTANGLE) ? (Math.Max((columnsModel.InfoModels[0].b), (columnsModel.InfoModels[0].h))) : (columnsModel.InfoModels[0].D);
            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start(ActionRebar[0]);
                for (int i = 0; i < columnsModel.StirrupModels.Count; i++)
                {
                    columnsModel.StirrupModels[i].CreateStirrup(columnsModel.SectionStyle, columnsModel.InfoModels[i], document, unit, columnsModel.SettingModel, columnsModel.Cover); columnsModel.ProgressModel.SetValue(uc, 1);
                    columnsModel.StirrupModels[i].CreateTagStirrupDetail(columnsModel.SectionStyle, columnsModel.DetailColumnView.DetailViewX, columnsModel.InfoModels[i], document, unit, columnsModel.SettingModel, true, offset0*0.5); columnsModel.ProgressModel.SetValue(uc, 3);
                    columnsModel.StirrupModels[i].CreateTagStirrupDetail(columnsModel.SectionStyle, columnsModel.DetailColumnView.DetailViewY, columnsModel.InfoModels[i], document, unit, columnsModel.SettingModel, true, offset0*0.5); columnsModel.ProgressModel.SetValue(uc, 3);
                }
                for (int i = 0; i < columnsModel.StirrupModels.Count; i++)
                {
                    columnsModel.StirrupModels[i].CreateAddHorizontalStirrup(columnsModel.SectionStyle, columnsModel.InfoModels[i], document, unit, columnsModel.SettingModel, columnsModel.Cover); if (columnsModel.StirrupModels[i].AddH) columnsModel.ProgressModel.SetValue(uc, 1);

                }
                for (int i = 0; i < columnsModel.StirrupModels.Count; i++)
                {
                    columnsModel.StirrupModels[i].CreateAddVerticalStirrup(columnsModel.SectionStyle, columnsModel.InfoModels[i], document, unit, columnsModel.SettingModel, columnsModel.Cover); if (columnsModel.StirrupModels[i].AddV) columnsModel.ProgressModel.SetValue(uc, 1);
                }
                transaction.Commit();
            }
        }
        private static void CreateMainBar(ProgressBar uc, ColumnsModel columnsModel, Document document, UnitProject unit)
        {
            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start(ActionRebar[1]);
                for (int i = 0; i < columnsModel.BarMainModels.Count; i++)
                {
                    if (columnsModel.BarMainModels[i].BarModels.Count != 0)
                    {
                        for (int j = 0; j < columnsModel.BarMainModels[i].BarModels.Count; j++)
                        {
                            columnsModel.BarMainModels[i].BarModels[j].CreateMainBar(columnsModel.SectionStyle, document, columnsModel.PlanarFaces[0], unit, columnsModel.InfoModels[0], columnsModel.InfoModels[i],columnsModel.SettingModel); columnsModel.ProgressModel.SetValue(uc, 1);
                        }
                    }
                    if (columnsModel.BarMainModels[i].AddBarModels.Count != 0)
                    {
                        for (int j = 0; j < columnsModel.BarMainModels[i].AddBarModels.Count; j++)
                        {
                            columnsModel.BarMainModels[i].AddBarModels[j].CreateMainBar(columnsModel.SectionStyle, document, columnsModel.PlanarFaces[0], unit, columnsModel.InfoModels[0], columnsModel.InfoModels[i], columnsModel.SettingModel); columnsModel.ProgressModel.SetValue(uc, 1);
                        }
                    }
                }
                transaction.Commit();
            }
        }
        private static void CreateTagBar(ProgressBar uc, ColumnsModel columnsModel, Document document, UnitProject unit)
        {
            double offset0 = (columnsModel.SectionStyle == ErrorColumns.SectionStyle.RECTANGLE) ? (Math.Max((columnsModel.InfoModels[0].b), (columnsModel.InfoModels[0].h))) : (columnsModel.InfoModels[0].D);
            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start(ActionRebar[2]);
                for (int i = 0; i < columnsModel.SectionColumnViews.Count; i++)
                {
                    columnsModel.TagColumn.CreateTable(columnsModel.SectionColumnViews[i].MidView, columnsModel.SectionStyle, unit, document, columnsModel.InfoModels[i], columnsModel.StirrupModels[i], columnsModel.BarMainModels[i], columnsModel.SettingModel,  0.5*offset0);
                    columnsModel.ProgressModel.SetValue(uc, 1);
                }
                transaction.Commit();
            }
        }
        #endregion

        #region Action


        private static int GetProgressBarRebar(Document document, ColumnsModel columnsModel)
        {
            int a = 0;
            for (int i = 0; i < columnsModel.StirrupModels.Count; i++)
            {
                a += 1;
                if (columnsModel.StirrupModels[i].AddH) a += 1;
                if (columnsModel.StirrupModels[i].AddV) a += 1;
                a += 6;
            }
            for (int i = 0; i < columnsModel.BarMainModels.Count; i++)
            {
                if (columnsModel.BarMainModels[i].BarModels.Count != 0)
                {
                    for (int j = 0; j < columnsModel.BarMainModels[i].BarModels.Count; j++)
                    {
                        a += 1;
                    }
                }
                if (columnsModel.BarMainModels[i].AddBarModels.Count != 0)
                {
                    for (int j = 0; j < columnsModel.BarMainModels[i].AddBarModels.Count; j++)
                    {
                        a += 1;
                    }
                }
            }
            for (int i = 0; i < columnsModel.StirrupModels.Count; i++)
            {
                a += 1;
            }
            return a;
        }
        
        private static List<string> ActionRebar = new List<string>()
        {
            "Create Stirrup Bars",
            "Create Main Bars",
            "Create Tag Bars",
        };
        #endregion
    }
}
