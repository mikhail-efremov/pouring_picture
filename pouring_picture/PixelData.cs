
namespace pouring_picture
{
    public struct PixelData
    {
        public byte red;
        public byte green;
        public byte blue;

        public PixelData(byte blue, byte green, byte red)
        {
            this.blue = blue;
            this.green = green;
            this.red = red;
        }

        public override string ToString()
        {
            return string.Format("[PixelData] R:{2} G:{3} B:{4}", red, green, blue);
        }
    }
}
