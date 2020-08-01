using System;
using System.IO;

using System.Linq;
using FolderVisor.ConsoleUI.Resources;

namespace FolderVisor.ConsoleUI
{
    public class SystemWatcher
    {
        private readonly SystemWatcherSettings _settings;
        private int _currentId = 1;
        private const string FolderSeparator = "\\";

        public SystemWatcher(SystemWatcherSettings settings)
        {
            _settings = settings;
        }

        public void Run()
        {
            foreach (var folderPath in _settings.FoldersToListen)
            {
                Watch(folderPath);
            }

            while (true) ;
        }

        public void Watch(string path)
        {
            var watcher = new FileSystemWatcher
            {
                Path = path,
                NotifyFilter = NotifyFilters.LastWrite
                               | NotifyFilters.FileName,
                Filter = string.Empty
            };

            watcher.Created += HandleFileCreated;
            watcher.EnableRaisingEvents = true;
        }

        private void HandleFileCreated(object source, FileSystemEventArgs e)
        {
            MoveFile(e.FullPath);
        }

        private void Log(string message)
        {
            Console.WriteLine(message);
        }

        private void MoveFile(string filePath)
        {
            Log($"{filePath} {Local.NewEnteryDetected} {DateTime.Now:dd.MM.yyyy}");
            string fileName = GetFileNameByFullPath(filePath);

            string creationDate = _settings.ShouldAddCreationDate ? $"{DateTime.Now:dd.MM.yyyy}" : string.Empty;
            string id = _settings.ShouldAddId ? $"№{_currentId}" : string.Empty;

            foreach (var keyValue in _settings.PatternToFolder)
            {
                if (filePath.Contains(keyValue.Key))
                {
                    Log($"{filePath} {Local.MatchesPattern} '{keyValue.Key}'!");
                    string destination = $"{keyValue.Value}\\{creationDate}{id}{fileName}";
                    File.Move(filePath, destination);
                    Log($"{filePath} {Local.MovedTo} {destination}");
                    _currentId++;
                    return;
                }
            }

            Log($"{filePath} {Local.NotMatchesAnyPattern}");
            File.Move(filePath, $"{_settings.DefaultFilePath}\\{fileName}");
            Log($"{filePath} {Local.MovedToDefaultFolder} {_settings.DefaultFilePath}");
            _currentId++;
        }

        private string GetFileNameByFullPath(string path)
        {
            string[] splitPath = path.Split(FolderSeparator);

            return splitPath.Last();
        }
    }
}
