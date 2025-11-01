//Disclaimer: The creator of 'C# Shell (C# Offline Compiler)' is in no way responsible for the content posted by any user.
using HelloWold;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using CrimeCity.Classes;
using CrimeCity.Helpers;

namespace HelloWold;

public class Program
{
    public static void Main(string[] args)
    {
        Table table = new Table();
        table.FillTable();
        Person player = new Person();
        EdgeRunner edgeRunner = new EdgeRunner();
        int noOfRobbers = 5;

        Robber[] robbers = new Robber[noOfRobbers];
        for (int i = 0; i < robbers.Length; i++)
        {
            robbers[i] = new Robber() { FirstName = $"{i + 1}" };
        }

        string input = string.Empty;

        while (input != "l" && player.Life > 0)
        {
            ++Table.Turn;
            Money.Draw();

            foreach (Robber robber in robbers)
            {
                robber.Move(table, player, null, robbers);
                robber.Attack(player);
            }
            Table.Stats(player, robbers);
            table.Draw(player, edgeRunner, robbers);

            Console.WriteLine(Table.Message);

            Table.Message = "";

            input = player.Input(table, player, edgeRunner, robbers);
            Console.Clear();

            ExLogger.Log("", true);

            Console.WriteLine("Old Moves:");
            table.Draw(player, edgeRunner, robbers);
            Console.WriteLine();

            //ExLogger.Display();
            ExLogger.Clear();
        }
    }
}












