
namespace VoxelValley.Client.Engine.Input
{
    public class Action
    {
        public string KeyName;
        public string Name;
        public bool Occlude;
        public bool OnKeyDown;
        public bool AllowRepeat;
        public System.Action Callback;

        public Action(string name, string key, bool onKeyDown, bool allowRepeat, bool occlude)
        {
            Name = name;
            OnKeyDown = onKeyDown;
            AllowRepeat = allowRepeat;
            Occlude = occlude;
            KeyName = key;
        }
    }
}