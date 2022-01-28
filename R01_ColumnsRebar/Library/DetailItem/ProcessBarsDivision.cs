using System;
using System.Collections.ObjectModel;
using System.Linq;
using static R01_ColumnsRebar.ErrorColumns;

namespace R01_ColumnsRebar
{
    public class ProcessBarsDivision
    {

        #region Stirrup
        public static ObservableCollection<ItemDivision> GetStirrup(SectionStyle sectionStyle, StirrupModel stirrupModel, InfoModel infoModel, DivisionBar divisionBar, double Cover)
        {
            ObservableCollection<ItemDivision> Stirrup = new ObservableCollection<ItemDivision>();
            if (sectionStyle == SectionStyle.RECTANGLE)
            {
                if (stirrupModel.TypeDis == 0)
                {

                    var a = new ItemDivision("L1", (int)(stirrupModel.L / stirrupModel.S) + 1, stirrupModel.BarS.Diameter, 5 * stirrupModel.BarS.Diameter, (infoModel.b - 2 * Cover), (infoModel.h - 2 * Cover));
                    double x = (infoModel.WestPosition + infoModel.EastPosition) / 2;
                    double y = (infoModel.SouthPosition + infoModel.NouthPosition) / 2;
                    a.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L / 2 + infoModel.h / 2);
                    a.Type = DetailShopStyle.DS10;
                    Stirrup.Add(a);
                }
                else
                {
                    double x = (infoModel.WestPosition + infoModel.EastPosition) / 2;
                    double y = (infoModel.SouthPosition + infoModel.NouthPosition) / 2;
                    var a1 = new ItemDivision("L1", (int)(stirrupModel.L1 / stirrupModel.S1) + 1, stirrupModel.BarS.Diameter, 5 * stirrupModel.BarS.Diameter, (infoModel.b - 2 * Cover), (infoModel.h - 2 * Cover));
                    a1.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L1 / 2 + infoModel.h / 2);
                    a1.Type = DetailShopStyle.DS10;
                    Stirrup.Add(a1);
                    var a2 = new ItemDivision("L1", (int)(stirrupModel.L2 / stirrupModel.S2) + 1, stirrupModel.BarS.Diameter, 5 * stirrupModel.BarS.Diameter, (infoModel.b - 2 * Cover), (infoModel.h - 2 * Cover));
                    a2.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L1 + stirrupModel.L2 / 2 + infoModel.h / 2);
                    a2.Type = DetailShopStyle.DS10;
                    Stirrup.Add(a2);
                    var a3 = new ItemDivision("L1", (int)(stirrupModel.L1 / stirrupModel.S1) + 1, stirrupModel.BarS.Diameter, 5 * stirrupModel.BarS.Diameter, (infoModel.b - 2 * Cover), (infoModel.h - 2 * Cover));
                    a3.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L1 + stirrupModel.L2 + stirrupModel.L1 / 2 + infoModel.h / 2);
                    a3.Type = DetailShopStyle.DS10;
                    Stirrup.Add(a3);
                }
            }
            else
            {
                if (stirrupModel.TypeDis == 0)
                {
                    var a = new ItemDivision("L1", (int)(stirrupModel.L / stirrupModel.S) + 1, stirrupModel.BarS.Diameter, 5 * stirrupModel.BarS.Diameter, (infoModel.D - 2 * Cover), (infoModel.D - 2 * Cover));
                    double x = (infoModel.PointXPosition);
                    double y = (infoModel.PointYPosition);
                    a.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L / 2);
                    a.Type = DetailShopStyle.DS10A;
                    Stirrup.Add(a);
                }
                else
                {
                    double x = (infoModel.PointXPosition);
                    double y = (infoModel.PointYPosition);
                    var a1 = new ItemDivision("L1", (int)(stirrupModel.L1 / stirrupModel.S1) + 1, stirrupModel.BarS.Diameter, 5 * stirrupModel.BarS.Diameter, (infoModel.D - 2 * Cover), (infoModel.D - 2 * Cover));
                    a1.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L1 / 2);
                    a1.Type = DetailShopStyle.DS10A;
                    Stirrup.Add(a1);
                    var a2 = new ItemDivision("L1", (int)(stirrupModel.L2 / stirrupModel.S2) + 1, stirrupModel.BarS.Diameter, 5 * stirrupModel.BarS.Diameter, (infoModel.D - 2 * Cover), (infoModel.D - 2 * Cover));
                    a2.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L1 + stirrupModel.L2 / 2);
                    a2.Type = DetailShopStyle.DS10A;
                    Stirrup.Add(a2);
                    var a3 = new ItemDivision("L1", (int)(stirrupModel.L1 / stirrupModel.S1) + 1, stirrupModel.BarS.Diameter, 5 * stirrupModel.BarS.Diameter, (infoModel.D - 2 * Cover), (infoModel.D - 2 * Cover));
                    a3.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L1 + stirrupModel.L2 + stirrupModel.L1 / 2);
                    a3.Type = DetailShopStyle.DS10A;
                    Stirrup.Add(a3);
                }
            }


            return Stirrup;
        }
        #endregion
        #region Add Horizontal
        public static ObservableCollection<ItemDivision> GetAddHorizontal(SectionStyle sectionStyle, StirrupModel stirrupModel, InfoModel infoModel, DivisionBar divisionBar, double Cover)
        {
            ObservableCollection<ItemDivision> AddHorizontal = new ObservableCollection<ItemDivision>();
            if (sectionStyle == SectionStyle.RECTANGLE)
            {
                if (stirrupModel.AddH)
                {
                    AddHorizontal = GetAddHorizontalRectangle(sectionStyle, stirrupModel, infoModel, divisionBar, Cover);
                }
            }
            else
            {
                if (stirrupModel.AddH)
                {
                    AddHorizontal = GetAddHorizontalCylindrical(sectionStyle, stirrupModel, infoModel, divisionBar, Cover);
                }
            }

            return AddHorizontal;
        }
        private static ObservableCollection<ItemDivision> GetAddHorizontalRectangle(SectionStyle sectionStyle, StirrupModel stirrupModel, InfoModel infoModel, DivisionBar divisionBar, double Cover)
        {
            ObservableCollection<ItemDivision> AddHorizontal = new ObservableCollection<ItemDivision>();
            if (stirrupModel.TypeH == 0)
            {
                if (stirrupModel.TypeDis == 0)
                {

                    var a = new ItemDivision("L1", divisionBar.NumberColumns * (int)(stirrupModel.L / stirrupModel.S) + 1, stirrupModel.BarH.Diameter, 5 * stirrupModel.BarH.Diameter, (stirrupModel.aH), (infoModel.h - 2 * Cover));
                    double x = (infoModel.WestPosition + infoModel.EastPosition) / 2;
                    double y = (infoModel.SouthPosition + infoModel.NouthPosition) / 2;
                    a.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L / 2 + infoModel.h / 2);
                    a.Type = DetailShopStyle.DS10;
                    AddHorizontal.Add(a);
                }
                else
                {
                    double x = (infoModel.WestPosition + infoModel.EastPosition) / 2;
                    double y = (infoModel.SouthPosition + infoModel.NouthPosition) / 2;
                    var a1 = new ItemDivision("L1", divisionBar.NumberColumns * (int)(stirrupModel.L1 / stirrupModel.S1) + 1, stirrupModel.BarH.Diameter, 5 * stirrupModel.BarH.Diameter, (stirrupModel.aH), (infoModel.h - 2 * Cover));
                    a1.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L1 / 2 + infoModel.h / 2);
                    a1.Type = DetailShopStyle.DS10;
                    AddHorizontal.Add(a1);
                    var a2 = new ItemDivision("L1", divisionBar.NumberColumns * (int)(stirrupModel.L2 / stirrupModel.S2) + 1, stirrupModel.BarH.Diameter, 5 * stirrupModel.BarH.Diameter, (stirrupModel.aH), (infoModel.h - 2 * Cover));
                    a2.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L1 + stirrupModel.L2 / 2 + infoModel.h / 2);
                    a2.Type = DetailShopStyle.DS10;
                    AddHorizontal.Add(a2);
                    var a3 = new ItemDivision("L1", divisionBar.NumberColumns * (int)(stirrupModel.L1 / stirrupModel.S1) + 1, stirrupModel.BarH.Diameter, 5 * stirrupModel.BarH.Diameter, (stirrupModel.aH), (infoModel.h - 2 * Cover));
                    a3.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L1 + stirrupModel.L2 + stirrupModel.L1 / 2 + infoModel.h / 2);
                    a3.Type = DetailShopStyle.DS10;
                    AddHorizontal.Add(a3);
                }
            }
            else
            {
                DetailShopStyle detailShopStyle = DetailShopStyle.DS11A;
                switch (stirrupModel.TypeH)
                {
                    case 1: detailShopStyle = DetailShopStyle.DS11A; break;
                    case 2: detailShopStyle = DetailShopStyle.DS12A; break;
                    case 3: detailShopStyle = DetailShopStyle.DS13A; break;
                    default: detailShopStyle = DetailShopStyle.DS11A; break;
                }
                if (stirrupModel.TypeDis == 0)
                {

                    var a = new ItemDivision("L1", divisionBar.NumberColumns * stirrupModel.nH * (int)(stirrupModel.L / stirrupModel.S) + 1, stirrupModel.BarH.Diameter, (infoModel.h - 2 * Cover), 5 * stirrupModel.BarH.Diameter, 5 * stirrupModel.BarH.Diameter);
                    double x = (infoModel.WestPosition + infoModel.EastPosition) / 2;
                    double y = (infoModel.SouthPosition + infoModel.NouthPosition) / 2;
                    a.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L / 2 + infoModel.h / 2 - Cover);
                    a.Type = detailShopStyle;
                    AddHorizontal.Add(a);
                }
                else
                {
                    double x = (infoModel.WestPosition + infoModel.EastPosition) / 2;
                    double y = (infoModel.SouthPosition + infoModel.NouthPosition) / 2;
                    var a1 = new ItemDivision("L1", divisionBar.NumberColumns * stirrupModel.nH * (int)(stirrupModel.L1 / stirrupModel.S1) + 1, stirrupModel.BarH.Diameter, (infoModel.h - 2 * Cover), 5 * stirrupModel.BarH.Diameter, 5 * stirrupModel.BarH.Diameter);
                    a1.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L1 / 2 + infoModel.h / 2 - Cover);
                    a1.Type = detailShopStyle;
                    AddHorizontal.Add(a1);
                    var a2 = new ItemDivision("L1", divisionBar.NumberColumns * stirrupModel.nH * (int)(stirrupModel.L2 / stirrupModel.S2) + 1, stirrupModel.BarH.Diameter, (infoModel.h - 2 * Cover), 5 * stirrupModel.BarH.Diameter, 5 * stirrupModel.BarH.Diameter);
                    a2.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L1 + stirrupModel.L2 / 2 + infoModel.h / 2 - Cover);
                    a2.Type = detailShopStyle;
                    AddHorizontal.Add(a2);
                    var a3 = new ItemDivision("L1", divisionBar.NumberColumns * stirrupModel.nH * (int)(stirrupModel.L1 / stirrupModel.S1) + 1, stirrupModel.BarH.Diameter, (infoModel.h - 2 * Cover), 5 * stirrupModel.BarH.Diameter, 5 * stirrupModel.BarH.Diameter);
                    a3.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L1 + stirrupModel.L2 + stirrupModel.L1 / 2 + infoModel.h / 2 - Cover);
                    a3.Type = detailShopStyle;
                    AddHorizontal.Add(a3);
                }
            }

            return AddHorizontal;
        }
        private static ObservableCollection<ItemDivision> GetAddHorizontalCylindrical(SectionStyle sectionStyle, StirrupModel stirrupModel, InfoModel infoModel, DivisionBar divisionBar, double Cover)
        {
            ObservableCollection<ItemDivision> AddHorizontal = new ObservableCollection<ItemDivision>();
            if (stirrupModel.TypeDis == 0)
            {

                var a = new ItemDivision("L1", divisionBar.NumberColumns * (int)(stirrupModel.L / stirrupModel.S) + 1, stirrupModel.BarH.Diameter, 5 * stirrupModel.BarH.Diameter, Math.Round(Math.Sqrt(2) * (infoModel.D - 2 * Cover) * 0.5, 3), Math.Round(Math.Sqrt(2) * (infoModel.D - 2 * Cover) * 0.5, 3));
                double x = infoModel.PointXPosition;
                double y = infoModel.PointYPosition;
                a.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L / 2 + Math.Round(Math.Sqrt(2) * (infoModel.D - 2 * Cover) / 2, 3));
                a.Type = DetailShopStyle.DS10;
                AddHorizontal.Add(a);
            }
            else
            {
                double x = infoModel.PointXPosition;
                double y = infoModel.PointYPosition;
                var a1 = new ItemDivision("L1", divisionBar.NumberColumns * (int)(stirrupModel.L1 / stirrupModel.S1) + 1, stirrupModel.BarS.Diameter, 5 * stirrupModel.BarS.Diameter, Math.Round(Math.Sqrt(2) * (infoModel.D - 2 * Cover) * 0.5, 3), Math.Round(Math.Sqrt(2) * (infoModel.D - 2 * Cover) * 0.5, 3));
                a1.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L1 / 2 + Math.Round(Math.Sqrt(2) * (infoModel.D - 2 * Cover) / 2, 3));
                a1.Type = DetailShopStyle.DS10;
                AddHorizontal.Add(a1);
                var a2 = new ItemDivision("L1", divisionBar.NumberColumns * (int)(stirrupModel.L2 / stirrupModel.S2) + 1, stirrupModel.BarS.Diameter, 5 * stirrupModel.BarS.Diameter, Math.Round(Math.Sqrt(2) * (infoModel.D - 2 * Cover) * 0.5, 3), Math.Round(Math.Sqrt(2) * (infoModel.D - 2 * Cover) * 0.5, 3));
                a2.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L1 + stirrupModel.L2 / 2 + Math.Round(Math.Sqrt(2) * (infoModel.D - 2 * Cover) / 2, 3));
                a2.Type = DetailShopStyle.DS10;
                AddHorizontal.Add(a2);
                var a3 = new ItemDivision("L1", divisionBar.NumberColumns * (int)(stirrupModel.L1 / stirrupModel.S1) + 1, stirrupModel.BarS.Diameter, 5 * stirrupModel.BarS.Diameter, Math.Round(Math.Sqrt(2) * (infoModel.D - 2 * Cover) * 0.5, 3), Math.Round(Math.Sqrt(2) * (infoModel.D - 2 * Cover) * 0.5, 3));
                a3.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L1 + stirrupModel.L2 + stirrupModel.L1 / 2 + Math.Round(Math.Sqrt(2) * (infoModel.D - 2 * Cover) / 2, 3));
                a3.Type = DetailShopStyle.DS10;
                AddHorizontal.Add(a3);
            }
            return AddHorizontal;
        }

        #endregion
        #region Add Vertical
        public static ObservableCollection<ItemDivision> GetAddVertical(SectionStyle sectionStyle, StirrupModel stirrupModel, InfoModel infoModel, DivisionBar divisionBar, double Cover)
        {
            ObservableCollection<ItemDivision> AddVertical = new ObservableCollection<ItemDivision>();
            if (sectionStyle == SectionStyle.RECTANGLE)
            {
                if (stirrupModel.AddV)
                {
                    AddVertical = GetAddVerticalRectangle(sectionStyle, stirrupModel, infoModel, divisionBar, Cover);
                }
            }
            else
            {
                if (stirrupModel.AddV)
                {
                    AddVertical = GetAddVerticalCylindrical(sectionStyle, stirrupModel, infoModel, divisionBar, Cover);
                }
            }
            return AddVertical;
        }
        private static ObservableCollection<ItemDivision> GetAddVerticalRectangle(SectionStyle sectionStyle, StirrupModel stirrupModel, InfoModel infoModel, DivisionBar divisionBar, double Cover)
        {
            ObservableCollection<ItemDivision> AddVertical = new ObservableCollection<ItemDivision>();
            if (stirrupModel.TypeV == 0)
            {
                if (stirrupModel.TypeDis == 0)
                {

                    var a = new ItemDivision("L1", divisionBar.NumberColumns * (int)(stirrupModel.L / stirrupModel.S) + 1, stirrupModel.BarV.Diameter, 5 * stirrupModel.BarV.Diameter, (infoModel.b - 2 * Cover), (stirrupModel.aV));
                    double x = (infoModel.WestPosition + infoModel.EastPosition) / 2;
                    double y = (infoModel.SouthPosition + infoModel.NouthPosition) / 2;
                    a.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L / 2 + stirrupModel.aV / 2);
                    a.Type = DetailShopStyle.DS10;
                    AddVertical.Add(a);
                }
                else
                {
                    double x = (infoModel.WestPosition + infoModel.EastPosition) / 2;
                    double y = (infoModel.SouthPosition + infoModel.NouthPosition) / 2;
                    var a1 = new ItemDivision("L1", divisionBar.NumberColumns * (int)(stirrupModel.L1 / stirrupModel.S1) + 1, stirrupModel.BarV.Diameter, 5 * stirrupModel.BarV.Diameter, (infoModel.b - 2 * Cover), (stirrupModel.aV));
                    a1.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L1 / 2 + stirrupModel.aV / 2);
                    a1.Type = DetailShopStyle.DS10;
                    AddVertical.Add(a1);
                    var a2 = new ItemDivision("L1", divisionBar.NumberColumns * (int)(stirrupModel.L2 / stirrupModel.S2) + 1, stirrupModel.BarV.Diameter, 5 * stirrupModel.BarV.Diameter, (infoModel.b - 2 * Cover), (stirrupModel.aV));
                    a2.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L1 + stirrupModel.L2 / 2 + stirrupModel.aV / 2);
                    a2.Type = DetailShopStyle.DS10;
                    AddVertical.Add(a2);
                    var a3 = new ItemDivision("L1", divisionBar.NumberColumns * (int)(stirrupModel.L1 / stirrupModel.S1) + 1, stirrupModel.BarV.Diameter, 5 * stirrupModel.BarV.Diameter, (infoModel.b - 2 * Cover), (stirrupModel.aV));
                    a3.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L1 + stirrupModel.L2 + stirrupModel.L1 / 2 + stirrupModel.aV / 2);
                    a3.Type = DetailShopStyle.DS10;
                    AddVertical.Add(a3);
                }
            }
            else
            {
                DetailShopStyle detailShopStyle = DetailShopStyle.DS11;
                switch (stirrupModel.TypeV)
                {
                    case 1: detailShopStyle = DetailShopStyle.DS11; break;
                    case 2: detailShopStyle = DetailShopStyle.DS12; break;
                    case 3: detailShopStyle = DetailShopStyle.DS13; break;
                    default: detailShopStyle = DetailShopStyle.DS11; break;
                }
                if (stirrupModel.TypeDis == 0)
                {

                    var a = new ItemDivision("L1", divisionBar.NumberColumns * divisionBar.NumberColumns * stirrupModel.nV * (int)(stirrupModel.L / stirrupModel.S) + 1, stirrupModel.BarV.Diameter, (infoModel.b - 2 * Cover), 5 * stirrupModel.BarV.Diameter, 5 * stirrupModel.BarH.Diameter);
                    double x = (infoModel.WestPosition + infoModel.EastPosition) / 2 - (infoModel.b - 2 * Cover) / 2;
                    double y = (infoModel.SouthPosition + infoModel.NouthPosition) / 2;
                    a.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L / 2 + stirrupModel.aV / 2);
                    a.Type = detailShopStyle;
                    AddVertical.Add(a);
                }
                else
                {
                    double x = (infoModel.WestPosition + infoModel.EastPosition) / 2 - (infoModel.b - 2 * Cover) / 2;
                    double y = (infoModel.SouthPosition + infoModel.NouthPosition) / 2;
                    var a1 = new ItemDivision("L1", divisionBar.NumberColumns * stirrupModel.nV * (int)(stirrupModel.L1 / stirrupModel.S1) + 1, stirrupModel.BarV.Diameter, (infoModel.b - 2 * Cover), 5 * stirrupModel.BarV.Diameter, 5 * stirrupModel.BarV.Diameter);
                    a1.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L1 / 2 + stirrupModel.aV / 2);
                    a1.Type = detailShopStyle;
                    AddVertical.Add(a1);
                    var a2 = new ItemDivision("L1", divisionBar.NumberColumns * stirrupModel.nV * (int)(stirrupModel.L2 / stirrupModel.S2) + 1, stirrupModel.BarV.Diameter, (infoModel.b - 2 * Cover), 5 * stirrupModel.BarV.Diameter, 5 * stirrupModel.BarV.Diameter);
                    a2.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L1 + stirrupModel.L2 / 2 + stirrupModel.aV / 2);
                    a2.Type = detailShopStyle;
                    AddVertical.Add(a2);
                    var a3 = new ItemDivision("L1", divisionBar.NumberColumns * stirrupModel.nV * (int)(stirrupModel.L1 / stirrupModel.S1) + 1, stirrupModel.BarV.Diameter, (infoModel.b - 2 * Cover), 5 * stirrupModel.BarV.Diameter, 5 * stirrupModel.BarV.Diameter);
                    a3.Location = new LocationBarModel(x, y, infoModel.BottomPosition + stirrupModel.L1 + stirrupModel.L2 + stirrupModel.L1 / 2 + stirrupModel.aV / 2);
                    a3.Type = detailShopStyle;
                    AddVertical.Add(a3);
                }
            }


            return AddVertical;
        }
        private static ObservableCollection<ItemDivision> GetAddVerticalCylindrical(SectionStyle sectionStyle, StirrupModel stirrupModel, InfoModel infoModel, DivisionBar divisionBar, double Cover)
        {
            ObservableCollection<ItemDivision> AddVertical = new ObservableCollection<ItemDivision>();
            DetailShopStyle detailShopStyleH = DetailShopStyle.DS11A;
            DetailShopStyle detailShopStyleV = DetailShopStyle.DS11;
            switch (stirrupModel.TypeV)
            {
                case 0: detailShopStyleH = DetailShopStyle.DS11A; detailShopStyleV = DetailShopStyle.DS11; break;
                case 1: detailShopStyleH = DetailShopStyle.DS12A; detailShopStyleV = DetailShopStyle.DS12; break;
                case 2: detailShopStyleH = DetailShopStyle.DS13A; detailShopStyleV = DetailShopStyle.DS13; break;
                default: detailShopStyleH = DetailShopStyle.DS11A; detailShopStyleV = DetailShopStyle.DS11; break;
            }
            double l = infoModel.D - 2 * Cover;
            double xH = infoModel.PointXPosition;
            double yH = infoModel.PointYPosition;
            double xV = infoModel.PointXPosition-0.5*l;
            double yV = infoModel.PointYPosition;
            
            if (stirrupModel.TypeDis == 0)
            {

                var aH = new ItemDivision("L1", divisionBar.NumberColumns * (int)(stirrupModel.L / stirrupModel.S) + 1, stirrupModel.BarV.Diameter, l, 5 * stirrupModel.BarV.Diameter, 5 * stirrupModel.BarV.Diameter);
                aH.Location = new LocationBarModel(xH, yH, infoModel.BottomPosition + stirrupModel.L / 2 + l / 2);
                aH.Type = detailShopStyleH;
                AddVertical.Add(aH);
                var aV = new ItemDivision("L1", divisionBar.NumberColumns * (int)(stirrupModel.L / stirrupModel.S) + 1, stirrupModel.BarV.Diameter, l, 5 * stirrupModel.BarV.Diameter, 5 * stirrupModel.BarV.Diameter);
                aV.Location = new LocationBarModel(xV, yV, infoModel.BottomPosition + stirrupModel.L / 2);
                aV.Type = detailShopStyleV;
                AddVertical.Add(aV);
            }
            else
            {

                var a1H = new ItemDivision("L1", divisionBar.NumberColumns * stirrupModel.nV * (int)(stirrupModel.L1 / stirrupModel.S1) + 1, stirrupModel.BarV.Diameter, l, 5 * stirrupModel.BarV.Diameter, 5 * stirrupModel.BarV.Diameter);
                a1H.Location = new LocationBarModel(xH, yH, infoModel.BottomPosition + stirrupModel.L1 / 2 + l / 2);
                a1H.Type = detailShopStyleH;
                AddVertical.Add(a1H);
                var a2H = new ItemDivision("L1", divisionBar.NumberColumns * stirrupModel.nV * (int)(stirrupModel.L2 / stirrupModel.S2) + 1, stirrupModel.BarV.Diameter, l, 5 * stirrupModel.BarV.Diameter, 5 * stirrupModel.BarV.Diameter);
                a2H.Location = new LocationBarModel(xH, yH, infoModel.BottomPosition + stirrupModel.L1 + stirrupModel.L2 / 2 + l / 2);
                a2H.Type = detailShopStyleH;
                AddVertical.Add(a2H);
                var a3H = new ItemDivision("L1", divisionBar.NumberColumns * stirrupModel.nV * (int)(stirrupModel.L1 / stirrupModel.S1) + 1, stirrupModel.BarV.Diameter, l, 5 * stirrupModel.BarV.Diameter, 5 * stirrupModel.BarV.Diameter);
                a3H.Location = new LocationBarModel(xH, yH, infoModel.BottomPosition + stirrupModel.L1 + stirrupModel.L2 + stirrupModel.L1 / 2 + l / 2);
                a3H.Type = detailShopStyleH;
                AddVertical.Add(a3H);
                var a1V = new ItemDivision("L1", divisionBar.NumberColumns * stirrupModel.nV * (int)(stirrupModel.L1 / stirrupModel.S1) + 1, stirrupModel.BarV.Diameter, l, 5 * stirrupModel.BarV.Diameter, 5 * stirrupModel.BarV.Diameter);
                a1V.Location = new LocationBarModel(xV, yV, infoModel.BottomPosition + stirrupModel.L1 / 2);
                a1V.Type = detailShopStyleV;
                AddVertical.Add(a1V);
                var a2V = new ItemDivision("L1", divisionBar.NumberColumns * stirrupModel.nV * (int)(stirrupModel.L2 / stirrupModel.S2) + 1, stirrupModel.BarV.Diameter, l, 5 * stirrupModel.BarV.Diameter, 5 * stirrupModel.BarV.Diameter);
                a2V.Location = new LocationBarModel(xV, yV, infoModel.BottomPosition + stirrupModel.L1 + stirrupModel.L2 / 2);
                a2V.Type = detailShopStyleV;
                AddVertical.Add(a2V);
                var a3V = new ItemDivision("L1", divisionBar.NumberColumns * stirrupModel.nV * (int)(stirrupModel.L1 / stirrupModel.S1) + 1, stirrupModel.BarV.Diameter, l, 5 * stirrupModel.BarV.Diameter, 5 * stirrupModel.BarV.Diameter);
                a3V.Location = new LocationBarModel(xV, yV, infoModel.BottomPosition + stirrupModel.L1 + stirrupModel.L2 + stirrupModel.L1 / 2);
                a3V.Type = detailShopStyleV;
                AddVertical.Add(a3V);
            }
            return AddVertical;
        }
        #endregion
        #region Main
        public static ObservableCollection<ItemDivision> GetMain(SectionStyle sectionStyle, BarMainModel barMainModel, InfoModel infoModel, DivisionBar divisionBar, double Cover)
        {
            ObservableCollection<ItemDivision> main = new ObservableCollection<ItemDivision>();
            ObservableCollection<BarModel> barModels = new ObservableCollection<BarModel>();
            for (int i = 0; i < barMainModel.BarModels.Count; i++)
            {
                barModels.Add(barMainModel.BarModels[i]);
            }
            BarModel b1 = barModels[0];
            while (barModels.Count != 0)
            {
                ObservableCollection<BarModel> barModel1 = new ObservableCollection<BarModel>(barModels.Where(x=>(ConditionSameBarModel(b1,x))).ToList());
                //for (int i = 0; i < barModels.Count; i++)
                //{
                //    if (ConditionSameBarModel(b1, barModels[i]))
                //    {
                //        barModel1.Add(barModels[i]);
                //    }
                //}
                //ObservableCollection<BarModel> barModel1 = new ObservableCollection<BarModel>(barModels.Where(x=> ConditionSameBarModel(b1, x)).ToList());
                
                if (barModel1.Count != 0)
                {
                    main.Add(GetItemDivision(barModel1.Count, barModel1[0], divisionBar, Cover));
                    for (int i = 0; i < barModel1.Count; i++)
                    {
                        barModels.Remove(barModel1[i]);
                    }
                }
                if (barModels.Count != 0)
                {
                    b1 = barModels[0];
                }
                else
                {
                    break;
                }

            }
            if (barMainModel.AddBarModels.Count!=0)
            {
                ObservableCollection<BarModel> addbarModels = new ObservableCollection<BarModel>();
                for (int i = 0; i < barMainModel.AddBarModels.Count; i++)
                {
                    addbarModels.Add(barMainModel.AddBarModels[i]);
                }
                BarModel ba = addbarModels[0];
                while (addbarModels.Count != 0)
                {
                    ObservableCollection<BarModel> addbarModel1 = new ObservableCollection<BarModel>(addbarModels.Where(x=>ConditionSameAddBarModel(ba,x)).ToList());
                    //for (int i = 0; i < addbarModels.Count; i++)
                    //{
                    //    if (ConditionSameAddBarModel(ba, addbarModels[i]))
                    //    {
                    //        addbarModel1.Add(addbarModels[i]);
                    //    }
                    //}
                    //ObservableCollection<BarModel> barModel1 = new ObservableCollection<BarModel>(barModels.Where(x=> ConditionSameBarModel(b1, x)).ToList());

                    if (addbarModel1.Count != 0)
                    {
                        main.Add(GetAddItemDivision(addbarModel1.Count, addbarModel1[0], divisionBar, Cover));
                        for (int i = 0; i < addbarModel1.Count; i++)
                        {
                           
                            addbarModels.Remove(addbarModel1[i]);
                        }
                    }
                    if (addbarModels.Count != 0)
                    {
                        ba = addbarModels[0];
                    }
                    else
                    {
                        break;
                    }

                }
            }
            
            return main;
        }
        private static ItemDivision GetItemDivision(int count, BarModel barModel, DivisionBar divisionBar, double Cover)
        {
            string name = "L1";
            int number = divisionBar.NumberColumns * count;
            double diameter = barModel.Bar.Diameter;
            double l = 0, l1 = 0, la = 0, lb = 0;
            DetailShopStyle detailShopStyle = DetailShopStyle.DS00;
            double sx = 0;
            double sy = 0;
            double x0 = 0, y0 = 0, z0 = barModel.Location[0].Z;
            if (barModel.ConditionMultiCurve())
            {
                detailShopStyle = DetailShopStyle.DS00;
                l = GetLenght(barModel);
                la = 0; lb = 0;

            }
            else
            {
                if (barModel.IsBottomDowels)
                {
                    if (barModel.BottomDowels == 0)
                    {
                        if (barModel.IsTopDowels)
                        {
                            if (barModel.TopDowels == 0)
                            {
                                if ((!PointModel.AreEqual(barModel.Location[barModel.Location.Count - 3].X, barModel.Location[barModel.Location.Count - 2].X))||(!PointModel.AreEqual(barModel.Location[barModel.Location.Count - 3].Y, barModel.Location[barModel.Location.Count - 2].Y)))
                                {
                                    detailShopStyle = DetailShopStyle.DS07;
                                    double x1 = Math.Abs(barModel.Location[barModel.Location.Count - 3].X - barModel.Location[barModel.Location.Count - 2].X);
                                    double y1 = Math.Abs(barModel.Location[barModel.Location.Count - 3].Y - barModel.Location[barModel.Location.Count - 2].Y);
                                    sx = Math.Abs(barModel.Location[barModel.Location.Count - 3].Z - barModel.Location[barModel.Location.Count - 2].Z);
                                    sy = Math.Sqrt(x1 * x1 + y1 * y1);
                                    l = Math.Sqrt(sx * sx + sy * sy);
                                    la = GetLenghtItem(barModel.Location[barModel.Location.Count - 4], barModel.Location[barModel.Location.Count - 3]);
                                    lb = GetLenghtItem(barModel.Location[barModel.Location.Count - 1], barModel.Location[barModel.Location.Count - 2]);
                                }
                                else
                                {
                                    detailShopStyle = DetailShopStyle.DS00;
                                    l = GetLenght(barModel);
                                    la = 0; lb = 0;
                                }
                            }
                            else
                            {
                                if (barModel.LaTopDowels == 0)
                                {
                                    detailShopStyle = DetailShopStyle.DS00;
                                    l = GetLenght(barModel);
                                    la = 0; lb = 0;
                                }
                                else
                                {
                                    detailShopStyle = (barModel.LaTopDowels > 0) ? DetailShopStyle.DS05 : DetailShopStyle.DS02;
                                    la = 0; lb = Math.Abs(barModel.LaTopDowels);
                                    l = GetLenght(barModel)-lb;
                                    
                                }
                            }
                        }
                        else
                        {
                            detailShopStyle = DetailShopStyle.DS00;
                            l = GetLenght(barModel);
                            la = 0; lb = 0;
                        }
                    }
                    else
                    {
                        if (barModel.LaBottomDowels == 0)
                        {
                            if (barModel.IsTopDowels)
                            {
                                if (barModel.TopDowels == 0)
                                {
                                    if ((!PointModel.AreEqual(barModel.Location[barModel.Location.Count - 3].X, barModel.Location[barModel.Location.Count - 2].X)) || (!PointModel.AreEqual(barModel.Location[barModel.Location.Count - 3].Y, barModel.Location[barModel.Location.Count - 2].Y)))
                                    {
                                        detailShopStyle = DetailShopStyle.DS07;
                                        double x1 = Math.Abs(barModel.Location[barModel.Location.Count - 3].X - barModel.Location[barModel.Location.Count - 2].X);
                                        double y1 = Math.Abs(barModel.Location[barModel.Location.Count - 3].Y - barModel.Location[barModel.Location.Count - 2].Y);
                                        sx = Math.Abs(barModel.Location[barModel.Location.Count - 3].Z - barModel.Location[barModel.Location.Count - 2].Z);
                                        sy = Math.Sqrt(x1 * x1 + y1 * y1);
                                        l = Math.Sqrt(sx * sx + sy * sy);
                                        la = GetLenghtItem(barModel.Location[barModel.Location.Count - 4], barModel.Location[barModel.Location.Count - 3]);
                                        lb = GetLenghtItem(barModel.Location[barModel.Location.Count - 1], barModel.Location[barModel.Location.Count - 2]);
                                    }
                                    else
                                    {
                                        detailShopStyle = DetailShopStyle.DS00;
                                        l = GetLenght(barModel);
                                        la = 0; lb = 0;
                                    }

                                }
                                else
                                {
                                    if (barModel.LaTopDowels == 0)
                                    {
                                        detailShopStyle = DetailShopStyle.DS00;
                                        l = GetLenght(barModel);
                                        la = 0; lb = 0;
                                    }
                                    else
                                    {
                                        detailShopStyle = (barModel.LaTopDowels > 0) ? DetailShopStyle.DS05 : DetailShopStyle.DS02;
                                        la = 0; lb = Math.Abs(barModel.LaTopDowels);
                                        l = GetLenght(barModel)-lb;
                                        
                                    }
                                }
                            }
                            else
                            {
                                detailShopStyle = DetailShopStyle.DS00;
                                l = GetLenght(barModel);
                                la = 0; lb = 0;
                            }
                        }
                        else
                        {
                            if (barModel.IsTopDowels)
                            {
                                if (barModel.TopDowels == 0)
                                {
                                    if ((!PointModel.AreEqual(barModel.Location[barModel.Location.Count - 3].X, barModel.Location[barModel.Location.Count - 2].X)) || (!PointModel.AreEqual(barModel.Location[barModel.Location.Count - 3].Y, barModel.Location[barModel.Location.Count - 2].Y)))
                                    {
                                        detailShopStyle = (barModel.LaBottomDowels > 0) ? DetailShopStyle.DS07A : DetailShopStyle.DS07B;
                                        double x1 = Math.Abs(barModel.Location[barModel.Location.Count - 3].X - barModel.Location[barModel.Location.Count - 2].X);
                                        double y1 = Math.Abs(barModel.Location[barModel.Location.Count - 3].Y - barModel.Location[barModel.Location.Count - 2].Y);
                                        sx = Math.Abs(barModel.Location[barModel.Location.Count - 3].Z - barModel.Location[barModel.Location.Count - 2].Z);
                                        sy = Math.Sqrt(x1 * x1 + y1 * y1);
                                        l = Math.Sqrt(sx * sx + sy * sy);
                                        la = GetLenghtItem(barModel.Location[barModel.Location.Count - 4], barModel.Location[barModel.Location.Count - 3]);
                                        lb = GetLenghtItem(barModel.Location[barModel.Location.Count - 1], barModel.Location[barModel.Location.Count - 2]);
                                        l1 = Math.Abs(barModel.LaBottomDowels);
                                    }
                                    else
                                    {
                                        detailShopStyle = (barModel.LaBottomDowels > 0) ? DetailShopStyle.DS04 : DetailShopStyle.DS01;
                                        la = Math.Abs(barModel.LaBottomDowels); lb = 0;
                                        l = GetLenght(barModel) - la;
                                    }
                                    
                                }
                                else
                                {
                                    if (barModel.LaTopDowels == 0)
                                    {
                                        detailShopStyle = (barModel.LaBottomDowels > 0) ? DetailShopStyle.DS01 : DetailShopStyle.DS04;
                                        la = Math.Abs(barModel.LaBottomDowels); lb = 0;
                                        l = GetLenght(barModel)-la;
                                        
                                    }
                                    else
                                    {
                                        detailShopStyle = (barModel.LaTopDowels > 0) ? ((barModel.LaBottomDowels > 0) ? DetailShopStyle.DS06 : DetailShopStyle.DS06A) : ((barModel.LaBottomDowels > 0) ? DetailShopStyle.DS03A : DetailShopStyle.DS03);
                                        la = Math.Abs(barModel.LaBottomDowels); lb = Math.Abs(barModel.LaTopDowels);
                                        l = GetLenght(barModel)-la-lb;
                                        
                                    }
                                }
                            }
                            else
                            {
                                detailShopStyle = (barModel.LaBottomDowels > 0) ? DetailShopStyle.DS04 : DetailShopStyle.DS01;
                                la = Math.Abs(barModel.LaBottomDowels); lb = 0;
                                l = GetLenght(barModel)-la;
                                
                            }
                        }

                    }
                }
                else
                {
                    if (barModel.IsTopDowels)
                    {
                        if (barModel.TopDowels == 0)
                        {
                            if ((!PointModel.AreEqual(barModel.Location[barModel.Location.Count - 3].X, barModel.Location[barModel.Location.Count - 2].X)) || (!PointModel.AreEqual(barModel.Location[barModel.Location.Count - 3].Y, barModel.Location[barModel.Location.Count - 2].Y)))
                            {
                                detailShopStyle = DetailShopStyle.DS07;
                                double x1 = Math.Abs(barModel.Location[barModel.Location.Count - 3].X - barModel.Location[barModel.Location.Count - 2].X);
                                double y1 = Math.Abs(barModel.Location[barModel.Location.Count - 3].Y - barModel.Location[barModel.Location.Count - 2].Y);
                                sx = Math.Abs(barModel.Location[barModel.Location.Count - 3].Z - barModel.Location[barModel.Location.Count - 2].Z);
                                sy = Math.Sqrt(x1 * x1 + y1 * y1);
                                l = Math.Sqrt(sx * sx + sy * sy);
                                la = GetLenghtItem(barModel.Location[barModel.Location.Count - 4], barModel.Location[barModel.Location.Count - 3]);
                                lb = GetLenghtItem(barModel.Location[barModel.Location.Count - 1], barModel.Location[barModel.Location.Count - 2]);
                            }
                            else
                            {
                                detailShopStyle = DetailShopStyle.DS00;
                                l = GetLenght(barModel);
                                la = 0; lb = 0;
                            }
                        }
                        else
                        {
                            if (barModel.LaTopDowels == 0)
                            {
                                detailShopStyle = DetailShopStyle.DS00;
                                l = GetLenght(barModel);
                                la = 0; lb = 0;
                            }
                            else
                            {
                                detailShopStyle = (barModel.LaTopDowels > 0) ? DetailShopStyle.DS05 : DetailShopStyle.DS02;
                                la = 0; lb = Math.Abs(barModel.LaTopDowels);
                                l = GetLenght(barModel)-lb;
                                
                            }
                        }
                    }
                    else
                    {
                        detailShopStyle = DetailShopStyle.DS00;
                        l = GetLenght(barModel);
                        la = 0; lb = 0;
                    }
                }
            }

            var a = new ItemDivision("L1", divisionBar.NumberColumns * count, barModel.Bar.Diameter, l, la, lb);
            a.Location = new LocationBarModel(x0, y0, z0);
            a.Type = detailShopStyle;
            a.SlopeX = sx; a.SlopeY = sy; a.L1 = l1;
            a.GetAllLocation();
            return a;
        }
        private static ItemDivision GetAddItemDivision(int count, BarModel barModel, DivisionBar divisionBar, double Cover)
        {
            string name = "L1";
            int number = divisionBar.NumberColumns * count;
            double diameter = barModel.Bar.Diameter;
            double l = GetLenghtItem(barModel.Location[1],barModel.Location[2]), l1 = 0, la = GetLenghtItem(barModel.Location[0],barModel.Location[1]), lb = 0;
            DetailShopStyle detailShopStyle = DetailShopStyle.DS04;
            double sx = 0;
            double sy = 0;
            double x0 = 0, y0 = 0, z0 = barModel.Location[0].Z;
            
            var a = new ItemDivision("L1", number, barModel.Bar.Diameter, l, la, lb);
            a.Location = new LocationBarModel(x0, y0, z0);
            a.Type = detailShopStyle;
            a.SlopeX = sx; a.SlopeY = sy; a.L1 = l1;
            a.GetAllLocation();
            return a;
        }
        #endregion
        private static bool ConditionSameBarModel(BarModel b1, BarModel b2)
        {
            if (!b1.Bar.Type.Equals(b2.Bar.Type)) return false;
            if (!(PointModel.AreEqual(b1.Bar.Diameter, b2.Bar.Diameter))) return false;
            if ((b1.IsTopDowels != b2.IsTopDowels))
            {
                return false;
            }
            else
            {
                if (b1.TopDowels != b2.TopDowels)
                {
                    return false;
                }
                else
                {
                    if (b1.TopDowels != 0)
                    {
                        if (!PointModel.AreEqual(b1.LaTopDowels, b2.LaTopDowels)) return false;
                    }
                }
            }
            if (b1.IsBottomDowels != b2.IsBottomDowels)
            {
                return false;
            }
            else
            {
                if (b1.BottomDowels != b2.BottomDowels)
                {
                    return false;
                }
                else
                {
                    if (b1.BottomDowels == 0)
                    {
                        if (!PointModel.AreEqual(b1.LcBottomDowels, b2.LcBottomDowels)) return false;
                    }
                    else
                    {
                        if ((!PointModel.AreEqual(b1.LaBottomDowels, b2.LaBottomDowels)) || (!PointModel.AreEqual(b1.LbBottomDowels, b2.LbBottomDowels))) return false;
                    }
                }
            }
            if (b1.Location.Count != b2.Location.Count)
            {
                return false;
            }
            else
            {
                if (!PointModel.AreEqual(GetLenght(b1), GetLenght(b2))) return false;
            }

            return true;

        }
        private static bool ConditionSameAddBarModel(BarModel b1, BarModel b2)
        {
            
            if (b1.Location.Count != b2.Location.Count)
            {
                return false;
            }
            else
            {
                if (!PointModel.AreEqual(GetLenght(b1), GetLenght(b2))) return false;
            }

            return true;

        }
        private static double GetLenght(BarModel bar)
        {
            double a = 0;
            for (int i = 1; i < bar.Location.Count; i++)
            {
                a += Math.Sqrt((bar.Location[i - 1].X - bar.Location[i].X) * (bar.Location[i - 1].X - bar.Location[i].X) + (bar.Location[i - 1].Y - bar.Location[i].Y) * (bar.Location[i - 1].Y - bar.Location[i].Y) + (bar.Location[i - 1].Z - bar.Location[i].Z) * (bar.Location[i - 1].Z - bar.Location[i].Z));
            }
            return a;
        }
        private static double GetLenghtItem(LocationBarModel l1, LocationBarModel l2)
        {
            return Math.Sqrt((l1.X - l2.X) * (l1.X - l2.X) + (l1.Y - l2.Y) * (l1.Y - l2.Y) + (l1.Z - l2.Z) * (l1.Z - l2.Z));
        }




    }
}
