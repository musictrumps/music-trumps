using TrumpEngine.Core;

namespace TrumpEngine.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            BandCore bandCore = new BandCore();
            var bands = bandCore.GetBandsByGenre("rock");

            foreach (var band in bands)
            {
                System.Console.WriteLine("Band: {0}, Year: {1}, Picture: {2}", 
                    band.Name, band.Begin.Year, band.Picture);
            }
        }
    }
}