using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using TrumpEngine.Business;
using TrumpEngine.Model;
using TrumpEngine.Scraper.Core;
using TrumpEngine.Scraper.Data;
using TrumpEngine.Shared.Settings;

namespace TrumpEngine.Loader
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
               .AddUserSecrets<Program>()
               .AddEnvironmentVariables();

            var configuration = builder.Build();
            var _settings = configuration.Get<Settings>();

            for (int i = 0; i < 50; i++)
            {
                string genre = "rock";
                try
                {
                    BandCore bandCore = new BandCore(new BandData(_settings));
                    var bands = bandCore.GetBandsByGenre(genre);

                    foreach (var band in bands)
                    {
                        System.Console.WriteLine("Band: {0}, Year: {1}, Picture: {2}",
                            band.Name, band.Begin.Year, band.Picture);

                        BandBusiness bandBusiness = new BandBusiness();
                        bandBusiness.Insert(band);
                    }
                }
                catch { }
            }
        }
    }
}
