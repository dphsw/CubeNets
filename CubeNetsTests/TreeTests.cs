using CubeNetsLibrary;

namespace CubeNetsTests
{
    public class TreeTests
    {
        [Fact]
        public void TreeValidation()
        {
            Assert.Throws<Exception>(() => { new Tree([1, 1, 1]); });
            Assert.Throws<Exception>(() => { new Tree([0, 1, 5]); });
            _ = new Tree([0, 1, 2]);
            _ = new Tree([0, 0, 0]);
        }

        [Fact]
        public void FindEdgeBitmasks()
        {
            Tree linear = new Tree([0, 1, 2, 3]);
            Tree star = new Tree([0, 1, 1, 1]);

            int[] linearEdges = linear.EdgeBitMasks;
            int[] starEdges = star.EdgeBitMasks;

            Assert.Contains(0b0011, linearEdges);
            Assert.Contains(0b0110, linearEdges);
            Assert.Contains(0b1100, linearEdges);
            Assert.DoesNotContain(0b1010, linearEdges);
            Assert.DoesNotContain(0b0101, linearEdges);
            Assert.DoesNotContain(0b1001, linearEdges);
            Assert.DoesNotContain(0b0000, linearEdges);
            Assert.DoesNotContain(0b10000, linearEdges);
            Assert.Equal(3, linearEdges.Length);

            Assert.Contains(0b0011, starEdges);
            Assert.Contains(0b0101, starEdges);
            Assert.Contains(0b1001, starEdges);
            Assert.DoesNotContain(0b1010, starEdges);
            Assert.DoesNotContain(0b0110, starEdges);
            Assert.DoesNotContain(0b1100, starEdges);
            Assert.DoesNotContain(0b0000, starEdges);
            Assert.DoesNotContain(0b10000, starEdges);
            Assert.Equal(3, starEdges.Length);
        }
    }
}
