using System;
using SampleWebApi.Models;

namespace SampleWebApi
{
	public class SingletonCards
	{
        private static SingletonCards _instance = null;

        public PlayingCard myCard1 { get; }
        public PlayingCard myCard2 { get; }


        public static SingletonCards Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SingletonCards();
                    return _instance;
                }
                return _instance;
            }
        }

        private SingletonCards()
		{
            myCard1 = PlayingCard.CreateRandom();
            myCard2 = PlayingCard.CreateRandom();
        }
    }
}

