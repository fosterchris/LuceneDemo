using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Core;
using Lucene.Net.Analysis.En;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis.Util;
using Lucene.Net.Util;

/// <summary>
/// Analyser is responsible for building a token stream
/// </summary>
public class OurAnalyzer : StopwordAnalyzerBase
{
    public OurAnalyzer() : base(LuceneVersion.LUCENE_48)
    {
    }

    protected override TokenStreamComponents CreateComponents(string fieldName, TextReader reader)
    {
        var tokeniser = new StandardTokenizer(LuceneVersion.LUCENE_48, reader);
        return new TokenStreamComponents(tokeniser, tokeniser);
    }
}

//public class OurAnalyzer : StopwordAnalyzerBase
//{
//    public OurAnalyzer() : base(LuceneVersion.LUCENE_48)
//    {
//    }

//    protected override TokenStreamComponents CreateComponents(string fieldName, TextReader reader)
//    {
//        var tokeniser = new StandardTokenizer(LuceneVersion.LUCENE_48, reader);
//        TokenStream result = new LowerCaseFilter(LuceneVersion.LUCENE_48, tokeniser);
//        return new TokenStreamComponents(tokeniser, result);
//    }
//}

//public class OurAnalyzer : StopwordAnalyzerBase
//{
//    //This one has a stop words set that can remove common words
//    public OurAnalyzer() : base(LuceneVersion.LUCENE_48, StandardAnalyzer.STOP_WORDS_SET) { }

//    protected override TokenStreamComponents CreateComponents(string fieldName, TextReader reader)
//    {
//        var tokeniser = new StandardTokenizer(LuceneVersion.LUCENE_48, reader);
//        TokenStream result = new LowerCaseFilter(LuceneVersion.LUCENE_48, tokeniser);
//        result = new StopFilter(LuceneVersion.LUCENE_48, result, m_stopwords);
//        return new TokenStreamComponents(tokeniser, result);
//    }
//}

//public class OurAnalyzer : StopwordAnalyzerBase
//{
//    //This one has a stop words set that can remove common words
//    public OurAnalyzer() : base(LuceneVersion.LUCENE_48, StandardAnalyzer.STOP_WORDS_SET) { }

//    protected override TokenStreamComponents CreateComponents(string fieldName, TextReader reader)
//    {
//        var tokeniser = new StandardTokenizer(LuceneVersion.LUCENE_48, reader);
//        TokenStream result = new LowerCaseFilter(LuceneVersion.LUCENE_48, tokeniser);
//        result = new StopFilter(LuceneVersion.LUCENE_48, result, m_stopwords);
//        result = new PorterStemFilter(result);
//        return new TokenStreamComponents(tokeniser, result);
//    }
//}