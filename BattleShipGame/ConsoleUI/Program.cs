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

            PrintGameRules();
            Console.ReadLine();

            UserModel user1 = CreatePlayer(1);
            List<GridSpot> user1Grid = GetShipLocations(); // Check for valid placement
            
            UserModel user2 = CreatePlayer(2);
            List<GridSpot> user2Grid = GetShipLocations(); // Check for valid placement

            while (!winner)
            {
                //PrintEnemyGrid(); // Conceal enemy ship location (Only print Hits & Misses)
                //PrintFriendlyGrid(); // Print active ship locations, sunk ships, Enemy Hits & Misses
                
                //AskUserForShot();
                //DetermineGridStatus();
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
            GridSpot tile = new GridSpot();

            Console.Clear();
            Console.Write(" Where would you like to sail your ships? (Enter a valid grid coordinate [A1-E5]...\n\n ");

            for (int i = 0; i < 5; i++)
            {
                do
                {
                    Console.Write($"\tPlease enter the coordinate of ship #{counter}:\t");
                    gridTile = Console.ReadLine();
                    isValidPlacement = ValidateGridCoordinate(gridTile, strCoordinates);
                } while (isValidPlacement == false);

                tile.Coordinate = gridTile;
                tile.Letter = gridTile[0].ToString();
                tile.Number = int.Parse(gridTile[1].ToString());
                shipPlacements.Add(tile);
                strCoordinates.Add(gridTile);
                counter++;
            }

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
    }
}
