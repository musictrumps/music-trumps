using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            bool isAlbumEqualsZero = bands.Exists(b => b.Albums == 0);

            Assert.AreEqual(false, isNameBlank, "There are some blank names.");
            Assert.AreEqual(false, isAlbumEqualsZero, "There are no albums.");
            Assert.AreEqual(false, isPictureBlank, "There are some blank pictures.");
        }
    }
}