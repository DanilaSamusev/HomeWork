using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FileSystemVisitor.UnitTests
{
    public class SystemVisitorTest
    {

        private string[] _filePaths;
        private string[] _folderPaths;

        public SystemVisitorTest()
        {
            _filePaths = new[]
            {
                "root\\file.txt",
                "root\\file2.exe",
                "root\\data\\README.txt",
            };

            _folderPaths = new[]
            {
                "root\\data",
                "root\\files",
                "root\\bin",
            };
        }

        [Fact]
        public void GetFileNodesTest()
        {
            // Arrange
            SystemVisitor visitor = new SystemVisitor(false);

            // Act
            List<Node> fileNodes = visitor.GetFileNodes(_filePaths).ToList();

            // Assert
            Assert.Equal(3, fileNodes.Count());
            Assert.Equal("root\\data\\README.txt", fileNodes[2].Path);
            Assert.Equal(NodeType.File, fileNodes[0].Type);
        }

        [Fact]
        public void GetFolderNodesTest()
        {
            // Arrange
            SystemVisitor visitor = new SystemVisitor(false);

            // Act
            List<Node> fileNodes = visitor.GetFolderNodes(_folderPaths).ToList();

            // Assert
            Assert.Equal(3, fileNodes.Count());
            Assert.Equal("root\\data", fileNodes[0].Path);
            Assert.Equal(NodeType.Folder, fileNodes[1].Type);
        }

        [Fact]
        public void FilterNodesWithStopTest()
        {
            // Arrange
            SystemVisitor visitor = new SystemVisitor(
                (node, pattern) => node.Path.Contains(pattern),
                "txt",
                true);
            visitor.Nodes = visitor.GetFileNodes(_filePaths).ToList();

            // Act
            List<Node> nodes = visitor.FilterNodes().ToList();

            // Assert
            Assert.Single(nodes);
        }

        [Fact]
        public void FilterNodesWithoutStopTest()
        {
            // Arrange
            SystemVisitor visitor = new SystemVisitor(
                (node, pattern) => node.Path.Contains(pattern),
                "txt",
                false);
            visitor.Nodes = visitor.GetFileNodes(_filePaths).ToList();

            // Act
            List<Node> nodes = visitor.FilterNodes().ToList();

            // Assert
            Assert.Equal(2, nodes.Count);
        }
    }
}
