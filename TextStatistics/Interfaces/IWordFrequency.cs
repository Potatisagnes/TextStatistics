namespace TextStatisticsProject.Interfaces
{
    /**
    * Represents a word and its frequency.
    */
    public interface IWordFrequency
    {
        /**
        * The word.
        * @return the word as a string.
        */
        string Word();

        /**
         * Adds 1 to frequency of this word
         */
        void Count();

        /**
        * The frequency.
        * @return a long representing the frequency of the word.
        */
        long Frequency();
    }
}
