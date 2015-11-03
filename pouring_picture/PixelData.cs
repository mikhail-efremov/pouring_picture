
namespace pouring_picture
{
    public struct PixelData
    {
        public PixelData(byte blue, byte green, byte red, int X, int Y)
        {
            this.blue = blue;
            this.green = green;
            this.red = red;
            this.X = X;
            this.Y = Y;
        }

        public byte blue;
        public byte green;
        public byte red;

        public int X;
        public int Y;
    }
}
