using System;
using SampleWebApi.Models;

namespace SampleWebApi
{

    public interface IGameManagerDI
    {
        public DeckOfCards myDeck { get; set; }
        public GameSession myGame { get; set; }

    }

    public class GameManagerPoker : IGameManagerDI
	{
        public DeckOfCards myDeck { get; set; } = null;
        public GameSession myGame { get; set; } = null;

    
        public GameManagerPoker()
		{
            myDeck = new DeckOfCards();
            myGame = new GameSession();
        }
    }
    public class GameManagerHiCard : IGameManagerDI
    {
        public DeckOfCards myDeck { get; set; } = null;
        public GameSession myGame { get; set; } = null;


        public GameManagerHiCard()
        {
            myDeck = new DeckOfCards();
            myGame = new GameSession();
        }
    }
}

