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
            if (Table.Turn == 1)
            {
                GenerateBag();
            }
            else if (Table.Random.Next(1, 20) == 1 && Position == -1)
            {
                GenerateBag();
            }
        }
        public static void GenerateBag()
        {
            Position = Table.Random.Next(1, 100);
            Amount = Table.Random.Next(1, Config.Wallet);
        }
    }
}
