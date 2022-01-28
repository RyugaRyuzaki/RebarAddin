using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using static R01_ColumnsRebar.ErrorColumns;

namespace R01_ColumnsRebar
{
    public class DrawDetailShop
    {
        #region BarsDivision
        public static void DrawInfoColumnsBarsDivision(Canvas canvas, ColumnsModel columnsModel)
        {
            canvas.Children.Clear();       
            double maxWidth = (columnsModel.SectionStyle == SectionStyle.RECTANGLE) ? columnsModel.DrawModel.GetBmax(columnsModel.InfoModels) : columnsModel.DrawModel.GetDmax(columnsModel.InfoModels);
            double p30 = columnsModel.InfoModels[0].BottomPosition;
            for (int i = 0; i < columnsModel.InfoModels.Count; i++)
            {   
                DrawMainCanvas.DrawColumnItemLevel(canvas, columnsModel.DrawModel, columnsModel.InfoModels[i]);
                DrawImage.DimVertical(canvas, columnsModel.DrawModel.Left - 40, columnsModel.DrawModel.Top - columnsModel.InfoModels[i].TopPosition / columnsModel.DrawModel.Scale, columnsModel.DrawModel.Scale, (columnsModel.InfoModels[i].TopPosition - columnsModel.InfoModels[i].BottomPosition), 11, 40, 5);
                DrawStirrupItem(canvas, columnsModel.SectionStyle, columnsModel.BarsDivisionModels[i], columnsModel.DrawModel, columnsModel.SettingModel, columnsModel.Cover);
                DrawAddHorizontalItem(canvas, columnsModel.SectionStyle, columnsModel.BarsDivisionModels[i], columnsModel.DrawModel, columnsModel.SettingModel, columnsModel.Cover);
                DrawAddVerticalItem(canvas, columnsModel.SectionStyle, columnsModel.BarsDivisionModels[i], columnsModel.DrawModel, columnsModel.SettingModel, columnsModel.Cover);
                DrawItemBar(canvas, columnsModel.BarsDivisionModels[i], columnsModel.DrawModel, columnsModel.SettingModel, columnsModel.Cover, maxWidth);
            }
            DrawMainCanvas.DrawFoundationBottom(canvas, columnsModel.DrawModel.Left, columnsModel.DrawModel.Top, columnsModel.DrawModel.Scale, p30, columnsModel.DrawModel.StrokeBound, 50, columnsModel.DrawModel.ColorBound);  
        }

