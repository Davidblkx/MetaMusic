using System;
using System.Linq;
using System.Threading.Tasks;
using MetaMusic.API.Discogs;
using MetaMusic.Tests.Private;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetaMusic.Tests
{
    [TestClass]
    public class TestDiscogsAgent
    {
        private const string ArtistId = "116345"; //The White Stripes Id
        private const string AlbumId = "666";

        [TestMethod]
        public async Task DiscogsAgentGetArtistById()
        {
            DiscogsAgent agent = new DiscogsAgent(Secret.DiscogsAuth);
            Assert.IsNotNull(agent, "Discogs agent is null");

            DiscogsArtist artist = await agent.GetArtistById(ArtistId);
            Assert.IsNotNull(artist, "DiscogsArtist is null");
            Assert.AreNotEqual(artist.Images.First().Size.Height, -1);
        }

        [TestMethod]
        public async Task DiscogsAgentGetAlbumById()
        {
            DiscogsAgent agent = new DiscogsAgent(Secret.DiscogsAuth);
            Assert.IsNotNull(agent, "Discogs agent is null");

            DiscogsAlbum album = await agent.GetAlbumById(AlbumId);
            Assert.IsNotNull(album, "DiscogsAlbum is null");
        }
    }
}
