using System;
using System.Collections.Generic;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Engine;
using VoxelValley.Client.Engine.Graphics.Rendering;
using VoxelValley.Client.Engine.SceneGraph;
using VoxelValley.Client.Engine.SceneGraph.Components;
using VoxelValley.Client.Game.Entities;
using VoxelValley.Common.Helper;


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

        protected override void OnUpdate(float deltaTime)
        {
            Vector3i palyerPosInChukSpace = CoordinateHelper.ConvertFromWorldSpaceToChunkSpace(new Vector3i(
                            (int)Player.Transform.Position.X,
                            (int)Player.Transform.Position.Y,
                            (int)Player.Transform.Position.Z));

            // UnloadDistant(palyerPosInChukSpace);

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

        public void UnloadDistant(Vector3i positionInChunkSpace)
        {
            foreach (Vector3i chunkPos in chunks.Keys)
            {
                double distance = Math.Sqrt((Math.Pow((chunkPos.X - positionInChunkSpace.X), 2) + Math.Pow((chunkPos.Z - positionInChunkSpace.Z), 2)));
                if (distance > ClientConstants.Graphics.ViewDistance)
                    RemoveChunk(chunkPos);
            }
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
                MeshRenderer meshRenderer = chunk.GetComponent<MeshRenderer>();
                meshRenderer.Mesh = null;

                OnVoxelRenderBufferChanged(meshRenderer);

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