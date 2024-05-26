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

            if (numPairs * 2 > numPositions) return; // Wouldn't be possible to have any sets of pairs

            int[] pairs = new int[numPairs];
            int[] loop = new int[numPairs];
            int[] cumulativeBitmask = new int[numPairs];
            int depth = 0;
            int firstLoopEnd = firstPairExcluded ? (numPositions - 2) : (numPositions - 1);
            int bitsUsed, bits, temp;
            bool endOfLoop;

            while (loop[0] <= firstLoopEnd)
            {
                endOfLoop = loop[depth] >= possiblePairs;
                
                if (!endOfLoop) {
                    bitsUsed = (depth == 0) ? 0 : cumulativeBitmask[depth - 1];
                    temp = loop[depth];
                    do
                    {
                        bits = pairBitmasks[temp];
                        temp++;
                    } while ((bits & bitsUsed) != 0 && temp < possiblePairs);

                    endOfLoop = (bits & bitsUsed) != 0;
                    
                    if (!endOfLoop)
                    {
                        loop[depth] = temp - 1;
                        pairs[depth] = bits;
                        cumulativeBitmask[depth] = bits | bitsUsed;

                        if (depth < numPairs - 1)
                        {
                            depth++;
                            loop[depth] = loop[depth - 1] + 1;
                        }
                        else
                        {
                            int[] clone = new int[numPairs];
                            for (temp = 0; temp < numPairs; temp++) clone[temp] = pairs[temp];
                            pairSets.Add(clone);

                            loop[depth]++;
                        }

                    }
                }

                if(endOfLoop)
                {
                    depth--;
                    loop[depth]++;
                }

            }

        }
    }
}
