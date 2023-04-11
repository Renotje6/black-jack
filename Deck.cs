namespace Blackjack
{
    class Deck
    {
        private List<Card> _cards = new List<Card>();

        public Deck(int numberOfDecks = 1)
        {
            //Generate a new sorted Deck
            for (int i = 0; i < numberOfDecks; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        this._cards.Add(new Card((Suit)k, ((Face)j + 1)));
                    }
                }
            }

            this.shuffleDeck();
        }

        // Shuffle the deck
        private void shuffleDeck()
        {
            Random rng = new Random();
            int n = this._cards.Count;

            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card rdmCard = this._cards[k];
                this._cards[k] = this._cards[n];
                this._cards[n] = rdmCard;
            }
        }

        public Card takeCard()
        {
            Card card = this._cards[0];
            this._cards.RemoveAt(0);
            return card;
        }
    }
}