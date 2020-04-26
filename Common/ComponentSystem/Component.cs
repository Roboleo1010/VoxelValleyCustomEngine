namespace VoxelValley.Engine.Core.ComponentSystem
{
    public abstract class Component
    {
        internal GameObject ParentGameObject;
        internal bool IsActive = true;

        internal virtual void OnAdd()
        {

        }

        internal virtual void OnUpdate(float deltaTime)
        {

        }

        internal virtual void OnTick(float deltaTime)
        {

        }

        internal virtual void OnRemove()
        {

        }
    }
}