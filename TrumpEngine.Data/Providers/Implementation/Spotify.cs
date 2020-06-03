using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TrumpEngine.Data.Providers.Implementation.Model;
using TrumpEngine.Data.Providers.Interface;
using TrumpEngine.Model;
using TrumpEngine.Shared;

namespace TrumpEngine.Data.Providers.Implementation
{
    internal class Spotify : IProvider
    {
        
        private const string SPOTIFY_GRANT_TYPE_HEADER = "grant_type";
        private const string SPOTIFY_GRANT_TYPE_VALUE = "client_credentials";
        private const string SPOTIFY_AUTHORIZATION_HEADER = "Authorization";
        private const string SPOTIFY_ACCESS_TOKEN = "Bearer {0}";
        private const int SPOTIFY_MAX_AMOUNT_IDS_BY_SEVERAL_ARISTS = 50; //LIMIT DEFINED BY THE SPOTIFY API
        private readonly SpotifySecrets _spotifySecrets;
        private string AccessToken { get; set; }

        public Spotify(SpotifySecrets spotifySecrets)
        {
            _spotifySecrets = spotifySecrets;
            this.AccessToken = GetAccessToken();
            
        }

        public List<Band> GetBandsByGenre(string genre)
        {
            try
            {
                List<Band> bands = null;
                RecommendedTracks json = null;

                using (System.Net.WebClient web = new System.Net.WebClient())
                {
                    web.Headers.Add(SPOTIFY_AUTHORIZATION_HEADER, string.Format(SPOTIFY_ACCESS_TOKEN, AccessToken));
                    string response = web.DownloadString(string.Format(_spotifySecrets.GetUrlByGenre(), genre));
                    json = JsonConvert.DeserializeObject<RecommendedTracks>(response);
                }

                bands = new List<Band>();

                foreach (var track in json.Tracks)
                {
                    Band band = new Band
                    {
                        Id = track.Artists.FirstOrDefault().Id,
                        Name = track.Artists.FirstOrDefault().Name
                    };

                    if (!bands.Exists(b => b.Id.Equals(band.Id)))
                        bands.Add(band);
                }

                int totalPagesBySeveralArtists = (int)Math.Ceiling((decimal)bands.Count / SPOTIFY_MAX_AMOUNT_IDS_BY_SEVERAL_ARISTS);
                List<Artist> artists = new List<Artist>();

                //Get pictures in batch from a list of artists
                for (int i = 0; i < totalPagesBySeveralArtists; i++)
                {
                    var filteredIds = bands.Skip(i * SPOTIFY_MAX_AMOUNT_IDS_BY_SEVERAL_ARISTS)
                        .Take(SPOTIFY_MAX_AMOUNT_IDS_BY_SEVERAL_ARISTS).ToList();

                    string ids = string.Join(",", filteredIds.Select(b => b.Id).ToArray());
                    artists.AddRange(GetArtistsPicturesFromIds(ids).Artists);
                }

                //TODO: INJECT BY A INJECTION MECHANISM
                MusicBrainz.MusicBrainz musicBrainz = new MusicBrainz.MusicBrainz();

                //TODO: MOVE THE LINES ABOVE TO A CLASS TO COMBINE ALL THE DATA
                foreach (var artist in artists)
                {
                    var band = bands.FirstOrDefault(b => b.Id.Equals(artist.Id));
                    band.Picture = artist.Images?.FirstOrDefault().Url;
                    Task.Delay(2000).Wait(); //FIXME: IT'S JUST A WORKAROUND DUE TO THE RATE LIMITING OF MUSICBRAINZ.ORG API
                    band.Begin = musicBrainz.GetBeginDate(artist.Name, genre);
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

                using (WebClient web = new System.Net.WebClient())
                {
                    web.Headers.Add(SPOTIFY_AUTHORIZATION_HEADER, string.Format(SPOTIFY_ACCESS_TOKEN, AccessToken));
                    string response = web.DownloadString(string.Format(_spotifySecrets.GetUrlArtistInformation(), ids));
                    artists = JsonConvert.DeserializeObject<SeveralArtists>(response);
                }

                return artists;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GetAccessToken()
        {
            try
            {
                byte[] credentials = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", "1cea836e0b114e1b865afc7ebcbf3865", "8d698f13989d4b47aa86f1e212d1ffdc")); //FIXME
                string encodedCredentials = Convert.ToBase64String(credentials);
                Token token = null;

                using (System.Net.WebClient web = new System.Net.WebClient())
                {
                    var data = new NameValueCollection();
                    data.Add(SPOTIFY_GRANT_TYPE_HEADER, SPOTIFY_GRANT_TYPE_VALUE);

                    web.Headers.Add(SPOTIFY_AUTHORIZATION_HEADER, string.Format("Basic {0}", encodedCredentials));
                    web.Encoding = Encoding.UTF8;

                    byte[] response = web.UploadValues(_spotifySecrets.TokenUrl, data);
                    token = JsonConvert.DeserializeObject<Token>(Encoding.UTF8.GetString(response));
                }

                return token.AccessToken;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}