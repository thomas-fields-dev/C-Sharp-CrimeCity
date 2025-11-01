//Disclaimer: The creator of 'C# Shell (C# Offline Compiler)' is in no way responsible for the content posted by any user.
using HelloWold;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;

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

            Debugger.Log("", true);
            table.Draw(player, edgeRunner, robbers);
            Debugger.Display();
            Debugger.Clear();
        }
    }
}

public class Person
{
    public Person()
    {
        BankAccount = Table.Random.Next(1, 20001);
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Position { get; set; }
    public int BankAccount { get; set; }
    public int Life { get; set; } = 10000000;
    public Coordinate Coordinate { get; set; }

    public string Input(Table table, Person player, EdgeRunner edgeRunner, Robber[] robbers)
    {
        if (Life > 0)
            Console.WriteLine("Whats your move? q,w,e,a,s,d,z,x,c,i\nor l to leave");

        string input = Console.ReadLine();
        Move(table, player, edgeRunner, robbers, input);
        return input;
    }

    public virtual void Move(Table table, Person player, EdgeRunner edgeRunner = null, Robber[] robbers = null, string input = "", int proximity = 0)
    {
        int[] robbersPositions = new int[robbers.Length];
        for (int i = 0; i < robbers.Length; i++)
            robbersPositions[i] = robbers[i].Position;

        switch (input)
        {
            case "q":
                if (Position - 11 == edgeRunner.Position ||
                Table.LeftBoarder.Contains(Position) && Table.RightBorder.Contains(Position - 11) ||
                Table.RightBorder.Contains(Position) && Table.LeftBoarder.Contains(Position - 11) ||
                Position - 11 < 0 || robbersPositions.Contains(Position - 11))
                    Table.Message = "You cant move there!\n";
                else
                    Position -= 11;
                break;
            case "w":
                if (Position - 10 == edgeRunner.Position || Position - 10 < 0 || robbersPositions.Contains(Position - 10))
                    Table.Message = "You cant move there!\n";
                else
                    Position -= 10;
                break;
            case "e":
                if (Position - 9 == edgeRunner.Position ||
                Table.LeftBoarder.Contains(Position) && Table.RightBorder.Contains(Position - 9) ||
                Table.RightBorder.Contains(Position) && Table.LeftBoarder.Contains(Position - 9) ||
               Position - 9 < 0 || robbersPositions.Contains(Position - 9))
                    Table.Message = "You cant move there!\n";
                else
                    Position -= 9;
                break;
            case "a":
                if (Position - 1 == edgeRunner.Position ||
                Table.LeftBoarder.Contains(Position) && Table.RightBorder.Contains(Position - 1) ||
                Table.RightBorder.Contains(Position) && Table.LeftBoarder.Contains(Position - 1) ||
               Position - 1 < 0 || robbersPositions.Contains(Position - 1))
                    Table.Message = "You cant move there!\n";
                else
                    Position -= 1;
                break;
            case "s":
                if (Position - 0 != edgeRunner.Position)
                    Position += 0;
                else
                    Table.Message = "You cant move there!\n";
                break;
            case "d":
                if (Position + 1 == edgeRunner.Position ||
                Table.LeftBoarder.Contains(Position) && Table.RightBorder.Contains(Position + 1) ||
                Table.RightBorder.Contains(Position) && Table.LeftBoarder.Contains(Position + 1) ||
               Position + 1 > 99 || robbersPositions.Contains(Position + 1))
                    Table.Message = "You cant move there!\n";
                else
                    Position += 1;
                break;
            case "z":
                if (Position + 9 == edgeRunner.Position ||
                Table.LeftBoarder.Contains(Position) && Table.RightBorder.Contains(Position + 9) ||
                Table.RightBorder.Contains(Position) && Table.LeftBoarder.Contains(Position + 9) ||
               Position + 9 > 99 || robbersPositions.Contains(Position + 9))
                    Table.Message = "You cant move there!\n";
                else
                    Position += 9;
                break;
            case "x":
                if (Position + 10 == edgeRunner.Position ||
               Position + 10 > 99 || robbersPositions.Contains(Position + 10))
                    Table.Message = "You cant move there!\n";
                else
                    Position += 10;
                break;
            case "c":
                if (Position + 11 == edgeRunner.Position ||
                Table.LeftBoarder.Contains(Position) && Table.RightBorder.Contains(Position + 11) ||
                Table.RightBorder.Contains(Position) && Table.LeftBoarder.Contains(Position + 11) ||
               Position + 11 > 99 || robbersPositions.Contains(Position + 11))
                    Table.Message = "You cant move there!\n";
                else
                    Position += 11;
                break;
            case "i":
                // add interaction
                proximity = edgeRunner.Position - Position;

                if (Config.ValidProximities.Contains(proximity))
                {
                    string hireMe = "";
                    edgeRunner.EdgeRunnerBillboard();
                    Console.WriteLine($"Do you wanna hire {edgeRunner.FirstName}? Enter Y or N");
                    hireMe = Console.ReadLine();
                    switch (hireMe.ToUpper())
                    {
                        case "Y":
                            if (BankAccount > edgeRunner.Price)
                            {
                                Table.Message += edgeRunner.Hack();
                                BankAccount -= edgeRunner.Price;
                            }
                            else
                                Table.Message += $"You cant afford {edgeRunner.FirstName}!\n";
                            break;
                        case "N":
                            Table.Message += $"Fuck you {edgeRunner.FirstName}!\n";
                            break;
                        default:
                            break;
                    }
                }
                else if (Position == Money.Position)
                {
                    BankAccount += Money.Amount;
                    Table.Message = $"You just found {Money.Amount}$$, Lucky Dog!\n";
                    Money.Amount = 0;
                }
                else
                    Table.Message = "You cant do that!\n";
                break;
            default:
                break;
        }

    }
    public virtual void Attack(Person person)
    {
        // not yet implemented
    }
}

public class Robber : Person
{
    public Robber()
    {
        Position = Table.Random.Next(1, 100);
        int randomWeapon = Table.Random.Next(0, Config.Weapons.Length / 2);
        Weapon = new string[,] { { Config.Weapons[randomWeapon, 0], Config.Weapons[randomWeapon, 1] } };
    }
    public string[,] Weapon { get; set; }
    public static int[] RobberPositions { get; set; }

