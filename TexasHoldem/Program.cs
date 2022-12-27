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

            //Console.WriteLine($"Jake has    {Jake.Hand[0].Rank}{Jake.Hand[0].Suit} and {Jake.Hand[1].Rank}{Jake.Hand[1].Suit}\nAnt has     {Ant.Hand[0].Rank}{Ant.Hand[0].Suit} and {Ant.Hand[1].Rank}{Ant.Hand[1].Suit} \nJared has   {Jared.Hand[0].Rank}{Jared.Hand[0].Suit} and {Jared.Hand[1].Rank}{Jared.Hand[1].Suit}");

            Game Test = new Game(Players, gameDeck.Flop, gameDeck.River, gameDeck.Turn);

            Test.Anti();

            Test.PostFlop();

            Test.PostRiver();

            Test.PostTurn();

            Test.EndStats();
        }
    }
}
