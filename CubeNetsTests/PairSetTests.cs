using CubeNetsLibrary;

namespace CubeNetsTests
{
    public class PairSetTests
    {
        [Fact]
        public void FindPairs()
        {
            PairSets pairSets = new PairSets(4, 2, true);
            List<int[]> sets = pairSets.GetPairSets();

            Assert.Contains([0b0101, 0b1010], sets);
            Assert.Contains([0b1001, 0b0110], sets);

            Assert.DoesNotContain([0b0101, 0b0101], sets);
            Assert.DoesNotContain([0b0101, 0b0110], sets);
        }

        [Fact]
        public void FindPairsForTree()
        {
            PairSets pairSets = new PairSets(6, 3, true);
            Tree linear = new Tree([0, 1, 2, 3, 4, 5]);

            List<int[]> sets = pairSets.GetPairSetsForTree(linear);

            Assert.Contains([0b001001, 0b010010, 0b100100], sets);
            Assert.Contains([0b001001, 0b100010, 0b010100], sets);// duplicate
            Assert.Contains([0b000101, 0b010010, 0b101000], sets);
            Assert.Contains([0b010001, 0b001010, 0b100100], sets);// duplicate
            Assert.Contains([0b100001, 0b001010, 0b010100], sets);

        }
    }
}
