using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using TrumpEngine.Data.Providers.Implementation.Model;
using TrumpEngine.Data.Providers.Interface;
using TrumpEngine.Model;

namespace TrumpEngine.Data.Providers.Implementation
{
    public class Spotify : IProvider
    {
        private const string SPOTIFY_URL_RECOMMENDATIONS_BY_GENRE = "https://api.spotify.com/v1/recommendations?market=US&seed_genres={0}&limit=100";
        private const string SPOTIFY_URL_ARTIST_INFORMATION = "https://api.spotify.com/v1/artists?ids={0}";
        private const int SPOTIFY_MAX_AMOUNT_IDS_BY_SEVERAL_ARISTS = 50;

        private const string SPOTIFY_AUTHORIZATION_HEADER = "Authorization";
        private const string TOKEN = "Bearer BQDLiwYqu8fwlLgx_UDuqW0HernovKqPybNb0-lbivQBSmErB_39S-3X4dfG5KKla05W8uGrCQxLYWWQiPdOTxGuCnJKBHI8-GvXHIbkA2GynW0YPfxa1XTHmX7B0clYF0is8YedB9S7Jf8GhH5RSw";

        public List<Band> GetBandsByGenre(string genre)
        {
            try
            {
                List<Band> bands = null;
                RecommendedTracks json = null;

                using (System.Net.WebClient web = new System.Net.WebClient())
                {
                    web.Headers.Add(SPOTIFY_AUTHORIZATION_HEADER, TOKEN);
                    string response = web.DownloadString(string.Format(SPOTIFY_URL_RECOMMENDATIONS_BY_GENRE, genre));
                    json = JsonConvert.DeserializeObject<RecommendedTracks>(response);
                }

                bands = new List<Band>();

                foreach (var track in json.Tracks)
                {
                    Band band = new Band();
                    band.Id = track.Artists.FirstOrDefault().Id;
                    band.Name = track.Artists.FirstOrDefault().Name;

                    if (!bands.Exists(b => b.Id.Equals(band.Id)))
                        bands.Add(band);
                }

                int totalPagesBySeveralArtists = (int)Math.Ceiling((decimal)bands.Count / SPOTIFY_MAX_AMOUNT_IDS_BY_SEVERAL_ARISTS);
                List<Artist> artists = new List<Artist>();

                //Get details (picture) in batch from an artist
                for (int i = 0; i < totalPagesBySeveralArtists; i++)
                {
                    var filteredIds = bands.Skip(i * SPOTIFY_MAX_AMOUNT_IDS_BY_SEVERAL_ARISTS)
                        .Take(SPOTIFY_MAX_AMOUNT_IDS_BY_SEVERAL_ARISTS).ToList();

                    string ids = string.Join(",", filteredIds.Select(b => b.Id).ToArray());
                    artists.AddRange(GetArtistsPicturesFromIds(ids.Substring(0, ids.Length - 1)).Artists);
                }

                //combining all the data
                foreach (var artist in artists)
                {
                    if (artist != null)
                    {
                        var band = bands.FirstOrDefault(b => b.Id.Equals(artist.Id));
                        band.Picture = artist.Images?.FirstOrDefault().Url;
                        band.Albums = 10; //TODO
                    }
                }

                return bands;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private SeveralArtists GetArtistsPicturesFromIds(string ids)
        {
            try
            {
                SeveralArtists artists = null;

                using (System.Net.WebClient web = new System.Net.WebClient())
                {
                    web.Headers.Add(SPOTIFY_AUTHORIZATION_HEADER, TOKEN);
                    string response = web.DownloadString(string.Format(SPOTIFY_URL_ARTIST_INFORMATION, ids));
                    artists = JsonConvert.DeserializeObject<SeveralArtists>(response);
                }

                return artists;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }   
}