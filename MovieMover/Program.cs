using System;
using System.Configuration;
using System.IO;

using TMDbLib.Client;
using TMDbLib.Objects.Search;

namespace MovieMover
{
    class Program
    {
        static void Main(string[] args)
        {
            var sourceFolder = args[0].EndsWith("\\") ? args[0] : args[0] + "\\";
            var destinationFolder = args[1].EndsWith("\\") ? args[1] : args[1] + "\\";
            var tmdbApiKey = ConfigurationManager.AppSettings["apiKey"];

            Logger.LogInfo("Source folder: {0}", sourceFolder);
            Logger.LogInfo("Target folder: {0}", destinationFolder);

            foreach (var folder in Directory.GetDirectories(sourceFolder))
            {
                Logger.LogInfo("Examining {0}", folder);
                if (FileHelper.IsAnyFileLocked(folder))
                {
                    Logger.LogInfo("Bypassing {0}, due to a file being locked", folder);
                }
                else
                {
                    MoveFolder(folder, destinationFolder, tmdbApiKey);   
                }
            }

            Console.ReadKey();
        }

        public static void MoveFolder(string source, string destination, string apiKey)
        {
            var folderName = new DirectoryInfo(source).Name;
            var client = new TMDbClient(apiKey);
            var results = client.SearchMovie(folderName);

            Logger.LogInfo($"Got back {results.TotalResults:N0} results");

            if (results.Results.Count > 0)
            {
                var result = results.Results[0]; // use the first
                var targetFolder = String.Format("{0}{1} ({2:yyyy})",
                    destination,
                    FileHelper.RemoveIllegalFolderCharacters(result.Title),
                    result.ReleaseDate);

                Logger.LogInfo("Moving from {0} to {1}...", source, targetFolder);
                FileHelper.MoveFolder(source, targetFolder);
                Logger.LogInfo("Move from {0} to {1}...Complete", source, targetFolder);
            }
        }
    }
}