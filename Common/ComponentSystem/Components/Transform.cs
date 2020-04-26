using System;
using OpenToolkit.Mathematics;

namespace VoxelValley.Common.ComponentSystem.Components
{
    public class Transform : Component
    {
        #region  Position
        Vector3 _position;
        public Vector3 Position
        {
            get
            {
                if (ParentGameObject.Parent == null)
                {
                    return _position;
                }
                else
                {
                    return ParentGameObject.Parent.Transform.Position + _position;
                }
            }
            set
            {
                _position = value;
            }
        }
        #endregion

        #region Rotation
                Vector3 _rotation;
        public Vector3 Rotation
        {
            get
            {
                if (ParentGameObject.Parent == null)
                {
                    return _rotation;
                }
                else
                {
                    return ParentGameObject.Parent.Transform.Rotation + _rotation;
                }
            }
            set
            {
                _rotation = value;
            }
        }
        #endregion

        #region  Scale
        Vector3 _scale;
        public Vector3 Scale
        {
            get
            {
                if (ParentGameObject.Parent == null)
                {
                    return _scale;
                }
                else
                {
                    return ParentGameObject.Parent.Transform.Scale * _scale;
                }
            }
            set
            {
                _scale = value;
            }
        }
        #endregion

        public Transform()
        {
            Position = Vector3.Zero;
            Rotation = Vector3.Zero;
            Scale = Vector3.One;
        }
    }
}