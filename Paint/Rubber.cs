using System;
using System.Drawing;

namespace Paint
{
    public class Rubber : Shape
    {
        protected Point start = new Point();

        public Rubber(Color color, Point start)
            : base(color)
        {
            this.start = start;
        }

        public override void DrawToBitmap(Bitmap bitmap)
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (SolidBrush brush = new SolidBrush(color))
                {
                    graphics.FillEllipse(brush, start.X - 10, start.Y - 10, 20, 20);
                }
            }
        }
    }
}
