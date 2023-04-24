using ToDoApp.Models;

namespace ToDoApp.Repository
{
    public interface ICategoryRepository
    {
        List<CategoryDto> Get();

        void Add(CategoryDto category);

        void Delete(int id);
    }
}
