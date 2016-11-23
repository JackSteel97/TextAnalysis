using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    class Program
    {
        /// <summary>
        /// Mains method
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
           
            int choice = getUserChoice();
            Console.Clear();
            if (choice == 1)
            {
                //option 1 - enter text manually, sentence by sentence, * terminator
                option1(); 
            }else
            {
                //option 2 - read text from file
                option2();
            }

            Console.Read();
        }

        /// <summary>
        /// Gets the user choice.
        /// </summary>
        /// <returns>choice as an integer, either 1 or 2</returns>
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


        /// <summary>
        /// executes option 1.
        /// </summary>
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
                    //remove the termination character so it is not counted in analysis
                    userInput = userInput.Remove(userInput.Length - 1, 1);
                }

                //add sentence entered to the list of sentences so far
                Sentence currentSentence = new Sentence(userInput);
                sentences.Add(currentSentence);
            }
            analyseSentences(sentences);
        }

        /// <summary>
        /// executes option 2.
        /// </summary>
        private static void option2()
        {
            string filename = "example.txt";
            string fileContents = "";
            List<Sentence> sentences = new List<Sentence>();


            string[] lines = System.IO.File.ReadAllLines(filename);
            foreach(string line in lines)
            {
                //assuming each sentence is on a new line
                if (line.Length > 0)
                {
                    sentences.Add(new Sentence(line));
                }
            }

            analyseSentences(sentences);
            findLongWords(sentences);
        }

        /// <summary>
        /// Finds the long words (over 7 characters) in a given set of sentences.
        /// </summary>
        /// <param name="sentences">The sentences.</param>
        private static void findLongWords(List<Sentence> sentences)
        {
            const int MIN_CHARACTERS_NEEDED = 7;

            List<string> foundWords = new List<string>();
            
            foreach(Sentence sentence in sentences)
            {
                string content = sentence.getSentenceContent();
                string currentWord = "";

                for(int i = 0; i < content.Length; i++)
                {
                    char currentChar = content[i].ToString().ToUpper().ToCharArray()[0];
                    if((int)currentChar<65 || (int)currentChar > 90)
                    {
                        //this character is not a letter, therefore, the word ended
                        if (currentWord.Length > MIN_CHARACTERS_NEEDED)
                        {
                            //this is a long word
                            if (!foundWords.Contains(currentWord))
                            {
                                foundWords.Add(currentWord);
                            }
                        }
                        //empty word holder
                        currentWord = "";
                    }else
                    {
                        //concat the letter
                        currentWord += content[i];
                    }
                }
            }

            saveWordsToFile(foundWords);


        }

        /// <summary>
        /// Saves the words to a file named LongWords.txt in the local directory.
        /// </summary>
        /// <param name="words">The words.</param>
        private static void saveWordsToFile(List<string> words)
        {
            System.IO.File.WriteAllLines("LongWords.txt", words.ToArray());
        }

        /// <summary>
        /// Analyses the given sentences and outputs the results.
        /// </summary>
        /// <param name="sentences">The sentences.</param>
        private static void analyseSentences(List<Sentence> sentences)
        {
            //initialise counters
            int sentenceCount = sentences.Count();
            int wordCount = 0;
            int vowelCount = 0;
            int consonantCount = 0;
            int uppercaseCount = 0;
            int lowercaseCount = 0;
            int[] letterFrequency = new int[26];

            for (int i = 0; i < sentences.Count; i++)
            {
                Sentence currentSentence = sentences[i];
                //for every sentence add counts to total
                wordCount += currentSentence.getWordCount();
                vowelCount += currentSentence.getVowelCount();
                consonantCount += currentSentence.getConsonantCount();
                uppercaseCount += currentSentence.getUppercaseCount();
                lowercaseCount += currentSentence.getLowercaseCount();

                int[] sentenceLetterFrequency = currentSentence.getLetterFrequency();

                for (int n = 0; n < sentenceLetterFrequency.Length; n++)
                {
                    letterFrequency[n] += sentenceLetterFrequency[n];
                }
            }

            //print results
            Console.Clear();

            Console.WriteLine("    RESULTS     \n\n");
            Console.WriteLine("Number of sentences entered = {0}", sentenceCount);
            Console.WriteLine("Number of words = {0}", wordCount);
            Console.WriteLine("Number of vowels = {0}", vowelCount);
            Console.WriteLine("Number of consonants = {0}", consonantCount);
            Console.WriteLine("Number of upper case letters = {0}", uppercaseCount);
            Console.WriteLine("Number of lower case letters = {0}\n", lowercaseCount);
            for (int i = 0; i < letterFrequency.Length; i++)
            {
                if (letterFrequency[i] != 0) {
                    char currentChar = Convert.ToChar(i + 65);
                    Console.WriteLine("The letter '{0}' appeared {1} times",currentChar,letterFrequency[i]);
                }
            }
        }

    }
}

