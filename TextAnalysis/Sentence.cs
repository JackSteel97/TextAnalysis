using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalysis
{
    class Sentence
    {
        private string sentence = "";
        private int wordCount = 0;
        private int vowelCount = 0;
        private int consonantCount = 0;
        private int uppercaseCount = 0;
        private int lowercaseCount = 0;
        private int[] letterFrequency = new int[26];


        public Sentence(string sentenceContent){
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


        private void calculateWordCount()
        {
            int words = 0;
            for(int i = 0; i < sentence.Length; i++)
            {
                if(sentence[i] == ' ')
                {
                    words++;
                }
            }
            words++;
            this.wordCount = words;
        }

        private void calculateVowelAndConsonantCount()
        {
            int vowels = 0;
            int consonants = 0;

            for (int i = 0; i < sentence.Length; i++)
            {
                string currentChar = sentence[i].ToString().ToLower();
                if (currentChar == "a" || currentChar == "e" || currentChar == "i" || currentChar == "o" || currentChar == "u")
                {
                    vowels++;

                    //else, check the character is actually an upper or lower case letter before assuming it is a consonant
                }else if(((int)currentChar.ToCharArray()[0]<=90 && (int)currentChar.ToCharArray()[0] >= 65) || ((int)currentChar.ToCharArray()[0] <=122 && (int)currentChar.ToCharArray()[0] >= 97))
                {
                    consonants++;
                }
            }
            this.vowelCount = vowels;
            this.consonantCount = consonants;
        }

        private void calculateUpperAndLowerCaseCount()
        {
            int lowerCase = 0;
            int upperCase = 0;

            for (int i = 0; i < sentence.Length; i++)
            {
                char currentChar = sentence[i];

                if((int)currentChar <= 90 && (int)currentChar>= 65)
                {
                    //upper case letter
                    upperCase++;
                }else if ((int) currentChar <= 122 && (int)currentChar >= 97)
                {
                    //lower case
                    lowerCase++;
                }
            }

            this.lowercaseCount = lowerCase;
            this.uppercaseCount = upperCase;
        }

        private void calculateLetterFrequency()
        {

            //clear the array count first
            for (int i = 0; i < 26; i++)
            {
                letterFrequency[i] = 0;
            }

            for (int i = 0; i < sentence.Length; i++)
            {
                char currentChar = sentence[i].ToString().ToUpper().ToCharArray()[0];
                //using ASCII value to calculate the characters position in the alphabet, if it is a letter
                int indexOfChar = (int)currentChar - 65;
                //verify it is within the alphabet range
                if(indexOfChar>=0 && indexOfChar <= 25)
                {
                    letterFrequency[indexOfChar]++;
                }
            }
        }

        public int getWordCount()
        {
            return this.wordCount;
        }


        public int getVowelCount()
        {
            return this.vowelCount;
        }

        public int getConsonantCount()
        {
            return this.consonantCount;
        }

        public int getUppercaseCount()
        {
            return this.uppercaseCount;
        }

        public int getLowercaseCount()
        {
            return this.lowercaseCount;
        }

        public int[] getLetterFrequency()
        {
            return this.letterFrequency;
        }
    }
}
