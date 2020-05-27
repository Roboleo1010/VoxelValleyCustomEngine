using System;
using System.Threading;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Engine.SceneGraph;
using VoxelValley.Client.Engine.SceneGraph.Components;
using VoxelValley.Client.Engine.Threading;
using VoxelValley.Client.Engine.Graphics.Rendering;
using VoxelValley.Common.Enviroment.RegionManagement;
using VoxelValley.Client.Engine.Graphics.Shading;
using VoxelValley.Common;

namespace VoxelValley.Client.Game.Enviroment
{
    public class Chunk : GameObject
    {
        public bool IsFinished = false;

        Type type = typeof(Chunk);
        Vector3i positionInWorldSpace;

        public ushort[,,] voxels;

        public Chunk(string name, GameObject parent, Vector3i positionInChunkSpace, bool generateThreaded = true) : base(name, parent)
        {
            positionInWorldSpace = World.ConvertFromChunkSpaceToWorldSpace(positionInChunkSpace);
            Transform.Position = positionInWorldSpace.ToVector3();

            if (generateThreaded)
                ThreadManager.CreateThread(() => { Generate(); CreateMesh(); }, () => { IsFinished = true; }, $"Chunk_{positionInChunkSpace}", ThreadPriority.AboveNormal);
            else
            {
                Generate();
                CreateMesh();
            }
        }

        void Generate()
        {
            voxels = RegionManager.GetChunk(positionInWorldSpace.X, positionInWorldSpace.Z);
        }

        void CreateMesh()
        {
            ChunkMesh chunkMesh = new ChunkMesh(this);
            chunkMesh.Create();

            ((VoxelRenderBuffer)RenderBufferManager.GetBuffer(ShaderManager.ShaderType.VOXEL)).Add(chunkMesh);
        }
    }
}