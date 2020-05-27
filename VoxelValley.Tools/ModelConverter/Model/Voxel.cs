using System.Drawing;
using System.Text.Json.Serialization;

namespace VoxelValley.Tools.ModelConverter
{
    public class Voxel
    {
        public string voxel { get; set; }
        public int[] position { get; set; }

        [JsonIgnore]
        public Color color { get; set; }

        public Voxel(Color color, int[] position)
        {
            this.color = color;
            this.position = position;
        }

        public override string ToString()
        {
            return voxel;
        }
    }
}