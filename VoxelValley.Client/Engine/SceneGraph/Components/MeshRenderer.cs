using VoxelValley.Client.Engine.Graphics;
using VoxelValley.Client.Engine.Graphics.Rendering;

namespace VoxelValley.Client.Engine.SceneGraph.Components
{
    public class MeshRenderer : Component
    {
        #region Mesh
        Mesh _mesh;
        public Mesh Mesh
        {
            get
            {
                return _mesh;
            }
            set
            {
                Mesh oldValue = _mesh;
                _mesh = value;

                if (oldValue != null)
                    RenderBufferManager.GetBuffer(oldValue.ShaderType).RemoveMesh(oldValue);

                RenderBufferManager.GetBuffer(_mesh.ShaderType).AddMesh(_mesh);
            }
        }
        #endregion

        internal override void OnRemove()
        {
            Mesh = null;
        }
    }
}