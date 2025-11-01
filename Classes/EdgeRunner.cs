using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeCity.Classes
{
    public class EdgeRunner : Person
    {
        public EdgeRunner()
        {
            Position = Table.Random.Next(1, 100);
            FirstName = Config.FirstNames[Table.Random.Next(0, Config.FirstNames.Length)];
            LastName = Config.LastNames[Table.Random.Next(0, Config.LastNames.Length)];
            Price = Table.Random.Next(100, 10001);
        }

        public int Price { get; set; }
        public int HackingSkill { get; set; }
        public string Hack()
        {
            return "performing hack...\n";
        }
        public override string ToString()
        {
            return $"{FirstName} {LastName} is here to kick ass!";
        }
    }

}
