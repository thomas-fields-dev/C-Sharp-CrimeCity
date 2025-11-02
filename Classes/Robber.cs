using CrimeCity.Helpers;
using CrimeCity.Structs;
using HelloWold;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CrimeCity.Classes
{
    public class Robber : Person
    {
        public Robber()
        {
            Position = Table.Random.Next(1, 100);
            int randomWeapon = Table.Random.Next(0, Config.Weapons.Length / 2);
            Weapon = new string[,] { { Config.Weapons[randomWeapon, 0], Config.Weapons[randomWeapon, 1] } };
        }
        public string[,] Weapon { get; set; }
        public static int[]? RobberPositions { get; set; }

        public override void Move(Table table, Person player, EdgeRunner? edgeRunner = null, Robber[]? robbers = null, string input = "", int proximity = 0)
        {
            RobberPositions = null;
            if (robbers != null)
            {
                RobberPositions = new int[robbers.Length];
                for (int i = 0; i < robbers.Length; i++)
                {
                    RobberPositions[i] = robbers[i].Position;
                }
            }

            if (Table.Turn > 0)
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

                if (Table.Turn % Config.RobberSpeed != 0)
                    newPosition = 0;

                if (Position + newPosition == player.Position)
                    newPosition = 0;

                if (newPosition != 0)
                {
                    List<int> validPositions = new List<int>();

                    if (RobberPositions != null && RobberPositions.Contains(Position + newPosition))
                    {
                        ExLogger.Log($"Robber {FirstName} ({Coordinate.X}, {Coordinate.Y}): ", true);
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
                        List<int> unoccupiedAroundPlayer = new List<int>();
                        unoccupiedAroundPlayer = Config.ValidProximities.Except(occupiedAroundPlayer.ToArray()).ToList();

                        List<int> legalPositionsAroundPlayer = CheckBoundrys(unoccupiedAroundPlayer, player.Position);

                        string name = FirstName;
                        if (legalPositionsAroundPlayer.Any())
                        {
                            newPosition = DetermineBestMove(legalPositionsAroundPlayer, legalPositionsAroundRobber, player, isSameRow, isSameColumn, moveRobberLeft, moveRobberRight, isPlayerBelow, isPlayerAbove);
                        }
                    }
                }
                Position += newPosition;
                ExLogger.Log($"Robber {FirstName} position: {Position}", true);
            }
        }

        private int DetermineBestMove(List<int> legalPositionsAroundPlayer, List<int> legalPositionsAroundRobber, Person player, bool isSameRow, bool isSameColumn, bool moveRobberLeft, bool moveRobberRight, bool isPlayerBelow, bool isPlayerAbove)
        {
            int newPosition = 0;
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
                    ExLogger.Log($"-1", true);
                    if (!isSameRow && !isSameColumn && moveRobberLeft && isPlayerBelow)
                    {
                        int[] validMoves = [-11, 10, 9, -1, -10];
                        newPosition = GetRandomValidMove(legalPositionsAroundRobber, validMoves);
                    }
                    else if (!isSameRow && !isSameColumn && moveRobberLeft && isPlayerAbove)
                    {
                        int[] validMoves = [-11, -10, -9];
                        newPosition = GetRandomValidMove(legalPositionsAroundRobber, validMoves);

                    }
                    else if (isSameRow && !isSameColumn && moveRobberLeft)
                    {
                        int[] validMoves = [-11, -1, 9, 10, 10];
                        newPosition = GetRandomValidMove(legalPositionsAroundRobber, validMoves);
                    }
                }
                else if (!legalPositionsAroundRobber.Contains(9))
                {
                    ExLogger.Log($"9", true);
                    if (!isSameRow && !isSameColumn && moveRobberLeft && isPlayerBelow)
                    {
                        int[] validMoves = [-1, 10, 11];
                        newPosition = GetRandomValidMove(legalPositionsAroundRobber, validMoves);

                    }
                    else if (!isSameRow && isSameColumn && isPlayerBelow)
                    {
                        int[] validMoves = [-1, 1];
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
                    ExLogger.Log($"10", true);
                    if (!isSameRow && !isSameColumn && moveRobberRight && isPlayerBelow)
                    {
                        int[] validMoves = [9, 1];
                        newPosition = GetRandomValidMove(legalPositionsAroundRobber, validMoves);
                    }
                    else if (!isSameRow && isSameColumn && isPlayerBelow)
                    {
                        int[] validMoves = [9, 11, -1, 1, 10];
                        newPosition = GetRandomValidMove(legalPositionsAroundRobber, validMoves);
                    }

                }
                else if (!legalPositionsAroundRobber.Contains(1))
                {
                    ExLogger.Log($"1", true);
                    if (!isSameRow && !isSameColumn && moveRobberRight && isPlayerAbove)
                    {
                        int[] validMoves = [-1, -11, -10];
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
                        int[] validMoves = [10, -10, -9, 1, 11];
                        newPosition = GetRandomValidMove(legalPositionsAroundRobber, validMoves);
                    }
                    else if (!isSameRow && isSameColumn && isPlayerAbove)
                    {
                        int[] validMoves = [-1, 1];
                        newPosition = GetRandomValidMove(legalPositionsAroundRobber, validMoves);
                    }
                }
                else if (!legalPositionsAroundRobber.Contains(-10))
                {
                    ExLogger.Log($"-10", true);
                    if (!isSameRow && isSameColumn && isPlayerAbove)
                    {
                        int[] validMoves = [-9, 1, -11, -1];
                        newPosition = GetRandomValidMove(legalPositionsAroundRobber, validMoves);
                    }
                    else if (!isSameRow && !isSameColumn && isPlayerAbove && moveRobberRight)
                    {
                        int[] validMoves = [-9, 1];
                        newPosition = GetRandomValidMove(legalPositionsAroundRobber, validMoves);
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
                    ExLogger.Log($"-11", true);
                    if (!isSameRow && !isSameColumn && isPlayerAbove && moveRobberLeft)
                    {
                        int[] validMoves = [-1, -10];
                        newPosition = GetRandomValidMove(legalPositionsAroundRobber, validMoves);
                    }
                }
                else if (!legalPositionsAroundRobber.Contains(11))
                {
                    ExLogger.Log($"11", true);
                    if (!isSameRow && !isSameColumn && isPlayerBelow)
                    {
                        int[] validMoves = [1, 10];
                        newPosition = GetRandomValidMove(legalPositionsAroundRobber, validMoves);
                    }
                }
                else if (!legalPositionsAroundRobber.Contains(-9))
                {
                    ExLogger.Log($"-9", true);
                    if (!isSameRow && !isSameColumn && moveRobberRight && isPlayerAbove)
                    {
                        int[] validMoves = [-10, -9];
                        newPosition = GetRandomValidMove(legalPositionsAroundRobber, validMoves);
                    }
                }
            }
            return newPosition;
        }

        private int GetRandomValidMove(List<int> legalPositions, int[] validMoves)
        {
            int attemptes = 0;
            List<int> moves = new List<int>();
            int rnadomMove = 0;
            do
            {
                attemptes++;
                moves.Clear();
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
                if (moves.Any())
                {
                    {
                        rnadomMove = moves[Table.Random.Next(0, moves.Count())];
                    }
                    if (attemptes > 20)
                    {
                        rnadomMove = 0;
                        break;
                    }
                }
                else
                {
                    rnadomMove = 0;
                    break;
                }
            }
            while (Position + rnadomMove < -1 || Position + rnadomMove > 100);
            return rnadomMove;
        }

        public static List<int> CheckBoundrys(List<int> positions, int position, bool useRobberPositions = false)
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
                bool isWarpPosition = 
                    Table.LeftBoarder.Contains(position) && Table.RightBorder.Contains(newValidPosition) || 
                    Table.RightBorder.Contains(position) && Table.LeftBoarder.Contains(newValidPosition);
                if (upperBound && !isBorderPosition && !isWarpPosition && inRange)
                {
                    legalPositions.Add(validPosition);
                    ExLogger.Log($"*2 {validPosition}:{newValidPosition} ", true);
                }
                else if (lowerBound && !isBorderPosition && !isWarpPosition && inRange)
                {
                    legalPositions.Add(validPosition);
                    ExLogger.Log($"*2.0 {validPosition}:{position + validPosition} ", true);
                }
                else if (!Yleft.Contains(validPosition) && (Table.LeftBoarder.Contains(position) && inRange) && !isWarpPosition)
                {
                    legalPositions.Add(validPosition);
                    ExLogger.Log($"*0 {validPosition}:{position + validPosition} ", true);
                }
                else if (!Yright.Contains(validPosition) && (Table.RightBorder.Contains(position) && inRange) && !isWarpPosition)
                {
                    legalPositions.Add(validPosition);
                    ExLogger.Log($"*1 {validPosition}:{position + validPosition}  ", true);
                }
                else if (inRange && !isBorderPosition)
                {
                    legalPositions.Add(validPosition);
                    ExLogger.Log($"*1 {validPosition}:{position + validPosition}  ", true);
                }
                else if (RobberPositions != null && !RobberPositions.Contains(newValidPosition) && useRobberPositions && !isBorderPosition && !isWarpPosition && inRange)
                {
                    legalPositions.Add(validPosition);
                    ExLogger.Log($"*1 {validPosition}:{position + validPosition}  ", true);
                }
                else if (RobberPositions != null && !RobberPositions.Contains(newValidPosition) && isBorderPosition && !isWarpPosition && inRange)
                {
                    legalPositions.Add(validPosition);
                    ExLogger.Log($"*1 {validPosition}:{position + validPosition}  ", true);
                }
                else if (RobberPositions != null && !RobberPositions.Contains(newValidPosition) && useRobberPositions && isBorderPosition && inRange && !isWarpPosition)
                {
                    legalPositions.Add(validPosition);
                    ExLogger.Log($"*1 {validPosition}:{position + validPosition}  ", true);
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

}
