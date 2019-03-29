using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class Intro
    {
        public static void IntroToGame()
        {
            Console.WriteLine("Welcome to a simple BlackJack style game. The rules of the game are:\n\n" +
                              "\t1. You start by betting\n" +
                              "\t2. You will receive one card\n" +
                              "\t3. You can decide to hit or hold\n" +
                              "\t4. If you hit you will receive another card\n" +
                              "\t5. If you get over 21 you lose\n" +
                              "\t6. If you hold, your value will be compared to the dealers\n" +
                              "\t7. If you win, well, you win and vis versa\n" +
                              "\t8. The goal is to make $1,000,000\n\n" +
                              "Good Luck!\n\n" +
                              "Press any key to continue...");
            Console.ReadKey();
        }
    }
}
