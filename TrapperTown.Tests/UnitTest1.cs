using CrimeCity.Classes;
using System.Diagnostics;

namespace TrapperTown.Tests
{


    //  0  1  2  3  4  5  6  7  8  9
    // 10 11 12 13 14 15 16 17 18 19
    // 20 21 22 23 24 25 26 27 28 29
    // 30 31 32 33 34 35 36 37 38 39
    // 40 41 42 43 44 45 46 47 48 49
    // 50 51 52 53 54 55 56 57 58 59
    // 60 61 62 63 64 65 66 67 68 69
    // 70 71 72 73 74 75 76 77 78 79
    // 80 81 82 83 84 85 86 87 88 89
    // 90 91 92 93 94 95 96 97 98 99

    //-11, -10, -9
    // -1,  -0,  1
    //  9,  10, 11
    public class UnitTest1
    {
        // robber at negative
        // robber at positive
        // player teleporting 
        // robber teleporting
        // robbers stacking

        [Fact]
        public void robber_cant_wwrp_right_left()
        {
            Config.RobberSpeed = 1;
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

            table.Draw(player, edgeRunner, robbers);

            string input = string.Empty;
            string[] inputs = ["q", "w", "e", "a", "s", "d", "z", "x", "c"];
            input = inputs[Table.Random.Next(0, inputs.Length)];

            Table.Turn++;

            player.Position = 79;
            robbers[0].Position = 69;
            robbers[1].Position = 68;
            robbers[2].Position = 78;
            robbers[3].Position = 88;
            robbers[4].Position = 89;

            //  0  1  2  3  4  5  6  7  8  9
            // 10 11 12 13 14 15 16 17 18 19
            // 20 21 22 23 24 25 26 27 28 29
            // 30 31 32 33 34 35 36 37 38 39
            // 40 41 42 43 44 45 46 47 48 49
            // 50 51 52 53 54 55 56 57 58 59
            // 60 61 62 63 64 65 66 67 68 69          ZZ|
            // 70 71 72 73 74 75 76 77 78 79          ZX|
            // 80 81 82 83 84 85 86 87 88 89          ZZ|
            // 90 91 92 93 94 95 96 97 98 99

            player.Move(table, player, edgeRunner, robbers, input);
            for (int i = 0; i < 100; i++)
            {
                robbers[0].Move(table, player, edgeRunner, robbers, "");
                Assert.NotEqual(robbers[0].Position, 70);
            }
        }

        [Fact]
        public void robber_cant_wwrp_left_right()
        {
            Config.RobberSpeed = 1;
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

            table.Draw(player, edgeRunner, robbers);

            string input = string.Empty;
            string[] inputs = ["q", "w", "e", "a", "s", "d", "z", "x", "c"];
            input = inputs[Table.Random.Next(0, inputs.Length)];

            Table.Turn++;

            player.Position = 60;
            robbers[0].Position = 50;
            robbers[1].Position = 51;
            robbers[2].Position = 61;
            robbers[3].Position = 71;
            robbers[4].Position = 70;

            //  0  1  2  3  4  5  6  7  8  9
            // 10 11 12 13 14 15 16 17 18 19
            // 20 21 22 23 24 25 26 27 28 29
            // 30 31 32 33 34 35 36 37 38 39
            // 40 41 42 43 44 45 46 47 48 49
            // 50 51 52 53 54 55 56 57 58 59    |ZZ
            // 60 61 62 63 64 65 66 67 68 69    |XZ
            // 70 71 72 73 74 75 76 77 78 79    |ZZ
            // 80 81 82 83 84 85 86 87 88 89
            // 90 91 92 93 94 95 96 97 98 99

            player.Move(table, player, edgeRunner, robbers, input);
            for (int i = 0; i < 100; i++)
            {
                robbers[0].Move(table, player, edgeRunner, robbers, "");
                Assert.NotEqual(robbers[0].Position, 49);
            }
        }

