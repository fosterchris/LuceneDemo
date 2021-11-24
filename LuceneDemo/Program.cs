using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();

IServiceProvider provider = serviceCollection.BuildServiceProvider();

while(true)
{
    var input = Console.ReadLine();

    //get provider 

    //Do searches with scores

    Console.WriteLine(input);
    Console.WriteLine("Do you want to quit? (y/n)");
    if (Console.ReadKey().KeyChar.ToString() == "y")
    {
        break;
    }
}