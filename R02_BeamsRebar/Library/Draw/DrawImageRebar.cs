
using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace R02_BeamsRebar
{
    public class DrawImageRebar
    {
        public static void DrawRebar(Canvas canvas, ItemDivision itemDivision)
        {
            canvas.Children.Clear();
            double width = 140; double height = 60;
            double left = 10; double top = 10;
            int font = 11;
            switch (itemDivision.Type)
            {
                case DetailShopStyle.DS00:
                    DrawItem00(canvas, left,top,width,height,itemDivision.L, itemDivision.La, itemDivision.Lb, font);
                    break;
                case DetailShopStyle.DS01:
                    DrawItem01(canvas, left, top, width, height, itemDivision.L, itemDivision.La, itemDivision.Lb, font);
                    break;
                case DetailShopStyle.DS02:
                    DrawItem02(canvas, left, top, width, height, itemDivision.L, itemDivision.La, itemDivision.Lb, font);
                    break;
                case DetailShopStyle.DS03:
                    DrawItem03(canvas, left, top, width, height, itemDivision.L,itemDivision.La,itemDivision.Lb, font);
                    break;
                case DetailShopStyle.DS04:
                    DrawItem04(canvas, left, top, width, height, itemDivision.L, itemDivision.La, itemDivision.Lb, font);
                    break;
                case DetailShopStyle.DS05:
                    DrawItem05(canvas, left, top, width, height, itemDivision.L, itemDivision.Lb, itemDivision.Lb, font);
                    break;
                case DetailShopStyle.DS06:
                    DrawItem06(canvas, left, top, width, height, itemDivision.L, itemDivision.La, itemDivision.Lb, font);
                    break;
                case DetailShopStyle.DS07:
                    DrawItem07(canvas, left, top, width, height, itemDivision.L, itemDivision.La, itemDivision.Lb, font);
                    break;
                case DetailShopStyle.DS08:
                    DrawItem08(canvas, left, top, width, height, itemDivision.L, itemDivision.La, itemDivision.Lb, font);
                    break;
                case DetailShopStyle.DS09:
                    DrawItem09(canvas, left, top, width, height, itemDivision.L, itemDivision.L1, itemDivision.La, itemDivision.Lb, font);
                    break;
                case DetailShopStyle.DS10:
                    DrawItem10(canvas, left, top, width, height, itemDivision.L, itemDivision.La, itemDivision.Lb, font);
                    break;
                case DetailShopStyle.DS11:
                    DrawItem11(canvas, left, top, width, height, itemDivision.L, itemDivision.La, itemDivision.Lb, font);
                    break;
                case DetailShopStyle.DS12:
                    DrawItem12(canvas, left, top, width, height, itemDivision.L, itemDivision.La, itemDivision.Lb, font);
                    break;
                case DetailShopStyle.DS13:
                    DrawItem13(canvas, left, top, width, height, itemDivision.L, itemDivision.La, itemDivision.Lb, font);
                    break;
                default:
                    break;
            }
        }
        private static void DrawItem00(Canvas canvas, double left, double top, double width, double height, double length, double la, double lb, int font)
        {
            DrawLine(canvas, left, width - left,  top, top);
            TextBlock text1 = GetPropertyTextBlock(length, font);
            Canvas.SetTop(text1,top+0.5*text1.ActualHeight);
            Canvas.SetLeft(text1, width / 2 - text1.ActualWidth / 2);
            canvas.Children.Add(text1);
        }
        private static void DrawItem01(Canvas canvas, double left, double top, double width, double height, double length, double la, double lb, int font)
        {
            DrawLine(canvas, left, width - left, top, top);
            TextBlock text1 = GetPropertyTextBlock(length, font);
            Canvas.SetTop(text1, top + 0.5 * text1.ActualHeight);
            Canvas.SetLeft(text1, width / 2 - text1.ActualWidth / 2);
            canvas.Children.Add(text1);
            DrawLine(canvas, left,  left, top,height- top);
            TextBlock text2 = GetPropertyTextBlock(la, font);
            text2.LayoutTransform = new RotateTransform(90, 25, 25);
            Canvas.SetTop(text2, height/2 - 0.5 * text2.ActualHeight);
            Canvas.SetLeft(text2, left );
            canvas.Children.Add(text2);
        }
        private static void DrawItem02(Canvas canvas, double left, double top, double width, double height, double length, double la, double lb, int font)
        {
            DrawLine(canvas, left, width - left, top, top);
            TextBlock text1 = GetPropertyTextBlock(length, font);
            Canvas.SetTop(text1, top + 0.5 * text1.ActualHeight);
            Canvas.SetLeft(text1, width / 2 - text1.ActualWidth / 2);
            canvas.Children.Add(text1);
            DrawLine(canvas,width- left,width- left, top, height - top);
            TextBlock text3 = GetPropertyTextBlock(lb, font);
            text3.LayoutTransform = new RotateTransform(90, 25, 25);
            Canvas.SetTop(text3, height / 2 - 0.5 * text3.ActualHeight);
            Canvas.SetLeft(text3,width- left-text3.ActualHeight);
            canvas.Children.Add(text3);
        }
        private static void DrawItem03(Canvas canvas, double left, double top, double width, double height, double length, double la, double lb, int font)
        {
            DrawLine(canvas, left, width - left, top, top);
            TextBlock text1 = GetPropertyTextBlock(length, font);
            Canvas.SetTop(text1, top + 0.5 * text1.ActualHeight);
            Canvas.SetLeft(text1, width / 2 - text1.ActualWidth / 2);
            canvas.Children.Add(text1);
            DrawLine(canvas, left, left, top, height - top);
            TextBlock text2 = GetPropertyTextBlock(la, font);
            text2.LayoutTransform = new RotateTransform(90, 25, 25);
            Canvas.SetTop(text2, height / 2 - 0.5 * text2.ActualHeight);
            Canvas.SetLeft(text2, left);
            canvas.Children.Add(text2);
            DrawLine(canvas, width - left, width - left, top, height - top);
            TextBlock text3 = GetPropertyTextBlock(lb, font);
            text3.LayoutTransform = new RotateTransform(90, 25, 25);
            Canvas.SetTop(text3, height / 2 - 0.5 * text3.ActualHeight);
            Canvas.SetLeft(text3, width - left - text3.ActualHeight);
            canvas.Children.Add(text3);
        }
        private static void DrawItem04(Canvas canvas, double left, double top, double width, double height, double length, double la, double lb, int font)
        {
            DrawLine(canvas, left, width - left,height- top,height- top);
            TextBlock text1 = GetPropertyTextBlock(length, font);
            Canvas.SetTop(text1,height- top - 1.5 * text1.ActualHeight);
            Canvas.SetLeft(text1, width / 2 - text1.ActualWidth / 2);
            canvas.Children.Add(text1);
            DrawLine(canvas, left, left, top, height - top);
            TextBlock text2 = GetPropertyTextBlock(la, font);
            text2.LayoutTransform = new RotateTransform(90, 25, 25);
            Canvas.SetTop(text2, height / 2 - 0.5 * text2.ActualHeight);
            Canvas.SetLeft(text2, left);
            canvas.Children.Add(text2);
        }
        private static void DrawItem05(Canvas canvas, double left, double top, double width, double height, double length, double la, double lb, int font)
        {
            DrawLine(canvas, left, width - left, height - top, height - top);
            TextBlock text1 = GetPropertyTextBlock(length, font);
            Canvas.SetTop(text1, height - top - 1.5 * text1.ActualHeight);
            Canvas.SetLeft(text1, width / 2 - text1.ActualWidth / 2);
            canvas.Children.Add(text1);
            DrawLine(canvas, width - left, width - left, top, height - top);
            TextBlock text3 = GetPropertyTextBlock(lb, font);
            text3.LayoutTransform = new RotateTransform(90, 25, 25);
            Canvas.SetTop(text3, height / 2 - 0.5 * text3.ActualHeight);
            Canvas.SetLeft(text3, width - left - text3.ActualHeight);
            canvas.Children.Add(text3);
        }
        private static void DrawItem06(Canvas canvas, double left, double top, double width, double height, double length, double la, double lb, int font)
        {
            DrawLine(canvas, left, width - left, height - top, height - top);
            TextBlock text1 = GetPropertyTextBlock(length, font);
            Canvas.SetTop(text1, height - top - 1.5 * text1.ActualHeight);
            Canvas.SetLeft(text1, width / 2 - text1.ActualWidth / 2);
            canvas.Children.Add(text1);
            DrawLine(canvas, left, left, top, height - top);
            TextBlock text2 = GetPropertyTextBlock(la, font);
            text2.LayoutTransform = new RotateTransform(90, 25, 25);
            Canvas.SetTop(text2, height / 2 - 0.5 * text2.ActualHeight);
            Canvas.SetLeft(text2, left);
            canvas.Children.Add(text2);
            DrawLine(canvas, width - left, width - left, top, height - top);
            TextBlock text3 = GetPropertyTextBlock(lb, font);
            text3.LayoutTransform = new RotateTransform(90, 25, 25);
            Canvas.SetTop(text3, height / 2 - 0.5 * text3.ActualHeight);
            Canvas.SetLeft(text3, width - left - text3.ActualHeight);
            canvas.Children.Add(text3);
        }
        private static void DrawItem07(Canvas canvas, double left, double top, double width, double height, double length, double la, double lb, int font)
        {
            double a = (width - 2 * left) / 3;
            DrawLine(canvas, left, left+a,  top,  top);
            TextBlock text1 = GetPropertyTextBlock(la, font);
            Canvas.SetTop(text1, top );
            Canvas.SetLeft(text1, left+(a)/2 - text1.ActualWidth / 2);
            canvas.Children.Add(text1);
            DrawLine(canvas, left+a, left + 2*a, top,height- top);
            TextBlock text2 = GetPropertyTextBlock(length, font);
            Canvas.SetTop(text2, height/2 - 1.5 * text2.ActualHeight);
            Canvas.SetLeft(text2, width/2);
            canvas.Children.Add(text2);
            DrawLine(canvas, left + 2*a, left + 3 * a,height- top, height - top);
            TextBlock text3 = GetPropertyTextBlock(lb, font);
            Canvas.SetTop(text3, height -top - 1.5 * text3.ActualHeight);
            Canvas.SetLeft(text3, left+2.5*a-text3.ActualWidth/2);
            canvas.Children.Add(text3);
        }
        private static void DrawItem08(Canvas canvas, double left, double top, double width, double height, double length, double la, double lb, int font)
        {
            double a = (width - 2 * left) / 3;
            DrawLine(canvas, left, left + a,height- top,height- top);
            TextBlock text1 = GetPropertyTextBlock(la, font);
            Canvas.SetTop(text1,height- top - 1.5 * text1.ActualHeight);
            Canvas.SetLeft(text1, left + (a) / 2 - text1.ActualWidth / 2);
            canvas.Children.Add(text1);
            DrawLine(canvas, left + a, left + 2 * a,height- top,  top);
            TextBlock text2 = GetPropertyTextBlock(length, font);
            Canvas.SetTop(text2, height / 2 - 1.5 * text2.ActualHeight);
            Canvas.SetLeft(text2, width / 2-text2.ActualWidth);
            canvas.Children.Add(text2);
            DrawLine(canvas, left + 2 * a, left + 3 * a, top, top);
            TextBlock text3 = GetPropertyTextBlock(lb, font);
            Canvas.SetTop(text3,  top );
            Canvas.SetLeft(text3, left + 2.5 * a - text3.ActualWidth / 2);
            canvas.Children.Add(text3);
        }
        private static void DrawItem09(Canvas canvas, double left, double top, double width, double height, double length, double l1, double la, double lb, int font)
        {
            double a = (width - 2 * left) / 5;
            DrawLine(canvas, left, left + a,  top,  top);
            DrawLine(canvas, left+a, left + 2*a,  top,height-  top);
            DrawLine(canvas, left+2*a, left + 3*a,height-  top,height-  top);
            DrawLine(canvas, left+3*a, left + 4*a,height-  top, top);
            DrawLine(canvas, left+4*a, left + 5*a,  top, top);
            TextBlock text1 = GetPropertyTextBlock(la, font);
            Canvas.SetTop(text1, top);
            Canvas.SetLeft(text1, left + (a) / 2 - text1.ActualWidth / 2);
            canvas.Children.Add(text1);
            TextBlock text2 = GetPropertyTextBlock(l1, font);
            Canvas.SetTop(text2, height / 2 );
            Canvas.SetLeft(text2, left+1.5*a - text2.ActualWidth);
            canvas.Children.Add(text2);
            TextBlock text3 = GetPropertyTextBlock(length, font);
            Canvas.SetTop(text3, height - top - 1.5 * text1.ActualHeight);
            Canvas.SetLeft(text3, width/2 - text3.ActualWidth / 2);
            canvas.Children.Add(text3);
            TextBlock text4 = GetPropertyTextBlock(l1, font);
            Canvas.SetTop(text4, height / 2 - 1.5 * text4.ActualHeight);
            Canvas.SetLeft(text4, left + 3.5 * a - text4.ActualWidth);
            canvas.Children.Add(text4);
            TextBlock text5 = GetPropertyTextBlock(lb, font);
            Canvas.SetTop(text5, top);
            Canvas.SetLeft(text5, left + (4.5*a) / 2 - text5.ActualWidth / 2);
            canvas.Children.Add(text5);
        }
        private static void DrawItem10(Canvas canvas, double left, double top, double width, double height, double length, double la, double lb, int font)
        {
            DrawImage.DrawStirrup(canvas, 0, 0, 1, width, height, left, 3, 6, Brushes.Red);
            TextBlock text1 = GetPropertyTextBlock(la, font);
            Canvas.SetTop(text1, top);
            Canvas.SetLeft(text1,width/2 - text1.ActualWidth / 2);
            canvas.Children.Add(text1);
            TextBlock text2 = GetPropertyTextBlock(lb, font);
            text2.LayoutTransform = new RotateTransform(90, 25, 25);
            Canvas.SetTop(text2, height/2-0.5*text2.ActualWidth);
            Canvas.SetLeft(text2, left);
            canvas.Children.Add(text2);
        }
        private static void DrawItem11(Canvas canvas, double left, double top, double width, double height, double length, double la, double lb, int font)
        {
            DrawImage.DrawHook(canvas, 0, 0, 1, width, left, 3, 6, Math.PI / 2, Brushes.Red);
            TextBlock text1 = GetPropertyTextBlock(la, font);
            Canvas.SetTop(text1, height - top - 1.5 * text1.ActualHeight);
            Canvas.SetLeft(text1, width / 2 - text1.ActualWidth / 2);
            canvas.Children.Add(text1);
        }
        private static void DrawItem12(Canvas canvas, double left, double top, double width, double height, double length, double la, double lb, int font)
        {
            DrawImage.DrawHook(canvas, 0, 0, 1, width, left, 3, 6, 0.75*Math.PI, Brushes.Red);
            TextBlock text1 = GetPropertyTextBlock(la, font);
            Canvas.SetTop(text1, height - top - 1.5 * text1.ActualHeight);
            Canvas.SetLeft(text1, width / 2 - text1.ActualWidth / 2);
            canvas.Children.Add(text1);
        }
        private static void DrawItem13(Canvas canvas, double left, double top, double width, double height, double length, double la, double lb, int font)
        {
            DrawImage.DrawHook(canvas, 0, 0, 1, width, left, 3, 6, Math.PI , Brushes.Red);
            TextBlock text1 = GetPropertyTextBlock(la, font);
            Canvas.SetTop(text1, height - top - 1.5 * text1.ActualHeight);
            Canvas.SetLeft(text1, width / 2 - text1.ActualWidth / 2);
            canvas.Children.Add(text1);
        }
        #region 

        private static void DrawLine(Canvas canvas, double x1,double x2,double y1, double y2)
        {
            Line l = new Line() { X1 = x1, X2 = x2, Y1 = y1, Y2 = y2 };
            l.Stroke = Brushes.Red;
            l.StrokeThickness = 3;
            canvas.Children.Add(l);
        }
        private static TextBlock GetPropertyTextBlock(double delta, int font)
        {
            TextBlock text = new TextBlock();
            text.Text =Math.Round( delta ,2)+ "";
            text.FontSize = font;
            text.Foreground = Brushes.Black;
            text.FontFamily = new FontFamily("Tahoma");
            text.Measure(new Size(System.Double.PositiveInfinity, System.Double.PositiveInfinity));
            text.Arrange(new Rect(text.DesiredSize));
            text.SnapsToDevicePixels = true;
            text.SetValue(RenderOptions.BitmapScalingModeProperty, BitmapScalingMode.HighQuality);
            return text;
        }
        #endregion

        #region render
        public static void ConvertCanvasToBitmap(Canvas canvas, string filename)
        {
            double actualWidth = canvas.RenderSize.Width;
            double actualHeight = canvas.RenderSize.Height;
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap((int)actualWidth,(int)actualHeight,96d,96d, PixelFormats.Pbgra32);

            //RenderTargetBitmap renderBitmap = new RenderTargetBitmap(1800, 200,96d, 96d, PixelFormats.Pbgra32);
            // needed otherwise the image output is black
           
            canvas.Measure(new Size((int)actualWidth, (int)actualHeight));
            canvas.Arrange(new Rect(new Size((int)actualWidth, (int)actualHeight)));
           
            renderBitmap.Render(canvas);
            RenderOptions.SetBitmapScalingMode(renderBitmap, BitmapScalingMode.HighQuality);
            //JpegBitmapEncoder encoder = new JpegBitmapEncoder();

            // for png bitmap
            PngBitmapEncoder encoder = new PngBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            using (FileStream fs = File.Create(filename))
            {
                encoder.Save(fs);
            }
        }
        public static class RenderVisualService
        {
            private const double defaultDpi = 96.0;

            public static ImageSource RenderToPNGImageSource(System.Windows.Media.Visual targetControl)
            {
                var renderTargetBitmap = GetRenderTargetBitmapFromControl(targetControl);

                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

                var result = new BitmapImage();

                using (var memoryStream = new MemoryStream())
                {
                    encoder.Save(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    result.BeginInit();
                    result.CacheOption = BitmapCacheOption.OnLoad;
                    result.StreamSource = memoryStream;
                    result.EndInit();
                }

                return result;
            }

            public static void RenderToPNGFile(System.Windows.Media.Visual targetControl, string filename)
            {
                var renderTargetBitmap = GetRenderTargetBitmapFromControl(targetControl);

                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

                var result = new BitmapImage();

                try
                {
                    using (var fileStream = new FileStream(filename, FileMode.Create))
                    {
                        encoder.Save(fileStream);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"There was an error saving the file: {ex.Message}");
                }
            }

            private static BitmapSource GetRenderTargetBitmapFromControl(System.Windows.Media.Visual targetControl, double dpi = defaultDpi)
            {
                if (targetControl == null) return null;

                var bounds = System.Windows.Media.VisualTreeHelper.GetDescendantBounds(targetControl);
                var renderTargetBitmap = new RenderTargetBitmap((int)(bounds.Width * dpi / 96.0),
                                                                (int)(bounds.Height * dpi / 96.0),
                                                                dpi,
                                                                dpi,
                                                                System.Windows.Media.PixelFormats.Pbgra32);

                var drawingVisual = new System.Windows.Media.DrawingVisual();

                using (var drawingContext = drawingVisual.RenderOpen())
                {
                    var visualBrush = new System.Windows.Media.VisualBrush(targetControl);
                    drawingContext.DrawRectangle(visualBrush, null, new Rect(new System.Windows.Point(), bounds.Size));
                }

                renderTargetBitmap.Render(drawingVisual);
                return renderTargetBitmap;
            }
        }
       
        private BitmapImage BmpImageFromBmp(System.Drawing.Bitmap bmp)
        {
            using (var memory = new MemoryStream())
            {
                bmp.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
        #endregion
    }
}
