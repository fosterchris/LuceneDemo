using Microsoft.Extensions.DependencyInjection;

public static class ServiceProviderCreator
{
    public static IServiceProvider Create()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IDirectoryBuilder, DirectoryBuilder>();
        serviceCollection.AddTransient<IQueryCreator, QueryCreator>();
        return serviceCollection.BuildServiceProvider();
    }
}