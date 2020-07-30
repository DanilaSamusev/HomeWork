using System.Collections.Generic;

namespace FolderVisor.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {

            Dictionary<string, string> patternToFolder = new Dictionary<string, string>()
            {
                {".txt", "C:\\Users\\Danila_Samuseu\\Desktop\\HomeWork\\Module5\\Txt"},
                {".docx", "C:\\Users\\Danila_Samuseu\\Desktop\\HomeWork\\Module5\\Docx"},
            };

            SystemWatcher watcher = new SystemWatcher(patternToFolder);
            watcher.Run();
        }
    }
}