        #endregion
        #region stirrup
        private static void DrawStirrupItem(Canvas canvas, SectionStyle sectionStyle, BarsDivisionModel barsDivisionModel, DrawModel drawModel, SettingModel settingModel, double Cover)
        {
            if (barsDivisionModel.Stirrup.Count != 0)
            {
                for (int i = 0; i < barsDivisionModel.Stirrup.Count; i++)
                {
                    if (sectionStyle == SectionStyle.RECTANGLE)
                    {
                        DrawItemStirrupDivisionRectangle(canvas, barsDivisionModel.Stirrup[i], drawModel, settingModel, Cover, 0, 0);
                    }
                    else
                    {
                        DrawItemStirrupDivisionCylindrical(canvas, barsDivisionModel.Stirrup[i], drawModel, settingModel, Cover);
                    }
                }
            }
        }
        private static void DrawItemStirrupDivisionRectangle(Canvas canvas, ItemDivision itemDivision, DrawModel drawModel, SettingModel settingModel, double c, double x0, double z0)
        {
            double b = itemDivision.La + 2 * c;
            double h = itemDivision.Lb + 2 * c;
            double left = drawModel.Left + itemDivision.Location.X / drawModel.Scale - (b / 2) / drawModel.Scale + x0 / drawModel.Scale;
            double top = drawModel.Top - itemDivision.Location.Z / drawModel.Scale + z0 / drawModel.Scale;
            DrawImage.DrawStirrup(canvas, left, top, drawModel.Scale, b, h, c, 6, 12, Brushes.Red);
            double x1a = left;
            double x2a = left + (itemDivision.La) / drawModel.Scale;
            double y1a = top;
            double y2a = top;
            DrawTextItemDivision(canvas, drawModel, settingModel, itemDivision.La, 0, x1a, x2a, y1a, y2a, true);
            double x1b = left;
            double x2b = left;
            double y1b = top + (h) / drawModel.Scale;
            double y2b = top;
            DrawTextItemDivision(canvas, drawModel, settingModel, itemDivision.Lb, 90, x1b, x2b, y1b, y2b, true);
        }
        private static void DrawItemStirrupDivisionCylindrical(Canvas canvas, ItemDivision itemDivision, DrawModel drawModel, SettingModel settingModel, double c)
        {
            double b = itemDivision.La + 2 * c;
            double left = drawModel.Left + itemDivision.Location.X / drawModel.Scale;
            double top = drawModel.Top - itemDivision.Location.Z / drawModel.Scale;
            DrawImage.DrawCylindricalStirrup(canvas, left, top, drawModel.Scale, c, b, itemDivision.Diameter, Brushes.Red);
            double x1a = left;
            double x2a = left;
            double y1a = top;
            double y2a = top;
            DrawTextItemDivision(canvas, drawModel, settingModel, itemDivision.La, 0, x1a, x2a, y1a, y2a, false);
        }
        #endregion
        #region  AddHorizontal
        private static void DrawAddHorizontalItem(Canvas canvas, SectionStyle sectionStyle, BarsDivisionModel barsDivisionModel, DrawModel drawModel, SettingModel settingModel, double Cover)
        {
            if (barsDivisionModel.AddH.Count != 0)
            {
                for (int i = 0; i < barsDivisionModel.AddH.Count; i++)
                {
                    if (sectionStyle == SectionStyle.RECTANGLE)
                    {
                        if (barsDivisionModel.AddH[i].Type == DetailShopStyle.DS10)
                        {
                            DrawItemStirrupDivisionRectangle(canvas, barsDivisionModel.AddH[i], drawModel, settingModel, Cover, barsDivisionModel.AddH[i].La / 4, barsDivisionModel.AddH[i].Lb / 4);
                        }
                        else
                        {
                            DrawItemAddHorizontalDivisionRectangle(canvas, barsDivisionModel.AddH[i], drawModel, settingModel, Cover, barsDivisionModel.AddH[i].L / 4, barsDivisionModel.AddH[i].L / 4);
                        }

                    }
                    else
                    {
                        DrawItemStirrupDivisionRectangle(canvas, barsDivisionModel.AddH[i], drawModel, settingModel, Cover, 0, 0);
                    }
                }
            }
        }
        private static void DrawItemAddHorizontalDivisionRectangle(Canvas canvas, ItemDivision itemDivision, DrawModel drawModel, SettingModel settingModel, double c, double x0, double z0)
        {

            double hook = 0;
            if (itemDivision.Type == DetailShopStyle.DS11A) hook = Math.PI / 2;
            if (itemDivision.Type == DetailShopStyle.DS12A) hook = Math.PI * 0.75;
            if (itemDivision.Type == DetailShopStyle.DS13A) hook = Math.PI;
            double h = itemDivision.L + 2 * c;
            double left = drawModel.Left + itemDivision.Location.X / drawModel.Scale + x0 / drawModel.Scale;
            double top = drawModel.Top - itemDivision.Location.Z / drawModel.Scale + z0 / drawModel.Scale;
            DrawImage.DrawHookVertical(canvas, left, top, drawModel.Scale, h, c, itemDivision.Diameter, 3 * itemDivision.Diameter, hook, Brushes.Red);
            double x1b = left;
            double x2b = left;
            double y1b = top + (h) / drawModel.Scale;
            double y2b = top;
            DrawTextItemDivision(canvas, drawModel, settingModel, itemDivision.L, 90, x1b, x2b, y1b, y2b, true);

        }

