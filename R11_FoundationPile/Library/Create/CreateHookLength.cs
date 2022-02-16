using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using DSP;
using System;
using System.Collections.Generic;
namespace R11_FoundationPile
{
    public class CreateHookLength
    {
        public static void Create(FoundationPileWindow p, FoundationPileModel FoundationPileModel, Document document, UnitProject unit)
        {
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
                    if (foundationModel!=null)
                    {
                        foundationModel.SetRebarCover(FoundationPileModel.SettingModel);
                    }
                    
                }
                transaction.Commit();
            }


        }


    }
}
