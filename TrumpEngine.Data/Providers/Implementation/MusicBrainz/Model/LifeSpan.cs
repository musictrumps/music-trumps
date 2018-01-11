using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TrumpEngine.Data.Providers.Implementation.MusicBrainz.Model
{
    internal class LifeSpan
    {
        [JsonProperty(PropertyName="begin")]
        public string Begin { get; set; }

        [JsonProperty(PropertyName = "ended")]
        public string Ended { get; set; }
    }
}