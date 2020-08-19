using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrumpEngine.Core;

namespace TrumpEngine.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class BandsController : ControllerBase
    {
        private readonly BandCore _bandCore;
        public BandsController(BandCore bandCore)
        {
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
