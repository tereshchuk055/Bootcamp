using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ToDoAppWebAPI.Services;
using ToDoAppWebAPI.Types;

namespace ToDoAppWebAPI.Data
{
    public class Query : ObjectGraphType
    {
        public Query()
        {
            Field<ListGraphType<TaskType>>(Name = "tasks")
                .Resolve(resolve =>
                {
                    var factory = resolve.RequestServices.GetRequiredService<RepositoryFactory>();
                    return factory.GetTaskRepository().Get();
                });


            Field<ListGraphType<TaskType>>(Name = "tasksByCategoryId")
                .Argument<IntGraphType>("CategoryId")
                .Resolve(resolve =>
                {
                    var factory = resolve.RequestServices.GetRequiredService<RepositoryFactory>();
                    return factory.GetTaskRepository().GetByCategory(resolve.GetArgument<int>("CategoryId"));
                });


            Field<ListGraphType<CategoryType>>(Name = "categories")
                .Resolve(resolve =>
                {
                    var factory = resolve.RequestServices.GetRequiredService<RepositoryFactory>();
                    return factory.GetCategoryRepository().Get();
                });
        }
    }
}
