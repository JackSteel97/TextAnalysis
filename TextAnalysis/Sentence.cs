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
        private int characterCount = 0;
        private int vowelCount = 0;
        private int consonantCount = 0;
        private int uppercaseCount = 0;
        private int lowercaseCount = 0;
        private int[] letterFrequency = new int[25];


        public Sentence(string sentenceContent){
            this.sentence = sentenceContent;

            //calculate word count
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

    }
}
