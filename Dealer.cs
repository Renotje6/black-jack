namespace Blackjack
{
    class Dealer
    {
        private List<Card> _hiddenCards = new List<Card>();
        private List<Card> _revealedCards = new List<Card>();

        public void addHiddenCard(Card card)
        {
            this._hiddenCards.Add(card);
        }

        public void addCard(Card card)
        {

            this._revealedCards.Add(card);

        }

        public void revealCard()
        {
            this._revealedCards.Add(this._hiddenCards[0]);
            this._hiddenCards.RemoveAt(0);
        }

        public int getHandValue()
        {
            int sum = 0;
            foreach (Card card in _hiddenCards)
            {
                sum += card.Value;
            }
            foreach (Card card in _revealedCards)
            {
                sum += card.Value;
            }
            return sum;
        }

        private void determineAceValue()
        {
            foreach (Card card in this._hiddenCards)
            {
                if (card.Face == Face.Ace && this.getHandValue() > 21)
                {
                    card.Value = 1;
                }
            }
            foreach (Card card in this._revealedCards)
            {
                if (card.Face == Face.Ace && this.getHandValue() > 21)
                {
                    card.Value = 1;
                }
            }
        }

        public void printHand()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine("Dealer hand:");
            Casino.resetColors();
            for (int i = 0; i < this._hiddenCards.Count; i++)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                System.Console.WriteLine("<Hidden>");
                Casino.resetColors();
            }
            foreach (Card card in _revealedCards)
            {
                card.printInfo();
            }
        }

        public void resetHand()
        {
            this._hiddenCards = new List<Card>();
            this._revealedCards = new List<Card>();
        }
    }
}