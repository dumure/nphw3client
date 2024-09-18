using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nphw3client
{
    public class Command
    {
        public const string Get = "GET";
        public const string Post = "POST";
        public const string Put = "PUT";
        public const string Delete = "DELETE";
        public string? Text { get; set; }
        private List<string?>? _params;

        public List<string?>? Params
        {
            get { return _params; }
            set 
            {
                if (value != null)
                {
                    foreach (string? param in value)
                    {
                        param.Replace(" ", "_");
                    }
                    _params = value;
                }
            }
        }
    }
}
