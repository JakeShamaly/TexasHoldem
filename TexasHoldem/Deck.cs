using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldem
{
    public class Deck
    {
        public Deck()
        {
            this.isShuff = false;
            this.Dek = new List<Card>();
            this.Flop = new List<Card>();
            this.River = new List<Card>();
            this.Turn = new List<Card>();
            this.Index = 0;
            this.suits = new string[] { "S", "H", "C", "D" };
        }
        public int Index { get; set; }
        public bool isShuff { get; set; }
        public List<Card> Dek { get; set; }
        public List<Card> Flop { get; set; }
        public List<Card> River { get; set; }
        public List<Card> Turn { get; set; }
        public string[] suits { get; set; }
        public void start()
        {
            Dek.Clear();
            isShuff = false;
            Card insert;
            foreach(string j in suits) 
            {
                for(int i = 1; i <= 13; i++)
                {
                    insert = new Card(i, j);
                    Dek.Add(insert);
                }
            }
        }

        public static void swap(List<Card> list, int indexA, int indexB) 
        {
            if (indexA != indexB)
            {
                Card tmp = list[indexA];
                list[indexA] = list[indexB];
                list[indexB] = tmp;
            }
        }
        public void shuffle()
        {
            Random rnd1 = new Random();
            Random rnd2 = new Random();
            isShuff = true;
            for (int i = 0; i < 50000; i++)
            {
                swap(Dek, rnd1.Next(0, 52), rnd1.Next(0, 52));
            }

        }

        public void deal(List<Player> players)
        {
            this.shuffle();

            foreach(Player f in players)
            {
                f.Hand.Clear();
                f.Hand.Add(this.Dek[Index]);
                Index++;
                f.Hand.Add(this.Dek[Index]);
                Index++;
            }

            Flop.Clear();
            Flop.Add(this.Dek[Index]);
            Index++;
            Flop.Add(this.Dek[Index]);
            Index++;
            Flop.Add(this.Dek[Index]);
            Index++;

            River.Clear();
            River.Add(this.Dek[Index]);
            Index++;

            Turn.Clear();
            Turn.Add(this.Dek[Index]);
            Index++;

        }
    }
    
    public class Card
    {
        public Card(int rank,string suit)
        {
            this.Rank = rank;
            this.Suit = suit;
        }

        public int Rank { get; set; }
        public string Suit { get; set; }

    }
}
