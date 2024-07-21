using Altin.Core;

namespace Altin.DataAccess;

public class TodoListRepository : BaseRepository<TodoList>, ITodoListRepository
{
    public TodoListRepository(DatabaseContext context) : base(context)
    {
    }
}
