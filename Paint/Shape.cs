using System;
using System.Drawing;

namespace Paint
{
    public abstract class Shape : IDrawable
    {
        protected Color color = new Color();

        public Shape(Color color)
        {
            this.color = color;
        }

        public abstract void DrawToBitmap(Bitmap bitmap);
    }
}
