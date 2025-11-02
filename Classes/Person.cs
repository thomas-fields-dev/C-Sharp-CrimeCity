using CrimeCity.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeCity.Classes
{
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
        public int Life { get; set; } = Config.LifePoints;
        public Coordinate Coordinate { get; set; }

        public string Input(Table table, Person player, EdgeRunner edgeRunner, Robber[] robbers)
        {
            if (Life > 0)
                Console.WriteLine("Whats your move? q,w,e,a,s,d,z,x,c,i\nor l to leave");

            string input = "";
            if (Config.MoveMentTest)
            {
                string[] inputs = ["q", "w", "e", "a", "s", "d", "z", "x", "c"];
                input = inputs[Table.Random.Next(0, inputs.Length)].ToString();
            }
            else
            {
                input = Console.ReadLine();
            }
                
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
                        Console.WriteLine($"Do you wanna hire {edgeRunner.FirstName} {edgeRunner.LastName}? Enter Y or N");
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
                                    Table.Message += $"You cant afford {edgeRunner.FirstName} {edgeRunner.LastName}!\n";
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
                        Money.Position = -1;
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

}
