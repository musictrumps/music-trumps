
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrumpEngine.Api.Configuration;
using TrumpEngine.Core;

namespace TrumpEngine.Api.Controllers
{
    [ApiController]
    public class BandsController : ControllerBase
    {
        private readonly FirebaseSecrets _firebaseSecrets;
        public BandsController(Secrets secrets)
        {
            _firebaseSecrets = secrets.Firebase;
        }

        [HttpGet("{genre}")]
        [Route("/api/bands/{genre}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult  Get(string genre)
        {
            BandCore bandCore = new BandCore();
            var bands = bandCore.GetBandsByGenre(genre);
            return Ok(bands);
        }
    }
}
