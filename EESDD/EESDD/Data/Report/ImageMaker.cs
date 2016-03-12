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
        public static void ViewToPng(FrameworkElement element, string filePath)
        {
            Application.Current.Dispatcher.BeginInvoke((Action)delegate()
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

                BitmapEncoder imgEncoder = new PngBitmapEncoder();
                imgEncoder.Frames.Add(BitmapFrame.Create(bmpCopied));


                FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write);
                imgEncoder.Save(fs);
                fs.Close();
            });
        }
    }
}
