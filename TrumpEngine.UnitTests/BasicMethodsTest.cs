using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Extensions.Configuration;
using TrumpEngine.Core;
using TrumpEngine.Data;
using TrumpEngine.Shared;
using TrumpEngine.Shared.Settings;

namespace TrumpEngine.UnitTests
{
    [TestClass]
    public class BasicMethodsTest
    {
        IConfiguration Configuration { get; set; }
        private readonly Settings _settings;
        public BasicMethodsTest()
        {
            // the type specified here is just so the secrets library can 
            // find the UserSecretId we added in the csproj file
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<BasicMethodsTest>();

            Configuration = builder.Build();
            _settings = Configuration.Get<Settings>();
        }

        [TestMethod]
        public void TestReturnsAListOfBandsByGenreWithFilledProperties()
        {
            var core = new BandCore(new BandData(_settings));
            var bands = core.GetBandsByGenre("rock");

            bool isNameBlank = bands.Exists(b => string.IsNullOrWhiteSpace(b.Name));
            bool isPictureBlank = bands.Exists(b => string.IsNullOrWhiteSpace(b.Picture));

            Assert.AreEqual(false, isNameBlank, "There are some blank names.");
            Assert.AreEqual(false, isPictureBlank, "There are some blank pictures.");
        }
    }
}