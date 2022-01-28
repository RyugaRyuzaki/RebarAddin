using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace R02_BeamsRebar
{
    public class MainBarImage
    {
        public static void DrawMainTopBar(Canvas canvas)
        {
            Polygon p = new Polygon();
            p.Stroke = Brushes.Black;
            p.StrokeThickness = 1.5;
            PointCollection points = new PointCollection();
            Point p1 = new Point() { X = 60, Y = 20 };
            Point p2 = new Point() { X = 100, Y = 20 };
            Point p3 = new Point() { X = 100, Y = 30 };
            Point p4 = new Point() { X = 460, Y = 30 };
            Point p5 = new Point() { X = 460, Y = 20 };
            Point p6 = new Point() { X = 500, Y = 20 };
            Point p7 = new Point() { X = 500, Y = 120 };
            Point p8 = new Point() { X = 460, Y = 120 };
            Point p9 = new Point() { X = 460, Y = 110 };
            Point p10 = new Point() { X = 100, Y = 110 };
            Point p11 = new Point() { X = 100, Y = 120 };
            Point p12 = new Point() { X = 60, Y = 120 };
            points.Add(p1); points.Add(p2); points.Add(p3); points.Add(p4); points.Add(p5); points.Add(p6); points.Add(p7); points.Add(p8); points.Add(p9); points.Add(p10); points.Add(p11); points.Add(p12);
            p.Points = points;

            Line l1 = new Line() { X1 = 65, X2 = 495, Y1 = 35, Y2 = 35 };
            l1.Stroke = Brushes.Red;
            l1.StrokeThickness = 3;
            Line l3 = new Line() { X1 = 65, X2 = 65, Y1 = 35, Y2 = 105 };
            l3.Stroke = Brushes.Red;
            l3.StrokeThickness = 3;
            Line l4 = new Line() { X1 = 495, X2 = 495, Y1 = 35, Y2 = 105 };
            l4.Stroke = Brushes.Red;
            l4.StrokeThickness = 3;
            Line l5 = new Line() { X1 = 5, X2 = 65, Y1 = 35, Y2 = 35 };
            l5.Stroke = Brushes.Red;
            l5.StrokeThickness = 3;
            l5.StrokeDashArray = new DoubleCollection() {8,3 };
            Line l6 = new Line() { X1 = 495, X2 = 555, Y1 = 35, Y2 = 35 };
            l6.Stroke = Brushes.Red;
            l6.StrokeThickness = 3;
            l6.StrokeDashArray = new DoubleCollection() { 8, 3 };

            for (int i = 0; i <= 360 / 36; i++)
            {
                Line l0 = new Line() { X1 = 100 + i * 36, X2 = 100 + i * 36, Y1 = 35, Y2 = 105 };
                l0.Stroke = Brushes.Green;
                l0.StrokeThickness = 2;
                canvas.Children.Add(l0);
            }
            DrawImage.DimHorizontalText(canvas, 5, 60, 1, 60, 11, 4, 2, "Ex-a");
            DrawImage.DimHorizontalText(canvas, 495, 60, 1, 60, 11, 4, 2, "Ex-b");
            DrawImage.DimVerticalText(canvas, 85, 35, 1, 70, 11, 4, 2, "La");
            DrawImage.DimVerticalText(canvas, 560-80, 35, 1, 70, 11, 4, 2, "Lb");
            canvas.Children.Add(p);
            canvas.Children.Add(l1);
            canvas.Children.Add(l3);
            canvas.Children.Add(l4);
            canvas.Children.Add(l5);
            canvas.Children.Add(l6);

        }
        public static void DrawMainBottomBar(Canvas canvas)
        {
            Polygon p = new Polygon();
            p.Stroke = Brushes.Black;
            p.StrokeThickness = 1.5;
            PointCollection points = new PointCollection();
            Point p1 = new Point() { X = 60, Y = 20 };
            Point p2 = new Point() { X = 100, Y = 20 };
            Point p3 = new Point() { X = 100, Y = 30 };
            Point p4 = new Point() { X = 460, Y = 30 };
            Point p5 = new Point() { X = 460, Y = 20 };
            Point p6 = new Point() { X = 500, Y = 20 };
            Point p7 = new Point() { X = 500, Y = 120 };
            Point p8 = new Point() { X = 460, Y = 120 };
            Point p9 = new Point() { X = 460, Y = 110 };
            Point p10 = new Point() { X = 100, Y = 110 };
            Point p11 = new Point() { X = 100, Y = 120 };
            Point p12 = new Point() { X = 60, Y = 120 };
            points.Add(p1); points.Add(p2); points.Add(p3); points.Add(p4); points.Add(p5); points.Add(p6); points.Add(p7); points.Add(p8); points.Add(p9); points.Add(p10); points.Add(p11); points.Add(p12);
            p.Points = points;

            Line l1 = new Line() { X1 = 65, X2 = 495, Y1 = 105, Y2 = 105 };
            l1.Stroke = Brushes.Red;
            l1.StrokeThickness = 3;
            Line l3 = new Line() { X1 = 65, X2 = 65, Y1 = 35, Y2 = 105 };
            l3.Stroke = Brushes.Red;
            l3.StrokeThickness = 3;
            Line l4 = new Line() { X1 = 495, X2 = 495, Y1 = 35, Y2 = 105 };
            l4.Stroke = Brushes.Red;
            l4.StrokeThickness = 3;
            Line l5 = new Line() { X1 = 5, X2 = 65, Y1 = 105, Y2 = 105 };
            l5.Stroke = Brushes.Red;
            l5.StrokeThickness = 3;
            l5.StrokeDashArray = new DoubleCollection() { 8, 3 };
            Line l6 = new Line() { X1 = 495, X2 = 555, Y1 = 105, Y2 = 105 };
            l6.Stroke = Brushes.Red;
            l6.StrokeThickness = 3;
            l6.StrokeDashArray = new DoubleCollection() { 8, 3 };

            for (int i = 0; i <= 360 / 36; i++)
            {
                Line l0 = new Line() { X1 = 100 + i * 36, X2 = 100 + i * 36, Y1 = 35, Y2 = 105 };
                l0.Stroke = Brushes.Green;
                l0.StrokeThickness = 2;
                canvas.Children.Add(l0);
            }
            DrawImage.DimHorizontalText(canvas, 5, 130, 1, 60, 11, 4, 2, "Ex-a");
            DrawImage.DimHorizontalText(canvas, 495, 130, 1, 60, 11, 4, 2, "Ex-b");
            DrawImage.DimVerticalText(canvas, 85, 35, 1, 70, 11, 4, 2, "La");
            DrawImage.DimVerticalText(canvas, 560 - 80, 35, 1, 70, 11, 4, 2, "Lb");
            canvas.Children.Add(p);
            canvas.Children.Add(l1);
            canvas.Children.Add(l3);
            canvas.Children.Add(l4);
            canvas.Children.Add(l5);
            canvas.Children.Add(l6);

        }
    }
}
