using System;
using System.Collections.Generic;
using OpenToolkit.Mathematics;
using VoxelValley.Client.Engine.SceneGraph.Components;

namespace VoxelValley.Client.Engine.SceneGraph
{
    public class GameObject
    {
        public string Name { get; set; }
        public bool IsActive { get; private set; } = true;

        public GameObject gameObject
        {
            get { return this; }
        }

        private Dictionary<Type, Component> components;

        //Component shorthands
        public Transform Transform;

        //Scene Graph
        public List<GameObject> Children { get; private set; }
        public GameObject Parent { get; private set; }

        #region  Constructors
        public GameObject(string name)
        {
            Init(name, null, Vector3.Zero);
        }

        public GameObject(string name, GameObject parent)
        {
            Init(name, parent, Vector3.Zero);
        }

        public GameObject(string name, GameObject parent, Vector3 position)
        {
            Init(name, parent, position);
        }

        void Init(string name, GameObject parent, Vector3 position)
        {
            Name = name;
            Children = new List<GameObject>();
            components = new Dictionary<Type, Component>();

            Transform = this.AddComponent<Transform>();
            Transform.Position = position;

            if (parent == null)
                GameObjectManager.AddToRoot(this);
            else
                parent.AddChild(this);

            GameObjectManager.SceneGraphUpdated();

            Start();
        }
        #endregion

        public void SetActive(bool newState)
        {
            IsActive = newState;
        }

        public override string ToString()
        {
            return Name;
        }

        #region Start
        internal void Start()
        {
            OnStart();
        }

        protected virtual void OnStart() { }
        #endregion

        #region Update & Tick
        internal void Update(float deltaTime)
        {
            foreach (Component component in components.Values)
                component.OnUpdate(deltaTime);

            OnUpdate(deltaTime);
        }

        protected virtual void OnUpdate(float deltaTime) { }

        internal void Tick(float deltaTime)
        {
            foreach (Component component in components.Values)
                component.OnTick(deltaTime);

            OnTick(deltaTime);
        }

        protected virtual void OnTick(float deltaTime) { }
        #endregion

        #region Destroy
        internal void Destroy()
        {
            OnDestroy();

            if (Parent != null)
                Parent.RemoveChild(this);

            GameObjectManager.SceneGraphUpdated();
        }

        protected virtual void OnDestroy() { }
        #endregion

        #region Components
        public T AddComponent<T>() where T : Component
        {
            T component = (T)Activator.CreateInstance(typeof(T));
            components.Add(typeof(T), component);
            component.ParentGameObject = this;
            component.OnAdd();

            return component;
        }

        public T GetComponent<T>() where T : Component
        {
            if (components.TryGetValue(typeof(T), out Component component))
                return (T)component;

            return null;
        }

        public bool HasComponent<T>() where T : Component
        {
            return components.ContainsKey(typeof(T));
        }

        public void RemoveComponent<T>() where T : Component
        {
            if (components.TryGetValue(typeof(T), out Component component))
            {
                component.OnRemove();
                components.Remove(typeof(T));
            }
        }
        #endregion

        #region  SceneGraph
        //TODO Handle cascading deletes, adds & Reference changes
        public void AddChild(GameObject gameObject)
        {
            if (Children.Contains(gameObject))
                return;

            Children.Add(gameObject);
            gameObject.Parent = this;
            GameObjectManager.SceneGraphUpdated();
        }

        public void RemoveChild(GameObject gameObject)
        {
            if (Children.Contains(gameObject))
            {
                foreach (GameObject child in gameObject.Children)
                    child.RemoveChild(child);

                Children.Remove(gameObject);
            }
        }

        public List<GameObject> GetGameObjectsByName(string name)
        {
            List<GameObject> result = new List<GameObject>();

            foreach (GameObject gameObject in GameObjectManager.GameObjects)
            {
                if (gameObject.Name == name)
                    result.Add(gameObject);

                result.AddRange(GetGameObjectsByNameRecursive(name, gameObject));
            }

            return result;
        }

        List<GameObject> GetGameObjectsByNameRecursive(string name, GameObject gameObject)
        {
            List<GameObject> result = new List<GameObject>();

            if (gameObject.Name == name)
                result.Add(gameObject);

            foreach (GameObject child in gameObject.Children)
                result.AddRange(GetGameObjectsByNameRecursive(name, child));

            return result;
        }
        #endregion
    }
}