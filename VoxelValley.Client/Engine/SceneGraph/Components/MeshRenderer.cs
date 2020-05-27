using System;
using VoxelValley.Client.Engine.Graphics;

namespace VoxelValley.Client.Engine.SceneGraph.Components
{
    public class MeshRenderer : Component
    {
        #region Mesh
        public event Action<MeshRenderer, Mesh, Mesh> OnMeshChanged;
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

                if (OnMeshChanged != null)
                    OnMeshChanged(this, oldValue, _mesh);
            }
        }
        #endregion

        internal override void OnRemove()
        {
            Mesh = null;
        }
    }
}