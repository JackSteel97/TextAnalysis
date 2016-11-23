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

            if(choice == 1)
            {
                //option 1 - enter text manually, sentence by sentence, * terminator
                Console.Clear();
                option1();
                

            }else
            {
                //option 2
            }

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
                    choice = Convert.ToInt32(userInput);

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


        private static void option1()
        {
            bool endRequested = false;
            Console.WriteLine("Enter each sentence one at a time, pressing enter at the end of every sentence.");
            Console.WriteLine("End your last sentence with '*' to terminate entry.");

            List<Sentence> sentences = new List<Sentence>();

            while (!endRequested)
            {
                string userInput = Console.ReadLine();

                if (userInput.Length == 0)
                {
                    Console.WriteLine("\nThat sentence was empty so we've ignored it.\n");
                }

                if (userInput[userInput.Length - 1] == '*')
                {
                    //termination requested
                    endRequested = true;
                    userInput = userInput.Remove(userInput.Length - 1, 1);
                }


                Sentence currentSentence = new Sentence(userInput);




            }
        }
    }
}
