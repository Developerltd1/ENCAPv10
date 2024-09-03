 
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts.WinForms;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace ENCAPv3
{
    public class PdfSetting
    {
        //public void ExportChartToPdf(CartesianChart chart, string filePath)
        //{
        //    // Create a new PDF document
        //    PdfDocument document = new PdfDocument();
        //    PdfPage page = document.AddPage();
        //    XGraphics gfx = XGraphics.FromPdfPage(page);

        //    // Define font and heading text
        //    XFont headingFont = new XFont("Arial", 16, XFontStyle.Bold);
        //    string headingText = "Chart Exported in PDF";

        //    // Measure the heading text size
        //    XSize headingSize = gfx.MeasureString(headingText, headingFont);

        //    // Draw the heading text
        //    gfx.DrawString(headingText, headingFont, XBrushes.Black,
        //        (page.Width - headingSize.Width) / 2, 20); // Centered horizontally, 20 points from the top

        //    // Render the chart to a bitmap
        //    Bitmap bitmap = CaptureControlAsBitmap(chart);

        //    // Calculate the aspect ratio of the chart and the page
        //    double chartAspectRatio = (double)bitmap.Width / bitmap.Height;
        //    double pageAspectRatio = (double)page.Width / page.Height;

        //    // Determine the size of the image to fit the page while maintaining the aspect ratio
        //    double newWidth, newHeight;

        //    if (chartAspectRatio > pageAspectRatio)
        //    {
        //        // Chart is wider relative to its height than the page
        //        newWidth = page.Width - 40; // Leave some margin
        //        newHeight = newWidth / chartAspectRatio;
        //    }
        //    else
        //    {
        //        // Chart is taller relative to its width than the page
        //        newHeight = page.Height - 60 - headingSize.Height; // Leave some margin plus heading space
        //        newWidth = newHeight * chartAspectRatio;
        //    }

        //    // Calculate position to center the image horizontally and position below the heading
        //    double xPosition = (page.Width - newWidth) / 2;
        //    double yPosition = 20 + headingSize.Height + 10; // 10 points margin below the heading

        //    // Convert Bitmap to MemoryStream
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        bitmap.Save(stream, ImageFormat.Png);
        //        stream.Position = 0;

        //        // Load the image from the MemoryStream
        //        XImage xImage = XImage.FromStream(stream);

        //        // Draw the image onto the PDF page with calculated dimensions and position
        //        gfx.DrawImage(xImage, xPosition, yPosition, newWidth, newHeight);
        //    }

        //    // Save the PDF document
        //    document.Save(filePath);
        //}


        public void ExportChart(CartesianChart chart, string filePath)
        {
           
            // Render the chart to a bitmap
            Bitmap bitmap = CaptureControlAsBitmap(chart);

            

            // Determine the size of the image to fit the page while maintaining the aspect ratio
            double newWidth, newHeight;

           
            // Convert Bitmap to MemoryStream
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                stream.Position = 0;

                // Load the image from the MemoryStream
                XImage xImage = XImage.FromStream(stream);
            }

           
        }
        private Bitmap CaptureControlAsBitmap(Control control)
        {
            // Create a bitmap with the same dimensions as the control
            Bitmap bitmap = new Bitmap(control.Width, control.Height);
            control.DrawToBitmap(bitmap, new Rectangle(0, 0, control.Width, control.Height));
            return bitmap;
        }

    }
}
