using System;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;

using TMDbLib.Client;

namespace MovieMover
{
    class Program
    {
        static void Main(string[] args)
        {
            var parameters = new Parameters()
            {
                SourceFolder = args[0].EndsWith("\\") ? args[0] : args[0] + "\\",
                DestinationFolder = args[1].EndsWith("\\") ? args[1] : args[1] + "\\",
                ApiKey = args.Length > 2 ? args[2] : ConfigurationManager.AppSettings["apiKey"] 
            };

            if (!Directory.Exists(parameters.SourceFolder))
            {
                Logger.LogInfo("Folder {0}, does not exist - exiting", parameters.SourceFolder);
                return;
            }


            if (!Directory.Exists(parameters.DestinationFolder))
            {
                Logger.LogInfo("Folder {0}, does not exist - exiting", parameters.DestinationFolder);
                return;
            }

            if (String.IsNullOrEmpty(parameters.ApiKey) || parameters.ApiKey == "AddYourApiKeyHere")
            {
                Logger.LogInfo("Could not find a valid TMDB Api Key, please add to the commandline, or the config file");
                return;
            }
            
            Logger.LogInfo("Launch Parameters:\r\n{0}", parameters);

            foreach (var folder in Directory.GetDirectories(parameters.SourceFolder))
            {
                Logger.LogInfo("Examining {0}", folder);
                if (FileHelper.IsAnyFileLocked(folder))
                {
                    Logger.LogInfo("Bypassing {0}, due to a file being locked", folder);
                }
                else
                {
                    try
                    {
                        LookupDetailsAndMoveFolder(parameters, folder);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogException(ex, "Exception occured moving {0}", folder);
                    }
                }
            }
        }

        public static void LookupDetailsAndMoveFolder(Parameters parameters, string source)
        {
            var folderName = new DirectoryInfo(source).Name;
            var targetFolder = parameters.DestinationFolder + folderName;

            // Check if the folder doesn't already has a year on it "The Avengers (2011)"
            if (!Regex.IsMatch(folderName, @"^[\s\S]*\s\(\d{4}\)$"))
            {
                var client = new TMDbClient(parameters.ApiKey);
                var results = client.SearchMovie(folderName);

                Logger.LogInfo($"Got back {results.TotalResults:N0} results");

                if (results.Results.Count < 1)
                {
                    return;
                }

                var result = results.Results[0]; // use the first
                targetFolder = String.Format(
                    "{0}{1} ({2:yyyy})",
                    parameters.DestinationFolder,
                    FileHelper.RemoveIllegalFolderCharacters(result.Title),
                    result.ReleaseDate);
            }

            FileHelper.MoveFolder(source, targetFolder);
        }
    }
}