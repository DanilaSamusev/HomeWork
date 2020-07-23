using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystemVisitor.Tests
{
    [TestClass]
    class SystemVisitorTest
    {

        private string[] _filePaths;
        private string[] _folderPaths;

        [TestInitialize]
        public void SetUp()
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


        public void GetAllNodesTests()
        {

        }

    }
}
