using Lucene.Net.Analysis.TokenAttributes;

public class PatternTokenizer : IPatternTokenizer
    {
        //Example of how you can use the analyser to get the search terms
        public IEnumerable<string> GetTerms(string queryString)
        {
            var analyzer = new OurAnalyzer();

            //We must pass a field name into the analyser as it can contain "by field" representation
            var stream = analyzer.GetTokenStream("Fact", queryString);

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
