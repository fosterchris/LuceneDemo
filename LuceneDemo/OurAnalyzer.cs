using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Core;
using Lucene.Net.Analysis.En;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis.Util;
using Lucene.Net.Util;

/// <summary>
/// Analyser is responsible for buildng a token stream
/// </summary>
public class OurAnalyzer : StopwordAnalyzerBase
{
    //This one has a stop words set that can remove common words
    public OurAnalyzer() : base(LuceneVersion.LUCENE_48, StandardAnalyzer.STOP_WORDS_SET) { }

    protected override TokenStreamComponents CreateComponents(string fieldName, TextReader reader)
    {
        var tokeniser = new StandardTokenizer(LuceneVersion.LUCENE_48, reader);
        //result = new LowerCaseFilter(LuceneVersion.LUCENE_48, tokeniser);
        //var result = new StopFilter(LuceneVersion.LUCENE_48, tokeniser, m_stopwords);
        //result = new PorterStemFilter(lowerCaseFilter);
        return new TokenStreamComponents(tokeniser, tokeniser);
    }
}