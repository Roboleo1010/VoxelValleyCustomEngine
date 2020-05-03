using System.Collections.Generic;
using Newtonsoft.Json;
using OpenToolkit.Windowing.Common;
using OpenToolkit.Windowing.Common.Input;
using VoxelValley.Common.Helper;

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
        public Dictionary<Key, State> States;

        [JsonConstructor]
        public Context(int priority, bool isActive, List<Action> actions, List<State> states)
        {
            Priority = priority;
            IsActive = isActive;
            Actions = new Dictionary<Key, Action>();
            States = new Dictionary<Key, State>();

            if (actions != null)
                foreach (Action action in actions)
                    Actions.Add(InputHelper.GetKeyFromString(action.KeyName), action);

            if (states != null)
                foreach (State state in states)
                    States.Add(InputHelper.GetKeyFromString(state.KeyName), state);                    
        }

        public void HandleInputs(ref List<KeyboardKeyEventArgs> keyDownSinceLastUpdate, ref List<KeyboardKeyEventArgs> keyUpSinceLastUpdate)
        {
            //Key Down
            List<KeyboardKeyEventArgs> keyDownSinceLastUpdateCopy = new List<KeyboardKeyEventArgs>(keyDownSinceLastUpdate);

            foreach (KeyboardKeyEventArgs keyEvent in keyDownSinceLastUpdate)
            {
                if (Actions.TryGetValue(keyEvent.Key, out Action action) && action.OnKeyDown)   //Action
                {
                    if (!keyEvent.IsRepeat || (keyEvent.IsRepeat && action.AllowRepeat))
                        if (action.Callback != null)
                        {
                            action.Callback();
                            keyDownSinceLastUpdateCopy.Remove(keyEvent);
                        }
                }
                else if (States.TryGetValue(keyEvent.Key, out State state)) //State
                {
                    if (state.Callback != null && !state.CurrentState)
                    {
                        state.Callback(true);
                        state.CurrentState = true;
                        keyDownSinceLastUpdateCopy.Remove(keyEvent);
                    }
                }
            }

            keyDownSinceLastUpdate = keyDownSinceLastUpdateCopy;

            //Key Up
            List<KeyboardKeyEventArgs> keyUpSinceLastUpdateCopy = new List<KeyboardKeyEventArgs>(keyUpSinceLastUpdate);

            foreach (KeyboardKeyEventArgs keyEvent in keyUpSinceLastUpdate)
            {
                if (Actions.TryGetValue(keyEvent.Key, out Action action) && !action.OnKeyDown)   //Action
                {
                    if (action.Callback != null)
                    {
                        action.Callback();
                        keyUpSinceLastUpdateCopy.Remove(keyEvent);
                    }
                }
                else if (States.TryGetValue(keyEvent.Key, out State state)) //State
                {
                    if (state.Callback != null && state.CurrentState)
                    {
                        state.Callback(false);
                        state.CurrentState = false;

                        keyUpSinceLastUpdateCopy.Remove(keyEvent);
                    }
                }
            }

            keyUpSinceLastUpdate = keyUpSinceLastUpdateCopy;
        }
    }
}