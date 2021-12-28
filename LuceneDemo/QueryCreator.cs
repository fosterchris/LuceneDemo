using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;

public class QueryCreator : IQueryCreator
{
    private readonly IDirectoryBuilder _directoryBuilder;
    private readonly IPatternTokenizer _patternTokenizer;

    public QueryCreator(IDirectoryBuilder directoryBuilder, IPatternTokenizer patternTokenizer)
    {
        _directoryBuilder = directoryBuilder;
        _patternTokenizer = patternTokenizer;
    }

    public IEnumerable<string> SearchIndex(string queryString)
    {
        var directory = _directoryBuilder.GetIndex;

        using DirectoryReader reader = DirectoryReader.Open(directory);

        IndexSearcher isearcher = new IndexSearcher(reader);

        var terms = _patternTokenizer.GetTerms(queryString).ToList();

        var query = new TermQuery(new Term("Fact", terms.First()));

        ScoreDoc[] hits = isearcher.Search(query, null, 1000).ScoreDocs;
        // Iterate through the results:
        for (int i = 0; i < hits.Length; i++)
        {
            Document hitDoc = isearcher.Doc(hits[i].Doc);
            yield return hitDoc.Get("Fact");
        }
    }

    private Query GetMultiTermQuery(List<string> terms)
    {
        var queries = terms.Select(t => new TermQuery(new Term("Fact", t))).ToList();
        var bq = new BooleanQuery();
        queries.ForEach(q => bq.Add(q, Occur.SHOULD));

        return bq;
    }

    private Query GetFuzzyQuery(List<string> terms)
    {
        var query = terms.First() ?? "";
        var fq = new FuzzyQuery(new Term("Fact", terms.First()));
        return fq;
    }
}