    public override void Move(Table table, Person player, EdgeRunner edgeRunner = null, Robber[] robbers = null, string input = "", int proximity = 0)
    {
        RobberPositions = new int[robbers.Length];
        for (int i = 0; i < robbers.Length; i++)
            RobberPositions[i] = robbers[i].Position;

        if (Table.Turn > 1)
        {
            int playerRow = 0;
            int robberRow = 0;
            int playerCol = 0;
            int robberCol = 0;

            bool firstRow = player.Position < 10;
            if (!firstRow)
                playerRow = int.Parse(player.Position.ToString().Substring(0, 1));

            firstRow = Position < 10;
            if (!firstRow)
                robberRow = int.Parse(Position.ToString().Substring(0, 1));

            bool firstCol = Position < 10;

            if (!firstCol)
                robberCol = int.Parse(Position.ToString().Substring(1, 1));
            else
                robberCol = Position;

            firstCol = player.Position < 10;
            if (!firstCol)
                playerCol = int.Parse(player.Position.ToString().Substring(1, 1));
            else
                playerCol = player.Position;

            player.Coordinate = new Coordinate(playerCol, playerRow);
            Coordinate = new Coordinate(robberCol, robberRow);

            bool moveRobberLeft = player.Coordinate.X < Coordinate.X;
            bool moveRobberRight = player.Coordinate.X > Coordinate.X;
            bool isPlayerAbove = player.Coordinate.Y < Coordinate.Y;
            bool isPlayerBelow = player.Coordinate.Y > Coordinate.Y;
            bool isSameRow = player.Coordinate.Y == Coordinate.Y;
            bool isSameColumn = player.Coordinate.X == Coordinate.X;
            // Console.WriteLine($"isPlayerAbove {isPlayerAbove}, isPlayerBelow {isPlayerBelow}, isPlayerEqual {isPlayerEqual}, moveRobberLeft {moveRobberLeft}, moveRobberRight {moveRobberRight}, isSameColumm: {isSameColumn}");

            int newPosition = 0;

            if (isSameRow && moveRobberLeft)
                newPosition = -1;
            else if (isSameRow && moveRobberRight)
                newPosition = 1;
            else if (isPlayerAbove && isSameColumn)
                newPosition = -10;
            else if (isPlayerBelow && isSameColumn)
                newPosition = 10;
            else if (isPlayerAbove && moveRobberLeft)
                newPosition = -11;
            else if (isPlayerBelow && moveRobberLeft)
                newPosition = 9;
            else if (isPlayerAbove && moveRobberRight)
                newPosition = -9;
            else if (isPlayerBelow && moveRobberRight)
                newPosition = 11;

            if (Table.Turn % 1 != 0)
                newPosition = 0;

            if (Position + newPosition == player.Position)
                newPosition = 0;

            if (newPosition != 0)
            {
                List<int> validPositions = new List<int>();

                if (RobberPositions.Contains(Position + newPosition))
                {
                    Debugger.Log($"Robber {FirstName} ({Coordinate.X}, {Coordinate.Y}): ", true);
                    newPosition = 0;

                    foreach (int pos in Config.ValidProximities)
                    {
                        if (pos != 0 && !RobberPositions.Contains(Position + pos))
                        {
                            validPositions.Add(pos);
                        }
                    }

                    List<int> legalPositionsAroundRobber = CheckBoundrys(validPositions, Position, true);

                    List<int> occupiedAroundPlayer = new List<int>();
                    foreach (int vp in Config.ValidProximities)
                    {
                        foreach (int p in RobberPositions)
                        {
                            if (p == player.Position + vp)
                            {
                                occupiedAroundPlayer.Add(vp);
                            }
                        }
                        occupiedAroundPlayer.Add(0);
                    }
                    occupiedAroundPlayer.ForEach(delegate (int o) { Console.WriteLine(o); });
                    List<int> unoccupiedAroundPlayer = new List<int>();
                    unoccupiedAroundPlayer = Config.ValidProximities.Except(occupiedAroundPlayer.ToArray()).ToList();
                    unoccupiedAroundPlayer.ForEach(delegate (int u) { Console.WriteLine($"unoccupied {u}"); });

                    List<int> legalPositionsAroundPlayer = CheckBoundrys(unoccupiedAroundPlayer, player.Position);

                    string name = FirstName;
                    if (legalPositionsAroundPlayer.Any())
                    {
                        foreach (int l in legalPositionsAroundPlayer)
                        {
                            foreach (int m in legalPositionsAroundRobber)
                            {
                                int movedPlayer = Position + m;
                                int emptyPos = player.Position + l;
                                if (movedPlayer == emptyPos)
                                {
                                    newPosition = m;
                                }
                            }
                        }
                        if (newPosition == 0)
                        {
                            if (!legalPositionsAroundRobber.Contains(-1))
                            {
                                Debugger.Log($"-1", true);
                                if (!isSameRow && !isSameColumn && moveRobberLeft && isPlayerBelow)
                                {
                                    int[] validMoves = [-11, 10];
                                    newPosition = GetRandomValidMove(legalPositionsAroundRobber, validMoves);
                                }
                                else if (!isSameRow && !isSameColumn && moveRobberLeft && isPlayerAbove)
                                {
                                    int[] validMoves = [-11, -10];
                                    newPosition = GetRandomValidMove(legalPositionsAroundRobber, validMoves);
                                 
                                }
                                else if (isSameRow && !isSameColumn && moveRobberLeft)
                                {
                                    int[] validMoves = [9, -10, 10, -11];
                                    newPosition = GetRandomValidMove(legalPositionsAroundRobber, validMoves);
                                }
                            }
                            else if (!legalPositionsAroundRobber.Contains(9))
                            {
                                Debugger.Log($"9", true);
                                if (!isSameRow && !isSameColumn && moveRobberLeft && isPlayerBelow)
                                {
                                    int[] validMoves = [-1, 10, 11];
                                    newPosition = GetRandomValidMove(legalPositionsAroundRobber, validMoves);
                                 
                                }
                                else if (!isSameRow && !isSameColumn && moveRobberRight && isPlayerBelow)
                                {
                                    if (legalPositionsAroundRobber.Contains(-1))
                                    {
                                        newPosition = -1;
                                    }
                                }
                            }
                            else if (!legalPositionsAroundRobber.Contains(10))
                            {
                                Debugger.Log($"10", true);
                                if (!isSameRow && !isSameColumn && moveRobberRight && isPlayerBelow)
                                {
                                    int[] validMoves = [9, 1];
                                    newPosition = GetRandomValidMove(legalPositionsAroundRobber, validMoves);
                                    
                                }
                                else if (!isSameRow && isSameColumn && isPlayerBelow)
                                {
                                    int[] validMoves = [9, 11, -1];
                                    newPosition = GetRandomValidMove(legalPositionsAroundRobber, validMoves);
                                    
                                }

                            }
                            else if (!legalPositionsAroundRobber.Contains(1))
                            {
                                Debugger.Log($"1", true);
                                if (!isSameRow && !isSameColumn && moveRobberRight && isPlayerAbove)
                                {
                                    int[] validMoves = [11, 10];
                                    newPosition = GetRandomValidMove(legalPositionsAroundRobber, validMoves);
                                }
                                else if (!isSameRow && !isSameColumn && moveRobberRight && isPlayerBelow)
                                {
                                    int[] validMoves = [-9, 10];
                                    newPosition = GetRandomValidMove(legalPositionsAroundRobber, validMoves);
                                }
                                else if (!isSameRow && !isSameColumn && moveRobberLeft && isPlayerBelow)
                                {
                                    int[] validMoves = [-9, 10];
                                    newPosition = GetRandomValidMove(legalPositionsAroundRobber, validMoves);
                                }
                                else if (isSameRow && !isSameColumn && moveRobberRight)
                                {
                                    if (legalPositionsAroundRobber.Contains(10))
                                    {
                                        newPosition = 10;
                                    }
                                }
                            }
                            else if (!legalPositionsAroundRobber.Contains(-10))
                            {
                                Debugger.Log($"-10", true);
                                if (!isSameRow && isSameColumn && isPlayerAbove)
                                {
                                    int[] validMoves = [-9, 1, -11];
                                    newPosition = GetRandomValidMove(legalPositionsAroundRobber, validMoves);
                                }
                                else if (!isSameRow && !isSameColumn && isPlayerAbove && moveRobberRight)
                                {
                                    if (legalPositionsAroundRobber.Contains(1))
                                    {
                                        newPosition = 1;
                                    }
                                }
                                else if (!isSameRow && !isSameColumn && isPlayerAbove && moveRobberLeft)
                                {
                                    if (legalPositionsAroundRobber.Contains(-9))
                                    {
                                        newPosition = -9;
                                    }
                                }
                            }
                            else if (!legalPositionsAroundRobber.Contains(-11))
                            {
                                Debugger.Log($"-11", true);
                                if (!isSameRow && !isSameColumn && isPlayerAbove && moveRobberLeft)
                                {
                                    int[] validMoves = [-1, -10];
                                    newPosition = GetRandomValidMove(legalPositionsAroundRobber, validMoves);
                                }
                            }
                            else if (!legalPositionsAroundRobber.Contains(11))
                            {
                                Debugger.Log($"11", true);
                            }
                            else if (!legalPositionsAroundRobber.Contains(-9))
                            {
                                Debugger.Log($"-9", true);
                            }
                        }
                    }
                }
            }
            Position += newPosition;
            Debugger.Log($"Robber {FirstName} position: {Position}", true);
        }
    }

