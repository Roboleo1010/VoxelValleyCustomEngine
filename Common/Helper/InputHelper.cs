using System;
using OpenToolkit.Windowing.Common.Input;
using VoxelValley.Common.Diagnostics;

namespace VoxelValley.Common.Helper
{
    public static class InputHelper
    {
        static Type type = typeof(InputHelper);

        public static Key GetKeyFromString(string keyName)
        {
            if (Enum.TryParse(typeof(Key), keyName, true, out object result))
                return (Key)result;

            Log.Warn(type, $"Can't parse Key '{keyName}' to Key enum.");
            return Key.Unknown;
        }
    }
}