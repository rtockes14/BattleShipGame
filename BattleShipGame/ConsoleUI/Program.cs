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

            //PrintGameRules();
            //Console.ReadLine();

            UserModel user1 = CreatePlayer(1);
            List<GridSpot> user1Grid = GetShipLocations(); // Check for valid placement
            
            UserModel user2 = CreatePlayer(2);
            List<GridSpot> user2Grid = GetShipLocations(); // Check for valid placement

            while (!winner)
            {
                GridSpot[,] user2Matrix = PopulateMatrix(user2Grid);
                PrintEnemyGrid(user2Grid,user2Matrix); // Conceal enemy ship location (Only print Hits & Misses)

                GridSpot[,] user1Matrix = PopulateMatrix(user1Grid);
                PrintFriendlyGrid(user1Grid,user1Matrix);
                List<GridSpot> user1Shots= AskUserForShot();
                //DetermineGridStatus();
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
                Console.Write($"Player {playerNumber} -- Please enter your name: ");
                name = Console.ReadLine();
                user.UserName = name;

            } while (string.IsNullOrEmpty(name));

            Console.WriteLine($"\nGreetings Admiral {user.UserName}.");
            Thread.Sleep(1000);

            return user;
        }
        public static List<GridSpot> GetShipLocations()
        {
            bool isValidPlacement;
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
                    isValidPlacement = ValidateGridCoordinate(gridTile, strCoordinates);
                } while (isValidPlacement == false);

                tile.Coordinate = gridTile;
                tile.Letter = gridTile[0].ToString();
                tile.Number = int.Parse(gridTile[1].ToString());
                tile.Status = GridSpotStatus.Ship;
                shipPlacements.Add(tile);
                strCoordinates.Add(gridTile);
                counter++;
            }

            Console.ReadLine();

            return shipPlacements;
        }

        public static bool ValidateGridCoordinate(string coordinate, List<string> shipPlacements)
        {
            bool validNumber = int.TryParse(coordinate[1].ToString(), out _);
            if (string.IsNullOrEmpty (coordinate))
            {
                return false;
            }
            if (coordinate[0].ToString().ToLower() != "a" &&
                coordinate[0].ToString().ToLower() != "b" &&
                coordinate[0].ToString().ToLower() != "c" &&
                coordinate[0].ToString().ToLower() != "d" &&
                coordinate[0].ToString().ToLower() != "e")
            {
                return false;
            }
            if (shipPlacements.Contains(coordinate))
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

        private static void PrintEnemyGrid(List<GridSpot> enemyGrid, GridSpot[,] matrix)
        {
            bool isEnemy = true;
            int rowNumCounter = 1;
            Console.Clear();

            Console.WriteLine("\n\n\t\t--- ENEMY GRID ---\n");
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

            Console.WriteLine("\t---- FRIENDLY GRID ----\n");
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
            Console.ReadLine();
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
                        { }
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
            }
            return output;
        }

        private static string CheckHitOrMiss(List<GridSpot> shots, GridSpot[,] matrix)
        {
            string output = " ";
            return output;
        }

        private static GridSpot[,] PopulateMatrix(List<GridSpot> gridCoordinates)
        {
            int index = 0;
            GridSpot tile = new GridSpot();
            GridSpot[,] matrix = new GridSpot[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (gridCoordinates[index].Number == i+1)
                    {
                        matrix[i, j] = gridCoordinates[index];
                        //Console.Write($"[{matrix[i, j].Status}]");
                    }
                    else
                    {
                        matrix[i, j] = tile;
                        //Console.Write($"[{matrix[i,j].Status}]");
                    }
                    if (index != 4)
                    {
                        index++;
                    }
                }
                index = 0;
                Console.WriteLine();
            }
            //Console.ReadLine();
            return matrix;
        }

        private static List<GridSpot> AskUserForShots()
        {
            bool isValidPlacement;
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
                    isValidPlacement = ValidateGridCoordinate(gridTile, strCoordinates);
                } while (isValidPlacement == false);

                tile.Coordinate = gridTile;
                tile.Letter = gridTile[0].ToString();
                tile.Number = int.Parse(gridTile[1].ToString());
                tile.Status = GridSpotStatus.Shot;
                shipPlacements.Add(tile);
                strCoordinates.Add(gridTile);
                counter++;
            }
            Console.ReadLine();

            return shipPlacements;
        } 
    }
}
