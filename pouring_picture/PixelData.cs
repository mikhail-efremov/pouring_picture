using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pouring_picture
{
    public struct PixelData
    {
        public PixelData(byte blue, byte green, byte red)
        {
            this.blue = blue;
            this.green = green;
            this.red = red;
        }

        public byte blue;
        public byte green;
        public byte red;
    }
}
