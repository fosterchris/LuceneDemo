using Lucene.Net.Analysis;
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

    public Lucene.Net.Store.Directory GetIndex { get => _index ?? BuildDirectory(); }

    private RAMDirectory BuildDirectory()
    {
        //Build index

        // Store the index in memory:
        var directory = new RAMDirectory();
        IndexWriterConfig config = new IndexWriterConfig(_luceneVersion, _analyzer);
        using IndexWriter iwriter = new IndexWriter(directory, config);

        var doc = CreateDocument("Joe Bloggs", "Potato contains vitamin B6");
        var doc2 = CreateDocument("Steve Irwin", "Vodka can be made with potatoes");

        iwriter.AddDocument(doc, _analyzer);
        iwriter.AddDocument(doc2, _analyzer);
        iwriter.Dispose();

        _index = directory;
        return directory;
    }

    private Document CreateDocument(string author, string fact)
    {
        Document doc = new Document();

        //Standard example of getting title
        doc.Add(new Field("Fact", fact, TextField.TYPE_STORED));
        doc.Add(new Field("Author", author, TextField.TYPE_STORED));

        //Standard example to show normalisation
        return doc;
    }    
}
