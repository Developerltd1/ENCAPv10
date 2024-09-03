using BusinessLogic;
using BusinessLogic.Model;
using LiveCharts.WinForms;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace ENCAPv3
{
    public class CoPdfSetting
    {
        public void ExportChartAndDataToPdf(CartesianChart chart, List<List<StorePoint>> allList, string filePath)
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Define fonts
            XFont headingFont = new XFont("Arial", 16, XFontStyle.Bold);
            XFont tableFont = new XFont("Arial", 10, XFontStyle.Regular);

            // Define heading text
            string headingText = "Chart Exported in PDF";

            // Measure the heading text size
            XSize headingSize = gfx.MeasureString(headingText, headingFont);

            // Draw the heading text
            gfx.DrawString(headingText, headingFont, XBrushes.Black,
                (page.Width - headingSize.Width) / 2, 20); // Centered horizontally, 20 points from the top

            // Render the chart to a bitmap
            Bitmap bitmap = CaptureControlAsBitmap(chart);

            // Calculate the aspect ratio and size for the image
            double chartAspectRatio = (double)bitmap.Width / bitmap.Height;
            double pageAspectRatio = (double)page.Width / page.Height;
            double newWidth, newHeight;

            if (chartAspectRatio > pageAspectRatio)
            {
                newWidth = page.Width - 40;
                newHeight = newWidth / chartAspectRatio;
            }
            else
            {
                newHeight = page.Height - 80 - headingSize.Height;
                newWidth = newHeight * chartAspectRatio;
            }

            double xPosition = (page.Width - newWidth) / 2;
            double yPosition = 20 + headingSize.Height + 10;

            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                stream.Position = 0;
                XImage xImage = XImage.FromStream(stream);
                gfx.DrawImage(xImage, xPosition, yPosition, newWidth, newHeight);
            }

            // Define table position and dimensions
            double tableTop = yPosition + newHeight + 20; // Start below the chart image
            double rowHeight = 20;
            double columnWidth = 100;
            double xOffset = 20;

            // Draw table header
            gfx.DrawRectangle(XBrushes.LightGray, xOffset, tableTop, page.Width - 40, rowHeight);
            gfx.DrawString("Parameter", tableFont, XBrushes.Black, xOffset, tableTop + 2);
            gfx.DrawString("Value", tableFont, XBrushes.Black, xOffset + columnWidth, tableTop + 2);

            tableTop += rowHeight;

            // Draw table rows
            foreach (var list in allList)
            {
                foreach (var point in list)
                {
                    gfx.DrawString(point.Parameter, tableFont, XBrushes.Black, xOffset, tableTop);
                    gfx.DrawString(point.Battery1.ToString(), tableFont, XBrushes.Black, xOffset + columnWidth, tableTop);
                    tableTop += rowHeight;
                }
            }

            // Save the PDF document
            document.Save(filePath);

            // Show success message
            JIMessageBox.InformationMessage("Exported Successfully");
        }

        private Bitmap CaptureControlAsBitmap(Control control)
        {
            Bitmap bitmap = new Bitmap(control.Width, control.Height);
            control.DrawToBitmap(bitmap, new Rectangle(0, 0, control.Width, control.Height));
            return bitmap;
        }

    }
}
