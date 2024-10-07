using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desszert
{
    internal class Dessert
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Awarded { get; set; }
        public int Price { get; set; }
        public string Amount { get; set; }

        public Dessert(string sor) {
            var i = sor.Split(';');
            Name = i[0];
            Type = i[1];
            Awarded = bool.Parse(i[2]);
            Price = int.Parse(i[3]);
            Amount = i[4];
        }

        public Dessert(string nev, string tipus, bool dijazott, int ar, string mertekegyseg) {
            Name = nev;
            Type = tipus;
            Awarded = dijazott;
            Price = ar;
            Amount = mertekegyseg;
        }
    }
}
