using System;
using System.Drawing;
using System.Windows.Forms;

namespace EMView.UI
{
    public class RotatedLabel : Label
    {
        public int RotationAngle { get; set; } = 90;

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(this.Width / 2, this.Height / 2);
            e.Graphics.RotateTransform(RotationAngle);
            e.Graphics.TranslateTransform(-this.Width / 2, -this.Height / 2);
            e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), new PointF(0, 0));
        }
    }

}
