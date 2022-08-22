using System;
namespace SampleWebApi.Models
{
    public class DeckOfCards
    {
        #region cards List related
        protected const int MaxNrOfCards = 52;
        protected List<PlayingCard> cards = new List<PlayingCard>(MaxNrOfCards);
        #endregion

        public int Count => cards.Count();
        public PlayingCard DealOne()
        {
            PlayingCard card = cards[^1];
            cards.RemoveAt(cards.Count - 1);

            return card;
        }

        #region Shuffle and Sorting
        public void Shuffle()
        {
            if (cards.Count <= 0) return;

            var rnd = new Random();
            int nrOfShuffles = rnd.Next(100, 100000);
            for (int shuffle = 0; shuffle < nrOfShuffles; shuffle++)
            {
                //Swap to random cards with each other
                int loCard = rnd.Next(0, cards.Count);
                int hiCard = rnd.Next(0, cards.Count);

                (cards[loCard], cards[hiCard]) = (cards[hiCard], cards[loCard]);
            }
        }
        public void Sort() => cards.Sort();
        #endregion

        public DeckOfCards()
        {
            for (PlayingCardColor c = PlayingCardColor.Clubs; c <= PlayingCardColor.Spades; c++)
            {
                for (PlayingCardValue v = PlayingCardValue.Two; v <= PlayingCardValue.Ace; v++)
                {
                    cards.Add(new PlayingCard { Color = c, Value = v });
                }
            }
        }
    }
}

