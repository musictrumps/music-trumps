using Newtonsoft.Json;

namespace TrumpEngine.Data.Providers.Implementation.MusicBrainz.Model
{
    internal class Tag
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }
        
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}