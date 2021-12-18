using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TrumpEngine.Business;
using TrumpEngine.Model;

namespace TrumpEngine.Api.Controllers
{
    [ApiController]
    public class GameController : ControllerBase
    {
        private static Random rng = new Random();
        private const int TOTAL_CARDS = 3; //TODO: put this on the appsettings.json

        [Route("/api/game/new")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult New(string playerName)
        {
            List<Band> bands = ReadAllBands();
            var shuffledBands = bands.OrderBy(a => rng.Next()).ToList();

            var game = new Game();
            game.UUID = Guid.NewGuid().ToString();
            game.Player1 = playerName.Trim();
            game.Player1_Turn = 1;
            game.Player1_Cards = JsonConvert.SerializeObject(shuffledBands.Take(TOTAL_CARDS));
            InsertGame(game);

            return Ok(game.UUID);
        }

        [Route("/api/game/join")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Join(string uuid, string playerName)
        {
            var game = GetGame(uuid);

            List<Band> bands = ReadAllBands();
            var shuffledBands = bands.OrderBy(a => rng.Next()).ToList();

            string message = "Room is full";

            //TODO: Need to consider the given cards from player1 to avoid repeating to player2.
            if (game != null
                && !string.IsNullOrEmpty(game.Player1)
                && string.IsNullOrEmpty(game.Player2))
            {
                game.Player2 = playerName;
                game.Player2_Turn = 0;
                game.Player2_Cards = JsonConvert.SerializeObject(shuffledBands.Skip(10).Take(TOTAL_CARDS));

                UpdateGame(game);
                return Ok(JsonConvert.DeserializeObject<List<Band>>(game.Player2_Cards));
            }

            return Ok(message);
        }

        [Route("/api/game/play")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Play(string uuid, string band)
        {
            Game game = GetGame(uuid);
            if (game.Player1_Turn == 1)
            {
                game.Player1_CurrentBand = band;
                //Remove the played card from player's deck.
                List<Band> bands = JsonConvert.DeserializeObject<List<Band>>(game.Player1_Cards);
                Band currentBand = bands.Find(i => i.Name == band.Trim());
                currentBand.Visible = false;
                game.Player1_Cards = JsonConvert.SerializeObject(bands);
            }

            if (game.Player2_Turn == 1)
            {
                game.Player2_CurrentBand = band;
                //Remove the played card from player's deck.
                List<Band> bands = JsonConvert.DeserializeObject<List<Band>>(game.Player2_Cards);
                Band currentBand = bands.Find(i => i.Name == band.Trim());
                currentBand.Visible = false;
                game.Player2_Cards = JsonConvert.SerializeObject(bands);
            }

            if (game.Player1_Turn == 1)
            {
                game.Player2_Turn = 1;
                game.Player1_Turn = 0;
            }
            else
            {
                game.Player2_Turn = 0;
                game.Player1_Turn = 1;
            }

            UpdateGame(game);
            return Ok();
        }

        [Route("/api/game/round")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Round(string uuid)
        {
            Game game = GetGame(uuid);

            //if both players
            if (!string.IsNullOrEmpty(game.Player1_CurrentBand)
                && !string.IsNullOrEmpty(game.Player2_CurrentBand))
            {
                var player1Bands = JsonConvert.DeserializeObject<List<Band>>(game.Player1_Cards);
                var player2Bands = JsonConvert.DeserializeObject<List<Band>>(game.Player2_Cards);

                var player1ChoosedBand = player1Bands.Find(i => i.Name == game.Player1_CurrentBand);
                var player2ChoosedBand = player2Bands.Find(i => i.Name == game.Player2_CurrentBand);

                if (player1ChoosedBand.Begin != player2ChoosedBand.Begin)
                {
                    //It's time to see who won!!!
                    if (player1ChoosedBand.Begin < player2ChoosedBand.Begin)
                        game.Player1_Points += 1;
                    else
                        game.Player2_Points += 1;

                    game.Player1_CurrentBand = string.Empty;
                    game.Player2_CurrentBand = string.Empty;
                    UpdateGame(game);
                }
            }

            return Ok(game);
        }

        [Route("/api/game")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(string uuid)
        {
            return Ok(GetGame(uuid));
        }

        [Route("/api/game/getVisibleCards")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetVisibleCardsByPlayer(string uuid, string playerName)
        {
            var game = GetGame(uuid);
            if (game.Player1.Equals(playerName))
            {
                List<Band> bands = JsonConvert.DeserializeObject<List<Band>>(game.Player1_Cards);
                return Ok(bands.FindAll(i => i.Visible == true));
            }

            if (game.Player2.Equals(playerName))
            {
                List<Band> bands = JsonConvert.DeserializeObject<List<Band>>(game.Player2_Cards);
                return Ok(bands.FindAll(i => i.Visible == true));
            }

            return Ok();
        }

        private Game GetGame(string uuid)
        {
            GameBusiness gameBusiness = new GameBusiness();
            return gameBusiness.FindByUUID(uuid);
        }

        private void UpdateGame(Game game)
        {
            GameBusiness gameBusiness = new GameBusiness();
            gameBusiness.Update(game);
        }

        private void InsertGame(Game game)
        {
            GameBusiness gameBusiness = new GameBusiness();
            gameBusiness.Insert(game);
        }

        private List<Band> ReadAllBands()
        {
            BandBusiness bandBusiness = new BandBusiness();
            return bandBusiness.FindAll();
        }
    }
}