    private int GetRandomValidMove(List<int> legalPositions, int[] validMoves)
    {
        List<int> moves = new List<int>();
        legalPositions.ForEach(delegate (int l)
        {
            foreach (int move in validMoves)
            {
                if (l == move)
                {
                    moves.Add(l);
                }
            }
        });
        int rnadomMove = moves[Table.Random.Next(0, moves.Count())];
        return rnadomMove;
    }

    private List<int> CheckBoundrys(List<int> positions, int position, bool useRobberPositions = false)
    {
        int[] Yright = [-9, 1, 11];
        int[] Yleft = [-11, -1, 9];
        int[] Xup = [-11, -10, -9];
        int[] Xdown = [9, 10, 11];
        int[] YHorizontal = [-1, 1];
        int[] XVertical = [-10, 10];

        List<int> legalPositions = new List<int>();
        foreach (var validPosition in positions)
        {
            int newValidPosition = position + validPosition;
            bool upperBound = newValidPosition > 0 && position > -1 && position < 10;
            bool lowerBound = newValidPosition < 99 && position > 89 && position < 100;
            bool inRange = newValidPosition > -1 && newValidPosition < 100;
            bool isBorderPosition = Table.RightBorder.Contains(newValidPosition) || Table.LeftBoarder.Contains(newValidPosition);
            if (upperBound && !isBorderPosition)
            {
                legalPositions.Add(validPosition);
                Debugger.Log($"*2 {validPosition}:{newValidPosition} ", true);
            }
            else if (lowerBound && !isBorderPosition)
            {
                legalPositions.Add(validPosition);
                Debugger.Log($"*2.0 {validPosition}:{position + validPosition} ", true);
            }
            else if (!Yleft.Contains(validPosition) && (Table.LeftBoarder.Contains(position) && inRange))
            {
                legalPositions.Add(validPosition);
                Debugger.Log($"*0 {validPosition}:{position + validPosition} ", true);
            }
            else if (!Yright.Contains(validPosition) && Yleft.Contains(validPosition) && (Table.RightBorder.Contains(position) && inRange))
            {
                legalPositions.Add(validPosition);
                Debugger.Log($"*1 {validPosition}:{position + validPosition}  ", true);
            }
            else if (inRange && !isBorderPosition)
            {
                legalPositions.Add(validPosition);
                Debugger.Log($"*1 {validPosition}:{position + validPosition}  ", true);
            }
            else if (!RobberPositions.Contains(newValidPosition) && useRobberPositions)
            {
                legalPositions.Add(validPosition);
                Debugger.Log($"*1 {validPosition}:{position + validPosition}  ", true);
            }
        }
        return legalPositions;
    }

