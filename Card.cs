using static Blackjack.Suit;
using static Blackjack.Face;

namespace Blackjack
{
    public enum Suit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

    public enum Face
    {
        Ace = 1,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }

    class Card
    {
        public Suit Suit { get; }
        public Face Face { get; }
        public int Value { get; set; }


        public Card(Suit suit, Face face)
        {
            this.Suit = suit;
            this.Face = face;

            switch (this.Face)
            {
                case Ten:
                case Jack:
                case Queen:
                case King:
                    this.Value = 10;
                    break;
                case Ace:
                    this.Value = 11;
                    break;
                default:
                    this.Value = (int)this.Face;
                    break;
            }

        }

        public void printInfo()
        {
            if (this.Face == Ace)
            {
                if (this.Value == 11)
                {
                    System.Console.WriteLine("Soft {0} of {1}", this.Face, this.Suit);
                }
                else
                {
                    System.Console.WriteLine("Hard {0} of {1}", this.Face, this.Suit);
                }
            }
            else
            {
                System.Console.WriteLine("{0} of {1}", this.Face, this.Suit);
            }
        }
    }
}