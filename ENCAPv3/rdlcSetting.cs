using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
using System.Data;
using Microsoft.Reporting.WinForms;

namespace EMView
{
    public class rdlcSetting
    {
        public byte[] ExportChart(CartesianChart chart)
        {
            // Render the chart to a bitmap
            Bitmap bitmap = CaptureControlAsBitmap(chart);

            // Convert Bitmap to Byte Array
            byte[] chartImageBytes;
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                chartImageBytes = stream.ToArray();
            }
            return chartImageBytes;

           
         
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
