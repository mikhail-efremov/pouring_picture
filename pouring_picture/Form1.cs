using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace pouring_picture
{
    public partial class Form1 : Form
    {
        private int maxHeight = 510;
        private int maxWidth = 455;

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

            PouringImage(coordinates);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveImage();
        }

        private void buttonGetColor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            var color = colorDialog1;
            
            Bitmap flag = new Bitmap(30, 30);
            Graphics flagGraphics = Graphics.FromImage(flag);
            int iterator = 0;
            while (iterator <= 30)
            {
                var myBrush = new SolidBrush(colorDialog1.Color);
                flagGraphics.FillRectangle(myBrush, 0, iterator, 30, 30);
                iterator++;
            }
            pictureBoxPick.Image = flag;

            labelRed.Text = color.Color.R.ToString();
            labelGreen.Text = color.Color.G.ToString();
            labelBlue.Text = color.Color.B.ToString();
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
            var result = image.Size.Height < maxHeight && image.Size.Width < maxWidth;
            if (!result)
            MessageBox.Show("Size of your image have to be smaller then " + maxHeight + "/" + maxWidth, "Huge size error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return result;
        }

        private void PouringImage(Point point)
        {
            try
            {
                int red = Convert.ToInt32(labelRed.Text);
                int green = Convert.ToInt32(labelGreen.Text);
                int blue = Convert.ToInt32(labelBlue.Text);

                var setColor = Color.FromArgb(red, green, blue);
                var bmp = new Bitmap(pictureBox1.Image);

                Color color = bmp.GetPixel(point.X, point.Y);

                for (int i = 0; i < bmp.Size.Height; i++)
                    for (int j = 0; j < bmp.Size.Width; j++)
                    {
                        var _color = bmp.GetPixel(j, i);
                        if (color.R == _color.R && color.G == _color.G && color.B == _color.B)
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

        private void SaveImage()
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
                saveFileDialog1.Title = "Save an Image File";
                saveFileDialog1.ShowDialog();

                if (saveFileDialog1.FileName != "")
                {
                    System.IO.FileStream fs =
                       (System.IO.FileStream)saveFileDialog1.OpenFile();
                    switch (saveFileDialog1.FilterIndex)
                    {
                        case 1:
                            pictureBox1.Image.Save(fs,
                               System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;

                        case 2:
                            pictureBox1.Image.Save(fs,
                               System.Drawing.Imaging.ImageFormat.Bmp);
                            break;

                        case 3:
                            pictureBox1.Image.Save(fs,
                               System.Drawing.Imaging.ImageFormat.Gif);
                            break;
                    }

                    fs.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error in SaveImage()",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}