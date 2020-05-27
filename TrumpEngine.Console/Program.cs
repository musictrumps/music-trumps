using System.Net.Http;
using System.Text;
using TrumpEngine.Core;
using TrumpEngine.Model;

namespace TrumpEngine.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string genre = "rock";
            BandCore bandCore = new BandCore();
            var bands = bandCore.GetBandsByGenre(genre);

            foreach (var band in bands)
            {
                System.Console.WriteLine("Band: {0}, Year: {1}, Picture: {2}", 
                    band.Name, band.Begin.Year, band.Picture);

                InsertBandToFirebase(genre, band);
            }
        }

        static async void InsertBandToFirebase(string genre, Band band)
        {
            HttpClient client = new HttpClient();
            var stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(band), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(string.Format("https://music-trumps-database.firebaseio.com/{0}.json", genre), stringContent);
            System.Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }
}