namespace VoxelValley.Engine.Core.Helper
{
    public static class MathHelper
    {
        public static float Map(float newmin, float newmax, float origmin, float origmax, float value)
        {
            return (value - origmin) / (origmax - origmin) * (newmax - newmin) + newmin;
        }
    }
}