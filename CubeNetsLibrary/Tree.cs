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

            for (int fa = 0; fa < numAngles; fa++) TryIterateCoord(1, fa);

            return bestLayout;

            void TryIterateCoord(int Coord, int Direction)
            {
                int CoordTo = Coord;
                int CoordFrom = parentNodes[Coord];

                coords[2 * CoordTo + 0] = coords[2 * CoordFrom + 0] + coordDiffs[2 * Direction + 0];
                coords[2 * CoordTo + 1] = coords[2 * CoordFrom + 1] + coordDiffs[2 * Direction + 1];

                for (int c = 0; c < CoordTo; c++)
                {
                    float xd = coords[2 * c + 0] - coords[2 * CoordTo + 0];
                    float yd = coords[2 * c + 1] - coords[2 * CoordTo + 1];
                    float dist2 = xd * xd + yd * yd;
                    if (dist2 < tooClose2) return;
                }

                if (Coord < numNodes - 1)
                {
                    for (int a = 0; a < numAngles; a++) TryIterateCoord(Coord + 1, a);
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
                float minX, maxX;
                float minY, maxY;
                minX = minY = float.MaxValue;
                maxX = maxY = float.MinValue;

                int a, b;
                float ax, ay, bx, by;
                float dx, dy;
                float score = 0f;

                for (a = 0; a < numNodes; a++) {
                    ax = coords[2 * a + 0];
                    ay = coords[2 * a + 1];

                    minX = Math.Min(minX, ax);
                    maxX = Math.Max(maxX, ax);
                    minY = Math.Min(minY, ay);
                    maxY = Math.Max(maxY, ay);

                    for (b = a + 1; b < numNodes; b++) {
                        bx = coords[2 * b + 0];
                        by = coords[2 * b + 1];

                        dx = ax - bx;
                        dy = ay - by;
                        score += dx * dx + dy * dy;
                    }

                }

                score += 1000f * (maxX - minX);

                if (score > bestLayoutScore) {
                    // If this is our best layout, let's centre it now
                    // while we have the min and max x and y coords
                    float xAdd = -0.5f * (minX + maxX);
                    float yAdd = -0.5f * (minY + maxY);

                    float height, totalheight = 0f;
                    float leftGrav, totalLeftGrav = 0f;
                    
                    for (a = 0; a < numNodes; a++) {
                        height = coords[2 * a + 1] - minY;
                        totalheight += height;

                        leftGrav = coords[2 * a + 0] - minX;
                        totalLeftGrav += leftGrav;

                        coords[2 * a + 0] += xAdd;
                        coords[2 * a + 1] += yAdd;
                    }

                    // Subtract from score since if more was added on after the test above,
                    // we might get false negatives.  (False positives aren't a problem
                    // as long as we don't leave the score artificially high, hence return 0
                    // if the test doesn't pass.)
                    score -= totalheight;
                    score -= 0.06125f * totalLeftGrav;
                    return score;
                }

                return 0;
            }
        }
    }
}
