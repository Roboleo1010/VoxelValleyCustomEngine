using System;
using System.Collections.Generic;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Engine.SceneGraph;
using VoxelValley.Client.Game.Entities;
using VoxelValley.Common;

namespace VoxelValley.Client.Game.Enviroment
{
    public class World : GameObject
    {
        public static World Instance;
        Type type = typeof(World);
        Dictionary<Vector3i, Chunk> chunks;

        public World(string name) : base(name)
        {
            Instance = this;
            chunks = new Dictionary<Vector3i, Chunk>();
            CreateChunk(new Vector3i(0, 0, 0));
        }

        protected override void OnUpdate(float deltaTime)
        {
            Vector3i palyerPosInChukSpace = World.ConvertFromWorldSpaceToVoxelSpace(Player.Instance.Transform.Position).chunk;
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
            for (int x = -CommonConstants.World.DrawDistance; x < CommonConstants.World.DrawDistance; x++)
                for (int z = -CommonConstants.World.DrawDistance; z < CommonConstants.World.DrawDistance; z++)
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
            (Vector3i chunk, Vector3i voxel) convertedPos = World.ConvertFromWorldSpaceToVoxelSpace(pos);

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

        public static Vector3i ConvertFromChunkSpaceToWorldSpace(Vector3i chunkSpacePos)
        {
            return new Vector3i(
                        chunkSpacePos.X * CommonConstants.World.ChunkSize.X,
                        chunkSpacePos.Y * CommonConstants.World.ChunkSize.Y,
                        chunkSpacePos.Z * CommonConstants.World.ChunkSize.Z);
        }

        public static (Vector3i chunk, Vector3i voxel) ConvertFromWorldSpaceToVoxelSpace(Vector3 worldSpacePos)
        {
            Vector3i chunk = new Vector3i(0, 0, 0);
            Vector3i voxel = new Vector3i(0, (int)worldSpacePos.Y, 0);

            if (worldSpacePos.X < 0)
            {
                chunk.X = (int)Math.Floor(worldSpacePos.X / CommonConstants.World.ChunkSize.X);
                voxel.X = (CommonConstants.World.ChunkSize.X - 1) - (((int)worldSpacePos.X % CommonConstants.World.ChunkSize.X) * -1);
            }
            else
            {
                chunk.X = (int)worldSpacePos.X / CommonConstants.World.ChunkSize.X;
                voxel.X = ((int)worldSpacePos.X % CommonConstants.World.ChunkSize.X);
            }

            if (worldSpacePos.Z < 0)
            {
                chunk.Z = (int)Math.Floor(worldSpacePos.Z / CommonConstants.World.ChunkSize.Z);
                voxel.Z = (CommonConstants.World.ChunkSize.Z - 1) - ((int)worldSpacePos.Z % CommonConstants.World.ChunkSize.Z * -1);
            }
            else
            {
                chunk.Z = (int)worldSpacePos.Z / CommonConstants.World.ChunkSize.Z;
                voxel.Z = (int)worldSpacePos.Z % CommonConstants.World.ChunkSize.Z;
            }

            return (chunk, voxel);
        }
    }
}