    public override void Attack(Person person)
    {
        int distanceFromRobber = Position - person.Position;
        if (Config.ValidProximities.Contains(distanceFromRobber))
        {
            int amountStolen = person.BankAccount - Table.Random.Next(0, person.BankAccount);
            person.BankAccount -= amountStolen;
            Table.Message += $"You just got robbed by Robber {FirstName} for {amountStolen}$$!\n";

            int damage = Table.Random.Next(0, int.Parse(Weapon[0, 1]));
            person.Life -= damage;

            Table.Message += $"You were beaten with an {Weapon[0, 0]} by Robber {FirstName} for {damage}!\n";

            if (person.Life <= 0)
            {
                person.Life = 0;
                Table.Message += "\nYou were found dead!\n";
            }
        }
    }
}

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

public static class Billboard
{
    public static void NiteCityBillboard()
    {
        Console.WriteLine("Welcome To Nite City!");
    }

    public static void EdgeRunnerBillboard(this EdgeRunner edgeRunner)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(edgeRunner);
        sb.Append("\n");
        sb.Append($"Hire {edgeRunner.FirstName} for {edgeRunner.Price}$$");
        sb.Append("\n");
        Console.WriteLine(sb.ToString());
    }

    public static void NewInTown()
    {
        Console.WriteLine("New in town huh?");
    }
}

