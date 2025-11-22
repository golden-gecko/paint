using System;
using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
    public partial class NewForm : Form
    {
        protected Canvas canvas = null;

        public NewForm(Canvas canvas)
        {
            this.canvas = canvas;

            InitializeComponent();

            // Set default values to canvas size.
            newWidth.Value = canvas.CurrentBuffer.Width;
            newHeight.Value = canvas.CurrentBuffer.Height;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            canvas.Clear(Convert.ToInt32(newWidth.Value), Convert.ToInt32(newHeight.Value));

            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
