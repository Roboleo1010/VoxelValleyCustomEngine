using System;
using VoxelValley.Engine.Graphics;

namespace VoxelValley.Engine.Core.ComponentSystem.Components
{
    public class MeshRenderer : Component
    {
        #region  Mesh
        public Action<MeshRenderer> OnMeshChanged;
        Mesh _mesh;
        public Mesh Mesh
        {
            get { return _mesh; }
            set
            {
                _mesh = value;
                if (OnMeshChanged != null)
                    OnMeshChanged(this);
            }
        }
        #endregion
    }
}