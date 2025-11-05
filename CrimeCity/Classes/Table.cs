using HelloWold;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeCity.Classes
{
    public class Table()
    {
        public void FillTable()
        {
            for (int i = 0; i < Rows.Length; i++)
            {
                Rows[i] = i;
                Columns[i] = i;
            }
        }

        public static Random Random { get; set; } = new Random();
        public static string Message { get; set; } = string.Empty;
        public static int[] Rows { get; set; } = new int[10];
        public static int[] Columns { get; set; } = new int[10];
        public static int Turn { get; set; }
        public static int[] Borders = new int[38];
        public static int[] RightBorder = new int[10];
        public static int[] LeftBoarder = new int[10];
        public static int[] BottomBorder = new int[10];
        public static int[] TopBoarder = new int[10];

        public void DrawTiles(int positionOnMap, Person player, EdgeRunner edgeRunner, Robber[] robbers)
        {
            if (Config.TestMode)
                Console.Write($"{positionOnMap} ");
            else
            {
                foreach (Robber robber in robbers)
                {
                    if (positionOnMap == robber.Position)
                    {
                        if (Config.ShowRobberNames)
                            Console.Write(robber.FirstName);
                        else
                            Console.Write("Z");
                        return;
                    }
                }
                if (positionOnMap == player.Position && player.Life > 0)
                    Console.Write("*");
                else if (positionOnMap == player.Position && player.Life == 0)
                    Console.Write("X");
                else if (positionOnMap == edgeRunner.Position)
                    Console.Write("%");
                else if (positionOnMap == Money.Position && Money.Amount > 0)
                    Console.Write("$");
                else
                    Console.Write(".");
            }
        }

        public void Draw(Person player, EdgeRunner edgeRunner, Robber[] robbers)
        {
            int borderIndex = 0;
            int leftBorderIndex = 0;
            int rightBorderIndex = 0;
            int topBorderIndex = 0;
            int bottomBorderIndex = 0;

            int count = 0;
            for (int a = 0; a < Rows.Length; a++)
            {
                bool markBorder = false;
                bool firstRows = false;
                bool lastRows = false;
                if (Rows.Last() == a || Rows.First() == a)
                    markBorder = true;
                if (Rows.First() == a)
                    firstRows = true;
                if (Rows.Last() == a)
                    lastRows = true;
                for (int b = 0; b < Columns.Length; b++)
                {
                    int positionOnMap = count + b;
                    if (markBorder || Columns.First() == b || Columns.Last() == b)
                    {
                        Borders[borderIndex++] = positionOnMap;
                    }
                    if (Columns.First() == b)
                    {
                        LeftBoarder[leftBorderIndex++] = positionOnMap;
                    }
                    if (Columns.Last() == b)
                    {
                        RightBorder[rightBorderIndex++] = positionOnMap;
                    }
                    if (firstRows)
                    {
                        TopBoarder[topBorderIndex++] = positionOnMap;
                    }
                    if (lastRows)
                    {
                        BottomBorder[bottomBorderIndex++] = positionOnMap;
                    }
                    DrawTiles(positionOnMap, player, edgeRunner, robbers);
                }
                count += 10;
                Console.WriteLine();
            }
        }

        public static void Stats(Person player, Person[] robbers)
        {
            Console.WriteLine($"Cash: {player.BankAccount}$$");
            //Console.WriteLine($"Location: {player.Position}");
            Console.WriteLine($"Bonus: {Money.Amount}$$");
            Console.WriteLine($"Turns: {Turn}");
            Console.WriteLine($"Player Coordinate: (X:{player.Coordinate.X}, Y:{player.Coordinate.Y})");
            foreach (Person robber in robbers)
            {
                Console.Write($"Robber {robber.FirstName} (X:{robber.Coordinate.X}, Y:{robber.Coordinate.Y})");
                if (robber != robbers.Last())
                    Console.WriteLine();
                else
                    Console.WriteLine();
            }

            foreach (Person robber in robbers)
            {
                if (Config.TestMode)
                    Console.WriteLine($"Robber: {robber.Position}");
            }

            Console.WriteLine($"Life: {player.Life}");
            string borders = string.Empty;
            for (int i = 0; i < Table.Borders.Length; i++)
            {
                borders += Table.Borders[i];
                if (Config.TestMode)
                {
                    Console.WriteLine();
                }
            }

            string leftBorders = string.Empty;
            for (int i = 0; i < LeftBoarder.Length; i++)
            {
                leftBorders += LeftBoarder[i];
                if (Config.TestMode)
                {
                    Console.WriteLine(leftBorders);
                }
            }
        }
    }

}
