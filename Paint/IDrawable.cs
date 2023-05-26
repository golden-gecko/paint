using System;
using System.Drawing;

namespace Paint
{
    public interface IDrawable
    {
        void DrawToBitmap(Bitmap bitmap);
    }
}
