using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace R02_BeamsRebar
{
    public class CanvasDetailItem
    {
        public Canvas Canvas { get; set; }
        public CanvasDetailItem(Grid grid, int column, int row)
        {
            Canvas = new Canvas();
            //Canvas.Background = Brushes.Ivory;
            Grid.SetColumn(Canvas, column);
            Grid.SetRow(Canvas, row);
            grid.Children.Add(Canvas);
        }
        public void Draw(DetailShopStyle style)
        {
            switch (style)
            {
                case DetailShopStyle.DS00:
                    DrawStyle16(0, DetailShopStyle.DS00.ToString());
                    break;
                case DetailShopStyle.DS01:
                    DrawStyle16(1, DetailShopStyle.DS01.ToString());
                    break;
                case DetailShopStyle.DS02:
                    DrawStyle16(2, DetailShopStyle.DS02.ToString());
                    break;
                case DetailShopStyle.DS03:
                    DrawStyle16(3, DetailShopStyle.DS03.ToString());
                    break;
                case DetailShopStyle.DS04:
                    DrawStyle16(4, DetailShopStyle.DS04.ToString());
                    break;
                case DetailShopStyle.DS05:
                    DrawStyle16(5, DetailShopStyle.DS05.ToString());
                    break;
                case DetailShopStyle.DS06:
                    DrawStyle16(6, DetailShopStyle.DS06.ToString());
                    break;
                case DetailShopStyle.DS07:
                    DrawStyle712(7, DetailShopStyle.DS07.ToString());
                    break;
                case DetailShopStyle.DS08:
                    DrawStyle712(8, DetailShopStyle.DS08.ToString());
                    break;
                case DetailShopStyle.DS09:
                    DrawStyle9(DetailShopStyle.DS09.ToString());
                    break;
                case DetailShopStyle.DS10:
                    DrawStyle10(DetailShopStyle.DS10.ToString());
                    break;
                case DetailShopStyle.DS11:
                    DrawStyle123(DetailShopStyle.DS11.ToString());
                    break;
                case DetailShopStyle.DS12:
                    DrawStyle123(DetailShopStyle.DS12.ToString());
                    break;
                case DetailShopStyle.DS13:
                    DrawStyle123(DetailShopStyle.DS13.ToString());
                    break;
                default:
                    break;
            }
        }

        private void DrawStyle123(string v)
        {
            double hook = 0;
            if (v.Equals(DetailShopStyle.DS11.ToString()))
            {
                hook = Math.PI / 2;
            }
            if (v.Equals(DetailShopStyle.DS12.ToString()))
            {
                hook = 0.75 * Math.PI;
            }
            if (v.Equals(DetailShopStyle.DS13.ToString()))
            {
                hook = Math.PI;
            }
            DrawImage.DrawHook(Canvas, 10, 30, 1, 120, 5, 2, 12, hook, Brushes.Red);
            TextBlock text = new TextBlock();
            text.Text = v;
            text.FontSize = 11;
            text.Foreground = Brushes.Black;
            text.FontFamily = new FontFamily("Tahoma");
            text.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text.Arrange(new Rect(text.DesiredSize));
            Canvas.SetTop(text, 0);
            Canvas.SetLeft(text, 70 - text.ActualWidth / 2);
            Canvas.Children.Add(text);
            TextBlock text1 = new TextBlock();
            text1.Text = "L";
            text1.FontSize = 11;
            text1.Foreground = Brushes.Black;
            text1.FontFamily = new FontFamily("Tahoma");
            text1.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text1.Arrange(new Rect(text1.DesiredSize));
            Canvas.SetTop(text1, 17);
            Canvas.SetLeft(text1, 70 - text1.ActualWidth / 2);
            Canvas.Children.Add(text1);
            TextBlock text2 = new TextBlock();
            text2.Text = "La";
            text2.FontSize = 11;
            text2.Foreground = Brushes.Black;
            text2.FontFamily = new FontFamily("Tahoma");
            text2.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text2.Arrange(new Rect(text2.DesiredSize));
            Canvas.SetTop(text2, 17);
            Canvas.SetLeft(text2, 25 - text2.ActualWidth / 2);
            Canvas.Children.Add(text2);
            TextBlock text3 = new TextBlock();
            text3.Text = "Lb";
            text3.FontSize = 11;
            text3.Foreground = Brushes.Black;
            text3.FontFamily = new FontFamily("Tahoma");
            text3.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text3.Arrange(new Rect(text3.DesiredSize));
            Canvas.SetTop(text3, 17);
            Canvas.SetLeft(text3, 110 - text3.ActualWidth / 2);
            Canvas.Children.Add(text3);
        }

        private void DrawStyle10(string ds)
        {
            DrawImage.DrawStirrup(Canvas, 10, 10, 1, 120, 40, 5, 2, 12, Brushes.Red);
            TextBlock text = new TextBlock();
            text.Text = ds;
            text.FontSize = 11;
            text.Foreground = Brushes.Black;
            text.FontFamily = new FontFamily("Tahoma");
            text.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text.Arrange(new Rect(text.DesiredSize));
            Canvas.SetTop(text, 17);
            Canvas.SetLeft(text, 70 - text.ActualWidth / 2);
            Canvas.Children.Add(text);
            TextBlock text1 = new TextBlock();
            text1.Text = "La";
            text1.FontSize = 11;
            text1.Foreground = Brushes.Black;
            text1.FontFamily = new FontFamily("Tahoma");
            text1.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text1.Arrange(new Rect(text1.DesiredSize));
            Canvas.SetTop(text1, 0);
            Canvas.SetLeft(text1, 70 - text1.ActualWidth / 2);
            Canvas.Children.Add(text1);
            TextBlock text2 = new TextBlock();
            text2.Text = "Lb";
            text2.FontSize = 11;
            text2.Foreground = Brushes.Black;
            text2.FontFamily = new FontFamily("Tahoma");
            text2.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text2.Arrange(new Rect(text2.DesiredSize));
            text2.LayoutTransform = new RotateTransform(90, 25, 25);
            Canvas.SetTop(text2, 25 - text2.ActualWidth / 2);
            Canvas.SetLeft(text2, 0);
            Canvas.Children.Add(text2);
            TextBlock text3 = new TextBlock();
            text3.Text = "L";
            text3.FontSize = 11;
            text3.Foreground = Brushes.Black;
            text3.FontFamily = new FontFamily("Tahoma");
            text3.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text3.Arrange(new Rect(text3.DesiredSize));
            Canvas.SetTop(text3, 17 );
            Canvas.SetLeft(text3, 20);
            Canvas.Children.Add(text3);
        }

        private void DrawStyle9(string ds)
        {
            Line l1 = new Line() { X1 = 0, X2 = 25, Y1 = 30, Y2 = 30 };
            l1.Stroke = Brushes.Red;
            l1.StrokeThickness = 2;
            Canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = 25, X2 = 55, Y1 = 30, Y2 = 50 };
            l2.Stroke = Brushes.Red;
            l2.StrokeThickness = 2;
            Canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = 55, X2 = 85, Y1 = 50, Y2 = 50 };
            l3.Stroke = Brushes.Red;
            l3.StrokeThickness = 2;
            Canvas.Children.Add(l3);
            Line l4 = new Line() { X1 = 85, X2 = 115, Y1 = 50, Y2 = 30 };
            l4.Stroke = Brushes.Red;
            l4.StrokeThickness = 2;
            Canvas.Children.Add(l4);
            Line l5 = new Line() { X1 = 115, X2 = 140, Y1 = 30, Y2 = 30 };
            l5.Stroke = Brushes.Red;
            l5.StrokeThickness = 2;
            Canvas.Children.Add(l5);
            TextBlock text = new TextBlock();
            text.Text = ds;
            text.FontSize = 11;
            text.Foreground = Brushes.Black;
            text.FontFamily = new FontFamily("Tahoma");
            text.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text.Arrange(new Rect(text.DesiredSize));
            Canvas.SetTop(text, 5);
            Canvas.SetLeft(text, 70 - text.ActualWidth / 2);
            Canvas.Children.Add(text);
            TextBlock text1 = new TextBlock();
            text1.Text = "L";
            text1.FontSize = 11;
            text1.Foreground = Brushes.Black;
            text1.FontFamily = new FontFamily("Tahoma");
            text1.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text1.Arrange(new Rect(text1.DesiredSize));
            Canvas.SetTop(text1, 30);
            Canvas.SetLeft(text1, 70 - text1.ActualWidth / 2);
            Canvas.Children.Add(text1);
        }

        private void DrawStyle16(int v, string ds)
        {
            Line l1 = new Line() { X1 = 30, X2 = 110, Y1 = (v <= 3) ? 30 : 50, Y2 = (v <= 3) ? 30 : 50 };
            l1.Stroke = Brushes.Red;
            l1.StrokeThickness = 2;
            Canvas.Children.Add(l1);
            TextBlock text = new TextBlock();
            text.Text = ds;
            text.FontSize = 11;
            text.Foreground = Brushes.Black;
            text.FontFamily = new FontFamily("Tahoma");
            text.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text.Arrange(new Rect(text.DesiredSize));
            Canvas.SetTop(text, 5);
            Canvas.SetLeft(text, 70 - text.ActualWidth / 2);
            Canvas.Children.Add(text);
            TextBlock text1 = new TextBlock();
            text1.Text = "L";
            text1.FontSize = 11;
            text1.Foreground = Brushes.Black;
            text1.FontFamily = new FontFamily("Tahoma");
            text1.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text1.Arrange(new Rect(text1.DesiredSize));
            Canvas.SetTop(text1, 30);
            Canvas.SetLeft(text1, 70 - text1.ActualWidth / 2);
            Canvas.Children.Add(text1);
            if ((v == 1) || (v == 3) || (v == 4) || (v == 6))
            {
                TextBlock text2 = new TextBlock();
                text2.Text = "La";
                text2.FontSize = 11;
                text2.Foreground = Brushes.Black;
                text2.FontFamily = new FontFamily("Tahoma");
                text2.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
                text2.Arrange(new Rect(text2.DesiredSize));
                text2.LayoutTransform = new RotateTransform(90, 25, 25);
                Canvas.SetTop(text2, 35 - text2.ActualWidth / 2);
                Canvas.SetLeft(text2, 30);
                Canvas.Children.Add(text2);
            }
            if ((v == 2) || (v == 3) || (v == 5) || (v == 6))
            {
                TextBlock text3 = new TextBlock();
                text3.Text = "Lb";
                text3.FontSize = 11;
                text3.Foreground = Brushes.Black;
                text3.FontFamily = new FontFamily("Tahoma");
                text3.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
                text3.Arrange(new Rect(text3.DesiredSize));
                text3.LayoutTransform = new RotateTransform(-90, 25, 25);
                Canvas.SetTop(text3, 35 - text3.ActualWidth / 2);
                Canvas.SetLeft(text3, 100 - 11);
                Canvas.Children.Add(text3);
            }
            Line l2 = null;
            Line l3 = null;
            switch (v)
            {
                case 1:
                    l2 = new Line() { X1 = 30, X2 = 30, Y1 = 30, Y2 = 50 };
                    l2.Stroke = Brushes.Red;
                    l2.StrokeThickness = 2;
                    Canvas.Children.Add(l2);
                    break;
                case 2:
                    l2 = new Line() { X1 = 110, X2 = 110, Y1 = 30, Y2 = 50 };
                    l2.Stroke = Brushes.Red;
                    l2.StrokeThickness = 2;
                    Canvas.Children.Add(l2);
                    break;
                case 3:
                    l2 = new Line() { X1 = 30, X2 = 30, Y1 = 30, Y2 = 50 };
                    l2.Stroke = Brushes.Red;
                    l2.StrokeThickness = 2;
                    Canvas.Children.Add(l2);
                    l3 = new Line() { X1 = 110, X2 = 110, Y1 = 30, Y2 = 50 };
                    l3.Stroke = Brushes.Red;
                    l3.StrokeThickness = 2;
                    Canvas.Children.Add(l3);
                    break;
                case 4:
                    l2 = new Line() { X1 = 30, X2 = 30, Y1 = 30, Y2 = 50 };
                    l2.Stroke = Brushes.Red;
                    l2.StrokeThickness = 2;
                    Canvas.Children.Add(l2);
                    break;
                case 5:
                    l2 = new Line() { X1 = 110, X2 = 110, Y1 = 30, Y2 = 50 };
                    l2.Stroke = Brushes.Red;
                    l2.StrokeThickness = 2;
                    Canvas.Children.Add(l2);
                    break;
                case 6:
                    l2 = new Line() { X1 = 30, X2 = 30, Y1 = 30, Y2 = 50 };
                    l2.Stroke = Brushes.Red;
                    l2.StrokeThickness = 2;
                    Canvas.Children.Add(l2);
                    l3 = new Line() { X1 = 110, X2 = 110, Y1 = 30, Y2 = 50 };
                    l3.Stroke = Brushes.Red;
                    l3.StrokeThickness = 2;
                    Canvas.Children.Add(l3);
                    break;
                default: break;
            }
        }
        private void DrawStyle712(int v, string ds)
        {
            Line l1 = new Line() { X1 = 0, X2 = 40, Y1 = (v == 7) ? 30 : 50, Y2 = (v == 7) ? 30 : 50 };
            l1.Stroke = Brushes.Red;
            l1.StrokeThickness = 2;
            Canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = 100, X2 = 140, Y1 = (v == 7) ? 50 : 30, Y2 = (v == 7) ? 50 : 30 };
            l2.Stroke = Brushes.Red;
            l2.StrokeThickness = 2;
            Canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = 40, X2 = 100, Y1 = (v == 7) ? 30 : 50, Y2 = (v == 7) ? 50 : 30 };
            l3.Stroke = Brushes.Red;
            l3.StrokeThickness = 2;
            Canvas.Children.Add(l3);
            TextBlock text = new TextBlock();
            text.Text = ds;
            text.FontSize = 11;
            text.Foreground = Brushes.Black;
            text.FontFamily = new FontFamily("Tahoma");
            text.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text.Arrange(new Rect(text.DesiredSize));
            Canvas.SetTop(text, 5);
            Canvas.SetLeft(text, 70 - text.ActualWidth / 2);
            Canvas.Children.Add(text);
            TextBlock text1 = new TextBlock();
            text1.Text = "L";
            text1.FontSize = 11;
            text1.Foreground = Brushes.Black;
            text1.FontFamily = new FontFamily("Tahoma");
            text1.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text1.Arrange(new Rect(text1.DesiredSize));
            Canvas.SetTop(text1, 35);
            Canvas.SetLeft(text1, (v == 7) ? 60 - text.ActualWidth / 2 : 90 - text.ActualWidth / 2);
            Canvas.Children.Add(text1);
            TextBlock text2 = new TextBlock();
            text2.Text = "La";
            text2.FontSize = 11;
            text2.Foreground = Brushes.Black;
            text2.FontFamily = new FontFamily("Tahoma");
            text2.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text2.Arrange(new Rect(text2.DesiredSize));
            Canvas.SetTop(text2, 30);
            Canvas.SetLeft(text2, 20 - text.ActualWidth / 2);
            Canvas.Children.Add(text2);
            TextBlock text3 = new TextBlock();
            text3.Text = "Lb";
            text3.FontSize = 11;
            text3.Foreground = Brushes.Black;
            text3.FontFamily = new FontFamily("Tahoma");
            text3.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text3.Arrange(new Rect(text3.DesiredSize));
            Canvas.SetTop(text3, 30);
            Canvas.SetLeft(text3, 120 - text.ActualWidth / 2);
            Canvas.Children.Add(text3);
        }
    }
}