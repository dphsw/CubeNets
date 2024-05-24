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
    }
}
