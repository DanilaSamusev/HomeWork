using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FolderVisor.ConsoleUI
{
    public class SystemWatcher
    {
        public Dictionary<string, string> PatternToFolder { get; set; }
        public string DefaultFilePath { get; set; }
        public bool ShouldAddId { get; set; }
        public bool ShouldAddCreationDate { get; set; }
        public int CurrentId { get; set; }

        public SystemWatcher(Dictionary<string, string> patternToFolder)
        {
            PatternToFolder = patternToFolder;
            DefaultFilePath = "C:\\Users\\Danila_Samuseu\\Desktop\\HomeWork\\Module5\\Default";
            ShouldAddId = true;
            ShouldAddCreationDate = true;
            CurrentId = 1;
        }

        public void Run()
        {
            string[] folderPaths = { "C:\\Users\\Danila_Samuseu\\Desktop\\HomeWork\\Module5", "C:\\Users\\Danila_Samuseu\\Desktop\\HomeWork\\Module4" };

            foreach (var folderPath in folderPaths)
            {
                Watch(folderPath);
            }

            while (true) ;
        }

        public void Watch(string path)
        {
            var watcher = new FileSystemWatcher();

            watcher.Path = path;
            watcher.NotifyFilter = NotifyFilters.LastWrite
                                   | NotifyFilters.FileName;
            watcher.Filter = "";
            watcher.Created += HandleFileCreated;
            watcher.EnableRaisingEvents = true;
        }

        private void HandleFileCreated(object source, FileSystemEventArgs e)
        {
            Log($"New file {e.FullPath} detected. Creation date: {DateTime.Now}");
            MoveFile(e.FullPath);
        }

        private void Log(string message)
        {
            Console.WriteLine(message);
        }

        private void MoveFile(string filePath)
        {
            string fileName = GetFileNameByFullPath(filePath);

            string creationDate = ShouldAddCreationDate ? $"{DateTime.Now:dd.MM.yyyy}" : "";
            string id = ShouldAddId ? $"№{CurrentId}" : "";

            foreach (var keyValue in PatternToFolder)
            {
                if (filePath.Contains(keyValue.Key))
                {
                    Log($"{filePath} mathes the patter '{keyValue.Key}'!");
                    string destination = $"{keyValue.Value}\\{creationDate}{id}{fileName}";
                    File.Move(filePath, destination);
                    Log($"{filePath} was moved to {destination}");
                    return;
                }
            }

            Log($"{filePath} not matches any patterns!");
            File.Move(filePath, $"{DefaultFilePath}\\{fileName}");
            Log($"{filePath} was moved to default folder {DefaultFilePath}");
            CurrentId++;
        }

        private string GetFileNameByFullPath(string path)
        {
            string[] splitPath = path.Split("\\");

            return splitPath.Last();
        }
    }
}
