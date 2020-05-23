using System;
using System.Collections.Generic;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Engine;
using VoxelValley.Client.Engine.SceneGraph;
using VoxelValley.Client.Game.Entities;
using VoxelValley.Common.Helper;

namespace VoxelValley.Client.Game.Enviroment
{
    public class World : GameObject
    {
        public static World Instance;
        Type type = typeof(World);
        Dictionary<Vector3i, Chunk> chunks;

        public Player Player;

        public World(string name) : base(name)
        {
            Instance = this;
            chunks = new Dictionary<Vector3i, Chunk>();
            CreateChunk(new Vector3i(0, 0, 0), true);
        }

        protected override void OnUpdate(float deltaTime)
        {
            Vector3i palyerPosInChukSpace = CoordinateHelper.ConvertFromWorldSpaceToVoxelSpace(Player.Transform.Position).chunk;

             CreateAround(palyerPosInChukSpace);
        }

        private Chunk CreateChunk(Vector3i positionInChunkSpace, bool generateThreaded = true)
        {
            Chunk chunk = GetChunk(positionInChunkSpace);
            if (chunk == null)
            {
                chunk = new Chunk($"Chunk {positionInChunkSpace.ToString()}", this.gameObject, positionInChunkSpace, generateThreaded);
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

        public ushort GetVoxelFromWoldSpace(Vector3 pos)
        {
            (Vector3i chunk, Vector3i voxel) convertedPos = CoordinateHelper.ConvertFromWorldSpaceToVoxelSpace(pos);

            Chunk chunk = GetChunk(convertedPos.chunk);

            if (chunk == null || chunk.voxels == null)
                return 0;

            return chunk.voxels[convertedPos.voxel.X, convertedPos.voxel.Y, convertedPos.voxel.Z];
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