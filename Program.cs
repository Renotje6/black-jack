namespace Blackjack
{
    class Program
    {
        static void Main(String[] args)
        {
            Casino casino = new Casino();
            Casino.resetColors();

            Console.Title = "♠♥♣♦ Blackjack";
            System.Console.WriteLine("♠♥♣♦ Welcome to Blackjack!");
            System.Console.WriteLine("Press a key to play...");
            Console.ReadKey();

            casino.startGame();
        }
    }
}