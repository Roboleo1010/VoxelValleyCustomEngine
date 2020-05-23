namespace VoxelValley.Engine.Mathematics
{
    public static class MathHelper
    {
        public static float Map(float newmin, float newmax, float origmin, float origmax, float value)
        {
            return (value - origmin) / (origmax - origmin) * (newmax - newmin) + newmin;
        }

        public static int Map(int newmin, int newmax, int origmin, int origmax, int value)
        {
            return (value - origmin) / (origmax - origmin) * (newmax - newmin) + newmin;
        }

        /// <summary>
        /// Interpolates linerally between values first and second
        /// </summary>
        /// <param name="by">0 returns first value, 1 returns second value, 0.5 retuns interpolated value in middle</param>
        /// <returns>Interpolated value</returns>
        public static float InterpolateLinear(float first, float second, float by)
        {
            return first + (second - first) * by;
        }
    }
}