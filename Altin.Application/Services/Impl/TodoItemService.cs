using Altin.Application.Models.TodoItem;
using Altin.Core;
using Altin.DataAccess;
using AutoMapper;

namespace Altin.Application.Services.Impl;

public class TodoItemService : ITodoItemService
{
    private readonly IMapper _mapper;
    private readonly ITodoItemRepository _todoItemRepository;
    private readonly ITodoListRepository _todoListRepository;

    public TodoItemService(IMapper mapper, ITodoItemRepository todoItemRepository,
        ITodoListRepository todoListRepository)
    {
        _mapper = mapper;
        _todoItemRepository = todoItemRepository;
        _todoListRepository = todoListRepository;
    }

    public async Task<IEnumerable<TodoItemResponseModel>> GetAllByListIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        var todoItems = await _todoItemRepository.GetAllAsync(ti => ti.List.Id == id);

        return _mapper.Map<IEnumerable<TodoItemResponseModel>>(todoItems);
    }

    public async Task<CreateTodoItemResponseModel> CreateAsync(CreateTodoItemModel createTodoItemModel,
        CancellationToken cancellationToken = default)
    {
        var todoList = await _todoListRepository.GetFirstAsync(tl => tl.Id == createTodoItemModel.TodoListId);
        var todoItem = _mapper.Map<TodoItem>(createTodoItemModel);

        todoItem.List = todoList;

        return new CreateTodoItemResponseModel
        {
            Id = (await _todoItemRepository.AddAsync(todoItem)).Id
        };
    }

    public async Task<UpdateTodoItemResponseModel> UpdateAsync(Guid id, UpdateTodoItemModel updateTodoItemModel,
        CancellationToken cancellationToken = default)
    {
        var todoItem = await _todoItemRepository.GetFirstAsync(ti => ti.Id == id);

        _mapper.Map(updateTodoItemModel, todoItem);

        return new UpdateTodoItemResponseModel
        {
            Id = (await _todoItemRepository.UpdateAsync(todoItem)).Id
        };
    }

    public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var todoItem = await _todoItemRepository.GetFirstAsync(ti => ti.Id == id);
        
        return (await _todoItemRepository.DeleteAsync(todoItem)).Id;
    }
}