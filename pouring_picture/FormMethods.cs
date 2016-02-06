using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using ZedGraph;

namespace pouring_picture
{
    public static class FormMethods
    {
        public static void SaveImage(PictureBox pictureBox)
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
                            pictureBox.Image.Save(fs,
                               ImageFormat.Jpeg);
                            break;

                        case 2:
                            pictureBox.Image.Save(fs,
                               ImageFormat.Bmp);
                            break;

                        case 3:
                            pictureBox.Image.Save(fs,
                               ImageFormat.Gif);
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

        public static void PrepareGraph(ZedGraphControl zedGraph, ZedGraphControl zedGraph1, ZedGraphControl zedGraph2)
        {
            zedGraph.IsEnableZoom = false;
            zedGraph.IsEnableVPan = false;
            zedGraph.IsEnableHPan = false;

            zedGraph1.IsEnableZoom = false;
            zedGraph1.IsEnableVPan = false;
            zedGraph1.IsEnableHPan = false;

            zedGraph2.IsEnableZoom = false;
            zedGraph2.IsEnableVPan = false;
            zedGraph2.IsEnableHPan = false;
        }

        public static bool UploadImage(PictureBox pictureBox, int maxHeight, int maxWidth, out Bitmap savedBitmap)
        {
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                var image = new Bitmap(open.FileName);

                if (VerifyImage(image, maxHeight, maxWidth))
                {
                    pictureBox.Image = image;
                    savedBitmap = image;
                    return true;
                }
            }
            savedBitmap = null;
            return false;
        }
        
        private static bool VerifyImage(Bitmap image, int maxHeight, int maxWidth)
        {
            var result = image.Size.Height < maxHeight && image.Size.Width < maxWidth;
            if (!result)
                MessageBox.Show("Size of your image have to be smaller then " + maxHeight + "/" + maxWidth, "Huge size error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            return result;
        }
    }
}