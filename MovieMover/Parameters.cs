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

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("SourceFolder: {0}\r\n", SourceFolder);
            builder.AppendFormat("DestinationFolder: {0}\r\n", DestinationFolder);
            builder.AppendFormat("ApiKey: {0}\r\n", ApiKey);
            return builder.ToString();
        }
    }
}
