using System;
using System.Collections.Generic;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Engine;
using VoxelValley.Client.Engine.Graphics.Rendering;
using VoxelValley.Client.Engine.SceneGraph;
using VoxelValley.Client.Engine.SceneGraph.Components;
using VoxelValley.Common.Helper;
using VoxelValley.Client.Game.Entities;

namespace VoxelValley.Client.Game.Enviroment
{
    public class World : GameObject
    {
        Type type = typeof(World);
        Dictionary<Vector3i, Chunk> chunks;

        RenderBuffer renderBufferVoxel;
        List<MeshRenderer> meshRenderersInVoxelRenderBuffer;

        public Player Player;

        public World(string name) : base(name)
        {
            chunks = new Dictionary<Vector3i, Chunk>();

            renderBufferVoxel = RenderBufferManager.AddBuffer("Voxel", "voxel");
            meshRenderersInVoxelRenderBuffer = new List<MeshRenderer>();
        }

        protected override void OnTick(float deltaTime)
        {
            CreateChunk(new Vector3i(0, 0, 0));
        }

        protected override void OnUpdate(float deltaTime)
        {
            CreateAround(CoordinateHelper.ConvertFromWorldSpaceToChunkSpace(new Vector3i(
                (int)Player.Transform.Position.X,
                (int)Player.Transform.Position.Y,
                (int)Player.Transform.Position.Z)));
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
            for (int x = -ClientConstants.Graphics.ViewDistance; x < ClientConstants.Graphics.ViewDistance; x++)
                for (int z = -ClientConstants.Graphics.ViewDistance; z < ClientConstants.Graphics.ViewDistance; z++)
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