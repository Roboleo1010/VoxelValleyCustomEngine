using System;
using System.Collections.Generic;

namespace VoxelValley.Common.Diagnostics
{
    public static class ConsoleTree
    {
        private static Type type = typeof(ConsoleTree);
        private static string cross = " ├─";
        private static string corner = " └─";
        private static string vertical = " │ ";
        private static string space = "   ";

        public static void PrintTree(List<ConsoleTreeNode> topLevelNodes)
        {
            foreach (var node in topLevelNodes)
                PrintNode(node, indent: "");
        }

        static void PrintNode(ConsoleTreeNode node, string indent)
        {
            Console.WriteLine(node.Name);

            // Loop through the children recursively.
            int numberOfChildren = node.Children.Count;
            for (int i = 0; i < numberOfChildren; i++)
                PrintChildNode(node.Children[i], indent, (i == (numberOfChildren - 1)));
        }

        static void PrintChildNode(ConsoleTreeNode node, string indent, bool isLast)
        {
            // Print the provided pipes/spaces indent
            Console.Write(indent);

            // Depending if this node is a last child, print the
            // corner or cross, and calculate the indent that will
            // be passed to its children
            if (isLast)
            {
                Console.Write(corner);
                indent += space;
            }
            else
            {
                Console.Write(cross);
                indent += vertical;
            }

            PrintNode(node, indent);
        }
    }
}