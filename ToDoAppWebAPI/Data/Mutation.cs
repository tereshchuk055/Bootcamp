using System.Threading.Tasks;
using ToDoAppWebAPI.Services;
using ToDoAppWebAPI.Types;

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
                    return "Task added successfully.";
                });

            Field<StringGraphType>("createCategory")
                .Argument<InputCategoryType>("category")
                .Resolve(resolve =>
                {
                    var factory = resolve.RequestServices.GetRequiredService<RepositoryFactory>();
                    var category = resolve.GetArgument<CategoryDto>("category");
                    factory.GetCategoryRepository().Add(category);
                    return "Category added successfully.";
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
                    return "State changed successfully.";
                });

            Field<StringGraphType>("deleteTask")
                .Argument<int>("TaskId")
                .Resolve(resolve =>
                {
                    var factory = resolve.RequestServices.GetRequiredService<RepositoryFactory>();
                    factory.GetTaskRepository().Delete(resolve.GetArgument<int>("TaskId"));
                    return "Task deleted successfully";
                });

            Field<StringGraphType>("deleteCategory")
                .Argument<int>("CategoryId")
                .Resolve(resolve =>
                {
                    var factory = resolve.RequestServices.GetRequiredService<RepositoryFactory>();
                    factory.GetCategoryRepository().Delete(resolve.GetArgument<int>("CategoryId"));
                    return "Category deleted successfully";
                });
        }
    }
}
