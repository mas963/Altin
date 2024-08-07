using Altin.Application.Exceptions;
using Altin.Application.Models.TodoList;
using Altin.Core;
using Altin.DataAccess;
using Altin.Shared;
using AutoMapper;

namespace Altin.Application.Services.Impl;

public class TodoListService : ITodoListService
{
    private readonly IClaimService _claimService;
    private readonly IMapper _mapper;
    private readonly ITodoListRepository _todoListRepository;

    public TodoListService(ITodoListRepository todoListRepository, IClaimService claimService, IMapper mapper)
    {
        _todoListRepository = todoListRepository;
        _claimService = claimService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TodoListResponseModel>> GetAllAsync()
    {
        var currentUserId = _claimService.GetUserId();

        var todoLists = await _todoListRepository.GetAllAsync(tl => tl.CreatedBy == currentUserId);

        return _mapper.Map<IEnumerable<TodoListResponseModel>>(todoLists);
    }

    public async Task<CreateTodoListResponseModel> CreateAsync(CreateTodoListModel createTodoListModel)
    {
        var todoList = _mapper.Map<TodoList>(createTodoListModel);

        var addedTodoList = await _todoListRepository.AddAsync(todoList);

        return new CreateTodoListResponseModel
        {
            Id = addedTodoList.Id
        };
    }

    public async Task<UpdateTodoListResponseModel> UpdateAsync(Guid id, UpdateTodoListModel updateTodoListModel)
    {
        var todoList = await _todoListRepository.GetAsync(tl => tl.Id == id);

        var userId = _claimService.GetUserId();

        if (userId != todoList.CreatedBy)
            throw new BadRequestException("The selected list does not belong to you");

        todoList.Title = updateTodoListModel.Title;

        await _todoListRepository.UpdateAsync(todoList);

        return new UpdateTodoListResponseModel
        {
            Id = id
        };
    }

    public async Task<Guid> DeleteAsync(Guid id)
    {
        var todoList = await _todoListRepository.GetAsync(tl => tl.Id == id);

        await _todoListRepository.DeleteAsync(todoList);

        return id;
    }
}