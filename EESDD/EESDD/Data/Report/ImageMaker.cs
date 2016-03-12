using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EESDD.Data.Report
{
    class ImageMaker
    {
        public static RenderTargetBitmap UIElementToBitmap(FrameworkElement element)
        {
            double width = element.ActualWidth;
            double height = element.ActualHeight;
            RenderTargetBitmap bmpCopied = new RenderTargetBitmap((int)Math.Round(width), (int)Math.Round(height), 96, 96, PixelFormats.Default);
            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext dc = dv.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(element);
                dc.DrawRectangle(vb, null, new Rect(new Point(), new Size(width, height)));
            }
            bmpCopied.Render(dv);

            return bmpCopied;

        }

        public static void BitmapToPngFile(RenderTargetBitmap bitmap, string filePath)
        {
            BitmapEncoder imgEncoder = new PngBitmapEncoder();
            imgEncoder.Frames.Add(BitmapFrame.Create(bitmap));

            using (var file = File.OpenWrite(filePath))
            {
                imgEncoder.Save(file);
            }
        }

        public static void ViewToPng(FrameworkElement element, string filePath)
        {
            BitmapToPngFile(UIElementToBitmap(element), filePath);
        }
    }
}
