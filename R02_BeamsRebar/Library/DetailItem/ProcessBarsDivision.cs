
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace R02_BeamsRebar
{
    public class ProcessBarsDivision
    {
        #region Main Top
        public static ObservableCollection<ItemDivision> GetMainTop(SingleMainTopBarModel SingleMainTopBarModel, List<MainTopBarModel> MainTopBarModel, DivisionBar DivisionBar, SelectedIndexModel SelectedIndexModel)
        {

            if (SelectedIndexModel.StyleMainTop == 0)
            {
                return GetMainTopSingle(SingleMainTopBarModel, DivisionBar);
            }
            else
            {
                return GetMainTopMulti(MainTopBarModel, DivisionBar);
            }
        }
        private static ObservableCollection<ItemDivision> GetMainTopSingle(SingleMainTopBarModel SingleMainTopBarModel, DivisionBar DivisionBar)
        {
            var MainTop = new ObservableCollection<ItemDivision>();
            double d = SingleMainTopBarModel.Bar.Diameter;
            if (SingleMainTopBarModel.Location.Count <= 4)
            {
                double length = GetLengthTotal(SingleMainTopBarModel.Location);
                double left = GetLengthFromLocation(SingleMainTopBarModel.Location[0], SingleMainTopBarModel.Location[1]);
                double right = GetLengthFromLocation(SingleMainTopBarModel.Location[SingleMainTopBarModel.Location.Count - 2], SingleMainTopBarModel.Location[SingleMainTopBarModel.Location.Count - 1]);
                double l = GetLengthFromLocation(SingleMainTopBarModel.Location[1], SingleMainTopBarModel.Location[2]);
                int total = DivisionBar.NumberBeams * SingleMainTopBarModel.NumberBar;
                int n0 = (int)(DivisionBar.Lmax / length);
                if (PointModel.AreEqual(length, DivisionBar.Lmax))
                {
                    var a = new ItemDivision("L1", total, d, l, left, right);
                    a.Location = new LocationBarModel(SingleMainTopBarModel.X0, SingleMainTopBarModel.Y0);
                    a.SetTypeDown(left, right);
                    a.GetAllLocation(0);
                    MainTop.Add(a);
                }
                else
                {
                    if (length > DivisionBar.Lmax)
                    {
                        MainTop = GetMainTopSingle00(SingleMainTopBarModel, DivisionBar, total, d, length, l, left, right);
                    }
                    else
                    {
                        var a = new ItemDivision("L1", total, d, l, left, right);
                        a.Location = new LocationBarModel(SingleMainTopBarModel.X0, SingleMainTopBarModel.Y0);
                        a.SetTypeDown(left, right);
                        a.GetAllLocation(0);
                        MainTop.Add(a);
                    }
                }
            }
            else
            {
                double length = GetLengthTotal(SingleMainTopBarModel.Location);
                double left = GetLengthFromLocation(SingleMainTopBarModel.Location[0], SingleMainTopBarModel.Location[1]);
                double right = GetLengthFromLocation(SingleMainTopBarModel.Location[SingleMainTopBarModel.Location.Count - 2], SingleMainTopBarModel.Location[SingleMainTopBarModel.Location.Count - 1]);
                int total = DivisionBar.NumberBeams * SingleMainTopBarModel.NumberBar;
                MainTop = GetMainTopSingle11(SingleMainTopBarModel, DivisionBar, total, d, length, left, right);
            }
            return MainTop;
        }
        private static ObservableCollection<ItemDivision> GetMainTopMulti(List<MainTopBarModel> MainTopBarModel, DivisionBar DivisionBar)
        {
            var MainTop = new ObservableCollection<ItemDivision>();
            for (int i = 0; i < MainTopBarModel.Count; i++)
            {
                var main = GetMultiMainTopBarItem(MainTopBarModel[i], DivisionBar);
                for (int j = 0; j < main.Count; j++)
                {
                    MainTop.Add(main[j]);
                }
            }
            for (int k = 0; k < MainTop.Count; k++)
            {
                MainTop[k].Name = "L" + (k + 1);
            }
            return MainTop;
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
        #endregion
        #region Single barItem
        private static ObservableCollection<ItemDivision> GetMainTopSingle00(SingleMainTopBarModel SingleMainTopBarModel, DivisionBar DivisionBar, int total, double d, double lenght, double l, double left, double right)
        {
            if (PointModel.AreEqual(left, 0))
            {
                if (PointModel.AreEqual(right, 0))
                {
                    return GetMainTopSingle01(SingleMainTopBarModel, DivisionBar, total, d, lenght);
                }
                else
                {
                    return GetMainTopSingle02(SingleMainTopBarModel, DivisionBar, total, d, lenght, l, left, right);
                }
            }
            else
            {
                if (PointModel.AreEqual(right, 0))
                {
                    return GetMainTopSingle03(SingleMainTopBarModel, DivisionBar, total, d, lenght, l, left, right);
                }
                else
                {
                    return GetMainTopSingle04(SingleMainTopBarModel, DivisionBar, total, d, lenght, l, left, right);
                }
            }
        }
        private static ObservableCollection<ItemDivision> GetMainTopSingle01(SingleMainTopBarModel SingleMainTopBarModel, DivisionBar DivisionBar, int total, double d, double lenght)
        {
            var MainTop = new ObservableCollection<ItemDivision>();
            double length0 = DivisionBar.Lmax + DivisionBar.Overlap * SingleMainTopBarModel.Bar.Diameter;
            double overlap = DivisionBar.Overlap * SingleMainTopBarModel.Bar.Diameter;
            var a = new ItemDivision("L1", total, d, DivisionBar.Lmax, 0, 0);
            a.Location = new LocationBarModel(SingleMainTopBarModel.X0, SingleMainTopBarModel.Y0);
            a.SetTypeDown(0, 0);
            a.GetAllLocation(0);
            MainTop.Add(a);
            double length1 = DivisionBar.Lmax;
            double deta = DivisionBar.Lmax - overlap;
            int n = 1;
            while (length1 < lenght)
            {
                length1 += deta;
                if (length1 >= lenght)
                {
                    double length2 = lenght - (length1 - deta) + overlap;
                    var a2 = new ItemDivision("L2", total, d, length2, 0, 0);
                    a2.Location = new LocationBarModel(SingleMainTopBarModel.X0 + length1 - deta - overlap, SingleMainTopBarModel.Y0);
                    a2.SetTypeDown(0, 0);
                    a2.GetAllLocation(0);
                    MainTop.Add(a2);

                    break;
                }
                else
                {
                    var a2 = new ItemDivision("Li" + n, total, d, DivisionBar.Lmax, 0, 0);
                    a2.Location = new LocationBarModel(SingleMainTopBarModel.X0 + length1 - deta - overlap, SingleMainTopBarModel.Y0);
                    a2.SetTypeDown(0, 0);
                    a2.GetAllLocation(0);
                    MainTop.Add(a2);
                }
                n++;
            }
            return MainTop;
        }
        private static ObservableCollection<ItemDivision> GetMainTopSingle02(SingleMainTopBarModel SingleMainTopBarModel, DivisionBar DivisionBar, int total, double d, double lenght, double l, double left, double right)
        {
            var MainTop = new ObservableCollection<ItemDivision>();
            double length0 = DivisionBar.Lmax + DivisionBar.Overlap * SingleMainTopBarModel.Bar.Diameter;
            double overlap = DivisionBar.Overlap * SingleMainTopBarModel.Bar.Diameter;
            if (l <= DivisionBar.Lmax)
            {
                var a = new ItemDivision("L1", total, d, l / 2 + overlap / 2, 0, 0);
                a.Location = new LocationBarModel(SingleMainTopBarModel.X0, SingleMainTopBarModel.Y0);
                a.SetTypeDown(0, 0);
                a.GetAllLocation(0);
                MainTop.Add(a);
                var a1 = new ItemDivision("L2", total, d, l / 2 + overlap / 2, 0, right);
                a1.Location = new LocationBarModel(SingleMainTopBarModel.X0 + l / 2 - overlap / 2, SingleMainTopBarModel.Y0);
                a1.SetTypeDown(0, right);
                a1.GetAllLocation(0);
                MainTop.Add(a1);
            }
            else
            {
                var a = new ItemDivision("L1", total, d, DivisionBar.Lmax, 0, 0);
                a.Location = new LocationBarModel(SingleMainTopBarModel.X0, SingleMainTopBarModel.Y0);
                a.SetTypeDown(0, 0);
                a.GetAllLocation(0);
                MainTop.Add(a);
                double length1 = DivisionBar.Lmax;
                double deta = DivisionBar.Lmax - overlap;
                int n = 1;
                while (length1 < l)
                {
                    length1 += deta;
                    if (length1 >= l)
                    {
                        double length2 = l - (length1 - deta) + overlap;
                        int n0 = (int)(DivisionBar.Lmax / length2 + right);
                        var a2 = new ItemDivision("L2", total, d, length2, 0, right);
                        a2.Location = new LocationBarModel(SingleMainTopBarModel.X0 + length1 - deta - overlap, SingleMainTopBarModel.Y0);
                        a2.SetTypeDown(0, right);
                        a2.GetAllLocation(0);
                        MainTop.Add(a2);
                        break;
                    }
                    else
                    {
                        var a2 = new ItemDivision("Li" + n, total, d, DivisionBar.Lmax, 0, 0);
                        a2.Location = new LocationBarModel(SingleMainTopBarModel.X0 + length1 - deta - overlap, SingleMainTopBarModel.Y0);
                        a2.SetTypeDown(0, 0);
                        a2.GetAllLocation(0);
                        MainTop.Add(a2);
                    }
                    n++;
                }
            }

            return MainTop;
        }
        private static ObservableCollection<ItemDivision> GetMainTopSingle03(SingleMainTopBarModel SingleMainTopBarModel, DivisionBar DivisionBar, int total, double d, double lenght, double l, double left, double right)
        {
            var MainTop = new ObservableCollection<ItemDivision>();
            double length0 = DivisionBar.Lmax + DivisionBar.Overlap * SingleMainTopBarModel.Bar.Diameter;
            double overlap = DivisionBar.Overlap * SingleMainTopBarModel.Bar.Diameter;
            var a = new ItemDivision("L1", total, d, DivisionBar.Lmax - left, left, 0);
            a.Location = new LocationBarModel(SingleMainTopBarModel.X0, SingleMainTopBarModel.Y0);
            a.SetTypeDown(left, 0);
            a.GetAllLocation(0);
            MainTop.Add(a);
            double length1 = DivisionBar.Lmax - left;
            double deta = DivisionBar.Lmax - overlap;
            int n = 1;
            while (length1 < l)
            {
                length1 += deta;
                if (length1 >= l)
                {
                    double length2 = l - (length1 - deta) + overlap;
                    int n0 = (int)(DivisionBar.Lmax / length2);
                    var a2 = new ItemDivision("L2", total, d, length2, 0, 0);
                    a2.Location = new LocationBarModel(SingleMainTopBarModel.X0 + length1 - deta - overlap, SingleMainTopBarModel.Y0);
                    a2.SetTypeDown(0, 0);
                    a2.GetAllLocation(0);
                    MainTop.Add(a2);
                    break;
                }
                else
                {
                    var a2 = new ItemDivision("Li" + n, total, d, DivisionBar.Lmax, 0, 0);
                    a2.Location = new LocationBarModel(SingleMainTopBarModel.X0 + length1 - deta - overlap, SingleMainTopBarModel.Y0);
                    a2.SetTypeDown(0, 0);
                    a2.GetAllLocation(0);
                    MainTop.Add(a2);
                }
                n++;
            }

            return MainTop;
        }
        private static ObservableCollection<ItemDivision> GetMainTopSingle04(SingleMainTopBarModel SingleMainTopBarModel, DivisionBar DivisionBar, int total, double d, double lenght, double l, double left, double right)
        {
            var MainTop = new ObservableCollection<ItemDivision>();
            double length0 = DivisionBar.Lmax + DivisionBar.Overlap * SingleMainTopBarModel.Bar.Diameter;
            double overlap = DivisionBar.Overlap * SingleMainTopBarModel.Bar.Diameter;
            if (l + left <= DivisionBar.Lmax)
            {
                var a = new ItemDivision("L1", total, d, l / 2 + overlap / 2, left, 0);
                a.Location = new LocationBarModel(SingleMainTopBarModel.X0, SingleMainTopBarModel.Y0);
                a.SetTypeDown(left, 0);
                a.GetAllLocation(0);
                MainTop.Add(a);
                var a1 = new ItemDivision("L2", total, d, l / 2 + overlap / 2, 0, right);
                a1.Location = new LocationBarModel(SingleMainTopBarModel.X0 + l / 2 - overlap / 2, SingleMainTopBarModel.Y0);
                a1.SetTypeDown(0, right);
                a1.GetAllLocation(0);
                MainTop.Add(a1);
            }
            else
            {
                var a = new ItemDivision("L1", total, d, DivisionBar.Lmax - left, left, 0);
                a.Location = new LocationBarModel(SingleMainTopBarModel.X0, SingleMainTopBarModel.Y0);
                a.SetTypeDown(left, 0);
                a.GetAllLocation(0);
                MainTop.Add(a);
                double length1 = DivisionBar.Lmax - left;
                double deta = DivisionBar.Lmax - overlap;
                int n = 1;
                while (length1 < l)
                {
                    length1 += deta;
                    if (length1 >= l)
                    {
                        double length2 = l - (length1 - deta) + overlap;
                        int n0 = (int)(DivisionBar.Lmax / (length2 + right));
                        var a2 = new ItemDivision("L2", total, d, length2, 0, right);
                        a2.Location = new LocationBarModel(SingleMainTopBarModel.X0 + length1 - deta - overlap, SingleMainTopBarModel.Y0);
                        a2.SetTypeDown(0, right);
                        a2.GetAllLocation(0);
                        MainTop.Add(a2);
                        break;
                    }
                    else
                    {
                        var a2 = new ItemDivision("Li" + n, total, d, DivisionBar.Lmax, 0, 0);
                        a2.Location = new LocationBarModel(SingleMainTopBarModel.X0 + length1 - deta - overlap, SingleMainTopBarModel.Y0);
                        a2.SetTypeDown(0, 0);
                        a2.GetAllLocation(0);
                        MainTop.Add(a2);
                    }
                    n++;
                }
            }

            return MainTop;
        }

        private static ObservableCollection<ItemDivision> GetMainTopSingle11(SingleMainTopBarModel SingleMainTopBarModel, DivisionBar DivisionBar, int total, double d, double lenght, double left, double right)
        {
            var MainTop = new ObservableCollection<ItemDivision>();
            double overlap = DivisionBar.Overlap * SingleMainTopBarModel.Bar.Diameter;
            double lxa1 = GetLengthFromLocation(SingleMainTopBarModel.Location[1], SingleMainTopBarModel.Location[1 + 1]) / 2 + overlap / 2;
            ItemDivision a1 = new ItemDivision("L1", total, d, lxa1, left, 0);
            a1.Location = new LocationBarModel(SingleMainTopBarModel.Location[1].X, SingleMainTopBarModel.Location[1].Y);
            a1.Type = (PointModel.AreEqual(left, 0)) ? DetailShopStyle.DS00 : DetailShopStyle.DS01;
            a1.GetAllLocation(0);
            MainTop.Add(a1);
            int li = 1;
            for (int i = 1; i < SingleMainTopBarModel.Location.Count - 3; i += 2)
            {
                double lxa = GetLengthFromLocation(SingleMainTopBarModel.Location[i], SingleMainTopBarModel.Location[i + 1]);
                double l = GetLengthFromLocation(SingleMainTopBarModel.Location[i + 1], SingleMainTopBarModel.Location[i + 2]);
                double lxb = GetLengthFromLocation(SingleMainTopBarModel.Location[i + 2], SingleMainTopBarModel.Location[i + 3]);
                ItemDivision a = GetMid(i, li, overlap, SingleMainTopBarModel, DivisionBar, total, d, lxa, l, lxb);
                a.GetAllLocation(SingleMainTopBarModel.Location[i + 2].Y);
                double b1 = a.Lb - overlap / 2;
                double b2 = a.Lb - overlap / 2;
                MainTop.Add(a);
                li++;
            }
            double lxaLast = GetLengthFromLocation(SingleMainTopBarModel.Location[SingleMainTopBarModel.Location.Count - 3], SingleMainTopBarModel.Location[SingleMainTopBarModel.Location.Count - 2]) / 2 + overlap / 2;

            ItemDivision aLast = new ItemDivision("L2", total, d, lxaLast, 0, right);
            aLast.Location = new LocationBarModel(SingleMainTopBarModel.Location[SingleMainTopBarModel.Location.Count - 2].X - lxaLast, SingleMainTopBarModel.Location[SingleMainTopBarModel.Location.Count - 2].Y);
            aLast.Type = (PointModel.AreEqual(right, 0)) ? DetailShopStyle.DS00 : DetailShopStyle.DS02;
            aLast.GetAllLocation(0);
            MainTop.Add(aLast);
            return MainTop;
        }

        #endregion
        #region MultiMainTopBarItem
        private static ObservableCollection<ItemDivision> GetMultiMainTopBarItem(MainTopBarModel MainTopBarModel, DivisionBar DivisionBar)
        {
            var MainTop = new ObservableCollection<ItemDivision>();
            double length = GetLengthTotal(MainTopBarModel.Location);
            double left = MainTopBarModel.La;
            double right = MainTopBarModel.Lb;
            double l = 0;
            if (PointModel.AreEqual(left, 0))
            {
                if (PointModel.AreEqual(right, 0))
                {
                    l = length;
                }
                else
                {
                    l = length - right;
                }
            }
            else
            {
                if (PointModel.AreEqual(right, 0))
                {
                    l = length - left;
                }
                else
                {
                    l = length - left - right;
                }
            }
            int total = DivisionBar.NumberBeams * MainTopBarModel.NumberBar;
            int n0 = (int)(DivisionBar.Lmax / length);
            double d = MainTopBarModel.Bar.Diameter;
            double x0 = (MainTopBarModel.Exa == 0) ? MainTopBarModel.X0 : MainTopBarModel.X0 - MainTopBarModel.Exa;
            if (PointModel.AreEqual(length, DivisionBar.Lmax))
            {
                var a = new ItemDivision("L1", total, d, l, left, right);
                a.Location = new LocationBarModel(x0, MainTopBarModel.Y0);
                a.SetTypeDown(left, right);
                a.GetAllLocation(0);
                MainTop.Add(a);
            }
            else
            {
                if (length > DivisionBar.Lmax)
                {
                    MainTop = GetMultiMainTop00(MainTopBarModel, DivisionBar, total, d, length, l, left, right);
                }
                else
                {
                    var a = new ItemDivision("L1", total, d, l, left, right);
                    a.Location = new LocationBarModel(x0, MainTopBarModel.Y0);
                    a.SetTypeDown(left, right);
                    a.GetAllLocation(0);
                    MainTop.Add(a);
                }

            }
            return MainTop;
        }
        private static ObservableCollection<ItemDivision> GetMultiMainTop00(MainTopBarModel MainTopBarModel, DivisionBar DivisionBar, int total, double d, double lenght, double l, double left, double right)
        {
            if (PointModel.AreEqual(left, 0))
            {
                if (PointModel.AreEqual(right, 0))
                {
                    return GetMultiMainTop01(MainTopBarModel, DivisionBar, total, d, lenght);
                }
                else
                {
                    return GetMultiMainTop02(MainTopBarModel, DivisionBar, total, d, lenght, l, left, right);
                }
            }
            else
            {
                if (PointModel.AreEqual(right, 0))
                {
                    return GetMultiMainTop03(MainTopBarModel, DivisionBar, total, d, lenght, l, left, right);
                }
                else
                {
                    return GetMultiMainTop04(MainTopBarModel, DivisionBar, total, d, lenght, l, left, right);
                }
            }
        }
        private static ObservableCollection<ItemDivision> GetMultiMainTop01(MainTopBarModel MainTopBarModel, DivisionBar DivisionBar, int total, double d, double lenght)
        {
            var MainTop = new ObservableCollection<ItemDivision>();
            double length0 = DivisionBar.Lmax + DivisionBar.Overlap * MainTopBarModel.Bar.Diameter;
            double overlap = DivisionBar.Overlap * MainTopBarModel.Bar.Diameter;
            var a = new ItemDivision("L1", total, d, DivisionBar.Lmax, 0, 0);
            a.Location = new LocationBarModel(MainTopBarModel.X0, MainTopBarModel.Y0);
            a.SetTypeDown(0, 0);
            a.GetAllLocation(0);
            MainTop.Add(a);
            double length1 = DivisionBar.Lmax;
            double deta = DivisionBar.Lmax - overlap;
            int n = 1;
            while (length1 < lenght)
            {
                length1 += deta;
                if (length1 >= lenght)
                {
                    double length2 = lenght - (length1 - deta) + overlap;
                    int n0 = (int)(DivisionBar.Lmax / length2);
                    var a2 = new ItemDivision("L2", total, d, length2, 0, 0);
                    a2.Location = new LocationBarModel(MainTopBarModel.X0 + length1 - deta - overlap, MainTopBarModel.Y0);
                    a2.SetTypeDown(0, 0);
                    a2.GetAllLocation(0);
                    MainTop.Add(a2);

                    break;
                }
                else
                {
                    var a2 = new ItemDivision("Li" + n, total, d, DivisionBar.Lmax, 0, 0);
                    a2.Location = new LocationBarModel(MainTopBarModel.X0 + length1 - deta - overlap, MainTopBarModel.Y0);
                    a2.SetTypeDown(0, 0);
                    a2.GetAllLocation(0);
                    MainTop.Add(a2);
                }
                n++;
            }
            return MainTop;
        }
        private static ObservableCollection<ItemDivision> GetMultiMainTop02(MainTopBarModel MainTopBarModel, DivisionBar DivisionBar, int total, double d, double lenght, double l, double left, double right)
        {
            var MainTop = new ObservableCollection<ItemDivision>();
            double length0 = DivisionBar.Lmax + DivisionBar.Overlap * MainTopBarModel.Bar.Diameter;
            double overlap = DivisionBar.Overlap * MainTopBarModel.Bar.Diameter;
            if (l <= DivisionBar.Lmax)
            {
                var a = new ItemDivision("L1", total, d, l / 2 + overlap / 2, 0, 0);
                a.Location = new LocationBarModel(MainTopBarModel.X0, MainTopBarModel.Y0);
                a.SetTypeDown(0, 0);
                a.GetAllLocation(0);
                MainTop.Add(a);
                var a1 = new ItemDivision("L2", total, d, l / 2 + overlap / 2, 0, right);
                a1.Location = new LocationBarModel(MainTopBarModel.X0 + l / 2 - overlap / 2, MainTopBarModel.Y0);
                a1.SetTypeDown(0, right);
                a1.GetAllLocation(0);
                MainTop.Add(a1);
            }
            else
            {
                var a = new ItemDivision("L1", total, d, DivisionBar.Lmax, 0, 0);
                a.Location = new LocationBarModel(MainTopBarModel.X0, MainTopBarModel.Y0);
                a.SetTypeDown(0, 0);
                a.GetAllLocation(0);
                MainTop.Add(a);
                double length1 = DivisionBar.Lmax;
                double deta = DivisionBar.Lmax - overlap;
                int n = 1;
                while (length1 < l)
                {
                    length1 += deta;
                    if (length1 >= l)
                    {
                        double length2 = l - (length1 - deta) + overlap;
                        int n0 = (int)(DivisionBar.Lmax / length2 + right);
                        var a2 = new ItemDivision("L2", total, d, length2, 0, right);
                        a2.Location = new LocationBarModel(MainTopBarModel.X0 + length1 - deta - overlap, MainTopBarModel.Y0);
                        a2.SetTypeDown(0, right);
                        a2.GetAllLocation(0);
                        MainTop.Add(a2);
                        break;
                    }
                    else
                    {
                        var a2 = new ItemDivision("Li" + n, total, d, DivisionBar.Lmax, 0, 0);
                        a2.Location = new LocationBarModel(MainTopBarModel.X0 + length1 - deta - overlap, MainTopBarModel.Y0);
                        a2.SetTypeDown(0, 0);
                        a2.GetAllLocation(0);
                        MainTop.Add(a2);
                    }
                    n++;
                }
            }

            return MainTop;
        }
        private static ObservableCollection<ItemDivision> GetMultiMainTop03(MainTopBarModel MainTopBarModel, DivisionBar DivisionBar, int total, double d, double lenght, double l, double left, double right)
        {
            var MainTop = new ObservableCollection<ItemDivision>();
            double length0 = DivisionBar.Lmax + DivisionBar.Overlap * MainTopBarModel.Bar.Diameter;
            double overlap = DivisionBar.Overlap * MainTopBarModel.Bar.Diameter;
            var a = new ItemDivision("L1", total, d, DivisionBar.Lmax - left, left, 0);
            a.Location = new LocationBarModel(MainTopBarModel.X0, MainTopBarModel.Y0);
            a.SetTypeDown(left, 0);
            a.GetAllLocation(0);
            MainTop.Add(a);
            double length1 = DivisionBar.Lmax - left;
            double deta = DivisionBar.Lmax - overlap;
            int n = 1;
            while (length1 < l)
            {
                length1 += deta;
                if (length1 >= l)
                {
                    double length2 = l - (length1 - deta) + overlap;
                    int n0 = (int)(DivisionBar.Lmax / length2);
                    var a2 = new ItemDivision("L2", total, d, length2, 0, 0);
                    a2.Location = new LocationBarModel(MainTopBarModel.X0 + length1 - deta - overlap, MainTopBarModel.Y0);
                    a2.SetTypeDown(0, 0);
                    a2.GetAllLocation(0);
                    MainTop.Add(a2);
                    break;
                }
                else
                {
                    var a2 = new ItemDivision("Li" + n, total, d, DivisionBar.Lmax, 0, 0);
                    a2.Location = new LocationBarModel(MainTopBarModel.X0 + length1 - deta - overlap, MainTopBarModel.Y0);
                    a2.SetTypeDown(0, 0);
                    a2.GetAllLocation(0);
                    MainTop.Add(a2);
                }
                n++;
            }

            return MainTop;
        }
        private static ObservableCollection<ItemDivision> GetMultiMainTop04(MainTopBarModel MainTopBarModel, DivisionBar DivisionBar, int total, double d, double lenght, double l, double left, double right)
        {
            var MainTop = new ObservableCollection<ItemDivision>();
            double length0 = DivisionBar.Lmax + DivisionBar.Overlap * MainTopBarModel.Bar.Diameter;
            double overlap = DivisionBar.Overlap * MainTopBarModel.Bar.Diameter;
            if (l + left <= DivisionBar.Lmax)
            {
                var a = new ItemDivision("L1", total, d, l / 2 + overlap / 2, left, 0);
                a.Location = new LocationBarModel(MainTopBarModel.X0, MainTopBarModel.Y0);
                a.SetTypeDown(left, 0);
                a.GetAllLocation(0);
                MainTop.Add(a);
                var a1 = new ItemDivision("L2", total, d, l / 2 + overlap / 2, 0, right);
                a1.Location = new LocationBarModel(MainTopBarModel.X0 + l / 2 - overlap / 2, MainTopBarModel.Y0);
                a1.SetTypeDown(0, right);
                a1.GetAllLocation(0);
                MainTop.Add(a1);
            }
            else
            {
                var a = new ItemDivision("L1", total, d, DivisionBar.Lmax - left, left, 0);
                a.Location = new LocationBarModel(MainTopBarModel.X0, MainTopBarModel.Y0);
                a.SetTypeDown(left, 0);
                a.GetAllLocation(0);
                MainTop.Add(a);
                double length1 = DivisionBar.Lmax - left;
                double deta = DivisionBar.Lmax - overlap;
                int n = 1;
                while (length1 < l)
                {
                    length1 += deta;
                    if (length1 >= l)
                    {
                        double length2 = l - (length1 - deta) + overlap;
                        int n0 = (int)(DivisionBar.Lmax / (length2 + right));
                        var a2 = new ItemDivision("L2", total, d, length2, 0, right);
                        a2.Location = new LocationBarModel(MainTopBarModel.X0 + length1 - deta - overlap, MainTopBarModel.Y0);
                        a2.SetTypeDown(0, right);
                        a2.GetAllLocation(0);
                        MainTop.Add(a2);
                        break;
                    }
                    else
                    {
                        var a2 = new ItemDivision("Li" + n, total, d, DivisionBar.Lmax, 0, 0);
                        a2.Location = new LocationBarModel(MainTopBarModel.X0 + length1 - deta - overlap, MainTopBarModel.Y0);
                        a2.SetTypeDown(0, 0);
                        a2.GetAllLocation(0);
                        MainTop.Add(a2);
                    }
                    n++;
                }
            }

            return MainTop;
        }
        #endregion
        #region Main Bottom
        public static ObservableCollection<ItemDivision> GetMainBottomMulti(List<MainBottomBarModel> MainBottomBarModel, DivisionBar DivisionBar)
        {

            var MainBottom = new ObservableCollection<ItemDivision>();
            for (int i = 0; i < MainBottomBarModel.Count; i++)
            {
                var main = GetMultiMainBottomBarItem(MainBottomBarModel[i], DivisionBar);
                for (int j = 0; j < main.Count; j++)
                {
                    MainBottom.Add(main[j]);
                }
            }
            for (int k = 0; k < MainBottom.Count; k++)
            {
                MainBottom[k].Name = "L" + (k + 1);
            }
            MainBottom = new ObservableCollection<ItemDivision>(MainBottom.OrderBy(x => x.Location.X));
            return MainBottom;
        }
        private static ObservableCollection<ItemDivision> GetMultiMainBottomBarItem(MainBottomBarModel MainBottomBarModel, DivisionBar DivisionBar)
        {
            var MainBottom = new ObservableCollection<ItemDivision>();
            double length = GetLengthTotal(MainBottomBarModel.Location);
            double left = MainBottomBarModel.La;
            double right = MainBottomBarModel.Lb;
            double l = 0;
            if (PointModel.AreEqual(left, 0))
            {
                if (PointModel.AreEqual(right, 0))
                {
                    l = length;
                }
                else
                {
                    l = length - right;
                }
            }
            else
            {
                if (PointModel.AreEqual(right, 0))
                {
                    l = length - left;
                }
                else
                {
                    l = length - left - right;
                }
            }
            int total = DivisionBar.NumberBeams * MainBottomBarModel.NumberBar;
            int n0 = (int)(DivisionBar.Lmax / length);
            double d = MainBottomBarModel.Bar.Diameter;
            double x0 = (MainBottomBarModel.Exa == 0) ? MainBottomBarModel.X0 : MainBottomBarModel.X0 - MainBottomBarModel.Exa;
            if (PointModel.AreEqual(length, DivisionBar.Lmax))
            {
                var a = new ItemDivision("L1", total, d, l, left, right);
                a.Location = new LocationBarModel(x0, MainBottomBarModel.Y0);
                a.SetTypeUp(left, right);
                a.GetAllLocation(0);
                MainBottom.Add(a);
            }
            else
            {
                if (length > DivisionBar.Lmax)
                {
                    MainBottom = GetMultiMainBottom00(MainBottomBarModel, DivisionBar, total, d, x0, length, l, left, right);
                }
                else
                {
                    var a = new ItemDivision("L1", total, d, l, left, right);
                    a.Location = new LocationBarModel(x0, MainBottomBarModel.Y0);
                    a.SetTypeUp(left, right);
                    a.GetAllLocation(0);
                    MainBottom.Add(a);
                }

            }

            return MainBottom;
        }

        private static ObservableCollection<ItemDivision> GetMultiMainBottom00(MainBottomBarModel MainBottomBarModel, DivisionBar DivisionBar, int total, double d, double x0, double length, double l, double left, double right)
        {
            if (PointModel.AreEqual(left, 0))
            {
                if (PointModel.AreEqual(right, 0))
                {
                    return GetMultiMainBottom01(MainBottomBarModel, DivisionBar, total, d, x0, length);
                }
                else
                {
                    return GetMultiMainBottom02(MainBottomBarModel, DivisionBar, total, d, x0, length, l, left, right);
                }
            }
            else
            {
                if (PointModel.AreEqual(right, 0))
                {
                    return GetMultiMainBottom03(MainBottomBarModel, DivisionBar, total, d, x0, length, l, left, right);
                }
                else
                {
                    return GetMultiMainBottom04(MainBottomBarModel, DivisionBar, total, d, x0, length, l, left, right);
                }
            }
        }

        private static ObservableCollection<ItemDivision> GetMultiMainBottom01(MainBottomBarModel MainBottomBarModel, DivisionBar DivisionBar, int total, double d, double x0, double length)
        {
            var MainBottom = new ObservableCollection<ItemDivision>();
            double length0 = DivisionBar.Lmax + DivisionBar.Overlap * MainBottomBarModel.Bar.Diameter;
            double overlap = DivisionBar.Overlap * MainBottomBarModel.Bar.Diameter;
            double x01 = x0 + length - DivisionBar.Lmax;
            double y0 = MainBottomBarModel.Y0;
            var a = new ItemDivision("L2", total, d, DivisionBar.Lmax, 0, 0);
            a.Location = new LocationBarModel(x01, y0);
            a.SetTypeUp(0, 0);
            a.GetAllLocation(0);
            MainBottom.Add(a);
            int n = 1;
            
            while (x01 > 0)
            {
                x01 -= DivisionBar.Lmax - overlap;
                if (x01 < 0)
                {
                    x01 += DivisionBar.Lmax - overlap;
                    double length1 = x01 - x0 + overlap;
                    var a1 = new ItemDivision("L1", total, d, length1, 0, 0);
                    a1.Location = new LocationBarModel(x0, y0);
                    a1.SetTypeUp(0, 0);
                    a1.GetAllLocation(0);
                    MainBottom.Add(a1);
                    break;
                }
                else
                {
                    var a2 = new ItemDivision("Li" + n, total, d, DivisionBar.Lmax, 0, 0);
                    a2.Location = new LocationBarModel(x01, y0);
                    a2.SetTypeUp(0, 0);
                    a2.GetAllLocation(0);
                    MainBottom.Add(a2);
                }
                n++;
            }
            
            return MainBottom;
        }

        private static ObservableCollection<ItemDivision> GetMultiMainBottom02(MainBottomBarModel MainBottomBarModel, DivisionBar DivisionBar, int total, double d, double x0, double length, double l, double left, double right)
        {
            var MainBottom = new ObservableCollection<ItemDivision>();
            double length0 = DivisionBar.Lmax + DivisionBar.Overlap * MainBottomBarModel.Bar.Diameter;
            double overlap = DivisionBar.Overlap * MainBottomBarModel.Bar.Diameter;
            double x01 = x0 + l - DivisionBar.Lmax + right;
            double y0 = MainBottomBarModel.Y0;
            var a = new ItemDivision("L2", total, d, DivisionBar.Lmax - right, 0, right);
            a.Location = new LocationBarModel(x01, y0);
            a.SetTypeUp(0, right);
            a.GetAllLocation(0);
            MainBottom.Add(a);
            int n = 1;
            while (x01 > 0)
            {
                x01 -= DivisionBar.Lmax - overlap;
                if (x01 < 0)
                {
                    x01 += DivisionBar.Lmax - overlap;
                    double length1 = x01 - x0 + overlap;
                    var a1 = new ItemDivision("L1", total, d, length1, 0, 0);
                    a1.Location = new LocationBarModel(x0, y0);
                    a1.SetTypeUp(0, 0);
                    a1.GetAllLocation(0);
                    MainBottom.Add(a1);
                    break;
                }
                else
                {
                    var a2 = new ItemDivision("Li" + n, total, d, DivisionBar.Lmax, 0, 0);
                    a2.Location = new LocationBarModel(x01, y0);
                    a2.SetTypeUp(0, 0);
                    a2.GetAllLocation(0);
                    MainBottom.Add(a2);
                }
                n++;
            }
            

            return MainBottom;
        }

        private static ObservableCollection<ItemDivision> GetMultiMainBottom03(MainBottomBarModel MainBottomBarModel, DivisionBar DivisionBar, int total, double d, double x0, double length, double l, double left, double right)
        {
            var MainBottom = new ObservableCollection<ItemDivision>();
            double length0 = DivisionBar.Lmax + DivisionBar.Overlap * MainBottomBarModel.Bar.Diameter;
            double overlap = DivisionBar.Overlap * MainBottomBarModel.Bar.Diameter;
            double y0 = MainBottomBarModel.Y0;
            if (l <= DivisionBar.Lmax)
            {
                var a = new ItemDivision("L2", total, d, l / 2 + overlap / 2, 0, 0);
                a.Location = new LocationBarModel(x0 + l / 2 - overlap / 2, y0);
                a.SetTypeUp(0, 0);
                a.GetAllLocation(0);
                MainBottom.Add(a);
                var a1 = new ItemDivision("L1", total, d, l / 2 + overlap / 2, left, 0);
                a1.Location = new LocationBarModel(x0, y0);
                a1.SetTypeUp(left, 0);
                a1.GetAllLocation(0);
                MainBottom.Add(a1);
            }
            else
            {
                double x01 = x0 + l - DivisionBar.Lmax;
                var a = new ItemDivision("L2", total, d, DivisionBar.Lmax, 0, 0);
                a.Location = new LocationBarModel(x01, y0);
                a.SetTypeUp(0, 0);
                a.GetAllLocation(0);
                MainBottom.Add(a);
                int n = 1;
                while (x01 > 0)
                {
                    x01 -= DivisionBar.Lmax - overlap;
                    if (x01 < 0)
                    {
                        x01 += DivisionBar.Lmax - overlap;
                        double length1 = x01 - x0 + overlap;
                        var a1 = new ItemDivision("L1", total, d, length1, left, 0);
                        a1.Location = new LocationBarModel(x0, y0);
                        a1.SetTypeUp(left, 0);
                        a1.GetAllLocation(0);
                        MainBottom.Add(a1);
                        break;
                    }
                    else
                    {
                        var a2 = new ItemDivision("Li" + n, total, d, DivisionBar.Lmax, 0, 0);
                        a2.Location = new LocationBarModel(x01, y0);
                        a2.SetTypeUp(0, 0);
                        a2.GetAllLocation(0);
                        MainBottom.Add(a2);
                    }
                    n++;
                }
            }

            
            return MainBottom;
        }

        private static ObservableCollection<ItemDivision> GetMultiMainBottom04(MainBottomBarModel MainBottomBarModel, DivisionBar DivisionBar, int total, double d, double x0, double length, double l, double left, double right)
        {
            var MainBottom = new ObservableCollection<ItemDivision>();
            double length0 = DivisionBar.Lmax + DivisionBar.Overlap * MainBottomBarModel.Bar.Diameter;
            double overlap = DivisionBar.Overlap * MainBottomBarModel.Bar.Diameter;
            double y0 = MainBottomBarModel.Y0;
            if (l + right <= DivisionBar.Lmax)
            {
                var a = new ItemDivision("L2", total, d, l / 2 + overlap / 2, 0, right);
                a.Location = new LocationBarModel(x0 + l / 2 - overlap / 2, y0);
                a.SetTypeUp(0, right);
                a.GetAllLocation(0);
                MainBottom.Add(a);
                var a1 = new ItemDivision("L1", total, d, l / 2 + overlap / 2, left, 0);
                a1.Location = new LocationBarModel(x0, y0);
                a1.SetTypeUp(left, 0);
                a1.GetAllLocation(0);
                MainBottom.Add(a1);
            }
            else
            {
                double x01 = x0 + l - DivisionBar.Lmax + right;
                var a = new ItemDivision("L2", total, d, DivisionBar.Lmax - right, 0, right);
                a.Location = new LocationBarModel(x01, y0);
                a.SetTypeUp(0, right);
                a.GetAllLocation(0);
                MainBottom.Add(a);
                int n = 1;
                while (x01 > 0)
                {
                    x01 -= DivisionBar.Lmax - overlap;
                    if (x01 < 0)
                    {
                        x01 += DivisionBar.Lmax - overlap;
                        double length1 = x01 - x0 + overlap;
                        var a1 = new ItemDivision("L1", total, d, length1, left, 0);
                        a1.Location = new LocationBarModel(x0, y0);
                        a1.SetTypeUp(left, 0);
                        a1.GetAllLocation(0);
                        MainBottom.Add(a1);
                        break;
                    }
                    else
                    {
                        var a2 = new ItemDivision("Li" + n, total, d, DivisionBar.Lmax, 0, 0);
                        a2.Location = new LocationBarModel(x01, y0);
                        a2.SetTypeUp(0, 0);
                        a2.GetAllLocation(0);
                        MainBottom.Add(a2);
                    }
                    n++;
                }
            }
            
            return MainBottom;
        }
        #endregion
        #region Additional 
        public static ObservableCollection<ItemDivision> GetAddTop(AddTopBarModel AddTopBarModel, DivisionBar DivisionBar, SelectedIndexModel SelectedIndexModel)
        {
            int li = 1;
            var AddTop = new ObservableCollection<ItemDivision>();
            if (SelectedIndexModel.StartTopChecked)
            {
                if (AddTopBarModel.Start.Model.Count != 0)
                {
                    for (int i = 0; i < AddTopBarModel.Start.Model.Count; i++)
                    {
                        int total = DivisionBar.NumberBeams * AddTopBarModel.Start.Model[i].NumberBar;
                        double d = AddTopBarModel.Start.Model[i].Bar.Diameter;
                        double length = GetLengthTotal(AddTopBarModel.Start.Model[i].Location);
                        double left = GetLengthFromLocation(AddTopBarModel.Start.Model[i].Location[0], AddTopBarModel.Start.Model[i].Location[1]);
                        double l = GetLengthFromLocation(AddTopBarModel.Start.Model[i].Location[1], AddTopBarModel.Start.Model[i].Location[2]);
                        var a = new ItemDivision("Li " + (li), total, d, l, left, 0);
                        a.Location = new LocationBarModel(AddTopBarModel.Start.Model[i].X0, AddTopBarModel.Start.Model[i].Y0);
                        a.SetTypeDown(left, 0);
                        a.GetAllLocation(0);
                        AddTop.Add(a);
                        li++;
                    }
                }
            }

            if (AddTopBarModel.Mid != null)
            {
                for (int i = 0; i < AddTopBarModel.Mid.Count; i++)
                {
                    if (AddTopBarModel.Mid[i].Model.Count != 0)
                    {
                        for (int j = 0; j < AddTopBarModel.Mid[i].Model.Count; j++)
                        {
                            int total = DivisionBar.NumberBeams * AddTopBarModel.Mid[i].Model[j].NumberBar;
                            double d = AddTopBarModel.Mid[i].Model[j].Bar.Diameter;
                            double length = GetLengthTotal(AddTopBarModel.Mid[i].Model[j].Location);
                            if (AddTopBarModel.Mid[i].Model[j].ConditionMidAddTop())
                            {
                                var a = new ItemDivision("Li " + (li), total, d, length, 0, 0);
                                a.Location = new LocationBarModel(AddTopBarModel.Mid[i].Model[j].Location[0].X, AddTopBarModel.Mid[i].Model[j].Location[0].Y);
                                a.SetTypeDown(0, 0);
                                a.GetAllLocation(0);
                                AddTop.Add(a);
                            }
                            else
                            {
                                double left = GetLengthFromLocation(AddTopBarModel.Mid[i].Model[j].Location[0], AddTopBarModel.Mid[i].Model[j].Location[1]);
                                double right = GetLengthFromLocation(AddTopBarModel.Mid[i].Model[j].Location[AddTopBarModel.Mid[i].Model[j].Location.Count - 2], AddTopBarModel.Mid[i].Model[j].Location[AddTopBarModel.Mid[i].Model[j].Location.Count - 1]);
                                double l = GetLengthFromLocation(AddTopBarModel.Mid[i].Model[j].Location[1], AddTopBarModel.Mid[i].Model[j].Location[2]);
                                var a = new ItemDivision("Li " + (li), total, d, l, left, right);
                                a.Location = new LocationBarModel(AddTopBarModel.Mid[i].Model[j].Location[0].X, AddTopBarModel.Mid[i].Model[j].Location[0].Y);
                                a.Type = (AddTopBarModel.Mid[i].Model[j].Location[1].Y < AddTopBarModel.Mid[i].Model[j].Location[2].Y) ? DetailShopStyle.DS07 : DetailShopStyle.DS08;
                                a.GetAllLocation(AddTopBarModel.Mid[i].Model[j].Location[2].Y);
                                AddTop.Add(a);
                            }
                            li++;
                        }
                    }
                }
            }
            if (SelectedIndexModel.EndTopChecked)
            {
                if (AddTopBarModel.End.Model.Count != 0)
                {
                    for (int i = 0; i < AddTopBarModel.End.Model.Count; i++)
                    {
                        int total = DivisionBar.NumberBeams * AddTopBarModel.End.Model[i].NumberBar;
                        double d = AddTopBarModel.End.Model[i].Bar.Diameter;
                        double length = GetLengthTotal(AddTopBarModel.End.Model[i].Location);
                        double right = GetLengthFromLocation(AddTopBarModel.End.Model[i].Location[AddTopBarModel.End.Model[i].Location.Count - 2], AddTopBarModel.End.Model[i].Location[AddTopBarModel.End.Model[i].Location.Count - 1]);
                        double l = GetLengthFromLocation(AddTopBarModel.End.Model[i].Location[0], AddTopBarModel.End.Model[i].Location[1]);
                        var a = new ItemDivision("Li " + (li), total, d, l, 0, right);
                        a.Location = new LocationBarModel(AddTopBarModel.End.Model[i].Location[0].X, AddTopBarModel.End.Model[i].Location[0].Y);
                        a.SetTypeDown(0, right);
                        a.GetAllLocation(0);
                        AddTop.Add(a);
                        li++;
                    }
                }
            }
            return AddTop;
        }
        public static ObservableCollection<ItemDivision> GetAddBottom(ObservableCollection<AddBottomBarModel> AddBottomBarModel, DivisionBar DivisionBar, List<SelectedBottomModel> SelectedBottomModels)
        {
            var AddBottom = new ObservableCollection<ItemDivision>();
            int li = 1;
            for (int i = 0; i < AddBottomBarModel.Count; i++)
            {
                if (SelectedBottomModels[i].StartBottomChecked)
                {
                    if (AddBottomBarModel[i].Start.Model.Count != 0)
                    {
                        
                        for (int j = 0; j < AddBottomBarModel[i].Start.Model.Count; j++)
                        {
                            int total = DivisionBar.NumberBeams * AddBottomBarModel[i].Start.Model[j].NumberBar;
                            double d = AddBottomBarModel[i].Start.Model[j].Bar.Diameter;
                            double length = GetLengthTotal(AddBottomBarModel[i].Start.Model[j].Location);
                            double left = GetLengthFromLocation(AddBottomBarModel[i].Start.Model[j].Location[0], AddBottomBarModel[i].Start.Model[j].Location[1]);
                            double l = GetLengthFromLocation(AddBottomBarModel[i].Start.Model[j].Location[1], AddBottomBarModel[i].Start.Model[j].Location[2]);
                            var a = new ItemDivision("Li " + (li), total, d, l, left, 0);
                            a.Location = new LocationBarModel(AddBottomBarModel[i].Start.Model[j].X0, AddBottomBarModel[i].Start.Model[j].Y0);
                            a.SetTypeUp(left, 0);
                            a.GetAllLocation(0);
                            AddBottom.Add(a);
                            li++;
                        }
                    }
                }
                if (AddBottomBarModel[i].Mid.Model.Count != 0)
                {
                    for (int j = 0; j < AddBottomBarModel[i].Mid.Model.Count; j++)
                    {
                        int total = DivisionBar.NumberBeams * AddBottomBarModel[i].Mid.Model[j].NumberBar;
                        double d = AddBottomBarModel[i].Mid.Model[j].Bar.Diameter;
                        double length = GetLengthTotal(AddBottomBarModel[i].Mid.Model[j].Location);
                        var a = new ItemDivision("Li " + (li), total, d, length, 0, 0);
                        a.Location = new LocationBarModel(AddBottomBarModel[i].Mid.Model[j].X0 - AddBottomBarModel[i].Mid.Model[j].La, AddBottomBarModel[i].Mid.Model[j].Y0);
                        a.SetTypeUp(0, 0);
                        a.GetAllLocation(0);
                        AddBottom.Add(a);
                        li++;
                    }
                }
                if (SelectedBottomModels[i].EndBottomChecked)
                {
                    if (AddBottomBarModel[i].End.Model.Count != 0)
                    {
                        for (int j = 0; j < AddBottomBarModel[i].End.Model.Count; j++)
                        {
                            int total = DivisionBar.NumberBeams * AddBottomBarModel[i].End.Model[j].NumberBar;
                            double d = AddBottomBarModel[i].End.Model[j].Bar.Diameter;
                            double length = GetLengthTotal(AddBottomBarModel[i].End.Model[j].Location);
                            double right = GetLengthFromLocation(AddBottomBarModel[i].End.Model[j].Location[AddBottomBarModel[i].End.Model[j].Location.Count - 2], AddBottomBarModel[i].End.Model[j].Location[AddBottomBarModel[i].End.Model[j].Location.Count - 1]);
                            double l = GetLengthFromLocation(AddBottomBarModel[i].End.Model[j].Location[1], AddBottomBarModel[i].End.Model[j].Location[2]);
                            var a = new ItemDivision("Li " + (li), total, d, l, 0, right);
                            a.Location = new LocationBarModel(AddBottomBarModel[i].End.Model[j].X0, AddBottomBarModel[i].End.Model[j].Y0);
                            a.SetTypeUp(0, right);
                            a.GetAllLocation(0);
                            AddBottom.Add(a);
                            li++;
                        }
                    }
                }

            }
            return AddBottom;
        }
        #endregion
        #region Special bar
        public static ObservableCollection<ItemDivision> GetSpecialBar(List<SpecialBarModel> SpecialBarModel, DivisionBar DivisionBar)
        {
            var SpecialBar = new ObservableCollection<ItemDivision>();
            for (int i = 0; i < SpecialBarModel.Count; i++)
            {
                if (SpecialBarModel[i].IsSP)
                {

                    double d = SpecialBarModel[i].BarSP.Diameter;
                    int total = SpecialBarModel[i].NumberSP * DivisionBar.NumberBeams;
                    double length = GetLengthTotal(SpecialBarModel[i].LocationSP);
                    double left = GetLengthFromLocation(SpecialBarModel[i].LocationSP[0], SpecialBarModel[i].LocationSP[1]);
                    double right = GetLengthFromLocation(SpecialBarModel[i].LocationSP[SpecialBarModel[i].LocationSP.Count - 2], SpecialBarModel[i].LocationSP[SpecialBarModel[i].LocationSP.Count - 1]);
                    double L1 = GetLengthFromLocation(SpecialBarModel[i].LocationSP[1], SpecialBarModel[i].LocationSP[2]);
                    double l = GetLengthFromLocation(SpecialBarModel[i].LocationSP[2], SpecialBarModel[i].LocationSP[3]);
                    var a = new ItemDivision("L1", total, d, l, left, right);
                    a.Location = new LocationBarModel(SpecialBarModel[i].X0, SpecialBarModel[i].Y0);
                    a.Type = DetailShopStyle.DS09;
                    a.L1 = L1;

                    for (int j = 0; j < SpecialBarModel[i].LocationSP.Count; j++)
                    {
                        a.AllLocation.Add(new LocationBarModel(SpecialBarModel[i].LocationSP[j].X, SpecialBarModel[i].LocationSP[j].Y));
                    }
                    SpecialBar.Add(a);
                }
            }
            return SpecialBar;

        }
        public static ObservableCollection<ItemDivision> GetSideBar(List<SideBarModel> sideBarModels, DivisionBar DivisionBar)
        {
            var SideBar = new ObservableCollection<ItemDivision>();
            
            for (int i = 0; i < sideBarModels.Count; i++)
            {
                int total = sideBarModels[i].NumberBar * DivisionBar.NumberBeams;
                if (sideBarModels[i].IsSide)
                {
                    double length = GetLengthTotal(sideBarModels[i].Location);
                    double x0 = sideBarModels[i].Location[0].X;
                    double y0 = sideBarModels[i].Location[0].Y;
                    var a = new ItemDivision("Li", total, sideBarModels[i].Bar.Diameter, length, 0, 0);
                    a.Location = new LocationBarModel(x0, y0);
                    a.Type = DetailShopStyle.DS00;
                    a.GetAllLocation(0);
                    SideBar.Add(a);
                }
            }
            return SideBar;

        }
        #endregion
        #region Stirrup Bar
        public static ObservableCollection<ItemDivision> GetStirrup(List<InfoModel> InfoModels, List<StirrupModel> StirrupModels, List<DistributeStirrup> DistributeStirrups, List<SpecialBarModel> SpecialBarModels, List<SpecialNodeModel> SpecialNodeModels, DivisionBar DivisionBar)
        {
            var Stirrups = new ObservableCollection<ItemDivision>();
            bool hasSpecial = (SpecialBarModels.Count != 0);
            for (int i = 0; i < StirrupModels.Count; i++)
            {
                var Stirrups0 = new ObservableCollection<ItemDivision>();
                SpecialBarModel a = null;
                if (hasSpecial)
                {
                    a = SpecialBarModels.Where(x => x.Span == InfoModels[i].NumberSpan).FirstOrDefault();
                }
                bool special = (hasSpecial && (a != null) && (a.IsSP));
                double start = 0;
                double end = 0;
                SpecialNodeModel b = null;
                if (special)
                {
                    b = SpecialNodeModels.Where(x => x.NumberSpan == a.Span).FirstOrDefault();
                    start = b.Mid - a.L3 / 2 - a.a * a.NumberST / 2 - InfoModels[i].startPosition;
                    end = b.Mid + a.L3 / 2 - InfoModels[i].startPosition + a.a * a.NumberST / 2;
                }
                if (StirrupModels[i].Type == 0)
                {
                    if (DistributeStirrups[i].Type == 0)
                    {
                        Stirrups0 = GetStirrupItem00(InfoModels[i], StirrupModels[i], DistributeStirrups[i], DivisionBar,  special, start, end);
                    }
                    else
                    {
                        Stirrups0 = GetStirrupItem01(InfoModels[i], StirrupModels[i], DistributeStirrups[i], DivisionBar,  special, start, end);
                    }

                }
                else
                {
                    if (DistributeStirrups[i].Type == 0)
                    {
                        Stirrups0 = GetStirrupItem10(InfoModels[i], StirrupModels[i], DistributeStirrups[i], DivisionBar,  special, start, end);
                    }
                    else
                    {
                        Stirrups0 = GetStirrupItem11(InfoModels[i], StirrupModels[i], DistributeStirrups[i], DivisionBar,  special, start, end);
                    }
                }
                foreach (var item in Stirrups0)
                {
                    Stirrups.Add(item);
                }
            }
            return Stirrups;
        }

        private static ObservableCollection<ItemDivision> GetStirrupItem00(InfoModel infoModel, StirrupModel stirrupModel, DistributeStirrup distributeStirrup, DivisionBar divisionBar,  bool special, double start, double end)
        {
            var Stirrups = new ObservableCollection<ItemDivision>();
            if (!special)
            {
                int total = divisionBar.NumberBeams * ((int)(infoModel.Length / distributeStirrup.S) + 1);
                double x0 = infoModel.startPosition + infoModel.Length / 2;
                double y0 = Math.Abs(infoModel.zOffset);
                var a = new ItemDivision("Li", total, stirrupModel.BarS.Diameter, 50, infoModel.b - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
                a.Location = new LocationBarModel(x0, y0);
                a.Type = DetailShopStyle.DS10;
                Stirrups.Add(a);
            }
            else
            {
                int total1 = divisionBar.NumberBeams * ((int)(start / distributeStirrup.S) + 1);
                double x01 = infoModel.startPosition + start / 2;
                double y01 = Math.Abs(infoModel.zOffset);
                var a1 = new ItemDivision("Li", total1, stirrupModel.BarS.Diameter, 50, infoModel.b - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
                a1.Location = new LocationBarModel(x01, y01);
                a1.Type = DetailShopStyle.DS10;
                Stirrups.Add(a1);
                
                int total2 = divisionBar.NumberBeams * ((int)((infoModel.Length - end) / distributeStirrup.S) + 1);
                double x02 = infoModel.startPosition + end + (infoModel.Length - end) / 2;
                double y02 = Math.Abs(infoModel.zOffset);
                var a2 = new ItemDivision("Li", total2, stirrupModel.BarS.Diameter, 50, infoModel.b - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
                a2.Location = new LocationBarModel(x02, y02);
                a2.Type = DetailShopStyle.DS10;
                Stirrups.Add(a2);
            }

            return Stirrups;
        }
        private static ObservableCollection<ItemDivision> GetStirrupItem01(InfoModel infoModel, StirrupModel stirrupModel, DistributeStirrup distributeStirrup, DivisionBar divisionBar, bool special, double start, double end)
        {
            var Stirrups = new ObservableCollection<ItemDivision>();
            int total1 = divisionBar.NumberBeams * ((int)(distributeStirrup.L1 / distributeStirrup.S1) + 1);
            double x01 = infoModel.startPosition + (0.125) * infoModel.Length;
            double y01 = Math.Abs(infoModel.zOffset);
            var a1 = new ItemDivision("Li", total1, stirrupModel.BarS.Diameter, 50, infoModel.b - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
            a1.Location = new LocationBarModel(x01, y01);
            a1.Type = DetailShopStyle.DS10;
            Stirrups.Add(a1);
            if (!special)
            {
                int total2 = divisionBar.NumberBeams * ((int)(distributeStirrup.L2 / distributeStirrup.S2) + 1);
                double x02 = infoModel.startPosition + (0.5) * infoModel.Length;
                double y02 = Math.Abs(infoModel.zOffset);
                var a2 = new ItemDivision("Li", total2 - 2, stirrupModel.BarS.Diameter, 50, infoModel.b - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
                a2.Location = new LocationBarModel(x02, y02);
                a2.Type = DetailShopStyle.DS10;
                Stirrups.Add(a2);

            }
            else
            {
                if ((end < distributeStirrup.L1 + distributeStirrup.L2) && start > distributeStirrup.L1)
                {
                    int total2 = divisionBar.NumberBeams * ((int)(Math.Abs(start - distributeStirrup.L1) / distributeStirrup.S2) + 1);
                    double x02 = infoModel.startPosition + (0.25) * infoModel.Length + Math.Abs(start - distributeStirrup.L1) / 2;
                    double y02 = Math.Abs(infoModel.zOffset);
                    var a2 = new ItemDivision("Li", total2 - 1, stirrupModel.BarS.Diameter, 50, infoModel.b - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
                    a2.Location = new LocationBarModel(x02, y02);
                    a2.Type = DetailShopStyle.DS10;
                    Stirrups.Add(a2);
                    int total4 = divisionBar.NumberBeams * ((int)(Math.Abs(distributeStirrup.L1 + distributeStirrup.L2 - end) / distributeStirrup.S2) + 1);
                    double x04 = infoModel.startPosition + end + Math.Abs(distributeStirrup.L1 + distributeStirrup.L2 - end) / 2;
                    double y04 = Math.Abs(infoModel.zOffset);
                    var a4 = new ItemDivision("Li", total4 - 1, stirrupModel.BarS.Diameter, 50, infoModel.b - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
                    a4.Location = new LocationBarModel(x04, y04);
                    a4.Type = DetailShopStyle.DS10;
                    Stirrups.Add(a4);
                }
                else
                {
                    int total2 = divisionBar.NumberBeams * ((int)(distributeStirrup.L2 / distributeStirrup.S2) + 1);
                    double x02 = infoModel.startPosition + (0.5) * infoModel.Length;
                    double y02 = Math.Abs(infoModel.zOffset);
                    var a2 = new ItemDivision("Li", total2 - 2, stirrupModel.BarS.Diameter, 50, infoModel.b - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
                    a2.Location = new LocationBarModel(x02, y02);
                    a2.Type = DetailShopStyle.DS10;
                    Stirrups.Add(a2);
                }
            }
            int total3 = total1;
            double x03 = infoModel.startPosition + (0.875) * infoModel.Length;
            double y03 = Math.Abs(infoModel.zOffset);
            var a3 = new ItemDivision("Li", total3, stirrupModel.BarS.Diameter, 50, infoModel.b - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
            a3.Location = new LocationBarModel(x03, y03);
            a3.Type = DetailShopStyle.DS10;
            Stirrups.Add(a3);
            return Stirrups;
        }
        private static ObservableCollection<ItemDivision> GetStirrupItem10(InfoModel infoModel, StirrupModel stirrupModel, DistributeStirrup distributeStirrup, DivisionBar divisionBar,  bool special, double start, double end)
        {
            var Stirrups = new ObservableCollection<ItemDivision>();
            double b0 = 4 * stirrupModel.c + stirrupModel.a + (infoModel.b - 4 * stirrupModel.c - stirrupModel.a) / 2;
            if (!special)
            {
                int total = divisionBar.NumberBeams * ((int)(infoModel.Length / distributeStirrup.S) + 1);
                double x0 = infoModel.startPosition + infoModel.Length / 2;
                double y0 = Math.Abs(infoModel.zOffset);
                double x1 = x0 - ((infoModel.b) / 2 - b0 / 2);
                double x2 = x0 + ((infoModel.b) / 2 - b0 / 2);
                double y1 = y0;
                double y2 = y1 + infoModel.h / 4;
                var a1 = new ItemDivision("Li", total, stirrupModel.BarS.Diameter, 50, b0 - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
                a1.Location = new LocationBarModel(x1, y1);
                a1.Type = DetailShopStyle.DS10;
                Stirrups.Add(a1);
                var a2 = new ItemDivision("Li", total, stirrupModel.BarS.Diameter, 50, b0 - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
                a2.Location = new LocationBarModel(x2, y2);
                a2.Type = DetailShopStyle.DS10;
                Stirrups.Add(a2);
            }
            else
            {
                int total1 = divisionBar.NumberBeams * ((int)(start / distributeStirrup.S) + 1);
                double x01 = infoModel.startPosition + start / 2;
                double y01 = Math.Abs(infoModel.zOffset);
                double x11 = x01 - ((infoModel.b) / 2 - b0 / 2);
                double x21 = x01 + ((infoModel.b) / 2 - b0 / 2);
                double y11 = y01;
                double y21 = y11 + infoModel.h / 4;
                var a11 = new ItemDivision("Li", total1, stirrupModel.BarS.Diameter, 50, b0 - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
                a11.Location = new LocationBarModel(x11, y11);
                a11.Type = DetailShopStyle.DS10;
                Stirrups.Add(a11);
                var a21 = new ItemDivision("Li", total1, stirrupModel.BarS.Diameter, 50, b0 - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
                a21.Location = new LocationBarModel(x21, y21);
                a21.Type = DetailShopStyle.DS10;
                Stirrups.Add(a21);
                int total2 = divisionBar.NumberBeams * ((int)((infoModel.Length - end) / distributeStirrup.S) + 1);
                double x02 = infoModel.startPosition + end + (infoModel.Length - end) / 2;
                double y02 = Math.Abs(infoModel.zOffset);
                double x12 = x02 - ((infoModel.b) / 2 - b0 / 2);
                double x22 = x02 + ((infoModel.b) / 2 - b0 / 2);
                double y12 = y02;
                double y22 = y12 + infoModel.h / 4;
                var a12 = new ItemDivision("Li", total2, stirrupModel.BarS.Diameter, 50, b0 - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
                a12.Location = new LocationBarModel(x12, y12);
                a12.Type = DetailShopStyle.DS10;
                Stirrups.Add(a12);
                var a22 = new ItemDivision("Li", total2, stirrupModel.BarS.Diameter, 50, b0 - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
                a22.Location = new LocationBarModel(x22, y22);
                a22.Type = DetailShopStyle.DS10;
                Stirrups.Add(a22);
            }

            return Stirrups;
        }
        private static ObservableCollection<ItemDivision> GetStirrupItem11(InfoModel infoModel, StirrupModel stirrupModel, DistributeStirrup distributeStirrup, DivisionBar divisionBar, bool special, double start, double end)
        {
            var Stirrups = new ObservableCollection<ItemDivision>();
            double b0 = 4 * stirrupModel.c + stirrupModel.a + (infoModel.b - 4 * stirrupModel.c - stirrupModel.a) / 2;
            int total1 = divisionBar.NumberBeams * ((int)(distributeStirrup.L1 / distributeStirrup.S1) + 1);
            double x01 = infoModel.startPosition + (0.125) * infoModel.Length;
            double y01 = Math.Abs(infoModel.zOffset);
            double x11 = x01 - ((infoModel.b) / 2 - b0 / 2);
            double x21 = x01 + ((infoModel.b) / 2 - b0 / 2);
            double y11 = y01;
            double y21 = y11 + infoModel.h / 4;
            var a11 = new ItemDivision("Li", total1, stirrupModel.BarS.Diameter, 50, b0 - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
            a11.Location = new LocationBarModel(x11, y11);
            a11.Type = DetailShopStyle.DS10;
            Stirrups.Add(a11);
            var a21 = new ItemDivision("Li", total1, stirrupModel.BarS.Diameter, 50, b0 - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
            a21.Location = new LocationBarModel(x21, y21);
            a21.Type = DetailShopStyle.DS10;
            Stirrups.Add(a21);
            if (!special)
            {
                int total2 = divisionBar.NumberBeams * ((int)(distributeStirrup.L2 / distributeStirrup.S2) + 1);
                double x02 = infoModel.startPosition + (0.5) * infoModel.Length;
                double y02 = Math.Abs(infoModel.zOffset);
                double x12 = x02 - ((infoModel.b) / 2 - b0 / 2);
                double x22 = x02 + ((infoModel.b) / 2 - b0 / 2);
                double y12 = y02;
                double y22 = y12 + infoModel.h / 4;
                var a12 = new ItemDivision("Li", total2 - 2, stirrupModel.BarS.Diameter, 50, b0 - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
                a12.Location = new LocationBarModel(x12, y12);
                a12.Type = DetailShopStyle.DS10;
                Stirrups.Add(a12);
                var a22 = new ItemDivision("Li", total2 - 2, stirrupModel.BarS.Diameter, 50, b0 - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
                a22.Location = new LocationBarModel(x22, y22);
                a22.Type = DetailShopStyle.DS10;
                Stirrups.Add(a22);
            }
            else
            {
                if ((end < distributeStirrup.L1 + distributeStirrup.L2) && start > distributeStirrup.L1)
                {
                    int total2 = divisionBar.NumberBeams * ((int)(Math.Abs(start - distributeStirrup.L1) / distributeStirrup.S2) + 1);
                    double x02 = infoModel.startPosition + (0.25) * infoModel.Length + Math.Abs(start - distributeStirrup.L1) / 2;
                    double y02 = Math.Abs(infoModel.zOffset);
                    double x12 = x02 - ((infoModel.b) / 2 - b0 / 2);
                    double x22 = x02 + ((infoModel.b) / 2 - b0 / 2);
                    double y12 = y02;
                    double y22 = y12 + infoModel.h / 4;
                    var a12 = new ItemDivision("Li", total2 - 1, stirrupModel.BarS.Diameter, 50, b0 - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
                    a12.Location = new LocationBarModel(x12, y12);
                    a12.Type = DetailShopStyle.DS10;
                    Stirrups.Add(a12);
                    var a22 = new ItemDivision("Li", total2 - 1, stirrupModel.BarS.Diameter, 50, b0 - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
                    a22.Location = new LocationBarModel(x22, y22);
                    a22.Type = DetailShopStyle.DS10;
                    Stirrups.Add(a22);
                    int total4 = divisionBar.NumberBeams * ((int)(Math.Abs(distributeStirrup.L1 + distributeStirrup.L2 - end) / distributeStirrup.S2) + 1);
                    double x04 = infoModel.startPosition + end + Math.Abs(distributeStirrup.L1 + distributeStirrup.L2 - end) / 2;
                    double y04 = Math.Abs(infoModel.zOffset);
                    double x14 = x04 - ((infoModel.b) / 2 - b0 / 2);
                    double x24 = x04 + ((infoModel.b) / 2 - b0 / 2);
                    double y14 = y04;
                    double y24 = y14 + infoModel.h / 4;
                    var a14 = new ItemDivision("Li", total4 - 1, stirrupModel.BarS.Diameter, 50, b0 - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
                    a14.Location = new LocationBarModel(x14, y14);
                    a14.Type = DetailShopStyle.DS10;
                    Stirrups.Add(a14);
                    var a24 = new ItemDivision("Li", total4 - 1, stirrupModel.BarS.Diameter, 50, b0 - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
                    a24.Location = new LocationBarModel(x24, y24);
                    a24.Type = DetailShopStyle.DS10;
                    Stirrups.Add(a24);
                }
                else
                {
                    int total2 = divisionBar.NumberBeams * ((int)(distributeStirrup.L2 / distributeStirrup.S2) + 1);
                    double x02 = infoModel.startPosition + (0.5) * infoModel.Length;
                    double y02 = Math.Abs(infoModel.zOffset);
                    double x12 = x02 - ((infoModel.b) / 2 - b0 / 2);
                    double x22 = x02 + ((infoModel.b) / 2 - b0 / 2);
                    double y12 = y02;
                    double y22 = y12 + infoModel.h / 4;
                    var a12 = new ItemDivision("Li", total2 - 2, stirrupModel.BarS.Diameter, 50, b0 - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
                    a12.Location = new LocationBarModel(x12, y12);
                    a12.Type = DetailShopStyle.DS10;
                    Stirrups.Add(a12);
                    var a22 = new ItemDivision("Li", total2 - 2, stirrupModel.BarS.Diameter, 50, b0 - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
                    a22.Location = new LocationBarModel(x22, y22);
                    a22.Type = DetailShopStyle.DS10;
                    Stirrups.Add(a22);
                }
            }
            int total3 = total1;
            double x03 = infoModel.startPosition + (0.875) * infoModel.Length;
            double y03 = Math.Abs(infoModel.zOffset);
            double x13 = x03 - ((infoModel.b) / 2 - b0 / 2);
            double x23 = x03 + ((infoModel.b) / 2 - b0 / 2);
            double y13 = y03;
            double y23 = y13 + infoModel.h / 4;
            var a13 = new ItemDivision("Li", total3, stirrupModel.BarS.Diameter, 50, b0 - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
            a13.Location = new LocationBarModel(x13, y13);
            a13.Type = DetailShopStyle.DS10;
            Stirrups.Add(a13);
            var a23 = new ItemDivision("Li", total3, stirrupModel.BarS.Diameter, 50, b0 - 2 * stirrupModel.c, infoModel.h - 2 * stirrupModel.c);
            a23.Location = new LocationBarModel(x23, y23);
            a23.Type = DetailShopStyle.DS10;
            Stirrups.Add(a23);
            return Stirrups;
        }


        #endregion
        #region Anti-Stirrup
        public static ObservableCollection<ItemDivision> GetAntiStirrup(List<InfoModel> InfoModels, List<StirrupModel> StirrupModels, List<DistributeStirrup> DistributeStirrups, List<SpecialBarModel> SpecialBarModels, List<SpecialNodeModel> SpecialNodeModels, DivisionBar DivisionBar,SettingModel settingModel)
        {
            var antis = new ObservableCollection<ItemDivision>();
            bool hasSpecial = (SpecialBarModels.Count != 0);
            for (int i = 0; i < StirrupModels.Count; i++)
            {
                var antis0 = new ObservableCollection<ItemDivision>();
                SpecialBarModel a = null;
                if (hasSpecial)
                {
                    a = SpecialBarModels.Where(x => x.Span == InfoModels[i].NumberSpan).FirstOrDefault();
                }
                bool special = (hasSpecial && (a != null) && (a.IsSP));
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
                    if (StirrupModels[i].Anti&&StirrupModels[i].Na!=0&&StirrupModels[i].Sa!=0)
                    {
                        antis0 = GetAntiStirrupItem0(InfoModels[i], StirrupModels[i], DistributeStirrups[i], DivisionBar, settingModel, special, start, end);
                    }
                    
                }
                else
                {
                    if (StirrupModels[i].Anti && StirrupModels[i].Na != 0 && StirrupModels[i].Sa != 0)
                    {
                        antis0 = GetAntiStirrupItem1(InfoModels[i], StirrupModels[i], DistributeStirrups[i], DivisionBar, settingModel, special, start, end);
                    }
                }
                foreach (var item in antis0)
                {
                    antis.Add(item);
                }
            }
            return antis;
        }
        private static ObservableCollection<ItemDivision> GetAntiStirrupItem0(InfoModel infoModel, StirrupModel stirrupModel, DistributeStirrup distributeStirrup, DivisionBar divisionBar,SettingModel settingModel, bool special, double start, double end)
        {
            var Antis = new ObservableCollection<ItemDivision>();
            if (!special)
            {
                double x0 = infoModel.startPosition + infoModel.Length / 2;
                double y0 = Math.Abs(infoModel.zOffset);
                int totalAnti = divisionBar.NumberBeams * ((int)(infoModel.Length / stirrupModel.Sa) + 1);
                double x0Anti = x0 - (infoModel.b -2*stirrupModel.c) / 2;
                if (stirrupModel.Na == 1)
                {
                    var anti1 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti, x0Anti, y0 + infoModel.h / 2.0);
                    Antis.Add(anti1);
                }
                else
                {
                    var anti1 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti, x0Anti, y0 + infoModel.h / 3.0);
                    Antis.Add(anti1);
                    var anti2 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti, x0Anti, y0 + 2 * infoModel.h / 3.0);
                    Antis.Add(anti2);
                }
            }
            else
            {
                double x01 = infoModel.startPosition + start / 2;
                double y01 = Math.Abs(infoModel.zOffset);
                double x02 = infoModel.startPosition + end + (infoModel.Length - end) / 2;
                double y02 = Math.Abs(infoModel.zOffset);
                int totalAnti1 = divisionBar.NumberBeams * ((int)(start / stirrupModel.Sa) + 1);
                int totalAnti2 = divisionBar.NumberBeams * ((int)(infoModel.Length - end / stirrupModel.Sa) + 1);
                double x0Anti1 = x01 - (infoModel.b - 2 * stirrupModel.c) / 2;
                double x0Anti2 = x02 - (infoModel.b - 2 * stirrupModel.c) / 2;
                if (stirrupModel.Na == 1)
                {
                    var anti1 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti1, x0Anti1, y01 + infoModel.h / 2.0);
                    Antis.Add(anti1);
                    var anti2 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti2, x0Anti2, y01 + infoModel.h / 2.0);
                    Antis.Add(anti2);
                }
                else
                {
                    var anti11 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti1, x0Anti1, y01 + infoModel.h / 3.0);
                    Antis.Add(anti11);
                    var anti21 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti1, x0Anti1, y01 + 2 * infoModel.h / 3.0);
                    Antis.Add(anti21);
                    var anti12 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti2, x0Anti2, y01 + infoModel.h / 3.0);
                    Antis.Add(anti12);
                    var anti22 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti2, x0Anti2, y01 + 2 * infoModel.h / 3.0);
                    Antis.Add(anti22);
                }
            }
            return Antis;
        }
        private static ObservableCollection<ItemDivision> GetAntiStirrupItem1(InfoModel infoModel, StirrupModel stirrupModel, DistributeStirrup distributeStirrup, DivisionBar divisionBar, SettingModel settingModel, bool special, double start, double end)
        {
            var Antis = new ObservableCollection<ItemDivision>();
            double x01 = infoModel.startPosition + (0.125) * infoModel.Length;
            double y01 = Math.Abs(infoModel.zOffset);
            int totalAnti1 = divisionBar.NumberBeams * ((int)(distributeStirrup.L1 / stirrupModel.Sa) + 1);
            double x0Anti1 = x01 - (infoModel.b - 2 * stirrupModel.c) / 2;
            if (stirrupModel.Na == 1)
            {
                var anti1 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti1, x0Anti1, y01 + infoModel.h / 2.0);
                Antis.Add(anti1);
            }
            else
            {
                var anti1 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti1, x0Anti1, y01 + infoModel.h / 3.0);
                Antis.Add(anti1);
                var anti2 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti1, x0Anti1, y01 + 2 * infoModel.h / 3.0);
                Antis.Add(anti2);
            }
            if (!special)
            {
                double x02 = infoModel.startPosition + (0.5) * infoModel.Length;
                double y02 = Math.Abs(infoModel.zOffset);
                int totalAnti2 = divisionBar.NumberBeams * ((int)(distributeStirrup.L2 / stirrupModel.Sa) + 1);
                double x0Anti2 = x02 - (infoModel.b - 2 * stirrupModel.c) / 2;
                if (stirrupModel.Na == 1)
                {
                    var anti1 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti2, x0Anti2, y02 + infoModel.h / 2.0);
                    Antis.Add(anti1);
                }
                else
                {
                    var anti1 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti2, x0Anti2, y02 + infoModel.h / 3.0);
                    Antis.Add(anti1);
                    var anti2 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti2, x0Anti2, y02 + 2 * infoModel.h / 3.0);
                    Antis.Add(anti2);
                }
            }
            else
            {
                if ((end < distributeStirrup.L1 + distributeStirrup.L2) && start > distributeStirrup.L1)
                {
                    double x02 = infoModel.startPosition + (0.25) * infoModel.Length + Math.Abs(start - distributeStirrup.L1) / 2;
                    double y02 = Math.Abs(infoModel.zOffset);
                    int totalAnti2 = divisionBar.NumberBeams * ((int)(start - distributeStirrup.L1 / stirrupModel.Sa) + 1);
                    double x0Anti2 = x02 - (infoModel.b - 2 * stirrupModel.c) / 2;
                    if (stirrupModel.Na == 1)
                    {
                        var anti1 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti2, x0Anti2, y02 + infoModel.h / 2.0);
                        Antis.Add(anti1);
                    }
                    else
                    {
                        var anti1 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti2, x0Anti2, y02 + infoModel.h / 3.0);
                        Antis.Add(anti1);
                        var anti2 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti2, x0Anti2, y02 + 2 * infoModel.h / 3.0);
                        Antis.Add(anti2);
                    }
                    double x04 = infoModel.startPosition + end + Math.Abs(distributeStirrup.L1 + distributeStirrup.L2 - end) / 2;
                    double y04 = Math.Abs(infoModel.zOffset);
                    int totalAnti4 = divisionBar.NumberBeams * ((int)(distributeStirrup.L1 + distributeStirrup.L2 - end / stirrupModel.Sa) + 1);
                    double x0Anti4 = x04 - (infoModel.b - 2 * stirrupModel.c) / 2;
                    if (stirrupModel.Na == 1)
                    {
                        var anti1 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti4, x0Anti4, y04 + infoModel.h / 2.0);
                        Antis.Add(anti1);
                    }
                    else
                    {
                        var anti1 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti4, x0Anti4, y04 + infoModel.h / 3.0);
                        Antis.Add(anti1);
                        var anti2 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti4, x0Anti4, y04 + 2 * infoModel.h / 3.0);
                        Antis.Add(anti2);
                    }
                }
                else
                {
                    double x02 = infoModel.startPosition + (0.5) * infoModel.Length;
                    double y02 = Math.Abs(infoModel.zOffset);
                    int totalAnti2 = divisionBar.NumberBeams * ((int)(distributeStirrup.L2 / stirrupModel.Sa) + 1);
                    double x0Anti2 = x02 - (infoModel.b - 2 * stirrupModel.c) / 2;
                    if (stirrupModel.Na == 1)
                    {
                        var anti1 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti2, x0Anti2, y02 + infoModel.h / 2.0);
                        Antis.Add(anti1);
                    }
                    else
                    {
                        var anti1 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti2, x0Anti2, y02 + infoModel.h / 3.0);
                        Antis.Add(anti1);
                        var anti2 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti2, x0Anti2, y02 + 2 * infoModel.h / 3.0);
                        Antis.Add(anti2);
                    }
                }
            }
            double x03 = infoModel.startPosition + (0.875) * infoModel.Length;
            double y03 = Math.Abs(infoModel.zOffset);
            int totalAnti3 = totalAnti1;
            double x0Anti3 = x03 - (infoModel.b - 2 * stirrupModel.c) / 2;
            if (stirrupModel.Na == 1)
            {
                var anti1 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti3, x0Anti3, y03 + infoModel.h / 2.0);
                Antis.Add(anti1);
            }
            else
            {
                var anti1 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti3, x0Anti3, y03 + infoModel.h / 3.0);
                Antis.Add(anti1);
                var anti2 = GetAntiItem(infoModel, stirrupModel, settingModel, totalAnti3, x0Anti3, y03 + 2 * infoModel.h / 3.0);
                Antis.Add(anti2);
            }
            return Antis;
        }
        #endregion
        #region Item

        private static ItemDivision GetMid(int i, int li, double overlap, SingleMainTopBarModel SingleMainTopBarModel, DivisionBar DivisionBar, int total, double d, double lxa, double l, double lxb)
        {
            ItemDivision a = null;
            double la1 = lxa / 2 + overlap / 2;
            double lb1 = lxb / 2 + overlap / 2;
            a = new ItemDivision("Li " + li, total, d, l, la1, lb1);
            a.Location = new LocationBarModel(SingleMainTopBarModel.Location[i + 1].X - la1, SingleMainTopBarModel.Location[i + 1].Y);
            a.Type = (SingleMainTopBarModel.Location[i + 1].Y < SingleMainTopBarModel.Location[i + 2].Y) ? DetailShopStyle.DS07 : DetailShopStyle.DS08;
            return a;
        }
        private static ItemDivision GetAntiItem(InfoModel infoModel, StirrupModel stirrupModel, SettingModel settingModel, int total, double x0, double y0)
        {
            ItemDivision a = null;
            double hook = settingModel.SelectedHook.get_Parameter(BuiltInParameter.REBAR_HOOK_ANGLE).AsDouble();
            a = new ItemDivision("Li", total, stirrupModel.BarS.Diameter, (infoModel.b - 2 * stirrupModel.c), 5* stirrupModel.BarS.Diameter, 5 * stirrupModel.BarS.Diameter);
            a.Location = new LocationBarModel(x0, y0);
            if (PointModel.AreEqual(hook, Math.PI / 2))
            {
                a.Type = DetailShopStyle.DS11;
            }
            if (PointModel.AreEqual(hook, 0.75 * Math.PI))
            {
                a.Type = DetailShopStyle.DS12;
            }
            if (PointModel.AreEqual(hook, Math.PI))
            {
                a.Type = DetailShopStyle.DS13;
            }
            return a;
        }
        #endregion


    }
}
