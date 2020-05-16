namespace VoxelValley.Client.Game.Enviroment.Structures
{
    public class StructureSpawn
    {
        public Structure Structure;
        public float Chance;

        public StructureSpawn(Structure structure, Spawn spawn)
        {
            Structure = structure;
            Chance = spawn.Chance;
        }

        public override string ToString()
        {
            return Structure.Name;
        }
    }
}