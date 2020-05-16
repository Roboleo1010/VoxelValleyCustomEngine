using System;
using VoxelValley.Common;

namespace VoxelValley.Client.Game.Enviroment.Generation
{
    public static class GenerationUtilities
    {
        public static FastNoise.FastNoise Noise;
        public static Random Random = new Random();

        static GenerationUtilities()
        {
            Noise = new FastNoise.FastNoise(); //TODO: Seed here
        }

        /// <summary>
        /// Layers multiple Perlin Noises
        /// </summary>
        /// <param name="x">x position in worldspace</param>
        /// <param name="z">Z position in world space</param>
        /// <param name="octaves">Octaves are how many layers you are putting together. If you start with big features, the number of octaves determines how detailed the map will look.</param>
        /// <param name="frequency">The frequency of a layer is how many points fit into the space you've created. So for the mountain scale, you only need a few points, but at the rock scale you may need hundreds of points. In the code above, I start with a small frequency (which equates to large features) and move to higher frequencies which produce smaller details.</param>
        /// <param name="amplitude">The amplitude is how tall the features should be. Frequency determines the width of features, amplitude determines the height. Each octave the amplitude shrinks, meaning small features are also short. This doesn't have to be the case, but for this case it makes pleasing maps.</param>
        /// <param name="lacunarity">Lacunarity is what makes the frequency grow. Each octave the frequency is multiplied by the lacunarity. I use a lacunarity of 2.0, however values of 1.8715 or 2.1042 can help to reduce artifacts in some algorithms. A lacunarity of 2.0 means that the frequency doubles each octave, so if the first octave had 3 points the second would have 6, then 12, then 24, etc. This is used almost exclusively, partly because octaves in music double in frequency. Other values are perfectly acceptable, but the results will vary.</param>
        /// <param name="offsetValue">Gets added to the value before normalization</param>
        /// <param name="z">Z position in world space</param>
        /// <returns></returns>
        public static float FBMPerlin(float x, float z, int octaves, float frequency, float amplitude, float lacunarity = 2, float persistence = 0.5f)
        {
            float total = 0;

            for (int o = 0; o < octaves; o++)
            {
                total += Noise.GetPerlin(x * frequency, z * frequency) * amplitude;
                frequency *= lacunarity;
                amplitude *= persistence;
            }

            return NormalizeNoise(total);
        }

        public static float Perlin(float x, float z, float frequency, float amplitude, int offsetX = 0, int offsetZ = 0)
        {
            return NormalizeNoise(Noise.GetPerlin((x + offsetX) * frequency, (z + offsetZ) * frequency) * amplitude);
        }

        /// <summary>
        /// Layers multiple Simplex Noises
        /// </summary>
        /// <param name="x">x position in worldspace</param>
        /// <param name="z">Z position in world space</param>
        /// <param name="octaves">Octaves are how many layers you are putting together. If you start with big features, the number of octaves determines how detailed the map will look.</param>
        /// <param name="frequency">The frequency of a layer is how many points fit into the space you've created. So for the mountain scale, you only need a few points, but at the rock scale you may need hundreds of points. In the code above, I start with a small frequency (which equates to large features) and move to higher frequencies which produce smaller details.</param>
        /// <param name="amplitude">The amplitude is how tall the features should be. Frequency determines the width of features, amplitude determines the height. Each octave the amplitude shrinks, meaning small features are also short. This doesn't have to be the case, but for this case it makes pleasing maps.</param>
        /// <param name="lacunarity">Lacunarity is what makes the frequency grow. Each octave the frequency is multiplied by the lacunarity. I use a lacunarity of 2.0, however values of 1.8715 or 2.1042 can help to reduce artifacts in some algorithms. A lacunarity of 2.0 means that the frequency doubles each octave, so if the first octave had 3 points the second would have 6, then 12, then 24, etc. This is used almost exclusively, partly because octaves in music double in frequency. Other values are perfectly acceptable, but the results will vary.</param>
        /// <param name="persistence">Persistence is what makes the amplitude shrink (or not shrink). Each octave the amplitude is multiplied by the gain. I use a gain of 0.65. If it is higher then the amplitude will barely shrink, and maps get crazy. Too low and the details become miniscule, and the map looks washed out. However, most use 1/lacunarity. Since the standard for lacunarity is 2.0, the standard for the gain is 0.5. Noise that has a gain of 0.5 and a lacunarity of 2.0 is referred to as 1/f noise, and is the industry standard.</param>
        /// <param name="offsetValue">Gets added to the value before normalization</param>
        /// <returns></returns>
        public static float FBMSimplex(float x, float z, int octaves, float frequency, float amplitude, float lacunarity = 2, float persistence = 0.5f)
        {
            float total = 0;

            for (int o = 0; o < octaves; o++)
            {
                total += Noise.GetSimplex(x * frequency, z * frequency) * amplitude;
                frequency *= lacunarity;
                amplitude *= persistence;
            }

            return NormalizeNoise(total);
        }

        public static float Simplex(float x, float z, float frequency, float amplitude, int offsetX = 0, int offsetZ = 0)
        {
            return NormalizeNoise(Noise.GetSimplex((x + offsetX) * frequency, (z + offsetZ) * frequency) * amplitude);
        }

