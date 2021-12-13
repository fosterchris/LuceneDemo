using Microsoft.Extensions.DependencyInjection;


var provider = ServiceProviderCreator.Create();
var queryDemo = provider.GetRequiredService<IQueryCreator>();

while (true)
{
    var input = Console.ReadLine() ?? "n";
    var result = queryDemo.SearchIndex(input).ToList();
    //get provider 

    //Do searches with scores
    result.ForEach(Console.WriteLine);
    Console.WriteLine("-- NextSearch --");
}

