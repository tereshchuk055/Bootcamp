using AutoMapper;
using ToDoApp.Models;
using ToDoApp.ViewModels;

namespace ToDoApp.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateTaskViewModel, TaskDto>();
            CreateMap<CreateCategoryViewModel, CategoryDto>();
        }
    }
}
