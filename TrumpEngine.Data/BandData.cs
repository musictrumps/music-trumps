using System;
using System.Collections.Generic;
using TrumpEngine.Data.Providers.Implementation;
using TrumpEngine.Data.Providers.Interface;
using TrumpEngine.Model;
using TrumpEngine.Shared.Settings;

namespace TrumpEngine.Data
{
    public class BandData
    {
        private readonly IProvider provider;
        
        public BandData(Settings settings)
        {
            this.provider = new Spotify(settings.Spotify);
        }

        public List<Band> GetBandsByGenre(string genre)
        {
            try
            {
                return this.provider.GetBandsByGenre(genre);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}