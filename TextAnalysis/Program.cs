using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

//TODO: Stretch Exercise: mood analysis
namespace TextAnalysis
{
    /// <summary>
    /// Program to analyse text from user input or file.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Mains method
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            //get the users choice into an int
            int choice = getUserChoice();
            //clear the contents of the console display
            Console.Clear();

            //execute the appropriate function based on the user's choice
            switch (choice)
            {
                case 1:
                    //option 1 - enter text manually, sentence by sentence, * terminator
                    option1();
                    break;
                case 2:
                    //option 2 - read text from file
                    option2();
                    break;
            }
            
            //halt application for reading
            Console.Read();
        }

        /// <summary>
        /// Analyses the sentiment of a given set of sentences
        /// </summary>
        /// <param name="sentences">The sentences to be analysed.</param>
        private static async void analyseSentiment(List<Sentence> sentences)
        {
            //instantiate a new instance of the SentimentAnalysis class
            SentimentAnalysis analyser = new SentimentAnalysis();
            string text = "";
            Console.WriteLine("\n\nAnalysing Sentiment, Please Wait...\n");
            
            //loop through the list of sentences, sentence by sentence
            foreach (Sentence sentence in sentences)
            {
                //concatenate each sentence into one string
                text += sentence.getSentenceContent() + " ";
            }
            //call the getSentiment method and wait for it to finish
            string response = await analyser.getSentiment(text);

            //convert the JSON response to a dynamic object so we don't have to create classes for the response ourselves
            dynamic stuff = JsonConvert.DeserializeObject(response);

            //initialise a score variable
            decimal score = 0;

            try
            {
                //try to read the score from the dynamic object, if it exists. multiply by 100 to make it a percentage
                score = stuff.documents[0].score * 100;
            }
            catch
            {
                //the score does not exist, something went wrong with the API call
                Console.WriteLine("Sentiment analysis failed!");
                return;
            }

            //Output the score
            Console.WriteLine("With a sentiment score of {0}% we determined that: ", score);

            //Output the score in a more user friendly way
            if(score >= 40 && score <= 60)
            {
                //fairly neutral text
                Console.WriteLine("This text is neutral");
            }else if(score<40 && score >= 20)
            {
                Console.WriteLine("This text is somewhat negative");
            }else if (score < 20 && score>=10)
            {
                Console.WriteLine("This text is negative");
            }else if (score < 10)
            {
                Console.WriteLine("This text is very negative");
            }else if (score > 60 && score <= 80)
            {
                Console.WriteLine("This text is somewhat positive");
            }else if(score > 80 && score <= 90)
            {
                Console.WriteLine("This text is positive");
            }else if (score > 90)
            {
                Console.WriteLine("This text is very positive");
            }
            


        }

        /// <summary>
        /// Gets the user choice.
        /// </summary>
        /// <returns>choice as an integer, either 1 or 2</returns>
        private static int getUserChoice()
        {
            //present the choices to the user and ask for an input
            Console.WriteLine("1. Do you want to enter the text via the keyboard?");
            Console.WriteLine("2. Do you want to read in the text from a file?");

            //get the user's input as a string
            string userInput = Console.ReadLine();
            //initialise validity flag and choice variable
            bool validInput = false;
            int choice = 0;

            //keep asking till we get a valid answer
            while (!validInput)
            {
                try
                {
                    //attempt conversion to an integer
                    //if the user enters a non-numeric input, run catch block
                    choice = Convert.ToInt32(userInput);
                    
                    //check if the choice is one of the expected ones
                    if (choice == 1 || choice == 2)
                    {
                        //it is, input is valid, we can exit the loop now
                        validInput = true;
                    }
                    else
                    {
                        //it was a number but not what we expected
                        //clear the console
                        Console.Clear();
                        //inform user of invalid input
                        Console.WriteLine("Invalid input, please try again.\n");
                        //ask for inputs again
                        Console.WriteLine("1. Do you want to enter the text via the keyboard?");
                        Console.WriteLine("2. Do you want to read in the text from a file?");
                        //get user's input
                        userInput = Console.ReadLine();
                        //attempt conversion to integer again and run catch block if non-numeric
                        choice = Convert.ToInt32(userInput);
                    }
                }
                //runs if the conversion to integer in the try block fails because 'userInput' is non-numeric
                catch (Exception ex)
                {
                    //clear the console
                    Console.Clear();
                    //inform user of invalid input
                    Console.WriteLine("Invalid input, please try again.\n");
                    //as for inputs again
                    Console.WriteLine("1. Do you want to enter the text via the keyboard?");
                    Console.WriteLine("2. Do you want to read in the text from a file?");
                    //get the user's input
                    userInput = Console.ReadLine();
                    //don't try conversion here because if it fails the program will crash.                    
                }
            }
            //return the valid choice
            return choice;

        }


        /// <summary>
        /// executes option 1.
        /// </summary>
        private static void option1()
        {
            //initialise end flag
            bool endRequested = false;
            //tell the user what they need to do
            Console.WriteLine("Enter each sentence one at a time, pressing enter at the end of every sentence.");
            Console.WriteLine("End your last sentence with '*' to terminate entry.");
            
            //initialise the sentences list
            List<Sentence> sentences = new List<Sentence>();

            //repeat till the user asks us to stop
            while (!endRequested)
            {
                //get the user's input as a string
                string userInput = Console.ReadLine();

                //check it isn't empty
                if (userInput.Length == 0)
                {
                    //it is empty, let them know and ignore rest of loop
                    Console.WriteLine("\nThat sentence was empty so we've ignored it.\n");

                }
                else
                {
                    //it is not empty

                    //is the last character of their input the termination character?
                    if (userInput[userInput.Length - 1] == '*')
                    {
                        //yes, it is
                        //termination requested
                        endRequested = true;
                        //remove the termination character so it is not counted in analysis
                        userInput = userInput.Remove(userInput.Length - 1, 1);
                    }

                    //add sentence entered to the list of sentences so far
                    Sentence currentSentence = new Sentence(userInput);
                    sentences.Add(currentSentence);

                }
            }
            //call analyseSentences and pass the list of sentences to carry out the analysis
            analyseSentences(sentences);
            //call analyseSentiment and pass the list of sentences to find the sentiment of the text
            analyseSentiment(sentences);
        }

        /// <summary>
        /// executes option 2.
        /// </summary>
        private static void option2()
        {
            //initialise the filename, fileContents, and list of sentences for reading from file
            string fileName = "example.txt";
            string fileContents = "";
            List<Sentence> sentences = new List<Sentence>();

            //read each line from the file into a new index of an array
            string fileText = System.IO.File.ReadAllText(fileName);

            //Regular expression for finding sentences in text
            Regex sentenceRx = new Regex(@"(\S.+?[.!?])(?=\s+|$)");

            //loop through the regex matches
            foreach (Match aSentence in sentenceRx.Matches(fileText))
            {
                //add each match to the list of sentences
                sentences.Add(new Sentence(aSentence.Value));
            }

            //call analyseSentences and pass the list of sentences to carry out the analysis
            analyseSentences(sentences);
            //call findLongWords and pass the list of sentences to find long words and save to file
            findLongWords(sentences);
            //call analyseSentiment and pass the list of sentences to find the sentiment of the text
            analyseSentiment(sentences);
        }

        /// <summary>
        /// Finds the long words (over 7 characters) in a given set of sentences.
        /// </summary>
        /// <param name="sentences">The sentences.</param>
        private static void findLongWords(List<Sentence> sentences)
        {
            //set the constant for how many characters the word must exceed to be considered 'long'
            const int MIN_CHARACTERS_NEEDED = 7;

            //initialise a list of string for the words we find that are 'long'
            List<string> foundWords = new List<string>();
            
            //loop through the list of sentences, sentence by sentence
            foreach(Sentence sentence in sentences)
            {
                //get the sentence content
                string content = sentence.getSentenceContent();
                //initialise a current word holder
                string currentWord = "";
                
                //loop through the current sentence, character by character
                for(int i = 0; i < content.Length; i++)
                {
                    //get the current character in uppercase for processing ease
                    char currentChar = content[i].ToString().ToUpper().ToCharArray()[0];
                    //check if the current character not a letter, indicating the end of a word.
                    if(!char.IsLetter(currentChar))
                    {
                        //this character is not a letter, therefore, the word ended
                        if (currentWord.Length > MIN_CHARACTERS_NEEDED)
                        {
                            //this is a long word
                            //check we haven't already come accross this word
                            if (!foundWords.Contains(currentWord))
                            {
                                //add the long word to the list
                                foundWords.Add(currentWord);
                            }
                        }
                        //clear word holder
                        currentWord = "";
                    }else
                    {
                        //concat the letter to the word holder
                        currentWord += content[i];
                    }
                }
            }

            //call saveWordsToFile and pass the list of foundWords to handle saving to file
            saveWordsToFile(foundWords);


        }

        /// <summary>
        /// Saves the words to a file named LongWords.txt in the local directory.
        /// </summary>
        /// <param name="words">The words.</param>
        private static void saveWordsToFile(List<string> words)
        {
            //use WriteAllLines to write each item in the list to the file
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

            //loop through the sentence, character by character
            for (int i = 0; i < sentences.Count; i++)
            {
                //get the current sentence from the list
                Sentence currentSentence = sentences[i];
                //for every sentence add counts to total
                wordCount += currentSentence.getWordCount();
                vowelCount += currentSentence.getVowelCount();
                consonantCount += currentSentence.getConsonantCount();
                uppercaseCount += currentSentence.getUppercaseCount();
                lowercaseCount += currentSentence.getLowercaseCount();

                int[] sentenceLetterFrequency = currentSentence.getLetterFrequency();
                //for letter frequency we need to loop through each item and add it to it's respective item in the total array
                for (int n = 0; n < sentenceLetterFrequency.Length; n++)
                {
                    letterFrequency[n] += sentenceLetterFrequency[n];
                }
            }

            //print results
            //clear the console content
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
                //don't bother printing if the letter has no occurences
                if (letterFrequency[i] != 0) {
                    //get the letter in uppercase from the array index
                    char currentChar = Convert.ToChar(i + 65);
                    Console.WriteLine("The letter '{0}' appeared {1} times",currentChar,letterFrequency[i]);
                }
            }
        }

    }
}

