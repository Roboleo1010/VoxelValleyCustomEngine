namespace VoxelValley.Tools.Gox2Json.Model
{
    public class Voxel
    {
        public string voxel;
        public int[] position;

        public Voxel(string name, int[] position)
        {
            voxel = name;
            this.position = position;
        }

        public override string ToString()
        {
            return voxel;
        }
    }
}