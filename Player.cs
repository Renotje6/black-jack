namespace Blackjack
{
    class Player
    {
        private List<Card> _hand = new List<Card>();
        public int Chips { get; set; }
        public int Bet { get; set; }

        public Player()
        {
            this.Chips = 500;
        }

        public void addCard(Card card)
        {
            this._hand.Add(card);
            this.determineAceValue();
        }

        public int getHandValue()
        {
            int sum = 0;
            foreach (Card card in this._hand)
            {
                sum += card.Value;
            }

            return sum;
        }

        public List<Card> getHand()
        {
            return this._hand;
        }

        private void determineAceValue()
        {
            foreach (Card card in this._hand)
            {
                if (card.Face == Face.Ace && this.getHandValue() > 21)
                {
                    card.Value = 1;
                }
            }
        }

        public void printHand()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.WriteLine("Player hand:");
            Casino.resetColors();
            foreach (Card card in this._hand)
            {
                card.printInfo();
            }
        }

        public void resetBet()
        {
            this.Bet = 0;
        }

        public void resetHand()
        {
            this._hand = new List<Card>();
        }
    }
}