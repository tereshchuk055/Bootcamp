global using GraphQL.Types;
global using GraphQL;

global using ToDoApp.Models;
global using ToDoApp.Repository;
global using ToDoApp.Storage;

using ToDoAppWebAPI.Services;
using ToDoApp.Services;
using ToDoAppWebAPI.Data;
using ToDoAppWebAPI.Types;
using Schema = ToDoAppWebAPI.Data.Schema;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<DbContext>();
builder.Services.AddTransient<RepositoryFactory>();

builder.Services.AddGraphQL((opt =>
{
    opt.AddSystemTextJson();
    GraphQLBuilderExtensions.AddSchema<Schema>(opt);
    opt.AddGraphTypes(typeof(Schema).Assembly);
}));

var app = builder.Build();

app.UseGraphQL<Schema>();
app.UseGraphQLAltair();
app.Run();

