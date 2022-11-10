using TextStatisticsProject;

string fileContent = File.ReadAllText(@"../../../BooksToAnalyze.txt").Replace("\r", "");
char[] sepatators = { '\t', '\n' };
string[] booksToAnalyze = fileContent.Split(sepatators);
Dictionary<string, string> books = new Dictionary<string, string>();
int index = 0;
while (index + 1 < booksToAnalyze.Length)
{
    books.Add(booksToAnalyze[index], booksToAnalyze[index +1]);
    index = index + 2;
}
TextStatisticsWriter summator = new TextStatisticsWriter(books);
await summator.WriteTextStatistics(20, 10);