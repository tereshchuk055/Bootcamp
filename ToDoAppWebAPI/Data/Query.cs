using Microsoft.AspNetCore.Mvc;
using ToDoAppWebAPI.Services;
using ToDoAppWebAPI.Types;

namespace ToDoAppWebAPI.Data
{
    public class Query : ObjectGraphType
    {
        public Query(RepositoryFactory factory)
        {

            Field<ListGraphType<TaskType>>(Name = "tasks")
                .Resolve(resolve => factory.GetTaskRepository().Get());

            Field<ListGraphType<TaskType>>(Name = "tasksByCategoryId")
                .Argument<IntGraphType>("CategoryId")
                .Resolve(resolve => factory.GetTaskRepository().GetByCategory(resolve.GetArgument<int>("CategoryId")));

            Field<ListGraphType<CategoryType>>(Name = "categories")
                .Resolve(resolve => factory.GetCategoryRepository().Get());
        }
    }
}
