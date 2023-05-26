using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Paint
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutBox = new AboutForm();
            aboutBox.ShowDialog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                canvas.LoadFromFile(openFileDialog1.FileName);

                saveFileDialog1.FileName = openFileDialog1.FileName;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewForm newForm = new NewForm(canvas);
            newForm.ShowDialog();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;

            if (item == null)
            {
                return;
            }

            canvas.RotateFlip(RotateFlipType.Rotate90FlipNone);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                canvas.SaveToFile(saveFileDialog1.FileName, ImageFormat.Bmp);
            }
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            CheckItem(toolStrip2.Items);

            ToolStripButton clicked = e.ClickedItem as ToolStripButton;

            if (clicked != null)
            {
                canvas.CurrentColor = clicked.BackColor;
            }
        }
        
        protected void CheckItem(ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                ToolStripButton button = item as ToolStripButton;

                if (button != null)
                {
                    button.Checked = false;
                }
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            canvas.Undo();
        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            canvas.RotateFlip(RotateFlipType.RotateNoneFlipY);
        }

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            canvas.RotateFlip(RotateFlipType.RotateNoneFlipX);
        }

        private void toolStripMenuItem5_Click_1(object sender, EventArgs e)
        {
            canvas.RotateFlip(RotateFlipType.Rotate90FlipNone);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            canvas.RotateFlip(RotateFlipType.Rotate180FlipNone);
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            canvas.RotateFlip(RotateFlipType.Rotate270FlipNone);
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            CheckItem(toolStrip1.Items);

            string shapeName = e.ClickedItem.Tag as string;

            if (shapeName != null)
            {
                canvas.CurrentShape = shapeName;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.FileName.Length == 0)
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
            else
            {
                canvas.SaveToFile(saveFileDialog1.FileName, ImageFormat.Bmp);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (canvas.Saved == false)
            {
                DialogResult result = MessageBox.Show(
                    "Do you want to save changes to the image?",
                    "Paint",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning
                );

                switch (result)
                {
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;

                    case DialogResult.Yes:
                        saveToolStripMenuItem_Click(sender, e);
                        break;
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            canvas.CurrentColor = Color.Black;
        }
    }
}
