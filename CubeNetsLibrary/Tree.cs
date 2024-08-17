using System;
using System.Collections.Generic;
using System.Linq;

namespace CubeNetsLibrary
{
    public class Tree
    {
        readonly int numNodes;
        readonly int[] parentNodes;

        int[] edgeBitMasks = null;
        public int[] EdgeBitMasks { get {
                if (edgeBitMasks == null) CreateEdgeBitmasks();
                return edgeBitMasks; 
            } }

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

        private void CreateEdgeBitmasks() { 
            edgeBitMasks = new int[numNodes - 1];
            for (int n = 1; n < numNodes; n++)
            {
                edgeBitMasks[n - 1] = (1 << n) | (1 << parentNodes[n]);
            }
        }

        public float[] FindGraphCoords()
        {
            int numCoords = 2 * numNodes;
            float[] coords = new float[numCoords];

            float[] bestLayout = new float[numCoords];
            float bestLayoutScore = -1;

            float tooClose2 = 0.25f;

            int numAngles = 8;
            float[] coordDiffs = new float[2 * numAngles];
            double angle;
            for (int a = 0; a < numAngles; a++)
            {
                angle = 2.0 * Math.PI * ((double)a / numAngles);
                coordDiffs[2 * a + 0] = (float)Math.Cos(angle);
                coordDiffs[2 * a + 1] = (float)Math.Sin(angle);
            }

            for (int fa = 0; fa < numAngles; fa++) TryIterateCoord(0, fa);

            void TryIterateCoord(int CoordFrom, int Direction)
            {
                int CoordTo = CoordFrom + 1;

                coords[2 * CoordTo + 0] = coords[2 * CoordFrom + 0] + coordDiffs[2 * Direction + 0];
                coords[2 * CoordTo + 1] = coords[2 * CoordFrom + 1] + coordDiffs[2 * Direction + 1];

                for (int c = 0; c < CoordTo; c++)
                {
                    float xd = coords[2 * c + 0] - coords[2 * CoordTo + 0];
                    float yd = coords[2 * c + 1] - coords[2 * CoordTo + 1];
                    float dist2 = xd * xd + yd * yd;
                    if (dist2 < tooClose2) return;
                }

                if (CoordTo < numNodes - 1)
                {
                    for (int a = 0; a < numAngles; a++) TryIterateCoord(CoordTo, a);
                } 
                else
                {
                    // We now have a layout where things aren't too close
                    float layoutScore = GetLayoutScore();
                    if (layoutScore > bestLayoutScore)
                    {
                        bestLayoutScore = layoutScore;
                        for (int c = 0; c < numCoords; c++) bestLayout[c] = coords[c];
                    }
                }
            }

            float GetLayoutScore()
            {
                // Get layout of current set of coords
                // NYI
                return 1f;
            }
        }
    }
}
