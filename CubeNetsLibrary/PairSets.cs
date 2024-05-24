using System.Collections.Generic;

namespace CubeNetsLibrary
{
    public class PairSets
    {
        readonly int numPositions;
        readonly int numPairs;
        readonly bool firstPairExcluded;

        int possiblePairs;
        int[] pairBitmasks;

        List<int[]> pairSets = null;

        public PairSets(int numPositions, int numPairs, bool excludeFirstPair)
        {
            // In all the tree data we've found, the first two nodes are always
            // next to each other and will never be a useful pair, so excluding
            // this from consideration will be useful for our purposes.

            this.numPositions = numPositions;
            this.numPairs = numPairs;
            firstPairExcluded = excludeFirstPair;
            SetPairs();
        }

        private void SetPairs()
        {
            possiblePairs = (numPositions * (numPositions - 1)) / 2;
            if (firstPairExcluded) possiblePairs--;

            pairBitmasks = new int[possiblePairs];

            int a, b, limit = 1 << (numPositions - 1);
            int pairs = 0;
            for (a = 1; a < limit; a <<= 1)
            {
                for (b = ((a == 1 && firstPairExcluded) ? 4 : (a << 1)); b <= limit; b <<= 1)
                {
                    pairBitmasks[pairs++] = a|b;
                }
            }

        }

        public List<int[]> GetPairSets() {
            if (pairSets == null) GeneratePairSets();
            return pairSets;
        }

        private void GeneratePairSets()
        {
            pairSets = new List<int[]>();
        }
    }
}
