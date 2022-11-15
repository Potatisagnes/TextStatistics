using TextStatisticsProject.Interfaces;

namespace TextStatisticsProject
{
    public class TextStatistics : ITextStatistics
    {
        private readonly string _bookName;
        private readonly long _nbrOfLines;
        private readonly List<IWordFrequency> _wordFrequencies;

        // Creates a text statistics object from a book name and a book's content. 
        public TextStatistics(string name, string content)
        {
            _bookName = name;
            _nbrOfLines = content.Split('\n').Length;
            _wordFrequencies = new List<IWordFrequency>();

            string[] toRemove = { "'s", "'", "\r", ".", ",", ":", ";", "!", "(", ")", "?", "[", "]", "{", "}"};
            foreach (string s in toRemove)
            {
                content = content.Replace(s, "");
            }
            char[] toReplace = { '-', '"', '_', '/', '&', '@', '£', '#', '='};
            foreach(char c in toReplace)
            {
                content = content.Replace(c, ' ');
            }
            char[] splitChars = { ' ', '\t', '\n' };
            string[] words = content.Split(splitChars);
            foreach (string word in words)
            {
                if (word.ToLower() != "")
                {
                    IWordFrequency wordFrequency = new WordFrequency(word.ToLower());
                    IWordFrequency? found = _wordFrequencies.Find(w => w.Word() == wordFrequency.Word());
                    if (found == null)
                    {
                        _wordFrequencies.Add(wordFrequency);
                    }
                    else
                    {
                        found.Count();
                    }
                }
            }
        }
        
        // Creates a TextStatistics object by combining a set of TextStatistics objects. 
        public TextStatistics(List<ITextStatistics> allBooks)
        {
            _bookName = "All books";
            _nbrOfLines = 0;
            _wordFrequencies = new List<IWordFrequency>();
            foreach (TextStatistics book in allBooks)
            {
                _nbrOfLines += book._nbrOfLines;
                // Check if a word already exists in _wordFrequencies to prevent duplicates 
                foreach (IWordFrequency wordFrequency in book._wordFrequencies)
                {
                    IWordFrequency? found = _wordFrequencies.Find(w => w.Word() == wordFrequency.Word());
                    if (found == null) { _wordFrequencies.Add(wordFrequency); }
                    else
                    {
                        found.Count(wordFrequency.Frequency());
                    }
                }
            }
        }

        public string BookName()
        {
            return _bookName;
        }

        public List<IWordFrequency> TopWords(int n)
        {
            List<IWordFrequency> topWords = new List<IWordFrequency>();
            _wordFrequencies.Sort(CompareFrequenciesDesc);

            int counter = 0;
            while (_wordFrequencies.Count > counter && counter < n)
            {
                topWords.Add(_wordFrequencies[counter]);
                counter++;
            }
            return topWords;
        }

        public List<string> LongestWords(int n)
        {
            List<string> longestWords = new List<string>();

            _wordFrequencies.Sort(CompareLengthDesc);
            int counter = 0;
            while (_wordFrequencies.Count > counter && counter < n)
            {
                longestWords.Add(_wordFrequencies[counter].Word());
                counter++;
            }
            return longestWords;
        }

        public long NumberOfWords()
        {
            return _wordFrequencies.Count;
        }

        public long NumberOfLines()
        {
            return _nbrOfLines;
        }

        public string ToString(int nbrOfLongWords, int nbrOfTopWords)
        {
            string statString = "\n";
            statString += _bookName + ":\n";
            statString += "Number of lines = " + _nbrOfLines + '\n';
            statString += "Number of unique words = " + NumberOfWords() + '\n';
            statString += "The " + nbrOfLongWords + " longest words are: [ ";
            foreach (string word in LongestWords(nbrOfLongWords))
            {
                statString += word + " ";
            }
            statString += "]\n";
            statString += "The " + nbrOfTopWords + " most common words are: [ ";
            foreach (IWordFrequency wordFrequency in TopWords(nbrOfTopWords))
            {
                statString += wordFrequency.Word() + " ";
            }
            statString += "]\n";
            return statString;
        }

        private int CompareFrequenciesDesc(IWordFrequency w1, IWordFrequency w2)
        {
            if (w1.Frequency() > w2.Frequency()) return -1;
            if (w1.Frequency() < w2.Frequency()) return 1;
            return 0;
        }
        private int CompareLengthDesc(IWordFrequency w1, IWordFrequency w2)
        {
            if (w1.Word().Length > w2.Word().Length) return -1;
            if (w1.Word().Length < w2.Word().Length) return 1;
            return 0;
        }
    }
}
