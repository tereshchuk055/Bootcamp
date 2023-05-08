namespace ToDoAppWebAPI.Types
{
    public class TaskType : ObjectGraphType<TaskDto>
    {
        public TaskType()
        {
            Field(x => x.Id);
            Field(x => x.CategoryId);
            Field(x => x.Name);
            Field(x => x.Deadline);
            Field(x => x.IsCompleted);
        }
    }
}
