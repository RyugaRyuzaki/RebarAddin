using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Windows.Threading;
using System.IO;
using R02_BeamsRebar.View;
using System.Windows.Controls;
using WpfCustomControls;
using DSP;
namespace R02_BeamsRebar
{
    public class CreateDetailShop
    {
        
        #region Create
        public static void Create(BeamsWindow p,BeamsModel BeamsModel, Document document,UnitProject unit,List<Element> beams)
        {
            BarsDivisionView bv = ProcessInfoBeamRebar.FindChild<BarsDivisionView>(p, "BarsDivisionUC");
            ProgressBar uc = VisualTreeHelper.FindChild<ProgressBar>(p, "Progress");
            uc.Maximum = GetProgressBarDetailShop(BeamsModel) * 1.0;
            string folder = FolderImage(document, BeamsModel);
            using (Transaction transaction = new Transaction(document))
            {
                transaction.Start(ActionDetailShop);
                
                #region create detail shop view
                BeamsModel.DetailShopView.CeateDetailShopView(document, unit, BeamsModel.InfoModels[0],BeamsModel.PlanarFaces, beams, BeamsModel.SettingModel, BeamsModel.SettingModel.DetailViewName + "-S", 2 * BeamsModel.SettingModel.L1, 2 * BeamsModel.SettingModel.L3+ 2*BeamsModel.SettingModel.L4);
                BeamsModel.ProgressModel.SetValue(uc, 1);
                #endregion
                int number = 1;
                #region Stirrup
                if (BeamsModel.BarsDivisionModel.Stirrups.Count!=0)
                {
                    double y0 = (2 * BeamsModel.SettingModel.L3 + BeamsModel.SettingModel.L4) / 2;
                    for (int i = 0; i < BeamsModel.InfoModels.Count; i++)
                    {
                        for (int j = 0; j < BeamsModel.BarsDivisionModel.Stirrups.Count; j++)
                        {
                            if (BeamsModel.BarsDivisionModel.Stirrups[j].Location.X> BeamsModel.InfoModels[i].startPosition&& BeamsModel.BarsDivisionModel.Stirrups[j].Location.X < BeamsModel.InfoModels[i].endPosition)
                            {
                                BeamsModel.BarsDivisionModel.Stirrups[j].CreateDetailItem(document, BeamsModel, unit, y0, number);
                                BeamsModel.BarsDivisionModel.Stirrups[j].CreateImage(bv, document, BeamsModel.SettingModel, folder, number);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                            }
                        }
                        number++;
                    }
                }
                if (BeamsModel.BarsDivisionModel.AntiStirrups.Count != 0)
                {
                    double y0 = (2 * BeamsModel.SettingModel.L3 + BeamsModel.SettingModel.L4) / 2;
                    for (int i = 0; i < BeamsModel.InfoModels.Count; i++)
                    {
                        for (int j = 0; j < BeamsModel.BarsDivisionModel.AntiStirrups.Count; j++)
                        {
                            if (BeamsModel.BarsDivisionModel.AntiStirrups[j].Location.X > BeamsModel.InfoModels[i].startPosition && BeamsModel.BarsDivisionModel.AntiStirrups[j].Location.X < BeamsModel.InfoModels[i].endPosition)
                            {
                                BeamsModel.BarsDivisionModel.AntiStirrups[j].CreateDetailItem(document, BeamsModel, unit, y0, number);
                                BeamsModel.BarsDivisionModel.AntiStirrups[j].CreateImage(bv, document, BeamsModel.SettingModel, folder, number);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                number++;
                            }
                        }
                    }
                }
                #endregion
                #region MainTop
                if (BeamsModel.BarsDivisionModel.MainTop.Count != 0)
                {
                    
                    double y0 = 0.2 * BeamsModel.SettingModel.L3;
                    if (BeamsModel.BarsDivisionModel.MainTop.Count == 1)
                    {
                        BeamsModel.BarsDivisionModel.MainTop[0].CreateDetailItem(document, BeamsModel, unit,0, number);
                        BeamsModel.BarsDivisionModel.MainTop[0].CreateImage(bv, document, BeamsModel.SettingModel, folder, number);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        number++;
                    }
                    else
                    {
                        for (int i = 0; i < BeamsModel.BarsDivisionModel.MainTop.Count; i++)
                        {
                            if (i == BeamsModel.BarsDivisionModel.MainTop.Count - 1)
                            {
                                BeamsModel.BarsDivisionModel.MainTop[i].CreateDetailItem(document, BeamsModel, unit, (i % 2 == 0) ? 0 : y0, number);
                                BeamsModel.BarsDivisionModel.MainTop[i].CreateImage(bv, document, BeamsModel.SettingModel, folder, number);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                number++;
                            }
                            else
                            {
                                bool a = ConditionDrawOverlap(BeamsModel.BarsDivisionModel.MainTop[i], BeamsModel.BarsDivisionModel.MainTop[i + 1]);
                                BeamsModel.BarsDivisionModel.MainTop[i].CreateDetailItem(document, BeamsModel, unit, (i % 2 == 0 && a) ? 0 : y0, number);
                                BeamsModel.BarsDivisionModel.MainTop[i].CreateImage(bv, document, BeamsModel.SettingModel, folder, number);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                number++;
                                if (a)
                                {
                                    BeamsModel.ProgressModel.SetValue(uc, 1);
                                }
                            }
                        }
                    }
                }
                #endregion
                #region MainBottom
                if (BeamsModel.BarsDivisionModel.MainBottom.Count != 0)
                {
                    double y0 = BeamsModel.SettingModel.L4 + 2 * BeamsModel.SettingModel.L3;
                    double y1 = 0.2 * BeamsModel.SettingModel.L3;
                    if (BeamsModel.BarsDivisionModel.MainBottom.Count == 1)
                    {
                        BeamsModel.BarsDivisionModel.MainBottom[0].CreateDetailItem(document, BeamsModel, unit, y0, number);
                        BeamsModel.BarsDivisionModel.MainBottom[0].CreateImage(bv, document, BeamsModel.SettingModel, folder, number);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        number++;
                    }
                    else
                    {
                        for (int i = 0; i < BeamsModel.BarsDivisionModel.MainBottom.Count; i++)
                        {
                            if (i == BeamsModel.BarsDivisionModel.MainBottom.Count - 1)
                            {
                                BeamsModel.BarsDivisionModel.MainBottom[i].CreateDetailItem(document, BeamsModel, unit, (i % 2 == 0) ? y0 : y0 + y1, number);
                                BeamsModel.BarsDivisionModel.MainBottom[i].CreateImage(bv, document, BeamsModel.SettingModel, folder, number);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                number++;
                            }
                            else
                            {
                                bool a = ConditionDrawOverlap(BeamsModel.BarsDivisionModel.MainBottom[i], BeamsModel.BarsDivisionModel.MainBottom[i + 1]);
                                BeamsModel.BarsDivisionModel.MainBottom[i].CreateDetailItem(document, BeamsModel, unit, (i % 2 == 0 && a) ? y0 : y0 + y1, number);
                                BeamsModel.BarsDivisionModel.MainBottom[i].CreateImage(bv, document, BeamsModel.SettingModel, folder, number);
                                BeamsModel.ProgressModel.SetValue(uc, 1);
                                number++;
                                if (a)
                                {
                                    BeamsModel.ProgressModel.SetValue(uc, 1);
                                }
                            }
                        }
                    }
                }
                #endregion
                #region AddTop
                if (BeamsModel.BarsDivisionModel.AddTop.Count != 0)
                {
                    double y0 = BeamsModel.SettingModel.L3;
                    for (int i = 0; i < BeamsModel.BarsDivisionModel.AddTop.Count; i++)
                    {
                        BeamsModel.BarsDivisionModel.AddTop[i].CreateDetailItem(document, BeamsModel, unit, y0, number);
                        BeamsModel.BarsDivisionModel.AddTop[i].CreateImage(bv, document, BeamsModel.SettingModel, folder, number);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        number++;
                    }
                }
                #endregion
                #region AddBottom
                if (BeamsModel.BarsDivisionModel.AddBottom.Count != 0)
                {
                    double y0 = BeamsModel.SettingModel.L3 + BeamsModel.SettingModel.L4;
                    for (int i = 0; i < BeamsModel.BarsDivisionModel.AddBottom.Count; i++)
                    {
                        BeamsModel.BarsDivisionModel.AddBottom[i].CreateDetailItem(document, BeamsModel, unit, y0, number);
                        BeamsModel.BarsDivisionModel.AddBottom[i].CreateImage(bv, document, BeamsModel.SettingModel, folder, number);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        number++;
                    }
                }
                #endregion
                #region Special
                if (BeamsModel.BarsDivisionModel.Special.Count != 0)
                {
                    double y0 = (2 * BeamsModel.SettingModel.L3 + BeamsModel.SettingModel.L4) / 2;
                    for (int i = 0; i < BeamsModel.BarsDivisionModel.Special.Count; i++)
                    {
                        BeamsModel.BarsDivisionModel.Special[i].CreateDetailItem(document, BeamsModel, unit, y0, number);
                        BeamsModel.BarsDivisionModel.Special[i].CreateImage(bv, document, BeamsModel.SettingModel, folder, number);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        number++;
                    }
                }
                if (BeamsModel.BarsDivisionModel.Side.Count != 0)
                {
                    double y0 = (2 * BeamsModel.SettingModel.L3 + BeamsModel.SettingModel.L4) / 2;
                    for (int i = 0; i < BeamsModel.BarsDivisionModel.Side.Count; i++)
                    {
                        BeamsModel.BarsDivisionModel.Side[i].CreateDetailItem(document, BeamsModel, unit, y0, number);
                        BeamsModel.BarsDivisionModel.Side[i].CreateImage(bv, document, BeamsModel.SettingModel, folder, number);
                        BeamsModel.ProgressModel.SetValue(uc, 1);
                        number++;
                    }
                }
                #endregion
                #region Schedule
                
                #endregion
                transaction.Commit();
                BeamsModel.IsCreateDetailShop = true;
                BeamsModel.ProgressModel.ResetValue(uc);
            }
        }
        public static string FolderImage(Document document,BeamsModel beamsModel)
        {
            string folderName = document.PathName.Replace( document.Title + ".rvt", "ImageRebar");
            string pathString = Path.Combine(folderName,beamsModel.SettingModel.BeamsName);
            Directory.CreateDirectory(pathString);
            return pathString;
        }
        #endregion
        #region Load Family
        

