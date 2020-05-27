using Newtonsoft.Json;
using OpenToolkit.Mathematics;

namespace VoxelValley.Client.Game.Enviroment.Structures
{
    /// <summary>
    /// JSON Helper Class
    /// </summary>
    public class Voxel
    {
        public ushort voxel { get; private set; }
        public Vector3i position { get; private set; }

        [JsonConstructor]
        public Voxel(string voxel, int[] position)
        {
            this.voxel = VoxelManager.GetVoxel(voxel).Id;
            this.position = new Vector3i(position[0], position[1], position[2]);
        }
    }
}