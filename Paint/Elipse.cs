using System;
using System.Drawing;

namespace Paint
{
    public class Elipse : Shape
    {
        protected Point start = new Point();
        protected Point end = new Point();

        public Elipse(Color color, Point start, Point end)
            : base(color)
        {
            this.start = start;
            this.end = end;
        }

        public override void DrawToBitmap(Bitmap bitmap)
        {
            if (start == end)
            {
                return;
            }

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Pen pen = new Pen(color))
                {
                    int x = start.X < end.X ? start.X : end.X;
                    int y = start.Y < end.Y ? start.Y : end.Y;

                    graphics.DrawEllipse(pen, x, y, Math.Abs(end.X - start.X), Math.Abs(end.Y - start.Y));
                }
            }
        }
    }
}