        [Fact]
        public void robber_cant_warp_top_bottom()
        {
            Config.RobberSpeed = 1;
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

            table.Draw(player, edgeRunner, robbers);

            string input = string.Empty;
            string[] inputs = ["q", "w", "e", "a", "s", "d", "z", "x", "c"];
            input = inputs[Table.Random.Next(0, inputs.Length)];

            Table.Turn++;

            player.Position = 2;
            robbers[0].Position = 1;
            robbers[1].Position = 11;
            robbers[2].Position = 12;
            robbers[3].Position = 13;
            robbers[4].Position = 3;
            //                                  ___
            //  0  1  2  3  4  5  6  7  8  9    ZXZ  
            // 10 11 12 13 14 15 16 17 18 19    ZZZ  
            // 20 21 22 23 24 25 26 27 28 29
            // 30 31 32 33 34 35 36 37 38 39
            // 40 41 42 43 44 45 46 47 48 49
            // 50 51 52 53 54 55 56 57 58 59
            // 60 61 62 63 64 65 66 67 68 69
            // 70 71 72 73 74 75 76 77 78 79
            // 80 81 82 83 84 85 86 87 88 89
            // 90 91 92 93 94 95 96 97 98 99      

            player.Move(table, player, edgeRunner, robbers, input);
            for (int i = 0; i < 100; i++)
            {
                robbers[0].Move(table, player, edgeRunner, robbers, "");
                Assert.False(robbers[0].Position < 0);
            }
        }

        [Fact]
        public void robber_cant_warp_bottom_top()
        {
            Config.RobberSpeed = 1;
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

            table.Draw(player, edgeRunner, robbers);

            string input = string.Empty;
            string[] inputs = ["q", "w", "e", "a", "s", "d", "z", "x", "c"];
            input = inputs[Table.Random.Next(0, inputs.Length)];

            Table.Turn++;

            player.Position = 95;
            robbers[0].Position = 96;
            robbers[1].Position = 86;
            robbers[2].Position = 85;
            robbers[3].Position = 84;
            robbers[4].Position = 94;

            //  0  1  2  3  4  5  6  7  8  9
            // 10 11 12 13 14 15 16 17 18 19
            // 20 21 22 23 24 25 26 27 28 29
            // 30 31 32 33 34 35 36 37 38 39
            // 40 41 42 43 44 45 46 47 48 49
            // 50 51 52 53 54 55 56 57 58 59
            // 60 61 62 63 64 65 66 67 68 69
            // 70 71 72 73 74 75 76 77 78 79         
            // 80 81 82 83 84 85 86 87 88 89     ZZZ     
            // 90 91 92 93 94 x5 96 97 98 99     ZXZ
            //                                   ---

            player.Move(table, player, edgeRunner, robbers, input);
            for (int i = 0; i < 100; i++)
            {
                robbers[0].Move(table, player, edgeRunner, robbers, "");
                Assert.False(robbers[0].Position > 99);
            }
        }

        [Fact]
        public void robber_moves_top_right_top_border()
        {
            Config.RobberSpeed = 1;
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

            table.Draw(player, edgeRunner, robbers);

            string input = "s";

            Table.Turn++;

            player.Position = 2;
            robbers[0].Position = 10;
            robbers[1].Position = 11;
            robbers[2].Position = 12;
            robbers[3].Position = 13;
            robbers[4].Position = 1;

            //                                    ____
            //  0  1  2  3  4  5  6  7  8  9       XZ
            // 10 11 12 13 14 15 16 17 18 19      XXXX
            // 20 21 22 23 24 25 26 27 28 29
            // 30 31 32 33 34 35 36 37 38 39
            // 40 41 42 43 44 45 46 47 48 49
            // 50 51 52 53 54 55 56 57 58 59
            // 60 61 62 63 64 65 66 67 68 69
            // 70 71 72 73 74 75 76 77 78 79
            // 80 81 82 83 84 85 86 87 88 89
            // 90 91 92 93 94 95 96 97 98 99

            player.Move(table, player, edgeRunner, robbers, input);
            for (int i = 0; i < 100; i++)
            {
                robbers[0].Move(table, player, edgeRunner, robbers, "");
                if (robbers[0].Position == 3)
                    break;
            }
            Assert.Equal(3, robbers[0].Position);
        }

