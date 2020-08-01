using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using FolderVisor.ConsoleUI.Resources;

namespace FolderVisor.ConsoleUI
{
    class Program
    {
        private static IConfiguration _configuration;

        public static void Main(string[] args)
        {
            Configure();
            SetCultureInfo();

            SystemWatcherSettings settings = GetSettings();
            SystemWatcher watcher = new SystemWatcher(settings);

            watcher.Run();
        }

        public static void Configure()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json");

            _configuration = builder.Build();
        }

        public static SystemWatcherSettings GetSettings()
        {
            Dictionary<string, string> patternToFolders = _configuration.GetSection("PatternToFolder")
                .GetChildren()
                .ToDictionary(x => x.Key, x => x.Value);

            string[] foldersToListen = _configuration.GetSection("FoldersToListen")
                .GetChildren()
                .Select(x => x.Value)
                .ToArray();

            string defaultFilePath = _configuration.GetSection("DefaultFilePath").Value;
            bool shouldAddId = bool.Parse(_configuration.GetSection("ShouldAddId").Value);
            bool shouldAddCreationDate = bool.Parse(_configuration.GetSection("ShouldAddCreationDate").Value);

            return new SystemWatcherSettings()
            {
                PatternToFolder = patternToFolders,
                FoldersToListen = foldersToListen,
                DefaultFilePath = defaultFilePath,
                ShouldAddId = shouldAddId,
                ShouldAddCreationDate = shouldAddCreationDate,
            };
        }

        public static void SetCultureInfo()
        {
            string cultureInfo = _configuration.GetSection("Language").Value;
            Local.Culture = new CultureInfo(cultureInfo);
        }
    }
}
