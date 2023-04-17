using ToDoApp.Models;

namespace ToDoApp.Interfaces
{
    public interface ICategoryRepository
    {
        List<CategoryDto> Get();

        void Add(Models.CategoryDto category);

        void Delete(int id);
    }
}
