using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeCity.Classes
{
    public static class Config
    {
        public static string[] LastNames = ["Smasher", "Numonic", "Tom", "Crashout", "Hard"];
        public static string[] FirstNames = ["Johnny", "Atom", "Trapper", "Max", "Susan"];
        public static int[] ValidProximities = [-11, -10, -9, -1, 0, 1, 9, 10, 11];
        public static string[,] Weapons = { { "axe", "500" }, { "knife", "300" }, { "bat", "150" }, { "fist", "50" } };
        public static bool TestMode = false;
    }
}