        [Fact]
        public void robber_moves_down_same_column_right_border()
        {
            Config.RobberSpeed = 1;
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

            table.Draw(player, edgeRunner, robbers);

            string input = string.Empty;
            string[] inputs = ["q", "w", "e", "a", "s", "d", "z", "x", "c"];
            input = inputs[Table.Random.Next(0, inputs.Length)];

            Table.Turn++;

            player.Position = 39;
            robbers[0].Position = 19;
            robbers[1].Position = 29;
            robbers[2].Position = 28;
            robbers[3].Position = 38;
            robbers[4].Position = 48;

            // 

            //  0  1  2  3  4  5  6  7  8  9
            // 10 11 12 13 14 15 16 17 18 N9        Z|
            // 20 21 22 23 24 25 26 27 T8 T9       ZZ|
            // 30 31 32 33 34 35 36 37 T8 3N       ZX|
            // 40 41 42 43 44 45 46 47 F8 49       Z |
            // 50 51 52 53 54 55 56 57 58 59
            // 60 61 62 63 64 65 66 67 68 69
            // 70 71 72 73 74 75 76 77 78 79
            // 80 81 82 83 84 85 86 87 88 89
            // 90 91 92 93 94 95 96 97 98 99

            player.Move(table, player, edgeRunner, robbers, input);
            for (int i = 0; i < 100; i++)
            {
                //-11, -10, -9
                // -1,  -0,  1
                //  9,  10, 11

                robbers[0].Move(table, player, edgeRunner, robbers, "");
                if (robbers[0].Position == 49)
                    break;
            }
            Assert.True(robbers[0].Position == 49);
        }

        [Fact]
        public void robber_moves_up_same_column_right_border()
        {
            Config.RobberSpeed = 1;
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

            table.Draw(player, edgeRunner, robbers);

            string input = string.Empty;
            string[] inputs = ["q", "w", "e", "a", "s", "d", "z", "x", "c"];
            input = inputs[Table.Random.Next(0, inputs.Length)];

            Table.Turn++;

            player.Position = 69;
            robbers[0].Position = 89;
            robbers[1].Position = 79;
            robbers[2].Position = 78;
            robbers[3].Position = 68;
            robbers[4].Position = 58;

            // 

            //  0  1  2  3  4  5  6  7  8  9
            // 10 11 12 13 14 15 16 17 18 N9
            // 20 21 22 23 24 25 26 27 T8 T9
            // 30 31 32 33 34 35 36 37 T8 3N
            // 40 41 42 43 44 45 46 47 F8 49
            // 50 51 52 53 54 55 56 57 58 59       Z |
            // 60 61 62 63 64 65 66 67 68 *9       ZX|
            // 70 71 72 73 74 75 76 77 78 79       ZZ|
            // 80 81 82 83 84 85 86 87 88 89        Z|
            // 90 91 92 93 94 95 96 97 98 99

            player.Move(table, player, edgeRunner, robbers, input);
            for (int i = 0; i < 100; i++)
            {
                //-11, -10, -9
                // -1,  -0,  1
                //  9,  10, 11

                robbers[0].Move(table, player, edgeRunner, robbers, "");
                if (robbers[0].Position == 59)
                    break;
            }
            Assert.True(robbers[0].Position == 59);
        }


