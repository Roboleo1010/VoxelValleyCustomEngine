using System;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Game;
using VoxelValley.Client.Game.Enviroment;
using VoxelValley.Common.ComponentSystem;
using VoxelValley.Common.ComponentSystem.Components;
using VoxelValley.Common.Enviroment.Generation;
using VoxelValley.Common.Helper;
using VoxelValley.Common.Threading;
using VoxelValley.Game;

namespace VoxelValley.Common.Enviroment
{
    public class Chunk : GameObject
    {
        Type type = typeof(Chunk);
        Vector3i positionInChunkSpace;
        Vector3i positionInWorldSpace;

        public Voxel[,,] voxels = new Voxel[Constants.World.chunkSize.X, Constants.World.chunkSize.Y, Constants.World.chunkSize.Z];
        public bool KeepAlive = true;

        public Chunk(string name, GameObject parent, Vector3i positionInChunkSpace) : base(name, parent)
        {
            this.positionInChunkSpace = positionInChunkSpace;
            positionInWorldSpace = CoordinateHelper.ConvertFromChunkSpaceToWorldSpace(positionInChunkSpace);

            Transform.Position = positionInWorldSpace.ToVector3();

            ThreadManager.CreateThread(() => { Generate(); CreateMesh(); }, () => { }, $"Chunk_{positionInChunkSpace}", ThreadManager.ThreadPriority.NORMAL);
        }

        void Generate()
        {
            Random r = new Random();

            for (int x = 0; x < Constants.World.chunkSize.X; x++)
                for (int z = 0; z < Constants.World.chunkSize.Z; z++)
                    for (int y = 0; y < Constants.World.chunkSize.Y; y++)
                        if (WorldGenerator.GetHeight(positionInWorldSpace.X + x, positionInWorldSpace.Z + z) == y)
                            voxels[x, y, z] = new Voxel(new Vector3(new Vector3((float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble())));
        }

        void CreateMesh()
        {
            ChunkMesh mesh = new ChunkMesh(this);
            mesh.Create();

            MeshRenderer meshRenderer = AddComponent<MeshRenderer>();
            meshRenderer.Mesh = mesh;

            ReferencePointer.World.OnVoxelRenderBufferChanged(meshRenderer);
        }
    }
}