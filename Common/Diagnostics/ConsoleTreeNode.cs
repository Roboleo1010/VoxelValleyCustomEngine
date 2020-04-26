using System.Collections.Generic;

namespace VoxelValley.Common.Diagnostics
{
    public class ConsoleTreeNode
    {
        public string Name { get; set; }
        public List<ConsoleTreeNode> Children { get; }

        public ConsoleTreeNode(string name)
        {
            Name = name;
            Children = new List<ConsoleTreeNode>();
        }

        public ConsoleTreeNode(string name, List<ConsoleTreeNode> children)
        {
            Name = name;
            Children = children; ;
        }
    }
}