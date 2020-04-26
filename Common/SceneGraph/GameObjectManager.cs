using System;
using System.Collections.Generic;
using VoxelValley.Client.Engine.Graphics;
using VoxelValley.Common.SceneGraph.Components;
using VoxelValley.Common.Diagnostics;

namespace VoxelValley.Common.SceneGraph
{
    public static class GameObjectManager
    {
        static Type type = typeof(GameObjectManager);
        static List<GameObject> gameObjects = new List<GameObject>();

        public static void AddToRoot(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }

        #region OnUpdate & OnTick
        public static void OnUpdate(float deltaTime)
        {
            foreach (GameObject gameObject in gameObjects)
                OnUpdateChildren(gameObject, deltaTime);
        }

        static void OnUpdateChildren(GameObject gameObject, float deltaTime)
        {
            gameObject.Update(deltaTime);

            foreach (GameObject child in gameObject.Children)
                if (child.IsActive)
                {
                    child.Update(deltaTime);
                    OnUpdateChildren(child, deltaTime);
                }
        }

        public static void OnTick(float deltaTime)
        {
            foreach (GameObject gameObject in gameObjects)
                OnTickChildren(gameObject, deltaTime);
        }

        static void OnTickChildren(GameObject gameObject, float deltaTime)
        {
            gameObject.Tick(deltaTime);

            foreach (GameObject child in gameObject.Children)
                if (child.IsActive)
                {
                    child.Tick(deltaTime);
                    OnTickChildren(child, deltaTime);
                }
        }
        #endregion

        #region  GetMeshes
        internal static Mesh[] GetMeshes()
        {
            List<Mesh> meshes = new List<Mesh>();

            foreach (GameObject gameObject in gameObjects)
                meshes.AddRange(GetChildMehes(gameObject));

            return meshes.ToArray();
        }

        static List<Mesh> GetChildMehes(GameObject gameObject)
        {
            List<Mesh> meshes = new List<Mesh>();

            MeshRenderer meshRendererParent = gameObject.GetComponent<MeshRenderer>();
            if (meshRendererParent != null && meshRendererParent.IsActive && meshRendererParent.Mesh != null)
                meshes.Add(meshRendererParent.Mesh);


            foreach (GameObject child in gameObject.Children)
                if (child.IsActive)
                {
                    MeshRenderer meshRendererChild = child.GetComponent<MeshRenderer>();
                    if (meshRendererChild != null && meshRendererChild.IsActive && meshRendererChild.Mesh != null)
                        meshes.Add(meshRendererChild.Mesh);

                    meshes.AddRange(GetChildMehes(child));
                }

            return meshes;
        }
        #endregion

        #region  Scene Graph
        public static void DrawSceneGraph()
        {
            List<ConsoleTreeNode> rootNodes = new List<ConsoleTreeNode>();

            foreach (GameObject gameObject in gameObjects)
                rootNodes.Add(new ConsoleTreeNode(gameObject.Name, GetChildNodes(gameObject)));

            ConsoleTree.PrintTree(rootNodes);
        }

        static List<ConsoleTreeNode> GetChildNodes(GameObject start)
        {
            List<ConsoleTreeNode> rootNodes = new List<ConsoleTreeNode>();

            foreach (GameObject child in start.Children)
                rootNodes.Add(new ConsoleTreeNode(child.Name, GetChildNodes(child)));

            return rootNodes;
        }

        public static void SceneGraphUpdated()
        {

        }
        #endregion
    }
}