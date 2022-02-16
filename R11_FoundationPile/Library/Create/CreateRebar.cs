using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using DSP;
using System;
using System.Linq;

namespace R11_FoundationPile
{
    public class CreateRebar
    {
       
        public static void Create(FoundationPileWindow p, FoundationPileModel FoundationPileModel, Document document, UnitProject unit)
        {
            double coverTop = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, FoundationPileModel.SettingModel.SelectedTopCover.CoverDistance, false));
            double coverBottom = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, FoundationPileModel.SettingModel.SelectedBotomCover.CoverDistance, false));
            double coverSide = double.Parse(UnitFormatUtils.Format(document.GetUnits(), SpecTypeId.Length, FoundationPileModel.SettingModel.SelectedSideCover.CoverDistance, false));
            using (Transaction transaction = new Transaction(document))
            {

                transaction.Start("Aa");
                for (int i = 0; i < FoundationPileModel.FoundationBarModels.Count; i++)
                {
                    FoundationModel foundationModel = FoundationPileModel.FindFoundationModelByLoacationName(FoundationPileModel.FoundationBarModels[i].LocationName);
                    for (int j = 0; j < FoundationPileModel.FoundationBarModels[i].BarModels.Count; j++)
                    {
                        
                        double dMainBottom = FoundationPileModel.FoundationBarModels[i].BarModels.Where(x => x.Name.Equals("MainBottom")).FirstOrDefault().Bar.Diameter;
                        double dMainTop = FoundationPileModel.FoundationBarModels[i].BarModels.Where(x => x.Name.Equals("MainTop")).FirstOrDefault().Bar.Diameter;
                        double dSide = FoundationPileModel.FoundationBarModels[i].BarModels.Where(x => x.Name.Equals("Side")).FirstOrDefault().Bar.Diameter;
                        FoundationPileModel.FoundationBarModels[i].BarModels[j].Bar.CreateRebar(document,FoundationPileModel.SettingModel, foundationModel, FoundationPileModel.FoundationBarModels[i], FoundationPileModel.FoundationBarModels[i].BarModels[j],unit,dMainBottom,dMainTop,dSide,coverTop,coverBottom,coverSide);
                    }

                }
                transaction.Commit();
            }

        }
    }
}
