using System;
using System.Collections.Generic;
using TrumpEngine.Data;
using TrumpEngine.Model;

namespace TrumpEngine.Business
{
    public class BandBusiness
    {
        private readonly BandData _data;

        public BandBusiness()
        {
            _data = new BandData();
        }

        public void Insert(Band band)
        {
            try
            {
                _data.Insert(band);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Band> FindAll()
        {
            try
            {
                return _data.FindAll();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
