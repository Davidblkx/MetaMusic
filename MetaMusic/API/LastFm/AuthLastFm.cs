﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaMusic.API.LastFm
{
    public class AuthLastFm
    {
        public AuthLastFm(string apiSecret, string apiKey)
        {
            ApiSecret = apiSecret;
            ApiKey = apiKey;
        }

        public string ApiSecret { get; private set; }
        public string ApiKey { get; private set; }
    }
}
