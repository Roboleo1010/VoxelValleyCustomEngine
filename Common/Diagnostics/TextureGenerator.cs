using System.Drawing;
using System.Drawing.Imaging;

namespace VoxelValley.Common.Diagnostics
{
    public static class TextureGenerator
    {
        /// <summary>
        /// Generates a Black & White texture and retuns it. If a Filename is given, the Texture is saved under Output/{filenam}.png
        /// </summary>
        /// <param name="data">2 dimensional array with data between 0-1</param>
        /// <param name="filename">If a Filename is given, the Texture is saved under Output/{filenam}.png</param>
        /// <returns></returns>
        public static Bitmap GenerateTexture(float[,] data, string filename = "")
        {
            Bitmap bitmap = new Bitmap(data.GetLength(0), data.GetLength(1));

            for (int x = 0; x < data.GetLength(0); x++)
                for (int y = 0; y < data.GetLength(1); y++)
                {
                    bitmap.SetPixel(x, y, Color.FromArgb(255, (int)(data[x, y] * 255), (int)(data[x, y] * 255), (int)(data[x, y] * 255)));
                }

            if (!string.IsNullOrEmpty(filename))
                bitmap.Save($"Output/{filename}.png", ImageFormat.Png);

            return bitmap;
        }


        /// <summary>
        /// Generates a Color texture and retuns it. If a Filename is given, the Texture is saved under Output/{filenam}.png
        /// </summary>
        /// <param name="data">2 dimensional array with colors</param>
        /// <param name="filename">If a Filename is given, the Texture is saved under Output/{filenam}.png</param>
        /// <returns></returns>
        public static Bitmap GenerateTexture(Color[,] data, string filename = "")
        {
            Bitmap bitmap = new Bitmap(data.GetLength(0), data.GetLength(1));

            for (int x = 0; x < data.GetLength(0); x++)
                for (int y = 0; y < data.GetLength(1); y++)
                {
                    bitmap.SetPixel(x, y, data[x, y]);
                }

            if (!string.IsNullOrEmpty(filename))
                bitmap.Save($"Output/{filename}.png", ImageFormat.Png);

            return bitmap;
        }
    }
}