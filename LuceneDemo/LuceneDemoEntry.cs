using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Util;

public class LuceneDemoEntry
{

    public void SearchIndex(Lucene.Net.Store.Directory directory)
    {
        var analyzer = new StandardAnalyzer(LuceneVersion.LUCENE_CURRENT);

        using DirectoryReader reader = DirectoryReader.Open(directory);

        IndexSearcher isearcher = new IndexSearcher(reader);

        // Parse a simple query that searches for "text":
        QueryParser parser = new QueryParser(LuceneVersion.LUCENE_CURRENT, "fieldname", analyzer);
        Query query = parser.Parse("text");
        ScoreDoc[] hits = isearcher.Search(query, null, 1000).ScoreDocs;
        // Iterate through the results:
        for (int i = 0; i < hits.Length; i++)
        {
            Document hitDoc = isearcher.Doc(hits[i].Doc);
        }
    }

    
}
