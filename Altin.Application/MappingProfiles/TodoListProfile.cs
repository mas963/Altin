using Altin.Application.Models.TodoList;
using Altin.Core;
using AutoMapper;

namespace Altin.Application.MappingProfiles;

public class TodoListProfile : Profile
{
    public TodoListProfile()
    {
        CreateMap<CreateTodoListModel, TodoList>();

        CreateMap<TodoList, TodoListResponseModel>();
    }
}