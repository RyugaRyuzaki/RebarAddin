

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfCustomControls
{
    public class DrawIcon
    {
        /// <summary>
        /// vẽ icon của geometry menu
        /// </summary>
        /// <param name="canvas"></param>
        public static void DrawLogo(Canvas canvas)
        {
            DrawLogo1(canvas);
            DrawLogo2(canvas);
            DrawLogo3(canvas);
        }
        private static void DrawLogo1(Canvas canvas)
        {
            PointCollection points = new PointCollection();
            Point p1 = new Point() { X = 28, Y = 5 };
            Point p2 = new Point() { X = 5, Y = 5+(28-5)/3 };
            Point p3 = new Point() { X = 5, Y = 5 + (28 - 5) / 3 +5};
            Point p4 = new Point() { X = 10, Y = 5 + (28 - 5) / 3 + 5 -5/3};
            Point p5 = new Point() { X = 10, Y = 33 };
            Point p6 = new Point() { X = 14, Y = 33 };
            Point p7 = new Point() { X = 14, Y = 5 + (28 - 5) / 3 + 5 - 10 / 3 };
            Point p8 = new Point() { X = 28, Y = 10 };
            Point p9 = new Point() { X = 51, Y = 5 + (28 - 5) / 3 + 5 - 10 / 3 };
            Point p10 = new Point() { X = 51, Y = 5 + (28 - 5) / 3  - 10 / 3 };

            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            points.Add(p5);
            points.Add(p6);
            points.Add(p7);
            points.Add(p8);
            points.Add(p9);
            points.Add(p10);
            Polygon polygon = new Polygon();
            polygon.Fill = Brushes.Maroon;
            polygon.Points = points;
            canvas.Children.Add(polygon);
        }
        private static void DrawLogo2(Canvas canvas)
        {
            PointCollection points = new PointCollection();
            Point p1 = new Point() { X = 28, Y = 0 };
            Point p2 = new Point() { X = 0, Y = (28 ) / 3 };
            Point p3 = new Point() { X = 0, Y =  (28 ) / 3 + 12 };
            Point p4 = new Point() { X = 5, Y =  (28 ) / 3 + 12 - 5 / 3 };
            Point p5 = new Point() { X = 5, Y = 28 };
            Point p6 = new Point() { X = 10, Y = 33 };
            Point p7 = new Point() { X = 10, Y = 5 + (28 - 5) / 3 + 5 - 5 / 3 };
            Point p8 = new Point() { X = 5, Y = 5 + (28 - 5) / 3 + 5 };
            Point p9 = new Point() { X = 5, Y = 5 + (28 - 5) / 3 };
            Point p10 = new Point() { X = 28, Y = 5 };
            Point p11 = new Point() { X = 51, Y = 5 + (28 - 5) / 3  - 10 / 3 };
            Point p12 = new Point() { X = 51, Y = 5 + (28 - 5) / 3 + 5 - 10 / 3 };
            Point p13 = new Point() { X = 56, Y = 5 + (28 - 5) / 3 + 5 - 10 / 3 };
            Point p14 = new Point() { X = 56, Y = 5 + (28 - 5) / 3 - 15 / 3 };
           
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            points.Add(p5);
            points.Add(p6);
            points.Add(p7);
            points.Add(p8);
            points.Add(p9);
            points.Add(p10);
            points.Add(p11);
            points.Add(p12);
            points.Add(p13);
            points.Add(p14);
            Polygon polygon = new Polygon();
            polygon.Fill = Brushes.BurlyWood;
            polygon.Points = points;
            canvas.Children.Add(polygon);
        }
        private static void DrawLogo3(Canvas canvas)
        {
            TextBlock text = new TextBlock();
            text.Text ="DS+";
            text.FontSize = 22;
            text.FontWeight = FontWeights.Bold;
            text.FontStyle = FontStyles.Italic;
            text.Foreground = Brushes.DarkSlateGray;
            text.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text.Arrange(new Rect(text.DesiredSize));
            Canvas.SetTop(text, 3 );
            Canvas.SetLeft(text, 34-text.ActualWidth/2);
            canvas.Children.Add(text);
        }
    }
}
