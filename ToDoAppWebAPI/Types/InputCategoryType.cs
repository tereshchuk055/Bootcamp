namespace ToDoAppWebAPI.Types
{
    public class InputCategoryType : InputObjectGraphType<CategoryDto>
    {
        public InputCategoryType()
        {
            Field(x => x.Name);
        }
    }
}
