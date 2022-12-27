using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldem
{
    public class Player
    {
        public Player(string name, int cash)
        {
            this.Cash = cash;
            this.Hand = new List<Card>();
            this.Posit = "In";
            this.Name = name;
            this.InPot = 0;
        }

        public string Name { get; set; }
        public string Posit { get; set; }
        public int Cash { get; set; }
        public List<Card> Hand { get; set; }
        public int InPot { get; set;}
    }
}
