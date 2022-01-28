
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;

namespace R01_ColumnsRebar
{
    public class CreateRebar
    {
        #region Create
        public static void Create(ColumnsWindow p, ColumnsModel ColumnsModel, Document document, UnitProject unit)
        {
            GetProgressBarRebar(document, p, ColumnsModel);
            CreateStirrupBar(p, ColumnsModel, document, unit);
            CreateMainBar(p, ColumnsModel, document, unit);
            CreateTagBar(p, ColumnsModel, document, unit);
        }

        private static void CreateStirrupBar(ColumnsWindow p, ColumnsModel columnsModel, Document document, UnitProject unit)
        {
            double offset0 = (columnsModel.SectionStyle == ErrorColumns.SectionStyle.RECTANGLE) ? (Math.Max((columnsModel.InfoModels[0].b), (columnsModel.InfoModels[0].h))) : (columnsModel.InfoModels[0].D);
            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start(ActionRebar[0]);
                columnsModel.SelectedAction = ActionRebar[0];
                for (int i = 0; i < columnsModel.StirrupModels.Count; i++)
                {
                    columnsModel.StirrupModels[i].CreateStirrup(columnsModel.SectionStyle, columnsModel.InfoModels[i], document, unit, columnsModel.SettingModel, columnsModel.Cover); SetValue(p, columnsModel, 1);
                    columnsModel.StirrupModels[i].CreateTagStirrupDetail(columnsModel.SectionStyle, columnsModel.DetailColumnView.DetailViewX, columnsModel.InfoModels[i], document, unit, columnsModel.SettingModel, true, offset0*0.5); SetValue(p, columnsModel, 3);
                    columnsModel.StirrupModels[i].CreateTagStirrupDetail(columnsModel.SectionStyle, columnsModel.DetailColumnView.DetailViewY, columnsModel.InfoModels[i], document, unit, columnsModel.SettingModel, true, offset0*0.5); SetValue(p, columnsModel, 3);
                }
                for (int i = 0; i < columnsModel.StirrupModels.Count; i++)
                {
                    columnsModel.StirrupModels[i].CreateAddHorizontalStirrup(columnsModel.SectionStyle, columnsModel.InfoModels[i], document, unit, columnsModel.SettingModel, columnsModel.Cover); if (columnsModel.StirrupModels[i].AddH) SetValue(p, columnsModel, 1);
                    
                }
                for (int i = 0; i < columnsModel.StirrupModels.Count; i++)
                {
                    columnsModel.StirrupModels[i].CreateAddVerticalStirrup(columnsModel.SectionStyle, columnsModel.InfoModels[i], document, unit, columnsModel.SettingModel, columnsModel.Cover); if (columnsModel.StirrupModels[i].AddV) SetValue(p, columnsModel, 1);
                }
                transaction.Commit();
            }
        }
        private static void CreateMainBar(ColumnsWindow p, ColumnsModel columnsModel, Document document, UnitProject unit)
        {
            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start(ActionRebar[1]);
                columnsModel.SelectedAction = ActionRebar[1];
                for (int i = 0; i < columnsModel.BarMainModels.Count; i++)
                {
                    if (columnsModel.BarMainModels[i].BarModels.Count != 0)
                    {
                        for (int j = 0; j < columnsModel.BarMainModels[i].BarModels.Count; j++)
                        {
                            columnsModel.BarMainModels[i].BarModels[j].CreateMainBar(columnsModel.SectionStyle, document, columnsModel.PlanarFaces[0], unit, columnsModel.InfoModels[0], columnsModel.InfoModels[i],columnsModel.SettingModel); SetValue(p, columnsModel, 1);
                        }
                    }
                    if (columnsModel.BarMainModels[i].AddBarModels.Count != 0)
                    {
                        for (int j = 0; j < columnsModel.BarMainModels[i].AddBarModels.Count; j++)
                        {
                            columnsModel.BarMainModels[i].AddBarModels[j].CreateMainBar(columnsModel.SectionStyle, document, columnsModel.PlanarFaces[0], unit, columnsModel.InfoModels[0], columnsModel.InfoModels[i], columnsModel.SettingModel); SetValue(p, columnsModel, 1);
                        }
                    }
                }
                transaction.Commit();
            }
        }
        private static void CreateTagBar(ColumnsWindow p, ColumnsModel columnsModel, Document document, UnitProject unit)
        {
            double offset0 = (columnsModel.SectionStyle == ErrorColumns.SectionStyle.RECTANGLE) ? (Math.Max((columnsModel.InfoModels[0].b), (columnsModel.InfoModels[0].h))) : (columnsModel.InfoModels[0].D);
            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start(ActionRebar[2]);
                columnsModel.SelectedAction = ActionRebar[2];
                for (int i = 0; i < columnsModel.SectionColumnViews.Count; i++)
                {
                    columnsModel.TagColumn.CreateTable(columnsModel.SectionColumnViews[i].MidView, columnsModel.SectionStyle, unit, document, columnsModel.InfoModels[i], columnsModel.StirrupModels[i], columnsModel.BarMainModels[i], columnsModel.SettingModel,  0.5*offset0);
                    SetValue(p, columnsModel, 1);
                }
                transaction.Commit();
            }
        }
        #endregion

        #region Action


        private static void GetProgressBarRebar(Document document, ColumnsWindow p, ColumnsModel columnsModel)
        {
            columnsModel.Value = 0;
            p.ProgressWindow.Maximum = 0;
            for (int i = 0; i < columnsModel.StirrupModels.Count; i++)
            {
                p.ProgressWindow.Maximum += 1;
                if (columnsModel.StirrupModels[i].AddH) p.ProgressWindow.Maximum += 1;
                if (columnsModel.StirrupModels[i].AddV) p.ProgressWindow.Maximum += 1;
                p.ProgressWindow.Maximum += 6;
            }
            for (int i = 0; i < columnsModel.BarMainModels.Count; i++)
            {
                if (columnsModel.BarMainModels[i].BarModels.Count != 0)
                {
                    for (int j = 0; j < columnsModel.BarMainModels[i].BarModels.Count; j++)
                    {
                        p.ProgressWindow.Maximum += 1;
                    }
                }
                if (columnsModel.BarMainModels[i].AddBarModels.Count != 0)
                {
                    for (int j = 0; j < columnsModel.BarMainModels[i].AddBarModels.Count; j++)
                    {
                        p.ProgressWindow.Maximum += 1;
                    }
                }
            }
            for (int i = 0; i < columnsModel.StirrupModels.Count; i++)
            {
                p.ProgressWindow.Maximum += 1;
            }
        }
        private static void SetValue(ColumnsWindow p,  ColumnsModel columnsModel,int n)
        {
            columnsModel.Value += n;
            columnsModel.Percent = columnsModel.Value / p.ProgressWindow.Maximum * 100;
            p.ProgressWindow.Dispatcher.Invoke(() => p.ProgressWindow.Value = columnsModel.Value,
                DispatcherPriority.Background);
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
