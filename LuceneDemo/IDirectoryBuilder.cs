using Lucene.Net.Analysis;

public interface IDirectoryBuilder
{
    Lucene.Net.Store.Directory GetIndex { get; }
}