using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class App
    {
        int cardOne;
        int cardTotal;
        int dealerTotal;
        string suit;
        int playerMoney = 100000;
        int bet;
        string holdHit;
        Random card = new Random();


        public bool DealCards()
        {
            Console.Clear();

            Bet();

            playerMoney -= bet;

            int x = 1;
            while (x < 6)
            {
                bool test = true;
                while (test)
                {
                    InitialDeal(card);

                    if (cardTotal > 21)
                    {
                        Console.WriteLine("You busted!\n");
                        x = 7;
                        return Play();
                    }

                    HoldHit(ref x, ref test);

                }
                x++;
            }
            return Play();
        }

        private void InitialDeal(Random card)
        {
            cardOne = CreateCards(card);

            Console.WriteLine("Dealing Cards:\n");

            Console.Write($"You received a {cardOne} of {suit}\n");
            cardTotal += cardOne;
            Console.WriteLine($"Your total: {cardTotal}");
        }

        private int CreateCards(Random card)
        {
            Console.Clear();

            int cardOne = card.Next(2, 12);
            cardOne = Ace(cardOne);

            int cardSuit = card.Next(1, 4);
            SuitType(cardSuit);

            return cardOne;
        }

        private void SuitType(int cardSuit)
        {
            switch (cardSuit)
            {
                case 1:
                    suit = "Clubs";
                    break;
                case 2:
                    suit = "Spades";
                    break;
                case 3:
                    suit = "Hearts";
                    break;
                case 4:
                    suit = "Diamonds";
                    break;
            }
        }

        private void HoldHit(ref int x, ref bool test)
        {
            bool hold = true;
            while (hold)
            {
                Console.WriteLine("\nHold or Hit?");
                holdHit = Console.ReadLine();

                switch (holdHit)
                {
                    case "hold":
                        DealerHand();
                        hold = false;
                        test = false;
                        x = 10;
                        break;
                    case "hit":
                        hold = false;
                        test = true;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine($"You have: {cardTotal}");
                        hold = true;
                        break;
                }
            }
        }

        private static int Ace(int cardOne)
        {
            Console.Clear();
            try
            {
                if (cardOne == 11)
                {
                    cardOne = AceValue(cardOne);
                }
            }

            catch(Exception)
            {
                Ace(cardOne);
            }
            return cardOne;
        }

        private static int AceValue(int cardOne)
        {
            Console.WriteLine("You've received an Ace, would you like it to be a 1 or 11?");

            var ace = Convert.ToInt32(Console.ReadLine());
            switch (ace)
            {
                case 1:
                    cardOne = 1;
                    break;
                case 11:
                    cardOne = 11;
                    break;
                default:
                    Ace(cardOne);
                    break;
            }

            return cardOne;
        }

        private void Bet()
        {
            Console.Clear();

            Console.WriteLine($"Your money: {playerMoney}\n");

            try
            {
                Console.WriteLine("Place your bet!");
                bet = Convert.ToInt32(Console.ReadLine());
                
                if (bet>playerMoney)
                {
                    Bet();
                }
            }

            catch (Exception)
            {
                Bet();
            }
        }

        public bool Play()
        {

            if (playerMoney == 0)
            {
                Console.WriteLine("Unfortunately you have no more money! Goodbye!");
                return false;
            }

            Console.WriteLine("Would you like to play again?");

            var input = Console.ReadLine();
            switch(input)
            {
                case "no":
                    Console.WriteLine("Sorry to see you go");
                    break;
                case "yes":
                    cardTotal = 0;
                    return DealCards();
                default:
                    Console.Clear();
                    Play();
                    break;
            }
            return false;
        }

        public void DealerHand()
        {
            Random dealerHand = new Random();
            dealerTotal = dealerHand.Next(15, 26);

            if (dealerTotal>21)
            {
                Console.WriteLine($"\nDealer got {dealerTotal} and busted!\nYou won {bet}!");
                playerMoney += 2 * bet;
            }

            if (dealerTotal<=21&&dealerTotal>cardTotal)
            {
                Console.WriteLine($"\nDealer got {dealerTotal} and beat your {cardTotal}\nYou Lose!");
            }

            if (dealerTotal <= 21 && dealerTotal < cardTotal)
            {
                Console.WriteLine($"\nYou got {cardTotal} and beat the dealer's {dealerTotal}\nYou Win {bet}!");
                playerMoney += 2 * bet;
            }

            if (dealerTotal==cardTotal)
            {
                Console.WriteLine($"You got {cardTotal} and the dealer got {dealerTotal}!\n" +
                                  $"Fortunately for you, you win {bet}!");
                playerMoney += 2 * bet;
            }
        }
    }
}