        [Fact]
        public void robber_moves_up_same_column()
        {
            Config.RobberSpeed = 1;
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

            table.Draw(player, edgeRunner, robbers);

            string input = "s";

            Table.Turn++;

            player.Position = 54;
            robbers[0].Position = 63;
            robbers[1].Position = 64;
            robbers[2].Position = 65;
            robbers[3].Position = 74;
            robbers[4].Position = 84;

            // 

            //  0  1  2  3  4  5  6  7  8  9
            // 10 11 12 13 14 15 16 17 18 N9
            // 20 21 22 23 24 25 26 27 T8 T9
            // 30 31 32 33 34 35 36 37 T8 3N
            // 40 41 42 43 44 45 46 47 F8 49
            // 50 51 52 53 *4 55 56 57 58 59        X
            // 60 61 62 63 64 65 66 67 68 69       ZZZ 
            // 70 71 72 73 74 75 76 77 78 79        Z
            // 80 81 82 83 z4 85 86 87 88 89        Z
            // 90 91 92 93 94 95 96 97 98 99

            player.Move(table, player, edgeRunner, robbers, input);
            for (int i = 0; i < 100; i++)
            {
                //-11, -10, -9
                // -1,  -0,  1
                //  9,  10, 11
                int lastPosition = robbers[0].Position;
                robbers[0].Move(table, player, edgeRunner, robbers, "");
                if (robbers[0].Position == 54)
                    continue;
                if (robbers[0].Position == 63 || robbers[0].Position == 65)
                    break;
            }
            Assert.True(robbers[0].Position == 63 || robbers[0].Position == 65);
        }

        [Fact]
        public void robber_moves_down_same_column()
        {
            Config.RobberSpeed = 1;
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

            table.Draw(player, edgeRunner, robbers);

            string input = "s";

            Table.Turn++;

            player.Position = 54;
            robbers[1].Position = 43;
            robbers[2].Position = 44;
            robbers[3].Position = 45;
            robbers[4].Position = 34;
            robbers[0].Position = 24;

            // 

            //  0  1  2  3  4  5  6  7  8  9
            // 10 11 12 13 14 15 16 17 18 N9
            // 20 21 22 23 z4 25 26 27 T8 T9         Z     
            // 30 31 32 33 34 35 36 37 T8 3N         Z
            // 40 41 42 43 44 45 46 47 F8 49        ZZZ
            // 50 51 52 53 *4 55 56 57 58 59         X
            // 60 61 62 63 64 65 66 67 68 69
            // 70 71 72 73 74 75 76 77 78 79
            // 80 81 82 83 84 85 86 87 88 89
            // 90 91 92 93 94 95 96 97 98 99

            player.Move(table, player, edgeRunner, robbers, input);
            for (int i = 0; i < 100; i++)
            {
                //-11, -10, -9
                // -1,  -0,  1
                //  9,  10, 11
                int lastPosition = robbers[0].Position;
                robbers[0].Move(table, player, edgeRunner, robbers, "");
                if (robbers[0].Position == 53 || robbers[0].Position == 55)
                    break;
            }
            Assert.True(robbers[0].Position == 53 || robbers[0].Position == 55);
        }

        [Fact]
        public void robber_moves_down_same_column_left_border()
        {
            Config.RobberSpeed = 1;
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

            table.Draw(player, edgeRunner, robbers);

            string input = "s";

            Table.Turn++;

            player.Position = 30;
            robbers[1].Position = 41;
            robbers[2].Position = 20;
            robbers[3].Position = 21;
            robbers[4].Position = 31;
            robbers[0].Position = 10;

            // 

            //  0  1  2  3  4  5  6  7  8  9
            // z0 11 12 13 14 15 16 17 18 19       |Z
            // 20 21 22 23 24 25 26 27 28 29       |ZZ
            // x0 31 32 33 34 35 36 37 38 39       |XZ
            // 40 41 42 43 44 45 46 47 48 49       | Z 
            // 50 51 52 53 54 55 56 57 58 59
            // 60 61 62 63 64 65 66 67 68 69
            // 70 71 72 73 74 75 76 77 78 79
            // 80 81 82 83 84 85 86 87 88 89
            // 90 91 92 93 94 95 96 97 98 99

            player.Move(table, player, edgeRunner, robbers, input);
            for (int i = 0; i < 100; i++)
            {
                //-11, -10, -9
                // -1,  -0,  1
                //  9,  10, 11

                robbers[0].Move(table, player, edgeRunner, robbers, "");
                if (robbers[0].Position == 40)
                    break;
            }
            Assert.Equal(40, robbers[0].Position);
        }

