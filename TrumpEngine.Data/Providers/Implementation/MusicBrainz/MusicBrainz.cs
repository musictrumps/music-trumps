using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using TrumpEngine.Data.Providers.Implementation.MusicBrainz.Model;
using System.Linq;

namespace TrumpEngine.Data.Providers.Implementation.MusicBrainz
{
    internal class MusicBrainz
    {
        private const string MUSICBRAINZ_URL_QUERY_ARTISTS = "http://musicbrainz.org/ws/2/artist/?query={0}&fmt=json";
        private const string MUSICBRAINZ_USERAGENT_HEADER = "User-Agent";
        private const string MUSICBRAINZ_USERAGENT_VALUE = "TrumpEngine-Game (tsigwt@gmail.com)"; //TODO: ADD IN A CONFIGURATION FILE
        private const int MUSICBRAINZ_SCORE_GOOD_QUALITY_DATA = 80; //TODO: ADD IN A CONFIGURATION FILE

        public DateTime GetBeginDate(string name, string genre)
        {
            try
            {
                ArtistDataHolder json = null;
                using (WebClient web = new WebClient())
                {
                    web.Headers.Add(MUSICBRAINZ_USERAGENT_HEADER, MUSICBRAINZ_USERAGENT_VALUE);
                    string response = web.DownloadString(string.Format(MUSICBRAINZ_URL_QUERY_ARTISTS, System.Web.HttpUtility.HtmlEncode(name)));
                    json = JsonConvert.DeserializeObject<ArtistDataHolder> (response);
                }

                Artist artist = DecideProperlyArtistByGenreAndScoreCriteria(json.Artists, genre, MUSICBRAINZ_SCORE_GOOD_QUALITY_DATA);

                if (artist != null &&
                    artist.LifeSpan != null && 
                    !string.IsNullOrWhiteSpace(artist.LifeSpan.Begin))
                    return new DateTime(Convert.ToInt32(artist.LifeSpan.Begin.Substring(0, 4)), 1, 1); //FIXME: IT'S JUST A WORKAROUND!!!
                else
                    return DateTime.MinValue; //FIXME: IT'S JUST A WORKAROUND!!!
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Artist DecideProperlyArtistByGenreAndScoreCriteria(List<Artist> artists, string genre, int score)
        {
            try
            {
                //Since it could happen to have some similar name, it's important to take a look at the score and the tags (if exists).
                //TODO: To increase the chance to the get the right Artist, take a look at the 'disambiguation' property on the JSON.
                List<Artist> highScoredArtists = artists.FindAll(a => a.Score >= score);
                if (highScoredArtists.Count > 1 &&
                    highScoredArtists.Exists(a => a.Tags != null))
                {
                    return highScoredArtists.Find(a => a.Tags != null &&
                        a.Tags.Any(t => t.Name.Contains(genre) && t.Count > 0));
                }
                else
                    return highScoredArtists.FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}