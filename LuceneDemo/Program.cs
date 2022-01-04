using Microsoft.Extensions.DependencyInjection;


var provider = ServiceProviderCreator.Create();
var queryDemo = provider.GetRequiredService<IQueryCreator>();

while (true)
{
    Console.WriteLine($"Enter SearchTerm:");
    var input = Console.ReadLine() ?? "n";
    var result = queryDemo.SearchIndex(input).ToList();
    //get provider 

    Console.WriteLine("---------");
    Console.WriteLine("Results:");
    //Do searches with scores
    result.ForEach(Console.WriteLine);
    Console.WriteLine("---------");
    Console.WriteLine("-- NextSearch --");
}

