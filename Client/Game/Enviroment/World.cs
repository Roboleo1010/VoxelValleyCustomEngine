using System;
using System.Collections.Generic;
using System.Linq;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Engine;
using VoxelValley.Client.Engine.SceneGraph;
using VoxelValley.Client.Game.Entities;
using VoxelValley.Common.Helper;

namespace VoxelValley.Client.Game.Enviroment
{
    public class World : GameObject
    {
        Type type = typeof(World);
        Dictionary<Vector3i, Chunk> chunks;

        public Player Player;

        public World(string name) : base(name)
        {
            chunks = new Dictionary<Vector3i, Chunk>();
            CreateChunk(new Vector3i(0, 0, 0));
        }

        protected override void OnUpdate(float deltaTime)
        {
            Vector3i palyerPosInChukSpace = CoordinateHelper.ConvertFromWorldSpaceToChunkSpace(new Vector3i(
                            (int)Player.Transform.Position.X,
                            (int)Player.Transform.Position.Y,
                            (int)Player.Transform.Position.Z));

            if (chunks.Count > 0 && chunks.ElementAt(0).Value.IsFinished) //FIXME For testing, so first region is always at 0,0
                CreateAround(palyerPosInChukSpace);
        }

        private Chunk CreateChunk(Vector3i positionInChunkSpace)
        {
            Chunk chunk = GetChunk(positionInChunkSpace);
            if (chunk == null)
            {
                chunk = new Chunk($"Chunk {positionInChunkSpace.ToString()}", this.gameObject, positionInChunkSpace);
                chunks.Add(positionInChunkSpace, chunk);
            }
            return chunk;
        }

        private void CreateAround(Vector3i position)
        {
            for (int x = -ClientConstants.Graphics.RenderDistance; x < ClientConstants.Graphics.RenderDistance; x++)
                for (int z = -ClientConstants.Graphics.RenderDistance; z < ClientConstants.Graphics.RenderDistance; z++)
                    CreateChunk(new Vector3i(position.X + x, 0, position.Z + z));
        }

        public Chunk GetChunk(Vector3i positionInChunkSpace)
        {
            if (chunks.TryGetValue(positionInChunkSpace, out Chunk chunk))
                return chunk;
            return null;
        }

        void RemoveChunk(Vector3i positionInChunkSpace)
        {
            Chunk chunk = GetChunk(positionInChunkSpace);

            if (chunk != null)
            {
                chunk.Destroy();
                chunks.Remove(positionInChunkSpace);
            }
        }
    }
}