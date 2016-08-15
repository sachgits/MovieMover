using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Limilabs.FTP.Client;

namespace MovieMover
{
    public class FtpHelper
    {
        public static void UploadFolder(FtpParameters parameters, string targetFolder, string sourceFolder)
        {
            using (var ftp = new Ftp())
            {
                ftp.Connect(parameters.Address, parameters.Port);
                ftp.Login(parameters.UserName, parameters.Password);

                if (!ftp.FolderExists(targetFolder))
                {
                    ftp.CreateFolder(targetFolder);
                }

                Logger.LogInfo("Uploading from {0} to {1}...", sourceFolder, targetFolder);
                ftp.UploadFiles(targetFolder, sourceFolder);
                Logger.LogInfo("Move from {0} to {1}...Complete", sourceFolder, targetFolder);
            }
        }
    }
}
