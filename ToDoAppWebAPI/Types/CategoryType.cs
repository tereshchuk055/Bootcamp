namespace ToDoAppWebAPI.Types
{
    public class CategoryType : ObjectGraphType<CategoryDto>
    {
        public CategoryType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
        }
    }
}