        #endregion
        #region Action

        private static int GetProgressBarDetailShop( BeamsModel BeamsModel)
        {
            int a = 0;
            a+= 1;
            #region create Stirrup
            if (BeamsModel.BarsDivisionModel.Stirrups.Count != 0)
            {
                for (int i = 0; i < BeamsModel.BarsDivisionModel.Stirrups.Count; i++)
                {
                   a += 1;
                }
            }
            if (BeamsModel.BarsDivisionModel.AntiStirrups.Count != 0)
            {
                for (int i = 0; i < BeamsModel.BarsDivisionModel.AntiStirrups.Count; i++)
                {
                    a += 1;
                }
            }
            #endregion

            #region MainTop
            if (BeamsModel.BarsDivisionModel.MainTop.Count != 0)
            {
                if (BeamsModel.BarsDivisionModel.MainTop.Count == 1)
                {
                    a+= 1;
                }
                else
                {
                    for (int i = 0; i < BeamsModel.BarsDivisionModel.MainTop.Count; i++)
                    {
                        if (i == BeamsModel.BarsDivisionModel.MainTop.Count - 1)
                        {
                            a += 1;
                        }
                        else
                        {
                            bool x = ConditionDrawOverlap(BeamsModel.BarsDivisionModel.MainTop[i], BeamsModel.BarsDivisionModel.MainTop[i + 1]);
                            a += 1;
                            if (x)
                            {
                                a += 1;
                            }
                        }
                    }
                }
            }
            #endregion
            #region MainBottom
            if (BeamsModel.BarsDivisionModel.MainBottom.Count != 0)
            {
                double y0 = BeamsModel.SettingModel.L4 + 2 * BeamsModel.SettingModel.L3;
                double y1 = 0.2 * BeamsModel.SettingModel.L3;
                if (BeamsModel.BarsDivisionModel.MainBottom.Count == 1)
                {
                    a += 1;
                }
                else
                {
                    for (int i = 0; i < BeamsModel.BarsDivisionModel.MainBottom.Count; i++)
                    {
                        if (i == BeamsModel.BarsDivisionModel.MainBottom.Count - 1)
                        {
                            a += 1;
                        }
                        else
                        {
                            bool x = ConditionDrawOverlap(BeamsModel.BarsDivisionModel.MainBottom[i], BeamsModel.BarsDivisionModel.MainBottom[i + 1]);
                            a += 1;
                            if (x)
                            {
                                a += 1;
                            }
                        }
                    }
                }
            }
            #endregion
            #region AddTop
            if (BeamsModel.BarsDivisionModel.AddTop.Count != 0)
            {
                for (int i = 0; i < BeamsModel.BarsDivisionModel.AddTop.Count; i++)
                {
                    a += 1;
                }
            }
            #endregion
            #region AddBottom
            if (BeamsModel.BarsDivisionModel.AddBottom.Count != 0)
            {
                for (int i = 0; i < BeamsModel.BarsDivisionModel.AddBottom.Count; i++)
                {
                    a += 1;
                }
            }
            #endregion
            #region Special
            if (BeamsModel.BarsDivisionModel.Special.Count != 0)
            {
                for (int i = 0; i < BeamsModel.BarsDivisionModel.Special.Count; i++)
                {
                    a += 1;
                }
            }
            if (BeamsModel.BarsDivisionModel.Side.Count != 0)
            {
                for (int i = 0; i < BeamsModel.BarsDivisionModel.Side.Count; i++)
                {
                    a+= 1;
                }
            }
            #endregion
            return a;
        }
       
        private static string ActionDetailShop = "Create Detail Shop";
        private static bool ConditionDrawOverlap(ItemDivision itemDivision1, ItemDivision itemDivision2)
        {
            if (itemDivision1.Type == DetailShopStyle.DS02 || itemDivision1.Type == DetailShopStyle.DS03 || itemDivision1.Type == DetailShopStyle.DS05 || itemDivision1.Type == DetailShopStyle.DS06)
            {
                return false;
            }
            if (itemDivision2.Type == DetailShopStyle.DS01 || itemDivision2.Type == DetailShopStyle.DS03 || itemDivision2.Type == DetailShopStyle.DS04 || itemDivision2.Type == DetailShopStyle.DS06)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