        #endregion
        #region  AddVertical
        private static void DrawAddVerticalItem(Canvas canvas, SectionStyle sectionStyle, BarsDivisionModel barsDivisionModel, DrawModel drawModel, SettingModel settingModel, double Cover)
        {
            if (barsDivisionModel.AddV.Count != 0)
            {
                for (int i = 0; i < barsDivisionModel.AddV.Count; i++)
                {
                    if (sectionStyle == SectionStyle.RECTANGLE)
                    {
                        if (barsDivisionModel.AddV[i].Type == DetailShopStyle.DS10)
                        {
                            DrawItemStirrupDivisionRectangle(canvas, barsDivisionModel.AddV[i], drawModel, settingModel, Cover, barsDivisionModel.AddV[i].La / 4, barsDivisionModel.AddV[i].Lb / 4);
                        }
                        else
                        {
                            DrawItemAddVerticalDivisionRectangle(canvas, barsDivisionModel.AddV[i], drawModel, settingModel, Cover, barsDivisionModel.AddV[i].L / 4, barsDivisionModel.AddV[i].L / 4);
                        }

                    }
                    else
                    {
                        DrawItemAddVerticalDivisionCylindrical(canvas, barsDivisionModel.AddV[i], drawModel, settingModel, Cover, barsDivisionModel.AddV[i].L / 4, barsDivisionModel.AddV[i].L / 4);
                    }
                }
            }
        }
        private static void DrawItemAddVerticalDivisionRectangle(Canvas canvas, ItemDivision itemDivision, DrawModel drawModel, SettingModel settingModel, double c, double x0, double z0)
        {
            double hook = 0;
            if (itemDivision.Type == DetailShopStyle.DS11) hook = Math.PI / 2;
            if (itemDivision.Type == DetailShopStyle.DS12) hook = Math.PI * 0.75;
            if (itemDivision.Type == DetailShopStyle.DS13) hook = Math.PI;
            double h = itemDivision.L + 2 * c;
            double left = drawModel.Left + itemDivision.Location.X / drawModel.Scale + x0 / drawModel.Scale;
            double top = drawModel.Top - itemDivision.Location.Z / drawModel.Scale + z0 / drawModel.Scale;
            DrawImage.DrawHook(canvas, left, top, drawModel.Scale, h, c, itemDivision.Diameter, 3 * itemDivision.Diameter, hook, Brushes.Red);
            double x1a = left;
            double x2a = left + (itemDivision.L) / drawModel.Scale;
            double y1a = top;
            double y2a = top;
            DrawTextItemDivision(canvas, drawModel, settingModel, itemDivision.La, 0, x1a, x2a, y1a, y2a, true);
        }
        private static void DrawItemAddVerticalDivisionCylindrical(Canvas canvas, ItemDivision itemDivision, DrawModel drawModel, SettingModel settingModel, double c, double x0, double z0)
        {
            double h = itemDivision.L + 2 * c;
            double left = drawModel.Left + itemDivision.Location.X / drawModel.Scale + x0 / drawModel.Scale;
            double top = drawModel.Top - itemDivision.Location.Z / drawModel.Scale + z0 / drawModel.Scale;
            double x1a = left;
            double x2a = left + (itemDivision.L) / drawModel.Scale;
            double y1a = top;
            double y2a = top;

            double x1b = left;
            double x2b = left;
            double y1b = top + (h) / drawModel.Scale;
            double y2b = top;
            switch (itemDivision.Type)
            {
                case DetailShopStyle.DS11:
                    DrawImage.DrawHook(canvas, left, top, drawModel.Scale, h, c, itemDivision.Diameter, 3 * itemDivision.Diameter, Math.PI / 2, Brushes.Red);
                    DrawTextItemDivision(canvas, drawModel, settingModel, itemDivision.L, 0, x1a, x2a, y1a, y2a, true);
                    break;
                case DetailShopStyle.DS12:
                    DrawImage.DrawHook(canvas, left, top, drawModel.Scale, h, c, itemDivision.Diameter, 3 * itemDivision.Diameter, Math.PI * 0.75, Brushes.Red);
                    DrawTextItemDivision(canvas, drawModel, settingModel, itemDivision.L, 0, x1a, x2a, y1a, y2a, true);
                    break;
                case DetailShopStyle.DS13:
                    DrawImage.DrawHook(canvas, left, top, drawModel.Scale, h, c, itemDivision.Diameter, 3 * itemDivision.Diameter, Math.PI, Brushes.Red);
                    DrawTextItemDivision(canvas, drawModel, settingModel, itemDivision.L, 0, x1a, x2a, y1a, y2a, true);
                    break;
                case DetailShopStyle.DS11A:
                    DrawImage.DrawHookVertical(canvas, left, top, drawModel.Scale, h, c, itemDivision.Diameter, 3 * itemDivision.Diameter, Math.PI / 2, Brushes.Red);
                    DrawTextItemDivision(canvas, drawModel, settingModel, itemDivision.L, 90, x1b, x2b, y1b, y2b, true);
                    break;
                case DetailShopStyle.DS12A:
                    DrawImage.DrawHookVertical(canvas, left, top, drawModel.Scale, h, c, itemDivision.Diameter, 3 * itemDivision.Diameter, Math.PI * 0.75, Brushes.Red);
                    DrawTextItemDivision(canvas, drawModel, settingModel, itemDivision.L, 90, x1b, x2b, y1b, y2b, true);
                    break;
                case DetailShopStyle.DS13A:
                    DrawImage.DrawHookVertical(canvas, left, top, drawModel.Scale, h, c, itemDivision.Diameter, 3 * itemDivision.Diameter, Math.PI, Brushes.Red);
                    DrawTextItemDivision(canvas, drawModel, settingModel, itemDivision.L, 90, x1b, x2b, y1b, y2b, true);
                    break;
                default: break;
            }
        }
        #endregion
        #region Bar
        private static void DrawItemBar(Canvas canvas, BarsDivisionModel barsDivisionModel, DrawModel drawModel, SettingModel settingModel, double Cover,double maxWidth)
        {
            if (barsDivisionModel.Main.Count != 0)
            {
                for (int i = 0; i < barsDivisionModel.Main.Count; i++)
                {
                    DrawItemDivision(canvas, barsDivisionModel.Main[i], drawModel, settingModel, settingModel.L2 * (i+1)+ maxWidth);
                    
                }
            }
        }
        #endregion
        #region Item
        private static void DrawTextItemDivision(Canvas canvas, DrawModel drawModel, SettingModel settingModel, double delta, double angle, double x1, double x2, double y1, double y2, bool rectangle)
        {
            bool set = (PointModel.AreEqual(angle, 90)) || (PointModel.AreEqual(angle, -90));
            bool set1 = (PointModel.AreEqual(angle, 90));
            bool set2 = (PointModel.AreEqual(angle, -90));
            double deltaTop = ((set) ? 0 : (angle == 0) ? 14 : 14 * Math.Cos(angle * Math.PI / 180));
            double deltaLeft = (set) ? ((set1) ? 8 : ((set2) ? -11 : 0)) : 14 * Math.Sin(angle * Math.PI / 180);
            TextBlock textL = new TextBlock();
            textL.Text = ((rectangle) ? "" : "D=") + Math.Round(delta, 3) + "";
            textL.FontSize = 11;
            textL.Foreground = Brushes.Black;
            textL.FontFamily = new FontFamily("Tahoma");
            textL.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            textL.Arrange(new Rect(textL.DesiredSize));
            Canvas.SetTop(textL, (y1 + y2) / 2 - textL.ActualHeight / 2 - deltaTop);
            Canvas.SetLeft(textL, (x1 + x2) / 2 - textL.ActualWidth / 2 + deltaLeft);
            textL.LayoutTransform = new RotateTransform(angle, 25, 25);
            canvas.Children.Add(textL);
        }
        private static void DrawItemDivision(Canvas canvas, ItemDivision itemDivision, DrawModel drawModel, SettingModel settingModel, double x0)
        {
            for (int i = 1; i < itemDivision.AllLocation.Count; i++)
            {
                double x01 = Math.Sqrt(itemDivision.AllLocation[i - 1].X * itemDivision.AllLocation[i - 1].X + itemDivision.AllLocation[i - 1].Y * itemDivision.AllLocation[i - 1].Y);
                double x02 = Math.Sqrt(itemDivision.AllLocation[i].X * itemDivision.AllLocation[i].X + itemDivision.AllLocation[i].Y * itemDivision.AllLocation[i].Y);
                double x1 = drawModel.Left + itemDivision.AllLocation[i - 1].X / drawModel.Scale + x0 / drawModel.Scale;
                double x2 = drawModel.Left + itemDivision.AllLocation[i ].X / drawModel.Scale + x0 / drawModel.Scale;
                double y1 = drawModel.Top - (itemDivision.AllLocation[i - 1].Z) / drawModel.Scale;
                double y2 = drawModel.Top - (itemDivision.AllLocation[i].Z) / drawModel.Scale;
                double delta = Math.Sqrt(
                    (itemDivision.AllLocation[i - 1].X - itemDivision.AllLocation[i].X) * (itemDivision.AllLocation[i - 1].X - itemDivision.AllLocation[i].X) +
                    (itemDivision.AllLocation[i - 1].Z - itemDivision.AllLocation[i].Z) * (itemDivision.AllLocation[i - 1].Z - itemDivision.AllLocation[i].Z));
                double angle = GetAngle(x1, x2, y1, y2);
                Line l1 = new Line()
                {
                    X1 = x1,
                    X2 = x2,
                    Y1 = y1,
                    Y2 = y2,
                };
                l1.Stroke = drawModel.ColorMainBarChoose;
                l1.StrokeThickness = drawModel.StrokeMain;
                canvas.Children.Add(l1);
                DrawTextItemDivision(canvas, drawModel, settingModel, delta, angle, x1, x2, y1, y2, true);
            }
        }
        private static double GetAngle(double x1, double x2, double y1, double y2)
        {
            if (x1 == x2)
            {
                return (y1 > y2) ? 90 : -90;
            }
            else
            {
                return Math.Atan((y2 - y1) / (x2 - x1)) * (180 / Math.PI);
            }
        }
        #endregion

    }
}
