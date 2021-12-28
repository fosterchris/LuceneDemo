public interface IPatternTokenizer
{
    IEnumerable<string> GetTerms(string queryString);

}