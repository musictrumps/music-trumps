using Newtonsoft.Json;
using System.Collections.Generic;

namespace TrumpEngine.Scraper.Data.Providers.Implementation.Model
{
    internal class Artist
    {
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "images")]
        public List<Image> Images { get; set; }
    }
}