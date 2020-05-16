using System;
using Newtonsoft.Json;
using OpenToolkit.Mathematics;

namespace VoxelValley.Client.Game.Enviroment.Structures
{
    public class Structure
    {
        public string Name { get; private set; }
        public Vector3i Origin { get; private set; }
        public Vector3i Dimension { get; private set; }
        public ushort[,,] Voxels { get; private set; }
        public Spawn[] Spawns { get; set; }
        public Vector2i NoiseOffset { get; private set; }

        [JsonConstructor]
        public Structure(string name, Voxel[] voxels, Spawn[] spawns)
        {
            Name = name;

            int nameHashCode = name.GetHashCode();

            NoiseOffset = new Vector2i(Common.Helper.MathHelper.Map(-1000, 1000, int.MinValue / name.Length, int.MaxValue / name.Length, nameHashCode / name.Length),
                                       Common.Helper.MathHelper.Map(-1000, 1000, int.MinValue / voxels.Length, int.MaxValue / voxels.Length, nameHashCode / voxels.Length));

            int minX = int.MaxValue;
            int maxX = int.MinValue;

            int minY = int.MaxValue;
            int maxY = int.MinValue;

            int minZ = int.MaxValue;
            int maxZ = int.MinValue;

            foreach (Voxel voxel in voxels)
            {
                if (voxel.position.X > maxX)
                    maxX = voxel.position.X;
                if (voxel.position.X < minX)
                    minX = voxel.position.X;

                if (voxel.position.Y > maxY)
                    maxY = voxel.position.Y;
                if (voxel.position.Y < minY)
                    minY = voxel.position.Y;

                if (voxel.position.Z > maxZ)
                    maxZ = voxel.position.Z;
                if (voxel.position.Z < minZ)
                    minZ = voxel.position.Z;
            }

            minX = Math.Abs(minX);
            maxX = Math.Abs(maxX);

            minY = Math.Abs(minY);
            maxY = Math.Abs(maxY);

            minZ = Math.Abs(minZ);
            maxZ = Math.Abs(maxZ);

            Dimension = new Vector3i(minX + maxX + 1, minY + maxY + 1, minZ + maxZ + 1);

            Voxels = new ushort[Dimension.X, Dimension.Y, Dimension.Z];

            Origin = new Vector3i(minX, minY, minZ);

            foreach (Voxel voxel in voxels)
                Voxels[voxel.position.X + minX, voxel.position.Y + minY, voxel.position.Z + minZ] = voxel.voxel;

            Spawns = spawns;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}