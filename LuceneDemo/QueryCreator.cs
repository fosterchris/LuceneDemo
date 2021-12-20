using Lucene.Net.Analysis.TokenAttributes;
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
        var directory = _directoryBuilder.GetIndex;

        using DirectoryReader reader = DirectoryReader.Open(directory);

        IndexSearcher isearcher = new IndexSearcher(reader);

        var terms = GetTerms(queryString).ToList();

        var query = new TermQuery(new Term("Title", terms.First()));

        ScoreDoc[] hits = isearcher.Search(query, null, 1000).ScoreDocs;
        // Iterate through the results:
        for (int i = 0; i < hits.Length; i++)
        {
            Document hitDoc = isearcher.Doc(hits[i].Doc);
            yield return hitDoc.Get("Title");
        }
    }

    private Query GetMultiTermQuery(List<string> terms)
    {
        var queries = terms.Select(t => new TermQuery(new Term("Title", t))).ToList();
        var bq = new BooleanQuery();
        queries.ForEach(q => bq.Add(q, Occur.SHOULD));

        return bq;
    }

    private Query GetFuzzyQuery(List<string> terms)
    {
        var query = terms.First() ?? "";
        var fq = new FuzzyQuery(new Term("Title", terms.First()));
        return fq;
    }

    //Example of how you can use the analyser to get the search terms
    private IEnumerable<string> GetTerms(string queryString)
    {
        var analyzer = _directoryBuilder.GetAnalyzer;
        var stream = analyzer.GetTokenStream("null_term_field", queryString);

        // get the CharTermAttribute from the TokenStream
        var termAtt = stream.AddAttribute<ICharTermAttribute>();

        try
        {
            stream.Reset();

            // print all tokens until stream is exhausted
            while (stream.IncrementToken())
            {
                yield return termAtt.ToString();
            }

            stream.End();
        }
        finally
        {
            stream.Dispose();
        }
    }
}
