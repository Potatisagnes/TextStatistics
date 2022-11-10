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

        public void Count()
        {
            _frequency++;
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
