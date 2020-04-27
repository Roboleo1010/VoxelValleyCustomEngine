using System;
using System.Collections.Generic;
using System.Linq;
using OpenToolkit.Windowing.Common;
using OpenToolkit.Windowing.Common.Input;
using VoxelValley.Common.Diagnostics;

namespace VoxelValley.Client.Engine.Input
{
    public static class InputManager
    {
        public static Dictionary<string, Context> Contexts = new Dictionary<string, Context>();

        static Type type = typeof(InputManager);
        private static List<KeyboardKeyEventArgs> keyUpSinceLastUpdate = new List<KeyboardKeyEventArgs>();
        private static List<KeyboardKeyEventArgs> keyDownSinceLastUpdate = new List<KeyboardKeyEventArgs>();

        static InputManager()
        {
            Context c = new Context(200, true);
            c.Actions.Add(Key.W, new Action("Walk_Forward", false, false, true));

            Contexts.Add("Main_Game", c);
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
            List<Context> activeContexts = Contexts.Values.Where(c => c.IsActive).ToList();

            foreach (Context context in activeContexts)
                context.HandleInputs(ref keyDownSinceLastUpdate, ref keyUpSinceLastUpdate);

            keyDownSinceLastUpdate.Clear();
            keyUpSinceLastUpdate.Clear();
        }

        #region  Context Management
        public static void ActivateContext(Context context)
        {
            // activeContexts.Add(context);

            //TODO: Sort active contexts

        }

        public static void DeactivateContext(Context context)
        {
            // activeContexts.Remove(context);
        }
        #endregion

        public static Action GetAction(string contextName, string actionName)
        {
            if (Contexts.TryGetValue(contextName, out Context context))
            {
                Action action = context.Actions.Values.Where(a => a.Name == actionName).FirstOrDefault();
                if (action != null)
                    return action;
            }
            Log.Warn(type, $"Can't get Action {contextName}/{actionName}");
            return null;
        }
    }
}