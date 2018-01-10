using System;
using System.Collections.Generic;
using TrumpEngine.Data;
using TrumpEngine.Model;

namespace TrumpEngine.Core
{
    public class BandCore
    {
        private readonly BandData data;

        public BandCore()
        {
            this.data = new BandData();
        }

        public List<Band> GetBandsByGenre(string genre)
        {
            try
            {
                return data.GetBandsByGenre(genre);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}