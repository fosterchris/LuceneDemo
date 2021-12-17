using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Core;
using Lucene.Net.Analysis.En;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis.Util;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Util;

public class DirectoryBuilder : IDirectoryBuilder
{
    private readonly LuceneVersion _luceneVersion = LuceneVersion.LUCENE_CURRENT;
    private readonly Analyzer _analyzer;
    private Lucene.Net.Store.Directory _index;

    public DirectoryBuilder()
    {
        _analyzer = new OurAnalyzer();
    }

    public Analyzer GetAnalyzer{ get => _analyzer; }
    public Lucene.Net.Store.Directory GetIndex { get => _index ?? BuildDirectory(); }

    private RAMDirectory BuildDirectory()
    {
        //Build index

        // Store the index in memory:
        var directory = new RAMDirectory();
        IndexWriterConfig config = new IndexWriterConfig(_luceneVersion, _analyzer);
        using IndexWriter iwriter = new IndexWriter(directory, config);

        var doc = CreateStandardDocument();
        var doc2 = CreateCapitalizedCaseDocument();
        var doc3 = CreatePluralDocument();
        var doc4 = CreateFoldingDocument();



        iwriter.AddDocument(doc, _analyzer);
        iwriter.AddDocument(doc2, _analyzer);
        iwriter.AddDocument(doc3, _analyzer);
        iwriter.AddDocument(doc4, _analyzer);
        iwriter.Dispose();

        return directory;
    }

    private Document CreateStandardDocument()
    {
        Document doc = new Document();

        //Standard example of getting title
        doc.Add(new Field("Title", "potato stew", TextField.TYPE_STORED));

        //Standard example to show normalisation
        return doc;
    }

    private Document CreateCapitalizedCaseDocument()
    {
        Document doc = new Document();

        //Standard example of getting title
        doc.Add(new Field("Title", "Mash Potato", TextField.TYPE_STORED));

        //Standard example to show normalisation
        return doc;
    }

    private Document CreatePluralDocument()
    {
        Document doc = new Document();

        //Standard example of getting title
        doc.Add(new Field("Title", "Mash Potatoes", TextField.TYPE_STORED));

        //Standard example to show normalisation
        return doc;
    }

    private Document CreateFoldingDocument()
    {
        Document doc = new Document();

        //Standard example of getting title
        doc.Add(new Field("Title", "purée de pommes de terre", TextField.TYPE_STORED));

        //Standard example to show normalisation
        return doc;
    }


    
}

/// <summary>
/// Analyser is responsible for buildng a token stream
/// </summary>
public class OurAnalyzer : StopwordAnalyzerBase
{
    //Analzer
    //public OurAnalyzer() : base(LuceneVersion.LUCENE_48) {}

    //This one has a stop words set that can remove common words
    public OurAnalyzer() : base(LuceneVersion.LUCENE_48, StandardAnalyzer.STOP_WORDS_SET) { }
    protected override TokenStreamComponents CreateComponents(string fieldName, TextReader reader)
    {
        var tokeniser = new StandardTokenizer(LuceneVersion.LUCENE_48, reader);
        var lcfilter = new LowerCaseFilter(LuceneVersion.LUCENE_48, tokeniser);
        var stemmer = new PorterStemFilter(lcfilter);
        return new TokenStreamComponents(tokeniser, stemmer);
    }
}