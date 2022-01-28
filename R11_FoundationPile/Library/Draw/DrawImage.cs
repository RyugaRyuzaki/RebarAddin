using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace R11_FoundationPile
{
    public class DrawImage
    {

        public static void DrawStirrupType1(Canvas canvas, double left, double top, double scale, double b, double h, double c)
        {
            Rectangle rec = new Rectangle() { Width = (b - 2 * c - 6) / scale, Height = (h - 2 * c - 6) / scale };
            rec.Stroke = Brushes.Blue;
            rec.StrokeThickness = 3;
            rec.RadiusX = c / scale;
            rec.RadiusY = c / scale;
            Canvas.SetTop(rec, top + c / scale + 3);
            Canvas.SetLeft(rec, left + c / scale + 3);
            Line l1 = new Line() { X1 = left + c / scale + 3, X2 = left + 3 * c / scale, Y1 = top + 2 * c / scale + 3, Y2 = top + 4 * c / scale };
            l1.Stroke = Brushes.Blue;
            l1.StrokeThickness = 3;
            Line l2 = new Line() { X1 = left + 2 * c / scale + 3, X2 = left + 4 * c / scale, Y1 = top + c / scale + 3, Y2 = top + 3 * c / scale };
            l2.Stroke = Brushes.Blue;
            l2.StrokeThickness = 3;
            canvas.Children.Add(rec);
            canvas.Children.Add(l1);
            canvas.Children.Add(l2);
        }
        public static void DrawStirrupType2(Canvas canvas, double left, double top, double scale, double b, double h, double c, double a)
        {
            if (a < b - 2 * c)
            {
                DrawStirrupType1(canvas, left, top, scale, (b + a + 2 * c) / 2, h, c);
                DrawStirrupType1(canvas, left + (b - 2 * c - a) / (2 * scale), top, scale, (b + a + 2 * c) / 2, h, c);
            }

        }
        public static void DimHorizontal(Canvas canvas, double left, double top, double scale, double l, int font, double offset, double extend)
        {
            Line l1 = new Line() { X1 = left, X2 = left + l / scale, Y1 = top - offset, Y2 = top - offset };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = 0.5;
            Line l2 = new Line() { X1 = left - extend, X2 = left + extend, Y1 = top - offset + extend, Y2 = top - offset - extend };
            l2.Stroke = Brushes.Black;
            l2.StrokeThickness = 0.5;
            Line l3 = new Line() { X1 = left - extend + l / scale, X2 = left + extend + l / scale, Y1 = top - offset + extend, Y2 = top - offset - extend };
            l3.Stroke = Brushes.Black;
            l3.StrokeThickness = 0.5;
            Line l4 = new Line() { X1 = left, X2 = left, Y1 = top - offset - extend, Y2 = top - extend };
            l4.Stroke = Brushes.Black;
            l4.StrokeThickness = 0.5;
            Line l5 = new Line() { X1 = left + l / scale, X2 = left + l / scale, Y1 = top - offset - extend, Y2 = top - extend };
            l5.Stroke = Brushes.Black;
            l5.StrokeThickness = 0.5;
            TextBlock text = new TextBlock();
            text.Text = l.ToString();
            text.FontSize = font;
            text.Foreground = Brushes.Black;
            text.FontFamily = new FontFamily("Tahoma");
            text.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text.Arrange(new Rect(text.DesiredSize));
            Canvas.SetTop(text, top - offset - 2 * font);
            Canvas.SetLeft(text, left + l / (2 * scale) - text.ActualWidth / 2);
            canvas.Children.Add(l1);
            canvas.Children.Add(l2);
            canvas.Children.Add(l3);
            canvas.Children.Add(l4);
            canvas.Children.Add(l5);
            canvas.Children.Add(text);

        }
        public static void DimVertical(Canvas canvas, double left, double top, double scale, double l, int font, double offset, double extend)
        {
            Line l1 = new Line() { X1 = left - offset, X2 = left - offset, Y1 = top, Y2 = top + l / scale };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = 0.5;
            Line l2 = new Line() { X1 = left - extend - offset, X2 = left + extend - offset, Y1 = top + extend, Y2 = top - extend };
            l2.Stroke = Brushes.Black;
            l2.StrokeThickness = 0.5;
            Line l3 = new Line() { X1 = left - extend - offset, X2 = left + extend - offset, Y1 = top + l / scale + extend, Y2 = top + l / scale - extend };
            l3.Stroke = Brushes.Black;
            l3.StrokeThickness = 0.5;
            Line l4 = new Line() { X1 = left - offset - extend, X2 = left - extend, Y1 = top, Y2 = top };
            l4.Stroke = Brushes.Black;
            l4.StrokeThickness = 0.5;
            Line l5 = new Line() { X1 = left - offset - extend, X2 = left - extend, Y1 = top + l / scale, Y2 = top + l / scale };
            l5.Stroke = Brushes.Black;
            l5.StrokeThickness = 0.5;
            TextBlock text = new TextBlock();
            text.Text = l.ToString();
            text.FontSize = font;
            text.Foreground = Brushes.Black;
            text.LayoutTransform = new RotateTransform(-90, 10, 10);
            text.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text.Arrange(new Rect(text.DesiredSize));
            Canvas.SetTop(text, top + l / (2 * scale) - font);
            Canvas.SetLeft(text, left - offset - 10 - font);
            canvas.Children.Add(l1);
            canvas.Children.Add(l2);
            canvas.Children.Add(l3);
            canvas.Children.Add(l4);
            canvas.Children.Add(l5);
            canvas.Children.Add(text);

        }

       

        public static void DrawSection(Canvas canvas, double scale, double left, double top, double b, double h)
        {
            Rectangle rec = new Rectangle() { Width = b / scale, Height = h / scale };
            rec.Stroke = Brushes.Black;
            rec.StrokeThickness = 1;
            Canvas.SetTop(rec, top);
            Canvas.SetLeft(rec, left);
            canvas.Children.Add(rec);
        }
        public static void DrawSectionDashArraySolidColorBrush(Canvas canvas, double scale, double left, double top, double b, double h,SolidColorBrush solidColorBrush)
        {
            Rectangle rec = new Rectangle() { Width = b / scale, Height = h / scale };
            rec.Stroke = solidColorBrush;
            rec.StrokeThickness = 1;
            rec.StrokeDashArray = new DoubleCollection() { 5, 2 };
            Canvas.SetTop(rec, top);
            Canvas.SetLeft(rec, left);
            canvas.Children.Add(rec);
        }
        public static void DimHorizontalText(Canvas canvas, double left, double top, double scale, double l, int font, double offset, double extend, string t)
        {
            Line l1 = new Line() { X1 = left, X2 = left + l / scale, Y1 = top - offset, Y2 = top - offset };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = 0.5;
            Line l2 = new Line() { X1 = left - extend, X2 = left + extend, Y1 = top - offset + extend, Y2 = top - offset - extend };
            l2.Stroke = Brushes.Black;
            l2.StrokeThickness = 0.5;
            Line l3 = new Line() { X1 = left - extend + l / scale, X2 = left + extend + l / scale, Y1 = top - offset + extend, Y2 = top - offset - extend };
            l3.Stroke = Brushes.Black;
            l3.StrokeThickness = 0.5;
            Line l4 = new Line() { X1 = left, X2 = left, Y1 = top - offset - extend, Y2 = top - extend };
            l4.Stroke = Brushes.Black;
            l4.StrokeThickness = 0.5;
            Line l5 = new Line() { X1 = left + l / scale, X2 = left + l / scale, Y1 = top - offset - extend, Y2 = top - extend };
            l5.Stroke = Brushes.Black;
            l5.StrokeThickness = 0.5;
            TextBlock text = new TextBlock();
            text.Text = t;
            text.FontSize = font;
            text.Foreground = Brushes.Black;
            text.FontFamily = new FontFamily("Tahoma");
            text.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text.Arrange(new Rect(text.DesiredSize));
            Canvas.SetTop(text, top - offset - 2 * font);
            Canvas.SetLeft(text, left + l / (2 * scale) - text.ActualWidth / 2);
            canvas.Children.Add(l1);
            canvas.Children.Add(l2);
            canvas.Children.Add(l3);
            canvas.Children.Add(l4);
            canvas.Children.Add(l5);
            canvas.Children.Add(text);

        }
        public static void DimVerticalText(Canvas canvas, double left, double top, double scale, double l, int font, double offset, double extend, string t)
        {
            Line l1 = new Line() { X1 = left - offset, X2 = left - offset, Y1 = top, Y2 = top + l / scale };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = 0.5;
            Line l2 = new Line() { X1 = left - extend - offset, X2 = left + extend - offset, Y1 = top + extend, Y2 = top - extend };
            l2.Stroke = Brushes.Black;
            l2.StrokeThickness = 0.5;
            Line l3 = new Line() { X1 = left - extend - offset, X2 = left + extend - offset, Y1 = top + l / scale + extend, Y2 = top + l / scale - extend };
            l3.Stroke = Brushes.Black;
            l3.StrokeThickness = 0.5;
            Line l4 = new Line() { X1 = left - offset - extend, X2 = left - extend, Y1 = top, Y2 = top };
            l4.Stroke = Brushes.Black;
            l4.StrokeThickness = 0.5;
            Line l5 = new Line() { X1 = left - offset - extend, X2 = left - extend, Y1 = top + l / scale, Y2 = top + l / scale };
            l5.Stroke = Brushes.Black;
            l5.StrokeThickness = 0.5;
            TextBlock text = new TextBlock();
            text.Text = t;
            text.FontSize = font;
            text.Foreground = Brushes.Black;
            text.LayoutTransform = new RotateTransform(90, 25, 25);
            Canvas.SetTop(text, top + l / (2 * scale) - font);
            Canvas.SetLeft(text, left - offset);
            canvas.Children.Add(l1);
            canvas.Children.Add(l2);
            canvas.Children.Add(l3);
            canvas.Children.Add(l4);
            canvas.Children.Add(l5);
            canvas.Children.Add(text);

        }
        public static void DimHorizontalText1(Canvas canvas, double left, double top, double scale, double l, int font, double offset, double extend, string t)
        {
            Line l1 = new Line() { X1 = left, X2 = left + l / scale, Y1 = top - offset, Y2 = top - offset };
            l1.Stroke = Brushes.Red;
            l1.StrokeThickness = 0.5;
            Line l2 = new Line() { X1 = left - extend, X2 = left + extend, Y1 = top - offset + extend, Y2 = top - offset - extend };
            l2.Stroke = Brushes.Red;
            l2.StrokeThickness = 0.5;
            Line l3 = new Line() { X1 = left - extend + l / scale, X2 = left + extend + l / scale, Y1 = top - offset + extend, Y2 = top - offset - extend };
            l3.Stroke = Brushes.Red;
            l3.StrokeThickness = 0.5;
            Line l4 = new Line() { X1 = left, X2 = left, Y1 = top - offset - extend, Y2 = top - extend };
            l4.Stroke = Brushes.Red;
            l4.StrokeThickness = 0.5;
            Line l5 = new Line() { X1 = left + l / scale, X2 = left + l / scale, Y1 = top - offset - extend, Y2 = top - extend };
            l5.Stroke = Brushes.Red;
            l5.StrokeThickness = 0.5;
            TextBlock text = new TextBlock();
            text.Text = t;
            text.FontSize = font;
            text.Foreground = Brushes.Red;
            text.FontFamily = new FontFamily("Tahoma");
            text.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text.Arrange(new Rect(text.DesiredSize));
            Canvas.SetTop(text, top - offset - 2 * font);
            Canvas.SetLeft(text, left + l / (2 * scale) - text.ActualWidth / 2);
            canvas.Children.Add(l1);
            canvas.Children.Add(l2);
            canvas.Children.Add(l3);
            canvas.Children.Add(l4);
            canvas.Children.Add(l5);
            canvas.Children.Add(text);

        }
        public static void DimHorizontalText2(Canvas canvas, double left, double top, double scale, double l, int font, double offset, double extend, string t)
        {
            Line l1 = new Line() { X1 = left, X2 = left + l / scale, Y1 = top + offset, Y2 = top + offset };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = 0.5;
            Line l2 = new Line() { X1 = left - extend, X2 = left + extend, Y1 = top + offset + extend, Y2 = top + offset - extend };
            l2.Stroke = Brushes.Black;
            l2.StrokeThickness = 0.5;
            Line l3 = new Line() { X1 = left - extend + l / scale, X2 = left + extend + l / scale, Y1 = top + offset + extend, Y2 = top + offset - extend };
            l3.Stroke = Brushes.Black;
            l3.StrokeThickness = 0.5;
            Line l4 = new Line() { X1 = left, X2 = left, Y1 = top + offset + extend, Y2 = top + extend };
            l4.Stroke = Brushes.Black;
            l4.StrokeThickness = 0.5;
            Line l5 = new Line() { X1 = left + l / scale, X2 = left + l / scale, Y1 = top + offset + extend, Y2 = top + extend };
            l5.Stroke = Brushes.Black;
            l5.StrokeThickness = 0.5;
            TextBlock text = new TextBlock();
            text.Text = t;
            text.FontSize = font;
            text.Foreground = Brushes.Black;
            text.FontFamily = new FontFamily("Tahoma");
            text.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text.Arrange(new Rect(text.DesiredSize));
            Canvas.SetTop(text, top + offset);
            Canvas.SetLeft(text, left + l / (2 * scale) - text.ActualWidth / 2);
            canvas.Children.Add(l1);
            canvas.Children.Add(l2);
            canvas.Children.Add(l3);
            canvas.Children.Add(l4);
            canvas.Children.Add(l5);
            canvas.Children.Add(text);

        }
        public static void DimVerticalText1(Canvas canvas, double left, double top, double scale, double l, int font, double offset, double extend, string t)
        {
            Line l1 = new Line() { X1 = left - offset, X2 = left - offset, Y1 = top, Y2 = top + l / scale };
            l1.Stroke = Brushes.Red;
            l1.StrokeThickness = 0.5;
            Line l2 = new Line() { X1 = left - extend - offset, X2 = left + extend - offset, Y1 = top + extend, Y2 = top - extend };
            l2.Stroke = Brushes.Red;
            l2.StrokeThickness = 0.5;
            Line l3 = new Line() { X1 = left - extend - offset, X2 = left + extend - offset, Y1 = top + l / scale + extend, Y2 = top + l / scale - extend };
            l3.Stroke = Brushes.Red;
            l3.StrokeThickness = 0.5;
            Line l4 = new Line() { X1 = left - offset - extend, X2 = left - extend, Y1 = top, Y2 = top };
            l4.Stroke = Brushes.Red;
            l4.StrokeThickness = 0.5;
            Line l5 = new Line() { X1 = left - offset - extend, X2 = left - extend, Y1 = top + l / scale, Y2 = top + l / scale };
            l5.Stroke = Brushes.Red;
            l5.StrokeThickness = 0.5;
            TextBlock text = new TextBlock();
            text.Text = t;
            text.FontSize = font;
            text.Foreground = Brushes.Red;
            text.LayoutTransform = new RotateTransform(90, 25, 25);
            Canvas.SetTop(text, top + l / (2 * scale) - font);
            Canvas.SetLeft(text, left - offset);
            canvas.Children.Add(l1);
            canvas.Children.Add(l2);
            canvas.Children.Add(l3);
            canvas.Children.Add(l4);
            canvas.Children.Add(l5);
            canvas.Children.Add(text);

        }
        public static void DrawDistribute(Canvas canvas, int type, bool tiesUp)
        {
            canvas.Children.Clear();
            Line l1 = new Line() { X1 = 70, X2 = 150, Y1 = 10, Y2 = 10 };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = 1;
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = 150, X2 = 150, Y1 = 10, Y2 = 50 };
            l2.Stroke = Brushes.Black;
            l2.StrokeThickness = 1;
            l2.StrokeDashArray = new DoubleCollection() { 5, 2 };
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = 150, X2 = 130, Y1 = 50, Y2 = 50 };
            l3.Stroke = Brushes.Black;
            l3.StrokeThickness = 1;
            canvas.Children.Add(l3);
            Line l4 = new Line() { X1 = 130, X2 = 130, Y1 = 50, Y2 = 240 };
            l4.Stroke = Brushes.Black;
            l4.StrokeThickness = 1;
            canvas.Children.Add(l4);
            Line l5 = new Line() { X1 = 130, X2 = 90, Y1 = 240, Y2 = 240 };
            l5.Stroke = Brushes.Black;
            l5.StrokeThickness = 1;
            l5.StrokeDashArray = new DoubleCollection() { 5, 2 };
            canvas.Children.Add(l5);
            Line l6 = new Line() { X1 = 90, X2 = 90, Y1 = 240, Y2 = 50 };
            l6.Stroke = Brushes.Black;
            l6.StrokeThickness = 1;
            canvas.Children.Add(l6);
            Line l7 = new Line() { X1 = 90, X2 = 70, Y1 = 50, Y2 = 50 };
            l7.Stroke = Brushes.Black;
            l7.StrokeThickness = 1;
            canvas.Children.Add(l7);
            Line l8 = new Line() { X1 = 70, X2 = 70, Y1 = 50, Y2 = 10 };
            l8.Stroke = Brushes.Black;
            l8.StrokeThickness = 1;
            l8.StrokeDashArray = new DoubleCollection() { 5, 2 };
            canvas.Children.Add(l8);
            Line l9 = new Line() { X1 = 95, X2 = 95, Y1 = 10, Y2 = 240 };
            l9.Stroke = Brushes.Red;
            l9.StrokeThickness = 2;
            canvas.Children.Add(l9);
            Line l10 = new Line() { X1 = 125, X2 = 125, Y1 = 10, Y2 = 240 };
            l10.Stroke = Brushes.Red;
            l10.StrokeThickness = 2;
            canvas.Children.Add(l10);
            double l = tiesUp ? (220) : 180;
            double s = 20, s1 = 10;
            if (type == 0)
            {
                for (int i = 0; i <= (l / s); i++)
                {
                    Line l11 = new Line() { X1 = 95, X2 = 125, Y1 = (tiesUp ? 15 : 55) + i * s, Y2 = (tiesUp ? 15 : 55) + i * s };
                    l11.Stroke = Brushes.Blue;
                    l11.StrokeThickness = 1.5;
                    canvas.Children.Add(l11);
                }
            }
            else
            {
                int i = 0, j = 0, k = 0;
                for (i = 0; i <= 60 / s1; i++)
                {
                    Line l11 = new Line() { X1 = 95, X2 = 125, Y1 = (tiesUp ? 15 : 55) + i * s1, Y2 = (tiesUp ? 15 : 55) + i * s1 };
                    l11.Stroke = Brushes.Blue;
                    l11.StrokeThickness = 1.5;
                    canvas.Children.Add(l11);

                }
                for (j = 0; j < (tiesUp ? 110 : 70) / s; j++)
                {
                    Line l11 = new Line() { X1 = 95, X2 = 125, Y1 = (tiesUp ? 15 : 55) + (i - 1) * s1 + j * s, Y2 = (tiesUp ? 15 : 55) + (i - 1) * s1 + j * s };
                    l11.Stroke = Brushes.Blue;
                    l11.StrokeThickness = 1.5;
                    canvas.Children.Add(l11);
                }
                for (k = 0; k <= 60 / s1; k++)
                {
                    Line l11 = new Line() { X1 = 95, X2 = 125, Y1 = (tiesUp ? 15 : 55) + (i - 1) * s1 + (j - 1) * s + k * s1, Y2 = (tiesUp ? 15 : 55) + (i - 1) * s1 + (j - 1) * s + k * s1 };
                    l11.Stroke = Brushes.Blue;
                    l11.StrokeThickness = 1.5;
                    canvas.Children.Add(l11);
                }
            }


            DimVerticalText(canvas, 20, tiesUp ? 10 : 50, 1, tiesUp ? 230 : 190, 11, 20, 5, "L");
            DimVerticalText(canvas, 40, tiesUp ? 10 : 50, 1, 60, 11, 20, 5, "L1");
            DimVerticalText(canvas, 40, 180, 1, 60, 11, 20, 5, "L1");
            DimVerticalText(canvas, 40, tiesUp ? 70 : 110, 1, tiesUp ? 110 : 70, 11, 20, 5, "L2");
            DimVerticalText(canvas, 70, (tiesUp ? 15 : 55), 1, (type == 0) ? s : s1, 11, 20, 5, (type == 0) ? "S" : "S1");
            DimVerticalText(canvas, 70, (tiesUp ? 15 : 55) + 60 + s, 1, s, 11, 20, 5, (type == 0) ? "S" : "S2");
            DimVerticalText(canvas, 70, (type == 0) ? 235 - s : 235 - s1, 1, (type == 0) ? s : s1, 11, 20, 5, (type == 0) ? "S" : "S1");
        }
        public static void DrawDistribute2(Canvas canvas)
        {
            canvas.Children.Clear();
            Polygon p = new Polygon();
            p.Stroke = Brushes.Black;
            p.StrokeThickness = 1.5;
            PointCollection points = new PointCollection();
            Point p1 = new Point() { X = 10, Y = 20 };
            Point p2 = new Point() { X = 40, Y = 20 };
            Point p3 = new Point() { X = 40, Y = 30 };
            Point p4 = new Point() { X = 400, Y = 30 };
            Point p5 = new Point() { X = 400, Y = 20 };
            Point p6 = new Point() { X = 430, Y = 20 };
            Point p7 = new Point() { X = 430, Y = 80 };
            Point p8 = new Point() { X = 400, Y = 80 };
            Point p9 = new Point() { X = 400, Y = 70 };
            Point p10 = new Point() { X = 40, Y = 70 };
            Point p11 = new Point() { X = 40, Y = 80 };
            Point p12 = new Point() { X = 10, Y = 80 };
            points.Add(p1); points.Add(p2); points.Add(p3); points.Add(p4); points.Add(p5); points.Add(p6); points.Add(p7); points.Add(p8); points.Add(p9); points.Add(p10); points.Add(p11); points.Add(p12);
            p.Points = points;

            Line l1 = new Line() { X1 = 10, X2 = 430, Y1 = 35, Y2 = 35 };
            l1.Stroke = Brushes.Red;
            l1.StrokeThickness = 2;
            Line l2 = new Line() { X1 = 10, X2 = 430, Y1 = 65, Y2 = 65 };
            l2.Stroke = Brushes.Red;
            l2.StrokeThickness = 2;
            for (int i = 0; i <= 360 / (20 * 4); i++)
            {
                Line l01 = new Line() { X1 = 40 + i * 20, X2 = 40 + i * 20, Y1 = 35, Y2 = 65 };
                l01.Stroke = Brushes.Green;
                l01.StrokeThickness = 2;
                canvas.Children.Add(l01);

            }
            for (int i = (360 * 3) / (20 * 4); i <= 360 / 20; i++)
            {
                Line l01 = new Line() { X1 = 40 + i * 20, X2 = 40 + i * 20, Y1 = 35, Y2 = 65 };
                l01.Stroke = Brushes.Green;
                l01.StrokeThickness = 2;
                canvas.Children.Add(l01);

            }
            for (int i = 0; i <= 180 / 40; i++)
            {
                Line l01 = new Line() { X1 = 40 + i * 40 + 36 * 20 / 8, X2 = 40 + i * 40 + 36 * 20 / 8, Y1 = 35, Y2 = 65 };
                l01.Stroke = Brushes.Green;
                l01.StrokeThickness = 2;
                canvas.Children.Add(l01);

            }
            canvas.Children.Add(p);
            canvas.Children.Add(l1);
            canvas.Children.Add(l2);
            DimHorizontalText(canvas, 210, 20, 1, 40, 9, 5, 2, "S2");
            DimHorizontalText(canvas, 60, 20, 1, 20, 9, 5, 2, "S1");
            DimHorizontalText(canvas, 360, 20, 1, 20, 9, 5, 2, "S1");
            DimHorizontalText(canvas, 40, 95, 1, 90, 9, 5, 2, "L1");
            DimHorizontalText(canvas, 310, 95, 1, 90, 9, 5, 2, "L1");
            DimHorizontalText(canvas, 130, 95, 1, 180, 9, 5, 2, "L2");
        }
        public static void DrawCutHorizontal(Canvas canvas, double left, double top, double scale, double length, double extend, double offset, SolidColorBrush colorSelected)
        {
            Line l1 = new Line() { X1 = left, X2 = left + (length / (scale)), Y1 = top, Y2 = top };
            l1.Stroke = colorSelected;
            l1.StrokeThickness = 0.5;
            l1.StrokeDashArray = new DoubleCollection() { 10, 5 };
            //Line l2 = new Line() { X1 = left + (length / (2 * scale)) - extend, X2 = left + (length / (2 * scale)), Y1 = top, Y2 = top - offset };
            //l2.Stroke = colorSelected;
            //l2.StrokeThickness = 0.5;
            //Line l3 = new Line() { X1 = left + (length / (2 * scale)), X2 = left + (length / (2 * scale)), Y1 = top - offset, Y2 = top + offset };
            //l3.Stroke = colorSelected;
            //l3.StrokeThickness = 0.5;
            //Line l4 = new Line() { X1 = left + (length / (2 * scale)), X2 = left + (length / (2 * scale)) + extend, Y1 = top + offset, Y2 = top };
            //l4.Stroke = colorSelected;
            //l4.StrokeThickness = 0.5;
            //Line l5 = new Line() { X1 = left + (length / (2 * scale)) + extend, X2 = left + length / scale + extend, Y1 = top, Y2 = top };
            //l5.Stroke = colorSelected;
            //l5.StrokeThickness = 0.5;

            canvas.Children.Add(l1); /*canvas.Children.Add(l2); canvas.Children.Add(l3); canvas.Children.Add(l4); canvas.Children.Add(l5);*/

        }
        public static void DrawStartAddTopBar(Canvas canvas, bool check)
        {
            canvas.Children.Clear();
            SolidColorBrush colorSelected = Brushes.Transparent;
            if (!check)
            {
                colorSelected = Brushes.Gainsboro;
            }
            else
            {
                colorSelected = Brushes.Transparent;
            }
            Rectangle r = new Rectangle() { Width = 330, Height = 90 };
            r.Fill = colorSelected;
            canvas.Children.Add(r);
            Line l1 = new Line() { X1 = 20, X2 = 320, Y1 = 20, Y2 = 20 };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = 1.5;
            Line l2 = new Line() { X1 = 320, X2 = 320, Y1 = 20, Y2 = 70 };
            l2.Stroke = Brushes.Black;
            l2.StrokeThickness = 1.5;
            l2.StrokeDashArray = new DoubleCollection() { 5, 2 };
            Line l3 = new Line() { X1 = 320, X2 = 60, Y1 = 70, Y2 = 70 };
            l3.Stroke = Brushes.Black;
            l3.StrokeThickness = 1.5;
            Line l4 = new Line() { X1 = 60, X2 = 60, Y1 = 70, Y2 = 80 };
            l4.Stroke = Brushes.Black;
            l4.StrokeThickness = 1.5;
            Line l5 = new Line() { X1 = 60, X2 = 20, Y1 = 80, Y2 = 80 };
            l5.Stroke = Brushes.Black;
            l5.StrokeThickness = 1.5;
            Line l6 = new Line() { X1 = 20, X2 = 20, Y1 = 80, Y2 = 20 };
            l6.Stroke = Brushes.Black;
            l6.StrokeThickness = 1.5;
            Line l7 = new Line() { X1 = 20, X2 = 315, Y1 = 65, Y2 = 65 };
            l7.Stroke = Brushes.Black;
            l7.StrokeThickness = 1.5;
            Line l8 = new Line() { X1 = 25, X2 = 160, Y1 = 25, Y2 = 25 };
            l8.Stroke = Brushes.Red;
            l8.StrokeThickness = 1.5;
            Line l9 = new Line() { X1 = 25, X2 = 25, Y1 = 25, Y2 = 65 };
            l9.Stroke = Brushes.Red;
            l9.StrokeThickness = 1.5;
            canvas.Children.Add(l1);
            canvas.Children.Add(l2);
            canvas.Children.Add(l3);
            canvas.Children.Add(l4);
            canvas.Children.Add(l5);
            canvas.Children.Add(l6);
            //canvas.Children.Add(l7);
            canvas.Children.Add(l8);
            canvas.Children.Add(l9);
            for (int i = 0; i < 12; i++)
            {
                Line l0 = new Line() { X1 = 60 + i * 20, X2 = 60 + i * 20, Y1 = 22, Y2 = 66 };
                l0.Stroke = Brushes.Black;
                l0.StrokeThickness = 1.5;
                canvas.Children.Add(l0);
            }
            DimHorizontalText(canvas, 25, 17, 1, 135, 11, 8, 4, "L");
            DimVerticalText(canvas, 35, 25, 1, 40, 11, 8, 4, "La");
        }
        public static void DrawEndAddTopBar(Canvas canvas, bool check)
        {
            canvas.Children.Clear();
            SolidColorBrush colorSelected = Brushes.Transparent;
            if (!check)
            {
                colorSelected = Brushes.Gainsboro;
            }
            else
            {
                colorSelected = Brushes.Transparent;
            }
            Rectangle r = new Rectangle() { Width = 330, Height = 90 };
            r.Fill = colorSelected;
            canvas.Children.Add(r);
            Line l1 = new Line() { X1 = 20, X2 = 320, Y1 = 20, Y2 = 20 };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = 1.5;
            Line l2 = new Line() { X1 = 20, X2 = 20, Y1 = 20, Y2 = 70 };
            l2.Stroke = Brushes.Black;
            l2.StrokeThickness = 1.5;
            l2.StrokeDashArray = new DoubleCollection() { 5, 2 };
            Line l6 = new Line() { X1 = 320, X2 = 320, Y1 = 20, Y2 = 80 };
            l6.Stroke = Brushes.Black;
            l6.StrokeThickness = 1.5;
            Line l3 = new Line() { X1 = 320, X2 = 280, Y1 = 80, Y2 = 80 };
            l3.Stroke = Brushes.Black;
            l3.StrokeThickness = 1.5;
            Line l4 = new Line() { X1 = 280, X2 = 280, Y1 = 80, Y2 = 70 };
            l4.Stroke = Brushes.Black;
            l4.StrokeThickness = 1.5;
            Line l5 = new Line() { X1 = 280, X2 = 20, Y1 = 70, Y2 = 70 };
            l5.Stroke = Brushes.Black;
            l5.StrokeThickness = 1.5;
            Line l8 = new Line() { X1 = 315, X2 = 160, Y1 = 25, Y2 = 25 };
            l8.Stroke = Brushes.Red;
            l8.StrokeThickness = 1.5;
            Line l9 = new Line() { X1 = 315, X2 = 315, Y1 = 25, Y2 = 65 };
            l9.Stroke = Brushes.Red;
            l9.StrokeThickness = 1.5;
            canvas.Children.Add(l1);
            canvas.Children.Add(l2);
            canvas.Children.Add(l3);
            canvas.Children.Add(l4);
            canvas.Children.Add(l5);
            canvas.Children.Add(l6);
            canvas.Children.Add(l8);
            canvas.Children.Add(l9);
            for (int i = 0; i < 12; i++)
            {
                Line l0 = new Line() { X1 = 60 + i * 20, X2 = 60 + i * 20, Y1 = 22, Y2 = 66 };
                l0.Stroke = Brushes.Black;
                l0.StrokeThickness = 1.5;
                canvas.Children.Add(l0);
            }
            DimHorizontalText(canvas, 160, 17, 1, 155, 11, 8, 4, "L");
            DimVerticalText(canvas, 305, 25, 1, 40, 11, 8, 4, "La");
        }
        public static void DrawMidAddTopBar(Canvas canvas)
        {
            canvas.Children.Clear();

            Line l1 = new Line() { X1 = 20, X2 = 20, Y1 = 20, Y2 = 70 };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = 1.5;
            l1.StrokeDashArray = new DoubleCollection() { 5, 2 };
            Line l2 = new Line() { X1 = 340, X2 = 340, Y1 = 20, Y2 = 70 };
            l2.Stroke = Brushes.Black;
            l2.StrokeThickness = 1.5;
            l2.StrokeDashArray = new DoubleCollection() { 5, 2 };
            Line l3 = new Line() { X1 = 20, X2 = 340, Y1 = 20, Y2 = 20 };
            l3.Stroke = Brushes.Black;
            l3.StrokeThickness = 1.5;
            Line l4 = new Line() { X1 = 340, X2 = 200, Y1 = 70, Y2 = 70 };
            l4.Stroke = Brushes.Black;
            l4.StrokeThickness = 1.5;
            Line l5 = new Line() { X1 = 200, X2 = 200, Y1 = 70, Y2 = 80 };
            l5.Stroke = Brushes.Black;
            l5.StrokeThickness = 1.5;
            Line l6 = new Line() { X1 = 200, X2 = 160, Y1 = 80, Y2 = 80 };
            l6.Stroke = Brushes.Black;
            l6.StrokeThickness = 1.5;
            Line l7 = new Line() { X1 = 160, X2 = 160, Y1 = 80, Y2 = 70 };
            l7.Stroke = Brushes.Black;
            l7.StrokeThickness = 1.5;
            Line l8 = new Line() { X1 = 160, X2 = 20, Y1 = 70, Y2 = 70 };
            l8.Stroke = Brushes.Black;
            l8.StrokeThickness = 1.5;
            Line l9 = new Line() { X1 = 70, X2 = 290, Y1 = 25, Y2 = 25 };
            l9.Stroke = Brushes.Red;
            l9.StrokeThickness = 1.5;
            canvas.Children.Add(l1);
            canvas.Children.Add(l2);
            canvas.Children.Add(l3);
            canvas.Children.Add(l4);
            canvas.Children.Add(l5);
            canvas.Children.Add(l6);
            canvas.Children.Add(l7);
            canvas.Children.Add(l8);
            canvas.Children.Add(l9);
            for (int i = 0; i < 7; i++)
            {
                Line l0 = new Line() { X1 = 200 + i * 20, X2 = 200 + i * 20, Y1 = 22, Y2 = 66 };
                l0.Stroke = Brushes.Black;
                l0.StrokeThickness = 1.5;
                canvas.Children.Add(l0);
            }
            for (int i = 0; i < 7; i++)
            {
                Line l0 = new Line() { X1 = 160 - i * 20, X2 = 160 - i * 20, Y1 = 22, Y2 = 66 };
                l0.Stroke = Brushes.Black;
                l0.StrokeThickness = 1.5;
                canvas.Children.Add(l0);
            }
            DimHorizontalText(canvas, 200, 17, 1, 90, 11, 8, 4, "L2");
            DimHorizontalText(canvas, 70, 17, 1, 90, 11, 8, 4, "L1");

        }
        public static void DrawStartAddBottomBar(Canvas canvas, bool check)
        {
            canvas.Children.Clear();
            SolidColorBrush colorSelected = Brushes.Transparent;
            if (!check)
            {
                colorSelected = Brushes.Gainsboro;
            }
            else
            {
                colorSelected = Brushes.Transparent;
            }
            Rectangle r = new Rectangle() { Width = 330, Height = 90 };
            r.Fill = colorSelected;
            canvas.Children.Add(r);
            Line l1 = new Line() { X1 = 20, X2 = 320, Y1 = 20, Y2 = 20 };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = 1.5;
            Line l2 = new Line() { X1 = 320, X2 = 320, Y1 = 20, Y2 = 70 };
            l2.Stroke = Brushes.Black;
            l2.StrokeThickness = 1.5;
            l2.StrokeDashArray = new DoubleCollection() { 5, 2 };
            Line l3 = new Line() { X1 = 320, X2 = 60, Y1 = 70, Y2 = 70 };
            l3.Stroke = Brushes.Black;
            l3.StrokeThickness = 1.5;
            Line l4 = new Line() { X1 = 60, X2 = 60, Y1 = 70, Y2 = 80 };
            l4.Stroke = Brushes.Black;
            l4.StrokeThickness = 1.5;
            Line l5 = new Line() { X1 = 60, X2 = 20, Y1 = 80, Y2 = 80 };
            l5.Stroke = Brushes.Black;
            l5.StrokeThickness = 1.5;
            Line l6 = new Line() { X1 = 20, X2 = 20, Y1 = 80, Y2 = 20 };
            l6.Stroke = Brushes.Black;
            l6.StrokeThickness = 1.5;
            Line l7 = new Line() { X1 = 20, X2 = 315, Y1 = 65, Y2 = 65 };
            l7.Stroke = Brushes.Black;
            l7.StrokeThickness = 1.5;
            Line l8 = new Line() { X1 = 25, X2 = 160, Y1 = 65, Y2 = 65 };
            l8.Stroke = Brushes.Red;
            l8.StrokeThickness = 1.5;
            Line l9 = new Line() { X1 = 25, X2 = 25, Y1 = 25, Y2 = 65 };
            l9.Stroke = Brushes.Red;
            l9.StrokeThickness = 1.5;
            canvas.Children.Add(l1);
            canvas.Children.Add(l2);
            canvas.Children.Add(l3);
            canvas.Children.Add(l4);
            canvas.Children.Add(l5);
            canvas.Children.Add(l6);
            //canvas.Children.Add(l7);
            canvas.Children.Add(l8);
            canvas.Children.Add(l9);
            for (int i = 0; i < 12; i++)
            {
                Line l0 = new Line() { X1 = 60 + i * 20, X2 = 60 + i * 20, Y1 = 22, Y2 = 66 };
                l0.Stroke = Brushes.Black;
                l0.StrokeThickness = 1.5;
                canvas.Children.Add(l0);
            }
            DimHorizontalText(canvas, 25, 17, 1, 135, 11, 8, 4, "L");
            DimVerticalText(canvas, 45, 25, 1, 40, 11, 8, 4, "La");
        }
        public static void DrawEndAddBottomBar(Canvas canvas, bool check)
        {
            canvas.Children.Clear();
            SolidColorBrush colorSelected = Brushes.Transparent;
            if (!check)
            {
                colorSelected = Brushes.Gainsboro;
            }
            else
            {
                colorSelected = Brushes.Transparent;
            }
            Rectangle r = new Rectangle() { Width = 330, Height = 90 };
            r.Fill = colorSelected;
            canvas.Children.Add(r);
            Line l1 = new Line() { X1 = 20, X2 = 320, Y1 = 20, Y2 = 20 };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = 1.5;
            Line l2 = new Line() { X1 = 20, X2 = 20, Y1 = 20, Y2 = 70 };
            l2.Stroke = Brushes.Black;
            l2.StrokeThickness = 1.5;
            l2.StrokeDashArray = new DoubleCollection() { 5, 2 };
            Line l6 = new Line() { X1 = 320, X2 = 320, Y1 = 20, Y2 = 80 };
            l6.Stroke = Brushes.Black;
            l6.StrokeThickness = 1.5;
            Line l3 = new Line() { X1 = 320, X2 = 280, Y1 = 80, Y2 = 80 };
            l3.Stroke = Brushes.Black;
            l3.StrokeThickness = 1.5;
            Line l4 = new Line() { X1 = 280, X2 = 280, Y1 = 80, Y2 = 70 };
            l4.Stroke = Brushes.Black;
            l4.StrokeThickness = 1.5;
            Line l5 = new Line() { X1 = 280, X2 = 20, Y1 = 70, Y2 = 70 };
            l5.Stroke = Brushes.Black;
            l5.StrokeThickness = 1.5;
            Line l8 = new Line() { X1 = 315, X2 = 160, Y1 = 65, Y2 = 65 };
            l8.Stroke = Brushes.Red;
            l8.StrokeThickness = 1.5;
            Line l9 = new Line() { X1 = 315, X2 = 315, Y1 = 25, Y2 = 65 };
            l9.Stroke = Brushes.Red;
            l9.StrokeThickness = 1.5;
            canvas.Children.Add(l1);
            canvas.Children.Add(l2);
            canvas.Children.Add(l3);
            canvas.Children.Add(l4);
            canvas.Children.Add(l5);
            canvas.Children.Add(l6);
            canvas.Children.Add(l8);
            canvas.Children.Add(l9);
            for (int i = 0; i < 12; i++)
            {
                Line l0 = new Line() { X1 = 60 + i * 20, X2 = 60 + i * 20, Y1 = 22, Y2 = 66 };
                l0.Stroke = Brushes.Black;
                l0.StrokeThickness = 1.5;
                canvas.Children.Add(l0);
            }
            DimHorizontalText(canvas, 160, 17, 1, 155, 11, 8, 4, "L");
            DimVerticalText(canvas, 305, 25, 1, 40, 11, 8, 4, "La");
        }
        public static void DrawMidAddBottomBar(Canvas canvas)
        {
            canvas.Children.Clear();

            Line l1 = new Line() { X1 = 20, X2 = 340, Y1 = 20, Y2 = 20 };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = 1.5;
            Line l2 = new Line() { X1 = 340, X2 = 340, Y1 = 20, Y2 = 80 };
            l2.Stroke = Brushes.Black;
            l2.StrokeThickness = 1.5;
            Line l3 = new Line() { X1 = 340, X2 = 300, Y1 = 80, Y2 = 80 };
            l3.Stroke = Brushes.Black;
            l3.StrokeThickness = 1.5;
            Line l4 = new Line() { X1 = 300, X2 = 300, Y1 = 80, Y2 = 70 };
            l4.Stroke = Brushes.Black;
            l4.StrokeThickness = 1.5;
            Line l5 = new Line() { X1 = 300, X2 = 60, Y1 = 70, Y2 = 70 };
            l5.Stroke = Brushes.Black;
            l5.StrokeThickness = 1.5;
            Line l6 = new Line() { X1 = 60, X2 = 60, Y1 = 70, Y2 = 80 };
            l6.Stroke = Brushes.Black;
            l6.StrokeThickness = 1.5;
            Line l7 = new Line() { X1 = 60, X2 = 20, Y1 = 80, Y2 = 80 };
            l7.Stroke = Brushes.Black;
            l7.StrokeThickness = 1.5;
            Line l8 = new Line() { X1 = 20, X2 = 20, Y1 = 80, Y2 = 20 };
            l8.Stroke = Brushes.Black;
            l8.StrokeThickness = 1.5;
            Line l9 = new Line() { X1 = 70, X2 = 290, Y1 = 65, Y2 = 65 };
            l9.Stroke = Brushes.Red;
            l9.StrokeThickness = 1.5;
            Line l10 = new Line() { X1 = 180, X2 = 180, Y1 = 15, Y2 = 85 };
            l10.Stroke = Brushes.Black;
            l10.StrokeThickness = 1.5;
            l10.StrokeDashArray = new DoubleCollection() { 10, 5 };
            canvas.Children.Add(l1);
            canvas.Children.Add(l2);
            canvas.Children.Add(l3);
            canvas.Children.Add(l4);
            canvas.Children.Add(l5);
            canvas.Children.Add(l6);
            canvas.Children.Add(l7);
            canvas.Children.Add(l8);
            canvas.Children.Add(l9);
            canvas.Children.Add(l10);
            for (int i = 0; i < 7; i++)
            {
                Line l0 = new Line() { X1 = 200 + i * 20, X2 = 200 + i * 20, Y1 = 22, Y2 = 66 };
                l0.Stroke = Brushes.Black;
                l0.StrokeThickness = 1.5;
                canvas.Children.Add(l0);
            }
            for (int i = 0; i < 7; i++)
            {
                Line l0 = new Line() { X1 = 160 - i * 20, X2 = 160 - i * 20, Y1 = 22, Y2 = 66 };
                l0.Stroke = Brushes.Black;
                l0.StrokeThickness = 1.5;
                canvas.Children.Add(l0);
            }
            DimHorizontalText(canvas, 180, 95, 1, 110, 11, 8, 4, "L2");
            DimHorizontalText(canvas, 70, 95, 1, 110, 11, 8, 4, "L1");

        }
        #region Item
        public static void DrawLayerBar(Canvas canvas, double left, double top, double scale, double b, double c, double ds, double d, int n, SolidColorBrush solidColorBrush)
        {
            double a = (b - 2 * c - 2 * ds - d) / n;
            double r = (ds + d) / (2);
            for (int i = 0; i <= n; i++)
            {
                DrawOneBarSection(canvas, left + (c + r + ds / 2) / scale + i * a / scale, top, scale, d, solidColorBrush);
            }
        }
        public static void DrawLayerBarTag(Canvas canvas, double left, double top, double scale, double b, double c, double ds, double d, int n, double tagV, double tagH, string type, SolidColorBrush colorbar, SolidColorBrush colortag, bool updown)
        {
            double a = (b - 2 * c - 2 * ds - d) / (n - 1);
            double r = (ds + d) / (2);
            for (int i = 0; i < n; i++)
            {
                DrawOneBarSection(canvas, left + (c + r + ds / 2) / scale + i * a / scale, top, scale, d, colorbar);
                if (updown)
                {
                    DrawOneTagVerticalUp(canvas, left + (c + r + ds / 2) / scale + i * a / scale, top, scale, tagV, colortag);
                }
                else
                {
                    DrawOneTagVerticalDown(canvas, left + (c + r + ds / 2) / scale + i * a / scale, top, scale, tagV, colortag);
                }
            }
            if (updown)
            {
                Line l = new Line() { X1 = left + (c + r + ds / 2) / scale, X2 = left + b / scale + tagH, Y1 = top - tagV, Y2 = top - tagV };
                l.Stroke = colortag;
                l.StrokeThickness = 0.5;
                canvas.Children.Add(l);
                DrawTagBarNumber(canvas, left + b / scale + tagH + 11, top - tagV, scale, 11, "--", n, d, type, colorbar);
            }
            else
            {
                Line l = new Line() { X1 = left + (c + r + ds / 2) / scale, X2 = left + b / scale + tagH, Y1 = top + tagV, Y2 = top + tagV };
                l.Stroke = colortag;
                l.StrokeThickness = 0.5;
                canvas.Children.Add(l);
                DrawTagBarNumber(canvas, left + b / scale + tagH + 11, top + tagV, scale, 11, "--", n, d, type, colorbar);
            }
        }
        public static void DrawLayerBarTagAddTop(Canvas canvas, double left, double top, double scale, double b, double c, double ds, double d, int n, double tagV, double tagH, string type, SolidColorBrush colorbar, SolidColorBrush colortag, bool updown)
        {
            if (n == 1)
            {
                DrawOneBarSection(canvas, left + (b / 2) / scale, top, scale, d, colorbar);
                if (updown)
                {
                    DrawOneTagVerticalUp(canvas, left + (b / 2) / scale, top, scale, tagV, colortag);
                    Line l = new Line() { X1 = left + (b / 2) / scale, X2 = left + b / scale + tagH, Y1 = top - tagV, Y2 = top - tagV };
                    l.Stroke = colortag;
                    l.StrokeThickness = 0.5;
                    canvas.Children.Add(l);
                    DrawTagBarNumber(canvas, left + b / scale + tagH + 11, top - tagV, scale, 11, "--", n, d, type, colorbar);
                }
                else
                {
                    DrawOneTagVerticalDown(canvas, left + (b / 2) / scale, top, scale, tagV, colortag);
                    Line l = new Line() { X1 = left + (b / 2) / scale, X2 = left + b / scale + tagH, Y1 = top + tagV, Y2 = top + tagV };
                    l.Stroke = colortag;
                    l.StrokeThickness = 0.5;
                    canvas.Children.Add(l);
                    DrawTagBarNumber(canvas, left + b / scale + tagH + 11, top + tagV, scale, 11, "--", n, d, type, colorbar);
                }
            }
        }
        public static void DrawLayerMainBar(Canvas canvas, double left, double top, double scale, double b, double c, double ds, double d, int n, SolidColorBrush solidColorBrush)
        {
            double a = (b - 2 * c - 2 * ds - d) / n;
            double r = (ds + d) / (2 * scale);
            for (int i = 0; i <= n; i++)
            {
                DrawOneBarSection(canvas, left + (c + r + ds / 2) / scale + i * a / scale, top, scale, d, solidColorBrush);
            }
        }
        public static void DrawLayerMainBarTag(Canvas canvas, double left, double top, double scale, double b, double c, double ds, double d, int n, double tagV, double tagH, SolidColorBrush colorbar, SolidColorBrush colortag, bool updown)
        {
            double a = (b - 2 * c - 2 * ds - d) / n;
            double r = (ds + d) / (2 * scale);
            for (int i = 0; i <= n; i++)
            {
                DrawOneBarSection(canvas, left + (c + r + ds / 2) / scale + i * a / scale, top, scale, d, colorbar);
                if (updown)
                {
                    DrawOneTagVerticalUp(canvas, left + (c + r + ds / 2) / scale + i * a / scale, top, scale, tagV, colortag);
                }
                else
                {
                    DrawOneTagVerticalDown(canvas, left + (c + r + ds / 2) / scale + i * a / scale, top, scale, tagV, colortag);
                }
            }
            if (updown)
            {
                Line l = new Line()
                {
                    X1 = left + (c + r + ds / 2) / scale,
                    X2 = left + b + tagH,
                    Y1 = top - tagV,
                    Y2 = top - tagV
                };
                l.Stroke = colortag;
                l.StrokeThickness = 0.5;
                canvas.Children.Add(l);
                DrawBarNumber(canvas, left + b + tagH + 11, top - tagV, scale, 11, "--", colortag);
            }
            else
            {
                Line l = new Line()
                {
                    X1 = left + (c + r + ds / 2) / scale,
                    X2 = left + b + tagH,
                    Y1 = top + tagV,
                    Y2 = top + tagV
                };
                l.Stroke = colortag;
                l.StrokeThickness = 0.5;
                canvas.Children.Add(l);
                DrawBarNumber(canvas, left + b + tagH + 11, top + tagV, scale, 11, "--", colortag);
            }
        }
        public static void DrawLayerMainBarTop(Canvas canvas, double left, double top, double scale, double b, double c, double ds, double d, int n, SolidColorBrush solidColorBrush)
        {
            double a = (b - 2 * c - 2 * ds - d) / n;
            double r = (ds + d) / (2 * scale);
            for (int i = 0; i <= n; i++)
            {
                DrawOneBarSection(canvas, left + (c + r + ds / 2) / scale + i * a / scale, top + (c + r + ds / 2) / scale, scale, d, solidColorBrush);
            }
        }
        public static void DrawLayerMainBarTopTag(Canvas canvas, double left, double top, double scale, double b, double c, double ds, double d, int n, double tagV, double tagH, SolidColorBrush solidColorBrush, bool updown)
        {
            double a = (b - 2 * c - 2 * ds - d) / n;
            double r = (ds + d) / (2 * scale);
            for (int i = 0; i <= n; i++)
            {
                DrawOneBarSection(canvas, left + (c + r + ds / 2) / scale + i * a / scale, top + (c + r + ds / 2) / scale, scale, d, solidColorBrush);
                if (updown)
                {
                    DrawOneTagVerticalUp(canvas, left + (c + r + ds / 2) / scale + i * a / scale, top + (c + r + ds / 2) / scale, scale, tagV, solidColorBrush);
                }
                else
                {
                    DrawOneTagVerticalDown(canvas, left + (c + r + ds / 2) / scale + i * a / scale, top + (c + r + ds / 2) / scale, scale, tagV, solidColorBrush);
                }

            }
            if (updown)
            {
                Line l = new Line()
                {
                    X1 = left + (c + r + ds / 2) / scale,
                    X2 = left + b + tagH,
                    Y1 = top + (c + r + ds / 2) / scale - tagV,
                    Y2 = top + (c + r + ds / 2) / scale - tagV
                };
                l.Stroke = solidColorBrush;
                l.StrokeThickness = 0.5;
                canvas.Children.Add(l);
            }
            else
            {
                Line l = new Line()
                {
                    X1 = left + (c + r + ds / 2) / scale,
                    X2 = left + b + tagH,
                    Y1 = top - (c + r + ds / 2) / scale + tagV,
                    Y2 = top - (c + r + ds / 2) / scale + tagV
                };
                l.Stroke = solidColorBrush;
                l.StrokeThickness = 0.5;
                canvas.Children.Add(l);
            }


        }
        public static void DrawLayerMainBarBottom(Canvas canvas, double left, double top, double scale, double b, double h, double c, double ds, double d, int n, SolidColorBrush solidColorBrush)
        {
            double a = (b - 2 * c - 2 * ds - d) / n;
            double r = (ds + d) / (2 * scale);
            for (int i = 0; i <= n; i++)
            {
                DrawOneBarSection(canvas, left + (c + r + ds / 2) / scale + i * a / scale, top + (h - c - r - ds / 2) / scale, scale, d, solidColorBrush);
            }
        }
        public static void DrawStirrup(Canvas canvas, double left, double top, double scale, double b, double h, double c, double ds, double d, SolidColorBrush solidColorBrush)
        {

            double r = (ds + d) / (2);
            double t = (Math.Sqrt(2) / 2);
            Line l1 = new Line() { X1 = left + (c + ds / 2) / scale, X2 = left + (c + ds / 2) / scale, Y1 = top + (c + r) / scale, Y2 = top + (h - c - r) / scale };
            l1.Stroke = solidColorBrush;
            l1.StrokeThickness = ds / scale;
            Line l2 = new Line() { X1 = left + (b - c - ds / 2) / scale, X2 = left + (b - c - ds / 2) / scale, Y1 = top + (c + r) / scale, Y2 = top + (h - c - r) / scale };
            l2.Stroke = solidColorBrush;
            l2.StrokeThickness = ds / scale;
            Line l3 = new Line() { X1 = left + (c + r) / scale, X2 = left + (b - c - r) / scale, Y1 = top + (c + ds / 2) / scale, Y2 = top + (c + ds / 2) / scale };
            l3.Stroke = solidColorBrush;
            l3.StrokeThickness = ds / scale;
            Line l4 = new Line() { X1 = left + (c + r) / scale, X2 = left + (b - c - r) / scale, Y1 = top + (h - c - ds / 2) / scale, Y2 = top + (h - c - ds / 2) / scale };
            l4.Stroke = solidColorBrush;
            l4.StrokeThickness = ds / scale;
            canvas.Children.Add(l1);
            canvas.Children.Add(l2);
            canvas.Children.Add(l3);
            canvas.Children.Add(l4);
            DrawQualifyArc(canvas, left + (c + r + ds / 2) / scale, top + (c + r + ds / 2) / scale, scale, ds, d, 3, solidColorBrush);
            DrawQualifyArc(canvas, left + (b - c - r - ds / 2) / scale, top + (c + r + ds / 2) / scale, scale, ds, d, 4, solidColorBrush);
            DrawQualifyArc(canvas, left + (b - c - r - ds / 2) / scale, top + (h - c - r - ds / 2) / scale, scale, ds, d, 1, solidColorBrush);
            DrawQualifyArc(canvas, left + (c + r + ds / 2) / scale, top + (h - c - r - ds / 2) / scale, scale, ds, d, 2, solidColorBrush);
            DrawHaflQualifyArc(canvas, left + (c + r + ds / 2) / scale, top + (c + r + ds / 2) / scale, scale, ds, d, 4, solidColorBrush);
            DrawHaflQualifyArc(canvas, left + (c + r + ds / 2) / scale, top + (c + r + ds / 2) / scale, scale, ds, d, 7, solidColorBrush);
            Line l5 = new Line() { X1 = left + (c + r + ds / 2) / scale - t * r / scale, X2 = left + (c + r + ds / 2) / scale - t * r / scale + (5 * ds) / scale, Y1 = top + (c + r + ds / 2) / scale + t * r / scale, Y2 = top + (c + r + ds / 2) / scale + t * r / scale + (5 * ds) / scale };
            l5.Stroke = solidColorBrush;
            l5.StrokeThickness = ds / scale;
            canvas.Children.Add(l5);
            Line l6 = new Line() { X1 = left + (c + r + ds / 2) / scale + t * r / scale, X2 = left + (c + r + ds / 2) / scale + t * r / scale + (5 * ds) / scale, Y1 = top + (c + r + ds / 2) / scale - t * r / scale, Y2 = top + (c + r + ds / 2) / scale - t * r / scale + (5 * ds) / scale };
            l6.Stroke = solidColorBrush;
            l6.StrokeThickness = ds / scale;
            canvas.Children.Add(l6);
        }
        public static void DrawStirrupDashArray(Canvas canvas, double left, double top, double scale, double b, double h, double c, double ds, double d, SolidColorBrush solidColorBrush)
        {

            double r = (ds + d) / (2);
            double t = (Math.Sqrt(2) / 2);
            Line l1 = new Line() { X1 = left + (c + ds / 2) / scale, X2 = left + (c + ds / 2) / scale, Y1 = top + (c + r) / scale, Y2 = top + (h - c - r) / scale };
            l1.Stroke = solidColorBrush;
            l1.StrokeThickness = ds / scale;
            l1.StrokeDashArray = new DoubleCollection() { 5, 2 };
            Line l2 = new Line() { X1 = left + (b - c - ds / 2) / scale, X2 = left + (b - c - ds / 2) / scale, Y1 = top + (c + r) / scale, Y2 = top + (h - c - r) / scale };
            l2.Stroke = solidColorBrush;
            l2.StrokeThickness = ds / scale;
            l2.StrokeDashArray = new DoubleCollection() { 5, 2 };
            Line l3 = new Line() { X1 = left + (c + r) / scale, X2 = left + (b - c - r) / scale, Y1 = top + (c + ds / 2) / scale, Y2 = top + (c + ds / 2) / scale };
            l3.Stroke = solidColorBrush;
            l3.StrokeThickness = ds / scale;
            l3.StrokeDashArray = new DoubleCollection() { 5, 2 };
            Line l4 = new Line() { X1 = left + (c + r) / scale, X2 = left + (b - c - r) / scale, Y1 = top + (h - c - ds / 2) / scale, Y2 = top + (h - c - ds / 2) / scale };
            l4.Stroke = solidColorBrush;
            l4.StrokeThickness = ds / scale;
            l4.StrokeDashArray = new DoubleCollection() { 5, 2 };
            canvas.Children.Add(l1);
            canvas.Children.Add(l2);
            canvas.Children.Add(l3);
            canvas.Children.Add(l4);
            DrawQualifyArc(canvas, left + (c + r + ds / 2) / scale, top + (c + r + ds / 2) / scale, scale, ds, d, 3, solidColorBrush);
            DrawQualifyArc(canvas, left + (b - c - r - ds / 2) / scale, top + (c + r + ds / 2) / scale, scale, ds, d, 4, solidColorBrush);
            DrawQualifyArc(canvas, left + (b - c - r - ds / 2) / scale, top + (h - c - r - ds / 2) / scale, scale, ds, d, 1, solidColorBrush);
            DrawQualifyArc(canvas, left + (c + r + ds / 2) / scale, top + (h - c - r - ds / 2) / scale, scale, ds, d, 2, solidColorBrush);
            DrawHaflQualifyArc(canvas, left + (c + r + ds / 2) / scale, top + (c + r + ds / 2) / scale, scale, ds, d, 4, solidColorBrush);
            DrawHaflQualifyArc(canvas, left + (c + r + ds / 2) / scale, top + (c + r + ds / 2) / scale, scale, ds, d, 7, solidColorBrush);
            Line l5 = new Line() { X1 = left + (c + r + ds / 2) / scale - t * r / scale, X2 = left + (c + r + ds / 2) / scale - t * r / scale + (5 * ds) / scale, Y1 = top + (c + r + ds / 2) / scale + t * r / scale, Y2 = top + (c + r + ds / 2) / scale + t * r / scale + (5 * ds) / scale };
            l5.Stroke = solidColorBrush;
            l5.StrokeThickness = ds / scale;
            l5.StrokeDashArray = new DoubleCollection() { 5, 2 };
            canvas.Children.Add(l5);
            Line l6 = new Line() { X1 = left + (c + r + ds / 2) / scale + t * r / scale, X2 = left + (c + r + ds / 2) / scale + t * r / scale + (5 * ds) / scale, Y1 = top + (c + r + ds / 2) / scale - t * r / scale, Y2 = top + (c + r + ds / 2) / scale - t * r / scale + (5 * ds) / scale };
            l6.Stroke = solidColorBrush;
            l6.StrokeThickness = ds / scale;
            l6.StrokeDashArray = new DoubleCollection() { 5, 2 };
            canvas.Children.Add(l6);
        }
        public static void DrawHook(Canvas canvas, double left, double top, double scale, double b, double c, double ds, double d, double angle, SolidColorBrush solidColorBrush)
        {
            double r = (ds + d) / (2);
            double t = (Math.Sqrt(2) / 2);
            Line l4 = new Line() { X1 = left + (c + r) / scale, X2 = left + (b - c - r) / scale, Y1 = top + (ds / 2 + d / 2) / scale, Y2 = top + (ds / 2 + d / 2) / scale };
            l4.Stroke = solidColorBrush;
            l4.StrokeThickness = ds / scale;
            canvas.Children.Add(l4);
            DrawQualifyArc(canvas, left + (b - c - r - ds / 2) / scale, top, scale, ds, d, 1, solidColorBrush);
            DrawQualifyArc(canvas, left + (c + r + ds / 2) / scale, top, scale, ds, d, 2, solidColorBrush);
            if (Math.Abs(angle - Math.PI / 2) < 1e-10)
            {
                Line l5 = new Line() { X1 = left + (c + ds / 2) / scale, X2 = left + (c + ds / 2) / scale, Y1 = top, Y2 = top - d / scale };
                l5.Stroke = solidColorBrush;
                l5.StrokeThickness = ds / scale;
                canvas.Children.Add(l5);
                Line l6 = new Line() { X1 = left + (b - c - ds / 2) / scale, X2 = left + (b - c - ds / 2) / scale, Y1 = top, Y2 = top - d / scale };
                l6.Stroke = solidColorBrush;
                l6.StrokeThickness = ds / scale;
                canvas.Children.Add(l6);
            }
            if (Math.Abs(angle - Math.PI) < 1e-10)
            {
                DrawQualifyArc(canvas, left + (c + r + ds / 2) / scale, top, scale, ds, d, 3, solidColorBrush);
                DrawQualifyArc(canvas, left + (b - c - r - ds / 2) / scale, top, scale, ds, d, 4, solidColorBrush);
                Line l5 = new Line() { X1 = left + (c + r + ds / 2) / scale, X2 = left + (c + r + ds / 2) / scale + d / scale, Y1 = top - (d / 2 + ds / 2) / scale, Y2 = top - (d / 2 + ds / 2) / scale };
                l5.Stroke = solidColorBrush;
                l5.StrokeThickness = ds / scale;
                canvas.Children.Add(l5);
                Line l6 = new Line() { X1 = left + (b - c - r - ds / 2) / scale, X2 = left + (b - c - r - ds / 2) / scale - d / scale, Y1 = top - (d / 2 + ds / 2) / scale, Y2 = top - (d / 2 + ds / 2) / scale };
                l6.Stroke = solidColorBrush;
                l6.StrokeThickness = ds / scale;
                canvas.Children.Add(l6);
            }
            if (Math.Abs(angle - 3 * Math.PI / 4) < 1e-10)
            {
                DrawHaflQualifyArc(canvas, left + (c + r + ds / 2) / scale, top, scale, ds, d, 5, solidColorBrush);
                DrawHaflQualifyArc(canvas, left + (b - c - r - ds / 2) / scale, top, scale, ds, d, 8, solidColorBrush);
                Line l5 = new Line() { X1 = left + (c + r + ds / 2) / scale - t * r / scale, X2 = left + (c + r + ds / 2) / scale - t * r / scale + d / (2 * scale), Y1 = top - t * r / scale, Y2 = top - t * r / scale - d / (2 * scale) };
                l5.Stroke = solidColorBrush;
                l5.StrokeThickness = ds / scale;
                canvas.Children.Add(l5);
                Line l6 = new Line() { X1 = left + (b - c - r - ds / 2) / scale + t * r / scale, X2 = left + (b - c - r - ds / 2) / scale + t * r / scale - d / (2 * scale), Y1 = top - t * r / scale, Y2 = top - t * r / scale - d / (2 * scale) };
                l6.Stroke = solidColorBrush;
                l6.StrokeThickness = ds / scale;
                canvas.Children.Add(l6);
            }

        }
        public static void DrawHookVertical(Canvas canvas, double left, double top, double scale, double b, double c, double ds, double d, double angle, SolidColorBrush solidColorBrush)
        {
            double r = (ds + d) / (2);
            double t = (Math.Sqrt(2) / 2);
            Line l4 = new Line() { Y1 = top + (c + r) / scale, Y2 = top + (b - c - r) / scale, X1 = left - (ds / 2 + d / 2) / scale, X2 = left - (ds / 2 + d / 2) / scale };
            l4.Stroke = solidColorBrush;
            l4.StrokeThickness = ds / scale;
            canvas.Children.Add(l4);
            DrawQualifyArc(canvas, left, top + (b - c - r - ds / 2) / scale, scale, ds, d, 2, solidColorBrush);
            DrawQualifyArc(canvas, left, top + (c + r + ds / 2) / scale, scale, ds, d, 3, solidColorBrush);
            if (Math.Abs(angle - Math.PI / 2) < 1e-10)
            {
                Line l5 = new Line() { Y1 = top + (c + ds / 2) / scale, Y2 = top + (c + ds / 2) / scale, X1 = left, X2 = left + d / scale };
                l5.Stroke = solidColorBrush;
                l5.StrokeThickness = ds / scale;
                canvas.Children.Add(l5);
                Line l6 = new Line() { Y1 = top + (b - c - ds / 2) / scale, Y2 = top + (b - c - ds / 2) / scale, X1 = left, X2 = left + d / scale };
                l6.Stroke = solidColorBrush;
                l6.StrokeThickness = ds / scale;
                canvas.Children.Add(l6);
            }
            if (Math.Abs(angle - Math.PI) < 1e-10)
            {
                DrawQualifyArc(canvas, left, top + (c + r + ds / 2) / scale, scale, ds, d, 4, solidColorBrush);
                DrawQualifyArc(canvas, left, top + (b - c - r - ds / 2) / scale, scale, ds, d, 1, solidColorBrush);
                Line l5 = new Line() { Y1 = top + (c + r + ds / 2) / scale, Y2 = top + (c + r + ds / 2) / scale + d / scale, X1 = left + (d / 2 + ds / 2) / scale, X2 = left + (d / 2 + ds / 2) / scale };
                l5.Stroke = solidColorBrush;
                l5.StrokeThickness = ds / scale;
                canvas.Children.Add(l5);
                Line l6 = new Line() { Y1 = top + (b - c - r - ds / 2) / scale, Y2 = top + (b - c - r - ds / 2) / scale - d / scale, X1 = left + (d / 2 + ds / 2) / scale, X2 = left + (d / 2 + ds / 2) / scale };
                l6.Stroke = solidColorBrush;
                l6.StrokeThickness = ds / scale;
                canvas.Children.Add(l6);
            }
            if (Math.Abs(angle - 3 * Math.PI / 4) < 1e-10)
            {
                DrawHaflQualifyArc(canvas, left, top + (c + r + ds / 2) / scale, scale, ds, d, 7, solidColorBrush);
                DrawHaflQualifyArc(canvas, left, top + (b - c - r - ds / 2) / scale, scale, ds, d, 2, solidColorBrush);
                Line l5 = new Line() { Y1 = top + (c + r + ds / 2) / scale - t * r / scale, Y2 = top + (c + r + ds / 2) / scale - t * r / scale + d / (2 * scale), X1 = left + t * r / scale, X2 = left + t * r / scale + d / (2 * scale) };
                l5.Stroke = solidColorBrush;
                l5.StrokeThickness = ds / scale;
                canvas.Children.Add(l5);
                Line l6 = new Line() { Y1 = top + (b - c - r - ds / 2) / scale + t * r / scale, Y2 = top + (b - c - r - ds / 2) / scale + t * r / scale - d / (2 * scale), X1 = left + t * r / scale, X2 = left + t * r / scale + d / (2 * scale) };
                l6.Stroke = solidColorBrush;
                l6.StrokeThickness = ds / scale;
                canvas.Children.Add(l6);
            }

        }
        public static void DimText(Canvas canvas, double left, double top, double scale, int font, string t, double angle)
        {
            TextBlock text = new TextBlock();
            text.Text = t;
            text.FontSize = font;
            text.Foreground = Brushes.Black;
            text.LayoutTransform = new RotateTransform(angle, 25, 25);
            text.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text.Arrange(new Rect(text.DesiredSize));
            Canvas.SetTop(text, top - text.ActualHeight / 2);
            Canvas.SetLeft(text, left - text.ActualWidth / 2);
            canvas.Children.Add(text);
        }
        #endregion
        #region part
        public static void DrawQualifyArc(Canvas canvas, double left, double top, double scale, double ds, double d, int qualify, SolidColorBrush solidColorBrush)
        {
            ArcSegment arcSegment = new ArcSegment();
            arcSegment.Size = new Size((ds + d) / (2 * scale), (ds + d) / (2 * scale));
            PathFigure pthFigure = new PathFigure();
            switch (qualify)
            {
                case 1:
                    arcSegment.Point = new Point(left + (ds + d) / (2 * scale), top);
                    pthFigure.StartPoint = new Point(left, top + (ds + d) / (2 * scale));
                    break;
                case 2:
                    arcSegment.Point = new Point(left, top + (ds + d) / (2 * scale));
                    pthFigure.StartPoint = new Point(left - (ds + d) / (2 * scale), top);
                    break;
                case 3:
                    arcSegment.Point = new Point(left - (ds + d) / (2 * scale), top);
                    pthFigure.StartPoint = new Point(left, top - (ds + d) / (2 * scale));
                    break;
                case 4:
                    arcSegment.Point = new Point(left, top - (ds + d) / (2 * scale));
                    pthFigure.StartPoint = new Point(left + (ds + d) / (2 * scale), top);
                    break;
                default:
                    break;
            }
            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(arcSegment);
            pthFigure.Segments = myPathSegmentCollection;
            PathFigureCollection pthFigureCollection = new PathFigureCollection();
            pthFigureCollection.Add(pthFigure);

            PathGeometry pthGeometry = new PathGeometry();
            pthGeometry.Figures = pthFigureCollection;
            Path arcPath = new Path();

            arcPath.Stroke = solidColorBrush;
            arcPath.StrokeThickness = ds / scale;
            arcPath.Data = pthGeometry;
            canvas.Children.Add(arcPath);
        }
        public static void DrawHaflQualifyArc(Canvas canvas, double left, double top, double scale, double ds, double d, int qualify, SolidColorBrush solidColorBrush)
        {
            double r = (ds + d) / (2);
            double t = (Math.Sqrt(2) / 2);
            ArcSegment arcSegment = new ArcSegment();
            arcSegment.Size = new Size(r / scale, r / scale);
            PathFigure pthFigure = new PathFigure();
            switch (qualify)
            {
                case 1:
                    arcSegment.Point = new Point(left + r / (scale), top);
                    pthFigure.StartPoint = new Point(left + t * r / scale, top + t * r / scale);
                    break;
                case 2:
                    arcSegment.Point = new Point(left + t * r / scale, top + t * r / scale);
                    pthFigure.StartPoint = new Point(left, top + r / scale);
                    break;
                case 3:
                    arcSegment.Point = new Point(left, top + r / scale);
                    pthFigure.StartPoint = new Point(left - t * r / scale, top + t * r / scale);
                    break;
                case 4:
                    arcSegment.Point = new Point(left - t * r / scale, top + t * r / scale);
                    pthFigure.StartPoint = new Point(left - r / scale, top);
                    break;
                case 5:
                    arcSegment.Point = new Point(left - r / scale, top);
                    pthFigure.StartPoint = new Point(left - t * r / scale, top - t * r / scale);
                    break;
                case 6:
                    arcSegment.Point = new Point(left - t * r / scale, top - t * r / scale);
                    pthFigure.StartPoint = new Point(left, top - r / scale);
                    break;
                case 7:
                    arcSegment.Point = new Point(left, top - r / scale);
                    pthFigure.StartPoint = new Point(left + t * r / scale, top - t * r / scale);
                    break;
                case 8:
                    arcSegment.Point = new Point(left + t * r / scale, top - t * r / scale);
                    pthFigure.StartPoint = new Point(left + r / scale, top);
                    break;
                default:
                    break;
            }
            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(arcSegment);
            pthFigure.Segments = myPathSegmentCollection;
            PathFigureCollection pthFigureCollection = new PathFigureCollection();
            pthFigureCollection.Add(pthFigure);

            PathGeometry pthGeometry = new PathGeometry();
            pthGeometry.Figures = pthFigureCollection;
            Path arcPath = new Path();

            arcPath.Stroke = solidColorBrush;
            arcPath.StrokeThickness = ds / scale;
            arcPath.Data = pthGeometry;
            canvas.Children.Add(arcPath);

        }
        public static void DrawOneBarSection(Canvas canvas, double left, double top, double scale, double d, SolidColorBrush solidColorBrush)
        {
            Ellipse e = new Ellipse() { Width = d / scale, Height = d / scale };
            e.Fill = solidColorBrush;
            Canvas.SetTop(e, top - (d) / (2 * scale));
            Canvas.SetLeft(e, left - (d) / (2 * scale));
            canvas.Children.Add(e);

        }
        public static void DrawTextOnePileSection(Canvas canvas, double left, double top, int number, SolidColorBrush solidColorBrush)
        {
            TextBlock text = new TextBlock();
            text.Text = number + "";
            text.FontSize = 11;
            text.Foreground = solidColorBrush;
            text.FontFamily = new FontFamily("Tahoma");
            text.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text.Arrange(new Rect(text.DesiredSize));
            Canvas.SetTop(text, top - text.ActualHeight);
            Canvas.SetLeft(text, left - text.ActualWidth);
            canvas.Children.Add(text);
        }
        #endregion

        #region Tag
        public static void DrawOneTagVerticalUp(Canvas canvas, double left, double top, double scale, double dimV, SolidColorBrush solidColorBrush)
        {
            Line l = new Line()
            {
                X1 = left,
                X2 = left,
                Y1 = top,
                Y2 = top - dimV
            };
            l.Stroke = solidColorBrush;
            l.StrokeThickness = 0.5;
            canvas.Children.Add(l);
        }
        public static void DrawOneTagVerticalDown(Canvas canvas, double left, double top, double scale, double dimV, SolidColorBrush solidColorBrush)
        {
            Line l = new Line()
            {
                X1 = left,
                X2 = left,
                Y1 = top,
                Y2 = top + dimV
            };
            l.Stroke = solidColorBrush;
            l.StrokeThickness = 0.5;
            canvas.Children.Add(l);
        }
        public static void DrawBarNumber(Canvas canvas, double left, double top, double scale, int font, string number, SolidColorBrush solidColorBrush)
        {
            Ellipse e = new Ellipse() { Width = 2 * font, Height = 2 * font };
            e.Stroke = solidColorBrush;
            e.StrokeThickness = 0.5;
            Canvas.SetTop(e, top - font);
            Canvas.SetLeft(e, left - font);
            canvas.Children.Add(e);
            TextBlock text = new TextBlock();
            text.Text = number;
            text.FontSize = font;
            text.Foreground = solidColorBrush;
            text.FontFamily = new FontFamily("Tahoma");
            text.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text.Arrange(new Rect(text.DesiredSize));
            Canvas.SetTop(text, top - 4 * text.ActualHeight / 5);
            Canvas.SetLeft(text, left - text.ActualWidth);
            canvas.Children.Add(text);
        }
        public static void DrawTagBarNumber(Canvas canvas, double left, double top, double scale, int font, string number, int rebar, double diameter, string type, SolidColorBrush solidColorBrush)
        {
            DrawBarNumber(canvas, left, top, scale, font, number, solidColorBrush);
            //string t = rebar.ToString() + " " + type+" " + diameter.ToString();
            string t = rebar.ToString() + " " + type + " ";
            TextBlock text = new TextBlock();
            text.Text = t;
            text.FontSize = font;
            text.Foreground = solidColorBrush;
            text.FontFamily = new FontFamily("Tahoma");
            text.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text.Arrange(new Rect(text.DesiredSize));
            Canvas.SetTop(text, top - 4 * text.ActualHeight / 5);
            Canvas.SetLeft(text, left + font);
            canvas.Children.Add(text);
        }
        public static void DrawTagStirrup(Canvas canvas, double left, double top, double scale, double b, double c, double tagH, double distance, double diameter, SolidColorBrush solidColorBrush)
        {
            Line l = new Line() { X1 = left + b / scale - c / scale, X2 = left + b / scale + tagH, Y1 = top, Y2 = top };
            l.Stroke = solidColorBrush;
            l.StrokeThickness = 0.5;
            canvas.Children.Add(l);
            DrawBarNumber(canvas, left + b / scale + tagH + 11, top, scale, 11, "--", solidColorBrush);
            string t = " Ø " + diameter.ToString() + " @ " + distance.ToString();
            TextBlock text = new TextBlock();
            text.Text = t;
            text.FontSize = 11;
            text.Foreground = solidColorBrush;
            text.FontFamily = new FontFamily("Tahoma");
            text.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text.Arrange(new Rect(text.DesiredSize));
            Canvas.SetTop(text, top - 4 * text.ActualHeight / 5);
            Canvas.SetLeft(text, left + b / scale + tagH + 22);
            canvas.Children.Add(text);
        }
        public static void DrawLineLevel(Canvas canvas, double left, double top, double scale, int font, string level, double with)
        {
            Line l1 = new Line() { X1 = 0, X2 = with, Y1 = top, Y2 = top };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = 2;
            l1.StrokeDashArray = new DoubleCollection() { 10, 2, 4, 2 };
            canvas.Children.Add(l1);
            TextBlock text = new TextBlock();
            text.Text = level;
            text.FontSize = font;
            text.Foreground = Brushes.Black;
            text.FontFamily = new FontFamily("Tahoma");
            text.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text.Arrange(new Rect(text.DesiredSize));
            Canvas.SetTop(text, top - 1.5 * text.ActualHeight);
            Canvas.SetLeft(text, with - 2 * text.ActualWidth);
            canvas.Children.Add(text);
        }
        #endregion
        #region setting view
        public static void DrawPileLocation(Canvas canvas)
        {
            Rectangle r = new Rectangle() { Width = 130, Height = 155 };
            r.Stroke = Brushes.Black;
            r.StrokeThickness = 1;
            r.StrokeDashArray = new DoubleCollection() { 5, 2 };
            canvas.Children.Add(r);
            Rectangle r1 = new Rectangle() { Width = 110, Height = 130 };
            r1.Stroke = Brushes.Black;
            r1.StrokeThickness = 1;
            Canvas.SetTop(r1, 10);
            Canvas.SetLeft(r1, 10);
            canvas.Children.Add(r1);
            DrawCylindricalSectionDashArray(canvas, 35, 45, 1, 30);
            DrawCylindricalSectionDashArray(canvas, 95, 45, 1, 30);
            DrawCylindricalSectionDashArray(canvas, 35, 105, 1, 30);
            DrawCylindricalSectionDashArray(canvas, 95, 105, 1, 30);
            DimHorizontalText(canvas, 10, 85, 1, 25, 11, 8, 4, "Dis p-s");
            DimHorizontalText(canvas, 35, 85, 1, 60, 11, 8, 4, "Dis p-p");
            DimHorizontalText(canvas, 95, 85, 1, 25, 11, 8, 4, "Dis p-s");
        }
        public static void DrawLenthPile(Canvas canvas)
        {
            Rectangle r = new Rectangle() { Width = 170, Height = 155 };
            r.Stroke = Brushes.Black;
            r.StrokeThickness = 1;
            r.StrokeDashArray = new DoubleCollection() { 5, 2 };
            canvas.Children.Add(r);
            Rectangle r1 = new Rectangle() { Width = 150, Height = 30 };
            r1.Stroke = Brushes.Black;
            r1.StrokeThickness = 1;
            Canvas.SetTop(r1, 5);
            Canvas.SetLeft(r1, 10);
            canvas.Children.Add(r1);

            Rectangle r3 = new Rectangle() { Width = 40, Height = 135 };
            r3.Stroke = Brushes.Black;
            r3.StrokeThickness = 2;
            Canvas.SetTop(r3, 15);
            Canvas.SetLeft(r3, 30);
            r3.StrokeDashArray = new DoubleCollection() { 5, 2 };
            canvas.Children.Add(r3);


            Rectangle r4 = new Rectangle() { Width = 40, Height = 135 };
            r4.Stroke = Brushes.Black;
            r4.StrokeThickness = 2;
            Canvas.SetTop(r4, 15);
            Canvas.SetLeft(r4, 95);
            r4.StrokeDashArray = new DoubleCollection() { 5, 2 };
            canvas.Children.Add(r4);
            DimVerticalText(canvas, 90, 15, 1, 20, 11, 10, 5, "Overlap");
            DimVerticalText(canvas, 50, 15, 1, 135, 11, 10, 5, "Lenth Pile");
        }
        public static void DrawLineTagColumn(Canvas canvas, double left, double top, double x0, double y0)
        {
            Line l1 = new Line() { X1 = x0, X2 = left, Y1 = y0, Y2 = top };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = 0.5;
            canvas.Children.Add(l1);
        }
        public static void DrawOffsetDim(Canvas canvas)
        {
            DimHorizontalText(canvas, 20,35,1,100,11,20,5,"4000");
            DimHorizontalText(canvas, 20,70,1,50,11,20,5,"2000");
            DimHorizontalText(canvas, 70,70,1,50,11,20,5,"2000");
        }


        #endregion
        #region Cylindrical
        public static void DimCylindical(Canvas canvas, double left, double top, double scale, double offset, double extend, double D)
        {
            Line l1 = new Line() { X1 = left - Math.Sqrt(2) * D / (4 * scale) - offset, X2 = left + Math.Sqrt(2) * D / (4 * scale) + offset, Y1 = top - Math.Sqrt(2) * D / (4 * scale) - offset, Y2 = top + Math.Sqrt(2) * D / (4 * scale) + offset };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = 0.5;
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = left - Math.Sqrt(2) * D / (4 * scale), X2 = left - Math.Sqrt(2) * D / (4 * scale), Y1 = top - Math.Sqrt(2) * D / (4 * scale), Y2 = top - Math.Sqrt(2) * D / (4 * scale) - extend };
            l2.Stroke = Brushes.Black;
            l2.StrokeThickness = 0.5;
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = left - Math.Sqrt(2) * D / (4 * scale) - extend, X2 = left - Math.Sqrt(2) * D / (4 * scale), Y1 = top - Math.Sqrt(2) * D / (4 * scale), Y2 = top - Math.Sqrt(2) * D / (4 * scale) };
            l3.Stroke = Brushes.Black;
            l3.StrokeThickness = 0.5;
            canvas.Children.Add(l3);
            Line l4 = new Line() { X1 = left + Math.Sqrt(2) * D / (4 * scale), X2 = left + Math.Sqrt(2) * D / (4 * scale), Y1 = top + Math.Sqrt(2) * D / (4 * scale), Y2 = top + Math.Sqrt(2) * D / (4 * scale) + extend };
            l4.Stroke = Brushes.Black;
            l4.StrokeThickness = 0.5;
            canvas.Children.Add(l4);
            Line l5 = new Line() { X1 = left + Math.Sqrt(2) * D / (4 * scale) + extend, X2 = left + Math.Sqrt(2) * D / (4 * scale), Y1 = top + Math.Sqrt(2) * D / (4 * scale), Y2 = top + Math.Sqrt(2) * D / (4 * scale) };
            l5.Stroke = Brushes.Black;
            l5.StrokeThickness = 0.5;
            canvas.Children.Add(l5);
            TextBlock text = new TextBlock();
            text.Text = D + "";
            text.FontSize = 11;
            text.Foreground = Brushes.Black;
            text.LayoutTransform = new RotateTransform(45, 25, 25);
            Canvas.SetTop(text, top - Math.Sqrt(2) * D / (4 * scale) - offset - 22);
            Canvas.SetLeft(text, left - Math.Sqrt(2) * D / (4 * scale) - offset);
            canvas.Children.Add(text);
        }
        public static void DrawCylindricalSection(Canvas canvas, double left, double top, double scale, double d)
        {
            Ellipse e = new Ellipse() { Width = d / scale, Height = d / scale };
            e.Stroke = Brushes.Black;
            e.StrokeThickness = 1;
            Canvas.SetTop(e, top - (d) / (2 * scale));
            Canvas.SetLeft(e, left - (d) / (2 * scale));
            canvas.Children.Add(e);
        }
        public static void DrawCylindricalSectionDashArray(Canvas canvas, double left, double top, double scale, double d)
        {
            Ellipse e = new Ellipse() { Width = d / scale, Height = d / scale };
            e.Stroke = Brushes.Black;
            e.StrokeThickness = 1;
            e.StrokeDashArray = new DoubleCollection() { 5, 2 };
            Canvas.SetTop(e, top - (d) / (2 * scale));
            Canvas.SetLeft(e, left - (d) / (2 * scale));
            canvas.Children.Add(e);
        }
        public static void DrawCylindricalSectionDashArraySolidColorBrush(Canvas canvas, double left, double top, double scale, double d, SolidColorBrush solidColorBrush)
        {
            Ellipse e = new Ellipse() { Width = d / scale, Height = d / scale };
            e.Stroke = solidColorBrush;
            e.StrokeThickness = 1;
            e.StrokeDashArray = new DoubleCollection() { 5, 2 };
            Canvas.SetTop(e, top - (d) / (2 * scale));
            Canvas.SetLeft(e, left - (d) / (2 * scale));
            canvas.Children.Add(e);
        }
        public static void DrawCylindricalStirrup(Canvas canvas, double left, double top, double scale, double c, double d, double ds, SolidColorBrush solidColorBrush)
        {
            Ellipse e = new Ellipse() { Width = (d - 2 * c + ds / 2) / scale, Height = (d - 2 * c + ds / 2) / scale };
            e.Stroke = solidColorBrush;
            e.StrokeThickness = ds / scale;
            Canvas.SetTop(e, top - (d - 2 * c + ds / 2) / (2 * scale));
            Canvas.SetLeft(e, left - (d - 2 * c + ds / 2) / (2 * scale));
            canvas.Children.Add(e);
        }
        public static void DrawCylindricalStirrupDashArray(Canvas canvas, double left, double top, double scale, double c, double d, double ds, SolidColorBrush solidColorBrush)
        {
            Ellipse e = new Ellipse() { Width = (d - 2 * c + ds / 2) / scale, Height = (d - 2 * c + ds / 2) / scale };
            e.Stroke = solidColorBrush;
            e.StrokeThickness = ds / scale;
            e.StrokeDashArray = new DoubleCollection() { 5, 2 };
            Canvas.SetTop(e, top - (d - 2 * c + ds / 2) / (2 * scale));
            Canvas.SetLeft(e, left - (d - 2 * c + ds / 2) / (2 * scale));
            canvas.Children.Add(e);
        }
        public static void DrawCylindricalMainBarSettingView(Canvas canvas, double left, double top, double scale, double c, double D, double d, double ds, int n, SolidColorBrush solidColorBrush)
        {

            for (int i = 0; i < n; i++)
            {
                double angle = i * Math.PI * 2 / n;
                double x = left + ((D / 2 - c - ds - d / 2) / scale) * Math.Cos(angle);
                double y = top + ((D / 2 - c - ds - d / 2) / scale) * Math.Sin(angle);
                DrawOneBarSection(canvas, x, y, scale, d, solidColorBrush);
                if (i == 0)
                {
                    DimVerticalText(canvas, x, y, scale, 33, 11, 20, 4, "tmin");
                }
            }
        }
        public static void DrawCylindricalMainBar(Canvas canvas, double left, double top, double scale, double c, double D, double d, double ds, int n, SolidColorBrush solidColorBrush)
        {

            for (int i = 0; i < n; i++)
            {
                double angle = i * Math.PI * 2 / n;
                double x = left + ((D / 2 - c - ds - d / 2) / scale) * Math.Cos(angle);
                double y = top + ((D / 2 - c - ds - d / 2) / scale) * Math.Sin(angle);
                DrawOneBarSection(canvas, x, y, scale, d, solidColorBrush);
            }
        }
        public static void DrawCylindricalMainBarTagSettingView(Canvas canvas, double left, double top, double scale, double c, double D, double d, double ds, int n, SolidColorBrush solidColorBrush)
        {

            for (int i = 0; i < n; i++)
            {
                double angle = i * Math.PI * 2 / n;
                double x = left + ((D / 2 - c - ds - d / 2) / scale) * Math.Cos(angle);
                double y = top + ((D / 2 - c - ds - d / 2) / scale) * Math.Sin(angle);
                DrawOneBarSection(canvas, x, y, scale, d, solidColorBrush);
                DrawLineTagColumn(canvas, left, top, x, y);
            }
        }
       
        #endregion
      
        #region foundation design
        public static void DrawImageFoundation0(Canvas canvas)
        {
            Rectangle r = new Rectangle() { Width = 20, Height = 30 };
            r.Fill = Brushes.Black;
            Canvas.SetTop(r,65);
            Canvas.SetLeft(r, 60);
            canvas.Children.Add(r);
            Line l1 = new Line() { X1 = 10, X2 = 130, Y1 = 15, Y2 = 15 };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = 1.5;
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = 10, X2 = 10, Y1 = 15, Y2 = 45 };
            l2.Stroke = Brushes.Black;
            l2.StrokeThickness = 1.5;
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = 10, X2 = 55, Y1 = 45, Y2 = 145 };
            l3.Stroke = Brushes.Black;
            l3.StrokeThickness = 1.5;
            canvas.Children.Add(l3);
            Line l4 = new Line() { X1 = 55, X2 = 85, Y1 = 145, Y2 = 145 };
            l4.Stroke = Brushes.Black;
            l4.StrokeThickness = 1.5;
            canvas.Children.Add(l4);
            Line l5 = new Line() { X1 = 85, X2 = 130, Y1 = 145, Y2 = 45 };
            l5.Stroke = Brushes.Black;
            l5.StrokeThickness = 1.5;
            canvas.Children.Add(l5);
            Line l6 = new Line() { X1 = 130, X2 = 130, Y1 = 45, Y2 = 15 };
            l6.Stroke = Brushes.Black;
            l6.StrokeThickness = 1.5;
            canvas.Children.Add(l6);
            DrawCylindricalSectionDashArray(canvas, 30, 35, 1, 20);
            DrawCylindricalSectionDashArray(canvas, 110, 35, 1, 20);
            DrawCylindricalSectionDashArray(canvas, 70, 125, 1, 20);
            DimHorizontalText(canvas, 30, 45, 1, 80, 11, 10, 5, "Dp_p");
            DimVerticalText(canvas, 50, 35, 1, 45, 11, 10, 5, "L1");
            DimVerticalText(canvas, 50, 80, 1, 45, 11, 10, 5, "L2");
        }
        public static void DrawImageFoundation1(Canvas canvas)
        {
            //Rectangle r = new Rectangle() { Width = 20, Height = 30 };
            //r.Fill = Brushes.Black;
            //Canvas.SetTop(r, 65);
            //Canvas.SetLeft(r, 60);
            //canvas.Children.Add(r);
            Rectangle r1 = new Rectangle() { Width = 120, Height = 140 };
            r1.Stroke = Brushes.Black;
            r1.StrokeThickness = 1.5;
            Canvas.SetTop(r1, 10);
            Canvas.SetLeft(r1, 10);
            canvas.Children.Add(r1);
            DrawCylindricalSectionDashArray(canvas, 30, 30, 1, 20);
            DrawCylindricalSectionDashArray(canvas, 110, 30, 1, 20);
            DrawCylindricalSectionDashArray(canvas, 30, 130, 1, 20);
            DrawCylindricalSectionDashArray(canvas, 110, 130, 1, 20);
            DrawCylindricalSectionDashArray(canvas, 70, 80, 1, 20);
            DimHorizontalText(canvas, 30, 40, 1, 40, 11, 10, 5, "L1");
            DimHorizontalText(canvas, 70, 40, 1, 40, 11, 10, 5, "L1");
            DimVerticalText(canvas, 40, 30, 1, 50, 11, 10, 5, "L2");
            DimVerticalText(canvas, 40, 80, 1, 50, 11, 10, 5, "L2");
        }
        public static void DrawImageFoundation2(Canvas canvas)
        {
            
            DrawCylindricalSectionDashArray(canvas, 70, 80, 1, 20);
            double angle = 2*Math.PI / 5;
            double left = 70, top = 80;
            double radius = 65;
            for (int i = 0; i < 5; i++)
            {
                if (i==0)
                {
                    Line l0 = new Line() { X1 = left - Math.Cos(Math.PI * 0.5 + (4) * angle) * radius, X2 = left - Math.Cos(Math.PI * 0.5 + (0) * angle) * radius, Y1 = top - Math.Sin(Math.PI * 0.5 + (4) * angle) * radius, Y2 = top - Math.Sin(Math.PI * 0.5 + (0) * angle) * radius };
                    l0.Stroke = Brushes.Black;
                    l0.StrokeThickness = 1.5;
                    canvas.Children.Add(l0);
                }
                else
                {
                    Line l0 = new Line() {X1=left-Math.Cos(Math.PI*0.5+(i-1)*angle)* radius, X2= left -Math.Cos(Math.PI * 0.5 + (i ) * angle) * radius, Y1= top - Math.Sin(Math.PI * 0.5 + (i - 1) * angle) * radius, Y2= top - Math.Sin(Math.PI * 0.5 + (i ) * angle) * radius };
                    l0.Stroke = Brushes.Black;
                    l0.StrokeThickness = 1.5;
                    canvas.Children.Add(l0);  
                }
                DrawCylindricalSectionDashArray(canvas, left - Math.Cos(Math.PI * 0.5 + (i) * angle) * (radius - 20), top - Math.Sin(Math.PI * 0.5 + (i) * angle) * (radius - 20), 1, 20);
            }
        }
        public static void DrawImageFoundation3(Canvas canvas)
        {

            DrawCylindricalSectionDashArray(canvas, 70, 80, 1, 20);
            double angle = 2 * Math.PI / 6;
            double left = 70, top = 80;
            double radius = 65;
            for (int i = 0; i < 6; i++)
            {
                if (i == 0)
                {
                    Line l0 = new Line() { X1 = left - Math.Cos( (5) * angle) * radius, X2 = left - Math.Cos( + (0) * angle) * radius, Y1 = top - Math.Sin( (5) * angle) * radius, Y2 = top - Math.Sin( (0) * angle) * radius };
                    l0.Stroke = Brushes.Black;
                    l0.StrokeThickness = 1.5;
                    canvas.Children.Add(l0);
                }
                else
                {
                    Line l0 = new Line() { X1 = left - Math.Cos( (i - 1) * angle) * radius, X2 = left - Math.Cos( (i) * angle) * radius, Y1 = top - Math.Sin( (i - 1) * angle) * radius, Y2 = top - Math.Sin( (i) * angle) * radius };
                    l0.Stroke = Brushes.Black;
                    l0.StrokeThickness = 1.5;
                    canvas.Children.Add(l0);
                }
                DrawCylindricalSectionDashArray(canvas, left - Math.Cos( (i) * angle) * (radius - 20), top - Math.Sin( (i) * angle) * (radius - 20), 1, 20);
            }
        }
        #endregion
        public static void DrawAxis(Canvas canvas)
        {
            Line l1 = new Line() {X1=410,X2=410,Y1=0,Y2=820 };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = 1;
            l1.StrokeDashArray = new DoubleCollection() { 10, 2,2 };
            canvas.Children.Add(l1);
            Line l10 = new Line() { X1 = 0, X2 = 820, Y1 = 410, Y2 = 410 };
            l10.Stroke = Brushes.Black;
            l10.StrokeThickness = 1;
            l10.StrokeDashArray = new DoubleCollection() { 10, 2 };
            canvas.Children.Add(l10);
            for (int i = 0; i < 5; i++)
            {
                Line l2 = new Line() { X1 = 410-i * 80, X2 = 410 - i * 80, Y1 = 0, Y2 = 820 };
                l2.Stroke = Brushes.Black;
                l2.StrokeThickness = 0.5;
                l2.StrokeDashArray = new DoubleCollection() { 1,10, 1,10 };
                canvas.Children.Add(l2);
                Line l3 = new Line() { X1 = 410 + i * 80, X2 = 410 + i * 80, Y1 = 0, Y2 = 820 };
                l3.Stroke = Brushes.Black;
                l3.StrokeThickness = 0.5;
                l3.StrokeDashArray = new DoubleCollection() { 1, 10, 1, 10 };
                canvas.Children.Add(l3);
                Line l4 = new Line() { X1 = 0, X2 =820, Y1 = 410 - i * 80, Y2 = 410 - i * 80 };
                l4.Stroke = Brushes.Black;
                l4.StrokeThickness = 0.5;
                l4.StrokeDashArray = new DoubleCollection() { 1, 10, 1, 10 };
                canvas.Children.Add(l4);
                Line l5 = new Line() { X1 = 0, X2 = 820, Y1 = 410 + i * 80, Y2 = 410+ i * 80 };
                l5.Stroke = Brushes.Black;
                l5.StrokeThickness = 0.5;
                l5.StrokeDashArray = new DoubleCollection() { 1, 10, 1, 10 };
                canvas.Children.Add(l5);
            }
        }
        public static void DrawAxisSection(Canvas canvas)
        {
            Line l1 = new Line() { X1 = 175, X2 = 175, Y1 = 0, Y2 = 350 };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = 1;
            l1.StrokeDashArray = new DoubleCollection() { 10, 2, 2 };
            canvas.Children.Add(l1);
            Line l10 = new Line() { X1 = 0, X2 = 350, Y1 = 175, Y2 = 175 };
            l10.Stroke = Brushes.Black;
            l10.StrokeThickness = 1;
            l10.StrokeDashArray = new DoubleCollection() { 10, 2 };
            canvas.Children.Add(l10);
            for (int i = 0; i < 5; i++)
            {
                Line l2 = new Line() { X1 = 175 - i * 35, X2 = 175 - i * 35, Y1 = 0, Y2 = 350 };
                l2.Stroke = Brushes.Black;
                l2.StrokeThickness = 0.5;
                l2.StrokeDashArray = new DoubleCollection() { 1, 10, 1, 10 };
                canvas.Children.Add(l2);
                Line l3 = new Line() { X1 = 175 + i * 35, X2 = 175 + i * 35, Y1 = 0, Y2 = 350 };
                l3.Stroke = Brushes.Black;
                l3.StrokeThickness = 0.5;
                l3.StrokeDashArray = new DoubleCollection() { 1, 10, 1, 10 };
                canvas.Children.Add(l3);
                Line l4 = new Line() { X1 = 0, X2 = 350, Y1 = 175 - i * 35, Y2 = 175 - i * 35 };
                l4.Stroke = Brushes.Black;
                l4.StrokeThickness = 0.5;
                l4.StrokeDashArray = new DoubleCollection() { 1, 10, 1, 10 };
                canvas.Children.Add(l4);
                Line l5 = new Line() { X1 = 0, X2 = 350, Y1 = 175 + i * 35, Y2 = 175 + i * 35 };
                l5.Stroke = Brushes.Black;
                l5.StrokeThickness = 0.5;
                l5.StrokeDashArray = new DoubleCollection() { 1, 10, 1, 10 };
                canvas.Children.Add(l5);
            }
        }
        #region    Rule Pile Detail
        private static SolidColorBrush solid=Brushes.DarkSlateGray;
        private static void DrawBoundingRule(Canvas canvas)
        {
            Rectangle r = new Rectangle() { Width = 120, Height = 120 };
            r.Stroke = Brushes.Black;
            r.StrokeDashArray = new DoubleCollection() { 5, 5 };
            r.StrokeThickness = 1;
            Canvas.SetTop(r, 0);
            Canvas.SetLeft(r, 0);
            canvas.Children.Add(r);
        }
        private static void DrawLengthRuleUp(Canvas canvas)
        {
            Rectangle r = new Rectangle() {Width=100,Height=10 };
            r.Fill = solid;
            Canvas.SetTop(r, 15);
            Canvas.SetLeft(r, 10);
            canvas.Children.Add(r);
        }
        private static void DrawLengthRuleDown(Canvas canvas)
        {
            Rectangle r = new Rectangle() { Width = 100, Height = 10 };
            r.Fill = solid;
            Canvas.SetTop(r, 95);
            Canvas.SetLeft(r, 10);
            canvas.Children.Add(r);
        }
        private static void DrawLengthRuleLeft(Canvas canvas)
        {
            Rectangle r = new Rectangle() { Width = 10, Height = 90 };
            r.Fill = solid;
            Canvas.SetTop(r, 15);
            Canvas.SetLeft(r, 10);
            canvas.Children.Add(r);
        }
        private static void DrawLengthRuleRight(Canvas canvas)
        {
            Rectangle r = new Rectangle() { Width = 10, Height = 90 };
            r.Fill = solid;
            Canvas.SetTop(r, 15);
            Canvas.SetLeft(r, 100);
            canvas.Children.Add(r);
        }
        private static void DrawZiczacRuleLeft(Canvas canvas)
        {
            PointCollection points = new PointCollection();
            Point p1 = new Point() { X = 10, Y = 25 };
            Point p2 = new Point() { X = 95, Y = 95 };
            Point p3 = new Point() { X = 110, Y = 95 };
            Point p4 = new Point() { X = 25, Y = 25 };
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            Polygon polygon = new Polygon();
            polygon.Fill = solid;
            polygon.Points = points;
            canvas.Children.Add(polygon);
        }
        private static void DrawZiczacRuleRight(Canvas canvas)
        {
            PointCollection points = new PointCollection();
            Point p1 = new Point() { X = 10, Y = 95 };
            Point p2 = new Point() { X = 95, Y = 25 };
            Point p3 = new Point() { X = 110, Y = 25 };
            Point p4 = new Point() { X = 25, Y = 95 };
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            Polygon polygon = new Polygon();
            polygon.Fill = solid;
            polygon.Points = points;
            canvas.Children.Add(polygon);
        }
        private static void DrawZiczacRuleUp(Canvas canvas)
        {
            PointCollection points = new PointCollection();
            Point p1 = new Point() { X = 20, Y = 30 };
            Point p2 = new Point() { X = 100, Y = 105 };
            Point p3 = new Point() { X = 100, Y = 90 };
            Point p4 = new Point() { X = 20, Y = 15 };
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            Polygon polygon = new Polygon();
            polygon.Fill = solid;
            polygon.Points = points;
            canvas.Children.Add(polygon);
        }
        private static void DrawZiczacRuleDown(Canvas canvas)
        {
            PointCollection points = new PointCollection();
            Point p1 = new Point() { X = 20, Y = 105 };
            Point p2 = new Point() { X = 100, Y = 30 };
            Point p3 = new Point() { X = 100, Y = 15 };
            Point p4 = new Point() { X = 20, Y = 90 };
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            Polygon polygon = new Polygon();
            polygon.Fill = solid;
            polygon.Points = points;
            canvas.Children.Add(polygon);
        }
        private static void DrawArowLeftRightUp(Canvas canvas)
        {
            PointCollection points = new PointCollection();
            Point p1 = new Point() { X = 10, Y = 15 };
            Point p2 = new Point() { X = 90, Y = 15 };
            Point p3 = new Point() { X = 80, Y = 5 };
            Point p4 = new Point() { X = 110, Y = 20 };
            Point p5 = new Point() { X = 80, Y = 35 };
            Point p6 = new Point() { X = 90, Y = 25 };
            Point p7 = new Point() { X = 10, Y = 25 };
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            points.Add(p5);
            points.Add(p6);
            points.Add(p7);
            Polygon polygon = new Polygon();
            polygon.Fill = solid;
            polygon.Points = points;
            canvas.Children.Add(polygon);
        }
        private static void DrawArowRightLeftUp(Canvas canvas)
        {
            PointCollection points = new PointCollection();
            Point p1 = new Point() { X = 110, Y = 15 };
            Point p2 = new Point() { X = 30, Y = 15 };
            Point p3 = new Point() { X = 40, Y = 5 };
            Point p4 = new Point() { X = 10, Y = 20 };
            Point p5 = new Point() { X = 40, Y = 35 };
            Point p6 = new Point() { X = 30, Y = 25 };
            Point p7 = new Point() { X = 110, Y = 25 };
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            points.Add(p5);
            points.Add(p6);
            points.Add(p7);
            Polygon polygon = new Polygon();
            polygon.Fill = solid;
            polygon.Points = points;
            canvas.Children.Add(polygon);
        }
        private static void DrawArowLeftRightDown(Canvas canvas)
        {
            PointCollection points = new PointCollection();
            Point p1 = new Point() { X = 10, Y = 95 };
            Point p2 = new Point() { X = 90, Y = 95 };
            Point p3 = new Point() { X = 80, Y = 85 };
            Point p4 = new Point() { X = 110, Y = 100 };
            Point p5 = new Point() { X = 80, Y = 115 };
            Point p6 = new Point() { X = 90, Y = 105 };
            Point p7 = new Point() { X = 10, Y = 105 };
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            points.Add(p5);
            points.Add(p6);
            points.Add(p7);
            Polygon polygon = new Polygon();
            polygon.Fill = solid;
            polygon.Points = points;
            canvas.Children.Add(polygon);
        }
        private static void DrawArowRightLeftDown(Canvas canvas)
        {
            PointCollection points = new PointCollection();
            Point p1 = new Point() { X = 110, Y = 95 };
            Point p2 = new Point() { X = 30, Y = 95 };
            Point p3 = new Point() { X = 40, Y = 85 };
            Point p4 = new Point() { X = 10, Y = 100 };
            Point p5 = new Point() { X = 40, Y = 115 };
            Point p6 = new Point() { X = 30, Y = 105 };
            Point p7 = new Point() { X = 110, Y = 105 };
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            points.Add(p5);
            points.Add(p6);
            points.Add(p7);
            Polygon polygon = new Polygon();
            polygon.Fill = solid;
            polygon.Points = points;
            canvas.Children.Add(polygon);
        }
        private static void DrawArowRightUp(Canvas canvas)
        {
            PointCollection points = new PointCollection();
            Point p1 = new Point() { X = 100, Y = 105 };
            Point p2 = new Point() { X = 100, Y = 35 };
            Point p3 = new Point() { X = 90, Y = 45 };
            Point p4 = new Point() { X = 105, Y = 15 };
            Point p5 = new Point() { X = 120, Y = 45 };
            Point p6 = new Point() { X = 110, Y = 35 };
            Point p7 = new Point() { X = 110, Y = 105 };
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            points.Add(p5);
            points.Add(p6);
            points.Add(p7);
            Polygon polygon = new Polygon();
            polygon.Fill = solid;
            polygon.Points = points;
            canvas.Children.Add(polygon);
        }
        private static void DrawArowRightDown(Canvas canvas)
        {
            PointCollection points = new PointCollection();
            Point p1 = new Point() { X = 100, Y = 15 };
            Point p2 = new Point() { X = 100, Y = 85 };
            Point p3 = new Point() { X = 90, Y = 75 };
            Point p4 = new Point() { X = 105, Y = 105 };
            Point p5 = new Point() { X = 120, Y = 75 };
            Point p6 = new Point() { X = 110, Y = 85 };
            Point p7 = new Point() { X = 110, Y = 15 };
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            points.Add(p5);
            points.Add(p6);
            points.Add(p7);
            Polygon polygon = new Polygon();
            polygon.Fill = solid;
            polygon.Points = points;
            canvas.Children.Add(polygon);
        }
        private static void DrawArowLeftUp(Canvas canvas)
        {
            PointCollection points = new PointCollection();
            Point p1 = new Point() { X = 10, Y = 105 };
            Point p2 = new Point() { X = 10, Y = 35 };
            Point p3 = new Point() { X = 0, Y = 45 };
            Point p4 = new Point() { X = 15, Y = 15 };
            Point p5 = new Point() { X = 30, Y = 45 };
            Point p6 = new Point() { X = 20, Y = 35 };
            Point p7 = new Point() { X = 20, Y = 105 };
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            points.Add(p5);
            points.Add(p6);
            points.Add(p7);
            Polygon polygon = new Polygon();
            polygon.Fill = solid;
            polygon.Points = points;
            canvas.Children.Add(polygon);
        }
        private static void DrawArowLeftDown(Canvas canvas)
        {
            PointCollection points = new PointCollection();
            Point p1 = new Point() { X = 10, Y = 15 };
            Point p2 = new Point() { X = 10, Y = 85 };
            Point p3 = new Point() { X = 0, Y = 75 };
            Point p4 = new Point() { X = 15, Y = 105 };
            Point p5 = new Point() { X = 30, Y = 75 };
            Point p6 = new Point() { X = 20, Y = 85 };
            Point p7 = new Point() { X = 20, Y = 15 };
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            points.Add(p5);
            points.Add(p6);
            points.Add(p7);
            Polygon polygon = new Polygon();
            polygon.Fill = solid;
            polygon.Points = points;
            canvas.Children.Add(polygon);
        }
        public static void DrawFoundationRule0(Canvas canvas)
        {
            DrawBoundingRule(canvas);
            DrawLengthRuleDown(canvas);
            DrawZiczacRuleLeft(canvas);
            DrawArowLeftRightUp(canvas);
        }
        public static void DrawFoundationRule1(Canvas canvas)
        {
            DrawBoundingRule(canvas);
            DrawLengthRuleDown(canvas);
            DrawZiczacRuleRight(canvas);
            DrawArowRightLeftUp(canvas);
        }
        public static void DrawFoundationRule2(Canvas canvas)
        {
            DrawBoundingRule(canvas);
            DrawLengthRuleUp(canvas);
            DrawZiczacRuleRight(canvas);
            DrawArowLeftRightDown(canvas);
        }
        public static void DrawFoundationRule3(Canvas canvas)
        {
            DrawBoundingRule(canvas);
            DrawLengthRuleUp(canvas);
            DrawZiczacRuleLeft(canvas);
            DrawArowRightLeftDown(canvas);
        }
        public static void DrawFoundationRule4(Canvas canvas)
        {
            DrawBoundingRule(canvas);
            DrawLengthRuleLeft(canvas);
            DrawZiczacRuleUp(canvas);
            DrawArowRightUp(canvas);
        }
        public static void DrawFoundationRule5(Canvas canvas)
        {
            DrawBoundingRule(canvas);
            DrawLengthRuleLeft(canvas);
            DrawZiczacRuleDown(canvas);
            DrawArowRightDown(canvas);
        }
        public static void DrawFoundationRule6(Canvas canvas)
        {
            DrawBoundingRule(canvas);
            DrawLengthRuleRight(canvas);
            DrawZiczacRuleDown(canvas);
            DrawArowLeftUp(canvas);
        }
        public static void DrawFoundationRule7(Canvas canvas)
        {
            DrawBoundingRule(canvas);
            DrawLengthRuleRight(canvas);
            DrawZiczacRuleUp(canvas);
            DrawArowLeftDown(canvas);
        }
        #endregion
        public static void DrawVerticalOrientation(Canvas canvas,double x, SolidColorBrush solidColorBrush)
        {
            Line l1 = new Line() { X1 = x, X2 = x, Y1 = 0, Y2 = 200 };
            l1.Stroke = solidColorBrush;
            l1.StrokeThickness = 2;
            l1.StrokeDashArray = new DoubleCollection() { 5, 5 };
            canvas.Children.Add(l1);
            TextBlock text = new TextBlock();
            text.Text = "Vertical";
            text.FontSize = 11;
            text.Foreground = solidColorBrush;
            text.LayoutTransform = new RotateTransform(90, 25, 25);
            Canvas.SetTop(text, 0);
            Canvas.SetLeft(text, x +text.ActualHeight);
            canvas.Children.Add(text);
        }
        public static void DrawHorizontalOrientation(Canvas canvas,double y, SolidColorBrush solidColorBrush)
        {
            Line l1 = new Line() { X1 = 50, X2 = 300, Y1 = y, Y2 = y };
            l1.Stroke = solidColorBrush;
            l1.StrokeThickness = 2;
            l1.StrokeDashArray = new DoubleCollection() { 5, 5 };
            canvas.Children.Add(l1);
            TextBlock text = new TextBlock();
            text.Text = "Horizontal";
            text.FontSize = 11;
            text.Foreground = solidColorBrush;
            Canvas.SetTop(text, y-2*text.ActualHeight);
            Canvas.SetLeft(text, 50 );
            canvas.Children.Add(text);
        }
    }
}
