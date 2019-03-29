using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class App
    {
        int cardOne, cardTotal, dealerTotal, bet;
        string suit, holdHit;
        int playerMoney = 100000;
        readonly Random card = new Random();

        public int PlayerMoney { get => playerMoney; set => playerMoney = value; }
        public int CardOne { get => cardOne; set => cardOne = value; }
        public int CardTotal { get => cardTotal; set => cardTotal = value; }
        public string Suit { get => suit; set => suit = value; }
        public int Bet1 { get => bet; set => bet = value; }
        public string HoldHit1 { get => holdHit; set => holdHit = value; }
        public int DealerTotal { get => dealerTotal; set => dealerTotal = value; }

        public Random Card => card;

        public bool DealCards()
        {
            Console.Clear();

            Bet();

            PlayerMoney -= Bet1;

            int x = 1;
            while (x < 6)
            {
                bool test = true;
                while (test)
                {
                    InitialDeal(Card);

                    if (CardTotal > 21)
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
            CardOne = CreateCards(card);

            Console.WriteLine("Dealing Cards:\n");

            Console.Write($"You received a {CardOne} of {Suit}\n");
            CardTotal += CardOne;
            Console.WriteLine($"Your total: {CardTotal}");
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
                    Suit = "Clubs";
                    break;
                case 2:
                    Suit = "Spades";
                    break;
                case 3:
                    Suit = "Hearts";
                    break;
                case 4:
                    Suit = "Diamonds";
                    break;
            }
        }

        private void HoldHit(ref int x, ref bool test)
        {
            bool hold = true;
            while (hold)
            {
                Console.WriteLine("\nHold or Hit?");
                HoldHit1 = Console.ReadLine();

                switch (HoldHit1)
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
                        Console.WriteLine($"You have: {CardTotal}");
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

            Console.WriteLine($"Your money: {PlayerMoney}\n");

            try
            {
                Console.WriteLine("Place your bet!");
                Bet1 = Convert.ToInt32(Console.ReadLine());
                
                if (Bet1>PlayerMoney)
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

            if (PlayerMoney == 0)
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
                    CardTotal = 0;
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
            DealerTotal = dealerHand.Next(15, 26);

            if (DealerTotal>21)
            {
                Console.WriteLine($"\nDealer got {DealerTotal} and busted!\nYou won {Bet1}!");
                PlayerMoney += 2 * Bet1;
            }

            if (DealerTotal<=21&&DealerTotal>CardTotal)
            {
                Console.WriteLine($"\nDealer got {DealerTotal} and beat your {CardTotal}\nYou Lose!");
            }

            if (DealerTotal <= 21 && DealerTotal < CardTotal)
            {
                Console.WriteLine($"\nYou got {CardTotal} and beat the dealer's {DealerTotal}\nYou Win {Bet1}!");
                PlayerMoney += 2 * Bet1;
            }

            if (DealerTotal==CardTotal)
            {
                Console.WriteLine($"You got {CardTotal} and the dealer got {DealerTotal}!\n" +
                                  $"Fortunately for you, you win {Bet1}!");
                PlayerMoney += 2 * Bet1;
            }
        }
    }
}
