using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();

IServiceProvider provider = serviceCollection.BuildServiceProvider();
