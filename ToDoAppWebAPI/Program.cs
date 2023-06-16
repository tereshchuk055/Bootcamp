global using GraphQL.Types;
global using GraphQL;

global using ToDoApp.Models;
global using ToDoApp.Repository;
global using ToDoApp.Storage;

using ToDoAppWebAPI.Services;
using ToDoApp.Services;
using Schema = ToDoAppWebAPI.Data.Schema;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<DbContext>();
builder.Services.AddTransient<RepositoryFactory>(provider =>
{
    var headers = provider?.GetRequiredService<IHttpContextAccessor>().HttpContext.Request.Headers ?? throw new Exception();
    if (headers.TryGetValue("Storage-Type", out var value) && Enum.TryParse<StorageType>(value, out var type))
    {
        return new RepositoryFactory(provider.GetRequiredService<DbContext>(), type);
    }
    else
    {
        throw new Exception();
    }
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader();
        });
});

builder.Services.AddGraphQL((opt =>
{
    opt.AddSystemTextJson();
    GraphQLBuilderExtensions.AddSchema<Schema>(opt);
    opt.AddGraphTypes(typeof(Schema).Assembly);
}));

var app = builder.Build();

app.UseCors("MyAllowSpecificOrigins");
app.UseGraphQL<Schema>();
app.UseGraphQLAltair();
app.Run();

