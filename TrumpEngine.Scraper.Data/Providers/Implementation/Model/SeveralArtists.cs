using Newtonsoft.Json;
using System.Collections.Generic;

namespace TrumpEngine.Scraper.Data.Providers.Implementation.Model
{
    internal class SeveralArtists
    {
        [JsonProperty(PropertyName = "artists")]
        public List<Artist> Artists { get; set; }
    }
}