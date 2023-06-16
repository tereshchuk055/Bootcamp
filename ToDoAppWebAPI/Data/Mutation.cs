using ToDoAppWebAPI.Services;
using ToDoAppWebAPI.Types;
using System.Text.Json;

namespace ToDoAppWebAPI.Data
{
    public class Mutation : ObjectGraphType
    {
        public Mutation()
        {
            Field<StringGraphType>("createTask")
                .Argument<InputTaskType>("task")
                .Resolve(resolve =>
                {
                    var factory = resolve.RequestServices.GetRequiredService<RepositoryFactory>();
                    var task = resolve.GetArgument<TaskDto>("task");
                    factory.GetTaskRepository().Add(task);
                    return JsonSerializer.Serialize(true);
                });

            Field<StringGraphType>("createCategory")
                .Argument<InputCategoryType>("category")
                .Resolve(resolve =>
                {
                    var factory = resolve.RequestServices.GetRequiredService<RepositoryFactory>();
                    var category = resolve.GetArgument<CategoryDto>("category");
                    factory.GetCategoryRepository().Add(category);
                    return JsonSerializer.Serialize(true);
                });

            Field<StringGraphType>("changeCompletedState")
                .Argument<bool>("IsCompleted")
                .Argument<int>("TaskId")
                .Resolve(resolve =>
                {
                    var factory = resolve.RequestServices.GetRequiredService<RepositoryFactory>();
                    factory.GetTaskRepository().ChangeCompletedState(
                        resolve.GetArgument<int>("TaskId"),
                        resolve.GetArgument<bool>("IsCompleted"));
                    return JsonSerializer.Serialize(true);
                });

            Field<StringGraphType>("deleteTask")
                .Argument<int>("TaskId")
                .Resolve(resolve =>
                {
                    var factory = resolve.RequestServices.GetRequiredService<RepositoryFactory>();
                    factory.GetTaskRepository().Delete(resolve.GetArgument<int>("TaskId"));
                    return JsonSerializer.Serialize(true);
                });

            Field<StringGraphType>("deleteCategory")
                .Argument<int>("CategoryId")
                .Resolve(resolve =>
                {
                    var factory = resolve.RequestServices.GetRequiredService<RepositoryFactory>();
                    factory.GetCategoryRepository().Delete(resolve.GetArgument<int>("CategoryId"));
                    return JsonSerializer.Serialize(true);
                });
        }
    }
}
