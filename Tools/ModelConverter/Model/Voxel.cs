using System.Drawing;
using System.Text.Json.Serialization;

namespace VoxelValley.Tools.ModelConverter
{
    public class Voxel
    {
        [JsonIgnore]
        public Color color { get; set; }

        public string voxel;
        public int[] position;

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