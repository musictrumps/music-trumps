using System;
using System.Collections.Generic;
using System.Text;
using TrumpEngine.Model;

namespace TrumpEngine.Scraper.Data.Providers.Interface
{
    public interface IProvider
    {
        List<Band> GetBandsByGenre(string genre);
    }
}