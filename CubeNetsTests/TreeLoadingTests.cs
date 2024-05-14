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

        [Fact]
        public void CountTrees()
        {
            List<Tree> trees4 = reader.GetTrees(4);
            List<Tree> trees5 = reader.GetTrees(5);
            List<Tree> trees6 = reader.GetTrees(6);
            Assert.Equal(2, trees4.Count);
            Assert.Equal(3, trees5.Count);
            Assert.Equal(6, trees6.Count);
        }
    }
}