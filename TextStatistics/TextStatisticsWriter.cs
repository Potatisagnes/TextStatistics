using TextStatisticsProject.Interfaces;

namespace TextStatisticsProject
{
    public class TextStatisticsWriter
    {
        // Dictionary: key = book name, value = book source
        private readonly Dictionary<string, string> _books;
        private readonly HttpClient _httpClient;

        public TextStatisticsWriter(Dictionary<string, string> books)
        {
            _books = books;
            _httpClient = new HttpClient();
        }

        public async Task WriteTextStatistics(int nbrOfTopWords, int nbrOfLongWords)
        {
            List<ITextStatistics> bookStats = new List<ITextStatistics>();
            
            // Download the book collection
            foreach (KeyValuePair<string, string> book in _books)
            {
                Console.WriteLine("Loadning " + book.Key + "...");
                string? content = await GetBookAsync(book.Value);
                if (content != null) {
                    bookStats.Add(new TextStatistics(book.Key, content));
                    Console.WriteLine(book.Key + " was loaded!");
                }
                else
                {
                    Console.WriteLine(book.Key + " was not loaded");
                }
            }

            // Add summation of all books last in list 
            bookStats.Add(new TextStatistics(bookStats));

            // Write statistics for the book collection in console
            foreach(ITextStatistics stat in bookStats)
            {
                Console.WriteLine(stat.ToString(nbrOfTopWords, nbrOfLongWords));
            }
        }

        // Download a book
        private async Task<string?> GetBookAsync(string bookUri)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(bookUri);
            try
            {
                response.EnsureSuccessStatusCode();
                string responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
