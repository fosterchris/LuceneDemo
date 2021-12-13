using Lucene.Net.Analysis;

public interface IDirectoryBuilder
{
    Analyzer GetAnalyzer { get; }
    Lucene.Net.Store.Directory GetIndex { get; }
}