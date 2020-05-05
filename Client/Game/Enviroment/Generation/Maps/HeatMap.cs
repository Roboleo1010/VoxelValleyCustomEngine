using System;
using System.Collections.Generic;
using System.Drawing;

namespace VoxelValley.Client.Game.Enviroment.Generation.Maps
{
    public static class HeatMap
    {
        static float totalHeatMapHeight = 600;
        static float totalHeatMapHeightHalf = totalHeatMapHeight / 2;
        static FastNoise.FastNoise noise;
        static float baseScaleFactor = 0.08f;

        public enum HeatType
        {
            COLDEST,
            COLDER,
            COLD,
            HOT,
            HOTTER,
            HOTTEST
        };

        static HeatMap()
        {
            noise = new FastNoise.FastNoise(); //TODO: SEED
        }

        internal static HeatType GetHeatType(float x, float z, float height)
        {
            float value = GetValue(x, z, height);

            if (value < 0.05f)
                return HeatType.COLDEST;
            else if (value < 0.18f)
                return HeatType.COLDER;
            else if (value < 0.4f)
                return HeatType.COLD;
            else if (value < 0.6f)
                return HeatType.HOT;
            else if (value < 0.8f)
                return HeatType.HOTTER;
            else
                return HeatType.HOTTEST;
        }

        static float GetValue(float x, float z, float height)
        {
            float baseValue = GetBaseValue(z);
            float detailValue = GenerationUtilities.FBMSimplex(x, z, 3, 0.4f, 1.3f);
            //TODO: Use Terrain Height
            return baseValue * detailValue;
        }

        internal static float GetBaseValue(float z)
        {
            float relativeAbsoluteZ = Math.Abs(Math.Abs(z) % totalHeatMapHeight - totalHeatMapHeightHalf);

            if (relativeAbsoluteZ > (5 * baseScaleFactor) * totalHeatMapHeight)      //warmest
                return 1f;
            else if (relativeAbsoluteZ > (4 * baseScaleFactor) * totalHeatMapHeight) //warmer
                return 0.8f;
            else if (relativeAbsoluteZ > (3 * baseScaleFactor) * totalHeatMapHeight) //warm
                return 0.6f;
            else if (relativeAbsoluteZ > (2 * baseScaleFactor) * totalHeatMapHeight) //cold
                return 0.4f;
            else if (relativeAbsoluteZ > (1 * baseScaleFactor) * totalHeatMapHeight) //colder
                return 0.2f;
            else //coldest
                return 0;
        }

        internal static Color GetColor(HeatType type)
        {
            switch (type)
            {
                case HeatType.COLDEST:
                    return Color.FromArgb(255, 0, 255, 255);
                case HeatType.COLDER:
                    return Color.FromArgb(255, 170, 255, 255);
                case HeatType.COLD:
                    return Color.FromArgb(255, 0, 229, 133);
                case HeatType.HOT:
                    return Color.FromArgb(255, 255, 255, 100);
                case HeatType.HOTTER:
                    return Color.FromArgb(255, 255, 100, 0);
                case HeatType.HOTTEST:
                    return Color.FromArgb(255, 241, 12, 0);
                default:
                    return Color.Purple;
            }
        }
    }
}