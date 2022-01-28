using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using static R01_ColumnsRebar.ErrorColumns;

namespace R01_ColumnsRebar
{
    public class DrawMainCanvas
    {
        public static void DrawSectionAndStirrups(Canvas canvas, InfoModel infoModel, StirrupModel stirrupModel, DrawModel drawModelSection, SectionStyle sectionStyle, double Cover, double d, SolidColorBrush solidColorBrush)
        {
            canvas.Children.Clear();

            if (sectionStyle == SectionStyle.RECTANGLE)
            {
                double left = drawModelSection.Left + infoModel.WestPosition / (drawModelSection.Scale);
                double top = drawModelSection.Top - infoModel.NouthPosition / (drawModelSection.Scale);
                DrawImage.DrawAxis(canvas, false);
                DrawImage.DrawSection(canvas, drawModelSection.Scale, left, top, infoModel.b, infoModel.h);
                DrawImage.DrawStirrup(canvas, left, top, drawModelSection.Scale, infoModel.b, infoModel.h, Cover, stirrupModel.BarS.Diameter, d, solidColorBrush);
                DrawImage.DimVertical(canvas, drawModelSection.Left, top, drawModelSection.Scale, infoModel.h, 11, 20, 5);
                if(!PointModel.AreEqual(infoModel.SouthPosition,0)) DrawImage.DimVertical(canvas, drawModelSection.Left, drawModelSection.Top - infoModel.SouthPosition / (drawModelSection.Scale), drawModelSection.Scale, infoModel.SouthPosition, 11, 20, 5);
                DrawImage.DimHorizontal(canvas, left, drawModelSection.Top, drawModelSection.Scale, infoModel.b, 11, -20, -5);
                if (!PointModel.AreEqual(infoModel.WestPosition, 0)) DrawImage.DimHorizontal(canvas, drawModelSection.Left, drawModelSection.Top, drawModelSection.Scale, infoModel.WestPosition, 11, -20, -5);
            }
            else
            {
                double left = drawModelSection.Left + infoModel.PointXPosition / (drawModelSection.Scale);
                double top = drawModelSection.Top - infoModel.PointYPosition / (drawModelSection.Scale);
                DrawImage.DrawAxis(canvas, true);
                DrawImage.DrawCylindricalSection(canvas, left, top, drawModelSection.Scale, infoModel.D);
                DrawImage.DrawCylindricalStirrup(canvas, left, top, drawModelSection.Scale, Cover, infoModel.D, stirrupModel.BarS.Diameter, solidColorBrush);
                DrawImage.DimCylindical(canvas, left, top, drawModelSection.Scale, 40, 5, infoModel.D);
            }

        }
        public static void DrawSectionAndStirrupDowelsTop(Canvas canvas, InfoModel infoModel, StirrupModel stirrupModel, DrawModel drawModelDowels, SectionStyle sectionStyle, double Cover, double d, SolidColorBrush solidColorBrush,InfoModel infoModelUp=null, StirrupModel stirrupModelUp=null)
        {
            canvas.Children.Clear();

            if (sectionStyle == SectionStyle.RECTANGLE)
            {
                double left = drawModelDowels.Left + infoModel.WestPosition / (drawModelDowels.Scale);
                double top = drawModelDowels.Top - infoModel.NouthPosition / (drawModelDowels.Scale);
                DrawImage.DrawAxisDowels(canvas, false);
                DrawImage.DrawSection(canvas, drawModelDowels.Scale, left, top, infoModel.b, infoModel.h);
                DrawImage.DrawStirrup(canvas, left, top, drawModelDowels.Scale, infoModel.b, infoModel.h, Cover, stirrupModel.BarS.Diameter, d, solidColorBrush);
                if (infoModelUp != null)
                {
                    double leftUp = drawModelDowels.Left + infoModelUp.WestPosition / (drawModelDowels.Scale);
                    double topUp = drawModelDowels.Top - infoModelUp.NouthPosition / (drawModelDowels.Scale);
                    DrawImage.DrawSectionDashArray(canvas, drawModelDowels.Scale, leftUp, topUp, infoModelUp.b, infoModelUp.h);
                    DrawImage.DrawStirrupDashArray(canvas, leftUp, topUp, drawModelDowels.Scale, infoModelUp.b, infoModelUp.h, Cover, stirrupModelUp.BarS.Diameter, d, solidColorBrush);
                }
            }
            else
            {
                double left = drawModelDowels.Left + infoModel.PointXPosition / (drawModelDowels.Scale);
                double top = drawModelDowels.Top - infoModel.PointYPosition / (drawModelDowels.Scale);
                DrawImage.DrawAxisDowels(canvas, true);
                DrawImage.DrawCylindricalSection(canvas, left, top, drawModelDowels.Scale, infoModel.D);
                DrawImage.DrawCylindricalStirrup(canvas, left, top, drawModelDowels.Scale, Cover, infoModel.D, stirrupModel.BarS.Diameter, solidColorBrush);
                if (infoModelUp != null)
                {
                    double leftUp = drawModelDowels.Left + infoModelUp.PointXPosition / (drawModelDowels.Scale);
                    double topUp = drawModelDowels.Top - infoModelUp.PointYPosition / (drawModelDowels.Scale);
                    DrawImage.DrawCylindricalSectionDashArray(canvas, leftUp, topUp, drawModelDowels.Scale, infoModelUp.D);
                    DrawImage.DrawCylindricalStirrupDashArray(canvas, leftUp, topUp, drawModelDowels.Scale, Cover, infoModelUp.D, stirrupModelUp.BarS.Diameter, solidColorBrush);
                }
            }
            
        }
        public static void DrawSectionAndStirrupDowelsBottom(Canvas canvas, InfoModel infoModel, StirrupModel stirrupModel, DrawModel drawModelDowels, SectionStyle sectionStyle, double Cover, double d, SolidColorBrush solidColorBrush, InfoModel infoModelDown = null, StirrupModel stirrupModelDown = null)
        {
            canvas.Children.Clear();

            if (sectionStyle == SectionStyle.RECTANGLE)
            {
                double left = drawModelDowels.Left + infoModel.WestPosition / (drawModelDowels.Scale);
                double top = drawModelDowels.Top - infoModel.NouthPosition / (drawModelDowels.Scale);
                DrawImage.DrawAxisDowels(canvas, false);
                DrawImage.DrawSection(canvas, drawModelDowels.Scale, left, top, infoModel.b, infoModel.h);
                DrawImage.DrawStirrup(canvas, left, top, drawModelDowels.Scale, infoModel.b, infoModel.h, Cover, stirrupModel.BarS.Diameter, d, solidColorBrush);
                if (infoModelDown != null)
                {
                    double leftUp = drawModelDowels.Left + infoModelDown.WestPosition / (drawModelDowels.Scale);
                    double topUp = drawModelDowels.Top - infoModelDown.NouthPosition / (drawModelDowels.Scale);
                    DrawImage.DrawSectionDashArray(canvas, drawModelDowels.Scale, leftUp, topUp, infoModelDown.b, infoModelDown.h);
                    DrawImage.DrawStirrupDashArray(canvas, leftUp, topUp, drawModelDowels.Scale, infoModelDown.b, infoModelDown.h, Cover, stirrupModelDown.BarS.Diameter, d, solidColorBrush);
                }
            }
            else
            {
                double left = drawModelDowels.Left + infoModel.PointXPosition / (drawModelDowels.Scale);
                double top = drawModelDowels.Top - infoModel.PointYPosition / (drawModelDowels.Scale);
                DrawImage.DrawAxisDowels(canvas, true);
                DrawImage.DrawCylindricalSection(canvas, left, top, drawModelDowels.Scale, infoModel.D);
                DrawImage.DrawCylindricalStirrup(canvas, left, top, drawModelDowels.Scale, Cover, infoModel.D, stirrupModel.BarS.Diameter, solidColorBrush);
                if (infoModelDown != null)
                {
                    double leftUp = drawModelDowels.Left + infoModelDown.PointXPosition / (drawModelDowels.Scale);
                    double topUp = drawModelDowels.Top - infoModelDown.PointYPosition / (drawModelDowels.Scale);
                    DrawImage.DrawCylindricalSectionDashArray(canvas, leftUp, topUp, drawModelDowels.Scale, infoModelDown.D);
                    DrawImage.DrawCylindricalStirrupDashArray(canvas, leftUp, topUp, drawModelDowels.Scale, Cover, infoModelDown.D, stirrupModelDown.BarS.Diameter, solidColorBrush);
                }
            }

        }
        #region main
        public static void DrawInfoColumns(Canvas canvas, ColumnsModel columnsModel, int choose)
        {
            double maxWidth = (columnsModel.SectionStyle == SectionStyle.RECTANGLE) ? columnsModel.DrawModel.GetBmax(columnsModel.InfoModels) : columnsModel.DrawModel.GetDmax(columnsModel.InfoModels);
            double p30 = columnsModel.InfoModels[0].BottomPosition;
            for (int i = 0; i < columnsModel.InfoModels.Count; i++)
            {
                DrawColumnItem(canvas, columnsModel.DrawModel, columnsModel.InfoModels[i], columnsModel.SectionStyle, maxWidth, p30, (choose == i));
            }
            DrawFoundationBottom(canvas, columnsModel.DrawModel.Left, columnsModel.DrawModel.Top, columnsModel.DrawModel.Scale, p30, columnsModel.DrawModel.StrokeBound, 50, columnsModel.DrawModel.ColorBound);
        }
        public static void DrawStirrup(Canvas canvas, ColumnsModel columnsModel, int choose)
        {
            double maxWidth = (columnsModel.SectionStyle == SectionStyle.RECTANGLE) ? columnsModel.DrawModel.GetBmax(columnsModel.InfoModels) : columnsModel.DrawModel.GetDmax(columnsModel.InfoModels);
            for (int i = 0; i < columnsModel.StirrupModels.Count; i++)
            {
                DrawStirrupItem(canvas, columnsModel.DrawModel, columnsModel.InfoModels[i], columnsModel.StirrupModels[i], columnsModel.SectionStyle, maxWidth, columnsModel.Cover, (i == choose));
            }
        }
        #endregion
        #region InfoColumn
        private static void DrawColumnItem(Canvas canvas, DrawModel drawModel, InfoModel infoModel, SectionStyle sectionStyle, double maxWidth, double p30, bool choose)
        {
            if (sectionStyle == SectionStyle.RECTANGLE)
            {
                DrawColumnItemRectangle(canvas, drawModel, infoModel, maxWidth, p30, choose);
            }
            else
            {
                DrawColumnItemCylindrical(canvas, drawModel, infoModel, maxWidth, p30, choose);
            }
        }
        private static void DrawColumnItemRectangle(Canvas canvas, DrawModel drawModel, InfoModel infoModel, double maxWidth, double p30, bool choose)
        {
            DrawColumnItemRectangle(canvas, drawModel.Left, drawModel.Top, drawModel.Scale, infoModel.WestPosition, infoModel.EastPosition, infoModel.BottomPosition, infoModel.TopPosition, drawModel.StrokeBound, drawModel.ColorBound, drawModel.ColorFill, choose);
            DrawColumnItemRectangle(canvas, 2.5 * drawModel.Left + maxWidth / drawModel.Scale, drawModel.Top, drawModel.Scale, infoModel.SouthPosition, infoModel.NouthPosition, infoModel.BottomPosition, infoModel.TopPosition, drawModel.StrokeBound, drawModel.ColorBound, drawModel.ColorFill, choose);
            DrawColumnItemLevel(canvas, drawModel, infoModel);
            DrawColumnItemBeamTopLevelRectangle(canvas, drawModel.Left, drawModel.Top, drawModel.Scale, infoModel.WestPosition, infoModel.EastPosition, infoModel.BottomPosition, infoModel.TopPosition, drawModel.StrokeBound, 20, infoModel.hb, infoModel.zb, drawModel.ColorBound);
            DrawColumnItemBeamTopLevelRectangle(canvas, 2.5 * drawModel.Left + maxWidth / drawModel.Scale, drawModel.Top, drawModel.Scale, infoModel.SouthPosition, infoModel.NouthPosition, infoModel.BottomPosition, infoModel.TopPosition, drawModel.StrokeBound, 20, infoModel.hb, infoModel.zb, drawModel.ColorBound);
            
            DrawImage.DimVertical(canvas, drawModel.Left - 40, drawModel.Top - infoModel.TopPosition / drawModel.Scale, drawModel.Scale, (infoModel.TopPosition - infoModel.BottomPosition), 11, 40, 5);
            DrawColumnItemDimBeamTopLevel(canvas, drawModel.Left, drawModel.Top, drawModel.Scale, infoModel.TopPosition, 40, infoModel.hb, infoModel.zb);
        }
        private static void DrawColumnItemCylindrical(Canvas canvas, DrawModel drawModel, InfoModel infoModel, double maxWidth, double p30, bool choose)
        {
            DrawColumnItemCylindrical(canvas, drawModel.Left + maxWidth * 0.5 / drawModel.Scale, drawModel.Top, drawModel.Scale, infoModel.PointXPosition, infoModel.BottomPosition, infoModel.TopPosition, infoModel.D, drawModel.StrokeBound, drawModel.ColorBound, drawModel.ColorFill, choose);
            DrawColumnItemCylindrical(canvas, 2.5 * drawModel.Left + maxWidth / drawModel.Scale + maxWidth * 0.5 / drawModel.Scale, drawModel.Top, drawModel.Scale, infoModel.PointYPosition, infoModel.BottomPosition, infoModel.TopPosition, infoModel.D, drawModel.StrokeBound, drawModel.ColorBound, drawModel.ColorFill, choose);
            DrawColumnItemLevel(canvas, drawModel, infoModel);
            DrawColumnItemBeamTopLevelCylindrical(canvas, drawModel.Left + maxWidth * 0.5 / drawModel.Scale, drawModel.Top, drawModel.Scale, infoModel.PointXPosition, infoModel.D, infoModel.BottomPosition, infoModel.TopPosition, drawModel.StrokeBound, 50, infoModel.hb, infoModel.zb, drawModel.ColorBound);
            DrawColumnItemBeamTopLevelCylindrical(canvas, 2.5 * drawModel.Left + maxWidth / drawModel.Scale + maxWidth * 0.5 / drawModel.Scale, drawModel.Top, drawModel.Scale, infoModel.PointYPosition, infoModel.D, infoModel.BottomPosition, infoModel.TopPosition, drawModel.StrokeBound, 50, infoModel.hb, infoModel.zb, drawModel.ColorBound);
            
            DrawImage.DimVertical(canvas, drawModel.Left - 40, drawModel.Top - infoModel.TopPosition / drawModel.Scale, drawModel.Scale, (infoModel.TopPosition - infoModel.BottomPosition), 11, 40, 5);
            DrawColumnItemDimBeamTopLevel(canvas, drawModel.Left, drawModel.Top, drawModel.Scale, infoModel.TopPosition, 40, infoModel.hb, infoModel.zb);
        }
        private static void DrawColumnItemRectangle(Canvas canvas, double left, double top, double scale, double p1, double p2, double p3, double p4, double stroke, SolidColorBrush solidColorBrush, SolidColorBrush chooseColor, bool choose)
        {
            Line l1 = new Line() { X1 = left + p1 / scale, X2 = left + p1 / scale, Y1 = top - p3 / scale, Y2 = top - p4 / scale };
            l1.Stroke = solidColorBrush;
            l1.StrokeThickness = stroke;
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = left + p2 / scale, X2 = left + p2 / scale, Y1 = top - p3 / scale, Y2 = top - p4 / scale };
            l2.Stroke = solidColorBrush;
            l2.StrokeThickness = stroke;
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = left + p1 / scale, X2 = left + p2 / scale, Y1 = top - p3 / scale, Y2 = top - p3 / scale };
            l3.Stroke = solidColorBrush;
            l3.StrokeThickness = stroke;
            canvas.Children.Add(l3);
            Line l4 = new Line() { X1 = left + p1 / scale, X2 = left + p2 / scale, Y1 = top - p4 / scale, Y2 = top - p4 / scale };
            l4.Stroke = solidColorBrush;
            l4.StrokeThickness = stroke;
            canvas.Children.Add(l4);
            if (choose)
            {
                Rectangle r = new Rectangle() { Width = (p2 - p1) / scale, Height = (p4 - p3) / scale };
                r.Fill = chooseColor;
                Canvas.SetTop(r, top - p4 / scale);
                Canvas.SetLeft(r, left + p1 / scale);
                canvas.Children.Add(r);
            }
            DrawImage.DimHorizontal(canvas, left + p1 / scale, top - (p4 + p3) * 0.5 / scale, scale, Math.Abs(p2 - p1), 11, 20, 5);
        }
        private static void DrawColumnItemCylindrical(Canvas canvas, double left, double top, double scale, double p, double p3, double p4, double D, double stroke, SolidColorBrush solidColorBrush, SolidColorBrush chooseColor, bool choose)
        {
            Line l1 = new Line() { X1 = left + (p - D / 2) / scale, X2 = left + (p - D / 2) / scale, Y1 = top - p3 / scale, Y2 = top - p4 / scale };
            l1.Stroke = solidColorBrush;
            l1.StrokeThickness = stroke;
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = left + (p + D / 2) / scale, X2 = left + (p + D / 2) / scale, Y1 = top - p3 / scale, Y2 = top - p4 / scale };
            l2.Stroke = solidColorBrush;
            l2.StrokeThickness = stroke;
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = left + (p - D / 2) / scale, X2 = left + (p + D / 2) / scale, Y1 = top - p3 / scale, Y2 = top - p3 / scale };
            l3.Stroke = solidColorBrush;
            l3.StrokeThickness = stroke;
            canvas.Children.Add(l3);
            Line l4 = new Line() { X1 = left + (p - D / 2) / scale, X2 = left + (p + D / 2) / scale, Y1 = top - p4 / scale, Y2 = top - p4 / scale };
            l4.Stroke = solidColorBrush;
            l4.StrokeThickness = stroke;
            canvas.Children.Add(l4);
            if (choose)
            {
                Rectangle r = new Rectangle() { Width = (D) / scale, Height = (p4 - p3) / scale };
                r.Fill = chooseColor;
                Canvas.SetTop(r, top - p4 / scale);
                Canvas.SetLeft(r, left + (p - D / 2) / scale);
                canvas.Children.Add(r);
            }
            DrawImage.DimHorizontal(canvas, left + (p - D / 2) / scale, top - (p4 + p3) * 0.5 / scale, scale, D, 11, 20, 5);
        }
        public static void DrawColumnItemLevel(Canvas canvas, DrawModel drawModel, InfoModel infoModel)
        {
            Line l1 = new Line()
            {
                X1 = 0,
                X2 = 600,
                Y1 = drawModel.Top - (infoModel.BottomPosition - infoModel.eB) / drawModel.Scale,
                Y2 = drawModel.Top - (infoModel.BottomPosition - infoModel.eB) / drawModel.Scale
            };
            l1.Stroke = drawModel.ColorBound;
            l1.StrokeThickness = drawModel.StrokeBound;
            l1.StrokeDashArray = new DoubleCollection() { 10, 5, 2 };
            canvas.Children.Add(l1);
            TextBlock text = new TextBlock();
            text.Text = infoModel.BottomLevel.Name;
            text.FontSize = 15;
            text.Foreground = Brushes.Black;
            text.FontFamily = new FontFamily("Tahoma");
            text.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text.Arrange(new Rect(text.DesiredSize));
            Canvas.SetTop(text, drawModel.Top - (infoModel.BottomPosition - infoModel.eB) / drawModel.Scale);
            Canvas.SetLeft(text, -10);
            canvas.Children.Add(text);
            Line l2 = new Line()
            {
                X1 = 0,
                X2 = 600,
                Y1 = drawModel.Top - (infoModel.TopPosition - infoModel.eT) / drawModel.Scale,
                Y2 = drawModel.Top - (infoModel.TopPosition - infoModel.eT) / drawModel.Scale
            };
            l2.Stroke = drawModel.ColorBound;
            l2.StrokeThickness = drawModel.StrokeBound;
            l2.StrokeDashArray = new DoubleCollection() { 10, 5, 2 };
            TextBlock text1 = new TextBlock();
            text1.Text = infoModel.TopLevel.Name;
            text1.FontSize = 15;
            text1.Foreground = Brushes.Black;
            text1.FontFamily = new FontFamily("Tahoma");
            text1.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text1.Arrange(new Rect(text.DesiredSize));
            Canvas.SetTop(text1, drawModel.Top - (infoModel.TopPosition - infoModel.eT) / drawModel.Scale);
            Canvas.SetLeft(text1,-10);
            canvas.Children.Add(text1);
            canvas.Children.Add(l2);
        }
        private static void DrawColumnItemBeamTopLevelRectangle(Canvas canvas, double left, double top, double scale, double p1, double p2, double p3, double p4, double stroke, double offset, double hb, double zb, SolidColorBrush solidColorBrush)
        {
            if (!PointModel.AreEqual(hb + zb, 0))
            {
                Line l1 = new Line() { X1 = left - offset, X2 = left + p1 / scale, Y1 = top - (p4 - zb) / scale, Y2 = top - (p4 - zb) / scale };
                l1.Stroke = solidColorBrush;
                l1.StrokeThickness = stroke;
                canvas.Children.Add(l1);
                Line l2 = new Line() { X1 = left - offset, X2 = left + p1 / scale, Y1 = top - (p4 - zb - hb) / scale, Y2 = top - (p4 - zb - hb) / scale };
                l2.Stroke = solidColorBrush;
                l2.StrokeThickness = stroke;
                canvas.Children.Add(l2);
                Line l3 = new Line() { X1 = left - offset, X2 = left - offset, Y1 = top - (p4 - zb) / scale, Y2 = top - (p4 - zb - hb) / scale };
                l3.Stroke = solidColorBrush;
                l3.StrokeThickness = stroke;
                l3.StrokeDashArray = new DoubleCollection() { 5, 2 };
                canvas.Children.Add(l3);
                Line l4 = new Line() { X1 = left + p2 / scale, X2 = left + p2 / scale + offset, Y1 = top - (p4 - zb) / scale, Y2 = top - (p4 - zb) / scale };
                l4.Stroke = solidColorBrush;
                l4.StrokeThickness = stroke;
                canvas.Children.Add(l4);
                Line l5 = new Line() { X1 = left + p2 / scale, X2 = left + p2 / scale + offset, Y1 = top - (p4 - zb - hb) / scale, Y2 = top - (p4 - zb - hb) / scale };
                l5.Stroke = solidColorBrush;
                l5.StrokeThickness = stroke;
                canvas.Children.Add(l5);
                Line l6 = new Line() { X1 = left + p2 / scale + offset, X2 = left + p2 / scale + offset, Y1 = top - (p4 - zb) / scale, Y2 = top - (p4 - zb - hb) / scale };
                l6.Stroke = solidColorBrush;
                l6.StrokeThickness = stroke;
                l6.StrokeDashArray = new DoubleCollection() { 5, 2 };
                canvas.Children.Add(l6);
            }

        }
        private static void DrawColumnItemDimBeamTopLevel(Canvas canvas, double left, double top, double scale, double p4, double offset, double hb, double zb)
        {
            if (!PointModel.AreEqual(hb + zb, 0))
            {
                DrawImage.DimVertical(canvas, left - offset, top - (p4 - zb) / scale, scale, hb, 11, 20, 5);
                if (!PointModel.AreEqual(zb, 0))
                {
                    DrawImage.DimVertical(canvas, left - offset, top - (p4) / scale, scale, zb, 11, 20, 5);
                }
            }

        }
        private static void DrawColumnItemBeamTopLevelCylindrical(Canvas canvas, double left, double top, double scale, double p, double D, double p3, double p4, double stroke, double offset, double hb, double zb, SolidColorBrush solidColorBrush)
        {
            if (!PointModel.AreEqual(hb + zb, 0))
            {
                Line l1 = new Line() { X1 = left - offset + (p - D / 2) / scale, X2 = left + (p - D / 2) / scale, Y1 = top - (p4 - zb) / scale, Y2 = top - (p4 - zb) / scale };
                l1.Stroke = solidColorBrush;
                l1.StrokeThickness = stroke;
                canvas.Children.Add(l1);
                Line l2 = new Line() { X1 = left - offset + (p - D / 2) / scale, X2 = left + (p - D / 2) / scale, Y1 = top - (p4 - zb - hb) / scale, Y2 = top - (p4 - zb - hb) / scale };
                l2.Stroke = solidColorBrush;
                l2.StrokeThickness = stroke;
                canvas.Children.Add(l2);
                Line l3 = new Line() { X1 = left - offset + (p - D / 2) / scale, X2 = left - offset + (p - D / 2) / scale, Y1 = top - (p4 - zb) / scale, Y2 = top - (p4 - zb - hb) / scale };
                l3.Stroke = solidColorBrush;
                l3.StrokeThickness = stroke;
                l3.StrokeDashArray = new DoubleCollection() { 5, 2 };
                canvas.Children.Add(l3);
                Line l4 = new Line() { X1 = left + (p + D / 2) / scale, X2 = left + (p + D / 2) / scale + offset, Y1 = top - (p4 - zb) / scale, Y2 = top - (p4 - zb) / scale };
                l4.Stroke = solidColorBrush;
                l4.StrokeThickness = stroke;
                canvas.Children.Add(l4);
                Line l5 = new Line() { X1 = left + (p + D / 2) / scale, X2 = left + (p + D / 2) / scale + offset, Y1 = top - (p4 - zb - hb) / scale, Y2 = top - (p4 - zb - hb) / scale };
                l5.Stroke = solidColorBrush;
                l5.StrokeThickness = stroke;
                canvas.Children.Add(l5);
                Line l6 = new Line() { X1 = left + (p + D / 2) / scale + offset, X2 = left + (p + D / 2) / scale + offset, Y1 = top - (p4 - zb) / scale, Y2 = top - (p4 - zb - hb) / scale };
                l6.Stroke = solidColorBrush;
                l6.StrokeThickness = stroke;
                l6.StrokeDashArray = new DoubleCollection() { 5, 2 };
                canvas.Children.Add(l6);
            }

        }
        public static void DrawFoundationBottom(Canvas canvas, double left, double top, double scale, double p30, double stroke, double offset, SolidColorBrush solidColorBrush)
        {
            if (!PointModel.AreEqual(p30, 0))
            {
                Line l1 = new Line() { X1 = left - offset, X2 = 600 - (left - offset), Y1 = top, Y2 = top };
                l1.Stroke = solidColorBrush;
                l1.StrokeThickness = stroke;
                canvas.Children.Add(l1);
                Line l2 = new Line() { X1 = left - offset, X2 = 600 - (left - offset), Y1 = top - p30 / scale, Y2 = top - p30 / scale };
                l2.Stroke = solidColorBrush;
                l2.StrokeThickness = stroke;
                canvas.Children.Add(l2);
                Line l3 = new Line() { X1 = left - offset, X2 = left - offset, Y1 = top, Y2 = top - p30 / scale };
                l3.Stroke = solidColorBrush;
                l3.StrokeThickness = stroke;
                l3.StrokeDashArray = new DoubleCollection() { 5, 2 };
                canvas.Children.Add(l3);
                Line l4 = new Line() { X1 = 600 - (left - offset), X2 = 600 - (left - offset), Y1 = top, Y2 = top - p30 / scale };
                l4.Stroke = solidColorBrush;
                l4.StrokeThickness = stroke;
                l4.StrokeDashArray = new DoubleCollection() { 5, 2 };
                canvas.Children.Add(l4);
                DrawImage.DimVertical(canvas, left - offset, top - (p30) / scale, scale, p30, 11, 30, 5);
            }
        }
        #endregion
        #region Stirrup
        private static void DrawStirrupItem(Canvas canvas, DrawModel drawModel, InfoModel infoModel, StirrupModel stirrupModel,SectionStyle sectionStyle, double maxWidth, double Cover, bool choose)
        {
            if (sectionStyle == SectionStyle.RECTANGLE)
            {
                if (stirrupModel.TypeDis == 0)
                {

                    DrawStirrupItemRectangle0(canvas, drawModel.Left, drawModel.Top, drawModel.Scale, infoModel.WestPosition, infoModel.EastPosition, infoModel.BottomPosition, Cover, drawModel.StrokeStirrup, stirrupModel, choose ? drawModel.ColorStirrupChoose : drawModel.ColorStirrup);
                    DrawStirrupItemRectangle0(canvas, 2.5 * drawModel.Left + maxWidth / drawModel.Scale, drawModel.Top, drawModel.Scale, infoModel.SouthPosition, infoModel.NouthPosition, infoModel.BottomPosition, Cover, drawModel.StrokeStirrup, stirrupModel, choose ? drawModel.ColorStirrupChoose : drawModel.ColorStirrup);
                }
                else
                {
                    DrawStirrupItemRectangle1(canvas, drawModel.Left, drawModel.Top, drawModel.Scale, infoModel.WestPosition, infoModel.EastPosition, infoModel.BottomPosition, Cover, drawModel.StrokeStirrup, stirrupModel, choose ? drawModel.ColorStirrupChoose : drawModel.ColorStirrup);
                    DrawStirrupItemRectangle1(canvas, 2.5 * drawModel.Left + maxWidth / drawModel.Scale, drawModel.Top, drawModel.Scale, infoModel.SouthPosition, infoModel.NouthPosition, infoModel.BottomPosition, Cover, drawModel.StrokeStirrup, stirrupModel, choose ? drawModel.ColorStirrupChoose : drawModel.ColorStirrup);
                }
            }
            else
            {
                if (stirrupModel.TypeDis == 0)
                {
                    
                    DrawStirrupItemCylindrical0(canvas, drawModel.Left + maxWidth * 0.5 / drawModel.Scale, drawModel.Top, drawModel.Scale, infoModel.PointXPosition, infoModel.D, infoModel.BottomPosition, Cover, drawModel.StrokeStirrup, stirrupModel, choose ? drawModel.ColorStirrupChoose : drawModel.ColorStirrup);
                    DrawStirrupItemCylindrical0(canvas, 2.5 * drawModel.Left + maxWidth / drawModel.Scale + maxWidth * 0.5 / drawModel.Scale, drawModel.Top, drawModel.Scale, infoModel.PointYPosition, infoModel.D, infoModel.BottomPosition, Cover, drawModel.StrokeStirrup, stirrupModel, choose ? drawModel.ColorStirrupChoose : drawModel.ColorStirrup);
                }
                else
                {
                    DrawStirrupItemCylindrical1(canvas, drawModel.Left + maxWidth * 0.5 / drawModel.Scale, drawModel.Top, drawModel.Scale, infoModel.PointXPosition, infoModel.D, infoModel.BottomPosition, Cover, drawModel.StrokeStirrup, stirrupModel, choose ? drawModel.ColorStirrupChoose : drawModel.ColorStirrup);
                    DrawStirrupItemCylindrical1(canvas, 2.5 * drawModel.Left + maxWidth / drawModel.Scale + maxWidth * 0.5 / drawModel.Scale , drawModel.Top, drawModel.Scale, infoModel.PointYPosition, infoModel.D, infoModel.BottomPosition, Cover, drawModel.StrokeStirrup, stirrupModel, choose ? drawModel.ColorStirrupChoose : drawModel.ColorStirrup);
                }
            }
            
        }

        private static void DrawStirrupItemRectangle0(Canvas canvas, double left, double top, double scale, double p1, double p2, double p3, double Cover, double stroke, StirrupModel stirrupModel, SolidColorBrush solidColorBrush)
        {
            if (stirrupModel.S != 0)
            {
                int n = (int)(stirrupModel.L / stirrupModel.S);
                double del = 0.5 * (stirrupModel.L - n * stirrupModel.S);
                
                for (int i = 0; i < n + 1; i++)
                {
                    Line l1 = new Line() { X1 = left + p1 / scale + Cover / scale, X2 = left + p2 / scale - Cover / scale, Y1 = top - p3 / scale - del / scale - i * stirrupModel.S / scale, Y2 = top - p3 / scale - del / scale - i * stirrupModel.S / scale };
                    l1.Stroke = solidColorBrush;
                    l1.StrokeThickness = stroke;
                    canvas.Children.Add(l1);
                }
            }
        }
        private static void DrawStirrupItemCylindrical0(Canvas canvas, double left, double top, double scale, double p,double D, double p3, double Cover, double stroke, StirrupModel stirrupModel, SolidColorBrush solidColorBrush)
        {
            if (stirrupModel.S != 0)
            {
                int n = (int)(stirrupModel.L / stirrupModel.S);
                double del = 0.5 * (stirrupModel.L - n * stirrupModel.S);
                //System.Windows.Forms.MessageBox.Show("Test" + left + (p - D / 2) / scale);
                for (int i = 0; i < n + 1; i++)
                {
                    Line l1 = new Line() { X1 = left + (p - D / 2) / scale + Cover / scale, X2 = left + (p + D / 2) / scale - Cover / scale, Y1 = top - p3 / scale - del / scale - i * stirrupModel.S / scale, Y2 = top - p3 / scale - del / scale - i * stirrupModel.S / scale };
                    l1.Stroke = solidColorBrush;
                    l1.StrokeThickness = stroke;
                    canvas.Children.Add(l1);
                }
            }
        }
        private static void DrawStirrupItemRectangle1(Canvas canvas, double left, double top, double scale, double p1, double p2, double p3, double Cover, double stroke, StirrupModel stirrupModel, SolidColorBrush solidColorBrush)
        {
            if (stirrupModel.S1 != 0 && stirrupModel.S2 != 0)
            {
                int n1 = (int)(stirrupModel.L1 / stirrupModel.S1);
                double del1 = 0.5 * (stirrupModel.L1 - n1 * stirrupModel.S1);
                for (int i = 0; i < n1 + 1; i++)
                {
                    Line l1 = new Line() { X1 = left + p1 / scale + Cover / scale, X2 = left + p2 / scale - Cover / scale, Y1 = top - p3 / scale - del1 / scale - i * stirrupModel.S1 / scale, Y2 = top - p3 / scale - del1 / scale - i * stirrupModel.S1 / scale };
                    l1.Stroke = solidColorBrush;
                    l1.StrokeThickness = stroke;
                    canvas.Children.Add(l1);
                }
                int n2 = (int)(stirrupModel.L2 / stirrupModel.S2);
                double del2 = 0.5 * (stirrupModel.L2 - n2 * stirrupModel.S2);
                for (int i = 0; i < n2 + 1; i++)
                {
                    Line l1 = new Line() { X1 = left + p1 / scale + Cover / scale, X2 = left + p2 / scale - Cover / scale, Y1 = top - p3 / scale - del2 / scale - stirrupModel.L1 / scale - i * stirrupModel.S2 / scale, Y2 = top - p3 / scale - del2 / scale - stirrupModel.L1 / scale - i * stirrupModel.S2 / scale };
                    l1.Stroke = solidColorBrush;
                    l1.StrokeThickness = stroke;
                    canvas.Children.Add(l1);
                }
                for (int i = 0; i < n1 + 1; i++)
                {
                    Line l1 = new Line() { X1 = left + p1 / scale + Cover / scale, X2 = left + p2 / scale - Cover / scale, Y1 = top - p3 / scale - del1 / scale - stirrupModel.L1 / scale - stirrupModel.L2 / scale - i * stirrupModel.S1 / scale, Y2 = top - p3 / scale - del1 / scale - stirrupModel.L1 / scale - stirrupModel.L2 / scale - i * stirrupModel.S1 / scale };
                    l1.Stroke = solidColorBrush;
                    l1.StrokeThickness = stroke;
                    canvas.Children.Add(l1);
                }
            }
        }
        private static void DrawStirrupItemCylindrical1(Canvas canvas, double left, double top, double scale, double p, double D, double p3, double Cover, double stroke, StirrupModel stirrupModel, SolidColorBrush solidColorBrush)
        {
            if (stirrupModel.S1 != 0 && stirrupModel.S2 != 0)
            {
                int n1 = (int)(stirrupModel.L1 / stirrupModel.S1);
                double del1 = 0.5 * (stirrupModel.L1 - n1 * stirrupModel.S1);
                for (int i = 0; i < n1 + 1; i++)
                {
                    Line l1 = new Line() { X1 = left + (p - D / 2) / scale + Cover / scale, X2 = left + (p + D / 2) / scale - Cover / scale, Y1 = top - p3 / scale - del1 / scale - i * stirrupModel.S1 / scale, Y2 = top - p3 / scale - del1 / scale - i * stirrupModel.S1 / scale };
                    l1.Stroke = solidColorBrush;
                    l1.StrokeThickness = stroke;
                    canvas.Children.Add(l1);
                }
                int n2 = (int)(stirrupModel.L2 / stirrupModel.S2);
                double del2 = 0.5 * (stirrupModel.L2 - n2 * stirrupModel.S2);
                for (int i = 0; i < n2 + 1; i++)
                {
                    Line l1 = new Line() { X1 = left + (p - D / 2) / scale + Cover / scale, X2 = left + (p + D / 2) / scale - Cover / scale, Y1 = top - p3 / scale - del2 / scale - stirrupModel.L1 / scale - i * stirrupModel.S2 / scale, Y2 = top - p3 / scale - del2 / scale - stirrupModel.L1 / scale - i * stirrupModel.S2 / scale };
                    l1.Stroke = solidColorBrush;
                    l1.StrokeThickness = stroke;
                    canvas.Children.Add(l1);
                }
                for (int i = 0; i < n1 + 1; i++)
                {
                    Line l1 = new Line() { X1 = left + (p - D / 2) / scale + Cover / scale, X2 = left + (p + D / 2) / scale - Cover / scale, Y1 = top - p3 / scale - del1 / scale - stirrupModel.L1 / scale - stirrupModel.L2 / scale - i * stirrupModel.S1 / scale, Y2 = top - p3 / scale - del1 / scale - stirrupModel.L1 / scale - stirrupModel.L2 / scale - i * stirrupModel.S1 / scale };
                    l1.Stroke = solidColorBrush;
                    l1.StrokeThickness = stroke;
                    canvas.Children.Add(l1);
                }
            }
        }
        #endregion
        #region BarMain
        public static void DrawBarMains(Canvas canvas, ColumnsModel columnsModel,int choose, int numberBar,int numberAddBar)
        {
            double maxWidth = (columnsModel.SectionStyle == SectionStyle.RECTANGLE) ? columnsModel.DrawModel.GetBmax(columnsModel.InfoModels) : columnsModel.DrawModel.GetDmax(columnsModel.InfoModels);
            if (columnsModel.SectionStyle == SectionStyle.RECTANGLE)
            {

                DrawBarMainRectangle(canvas, columnsModel.DrawModel, columnsModel.BarMainModels[choose], maxWidth, numberBar, numberAddBar);
            }
            else
            {
                DrawBarMainCylindrical(canvas, columnsModel.DrawModel, columnsModel.BarMainModels[choose], maxWidth, numberBar, numberAddBar);
            }

        }

        private static void DrawBarMainRectangle(Canvas canvas,DrawModel drawModel, BarMainModel barMainModel, double maxWidth, int numberBar, int numberAddBar)
        {
            if (barMainModel.BarModels.Count != 0)
            {
                ObservableCollection<BarModel> XbarModels = new ObservableCollection<BarModel>(barMainModel.BarModels.Where(x => (x.BarNumber <= barMainModel.nx)).ToList());
                DrawBarMainItemRectangle(canvas, drawModel.Left, drawModel.Top, drawModel.Scale, drawModel.StrokeMain, drawModel, XbarModels, true, numberBar);
                ObservableCollection<BarModel> YbarModels = new ObservableCollection<BarModel>(barMainModel.BarModels.Where(x => (x.BarNumber == 1) || (x.BarNumber >= barMainModel.BarModels.Count - (barMainModel.ny - 2))).ToList());
                DrawBarMainItemRectangle(canvas, 2.5 * drawModel.Left + maxWidth / drawModel.Scale, drawModel.Top, drawModel.Scale, drawModel.StrokeMain, drawModel, YbarModels, false, numberBar);
            }
            if (barMainModel.AddBarModels.Count!=0)
            {
                ObservableCollection<BarModel> XAddbarModels = new ObservableCollection<BarModel>(barMainModel.AddBarModels.Where(x => (x.BarNumber <= barMainModel.nxA)).ToList());
                DrawBarMainItemRectangle(canvas, drawModel.Left, drawModel.Top, drawModel.Scale, drawModel.StrokeMain, drawModel, XAddbarModels, true, numberAddBar);
                ObservableCollection<BarModel> YAddbarModels = new ObservableCollection<BarModel>(barMainModel.AddBarModels.Where(x => (x.BarNumber == 1) || (x.BarNumber >= barMainModel.AddBarModels.Count - (barMainModel.nyA - 2))).ToList());
                DrawBarMainItemRectangle(canvas, 2.5 * drawModel.Left + maxWidth / drawModel.Scale, drawModel.Top, drawModel.Scale, drawModel.StrokeMain, drawModel, YAddbarModels, false, numberAddBar);
            }
        }
        private static void DrawBarMainCylindrical(Canvas canvas, DrawModel drawModel,  BarMainModel barMainModel, double maxWidth, int numberBar,int numberAddBar)
        {
            if (barMainModel.BarModels.Count != 0)
            {
                ObservableCollection<BarModel> XbarModels = new ObservableCollection<BarModel>(barMainModel.BarModels.Where(x => x.BarNumber <= barMainModel.nd / 2 + 1).ToList());
                DrawBarMainItemRectangle(canvas, drawModel.Left + maxWidth * 0.5 / drawModel.Scale, drawModel.Top, drawModel.Scale, drawModel.StrokeMain, drawModel, XbarModels, true, numberBar);
                ObservableCollection<BarModel> YbarModels = new ObservableCollection<BarModel>(barMainModel.BarModels.Where(x => (x.BarNumber >= barMainModel.nd / 4 + 1) || (x.BarNumber <= 3*barMainModel.nd / 4 + 1)).ToList());
                DrawBarMainItemRectangle(canvas, 2.5 * drawModel.Left + maxWidth / drawModel.Scale + maxWidth * 0.5 / drawModel.Scale, drawModel.Top, drawModel.Scale, drawModel.StrokeMain, drawModel, YbarModels, false, numberBar);
            }
            if (barMainModel.AddBarModels.Count != 0)
            {
                //ObservableCollection<BarModel> XAddbarModels = new ObservableCollection<BarModel>(barMainModel.AddBarModels.Where(x => x.BarNumber <= barMainModel.ndA / 2 + 1).ToList());
                //DrawBarMainItemRectangle(canvas, drawModel.Left + maxWidth * 0.5 / drawModel.Scale, drawModel.Top, drawModel.Scale, drawModel.StrokeMain, drawModel, XAddbarModels, true, numberAddBar);
                //ObservableCollection<BarModel> YAddbarModels = new ObservableCollection<BarModel>(barMainModel.AddBarModels.Where(x => (x.BarNumber == 1) || (x.BarNumber >= barMainModel.AddBarModels.Count - (barMainModel.ndA / 2) - 3)).ToList());
                //DrawBarMainItemRectangle(canvas, 2.5 * drawModel.Left + maxWidth / drawModel.Scale + maxWidth * 0.5 / drawModel.Scale, drawModel.Top, drawModel.Scale, drawModel.StrokeMain, drawModel, YAddbarModels, false, numberAddBar);
                ObservableCollection<BarModel> XAddbarModels = new ObservableCollection<BarModel>(barMainModel.AddBarModels.Where(x => x.BarNumber <= barMainModel.ndA / 2 + 1).ToList());
                DrawBarMainItemRectangle(canvas, drawModel.Left + maxWidth * 0.5 / drawModel.Scale, drawModel.Top, drawModel.Scale, drawModel.StrokeMain, drawModel, XAddbarModels, true, numberAddBar);
                ObservableCollection<BarModel> YAddbarModels = new ObservableCollection<BarModel>(barMainModel.AddBarModels.Where(x => (x.BarNumber >= barMainModel.ndA / 4 + 1) || (x.BarNumber <= 3 * barMainModel.ndA / 4 + 1)).ToList());
                DrawBarMainItemRectangle(canvas, 2.5 * drawModel.Left + maxWidth / drawModel.Scale + maxWidth * 0.5 / drawModel.Scale, drawModel.Top, drawModel.Scale, drawModel.StrokeMain, drawModel, YAddbarModels, false, numberAddBar);
            }
        }
        private static void DrawBarMainItemRectangle(Canvas canvas, double left, double top, double scale,double stroke, DrawModel drawModel, ObservableCollection<BarModel> barModels, bool xy,int numberBar)
        {
            if (barModels.Count!=0)
            {
                
                for (int i = 0; i < barModels.Count; i++)
                {
                    if (barModels[i].Location.Count>1)
                    {
                        for (int j = 1; j < barModels[i].Location.Count; j++)
                        {
                            double x1 = (xy ? barModels[i].Location[j-1].X : barModels[i].Location[j-1].Y) / scale;
                            double x2 = (xy ? barModels[i].Location[j].X : barModels[i].Location[j].Y) / scale;
                            Line l1 = new Line() { X1 = left + x1, X2 = left + x2, Y1 = top - barModels[i].Location[j-1].Z / scale, Y2 = top - barModels[i].Location[j ].Z / scale };
                            l1.Stroke = (barModels[i].BarNumber == numberBar)?drawModel.ColorMainBarChoose:drawModel.ColorMainBar;
                            l1.StrokeThickness = stroke;
                            canvas.Children.Add(l1);
                        }
                    }
                }
            } 

        }
        private static void DrawBarMainItemCylindrical(Canvas canvas, double left, double top, double scale, double stroke, DrawModel drawModel, ObservableCollection<BarModel> barModels, bool xy, int numberBar)
        {
            if (barModels.Count != 0)
            {

                for (int i = 0; i < barModels.Count/2+1; i++)
                {
                    if (barModels[i].Location.Count > 1)
                    {
                        for (int j = 1; j < barModels[i].Location.Count; j++)
                        {
                            double x1 = (xy ? barModels[i].Location[j - 1].X : barModels[i].Location[j - 1].Y) / scale;
                            double x2 = (xy ? barModels[i].Location[j].X : barModels[i].Location[j].Y) / scale;
                            Line l1 = new Line() { X1 = left + x1, X2 = left + x2, Y1 = top - barModels[i].Location[j - 1].Z / scale, Y2 = top - barModels[i].Location[j].Z / scale };
                            l1.Stroke = (barModels[i].BarNumber == numberBar) ? drawModel.ColorMainBarChoose : drawModel.ColorMainBar;
                            l1.StrokeThickness = stroke;
                            canvas.Children.Add(l1);
                        }
                    }
                }
            }
        }
        #endregion
        #region Dowels
        
        public static void DrawBarTopDowels(Canvas canvas, DrawModel drawModelDowels, BarMainModel barMainModel,int choose)
        {
            if (barMainModel.BarModels.Count!=0)
            {
                SolidColorBrush solidColorBrush = drawModelDowels.ColorMainBar;
                for (int i = 0; i < barMainModel.BarModels.Count; i++)
                {
                    if (barMainModel.BarModels[i].IsTopDowels)
                    {
                        if (i==choose)
                        {
                            solidColorBrush = drawModelDowels.ColorMainBarChoose;
                        }
                        else
                        {
                            if (barMainModel.BarModels[i].TopDowels==0 )
                            {
                                if (barMainModel.BarModels[i].BarNumber%2==0)
                                {
                                    solidColorBrush = Brushes.Aqua;
                                }
                                else
                                {
                                    solidColorBrush = Brushes.Orange;
                                }
                            }
                            else
                            {
                                solidColorBrush = drawModelDowels.ColorMainBar;
                            }
                           
                        }
                        DrawBarTopDowelsItem(canvas, drawModelDowels, barMainModel.BarModels[i], solidColorBrush);
                    }
                }
            }
        }
        public static void DrawAddBarTopDowels(Canvas canvas, DrawModel drawModelDowels, BarMainModel barMainModel, int choose)
        {
            if (barMainModel.AddBarModels.Count != 0)
            {
                SolidColorBrush solidColorBrush = drawModelDowels.ColorMainBar;
                for (int i = 0; i < barMainModel.AddBarModels.Count; i++)
                {
                    if (i == choose)
                    {
                        solidColorBrush = drawModelDowels.ColorMainBarChoose;
                    }
                    else
                    {
                        solidColorBrush = drawModelDowels.ColorMainBar;

                    }
                    DrawAddBarTopDowelsItem(canvas, drawModelDowels, barMainModel.AddBarModels[i], solidColorBrush);
                }
            }
        }
        private static void DrawBarTopDowelsItem(Canvas canvas, DrawModel drawModelDowels, BarModel barModel,SolidColorBrush solidColorBrush)
        {
            if (barModel.TopDowels == 0)
            {
                DrawImage.DrawLineItemDowels(canvas, drawModelDowels.Left, drawModelDowels.Top, drawModelDowels.Scale, barModel.Location[barModel.Location.Count - 3], barModel.Location[barModel.Location.Count - 2], solidColorBrush, false, barModel.Bar.Diameter);
                DrawImage.DrawOneBarSection(canvas, drawModelDowels.Left+ barModel.Location[barModel.Location.Count - 2] .X/ drawModelDowels.Scale, drawModelDowels.Top- barModel.Location[barModel.Location.Count - 2] .Y/ drawModelDowels.Scale, drawModelDowels.Scale, barModel.Bar.Diameter, Brushes.Black);
            }
            else
            {
                if (barModel.LaTopDowels != 0)
                {
                    DrawImage.DrawLineItemDowels(canvas, drawModelDowels.Left, drawModelDowels.Top, drawModelDowels.Scale, barModel.Location[barModel.Location.Count - 2], barModel.Location[barModel.Location.Count - 1], solidColorBrush, false, barModel.Bar.Diameter);
                }
            }
        }
        private static void DrawAddBarTopDowelsItem(Canvas canvas, DrawModel drawModelDowels, BarModel barModel, SolidColorBrush solidColorBrush)
        {
            DrawImage.DrawLineItemDowels(canvas, drawModelDowels.Left, drawModelDowels.Top, drawModelDowels.Scale, barModel.Location[0], barModel.Location[1], solidColorBrush, true, barModel.Bar.Diameter);
            DrawImage.DrawOneBarSection(canvas, drawModelDowels.Left + barModel.Location[1].X / drawModelDowels.Scale, drawModelDowels.Top - barModel.Location[1].Y / drawModelDowels.Scale, drawModelDowels.Scale, barModel.Bar.Diameter, solidColorBrush);
        }
        public static void DrawBarBottomDowels(Canvas canvas, DrawModel drawModelDowels, BarMainModel barMainModel, int choose, BarMainModel barMainModelDown=null)
        {
            if (barMainModel.BarModels.Count != 0)
            {
                SolidColorBrush solidColorBrush = drawModelDowels.ColorMainBar;
                for (int i = 0; i < barMainModel.BarModels.Count; i++)
                {
                    if (barMainModel.BarModels[i].IsBottomDowels)
                    {
                        if (i == choose)
                        {
                            solidColorBrush = drawModelDowels.ColorMainBarChoose;
                        }
                        else
                        {
                            solidColorBrush = drawModelDowels.ColorMainBar;

                        }
                        DrawBarBottomDowelsItem(canvas, drawModelDowels, barMainModel.BarModels[i], solidColorBrush);
                    }
                }
            }
            if (barMainModelDown != null)
            {
                if (barMainModelDown.BarModels.Count!=0)
                {
                    for (int i = 0; i < barMainModelDown.BarModels.Count; i++)
                    {

                        SolidColorBrush solidColorBrush = drawModelDowels.ColorMainBar;
                        if (barMainModelDown.BarModels[i].IsTopDowels&& barMainModelDown.BarModels[i].TopDowels==0)
                        {
                            if (barMainModelDown.BarModels[i].BarNumber%2==0)
                            {
                                solidColorBrush = Brushes.Aqua;
                            }
                            else
                            {
                                solidColorBrush = Brushes.Orange;
                            }
                            DrawImage.DrawLineItemDowels(canvas, drawModelDowels.Left, drawModelDowels.Top, drawModelDowels.Scale, barMainModelDown.BarModels[i].Location[barMainModelDown.BarModels[i].Location.Count - 3], barMainModelDown.BarModels[i].Location[barMainModelDown.BarModels[i].Location.Count - 2], solidColorBrush, false, barMainModelDown.BarModels[i].Bar.Diameter);
                            DrawImage.DrawOneBarSection(canvas, drawModelDowels.Left + barMainModelDown.BarModels[i].Location[barMainModelDown.BarModels[i].Location.Count - 2].X / drawModelDowels.Scale, drawModelDowels.Top - barMainModelDown.BarModels[i].Location[barMainModelDown.BarModels[i].Location.Count - 2].Y / drawModelDowels.Scale, drawModelDowels.Scale, barMainModelDown.BarModels[i].Bar.Diameter, solidColorBrush);
                        }
                    }

                }
                if (barMainModelDown.AddBarModels.Count!=0)
                {
                    SolidColorBrush solidColorBrush = drawModelDowels.ColorMainBar;
                    for (int i = 0; i < barMainModelDown.AddBarModels.Count; i++)
                    {
                        if (barMainModelDown.BarModels[i].BarNumber % 2 == 0)
                        {
                            solidColorBrush = Brushes.Aqua;
                        }
                        else
                        {
                            solidColorBrush = Brushes.Orange;
                        }
                        DrawImage.DrawLineItemDowels(canvas, drawModelDowels.Left, drawModelDowels.Top, drawModelDowels.Scale, barMainModelDown.AddBarModels[i].Location[0], barMainModelDown.AddBarModels[i].Location[1], solidColorBrush, false, barMainModelDown.AddBarModels[i].Bar.Diameter);
                        DrawImage.DrawOneBarSection(canvas, drawModelDowels.Left + barMainModelDown.AddBarModels[i].Location[1].X / drawModelDowels.Scale, drawModelDowels.Top - barMainModelDown.AddBarModels[i].Location[1].Y / drawModelDowels.Scale, drawModelDowels.Scale, barMainModelDown.AddBarModels[i].Bar.Diameter, solidColorBrush);
                    }
                }
            }
        }
        private static void DrawBarBottomDowelsItem(Canvas canvas, DrawModel drawModelDowels, BarModel barModel, SolidColorBrush solidColorBrush)
        {
            if (barModel.BottomDowels == 0)
            {
                DrawImage.DrawOneBarSection(canvas, drawModelDowels.Left + barModel.Location[0].X / drawModelDowels.Scale, drawModelDowels.Top - barModel.Location[0].Y / drawModelDowels.Scale, drawModelDowels.Scale, barModel.Bar.Diameter, Brushes.Black);
            }
            else
            {
                if (barModel.LaBottomDowels != 0)
                {
                    DrawImage.DrawLineItemDowels(canvas, drawModelDowels.Left, drawModelDowels.Top, drawModelDowels.Scale, barModel.Location[0], barModel.Location[1], solidColorBrush, false, barModel.Bar.Diameter);
                }
                DrawImage.DrawOneBarSection(canvas, drawModelDowels.Left + barModel.Location[1].X / drawModelDowels.Scale, drawModelDowels.Top - barModel.Location[1].Y / drawModelDowels.Scale, drawModelDowels.Scale, barModel.Bar.Diameter, Brushes.Black);
            }
        }
        #endregion
    }
}
