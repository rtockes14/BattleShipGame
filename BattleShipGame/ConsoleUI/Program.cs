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
            PrintGameRules();

            Console.ReadLine();

            UserModel user1 = AskUserForName();
            UserModel user2 = AskUserForName();

            //PrintEnemyGrid();

            //PrintFriendlyGrid();
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
                        Thread.Sleep(200);
                    }
                }
            }
        }


        public static UserModel AskUserForName()
        {
            UserModel user = new UserModel();
            string name = "";
            do
            {
                Console.Write("Player 1 -- Please enter your name: ");
                name = Console.ReadLine();
                user.UserName = name;

            } while (string.IsNullOrEmpty(name));

            Console.WriteLine($"\nGreetings Admiral {user.UserName}.");
            Console.Write("Where would you like to sail your ships? (Enter a valid grid coordinate [A1-E5]: ");

            return user;
        }


        public static bool ValidateGridCoordinate(string coordinate)
        {
            bool result = false;
            return result;
        }

    }
}
