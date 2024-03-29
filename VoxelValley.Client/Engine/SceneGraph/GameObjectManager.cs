using System;
using System.Collections.Generic;
using VoxelValley.Common.Diagnostics;

namespace VoxelValley.Client.Engine.SceneGraph
{
    public static class GameObjectManager
    {
        static Type type = typeof(GameObjectManager);
      public  static List<GameObject> GameObjects = new List<GameObject>();

        public static void AddToRoot(GameObject gameObject)
        {
            GameObjects.Add(gameObject);
        }

        #region OnUpdate & OnTick
        public static void OnUpdateOrTick(float deltaTime, bool isUpdate)
        {
            foreach (GameObject gameObject in GameObjects)
                OnUpdateOrTickChildren(gameObject, deltaTime, isUpdate);
        }

        static void OnUpdateOrTickChildren(GameObject gameObject, float deltaTime, bool isUpdate)
        {
            if (isUpdate)
                gameObject.Update(deltaTime);
            else
                gameObject.Tick(deltaTime);

            foreach (GameObject child in gameObject.Children)
                if (child.IsActive)
                {
                    if (isUpdate)
                        child.Update(deltaTime);
                    else
                        child.Tick(deltaTime);

                    OnUpdateOrTickChildren(child, deltaTime, isUpdate);
                }
        }
        #endregion

        #region  Scene Graph
        public static void DrawSceneGraph()
        {
            List<ConsoleTreeNode> rootNodes = new List<ConsoleTreeNode>();

            foreach (GameObject gameObject in GameObjects)
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