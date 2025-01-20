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

            //UserModel user1 = CreatePlayer(1);
            //List<GridSpot> user1Grid = GetShipLocations(); // Check for valid placement
            
            //UserModel user2 = CreatePlayer(2);
            List<GridSpot> user2Grid = GetShipLocations(); // Check for valid placement

            while (!winner)
            {
                PrintEnemyGrid(user2Grid); // Conceal enemy ship location (Only print Hits & Misses)
                //PrintFriendlyGrid(); // Print active ship locations, sunk ships, Enemy Hits & Misses

                //AskUserForShot();
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
            //int index = 0;
            //foreach (GridSpot shipSpot in shipPlacements)
            //{
            //    Console.WriteLine(shipPlacements[index].Coordinate);
            //    Console.WriteLine(shipPlacements[index].Letter);
            //    Console.WriteLine(shipPlacements[index].Number);
            //    Console.WriteLine(shipPlacements[index].Status);
            //    index++;
            //}
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

        private static void PrintEnemyGrid(List<GridSpot> enemyGrid)
        {
            //GridSpot[,] matrix = new GridSpot[5, 5];
            enemyGrid.Sort();

            bool isEnemy = true;
            int rowNumCounter = 1;
            int index = 0;
            Console.Clear();
            foreach (var enemy in enemyGrid)
            {
                    Console.WriteLine(enemy.Coordinate);
            }

            Console.WriteLine("\n\n\t\t--- ENEMY GRID ---\n");
            Console.WriteLine("\t   " + "|" + "  A  " + "|" + "  B  " + "|" + "  C  " + "|" + "  D  " + "|" + "  E  ");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("\t---+-----+-----+-----+-----+-----");
                Console.Write($"\t {rowNumCounter} ");
                for (int j = 0; j < 5; j++)
                {
                    string marker = CheckHitOrMiss(enemyGrid, index, rowNumCounter, isEnemy);
                    Console.Write("|  " + marker + "  ");
                    index++;
                }
                Console.WriteLine("");
                rowNumCounter++;
            }
            Console.ReadLine();
        }

        private static string CheckHitOrMiss(List<GridSpot> gridCoordinate, int index, int rowNumCounter, bool isEnemy)
        {
            string output = " ";
            switch (gridCoordinate[index].Status)
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
                        output = " "; break;
                    }
                    else
                    {
                        output = "V"; break;
                    }
            }
            return output;
        }
    }
}