public class Table()
{
    public void FillTable()
    {
        for (int i = 0; i < Rows.Length; i++)
        {
            Rows[i] = i;
            Columns[i] = i;
            Console.Write(Rows[i]);
        }
        Console.WriteLine();
    }

    public static Random Random { get; set; } = new Random();
    public static string Message { get; set; } = string.Empty;
    public static int[] Rows { get; set; } = new int[10];
    public static int[] Columns { get; set; } = new int[10];
    public static int Turn { get; set; }
    public static int[] Borders = new int[38];
    public static int[] RightBorder = new int[10];
    public static int[] LeftBoarder = new int[10];

    public void DrawTiles(int positionOnMap, Person player, EdgeRunner edgeRunner, Robber[] robbers)
    {
        if (Config.TestMode)
            Console.Write(positionOnMap);
        else
        {
            foreach (Robber robber in robbers)
            {
                if (positionOnMap == robber.Position)
                {
                    if (!Config.TestMode)
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
                Console.Write("•");
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

        int count = 0;
        for (int a = 0; a < Rows.Length; a++)
        {
            bool markBorder = false;

            if (Rows.Last() == a || Rows.First() == a)
                markBorder = true;
            for (int b = 0; b < Columns.Length; b++)
            {
                int positionOnMap = count + b;
                if (markBorder || Columns.First() == b || Columns.Last() == b)
                    Borders[borderIndex++] = positionOnMap;
                if (Columns.First() == b)
                    LeftBoarder[leftBorderIndex++] = positionOnMap;
                if (Columns.Last() == b)
                    RightBorder[rightBorderIndex++] = positionOnMap;
                DrawTiles(positionOnMap, player, edgeRunner, robbers);
            }
            count += 10;
            Console.WriteLine();
        }
    }

    public static void Stats(Person player, Person[] robbers)
    {
        Billboard.NiteCityBillboard();
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

public struct Coordinate
{
    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }
    public int X { get; }
    public int Y { get; }
}

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

public static class Config
{
    public static string[] LastNames = ["Smasher", "Numonic", "Tom", "Crashout", "Hard"];
    public static string[] FirstNames = ["Johnny", "Atom", "Trapper", "Max", "Susan"];
    public static int[] ValidProximities = [-11, -10, -9, -1, 0, 1, 9, 10, 11];
    public static string[,] Weapons = { { "axe", "500" }, { "knife", "300" }, { "bat", "150" }, { "fist", "50" } };
    public static bool TestMode = false;
}

public static class Debugger
{
    private static List<string> Message { get; set; } = new List<string>();
    public static void Log(string value = "", bool newLine = false)
    {
        if (newLine)
            Message.Add(value + "\n");
        else
            Message.Add(value + " ");
    }

    public static void Display()
    {
        string message_ = "";
        foreach (string m in Message)
        {
            message_ += m;
        }
        Console.WriteLine(message_);
    }
    public static void Clear()
    {
        Message.Clear();
    }
}
