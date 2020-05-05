using System;
using System.Threading;
using OpenToolkit.Mathematics;
using VoxelValley.Common.Helper;
using VoxelValley.Client.Engine.SceneGraph;
using VoxelValley.Client.Engine.SceneGraph.Components;
using VoxelValley.Client.Engine.Threading;
using VoxelValley.Common;
using VoxelValley.Client.Engine.Graphics.Rendering;
using VoxelValley.Client.Game.Enviroment.Generation.Biomes;

namespace VoxelValley.Client.Game.Enviroment
{
    public class Chunk : GameObject
    {
        Type type = typeof(Chunk);
        Vector3i positionInWorldSpace;

        public VoxelType[,,] voxels = new VoxelType[CommonConstants.World.chunkSize.X, CommonConstants.World.chunkSize.Y, CommonConstants.World.chunkSize.Z];
        public bool KeepAlive = true;

        public Chunk(string name, GameObject parent, Vector3i positionInChunkSpace) : base(name, parent)
        {
            positionInWorldSpace = CoordinateHelper.ConvertFromChunkSpaceToWorldSpace(positionInChunkSpace);

            Transform.Position = positionInWorldSpace.ToVector3();

            ThreadManager.CreateThread(() => { Generate(); CreateMesh(); }, () => { }, $"Chunk_{positionInChunkSpace}", ThreadPriority.Normal);
        }

        void Generate()
        {
            for (int x = 0; x < CommonConstants.World.chunkSize.X; x++)
                for (int z = 0; z < CommonConstants.World.chunkSize.Z; z++)
                    for (int y = 0; y < CommonConstants.World.chunkSize.Y; y++)
                    {
                        int height = BiomeManager.GetBiome(positionInWorldSpace.X + x, positionInWorldSpace.Z + z).GetHeight(positionInWorldSpace.X + x, positionInWorldSpace.Z + z);
                        if (height == y)
                        {
                            if (y > 100)
                                voxels[x, y, z] = VoxelTypeManager.GetVoxelType("snow");
                            else if (y > 90)
                                voxels[x, y, z] = VoxelTypeManager.GetVoxelType("stone");
                            else
                                voxels[x, y, z] = VoxelTypeManager.GetVoxelType("grass");
                        }
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