using System;
using SampleWebApi.Models;

namespace SampleWebApi
{
	public class GameManager
	{
        private static GameManager _instance = null;

        public DeckOfCards myDeck = null;
        public GameSession myGame = null;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                    return _instance;
                }
                return _instance;
            }
        }

        private GameManager()
		{
            myDeck = new DeckOfCards();
            myGame = new GameSession();
        }
    }
}

