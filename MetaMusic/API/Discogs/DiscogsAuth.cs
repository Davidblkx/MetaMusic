using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaMusic.API.Common;

namespace MetaMusic.API.Discogs
{
    public class DiscogsAuth : AuthCredentials
    {
        public string UserAgent { get; private set; }

        public DiscogsAuth(string apiSecret, string apiKey, string userAgent) : base(apiSecret, apiKey)
        {
            UserAgent = userAgent;
        }
    }
}
