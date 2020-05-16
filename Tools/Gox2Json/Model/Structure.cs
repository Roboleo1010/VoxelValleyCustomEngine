namespace VoxelValley.Tools.Gox2Json.Model
{
    public class Structure
    {
        public string Name { get; private set; }
        public Spawn[] Spawns { get; private set; }
        public Voxel[] Voxels { get; private set; }   

        public Structure(string name, Voxel[] voxels, Spawn[] spawns)
        {
            Name = name;
            Voxels = voxels;
            Spawns = spawns;           
        }
    }
}