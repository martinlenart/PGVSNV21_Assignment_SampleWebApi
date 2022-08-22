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
        static int _NrOfCardsDealt = 0;
        static GameSession myGame = new GameSession();

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(GameSession))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> StartGame(string gameType)
        {
            if (!myGame.IsRunning)
            {
                myGame.StartGame(gameType.ToLower().Trim());
                return Ok(myGame);
            }
            return BadRequest($"Game of type {myGame.GameType} is already running");
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(GameSession))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> EndGame()
        {
            if (myGame.IsRunning)
            {
                myGame.EndGame();
                return Ok(myGame);
            }
            return BadRequest($"No game is running");
        }



        [HttpGet]
        [ProducesResponseType(200, Type = typeof(DealCard))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DealCard()
        {
            if (myGame.IsRunning)
            {
                _NrOfCardsDealt++;
                var ret = new DealCard { Card = PlayingCard.CreateRandom(), NrOfCardsDealt = _NrOfCardsDealt };

                return Ok(ret);
            }
            return BadRequest($"No game is running");
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(DealCards))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DealCards(string NrOfCards)
        {
            if (!myGame.IsRunning)
                return BadRequest($"No game is running");

            int nrOfCards;
            if (int.TryParse(NrOfCards, out nrOfCards) && nrOfCards > 0 && nrOfCards < 10)
            {
                _NrOfCardsDealt += nrOfCards;
                DealCards ret = new DealCards { NrOfCardsDealt = nrOfCards };

                for (int i = 0; i < nrOfCards; i++)
                {
                    ret.Cards.Add(PlayingCard.CreateRandom());
                }
                return Ok(ret);
            }
            return BadRequest($"Cannot interpret NrOfCards");
        }

    }
}

