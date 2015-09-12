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

        private void UploadImage()
        {
            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(open.FileName);
            }
        }

        private void GetBitMap(Point point)
        {
            using (Bitmap bmp = new Bitmap(pictureBox1.Image))
            {
                Color clr = bmp.GetPixel(point.X, point.Y);
                int red = clr.R;
                int green = clr.G;
                int blue = clr.B;

                labelRed.Text = red.ToString();
                labelGreen.Text = green.ToString();
                labelBlue.Text = blue.ToString();
            }
        }
    }
}