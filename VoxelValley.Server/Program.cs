using System;

namespace VoxelValley.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            new Network.Server().Start();
        }
    }
}
