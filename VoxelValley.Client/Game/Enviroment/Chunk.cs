using System;
using System.Threading;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Engine.SceneGraph;
using VoxelValley.Client.Engine.SceneGraph.Components;
using VoxelValley.Client.Engine.Threading;
using VoxelValley.Common.Enviroment.RegionManagement;

namespace VoxelValley.Client.Game.Enviroment
{
    public class Chunk : GameObject
    {
        public bool IsFinished = false;

        Type type = typeof(Chunk);
        Vector3i positionInWorldSpace;

        public ushort[,,] voxels;

        public Chunk(string name, GameObject parent, Vector3i positionInChunkSpace) : base(name, parent)
        {
            positionInWorldSpace = World.ConvertFromChunkSpaceToWorldSpace(positionInChunkSpace);
            Transform.Position = positionInWorldSpace.ToVector3();

            ThreadManager.CreateThread(() => { Generate(); CreateMesh(); }, () => { IsFinished = true; }, $"Chunk_{positionInChunkSpace}", ThreadPriority.AboveNormal);
        }

        void Generate()
        {
            voxels = RegionManager.GetChunk(positionInWorldSpace.X, positionInWorldSpace.Z);
        }

        void CreateMesh()
        {
            ChunkMesh chunkMesh = new ChunkMesh(this);
            chunkMesh.Create();

            MeshRenderer meshRenderer = AddComponent<MeshRenderer>();
            meshRenderer.Mesh = chunkMesh;
        }
    }
}