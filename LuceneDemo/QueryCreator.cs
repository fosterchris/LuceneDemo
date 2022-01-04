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

        if (terms.Count == 0) yield break;

        var query = GetSingleTermQuery(terms);

        ScoreDoc[] hits = isearcher.Search(query, null, 1000).ScoreDocs;

        // Iterate through the results:
        for (int i = 0; i < hits.Length; i++)
        {
            Document hitDoc = isearcher.Doc(hits[i].Doc);
            yield return hitDoc.Get("Fact");
        }
    }

    private Query GetSingleTermQuery(List<string> terms)
    {
        return new TermQuery(new Term("Fact", terms.First()));
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
        var fq = new FuzzyQuery(new Term("Fact", terms.First()), 2);
        return fq;
    }
}