using System;
using System.Collections.Generic;
using VoxelValley.Engine.Core.ComponentSystem;
using VoxelValley.Engine.Core.ComponentSystem.Components;
using VoxelValley.Engine.Core.Helper;
using VoxelValley.Engine.Graphics.Rendering;
using VoxelValley.Engine.Graphics.Shading;
using VoxelValley.Game.Helper;

namespace VoxelValley.Game.Enviroment
{
    public class World : GameObject
    {
        Type type = typeof(World);
        Dictionary<Vector3Int, Chunk> chunks;

        RenderBuffer renderBufferVoxel;
        List<MeshRenderer> meshRenderersInVoxelRenderBuffer;

        public World(string name, GameObject parent) : base(name, parent)
        {
            chunks = new Dictionary<Vector3Int, Chunk>();

            renderBufferVoxel = RenderBufferManager.AddBuffer("Voxel", "voxel");
            meshRenderersInVoxelRenderBuffer = new List<MeshRenderer>();
        }

        protected override void OnTick(float deltaTime)
        {
            CreateChunk(new Vector3Int(0, 0, 0));
        }

        protected override void OnUpdate(float deltaTime)
        {
            CreateAround(CoordinateHelper.ConvertFromWorldSpaceToChunkSpace(new Vector3Int(ReferencePointer.Player.Transform.Position)));
        }

        private Chunk CreateChunk(Vector3Int positionInChunkSpace)
        {
            Chunk chunk = GetChunk(positionInChunkSpace);
            if (chunk == null)
            {
                chunk = new Chunk($"Chunk {positionInChunkSpace.ToString()}", this.gameObject, positionInChunkSpace);
                chunks.Add(positionInChunkSpace, chunk);
            }
            return chunk;
        }

        private void CreateAround(Vector3Int position)
        {
            for (int x = -Constants.Game.ViewDistance; x < Constants.Game.ViewDistance; x++)
                for (int z = -Constants.Game.ViewDistance; z < Constants.Game.ViewDistance; z++)
                    CreateChunk(new Vector3Int(position.X + x, 0, position.Z + z));
        }

        public Chunk GetChunk(Vector3Int positionInChunkSpace)
        {
            if (chunks.TryGetValue(positionInChunkSpace, out Chunk chunk))
                return chunk;
            return null;
        }

        void RemoveChunk(Vector3Int positionInChunkSpace)
        {
            Chunk chunk = GetChunk(positionInChunkSpace);

            if (chunk != null)
            {
                chunk.Destroy();
                chunks.Remove(positionInChunkSpace);
            }
        }

        public void OnVoxelRenderBufferChanged(MeshRenderer meshRenderer)
        {
            if (meshRenderersInVoxelRenderBuffer.Contains(meshRenderer))
                renderBufferVoxel.RemoveMesh(meshRenderer.Mesh);

            renderBufferVoxel.AddMesh(meshRenderer.Mesh);

            lock (meshRenderersInVoxelRenderBuffer)
                meshRenderersInVoxelRenderBuffer.Add(meshRenderer);
        }
    }
}