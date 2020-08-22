
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TrumpEngine.Data.Providers.Implementation.Model
{
    internal class LastFmArtistInfo
    {
        [JsonProperty(PropertyName = "bio")]
        public LastFmArtistBiography Biography {get;set;}
    }

    internal class LastFmArtistBiography
    {
        [JsonProperty(PropertyName = "summary")]
        public string Summary {get;set;}
        
        [JsonProperty(PropertyName = "content")]
        public string Content {get;set;}
    }
}