        /// <summary>
        /// Layers multiple Value Noises
        /// </summary>
        /// <param name="x">x position in worldspace</param>
        /// <param name="z">Z position in world space</param>
        /// <param name="octaves">Octaves are how many layers you are putting together. If you start with big features, the number of octaves determines how detailed the map will look.</param>
        /// <param name="frequency">The frequency of a layer is how many points fit into the space you've created. So for the mountain scale, you only need a few points, but at the rock scale you may need hundreds of points. In the code above, I start with a small frequency (which equates to large features) and move to higher frequencies which produce smaller details.</param>
        /// <param name="amplitude">The amplitude is how tall the features should be. Frequency determines the width of features, amplitude determines the height. Each octave the amplitude shrinks, meaning small features are also short. This doesn't have to be the case, but for this case it makes pleasing maps.</param>
        /// <param name="lacunarity">Lacunarity is what makes the frequency grow. Each octave the frequency is multiplied by the lacunarity. I use a lacunarity of 2.0, however values of 1.8715 or 2.1042 can help to reduce artifacts in some algorithms. A lacunarity of 2.0 means that the frequency doubles each octave, so if the first octave had 3 points the second would have 6, then 12, then 24, etc. This is used almost exclusively, partly because octaves in music double in frequency. Other values are perfectly acceptable, but the results will vary.</param>
        /// <param name="persistence">Persistence is what makes the amplitude shrink (or not shrink). Each octave the amplitude is multiplied by the gain. I use a gain of 0.65. If it is higher then the amplitude will barely shrink, and maps get crazy. Too low and the details become miniscule, and the map looks washed out. However, most use 1/lacunarity. Since the standard for lacunarity is 2.0, the standard for the gain is 0.5. Noise that has a gain of 0.5 and a lacunarity of 2.0 is referred to as 1/f noise, and is the industry standard.</param>
        /// <param name="offsetValue">Gets added to the value before normalization</param>
        /// <returns></returns>
        public static float FBMValue(float x, float z, int octaves, float frequency, float amplitude, float lacunarity = 2, float persistence = 0.5f)
        {
            float total = 0;

            for (int o = 0; o < octaves; o++)
            {
                total += Noise.GetValue(x * frequency, z * frequency) * amplitude;
                frequency *= lacunarity;
                amplitude *= persistence;
            }

            return NormalizeNoise(total);
        }

        public static float Value(float x, float z, float frequency, float amplitude, int offsetX = 0, int offsetZ = 0)
        {
            return NormalizeNoise(Noise.GetValue((x + offsetX) * frequency, (z + offsetZ) * frequency) * amplitude);
        }

        /// <summary>
        /// Layers multiple Cellular Noises
        /// </summary>
        /// <param name="x">x position in worldspace</param>
        /// <param name="z">Z position in world space</param>
        /// <param name="octaves">Octaves are how many layers you are putting together. If you start with big features, the number of octaves determines how detailed the map will look.</param>
        /// <param name="frequency">The frequency of a layer is how many points fit into the space you've created. So for the mountain scale, you only need a few points, but at the rock scale you may need hundreds of points. In the code above, I start with a small frequency (which equates to large features) and move to higher frequencies which produce smaller details.</param>
        /// <param name="amplitude">The amplitude is how tall the features should be. Frequency determines the width of features, amplitude determines the height. Each octave the amplitude shrinks, meaning small features are also short. This doesn't have to be the case, but for this case it makes pleasing maps.</param>
        /// <param name="lacunarity">Lacunarity is what makes the frequency grow. Each octave the frequency is multiplied by the lacunarity. I use a lacunarity of 2.0, however values of 1.8715 or 2.1042 can help to reduce artifacts in some algorithms. A lacunarity of 2.0 means that the frequency doubles each octave, so if the first octave had 3 points the second would have 6, then 12, then 24, etc. This is used almost exclusively, partly because octaves in music double in frequency. Other values are perfectly acceptable, but the results will vary.</param>
        /// <param name="persistence">Persistence is what makes the amplitude shrink (or not shrink). Each octave the amplitude is multiplied by the gain. I use a gain of 0.65. If it is higher then the amplitude will barely shrink, and maps get crazy. Too low and the details become miniscule, and the map looks washed out. However, most use 1/lacunarity. Since the standard for lacunarity is 2.0, the standard for the gain is 0.5. Noise that has a gain of 0.5 and a lacunarity of 2.0 is referred to as 1/f noise, and is the industry standard.</param>
        /// <param name="offsetValue">Gets added to the value before normalization</param>
        /// <returns></returns>
        public static float FBMCellular(float x, float z, int octaves, float frequency, float amplitude, float lacunarity = 2, float persistence = 0.5f)
        {
            float total = 0;

            for (int o = 0; o < octaves; o++)
            {
                total += Noise.GetCellular(x * frequency, z * frequency) * amplitude;
                frequency *= lacunarity;
                amplitude *= persistence;
            }

            return NormalizeNoise(total);
        }

        public static float Cellular(float x, float z, float frequency, float amplitude, int offsetX = 0, int offsetZ = 0)
        {
            return NormalizeNoise(Noise.GetCellular((x + offsetX) * frequency, (z + offsetZ) * frequency) * amplitude);
        }

        public static float White(float x, float z, float frequency = 1, float amplitude = 1, int offsetX = 0, int offsetZ = 0)
        {
            return NormalizeNoise(Noise.GetWhiteNoise((x + offsetX) * frequency, (z + offsetZ) * frequency) * amplitude);
        }

        /// <summary>
        /// Used to normalize values between -1 and 1
        /// </summary>
        /// <param name="value"></param>
        /// <param name="offsetValue">Gets added to the value before normalization</param>
        /// <returns></returns>
        static float NormalizeNoise(float value)
        {
            value = (value / 2) + 0.5f;
            if (value < 0)
                value = 0;
            else if (value > 1)
                value = 1;

            return value;
        }

        public static int MapToWorld(float noiseValue)
        {
            return (int)(noiseValue * CommonConstants.World.chunkSize.Y);
        }

        public static byte MapToWorldByte(float noiseValue)
        {
            return (byte)(noiseValue * CommonConstants.World.chunkSize.Y);
        }
    }
}