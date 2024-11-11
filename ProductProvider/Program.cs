using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductProvider.Infrastructure.Data.Contexts;
using ProductProvider.Infrastructure.GraphQL.Mutations;
using ProductProvider.Infrastructure.GraphQL.ObjectTypes;
using ProductProvider.Infrastructure.GraphQL.Queries;
using ProductProvider.Infrastructure.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddPooledDbContextFactory<DataContext>(x =>
        {
            var cosmosUri = Environment.GetEnvironmentVariable("COSMOS_URI");
            var cosmosDb = Environment.GetEnvironmentVariable("COSMOS_DB");

            if (string.IsNullOrEmpty(cosmosUri) || string.IsNullOrEmpty(cosmosDb))
            {
                throw new InvalidOperationException("COSMOS_URI and COSMOS_DB environment variables must be set.");
            }

            x.UseCosmos(cosmosUri, cosmosDb).UseLazyLoadingProxies();

            //x.UseCosmos(Environment.GetEnvironmentVariable("COSMOS_URI")!, Environment.GetEnvironmentVariable("COSMOS_DB")!)
            //.UseLazyLoadingProxies();
        }
            );

        services.AddCors(options =>
        {
            options.AddPolicy("AllowReactApp", policy =>
            {
                policy.WithOrigins("*", "rikawebappgrupp8-gtg2dxecc0hac3a7.westeurope-01.azurewebsites.net")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        services.AddScoped<IProductService, ProductService>();

        services.AddGraphQLFunction()
                .AddQueryType<ProductQuery>()
                .AddMutationType<ProductMutation>()
                .AddType<ProductType>()
                .AddType<SizeType>();

        var sp = services.BuildServiceProvider();
        using var scope = sp.CreateScope();
        var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<DataContext>>();
        using var context = dbContextFactory.CreateDbContext();
        context.Database.EnsureCreated();
    })
    .Build();

host.Run();
