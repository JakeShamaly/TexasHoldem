using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldem
{
    public class Game
    {
        public Game(List<Player> Players, List<Card> flop, List<Card> river, List<Card> turn)
        {
            this.Pot = 0;
            this.Players = Players;
            this.Flop = flop;
            this.River = river;
            this.Turn = turn;
            //this.Check = false;
        }

        public int Pot { get; set; }
        public List<Player> Players { get; set; }
        List<Card> Flop { get; set; }
        List<Card> River { get; set; }      // Maybe change River and Turn to be Cards in later version to slightly improve efficiency.
        List<Card> Turn { get; set; }
        //public bool Check { get; set; } 

        /*
        The functions Anti, PostFlop, PostRiver, and PortTurn are all almost identicle.
        Anti will be fully documented, the rest will only have differences from anti documented.
        */
        public void Anti()  // First round of betting.
        {
            //Console.WriteLine("Anti is $5");
            int Bet = 5;            // Initialized to $5 for first round only.
            string response;        // Used to determine what the player wants to do in a ReadLine().
            bool contin = false;    // Used later to determine if the round of betting needs to continue.
            bool Raised = false;
            while (true)
            {
                contin = false;
                foreach (Player p in this.Players)
                {
                    if (((p.InPot >= Bet) && (Raised)) || (p.Posit == "Fold"))      // To see if player gets to play their turn.
                    {
                        continue;
                    }
                    Console.Clear();
                    Console.Write($"{p.Name}'s turn, hit enter to start: ");        // This ensures players only can see their own cards.
                    Console.ReadLine();
                    foreach(Card k in p.Hand)
                    {
                        Console.Write($"{k.Rank}{k.Suit} ");
                    }
                    Console.WriteLine($"\n{p.Name}, ${Bet - p.InPot} to you. Pot is {this.Pot} and your balance is {p.Cash} \n[Check/Call, Raise, or Fold]");
                    response = Console.ReadLine();

                    switch (response)
                    {
                        case "Check":       // In this version Checking with a bet assumes a Call.
                            p.Cash -= Bet - p.InPot;
                            Pot += Bet - p.InPot;   
                            p.InPot += Bet - p.InPot;
                            break;
                        case "Call":        // Takes money from player and transfers to Pot. Then it keeps track of how much it has put in this round.
                            p.Cash -= Bet - p.InPot;
                            Pot += Bet - p.InPot;
                            p.InPot += Bet - p.InPot;
                            break;
                        case "Raise":       // Raise and Reraise under one word. Sets raised flag and raises the bet. Condition at begining of foreach will not hit for other players that only Called/Checked.
                            Raised = true;
                            Console.WriteLine("How much would you like to Raise the Bet by (a posative integer)");
                            int pRaise = Convert.ToInt32(Console.ReadLine());
                            Bet += pRaise;
                            p.Cash -= Bet - p.InPot;
                            Pot += Bet - p.InPot;
                            p.InPot += Bet - p.InPot;
                            break;
                        case "Fold":        // Player position set to Fold. This will take them out of the game.
                            p.Posit = "Fold";
                            break;
                        default:            // Will implement retrying input in future but for now it will auto Fold.
                            Console.WriteLine("You automatically fold for not putting in a valid response. \n(Later version will allow for trying again)");
                            p.Posit = "Fold";
                            break;
                    }
                }

                foreach (Player c in Players)   // If every player either Folded or has put in the amount of money owed, the round will end.
                {
                    Console.WriteLine($"The Bet is {Bet} and {c.Name} has put in {c.InPot}. The balance is {c.Cash} Pot: {Pot}");
                    if ((c.InPot != Bet) && (c.Posit != "Fold"))
                    {
                        contin = true;
                    }
                }

                if (!contin)                    // Ends the round.
                {
                    Bet = 0;
                    foreach (Player b in Players)   // Resets values for next round.
                    {
                        b.InPot = 0;
                        Raised = false;
                    }
                    break;
                }
            }
        }

        public void PostFlop()
        {
            int Bet = 0;
            string response;
            bool contin = false;
            bool Raised = false;
            while (true)
            {
                contin = false;
                foreach (Player p in this.Players)
                {
                    if (((p.InPot >= Bet) && (Raised)) || (p.Posit == "Fold"))
                    {
                        continue;
                    }
                    Console.Clear();
                    Console.Write($"{p.Name}'s turn, hit enter to start: ");
                    Console.ReadLine();
                    Console.Write("\n===============\n");
                    foreach (Card c in Flop)
                    {
                        Console.Write($"{c.Rank}{c.Suit} ");
                    }
                    Console.Write("\n===============\n\n");
                    foreach (Card k in p.Hand)
                    {
                        Console.Write($"{k.Rank}{k.Suit} ");
                    }
                    Console.WriteLine($"\n\n{p.Name}, ${Bet - p.InPot} to you. Pot is {this.Pot} and your balance is {p.Cash} \n[Check/Call, Raise, or Fold]");
                    response = Console.ReadLine();

                    switch (response)
                    {
                        case "Check":
                            p.Cash -= Bet - p.InPot;
                            Pot += Bet - p.InPot;
                            p.InPot += Bet - p.InPot;
                            break;
                        case "Call":
                            p.Cash -= Bet - p.InPot;
                            Pot += Bet - p.InPot;
                            p.InPot += Bet - p.InPot;
                            break;
                        case "Raise":
                            Raised = true;
                            Console.WriteLine("How much would you like to Raise the Bet by (a posative integer)");
                            int pRaise = Convert.ToInt32(Console.ReadLine());
                            Bet += pRaise;
                            p.Cash -= Bet - p.InPot;
                            Pot += Bet - p.InPot;
                            p.InPot += Bet - p.InPot;
                            break;
                        case "Fold":
                            p.Posit = "Fold";
                            break;
                        default:
                            Console.WriteLine("You automatically fold for not putting in a valid response. \n(Later version will allow for trying again)");
                            p.Posit = "Fold";
                            break;
                    }
                }

                foreach (Player c in Players)
                {
                    Console.WriteLine($"The Bet is {Bet} and {c.Name} has put in {c.InPot}. The balance is {c.Cash} Pot: {Pot}");
                    if ((c.InPot != Bet) && (c.Posit != "Fold"))
                    {
                        contin = true;
                    }
                }

                if (!contin)
                {
                    Bet = 0;
                    foreach (Player b in Players)
                    {
                        b.InPot = 0;
                        Raised = false;
                    }
                    break;
                }
            }
        }

        public void PostRiver()
        {
            int Bet = 0;
            string response;
            bool contin = false;
            bool Raised = false;
            while (true)
            {
                contin = false;
                foreach (Player p in this.Players)
                {
                    if (((p.InPot >= Bet) && (Raised)) || (p.Posit == "Fold"))
                    {
                        continue;
                    }
                    Console.Clear();
                    Console.Write($"{p.Name}'s turn, hit enter to start: ");
                    Console.ReadLine();
                    Console.Write("\n===============\n");
                    foreach (Card c in Flop)
                    {
                        Console.Write($"{c.Rank}{c.Suit} ");
                    }
                    Console.Write($"{River[0].Rank}{River[0].Suit}");
                    Console.Write("\n===============\n\n");
                    foreach (Card k in p.Hand)
                    {
                        Console.Write($"{k.Rank}{k.Suit} ");
                    }
                    Console.WriteLine($"\n\n{p.Name}, ${Bet - p.InPot} to you. Pot is {this.Pot} and your balance is {p.Cash} \n[Check/Call, Raise, or Fold]");
                    response = Console.ReadLine();

                    switch (response)
                    {
                        case "Check":
                            p.Cash -= Bet - p.InPot;
                            Pot += Bet - p.InPot;
                            p.InPot += Bet - p.InPot;
                            break;
                        case "Call":
                            p.Cash -= Bet - p.InPot;
                            Pot += Bet - p.InPot;
                            p.InPot += Bet - p.InPot;
                            break;
                        case "Raise":
                            Raised = true;
                            Console.WriteLine("How much would you like to Raise the Bet by (a posative integer)");
                            int pRaise = Convert.ToInt32(Console.ReadLine());
                            Bet += pRaise;
                            p.Cash -= Bet - p.InPot;
                            Pot += Bet - p.InPot;
                            p.InPot += Bet - p.InPot;
                            break;
                        case "Fold":
                            p.Posit = "Fold";
                            break;
                        default:
                            Console.WriteLine("You automatically fold for not putting in a valid response. \n(Later version will allow for trying again)");
                            p.Posit = "Fold";
                            break;
                    }
                }

                foreach (Player c in Players)
                {
                    Console.WriteLine($"The Bet is {Bet} and {c.Name} has put in {c.InPot}. The balance is {c.Cash} Pot: {Pot}");
                    if ((c.InPot != Bet) && (c.Posit != "Fold"))
                    {
                        contin = true;
                    }
                }

                if (!contin)
                {
                    Bet = 0;
                    foreach (Player b in Players)
                    {
                        b.InPot = 0;
                        Raised = false;
                    }
                    break;
                }
            }
        }

        public void PostTurn()
        {
            int Bet = 0;
            string response;
            bool contin = false;
            bool Raised = false;
            while (true)
            {
                contin = false;
                foreach (Player p in this.Players)
                {
                    if (((p.InPot >= Bet) && (Raised)) || (p.Posit == "Fold"))
                    {
                        continue;
                    }
                    Console.Clear();
                    Console.Write($"{p.Name}'s turn, hit enter to start: ");
                    Console.ReadLine();
                    Console.Write("\n===============\n");
                    foreach (Card c in Flop)
                    {
                        Console.Write($"{c.Rank}{c.Suit} ");
                    }
                    Console.Write($"{River[0].Rank}{River[0].Suit} {Turn[0].Rank}{Turn[0].Suit}");
                    Console.Write("\n===============\n\n");
                    foreach (Card k in p.Hand)
                    {
                        Console.Write($"{k.Rank}{k.Suit} ");
                    }
                    Console.WriteLine($"\n\n{p.Name}, ${Bet - p.InPot} to you. Pot is {this.Pot} and your balance is {p.Cash} \n[Check/Call, Raise, or Fold]");
                    response = Console.ReadLine();

                    switch (response)
                    {
                        case "Check":
                            p.Cash -= Bet - p.InPot;
                            Pot += Bet - p.InPot;
                            p.InPot += Bet - p.InPot;
                            break;
                        case "Call":
                            p.Cash -= Bet - p.InPot;
                            Pot += Bet - p.InPot;
                            p.InPot += Bet - p.InPot;
                            break;
                        case "Raise":
                            Raised = true;
                            Console.WriteLine("How much would you like to Raise the Bet by (a posative integer)");
                            int pRaise = Convert.ToInt32(Console.ReadLine());
                            Bet += pRaise;
                            p.Cash -= Bet - p.InPot;
                            Pot += Bet - p.InPot;
                            p.InPot += Bet - p.InPot;
                            break;
                        case "Fold":
                            p.Posit = "Fold";
                            break;
                        default:
                            Console.WriteLine("You automatically fold for not putting in a valid response. \n(Later version will allow for trying again)");
                            p.Posit = "Fold";
                            break;
                    }
                }

                foreach (Player c in Players)
                {
                    Console.WriteLine($"The Bet is {Bet} and {c.Name} has put in {c.InPot}. The balance is {c.Cash}. Pot: {Pot}");
                    if ((c.InPot != Bet) && (c.Posit != "Fold"))
                    {
                        contin = true;
                    }
                }

                if (!contin)
                {
                    Bet = 0;
                    foreach (Player b in Players)
                    {
                        b.InPot = 0;
                        Raised = false;
                    }
                    break;
                }
            }
        }

        public void EndStats()  // Prints player's cards. In later versions it will determine who won.
        {
            foreach (Player p in Players)
            {
                List<Card> tot = new List<Card>();
                tot.AddRange(p.Hand);
                tot.AddRange(Flop);                         // Adding together all the cards
                tot.AddRange(River);
                tot.AddRange(Turn);
                Console.Write($"\n{p.Name} has ");
                foreach (Card c in tot)
                {
                    Console.Write($"{c.Rank}{c.Suit} ");
                }
                Console.Write("\n");
            }
        }

        public void DispCard()  // Prints out rank and suit.
        {
            //This will replace print statements in the future so that 12 prints as king and so on.
        }



    }


}
