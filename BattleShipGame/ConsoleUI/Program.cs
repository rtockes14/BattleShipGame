using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using BattleshipLibrary.Models;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            bool winner = false;
            string productOfAttack = "";

            //PrintGameRules();
            //Console.ReadLine();

            UserModel user1 = CreatePlayer(1);
            List<GridSpot> user1Grid = GetShipLocations(user1); // Check for valid placement
            
            UserModel user2 = CreatePlayer(2);
            List<GridSpot> user2Grid = GetShipLocations(user2); // Check for valid placement
            // Create a 2D Matrix for user 1 with friendly ship coordinates
            GridSpot[,] user1Matrix = PopulateMatrix(user1Grid);

            // Create a 2D Matrix for user 2 with friendly ship coordinates
            GridSpot[,] user2Matrix = PopulateMatrix(user2Grid);

            while (!winner)
            {
                PrintEnemyGrid(user2Grid,user2Matrix); // Conceal enemy ship location (Only print Hits & Misses)
                PrintFriendlyGrid(user1Grid,user1Matrix); 

                GridSpot user1Shot = new GridSpot();
                user1Shot = AskUserForShot(user1);
                (user2Matrix, productOfAttack) = CheckHitOrMiss(user1Shot, user2Matrix);
                PrintResults(productOfAttack, user1, user1Shot);

                Console.ReadLine();
            }
        }
        public static void PrintGameRules()
        {
            string filePath = "C:\\Users\\Randa\\OneDrive\\Desktop\\c#classpractice\\Battleship\\BattleShipGame\\Rules.txt";

            // Check if the file exists before printing
            if (File.Exists(filePath))
            {
                // Open the file
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                        Thread.Sleep(100);
                    }
                }
            }
        }

        public static UserModel CreatePlayer(int playerNumber)
        {
            Console.Clear();
            UserModel user = new UserModel();
            string name = "";
            do
            {
                Console.Write($" Player {playerNumber} -- Please enter your name: ");
                name = Console.ReadLine();
                user.UserName = name;

            } while (string.IsNullOrEmpty(name));

            Console.WriteLine($"\n\tGreetings Admiral {user.UserName}.");
            Thread.Sleep(1000);

            return user;
        }
        public static List<GridSpot> GetShipLocations(UserModel user)
        {
            bool isValidPlacement;
            bool isEnemy = false;
            int counter = 1;
            string gridTile = "";
            List<GridSpot> shipPlacements = new List<GridSpot>();
            List<string> strCoordinates = new List<string>();

            Console.Clear();
            Console.Write(" Where would you like to sail your ships? (Enter a valid grid coordinate [A1-E5]...\n\n ");

            for (int i = 0; i < 5; i++)
            {
                GridSpot tile = new GridSpot();
                do
                {
                    Console.Write($"\tPlease enter the coordinate of ship #{counter}:\t");
                    gridTile = Console.ReadLine();
                    isValidPlacement = ValidateGridCoordinate(gridTile, strCoordinates, isEnemy);
                } while (isValidPlacement == false);

                tile.Coordinate = gridTile;
                tile.Letter = gridTile[0].ToString();
                tile.Number = int.Parse(gridTile[1].ToString());
                tile.Status = GridSpotStatus.Ship;
                user.ShipLocations.Add(tile);
                shipPlacements.Add(tile);
                strCoordinates.Add(gridTile);
                counter++;
            }

            Console.ReadLine();

            return shipPlacements;
        }

        public static bool ValidateGridCoordinate(string coordinate, List<string> shipPlacements, bool isEnemy)
        {
            if (string.IsNullOrEmpty (coordinate))
            {
                return false;
            }
            bool validNumber = int.TryParse(coordinate[1].ToString(), out _);

            if (coordinate[0].ToString().ToLower() != "a" &&
                coordinate[0].ToString().ToLower() != "b" &&
                coordinate[0].ToString().ToLower() != "c" &&
                coordinate[0].ToString().ToLower() != "d" &&
                coordinate[0].ToString().ToLower() != "e")
            {
                return false;
            }
            // TODO: This will stop you from placing shots on the enemy grid in tiles with same coordinates as your ships
            if (shipPlacements.Contains(coordinate) && (!isEnemy))
            {
                return false;
            }
            if (validNumber == false)
            {
                return false;
            }
            if (int.Parse(coordinate.Substring(1).ToString()) < 1 || int.Parse(coordinate.Substring(1).ToString()) > 5)
            {
                return false;
            }

            return true;
        }

        private static GridSpot[,] PopulateMatrix(List<GridSpot> gridCoordinates)
        {
            string[] letters = { "A", "B", "C", "D", "E" };
            GridSpot tile = new GridSpot();
            GridSpot[,] matrix = new GridSpot[5, 5];
            for (int k = 0; k < 5; k++)
            {
                for (int l = 0; l < 5; l++)
                {
                    matrix[k, l] = tile;
                }
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    foreach (var ship in gridCoordinates)
                    {
                        if (ship.Number == i+1 && ship.Letter == letters[j])
                        {
                            matrix[i, j] = ship;  // This is a GridSpot
                        }
                    }
                }
                Console.WriteLine();
            }
            return matrix;
        }

        private static void PrintEnemyGrid(List<GridSpot> enemyGrid, GridSpot[,] matrix)
        {
            bool isEnemy = true;
            int rowNumCounter = 1;
            Console.Clear();

            Console.WriteLine("\n\n\t       --- ENEMY GRID ---\n");
            Console.WriteLine("\t   " + "|" + "  A  " + "|" + "  B  " + "|" + "  C  " + "|" + "  D  " + "|" + "  E  ");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("\t---+-----+-----+-----+-----+-----");
                Console.Write($"\t {rowNumCounter} ");
                for (int j = 0; j < 5; j++)
                {
                    string marker = ReturnTileMarker(matrix, i, j, isEnemy);
                    Console.Write("|  " + marker + "  ");
                }
                Console.WriteLine("");
                rowNumCounter++;
            }

            //Console.ReadLine();
        }

        private static void PrintFriendlyGrid(List<GridSpot> friendlyGrid, GridSpot[,] matrix)
        {
            bool isEnemy = false;
            int rowNumCounter = 1;

            Console.WriteLine("\n\t      --- FRIENDLY GRID ---\n");
            Console.WriteLine("\t   " + "|" + "  A  " + "|" + "  B  " + "|" + "  C  " + "|" + "  D  " + "|" + "  E  ");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("\t---+-----+-----+-----+-----+-----");
                Console.Write($"\t {rowNumCounter} ");
                for (int j = 0; j < 5; j++)
                {
                    string marker = ReturnTileMarker(matrix, i, j, isEnemy);
                    Console.Write("|  " + marker + "  ");
                }
                Console.WriteLine("");
                rowNumCounter++;
            }
            Console.WriteLine("\n");
        }

        private static string ReturnTileMarker(GridSpot[,] matrix, int i, int j, bool isEnemy)
        {
            GridSpot tile = new GridSpot();
            tile = matrix[i, j];
            string output = " ";
            //tile.Status = CheckHitOrMiss();
            switch (tile.Status)
            {
                case GridSpotStatus.Empty:
                    output = " "; break;
                case GridSpotStatus.Miss:
                    output = "O"; break;
                case GridSpotStatus.Hit:
                    output = "X"; break;
                case GridSpotStatus.Ship:
                    if (isEnemy)
                    {
                        output = "v"; break; // Change to Blank later
                    }
                    else
                    {
                        output = "V"; break;
                    }
                default:
                    output = "?"; break;
            }
            return output;
        }

        private static GridSpot AskUserForShot(UserModel user)
        {
            bool isValidPlacement;
            bool isEnemy = true;
            int counter = 1;
            string gridTile = "";
            // TODO: This is unused, but required for ValidateGridCoordinate.  Fix this later
            List<GridSpot> strCoordinates = new List<GridSpot>();

            Console.Write(" Where would you like to fire Admiral? -- (Enter a valid grid coordinate [A1-E5])..\n  ");

            GridSpot tile = new GridSpot();
            do
            {
                Console.Write($"\tPlease enter the coordinate of Fire-Mission 'Whiskey-{user.ShotCounter}':\t");
                gridTile = Console.ReadLine();
                isValidPlacement = ValidateGridCoordinate(gridTile, user.ShotsFired, isEnemy);
                user.ShotsFired.Add(gridTile);
                if (isValidPlacement == false)
                {
                    user.ShotsFired.Remove(gridTile);
                }
            } while (isValidPlacement == false);

            tile.Coordinate = gridTile;
            tile.Letter = gridTile[0].ToString();
            tile.Number = int.Parse(gridTile[1].ToString());
            tile.Status = GridSpotStatus.Shot;

            user.ShotCounter++;

            return tile;
        }

        private static (GridSpot[,], string) CheckHitOrMiss(GridSpot shot, GridSpot[,] matrix)
        {
            //TODO: THIS IS WHERE THE BULLSHIT IS HAPPENING!!!
            string[] letters = { "A", "B", "C", "D", "E" };
            string output = "MISS";
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < letters.Length; j++)
                {
                    if (shot.Number == i+1 && shot.Letter == letters[j] && shot.Status == GridSpotStatus.Shot && matrix[i,j].Status == GridSpotStatus.Ship)
                    {
                        matrix[i, j].Status = GridSpotStatus.Hit;
                        output = "HIT";
                        Console.WriteLine("HIT");
                    }
                    else if (shot.Number == i+1 && shot.Letter == letters[j] && shot.Status == GridSpotStatus.Shot && matrix[i,j].Status == GridSpotStatus.Empty)
                    {
                        matrix[i, j].Status = GridSpotStatus.Miss;
                        output = "MISS";
                        Console.WriteLine("MISS");
                    }
                    if (matrix[i, j].Status == GridSpotStatus.Empty)
                    {
                        matrix[i, j].Status = GridSpotStatus.Empty;
                    }
                    else if (matrix[i, j].Status == GridSpotStatus.Miss)
                    {
                        matrix[i, j].Status = GridSpotStatus.Miss;
                    }
                    else if (matrix[i, j].Status == GridSpotStatus.Ship)
                    {
                        matrix[i, j].Status = GridSpotStatus.Ship;
                    }
                    else 
                    {
                        //matrix[i, j].Status = GridSpotStatus.Empty;
                        //output = "Something";
                        Console.WriteLine("Not hittin' or missin'");
                    }
                }
            }
            return (matrix, output);
        }

        private static void PrintResults(string productOfAttack, UserModel user, GridSpot tile)
        {
            Console.WriteLine(" \t\t\tSplash inbound...\n");
            Thread.Sleep(1500);

            if (productOfAttack == "HIT")
            {
                Console.WriteLine($" Admiral {user.UserName}, That was direct Hit on {tile.Coordinate}! \n");
                Thread.Sleep(1000);
                Console.WriteLine(" Prepare for enemy attack!");
            }
            else if (productOfAttack == "MISS")
            {
                Console.WriteLine($" Admiral {user.UserName}, Our Fire-Mission didn't land!  Updating Grid {tile.Coordinate} as 'Miss'\n");
                Thread.Sleep(1000);
                Console.WriteLine(" Prepare for enemy attack!");
            }
            else
            {
                Console.WriteLine("Not sure what the hell is happening here");
            }
        }
    }
}
