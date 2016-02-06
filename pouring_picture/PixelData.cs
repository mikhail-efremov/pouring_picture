
namespace pouring_picture
{
    public struct PixelData
    {
        public byte red;
        public byte green;
        public byte blue;

        public int X;
        public int Y;

        public PixelData(byte blue, byte green, byte red, int X, int Y)
        {
            this.blue = blue;
            this.green = green;
            this.red = red;
            this.X = X;
            this.Y = Y;
        }

        public override string ToString()
        {
            return string.Format("[{0}:{1}] R:{2} G:{3} B:{4}", X, Y, red, green, blue);
        }
    }
}
