using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace R02_BeamsRebar
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
        public static void DrawGeometryView(Canvas canvas)
        {
            canvas.Children.Clear();
            Line l1 = new Line();
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = 1;
            l1.X1 = 50;
            l1.X2 = 400;
            l1.Y1 = 400;
            l1.Y2 = 400;
            canvas.Children.Add(l1);

            Line l2 = new Line();
            l2.Stroke = Brushes.Black;
            l2.StrokeThickness = 0.5;
            l2.StrokeDashArray = new DoubleCollection() { 10, 2 };
            l2.X1 = 100;
            l2.X2 = 100;
            l2.Y1 = 400;
            l2.Y2 = 300;
            canvas.Children.Add(l2);

            Line l3 = new Line();
            l3.Stroke = Brushes.Black;
            l3.StrokeThickness = 0.5;
            l3.StrokeDashArray = new DoubleCollection() { 10, 2 };
            l3.X1 = 350;
            l3.X2 = 350;
            l3.Y1 = 400;
            l3.Y2 = 250;
            canvas.Children.Add(l3);
            Polygon polygon = new Polygon();

            polygon.Stroke = Brushes.Black;
            polygon.StrokeThickness = 0.5;
            PointCollection points = new PointCollection();
            System.Windows.Point p1 = new System.Windows.Point(100, 300);
            System.Windows.Point p4 = new System.Windows.Point(350, 250);
            System.Windows.Point p2 = new System.Windows.Point(100, 280);

            System.Windows.Point p3 = new System.Windows.Point(350, 230);
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            polygon.Points = points;

            canvas.Children.Add(polygon);
            TextBlock e0 = new TextBlock();
            e0.Text = "e0";
            TextBlock e1 = new TextBlock();
            e1.Text = "e1";
            TextBlock b = new TextBlock();
            b.Text = "b";
            TextBlock h = new TextBlock();
            h.Text = "h";
            TextBlock Lb = new TextBlock();
            Lb.Text = "Lb";
            e0.FontSize = 11;
            e1.FontSize = 11;
            b.FontSize = 11;
            h.FontSize = 11;
            Lb.FontSize = 11;

            Canvas.SetTop(e0, 350);
            Canvas.SetLeft(e0, 85);
            Canvas.SetTop(e1, 325);
            Canvas.SetLeft(e1, 335);
            Canvas.SetTop(b, 210);
            Canvas.SetLeft(b, 370);
            Canvas.SetTop(h, 230);
            Canvas.SetLeft(h, 350);
            Canvas.SetTop(Lb, 230);
            Canvas.SetLeft(Lb, 225);
            canvas.Children.Add(e0);
            canvas.Children.Add(e1);
            canvas.Children.Add(b);
            canvas.Children.Add(h);
            canvas.Children.Add(Lb);


            Rectangle rec = new Rectangle();
            rec.Stroke = Brushes.Black;
            rec.StrokeThickness = 1;
            rec.Width = 20;
            rec.Height = 20;
            Canvas.SetTop(rec, 230);
            Canvas.SetLeft(rec, 370);
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
        public static void DrawDistribute1(Canvas canvas)
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
            for (int i = 0; i <= 360 / 20; i++)
            {
                Line l0 = new Line() { X1 = 40 + i * 20, X2 = 40 + i * 20, Y1 = 35, Y2 = 65 };
                l0.Stroke = Brushes.Green;
                l0.StrokeThickness = 2;
                canvas.Children.Add(l0);
            }
            canvas.Children.Add(p);
            canvas.Children.Add(l1);
            canvas.Children.Add(l2);
            DimHorizontalText(canvas, 220, 20, 1, 20, 9, 5, 2, "S");
            DimHorizontalText(canvas, 60, 20, 1, 20, 9, 5, 2, "S");
            DimHorizontalText(canvas, 360, 20, 1, 20, 9, 5, 2, "S");
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
            DrawHaflQualifyArc(canvas, left + (c + r + ds / 2) / scale, top + (c + r) / scale, scale, ds, d, 4, solidColorBrush);
            DrawHaflQualifyArc(canvas, left + (c + r) / scale, top + (c + r + ds / 2) / scale, scale, ds, d, 7, solidColorBrush);
            Line l5 = new Line() { X1 = left + (c + r + ds / 2) / scale - t * r / scale, X2 = left + (c + r + ds / 2) / scale - t * r / scale + (d) / scale, Y1 = top + (c + r + ds / 2) / scale + t * r / scale, Y2 = top + (c + r + ds / 2) / scale + t * r / scale + (d) / scale };
            l5.Stroke = solidColorBrush;
            l5.StrokeThickness = ds / scale;
            canvas.Children.Add(l5);
            Line l6 = new Line() { X1 = left + (c + r + ds / 2) / scale + t * r / scale, X2 = left + (c + r + ds / 2) / scale + t * r / scale + (d) / scale, Y1 = top + (c + r + ds / 2) / scale - t * r / scale, Y2 = top + (c + r + ds / 2) / scale - t * r / scale + (d) / scale };
            l6.Stroke = solidColorBrush;
            l6.StrokeThickness = ds / scale;
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
            if (Math.Abs(angle - Math.PI/2) < 1e-10)
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
            if (Math.Abs(angle - Math.PI ) < 1e-10)
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
            if (Math.Abs(angle - 3*Math.PI / 4) < 1e-10)
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
        public static void DimText(Canvas canvas, double left, double top, double scale,  int font,   string t,double angle)
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
        public static void DrawTagBarNumber(Canvas canvas, double left, double top, double scale, int font, string number, int rebar, double diameter,string type, SolidColorBrush solidColorBrush)
        {
            DrawBarNumber(canvas, left, top, scale, font, number, solidColorBrush);
            //string t = rebar.ToString() + " " + type+" " + diameter.ToString();
            string t = rebar.ToString() + " " + type+" " ;
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
        public static void DrawTagStirrup(Canvas canvas, double left, double top, double scale,double b, double c, double tagH, double distance, double diameter, SolidColorBrush solidColorBrush)
        {
            Line l = new Line() { X1 = left + b / scale  - c / scale, X2 = left + b / scale + tagH  , Y1 = top , Y2 = top  };
            l.Stroke = solidColorBrush;
            l.StrokeThickness = 0.5;
            canvas.Children.Add(l);
            DrawBarNumber(canvas, left + b / scale + tagH + 11 , top, scale, 11, "--", solidColorBrush);
            string t = " Ø " + diameter.ToString() + " @ " + distance.ToString();
            TextBlock text = new TextBlock();
            text.Text = t;
            text.FontSize = 11;
            text.Foreground = solidColorBrush;
            text.FontFamily = new FontFamily("Tahoma");
            text.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text.Arrange(new Rect(text.DesiredSize));
            Canvas.SetTop(text, top - 4 * text.ActualHeight / 5);
            Canvas.SetLeft(text, left + b / scale + tagH + 22 );
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
        public static void DrawBeamDetail(Canvas canvas)
        {
            #region boundary
            Line l1 = new Line() { X1 = 20, X2 = 20, Y1 = 40, Y2 = 100 };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = 1;
            l1.StrokeDashArray = new DoubleCollection() { 5, 2 };
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = 550, X2 = 550, Y1 = 40, Y2 = 100 };
            l2.Stroke = Brushes.Black;
            l2.StrokeThickness = 1;
            l2.StrokeDashArray = new DoubleCollection() { 5, 2 };
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = 20, X2 = 60, Y1 = 40, Y2 = 40 };
            l3.Stroke = Brushes.Black;
            l3.StrokeThickness = 1;
            canvas.Children.Add(l3);
            Line l4 = new Line() { X1 = 550, X2 = 510, Y1 = 40, Y2 = 40 };
            l4.Stroke = Brushes.Black;
            l4.StrokeThickness = 1;
            canvas.Children.Add(l4);
            Line l5 = new Line() { X1 = 20, X2 = 60, Y1 = 100, Y2 = 100 };
            l5.Stroke = Brushes.Black;
            l5.StrokeThickness = 1;
            canvas.Children.Add(l5);
            Line l6 = new Line() { X1 = 550, X2 = 510, Y1 = 100, Y2 = 100 };
            l6.Stroke = Brushes.Black;
            l6.StrokeThickness = 1;
            canvas.Children.Add(l6);
            Line l7 = new Line() { X1 = 60, X2 = 60, Y1 = 40, Y2 = 30 };
            l7.Stroke = Brushes.Black;
            l7.StrokeThickness = 1;
            canvas.Children.Add(l7);
            Line l8 = new Line() { X1 = 60, X2 = 60, Y1 = 100, Y2 = 110 };
            l8.Stroke = Brushes.Black;
            l8.StrokeThickness = 1;
            canvas.Children.Add(l8);
            Line l9 = new Line() { X1 = 60, X2 = 90, Y1 = 30, Y2 = 30 };
            l9.Stroke = Brushes.Black;
            l9.StrokeThickness = 1;
            l9.StrokeDashArray = new DoubleCollection() { 5, 2 };
            canvas.Children.Add(l9);
            Line l10 = new Line() { X1 = 60, X2 = 90, Y1 = 110, Y2 = 110 };
            l10.Stroke = Brushes.Black;
            l10.StrokeThickness = 1;
            l10.StrokeDashArray = new DoubleCollection() { 5, 2 };
            canvas.Children.Add(l10);
            Line l11 = new Line() { X1 = 510, X2 = 510, Y1 = 40, Y2 = 30 };
            l11.Stroke = Brushes.Black;
            l11.StrokeThickness = 1;
            canvas.Children.Add(l11);
            Line l12 = new Line() { X1 = 510, X2 = 510, Y1 = 100, Y2 = 110 };
            l12.Stroke = Brushes.Black;
            l12.StrokeThickness = 1;
            canvas.Children.Add(l12);
            Line l13 = new Line() { X1 = 510, X2 = 480, Y1 = 30, Y2 = 30 };
            l13.Stroke = Brushes.Black;
            l13.StrokeThickness = 1;
            l13.StrokeDashArray = new DoubleCollection() { 5, 2 };
            canvas.Children.Add(l13);
            Line l14 = new Line() { X1 = 510, X2 = 480, Y1 = 110, Y2 = 110 };
            l14.Stroke = Brushes.Black;
            l14.StrokeThickness = 1;
            l14.StrokeDashArray = new DoubleCollection() { 5, 2 };
            canvas.Children.Add(l14);
            Line l15 = new Line() { X1 = 90, X2 = 90, Y1 = 30, Y2 = 40 };
            l15.Stroke = Brushes.Black;
            l15.StrokeThickness = 1;
            canvas.Children.Add(l15);
            Line l16 = new Line() { X1 = 90, X2 = 90, Y1 = 100, Y2 = 110 };
            l16.Stroke = Brushes.Black;
            l16.StrokeThickness = 1;
            canvas.Children.Add(l16);
            Line l17 = new Line() { X1 = 480, X2 = 480, Y1 = 30, Y2 = 40 };
            l17.Stroke = Brushes.Black;
            l17.StrokeThickness = 1;
            canvas.Children.Add(l17);
            Line l18 = new Line() { X1 = 480, X2 = 480, Y1 = 100, Y2 = 110 };
            l18.Stroke = Brushes.Black;
            l18.StrokeThickness = 1;
            canvas.Children.Add(l18);
            Line l19 = new Line() { X1 = 480, X2 = 90, Y1 = 40, Y2 = 40 };
            l19.Stroke = Brushes.Black;
            l19.StrokeThickness = 1;
            canvas.Children.Add(l19);
            Line l20 = new Line() { X1 = 480, X2 = 90, Y1 = 100, Y2 = 100 };
            l20.Stroke = Brushes.Black;
            l20.StrokeThickness = 1;
            canvas.Children.Add(l20);
            #endregion
            DimHorizontalText(canvas, 90, 30, 1, 100, 11, 20, 5, "L/4");
            DimHorizontalText(canvas, 190, 30, 1, 190, 11, 20, 5, "L/2");
            DimHorizontalText(canvas, 380, 30, 1, 100, 11, 20, 5, "L/4");
            DimHorizontalText2(canvas, 60, 110, 1, 30, 11, 20, 5, "bc");
            DimHorizontalText2(canvas, 90, 110, 1, 60, 11, 20, 5, "(L/4)-hb");
            DimHorizontalText2(canvas, 150, 110, 1, 270, 11, 20, 5, "(L/2)-2hb");
            DimHorizontalText2(canvas, 420, 110, 1, 60, 11, 20, 5, "(L/4)-hb");
            DimHorizontalText2(canvas, 480, 110, 1, 30, 11, 20, 5, "bc");
            DimVerticalText(canvas, 265, 10, 1, 30, 11, 8, 4, "L1");
            DimVerticalText(canvas, 265, 100, 1, 30, 11, 8, 4, "L1");
            DimVerticalText(canvas, 265, 130, 1, 120, 11, 8, 4, "L2");
            DimVerticalText(canvas, 265, 250, 1, 30, 11, 8, 4, "L3");
            DimVerticalText(canvas, 265, 280, 1, 50, 11, 8, 4, "L4");
            DimVerticalText(canvas, 265, 330, 1, 30, 11, 8, 4, "L3");
        }
        public static void DrawBeamDetail1(Canvas canvas)
        {
            SolidColorBrush solidColorBrush = Brushes.Black;
            Line l1 = new Line() { X1 = 20, X2 = 550, Y1 = 45, Y2 = 45 };
            l1.Stroke = Brushes.Red;
            l1.StrokeThickness = 3;
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = 20, X2 = 550, Y1 = 95, Y2 = 95 };
            l2.Stroke = Brushes.Red;
            l2.StrokeThickness = 3;
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = 20, X2 = 190, Y1 = 50, Y2 = 50 };
            l3.Stroke = Brushes.Red;
            l3.StrokeThickness = 3;
            canvas.Children.Add(l3);
            Line l4 = new Line() { X1 = 550, X2 = 380, Y1 = 50, Y2 = 50 };
            l4.Stroke = Brushes.Red;
            l4.StrokeThickness = 3;
            canvas.Children.Add(l4);
            Line l5 = new Line() { X1 = 150, X2 = 420, Y1 = 90, Y2 = 90 };
            l5.Stroke = Brushes.Red;
            l5.StrokeThickness = 3;
            canvas.Children.Add(l5);
            Line l6 = new Line() { X1 = 20, X2 = 550, Y1 = 250, Y2 = 250 };
            l6.Stroke = Brushes.Red;
            l6.StrokeThickness = 3;
            canvas.Children.Add(l6);
            DrawTagBarNumber(canvas, 190, 235, 1, 11, "--", 2, 20, "Ø 20", solidColorBrush);
            Line l7 = new Line() { X1 = 20, X2 = 550, Y1 = 360, Y2 = 360 };
            l7.Stroke = Brushes.Red;
            l7.StrokeThickness = 3;
            canvas.Children.Add(l7);
            DrawTagBarNumber(canvas, 400, 345, 1, 11, "--", 2, 20, "Ø 20", solidColorBrush);
            Line l8 = new Line() { X1 = 20, X2 = 190, Y1 = 280, Y2 = 280 };
            l8.Stroke = Brushes.Red;
            l8.StrokeThickness = 3;
            canvas.Children.Add(l8);
            DrawTagBarNumber(canvas, 65, 265, 1, 11, "--", 2, 20, "Ø 20", solidColorBrush);
            Line l9 = new Line() { X1 = 550, X2 = 380, Y1 = 280, Y2 = 280 };
            l9.Stroke = Brushes.Red;
            l9.StrokeThickness = 3;
            canvas.Children.Add(l9);
            DrawTagBarNumber(canvas, 400, 265, 1, 11, "--", 2, 20, "Ø 20", solidColorBrush);
            Line l10 = new Line() { X1 = 150, X2 = 420, Y1 = 330, Y2 = 330 };
            l10.Stroke = Brushes.Red;
            l10.StrokeThickness = 3;
            canvas.Children.Add(l10);
            DrawTagBarNumber(canvas, 190, 315, 1, 11, "--", 2, 20, "Ø 20", solidColorBrush);

        }
        #endregion
        #region Special point
        public static void DrawSpecialBar(Canvas canvas)
        {
            Line l1 = new Line() { X1 = 0, X2 = 60, Y1 = 70, Y2 = 70 };
            l1.Stroke = Brushes.Red;
            l1.StrokeThickness = 3;
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = 60, X2 = 125, Y1 = 70, Y2 = 135 };
            l2.Stroke = Brushes.Red;
            l2.StrokeThickness = 3;
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = 125, X2 = 165, Y1 = 135, Y2 = 135 };
            l3.Stroke = Brushes.Red;
            l3.StrokeThickness = 3;
            canvas.Children.Add(l3);
            Line l4 = new Line() { X1 = 165, X2 = 230, Y1 = 135, Y2 = 70 };
            l4.Stroke = Brushes.Red;
            l4.StrokeThickness = 3;
            canvas.Children.Add(l4);
            Line l5 = new Line() { X1 = 230, X2 = 290, Y1 = 70, Y2 = 70 };
            l5.Stroke = Brushes.Red;
            l5.StrokeThickness = 3;
            canvas.Children.Add(l5);
            DimHorizontalText(canvas, 0, 70, 1, 60, 11, 20, 5, "L1");
            DimHorizontalText(canvas, 60, 70, 1, 65, 11, 20, 5, "L2");
            DimHorizontalText(canvas, 125, 70, 1, 40, 11, 20, 5, "L3");
            DimHorizontalText(canvas, 165, 70, 1, 65, 11, 20, 5, "L2");
            DimHorizontalText(canvas, 230, 70, 1, 60, 11, 20, 5, "L1");
            DimVerticalText(canvas, 60, 70, 1, 65, 11, 20, 5, "L2");
        }
        public static void DrawSpecialPoint(Canvas canvas)
        {
            Line l1 = new Line() { X1 = 10, X2 = 560, Y1 = 60, Y2 = 60 };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = 1;
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = 10, X2 = 560, Y1 = 250, Y2 = 250 };
            l2.Stroke = Brushes.Black;
            l2.StrokeThickness = 1;
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = 10, X2 = 10, Y1 = 60, Y2 = 250 };
            l3.Stroke = Brushes.Black;
            l3.StrokeThickness = 1;
            l3.StrokeDashArray = new DoubleCollection() { 10, 4 };
            canvas.Children.Add(l3);
            Line l4 = new Line() { X1 = 560, X2 = 560, Y1 = 60, Y2 = 250 };
            l4.Stroke = Brushes.Black;
            l4.StrokeThickness = 1;
            l4.StrokeDashArray = new DoubleCollection() { 10, 4 };
            canvas.Children.Add(l4);
            Line l5 = new Line() { X1 = 10, X2 = 560, Y1 = 75, Y2 = 75 };
            l5.Stroke = Brushes.Black;
            l5.StrokeThickness = 3;
            canvas.Children.Add(l5);
            Line l6 = new Line() { X1 = 10, X2 = 560, Y1 = 235, Y2 = 235 };
            l6.Stroke = Brushes.Black;
            l6.StrokeThickness = 3;
            canvas.Children.Add(l6);

        }
        public static void DrawSpecialBarDetail(Canvas canvas)
        {

            Line l7 = new Line() { X1 = 10, X2 = 90, Y1 = 70, Y2 = 70 };
            l7.Stroke = Brushes.Red;
            l7.StrokeThickness = 3;
            canvas.Children.Add(l7);
            Line l8 = new Line() { X1 = 90, X2 = 250, Y1 = 70, Y2 = 240 };
            l8.Stroke = Brushes.Red;
            l8.StrokeThickness = 3;
            canvas.Children.Add(l8);
            Line l9 = new Line() { X1 = 250, X2 = 320, Y1 = 240, Y2 = 240 };
            l9.Stroke = Brushes.Red;
            l9.StrokeThickness = 3;
            canvas.Children.Add(l9);
            Line l10 = new Line() { X1 = 320, X2 = 480, Y1 = 240, Y2 = 70 };
            l10.Stroke = Brushes.Red;
            l10.StrokeThickness = 3;
            canvas.Children.Add(l10);
            Line l11 = new Line() { X1 = 480, X2 = 560, Y1 = 70, Y2 = 70 };
            l11.Stroke = Brushes.Red;
            l11.StrokeThickness = 3;
            canvas.Children.Add(l11);
        }
        public static void DrawStirrupPoint(Canvas canvas, int n, double a)
        {
            for (int i = 0; i < n / 2; i++)
            {
                Line l1 = new Line() { X1 = 245 - i * a, X2 = 245 - i * a, Y1 = 70, Y2 = 250 };
                l1.Stroke = Brushes.Blue;
                l1.StrokeThickness = 2;
                canvas.Children.Add(l1);
                Line l2 = new Line() { X1 = 325 + i * a, X2 = 325 + i * a, Y1 = 70, Y2 = 250 };
                l2.Stroke = Brushes.Blue;
                l2.StrokeThickness = 2;
                canvas.Children.Add(l2);
            }


        }
        #endregion
        #region BarsDivision
        public static void DrawDivisionTop(Canvas canvas, bool isLeft)
        {
            Line l1 = new Line() {X1=50,X2=50,Y1=90,Y2=40 };
            l1.Stroke = Brushes.Red;
            l1.StrokeThickness = 2;
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = 50, X2 = 240, Y1 = 40, Y2 = 40 };
            l2.Stroke = Brushes.Red;
            l2.StrokeThickness = 2;
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = 540, X2 = 540, Y1 = 90, Y2 = 40 };
            l3.Stroke = Brushes.Red;
            l3.StrokeThickness = 2;
            canvas.Children.Add(l3);
            Line l4 = new Line() { X1 = 540, X2 = 350, Y1 = 40, Y2 = 40 };
            l4.Stroke = Brushes.Red;
            l4.StrokeThickness = 2;
            canvas.Children.Add(l4);
            Line l5 = new Line() { X1 = 180, X2 = 410, Y1 = 80, Y2 = 80 };
            l5.Stroke = Brushes.Red;
            l5.StrokeThickness = 2;
            canvas.Children.Add(l5);
            DimHorizontalText(canvas, 180, 80, 1, 60, 11, 20, 5, "S");
            DimHorizontalText(canvas, 350, 80, 1, 60, 11, 20, 5, "S");
            DimText(canvas, 145, 30, 1,  11, "L",0);
            DimText(canvas, 445, 30, 1,  11, "L",0);
            DimText(canvas, 50+11, 60, 1,  11, "Lleft",90);
            DimText(canvas, 540-11, 60, 1,  11, "Lright",-90);
            if (isLeft)
            {
                DimText(canvas, 145, 45, 1, 11, "L1(Lmax)", 0);
                DimText(canvas, 445, 45, 1, 11, "L2", 0);
            }
            else
            {
                DimText(canvas, 145, 45, 1, 11, "L1", 0);
                DimText(canvas, 445, 45, 1, 11, "L2(Lmax)", 0);
            }
            DimText(canvas, 295, 85, 1, 11, "Li(Lmax)", 0);
            DimText(canvas, 295, 40, 1, 20, "Top", 0);
        }
        public static void DrawDivisionBottom(Canvas canvas, bool isLeft)
        {
            Line l1 = new Line() { X1 = 50, X2 = 50, Y1 = 40, Y2 = 90 };
            l1.Stroke = Brushes.Red;
            l1.StrokeThickness = 2;
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = 50, X2 = 240, Y1 = 90, Y2 = 90 };
            l2.Stroke = Brushes.Red;
            l2.StrokeThickness = 2;
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = 540, X2 = 540, Y1 = 40, Y2 = 90 };
            l3.Stroke = Brushes.Red;
            l3.StrokeThickness = 2;
            canvas.Children.Add(l3);
            Line l4 = new Line() { X1 = 540, X2 = 350, Y1 = 90, Y2 = 90 };
            l4.Stroke = Brushes.Red;
            l4.StrokeThickness = 2;
            canvas.Children.Add(l4);
            Line l5 = new Line() { X1 = 180, X2 = 410, Y1 = 50, Y2 = 50 };
            l5.Stroke = Brushes.Red;
            l5.StrokeThickness = 2;
            canvas.Children.Add(l5);
            DimHorizontalText(canvas, 180, 50, 1, 60, 11, 20, 5, "S");
            DimHorizontalText(canvas, 350, 50, 1, 60, 11, 20, 5, "S");
            DimText(canvas, 145, 100, 1, 11, "L", 0);
            DimText(canvas, 445, 100, 1, 11, "L", 0);
            DimText(canvas, 50 + 11, 60, 1, 11, "Lleft", 90);
            DimText(canvas, 540 - 11, 60, 1, 11, "Lright", -90);
            if (!isLeft)
            {
                DimText(canvas, 145, 75, 1, 11, "L1(Lmax)", 0);
                DimText(canvas, 445, 75, 1, 11, "L2", 0);
            }
            else
            {
                DimText(canvas, 145, 75, 1, 11, "L1", 0);
                DimText(canvas, 445, 75, 1, 11, "L2(Lmax)", 0);
            }
            DimText(canvas, 295, 40, 1, 11, "Li(Lmax)", 0);
            DimText(canvas, 295, 90, 1, 20, "Bottom", 0);
        }
        #endregion

    }
}
