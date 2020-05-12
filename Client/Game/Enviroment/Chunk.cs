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

namespace VoxelValley.Client.Game.Enviroment
{
    public class Chunk : GameObject
    {
        public bool IsFinished = false;

        Type type = typeof(Chunk);
        Vector3i positionInWorldSpace;

        public ushort[,,] voxels = new ushort[CommonConstants.World.chunkSize.X, CommonConstants.World.chunkSize.Y, CommonConstants.World.chunkSize.Z]; //TODO Use short? instead of reference. Short = 2 byte ref = 8 byte

        public Chunk(string name, GameObject parent, Vector3i positionInChunkSpace) : base(name, parent)
        {
            positionInWorldSpace = CoordinateHelper.ConvertFromChunkSpaceToWorldSpace(positionInChunkSpace);

            Transform.Position = positionInWorldSpace.ToVector3();

            ThreadManager.CreateThread(() => { Generate(); CreateMesh(); }, () => { IsFinished = true; }, $"Chunk_{positionInChunkSpace}", ThreadPriority.Normal);
        }

        void Generate()
        {
            for (int localX = 0; localX < CommonConstants.World.chunkSize.X; localX++)
                for (int localZ = 0; localZ < CommonConstants.World.chunkSize.Z; localZ++)
                {
                    int worldX = positionInWorldSpace.X + localX;
                    int worldZ = positionInWorldSpace.Z + localZ;

                    for (int localY = 0; localY < CommonConstants.World.chunkSize.Y; localY++)
                        voxels[localX, localY, localZ] = RegionManager.GetRegion(worldX, worldZ).GetVoxel(worldX, localY, worldZ);
                }
        }

        void CreateMesh()
        {
            ChunkMesh mesh = new ChunkMesh(this);
            mesh.Create();

            MeshRenderBuffer renderBuffer = RenderBufferManager.GetBufferMeshRenderBuffer(RenderBufferManager.MeshRenderBufferType.VOXEL);

            MeshRenderer meshRenderer = AddComponent<MeshRenderer>();
            meshRenderer.OnMeshChanged += renderBuffer.OnMeshChanged;
            meshRenderer.Mesh = mesh;
        }
    }
}