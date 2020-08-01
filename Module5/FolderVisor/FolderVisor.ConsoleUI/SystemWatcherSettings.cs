using System;
using System.Collections.Generic;
using System.Text;

namespace FolderVisor.ConsoleUI
{
    public class SystemWatcherSettings
    {
        public Dictionary<string, string> PatternToFolder { get; set; }
        public string[] FoldersToListen { get; set; }
        public string DefaultFilePath { get; set; }
        public bool ShouldAddId { get; set; }
        public bool ShouldAddCreationDate { get; set; }
    }
}
