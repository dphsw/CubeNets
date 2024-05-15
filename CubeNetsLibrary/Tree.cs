using System;
using System.Collections.Generic;
using System.Linq;

namespace CubeNetsLibrary
{
    public class Tree
    {
        readonly int numNodes;
        readonly int[] parentNodes;

        public Tree(List<int> parentNodes)
        {
            numNodes = parentNodes.Count;
            this.parentNodes = parentNodes.Select(n => n-1).ToArray();
            ValidateTree();
        }

        private void ValidateTree()
        {
            if (parentNodes.Length != numNodes) throw new Exception("Tree has wrong number of parent nodes");
            for (int n = 0; n < numNodes; n++) if (parentNodes[n] >= n) throw new Exception($"Tree node {n} has parent {parentNodes[n]}; It must have a parent that has already been created.");
        }
    }
}
