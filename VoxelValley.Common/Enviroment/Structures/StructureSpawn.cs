using OpenToolkit.Mathematics;
using VoxelValley.Common.Enviroment.Generation;

namespace VoxelValley.Common.Enviroment.Structures
{
    public class StructureSpawn
    {
        public Structure Structure { get; private set; }
        public float Chance { get; private set; }
        public Vector2i NoiseOffset { get; private set; }

        public StructureSpawn(Structure structure, Spawn spawn)
        {
            Structure = structure;
            Chance = spawn.Chance;

            int nameHashCode = structure.Name.GetHashCode();
            int voxelSize = structure.Voxels.GetLength(0) + structure.Voxels.GetLength(1) + structure.Voxels.GetLength(2);

            NoiseOffset = new Vector2i((int)(GenerationUtilities.White(nameHashCode, voxelSize) * nameHashCode * 0.00001),
                                       (int)(-GenerationUtilities.White(nameHashCode / 10, -voxelSize) * structure.Name.Length * voxelSize * nameHashCode.ToString().Length * 5));
        }

        public override string ToString()
        {
            return Structure.Name;
        }
    }
}