// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldem
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck gameDeck = new Deck();
            gameDeck.start();
            gameDeck.shuffle();

            Player Jake = new Player("Jake", 500);
            Player Ant = new Player("Ant", 500);
            Player Jared = new Player("Jared", 500);
            List<Player> Players = new List<Player> { Jake, Ant, Jared};

            gameDeck.deal(Players);

            Game Test = new Game(Players, gameDeck.Flop, gameDeck.River, gameDeck.Turn);

            Test.Anti();

            Test.PostFlop();

            Test.PostRiver();

            Test.PostTurn();

            Test.EndStats();
        }
    }
}
