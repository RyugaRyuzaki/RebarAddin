

using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace R10_WallShear
{
    public class DrawIcon
    {
        /// <summary>
        /// vẽ icon của geometry menu
        /// </summary>
        /// <param name="canvas"></param>
        public static void DrawGeometry(Canvas canvas)
        {
            Rectangle r1 = new Rectangle() { Width = 22, Height = 4 };
            r1.Stroke = Brushes.Black;
            r1.StrokeThickness = 0.8;
            Canvas.SetTop(r1,2);
            Canvas.SetLeft(r1, 5);
            Rectangle r2 = new Rectangle() { Width = 22, Height = 4 };
            r2.Stroke = Brushes.Black;
            r2.StrokeThickness = 0.8;
            Canvas.SetTop(r2, 26);
            Canvas.SetLeft(r2, 5);
            Rectangle r3 = new Rectangle() { Width = 12, Height = 20 };
            r3.Fill = Brushes.Red;
            Canvas.SetTop(r3, 6);
            Canvas.SetLeft(r3, 10);
            canvas.Children.Add(r1);
            canvas.Children.Add(r2);
            canvas.Children.Add(r3);
        }
        public static void DrawBarsStirrups(Canvas canvas, bool bar)
        {
            Rectangle r1 = new Rectangle() { Width = 28, Height = 28 };
            r1.Stroke = Brushes.Black;
            r1.StrokeThickness = 0.8;
            Canvas.SetTop(r1, 2);
            Canvas.SetLeft(r1, 2);
            canvas.Children.Add(r1);
            SolidColorBrush barbrush = Brushes.Black;
            SolidColorBrush stirrupbrush = Brushes.Black;
            if (bar)
            {
                barbrush = Brushes.Red;
                stirrupbrush = Brushes.LightGray;
            }
            else
            {
                barbrush = Brushes.LightGray;
                stirrupbrush = Brushes.Red;
            }
            Rectangle r2 = new Rectangle() { Width = 24, Height = 24 };
            r2.Stroke = stirrupbrush;
            r2.StrokeThickness = 2;
            r2.RadiusX = 2;r2.RadiusY = 2;
            Canvas.SetTop(r2, 4);
            Canvas.SetLeft(r2, 4);
            canvas.Children.Add(r2);
            Line l1 = new Line() {X1=12,X2=12,Y1=4,Y2=28 };
            l1.Stroke = stirrupbrush;
            l1.StrokeThickness = 2;
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = 20, X2 = 20, Y1 = 4, Y2 = 28 };
            l2.Stroke = stirrupbrush;
            l2.StrokeThickness = 2;
            canvas.Children.Add(l2);
            Ellipse e1 = new Ellipse() { Width = 4, Height = 4 };
            e1.Fill = barbrush;
            Canvas.SetTop(e1, 6);
            Canvas.SetLeft(e1, 6);
            canvas.Children.Add(e1);
            Ellipse e2 = new Ellipse() { Width = 4, Height = 4 };
            e2.Fill = barbrush;
            Canvas.SetTop(e2, 6);
            Canvas.SetLeft(e2, 14);
            canvas.Children.Add(e2);
            Ellipse e3 = new Ellipse() { Width = 4, Height = 4 };
            e3.Fill = barbrush;
            Canvas.SetTop(e3, 6);
            Canvas.SetLeft(e3, 22);
            canvas.Children.Add(e3);
            Ellipse e4 = new Ellipse() { Width = 4, Height = 4 };
            e4.Fill = barbrush;
            Canvas.SetTop(e4, 22);
            Canvas.SetLeft(e4, 6);
            canvas.Children.Add(e4);
            Ellipse e5 = new Ellipse() { Width = 4, Height = 4 };
            e5.Fill = barbrush;
            Canvas.SetTop(e5, 22);
            Canvas.SetLeft(e5, 14);
            canvas.Children.Add(e5);
            Ellipse e6 = new Ellipse() { Width = 4, Height = 4 };
            e6.Fill = barbrush;
            Canvas.SetTop(e6, 22);
            Canvas.SetLeft(e6, 22);
            canvas.Children.Add(e6);
        }
        public static void DrawBarsAdditionalStirrups(Canvas canvas, bool bar)
        {
            Rectangle r1 = new Rectangle() { Width = 28, Height = 28 };
            r1.Stroke = Brushes.Black;
            r1.StrokeThickness = 0.8;
            Canvas.SetTop(r1, 2);
            Canvas.SetLeft(r1, 2);
            canvas.Children.Add(r1);
            SolidColorBrush barbrush = Brushes.Black;
            SolidColorBrush stirrupbrush = Brushes.Black;
            if (bar)
            {
                barbrush = Brushes.Red;
                stirrupbrush = Brushes.LightGray;
            }
            else
            {
                barbrush = Brushes.LightGray;
                stirrupbrush = Brushes.Red;
            }
            Rectangle r2 = new Rectangle() { Width = 24, Height = 24 };
            r2.Stroke = barbrush;
            r2.StrokeThickness = 2;
            r2.RadiusX = 2; r2.RadiusY = 2;
            Canvas.SetTop(r2, 4);
            Canvas.SetLeft(r2, 4);
            canvas.Children.Add(r2);
            Line l1 = new Line() { X1 = 12, X2 = 12, Y1 = 4, Y2 = 28 };
            l1.Stroke = stirrupbrush;
            l1.StrokeThickness = 2;
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = 20, X2 = 20, Y1 = 4, Y2 = 28 };
            l2.Stroke = stirrupbrush;
            l2.StrokeThickness = 2;
            canvas.Children.Add(l2);
            Ellipse e1 = new Ellipse() { Width = 4, Height = 4 };
            e1.Fill = barbrush;
            Canvas.SetTop(e1, 6);
            Canvas.SetLeft(e1, 6);
            canvas.Children.Add(e1);
            Ellipse e2 = new Ellipse() { Width = 4, Height = 4 };
            e2.Fill = barbrush;
            Canvas.SetTop(e2, 6);
            Canvas.SetLeft(e2, 14);
            canvas.Children.Add(e2);
            Ellipse e3 = new Ellipse() { Width = 4, Height = 4 };
            e3.Fill = barbrush;
            Canvas.SetTop(e3, 6);
            Canvas.SetLeft(e3, 22);
            canvas.Children.Add(e3);
            Ellipse e4 = new Ellipse() { Width = 4, Height = 4 };
            e4.Fill = barbrush;
            Canvas.SetTop(e4, 22);
            Canvas.SetLeft(e4, 6);
            canvas.Children.Add(e4);
            Ellipse e5 = new Ellipse() { Width = 4, Height = 4 };
            e5.Fill = barbrush;
            Canvas.SetTop(e5, 22);
            Canvas.SetLeft(e5, 14);
            canvas.Children.Add(e5);
            Ellipse e6 = new Ellipse() { Width = 4, Height = 4 };
            e6.Fill = barbrush;
            Canvas.SetTop(e6, 22);
            Canvas.SetLeft(e6, 22);
            canvas.Children.Add(e6);
        }
        public static void DrawTopDowels(Canvas canvas)
        {
            Line l1 = new Line() { X1 = 7, X2 = 7, Y1 = 8, Y2 = 30 };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = 1;
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = 25, X2 = 25, Y1 = 8, Y2 = 30 };
            l2.Stroke = Brushes.Black;
            l2.StrokeThickness = 1;
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = 7, X2 = 25, Y1 = 8, Y2 = 8 };
            l3.Stroke = Brushes.Black;
            l3.StrokeThickness = 1;
            canvas.Children.Add(l3);
            Line l4 = new Line() { X1 = 2, X2 = 32, Y1 = 30, Y2 = 30 };
            l4.Stroke = Brushes.Black;
            l4.StrokeThickness = 1;
            l4.StrokeDashArray = new DoubleCollection() { 5, 2 };
            canvas.Children.Add(l4);
            Line l5 = new Line() { X1 = 10, X2 = 10, Y1 = 0, Y2 = 30 };
            l5.Stroke = Brushes.Red;
            l5.StrokeThickness = 2;
            canvas.Children.Add(l5);
            Line l6 = new Line() { X1 = 23, X2 = 23, Y1 = 0, Y2 = 30 };
            l6.Stroke = Brushes.Red;
            l6.StrokeThickness = 2;
            canvas.Children.Add(l6);
            Line l7 = new Line() { X1 = 10, X2 = 23, Y1 = 12, Y2 = 12 };
            l7.Stroke = Brushes.Black;
            l7.StrokeThickness = 2;
            canvas.Children.Add(l7);
            Line l8 = new Line() { X1 = 10, X2 = 23, Y1 = 18, Y2 = 18 };
            l8.Stroke = Brushes.Black;
            l8.StrokeThickness = 2;
            canvas.Children.Add(l8);
            Line l9 = new Line() { X1 = 10, X2 = 23, Y1 = 24, Y2 = 24 };
            l9.Stroke = Brushes.Black;
            l9.StrokeThickness = 2;
            canvas.Children.Add(l9);
        }
        public static void DrawBottomDowels(Canvas canvas)
        {
            Line l1 = new Line() { X1 = 7, X2 = 7, Y1 = 2, Y2 = 20 };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = 1;
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = 25, X2 = 25, Y1 = 2, Y2 = 20 };
            l2.Stroke = Brushes.Black;
            l2.StrokeThickness = 1;
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = 7, X2 = 25, Y1 = 2, Y2 = 2 };
            l3.Stroke = Brushes.Black;
            l3.StrokeThickness = 1;
            canvas.Children.Add(l3);
            Line l4 = new Line() { X1 = 2, X2 = 30, Y1 = 30, Y2 = 30 };
            l4.Stroke = Brushes.Black;
            l4.StrokeThickness = 1;
            canvas.Children.Add(l4);
            Line l4a = new Line() { X1 = 2, X2 = 2, Y1 = 20, Y2 = 30 };
            l4a.Stroke = Brushes.Black;
            l4a.StrokeThickness = 1;
            canvas.Children.Add(l4a);
            Line l4b = new Line() { X1 = 30, X2 = 30, Y1 = 20, Y2 = 30 };
            l4b.Stroke = Brushes.Black;
            l4b.StrokeThickness = 1;
            canvas.Children.Add(l4b);
            Line l4c = new Line() { X1 = 2, X2 = 7, Y1 = 20, Y2 = 20 };
            l4c.Stroke = Brushes.Black;
            l4c.StrokeThickness = 1;
            canvas.Children.Add(l4c);
            Line l4d = new Line() { X1 = 25, X2 = 30, Y1 = 20, Y2 = 20 };
            l4d.Stroke = Brushes.Black;
            l4d.StrokeThickness = 1;
            canvas.Children.Add(l4d);
            Line l5 = new Line() { X1 = 10, X2 = 10, Y1 = 0, Y2 = 28 };
            l5.Stroke = Brushes.Red;
            l5.StrokeThickness = 2;
            canvas.Children.Add(l5);
            Line l5a = new Line() { X1 = 2, X2 = 10, Y1 = 28, Y2 = 28 };
            l5a.Stroke = Brushes.Red;
            l5a.StrokeThickness = 2;
            canvas.Children.Add(l5a);
            Line l6 = new Line() { X1 = 23, X2 = 23, Y1 = 0, Y2 = 28 };
            l6.Stroke = Brushes.Red;
            l6.StrokeThickness = 2;
            canvas.Children.Add(l6);
            Line l6a = new Line() { X1 = 23, X2 = 30, Y1 = 28, Y2 = 28 };
            l6a.Stroke = Brushes.Red;
            l6a.StrokeThickness = 2;
            canvas.Children.Add(l6a);
            Line l7 = new Line() { X1 = 10, X2 = 23, Y1 = 12, Y2 = 12 };
            l7.Stroke = Brushes.Black;
            l7.StrokeThickness = 2;
            canvas.Children.Add(l7);
            Line l8 = new Line() { X1 = 10, X2 = 23, Y1 = 18, Y2 = 18 };
            l8.Stroke = Brushes.Black;
            l8.StrokeThickness = 2;
            canvas.Children.Add(l8);
            Line l9 = new Line() { X1 = 10, X2 = 23, Y1 = 24, Y2 = 24 };
            l9.Stroke = Brushes.Black;
            l9.StrokeThickness = 2;
            canvas.Children.Add(l9);
            Line l10 = new Line() { X1 = 10, X2 = 23, Y1 = 6, Y2 = 6 };
            l10.Stroke = Brushes.Black;
            l10.StrokeThickness = 2;
            canvas.Children.Add(l10);
        }
        public static void DrawLayout(Canvas canvas)
        {
            Rectangle r1 = new Rectangle() { Width = 32, Height = 32 };
            r1.Stroke = Brushes.Black;
            r1.StrokeThickness = 1;
            Canvas.SetTop(r1, 0);
            Canvas.SetLeft(r1, 0);
            canvas.Children.Add(r1);
            Rectangle r2 = new Rectangle() { Width = 10, Height = 10 };
            r2.Stroke = Brushes.LightGray;
            r2.StrokeThickness = 2;
            Canvas.SetTop(r2, 2);
            Canvas.SetLeft(r2, 2);
            canvas.Children.Add(r2);
            Rectangle r3 = new Rectangle() { Width = 16, Height = 10 };
            r3.Stroke = Brushes.LightGray;
            r3.StrokeThickness = 2;
            Canvas.SetTop(r3, 2);
            Canvas.SetLeft(r3, 14);
            canvas.Children.Add(r3);
            Rectangle r4 = new Rectangle() { Width = 10, Height = 16 };
            r4.Stroke = Brushes.LightGray;
            r4.StrokeThickness = 2;
            Canvas.SetTop(r4, 14);
            Canvas.SetLeft(r4, 2);
            canvas.Children.Add(r4);
            Rectangle r5 = new Rectangle() { Width = 16, Height = 16 };
            r5.Stroke = Brushes.LightGray;
            r5.StrokeThickness = 2;
            Canvas.SetTop(r5, 14);
            Canvas.SetLeft(r5, 14);
            canvas.Children.Add(r5);
        }
        public static void DrawSetting(Canvas canvas)
        {
            Ellipse e1 = new Ellipse() { Width=24,Height=24};
            e1.Stroke = Brushes.Black;
            e1.StrokeThickness = 1;
            Canvas.SetTop(e1, 4);
            Canvas.SetLeft(e1, 4);
            canvas.Children.Add(e1);
            Ellipse e2 = new Ellipse() { Width = 18, Height = 18 };
            e2.Stroke = Brushes.Black;
            e2.StrokeThickness = 1;
            Canvas.SetTop(e2, 7);
            Canvas.SetLeft(e2, 7);
            canvas.Children.Add(e2);
            Line l1 = new Line() { X1 = 13, X2 = 13, Y1 = 0, Y2 = 4 };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = 1;
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = 19, X2 = 19, Y1 = 0, Y2 = 4 };
            l2.Stroke = Brushes.Black;
            l2.StrokeThickness = 1;
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = 13, X2 = 19, Y1 = 0, Y2 = 0 };
            l3.Stroke = Brushes.Black;
            l3.StrokeThickness = 1;
            canvas.Children.Add(l3);
            Line l4 = new Line() { X1 = 13, X2 = 13, Y1 = 28, Y2 = 32};
            l4.Stroke = Brushes.Black;
            l4.StrokeThickness = 1;
            canvas.Children.Add(l4);
            Line l5= new Line() { X1 = 19, X2 = 19, Y1 = 28, Y2 = 32 };
            l5.Stroke = Brushes.Black;
            l5.StrokeThickness = 1;
            canvas.Children.Add(l5);
            Line l6 = new Line() { X1 = 13, X2 = 19, Y1 = 32, Y2 = 32 };
            l6.Stroke = Brushes.Black;
            l6.StrokeThickness = 1;
            canvas.Children.Add(l6);
            Line l7 = new Line() { X1 = 0, X2 = 4, Y1 = 13, Y2 = 13 };
            l7.Stroke = Brushes.Black;
            l7.StrokeThickness = 1;
            canvas.Children.Add(l7);
            Line l8 = new Line() { X1 = 0, X2 = 4, Y1 = 19, Y2 = 19 };
            l8.Stroke = Brushes.Black;
            l8.StrokeThickness = 1;
            canvas.Children.Add(l8);
            Line l9 = new Line() { X1 = 0, X2 = 0, Y1 = 13, Y2 = 19 };
            l9.Stroke = Brushes.Black;
            l9.StrokeThickness = 1;
            canvas.Children.Add(l9);
            Line l10 = new Line() { X1 = 28, X2 = 32, Y1 = 13, Y2 = 13 };
            l10.Stroke = Brushes.Black;
            l10.StrokeThickness = 1;
            canvas.Children.Add(l10);
            Line l11 = new Line() { X1 = 28, X2 = 32, Y1 = 19, Y2 = 19 };
            l11.Stroke = Brushes.Black;
            l11.StrokeThickness = 1;
            canvas.Children.Add(l11);
            Line l12 = new Line() { X1 = 32, X2 = 32, Y1 = 13, Y2 = 19 };
            l12.Stroke = Brushes.Black;
            l12.StrokeThickness = 1;
            canvas.Children.Add(l12);
        }
        public static void DrawReinforcement(Canvas canvas)
        {
            Ellipse e = new Ellipse() { Width = 32, Height = 32 };
            e.Stroke = Brushes.Black;
            e.StrokeThickness = 1;
            Canvas.SetTop(e, 0);
            Canvas.SetLeft(e, 0);
            canvas.Children.Add(e);
            Ellipse e1 = new Ellipse() { Width = 28, Height = 28 };
            e1.Stroke = Brushes.Black;
            e1.StrokeThickness = 2;
            Canvas.SetTop(e1, 2);
            Canvas.SetLeft(e1, 2);
            canvas.Children.Add(e1);
            Ellipse e2 = new Ellipse() { Width = 4, Height = 4 };
            e2.Fill = Brushes.Red;
            Canvas.SetTop(e2, 7);
            Canvas.SetLeft(e2, 7);
            canvas.Children.Add(e2);
            Ellipse e3 = new Ellipse() { Width = 4, Height = 4 };
            e3.Fill = Brushes.Red;
            Canvas.SetTop(e3, 21);
            Canvas.SetLeft(e3, 7);
            canvas.Children.Add(e3);
            Ellipse e4 = new Ellipse() { Width = 4, Height = 4 };
            e4.Fill = Brushes.Red;
            Canvas.SetTop(e4, 7);
            Canvas.SetLeft(e4, 21);
            canvas.Children.Add(e4);
            Ellipse e5 = new Ellipse() { Width = 4, Height = 4 };
            e5.Fill = Brushes.Red;
            Canvas.SetTop(e5, 21);
            Canvas.SetLeft(e5, 21);
            canvas.Children.Add(e5);
        }
        public static void DrawBarsDivision(Canvas canvas)
        {
            Rectangle r1 = new Rectangle() { Width = 32, Height = 16 };
            r1.Stroke = Brushes.Black;
            r1.StrokeThickness = 1;
            Canvas.SetTop(r1, 8);
            Canvas.SetLeft(r1, 0);
            canvas.Children.Add(r1);
            Line l1 = new Line() { X1 = 0, X2 = 32, Y1 = 12, Y2 = 12 };
            l1.Stroke = Brushes.Black;
            l1.StrokeThickness = 2;
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = 0, X2 = 32, Y1 = 20, Y2 = 20 };
            l2.Stroke = Brushes.Black;
            l2.StrokeThickness = 2;
            canvas.Children.Add(l2);
            //for (int i = 1; i < 8; i++)
            //{
            //    Line l3 = new Line() { X1 = i * 4, X2 = i * 4, Y1 = 12, Y2 = 20 };
            //    l3.Stroke = Brushes.Black;
            //    l3.StrokeThickness = 2;
            //    canvas.Children.Add(l3);

            //}
            Line l4 = new Line() { X1 = 0, X2 = 12, Y1 = 12, Y2 = 12 };
            l4.Stroke = Brushes.Red;
            l4.StrokeThickness = 2;
            canvas.Children.Add(l4);
            Line l5 = new Line() { X1 = 20, X2 = 32, Y1 = 12, Y2 = 12 };
            l5.Stroke = Brushes.Red;
            l5.StrokeThickness = 2;
            canvas.Children.Add(l5);
            Line l6 = new Line() { X1 = 0, X2 = 12, Y1 = 20, Y2 = 20 };
            l6.Stroke = Brushes.Red;
            l6.StrokeThickness = 2;
            canvas.Children.Add(l6);
            Line l7 = new Line() { X1 = 20, X2 = 32, Y1 = 20, Y2 = 20 };
            l7.Stroke = Brushes.Red;
            l7.StrokeThickness = 2;
            canvas.Children.Add(l7);
            Line l8 = new Line() { X1 = 8, X2 = 24, Y1 = 16, Y2 = 16 };
            l8.Stroke = Brushes.Red;
            l8.StrokeThickness = 2;
            canvas.Children.Add(l8);
        }
    }
}
