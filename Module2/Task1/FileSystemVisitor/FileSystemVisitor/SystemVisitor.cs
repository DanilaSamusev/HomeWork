using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSystemVisitor
{
    public class SystemVisitor
    {
        private readonly Filter _filter;
        private IEnumerable<Node> _nodes;
        private readonly string _pattern;
        private readonly bool _shouldStop;

        public delegate void SearchStartEventHandler(string message);
        public delegate void SearchFinishEventHandler(string message);
        public delegate void FileFoundEventHandler(string message);
        public delegate void FolderFoundEventHandler(string message);
        public delegate void FileFilteredEventHandler(string message);
        public delegate void FolderFilteredEventHandler(string message);
        public delegate bool Filter(Node node, string pattern);

        public event SearchStartEventHandler NotifySearchStart;
        public event SearchFinishEventHandler NotifySearchFinish;
        public event FileFoundEventHandler NotifyFileFound;
        public event FolderFoundEventHandler NotifyFolderFound;
        public event FileFilteredEventHandler NotifyFileFiltered;
        public event FolderFilteredEventHandler NotifyFolderFiltered;

        public SystemVisitor(bool shouldStop)
        {
            _shouldStop = shouldStop;
        }

        public SystemVisitor(Filter filter, string pattern, bool shouldStop)
        {
            _filter = filter;
            _pattern = pattern;
            _shouldStop = shouldStop;
        }

        public IEnumerable<Node> StartSearch(string folderPath)
        {
            NotifySearchStart("Searching started...");

            _nodes = GetAllNodes(folderPath);

            NotifySearchFinish("Searching finished");

            if (_filter != null)
            {
                _nodes = FilterNodes().ToList();
            }

            return _nodes;
        }

        public List<Node> GetAllNodes(string folderPath)
        {
            List<Node> nodes = new List<Node>();

            string[] folderPaths = Directory.GetDirectories(folderPath);
            string[] filePaths = Directory.GetFiles(folderPath);

            var fileNodes = GetFileNodes(filePaths);
            var folderNodes = GetFolderNodes(folderPaths);

            nodes.AddRange(fileNodes);
            nodes.AddRange(folderNodes);

            foreach (var path in folderPaths)
            {
                nodes.AddRange(GetAllNodes(path));
            }

            return nodes;
        }

        private List<Node> GetFileNodes(string[] filePaths)
        {
            var nodes = new List<Node>();

            foreach (var path in filePaths)
            {
                var node = new Node
                {
                    Path = path,
                    Type = NodeType.File
                };

                NotifyFileFound($"Found file {path}");

                nodes.Add(node);
            }

            return nodes;
        }

        private List<Node> GetFolderNodes(string[] folderPaths)
        {
            var nodes = new List<Node>();

            foreach (var path in folderPaths)
            {
                var node = new Node
                {
                    Path = path,
                    Type = NodeType.Folder
                };

                NotifyFolderFound($"Found folder {path}");

                nodes.Add(node);
            }

            return nodes;
        }

        private IEnumerable<Node> FilterNodes()
        {
            foreach (var node in _nodes)
            {
                if (_filter(node, _pattern))
                {
                    if (node.Type == NodeType.File)
                    {
                        NotifyFileFiltered($"Filtered file {node.Path}");
                    }

                    if (node.Type == NodeType.Folder)
                    {
                        NotifyFolderFiltered($"Filtered folder {node.Path}");
                    }

                    yield return node;

                    if (_shouldStop)
                    {
                        yield break;
                    }
                }
            }
        }
    }
}
