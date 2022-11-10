using TextStatisticsProject.Interfaces;

namespace TextStatisticsProject
{
    public class WordFrequency : IWordFrequency
    {
        private readonly string _word;
        private long _frequency;

        public WordFrequency(string word)
        {
            _word = word;
            _frequency = 1;
        }

        public void Count(long n = 1)
        {
            _frequency += n;
        }

        public string Word()
        {
            return _word;
        }

        public long Frequency()
        {
            return _frequency;
        }
    }
}
