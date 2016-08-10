using System.IO;

namespace MovieMover
{
    public class FileHelper
    {
        public static bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                stream?.Close();
            }

            //file is not locked
            return false;
        }

        public static bool IsAnyFileLocked(string path)
        {
            var result = false;
            foreach (var file in Directory.GetFiles(path))
            {
                result = !result && IsFileLocked(new FileInfo(file));
            }
            return result;
        }

        public static string RemoveIllegalFolderCharacters(string name)
        {
            return name.Replace(":", "-");
        }

        public static void MoveFolder(string source, string destination)
        {
            if (Directory.Exists(destination))
            {
                Logger.LogInfo("Could not move the folder to {0}, as it already exists", destination);
            }
            else
            {
                Directory.Move(source, destination);
            }
        }
    }
}
