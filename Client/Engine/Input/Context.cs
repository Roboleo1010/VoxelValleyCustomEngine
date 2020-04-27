using System.Collections.Generic;
using Newtonsoft.Json;
using OpenToolkit.Windowing.Common;
using OpenToolkit.Windowing.Common.Input;

namespace VoxelValley.Client.Engine.Input
{
    /// <summary>
    /// Represents a control Map. For Example: Main Game, Inventory, Main Menu, Crafting Menu
    /// </summary>
    public class Context
    {
        public bool IsActive;
        public int Priority;
        public Dictionary<Key, Action> Actions;

        [JsonConstructor]
        public Context(int priority, bool isActive)
        {
            Priority = priority;
            IsActive = isActive;
            Actions = new Dictionary<Key, Action>();
        }

        public void HandleInputs(ref List<KeyboardKeyEventArgs> keyDownSinceLastUpdate, ref List<KeyboardKeyEventArgs> keyUpSinceLastUpdate)
        {
            List<KeyboardKeyEventArgs> keyDownSinceLastUpdateCopy = new List<KeyboardKeyEventArgs>(keyDownSinceLastUpdate);

            foreach (KeyboardKeyEventArgs keyEvent in keyDownSinceLastUpdate)
                if (Actions.TryGetValue(keyEvent.Key, out Action action) && action.OnKeyDown) //Is valid Key & Matches key down
                    if (!keyEvent.IsRepeat || (keyEvent.IsRepeat && action.AllowRepeat)) //Is not repeat or is allowed repeat
                        if (action.Callback != null) //Callback set
                        {
                            action.Callback();

                            if (action.Occlude)
                                keyDownSinceLastUpdateCopy.Remove(keyEvent);
                        }

            keyDownSinceLastUpdate = keyDownSinceLastUpdateCopy;

            List<KeyboardKeyEventArgs> keyUpSinceLastUpdateCopy = new List<KeyboardKeyEventArgs>(keyUpSinceLastUpdate);

            foreach (KeyboardKeyEventArgs keyEvent in keyUpSinceLastUpdate)
                if (Actions.TryGetValue(keyEvent.Key, out Action action) && !action.OnKeyDown) //Is valid Key & Matches key up
                        if (action.Callback != null) //Callback set
                        {
                            action.Callback();

                            if (action.Occlude)
                                keyUpSinceLastUpdateCopy.Remove(keyEvent);
                        }

            keyUpSinceLastUpdate = keyUpSinceLastUpdateCopy;
        }
    }
}