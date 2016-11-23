using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {

            int choice = getUserChoice();



        }

        private static int getUserChoice()
        {

            Console.WriteLine("1. Do you want to enter the text via the keyboard?");
            Console.WriteLine("2. Do you want to read in the text from a file?");

            string userInput = Console.ReadLine();
            bool validInput = false;
            int choice = 0;

            while (!validInput)
            {
                try
                {

                    if (choice == 1 || choice == 2)
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid input, please try again.\n");
                        Console.WriteLine("1. Do you want to enter the text via the keyboard?");
                        Console.WriteLine("2. Do you want to read in the text from a file?");
                        userInput = Console.ReadLine();
                        choice = Convert.ToInt32(userInput);
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input, please try again.\n");
                    Console.WriteLine("1. Do you want to enter the text via the keyboard?");
                    Console.WriteLine("2. Do you want to read in the text from a file?");
                    userInput = Console.ReadLine();
                    choice = Convert.ToInt32(userInput);
                }
            }

            return choice;

        }
    }
}
