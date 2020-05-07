using System.Drawing;

namespace VoxelValley.Client.Game.Enviroment.Generation.Maps
{
    public static class HeightMap
    {
        public enum HeightType
        {
            Lowest = 0,
            Lower = 1,
            Low = 2,
            High = 3,
            Higher = 4,
            Highest = 5
        };

        internal static HeightType GetHeightType(float x, float z)
        {
            float value = GetHeight(x, z);

            if (value < 0.1f)
                return HeightType.Lowest;
            else if (value < 0.25f)
                return HeightType.Lower;
            else if (value < 0.50f)
                return HeightType.Low;
            else if (value < 0.75f)
                return HeightType.High;
            else if (value < 0.9f)
                return HeightType.Higher;
            else
                return HeightType.Highest;
        }

        static float GetHeight(float x, float z)
        {
            return GenerationUtilities.FBMPerlin(x, z, 3, 1f, 1.4f);
        }

        internal static Color GetColor(HeightType type)
        {
            switch (type)
            {
                case HeightType.Lowest:
                    return Color.FromArgb(255, 0, 0, 128);
                case HeightType.Lower:
                    return Color.FromArgb(255, 25, 25, 150);
                case HeightType.Low:
                    return Color.FromArgb(255, 50, 220, 20);
                case HeightType.High:
                    return Color.FromArgb(255, 16, 160, 0);
                case HeightType.Higher:
                    return Color.FromArgb(255, 128, 128, 128);
                case HeightType.Highest:
                    return Color.FromArgb(255, 255, 255, 255);
                default:
                    return Color.Purple;
            }
        }
    }
}