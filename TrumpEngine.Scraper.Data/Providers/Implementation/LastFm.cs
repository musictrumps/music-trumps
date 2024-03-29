using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TrumpEngine.Scraper.Data.Providers.Implementation.Model;
using TrumpEngine.Model;
using TrumpEngine.Shared;
using TrumpEngine.Shared.Settings;

namespace TrumpEngine.Scraper.Data.Providers.Implementation
{
    internal class LastFm
    {
        private readonly LastFmSecrets _lastFmSecrets;
        private const string LASTFM_API_URL = "https://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist={0}&api_key={1}&format=json";
        
        public LastFm(LastFmSecrets lastFmSecrets)
        {
            _lastFmSecrets = lastFmSecrets;
        }
        
        public LastFmArtistInfo GetInfo(string artist) 
        {
            try
            {
                LastFmArtistInfo lastFmArtistInfo;
                using (System.Net.WebClient web = new System.Net.WebClient())
                {
                    string response = web.DownloadString(string.Format(LASTFM_API_URL, artist, _lastFmSecrets.ApiKey));
                    lastFmArtistInfo = JsonConvert.DeserializeObject<LastFmArtistInfo>(response);
                }

                return lastFmArtistInfo;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
