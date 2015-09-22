using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Collections.Generic;
using ZedGraph;

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

        private void Form1_Load(object sender, EventArgs e)
        {
            FillColorPickRegion();
        }

        private void imegeUploadButton_Click(object sender, EventArgs e)
        {
            UploadImage();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ImageClick(sender, e);
        }

        private void buttonGetColor_Click(object sender, EventArgs e)
        {
            GetColor();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveImage();
        }

        private void FillColorPickRegion()
        {
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
        }

        private bool UploadImage()
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            OpenFileDialog open = new OpenFileDialog();

            DrawGraph();
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

        private void ImageClick(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;

            Rectangle myRectangle = new Rectangle();

            myRectangle.Location = new Point(coordinates.X, coordinates.Y);

            myRectangle.Size = new Size(20, 10);

            var points = new List<Point>();

            var bmp = new Bitmap(pictureBox1.Image);

            for (int i = 0; i < bmp.Size.Height; i++)
                for (int j = 0; j < bmp.Size.Width; j++)
                {
                    if (myRectangle.Contains(new Point(j, i)))
                    {
                        points.Add(new Point(j, i));
                    }
                }

            PouringImage(points);
        }

        private void GetColor()
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

        private void PouringImage(List<Point> points)
        {
            try
            {
                int red = Convert.ToInt32(labelRed.Text);
                int green = Convert.ToInt32(labelGreen.Text);
                int blue = Convert.ToInt32(labelBlue.Text);
                
                var setColor = Color.FromArgb(red, green, blue);
                var bmp = new Bitmap(pictureBox1.Image);

                UnsafeBitmap uBitMap = new UnsafeBitmap(bmp);

                unsafe
                {
                    uBitMap.LockBitmap();
                    for (int i = 0; i < bmp.Size.Height; i++)
                    {
                        for (int j = 0; j < bmp.Size.Width; j++)
                        {
                            foreach (var point in points)
                            {
                                var color = uBitMap.PixelAt(point.X, point.Y);

                                var _color = uBitMap.PixelAt(j, i);
                                if (color->red == _color->red
                                    && color->green == _color->green
                                    && color->blue == _color->blue)
                                {
                                    uBitMap.SetPixel(j, i, new PixelData(setColor.B, setColor.G, setColor.R));
                                }
                            }
                        }
                    }
                    pictureBox1.Image = uBitMap.Bitmap;
                    uBitMap.UnlockBitmap();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error in PouringImage()",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool VerifyImage(Bitmap image)
        {
            var result = image.Size.Height < maxHeight && image.Size.Width < maxWidth;
            if (!result)
            MessageBox.Show("Size of your image have to be smaller then " + maxHeight + "/" + maxWidth, "Huge size error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return result;
        }

        private void DrawGraph()
        {
            // Получим панель для рисования
            GraphPane pane = zedGraph.GraphPane;

            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            pane.CurveList.Clear();

            int itemscount = 5;

            Random rnd = new Random();

            // Высоты столбиков
            double[] YValues1 = new double[itemscount];
            double[] YValues2 = new double[itemscount];
            double[] YValues3 = new double[itemscount];

            double[] XValues = new double[itemscount];

            // Заполним данные
            for (int i = 0; i < itemscount; i++)
            {
                XValues[i] = i + 1;

                YValues1[i] = rnd.NextDouble();
                YValues2[i] = rnd.NextDouble();
                YValues3[i] = rnd.NextDouble();
            }

            // Создадим три гистограммы
            // Так как для всех гистограмм мы передаем одинаковые массивы координат по X,
            // то столбики будут группироваться в кластеры в этих точках.
            BarItem bar1 = pane.AddBar("Values1", XValues, YValues1, Color.Blue);
            BarItem bar2 = pane.AddBar("Values2", XValues, YValues2, Color.Red);
            BarItem bar3 = pane.AddBar("Values3", XValues, YValues3, Color.Yellow);

            // !!! Расстояния между столбиками в кластере (группами столбиков)
            pane.BarSettings.MinBarGap = 0.0f;

            // !!! Увеличим расстояние между кластерами в 2.5 раза
            pane.BarSettings.MinClusterGap = 2.5f;


            // Вызываем метод AxisChange (), чтобы обновить данные об осях. 
            zedGraph.AxisChange();

            // Обновляем график
            zedGraph.Invalidate();
        }
    }
}