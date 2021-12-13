using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Util;

public class QueryCreator : IQueryCreator
{
    private readonly IDirectoryBuilder _directoryBuilder;

    public QueryCreator(IDirectoryBuilder directoryBuilder)
    {
        _directoryBuilder = directoryBuilder;
    }

    public IEnumerable<string> SearchIndex(string queryString)
    {
        var analyzer = _directoryBuilder.GetAnalyzer;
        var directory = _directoryBuilder.GetIndex;

        using DirectoryReader reader = DirectoryReader.Open(directory);

        IndexSearcher isearcher = new IndexSearcher(reader);

        // Parse a simple query that searches for "text":
        QueryParser parser = new QueryParser(LuceneVersion.LUCENE_CURRENT, "Title", analyzer);
        
        Query query = parser.Parse(queryString);
        
        ScoreDoc[] hits = isearcher.Search(query, null, 1000).ScoreDocs;
        // Iterate through the results:
        for (int i = 0; i < hits.Length; i++)
        {
            Document hitDoc = isearcher.Doc(hits[i].Doc);
            yield return hitDoc.Get("Title");
        }
    }
}

public interface IQueryCreator
{
    IEnumerable<string> SearchIndex(string queryString);
}
