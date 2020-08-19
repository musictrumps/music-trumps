using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using TrumpEngine.Core;
using TrumpEngine.Data;
using TrumpEngine.Shared.Settings;

namespace TrumpEngine.UnitTests
{
    [TestClass]
    public class BasicMethodsTest
    {
       
        private readonly Settings _settings;
        public BasicMethodsTest()
        {
           
            var builder = new ConfigurationBuilder() 
                .AddUserSecrets<BasicMethodsTest>()
                .AddEnvironmentVariables();


            var configuration = builder.Build();
            _settings = configuration.Get<Settings>();
            Console.WriteLine(JsonSerializer.Serialize(_settings));
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