
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace R02_BeamsRebar
{
    public class CreateImage
    {
        public static class RenderVisualService
        {
            private const double defaultDpi = 96.0;

            public static ImageSource RenderToPNGImageSource(Visual targetControl)
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

            public static void RenderToPNGFile(Visual targetControl, string filename)
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

            private static BitmapSource GetRenderTargetBitmapFromControl(Visual targetControl, double dpi = defaultDpi)
            {
                if (targetControl == null) return null;

                var bounds = VisualTreeHelper.GetDescendantBounds(targetControl);
                var renderTargetBitmap = new RenderTargetBitmap((int)(bounds.Width * dpi / 96.0),
                                                                (int)(bounds.Height * dpi / 96.0),
                                                                dpi,
                                                                dpi,
                                                                PixelFormats.Pbgra32);

                var drawingVisual = new DrawingVisual();

                using (var drawingContext = drawingVisual.RenderOpen())
                {
                    var visualBrush = new VisualBrush(targetControl);
                    drawingContext.DrawRectangle(visualBrush, null, new Rect(new System.Windows.Point(), bounds.Size));
                }

                renderTargetBitmap.Render(drawingVisual);
                return renderTargetBitmap;
            }
        }
    }
}
