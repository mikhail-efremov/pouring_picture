using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pouring_picture
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void imegeUploadButton_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            UploadImage();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;

            GetBitMap(coordinates);
        }

        private bool UploadImage()
        {
            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                var image = new Bitmap(open.FileName);

                if (VerifyImage(image))
                {
                    pictureBox1.Image = image;
                    return true;
                }
            }
            return false;
        }

        private bool VerifyImage(Bitmap image)
        {
            var height = 510;
            var width = 455;

            var result = image.Size.Height < height && image.Size.Width < width;
            if (!result)
            MessageBox.Show("Size of your image have to be smaller then " + height + "/" + width, "Huge size error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return result;
        }

        private void GetBitMap(Point point)
        {
            using (Bitmap bmp = new Bitmap(pictureBox1.Image))
            {
                Color clr = bmp.GetPixel(point.X, point.Y);

                labelRed.Text = clr.R.ToString();
                labelGreen.Text = clr.G.ToString();
                labelBlue.Text = clr.B.ToString();

                PouringImage();
            }
        }

        private void PouringImage()
        {
            try
            {
                int red = Convert.ToInt32(labelRed.Text);
                int green = Convert.ToInt32(labelGreen.Text);
                int blue = Convert.ToInt32(labelBlue.Text);

                var setColor = Color.FromArgb(red, 0,0);
                var bmp = new Bitmap(pictureBox1.Image);

                for (int i = 0; i < bmp.Size.Height; i++)
                    for (int j = 0; j < bmp.Size.Width; j++)
                    {
                        Color color = bmp.GetPixel(j, i);
                        if (color.R == red && color.G == green && color.B == blue)
                        {
                            bmp.SetPixel(j, i, setColor);
                        }
                    }
                pictureBox1.Image = bmp;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error in PouringImage()",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonGetColor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            var color = colorDialog1;

            labelRed.Text = color.Color.R.ToString();
            labelGreen.Text = color.Color.G.ToString();
            labelBlue.Text = color.Color.B.ToString();

            PouringImage();
        }
    }
}