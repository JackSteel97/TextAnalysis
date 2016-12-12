using System;
using System.Collections.Generic;


namespace TextAnalysis
{

    public class OfflineSentimentAnalysis
    {
        string[] posLines, negLines;
        List<string> positiveWords;
        List<string> negativeWords;

        public OfflineSentimentAnalysis()
        {
            posLines = System.IO.File.ReadAllLines("positive-words.txt");
            negLines = System.IO.File.ReadAllLines("negative-words.txt");
        }

        public void init()
        {
            positiveWords = new List<string>();
            negativeWords = new List<string>();


            //go through the positive lines
            foreach (string line in posLines)
            {
                //if it's a comment, skip line
                if (!line.Trim().StartsWith(";") && line.Length > 0)
                {
                    positiveWords.Add(line);
                }

            }

            //go through the negative lines
            foreach (string line in negLines)
            {
                //if it's a comment, skip line
                if(!line.Trim().StartsWith(";") && line.Length > 0)
                {
                    negativeWords.Add(line);
                }

            }
        }


        public double analyseSentences(List<Sentence> sentences)
        {
            int positiveWordCount = 0;
            int negativeWordCount = 0;
            int totalWordCount = 0;

            //count and categorise the words
            foreach(Sentence sentence in sentences)
            {
                string[] words = sentence.getSentenceContent().Split(' ');

                foreach (string word in words)
                {
                    totalWordCount++;
                    if(!(positiveWords.Contains(word) && negativeWords.Contains(word))) {
                        if(positiveWords.Contains(word)) {
                            positiveWordCount++;
                        }else if(negativeWords.Contains(word)) { 
                            negativeWordCount++;
                        }
                    }
                }
            }


            
            int posScore = 100 * positiveWordCount;
            int negScore = 0 * negativeWordCount;
            int neutScore = 50 * (totalWordCount - positiveWordCount - negativeWordCount);

            double sentimentScore = (posScore + negScore + neutScore) / totalWordCount;

            return sentimentScore;



        }
    }
}

