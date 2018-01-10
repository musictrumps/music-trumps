using System;
using System.Collections.Generic;
using System.Text;
using TrumpEngine.Model;

namespace TrumpEngine.Data.Providers.Interface
{
    public interface IProvider
    {
        List<Band> GetBandsByGenre(string genre);
    }
}