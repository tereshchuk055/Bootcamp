namespace ToDoAppWebAPI.Types
{
    public class InputTaskType : InputObjectGraphType<TaskDto>
    {
        public InputTaskType()
        {
            Field(x => x.CategoryId);
            Field(x => x.Name);
            Field(x => x.Deadline);
            Field(x => x.IsCompleted);
        }
    }
}
