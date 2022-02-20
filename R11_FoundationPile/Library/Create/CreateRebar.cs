using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using DSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using WpfCustomControls;
using DSP;
namespace R11_FoundationPile
{
    public class CreateRebar
    {
       
        public static void Create(FoundationPileWindow p, FoundationPileModel FoundationPileModel, Document document, UnitProject unit)
        {
            ProgressBar uc = VisualTreeHelper.FindChild<ProgressBar>(p, "Progress");
            uc.Maximum = GetProgressBar(FoundationPileModel, document) * 1.0;
            List<RebarHookType> rebarHookTypes = new List<RebarHookType>();
            List<double> hookLength = new List<double>();
            using (Transaction transaction = new Transaction(document))
            {

                transaction.Start("Aa");
                for (int i = 0; i < FoundationPileModel.FoundationBarModels.Count; i++)
                {
                    for (int j = 0; j < FoundationPileModel.FoundationBarModels[i].BarModels.Count; j++)
                    {
                        FoundationPileModel.FoundationBarModels[i].BarModels[j].CreateHookLength(document, FoundationPileModel.SettingModel, unit, FoundationPileModel.FoundationBarModels[i].LocationName);
                        if (FoundationPileModel.FoundationBarModels[i].BarModels[j].Hook != null && !PointModel.AreEqual(FoundationPileModel.FoundationBarModels[i].BarModels[j].HookLength, 0))
                        {
                            rebarHookTypes.Add(FoundationPileModel.FoundationBarModels[i].BarModels[j].Hook);
                            hookLength.Add(FoundationPileModel.FoundationBarModels[i].BarModels[j].HookLength);
                            FoundationPileModel.ProgressModel.SetValue(uc, 1);
                        }
                    }

                }
                transaction.Commit();
            }
            if (rebarHookTypes.Count != 0)
            {
                using (Transaction transaction = new Transaction(document))
                {

                    transaction.Start("Aa");
                    for (int i = 0; i < FoundationPileModel.AllBars.Count; i++)
                    {
                        for (int j = 0; j < rebarHookTypes.Count; j++)
                        {
                            FoundationPileModel.AllBars[i].RebarBarType.SetAutoCalcHookLengths(rebarHookTypes[j].Id, false);
                            FoundationPileModel.AllBars[i].RebarBarType.SetHookLength(rebarHookTypes[j].Id, Math.Abs(unit.Convert(hookLength[j])));
                        }
                    }
                    transaction.Commit();
                }
            }
            using (Transaction transaction = new Transaction(document))
            {

                transaction.Start("Aa");
                for (int i = 0; i < FoundationPileModel.FoundationBarModels.Count; i++)
                {
                    FoundationModel foundationModel = FoundationPileModel.FindFoundationModelByLoacationName(FoundationPileModel.FoundationBarModels[i].LocationName);
                    if (foundationModel != null)
                    {
                        foundationModel.SetRebarCover(FoundationPileModel.SettingModel);
                        FoundationPileModel.ProgressModel.SetValue(uc, 1);
                    }

                }
                transaction.Commit();
            }

            double coverTop = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, FoundationPileModel.SettingModel.SelectedTopCover.CoverDistance, false));
            double coverBottom = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, FoundationPileModel.SettingModel.SelectedBotomCover.CoverDistance, false));
            double coverSide = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, FoundationPileModel.SettingModel.SelectedSideCover.CoverDistance, false));
            using (Transaction transaction = new Transaction(document))
            {

                transaction.Start("Aa");
                for (int i = 0; i < FoundationPileModel.FoundationBarModels.Count; i++)
                {
                    FoundationModel foundationModel = FoundationPileModel.FindFoundationModelByLoacationName(FoundationPileModel.FoundationBarModels[i].LocationName);
                    double dMainBottom = FoundationPileModel.FoundationBarModels[i].BarModels.Where(x => x.Name.Equals("MainBottom")).FirstOrDefault().Bar.Diameter;
                    double dMainTop = FoundationPileModel.FoundationBarModels[i].BarModels.Where(x => x.Name.Equals("MainTop")).FirstOrDefault().Bar.Diameter;
                    double dSide = FoundationPileModel.FoundationBarModels[i].BarModels.Where(x => x.Name.Equals("Side")).FirstOrDefault().Bar.Diameter;
                    for (int j = 0; j < FoundationPileModel.FoundationBarModels[i].BarModels.Count; j++)
                    {
                        
                       
                        FoundationPileModel.FoundationBarModels[i].BarModels[j].Bar.CreateRebar(document,FoundationPileModel.SettingModel, foundationModel, FoundationPileModel.FoundationBarModels[i], FoundationPileModel.FoundationBarModels[i].BarModels[j],unit,dMainBottom,dMainTop,dSide,coverTop,coverBottom,coverSide);
                        //FailureHandlingOptions option = transaction.GetFailureHandlingOptions();
                        //option.SetFailuresPreprocessor(new DeleteWarningSuper());
                        //transaction.SetFailureHandlingOptions(option);
                        FoundationPileModel.ProgressModel.SetValue(uc, 1);
                    }

                }
                transaction.Commit();
            }
           
            FoundationPileModel.IsCreateReinforcement = true;
            FoundationPileModel.ProgressModel.ResetValue(uc);
        }
        private static int GetProgressBar(FoundationPileModel FoundationPileModel, Document document)
        {
            int a = 0;
            for (int i = 0; i < FoundationPileModel.FoundationBarModels.Count; i++)
            {
                for (int j = 0; j < FoundationPileModel.FoundationBarModels[i].BarModels.Count; j++)
                {
                    if (FoundationPileModel.FoundationBarModels[i].BarModels[j].Hook != null && !PointModel.AreEqual(FoundationPileModel.FoundationBarModels[i].BarModels[j].HookLength, 0))
                    {
                        a++;
                    }
                }

            }
            for (int i = 0; i < FoundationPileModel.FoundationBarModels.Count; i++)
            {
                a++;

            }


            for (int i = 0; i < FoundationPileModel.FoundationBarModels.Count; i++)
            {
                
                for (int j = 0; j < FoundationPileModel.FoundationBarModels[i].BarModels.Count; j++)
                {
                    a++;
                }

            }
            for (int i = 0; i < FoundationPileModel.FoundationBarModels.Count; i++)
            {

                a++;
            }
            return a;
        }
    }
}
