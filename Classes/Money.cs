using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeCity.Classes
{
    public static class Money
    {
        public static int Position { get; set; } = Table.Random.Next(1, 100);
        public static int Amount { get; set; } = Table.Random.Next(1, 5000);
        public static void Draw()
        {
            if (Amount == 0 && Table.Turn == Table.Random.Next(1, Table.Turn * 2))
            {
                Position = Table.Random.Next(1, 100);
                Amount = Table.Random.Next(1, 5000);
            }
        }
    }
}
