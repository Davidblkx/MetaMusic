using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaMusic.API.Common
{
    public class AuthCredentials
    {
        public AuthCredentials(string apiSecret, string apiKey)
        {
            ApiSecret = apiSecret;
            ApiKey = apiKey;
        }

        public string ApiKey { get; private set; }
        public string ApiSecret { get; private set; }
    }
}
