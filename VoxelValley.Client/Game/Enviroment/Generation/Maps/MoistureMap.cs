using System.Drawing;

namespace VoxelValley.Client.Game.Enviroment.Generation.Maps
{
    public static class MoistureMap
    {
        public enum MoistureType
        {
            Dryest = 0,
            Dryer = 1,
            Dry = 2,
            Wet = 3,
            Wetter = 4,
            Wettest = 5
        };

        internal static MoistureType GetMoistureType(float x, float z, float height)
        {
            float value = GetMoisture(x, z, height);

            if (value < 0.27f)
                return MoistureType.Dryest;
            else if (value < 0.4f)
                return MoistureType.Dryer;
            else if (value < 0.6f)
                return MoistureType.Dry;
            else if (value < 0.8f)
                return MoistureType.Wet;
            else if (value < 0.9f)
                return MoistureType.Wetter;
            else
                return MoistureType.Wettest;
        }

        static float GetMoisture(float x, float z, float height)
        {
            float value = GenerationUtilities.FBMSimplex(x, z, 3, 0.05f, 0.8f);

            // if (height < 0.2)
            //     value += 7 * height;
            // if (height < 0.4)
            //     value += 2 * height;
            // if (height < 0.5)
            //     value += 1 * height;

            return value;
        }

        internal static Color GetColor(MoistureType type)
        {
            switch (type)
            {
                case MoistureType.Dryest:
                    return Color.FromArgb(255, 255, 139, 17);
                case MoistureType.Dryer:
                    return Color.FromArgb(255, 245, 245, 23);
                case MoistureType.Dry:
                    return Color.FromArgb(255, 80, 255, 0);
                case MoistureType.Wet:
                    return Color.FromArgb(255, 85, 255, 255);
                case MoistureType.Wetter:
                    return Color.FromArgb(255, 20, 70, 255);
                case MoistureType.Wettest:
                    return Color.FromArgb(255, 0, 0, 100);
                default:
                    return Color.Purple;
            }
        }
    }
}