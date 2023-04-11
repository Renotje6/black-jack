namespace Blackjack
{
    class Casino
    {
        private Deck _cardDeck;
        private Player _player;
        private Dealer _dealer;
        private readonly int MINIMUM_BET = 50;

        private enum Result
        {
            PLAYER_WIN,
            PLAYER_BUST,
            PLAYER_BLACKJACK,
            DEALER_WIN,
            INVALID_BET
        }

        public Casino()
        {
            this._cardDeck = new Deck(6);
            this._player = new Player();
            this._dealer = new Dealer();
        }

        private void dealInitialHands()
        {
            for (int i = 0; i < 2; i++)
            {
                if (i % 2 == 0)
                {
                    this._dealer.addHiddenCard(this._cardDeck.takeCard());
                }
                else
                {
                    this._dealer.addCard(this._cardDeck.takeCard());
                }

                this._player.addCard(this._cardDeck.takeCard());
            }
        }

        private Boolean isHandBlackjack(List<Card> hand)
        {
            if (hand.Count == 2)
            {
                if (hand[0].Face == Face.Ace && hand[1].Value == 10) return true;
                else if (hand[1].Face == Face.Ace && hand[0].Value == 10) return true;

            }
            return false;
        }

        public void startGame()
        {
            this._player.resetHand();
            this._dealer.resetHand();

            Casino.resetColors();

            if (!this.inputUserBet())
            {
                this.endGame(Result.INVALID_BET);
                return;
            }

            this.dealInitialHands();

            this.userActions();


            this._dealer.revealCard();
            while (this._dealer.getHandValue() < 17)
            {
                this._dealer.addCard(this._cardDeck.takeCard());

                Console.Clear();
                this._dealer.printHand();
                System.Console.WriteLine();
                this._player.printHand();
                Thread.Sleep(1000);
            }


            Console.Clear();
            this._dealer.printHand();
            System.Console.WriteLine("");
            this._player.printHand();

            if (this._player.getHandValue() > 21)
            {
                this.endGame(Result.PLAYER_BUST);
            }
            else if (this._player.getHandValue() > this._dealer.getHandValue() || this._dealer.getHandValue() > 21)
            {
                if (this.isHandBlackjack(this._player.getHand()))
                {
                    this.endGame(Result.PLAYER_BLACKJACK);
                }
                else
                {
                    this.endGame(Result.PLAYER_WIN);
                }
            }
            else
            {
                this.endGame(Result.DEALER_WIN);
            }
        }

        private void endGame(Result result)
        {
            System.Console.Write("\n");
            switch (result)
            {
                case Result.PLAYER_WIN:
                    Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine("Player wins!");
                    Casino.resetColors();
                    Console.Write("Player received: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("{0} chips.", this.payOut(this.isHandBlackjack(this._player.getHand())).ToString());
                    break;
                case Result.PLAYER_BLACKJACK:
                    Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine("Player has Blackjack. Player Wins!");
                    Casino.resetColors();
                    Console.Write("Player received: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("{0} chips.", this.payOut(this.isHandBlackjack(this._player.getHand())).ToString());
                    break;
                case Result.PLAYER_BUST:
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("Player busts! Dealer wins.");
                    Casino.resetColors();
                    System.Console.Write("Player lost: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("{0} chips.", this._player.Bet);
                    this._player.resetBet();
                    break;
                case Result.DEALER_WIN:
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("Dealer wins.");
                    Casino.resetColors();
                    System.Console.Write("Player lost: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("{0} chips.", this._player.Bet);
                    this._player.resetBet();
                    break;
                case Result.INVALID_BET:
                    System.Console.WriteLine("Invalid bet.");
                    break;
            }

            Casino.resetColors();

            if (this._player.Chips <= MINIMUM_BET)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("\nYou do not have enough chips to play another round!");
                System.Console.WriteLine("You will have to start over...");
                this._player = new Player();
            }

            Casino.resetColors();
            System.Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            this.startGame();
        }

        private Boolean inputUserBet()
        {
            int bet = 0;
            Console.Clear();
            System.Console.WriteLine("Current chip amount: {0}", this._player.Chips);
            System.Console.WriteLine("Minimum bet: {0}", this.MINIMUM_BET);

            System.Console.WriteLine("\nEnter bet: ");
            try
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                bet = Convert.ToInt32(Console.ReadLine());
                Casino.resetColors();
                if (bet < this.MINIMUM_BET || bet > this._player.Chips) return false;

                this._player.Bet = bet;
                this._player.Chips -= bet;
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void userActions()
        {
            string? action = "";

            do
            {
                Console.Clear();
                this._dealer.printHand();
                System.Console.WriteLine("");
                this._player.printHand();

                System.Console.WriteLine("\nEnter action: ");
                action = Console.ReadLine();

                switch (action.ToUpper())
                {
                    case "HIT":
                        this._player.addCard(this._cardDeck.takeCard());
                        break;

                    case "DOUBLE":
                        this._player.addCard(this._cardDeck.takeCard());
                        this._player.Chips -= this._player.Bet;
                        this._player.Bet += this._player.Bet;
                        Console.ReadKey();
                        break;

                    case "STAND":
                        break;

                    default:
                        System.Console.WriteLine("\nInvalid action");
                        System.Console.WriteLine("Valid actions: \nHit, Stand");
                        System.Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                }
            } while (!action.ToUpper().Equals("STAND") && !action.ToUpper().Equals("DOUBLE") && this._player.getHandValue() <= 21);
        }

        private int payOut(Boolean blackJack)
        {
            double multiplier = blackJack ? 1.5 : 1;
            int chips = 0;

            chips = this._player.Chips += Convert.ToInt32(this._player.Bet + (this._player.Bet * multiplier));
            this._player.resetBet();

            return chips;
        }

        public static void resetColors()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}