using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using OpenToolkit.Windowing.Common;
using VoxelValley.Common.Diagnostics;
using VoxelValley.Engine.Helper;

namespace VoxelValley.Client.Engine.Input
{
    public static class InputManager
    {
        static Type type = typeof(InputManager);
        static Dictionary<string, Context> contexts = new Dictionary<string, Context>();
        private static List<KeyboardKeyEventArgs> keyUpSinceLastUpdate = new List<KeyboardKeyEventArgs>();
        private static List<KeyboardKeyEventArgs> keyDownSinceLastUpdate = new List<KeyboardKeyEventArgs>();

        public static void LoadContexts()
        {
            string[] paths = FileHelper.GetAllFilesOfType("Assets/Input", "*.json");

            foreach (string path in paths)
                LoadContext(path);
        }

        static void LoadContext(string path)
        {
            Context context;

            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                context = JsonConvert.DeserializeObject<Context>(reader.ReadToEnd());
            }

            contexts.Add(FileHelper.GetFileNameWithoutExtention(path), context);
        }

        public static void OnKeyDown(KeyboardKeyEventArgs e)
        {
            keyDownSinceLastUpdate.Add(e);
        }

        public static void OnKeyUp(KeyboardKeyEventArgs e)
        {
            keyUpSinceLastUpdate.Add(e);
        }

        public static void HandleInput()
        {
            List<Context> activeContexts = contexts.Values.Where(c => c.IsActive).OrderByDescending(c => c.Priority).ToList();

            foreach (Context context in activeContexts)
                context.HandleInputs(ref keyDownSinceLastUpdate, ref keyUpSinceLastUpdate);

            keyDownSinceLastUpdate.Clear();
            keyUpSinceLastUpdate.Clear();
        }

        #region  Context Management
        public static void ActivateContext(string name)
        {
            if (contexts.TryGetValue(name, out Context context))
                context.IsActive = true;
            else
                Log.Warn(type, $"Can't activate Context {name}");
        }

        public static void DeactivateContext(string name)
        {
            if (contexts.TryGetValue(name, out Context context))
                context.IsActive = false;
            else
                Log.Warn(type, $"Can't activate Context {name}");
        }
        #endregion

        public static Action GetAction(string contextName, string actionName)
        {
            if (contexts.TryGetValue(contextName, out Context context))
            {
                Action action = context.Actions.Values.Where(a => a.Name == actionName).FirstOrDefault();
                if (action != null)
                    return action;
            }
            Log.Warn(type, $"Can't get Action {contextName}/{actionName}");
            return null;
        }

        public static State GetState(string contextName, string stateName)
        {
            if (contexts.TryGetValue(contextName, out Context context))
            {
                State state = context.States.Values.Where(s => s.Name == stateName).FirstOrDefault();
                if (state != null)
                    return state;
            }
            Log.Warn(type, $"Can't get State {contextName}/{stateName}");
            return null;
        }
    }
}