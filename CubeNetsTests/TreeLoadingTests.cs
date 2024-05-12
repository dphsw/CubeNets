using CubeNetsLibrary;

namespace CubeNetsTests
{
    public class TreeLoadingTests
    {
        TreeDataReader reader = new TreeDataReader();

        [Fact]
        public void DetectFiles()
        {
            Assert.False(reader.TreeDataAvailable(2));
            Assert.True(reader.TreeDataAvailable(4));
            Assert.True(reader.TreeDataAvailable(10));
            Assert.True(reader.TreeDataAvailable(12));
            Assert.False(reader.TreeDataAvailable(40));
        }
    }
}