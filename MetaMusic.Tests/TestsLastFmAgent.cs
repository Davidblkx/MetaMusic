using System;
using System.Threading.Tasks;
using MetaMusic.API.LastFm;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetaMusic.Tests
{
    [TestClass]
    public class TestsLastFmAgent
    {
        [TestMethod]
        public async Task LastFmAgentGetArtistByMbid()
        {
            LastFmAgent agent = new LastFmAgent(new AuthLastFm
            {
                ApiKey = Private.Secret.LastFmKey,
                ApiSecret = "NULL"
            });

            LastFmArtist art = await agent.GetArtistbyMbid("83d91898-7763-47d7-b03b-b92132375c47");
            LastFmApiExceptionArgs error = null;

            try
            {
                await agent.GetArtistbyMbid("NULL");
            }
            catch (LastFmApiException exception)
            {
                error = exception.Args;
            }

            Assert.IsNotNull(error, "Error is null");
            Assert.IsNotNull(art, "Artist is null");
            Assert.AreEqual(art.Name, "Pink Floyd");
            Assert.AreEqual(6, error.ErrorCode);
        }

        [TestMethod]
        public async Task LastFmAgentGetAlbumByMbid()
        {
            /*
             * MBID:de10209f-2bff-4c01-8ad6-6825ecbfb44b
             * Wish you were here
             */

            LastFmAgent agent = new LastFmAgent(new AuthLastFm
            {
                ApiKey = Private.Secret.LastFmKey,
                ApiSecret = "NULL"
            });

            LastFmAlbum album = await agent.GetAlbumByMbid("de10209f-2bff-4c01-8ad6-6825ecbfb44b");
            LastFmApiExceptionArgs error = null;

            try
            {
                await agent.GetAlbumByMbid("datra");
            }
            catch (LastFmApiException exception)
            {
                error = exception.Args;
            }

            Assert.IsNotNull(error, "error not raised");
            Assert.IsNotNull(album, "wrongly initiated");

            Assert.AreEqual("Wish You Were Here", album.Title);
            Assert.AreEqual("Pink Floyd", album.Artist);
        }

        [TestMethod]
        public async Task LastFmAgentGetTrackByMbid()
        {
            /*
             * MBID:5b57aa2a-6a8c-473b-a5d6-f9b58ed251f7
             * Brain Damages
             */

            LastFmAgent agent = new LastFmAgent(new AuthLastFm
            {
                ApiKey = Private.Secret.LastFmKey,
                ApiSecret = "NULL"
            });

            LastFmTrack track = await agent.GetTrackByMbid("5b57aa2a-6a8c-473b-a5d6-f9b58ed251f7");
            LastFmApiExceptionArgs error = null;

            try
            {
                await agent.GetTrackByMbid("datra");
            }
            catch (LastFmApiException exception)
            {
                error = exception.Args;
            }

            Assert.IsNotNull(error, "error not raised");
            Assert.IsNotNull(track, "wrongly initiated");

            Assert.AreEqual("The Dark Side of the Moon", track.AlbumTitle);
            Assert.AreEqual("Pink Floyd", track.AlbumArtist);
        }

        [TestMethod]
        public async Task LastFmSearchArtist()
        {
            LastFmAgent agent = new LastFmAgent(new AuthLastFm
            {
                ApiKey = Private.Secret.LastFmKey,
                ApiSecret = "NULL"
            });

            var result = await agent.SearchArtist("Pink Floyd");

            Assert.IsNotNull(agent, "LastFM agent os Null");
            Assert.IsNotNull(result, "Search result is Null");
            Assert.IsTrue(result.Count == 30, "Item count invalid");
        }

        [TestMethod]
        public async Task LastFmSearchAlbum()
        {
            LastFmAgent agent = new LastFmAgent(new AuthLastFm
            {
                ApiKey = Private.Secret.LastFmKey,
                ApiSecret = "NULL"
            });

            var result = await agent.SearchAlbum("Dark side of the moon");

            Assert.IsNotNull(agent, "LastFM agent os Null");
            Assert.IsNotNull(result, "Search result is Null");
            Assert.IsTrue(result.Count == 50, "Item count invalid");
        }

        [TestMethod]
        public async Task LastFmSearchTracks()
        {
            LastFmAgent agent = new LastFmAgent(new AuthLastFm
            {
                ApiKey = Private.Secret.LastFmKey,
                ApiSecret = "NULL"
            });

            var result = await agent.SearchTrack("Brain Damage", "Pink Floyd");

            Assert.IsNotNull(agent, "LastFM agent os Null");
            Assert.IsNotNull(result, "Search result is Null");
            Assert.IsTrue(result.Count == 30, "Item count invalid");
        }
    }
}
