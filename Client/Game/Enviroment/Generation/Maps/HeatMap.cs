using System;
using System.Collections.Generic;
using System.Drawing;

namespace VoxelValley.Client.Game.Enviroment.Generation.Maps
{
    public static class HeatMap
    {
        static float totalHeatMapHeight = 5000;
        static float totalHeatMapHeightHalf = totalHeatMapHeight / 2;

        public enum HeatType
        {
            COLDEST = 0,
            COLDER = 1,
            COLD = 2,
            HOT = 3,
            HOTTER = 4,
            HOTTEST = 5
        };

        internal static HeatType GetHeatType(float x, float z, float height)
        {
            float value = GetHeat(x, z, height);

            if (value > 0.9f)
                return HeatType.HOTTEST;
            else if (value > 0.75f)
                return HeatType.HOTTER;
            else if (value > 0.60f)
                return HeatType.HOT;
            else if (value > 0.35f)
                return HeatType.COLD;
            else if (value > 0.15f)
                return HeatType.COLDER;
            else // value < 0.15
                return HeatType.COLDEST;

        }

        static float GetHeat(float x, float z, float height)
        {
            float baseValue = GetBaseValue(z);
            float detailValue = GenerationUtilities.FBMSimplex(x, z, 3, 0.1f, 1.3f);

             float combinedValue = ((baseValue * 1.2f) + (detailValue * 1)) / 2;

            //TODO: Use Height Data?
            // if (height > 0.6f) 
            //     combinedValue -= 0.1f * height;
            // if (height > 0.7f)
            //     combinedValue -= 0.2f * height;
            // if (height > 0.8f)
            //     combinedValue -= 0.3f * height;
            // if (height > 0.9f)
            //     combinedValue -= 0.4f * height;

            return combinedValue;
        }

        internal static float GetBaseValue(float z)
        {
            float relativeAbsoluteZ = Math.Abs(Math.Abs(z) % totalHeatMapHeight - totalHeatMapHeightHalf);

            if (relativeAbsoluteZ < (0.1f) * totalHeatMapHeightHalf)      //coldest
                return 0f;
            else if (relativeAbsoluteZ < (0.25f) * totalHeatMapHeightHalf) //colder
                return 0.2f;
            else if (relativeAbsoluteZ < (0.35f) * totalHeatMapHeightHalf) //cold
                return 0.4f;
            else if (relativeAbsoluteZ < (0.65f) * totalHeatMapHeightHalf) //warm
                return 0.6f;
            else if (relativeAbsoluteZ < (0.85f) * totalHeatMapHeightHalf) //warmer
                return 0.8f;
            else //warmest
                return 1;
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