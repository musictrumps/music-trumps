using Newtonsoft.Json;

namespace TrumpEngine.Data.Providers.Implementation.Model
{
    internal class Image
    {
        [JsonProperty(PropertyName = "height")]
        public int Height { get; set; }

        [JsonProperty(PropertyName = "width")]
        public int Width { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
}