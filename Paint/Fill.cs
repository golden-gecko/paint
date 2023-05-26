using System;
using System.Collections.Generic;
using System.Drawing;

namespace Paint
{
    public class Fill : Shape
    {
        protected Point start = new Point();

        public Fill(Color color, Point start)
            : base(color)
        {
            this.start = start;
        }

        /// <summary>
        /// Fills region with selected color. Inspired by http://rosettacode.org/wiki/Bitmap/Flood_fill#C.23.
        /// </summary>
        /// <param name="bitmap">Bitmap to process.</param>
        public override void DrawToBitmap(Bitmap bitmap)
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Pen pen = new Pen(color))
                {
                    Queue<Point> points = new Queue<Point>();

                    // Get color of first point.
                    Color baseColor = bitmap.GetPixel(start.X, start.Y);

                    // Add first point to the queue.
                    points.Enqueue(start);
                    
                    // Process bitmap.
                    while (points.Count > 0)
                    {
                        Point n = points.Dequeue();

                        if (bitmap.GetPixel(n.X, n.Y) != baseColor)
                        {
                            continue;
                        }

                        Point w = n, e = new Point(n.X + 1, n.Y);

                        while ((w.X > 0) && bitmap.GetPixel(w.X, w.Y) == baseColor)
                        {
                            bitmap.SetPixel(w.X, w.Y, color);

                            if ((w.Y > 0) && bitmap.GetPixel(w.X, w.Y - 1) == baseColor)
                            {
                                points.Enqueue(new Point(w.X, w.Y - 1));
                            }

                            if ((w.Y < bitmap.Height - 1) && bitmap.GetPixel(w.X, w.Y + 1) == baseColor)
                            {
                                points.Enqueue(new Point(w.X, w.Y + 1));
                            }

                            --w.X;
                        }

                        while ((e.X < bitmap.Width - 1) && bitmap.GetPixel(e.X, e.Y) == baseColor)
                        {
                            bitmap.SetPixel(e.X, e.Y, color);

                            if ((e.Y > 0) && bitmap.GetPixel(e.X, e.Y - 1) == baseColor)
                            {
                                points.Enqueue(new Point(e.X, e.Y - 1));
                            }

                            if ((e.Y < bitmap.Height - 1) && bitmap.GetPixel(e.X, e.Y + 1) == baseColor)
                            {
                                points.Enqueue(new Point(e.X, e.Y + 1));
                            }

                            ++e.X;
                        }
                    }
                }
            }
        }
    }
}
