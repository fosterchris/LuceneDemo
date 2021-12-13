using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
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
        _analyzer = new StandardAnalyzer(_luceneVersion);
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

        Document doc = new Document();
        var text = "melons potatoes";
        doc.Add(new Field("ingredients", text, TextField.TYPE_NOT_STORED));
        doc.Add(new Field("Title", "potato stew", TextField.TYPE_STORED));
        iwriter.AddDocument(doc);
        iwriter.Dispose();

        return directory;
    }
}