using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System;


namespace R02_BeamsRebar
{
    public class DrawDivisionShop
    {
        #region Draw Stirrup
        public static void DrawShopStirrup(Canvas canvas, BarsDivisionModel barsDivisionModel, DrawModel drawModel, SettingModel settingModel,double c)
        {
            double y0 = (2 * settingModel.L3 + settingModel.L4) / 2;
            for (int i = 0; i < barsDivisionModel.Stirrups.Count; i++)
            {
                DrawItemStirrupDivision(canvas, barsDivisionModel.Stirrups[i], drawModel, settingModel, c, y0);
            }

        }
        #endregion
        #region AntiStirrups
        public static void DrawShopAntiStirrup(Canvas canvas, BarsDivisionModel barsDivisionModel, DrawModel drawModel, SettingModel settingModel, double c)
        {
            double y0 = (2 * settingModel.L3 + settingModel.L4) / 2;
            if (barsDivisionModel.AntiStirrups.Count != 0)
            {
                for (int i = 0; i < barsDivisionModel.AntiStirrups.Count; i++)
                {
                    DrawItemStirrupAntiDivision(canvas, barsDivisionModel.AntiStirrups[i], drawModel, settingModel, c, y0);
                }
            }
           
        }
        #endregion
        #region Draw MainTop
        public static void DrawShopMainTop(Canvas canvas, BarsDivisionModel barsDivisionModel, DrawModel drawModel, SettingModel settingModel, SelectedIndexModel selectedIndexModel)
        {
            double y0 = 0.2 * settingModel.L3;
            if (barsDivisionModel.MainTop.Count == 1)
            {
                DrawItemDivision(canvas, barsDivisionModel.MainTop[0], drawModel, settingModel, 0);
            }
            else
            {
                for (int i = 0; i < barsDivisionModel.MainTop.Count; i++)
                {
                    if (i == barsDivisionModel.MainTop.Count - 1)
                    {
                        DrawItemDivision(canvas, barsDivisionModel.MainTop[i], drawModel, settingModel, (i % 2 == 0) ? 0 : y0);
                    }
                    else
                    {
                        bool a = ConditionDrawOverlap(barsDivisionModel.MainTop[i], barsDivisionModel.MainTop[i + 1]);
                        DrawItemDivision(canvas, barsDivisionModel.MainTop[i], drawModel, settingModel, (i % 2 == 0 && a) ? 0 : y0);
                        if (a)
                        {
                            DrawOverlapItemDivision(canvas, barsDivisionModel.MainTop[i], barsDivisionModel.MainTop[i + 1], drawModel, 0);

                        }
                    }
                }
            }

        }
        #endregion
        #region Draw Main Bottom
        public static void DrawShopMainBottom(Canvas canvas, BarsDivisionModel barsDivisionModel, DrawModel drawModel, SettingModel settingModel)
        {
            double y0 = settingModel.L4 + 2 * settingModel.L3;
            double y1 = 0.2 * settingModel.L3;
            if (barsDivisionModel.MainBottom.Count == 1)
            {
                DrawItemDivision(canvas, barsDivisionModel.MainBottom[0], drawModel, settingModel, y0);
            }
            else
            {
                for (int i = 0; i < barsDivisionModel.MainBottom.Count; i++)
                {
                    if (i == barsDivisionModel.MainBottom.Count - 1)
                    {
                        DrawItemDivision(canvas, barsDivisionModel.MainBottom[i], drawModel, settingModel, (i % 2 == 0) ? y0 : y0 + y1);
                    }
                    else
                    {
                        bool a = ConditionDrawOverlap(barsDivisionModel.MainBottom[i], barsDivisionModel.MainBottom[i + 1]);
                        DrawItemDivision(canvas, barsDivisionModel.MainBottom[i], drawModel, settingModel, (i % 2 == 0 && a) ? y0 : y0 + y1);
                        if (a)
                        {
                            DrawOverlapItemDivision(canvas, barsDivisionModel.MainBottom[i], barsDivisionModel.MainBottom[i + 1], drawModel, y0);

                        }
                    }
                }
            }
        }
        #endregion
        #region Draw Add Top 
        public static void DrawShopAddTop(Canvas canvas, BarsDivisionModel barsDivisionModel, DrawModel drawModel, SettingModel settingModel)
        {
            double y0 = settingModel.L3;
            if (barsDivisionModel.AddTop.Count != 0)
            {
                for (int i = 0; i < barsDivisionModel.AddTop.Count; i++)
                {
                    DrawItemDivision(canvas, barsDivisionModel.AddTop[i], drawModel, settingModel, y0);
                }
            }
        }
        #endregion
        #region Draw Add Bottom 
        public static void DrawShopAddBottom(Canvas canvas, BarsDivisionModel barsDivisionModel, DrawModel drawModel, SettingModel settingModel)
        {
            double y0 = settingModel.L3 + settingModel.L4;
            if (barsDivisionModel.AddBottom.Count != 0)
            {
                for (int i = 0; i < barsDivisionModel.AddBottom.Count; i++)
                {
                    DrawItemDivision(canvas, barsDivisionModel.AddBottom[i], drawModel, settingModel, y0);
                }
            }
        }
        public static void DrawShopSpecial(Canvas canvas, BarsDivisionModel barsDivisionModel, DrawModel drawModel, SettingModel settingModel)
        {

            double y0 = (2 * settingModel.L3 + settingModel.L4) / 2;
            if (barsDivisionModel.Special.Count != 0)
            {
                for (int i = 0; i < barsDivisionModel.Special.Count; i++)
                {
                    DrawItemDivision(canvas, barsDivisionModel.Special[i], drawModel, settingModel, y0);
                }
            }
        }
        #endregion
        #region Side bar
        public static void DrawShopSide(Canvas canvas, BarsDivisionModel barsDivisionModel, DrawModel drawModel, SettingModel settingModel)
        {
            double y0 = (2 * settingModel.L3 + settingModel.L4) / 2;
            if (barsDivisionModel.Side.Count != 0)
            {
                for (int i = 0; i < barsDivisionModel.Side.Count; i++)
                {
                    DrawItemDivision(canvas, barsDivisionModel.Side[i], drawModel, settingModel, y0);
                }
            }
        }
        #endregion
        #region Item
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
        private static void DrawItemDivision(Canvas canvas, ItemDivision itemDivision, DrawModel drawModel, SettingModel settingModel, double y0)
        {
            for (int i = 1; i < itemDivision.AllLocation.Count; i++)
            {
                double x1 = drawModel.Left + itemDivision.AllLocation[i - 1].X / drawModel.Scale;
                double x2 = drawModel.Left + itemDivision.AllLocation[i].X / drawModel.Scale;
                double y1 = drawModel.Top + (itemDivision.AllLocation[i - 1].Y + y0) / drawModel.Scale;
                double y2 = drawModel.Top + (itemDivision.AllLocation[i].Y + y0) / drawModel.Scale;
                double delta = Math.Sqrt((itemDivision.AllLocation[i - 1].X - itemDivision.AllLocation[i].X) *
                    (itemDivision.AllLocation[i - 1].X - itemDivision.AllLocation[i].X) +
                    (itemDivision.AllLocation[i - 1].Y - itemDivision.AllLocation[i].Y) * (itemDivision.AllLocation[i - 1].Y - itemDivision.AllLocation[i].Y));
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
                DrawTextItemDivision(canvas, drawModel, settingModel, delta, angle, x1, x2, y1, y2);

            }
        }
        private static void DrawTextItemDivision(Canvas canvas, DrawModel drawModel, SettingModel settingModel, double delta, double angle, double x1, double x2, double y1, double y2)
        {
            bool set = (PointModel.AreEqual(angle, 90)) || (PointModel.AreEqual(angle, -90));
            bool set1 = (PointModel.AreEqual(angle, 90));
            bool set2 = (PointModel.AreEqual(angle, -90));
            double deltaTop = ((set) ? 0 : (angle == 0) ? 14 : 14 * Math.Cos(angle*Math.PI/180));
            double deltaLeft = (set) ? ((set1) ? 8 : ((set2) ? -11 : 0)) : 14 * Math.Sin(angle * Math.PI / 180);
            TextBlock textL = new TextBlock();
            textL.Text = Math.Round(delta, 3) + "";
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
        private static void DrawOverlapItemDivision(Canvas canvas, ItemDivision itemDivision1, ItemDivision itemDivision2, DrawModel drawModel, double y0)
        {
            double left = drawModel.Left + itemDivision2.AllLocation[0].X / drawModel.Scale;
            double top = drawModel.Top + (itemDivision2.AllLocation[0].Y + y0) / drawModel.Scale;
            double overlap = Math.Round(itemDivision1.AllLocation[itemDivision1.AllLocation.Count - 1].X - itemDivision2.AllLocation[0].X, 3);
            DrawMainCanvas.DimHorizontalTopOverlap(canvas, left, top, drawModel.Scale, overlap, 11, drawModel.Offset, drawModel.Extend, drawModel.StrokeDim);
        }
        private static void DrawItemStirrupDivision(Canvas canvas, ItemDivision itemDivision, DrawModel drawModel, SettingModel settingModel,double c, double y0)
        {
            double b = itemDivision.La + 2 * c;
            double h = itemDivision.Lb + 2 * c;
            double left = drawModel.Left + itemDivision.Location.X / drawModel.Scale - (b / 2) / drawModel.Scale;
            double top = drawModel.Top + itemDivision.Location.Y / drawModel.Scale+y0/drawModel.Scale;
            DrawImage.DrawStirrup(canvas, left, top, drawModel.Scale, b, h, c, 6, 12, Brushes.Red);
            double x1a = left;
            double x2a = left + (itemDivision.La ) / drawModel.Scale;
            double y1a = top;
            double y2a = top;
            DrawTextItemDivision(canvas, drawModel, settingModel, itemDivision.La, 0, x1a, x2a, y1a, y2a);
            double x1b = left;
            double x2b = left;
            double y1b = top+h/drawModel.Scale;
            double y2b = top;
            DrawTextItemDivision(canvas, drawModel, settingModel, itemDivision.Lb, 90, x1b, x2b, y1b, y2b);
        }
        private static void DrawItemStirrupAntiDivision(Canvas canvas, ItemDivision itemDivision, DrawModel drawModel, SettingModel settingModel, double c, double y0)
        {
            double b = itemDivision.L + 2 * c;
            double hook = settingModel.SelectedHook.get_Parameter(Autodesk.Revit.DB. BuiltInParameter.REBAR_HOOK_ANGLE).AsDouble();
            double left = drawModel.Left + itemDivision.Location.X / drawModel.Scale ;
            double top = drawModel.Top + itemDivision.Location.Y / drawModel.Scale +y0/drawModel.Scale;
            DrawImage.DrawHook(canvas, left, top, drawModel.Scale, b, c, 6, 12, hook, Brushes.Red);
            double x1a = left;
            double x2a = left + (itemDivision.L) / drawModel.Scale;
            double y1a = top;
            double y2a = top;
            DrawTextItemDivision(canvas, drawModel, settingModel, itemDivision.L, 0, x1a, x2a, y1a, y2a);
        }
        #endregion
    }
}