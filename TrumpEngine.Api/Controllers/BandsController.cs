
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrumpEngine.Core;
using TrumpEngine.Shared;
using TrumpEngine.Shared.Settings;

namespace TrumpEngine.Api.Controllers
{
    [ApiController]
    public class BandsController : ControllerBase
    {
        private readonly FirebaseSecrets _firebaseSecrets;
        private readonly BandCore _bandCore;
        public BandsController(Settings secrets, BandCore bandCore)
        {
            _firebaseSecrets = secrets.Firebase;
            _bandCore = bandCore;
        }

        [HttpGet("{genre}")]
        [Route("/api/bands/{genre}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult  Get(string genre)
        {
            var bands = _bandCore.GetBandsByGenre(genre);
            return Ok(bands);
        }
    }
}
