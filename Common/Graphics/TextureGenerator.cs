using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenToolkit.Mathematics;

namespace VoxelValley.Common.Graphics
{
    public static class TextureGenerator
    {

        public static Bitmap GenerateTexture(Vector2i size, string filename = "")
        {
            Bitmap bitmap = new Bitmap(size.X, size.Y);

            Random r = new Random();

            for (int x = 0; x < size.X; x++)
                for (int y = 0; y < size.Y; y++)
                {
                    bitmap.SetPixel(x, y, Color.FromArgb(255, 0, 255, 255));
                }

            if (!string.IsNullOrEmpty(filename))
                bitmap.Save($"Output/{filename}.png", ImageFormat.Png);

            return bitmap;
        }
    }
}