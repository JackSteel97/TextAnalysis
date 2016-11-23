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
        private int[] letterFrequency = new int[25];


        public Sentence(string sentenceContent){
            this.sentence = sentenceContent;

            //calculate word count
            calculateWordCount();

            //calculate vowel count
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

        private void calculateVowelCount()
        {
            int vowels = 0;
            for (int i = 0; i < sentence.Length; i++)
            {
                string currentChar = sentence[i].ToString().ToLower();
                if (currentChar == "a" || currentChar == "e" || currentChar == "i" || currentChar == "o" || currentChar == "u")
                {
                    vowels++;
                }
            }
            vowels++;
            this.vowelCount = vowels;
        }

    }
}
