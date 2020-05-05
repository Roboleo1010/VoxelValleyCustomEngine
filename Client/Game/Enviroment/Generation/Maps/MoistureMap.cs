using System.Drawing;

namespace VoxelValley.Client.Game.Enviroment.Generation.Maps
{
    public static class MoistureMap
    {
        static FastNoise.FastNoise noise;

        public enum MoistureType
        {
            Wettest,
            Wetter,
            Wet,
            Dry,
            Dryer,
            Dryest
        };

        static MoistureMap()
        {
            noise = new FastNoise.FastNoise(); //TODO: SEED
        }

        internal static MoistureType GetMoistureType(float x, float z, float height)
        {
            float value = GetValue(x, z, height);

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

        static float GetValue(float x, float z, float height)
        {
            float detailValue = GenerationUtilities.FBMSimplex(x, z, 3, 0.3f, 1.3f);
            //TODO: Use Terrain Height
            if (height < 0.2)
                detailValue += 7 * height;
            if (height < 0.4)
                detailValue += 2 * height;
            if (height < 0.5)
                detailValue += 1 * height;

            return detailValue;
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