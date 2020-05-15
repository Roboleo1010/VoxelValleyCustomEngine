namespace VoxelValley.Tools.Gox2Json.Model
{
    public class Structure
    {
        public string name;
        public Spawn[] spawns;
        public Voxel[] voxels;

        public Structure(string name, Voxel[] voxels, Spawn[] spawns)
        {
            this.name = name;
            this.voxels = voxels;
            this.spawns = spawns;
        }
    }
}