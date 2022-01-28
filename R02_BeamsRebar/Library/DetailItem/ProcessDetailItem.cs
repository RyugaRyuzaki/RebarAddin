using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace R02_BeamsRebar
{
    public class ProcessDetailItem
    {
        #region stirrup
        private bool ConditionSameNumberBar(StirrupModel a, StirrupModel b, bool h)
        {
            return a.BarS.Diameter == b.BarS.Diameter && a.Type == b.Type && h;
        }
        public static ObservableCollection<DetailItem> GetStirrupsDetail(List<InfoModel> InfoModels, List<StirrupModel> StirrupModels, List<DistributeStirrup> DistributeStirrups, List<SpecialBarModel> SpecialBarModels, List<SpecialNodeModel> SpecialNodeModels)
        {
            var StirrupsDetail = new ObservableCollection<DetailItem>();
            bool hasSpecial = (SpecialBarModels.Count != 0);
            for (int i = 0; i < StirrupModels.Count; i++)
            {
                var Stirrups0 = new ObservableCollection<DetailItem>();
                SpecialBarModel a = null;
                if (hasSpecial)
                {
                    a = SpecialBarModels.Where(x => x.Span == InfoModels[i].NumberSpan).FirstOrDefault();
                }
                bool special = (hasSpecial && (a != null) && (a.IsST));
                double start = 0;
                double end = 0;
                SpecialNodeModel b = null;
                if (special)
                {
                    b = SpecialNodeModels.Where(x => x.NumberSpan == a.Span).FirstOrDefault();
                    start = b.Mid - a.L3 / 2 - a.a * a.NumberST / 2 - InfoModels[i].startPosition;
                    end = b.Mid + a.L3 / 2 - InfoModels[i].startPosition + a.a * a.NumberST / 2;
                }
                if (DistributeStirrups[i].Type == 0)
                {
                    Stirrups0 = GetStirrupItem00(InfoModels[i], StirrupModels[i], DistributeStirrups[i], special, start, end, i + 1);
                }
                else
                {
                    Stirrups0 = GetStirrupItem01(InfoModels[i], StirrupModels[i], DistributeStirrups[i], special, start, end, i + 1);
                }
                foreach (var item in Stirrups0)
                {
                    StirrupsDetail.Add(item);
                }
            }
            return StirrupsDetail;
        }


        private static ObservableCollection<DetailItem> GetStirrupItem00(InfoModel infoModel, StirrupModel stirrupModel, DistributeStirrup distributeStirrup, bool special, double start, double end, int i)
        {
            var Stirrups = new ObservableCollection<DetailItem>();
            double l = infoModel.h - 2 * stirrupModel.c;
            if (!special)
            {
                int total = ((int)(infoModel.Length / distributeStirrup.S));
                double overight = (infoModel.Length - total * distributeStirrup.S) / 2;
                double x0 = infoModel.startPosition;
                double y0 = Math.Abs(infoModel.zOffset) + infoModel.h / 2;
                var a = new DetailItem();
                a.Location = new LocationBarModel(x0, y0);
                a.Type = DetailItemStyle.DT01;
                a.GetPropertyStirrup(overight, distributeStirrup.S, infoModel.Length, stirrupModel.BarS.Diameter, total + 1, l);
                a.RebarNumber = i;
                Stirrups.Add(a);
            }
            else
            {
                int total1 = ((int)(start / distributeStirrup.S));
                double overight1 = (start - total1 * distributeStirrup.S) / 2;
                double x01 = infoModel.startPosition;
                double y01 = Math.Abs(infoModel.zOffset) + infoModel.h / 2;
                var a1 = new DetailItem();
                a1.Location = new LocationBarModel(x01, y01);
                a1.Type = DetailItemStyle.DT01;
                a1.GetPropertyStirrup(overight1, distributeStirrup.S, start, stirrupModel.BarS.Diameter, total1 + 1, l);
                a1.RebarNumber = i;
                Stirrups.Add(a1);

                int total2 = ((int)((infoModel.Length - end) / distributeStirrup.S));
                double overight2 = (infoModel.Length - end - total2 * distributeStirrup.S) / 2;
                double x02 = infoModel.startPosition + end;
                double y02 = Math.Abs(infoModel.zOffset) + infoModel.h / 2;
                var a2 = new DetailItem();
                a2.Location = new LocationBarModel(x01, y01);
                a2.Type = DetailItemStyle.DT01;
                a2.GetPropertyStirrup(overight2, distributeStirrup.S, infoModel.Length - end, stirrupModel.BarS.Diameter, total2 + 1, l);
                a2.RebarNumber = i;
                Stirrups.Add(a2);
            }
            return Stirrups;
        }
        private static ObservableCollection<DetailItem> GetStirrupItem01(InfoModel infoModel, StirrupModel stirrupModel, DistributeStirrup distributeStirrup, bool special, double start, double end, int i)
        {
            var Stirrups = new ObservableCollection<DetailItem>();
            double l = infoModel.h - 2 * stirrupModel.c;
            int total1 = ((int)(distributeStirrup.L1 / distributeStirrup.S1));
            double overight1 = (distributeStirrup.L1 - total1 * distributeStirrup.S1) / 2;
            double x01 = infoModel.startPosition;
            double y01 = Math.Abs(infoModel.zOffset) + infoModel.h / 2;
            var a1 = new DetailItem();
            a1.Location = new LocationBarModel(x01, y01);
            a1.Type = DetailItemStyle.DT01;
            a1.GetPropertyStirrup(overight1, distributeStirrup.S1, distributeStirrup.L1, stirrupModel.BarS.Diameter, total1 + 1, l);
            a1.RebarNumber = i;
            Stirrups.Add(a1);
            if (!special)
            {
                int total2 = ((int)(distributeStirrup.L2 / distributeStirrup.S2));
                double x02 = infoModel.startPosition + distributeStirrup.L1;
                double y02 = Math.Abs(infoModel.zOffset) + infoModel.h / 2;
                double overight2 = (distributeStirrup.L2 - total2 * distributeStirrup.S2) / 2;
                var a2 = new DetailItem();
                a2.Location = new LocationBarModel(x02, y02);
                a2.Type = DetailItemStyle.DT01;
                a2.GetPropertyStirrup(overight2, distributeStirrup.S2, distributeStirrup.L2, stirrupModel.BarS.Diameter, total2 + 1, l);
                a2.RebarNumber = i;
                Stirrups.Add(a2);
            }
            else
            {
                if ((end < distributeStirrup.L1 + distributeStirrup.L2) && start > distributeStirrup.L1)
                {
                    int total2 = ((int)(Math.Abs(start - distributeStirrup.L1) / distributeStirrup.S2));
                    double x02 = infoModel.startPosition + distributeStirrup.L1;
                    double y02 = Math.Abs(infoModel.zOffset) + infoModel.h / 2;
                    double overight2 = (distributeStirrup.L2 - total2 * distributeStirrup.S2) / 2;
                    var a2 = new DetailItem();
                    a2.Location = new LocationBarModel(x02, y02);
                    a2.Type = DetailItemStyle.DT01;
                    a2.GetPropertyStirrup(overight2, distributeStirrup.S2, start - distributeStirrup.L1, stirrupModel.BarS.Diameter, total2 + 1, l);
                    a2.RebarNumber = i;
                    Stirrups.Add(a2);


                    int total4 = (int)(Math.Abs(distributeStirrup.L1 + distributeStirrup.L2 - end) / distributeStirrup.S2);
                    double x04 = infoModel.startPosition + end;
                    double y04 = Math.Abs(infoModel.zOffset) + infoModel.h / 2;
                    double overight4 = ((distributeStirrup.L1 + distributeStirrup.L2 - end) - total4 * distributeStirrup.S2) / 2;
                    var a4 = new DetailItem();
                    a4.Location = new LocationBarModel(x04, y04);
                    a4.Type = DetailItemStyle.DT01;
                    a4.GetPropertyStirrup(overight4, distributeStirrup.S2, distributeStirrup.L1 + distributeStirrup.L2 - end, stirrupModel.BarS.Diameter, total4 + 1, l);
                    a4.RebarNumber = i;
                    Stirrups.Add(a4);
                }
                else
                {
                    int total2 = ((int)(distributeStirrup.L2 / distributeStirrup.S2));
                    double x02 = infoModel.startPosition + distributeStirrup.L1;
                    double y02 = Math.Abs(infoModel.zOffset) + infoModel.h / 2;
                    double overight2 = (distributeStirrup.L2 - total2 * distributeStirrup.S2) / 2;
                    var a2 = new DetailItem();
                    a2.Location = new LocationBarModel(x02, y02);
                    a2.Type = DetailItemStyle.DT01;
                    a2.GetPropertyStirrup(overight2, distributeStirrup.S2, distributeStirrup.L2, stirrupModel.BarS.Diameter, total2 + 1, l);
                    a2.RebarNumber = i;
                    Stirrups.Add(a2);
                }
            }
            int total3 = total1;
            double overight3 = overight1;
            double x03 = x01 + distributeStirrup.L1 + distributeStirrup.L2;
            double y03 = y01;
            var a3 = new DetailItem();
            a3.Location = new LocationBarModel(x03, y03);
            a3.Type = DetailItemStyle.DT01;
            a3.GetPropertyStirrup(overight3, distributeStirrup.S1, distributeStirrup.L1, stirrupModel.BarS.Diameter, total3 + 1, l);
            a3.RebarNumber = i;
            Stirrups.Add(a3);
            return Stirrups;
        }




        #endregion
        #region MainTopBar
        public static ObservableCollection<DetailItem> GetMainTopDetail(SingleMainTopBarModel SingleMainTopBarModel, List<MainTopBarModel> MainTopBarModel, SelectedIndexModel SelectedIndexModel, int number)
        {

            if (SelectedIndexModel.StyleMainTop == 0)
            {
                return GetMainTopDetailSingle(SingleMainTopBarModel, number);
            }
            else
            {
                return GetMainTopDetailMulti(MainTopBarModel, number);
            }
        }
        private static ObservableCollection<DetailItem> GetMainTopDetailSingle(SingleMainTopBarModel singleMainTopBarModel, int number)
        {
            var MainTopBar = new ObservableCollection<DetailItem>();
            if (singleMainTopBarModel.ConditionMidAddTop())
            {
                var a = new DetailItem();
                a.AllLocation.Add(singleMainTopBarModel.Location[0]);
                a.AllLocation.Add(singleMainTopBarModel.Location[singleMainTopBarModel.Location.Count - 1]);
                a.Type = DetailItemStyle.DT00;
                a.GetPropertyLong(singleMainTopBarModel.Bar.Diameter, singleMainTopBarModel.NumberBar);
                MainTopBar.Add(a);
            }
            else
            {
                for (int i = 0; i < singleMainTopBarModel.Location.Count - 1; i++)
                {
                    double x = GetLengthFromLocation(singleMainTopBarModel.Location[i], singleMainTopBarModel.Location[i + 1]);
                    if (!PointModel.AreEqual(x, 0))
                    {
                        var a = new DetailItem();
                        a.AllLocation.Add(singleMainTopBarModel.Location[i]);
                        a.AllLocation.Add(singleMainTopBarModel.Location[i + 1]);
                        a.Type = DetailItemStyle.DT00;
                        a.GetPropertyLong(singleMainTopBarModel.Bar.Diameter, singleMainTopBarModel.NumberBar);
                        MainTopBar.Add(a);
                    }
                }
            }
            for (int i = 0; i < MainTopBar.Count; i++)
            {
                MainTopBar[i].RebarNumber = number;
            }
            return MainTopBar;
        }
        private static ObservableCollection<DetailItem> GetMainTopDetailMulti(List<MainTopBarModel> mainTopBarModel, int number)
        {
            var MainTopBar = new ObservableCollection<DetailItem>();
            for (int i = 0; i < mainTopBarModel.Count; i++)
            {
                var MainTopBar0 = new ObservableCollection<DetailItem>();
                if (mainTopBarModel[i].ConditionMidAddTop())
                {
                    var a = new DetailItem();
                    a.AllLocation.Add(mainTopBarModel[i].Location[0]);
                    a.AllLocation.Add(mainTopBarModel[i].Location[mainTopBarModel[i].Location.Count - 1]);
                    a.Type = DetailItemStyle.DT00;
                    a.GetPropertyLong(mainTopBarModel[i].Bar.Diameter, mainTopBarModel[i].NumberBar);
                    MainTopBar0.Add(a);
                }
                else
                {
                    for (int j = 0; j < mainTopBarModel[i].Location.Count - 1; j++)
                    {
                        double x = GetLengthFromLocation(mainTopBarModel[i].Location[j], mainTopBarModel[i].Location[j + 1]);
                        if (!PointModel.AreEqual(x, 0))
                        {
                            var a = new DetailItem();
                            a.AllLocation.Add(mainTopBarModel[i].Location[j]);
                            a.AllLocation.Add(mainTopBarModel[i].Location[j + 1]);
                            a.Type = DetailItemStyle.DT00;
                            a.GetPropertyLong(mainTopBarModel[i].Bar.Diameter, mainTopBarModel[i].NumberBar);
                            MainTopBar0.Add(a);
                        }
                    }
                }
                for (int k = 0; k < MainTopBar0.Count; k++)
                {
                    MainTopBar0[k].RebarNumber = number + i;
                    MainTopBar.Add(MainTopBar0[k]);
                }
            }
            return MainTopBar;
        }

        #endregion
        #region MainBottomBar
        public static ObservableCollection<DetailItem> GetMainBottomDetail(List<MainBottomBarModel> mainBottomBarModels, int number)
        {
            var MainBottom = new ObservableCollection<DetailItem>();
            for (int i = 0; i < mainBottomBarModels.Count; i++)
            {
                var MainBottom0 = new ObservableCollection<DetailItem>();
                if (mainBottomBarModels[i].ConditionMidAddTop())
                {
                    var a = new DetailItem();
                    a.AllLocation.Add(mainBottomBarModels[i].Location[0]);
                    a.AllLocation.Add(mainBottomBarModels[i].Location[mainBottomBarModels[i].Location.Count - 1]);
                    a.Type = DetailItemStyle.DT00;
                    a.GetPropertyLong(mainBottomBarModels[i].Bar.Diameter, mainBottomBarModels[i].NumberBar);
                    MainBottom0.Add(a);
                }
                else
                {
                    for (int j = 0; j < mainBottomBarModels[i].Location.Count - 1; j++)
                    {
                        double x = GetLengthFromLocation(mainBottomBarModels[i].Location[j], mainBottomBarModels[i].Location[j + 1]);
                        if (!PointModel.AreEqual(x, 0))
                        {
                            var a = new DetailItem();
                            a.AllLocation.Add(mainBottomBarModels[i].Location[j]);
                            a.AllLocation.Add(mainBottomBarModels[i].Location[j + 1]);
                            a.Type = DetailItemStyle.DT00;
                            a.GetPropertyLong(mainBottomBarModels[i].Bar.Diameter, mainBottomBarModels[i].NumberBar);
                            MainBottom0.Add(a);
                        }
                    }
                }
                for (int k = 0; k < MainBottom0.Count; k++)
                {
                    MainBottom0[k].RebarNumber = number + i;
                    MainBottom.Add(MainBottom0[k]);
                }
            }
            return MainBottom;
        }

        #endregion
        #region AddTop
        public static ObservableCollection<DetailItem> GetAddTopBarDetail(AddTopBarModel addTopBarModel, List<InfoModel> InfoModels, int number)
        {
            var AddTopBarDetail = new ObservableCollection<DetailItem>();
            int i1 = 0;
            if (addTopBarModel.Start.Model.Count != 0)
            {
                for (int i = 0; i < addTopBarModel.Start.Model.Count; i++)
                {
                    var AddTopBarDetail0 = new ObservableCollection<DetailItem>();
                    if (addTopBarModel.Start.Model[i].ConditionMidAddTop())
                    {
                        var a = new DetailItem();
                        a.AllLocation.Add(addTopBarModel.Start.Model[i].Location[0]);
                        a.AllLocation.Add(addTopBarModel.Start.Model[i].Location[addTopBarModel.Start.Model[i].Location.Count - 1]);
                        a.Type = DetailItemStyle.DT00;
                        a.GetPropertyLong(addTopBarModel.Start.Model[i].Bar.Diameter, addTopBarModel.Start.Model[i].NumberBar);
                        AddTopBarDetail0.Add(a);
                    }
                    else
                    {
                        for (int j = 0; j < addTopBarModel.Start.Model[i].Location.Count - 1; j++)
                        {
                            double x = GetLengthFromLocation(addTopBarModel.Start.Model[i].Location[j], addTopBarModel.Start.Model[i].Location[j + 1]);
                            if (!PointModel.AreEqual(x, 0))
                            {
                                var a = new DetailItem();
                                a.AllLocation.Add(addTopBarModel.Start.Model[i].Location[j]);
                                a.AllLocation.Add(addTopBarModel.Start.Model[i].Location[j + 1]);
                                a.Type = DetailItemStyle.DT00;
                                a.GetPropertyLong(addTopBarModel.Start.Model[i].Bar.Diameter, addTopBarModel.Start.Model[i].NumberBar);
                                AddTopBarDetail0.Add(a);
                            }
                        }
                    }
                    for (int k = 0; k < AddTopBarDetail0.Count; k++)
                    {
                        AddTopBarDetail0[k].RebarNumber = number + i;
                        AddTopBarDetail.Add(AddTopBarDetail0[k]);
                    }
                    i1++;
                }
            }

            if (InfoModels.Count > 1)
            {
                if (addTopBarModel.Mid.Count != 0)
                {
                    for (int i = 0; i < addTopBarModel.Mid.Count; i++)
                    {
                        if (addTopBarModel.Mid[i].Model.Count != 0)
                        {
                            for (int j = 0; j < addTopBarModel.Mid[i].Model.Count; j++)
                            {
                                var AddTopBarDetail0 = new ObservableCollection<DetailItem>();
                                if (addTopBarModel.Mid[i].Model[j].ConditionMidAddTop())
                                {
                                    var a = new DetailItem();
                                    a.AllLocation.Add(addTopBarModel.Mid[i].Model[j].Location[0]);
                                    a.AllLocation.Add(addTopBarModel.Mid[i].Model[j].Location[addTopBarModel.Mid[i].Model[j].Location.Count - 1]);
                                    a.Type = DetailItemStyle.DT00;
                                    a.GetPropertyLong(addTopBarModel.Mid[i].Model[j].Bar.Diameter, addTopBarModel.Mid[i].Model[j].NumberBar);
                                    AddTopBarDetail0.Add(a);
                                }
                                else
                                {
                                    for (int k = 0; k < addTopBarModel.Mid[i].Model[j].Location.Count - 1; k++)
                                    {
                                        double x = GetLengthFromLocation(addTopBarModel.Mid[i].Model[j].Location[k], addTopBarModel.Mid[i].Model[j].Location[k + 1]);
                                        if (!PointModel.AreEqual(x, 0))
                                        {
                                            var a = new DetailItem();
                                            a.AllLocation.Add(addTopBarModel.Mid[i].Model[j].Location[k]);
                                            a.AllLocation.Add(addTopBarModel.Mid[i].Model[j].Location[k + 1]);
                                            a.Type = DetailItemStyle.DT00;
                                            a.GetPropertyLong(addTopBarModel.Mid[i].Model[j].Bar.Diameter, addTopBarModel.Mid[i].Model[j].NumberBar);
                                            AddTopBarDetail0.Add(a);
                                        }
                                    }
                                }
                                for (int k = 0; k < AddTopBarDetail0.Count; k++)
                                {
                                    AddTopBarDetail0[k].RebarNumber = number + j + i1;
                                    AddTopBarDetail.Add(AddTopBarDetail0[k]);
                                }
                                i1++;
                            }
                        }
                    }
                }
            }
            if (addTopBarModel.End.Model.Count != 0)
            {
                for (int i = 0; i < addTopBarModel.End.Model.Count; i++)
                {
                    var AddTopBarDetail0 = new ObservableCollection<DetailItem>();
                    if (addTopBarModel.End.Model[i].ConditionMidAddTop())
                    {
                        var a = new DetailItem();
                        a.AllLocation.Add(addTopBarModel.End.Model[i].Location[0]);
                        a.AllLocation.Add(addTopBarModel.End.Model[i].Location[addTopBarModel.End.Model[i].Location.Count - 1]);
                        a.Type = DetailItemStyle.DT00;
                        a.GetPropertyLong(addTopBarModel.End.Model[i].Bar.Diameter, addTopBarModel.End.Model[i].NumberBar);
                        AddTopBarDetail0.Add(a);
                    }
                    else
                    {
                        for (int j = 0; j < addTopBarModel.End.Model[i].Location.Count - 1; j++)
                        {
                            double x = GetLengthFromLocation(addTopBarModel.End.Model[i].Location[j], addTopBarModel.End.Model[i].Location[j + 1]);
                            if (!PointModel.AreEqual(x, 0))
                            {
                                var a = new DetailItem();
                                a.AllLocation.Add(addTopBarModel.End.Model[i].Location[j]);
                                a.AllLocation.Add(addTopBarModel.End.Model[i].Location[j + 1]);
                                a.Type = DetailItemStyle.DT00;
                                a.GetPropertyLong(addTopBarModel.End.Model[i].Bar.Diameter, addTopBarModel.End.Model[i].NumberBar);
                                AddTopBarDetail0.Add(a);
                            }
                        }
                    }
                    for (int k = 0; k < AddTopBarDetail0.Count; k++)
                    {
                        AddTopBarDetail0[k].RebarNumber = number + i + i1;
                        AddTopBarDetail.Add(AddTopBarDetail0[k]);
                    }
                    i1++;
                }
            }
            return AddTopBarDetail;
        }

        #endregion
        #region AddBottom
        public static ObservableCollection<DetailItem> GetAddBottomBarDetail(ObservableCollection<AddBottomBarModel> addBottomBarModels, List<SelectedBottomModel> selectedBottomModels, int number)
        {
            var AddBottomBarDetail = new ObservableCollection<DetailItem>();
            for (int i = 0; i < addBottomBarModels.Count; i++)
            {
                int i1 = 0;
                if (selectedBottomModels[i].StartBottomChecked && addBottomBarModels[i].Start.Model.Count != 0)
                {
                    for (int j = 0; j < addBottomBarModels[i].Start.Model.Count; j++)
                    {
                        var AddBottomBarDetail0 = new ObservableCollection<DetailItem>();
                        if (addBottomBarModels[i].Start.Model[j].ConditionMidAddTop())
                        {
                            var a = new DetailItem();
                            a.AllLocation.Add(addBottomBarModels[i].Start.Model[j].Location[0]);
                            a.AllLocation.Add(addBottomBarModels[i].Start.Model[j].Location[addBottomBarModels[i].Start.Model[j].Location.Count - 1]);
                            a.Type = DetailItemStyle.DT00;
                            a.GetPropertyLong(addBottomBarModels[i].Start.Model[j].Bar.Diameter, addBottomBarModels[i].Start.Model[j].NumberBar);
                            AddBottomBarDetail0.Add(a);
                        }
                        else
                        {
                            for (int k = 0; k < addBottomBarModels[i].Start.Model[j].Location.Count - 1; k++)
                            {
                                double x = GetLengthFromLocation(addBottomBarModels[i].Start.Model[j].Location[k], addBottomBarModels[i].Start.Model[j].Location[k + 1]);
                                if (!PointModel.AreEqual(x, 0))
                                {
                                    var a = new DetailItem();
                                    a.AllLocation.Add(addBottomBarModels[i].Start.Model[j].Location[k]);
                                    a.AllLocation.Add(addBottomBarModels[i].Start.Model[j].Location[k + 1]);
                                    a.Type = DetailItemStyle.DT00;
                                    a.GetPropertyLong(addBottomBarModels[i].Start.Model[j].Bar.Diameter, addBottomBarModels[i].Start.Model[j].NumberBar);
                                    AddBottomBarDetail0.Add(a);
                                }
                            }
                        }
                        for (int k = 0; k < AddBottomBarDetail0.Count; k++)
                        {
                            AddBottomBarDetail0[k].RebarNumber = number + j + i1;
                            AddBottomBarDetail.Add(AddBottomBarDetail0[k]);
                        }
                        i1++;
                    }
                }

                if (addBottomBarModels[i].Mid.Model.Count != 0)
                {
                    for (int j = 0; j < addBottomBarModels[i].Mid.Model.Count; j++)
                    {
                        var AddBottomBarDetail0 = new ObservableCollection<DetailItem>();
                        if (addBottomBarModels[i].Mid.Model[j].ConditionMidAddTop())
                        {
                            var a = new DetailItem();
                            a.AllLocation.Add(addBottomBarModels[i].Mid.Model[j].Location[0]);
                            a.AllLocation.Add(addBottomBarModels[i].Mid.Model[j].Location[addBottomBarModels[i].Mid.Model[j].Location.Count - 1]);
                            a.Type = DetailItemStyle.DT00;
                            a.GetPropertyLong(addBottomBarModels[i].Mid.Model[j].Bar.Diameter, addBottomBarModels[i].Mid.Model[j].NumberBar);
                            AddBottomBarDetail0.Add(a);
                        }
                        else
                        {
                            for (int k = 0; k < addBottomBarModels[i].Mid.Model[j].Location.Count - 1; k++)
                            {
                                double x = GetLengthFromLocation(addBottomBarModels[i].Mid.Model[j].Location[k], addBottomBarModels[i].Mid.Model[j].Location[k + 1]);
                                if (!PointModel.AreEqual(x, 0))
                                {
                                    var a = new DetailItem();
                                    a.AllLocation.Add(addBottomBarModels[i].Mid.Model[j].Location[k]);
                                    a.AllLocation.Add(addBottomBarModels[i].Mid.Model[j].Location[k + 1]);
                                    a.Type = DetailItemStyle.DT00;
                                    a.GetPropertyLong(addBottomBarModels[i].Mid.Model[j].Bar.Diameter, addBottomBarModels[i].Mid.Model[j].NumberBar);
                                    AddBottomBarDetail0.Add(a);
                                }
                            }
                        }
                        for (int k = 0; k < AddBottomBarDetail0.Count; k++)
                        {
                            AddBottomBarDetail0[k].RebarNumber = number + j + i1;
                            AddBottomBarDetail.Add(AddBottomBarDetail0[k]);
                        }
                        i1++;
                    }
                }
                if (selectedBottomModels[i].EndBottomChecked && addBottomBarModels[i].End.Model.Count != 0)
                {
                    for (int j = 0; j < addBottomBarModels[i].End.Model.Count; j++)
                    {
                        var AddBottomBarDetail0 = new ObservableCollection<DetailItem>();
                        if (addBottomBarModels[i].End.Model[j].ConditionMidAddTop())
                        {
                            var a = new DetailItem();
                            a.AllLocation.Add(addBottomBarModels[i].End.Model[j].Location[0]);
                            a.AllLocation.Add(addBottomBarModels[i].End.Model[j].Location[addBottomBarModels[i].End.Model[j].Location.Count - 1]);
                            a.Type = DetailItemStyle.DT00;
                            a.GetPropertyLong(addBottomBarModels[i].End.Model[j].Bar.Diameter, addBottomBarModels[i].End.Model[j].NumberBar);
                            AddBottomBarDetail0.Add(a);
                        }
                        else
                        {
                            for (int k = 0; k < addBottomBarModels[i].End.Model[j].Location.Count - 1; k++)
                            {
                                double x = GetLengthFromLocation(addBottomBarModels[i].End.Model[j].Location[k], addBottomBarModels[i].End.Model[j].Location[k + 1]);
                                if (!PointModel.AreEqual(x, 0))
                                {
                                    var a = new DetailItem();
                                    a.AllLocation.Add(addBottomBarModels[i].End.Model[j].Location[k]);
                                    a.AllLocation.Add(addBottomBarModels[i].End.Model[j].Location[k + 1]);
                                    a.Type = DetailItemStyle.DT00;
                                    a.GetPropertyLong(addBottomBarModels[i].End.Model[j].Bar.Diameter, addBottomBarModels[i].End.Model[j].NumberBar);
                                    AddBottomBarDetail0.Add(a);
                                }
                            }
                        }
                        for (int k = 0; k < AddBottomBarDetail0.Count; k++)
                        {
                            AddBottomBarDetail0[k].RebarNumber = number + j + i1;
                            AddBottomBarDetail.Add(AddBottomBarDetail0[k]);
                        }
                        i1++;
                    }
                }
            }
            return AddBottomBarDetail;
        }

        #endregion
        #region SideBar
        public static ObservableCollection<DetailItem> GetSideBarDetail(List<SideBarModel> SideBarModel, int number)
        {
            var SideBar = new ObservableCollection<DetailItem>();
            int i1 = 0;
            for (int i = 0; i < SideBarModel.Count; i++)
            {
                if (SideBarModel[i].IsSide)
                {
                    var a = new DetailItem();
                    a.AllLocation.Add(SideBarModel[i].Location[0]);
                    a.AllLocation.Add(SideBarModel[i].Location[SideBarModel[i].Location.Count - 1]);
                    a.Type = DetailItemStyle.DT00;
                    a.GetPropertyLong(SideBarModel[i].Bar.Diameter, SideBarModel[i].NumberBar);
                    a.RebarNumber = number + i1;
                    SideBar.Add(a);
                    i1++;
                }
            }
            return SideBar;
        }

        #endregion
        #region SpecialBar
        public static ObservableCollection<DetailItem> GetSpecialBarDetail(List<SpecialBarModel> SpecialBarModel, int number)
        {
            var SpecialBar = new ObservableCollection<DetailItem>();
            if (SpecialBarModel.Count != 0)
            {
                int i1 = 0;
                for (int i = 0; i < SpecialBarModel.Count; i++)
                {
                    if (SpecialBarModel[i].IsSP)
                    {
                        var SpecialBar0 = new ObservableCollection<DetailItem>();
                        for (int j = 0; j < SpecialBarModel[i].LocationSP.Count - 1; j++)
                        {
                            var a = new DetailItem();
                            a.AllLocation.Add(SpecialBarModel[i].LocationSP[j]);
                            a.AllLocation.Add(SpecialBarModel[i].LocationSP[j + 1]);
                            a.Type = DetailItemStyle.DT00;
                            a.GetPropertyLong(SpecialBarModel[i].BarSP.Diameter, SpecialBarModel[i].NumberSP);
                            SpecialBar0.Add(a);
                        }
                        for (int k = 0; k < SpecialBar0.Count; k++)
                        {
                            SpecialBar0[k].RebarNumber = number + i1;
                            SpecialBar.Add(SpecialBar0[k]);
                        }
                        i1++;
                    }
                }
            }
            return SpecialBar;
        }
        public static ObservableCollection<DetailItem> GetSpecialBarStirrupDetail(List<SpecialBarModel> SpecialBarModel, List<InfoModel> infoModels, double cover, int number)
        {
            var SpecialBarStirrup = new ObservableCollection<DetailItem>();
            if (SpecialBarModel.Count != 0)
            {
                int i1 = 0;
                for (int i = 0; i < SpecialBarModel.Count; i++)
                {
                    InfoModel b = infoModels.Where(x => x.NumberSpan == SpecialBarModel[i].Span).FirstOrDefault();
                    if (SpecialBarModel[i].IsST && b != null)
                    {
                        var SpecialBar0 = new ObservableCollection<DetailItem>();
                        double l = b.h - 2 * cover;
                        var a1 = new DetailItem();
                        double x01 = SpecialBarModel[i].X0 - SpecialBarModel[i].L3 / 2 - (SpecialBarModel[i].NumberST / 2 - 1) * SpecialBarModel[i].a;
                        double y01 = Math.Abs(b.zOffset) + b.h / 2;

                        double overight = 0;
                        double lenght1 = (SpecialBarModel[i].NumberST / 2 - 1) * SpecialBarModel[i].a;
                        a1.Location = new LocationBarModel(x01, y01);
                        a1.Type = DetailItemStyle.DT01;
                        a1.GetPropertyStirrup(overight, SpecialBarModel[i].a, lenght1, SpecialBarModel[i].BarST.Diameter, SpecialBarModel[i].NumberST / 2, l);
                        SpecialBar0.Add(a1);

                        var a2 = new DetailItem();
                        double x02 = SpecialBarModel[i].X0 + SpecialBarModel[i].L3 / 2;
                        double y02 = Math.Abs(b.zOffset) + b.h / 2;
                        double lenght2 = (SpecialBarModel[i].NumberST / 2 - 1) * SpecialBarModel[i].a;
                        a2.Location = new LocationBarModel(x02, y02);
                        a2.Type = DetailItemStyle.DT01;
                        a2.GetPropertyStirrup(overight, SpecialBarModel[i].a, lenght2, SpecialBarModel[i].BarST.Diameter, SpecialBarModel[i].NumberST / 2, l);
                        SpecialBar0.Add(a2);

                        for (int k = 0; k < SpecialBar0.Count; k++)
                        {
                            SpecialBar0[k].RebarNumber = number + i1;
                            SpecialBarStirrup.Add(SpecialBar0[k]);
                        }

                        i1++;
                    }
                }
            }
            return SpecialBarStirrup;
        }
        #endregion
        #region Item
        #endregion
        #region Section
        public static ObservableCollection<DetailItem> GetStirrupsSection(List<InfoModel> infoModels, List<StirrupModel> stirrupModels, List<DistributeStirrup> distributeStirrups)
        {
            var StirrupsSection = new ObservableCollection<DetailItem>();

            for (int i = 0; i < infoModels.Count; i++)
            {
                double x0Start = infoModels[i].startPosition + 0.125 * infoModels[i].Length;
                double y0Start = Math.Abs(infoModels[i].zOffset);
                double distanceStart = (distributeStirrups[i].Type == 0) ? distributeStirrups[i].S : distributeStirrups[i].S1;
                double x0Mid = infoModels[i].startPosition + 0.5 * infoModels[i].Length;
                double y0Mid = Math.Abs(infoModels[i].zOffset);
                double distanceMid = (distributeStirrups[i].Type == 0) ? distributeStirrups[i].S : distributeStirrups[i].S2;
                double x0End = infoModels[i].startPosition + 0.875 * infoModels[i].Length;
                double y0End = Math.Abs(infoModels[i].zOffset);
                double distanceEnd = distanceStart;
                if (stirrupModels[i].Type == 0)
                {
                    double la = infoModels[i].b - 2 * stirrupModels[i].c;
                    double lb = infoModels[i].h - 2 * stirrupModels[i].c;

                    var aStart = new DetailItem();
                    aStart.Location = new LocationBarModel(x0Start, y0Start);
                    aStart.Type = DetailItemStyle.DT04;
                    aStart.GetPropertyStirrupSection(distanceStart, stirrupModels[i].BarS.Diameter, stirrupModels[i].BarS.Diameter * 5, la, lb);
                    aStart.RebarNumber = i + 1;
                    aStart.AllLocation.Add(new LocationBarModel(infoModels[i].b / 2, y0Start));
                    StirrupsSection.Add(aStart);

                    var aMid = new DetailItem();
                    aMid.Location = new LocationBarModel(x0Mid, y0Mid);
                    aMid.Type = DetailItemStyle.DT04;
                    aMid.GetPropertyStirrupSection(distanceMid, stirrupModels[i].BarS.Diameter, stirrupModels[i].BarS.Diameter * 5, la, lb);
                    aMid.RebarNumber = i + 1;
                    aMid.AllLocation.Add(new LocationBarModel(infoModels[i].b / 2, y0Mid));
                    StirrupsSection.Add(aMid);

                    var aEnd = new DetailItem();
                    aEnd.Location = new LocationBarModel(x0End, y0End);
                    aEnd.Type = DetailItemStyle.DT04;
                    aEnd.GetPropertyStirrupSection(distanceEnd, stirrupModels[i].BarS.Diameter, stirrupModels[i].BarS.Diameter * 5, la, lb);
                    aEnd.RebarNumber = i + 1;
                    aEnd.AllLocation.Add(new LocationBarModel(infoModels[i].b / 2, y0End));
                    StirrupsSection.Add(aEnd);
                }
                else
                {
                    double x = (infoModels[i].b - 4 * stirrupModels[i].c - stirrupModels[i].a) / 2;
                    double la = x + stirrupModels[i].c + stirrupModels[i].a;
                    double lb = infoModels[i].h - 2 * stirrupModels[i].c;
                    double b0 = x + 3 * stirrupModels[i].c + stirrupModels[i].a;
                    double x01 = (b0) / 2;
                    double x02 = stirrupModels[i].c + x + (b0) / 2;

                    var aS1 = new DetailItem();
                    aS1.Location = new LocationBarModel(x0Start, y0Start);
                    aS1.Type = DetailItemStyle.DT04;
                    aS1.GetPropertyStirrupSection(distanceStart, stirrupModels[i].BarS.Diameter, stirrupModels[i].BarS.Diameter * 5, la, lb);
                    aS1.RebarNumber = i + 1;
                    aS1.AllLocation.Add(new LocationBarModel(x01, y0Start));
                    StirrupsSection.Add(aS1);
                    var aS2 = new DetailItem();
                    aS2.Location = new LocationBarModel(x0Start, y0Start);
                    aS2.Type = DetailItemStyle.DT04;
                    aS2.GetPropertyStirrupSection(distanceStart, stirrupModels[i].BarS.Diameter, stirrupModels[i].BarS.Diameter * 5, la, lb);
                    aS2.RebarNumber = i + 1;
                    aS2.AllLocation.Add(new LocationBarModel(x02, y0Start));
                    StirrupsSection.Add(aS2);

                    var aM1 = new DetailItem();
                    aM1.Location = new LocationBarModel(x0Mid, y0Mid);
                    aM1.Type = DetailItemStyle.DT04;
                    aM1.GetPropertyStirrupSection(distanceMid, stirrupModels[i].BarS.Diameter, stirrupModels[i].BarS.Diameter * 5, la, lb);
                    aM1.RebarNumber = i + 1;
                    aM1.AllLocation.Add(new LocationBarModel(x01, y0Mid));
                    StirrupsSection.Add(aM1);
                    var aM2 = new DetailItem();
                    aM2.Location = new LocationBarModel(x0Mid, y0Mid);
                    aM2.Type = DetailItemStyle.DT04;
                    aM2.GetPropertyStirrupSection(distanceMid, stirrupModels[i].BarS.Diameter, stirrupModels[i].BarS.Diameter * 5, la, lb);
                    aM2.RebarNumber = i + 1;
                    aM2.AllLocation.Add(new LocationBarModel(x02, y0Mid));
                    StirrupsSection.Add(aM2);

                    var aE1 = new DetailItem();
                    aE1.Location = new LocationBarModel(x0End, y0End);
                    aE1.Type = DetailItemStyle.DT04;
                    aE1.GetPropertyStirrupSection(distanceEnd, stirrupModels[i].BarS.Diameter, stirrupModels[i].BarS.Diameter * 5, la, lb);
                    aE1.RebarNumber = i + 1;
                    aE1.AllLocation.Add(new LocationBarModel(x01, y0End));
                    StirrupsSection.Add(aE1);
                    var aE2 = new DetailItem();
                    aE2.Location = new LocationBarModel(x0End, y0End);
                    aE2.Type = DetailItemStyle.DT04;
                    aE2.GetPropertyStirrupSection(distanceEnd, stirrupModels[i].BarS.Diameter, stirrupModels[i].BarS.Diameter * 5, la, lb);
                    aE2.RebarNumber = i + 1;
                    aE2.AllLocation.Add(new LocationBarModel(x02, y0End));
                    StirrupsSection.Add(aE2);

                }
            }
            return StirrupsSection;
        }
        public static ObservableCollection<DetailItem> GetAntiStirrupsSection(List<InfoModel> infoModels, List<StirrupModel> stirrupModels, List<DistributeStirrup> distributeStirrups, SettingModel settingModel, int number)
        {
            var AntiStirrupsSection = new ObservableCollection<DetailItem>();
            int i1 = 0;
            for (int i = 0; i < infoModels.Count; i++)
            {
                if (stirrupModels[i].Anti)
                {
                    i1++;
                    double la = stirrupModels[i].BarA.Diameter * 5;
                    double lb = stirrupModels[i].BarA.Diameter * 5;
                    double l = infoModels[i].b - 2 * stirrupModels[i].c;
                    double x0Start = infoModels[i].startPosition + 0.125 * infoModels[i].Length;
                    double x0Mid = infoModels[i].startPosition + 0.5 * infoModels[i].Length;
                    double x0End = infoModels[i].startPosition + 0.875 * infoModels[i].Length;
                    double x0 = stirrupModels[i].c;
                    double distance = stirrupModels[i].Sa;

                    if (stirrupModels[i].Na == 1)
                    {
                        double y0S1 = Math.Abs(infoModels[i].zOffset) + infoModels[i].h / 2;
                        AntiStirrupsSection.Add(GetAntiItem(x0Start, x0, y0S1, distance, stirrupModels[i].BarA.Diameter, l, la, lb, settingModel, i1 + number));
                        AntiStirrupsSection.Add(GetAntiItem(x0Mid, x0, y0S1, distance, stirrupModels[i].BarA.Diameter, l, la, lb, settingModel, i1 + number));
                        AntiStirrupsSection.Add(GetAntiItem(x0End, x0, y0S1, distance, stirrupModels[i].BarA.Diameter, l, la, lb, settingModel, i1 + number));
                    }
                    else
                    {
                        double y0S1 = Math.Abs(infoModels[i].zOffset) + infoModels[i].h / 3;
                        double y0S2 = Math.Abs(infoModels[i].zOffset) + 2 * infoModels[i].h / 3;
                        AntiStirrupsSection.Add(GetAntiItem(x0Start, x0, y0S1, distance, stirrupModels[i].BarA.Diameter, l, la, lb, settingModel, i1 + number));
                        AntiStirrupsSection.Add(GetAntiItem(x0Start, x0, y0S2, distance, stirrupModels[i].BarA.Diameter, l, la, lb, settingModel, i1 + number));
                        AntiStirrupsSection.Add(GetAntiItem(x0Mid, x0, y0S1, distance, stirrupModels[i].BarA.Diameter, l, la, lb, settingModel, i1 + number));
                        AntiStirrupsSection.Add(GetAntiItem(x0Mid, x0, y0S2, distance, stirrupModels[i].BarA.Diameter, l, la, lb, settingModel, i1 + number));
                        AntiStirrupsSection.Add(GetAntiItem(x0End, x0, y0S1, distance, stirrupModels[i].BarA.Diameter, l, la, lb, settingModel, i1 + number));
                        AntiStirrupsSection.Add(GetAntiItem(x0End, x0, y0S2, distance, stirrupModels[i].BarA.Diameter, l, la, lb, settingModel, i1 + number));
                    }

                }
            }
            return AntiStirrupsSection;
        }
        public static ObservableCollection<DetailItem> GetLongSection(ObservableCollection<SectionAreaModel> SectionAreaModels, List<InfoModel> infoModels, List<SideBarModel> sideBarModels, ObservableCollection<DetailItem> MaintopDetail, ObservableCollection<DetailItem> AddtopDetail, ObservableCollection<DetailItem> AddBottomDetail, ObservableCollection<DetailItem> MainBottomDetail, ObservableCollection<DetailItem> SideBarDetail, double Cover, double dsmax)
        {
            var LongSection = new ObservableCollection<DetailItem>();
            for (int i = 0; i < SectionAreaModels.Count; i++)
            {
                double x0Start = infoModels[i].startPosition + 0.125 * infoModels[i].Length;
                double x0Mid = infoModels[i].startPosition + 0.5 * infoModels[i].Length;
                double x0End = infoModels[i].startPosition + 0.875 * infoModels[i].Length;
                var LongStartSection = new ObservableCollection<DetailItem>();
                LongStartSection = GetLongStartSection(SectionAreaModels[i], infoModels[i], MaintopDetail, AddtopDetail, AddBottomDetail, MainBottomDetail, x0Start, Cover, dsmax);
                for (int j = 0; j < LongStartSection.Count; j++)
                {
                    LongSection.Add(LongStartSection[j]);
                }
                var LongMiddleSection = new ObservableCollection<DetailItem>();
                LongMiddleSection = GetLongMiddleSection(SectionAreaModels[i], infoModels[i], MaintopDetail, AddtopDetail, AddBottomDetail, MainBottomDetail, x0Mid, Cover, dsmax);
                for (int j = 0; j < LongMiddleSection.Count; j++)
                {
                    LongSection.Add(LongMiddleSection[j]);
                }
                var LongEndSection = new ObservableCollection<DetailItem>();
                LongEndSection = GetLongEndSection(SectionAreaModels[i], infoModels[i], MaintopDetail, AddtopDetail, AddBottomDetail, MainBottomDetail, x0End, Cover, dsmax);
                for (int j = 0; j < LongEndSection.Count; j++)
                {
                    LongSection.Add(LongEndSection[j]);
                }
                if (sideBarModels[i].IsSide)
                {
                    var LongSideSection = new ObservableCollection<DetailItem>();
                    LongSideSection = GetLongSideSection(SectionAreaModels[i], infoModels[i], sideBarModels[i], SideBarDetail, x0Start, x0Mid, x0End, Cover, dsmax);
                    for (int j = 0; j < LongSideSection.Count; j++)
                    {
                        LongSection.Add(LongSideSection[j]);
                    }
                }
            }
            return LongSection;
        }

        private static ObservableCollection<DetailItem> GetLongStartSection(SectionAreaModel sectionAreaModel, InfoModel infoModel, ObservableCollection<DetailItem> MaintopDetail, ObservableCollection<DetailItem> AddtopDetail, ObservableCollection<DetailItem> AddBottomDetail, ObservableCollection<DetailItem> MainBottomDetail, double x0Start, double Cover, double dsmax)
        {
            var LongStartSection = new ObservableCollection<DetailItem>();

            for (int i = 0; i < sectionAreaModel.Start.Count; i++)
            {
                double x1 = Cover + dsmax + sectionAreaModel.Start[i].Bar.Diameter;
                double x2 = infoModel.b - x1;
                double y0 = sectionAreaModel.Start[i].Y0;
                var aStart = new DetailItem();
                aStart.Location = new LocationBarModel(x0Start, y0);
                aStart.Diameter = sectionAreaModel.Start[i].Bar.Diameter;
                aStart.NoBar = sectionAreaModel.Start[i].NumberBar;
                aStart.AllLocation.Add(new LocationBarModel(x1, y0));
                aStart.AllLocation.Add(new LocationBarModel(x2, y0));
                if (i == 0)
                {

                    aStart.Type = DetailItemStyle.DT02;
                    DetailItem maintop = MaintopDetail.Where(x => x.AllLocation[0].X < x0Start && x.AllLocation[1].X > x0Start).FirstOrDefault();
                    if (maintop != null)
                    {
                        aStart.RebarNumber = maintop.RebarNumber;
                    }
                    if (sectionAreaModel.Start[i].NumberBar==1)
                    {
                        aStart.Type = DetailItemStyle.DT00A;
                        aStart.AllLocation[0].X = infoModel.b / 2;
                    }
                    
                    LongStartSection.Add(aStart);
                }
                else
                {
                    if (i == sectionAreaModel.Start.Count - 1)
                    {
                        aStart.Type = DetailItemStyle.DT03;
                        DetailItem mainBottom = MainBottomDetail.Where(x => x.AllLocation[0].X < x0Start && x.AllLocation[1].X > x0Start).FirstOrDefault();
                        if (mainBottom != null)
                        {
                            aStart.RebarNumber = mainBottom.RebarNumber;
                        }
                        if (sectionAreaModel.Start[i].NumberBar == 1)
                        {
                            aStart.Type = DetailItemStyle.DT00A;
                            aStart.AllLocation[0].X = infoModel.b / 2;
                        }
                        LongStartSection.Add(aStart);
                    }
                    else
                    {
                        if (y0 <= infoModel.h / 2 + Math.Abs(infoModel.zOffset))
                        {
                            aStart.Type = DetailItemStyle.DT03;
                            DetailItem addTop = AddtopDetail.Where(x => x.AllLocation[0].X < x0Start && x.AllLocation[1].X > x0Start).FirstOrDefault();
                            if (addTop != null)
                            {
                                aStart.RebarNumber = addTop.RebarNumber;
                            }
                            if (sectionAreaModel.Start[i].NumberBar == 1)
                            {
                                aStart.Type = DetailItemStyle.DT00A;
                                aStart.AllLocation[0].X = infoModel.b / 2;
                            }
                            LongStartSection.Add(aStart);
                        }
                        else
                        {
                            aStart.Type = DetailItemStyle.DT02;
                            DetailItem addBottom = AddBottomDetail.Where(x => x.AllLocation[0].X < x0Start && x.AllLocation[1].X > x0Start).FirstOrDefault();
                            if (addBottom != null)
                            {
                                aStart.RebarNumber = addBottom.RebarNumber;
                            }
                            if (sectionAreaModel.Start[i].NumberBar == 1)
                            {
                                aStart.Type = DetailItemStyle.DT00A;
                                aStart.AllLocation[0].X = infoModel.b / 2;
                            }
                            LongStartSection.Add(aStart);
                        }

                    }
                }

            }
            return LongStartSection;
        }
        private static ObservableCollection<DetailItem> GetLongMiddleSection(SectionAreaModel sectionAreaModel, InfoModel infoModel, ObservableCollection<DetailItem> MaintopDetail, ObservableCollection<DetailItem> AddtopDetail, ObservableCollection<DetailItem> AddBottomDetail, ObservableCollection<DetailItem> MainBottomDetail, double x0Mid, double Cover, double dsmax)
        {
            var LongMiddleSection = new ObservableCollection<DetailItem>();
            for (int i = 0; i < sectionAreaModel.Middle.Count; i++)
            {
                double x1 = Cover + dsmax + sectionAreaModel.Middle[i].Bar.Diameter;
                double x2 = infoModel.b - x1;
                double y0 = sectionAreaModel.Middle[i].Y0;
                var aMiddle = new DetailItem();
                aMiddle.Location = new LocationBarModel(x0Mid, y0);
                aMiddle.Diameter = sectionAreaModel.Middle[i].Bar.Diameter;
                aMiddle.NoBar = sectionAreaModel.Middle[i].NumberBar;
                aMiddle.AllLocation.Add(new LocationBarModel(x1, y0));
                aMiddle.AllLocation.Add(new LocationBarModel(x2, y0));
                if (i == 0)
                {

                    aMiddle.Type = DetailItemStyle.DT02;
                    DetailItem maintop = MaintopDetail.Where(x => x.AllLocation[0].X < x0Mid && x.AllLocation[1].X > x0Mid).FirstOrDefault();
                    if (maintop != null)
                    {
                        aMiddle.RebarNumber = maintop.RebarNumber;
                    }
                    if (sectionAreaModel.Middle[i].NumberBar == 1)
                    {
                        aMiddle.Type = DetailItemStyle.DT00A;
                        aMiddle.AllLocation[0].X = infoModel.b / 2;
                    }
                    LongMiddleSection.Add(aMiddle);
                }
                else
                {
                    if (i == sectionAreaModel.Start.Count - 1)
                    {
                        aMiddle.Type = DetailItemStyle.DT03;
                        DetailItem mainBottom = MainBottomDetail.Where(x => x.AllLocation[0].X < x0Mid && x.AllLocation[1].X > x0Mid).FirstOrDefault();
                        if (mainBottom != null)
                        {
                            aMiddle.RebarNumber = mainBottom.RebarNumber;
                        }
                        if (sectionAreaModel.Middle[i].NumberBar == 1)
                        {
                            aMiddle.Type = DetailItemStyle.DT00A;
                            aMiddle.AllLocation[0].X = infoModel.b / 2;
                        }
                        LongMiddleSection.Add(aMiddle);
                    }
                    else
                    {
                        if (y0 <= infoModel.h / 2 + Math.Abs(infoModel.zOffset))
                        {
                            aMiddle.Type = DetailItemStyle.DT03;
                            DetailItem addTop = AddtopDetail.Where(x => x.AllLocation[0].X < x0Mid && x.AllLocation[1].X > x0Mid).FirstOrDefault();
                            if (addTop != null)
                            {
                                aMiddle.RebarNumber = addTop.RebarNumber;
                            }
                            if (sectionAreaModel.Middle[i].NumberBar == 1)
                            {
                                aMiddle.Type = DetailItemStyle.DT00A;
                                aMiddle.AllLocation[0].X = infoModel.b / 2;
                            }
                            LongMiddleSection.Add(aMiddle);
                        }
                        else
                        {
                            aMiddle.Type = DetailItemStyle.DT02;
                            DetailItem addBottom = AddBottomDetail.Where(x => x.AllLocation[0].X < x0Mid && x.AllLocation[1].X > x0Mid).FirstOrDefault();
                            if (addBottom != null)
                            {
                                aMiddle.RebarNumber = addBottom.RebarNumber;
                            }
                            if (sectionAreaModel.Middle[i].NumberBar == 1)
                            {
                                aMiddle.Type = DetailItemStyle.DT00A;
                                aMiddle.AllLocation[0].X = infoModel.b / 2;
                            }
                            LongMiddleSection.Add(aMiddle);
                        }

                    }
                }

            }
            return LongMiddleSection;
        }
        private static ObservableCollection<DetailItem> GetLongEndSection(SectionAreaModel sectionAreaModel, InfoModel infoModel, ObservableCollection<DetailItem> MaintopDetail, ObservableCollection<DetailItem> AddtopDetail, ObservableCollection<DetailItem> AddBottomDetail, ObservableCollection<DetailItem> MainBottomDetail, double x0End, double Cover, double dsmax)
        {
            var LongEndSection = new ObservableCollection<DetailItem>();
            for (int i = 0; i < sectionAreaModel.End.Count; i++)
            {
                double x1 = Cover + dsmax + sectionAreaModel.End[i].Bar.Diameter;
                double x2 = infoModel.b - x1;
                double y0 = sectionAreaModel.End[i].Y0;
                var aEnd = new DetailItem();
                aEnd.Location = new LocationBarModel(x0End, y0);
                aEnd.Diameter = sectionAreaModel.End[i].Bar.Diameter;
                aEnd.NoBar = sectionAreaModel.End[i].NumberBar;
                aEnd.AllLocation.Add(new LocationBarModel(x1, y0));
                aEnd.AllLocation.Add(new LocationBarModel(x2, y0));
                if (i == 0)
                {

                    aEnd.Type = DetailItemStyle.DT02;
                    DetailItem maintop = MaintopDetail.Where(x => x.AllLocation[0].X < x0End && x.AllLocation[1].X > x0End).FirstOrDefault();
                    if (maintop != null)
                    {
                        aEnd.RebarNumber = maintop.RebarNumber;
                    }
                    if (sectionAreaModel.End[i].NumberBar == 1)
                    {
                        aEnd.Type = DetailItemStyle.DT00A;
                        aEnd.AllLocation[0].X = infoModel.b / 2;
                    }
                    LongEndSection.Add(aEnd);
                }
                else
                {
                    if (i == sectionAreaModel.Start.Count - 1)
                    {
                        aEnd.Type = DetailItemStyle.DT03;
                        DetailItem mainBottom = MainBottomDetail.Where(x => x.AllLocation[0].X < x0End && x.AllLocation[1].X > x0End).FirstOrDefault();
                        if (mainBottom != null)
                        {
                            aEnd.RebarNumber = mainBottom.RebarNumber;
                        }
                        if (sectionAreaModel.End[i].NumberBar == 1)
                        {
                            aEnd.Type = DetailItemStyle.DT00A;
                            aEnd.AllLocation[0].X = infoModel.b / 2;
                        }
                        LongEndSection.Add(aEnd);
                    }
                    else
                    {
                        if (y0 <= infoModel.h / 2 + Math.Abs(infoModel.zOffset))
                        {
                            aEnd.Type = DetailItemStyle.DT03;
                            DetailItem addTop = AddtopDetail.Where(x => x.AllLocation[0].X < x0End && x.AllLocation[1].X > x0End).FirstOrDefault();
                            if (addTop != null)
                            {
                                aEnd.RebarNumber = addTop.RebarNumber;
                            }
                            if (sectionAreaModel.End[i].NumberBar == 1)
                            {
                                aEnd.Type = DetailItemStyle.DT00A;
                                aEnd.AllLocation[0].X = infoModel.b / 2;
                            }
                            LongEndSection.Add(aEnd);
                        }
                        else
                        {
                            aEnd.Type = DetailItemStyle.DT02;
                            DetailItem addBottom = AddBottomDetail.Where(x => x.AllLocation[0].X < x0End && x.AllLocation[1].X > x0End).FirstOrDefault();
                            if (addBottom != null)
                            {
                                aEnd.RebarNumber = addBottom.RebarNumber;
                            }
                            if (sectionAreaModel.End[i].NumberBar == 1)
                            {
                                aEnd.Type = DetailItemStyle.DT00A;
                                aEnd.AllLocation[0].X = infoModel.b / 2;
                            }
                            LongEndSection.Add(aEnd);
                        }

                    }
                }

            }
            return LongEndSection;
        }
        private static ObservableCollection<DetailItem> GetLongSideSection(SectionAreaModel sectionAreaModel, InfoModel infoModel, SideBarModel sideBarModel, ObservableCollection<DetailItem> SideBarDetail, double x0Start, double x0Mid, double x0End, double Cover, double dsmax)
        {
            var LongSideSection = new ObservableCollection<DetailItem>();
            double x1 = Cover + dsmax + sideBarModel.Bar.Diameter;
            double x2 = infoModel.b - x1;
            double y0 = sideBarModel.Y0;
            DetailItem side = SideBarDetail.Where(x => x.AllLocation[0].X < x0Start && x.AllLocation[1].X > x0End).FirstOrDefault();
            var aStart = new DetailItem();
            aStart.Location = new LocationBarModel(x0Start, y0);
            aStart.Diameter = sideBarModel.Bar.Diameter;
            aStart.NoBar = sideBarModel.NumberBar;
            aStart.Type = DetailItemStyle.DT03;
            aStart.RebarNumber = side.RebarNumber;
            aStart.AllLocation.Add(new LocationBarModel(x1, y0));
            aStart.AllLocation.Add(new LocationBarModel(x2, y0));
            LongSideSection.Add(aStart);
            var aMiddle = new DetailItem();
            aMiddle.Location = new LocationBarModel(x0Mid, y0);
            aMiddle.Diameter = sideBarModel.Bar.Diameter;
            aMiddle.NoBar = sideBarModel.NumberBar;
            aMiddle.Type = DetailItemStyle.DT03;
            aMiddle.RebarNumber = side.RebarNumber;
            aMiddle.AllLocation.Add(new LocationBarModel(x1, y0));
            aMiddle.AllLocation.Add(new LocationBarModel(x2, y0));
            LongSideSection.Add(aMiddle);
            var aEnd = new DetailItem();
            aEnd.Location = new LocationBarModel(x0End, y0);
            aEnd.Diameter = sideBarModel.Bar.Diameter;
            aEnd.NoBar = sideBarModel.NumberBar;
            aEnd.Type = DetailItemStyle.DT03;
            aEnd.RebarNumber = side.RebarNumber;
            aEnd.AllLocation.Add(new LocationBarModel(x1, y0));
            aEnd.AllLocation.Add(new LocationBarModel(x2, y0));
            LongSideSection.Add(aEnd);
            return LongSideSection;
        }
        #endregion
        #region static 
        private static double GetLengthFromLocation(LocationBarModel l1, LocationBarModel l2)
        {
            return Math.Sqrt((l1.X - l2.X) * (l1.X - l2.X) + (l1.Y - l2.Y) * (l1.Y - l2.Y));
        }
        private static double GetLengthTotal(List<LocationBarModel> locations)
        {
            double a = 0;
            for (int i = 1; i < locations.Count; i++)
            {
                a += GetLengthFromLocation(locations[i - 1], locations[i]);
            }
            return a;
        }
        private static DetailItem GetAntiItem(double x0Start, double x0, double y0, double distance, double d, double l, double la, double lb, SettingModel settingModel, int i)
        {
            var aS = new DetailItem();
            aS.Location = new LocationBarModel(x0Start, y0);

            aS.GetPropertyStirrupSection(distance, d, l, la, lb);
            aS.RebarNumber = i;
            double hook = settingModel.SelectedHook.get_Parameter(BuiltInParameter.REBAR_HOOK_ANGLE).AsDouble();
            aS.AllLocation.Add(new LocationBarModel(x0, y0));
            if (PointModel.AreEqual(hook, Math.PI / 2))
            {
                aS.Type = DetailItemStyle.DT05;
            }
            if (PointModel.AreEqual(hook, 0.75 * Math.PI))
            {
                aS.Type = DetailItemStyle.DT06;
            }
            if (PointModel.AreEqual(hook, Math.PI))
            {
                aS.Type = DetailItemStyle.DT07;
            }
            return aS;
        }
        #endregion
    }
}
