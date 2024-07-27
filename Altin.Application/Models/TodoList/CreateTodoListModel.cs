namespace Altin.Application.Models.TodoList;

public class CreateTodoListModel
{
    public string Title { get; set; }
}

public class CreateTodoListResponseModel 
{
    public Guid Id { get; set; }
}