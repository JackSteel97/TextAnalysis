namespace TextAnalysis
{
    /// <summary>
    /// Class for storing a sentence and performing analysis of that sentence
    /// </summary>
    class Sentence
    {
        private string sentence = "";
        private int wordCount = 0;
        private int vowelCount = 0;
        private int consonantCount = 0;
        private int uppercaseCount = 0;
        private int lowercaseCount = 0;
        //amount of times each letter appears, index 0 = A, index 1 = B, etc...
        private int[] letterFrequency = new int[26];


        /// <summary>
        /// Initializes a new instance of the <see cref="Sentence"/> class.
        /// performs analysis of the sentence after initialization
        /// </summary>
        /// <param name="sentenceContent">Content of the sentence.</param>
        public Sentence(string sentenceContent){
            //set sentence content
            this.sentence = sentenceContent;

            //calculate word count
            calculateWordCount();

            //calculate vowel and consonant count
            calculateVowelAndConsonantCount();

            //calculate upper and lower case count
            calculateUpperAndLowerCaseCount();

            //calculate letter frequency
            calculateLetterFrequency();
        }


        /// <summary>
        /// Calculates the word count.
        /// </summary>
        private void calculateWordCount()
        {
            //initialise counter
            int words = 0;
            // loop through the sentence character by character
            for(int i = 0; i < sentence.Length; i++)
            {
                //if the character is a space then this is the end of a word
                if(sentence[i] == ' ')
                {
                    //increment the word count
                    words++;
                }
            }
            //add 1 to the count anyway to account for the last word not ending in a space
            words++;
            //set the global counter for this object
            this.wordCount = words;
        }

        /// <summary>
        /// Calculates the vowel count and consonant count.
        /// </summary>
        private void calculateVowelAndConsonantCount()
        {
            //initialise counters
            int vowels = 0;
            int consonants = 0;

            //loop through the sentence character by character
            for (int i = 0; i < sentence.Length; i++)
            {
                //get the current character in lowercase for ease of processing
                string currentChar = sentence[i].ToString().ToLower();
                //check if the current character is a vowel
                if (currentChar == "a" || currentChar == "e" || currentChar == "i" || currentChar == "o" || currentChar == "u")
                {
                    //increment the vowel counter
                    vowels++;

                    //else, check the character is actually an upper or lower case letter before assuming it is a consonant
                }else if(char.IsLetter(currentChar.ToCharArray()[0]))
                {
                    //increment the consonant counter
                    consonants++;
                }
            }
            //set the global counters for this object
            this.vowelCount = vowels;
            this.consonantCount = consonants;
        }

        /// <summary>
        /// Calculates the uppercase count and lowercase count.
        /// </summary>
        private void calculateUpperAndLowerCaseCount()
        {
            //initialise counters
            int lowerCase = 0;
            int upperCase = 0;

            //loop through the sentence character by character
            for (int i = 0; i < sentence.Length; i++)
            {
                //get the current character at index i
                char currentChar = sentence[i];
                
                //check if the character is uppercase
                if(char.IsUpper(currentChar))
                {
                    //upper case letter
                    //increment counter
                    upperCase++;
                    

                    //else, check if the character is lowercase 
                }else if (char.IsLower(currentChar))
                {
                    //lower case
                    //increment counter
                    lowerCase++;
                }
            }

            //set the global counters for this object
            this.lowercaseCount = lowerCase;
            this.uppercaseCount = upperCase;
        }

        /// <summary>
        /// Calculates the letter frequency of every letter.
        /// </summary>
        private void calculateLetterFrequency()
        {

            //clear the array count first
            for (int i = 0; i < 26; i++)
            {
                letterFrequency[i] = 0;
            }

            //loop through the sentence character by character
            for (int i = 0; i < sentence.Length; i++)
            {
                //get the current character at index i, in uppercase for processing ease
                char currentChar = sentence[i].ToString().ToUpper().ToCharArray()[0];
                //using ASCII value to calculate the characters position in the alphabet, if it is a letter
                int indexOfChar = (int)currentChar - 65;
                //verify it is within the alphabet range
                if(indexOfChar>=0 && indexOfChar <= 25)
                {
                    //increment the counter for this letter if it is
                    letterFrequency[indexOfChar]++;
                }
            }
        }

        /// <summary>
        /// Gets the word count.
        /// </summary>
        /// <returns>Word count as an integer</returns>
        public int getWordCount()
        {
            return this.wordCount;
        }


        /// <summary>
        /// Gets the vowel count.
        /// </summary>
        /// <returns>Vowel count as an integer</returns>
        public int getVowelCount()
        {
            return this.vowelCount;
        }

        /// <summary>
        /// Gets the consonant count.
        /// </summary>
        /// <returns>Consonant count as an integer</returns>
        public int getConsonantCount()
        {
            return this.consonantCount;
        }

        /// <summary>
        /// Gets the uppercase count.
        /// </summary>
        /// <returns>Uppercase count as an integer</returns>
        public int getUppercaseCount()
        {
            return this.uppercaseCount;
        }

        /// <summary>
        /// Gets the lowercase count.
        /// </summary>
        /// <returns>Lowercase count as an integer</returns>
        public int getLowercaseCount()
        {
            return this.lowercaseCount;
        }

        /// <summary>
        /// Gets the letter frequency.
        /// </summary>
        /// <returns>Array of letter frequency for this sentence</returns>
        public int[] getLetterFrequency()
        {
            return this.letterFrequency;
        }

        /// <summary>
        /// Gets the content of the sentence.
        /// </summary>
        /// <returns>Sentence content as a string</returns>
        public string getSentenceContent()
        {
            return sentence;
        }
    }
}
