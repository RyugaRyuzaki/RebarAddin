using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace R11_FoundationPile
{
    public class DrawMainCanvas
    {
        #region
        private static void DrawAxisAndSectionColumn(Canvas canvas, DrawModel drawModel, ColumnModel columnModel, bool IsAxis)
        {
            if (IsAxis) DrawImage.DrawAxis(canvas);
            if (columnModel.Style.Equals("RECTANGLE"))
            {
                DrawImage.DrawSection(canvas, drawModel.Scale, drawModel.Left - columnModel.b * 0.5 / drawModel.Scale, drawModel.Top - columnModel.h * 0.5 / drawModel.Scale, columnModel.b, columnModel.h);
            }
            else
            {
                DrawImage.DrawCylindricalSection(canvas, drawModel.Left, drawModel.Top, drawModel.Scale, columnModel.D);
            }
        }
        private static void DrawBounding(Canvas canvas, DrawModel drawModel, FoundationModel foundationModel, bool dim)
        {
            for (int i = 0; i < foundationModel.BoundingLocation.Count; i++)
            {
                if (i == 0)
                {
                    Line l1 = new Line() { X1 = drawModel.Left + (foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].X) / drawModel.Scale, X2 = drawModel.Left + (foundationModel.BoundingLocation[i].X) / drawModel.Scale, Y1 = drawModel.Top - (foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].Y) / drawModel.Scale, Y2 = drawModel.Top - (foundationModel.BoundingLocation[i].Y) / drawModel.Scale };
                    l1.Stroke = Brushes.Black;
                    l1.StrokeThickness = 1;
                    canvas.Children.Add(l1);
                }
                else
                {
                    Line l1 = new Line() { X1 = drawModel.Left + (foundationModel.BoundingLocation[i - 1].X) / drawModel.Scale, X2 = drawModel.Left + (foundationModel.BoundingLocation[i].X) / drawModel.Scale, Y1 = drawModel.Top - (foundationModel.BoundingLocation[i - 1].Y) / drawModel.Scale, Y2 = drawModel.Top - (foundationModel.BoundingLocation[i].Y) / drawModel.Scale };
                    l1.Stroke = Brushes.Black;
                    l1.StrokeThickness = 1;
                    canvas.Children.Add(l1);
                }
            }
            if (dim)
            {
                double minX = foundationModel.BoundingLocation.Min(x => x.X);
                double minY = foundationModel.BoundingLocation.Min(x => x.Y);
                double maxX = foundationModel.BoundingLocation.Max(x => x.X);
                double maxY = foundationModel.BoundingLocation.Max(x => x.Y);
                DrawImage.DimHorizontal(canvas, drawModel.Left + minX / drawModel.Scale, drawModel.Top - maxY / drawModel.Scale, drawModel.Scale, Math.Round((maxX - minX), 3), 11, 40, 5);
                DrawImage.DimVertical(canvas, drawModel.Left + minX / drawModel.Scale, drawModel.Top - maxY / drawModel.Scale, drawModel.Scale, Math.Round((maxY - minY), 3), 11, 40, 5);
            }

        }
        private static void DrawPiles(Canvas canvas, DrawModel drawModel, FoundationModel foundationModel, SettingModel settingModel, int numberPile, bool text)
        {
            SolidColorBrush solidColorBrush = drawModel.ColorMainBar;
            for (int i = 0; i < foundationModel.PileModels.Count; i++)
            {
                if (i == numberPile)
                {
                    solidColorBrush = drawModel.ColorMainBarChoose;
                }
                else
                {
                    solidColorBrush = drawModel.ColorMainBar;
                }
                if (settingModel.StyleFamilyType.Equals("RECTANGLE"))
                {
                    DrawImage.DrawSectionDashArraySolidColorBrush(canvas, drawModel.Scale, drawModel.Left - (settingModel.b * 0.5) / drawModel.Scale + foundationModel.PileModels[i].Location.X / drawModel.Scale, drawModel.Top - (settingModel.h * 0.5) / drawModel.Scale - foundationModel.PileModels[i].Location.Y / drawModel.Scale, settingModel.b, settingModel.h, solidColorBrush);
                }
                else
                {
                    DrawImage.DrawCylindricalSectionDashArraySolidColorBrush(canvas, drawModel.Left + foundationModel.PileModels[i].Location.X / drawModel.Scale, drawModel.Top - foundationModel.PileModels[i].Location.Y / drawModel.Scale, drawModel.Scale, settingModel.DiameterPile, solidColorBrush);
                }
                if (text) DrawImage.DrawTextOnePileSection(canvas, drawModel.Left + foundationModel.PileModels[i].Location.X / drawModel.Scale, drawModel.Top - foundationModel.PileModels[i].Location.Y / drawModel.Scale, foundationModel.PileModels[i].PileNumber, solidColorBrush);
            }

        }
        public static void DrawMainFoundation(Canvas canvas, DrawModel drawModel, FoundationModel foundationModel, SettingModel settingModel, int numberPile)
        {
            DrawAxisAndSectionColumn(canvas, drawModel, foundationModel.ColumnModel, true);
            DrawBounding(canvas, drawModel, foundationModel, true);
            DrawPiles(canvas, drawModel, foundationModel, settingModel, numberPile, true);
        }
        private static void DrawSpanOrientation(Canvas canvas, DrawModel drawModel, FoundationModel foundationModel, int Image, string orientation)
        {
            SolidColorBrush solidColorBrushChosse = drawModel.ColorMainBarChoose;
            SolidColorBrush solidColorBrush = drawModel.ColorMainBar;
            //draw Vertical

            double x = 0;
            double y = 0;
            switch (Image)
            {
                case 0:
                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE"))
                    {
                        if (foundationModel.ColumnModel.b <= foundationModel.ColumnModel.h)
                        {
                            x = drawModel.Left;
                            y = drawModel.Top - (foundationModel.BoundingLocation[0].Y * 0.5 + foundationModel.BoundingLocation[1].Y * 0.5) / drawModel.Scale;
                        }
                        else
                        {
                            x = drawModel.Left + (foundationModel.BoundingLocation[0].X * 0.5 + foundationModel.BoundingLocation[1].X * 0.5) / drawModel.Scale;
                            y = drawModel.Top;
                        }
                    }
                    else
                    {
                        x = drawModel.Left;
                        y = drawModel.Top - (foundationModel.BoundingLocation[0].Y * 0.5 + foundationModel.BoundingLocation[1].Y * 0.5) / drawModel.Scale;
                    }
                    break;
                case 1: x = drawModel.Left; y = drawModel.Top; break;
                case 2:
                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE"))
                    {
                        if (foundationModel.ColumnModel.b <= foundationModel.ColumnModel.h)
                        {
                            x = drawModel.Left;
                            y = drawModel.Top - (foundationModel.BoundingLocation[1].Y) / drawModel.Scale;
                        }
                        else
                        {
                            x = drawModel.Left + (foundationModel.BoundingLocation[1].X) / drawModel.Scale;
                            y = drawModel.Top;
                        }
                    }
                    else
                    {
                        x = drawModel.Left;
                        y = drawModel.Top - (foundationModel.BoundingLocation[1].Y) / drawModel.Scale;
                    }
                    break;
                case 3: x = drawModel.Left; y = drawModel.Top; break;
                default:
                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE"))
                    {
                        if (foundationModel.ColumnModel.b <= foundationModel.ColumnModel.h)
                        {
                            x = drawModel.Left;
                            y = drawModel.Top - (foundationModel.BoundingLocation[0].Y * 0.5 + foundationModel.BoundingLocation[1].Y * 0.5) / drawModel.Scale;
                        }
                        else
                        {
                            x = drawModel.Left + (foundationModel.BoundingLocation[0].X * 0.5 + foundationModel.BoundingLocation[1].X * 0.5) / drawModel.Scale;
                            y = drawModel.Top;
                        }
                    }
                    else
                    {
                        x = drawModel.Left;
                        y = drawModel.Top - (foundationModel.BoundingLocation[0].Y * 0.5 + foundationModel.BoundingLocation[1].Y * 0.5) / drawModel.Scale;
                    }
                    break;
            }
            DrawImage.DrawHorizontalOrientation(canvas, y, (orientation.Equals("Horizontal")) ? solidColorBrushChosse : solidColorBrush);
            DrawImage.DrawVerticalOrientation(canvas, x, (orientation.Equals("Horizontal")) ? solidColorBrush : solidColorBrushChosse);

        }
        public static void DrawBarFoundationSection(Canvas canvas, DrawModel drawModel, FoundationModel foundationModel, SettingModel settingModel, int numberPile, int Image, string orientation)
        {
            DrawAxisAndSectionColumn(canvas, drawModel, foundationModel.ColumnModel, false);
            DrawBounding(canvas, drawModel, foundationModel, false);
            DrawPiles(canvas, drawModel, foundationModel, settingModel, numberPile, false);
            DrawSpanOrientation(canvas, drawModel, foundationModel, Image, orientation);

        }
        #endregion
        #region   Section
        private static void DrawAxisAndSectionColumnSection(Canvas canvas, DrawModel drawModel, ColumnModel columnModel)
        {
            DrawImage.DrawAxisSection(canvas);
            if (columnModel.Style.Equals("RECTANGLE"))
            {
                DrawImage.DrawSection(canvas, drawModel.Scale, drawModel.Left - columnModel.b * 0.5 / drawModel.Scale, drawModel.Top - columnModel.h * 0.5 / drawModel.Scale, columnModel.b, columnModel.h);
            }
            else
            {
                DrawImage.DrawCylindricalSection(canvas, drawModel.Left, drawModel.Top, drawModel.Scale, columnModel.D);
            }
        }
        private static void DrawBoundingSection(Canvas canvas, DrawModel drawModel, FoundationModel foundationModel)
        {
            for (int i = 0; i < foundationModel.BoundingLocation.Count; i++)
            {
                if (i == 0)
                {
                    Line l1 = new Line() { X1 = drawModel.Left + (foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].X) / drawModel.Scale, X2 = drawModel.Left + (foundationModel.BoundingLocation[i].X) / drawModel.Scale, Y1 = drawModel.Top - (foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].Y) / drawModel.Scale, Y2 = drawModel.Top - (foundationModel.BoundingLocation[i].Y) / drawModel.Scale };
                    l1.Stroke = Brushes.Black;
                    l1.StrokeThickness = 1;
                    canvas.Children.Add(l1);
                }
                else
                {
                    Line l1 = new Line() { X1 = drawModel.Left + (foundationModel.BoundingLocation[i - 1].X) / drawModel.Scale, X2 = drawModel.Left + (foundationModel.BoundingLocation[i].X) / drawModel.Scale, Y1 = drawModel.Top - (foundationModel.BoundingLocation[i - 1].Y) / drawModel.Scale, Y2 = drawModel.Top - (foundationModel.BoundingLocation[i].Y) / drawModel.Scale };
                    l1.Stroke = Brushes.Black;
                    l1.StrokeThickness = 1;
                    canvas.Children.Add(l1);
                }
            }
            double minX = foundationModel.BoundingLocation.Min(x => x.X);
            double minY = foundationModel.BoundingLocation.Min(x => x.Y);
            double maxX = foundationModel.BoundingLocation.Max(x => x.X);
            double maxY = foundationModel.BoundingLocation.Max(x => x.Y);
            DrawImage.DimHorizontal(canvas, drawModel.Left + minX / drawModel.Scale, drawModel.Top - maxY / drawModel.Scale, drawModel.Scale, Math.Round((maxX - minX), 3), 11, 15, 5);
            DrawImage.DimVertical(canvas, drawModel.Left + minX / drawModel.Scale, drawModel.Top - maxY / drawModel.Scale, drawModel.Scale, Math.Round((maxY - minY), 3), 11, 15, 5);
        }
        private static void DrawPilesSection(Canvas canvas, DrawModel drawModel, FoundationModel foundationModel, SettingModel settingModel, int numberPile)
        {
            SolidColorBrush solidColorBrush = drawModel.ColorMainBar;
            for (int i = 0; i < foundationModel.PileModels.Count; i++)
            {
                if (i == numberPile)
                {
                    solidColorBrush = drawModel.ColorMainBarChoose;
                }
                else
                {
                    solidColorBrush = drawModel.ColorMainBar;
                }
                if (settingModel.StyleFamilyType.Equals("RECTANGLE"))
                {
                    DrawImage.DrawSectionDashArraySolidColorBrush(canvas, drawModel.Scale, drawModel.Left - (settingModel.b * 0.5) / drawModel.Scale + foundationModel.PileModels[i].Location.X / drawModel.Scale, drawModel.Top - (settingModel.h * 0.5) / drawModel.Scale - foundationModel.PileModels[i].Location.Y / drawModel.Scale, settingModel.b, settingModel.h, solidColorBrush);
                }
                else
                {
                    DrawImage.DrawCylindricalSectionDashArraySolidColorBrush(canvas, drawModel.Left + foundationModel.PileModels[i].Location.X / drawModel.Scale, drawModel.Top - foundationModel.PileModels[i].Location.Y / drawModel.Scale, drawModel.Scale, settingModel.DiameterPile, solidColorBrush);
                }
                DrawImage.DrawTextOnePileSection(canvas, drawModel.Left + foundationModel.PileModels[i].Location.X / drawModel.Scale, drawModel.Top - foundationModel.PileModels[i].Location.Y / drawModel.Scale, foundationModel.PileModels[i].PileNumber, solidColorBrush);
            }

        }
        public static void DrawMainFoundationSection(Canvas canvas, DrawModel drawModel, FoundationModel foundationModel, SettingModel settingModel, int numberPile)
        {
            DrawAxisAndSectionColumnSection(canvas, drawModel, foundationModel.ColumnModel);
            DrawBoundingSection(canvas, drawModel, foundationModel);
            DrawPilesSection(canvas, drawModel, foundationModel, settingModel, numberPile);
        }
        #endregion
        #region PileDetail
        private static void DrawBoundingPileDetail(Canvas canvas, DrawModel drawModel, FoundationModel foundationModel, SolidColorBrush solidColorBrush, double minX, double minY)
        {

            double left = (foundationModel.ColumnModel.PointXPosition - minX) / drawModel.Scale;
            double top = drawModel.Top - (foundationModel.ColumnModel.PointYPosition - minY) / drawModel.Scale;
            for (int i = 0; i < foundationModel.BoundingLocation.Count; i++)
            {

                if (i == 0)
                {
                    Line l1 = new Line()
                    {
                        X1 = left + (foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].X) / drawModel.Scale,
                        X2 = left + (foundationModel.BoundingLocation[i].X) / drawModel.Scale,
                        Y1 = top - (foundationModel.BoundingLocation[foundationModel.BoundingLocation.Count - 1].Y) / drawModel.Scale,
                        Y2 = top - (foundationModel.BoundingLocation[i].Y) / drawModel.Scale
                    };
                    l1.Stroke = solidColorBrush;
                    l1.StrokeThickness = 1;
                    canvas.Children.Add(l1);
                }
                else
                {
                    Line l1 = new Line() { X1 = left + (foundationModel.BoundingLocation[i - 1].X) / drawModel.Scale, X2 = left + (foundationModel.BoundingLocation[i].X) / drawModel.Scale, Y1 = top - (foundationModel.BoundingLocation[i - 1].Y) / drawModel.Scale, Y2 = top - (foundationModel.BoundingLocation[i].Y) / drawModel.Scale };
                    l1.Stroke = solidColorBrush;
                    l1.StrokeThickness = 1;
                    canvas.Children.Add(l1);
                }
            }
        }
        private static void DrawPilesDetail(Canvas canvas, DrawModel drawModel, FoundationModel foundationModel, SettingModel settingModel, double minX, double minY)
        {
            SolidColorBrush solidColorBrush = drawModel.ColorMainBar;
            double left = (foundationModel.ColumnModel.PointXPosition - minX) / drawModel.Scale;
            double top = drawModel.Top - (foundationModel.ColumnModel.PointYPosition - minY) / drawModel.Scale;
            for (int i = 0; i < foundationModel.PileModels.Count; i++)
            {

                if (settingModel.StyleFamilyType.Equals("RECTANGLE"))
                {
                    DrawImage.DrawSectionDashArraySolidColorBrush(canvas, drawModel.Scale, left - (settingModel.b * 0.5) / drawModel.Scale + foundationModel.PileModels[i].Location.X / drawModel.Scale, top - (settingModel.h * 0.5) / drawModel.Scale - foundationModel.PileModels[i].Location.Y / drawModel.Scale, settingModel.b, settingModel.h, solidColorBrush);
                }
                else
                {
                    DrawImage.DrawCylindricalSectionDashArraySolidColorBrush(canvas, left + foundationModel.PileModels[i].Location.X / drawModel.Scale, top - foundationModel.PileModels[i].Location.Y / drawModel.Scale, drawModel.Scale, settingModel.DiameterPile, solidColorBrush);
                }
                DrawImage.DrawTextOnePileSection(canvas, left + 11 + foundationModel.PileModels[i].Location.X / drawModel.Scale, top - 11 - foundationModel.PileModels[i].Location.Y / drawModel.Scale, foundationModel.PileModels[i].PileNumber, solidColorBrush);
            }

        }
        public static void DrawMainPileDetail(Canvas canvas, DrawModel drawModel, FoundationModel foundationModel, SettingModel settingModel, SolidColorBrush solidColorBrush, double minX, double minY)
        {
            DrawBoundingPileDetail(canvas, drawModel, foundationModel, solidColorBrush, minX, minY);
            DrawPilesDetail(canvas, drawModel, foundationModel, settingModel, minX, minY);
        }
        #endregion
        #region Bar Section
        private static void DrawBoundingItem(Canvas canvas, DrawModel drawModel, SettingModel settingModel, bool orientation, double p1, double p2)
        {
            Line l1 = new Line() { X1 = drawModel.Left + p1 / drawModel.Scale, X2 = drawModel.Left + p2 / drawModel.Scale, Y1 = (orientation) ? (410 - drawModel.Side) : (820 - drawModel.Side), Y2 = (orientation) ? (410 - drawModel.Side) : (820 - drawModel.Side) };
            l1.Stroke = drawModel.ColorBound;
            l1.StrokeThickness = 1;
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = drawModel.Left + p1 / drawModel.Scale, X2 = drawModel.Left + p2 / drawModel.Scale, Y1 = ((orientation) ? (410 - drawModel.Side) : (820 - drawModel.Side)) - settingModel.HeightFoundation / drawModel.Scale, Y2 = ((orientation) ? (410 - drawModel.Side) : (820 - drawModel.Side)) - settingModel.HeightFoundation / drawModel.Scale };
            l2.Stroke = drawModel.ColorBound;
            l2.StrokeThickness = 1;
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = drawModel.Left + p1 / drawModel.Scale, X2 = drawModel.Left + p1 / drawModel.Scale, Y1 = (orientation) ? (410 - drawModel.Side) : (820 - drawModel.Side), Y2 = ((orientation) ? (410 - drawModel.Side) : (820 - drawModel.Side)) - settingModel.HeightFoundation / drawModel.Scale };
            l3.Stroke = drawModel.ColorBound;
            l3.StrokeThickness = 1;
            canvas.Children.Add(l3);
            Line l4 = new Line() { X1 = drawModel.Left + p2 / drawModel.Scale, X2 = drawModel.Left + p2 / drawModel.Scale, Y1 = (orientation) ? (410 - drawModel.Side) : (820 - drawModel.Side), Y2 = ((orientation) ? (410 - drawModel.Side) : (820 - drawModel.Side)) - settingModel.HeightFoundation / drawModel.Scale };
            l4.Stroke = drawModel.ColorBound;
            l4.StrokeThickness = 1;
            canvas.Children.Add(l4);
            if (orientation)
            {
                DrawImage.DimVertical(canvas, drawModel.Left + p1 / drawModel.Scale, (410 - drawModel.Side) - settingModel.HeightFoundation / drawModel.Scale, drawModel.Scale, settingModel.HeightFoundation, 11, 20, 5);
            }
            else
            {
                DrawImage.DimVertical(canvas, drawModel.Left + p2 / drawModel.Scale, (820 - drawModel.Side) - settingModel.HeightFoundation / drawModel.Scale, drawModel.Scale, settingModel.HeightFoundation, 11, 20, 5);
            }
        }
        private static void DrawPileSectionItem(Canvas canvas, DrawModel drawModel, SettingModel settingModel, double D, double p, bool orientation)
        {
            DoubleCollection vs = new DoubleCollection() { 5, 2 };
            Line l1 = new Line() { X1 = drawModel.Left + p / drawModel.Scale - D * 0.5 / drawModel.Scale, X2 = drawModel.Left + p / drawModel.Scale - D * 0.5 / drawModel.Scale, Y1 = (orientation) ? (410) : (820), Y2 = ((orientation) ? (410 - drawModel.Side) : (820 - drawModel.Side)) - settingModel.Overlap / drawModel.Scale };
            l1.Stroke = drawModel.ColorBound;
            l1.StrokeThickness = 1;
            l1.StrokeDashArray = vs;
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = drawModel.Left + p / drawModel.Scale - D * 0.5 / drawModel.Scale, X2 = drawModel.Left + p / drawModel.Scale + D * 0.5 / drawModel.Scale, Y1 = (orientation) ? (410) : (820), Y2 = (orientation) ? (410) : (820) };
            l2.Stroke = drawModel.ColorBound;
            l2.StrokeThickness = 1;
            l2.StrokeDashArray = vs;
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = drawModel.Left + p / drawModel.Scale + D * 0.5 / drawModel.Scale, X2 = drawModel.Left + p / drawModel.Scale + D * 0.5 / drawModel.Scale, Y1 = (orientation) ? (410) : (820), Y2 = ((orientation) ? (410 - drawModel.Side) : (820 - drawModel.Side)) - settingModel.Overlap / drawModel.Scale };
            l3.Stroke = drawModel.ColorBound;
            l3.StrokeThickness = 1;
            l3.StrokeDashArray = vs;
            canvas.Children.Add(l3);
            Line l4 = new Line() { X1 = drawModel.Left + p / drawModel.Scale - D * 0.5 / drawModel.Scale, X2 = drawModel.Left + p / drawModel.Scale + D * 0.5 / drawModel.Scale, Y1 = ((orientation) ? (410 - drawModel.Side) : (820 - drawModel.Side)) - settingModel.Overlap / drawModel.Scale, Y2 = ((orientation) ? (410 - drawModel.Side) : (820 - drawModel.Side)) - settingModel.Overlap / drawModel.Scale };
            l4.Stroke = drawModel.ColorBound;
            l4.StrokeThickness = 1;
            l4.StrokeDashArray = vs;
            canvas.Children.Add(l4);
        }
        private static void DrawMainSpanBounding(Canvas canvas, DrawModel drawModel, FoundationModel foundationModel, SettingModel settingModel, int Image, string orientation, double p1, double p2, out List<double> p)
        {
            p = new List<double>();
            switch (Image)
            {
                case 0:

                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE") && foundationModel.ColumnModel.b > foundationModel.ColumnModel.h)
                    {
                        if (orientation.Equals("Horizontal"))
                        {
                            p.Add(foundationModel.PileModels[0].Location.X);
                            p.Add(foundationModel.PileModels[1].Location.X);
                        }
                        else
                        {
                            p.Add(foundationModel.PileModels[foundationModel.PileModels.Count - 1].Location.Y);
                            p.Add(foundationModel.PileModels[0].Location.Y);
                        }
                    }
                    else
                    {
                        if (orientation.Equals("Horizontal"))
                        {
                            p.Add(foundationModel.PileModels[foundationModel.PileModels.Count - 1].Location.X);
                            p.Add(foundationModel.PileModels[0].Location.X);
                        }
                        else
                        {
                            p.Add(foundationModel.PileModels[0].Location.Y);
                            p.Add(foundationModel.PileModels[1].Location.Y);
                        }
                    }

                    break;
                case 1:
                    if (orientation.Equals("Horizontal"))
                    {
                        for (int i = 0; i < foundationModel.PileModels.Count; i++)
                        {
                            p.Add(foundationModel.PileModels[i].Location.X);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < foundationModel.PileModels.Count; i++)
                        {
                            p.Add(foundationModel.PileModels[i].Location.Y);
                        }
                    }

                    break;
                case 2:
                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE") && foundationModel.ColumnModel.b > foundationModel.ColumnModel.h)
                    {
                        if (orientation.Equals("Horizontal"))
                        {
                            for (int i = 0; i < foundationModel.PileModels.Count; i++)
                            {
                                p.Add(foundationModel.PileModels[i].Location.X);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < foundationModel.PileModels.Count; i++)
                            {
                                p.Add(foundationModel.PileModels[i].Location.Y);
                            }
                        }
                    }
                    else
                    {
                        if (orientation.Equals("Horizontal"))
                        {
                            for (int i = 0; i < foundationModel.PileModels.Count; i++)
                            {
                                p.Add(foundationModel.PileModels[i].Location.X);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < foundationModel.PileModels.Count; i++)
                            {
                                p.Add(foundationModel.PileModels[i].Location.Y);
                            }
                        }
                    }
                    break;
                case 3:
                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE") && foundationModel.ColumnModel.b > foundationModel.ColumnModel.h)
                    {
                        if (orientation.Equals("Horizontal"))
                        {
                            for (int i = 0; i < foundationModel.PileModels.Count; i++)
                            {
                                p.Add(foundationModel.PileModels[i].Location.X);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < foundationModel.PileModels.Count; i++)
                            {
                                p.Add(foundationModel.PileModels[i].Location.Y);
                            }
                        }
                    }
                    else
                    {
                        if (orientation.Equals("Horizontal"))
                        {
                            for (int i = 0; i < foundationModel.PileModels.Count; i++)
                            {
                                p.Add(foundationModel.PileModels[i].Location.X);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < foundationModel.PileModels.Count; i++)
                            {
                                p.Add(foundationModel.PileModels[i].Location.Y);
                            }
                        }
                    }
                    break;
                default:
                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE") && foundationModel.ColumnModel.b > foundationModel.ColumnModel.h)
                    {
                        if (orientation.Equals("Horizontal"))
                        {
                            p.Add(foundationModel.PileModels[0].Location.X);
                            p.Add(foundationModel.PileModels[1].Location.X);
                        }
                        else
                        {
                            p.Add(foundationModel.PileModels[foundationModel.PileModels.Count - 1].Location.Y);
                            p.Add(foundationModel.PileModels[0].Location.Y);
                        }
                    }
                    else
                    {
                        if (orientation.Equals("Horizontal"))
                        {
                            p.Add(foundationModel.PileModels[foundationModel.PileModels.Count - 1].Location.X);
                            p.Add(foundationModel.PileModels[0].Location.X);
                        }
                        else
                        {
                            p.Add(foundationModel.PileModels[0].Location.Y);
                            p.Add(foundationModel.PileModels[1].Location.Y);
                        }
                    }
                    break;
            }
            DrawBoundingItem(canvas, drawModel, settingModel, true, p1, p2);
        }
        private static void DrawSecondarySpanBounding(Canvas canvas, DrawModel drawModel, FoundationModel foundationModel, SettingModel settingModel, int Image, string orientation, double p3, double p4, out List<double> p)
        {
            p = new List<double>();
            switch (Image)
            {
                case 0:

                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE") && foundationModel.ColumnModel.b > foundationModel.ColumnModel.h)
                    {
                        if (orientation.Equals("Horizontal"))
                        {
                            p.Add(foundationModel.PileModels[foundationModel.PileModels.Count - 1].Location.Y);
                            p.Add(foundationModel.PileModels[0].Location.Y);
                        }
                        else
                        {
                            p.Add(foundationModel.PileModels[0].Location.X);
                            p.Add(foundationModel.PileModels[1].Location.X);
                        }

                    }
                    else
                    {
                        if (orientation.Equals("Horizontal"))
                        {
                            p.Add(foundationModel.PileModels[0].Location.Y);
                            p.Add(foundationModel.PileModels[1].Location.Y);
                        }
                        else
                        {
                            p.Add(foundationModel.PileModels[foundationModel.PileModels.Count - 1].Location.X);
                            p.Add(foundationModel.PileModels[0].Location.X);
                        }
                    }

                    break;
                case 1:
                    if (orientation.Equals("Horizontal"))
                    {
                        for (int i = 0; i < foundationModel.PileModels.Count; i++)
                        {
                            p.Add(foundationModel.PileModels[i].Location.Y);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < foundationModel.PileModels.Count; i++)
                        {
                            p.Add(foundationModel.PileModels[i].Location.X);
                        }
                    }
                    break;
                case 2:
                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE") && foundationModel.ColumnModel.b > foundationModel.ColumnModel.h)
                    {
                        if (orientation.Equals("Horizontal"))
                        {
                            for (int i = 0; i < foundationModel.PileModels.Count; i++)
                            {
                                p.Add(foundationModel.PileModels[i].Location.Y);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < foundationModel.PileModels.Count; i++)
                            {
                                p.Add(foundationModel.PileModels[i].Location.X);
                            }
                        }

                    }
                    else
                    {
                        if (orientation.Equals("Horizontal"))
                        {
                            for (int i = 0; i < foundationModel.PileModels.Count; i++)
                            {
                                p.Add(foundationModel.PileModels[i].Location.Y);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < foundationModel.PileModels.Count; i++)
                            {
                                p.Add(foundationModel.PileModels[i].Location.X);
                            }
                        }
                    }
                    break;
                case 3:
                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE") && foundationModel.ColumnModel.b > foundationModel.ColumnModel.h)
                    {
                        if (orientation.Equals("Horizontal"))
                        {
                            for (int i = 0; i < foundationModel.PileModels.Count; i++)
                            {
                                p.Add(foundationModel.PileModels[i].Location.Y);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < foundationModel.PileModels.Count; i++)
                            {
                                p.Add(foundationModel.PileModels[i].Location.X);
                            }
                        }
                    }
                    else
                    {
                        if (orientation.Equals("Horizontal"))
                        {
                            for (int i = 0; i < foundationModel.PileModels.Count; i++)
                            {
                                p.Add(foundationModel.PileModels[i].Location.Y);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < foundationModel.PileModels.Count; i++)
                            {
                                p.Add(foundationModel.PileModels[i].Location.X);
                            }
                        }
                    }
                    break;
                default:
                    if (foundationModel.ColumnModel.Style.Equals("RECTANGLE") && foundationModel.ColumnModel.b > foundationModel.ColumnModel.h)
                    {
                        if (orientation.Equals("Horizontal"))
                        {
                            p.Add(foundationModel.PileModels[foundationModel.PileModels.Count - 1].Location.Y);
                            p.Add(foundationModel.PileModels[0].Location.Y);
                        }
                        else
                        {
                            p.Add(foundationModel.PileModels[0].Location.X);
                            p.Add(foundationModel.PileModels[1].Location.X);
                        }

                    }
                    else
                    {
                        if (orientation.Equals("Horizontal"))
                        {
                            p.Add(foundationModel.PileModels[0].Location.Y);
                            p.Add(foundationModel.PileModels[1].Location.Y);
                        }
                        else
                        {
                            p.Add(foundationModel.PileModels[foundationModel.PileModels.Count - 1].Location.X);
                            p.Add(foundationModel.PileModels[0].Location.X);
                        }
                    }
                    break;
            }
            DrawBoundingItem(canvas, drawModel, settingModel, false, p3, p4);
        }
        private static void DrawMainBarBottomItem(Canvas canvas, DrawModel drawModel, BarModel barModel, SettingModel settingModel, double p1, double p2, double coverTop, double coverBottom, double coverSide, SolidColorBrush solidColorBrush)
        {
            double left = (p1 + ((p1 < 0) ? (1) : (-1)) * (coverSide + barModel.Bar.Diameter * 0.5)) / drawModel.Scale;
            double right = (p2 + ((p2 < 0) ? (1) : (-1)) * (coverSide + barModel.Bar.Diameter * 0.5)) / drawModel.Scale;
            double top = 410 - drawModel.Side - (coverBottom + barModel.Bar.Diameter * 0.5) / drawModel.Scale - barModel.HookLength / drawModel.Scale;
            double bot = 410 - drawModel.Side - (coverBottom + barModel.Bar.Diameter * 0.5) / drawModel.Scale;
            Line l1 = new Line() { X1 = drawModel.Left + left, X2 = drawModel.Left + left, Y1 = top, Y2 = bot };
            l1.Stroke = solidColorBrush;
            l1.StrokeThickness = barModel.Bar.Diameter / drawModel.Scale;
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = drawModel.Left + left, X2 = drawModel.Left + right, Y1 = bot, Y2 = bot };
            l2.Stroke = solidColorBrush;
            l2.StrokeThickness = barModel.Bar.Diameter / drawModel.Scale;
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = drawModel.Left + right, X2 = drawModel.Left + right, Y1 = bot, Y2 = top };
            l3.Stroke = solidColorBrush;
            l3.StrokeThickness = barModel.Bar.Diameter / drawModel.Scale;
            canvas.Children.Add(l3);
        }
        private static void DrawMainBarBottomSectionItem(Canvas canvas, DrawModel drawModel, BarModel barModel, SettingModel settingModel, double p1, double p2, double coverTop, double coverBottom, double coverSide, SolidColorBrush solidColorBrush)
        {
            int n = (int)((Math.Abs(p1 - p2) - 2 * coverSide - barModel.Bar.Diameter) / barModel.Distance) + 1;
            double left = (p1 + ((p1 < 0) ? (1) : (-1)) * (coverSide + barModel.Bar.Diameter * 0.5)) / drawModel.Scale;
            double right = (p2 + ((p2 < 0) ? (1) : (-1)) * (coverSide + barModel.Bar.Diameter * 0.5)) / drawModel.Scale;
            double top = 820 - drawModel.Side - (coverBottom + barModel.Bar.Diameter * 0.5) / drawModel.Scale - barModel.HookLength / drawModel.Scale;
            double bot = 820 - drawModel.Side - (coverBottom + barModel.Bar.Diameter * 0.5) / drawModel.Scale;
            for (int i = 0; i < n; i++)
            {
                DrawImage.DrawOneBarSection(canvas, drawModel.Left + left + i * ((p1 < p2) ? (1) : (-1)) * (barModel.Distance / drawModel.Scale), bot, drawModel.Scale, barModel.Bar.Diameter, solidColorBrush);
            }
        }
        private static void DrawSecondaryBarBottomItem(Canvas canvas, DrawModel drawModel, BarModel barModel, BarModel main, SettingModel settingModel, double p1, double p2, double coverTop, double coverBottom, double coverSide, SolidColorBrush solidColorBrush)
        {
            double left = (p1 + ((p1 < 0) ? (1) : (-1)) * (coverSide + barModel.Bar.Diameter * 0.5)) / drawModel.Scale;
            double right = (p2 + ((p2 < 0) ? (1) : (-1)) * (coverSide + barModel.Bar.Diameter * 0.5)) / drawModel.Scale;
            double top = 820 - drawModel.Side - (coverBottom + barModel.Bar.Diameter * 0.5) / drawModel.Scale - barModel.HookLength / drawModel.Scale - main.Bar.Diameter / drawModel.Scale;
            double bot = 820 - drawModel.Side - (coverBottom + barModel.Bar.Diameter * 0.5) / drawModel.Scale - main.Bar.Diameter / drawModel.Scale;
            Line l1 = new Line() { X1 = drawModel.Left + left, X2 = drawModel.Left + left, Y1 = top, Y2 = bot };
            l1.Stroke = solidColorBrush;
            l1.StrokeThickness = barModel.Bar.Diameter / drawModel.Scale;
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = drawModel.Left + left, X2 = drawModel.Left + right, Y1 = bot, Y2 = bot };
            l2.Stroke = solidColorBrush;
            l2.StrokeThickness = barModel.Bar.Diameter / drawModel.Scale;
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = drawModel.Left + right, X2 = drawModel.Left + right, Y1 = bot, Y2 = top };
            l3.Stroke = solidColorBrush;
            l3.StrokeThickness = barModel.Bar.Diameter / drawModel.Scale;
            canvas.Children.Add(l3);
        }
        private static void DrawSecondaryBarBottomSectionItem(Canvas canvas, DrawModel drawModel, BarModel barModel, BarModel main, SettingModel settingModel, double p1, double p2, double coverTop, double coverBottom, double coverSide, SolidColorBrush solidColorBrush)
        {
            int n = (int)((Math.Abs(p1 - p2) - 2 * coverSide - barModel.Bar.Diameter) / barModel.Distance) + 1;
            double left = (p1 + ((p1 < 0) ? (1) : (-1)) * (coverSide + barModel.Bar.Diameter * 0.5)) / drawModel.Scale;
            double right = (p2 + ((p2 < 0) ? (1) : (-1)) * (coverSide + barModel.Bar.Diameter * 0.5)) / drawModel.Scale;
            double top = 410 - drawModel.Side - (coverBottom + barModel.Bar.Diameter * 0.5) / drawModel.Scale - barModel.HookLength / drawModel.Scale - main.Bar.Diameter / drawModel.Scale;
            double bot = 410 - drawModel.Side - (coverBottom + barModel.Bar.Diameter * 0.5) / drawModel.Scale - main.Bar.Diameter / drawModel.Scale;
            for (int i = 0; i < n; i++)
            {
                DrawImage.DrawOneBarSection(canvas, drawModel.Left + left + i * ((p1 < p2) ? (1) : (-1)) * (barModel.Distance / drawModel.Scale), bot, drawModel.Scale, barModel.Bar.Diameter, solidColorBrush);
            }
        }
        private static void DrawMainBarTopItem(Canvas canvas, DrawModel drawModel, BarModel barModel,BarModel mainBottom, BarModel side, SettingModel settingModel, double p1, double p2, double coverTop, double coverBottom, double coverSide, SolidColorBrush solidColorBrush)
        {
            double left = (p1 + ((p1 < 0) ? (1) : (-1)) * (coverSide + barModel.Bar.Diameter * 0.5+mainBottom.Bar.Diameter+side.Bar.Diameter)) / drawModel.Scale;
            double right = (p2 + ((p2 < 0) ? (1) : (-1)) * (coverSide + barModel.Bar.Diameter * 0.5 + mainBottom.Bar.Diameter + side.Bar.Diameter)) / drawModel.Scale;
            double top = 410 - drawModel.Side - settingModel.HeightFoundation / drawModel.Scale - (-coverTop - barModel.Bar.Diameter * 0.5) / drawModel.Scale ;
            double bot = top + barModel.HookLength / drawModel.Scale;
            Line l1 = new Line() { X1 = drawModel.Left + left, X2 = drawModel.Left + left, Y1 = bot, Y2 = top };
            l1.Stroke = solidColorBrush;
            l1.StrokeThickness = barModel.Bar.Diameter / drawModel.Scale;
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = drawModel.Left + left, X2 = drawModel.Left + right, Y1 = top, Y2 = top };
            l2.Stroke = solidColorBrush;
            l2.StrokeThickness = barModel.Bar.Diameter / drawModel.Scale;
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = drawModel.Left + right, X2 = drawModel.Left + right, Y1 = top, Y2 = bot };
            l3.Stroke = solidColorBrush;
            l3.StrokeThickness = barModel.Bar.Diameter / drawModel.Scale;
            canvas.Children.Add(l3);
        }
        private static void DrawMainBarTopSectionItem(Canvas canvas, DrawModel drawModel, BarModel barModel, BarModel mainBottom, BarModel side, SettingModel settingModel, double p1, double p2, double coverTop, double coverBottom, double coverSide, SolidColorBrush solidColorBrush)
        {
            int n = (int)((Math.Abs(p1 - p2) - 2 * coverSide - barModel.Bar.Diameter- 2*mainBottom.Bar.Diameter -2* side.Bar.Diameter) / barModel.Distance) + 1;
            double left = (p1 + ((p1 < 0) ? (1) : (-1)) * (coverSide + barModel.Bar.Diameter * 0.5 + mainBottom.Bar.Diameter + side.Bar.Diameter)) / drawModel.Scale;
            double right = (p2 + ((p2 < 0) ? (1) : (-1)) * (coverSide + barModel.Bar.Diameter * 0.5 + mainBottom.Bar.Diameter + side.Bar.Diameter)) / drawModel.Scale;
            double top = 820 - drawModel.Side - settingModel.HeightFoundation / drawModel.Scale - (-coverTop - barModel.Bar.Diameter * 0.5) / drawModel.Scale;
            double bot = top + barModel.HookLength / drawModel.Scale;
            for (int i = 0; i < n; i++)
            {
                DrawImage.DrawOneBarSection(canvas, drawModel.Left + left + i * ((p1 < p2) ? (1) : (-1)) * (barModel.Distance / drawModel.Scale), top, drawModel.Scale, barModel.Bar.Diameter, solidColorBrush);
            }
        }
        private static void DrawSecondaryBarTopItem(Canvas canvas, DrawModel drawModel, BarModel barModel, BarModel secondaryBottom, BarModel mainTop, BarModel side, SettingModel settingModel, double p1, double p2, double coverTop, double coverBottom, double coverSide, SolidColorBrush solidColorBrush)
        {
            double left = (p1 + ((p1 < 0) ? (1) : (-1)) * (coverSide + barModel.Bar.Diameter * 0.5 + secondaryBottom.Bar.Diameter + side.Bar.Diameter)) / drawModel.Scale;
            double right = (p2 + ((p2 < 0) ? (1) : (-1)) * (coverSide + barModel.Bar.Diameter * 0.5 + secondaryBottom.Bar.Diameter + side.Bar.Diameter)) / drawModel.Scale;
            double top = 820 - drawModel.Side - settingModel.HeightFoundation / drawModel.Scale - (-coverTop - barModel.Bar.Diameter * 0.5- mainTop.Bar.Diameter) / drawModel.Scale;
            double bot = top + barModel.HookLength / drawModel.Scale;
            Line l1 = new Line() { X1 = drawModel.Left + left, X2 = drawModel.Left + left, Y1 = bot, Y2 = top };
            l1.Stroke = solidColorBrush;
            l1.StrokeThickness = barModel.Bar.Diameter / drawModel.Scale;
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = drawModel.Left + left, X2 = drawModel.Left + right, Y1 = top, Y2 = top };
            l2.Stroke = solidColorBrush;
            l2.StrokeThickness = barModel.Bar.Diameter / drawModel.Scale;
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = drawModel.Left + right, X2 = drawModel.Left + right, Y1 = top, Y2 = bot };
            l3.Stroke = solidColorBrush;
            l3.StrokeThickness = barModel.Bar.Diameter / drawModel.Scale;
            canvas.Children.Add(l3);
        }
        private static void DrawSecondaryBarTopSectionItem(Canvas canvas, DrawModel drawModel, BarModel barModel, BarModel secondaryBottom, BarModel mainTop, BarModel side, SettingModel settingModel, double p1, double p2, double coverTop, double coverBottom, double coverSide, SolidColorBrush solidColorBrush)
        {
            int n = (int)((Math.Abs(p1 - p2) - 2 * coverSide - barModel.Bar.Diameter - 2 * secondaryBottom.Bar.Diameter - 2 * side.Bar.Diameter) / barModel.Distance) + 1;
            double left = (p1 + ((p1 < 0) ? (1) : (-1)) * (coverSide + barModel.Bar.Diameter * 0.5 + secondaryBottom.Bar.Diameter + side.Bar.Diameter)) / drawModel.Scale;
            double right = (p2 + ((p2 < 0) ? (1) : (-1)) * (coverSide + barModel.Bar.Diameter * 0.5 + secondaryBottom.Bar.Diameter + side.Bar.Diameter)) / drawModel.Scale;
            double top = 820 - drawModel.Side - settingModel.HeightFoundation / drawModel.Scale - (-coverTop - barModel.Bar.Diameter * 0.5 - mainTop.Bar.Diameter) / drawModel.Scale;
            double bot = top + barModel.HookLength / drawModel.Scale;
            for (int i = 0; i < n; i++)
            {
                DrawImage.DrawOneBarSection(canvas, drawModel.Left + left + i * ((p1 < p2) ? (1) : (-1)) * (barModel.Distance / drawModel.Scale), top, drawModel.Scale, barModel.Bar.Diameter, solidColorBrush);
            }
        }
        private static void DrawMainBarSideSectionItem(Canvas canvas, DrawModel drawModel, BarModel barModel, BarModel secondaryBottom, BarModel mainTop, BarModel side, SettingModel settingModel, double p1, double p2, double coverTop, double coverBottom, double coverSide, SolidColorBrush solidColorBrush)
        {
            int n = (int)((Math.Abs(p1 - p2) - 2 * coverSide - barModel.Bar.Diameter - 2 * secondaryBottom.Bar.Diameter - 2 * side.Bar.Diameter) / barModel.Distance) + 1;
            double left = (p1 + ((p1 < 0) ? (1) : (-1)) * (coverSide + barModel.Bar.Diameter * 0.5 + secondaryBottom.Bar.Diameter + side.Bar.Diameter)) / drawModel.Scale;
            double right = (p2 + ((p2 < 0) ? (1) : (-1)) * (coverSide + barModel.Bar.Diameter * 0.5 + secondaryBottom.Bar.Diameter + side.Bar.Diameter)) / drawModel.Scale;
            double top = 820 - drawModel.Side - settingModel.HeightFoundation / drawModel.Scale - (-coverTop - barModel.Bar.Diameter * 0.5 - mainTop.Bar.Diameter) / drawModel.Scale;
            double bot = top + barModel.HookLength / drawModel.Scale;
            for (int i = 0; i < n; i++)
            {
                DrawImage.DrawOneBarSection(canvas, drawModel.Left + left + i * ((p1 < p2) ? (1) : (-1)) * (barModel.Distance / drawModel.Scale), top, drawModel.Scale, barModel.Bar.Diameter, solidColorBrush);
            }
        }
        private static void DrawSecondaryBarSideSectionItem(Canvas canvas, DrawModel drawModel, BarModel barModel, BarModel secondaryBottom, BarModel mainTop, BarModel side, SettingModel settingModel, double p1, double p2, double coverTop, double coverBottom, double coverSide, SolidColorBrush solidColorBrush)
        {
            int n = (int)((Math.Abs(p1 - p2) - 2 * coverSide - barModel.Bar.Diameter - 2 * secondaryBottom.Bar.Diameter - 2 * side.Bar.Diameter) / barModel.Distance) + 1;
            double left = (p1 + ((p1 < 0) ? (1) : (-1)) * (coverSide + barModel.Bar.Diameter * 0.5 + secondaryBottom.Bar.Diameter + side.Bar.Diameter)) / drawModel.Scale;
            double right = (p2 + ((p2 < 0) ? (1) : (-1)) * (coverSide + barModel.Bar.Diameter * 0.5 + secondaryBottom.Bar.Diameter + side.Bar.Diameter)) / drawModel.Scale;
            double top = 820 - drawModel.Side - settingModel.HeightFoundation / drawModel.Scale - (-coverTop - barModel.Bar.Diameter * 0.5 - mainTop.Bar.Diameter) / drawModel.Scale;
            double bot = top + barModel.HookLength / drawModel.Scale;
            for (int i = 0; i < n; i++)
            {
                DrawImage.DrawOneBarSection(canvas, drawModel.Left + left + i * ((p1 < p2) ? (1) : (-1)) * (barModel.Distance / drawModel.Scale), top, drawModel.Scale, barModel.Bar.Diameter, solidColorBrush);
            }
        }
        private static void DrawBarSectionItem(Canvas canvas, DrawModel drawModel, ObservableCollection<BarModel> BarModels, BarModel barModel, SettingModel settingModel, int image, string orientation, double coverTop, double coverBottom, double coverSide, double p1, double p2, double p3, double p4,SolidColorBrush solidColorBrush)
        {
            BarModel mainBottom = BarModels.Where(x => x.Name.Equals("MainBottom")).FirstOrDefault();
            BarModel secondaryBottom = BarModels.Where(x => x.Name.Equals("SecondaryBottom")).FirstOrDefault();
            BarModel side = BarModels.Where(x => x.Name.Equals("Side")).FirstOrDefault();
            BarModel mainTop = BarModels.Where(x => x.Name.Equals("MainTop")).FirstOrDefault();
            switch (barModel.Name)
            {
                case "MainBottom":
                    DrawMainBarBottomItem(canvas, drawModel, barModel, settingModel, p1, p2, coverTop, coverBottom, coverSide, solidColorBrush);
                    DrawMainBarBottomSectionItem(canvas, drawModel, barModel, settingModel, p3, p4, coverTop, coverBottom, coverSide, solidColorBrush); break;
                case "MainTop":
                    if (mainBottom != null&& side!=null)
                    {
                        DrawMainBarTopItem(canvas, drawModel, barModel, mainBottom, side, settingModel, p1, p2, coverTop, coverBottom, coverSide, solidColorBrush);
                        DrawMainBarTopSectionItem(canvas, drawModel, barModel, mainBottom, side, settingModel, p3, p4, coverTop, coverBottom, coverSide, solidColorBrush);
                    }
                    break;
                case "MainAddHorizontal": break;
                case "MainAddVertical": break;
                case "SecondaryBottom":
                    if (mainBottom != null)
                    {
                        DrawSecondaryBarBottomItem(canvas, drawModel, barModel, mainBottom, settingModel, p3, p4, coverTop, coverBottom, coverSide, solidColorBrush);
                        DrawSecondaryBarBottomSectionItem(canvas, drawModel, barModel, mainBottom, settingModel, p1, p2, coverTop, coverBottom, coverSide, solidColorBrush);
                    }
                    break;
                case "SecondaryTop":
                    if (secondaryBottom != null && side != null&&mainTop!=null)
                    {
                        DrawSecondaryBarTopItem(canvas, drawModel, barModel, secondaryBottom, mainTop, side, settingModel, p3, p4, coverTop, coverBottom, coverSide, solidColorBrush);
                        DrawSecondaryBarTopSectionItem(canvas, drawModel, barModel, secondaryBottom, mainTop, side, settingModel, p3, p4, coverTop, coverBottom, coverSide, solidColorBrush);
                    }
                    break;
                case "SecondaryAddHorizontal": break;
                case "SecondaryAddVertical": break;
                case "Side": break;
                default: break;
            }
        }
        public static void DrawBarSection(Canvas canvas, DrawModel drawModel, FoundationModel foundationModel, ObservableCollection<BarModel> BarModels, BarModel barModel, SettingModel settingModel, int image, string orientation, double coverTop, double coverBottom, double coverSide, double p1, double p2, double p3, double p4)
        {
            DrawImage.DrawAxisAndLevel(canvas, drawModel.Side);
            List<double> pMain;
            List<double> pSecon;

            DrawMainSpanBounding(canvas, drawModel, foundationModel, settingModel, image, orientation, p1, p2, out pMain);
            for (int i = 0; i < pMain.Count; i++)
            {
                DrawPileSectionItem(canvas, drawModel, settingModel, settingModel.DiameterPile, pMain[i], true);
            }
            DrawSecondarySpanBounding(canvas, drawModel, foundationModel, settingModel, image, orientation, p3, p4, out pSecon);
            for (int i = 0; i < pSecon.Count; i++)
            {
                DrawPileSectionItem(canvas, drawModel, settingModel, settingModel.DiameterPile, pSecon[i], false);
            }
            for (int i = 0; i < BarModels.Count; i++)
            {
                SolidColorBrush solidColorBrush = (BarModels[i].Name.Equals(barModel.Name)) ? (drawModel.ColorMainBarChoose) : (drawModel.ColorMainBar);
                if (BarModels[i].IsModel)
                {
                    DrawBarSectionItem(canvas, drawModel, BarModels, BarModels[i], settingModel, image, orientation, coverTop, coverBottom, coverSide, p1, p2, p3, p4, solidColorBrush);
                }
            }
        }
        #endregion
    }
}
