
namespace VoxelValley.Client.Engine.Input
{
    public class Action
    {
        public string KeyName;
        public string Name;
        public bool OnKeyDown;
        public bool AllowRepeat;
        public System.Action Callback;

        public Action(string name, string key, bool onKeyDown, bool allowRepeat)
        {
            Name = name;
            OnKeyDown = onKeyDown;
            AllowRepeat = allowRepeat;
            KeyName = key;
        }
    }
}