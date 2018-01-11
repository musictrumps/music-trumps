using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TrumpEngine.Core;

namespace TrumpEngine.UnitTests
{
    [TestClass]
    public class BasicMethodsTest
    {
        [TestMethod]
        public void TestReturnsAListOfBandsByGenreWithFilledProperties()
        {
            var core = new BandCore();
            var bands = core.GetBandsByGenre("rock");

            bool isNameBlank = bands.Exists(b => string.IsNullOrWhiteSpace(b.Name));
            bool isPictureBlank = bands.Exists(b => string.IsNullOrWhiteSpace(b.Picture));
            bool isBeginDateMinDate = bands.Exists(b => b.Begin > DateTime.MinValue);

            Assert.AreEqual(false, isNameBlank, "There are some blank names.");
            Assert.AreEqual(false, isBeginDateMinDate, "There are no begin date defined.");
            Assert.AreEqual(false, isPictureBlank, "There are some blank pictures.");
        }
    }
}