
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TrumpEngine.Data.Providers.Implementation.Model
{
    internal class LastFmArtistInfo
    {
        [JsonProperty(PropertyName = "tracks")]
        public List<Track> Tracks { get; set; }
    }
}
