using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeCity.Classes
{
    public static class Config
    {
        public static string[] LastNames = ["Smasher", "Numonic", "Tom", "Crashout", "Hard", "Woadie", "Gabriels"];
        public static string[] FirstNames = ["Johnny", "Atom", "Trapper", "Max", "Susan", "Little", "Nick"];
        public static int[] ValidProximities = [-11, -10, -9, -1, 0, 1, 9, 10, 11];
        public static string[,] Weapons = { { "axe", "500" }, { "knife", "300" }, { "bat", "150" }, { "fist", "50" } };
        public static bool TestMode = false;
        public static bool MoveMentTest = true;
        public static bool ShowRobberNames = true;
        public static int RobberSpeed = 1; // value 1-5 lower is faster
        public static int Wallet = Table.Random.Next(0, 10000);
        public static int LifePoints = 5000000;
        public static int NoOfRobbers = 8;
        public static int SpeedDuringMoveTest = 100; //in milliseconds
    }
}
