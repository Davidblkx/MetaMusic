using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaMusic.API.Common;
using MetaMusic.Helpers;
using Newtonsoft.Json.Linq;

namespace MetaMusic.API.Discogs
{
    public class DiscogsArtist
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public IList<string> Alias { get; set; }
        public IList<DiscogsArtistMembers> Members { get; set; }
        public IList<ImageInfo> Images { get; set; }

        public static DiscogsArtist ParseArtist(JObject data)
        {
            DiscogsArtist art = new DiscogsArtist
            {
                Name = data.GetStringValue("name").FixDiscogsName(),
                Id = data.GetStringValue("id"),
                Alias = new List<string>(),
                Members = new List<DiscogsArtistMembers>(),
                Images = new List<ImageInfo>()
            };

            //add aliases
            foreach (var alias in data["namevariations"])
            {
                art.Alias.Add(alias.ToString());
            }

            //add members
            foreach (JToken mem in data["members"])
            {
                var nMember = new DiscogsArtistMembers
                {
                    Name = mem.GetStringValue("name"),
                    Id = mem.GetStringValue("id"),
                    IsActive = bool.Parse(mem.GetStringValue("active"))
                };
                art.Members.Add(nMember);
            }

            //add images
            foreach (JToken img in data["images"])
            {
                art.Images.Add(new ImageInfo
                {
                    Link = img.GetStringValue("uri"),
                    Size = new ImageSize(img.GetStringValue("width").ToInt(), img.GetStringValue("height").ToInt()),
                });
            }

            return art;
        }
    }
}
