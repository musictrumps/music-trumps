using System;
using System.Collections.Generic;
using TrumpEngine.Data;
using TrumpEngine.Model;
using TrumpEngine.Shared.Settings;

namespace TrumpEngine.Core
{
    public class BandCore
    {
        private readonly BandData _data;

        public BandCore()
        {
            _data = new BandData(new Settings());
        }

        public BandCore(BandData bandData)
        {
            _data = bandData;
        }

        public List<Band> GetBandsByGenre(string genre)
        {
            try
            {
                return _data.GetBandsByGenre(genre);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}