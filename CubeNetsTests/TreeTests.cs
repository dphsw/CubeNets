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
    }
}
