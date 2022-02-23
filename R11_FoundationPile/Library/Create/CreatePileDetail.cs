using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows.Controls;
using WpfCustomControls;
using DSP;
namespace R11_FoundationPile
{
    public class CreatePileDetail
    {
        public static void Create(FoundationPileWindow p, FoundationPileModel foundationPileModel, Document document, UnitProject unit)
        {
            ProgressBar uc = VisualTreeHelper.FindChild<ProgressBar>(p, "Progress");
            uc.Maximum = GetProgressBarPileDetail(foundationPileModel, document) * 1.0;
            using (Transaction transaction = new Transaction(document, Guid.NewGuid().GetHashCode().ToString()))
            {
                transaction.Start("aaa");
                foundationPileModel.FoundationPileDetail.CeatePileDetailPlan(document, unit, foundationPileModel.SettingModel);
                foundationPileModel.ProgressModel.SetValue(uc, 1);
                transaction.Commit();
            }
            if (foundationPileModel.FoundationPileDetail.PildeDetailView != null)
            {
                using (Transaction transaction = new Transaction(document, Guid.NewGuid().GetHashCode().ToString()))
                {
                    transaction.Start("aaa");
                    foundationPileModel.DimensionDetail.CreateDimensionGridXPile(foundationPileModel.FoundationPileDetail.PildeDetailView, document, unit, foundationPileModel.SettingModel); foundationPileModel.ProgressModel.SetValue(uc, 1);
                    foundationPileModel.DimensionDetail.CreateDimensionGridYPile(foundationPileModel.FoundationPileDetail.PildeDetailView, document, unit, foundationPileModel.SettingModel); foundationPileModel.ProgressModel.SetValue(uc, 1);
                    transaction.Commit();
                }
                using (Transaction transaction = new Transaction(document, Guid.NewGuid().GetHashCode().ToString()))
                {
                    transaction.Start("aaa");
                    for (int i = 0; i < foundationPileModel.AllFoundationModels.Count; i++)
                    {
                        for (int j = 0; j < foundationPileModel.AllFoundationModels[i].PileModels.Count; j++)
                        {
                            
                            foundationPileModel.AllFoundationModels[i].PileModels[j].SetPileName(foundationPileModel.SettingModel); foundationPileModel.ProgressModel.SetValue(uc, 1);
                            foundationPileModel.AllFoundationModels[i].PileModels[j].CreateTagPile(foundationPileModel.FoundationPileDetail.PildeDetailView, document, unit, foundationPileModel.SettingModel); foundationPileModel.ProgressModel.SetValue(uc, 1);
                            foundationPileModel.AllFoundationModels[i].PileModels[j].CreateDimentionPileToGrid(foundationPileModel.FoundationPileDetail.PildeDetailView, document, unit, foundationPileModel.SettingModel, foundationPileModel.DimensionDetail.GridX, foundationPileModel.DimensionDetail.GridY); foundationPileModel.ProgressModel.SetValue(uc, 1);
                        }
                    }
                    transaction.Commit();
                }
                using (Transaction transaction = new Transaction(document, Guid.NewGuid().GetHashCode().ToString()))
                {
                    transaction.Start("aaa");
                    foundationPileModel.FoundationPileDetail.CreatePileSpotCoodinateView(document, unit, foundationPileModel.SettingModel); foundationPileModel.ProgressModel.SetValue(uc, 1);
                    foundationPileModel.FoundationPileDetail.CreateSchedule(document, foundationPileModel.SettingModel); foundationPileModel.ProgressModel.SetValue(uc, 1);
                    foundationPileModel.DimensionDetail.CreateDimensionGridXPile(foundationPileModel.FoundationPileDetail.PileSpotCoordinateView, document, unit, foundationPileModel.SettingModel); foundationPileModel.ProgressModel.SetValue(uc, 1);
                    foundationPileModel.DimensionDetail.CreateDimensionGridYPile(foundationPileModel.FoundationPileDetail.PileSpotCoordinateView, document, unit, foundationPileModel.SettingModel); foundationPileModel.ProgressModel.SetValue(uc, 1);
                    if (foundationPileModel.FoundationPileDetail.PileSpotCoordinateView != null)
                    {
                        for (int i = 0; i < foundationPileModel.AllFoundationModels.Count; i++)
                        {
                            for (int j = 0; j < foundationPileModel.AllFoundationModels[i].PileModels.Count; j++)
                            {
                                foundationPileModel.AllFoundationModels[i].PileModels[j].CreateSpotDimensionPileCoordinate(foundationPileModel.FoundationPileDetail.PileSpotCoordinateView, document, unit, foundationPileModel.SettingModel); foundationPileModel.ProgressModel.SetValue(uc, 1);
                                XYZ point = (foundationPileModel.AllFoundationModels[i].PileModels[j].Pile.Location as LocationPoint).Point;
                                foundationPileModel.AllFoundationModels[i].PileModels[j].Pile.LookupParameter("XVector").Set(point.X);
                                foundationPileModel.AllFoundationModels[i].PileModels[j].Pile.LookupParameter("YVector").Set(point.Y);
                            }
                        }
                    }
                    transaction.Commit();
                }
                foundationPileModel.IsCreatePileDetail = true;
               
            }
            foundationPileModel.ProgressModel.ResetValue(uc);

        }
        private static int GetProgressBarPileDetail( FoundationPileModel foundationPileModel, Document document)
        {
            int a = 0;
            a++;

           a += 2;
            for (int i = 0; i < foundationPileModel.AllFoundationModels.Count; i++)
            {
                for (int j = 0; j < foundationPileModel.AllFoundationModels[i].PileModels.Count; j++)
                {

                    a += 3;
                }
            }
            a += 4;
            for (int i = 0; i < foundationPileModel.AllFoundationModels.Count; i++)
            {
                for (int j = 0; j < foundationPileModel.AllFoundationModels[i].PileModels.Count; j++)
                {
                    a++;
                }
            }
            return a;
        }
        //private static void SetValue(FoundationPileWindow p, FoundationPileModel foundationPileModel, int n)
        //{
        //    foundationPileModel.Value += n;
        //    foundationPileModel.Percent = (foundationPileModel.Value / p.ProgressWindow.Maximum) * 100;
        //    p.ProgressWindow.Dispatcher.Invoke(() => p.ProgressWindow.Value = foundationPileModel.Value,
        //        DispatcherPriority.Background);
        //}
        //private static void ResetValue(FoundationPileWindow p, FoundationPileModel foundationPileModel)
        //{
        //    foundationPileModel.Value = 0; ;
        //    foundationPileModel.Percent = 0;
        //    p.ProgressWindow.Dispatcher.Invoke(() => p.ProgressWindow.Value = foundationPileModel.Value,
        //        DispatcherPriority.Background);
        //}
    }
}
