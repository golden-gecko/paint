using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Paint
{
    public class Line : Shape
    {
        protected Point start = new Point();
        protected Point end = new Point();

        public Line(Color color, Point start, Point end)
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
                    graphics.DrawLine(pen, start.X, start.Y, end.X, end.Y);
                }
            }
        }
    }
}
