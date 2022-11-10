namespace TextStatisticsProject.Interfaces
{
    public interface ITextStatistics
    {
        /**
         * @return name of the book.
         */
        string BookName();

        /**
        * Returns a list of the most frequented words of the text.
        * @param n how many items of the list
        * @return a list representing the top n frequent words of the text.
        */
        List<IWordFrequency> TopWords(int n);

        /**
        * Returns a list of the longest words of the text.
        * @param n how many items to return.
        * @return a list with the n longest words of the text.
        */
        List<string> LongestWords(int n);

        /**
        * @return total number of words in the text.
        */
        long NumberOfWords();

        /**
        * @return total number of line of the text.
        */
        long NumberOfLines();

        /**
         * @return a string containing statistics about the book.
         */
        string ToString(int nbrOfLongWords, int nbrOfTopWords);
    }

}
