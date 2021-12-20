public interface IQueryCreator
{
    IEnumerable<string> SearchIndex(string queryString);
}
