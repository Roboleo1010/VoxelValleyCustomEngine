using System;
using OpenToolkit.Mathematics;

namespace VoxelValley.Engine.Core.ComponentSystem.Components
{
    public class Transform : Component
    {
        #region  Position
        public Action<Transform> OnPositionChanged;
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
                if (OnPositionChanged != null)
                    OnPositionChanged(this);
            }
        }
        #endregion

        #region Rotation
        public Action<Transform> OnRotationChanged;
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
                if (OnRotationChanged != null)
                    OnRotationChanged(this);
            }
        }
        #endregion

        #region  Scale
        public Action<Transform> OnScaleChanged;
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
                if (OnScaleChanged != null)
                    OnScaleChanged(this);
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