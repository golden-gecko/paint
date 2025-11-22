using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Paint
{
    public partial class Canvas : UserControl
    {
        /// <summary>
        /// List of buffers.
        /// </summary>
        protected List<Bitmap> buffers = new List<Bitmap>();

        /// <summary>
        /// Maximum buffer count.
        /// </summary>
        protected int maxBufferCount = 4;

        /// <summary>
        /// Currently selected shape.
        /// </summary>
        protected string currentShape = "";

        /// <summary>
        /// /// Currently selected color.
        /// </summary>
        protected Color currentColor = Color.Black;

        /// <summary>
        /// Draw start point.
        /// </summary>
        protected Point start = new Point();

        /// <summary>
        /// True if mouse button is down.
        /// </summary>
        protected bool buttonPressed = false;

        /// <summary>
        /// True if image is saved or not modified.
        /// </summary>
        protected bool saved = true;
        
        /// <summary>
        /// Returns current buffer.
        /// </summary>
        public Bitmap CurrentBuffer
        {
            get { return buffers[buffers.Count - 1]; }
            set { buffers[buffers.Count - 1] = value; }
        }

        /// <summary>
        /// Returns or sets shape.
        /// </summary>
        public string CurrentShape
        {
            get { return currentShape; }
            set { currentShape = value; }
        }

        /// <summary>
        /// Returns or sets color.
        /// </summary>
        public Color CurrentColor
        {
            get { return currentColor; }
            set { currentColor = value; }
        }

        /// <summary>
        /// Returns true if image is saved or was not modified.
        /// </summary>
        public bool Saved
        {
            get { return saved; }
        }

        public Canvas()
        {
            InitializeComponent();

            Clear();
        }

        /// <summary>
        /// Creates new image. Clears all buffers.
        /// </summary>
        /// <param name="width">Width of new image.</param>
        /// <param name="height">Height of new image.</param>
        public void Clear(int width = 300, int height = 300)
        {
            buffers.Clear();
            buffers.Add(new Bitmap(width, height));

            using (Graphics graphics = Graphics.FromImage(buffers[0]))
            {
                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    graphics.FillRectangle(brush, 0, 0, width - 1, height - 1);
                }
            }

            // Refresh canvas.
            Invalidate();

            saved = true;
        }

        public void SaveToFile(string filename, ImageFormat imageFormat)
        {
            CurrentBuffer.Save(filename, imageFormat);

            saved = true;
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(CurrentBuffer, Point.Empty);
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            start = e.Location;
            buttonPressed = true;
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            buttonPressed = false;

            // Draw shape.
            Shape shape = null;

            switch (currentShape)
            {
                case "Elipse":
                    shape = new Elipse(currentColor, start, e.Location);
                    break;

                case "Fill":
                    shape = new Fill(currentColor, start);
                    break;

                case "Line":
                    shape = new Line(CurrentColor, start, e.Location);
                    break;

                case "Rectangle":
                    shape = new Rectangle(currentColor, start, e.Location);
                    break;
            }

            if (shape != null)
            {
                SwapBuffers();

                // Draw shape on canvas.
                shape.DrawToBitmap(CurrentBuffer);

                // Refresh canvas.
                Invalidate();

                saved = false;
            }
        }

        public bool Undo()
        {
            if (buffers.Count < 2)
            {
                return false;
            }

            // Remove last buffer.
            buffers.RemoveAt(buffers.Count - 1);

            // Refresh canvas.
            Invalidate();

            saved = false;

            return true;
        }

        public void SwapBuffers()
        {
            // If buffer count exceeds maximum buffer count, remove first buffer.
            if (buffers.Count == maxBufferCount)
            {
                buffers.RemoveAt(0);
            }

            // Create new buffer.
            buffers.Add(CurrentBuffer.Clone() as Bitmap);
        }

        /// <summary>
        /// Rotates or flips image.
        /// </summary>
        /// <param name="type">Operation type.</param>
        public void RotateFlip(RotateFlipType type)
        {
            CurrentBuffer.RotateFlip(type);

            // Refresh canvas.
            Invalidate();

            saved = false;
        }

        public void LoadFromFile(string filename)
        {
            SwapBuffers();

            CurrentBuffer = new Bitmap(filename);

            saved = true;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (buttonPressed == false)
            {
                return;
            }

            Shape shape = null;

            switch (currentShape)
            {
                case "Rubber":
                    shape = new Rubber(Color.White, e.Location);
                    break;
            }

            if (shape != null)
            {
                SwapBuffers();

                // Draw shape on canvas.
                shape.DrawToBitmap(CurrentBuffer);

                // Refresh canvas.
                Invalidate();

                saved = false;
            }
        }

        private void Canvas_MouseLeave(object sender, EventArgs e)
        {
            buttonPressed = false;
        }
    }
}