        [Fact]
        public void robber_moves_up_same_column_left_border()
        {
            Config.RobberSpeed = 1;
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

            table.Draw(player, edgeRunner, robbers);

            string input = "s";

            Table.Turn++;

            player.Position = 20;
            robbers[1].Position = 11;
            robbers[2].Position = 21;
            robbers[3].Position = 30;
            robbers[4].Position = 31;
            robbers[0].Position = 40;

            //  0  1  2  3  4  5  6  7  8  9
            // z0 11 12 13 14 15 16 17 18 19       | Z
            // 20 21 22 23 24 25 26 27 28 29       |XZ
            // x0 31 32 33 34 35 36 37 38 39       |ZZ
            // 40 41 42 43 44 45 46 47 48 49       |Z 
            // 50 51 52 53 54 55 56 57 58 59
            // 60 61 62 63 64 65 66 67 68 69
            // 70 71 72 73 74 75 76 77 78 79
            // 80 81 82 83 84 85 86 87 88 89
            // 90 91 92 93 94 95 96 97 98 99

            player.Move(table, player, edgeRunner, robbers, input);
            for (int i = 0; i < 100; i++)
            {
                //-11, -10, -9
                // -1,  -0,  1
                //  9,  10, 11

                robbers[0].Move(table, player, edgeRunner, robbers, "");
                if (robbers[0].Position == 10)
                    break;
            }
            Assert.True(robbers[0].Position == 10);
        }

        [Fact]
        public void robber_moves_right_same_row_bottom_border()
        {
            Config.RobberSpeed = 1;
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

            table.Draw(player, edgeRunner, robbers);

            string input = "s";

            Table.Turn++;

            player.Position = 94;
            robbers[1].Position = 93;
            robbers[3].Position = 85;
            robbers[4].Position = 84;
            robbers[2].Position = 83;
            robbers[0].Position = 92;

            //  0  1  2  3  4  5  6  7  8  9
            // z0 11 12 13 14 15 16 17 18 19       
            // 20 21 22 23 24 25 26 27 28 29       
            // x0 31 32 33 34 35 36 37 38 39       
            // 40 41 42 43 44 45 46 47 48 49        
            // 50 51 52 53 54 55 56 57 58 59
            // 60 61 62 63 64 65 66 67 68 69
            // 70 71 72 73 74 75 76 77 78 79
            // 80 81 82 83 84 85 86 87 88 89        ZZZ
            // 90 91 z2 93 x4 95 96 97 98 99       ZZX
            //                                     ----
            player.Move(table, player, edgeRunner, robbers, input);
            for (int i = 0; i < 100; i++)
            {
                //-11, -10, -9
                // -1,  -0,  1
                //  9,  10, 11

                robbers[0].Move(table, player, edgeRunner, robbers, "");
                if (robbers[0].Position == 95)
                    break;
            }
            Assert.True(robbers[0].Position == 95);
        }
        [Fact]
        public void robber_moves_left_same_row_bottom_border()
        {
            Config.RobberSpeed = 1;
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

            table.Draw(player, edgeRunner, robbers);

            string input = "s";

            Table.Turn++;

            player.Position = 95;
            robbers[1].Position = 86;
            robbers[3].Position = 96;
            robbers[4].Position = 85;
            robbers[2].Position = 84;
            robbers[0].Position = 97;

            //  0  1  2  3  4  5  6  7  8  9
            // z0 11 12 13 14 15 16 17 18 19       
            // 20 21 22 23 24 25 26 27 28 29       
            // x0 31 32 33 34 35 36 37 38 39       
            // 40 41 42 43 44 45 46 47 48 49        
            // 50 51 52 53 54 55 56 57 58 59
            // 60 61 62 63 64 65 66 67 68 69
            // 70 71 72 73 74 75 76 77 78 79
            // 80 81 82 83 84 85 86 87 88 89       ZZZ
            // 90 91 z2 93 x4 95 96 97 98 99        XZZ
            //                                    -----
            player.Move(table, player, edgeRunner, robbers, input);
            for (int i = 0; i < 100; i++)
            {
                //-11, -10, -9
                // -1,  -0,  1
                //  9,  10, 11

                robbers[0].Move(table, player, edgeRunner, robbers, "");
                if (robbers[0].Position == 94)
                    break;
            }
            Assert.Equal(94, robbers[0].Position);
        }

    }
}