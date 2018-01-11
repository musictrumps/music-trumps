using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TrumpEngine.Data.Providers.Implementation.MusicBrainz.Model
{
    internal class Artist
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "life-span")]
        public LifeSpan LifeSpan { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public List<Tag> Tags { get; set; }

        [JsonProperty(PropertyName = "score")] 
        public int Score { get; set; }
    }

    internal class ArtistDataHolder
    {
        [JsonProperty(PropertyName = "artists")]
        public List<Artist> Artists { get; set; }
    }
}