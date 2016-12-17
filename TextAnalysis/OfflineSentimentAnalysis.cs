using System.Collections.Generic;

namespace TextAnalysis {

    /// <summary>
    /// Class for handling offline sentiment analysis
    /// </summary>
    public class OfflineSentimentAnalysis {

        // string arrays for storing the raw lines from each text file
        private string[] posLines, negLines;

        // lists of string for storing the words from each text file
        private List<string> positiveWords;

        private List<string> negativeWords;

        /// <summary>
        /// Initialises a new instance of the <see cref="OfflineSentimentAnalysis"/> class.
        /// </summary>
        public OfflineSentimentAnalysis () {
            posLines = System.IO.File.ReadAllLines("positive-words.txt");
            negLines = System.IO.File.ReadAllLines("negative-words.txt");
        }

        /// <summary>
        /// Initialises this instance, reading from the text files.
        /// </summary>
        public void init () {
            //initialise each list
            positiveWords = new List<string>();
            negativeWords = new List<string>();

            //go through the positive lines
            foreach(string line in posLines) {
                //if it's a comment or empty, skip line
                if(!line.Trim().StartsWith(";") && line.Length > 0) {
                    //otherwise add the word to the list
                    positiveWords.Add(line);
                }
            }

            //go through the negative lines
            foreach(string line in negLines) {
                //if it's a comment or empty, skip line
                if(!line.Trim().StartsWith(";") && line.Length > 0) {
                    //otherwise add the word to the list
                    negativeWords.Add(line);
                }
            }
        }

        /// <summary>
        /// Analyses the sentences.
        /// </summary>
        /// <param name="sentences">The sentences.</param>
        /// <returns>A percentage value, 0 being very negative, 100 being very positive</returns>
        public double analyseSentences (List<Sentence> sentences) {
            //initialise counters
            int positiveWordCount = 0;
            int negativeWordCount = 0;
            int totalWordCount = 0;

            //count and categorise the words
            //loop through the sentences
            foreach(Sentence sentence in sentences) {
                //get an array of the words in that sentence, by splitting at every space
                string[] words = sentence.getSentenceContent().Split(' ');

                //loop through the array of words
                foreach(string word in words) {
                    //increment the word counter
                    totalWordCount++;
                    //check if the word is in the positive list

                    if(positiveWords.Contains(word)) {
                        //increment the positive word count if it is
                        positiveWordCount++;
                    }
                    //check if the word is in the negative list
                    else if(negativeWords.Contains(word)) {
                        //increment the negative word count if it is
                        negativeWordCount++;
                    }
                }
            }

            // assign weights to each word category and calculate a score
            int posScore = 100 * positiveWordCount;
            int negScore = 0 * negativeWordCount;
            //the number of neutral words is equal to the total number of words minus the number of positive and negative words
            int neutScore = 50 * (totalWordCount - positiveWordCount - negativeWordCount);

            //calculate a percentage value based on the weighted scores
            double sentimentScore = (posScore + negScore + neutScore) / totalWordCount;

            //return percentage
            return sentimentScore;
        }
    }
}