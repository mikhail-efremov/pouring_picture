using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pouring_picture
{
    struct BackStepItem
    {
        private Bitmap bitmap;
        private List<PixelData> pixelDatas;

        public Bitmap Bitmap
        {
            get { return bitmap; }
            set { bitmap = value; }
        }

        public List<PixelData> PixelData
        {
            get { return pixelDatas; }
            set { pixelDatas = value; }
        }

        public BackStepItem(Bitmap bitmap, List<PixelData> pixelData)
        {
            this.bitmap = bitmap;
            this.pixelDatas = pixelData;
        }
    }
}
