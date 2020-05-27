namespace VoxelValley.Client.Engine.Input
{
    public class State
    {
        public string KeyName;
        public string Name;
        public bool OnKeyDown;
        public System.Action<bool> Callback;
        public bool CurrentState;

        public State(string name, string key, bool onKeyDown)
        {
            Name = name;
            OnKeyDown = onKeyDown;
            KeyName = key;
            CurrentState = false;
        }
    }
}