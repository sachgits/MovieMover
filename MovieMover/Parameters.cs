using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieMover
{
    public class Parameters
    {
        public string SourceFolder { get; set; }

        public string DestinationFolder { get; set; }

        public string ApiKey { get; set; }

        public FtpParameters FtpDetails { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("SourceFolder: {0}\r\n", SourceFolder);
            builder.AppendFormat("DestinationFolder: {0}\r\n", DestinationFolder);
            builder.AppendFormat("ApiKey: {0}\r\n", ApiKey);
            builder.AppendFormat("FtpDetails: {0}\r\n", FtpDetails);
            return builder.ToString();
        }
    }

    public class FtpParameters
    {
        public string Address { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string RemoteFolder { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("UserName: {0}\r\n", UserName);
            builder.AppendFormat("Password: {0}\r\n", Password);
            builder.AppendFormat("RemoteFolder: {0}\r\n", RemoteFolder);
            return builder.ToString();
        }
    }

}
