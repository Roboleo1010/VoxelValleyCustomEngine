using System;
using System.Threading;
using OpenToolkit.Mathematics;
using VoxelValley.Common.Helper;
using VoxelValley.Client.Engine.SceneGraph;
using VoxelValley.Client.Engine.SceneGraph.Components;
using VoxelValley.Client.Engine.Threading;
using VoxelValley.Common;
using VoxelValley.Client.Engine.Graphics.Rendering;
using VoxelValley.Client.Game.Enviroment.RegionManagement;
using VoxelValley.Client.Engine.Graphics.Shading;

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
            positionInWorldSpace = CoordinateHelper.ConvertFromChunkSpaceToWorldSpace(positionInChunkSpace);
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

        public static bool InChunk(int x, int y, int z)
        {
            return (x > 0 && y > 0 && z > 0 &&
                x < CommonConstants.World.chunkSize.X &&
                y < CommonConstants.World.chunkSize.Y &&
                z < CommonConstants.World.chunkSize.Z);
        }
    }
}