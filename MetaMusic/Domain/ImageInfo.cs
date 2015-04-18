﻿using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace MetaMusic.Domain
{
    /// <summary>
    /// Info from image
    /// </summary>
    public class ImageInfo
    {
        /// <summary>
        /// Url of image
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// Get image size
        /// </summary>
        public ImageInfoSizes ImageInfoSize { get; set; }

        /// <summary>
        /// Download image from web and returns stream content
        /// </summary>
        /// <returns></returns>
        public async Task<Stream> GetStream()
        {
            if (!Uri.IsWellFormedUriString(Link, UriKind.Absolute))
                throw new FormatException("Invalid URI Link");

            HttpClient client = new HttpClient();
            var stream = await client.GetStreamAsync(new Uri(Link));

            if(stream ==null)
                throw new Exception("Error getting file");

            return stream;
        }
    }
}
