using System;
using System.Collections.Generic;
using TrumpEngine.Data.Providers.Implementation;
using TrumpEngine.Data.Providers.Interface;
using TrumpEngine.Model;

namespace TrumpEngine.Data
{
    public class BandData
    {
        private readonly IProvider provider;

        public BandData()
        {
            this.provider = new Spotify();
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