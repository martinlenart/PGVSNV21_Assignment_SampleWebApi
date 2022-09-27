using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleWebApi.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
//https://developer.mozilla.org/en-US/docs/Web/HTTP/Methods
//https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-6.0
//https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.httpgetattribute?view=aspnetcore-6.0
//https://docs.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode?view=net-6.0
//https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-6.0#iactionresult-type
//https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase.ok?view=aspnetcore-6.0

namespace SampleWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CardController : ControllerBase
    {
        private IGameManagerDI _gameManager;

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(GameSession))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> StartGame(string gameType)
        {
            if (!_gameManager.myGame.IsRunning)
            {
                _gameManager.myGame.StartGame(gameType.ToLower().Trim());
                _gameManager.myDeck.Shuffle();
                return Ok(_gameManager.myGame);
            }
            return BadRequest($"Game of type {_gameManager.myGame.GameType} is already running");
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(GameSession))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> EndGame()
        {
            if (_gameManager.myGame.IsRunning)
            {
                _gameManager.myGame.EndGame();
                return Ok(_gameManager.myGame);
            }
            return BadRequest($"No game is running");
        }



        [HttpGet]
        [ProducesResponseType(200, Type = typeof(PlayingCard))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DealCard()
        {
            if (_gameManager.myGame.IsRunning)
            {
                 var ret = _gameManager.myDeck.DealOne();

                return Ok(ret);
            }
            return BadRequest($"No game is running");
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<PlayingCard>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DealCards(string NrOfCards)
        {
            if (!_gameManager.myGame.IsRunning)
                return BadRequest($"No game is running");

            int nrOfCards;
            if (int.TryParse(NrOfCards, out nrOfCards) && nrOfCards > 0 && nrOfCards < 10)
            {
                List<PlayingCard> ret = new List<PlayingCard>();

                for (int i = 0; i < nrOfCards; i++)
                {
                    ret.Add(_gameManager.myDeck.DealOne());
                }
                return Ok(ret);
            }
            return BadRequest($"Cannot interpret NrOfCards");
        }

        //POST: api/winningcards   
        //Body: List<PlayingCard> in Json
        [HttpPost]                          //Needs to be PUT or POST as I have a request Body
        [ProducesResponseType(200, Type = typeof(List<PlayingCard>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> WinningCards([FromBody] List<PlayingCard> hand)
        {
            if (!_gameManager.myGame.IsRunning)
                return BadRequest($"No game is running");

            if (hand.Count < 1)
                return BadRequest($"Empty hand");

            //logik som avgor vinnande handen
            List<PlayingCard> winners = new List<PlayingCard>();
            winners.Add(hand[0]);

            return Ok(winners);
        }

        public CardController(IGameManagerDI gameManager)
        {
            _gameManager = gameManager;
        }
    }
}

